using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x0200010B RID: 267
	public class SqlTableFunctionOperator : TableFunctionOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000B18 RID: 2840 RVA: 0x00036A30 File Offset: 0x00034C30
		internal SqlTableFunctionOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableFunction tableFunction, object[] parameters, IList<Column> columnsToFetch, SearchCriteria restriction, IReadOnlyDictionary<Column, Column> renameDictionary, int skipTo, int maxRows, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation) : this(connectionProvider, new TableFunctionOperator.TableFunctionOperatorDefinition(culture, tableFunction, parameters, columnsToFetch, restriction, renameDictionary, skipTo, maxRows, keyRanges, backwards, frequentOperation))
		{
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00036A5D File Offset: 0x00034C5D
		internal SqlTableFunctionOperator(IConnectionProvider connectionProvider, TableFunctionOperator.TableFunctionOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00036A68 File Offset: 0x00034C68
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

		// Token: 0x06000B1B RID: 2843 RVA: 0x00036ACC File Offset: 0x00034CCC
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

		// Token: 0x06000B1C RID: 2844 RVA: 0x00036B34 File Offset: 0x00034D34
		public void BuildSqlStatement(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, true);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00036B3E File Offset: 0x00034D3E
		public void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00036B40 File Offset: 0x00034D40
		public bool NeedCteForSqlStatement()
		{
			return false;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00036B44 File Offset: 0x00034D44
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			SqlQueryModel model = new SingleTableColumnRenameQueryModel(base.Table.Name, base.RenameDictionary);
			sqlCommand.Append("SELECT ");
			if (base.MaxRows != 0)
			{
				sqlCommand.Append("TOP(");
				sqlCommand.Append((base.SkipTo + base.MaxRows).ToString());
				sqlCommand.Append(")");
			}
			this.AppendSelectList(sqlCommand, model, true, orderedResultsNeeded);
			sqlCommand.Append(" FROM [Exchange].[");
			sqlCommand.Append(base.Table.Name);
			sqlCommand.Append("](");
			for (int i = 0; i < base.Parameters.Length; i++)
			{
				if (i > 0)
				{
					sqlCommand.Append(", ");
				}
				sqlCommand.AppendParameter(base.Parameters[i]);
			}
			sqlCommand.Append(")");
			this.AppendWhereClause(sqlCommand);
			if (this.sqlCommand == sqlCommand || base.MaxRows > 0)
			{
				sqlCommand.Append(" ORDER BY ");
				this.AppendDefaultOrderByList(sqlCommand);
			}
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00036C43 File Offset: 0x00034E43
		public void AppendSelectList(SqlCommand sqlCommand, SqlQueryModel model, bool orderedResultsNeeded)
		{
			this.AppendSelectList(sqlCommand, model, false, orderedResultsNeeded);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00036C50 File Offset: 0x00034E50
		private void AppendSelectList(SqlCommand sqlCommand, SqlQueryModel model, bool baseTable, bool orderedResultsNeeded)
		{
			IList<Column> list = SqlTableOperator.RemoveDuplicateColumns(base.ColumnsToFetch);
			for (int i = 0; i < list.Count; i++)
			{
				if (i != 0)
				{
					sqlCommand.Append(", ");
				}
				Column column = list[i];
				if (base.RenameDictionary != null)
				{
					column = base.ResolveColumn(column);
				}
				((ISqlColumn)column).AppendExpressionToQuery(model, ColumnUse.FetchList, sqlCommand);
				sqlCommand.Append(" AS ");
				((ISqlColumn)list[i]).AppendNameToQuery(sqlCommand);
			}
			if (orderedResultsNeeded)
			{
				SortOrder sortOrder = base.SortOrder;
				for (int j = 0; j < sortOrder.Count; j++)
				{
					sqlCommand.Append(", ");
					if (baseTable)
					{
						model.AppendColumnToQuery(sortOrder[j].Column, ColumnUse.FetchList, sqlCommand);
						sqlCommand.Append(" AS ");
					}
					sqlCommand.Append("OB_");
					((ISqlColumn)sortOrder[j].Column).AppendNameToQuery(sqlCommand);
				}
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00036D47 File Offset: 0x00034F47
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00036D51 File Offset: 0x00034F51
		internal void AddToUpdateDelete(SqlCommand sqlCommand)
		{
			this.AppendWhereClause(sqlCommand);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00036D5A File Offset: 0x00034F5A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlTableFunctionOperator>(this);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00036D62 File Offset: 0x00034F62
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00036D7C File Offset: 0x00034F7C
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
				this.sqlCommand.Append(" ");
			}
			this.BuildSqlStatement(this.sqlCommand);
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00036E00 File Offset: 0x00035000
		private void AppendWhereClause(SqlCommand sqlCommand)
		{
			if (base.KeyRanges.Count == 0)
			{
				sqlCommand.Append(" WHERE (0 = 1) ");
				return;
			}
			if (base.Criteria == null)
			{
				StartStopKey startKey = base.KeyRanges[0].StartKey;
				if (startKey.IsEmpty)
				{
					StartStopKey stopKey = base.KeyRanges[0].StopKey;
					if (stopKey.IsEmpty)
					{
						return;
					}
				}
			}
			bool flag = false;
			sqlCommand.Append(" WHERE ");
			StartStopKey startKey2 = base.KeyRanges[0].StartKey;
			if (startKey2.IsEmpty)
			{
				StartStopKey stopKey2 = base.KeyRanges[0].StopKey;
				if (stopKey2.IsEmpty)
				{
					goto IL_C4;
				}
			}
			SqlTableOperator.AppendKeyRangeCriteria(base.Culture, base.Connection, sqlCommand, base.SortOrder, base.Backwards, base.KeyRanges);
			flag = true;
			IL_C4:
			if (base.Criteria != null)
			{
				if (flag)
				{
					sqlCommand.Append(" AND ");
				}
				sqlCommand.Append("(");
				this.AppendCriteria(sqlCommand, base.Criteria);
				sqlCommand.Append(")");
			}
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00036F0C File Offset: 0x0003510C
		private void AppendDefaultOrderByList(SqlCommand sqlCommand)
		{
			int count = base.SortOrder.Count;
			for (int i = 0; i < count; i++)
			{
				if (i != 0)
				{
					sqlCommand.Append(", ");
				}
				sqlCommand.Append(base.SortOrder.Columns[i].Name);
				SqlCollationHelper.AppendCollation(base.SortOrder.Columns[i], base.Culture, sqlCommand);
				bool flag = !base.SortOrder.Ascending[i];
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

		// Token: 0x06000B29 RID: 2857 RVA: 0x00036FC0 File Offset: 0x000351C0
		private void AppendCriteria(SqlCommand sqlCommand, SearchCriteria restriction)
		{
			SqlQueryModel model = new SingleTableColumnRenameQueryModel(base.Table.Name, base.RenameDictionary);
			ISqlSearchCriteria sqlSearchCriteria = restriction as ISqlSearchCriteria;
			sqlSearchCriteria.AppendQueryText(base.Culture, model, sqlCommand);
		}

		// Token: 0x0400037C RID: 892
		private SqlCommand sqlCommand;
	}
}
