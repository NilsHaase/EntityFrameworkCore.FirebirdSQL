﻿/*
 *          Copyright (c) 2017 Rafael Almeida (ralms@ralms.net)
 *
 *                    EntityFrameworkCore.FirebirdSql
 *
 * THIS MATERIAL IS PROVIDED AS IS, WITH ABSOLUTELY NO WARRANTY EXPRESSED
 * OR IMPLIED.  ANY USE IS AT YOUR OWN RISK.
 * 
 * Permission is hereby granted to use or copy this program
 * for any purpose,  provided the above notices are retained on all copies.
 * Permission to modify the code and to distribute modified code is granted,
 * provided the above notices are retained, and a notice that the code was
 * modified is included with the above copyright notice.
 *
 */

 using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.FirebirdSql.Extensions;
using EntityFrameworkCore.FirebirdSql;

namespace EFCore.FirebirdSql.FunctionalTests
{
	public class TestContext : DbContext
	{
		public DbSet<Author> Author { get; set; }
		public DbSet<Book> Book { get; set; }
		public DbSet<Person> Person { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{ 
			var connectionString = @"database=localhost:efCoreFirebird.fdb;user=sysdba;password=masterkey";

			optionsBuilder.UseFirebird(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelo)
		{
			base.OnModelCreating(modelo);
			var author = modelo.Entity<Author>();
			author.Property(x => x.AuthorId).UseFbIdentityColumn();

			modelo.Entity<Person>().HasKey(person => new { person.Name, person.LastName });
		}
	}
}
