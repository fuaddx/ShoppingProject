﻿namespace ShoppingMvc.Models
{
	public class About: BaseEntity
	{
        public string Title { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
