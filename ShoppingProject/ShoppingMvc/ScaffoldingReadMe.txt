Scaffolding has generated all the files and added the required dependencies.

However the Application's Startup code may require additional changes for things to work end to end.
Add the following code to the Configure method in your Application's Startup class if not already done:

        app.UseEndpoints(endpoints =>
        {
          endpoints.MapControllerRoute(
            name : "areas",
            pattern : "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
        });





         <div class="single-hero-slider single-animation-wrap">
                    <div class="container">
                        <div class="row align-items-center slider-animated-1">
                            <div class="col-lg-5 col-md-6">
                                <div class="hero-slider-content-2">
                                    <h4 class="animated">Hot promotions</h4>
                                    <h2 class="animated fw-900">Fashion Trending</h2>
                                    <h1 class="animated fw-900 text-7">Great Collection</h1>
                                    <p class="animated">Save more with coupons & up to 20% off</p>
                                    <a class="animated btn btn-brush btn-brush-2" href="shop-product-right.html"> Discover Now </a>
                                </div>
                            </div>
                            <div class="col-lg-7 col-md-6">
                                <div class="single-slider-img single-slider-img-1">
                                    <img class="animated slider-1-2" src="~/Assets/assets/imgs/slider/slider-2.png" alt="">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="single-hero-slider single-animation-wrap">
                    <div class="container">
                        <div class="row align-items-center slider-animated-1">
                            <div class="col-lg-5 col-md-6">
                                <div class="hero-slider-content-2">
                                    <h4 class="animated">Upcoming Offer</h4>
                                    <h2 class="animated fw-900">Big Deals From</h2>
                                    <h1 class="animated fw-900 text-8">Manufacturer</h1>
                                    <p class="animated">Clothing, Shoes, Bags, Wallets...</p>
                                    <a class="animated btn btn-brush btn-brush-1" href="shop-product-right.html"> Shop Now </a>
                                </div>
                            </div>
                            <div class="col-lg-7 col-md-6">
                                <div class="single-slider-img single-slider-img-1">
                                    <img class="animated slider-1-3" src="~/Assets/assets/imgs/slider/slider-3.png" alt="">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>








     @model PaginationVm<IEnumerable<ProductListItemVm>>

<div class="product-list mb-50">
    @foreach (var item in Model.Items)
    {
        @if (!item.IsDeleted && !item.IsArchived)
        {
            <div class="product-cart-wrap">
                <div class="product-img-action-wrap">
                    <div class="product-img product-img-zoom">
                        <div class="product-img-inner">
                            <a href="shop-product-right.html">
                                <img class="default-img" src="~/Assets/assets/products/@item.ImageUrl" alt="">
                                <img class="hover-img" src="~/Assets/assets/imgs/shop/product-2-2.jpg" alt="">
                            </a>
                        </div>
                    </div>
                    <div class="product-action-1">
                        <a aria-label="Quick view" class="action-btn hover-up" data-bs-toggle="modal" data-bs-target="#quickViewModal">
                            <i class="fi-rs-search"></i>
                        </a>
                        <a aria-label="Add To Wishlist" class="action-btn hover-up" href="shop-wishlist.html"><i class="fi-rs-heart"></i></a>
                        <a aria-label="Compare" class="action-btn hover-up" href="shop-compare.html"><i class="fi-rs-shuffle"></i></a>
                    </div>
                    @foreach (var tag in item.Tags)
                    {
                        <div class="product-badges product-badges-position product-badges-mrg">
                            <span class="hot">@tag.Title</span>
                        </div>
                    }

                </div>
                <div class="product-content-wrap">
                    <div class="product-category">
                        <a href="shop-grid-right.html">@item.Category.Name</a>
                    </div>
                    <h2><a href="shop-product-right.html">@item.Title</a></h2>
                    <div class="product-price">
                        <span>$@item.SellPrice </span>
                        <span class="old-price">$@item.CostPrice</span>
                    </div>
                    <p class="mt-15">@item.Description</p>
                    <div class="product-action-1 show">
                        <a asp-action="AddBasket" asp-controller="ProductDetails" asp-route-id="@item.Id" aria-label="Buy now" class="action-btn"><i class="fi-rs-shopping-bag-add"></i>Add to Cart</a>
                        <div class="rating-result" title="90%">
                            <span>
                                <span>@item.RateRange %</span>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
        }
    }

    <!--single product-->
</div>

<div class="pagination-area mt-30 mb-50">
    <nav aria-label="Page navigation example">
        <ul class="pagination justify-content-start" id="prod-pag">
            <li class="page-item @(Model.HasPrev?"":"disabled")">
                <a class="page-link" asp-action="ProductPagination" asp-route-page="@(Model.CurrentPage-1)" asp-route-count="4" style="width:43px"><</a>
            </li>
            @for (int i = 1; i <= Model.LastPage; i++)
            {
                <li class="page-item @(i==Model.CurrentPage? "active":"")">
                    <a class="page-link" asp-action="ProductPagination" asp-route-page="@i" asp-route-count="4" style="width:44px">0 @i</a>
                </li>
            }
            <li class="page-item @(Model.LastPage > 1 && Model.HasNext ? "" : "disabled")">
                <a class="page-link" asp-action="ProductPagination" asp-route-page="@(Model.CurrentPage+1)" asp-route-count="4" style="width:43px">></a>
            </li>
        </ul>
    </nav>
</div>