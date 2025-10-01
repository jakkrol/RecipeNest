using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Models
{
    [SQLite.Table("shopping_list")]
    public class ShoppingList : IEntity, INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<ShoppingItem> Items { get; set; } = new();
        
        private double _listProgress;

        [Ignore]
        public double ListProgress
        {
            get => _listProgress;
            set
            {
                if (_listProgress != value)
                {
                    _listProgress = value;
                    OnPropertyChanged(nameof(ListProgress));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
