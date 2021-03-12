using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000F0 RID: 240
	public class SqlDeleteOperator : DeleteOperator
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x00032834 File Offset: 0x00030A34
		internal SqlDeleteOperator(CultureInfo culture, IConnectionProvider connectionProvider, TableOperator tableOperator, bool frequentOperation) : base(culture, connectionProvider, tableOperator, frequentOperation)
		{
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x00032844 File Offset: 0x00030A44
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			if (base.TableOperator.Table.ReadOnly)
			{
				throw new NonFatalDatabaseException("Cannot delete from a readonly table.");
			}
			this.sqlCommand = new SqlCommand(base.Connection);
			this.sqlCommand.StartNewStatement(Connection.OperationType.Delete);
			this.sqlCommand.Append("DELETE ");
			if (base.TableOperator.MaxRows != 0)
			{
				this.sqlCommand.Append("TOP(");
				this.sqlCommand.Append(base.TableOperator.MaxRows.ToString());
				this.sqlCommand.Append(") ");
			}
			this.sqlCommand.Append("FROM [Exchange].[");
			this.sqlCommand.Append(base.TableOperator.Table.Name);
			this.sqlCommand.Append("] ");
			SqlTableOperator sqlTableOperator = base.TableOperator as SqlTableOperator;
			sqlTableOperator.AddToUpdateDelete(this.sqlCommand);
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
			object result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				result = this.sqlCommand.ExecuteNonQuery(Connection.TransactionOption.NeedTransaction);
			}
			base.TraceOperationResult("ExecuteScalar", null, result);
			return result;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000329A4 File Offset: 0x00030BA4
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x000329C3 File Offset: 0x00030BC3
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlDeleteOperator>(this);
		}

		// Token: 0x04000364 RID: 868
		private SqlCommand sqlCommand;
	}
}
