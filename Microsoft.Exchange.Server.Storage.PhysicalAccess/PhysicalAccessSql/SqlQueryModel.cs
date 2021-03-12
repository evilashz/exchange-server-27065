using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000DA RID: 218
	public abstract class SqlQueryModel
	{
		// Token: 0x06000973 RID: 2419
		public abstract void AppendFromList(SqlCommand command);

		// Token: 0x06000974 RID: 2420
		public abstract void AppendSelectList(IList<Column> columnsToFetch, SqlCommand command);

		// Token: 0x06000975 RID: 2421
		public abstract void AppendOrderByList(CultureInfo culture, SortOrder sortOrder, bool reverse, SqlCommand command);

		// Token: 0x06000976 RID: 2422
		public abstract void AppendColumnToQuery(Column column, ColumnUse use, SqlCommand command);

		// Token: 0x06000977 RID: 2423
		public abstract void AppendSimpleColumnToQuery(Column column, ColumnUse use, SqlCommand command);

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x0002F06E File Offset: 0x0002D26E
		public virtual bool AllowCiSearch
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0002F071 File Offset: 0x0002D271
		public virtual string BaseTablePrefix
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000336 RID: 822
		public static SqlQueryModel Shorthand = new SqlQueryModel.ShorthandQueryModel();

		// Token: 0x020000DB RID: 219
		public enum PlanCacheHint
		{
			// Token: 0x04000338 RID: 824
			CachePlan,
			// Token: 0x04000339 RID: 825
			DoNotCachePlan
		}

		// Token: 0x020000DC RID: 220
		public enum IsolationLevelHint
		{
			// Token: 0x0400033B RID: 827
			ReadCommitted,
			// Token: 0x0400033C RID: 828
			ReadPast,
			// Token: 0x0400033D RID: 829
			ReadUncommitted
		}

		// Token: 0x020000DD RID: 221
		private sealed class ShorthandQueryModel : SqlQueryModel
		{
			// Token: 0x0600097C RID: 2428 RVA: 0x0002F088 File Offset: 0x0002D288
			public override void AppendFromList(SqlCommand command)
			{
			}

			// Token: 0x0600097D RID: 2429 RVA: 0x0002F08C File Offset: 0x0002D28C
			public override void AppendSelectList(IList<Column> columnsToFetch, SqlCommand command)
			{
				for (int i = 0; i < columnsToFetch.Count; i++)
				{
					if (i != 0)
					{
						command.Append(", ");
					}
					((ISqlColumn)columnsToFetch[i]).AppendNameToQuery(command);
				}
			}

			// Token: 0x0600097E RID: 2430 RVA: 0x0002F0CC File Offset: 0x0002D2CC
			public override void AppendOrderByList(CultureInfo culture, SortOrder sortOrder, bool reverse, SqlCommand command)
			{
				int count = sortOrder.Count;
				for (int i = 0; i < count; i++)
				{
					if (i != 0)
					{
						command.Append(", ");
					}
					((ISqlColumn)sortOrder[i].Column).AppendNameToQuery(command);
					SqlCollationHelper.AppendCollation(sortOrder[i].Column, culture, command);
					if ((!reverse && !sortOrder[i].Ascending) || (reverse && sortOrder[i].Ascending))
					{
						command.Append(" DESC");
					}
				}
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x0002F16B File Offset: 0x0002D36B
			public override void AppendColumnToQuery(Column column, ColumnUse use, SqlCommand command)
			{
				this.AppendSimpleColumnToQuery(column, use, command);
			}

			// Token: 0x06000980 RID: 2432 RVA: 0x0002F176 File Offset: 0x0002D376
			public override void AppendSimpleColumnToQuery(Column column, ColumnUse use, SqlCommand command)
			{
				((ISqlColumn)column).AppendNameToQuery(command);
			}
		}
	}
}
