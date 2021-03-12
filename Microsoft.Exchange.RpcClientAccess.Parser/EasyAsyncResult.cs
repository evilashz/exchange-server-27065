using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200009E RID: 158
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EasyAsyncResult : EasyAsyncResultBase
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x0000DB98 File Offset: 0x0000BD98
		public EasyAsyncResult(AsyncCallback asyncCallback, object asyncState) : base(asyncState)
		{
			this.asyncCallback = asyncCallback;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		protected sealed override void InternalCallback()
		{
			if (this.asyncCallback != null)
			{
				this.asyncCallback(this);
			}
		}

		// Token: 0x04000264 RID: 612
		private readonly AsyncCallback asyncCallback;
	}
}
