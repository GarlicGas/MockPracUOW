using MockPracUOW.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MockPracUOW.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        System.Threading.Tasks.Task Save(HttpContext httpContext);
        IGenericRepository<MockPracUOW.Shared.Domain.Task> Tasks { get; }
    }
}