﻿// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Ejyle.DevAccelerate.Core;
using Ejyle.DevAccelerate.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ejyle.DevAccelerate.Tasks.EF
{
    public class DaTasksDbContext
        : DaTasksDbContext<int, int?, DaTask>
    {
        public DaTasksDbContext() : base()
        { }

        public DaTasksDbContext(DbContextOptions<DaTasksDbContext> options)
            : base(options)
        { }

        public DaTasksDbContext(string connectionString)
            : base(connectionString)
        { }
    }

    public class DaTasksDbContext<TKey, TNullableKey, TTask> : DbContext
        where TKey : IEquatable<TKey>
        where TTask : DaTask<TKey, TNullableKey>
    {
        private const string SCHEMA_NAME = "Tasks";

        public DaTasksDbContext() : base()
        { }

        public DaTasksDbContext(DbContextOptions options)
            : base(options)
        { }

        public DaTasksDbContext(DbContextOptions<DaTasksDbContext<TKey, TNullableKey, TTask>> options)
            : base(options)
        { }

        public DaTasksDbContext(string connectionString)
            : base(GetOptions(connectionString))
        { }

        private static DbContextOptions<DaTasksDbContext<TKey, TNullableKey, TTask>> GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<DaTasksDbContext<TKey, TNullableKey, TTask>>(), connectionString).Options;
        }

        public virtual DbSet<TTask> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TTask>(entity =>
            {
                entity.ToTable("Tasks", SCHEMA_NAME);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.StatusReason)
                    .HasMaxLength(500);
            });
        }
    }
}
