﻿namespace ShoppingMvc.Models
{
    public class Setting:BaseEntity
    {
        public string Logo { get; set; }
        public string PhoneNumber { get; set; }
        public string Hours { get; set; }
        public string Address { get; set; }
    }
}
