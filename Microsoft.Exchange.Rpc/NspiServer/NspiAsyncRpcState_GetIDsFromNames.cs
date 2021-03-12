using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000320 RID: 800
	internal class NspiAsyncRpcState_GetIDsFromNames : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetIDsFromNames,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x00039718 File Offset: 0x00038B18
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiGetIDsFromNamesFlags flags, int mapiFlags, int nameCount, IntPtr pNames, IntPtr ppPropTags)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.mapiFlags = mapiFlags;
			this.nameCount = nameCount;
			this.pNames = pNames;
			this.ppPropTags = ppPropTags;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x00035E1C File Offset: 0x0003521C
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiGetIDsFromNamesFlags.None;
			this.mapiFlags = 0;
			this.nameCount = 0;
			this.pNames = IntPtr.Zero;
			this.ppPropTags = IntPtr.Zero;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0003975C File Offset: 0x00038B5C
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			if (this.ppPropTags != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppPropTags, IntPtr.Zero);
			}
			base.AsyncDispatch.BeginGetIDsFromNames(null, this.contextHandle, this.flags, this.mapiFlags, this.nameCount, this.pNames, asyncCallback, this);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000397CC File Offset: 0x00038BCC
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			PropertyTag[] array = null;
			NspiStatus result = NspiStatus.Success;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				array = null;
				result = base.AsyncDispatch.EndGetIDsFromNames(asyncResult, out array);
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

		// Token: 0x04000EF8 RID: 3832
		private IntPtr contextHandle;

		// Token: 0x04000EF9 RID: 3833
		private NspiGetIDsFromNamesFlags flags;

		// Token: 0x04000EFA RID: 3834
		private int mapiFlags;

		// Token: 0x04000EFB RID: 3835
		private int nameCount;

		// Token: 0x04000EFC RID: 3836
		private IntPtr pNames;

		// Token: 0x04000EFD RID: 3837
		private IntPtr ppPropTags;
	}
}
