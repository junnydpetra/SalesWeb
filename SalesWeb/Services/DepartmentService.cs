using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;

namespace SalesWeb.Services
{
    public class DepartmentService
    {
        private readonly SalesWebContext _context;

        public DepartmentService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<Models.Department>> FindAllAsync()
        {
            return await _context.Department.OrderBy(x => x.Name)
                                            .ToListAsync();
        }
    }
}
