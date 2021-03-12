using System;
using System.Globalization;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x0200000C RID: 12
	internal class PurgingScanner<T, DTable> : ChunkingScanner
	{
		// Token: 0x06000048 RID: 72 RVA: 0x000028F7 File Offset: 0x00000AF7
		public PurgingScanner(DateTime cutoffTime)
		{
			this.cutoffTime = cutoffTime;
			this.typeInfo = typeof(T);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002918 File Offset: 0x00000B18
		public PurgingScanner()
		{
			this.cutoffTime = new DateTime(0L);
			this.typeInfo = typeof(T);
			if (this.typeInfo != typeof(SenderReputationRowData))
			{
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Illegal type: {0}. This constructor can only be used to purge SenderReputationTable", new object[]
				{
					this.typeInfo
				}));
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002988 File Offset: 0x00000B88
		public void Scan()
		{
			if (Database.DataSource == null)
			{
				return;
			}
			using (DataConnection dataConnection = Database.DataSource.DemandNewConnection())
			{
				if (dataConnection != null)
				{
					DataTable tableByType = DbAccessServices.GetTableByType(typeof(DTable));
					using (DataTableCursor dataTableCursor = tableByType.OpenCursor(dataConnection))
					{
						if (dataTableCursor != null)
						{
							using (Transaction transaction = dataTableCursor.BeginTransaction())
							{
								if (transaction != null)
								{
									base.Scan(transaction, dataTableCursor, true);
									transaction.Commit();
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A34 File Offset: 0x00000C34
		protected override ChunkingScanner.ScanControl HandleRecord(DataTableCursor cursor)
		{
			DateTime t = new DateTime(0L);
			if (typeof(ProtocolAnalysisRowData) == this.typeInfo)
			{
				DataColumn<DateTime> dataColumn = (DataColumn<DateTime>)Database.ProtocolAnalysisTable.Schemas[1];
				t = dataColumn.ReadFromCursor(cursor);
			}
			else if (typeof(OpenProxyStatusTable) == this.typeInfo)
			{
				DataColumn<DateTime> dataColumn2 = (DataColumn<DateTime>)Database.OpenProxyStatusTable.Schemas[1];
				t = dataColumn2.ReadFromCursor(cursor);
			}
			if (this.typeInfo == typeof(SenderReputationTable) || t <= this.cutoffTime)
			{
				cursor.DeleteCurrentRow();
			}
			return ChunkingScanner.ScanControl.Continue;
		}

		// Token: 0x04000008 RID: 8
		private Type typeInfo;

		// Token: 0x04000009 RID: 9
		private DateTime cutoffTime;
	}
}
