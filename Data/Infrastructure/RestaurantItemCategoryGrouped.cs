using System.Collections.Generic;
using System.Collections.ObjectModel;
using Data.Entities;

namespace Data.Infrastructure
{
    public class RestaurantItemCategoryGrouped : ObservableCollection<RestaurantItemCategoryOrder>
    {
        public string Name { get;  set; }

        public RestaurantItemCategoryGrouped(string name)
            : base()
        {
            Name = name;
        }

        public RestaurantItemCategoryGrouped(string name, ObservableCollection<RestaurantItemCategoryOrder> source)
            : base(source)
        {
            Name = name;
        }
    }
}