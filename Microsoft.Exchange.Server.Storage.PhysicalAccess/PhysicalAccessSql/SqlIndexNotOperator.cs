using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x02000111 RID: 273
	public class SqlIndexNotOperator : IndexNotOperator, ISqlSimpleQueryOperator
	{
		// Token: 0x06000B60 RID: 2912 RVA: 0x00038710 File Offset: 0x00036910
		internal SqlIndexNotOperator(CultureInfo culture, IConnectionProvider connectionProvider, IList<Column> columnsToFetch, SimpleQueryOperator queryOperator, SimpleQueryOperator notOperator, bool frequentOperation) : this(connectionProvider, new IndexNotOperator.IndexNotOperatorDefinition(culture, columnsToFetch, queryOperator.OperatorDefinition, notOperator.OperatorDefinition, frequentOperation))
		{
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x00038730 File Offset: 0x00036930
		internal SqlIndexNotOperator(IConnectionProvider connectionProvider, IndexNotOperator.IndexNotOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0003873C File Offset: 0x0003693C
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

		// Token: 0x06000B63 RID: 2915 RVA: 0x00038798 File Offset: 0x00036998
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

		// Token: 0x06000B64 RID: 2916 RVA: 0x00038800 File Offset: 0x00036A00
		public void BuildSqlStatement(SqlCommand sqlCommand, bool orderedResultsNeeded)
		{
			new SingleTableQueryModel("IndexNotDrivingLeg");
			sqlCommand.Append("SELECT ");
			((ISqlSimpleQueryOperator)base.QueryOperator).AppendSelectList(sqlCommand, SqlQueryModel.Shorthand, false);
			sqlCommand.Append(" FROM IndexNotDrivingLeg");
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0003883C File Offset: 0x00036A3C
		public void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
			ISqlSimpleQueryOperator sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.QueryOperator;
			if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
			{
				sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
				sqlCommand.Append(", ");
			}
			sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.NotOperator;
			if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
			{
				sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
				sqlCommand.Append(", ");
			}
			sqlCommand.Append("IndexNotDrivingLeg AS (");
			sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.QueryOperator;
			sqlSimpleQueryOperator.BuildSqlStatement(sqlCommand, false);
			sqlCommand.Append(" EXCEPT ");
			sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.NotOperator;
			sqlSimpleQueryOperator.BuildSqlStatement(sqlCommand, false);
			sqlCommand.Append(")");
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000388DE File Offset: 0x00036ADE
		public bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000388E4 File Offset: 0x00036AE4
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

		// Token: 0x06000B68 RID: 2920 RVA: 0x00038948 File Offset: 0x00036B48
		public void AddToInsert(SqlCommand sqlCommand)
		{
			this.BuildSqlStatement(sqlCommand, false);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00038952 File Offset: 0x00036B52
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlIndexNotOperator>(this);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0003895A File Offset: 0x00036B5A
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0003897C File Offset: 0x00036B7C
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

		// Token: 0x04000383 RID: 899
		private SqlCommand sqlCommand;
	}
}
