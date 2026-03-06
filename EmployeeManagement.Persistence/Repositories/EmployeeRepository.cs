using EmployeeManagement.Application.DTOs;
using EmployeeManagement.Application.Interfaces.Repositories;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Persistence.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await FindByConditionIncludes(x => x.Id == Id)
                .Include(x => x.PositionHistories)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await FindAll()
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
        {
            await Create(employee, cancellationToken);
        }

        public async Task<IEnumerable<EmployeeWithProjectsDto>> GetByDepartmentWithProjects(Guid DepartmentId, CancellationToken cancellationToken)
        {
            return await FindByConditionIncludes(e => e.DepartmentId == DepartmentId)
                .Where(e => e.EmployeeProjects.Any())
                .Select(e => new EmployeeWithProjectsDto
                {
                    Id = e.Id,
                    Name = e.Name,
                    Salary = e.Salary,
                    Position = e.CurrentPosition.ToString(),
                    Projects = e.EmployeeProjects
                        .Select(ep => ep.Project.Name)
                        .ToList()
                })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
