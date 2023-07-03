using Microsoft.EntityFrameworkCore;
using StoreServices.Data;
using StoreServices.Data.Entity;

namespace StoreServices
{
    public class ItemsService
    {
        private readonly StoreDbContext _storeDbContext;

        public ItemsService(StoreDbContext dbContext)
        {
            _storeDbContext = dbContext;
        }

        public List<ItemEO> GetItems()
        {
            return _storeDbContext.Items.ToList();
        }

        public List<ItemEO> GetItemsPriceGraterThen(double price)
        {
            return _storeDbContext.Items.Where(item => item.Price > price).ToList();
        }

        public ItemEO GetItem(int id)
        {
            return _storeDbContext.Items.Find(id);
        }

        public List<ItemEO> CreateItem(ItemEO item)
        {
            _storeDbContext.Items.Add(item);
            _storeDbContext.SaveChanges();
            return _storeDbContext.Items.ToList();
        }

        public ItemEO UpdateItem(ItemEO item)
        {
            var itemToUpdate = _storeDbContext.Items.Find(item.Id);
            if (itemToUpdate is null) return null;

            if (item.Name is not null)
                itemToUpdate.Name = item.Name;

            if (item.Description is not null)
                itemToUpdate.Description = item.Description;

            if (item.Price is not null)
                itemToUpdate.Price = item.Price;

            if (item.Quantity is not null)
                itemToUpdate.Quantity = item.Quantity;

            _storeDbContext.SaveChanges();
            return itemToUpdate;
        }
        public ItemEO DeleteItem(int id)
        {
            var item = _storeDbContext.Items.Find(id);
            if (item is null) return null;
            _storeDbContext.Items.Remove(item);
            _storeDbContext.SaveChanges();
            return item;
        }
    }
}