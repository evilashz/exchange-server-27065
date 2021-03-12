using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x020002FE RID: 766
	internal class NspiAsyncRpcState_Unbind : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_Unbind,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000DDA RID: 3546 RVA: 0x0003831C File Offset: 0x0003771C
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr pContextHandle, NspiUnbindFlags flags)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.pContextHandle = pContextHandle;
			this.flags = flags;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00035998 File Offset: 0x00034D98
		public override void InternalReset()
		{
			this.pContextHandle = IntPtr.Zero;
			this.flags = NspiUnbindFlags.None;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00038340 File Offset: 0x00037740
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			IntPtr contextHandle = IntPtr.Zero;
			if (this.pContextHandle != IntPtr.Zero)
			{
				contextHandle = Marshal.ReadIntPtr(this.pContextHandle);
			}
			base.AsyncDispatch.BeginUnbind(null, contextHandle, this.flags, asyncCallback, this);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00038394 File Offset: 0x00037794
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			IntPtr zero = IntPtr.Zero;
			int result = (int)base.AsyncDispatch.EndUnbind(asyncResult, out zero);
			if (this.pContextHandle != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.pContextHandle, zero);
			}
			return result;
		}

		// Token: 0x04000E11 RID: 3601
		private NspiUnbindFlags flags;

		// Token: 0x04000E12 RID: 3602
		private IntPtr pContextHandle;
	}
}
