using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000EE RID: 238
	public class SqlCountOperator : CountOperator
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x00031C0E File Offset: 0x0002FE0E
		internal SqlCountOperator(CultureInfo culture, IConnectionProvider connectionProvider, SimpleQueryOperator queryOperator, bool frequentOperation) : this(connectionProvider, new CountOperator.CountOperatorDefinition(culture, (queryOperator != null) ? queryOperator.OperatorDefinition : null, frequentOperation))
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00031C2B File Offset: 0x0002FE2B
		internal SqlCountOperator(IConnectionProvider connectionProvider, CountOperator.CountOperatorDefinition definition) : base(connectionProvider, definition)
		{
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00031C38 File Offset: 0x0002FE38
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			object result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				if (base.QueryOperator != null)
				{
					this.BuildSqlStatement();
					result = this.sqlCommand.ExecuteScalar(Connection.TransactionOption.DontNeedTransaction);
				}
				else
				{
					result = 0;
				}
			}
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x00031CB0 File Offset: 0x0002FEB0
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlCountOperator>(this);
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00031CB8 File Offset: 0x0002FEB8
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00031CD8 File Offset: 0x0002FED8
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

		// Token: 0x06000A42 RID: 2626 RVA: 0x00031D5B File Offset: 0x0002FF5B
		private void BuildSqlStatement(SqlCommand sqlCommand)
		{
			sqlCommand.Append("SELECT COUNT(*) FROM countDrivingLeg");
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00031D68 File Offset: 0x0002FF68
		private void BuildCteForSqlStatement(SqlCommand sqlCommand)
		{
			ISqlSimpleQueryOperator sqlSimpleQueryOperator = (ISqlSimpleQueryOperator)base.QueryOperator;
			if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
			{
				sqlSimpleQueryOperator.BuildCteForSqlStatement(sqlCommand);
				sqlCommand.Append(", ");
			}
			sqlCommand.Append("countDrivingLeg AS (");
			sqlSimpleQueryOperator.BuildSqlStatement(sqlCommand, true);
			sqlCommand.Append(")");
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00031DB9 File Offset: 0x0002FFB9
		private bool NeedCteForSqlStatement()
		{
			return true;
		}

		// Token: 0x0400035F RID: 863
		private SqlCommand sqlCommand;
	}
}
