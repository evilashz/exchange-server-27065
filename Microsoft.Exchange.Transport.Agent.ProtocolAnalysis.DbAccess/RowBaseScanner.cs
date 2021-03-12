using System;
using System.Globalization;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess
{
	// Token: 0x0200000E RID: 14
	internal class RowBaseScanner<T> : ChunkingScanner
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002AE2 File Offset: 0x00000CE2
		public RowBaseScanner(int count, NextMessage<ProtocolAnalysisRowData> nextMessageHandler)
		{
			this.typeInfo = typeof(T);
			this.paHandler = nextMessageHandler;
			this.count = count;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B13 File Offset: 0x00000D13
		public RowBaseScanner(int count, NextMessage<OpenProxyStatusRowData> nextMessageHandler)
		{
			this.typeInfo = typeof(T);
			this.opHandler = nextMessageHandler;
			this.count = count;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B44 File Offset: 0x00000D44
		public RowBaseScanner(int count, NextMessage<SenderReputationRowData> nextMessageHandler)
		{
			this.typeInfo = typeof(T);
			this.srHandler = nextMessageHandler;
			this.count = count;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B75 File Offset: 0x00000D75
		public RowBaseScanner(int count, NextMessage<ConfigurationDataRowData> nextMessageHandler)
		{
			this.typeInfo = typeof(T);
			this.confHandler = nextMessageHandler;
			this.count = count;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002BA6 File Offset: 0x00000DA6
		public RowBaseScanner()
		{
			this.typeInfo = typeof(T);
			this.count = 0;
			this.action = "truncate";
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void Scan()
		{
			if (typeof(ProtocolAnalysisRowData) == this.typeInfo)
			{
				using (DataConnection dataConnection = Database.DataSource.DemandNewConnection())
				{
					using (DataTableCursor dataTableCursor = Database.ProtocolAnalysisTable.OpenCursor(dataConnection))
					{
						using (Transaction transaction = dataTableCursor.BeginTransaction())
						{
							base.Scan(transaction, dataTableCursor, true);
							transaction.Commit();
						}
					}
					return;
				}
			}
			if (typeof(OpenProxyStatusRowData) == this.typeInfo)
			{
				using (DataConnection dataConnection2 = Database.DataSource.DemandNewConnection())
				{
					using (DataTableCursor dataTableCursor2 = Database.OpenProxyStatusTable.OpenCursor(dataConnection2))
					{
						using (Transaction transaction2 = dataTableCursor2.BeginTransaction())
						{
							base.Scan(transaction2, dataTableCursor2, true);
							transaction2.Commit();
						}
					}
					return;
				}
			}
			if (typeof(SenderReputationRowData) == this.typeInfo)
			{
				using (DataConnection dataConnection3 = Database.DataSource.DemandNewConnection())
				{
					using (DataTableCursor dataTableCursor3 = Database.SenderReputationTable.OpenCursor(dataConnection3))
					{
						using (Transaction transaction3 = dataTableCursor3.BeginTransaction())
						{
							base.Scan(transaction3, dataTableCursor3, true);
							transaction3.Commit();
						}
					}
					return;
				}
			}
			if (typeof(ConfigurationDataRowData) == this.typeInfo)
			{
				using (DataConnection dataConnection4 = Database.DataSource.DemandNewConnection())
				{
					using (DataTableCursor dataTableCursor4 = Database.ConfigurationDataTable.OpenCursor(dataConnection4))
					{
						using (Transaction transaction4 = dataTableCursor4.BeginTransaction())
						{
							base.Scan(transaction4, dataTableCursor4, true);
							transaction4.Commit();
						}
					}
					return;
				}
			}
			throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Illegal type: {0}", new object[]
			{
				this.typeInfo
			}));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E78 File Offset: 0x00001078
		protected override ChunkingScanner.ScanControl HandleRecord(DataTableCursor cursor)
		{
			if ("truncate" == this.action)
			{
				cursor.DeleteCurrentRow();
				return ChunkingScanner.ScanControl.Continue;
			}
			if (this.count > 0 && this.numScanned >= this.count)
			{
				return ChunkingScanner.ScanControl.Stop;
			}
			if (typeof(ProtocolAnalysisRowData) == this.typeInfo)
			{
				ProtocolAnalysisRowData data = DataRowAccessBase<ProtocolAnalysisTable, ProtocolAnalysisRowData>.LoadCurrentRow(cursor);
				this.paHandler(data);
			}
			else if (typeof(OpenProxyStatusRowData) == this.typeInfo)
			{
				OpenProxyStatusRowData data2 = DataRowAccessBase<OpenProxyStatusTable, OpenProxyStatusRowData>.LoadCurrentRow(cursor);
				this.opHandler(data2);
			}
			else if (typeof(SenderReputationRowData) == this.typeInfo)
			{
				SenderReputationRowData data3 = DataRowAccessBase<SenderReputationTable, SenderReputationRowData>.LoadCurrentRow(cursor);
				this.srHandler(data3);
			}
			else
			{
				if (!(typeof(ConfigurationDataRowData) == this.typeInfo))
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Illegal type: {0}", new object[]
					{
						this.typeInfo
					}));
				}
				ConfigurationDataRowData data4 = DataRowAccessBase<ConfigurationDataTable, ConfigurationDataRowData>.LoadCurrentRow(cursor);
				this.confHandler(data4);
			}
			this.numScanned++;
			return ChunkingScanner.ScanControl.Continue;
		}

		// Token: 0x0400000A RID: 10
		private Type typeInfo;

		// Token: 0x0400000B RID: 11
		private NextMessage<ProtocolAnalysisRowData> paHandler;

		// Token: 0x0400000C RID: 12
		private NextMessage<OpenProxyStatusRowData> opHandler;

		// Token: 0x0400000D RID: 13
		private NextMessage<SenderReputationRowData> srHandler;

		// Token: 0x0400000E RID: 14
		private NextMessage<ConfigurationDataRowData> confHandler;

		// Token: 0x0400000F RID: 15
		private int count;

		// Token: 0x04000010 RID: 16
		private int numScanned;

		// Token: 0x04000011 RID: 17
		private string action = "get";
	}
}
