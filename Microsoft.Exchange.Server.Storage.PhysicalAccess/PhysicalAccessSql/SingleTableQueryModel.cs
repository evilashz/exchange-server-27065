using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000FB RID: 251
	public class SingleTableQueryModel : SqlQueryModel
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x000342C1 File Offset: 0x000324C1
		public SingleTableQueryModel(string viewName) : this(viewName, null, SqlQueryModel.IsolationLevelHint.ReadCommitted, SqlQueryModel.PlanCacheHint.CachePlan, false)
		{
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000342CE File Offset: 0x000324CE
		public SingleTableQueryModel(string viewName, bool allowCiSearch) : this(viewName, null, SqlQueryModel.IsolationLevelHint.ReadCommitted, SqlQueryModel.PlanCacheHint.CachePlan, allowCiSearch)
		{
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000342DB File Offset: 0x000324DB
		public SingleTableQueryModel(string viewName, string tablePrefix) : this(viewName, tablePrefix, SqlQueryModel.IsolationLevelHint.ReadCommitted, SqlQueryModel.PlanCacheHint.CachePlan, false)
		{
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000342E8 File Offset: 0x000324E8
		public SingleTableQueryModel(string viewName, string tablePrefix, SqlQueryModel.IsolationLevelHint isolationLevelHint, SqlQueryModel.PlanCacheHint planCacheHint) : this(viewName, tablePrefix, isolationLevelHint, planCacheHint, false)
		{
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000342F6 File Offset: 0x000324F6
		public SingleTableQueryModel(string viewName, string tablePrefix, SqlQueryModel.IsolationLevelHint isolationLevelHint, SqlQueryModel.PlanCacheHint planCacheHint, bool allowCiSearch)
		{
			this.viewName = viewName;
			this.tablePrefix = tablePrefix;
			this.isolationLevelHint = isolationLevelHint;
			this.allowCiSearch = allowCiSearch;
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x0003431B File Offset: 0x0003251B
		public string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x00034323 File Offset: 0x00032523
		public string TablePrefix
		{
			get
			{
				return this.tablePrefix;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x0003432B File Offset: 0x0003252B
		public override string BaseTablePrefix
		{
			get
			{
				return this.TablePrefix;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00034333 File Offset: 0x00032533
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0003433B File Offset: 0x0003253B
		public SqlQueryModel.IsolationLevelHint IsolationLevel
		{
			get
			{
				return this.isolationLevelHint;
			}
			set
			{
				this.isolationLevelHint = value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00034344 File Offset: 0x00032544
		public override bool AllowCiSearch
		{
			get
			{
				return this.allowCiSearch;
			}
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0003434C File Offset: 0x0003254C
		public override void AppendFromList(SqlCommand command)
		{
			command.Append("[Exchange].[");
			command.Append(this.viewName);
			command.Append("]");
			if (this.tablePrefix != null)
			{
				command.Append(" AS ");
				command.Append(this.tablePrefix);
			}
			if (this.isolationLevelHint == SqlQueryModel.IsolationLevelHint.ReadPast)
			{
				command.Append(" WITH(READPAST)");
				return;
			}
			if (this.isolationLevelHint == SqlQueryModel.IsolationLevelHint.ReadUncommitted)
			{
				command.Append(" WITH(READUNCOMMITTED)");
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000343C4 File Offset: 0x000325C4
		public override void AppendSelectList(IList<Column> columnsToFetch, SqlCommand command)
		{
			for (int i = 0; i < columnsToFetch.Count; i++)
			{
				if (i != 0)
				{
					command.Append(", ");
				}
				Column column = columnsToFetch[i];
				command.AppendColumn(column, this, ColumnUse.FetchList);
				command.Append(" AS ");
				((ISqlColumn)column).AppendNameToQuery(command);
			}
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00034418 File Offset: 0x00032618
		public override void AppendOrderByList(CultureInfo culture, SortOrder sortOrder, bool reverse, SqlCommand command)
		{
			int count = sortOrder.Count;
			for (int i = 0; i < count; i++)
			{
				if (i != 0)
				{
					command.Append(", ");
				}
				command.AppendColumn(sortOrder[i].Column, this, ColumnUse.OrderBy);
				SqlCollationHelper.AppendCollation(sortOrder[i].Column, culture, command);
				if ((!reverse && !sortOrder[i].Ascending) || (reverse && sortOrder[i].Ascending))
				{
					command.Append(" DESC");
				}
			}
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000344B1 File Offset: 0x000326B1
		public override void AppendColumnToQuery(Column column, ColumnUse use, SqlCommand command)
		{
			((ISqlColumn)column).AppendExpressionToQuery(this, use, command);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000344C1 File Offset: 0x000326C1
		public override void AppendSimpleColumnToQuery(Column column, ColumnUse use, SqlCommand command)
		{
			if (this.tablePrefix != null)
			{
				command.Append(this.tablePrefix);
				command.Append(".");
			}
			((ISqlColumn)column).AppendNameToQuery(command);
		}

		// Token: 0x0400036D RID: 877
		private string viewName;

		// Token: 0x0400036E RID: 878
		private string tablePrefix;

		// Token: 0x0400036F RID: 879
		private SqlQueryModel.IsolationLevelHint isolationLevelHint;

		// Token: 0x04000370 RID: 880
		private bool allowCiSearch;
	}
}
