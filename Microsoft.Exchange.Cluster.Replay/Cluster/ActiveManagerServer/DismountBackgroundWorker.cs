using System;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000070 RID: 112
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DismountBackgroundWorker : IDisposable
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000197A8 File Offset: 0x000179A8
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x000197B0 File Offset: 0x000179B0
		private IAsyncResult AsyncResult { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x000197B9 File Offset: 0x000179B9
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x000197C1 File Offset: 0x000179C1
		private DismountBackgroundWorker.DismountDelegate Func { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000197CA File Offset: 0x000179CA
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x000197D2 File Offset: 0x000179D2
		public ManualResetEvent CompletedEvent { get; private set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x000197DB File Offset: 0x000179DB
		// (set) Token: 0x060004BF RID: 1215 RVA: 0x000197E3 File Offset: 0x000179E3
		public Exception DismountException { get; private set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x000197EC File Offset: 0x000179EC
		// (set) Token: 0x060004C1 RID: 1217 RVA: 0x000197F4 File Offset: 0x000179F4
		public ExDateTime StartTime { get; private set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x000197FD File Offset: 0x000179FD
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x00019805 File Offset: 0x00017A05
		public FileIOonSourceException LastE00ReadException { get; set; }

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001980E File Offset: 0x00017A0E
		public DismountBackgroundWorker(DismountBackgroundWorker.DismountDelegate func)
		{
			this.Func = func;
			this.CompletedEvent = new ManualResetEvent(false);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00019849 File Offset: 0x00017A49
		public void Start()
		{
			this.StartTime = ExDateTime.Now;
			this.AsyncResult = this.Func.BeginInvoke(delegate(IAsyncResult ar)
			{
				this.DismountException = this.Func.EndInvoke(ar);
				this.CompletedEvent.Set();
			}, null);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00019874 File Offset: 0x00017A74
		public void Dispose()
		{
			GC.SuppressFinalize(this);
			this.Dispose(true);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00019883 File Offset: 0x00017A83
		public void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.CompletedEvent.Close();
				this.CompletedEvent = null;
			}
		}

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x060004CA RID: 1226
		public delegate Exception DismountDelegate();
	}
}
