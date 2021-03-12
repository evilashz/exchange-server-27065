using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000007 RID: 7
	internal class DispatchTaskAsyncResult : EasyCancelableAsyncResult
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003E7F File Offset: 0x0000207F
		public DispatchTaskAsyncResult(DispatchTask dispatchTask, CancelableAsyncCallback asyncCallback, object asyncState) : base(asyncCallback, asyncState)
		{
			this.dispatchTask = dispatchTask;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003E90 File Offset: 0x00002090
		public DispatchTask DispatchTask
		{
			get
			{
				return this.dispatchTask;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003E98 File Offset: 0x00002098
		protected override void InternalCancel()
		{
		}

		// Token: 0x04000031 RID: 49
		private readonly DispatchTask dispatchTask;
	}
}
