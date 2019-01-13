﻿namespace PressCenters.Services.Data
{
    using System.Threading.Tasks;

    public interface INewsService
    {
        Task<bool> AddAsync(RemoteNews remoteNews, int sourceId);

        Task UpdateAsync(int id, RemoteNews remoteNews);

        int Count();
    }
}
