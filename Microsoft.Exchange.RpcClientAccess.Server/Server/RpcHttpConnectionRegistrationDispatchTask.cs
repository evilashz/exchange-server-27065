using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x02000042 RID: 66
	internal abstract class RpcHttpConnectionRegistrationDispatchTask : DispatchTask
	{
		// Token: 0x06000252 RID: 594 RVA: 0x0000CF42 File Offset: 0x0000B142
		public RpcHttpConnectionRegistrationDispatchTask(string description, RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch, CancelableAsyncCallback asyncCallback, object asyncState) : base(description, asyncCallback, asyncState)
		{
			this.rpcHttpConnectionRegistrationDispatch = rpcHttpConnectionRegistrationDispatch;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000CF55 File Offset: 0x0000B155
		internal RpcHttpConnectionRegistrationDispatch RpcHttpConnectionRegistrationDispatch
		{
			get
			{
				return this.rpcHttpConnectionRegistrationDispatch;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000CF5D File Offset: 0x0000B15D
		internal override IntPtr ContextHandle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		// Token: 0x04000130 RID: 304
		private readonly RpcHttpConnectionRegistrationDispatch rpcHttpConnectionRegistrationDispatch;
	}
}
