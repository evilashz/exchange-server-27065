using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000110 RID: 272
	public class SqlIndexOrOperator : IndexOrOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x00038408 File Offset: 0x00036608
		internal SqlIndexOrOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator[] queryOperators, bool frequentOperation) : this(connectionProvider, new IndexOrOperator.IndexOrOperatorDefinition(culture, columnsToFetch, (from op in queryOperators
		select op.OperatorDefinition).ToArray<SimpleQueryOperator.SimpleQueryOperatorDefinition>(), frequentOperation))
		{
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00038443 File Offset: 0x00036643
		internal SqlIndexOrOperator(IConnectionProvider connectionProvider, IndexOrOperator.IndexOrOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00038450 File Offset: 0x00036650
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

		// Token: 0x06000B56 RID: 2902 RVA: 0x000384AC File Offset: 0x000366AC
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

		// Token: 0x06000B57 RID: 2903 RVA: 0x00038514 File Offset: 0x00036714
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			new SingleTableQueryModel("IndexOrDrivingLeg");
			sqlCommand.Append("SELECT ");
			((ISqlSimpleQueryOperator)base.QueryOperators[0]).AppendSelectList(sqlCommand, SqlQueryModel.Shorthand, false);
			sqlCommand.Append(" FROM IndexOrDrivingLeg");
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00038550 File Offset: 0x00036750
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
			bool flag = true;
			sqlCommand.Append("IndexOrDrivingLeg AS (");
			foreach (ISqlSimpleQueryOperator sqlSimpleQueryOperator2 in base.QueryOperators)
			{
				if (!flag)
				{
					sqlCommand.Append(" UNION ");
				}
				sqlSimpleQueryOperator2.BuildSqlStatement(sqlCommand, false);
				flag = false;
			}
			sqlCommand.Append(")");
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000385F0 File Offset: 0x000367F0
		public bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000385F4 File Offset: 0x000367F4
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

		// Token: 0x06000B5B RID: 2907 RVA: 0x00038658 File Offset: 0x00036858
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00038662 File Offset: 0x00036862
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlIndexOrOperator>(this);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0003866A File Offset: 0x0003686A
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0003868C File Offset: 0x0003688C
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

		// Token: 0x04000381 RID: 897
		private SqlCommand sqlCommand;
	}
}
