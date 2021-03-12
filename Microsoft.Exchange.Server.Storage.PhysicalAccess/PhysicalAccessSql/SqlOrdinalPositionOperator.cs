using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000F6 RID: 246
	public class SqlOrdinalPositionOperator : OrdinalPositionOperator
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x00033876 File Offset: 0x00031A76
		internal SqlOrdinalPositionOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, SortOrder keySortOrder, StartStopKey key, bool frequentOperation) : this(connectionProvider, new OrdinalPositionOperator.OrdinalPositionOperatorDefinition(culture, (queryOperator != null) ? queryOperator.OperatorDefinition : null, keySortOrder, key, frequentOperation))
		{
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00033897 File Offset: 0x00031A97
		internal SqlOrdinalPositionOperator(IConnectionProvider connectionProvider, OrdinalPositionOperator.OrdinalPositionOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000338A4 File Offset: 0x00031AA4
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			object result;
			if (base.QueryOperator != null)
			{
				this.BuildSqlStatement();
				using (base.Connection.TrackDbOperationExecution(this))
				{
					result = this.sqlCommand.ExecuteScalar(Connection.TransactionOption.DontNeedTransaction);
					goto IL_4A;
				}
			}
			result = 0;
			IL_4A:
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0003391C File Offset: 0x00031B1C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlOrdinalPositionOperator>(this);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00033924 File Offset: 0x00031B24
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00033944 File Offset: 0x00031B44
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

		// Token: 0x06000A9C RID: 2716 RVA: 0x000339C8 File Offset: 0x00031BC8
		private void BuildSqlStatement(SqlCommand sqlCommand)
		{
			sqlCommand.Append("SELECT COUNT(*) FROM ordinalDrivingLeg WHERE (");
			for (int i = 0; i < base.Key.Count; i++)
			{
				if (i > 0)
				{
					sqlCommand.Append(" OR ");
				}
				sqlCommand.Append("(");
				for (int j = 0; j < i; j++)
				{
					SqlTableOperator.AppendKeyColumnComparison(base.Culture, sqlCommand, base.QueryOperator.ColumnsToFetch[j], SearchCriteriaCompare.SearchRelOp.Equal, base.Key.Values[j]);
					sqlCommand.Append(" AND ");
				}
				bool flag = !base.KeySortOrder.Ascending[i];
				bool flag2 = i == base.Key.Count - 1;
				bool flag3 = flag2 && !base.Key.Inclusive;
				SearchCriteriaCompare.SearchRelOp relOp = flag ? (flag3 ? SearchCriteriaCompare.SearchRelOp.GreaterThanEqual : SearchCriteriaCompare.SearchRelOp.GreaterThan) : (flag3 ? SearchCriteriaCompare.SearchRelOp.LessThanEqual : SearchCriteriaCompare.SearchRelOp.LessThan);
				SqlTableOperator.AppendKeyColumnComparison(base.Culture, sqlCommand, base.QueryOperator.ColumnsToFetch[i], relOp, base.Key.Values[i]);
				sqlCommand.Append(")");
			}
			sqlCommand.Append(")");
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00033B10 File Offset: 0x00031D10
		private void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
			ISqlSimpleQueryOperator sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.QueryOperator;
			if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
			{
				sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
				sqlCommand.Append(", ");
			}
			sqlCommand.Append("ordinalDrivingLeg AS (");
			sqlSimpleQueryOperator.BuildSqlStatement(sqlCommand, true);
			sqlCommand.Append(")");
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00033B61 File Offset: 0x00031D61
		private bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x0400036B RID: 875
		private SqlCommand sqlCommand;
	}
}
