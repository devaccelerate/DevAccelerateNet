﻿// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Ejyle.DevAccelerate.Core.EF;
using Ejyle.DevAccelerate.Messages;
using Microsoft.EntityFrameworkCore;
using Ejyle.DevAccelerate.Core.Data;
using System.Xml.Linq;

namespace Ejyle.DevAccelerate.Messages.EF
{
    public class DaMessageRepository : DaMessageRepository<string, DaMessage, DaMessageVariable, DaMessageRecipient, DaMessageRecipientVariable, DbContext>
    {
        public DaMessageRepository(DbContext dbContext)
            : base(dbContext)
        { }
    }


    public class DaMessageRepository<TKey, TMessage, TMessageVariable, TMessageRecipient, TMessageRecipientVariable, TDbContext>
        : DaEntityRepositoryBase<TKey, TMessage, TDbContext>, IDaMessageRepository<TKey, TMessage>
        where TKey : IEquatable<TKey>
        where TMessage : DaMessage<TKey, TMessageVariable, TMessageRecipient>
        where TMessageVariable : DaMessageVariable<TKey, TMessage>
        where TMessageRecipient : DaMessageRecipient<TKey, TMessage, TMessageRecipientVariable>
        where TMessageRecipientVariable : DaMessageRecipientVariable<TKey, TMessageRecipient>
        where TDbContext : DbContext
    {
        public DaMessageRepository(TDbContext dbContext)
            : base(dbContext)
        { }

        private DbSet<TMessage> MessagesSet { get { return DbContext.Set<TMessage>(); } }

        public IQueryable<TMessage> Messages => MessagesSet.AsQueryable();

        public Task CreateAsync(TMessage message)
        {
            MessagesSet.Add(message);
            return SaveChangesAsync();
        }

        public Task DeleteAsync(TMessage message)
        {
            MessagesSet.Remove(message);
            return SaveChangesAsync();
        }

        public Task<TMessage> FindByIdAsync(TKey id)
        {
            return MessagesSet.Where(m => m.Id.Equals(id)).SingleOrDefaultAsync();
        }

        public Task UpdateAsync(TMessage messageTemplate)
        {
            DbContext.Entry<TMessage>(messageTemplate).State = EntityState.Modified;
            return SaveChangesAsync();
        }


        public async Task<DaPaginatedEntityList<TKey, TMessage>> FindByStatusAsync(DaMessageStatus status, DaDataPaginationCriteria paginationCriteria)
        {
            var totalCount = await MessagesSet.Where(m => m.Status.Equals(status)).CountAsync();

            if (totalCount <= 0)
            {
                return null;
            }

            var query = MessagesSet
                .Where(m => m.Status.Equals(status))
                .Skip((paginationCriteria.PageIndex - 1) * paginationCriteria.PageSize)
                .Take(paginationCriteria.PageSize)
                .AsQueryable();

            var result = await query.ToListAsync();

            return new DaPaginatedEntityList<TKey, TMessage>(result
                , new DaDataPaginationResult(paginationCriteria, totalCount));
        }
    }
}
