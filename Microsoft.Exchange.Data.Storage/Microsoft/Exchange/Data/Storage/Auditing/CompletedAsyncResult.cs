using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F4E RID: 3918
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CompletedAsyncResult : IAsyncResult
	{
		// Token: 0x170023A0 RID: 9120
		// (get) Token: 0x0600867B RID: 34427 RVA: 0x0024E94E File Offset: 0x0024CB4E
		public object AsyncState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170023A1 RID: 9121
		// (get) Token: 0x0600867C RID: 34428 RVA: 0x0024E951 File Offset: 0x0024CB51
		public WaitHandle AsyncWaitHandle
		{
			get
			{
				if (this.waitHandle == null)
				{
					this.waitHandle = new ManualResetEvent(true);
				}
				return this.waitHandle;
			}
		}

		// Token: 0x170023A2 RID: 9122
		// (get) Token: 0x0600867D RID: 34429 RVA: 0x0024E96D File Offset: 0x0024CB6D
		public bool CompletedSynchronously
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170023A3 RID: 9123
		// (get) Token: 0x0600867E RID: 34430 RVA: 0x0024E970 File Offset: 0x0024CB70
		public bool IsCompleted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04005A01 RID: 23041
		private WaitHandle waitHandle;
	}
}
