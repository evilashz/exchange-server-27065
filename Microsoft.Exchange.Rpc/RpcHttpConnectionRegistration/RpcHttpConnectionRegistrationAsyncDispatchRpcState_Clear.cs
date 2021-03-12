using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003D0 RID: 976
	internal class RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear : RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Clear>
	{
		// Token: 0x060010F6 RID: 4342 RVA: 0x00053334 File Offset: 0x00052734
		public void Initialize(SafeRpcAsyncStateHandle asyncState, RpcHttpConnectionRegistrationAsyncRpcServer asyncServer, IntPtr bindingHandle)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.m_bindingHandle = bindingHandle;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00053350 File Offset: 0x00052750
		public override void Reset()
		{
			base.InternalReset();
			this.m_bindingHandle = IntPtr.Zero;
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x00053370 File Offset: 0x00052770
		public override void InternalBegin(CancelableAsyncCallback asyncCallback, IRpcHttpConnectionRegistrationAsyncDispatch asyncDispatch)
		{
			asyncDispatch.BeginClear(asyncCallback, this);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x00053388 File Offset: 0x00052788
		public override int InternalEnd(ICancelableAsyncResult asyncResult, IRpcHttpConnectionRegistrationAsyncDispatch asyncDispatch)
		{
			return asyncDispatch.EndClear(asyncResult);
		}

		// Token: 0x04000FF7 RID: 4087
		private IntPtr m_bindingHandle;
	}
}
