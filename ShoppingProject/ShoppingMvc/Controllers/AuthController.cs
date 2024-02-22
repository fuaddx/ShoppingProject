using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingMvc.Contexts;
using ShoppingMvc.Helpers;
using ShoppingMvc.Models;
using ShoppingMvc.Models.Identity;
using ShoppingMvc.ViewModels.AuthVm;
using System.Security.Claims;

namespace ShoppingMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly EvaraDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, EvaraDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Policy = "NotAuthPolicy")]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [Authorize(Policy = "NotAuthPolicy")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = new AppUser()
            {
                Name = vm.Name,
                Surname = vm.Surname,
                Email = vm.Email,
                UserName = vm.Username
            };

            var result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }
            ViewBag.RegistrationSuccess = true;
            return RedirectToAction(nameof(Login));
        }
        [Authorize(Policy = "NotAuthPolicy")]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [Authorize(Policy = "NotAuthPolicy")]
        [HttpPost]
        public async Task<IActionResult> Login(string? returnUrl, LoginVm vm)
        {
            AppUser user;
			if (ModelState.IsValid)
			{
				var buser = await _userManager.FindByNameAsync(vm.UsernameorEmail);
				if (buser != null && !await _userManager.IsLockedOutAsync(buser))
				{
					var signInResult = await _signInManager.PasswordSignInAsync(buser, vm.Password, vm.IsRemember, lockoutOnFailure: false);
					if (signInResult.Succeeded)
					{
						var isSellerBanned = await _db.SellerDatas.AnyAsync(x => x.Seller.UserName == vm.UsernameorEmail && x.isBanned == true);
						if (isSellerBanned)
						{
							await _signInManager.SignOutAsync();

							return RedirectToAction("BannedPage");
						}
						return RedirectToAction("Index", "Home");
					}
				}
			}
			if (vm.UsernameorEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameorEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(vm.UsernameorEmail);
            }
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.IsRemember, true);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid Login attempt");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
                return View();
            }
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> SellerRegistration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SellerRegistration(SellerRegistrationVm vm)
        {
            string? username = Request.HttpContext.User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
                return NotFound();

            AppUser? user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            if (vm.IdentityImage != null)
            {
                if (!vm.IdentityImage.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "Wrong file type");
                }
                if (!vm.IdentityImage.IsValidSize())
                {
                    ModelState.AddModelError("ImageFile", "Files length must be less than kb");
                }
            }

            string filename = Guid.NewGuid() + Path.GetExtension(vm.IdentityImage?.FileName);

            SellerData sellerData = new SellerData()
            {
                IdentityNumber = vm.IdentityNumber,
                TaxIdentificationNumber = vm.TaxIdentificationNumber,
                ShopDescription = vm.ShopDescription,
                ShopName = vm.ShopName,
                ShopWebsite = vm.ShopWebsite,
                IsApproved = false,
                ShopCity = vm.ShopCity,
                ShopCountry = vm.ShopCountry,
                ShopState = vm.ShopState,
                ShopStreet = vm.ShopStreet,
                ShopPostalCode = vm.ShopPostalCode,
                ShopAdditionalAddressInfo = vm.ShopAdditionalAddressInfo,
                Seller = user
            };

            using (Stream fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "assets", "sellerIdentities", filename), FileMode.Create))
            {
                await vm.IdentityImage.CopyToAsync(fs);
                sellerData.IdentityImageUrl = Path.Combine("Assets", "assets", "sellerIdentities", filename);
            }

            if (vm.LogoImage != null)
            {
                if (!vm.LogoImage.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "Wrong file type");
                }
                if (!vm.LogoImage.IsValidSize())
                {
                    ModelState.AddModelError("ImageFile", "Files length must be less than kb");
                }

                string logofileName = Guid.NewGuid() + Path.GetExtension(vm.IdentityImage?.FileName);
                using (Stream fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "assets", "sellerImages", logofileName), FileMode.Create))
                {
                    await vm.LogoImage.CopyToAsync(fs);
                    sellerData.ShopLogoUrl = Path.Combine("Assets", "assets", "sellerImages", logofileName);
                }

            }

            if (vm.ThumbnailImage != null)
            {
                if (!vm.ThumbnailImage.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "Wrong file type");
                }
                if (!vm.ThumbnailImage.IsValidSize())
                {
                    ModelState.AddModelError("ImageFile", "Files length must be less than kb");
                }

                string thumbNailfileName = Guid.NewGuid() + Path.GetExtension(vm.IdentityImage?.FileName);
                using (Stream fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "assets", "sellerImages", thumbNailfileName), FileMode.Create))
                {
                    await vm.ThumbnailImage.CopyToAsync(fs);
                    sellerData.ThumbnailImageUrl = Path.Combine("Assets", "assets", "sellerImages", thumbNailfileName);
                }
            }

            await _db.SellerDatas.AddAsync(sellerData);

            await _db.SaveChangesAsync();

            return View();
        }


        [Authorize]
        public async Task<IActionResult> ProfileSettings()
        {
            var username = HttpContext.User.Identity?.Name;

            AppUser? user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null) return NotFound();

            return View(user.FromAppUser_ToProfileSettingsVm());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileSettingsVm vm)
        {
            AppUser? user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == vm.UserName);
            if (user == null) return NotFound();

            if (!ModelState.IsValid)
                return View(vm);

            user.Name = vm.Name;
            user.Surname = vm.Surname;
            user.UserName = vm.UserName;
            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;

            if (vm.CurrentPassword != null && vm.ConfirmPassword == vm.Password)
            {
                var pr = await _userManager.ChangePasswordAsync(user, vm.CurrentPassword, vm.Password);
            }
            if (vm.ProfileImageFile != null)
            {
                if (!vm.ProfileImageFile.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "Wrong file type");
                }
                if (!vm.ProfileImageFile.IsValidSize())
                {
                    ModelState.AddModelError("ImageFile", "Files length must be less than kb");
                }

                string fileName = Guid.NewGuid() + Path.GetExtension(vm.ProfileImageFile?.FileName);

                using (Stream fs = new FileStream(Path.Combine(_webHostEnvironment.WebRootPath, "Assets", "assets", "userProfiles", fileName), FileMode.Create))
                {
                    await vm.ProfileImageFile.CopyToAsync(fs);
                    user.ProfileImageUrl = Path.Combine(Path.Combine("Assets", "assets", "userProfiles", fileName));
                }
            }

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(result);

            await _db.SaveChangesAsync();

            return RedirectToAction("ProfileSettings");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveProfileImage(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            user.ProfileImageUrl = null;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            if (user.ProfileImageUrl == null)
            {
                return RedirectToAction("ProfileSettings");
            }

            return RedirectToAction("Index","Home");
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteFromData()
        {
            TempData["Delete"] = false;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            // Delete associated seller data
            var sellerData = await _db.SellerDatas.FirstOrDefaultAsync(sd => sd.UserId == user.Id);
            if (sellerData != null)
            {
                _db.SellerDatas.Remove(sellerData);
                await _db.SaveChangesAsync();
            }

            // Delete associated products
            var products = await _db.Products.Where(p => p.SellerId == user.Id).ToListAsync();
            _db.Products.RemoveRange(products);
            await _db.SaveChangesAsync();

            TempData["Delete"] = true;
            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> BannedPage()
        {
            return View();
        }
    }
}
