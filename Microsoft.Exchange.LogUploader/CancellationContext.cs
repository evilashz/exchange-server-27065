using System;
using System.Threading;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x0200000B RID: 11
	internal class CancellationContext
	{
		// Token: 0x060000BF RID: 191 RVA: 0x00003EAD File Offset: 0x000020AD
		public CancellationContext(CancellationToken stopToken, ManualResetEvent stopWaitHandle)
		{
			this.stopToken = stopToken;
			this.stopWaitHandle = stopWaitHandle;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003EC3 File Offset: 0x000020C3
		public CancellationToken StopToken
		{
			get
			{
				return this.stopToken;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003ECB File Offset: 0x000020CB
		public ManualResetEvent StopWaitHandle
		{
			get
			{
				return this.stopWaitHandle;
			}
		}

		// Token: 0x0400005E RID: 94
		private CancellationToken stopToken;

		// Token: 0x0400005F RID: 95
		private ManualResetEvent stopWaitHandle;
	}
}
