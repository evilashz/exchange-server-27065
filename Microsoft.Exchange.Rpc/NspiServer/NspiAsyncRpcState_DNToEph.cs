using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x0200030A RID: 778
	internal class NspiAsyncRpcState_DNToEph : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DNToEph,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000E58 RID: 3672 RVA: 0x00038A60 File Offset: 0x00037E60
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiDNToEphFlags flags, IntPtr pNames, IntPtr ppEphs)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pNames = pNames;
			this.ppEphs = ppEphs;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00035B60 File Offset: 0x00034F60
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiDNToEphFlags.None;
			this.pNames = IntPtr.Zero;
			this.ppEphs = IntPtr.Zero;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00038A94 File Offset: 0x00037E94
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			if (this.ppEphs != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppEphs, IntPtr.Zero);
			}
			string[] dns = NspiHelper.ConvertCountedStringArrayFromNative(this.pNames, true);
			base.AsyncDispatch.BeginDNToEph(null, this.contextHandle, this.flags, dns, asyncCallback, this);
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x00038B00 File Offset: 0x00037F00
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			int[] array = null;
			NspiStatus result = NspiStatus.Success;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				array = null;
				result = base.AsyncDispatch.EndDNToEph(asyncResult, out array);
				if (this.ppEphs != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = NspiHelper.ConvertIntArrayToPropTagArray(array, true);
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppEphs, val);
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

		// Token: 0x04000E66 RID: 3686
		private IntPtr contextHandle;

		// Token: 0x04000E67 RID: 3687
		private NspiDNToEphFlags flags;

		// Token: 0x04000E68 RID: 3688
		private IntPtr pNames;

		// Token: 0x04000E69 RID: 3689
		private IntPtr ppEphs;
	}
}
