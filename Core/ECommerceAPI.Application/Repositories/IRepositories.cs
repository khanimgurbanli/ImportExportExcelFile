using ECommerceAPI.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Application.Repositories
{
    public interface IRepositories<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
