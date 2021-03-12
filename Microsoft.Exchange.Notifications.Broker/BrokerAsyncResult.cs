using System;
using System.Threading;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000017 RID: 23
	internal class BrokerAsyncResult : ICancelableAsyncResult, IAsyncResult, IDisposable
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000630E File Offset: 0x0000450E
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00006316 File Offset: 0x00004516
		public object AsyncState { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00006320 File Offset: 0x00004520
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				lock (this.mutex)
				{
					if (this.endSignal == null)
					{
						this.endSignal = new ManualResetEvent(this.IsCompleted);
					}
				}
				return this.endSignal;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000637C File Offset: 0x0000457C
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00006384 File Offset: 0x00004584
		public bool CompletedSynchronously { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000638D File Offset: 0x0000458D
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00006398 File Offset: 0x00004598
		public bool IsCompleted
		{
			get
			{
				return this.isCompleted;
			}
			set
			{
				if (value)
				{
					lock (this.mutex)
					{
						if (this.endSignal != null)
						{
							this.endSignal.Set();
						}
						this.isCompleted = true;
						return;
					}
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000063F8 File Offset: 0x000045F8
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00006400 File Offset: 0x00004600
		public bool IsCanceled { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00006409 File Offset: 0x00004609
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00006411 File Offset: 0x00004611
		public BrokerStatus Result { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000641A File Offset: 0x0000461A
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00006422 File Offset: 0x00004622
		public string NotificationJson { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000642B File Offset: 0x0000462B
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00006433 File Offset: 0x00004633
		public CancelableAsyncCallback Callback { get; set; }

		// Token: 0x06000100 RID: 256 RVA: 0x0000643C File Offset: 0x0000463C
		public void Cancel()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006443 File Offset: 0x00004643
		public void Dispose()
		{
			if (this.endSignal != null)
			{
				this.endSignal.Dispose();
				this.endSignal = null;
			}
		}

		// Token: 0x0400006E RID: 110
		private object mutex = new object();

		// Token: 0x0400006F RID: 111
		private bool isCompleted;

		// Token: 0x04000070 RID: 112
		private ManualResetEvent endSignal;
	}
}
