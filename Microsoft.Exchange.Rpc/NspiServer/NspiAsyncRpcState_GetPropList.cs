using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x0200030C RID: 780
	internal class NspiAsyncRpcState_GetPropList : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetPropList,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E6D RID: 3693 RVA: 0x00038BA4 File Offset: 0x00037FA4
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiGetPropListFlags flags, int mid, int codePage, IntPtr ppPropTags)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.mid = mid;
			this.codePage = codePage;
			this.ppPropTags = ppPropTags;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00035B98 File Offset: 0x00034F98
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiGetPropListFlags.None;
			this.mid = 0;
			this.codePage = 0;
			this.ppPropTags = IntPtr.Zero;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x00038BE0 File Offset: 0x00037FE0
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			if (this.ppPropTags != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppPropTags, IntPtr.Zero);
			}
			base.AsyncDispatch.BeginGetPropList(null, this.contextHandle, this.flags, this.mid, this.codePage, asyncCallback, this);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00038C48 File Offset: 0x00038048
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			PropertyTag[] array = null;
			NspiStatus result = NspiStatus.Success;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				array = null;
				result = base.AsyncDispatch.EndGetPropList(asyncResult, out array);
				if (this.ppPropTags != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = MarshalHelper.ConvertPropertyTagArrayToSPropTagArray(array, true);
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppPropTags, val);
					}
				}
			}
			finally
			{
				if (safeRpcMemoryHandle != null)
				{
					((IDisposable)safeRpcMemoryHandle).Dispose();
				}
			}
			return (int)result;
		}

		// Token: 0x04000E72 RID: 3698
		private IntPtr contextHandle;

		// Token: 0x04000E73 RID: 3699
		private NspiGetPropListFlags flags;

		// Token: 0x04000E74 RID: 3700
		private int mid;

		// Token: 0x04000E75 RID: 3701
		private int codePage;

		// Token: 0x04000E76 RID: 3702
		private IntPtr ppPropTags;
	}
}
