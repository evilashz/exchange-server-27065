using System;
using System.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200009F RID: 159
	internal class ReportingFilterTable : IDisposable
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00011F9A File Offset: 0x0001019A
		public ReportingFilterTable()
		{
			this.dataTable = new DataTable("ReportingFilterTable");
			this.dataTable.Columns.Add(new DataColumn("ReportFilter", typeof(string)));
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00011FD8 File Offset: 0x000101D8
		~ReportingFilterTable()
		{
			this.Dispose(false);
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00012008 File Offset: 0x00010208
		public DataTable DataTable
		{
			get
			{
				this.ThrowIfDisposed();
				return this.dataTable;
			}
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00012018 File Offset: 0x00010218
		public void AddRow(string value)
		{
			this.ThrowIfDisposed();
			DataRow dataRow = this.DataTable.NewRow();
			dataRow[0] = value;
			this.DataTable.Rows.Add(dataRow);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00012050 File Offset: 0x00010250
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001205F File Offset: 0x0001025F
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.dataTable.Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001207E File Offset: 0x0001027E
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04000362 RID: 866
		private bool disposed;

		// Token: 0x04000363 RID: 867
		private DataTable dataTable;
	}
}
