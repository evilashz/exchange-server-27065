using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.Rpc.RfriServer
{
	// Token: 0x020003AA RID: 938
	internal class RfriAsyncRpcState_GetNewDSA : BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetNewDSA,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>
	{
		// Token: 0x06001094 RID: 4244 RVA: 0x0004CFD4 File Offset: 0x0004C3D4
		public void Initialize(SafeRpcAsyncStateHandle asyncState, RfriAsyncRpcServer asyncServer, IntPtr bindingHandle, RfriGetNewDSAFlags flags, IntPtr pUserDn, IntPtr ppUnused, IntPtr ppServer)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.bindingHandle = bindingHandle;
			this.flags = flags;
			this.pUserDn = pUserDn;
			this.ppUnused = ppUnused;
			this.ppServer = ppServer;
			this.clientBinding = null;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0004CAC4 File Offset: 0x0004BEC4
		public override void InternalReset()
		{
			this.bindingHandle = IntPtr.Zero;
			this.flags = RfriGetNewDSAFlags.None;
			this.pUserDn = IntPtr.Zero;
			this.ppUnused = IntPtr.Zero;
			this.ppServer = IntPtr.Zero;
			this.clientBinding = null;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0004D018 File Offset: 0x0004C418
		public unsafe override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			string userDn = null;
			if (this.ppServer != IntPtr.Zero)
			{
				*(long*)this.ppServer.ToPointer() = 0L;
			}
			if (this.pUserDn != IntPtr.Zero)
			{
				userDn = Marshal.PtrToStringAnsi(this.pUserDn);
			}
			RpcClientBinding rpcClientBinding = new RpcClientBinding(this.bindingHandle, base.AsyncState);
			this.clientBinding = rpcClientBinding;
			base.AsyncDispatch.BeginGetNewDSA(null, rpcClientBinding, this.flags, userDn, asyncCallback, this);
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0004D0AC File Offset: 0x0004C4AC
		public unsafe override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			string @string = null;
			bool flag = false;
			byte* ptr = null;
			int result;
			try
			{
				@string = null;
				RfriStatus rfriStatus = base.AsyncDispatch.EndGetNewDSA(asyncResult, out @string);
				if (rfriStatus == RfriStatus.Success && this.ppServer != IntPtr.Zero)
				{
					ptr = (byte*)<Module>.StringToUnmanagedMultiByte(@string, 0U);
					*(long*)this.ppServer.ToPointer() = ptr;
				}
				flag = true;
				result = (int)rfriStatus;
			}
			finally
			{
				if (!flag && ptr != null)
				{
					<Module>.FreeString((ushort*)ptr);
					if (this.ppServer != IntPtr.Zero)
					{
						*(long*)this.ppServer.ToPointer() = 0L;
					}
				}
			}
			return result;
		}

		// Token: 0x04000FB0 RID: 4016
		private IntPtr bindingHandle;

		// Token: 0x04000FB1 RID: 4017
		private IntPtr pUserDn;

		// Token: 0x04000FB2 RID: 4018
		private RfriGetNewDSAFlags flags;

		// Token: 0x04000FB3 RID: 4019
		private IntPtr ppUnused;

		// Token: 0x04000FB4 RID: 4020
		private IntPtr ppServer;

		// Token: 0x04000FB5 RID: 4021
		private ClientBinding clientBinding;
	}
}
