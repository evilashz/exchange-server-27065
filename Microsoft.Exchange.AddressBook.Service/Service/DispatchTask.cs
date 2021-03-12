using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000006 RID: 6
	internal abstract class DispatchTask : BaseObject
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003E41 File Offset: 0x00002041
		public DispatchTask(CancelableAsyncCallback asyncCallback, object asyncState)
		{
			this.asyncResult = new DispatchTaskAsyncResult(this, asyncCallback, asyncState);
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003E57 File Offset: 0x00002057
		public DispatchTaskAsyncResult AsyncResult
		{
			get
			{
				base.CheckDisposed();
				return this.asyncResult;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006C RID: 108
		protected abstract string TaskName { get; }

		// Token: 0x0600006D RID: 109 RVA: 0x00003E65 File Offset: 0x00002065
		protected void Completion()
		{
			this.asyncResult.InvokeCallback();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003E72 File Offset: 0x00002072
		protected void CheckCompletion()
		{
			this.asyncResult.WaitForCompletion();
		}

		// Token: 0x04000030 RID: 48
		private readonly DispatchTaskAsyncResult asyncResult;
	}
}
