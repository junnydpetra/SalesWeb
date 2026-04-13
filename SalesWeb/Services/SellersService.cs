using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;
using SalesWeb.Services.Exceptions;

namespace SalesWeb.Services
{
    public class SellersService
    {
        private readonly SalesWebContext _context;

        public SellersService(SalesWebContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller seller)
        {
            _context.Add(seller);
            _context.SaveChanges();
        }

        public Seller? FindById(int id)
        {
            return _context.Seller.Include(seller => seller.Department)
                                  .FirstOrDefault(seller => seller.Id == id);
        }

        public void Update(Seller seller)
        {
            if (!_context.Seller.Any(s => s.Id == seller.Id))
            {
                throw new NotFoundException("Seller not found!");
            }
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrencyException(ex.Message);
            }
        }

        public void Remove(int id)
        {
            var seller = _context.Seller.Find(id);

            if (seller != null)
            {
                _context.Seller.Remove(seller);
                _context.SaveChanges();
            }
        }
    }
}
