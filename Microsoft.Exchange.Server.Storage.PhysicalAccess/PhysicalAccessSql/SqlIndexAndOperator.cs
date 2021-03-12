using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x0200010F RID: 271
	public class SqlIndexAndOperator : IndexAndOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000B46 RID: 2886 RVA: 0x000380B7 File Offset: 0x000362B7
		internal SqlIndexAndOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation) : this(connectionProvider, new IndexAndOperator.IndexAndOperatorDefinition(culture, columnsToFetch, (from op in queryOperators
		select op.OperatorDefinition).ToArray<SimpleQueryOperator.SimpleQueryOperatorDefinition>(), frequentOperation))
		{
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000380F2 File Offset: 0x000362F2
		internal SqlIndexAndOperator(IConnectionProvider connectionProvider, IndexAndOperator.IndexAndOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000380FC File Offset: 0x000362FC
		public override Reader ExecuteReader(bool disposeQueryOperator)
		{
			base.TraceOperation("ExecuteReader");
			this.BuildSqlStatement();
			Reader result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				result = this.sqlCommand.ExecuteReader(Connection.TransactionOption.DontNeedTransaction, 0, this, disposeQueryOperator);
			}
			return result;
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00038158 File Offset: 0x00036358
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

		// Token: 0x06000B4A RID: 2890 RVA: 0x000381C0 File Offset: 0x000363C0
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			new SingleTableQueryModel("IndexAndLeg");
			sqlCommand.Append("SELECT ");
			((ISqlSimpleQueryOperator)base.QueryOperators[0]).AppendSelectList(sqlCommand, SqlQueryModel.Shorthand, orderedResultsNeeded);
			sqlCommand.Append(" FROM IndexAndLeg");
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000381FC File Offset: 0x000363FC
		public void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
			foreach (ISqlSimpleQueryOperator sqlSimpleQueryOperator in base.QueryOperators)
			{
				if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
				{
					sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
					sqlCommand.Append(", ");
				}
			}
			foreach (ISqlSimpleQueryOperator sqlSimpleQueryOperator2 in base.QueryOperators)
			{
				if (sqlSimpleQueryOperator2.NeedCteForSqlStatement())
				{
					sqlSimpleQueryOperator2.BuildCteForSqlStatement(sqlCommand);
					sqlCommand.Append(", ");
				}
			}
			bool flag = true;
			sqlCommand.Append("IndexAndLeg AS (");
			foreach (ISqlSimpleQueryOperator sqlSimpleQueryOperator3 in base.QueryOperators)
			{
				if (!flag)
				{
					sqlCommand.Append(" INTERSECT ");
				}
				flag = false;
				sqlSimpleQueryOperator3.BuildSqlStatement(sqlCommand, false);
			}
			sqlCommand.Append(")");
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000382DF File Offset: 0x000364DF
		public bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000382E4 File Offset: 0x000364E4
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

		// Token: 0x06000B4E RID: 2894 RVA: 0x00038348 File Offset: 0x00036548
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00038352 File Offset: 0x00036552
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlIndexAndOperator>(this);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0003835A File Offset: 0x0003655A
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0003837C File Offset: 0x0003657C
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
			this.BuildSqlStatement(this.sqlCommand, false);
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
		}

		// Token: 0x0400037F RID: 895
		private SqlCommand sqlCommand;
	}
}
