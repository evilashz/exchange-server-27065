using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200009F RID: 159
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EasyCancelableAsyncResult : EasyAsyncResultBase, ICancelableAsyncResult, IAsyncResult
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		public EasyCancelableAsyncResult(CancelableAsyncCallback asyncCallback, object asyncState) : base(asyncState)
		{
			this.asyncCallback = asyncCallback;
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000DBCE File Offset: 0x0000BDCE
		public bool IsCanceled
		{
			get
			{
				return this.isCanceled;
			}
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		public void Cancel()
		{
			if (this.isCanceled)
			{
				return;
			}
			lock (base.CompletionLock)
			{
				if (this.isCanceled)
				{
					return;
				}
				this.isCanceled = true;
			}
			if (!base.IsCompleted)
			{
				this.InternalCancel();
				return;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000DC3C File Offset: 0x0000BE3C
		protected virtual void InternalCancel()
		{
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000DC3E File Offset: 0x0000BE3E
		protected override void InternalCallback()
		{
			if (this.asyncCallback != null)
			{
				this.asyncCallback(this);
			}
		}

		// Token: 0x04000265 RID: 613
		private readonly CancelableAsyncCallback asyncCallback;

		// Token: 0x04000266 RID: 614
		private bool isCanceled;
	}
}
