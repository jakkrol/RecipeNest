using RecipeNest.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RecipeNest.ViewModels
{
    [QueryProperty(nameof(ListId), "listId")]
    public class ShoppingListDetailsViewModel : INotifyPropertyChanged
    {
        private int? listId;
        public string? ListId
        {
            get => listId.ToString();
            set
            {
                listId = Convert.ToInt32(value);
                LoadListDetails();
            }
        }

        private bool isChecked { get; set; }
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                //OnPropertyChanged(nameof(isChecked));
            }
        }
        public string Name { get; set; } = "";
        public ObservableCollection<Models.ShoppingItem> Items { get; set; }



        public void LoadListDetails()
        {
            if (!listId.HasValue)
                return;

            var list = ShoppingListService.Instance.ShoppingLists.FirstOrDefault(r => r.Id == listId.Value);
            if (list != null)
            {
                Name = list.Name;
                Items = new ObservableCollection<Models.ShoppingItem>(list.Items);
                //Name = recipe.Name;
                //Category = recipe.Category;
                //Description = recipe.Description;
                //Ingredients = string.Join(", ", recipe.Ingredients);
                //Instructions = recipe.Instructions;
                //ImageUrl = recipe.ImageUrl;
            }
            //OnPropertyChanged(nameof(Name));
            //OnPropertyChanged(nameof(Category));
            //OnPropertyChanged(nameof(Description));
            //OnPropertyChanged(nameof(Ingredients));
            //OnPropertyChanged(nameof(Instructions));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Items));
        }

        public async void checkItem(Models.ShoppingItem Item)
        {
            //Debug.WriteLine("ITEMEK: " + Item.Name);


            await Services.ShoppingListService.Instance.CheckItem(Item);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
