using System;
using Microsoft.Exchange.Diagnostics.Components.ObjectModel;

namespace Microsoft.Exchange.Configuration.ObjectModel
{
	// Token: 0x02000023 RID: 35
	internal abstract class DataSourceSession : IDisposable
	{
		// Token: 0x06000147 RID: 327 RVA: 0x00005B78 File Offset: 0x00003D78
		public DataSourceSession(DataSourceInfo dataSourceInfo)
		{
			ExTraceGlobals.DataSourceSessionTracer.Information((long)this.GetHashCode(), "DataSourceSession::DataSourceSession - initializing data source session with data source info type {0}.", new object[]
			{
				(dataSourceInfo == null) ? "null" : dataSourceInfo.GetType()
			});
			this.dataSourceInfo = dataSourceInfo;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005BC4 File Offset: 0x00003DC4
		~DataSourceSession()
		{
			ExTraceGlobals.DataSourceSessionTracer.Information((long)this.GetHashCode(), "DataSourceSession::~DataSourceSession - disposing of data source session.");
			this.Dispose(false);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005C08 File Offset: 0x00003E08
		public DataSourceInfo DataSourceInfo
		{
			get
			{
				return this.dataSourceInfo;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00005C10 File Offset: 0x00003E10
		public string ConnectionString
		{
			get
			{
				return this.DataSourceInfo.ConnectionString;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005C1D File Offset: 0x00003E1D
		public virtual void Dispose()
		{
			ExTraceGlobals.DataSourceSessionTracer.Information((long)this.GetHashCode(), "DataSourceSession::Dispose - disposing of data source session.");
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005C42 File Offset: 0x00003E42
		protected virtual void Dispose(bool disposing)
		{
			ExTraceGlobals.DataSourceSessionTracer.Information((long)this.GetHashCode(), "DataSourceSession::Dispose - disposing of data source session.");
			if (!this.isDisposed)
			{
				this.isDisposed = true;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005C6B File Offset: 0x00003E6B
		public virtual bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x0400006A RID: 106
		private bool isDisposed;

		// Token: 0x0400006B RID: 107
		private DataSourceInfo dataSourceInfo;
	}
}
