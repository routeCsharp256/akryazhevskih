﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces
{
    /// <summary>
    /// Фабрика подключений к базе данных.
    /// </summary>
    public interface IDbConnectionFactory<TConnection> : IDisposable
    {
        /// <summary>
        /// Создать подключение к БД.
        /// </summary>
        /// <returns><see cref="TConnection"/></returns>
        Task<TConnection> CreateConnection(CancellationToken cancellationToken);
    }
}
