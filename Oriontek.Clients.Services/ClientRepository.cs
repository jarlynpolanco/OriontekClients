using Oriontek.Clients.Contract;
using Oriontek.Clients.Data;
using Oriontek.Clients.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace Oriontek.Clients.Services
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _dbContext;

        public ClientRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Create(Client entity)
        {
            await _dbContext.Clients.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Client entity)
        {
            _dbContext.Clients.Remove(entity);
            return await Save();
        }

        public async Task<IList<Client>> FindAll()
        {
            var clients = await _dbContext.Clients.Include(x => x.Addresses).ToListAsync();
            return clients;
        }

        public async Task<Client> FindById(int id)
        {
            var client = await _dbContext.Clients.Include(x => x.Addresses)
                .FirstOrDefaultAsync(x => x.Id == id);
            return client;
        }

        public async Task<bool> Update(Client entity)
        {
            _dbContext.Clients.Update(entity);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> IsExist(int id) => await _dbContext.Clients.AnyAsync(x => x.Id == id);
    }
}
