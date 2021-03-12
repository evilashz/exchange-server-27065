using System;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003CE RID: 974
	internal class RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister : RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>
	{
		// Token: 0x060010EC RID: 4332 RVA: 0x00053288 File Offset: 0x00052688
		public void Initialize(SafeRpcAsyncStateHandle asyncState, RpcHttpConnectionRegistrationAsyncRpcServer asyncServer, IntPtr bindingHandle, IntPtr pAssociationGroupId, IntPtr pRequestId)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.m_bindingHandle = bindingHandle;
			this.m_pAssociationGroupId = pAssociationGroupId;
			this.m_pRequestId = pRequestId;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000532B4 File Offset: 0x000526B4
		public override void Reset()
		{
			base.InternalReset();
			this.m_bindingHandle = IntPtr.Zero;
			this.m_pAssociationGroupId = IntPtr.Zero;
			this.m_pRequestId = IntPtr.Zero;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00053BA4 File Offset: 0x00052FA4
		public override void InternalBegin(CancelableAsyncCallback asyncCallback, IRpcHttpConnectionRegistrationAsyncDispatch asyncDispatch)
		{
			Guid associationGroupId = RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>.CopyIntPtrToGuid(this.m_pAssociationGroupId);
			Guid requestId = RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Unregister>.CopyIntPtrToGuid(this.m_pRequestId);
			asyncDispatch.BeginUnregister(associationGroupId, requestId, asyncCallback, this);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000532E8 File Offset: 0x000526E8
		public override int InternalEnd(ICancelableAsyncResult asyncResult, IRpcHttpConnectionRegistrationAsyncDispatch asyncDispatch)
		{
			return asyncDispatch.EndUnregister(asyncResult);
		}

		// Token: 0x04000FF2 RID: 4082
		private IntPtr m_bindingHandle;

		// Token: 0x04000FF3 RID: 4083
		private IntPtr m_pAssociationGroupId;

		// Token: 0x04000FF4 RID: 4084
		private IntPtr m_pRequestId;
	}
}
