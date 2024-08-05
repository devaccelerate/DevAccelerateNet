// ----------------------------------------------------------------------------------------------------------------------
// Author: Tanveer Yousuf (@tanveery)
// ----------------------------------------------------------------------------------------------------------------------
// Copyright © Ejyle Technologies (P) Ltd. All rights reserved.
// Licensed under the MIT license. See the LICENSE file in the project's root directory for complete license information.
// ----------------------------------------------------------------------------------------------------------------------

using System;
using Ejyle.DevAccelerate.Platform.Applications;
using Ejyle.DevAccelerate.Platform.Features;
using Microsoft.EntityFrameworkCore;

namespace Ejyle.DevAccelerate.Platform.EF
{
    public class DaPlatformDbContext : DaPlatformDbContext<string, DaApplication, DaApplicationAttribute, DaFeature, DaFeatureAction>
    {
        public DaPlatformDbContext()
            : base()
        { }

        public DaPlatformDbContext(DbContextOptions<DaPlatformDbContext> options)
           : base(options)
        { }

        public DaPlatformDbContext(string connectionString)
            : base(connectionString)
        { }
    }

    public class DaPlatformDbContext<TKey, TApplication, TAppAttribute, TFeature, TFeatureAction> : DbContext
        where TKey : IEquatable<TKey>
        where TApplication : DaApplication<TKey, TAppAttribute, TFeature>
        where TAppAttribute : DaApplicationAttribute<TKey, TApplication>
        where TFeatureAction : DaFeatureAction<TKey, TFeature>
        where TFeature : DaFeature<TKey, TApplication, TFeatureAction>
    {
        private const string SCHEMA_NAME = "Da.Platform";

        public DaPlatformDbContext()
            : base()
        { }

        public DaPlatformDbContext(DbContextOptions options)
            : base(options)
        { }

        public DaPlatformDbContext(DbContextOptions<DaPlatformDbContext<TKey, TApplication, TAppAttribute, TFeature, TFeatureAction>> options)
            : base(options)
        { }

        public DaPlatformDbContext(string connectionString)
            : base(GetOptions(connectionString))
        { }

        private static DbContextOptions<DaPlatformDbContext<TKey, TApplication, TAppAttribute, TFeature, TFeatureAction>> GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder<DaPlatformDbContext<TKey, TApplication, TAppAttribute, TFeature, TFeatureAction>>(), connectionString).Options;
        }

        public virtual DbSet<TApplication> Applications { get; set; }
        public virtual DbSet<TAppAttribute> AppAttributes { get; set; }
        public virtual DbSet<TFeatureAction> FeatureActions { get; set; }
        public virtual DbSet<TFeature> Features { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Ejyle.DevAccelerate;Trusted_Connection = True;MultipleActiveResultSets=True";

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TAppAttribute>(entity =>
            {
                entity.ToTable("ApplicationAttributes", SCHEMA_NAME);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.AttributeName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Attributes)
                    .HasForeignKey(d => d.ApplicationId);
            });

            modelBuilder.Entity<TApplication>(entity =>
            {
                entity.ToTable("Applications", SCHEMA_NAME);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasIndex(e => e.Key)
                    .IsUnique();
            });

            modelBuilder.Entity<TFeatureAction>(entity =>
            {
                entity.ToTable("FeatureActions", SCHEMA_NAME);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Feature)
                    .WithMany(p => p.FeatureActions)
                    .HasForeignKey(d => d.FeatureId);
            });

            modelBuilder.Entity<TFeature>(entity =>
            {
                entity.ToTable("Features", SCHEMA_NAME);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasIndex(e => new { e.ApplicationId, e.Key })
                    .IsUnique();

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.Features)
                    .HasForeignKey(d => d.ApplicationId);
            });
        }
    }
}
