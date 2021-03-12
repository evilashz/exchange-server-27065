using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000F1 RID: 241
	public class SqlDistinctOperator : DistinctOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x000329CB File Offset: 0x00030BCB
		internal SqlDistinctOperator(IConnectionProvider connectionProvider, int skipTo, int maxRows, SimpleQueryOperator outerQuery, bool frequentOperation) : this(connectionProvider, new DistinctOperator.DistinctOperatorDefinition(skipTo, maxRows, outerQuery.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x000329E4 File Offset: 0x00030BE4
		internal SqlDistinctOperator(IConnectionProvider connectionProvider, DistinctOperator.DistinctOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000329F0 File Offset: 0x00030BF0
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

		// Token: 0x06000A6C RID: 2668 RVA: 0x00032A54 File Offset: 0x00030C54
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

		// Token: 0x06000A6D RID: 2669 RVA: 0x00032ABC File Offset: 0x00030CBC
		public void BuildSqlStatement(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00032AC8 File Offset: 0x00030CC8
		public void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
			ISqlSimpleQueryOperator sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.OuterQuery;
			if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
			{
				sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
				sqlCommand.Append(", ");
			}
			sqlCommand.Append("distinctDrivingLeg AS (");
			sqlSimpleQueryOperator.BuildSqlStatement(sqlCommand, false);
			sqlCommand.Append("), distinctInnerLeg AS (SELECT DISTINCT ");
			this.AppendSelectList(sqlCommand, SqlQueryModel.Shorthand, false);
			sqlCommand.Append(" FROM distinctDrivingLeg)");
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x00032B31 File Offset: 0x00030D31
		public bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x00032B34 File Offset: 0x00030D34
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			sqlCommand.Append("SELECT ");
			if (base.MaxRows != 0)
			{
				sqlCommand.Append(" TOP(");
				sqlCommand.Append((base.SkipTo + base.MaxRows).ToString());
				sqlCommand.Append(")");
			}
			this.AppendSelectList(sqlCommand, SqlQueryModel.Shorthand, false);
			sqlCommand.Append(" FROM distinctInnerLeg");
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00032BA0 File Offset: 0x00030DA0
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

		// Token: 0x06000A72 RID: 2674 RVA: 0x00032C04 File Offset: 0x00030E04
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00032C0E File Offset: 0x00030E0E
		internal void AddToUpdateDelete(SqlCommand sqlCommand)
		{
			Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "SqlDistinctOperator cannot be used as a sub-select in UPDATE or DELETE");
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x00032C1B File Offset: 0x00030E1B
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlDistinctOperator>(this);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00032C23 File Offset: 0x00030E23
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00032C44 File Offset: 0x00030E44
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
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
		}

		// Token: 0x04000365 RID: 869
		private SqlCommand sqlCommand;
	}
}
