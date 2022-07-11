using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core2_Api.Infra
{
	public static class Utility
	{
		public static void PlurizeTablesName(this ModelBuilder modelBuilder)
		{

			IEnumerable<IMutableEntityType> entityTypes = modelBuilder.Model.GetEntityTypes();
			foreach (var entityType in entityTypes)
			{
				var tablename = entityType.Relational().TableName;

				entityType.Relational().TableName = tablename + "s";
			}
		}

		///plurize name of a table
		public static void PlurizeTableName(this ModelBuilder modelBuilder, string tableName)
		{

			IMutableEntityType entityType = modelBuilder.Model.FindEntityType(tableName);

			if (entityType == null)
				throw new NullReferenceException();

			var tablename = entityType.Relational().TableName;
			entityType.Relational().TableName = tablename + "s";

		}

		///singular table name for one table
		public static void SingularTablesName(this ModelBuilder modelBuilder)
		{

			IEnumerable<IMutableEntityType> entityTypes = modelBuilder.Model.GetEntityTypes();
			foreach (var entityType in entityTypes)
			{
				var tablename = entityType.Relational().TableName;

				entityType.Relational().TableName = tablename + "s";///
			}
		}

		///for one table
		public static void SingularTableName(this ModelBuilder modelBuilder,string tableName)
		{
			IMutableEntityType entityType = modelBuilder.Model.FindEntityType(tableName);

			if (entityType == null)
				throw new NullReferenceException();

			var tablename = entityType.Relational().TableName;
			entityType.Relational().TableName = tablename + "s";
		}
	}
}
