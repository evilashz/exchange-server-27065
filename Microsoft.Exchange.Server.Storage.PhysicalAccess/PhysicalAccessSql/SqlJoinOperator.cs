using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000F3 RID: 243
	public class SqlJoinOperator : JoinOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x00032F60 File Offset: 0x00031160
		internal SqlJoinOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, IList<Column> columnsToFetch, IList<Column> longValueColumnsToPreread, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<Column> keyColumns, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new JoinOperator.JoinOperatorDefinition(culture, table, columnsToFetch, longValueColumnsToPreread, restriction, renameDictionary, skipTo, maxRows, keyColumns, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00032F92 File Offset: 0x00031192
		internal SqlJoinOperator(IConnectionProvider connectionProvider, JoinOperator.JoinOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00032F9C File Offset: 0x0003119C
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			this.BuildSqlStatement();
			Reader result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				result = this.sqlCommand.ExecuteReader(Connection.TransactionOption.DontNeedTransaction, base.SkipTo, this, disposeQueryOperator);
			}
			return result;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00033000 File Offset: 0x00031200
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			this.BuildSqlStatement();
			object result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				result = this.sqlCommand.ExecuteScalar(Connection.TransactionOption.DontNeedTransaction);
			}
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00033068 File Offset: 0x00031268
		public void BuildSqlStatement(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, !base.SortOrder.IsEmpty);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00033090 File Offset: 0x00031290
		public void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
			ISqlSimpleQueryOperator sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.OuterQuery;
			if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
			{
				sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
				sqlCommand.Append(", ");
			}
			SqlQueryModel model = new SqlJoinOperator.JoinQueryModel(this, "bt", "ol");
			string str = (base.OuterQuery is TableOperator) ? string.Empty : "2";
			sqlCommand.Append("drivingLeg");
			sqlCommand.Append(str);
			sqlCommand.Append(" AS (");
			sqlSimpleQueryOperator.BuildSqlStatement(sqlCommand, !base.SortOrder.IsEmpty);
			sqlCommand.Append("), innerLeg");
			sqlCommand.Append(str);
			sqlCommand.Append(" AS (SELECT ");
			this.AppendSelectList(sqlCommand, model, !base.SortOrder.IsEmpty);
			sqlCommand.Append(" FROM ");
			sqlCommand.AppendFromList(model);
			sqlCommand.Append(" ON ");
			for (int i = 0; i < base.KeyColumns.Count; i++)
			{
				if (i > 0)
				{
					sqlCommand.Append(" AND ");
				}
				bool flag = base.KeyColumns[i].IsNullable && base.OuterQuery.ColumnsToFetch[i].IsNullable;
				if (flag)
				{
					sqlCommand.Append(" ((");
					sqlCommand.Append("bt.");
					((ISqlColumn)base.KeyColumns[i]).AppendNameToQuery(sqlCommand);
					sqlCommand.Append(" IS NULL AND ");
					sqlCommand.Append("ol.");
					((ISqlColumn)base.OuterQuery.ColumnsToFetch[i]).AppendNameToQuery(sqlCommand);
					sqlCommand.Append(" IS NULL) OR ");
				}
				sqlCommand.Append("bt.");
				((ISqlColumn)base.KeyColumns[i]).AppendNameToQuery(sqlCommand);
				sqlCommand.Append(" = ");
				sqlCommand.Append("ol.");
				((ISqlColumn)base.OuterQuery.ColumnsToFetch[i]).AppendNameToQuery(sqlCommand);
				SqlCollationHelper.AppendCollation(base.KeyColumns[i], base.Culture, sqlCommand);
				if (flag)
				{
					sqlCommand.Append(")");
				}
			}
			this.AppendWhereClause(sqlCommand, model);
			sqlCommand.Append(")");
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x000332CD File Offset: 0x000314CD
		public bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000332D0 File Offset: 0x000314D0
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			string str = (base.OuterQuery is TableOperator) ? string.Empty : "2";
			sqlCommand.Append("SELECT ");
			if (base.MaxRows != 0)
			{
				sqlCommand.Append(" TOP(");
				sqlCommand.Append((base.SkipTo + base.MaxRows).ToString());
				sqlCommand.Append(")");
			}
			this.AppendSelectList(sqlCommand, SqlQueryModel.Shorthand, orderedResultsNeeded);
			sqlCommand.Append(" FROM innerLeg");
			sqlCommand.Append(str);
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0003335C File Offset: 0x0003155C
		public void AppendSelectList(SqlCommand sqlCommand, SqlQueryModel model, bool orderedResultsNeeded)
		{
			IList<Column> list = SqlTableOperator.RemoveDuplicateColumns(base.ColumnsToFetch);
			for (int i = 0; i < list.Count; i++)
			{
				if (i != 0)
				{
					sqlCommand.Append(", ");
				}
				model.AppendColumnToQuery(list[i], ColumnUse.FetchList, sqlCommand);
				sqlCommand.Append(" AS ");
				((ISqlColumn)list[i]).AppendNameToQuery(sqlCommand);
			}
			if (orderedResultsNeeded)
			{
				SortOrder sortOrder = base.OuterQuery.SortOrder;
				for (int j = 0; j < sortOrder.Count; j++)
				{
					sqlCommand.Append(", OB_");
					((ISqlColumn)sortOrder[j].Column).AppendNameToQuery(sqlCommand);
				}
			}
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00033408 File Offset: 0x00031608
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00033412 File Offset: 0x00031612
		internal void AddToUpdateDelete(SqlCommand sqlCommand)
		{
			this.AppendWhereClause(sqlCommand, SqlQueryModel.Shorthand);
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x00033420 File Offset: 0x00031620
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlJoinOperator>(this);
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00033428 File Offset: 0x00031628
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x00033448 File Offset: 0x00031648
		private void BuildSqlStatement()
		{
			if (this.sqlCommand == null)
			{
				this.sqlCommand = new SqlCommand(base.Connection);
			}
			this.sqlCommand.StartNewStatement(Connection.OperationType.Query);
			if (this.NeedCteForSqlStatement())
			{
				this.sqlCommand.Append("WITH ");
				this.BuildCteForSqlStatement(this.sqlCommand);
			}
			this.BuildSqlStatement(this.sqlCommand);
			this.AppendDefaultOrderByList(this.sqlCommand);
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000334C7 File Offset: 0x000316C7
		private void AppendWhereClause(SqlCommand sqlCommand, SqlQueryModel model)
		{
			if (base.Criteria != null)
			{
				sqlCommand.Append(" WHERE ");
				sqlCommand.Append("(");
				this.AppendCriteria(sqlCommand, model, base.Criteria);
				sqlCommand.Append(")");
			}
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x00033500 File Offset: 0x00031700
		private void AppendDefaultOrderByList(SqlCommand sqlCommand)
		{
			if (!base.SortOrder.IsEmpty)
			{
				sqlCommand.Append(" ORDER BY ");
				SortOrder sortOrder = base.OuterQuery.SortOrder;
				int count = sortOrder.Count;
				for (int i = 0; i < count; i++)
				{
					if (i != 0)
					{
						sqlCommand.Append(", ");
					}
					sqlCommand.Append("OB_");
					sqlCommand.Append(sortOrder[i].Column.Name);
					SqlCollationHelper.AppendCollation(sortOrder[i].Column, base.Culture, sqlCommand);
					bool flag = !sortOrder[i].Ascending;
					if (base.Backwards)
					{
						flag = !flag;
					}
					if (flag)
					{
						sqlCommand.Append(" DESC");
					}
				}
			}
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x000335D4 File Offset: 0x000317D4
		private void AppendCriteria(SqlCommand sqlCommand, SqlQueryModel model, SearchCriteria restriction)
		{
			ISqlSearchCriteria sqlSearchCriteria = (ISqlSearchCriteria)restriction;
			sqlSearchCriteria.AppendQueryText(base.Culture, model, sqlCommand);
		}

		// Token: 0x04000367 RID: 871
		private SqlCommand sqlCommand;

		// Token: 0x020000F4 RID: 244
		private class JoinQueryModel : SqlQueryModel
		{
			// Token: 0x06000A8C RID: 2700 RVA: 0x000335F6 File Offset: 0x000317F6
			public JoinQueryModel(SqlJoinOperator joinOperator, string baseTablePrefix, string outerLegPrefix)
			{
				this.joinOperator = joinOperator;
				this.baseTablePrefix = baseTablePrefix;
				this.outerLegPrefix = outerLegPrefix;
			}

			// Token: 0x06000A8D RID: 2701 RVA: 0x00033614 File Offset: 0x00031814
			public override void AppendFromList(SqlCommand command)
			{
				string str = (this.joinOperator.OuterQuery is TableOperator) ? string.Empty : "2";
				command.Append("drivingLeg");
				command.Append(str);
				command.Append(" AS ");
				command.Append(this.outerLegPrefix);
				command.Append(" JOIN [Exchange].[");
				command.Append(this.joinOperator.Table.Name);
				command.Append("] AS ");
				command.Append(this.baseTablePrefix);
			}

			// Token: 0x06000A8E RID: 2702 RVA: 0x000336A4 File Offset: 0x000318A4
			public override void AppendColumnToQuery(Column column, ColumnUse use, SqlCommand command)
			{
				if (this.joinOperator.OuterQuery.ColumnsToFetch.Contains(column))
				{
					command.Append(this.outerLegPrefix);
					command.Append(".");
					((ISqlColumn)column).AppendNameToQuery(command);
					return;
				}
				((ISqlColumn)column).AppendExpressionToQuery(this, use, command);
			}

			// Token: 0x06000A8F RID: 2703 RVA: 0x000336FC File Offset: 0x000318FC
			public override void AppendSelectList(IList<Column> columnsToFetch, SqlCommand command)
			{
				for (int i = 0; i < columnsToFetch.Count; i++)
				{
					if (i != 0)
					{
						command.Append(", ");
					}
					this.AppendSimpleColumnToQuery(columnsToFetch[i], ColumnUse.FetchList, command);
				}
			}

			// Token: 0x06000A90 RID: 2704 RVA: 0x00033738 File Offset: 0x00031938
			public override void AppendOrderByList(CultureInfo culture, SortOrder sortOrder, bool reverse, SqlCommand command)
			{
				int count = sortOrder.Count;
				for (int i = 0; i < count; i++)
				{
					if (i != 0)
					{
						command.Append(", ");
					}
					this.AppendSimpleColumnToQuery(sortOrder[i].Column, ColumnUse.OrderBy, command);
					SqlCollationHelper.AppendCollation(sortOrder[i].Column, culture, command);
					if ((!reverse && !sortOrder[i].Ascending) || (reverse && sortOrder[i].Ascending))
					{
						command.Append(" DESC");
					}
				}
			}

			// Token: 0x06000A91 RID: 2705 RVA: 0x000337D4 File Offset: 0x000319D4
			public override void AppendSimpleColumnToQuery(Column column, ColumnUse use, SqlCommand command)
			{
				if (this.joinOperator.OuterQuery.ColumnsToFetch.Contains(column))
				{
					command.Append(this.outerLegPrefix);
					command.Append(".");
				}
				else
				{
					command.Append(this.baseTablePrefix);
					command.Append(".");
				}
				((ISqlColumn)column).AppendNameToQuery(command);
			}

			// Token: 0x04000368 RID: 872
			private SqlJoinOperator joinOperator;

			// Token: 0x04000369 RID: 873
			private string baseTablePrefix;

			// Token: 0x0400036A RID: 874
			private string outerLegPrefix;
		}
	}
}
