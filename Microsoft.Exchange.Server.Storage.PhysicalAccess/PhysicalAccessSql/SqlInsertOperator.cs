using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessSql
{
	// Token: 0x020000F2 RID: 242
	public class SqlInsertOperator : InsertOperator
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x00032CB8 File Offset: 0x00030EB8
		internal SqlInsertOperator(CultureInfo culture, IConnectionProvider connectionProvider, Table table, SimpleQueryOperator simpleQueryOperator, IList<Column> columnsToInsert, IList<object> valuesToInsert, Column columnToFetch, bool frequentOperation) : base(culture, connectionProvider, table, simpleQueryOperator, columnsToInsert, valuesToInsert, columnToFetch, frequentOperation)
		{
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x00032CD8 File Offset: 0x00030ED8
		public override object ExecuteScalar()
		{
			base.TraceOperation("ExecuteScalar");
			if (base.Table.ReadOnly)
			{
				throw new NonFatalDatabaseException("Cannot insert into a readonly table.");
			}
			this.sqlCommand = new SqlCommand(base.Connection);
			this.sqlCommand.StartNewStatement(Connection.OperationType.Insert);
			ISqlSimpleQueryOperator sqlSimpleQueryOperator = null;
			if (base.SimpleQueryOperator != null)
			{
				sqlSimpleQueryOperator = (base.SimpleQueryOperator as ISqlSimpleQueryOperator);
				if (sqlSimpleQueryOperator.NeedCteForSqlStatement())
				{
					this.sqlCommand.Append("WITH ");
					sqlSimpleQueryOperator.BuildCteForSqlStatement(this.sqlCommand);
					this.sqlCommand.Append(" ");
				}
			}
			this.sqlCommand.Append("INSERT INTO [Exchange].[");
			this.sqlCommand.Append(base.Table.Name);
			this.sqlCommand.Append("] (");
			for (int i = 0; i < base.ColumnsToInsert.Count; i++)
			{
				if (i != 0)
				{
					this.sqlCommand.Append(", ");
				}
				this.sqlCommand.Append(base.ColumnsToInsert[i].Name);
			}
			this.sqlCommand.Append(") ");
			if (base.ColumnToFetch != null)
			{
				this.sqlCommand.Append(" OUTPUT INSERTED.");
				this.sqlCommand.Append(base.ColumnToFetch.Name);
				this.sqlCommand.Append(" ");
			}
			if (base.SimpleQueryOperator != null)
			{
				sqlSimpleQueryOperator.AddToInsert(this.sqlCommand);
			}
			else
			{
				this.sqlCommand.Append("VALUES (");
				for (int j = 0; j < base.ColumnsToInsert.Count; j++)
				{
					if (j != 0)
					{
						this.sqlCommand.Append(", ");
					}
					this.sqlCommand.AppendParameter(base.ValuesToInsert[j]);
				}
				this.sqlCommand.Append(") ");
			}
			this.sqlCommand.AppendQueryHints(base.FrequentOperation);
			object result;
			using (base.Connection.TrackDbOperationExecution(this))
			{
				if (base.ColumnToFetch != null)
				{
					result = this.sqlCommand.ExecuteScalar(Connection.TransactionOption.NeedTransaction);
				}
				else
				{
					result = this.sqlCommand.ExecuteNonQuery(Connection.TransactionOption.NeedTransaction);
				}
			}
			base.TraceOperationResult("ExecuteScalar", base.ColumnToFetch, result);
			return result;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00032F38 File Offset: 0x00031138
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.sqlCommand != null)
			{
				this.sqlCommand.Dispose();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00032F57 File Offset: 0x00031157
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SqlInsertOperator>(this);
		}

		// Token: 0x04000366 RID: 870
		private SqlCommand sqlCommand;
	}
}
