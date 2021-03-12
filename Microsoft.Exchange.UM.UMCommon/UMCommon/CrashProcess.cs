using System;
using System.Threading;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000058 RID: 88
	internal sealed class CrashProcess
	{
		// Token: 0x06000359 RID: 857 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		private CrashProcess()
		{
			this.signalEvent = new ManualResetEvent(false);
			this.waitEvent = new ManualResetEvent(false);
			Thread thread = new Thread(new ThreadStart(this.WorkerFunction));
			thread.Start();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0000CD9F File Offset: 0x0000AF9F
		internal static CrashProcess Instance
		{
			get
			{
				return CrashProcess.instance;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000CDA6 File Offset: 0x0000AFA6
		internal void CrashThisProcess(Exception ex)
		{
			this.message = ex.ToString();
			this.signalEvent.Set();
			this.waitEvent.WaitOne();
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		private void WorkerFunction()
		{
			this.signalEvent.WaitOne();
			throw new InvalidOperationException(this.message);
		}

		// Token: 0x04000287 RID: 647
		private static CrashProcess instance = new CrashProcess();

		// Token: 0x04000288 RID: 648
		private ManualResetEvent signalEvent;

		// Token: 0x04000289 RID: 649
		private ManualResetEvent waitEvent;

		// Token: 0x0400028A RID: 650
		private string message;
	}
}
