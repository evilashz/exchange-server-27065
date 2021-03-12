using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000108 RID: 264
	public class SqlSortOperator : SortOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000AFD RID: 2813 RVA: 0x00035FF8 File Offset: 0x000341F8
		internal SqlSortOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, int skipTo, int maxRows, SortOrder sortOrder, IList<KeyRange> keyRanges, bool backwards, bool frequentOperation) : this(connectionProvider, new SortOperator.SortOperatorDefinition(culture, queryOperator.OperatorDefinition, skipTo, maxRows, sortOrder, keyRanges, backwards, frequentOperation))
		{
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x00036024 File Offset: 0x00034224
		internal SqlSortOperator(IConnectionProvider connectionProvider, SortOperator.SortOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x00036030 File Offset: 0x00034230
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

		// Token: 0x06000B00 RID: 2816 RVA: 0x00036094 File Offset: 0x00034294
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

		// Token: 0x06000B01 RID: 2817 RVA: 0x000360FC File Offset: 0x000342FC
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			SqlQueryModel model = new SingleTableQueryModel("sortDrivingLeg");
			sqlCommand.Append("SELECT ");
			if (base.MaxRows != 0)
			{
				sqlCommand.Append(" TOP(");
				sqlCommand.Append((base.SkipTo + base.MaxRows).ToString());
				sqlCommand.Append(")");
			}
			this.AppendSelectList(sqlCommand, SqlQueryModel.Shorthand, true);
			sqlCommand.Append(" FROM sortDrivingLeg");
			this.AppendWhereClause(sqlCommand, model);
			if (this.sqlCommand == sqlCommand || base.MaxRows > 0)
			{
				sqlCommand.Append(" ORDER BY ");
				this.AppendOrderByList(sqlCommand);
			}
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0003619C File Offset: 0x0003439C
		public void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
			ISqlSimpleQueryOperator sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.QueryOperator;
			if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
			{
				sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
				sqlCommand.Append(", ");
			}
			sqlCommand.Append("sortDrivingLeg AS (");
			sqlSimpleQueryOperator.BuildSqlStatement(sqlCommand, true);
			sqlCommand.Append(")");
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000361ED File Offset: 0x000343ED
		public bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000361F0 File Offset: 0x000343F0
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
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00036254 File Offset: 0x00034454
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0003625E File Offset: 0x0003445E
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlSortOperator>(this);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00036266 File Offset: 0x00034466
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00036288 File Offset: 0x00034488
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
			this.BuildSqlStatement(this.sqlCommand, true);
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0003630C File Offset: 0x0003450C
		private void AppendWhereClause(SqlCommand sqlCommand, SqlQueryModel model)
		{
			if (base.KeyRanges.Count == 0)
			{
				sqlCommand.Append(" WHERE (0 = 1) ");
				return;
			}
			StartStopKey startKey = base.KeyRanges[0].StartKey;
			if (startKey.IsEmpty)
			{
				StartStopKey stopKey = base.KeyRanges[0].StopKey;
				if (stopKey.IsEmpty)
				{
					return;
				}
			}
			sqlCommand.Append(" WHERE ");
			SqlTableOperator.AppendKeyRangeCriteria(base.Culture, base.Connection, sqlCommand, base.SortOrder, base.Backwards, base.KeyRanges);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00036398 File Offset: 0x00034598
		private void AppendOrderByList(SqlCommand sqlCommand)
		{
			int count = base.SortOrder.Count;
			for (int i = 0; i < count; i++)
			{
				if (i != 0)
				{
					sqlCommand.Append(", ");
				}
				sqlCommand.Append(base.SortOrder[i].Column.Name);
				SqlCollationHelper.AppendCollation(base.SortOrder[i].Column, base.Culture, sqlCommand);
				bool flag = !base.SortOrder[i].Ascending;
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

		// Token: 0x0400037A RID: 890
		private SqlCommand sqlCommand;
	}
}
