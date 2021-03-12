using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Rpc.RpcHttpConnectionRegistration
{
	// Token: 0x020003CC RID: 972
	internal class RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register : RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>
	{
		// Token: 0x060010E1 RID: 4321 RVA: 0x00053060 File Offset: 0x00052460
		public void Initialize(SafeRpcAsyncStateHandle asyncState, RpcHttpConnectionRegistrationAsyncRpcServer asyncServer, IntPtr bindingHandle, IntPtr pAssociationGroupId, IntPtr pToken, IntPtr pServerTarget, IntPtr pSessionCookie, IntPtr pClientIp, IntPtr pRequestId, IntPtr ppFailureMessage, IntPtr ppFailureDetails)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.m_bindingHandle = bindingHandle;
			this.m_pAssociationGroupId = pAssociationGroupId;
			this.m_pToken = pToken;
			this.m_pServerTarget = pServerTarget;
			this.m_pSessionCookie = pSessionCookie;
			this.m_pClientIp = pClientIp;
			this.m_pRequestId = pRequestId;
			this.m_ppFailureMessage = ppFailureMessage;
			this.m_ppFailureDetails = ppFailureDetails;
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000530BC File Offset: 0x000524BC
		public override void Reset()
		{
			base.InternalReset();
			this.m_bindingHandle = IntPtr.Zero;
			this.m_pAssociationGroupId = IntPtr.Zero;
			this.m_pToken = IntPtr.Zero;
			this.m_pServerTarget = IntPtr.Zero;
			this.m_pSessionCookie = IntPtr.Zero;
			this.m_pClientIp = IntPtr.Zero;
			this.m_pRequestId = IntPtr.Zero;
			this.m_ppFailureMessage = IntPtr.Zero;
			this.m_ppFailureDetails = IntPtr.Zero;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00053B08 File Offset: 0x00052F08
		public override void InternalBegin(CancelableAsyncCallback asyncCallback, IRpcHttpConnectionRegistrationAsyncDispatch asyncDispatch)
		{
			Guid associationGroupId = RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>.CopyIntPtrToGuid(this.m_pAssociationGroupId);
			string token = Marshal.PtrToStringAnsi(this.m_pToken);
			string serverTarget = Marshal.PtrToStringAnsi(this.m_pServerTarget);
			string sessionCookie = Marshal.PtrToStringAnsi(this.m_pSessionCookie);
			string clientIp = Marshal.PtrToStringAnsi(this.m_pClientIp);
			Guid requestId = RpcHttpConnectionRegistrationAsyncDispatchRpcStatePool<Microsoft::Exchange::Rpc::RpcHttpConnectionRegistration::RpcHttpConnectionRegistrationAsyncDispatchRpcState_Register>.CopyIntPtrToGuid(this.m_pRequestId);
			asyncDispatch.BeginRegister(associationGroupId, token, serverTarget, sessionCookie, clientIp, requestId, asyncCallback, this);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00053134 File Offset: 0x00052534
		public unsafe override int InternalEnd(ICancelableAsyncResult asyncResult, IRpcHttpConnectionRegistrationAsyncDispatch asyncDispatch)
		{
			string text = null;
			string text2 = null;
			bool flag = false;
			byte* ptr = null;
			byte* ptr2 = null;
			int result;
			try
			{
				text = null;
				text2 = null;
				int num = asyncDispatch.EndRegister(asyncResult, out text, out text2);
				if (this.m_ppFailureMessage != IntPtr.Zero && text != null)
				{
					ptr = (byte*)<Module>.StringToUnmanagedMultiByte(text, 0U);
					*(long*)this.m_ppFailureMessage.ToPointer() = ptr;
				}
				if (this.m_ppFailureDetails != IntPtr.Zero && text2 != null)
				{
					ptr2 = (byte*)<Module>.StringToUnmanagedMultiByte(text2, 0U);
					*(long*)this.m_ppFailureDetails.ToPointer() = ptr2;
				}
				flag = true;
				result = num;
			}
			finally
			{
				if (!flag)
				{
					if (ptr != null)
					{
						<Module>.FreeString((ushort*)ptr);
						if (this.m_ppFailureMessage != IntPtr.Zero)
						{
							*(long*)this.m_ppFailureMessage.ToPointer() = 0L;
						}
					}
					if (ptr2 != null)
					{
						<Module>.FreeString((ushort*)ptr2);
						if (this.m_ppFailureDetails != IntPtr.Zero)
						{
							*(long*)this.m_ppFailureDetails.ToPointer() = 0L;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x04000FE7 RID: 4071
		private IntPtr m_bindingHandle;

		// Token: 0x04000FE8 RID: 4072
		private IntPtr m_pAssociationGroupId;

		// Token: 0x04000FE9 RID: 4073
		private IntPtr m_pToken;

		// Token: 0x04000FEA RID: 4074
		private IntPtr m_pServerTarget;

		// Token: 0x04000FEB RID: 4075
		private IntPtr m_pSessionCookie;

		// Token: 0x04000FEC RID: 4076
		private IntPtr m_pClientIp;

		// Token: 0x04000FED RID: 4077
		private IntPtr m_pRequestId;

		// Token: 0x04000FEE RID: 4078
		private IntPtr m_ppFailureMessage;

		// Token: 0x04000FEF RID: 4079
		private IntPtr m_ppFailureDetails;
	}
}
