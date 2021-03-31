using Oriontek.Clients.Contracts;
using Oriontek.Clients.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Oriontek.Clients.Data.Models;
using System.Threading.Tasks;

namespace Oriontek.Clients.Services
{
    public class AddressRepository : IAddressRepository
    {

        private readonly AppDbContext _dbContext;

        public AddressRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(Address entity)
        {
            await _dbContext.Addresses.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Address entity)
        {
            _dbContext.Addresses.Remove(entity);
            return await Save();
        }

        public async Task<IList<Address>> FindAll()
        {
            var address = await _dbContext.Addresses.Include(x => x.Client).ToListAsync();
            return address;
        }

        public async Task<Address> FindById(int id)
        {
            var address = await _dbContext.Addresses.Include(x => x.Client)
                .FirstOrDefaultAsync(x => x.Id == id);
            return address;
        }

        public async Task<bool> Update(Address entity)
        {
            _dbContext.Addresses.Update(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> IsExist(int id) => await _dbContext.Addresses.AnyAsync(x => x.Id == id);
    }
}
