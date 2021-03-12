using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Mapi
{
	// Token: 0x02000038 RID: 56
	internal class ConnectionPool : IDisposeTrackable, IDisposable
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000CA10 File Offset: 0x0000AC10
		private ConnectionPool()
		{
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000CA24 File Offset: 0x0000AC24
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ConnectionPool>(this);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CA41 File Offset: 0x0000AC41
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000CA50 File Offset: 0x0000AC50
		public ExRpcAdmin GetAdministration(string server)
		{
			return ExRpcAdmin.Create("Client=Management", server, null, null, null);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public void ReturnBack(ExRpcAdmin connection)
		{
			connection.Dispose();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000CA68 File Offset: 0x0000AC68
		private void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
		}

		// Token: 0x04000130 RID: 304
		internal const ConnectFlag ConnectionFlags = ConnectFlag.UseAdminPrivilege;

		// Token: 0x04000131 RID: 305
		private const int MaximumConnectionCacheSize = 10;

		// Token: 0x04000132 RID: 306
		private const bool DisallowConnectionCacheOverflow = false;

		// Token: 0x04000133 RID: 307
		internal static readonly ConnectionPool Instance = new ConnectionPool();

		// Token: 0x04000134 RID: 308
		private DisposeTracker disposeTracker;
	}
}
