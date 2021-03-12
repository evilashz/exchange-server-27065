using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Exchange.Diagnostics.Service.Common;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000013 RID: 19
	public class ManagedConnection : IDisposable
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00006422 File Offset: 0x00004622
		public ManagedConnection(SqlConnection connection, int tier)
		{
			this.connection = connection;
			this.buffers = new Dictionary<string, SqlOutputStream.PerformanceDataTable>(StringComparer.OrdinalIgnoreCase);
			this.flusherExecuted = false;
			this.tier = tier;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000644F File Offset: 0x0000464F
		public Dictionary<string, SqlOutputStream.PerformanceDataTable> Buffers
		{
			get
			{
				return this.buffers;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00006457 File Offset: 0x00004657
		public SqlConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000645F File Offset: 0x0000465F
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00006467 File Offset: 0x00004667
		public bool FlusherExecuted
		{
			get
			{
				return this.flusherExecuted;
			}
			set
			{
				this.flusherExecuted = value;
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00006470 File Offset: 0x00004670
		public void Dispose()
		{
			this.InternalDispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00006480 File Offset: 0x00004680
		public SqlOutputStream.PerformanceDataTable GetBuffer(string machineName)
		{
			SqlOutputStream.PerformanceDataTable performanceDataTable;
			if (!this.buffers.TryGetValue(machineName, out performanceDataTable))
			{
				performanceDataTable = new SqlOutputStream.PerformanceDataTable(this.tier, machineName);
				this.buffers.Add(machineName, performanceDataTable);
			}
			return performanceDataTable;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000064B8 File Offset: 0x000046B8
		protected void InternalDispose(bool disposing)
		{
			if (!this.disposed)
			{
				Logger.LogInformationMessage("ManagedConnection: Disposing SQL Connection.", new object[0]);
				if (disposing)
				{
					if (this.connection != null)
					{
						this.connection.Close();
						this.connection.Dispose();
					}
					if (this.buffers != null)
					{
						foreach (DataTable dataTable in this.buffers.Values)
						{
							dataTable.Dispose();
						}
					}
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000050 RID: 80
		private readonly Dictionary<string, SqlOutputStream.PerformanceDataTable> buffers;

		// Token: 0x04000051 RID: 81
		private readonly int tier;

		// Token: 0x04000052 RID: 82
		private readonly SqlConnection connection;

		// Token: 0x04000053 RID: 83
		private bool flusherExecuted;

		// Token: 0x04000054 RID: 84
		private volatile bool disposed;
	}
}
