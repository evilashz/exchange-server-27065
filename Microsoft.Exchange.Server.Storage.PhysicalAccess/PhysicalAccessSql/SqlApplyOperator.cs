using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000D9 RID: 217
	public class SqlApplyOperator : ApplyOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000963 RID: 2403 RVA: 0x0002EBDC File Offset: 0x0002CDDC
		internal SqlApplyOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, IList<Column> tableFunctionParameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new ApplyOperator.ApplyOperatorDefinition(culture, tableFunction, tableFunctionParameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002EC0C File Offset: 0x0002CE0C
		internal SqlApplyOperator(IConnectionProvider connectionProvider, ApplyOperator.ApplyOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002EC18 File Offset: 0x0002CE18
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

		// Token: 0x06000966 RID: 2406 RVA: 0x0002EC7C File Offset: 0x0002CE7C
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

		// Token: 0x06000967 RID: 2407 RVA: 0x0002ECE4 File Offset: 0x0002CEE4
		public void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
			ISqlSimpleQueryOperator sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.OuterQuery;
			if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
			{
				sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
				sqlCommand.Append(", ");
			}
			SqlQueryModel model = new SqlApplyOperator.ApplyQueryModel(this, "bt", "ol");
			sqlCommand.Append("applyDrivingLeg AS (");
			sqlSimpleQueryOperator.BuildSqlStatement(sqlCommand, true);
			sqlCommand.Append("), applyInnerLeg AS (SELECT ");
			this.AppendSelectList(sqlCommand, model, true);
			sqlCommand.Append(" FROM ");
			sqlCommand.AppendFromList(model);
			this.AppendWhereClause(sqlCommand, model);
			sqlCommand.Append(") ");
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002ED74 File Offset: 0x0002CF74
		public bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002ED78 File Offset: 0x0002CF78
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			sqlCommand.Append("SELECT ");
			if (base.MaxRows != 0)
			{
				sqlCommand.Append(" TOP(");
				sqlCommand.Append((base.SkipTo + base.MaxRows).ToString());
				sqlCommand.Append(")");
			}
			this.AppendSelectList(sqlCommand, SqlQueryModel.Shorthand, orderedResultsNeeded);
			sqlCommand.Append(" FROM applyInnerLeg");
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002EDE4 File Offset: 0x0002CFE4
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

		// Token: 0x0600096B RID: 2411 RVA: 0x0002EE90 File Offset: 0x0002D090
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002EE9A File Offset: 0x0002D09A
		internal void AddToUpdateDelete(SqlCommand sqlCommand)
		{
			this.AppendWhereClause(sqlCommand, SqlQueryModel.Shorthand);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002EEA8 File Offset: 0x0002D0A8
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlApplyOperator>(this);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x0002EEB0 File Offset: 0x0002D0B0
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x0002EED0 File Offset: 0x0002D0D0
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
			this.BuildSqlStatement(this.sqlCommand, true);
			this.AppendDefaultOrderByList(this.sqlCommand);
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002EF50 File Offset: 0x0002D150
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

		// Token: 0x06000971 RID: 2417 RVA: 0x0002EF8C File Offset: 0x0002D18C
		private void AppendDefaultOrderByList(SqlCommand sqlCommand)
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

		// Token: 0x06000972 RID: 2418 RVA: 0x0002F04C File Offset: 0x0002D24C
		private void AppendCriteria(SqlCommand sqlCommand, SqlQueryModel model, SearchCriteria restriction)
		{
			ISqlSearchCriteria sqlSearchCriteria = (ISqlSearchCriteria)restriction;
			sqlSearchCriteria.AppendQueryText(base.Culture, model, sqlCommand);
		}

		// Token: 0x04000335 RID: 821
		private SqlCommand sqlCommand;

		// Token: 0x020000DE RID: 222
		private class ApplyQueryModel : SqlQueryModel
		{
			// Token: 0x06000982 RID: 2434 RVA: 0x0002F18C File Offset: 0x0002D38C
			public ApplyQueryModel(SqlApplyOperator applyOperator, string baseTablePrefix, string outerLegPrefix)
			{
				this.applyOperator = applyOperator;
				this.baseTablePrefix = baseTablePrefix;
				this.outerLegPrefix = outerLegPrefix;
			}

			// Token: 0x06000983 RID: 2435 RVA: 0x0002F1AC File Offset: 0x0002D3AC
			public override void AppendFromList(SqlCommand command)
			{
				command.Append("applyDrivingLeg AS ");
				command.Append(this.outerLegPrefix);
				command.Append(" CROSS APPLY [Exchange].[");
				command.Append(this.applyOperator.TableFunction.Name);
				command.Append("](");
				for (int i = 0; i < this.applyOperator.TableFunctionParameters.Count; i++)
				{
					if (i > 0)
					{
						command.Append(", ");
					}
					command.Append(this.outerLegPrefix);
					command.Append(".");
					command.Append(this.applyOperator.TableFunctionParameters[i].Name);
				}
				command.Append(") AS ");
				command.Append(this.baseTablePrefix);
			}

			// Token: 0x06000984 RID: 2436 RVA: 0x0002F270 File Offset: 0x0002D470
			public override void AppendColumnToQuery(Column column, ColumnUse use, SqlCommand command)
			{
				if (this.applyOperator.RenameDictionary != null)
				{
					column = this.applyOperator.ResolveColumn(column);
				}
				if (this.applyOperator.OuterQuery.ColumnsToFetch.Contains(column))
				{
					command.Append(this.outerLegPrefix);
					command.Append(".");
					((ISqlColumn)column).AppendNameToQuery(command);
					return;
				}
				((ISqlColumn)column).AppendExpressionToQuery(this, use, command);
			}

			// Token: 0x06000985 RID: 2437 RVA: 0x0002F2E4 File Offset: 0x0002D4E4
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

			// Token: 0x06000986 RID: 2438 RVA: 0x0002F320 File Offset: 0x0002D520
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
					if ((!reverse && !sortOrder[i].Ascending) || (reverse && sortOrder[i].Ascending))
					{
						command.Append(" DESC");
					}
				}
			}

			// Token: 0x06000987 RID: 2439 RVA: 0x0002F3A0 File Offset: 0x0002D5A0
			public override void AppendSimpleColumnToQuery(Column column, ColumnUse use, SqlCommand command)
			{
				if (this.applyOperator.RenameDictionary != null)
				{
					column = this.applyOperator.ResolveColumn(column);
				}
				if (this.applyOperator.OuterQuery.ColumnsToFetch.Contains(column))
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

			// Token: 0x0400033E RID: 830
			private SqlApplyOperator applyOperator;

			// Token: 0x0400033F RID: 831
			private string baseTablePrefix;

			// Token: 0x04000340 RID: 832
			private string outerLegPrefix;
		}
	}
}
