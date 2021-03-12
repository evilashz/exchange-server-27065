using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;

namespace Microsoft.Exchange.Rpc.RfriServer
{
	// Token: 0x020003AC RID: 940
	internal class RfriAsyncRpcState_GetFQDNFromLegacyDN : BaseAsyncRpcState<Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcState_GetFQDNFromLegacyDN,Microsoft::Exchange::Rpc::RfriServer::RfriAsyncRpcServer,Microsoft::Exchange::Rpc::IRfriAsyncDispatch>
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x0004D170 File Offset: 0x0004C570
		public void Initialize(SafeRpcAsyncStateHandle asyncState, RfriAsyncRpcServer asyncServer, IntPtr bindingHandle, RfriGetFQDNFromLegacyDNFlags flags, uint cbServerDn, IntPtr pServerDn, IntPtr ppServerFqdn)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.bindingHandle = bindingHandle;
			this.flags = flags;
			this.cbServerDn = cbServerDn;
			this.pServerDn = pServerDn;
			this.ppServerFqdn = ppServerFqdn;
			this.clientBinding = null;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0004CB0C File Offset: 0x0004BF0C
		public override void InternalReset()
		{
			this.bindingHandle = IntPtr.Zero;
			this.flags = RfriGetFQDNFromLegacyDNFlags.None;
			this.cbServerDn = 0U;
			this.pServerDn = IntPtr.Zero;
			this.ppServerFqdn = IntPtr.Zero;
			this.clientBinding = null;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0004D1B4 File Offset: 0x0004C5B4
		public unsafe override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			string serverDn = null;
			if (this.ppServerFqdn != IntPtr.Zero)
			{
				*(long*)this.ppServerFqdn.ToPointer() = 0L;
			}
			if (this.pServerDn != IntPtr.Zero)
			{
				uint num = this.cbServerDn;
				if (num > 0U)
				{
					serverDn = Marshal.PtrToStringAnsi(this.pServerDn, (int)(num - 1U));
				}
			}
			RpcClientBinding rpcClientBinding = new RpcClientBinding(this.bindingHandle, base.AsyncState);
			this.clientBinding = rpcClientBinding;
			base.AsyncDispatch.BeginGetFQDNFromLegacyDN(null, rpcClientBinding, this.flags, serverDn, asyncCallback, this);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004D254 File Offset: 0x0004C654
		public unsafe override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			string @string = null;
			bool flag = false;
			byte* ptr = null;
			int result;
			try
			{
				@string = null;
				RfriStatus rfriStatus = base.AsyncDispatch.EndGetFQDNFromLegacyDN(asyncResult, out @string);
				if (rfriStatus == RfriStatus.Success && this.ppServerFqdn != IntPtr.Zero)
				{
					ptr = (byte*)<Module>.StringToUnmanagedMultiByte(@string, 0U);
					*(long*)this.ppServerFqdn.ToPointer() = ptr;
				}
				flag = true;
				result = (int)rfriStatus;
			}
			finally
			{
				if (!flag && ptr != null)
				{
					<Module>.FreeString((ushort*)ptr);
					if (this.ppServerFqdn != IntPtr.Zero)
					{
						*(long*)this.ppServerFqdn.ToPointer() = 0L;
					}
				}
			}
			return result;
		}

		// Token: 0x04000FBE RID: 4030
		private IntPtr bindingHandle;

		// Token: 0x04000FBF RID: 4031
		private RfriGetFQDNFromLegacyDNFlags flags;

		// Token: 0x04000FC0 RID: 4032
		private uint cbServerDn;

		// Token: 0x04000FC1 RID: 4033
		private IntPtr pServerDn;

		// Token: 0x04000FC2 RID: 4034
		private IntPtr ppServerFqdn;

		// Token: 0x04000FC3 RID: 4035
		private ClientBinding clientBinding;
	}
}
