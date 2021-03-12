using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x0200031E RID: 798
	internal class NspiAsyncRpcState_GetNamesFromIDs : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_GetNamesFromIDs,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x000394FC File Offset: 0x000388FC
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiGetNamesFromIDsFlags flags, IntPtr pGuid, IntPtr pPropTags, IntPtr ppReturnedPropTags, IntPtr ppNames)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pGuid = pGuid;
			this.pPropTags = pPropTags;
			this.ppReturnedPropTags = ppReturnedPropTags;
			this.ppNames = ppNames;
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x00035DD0 File Offset: 0x000351D0
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiGetNamesFromIDsFlags.None;
			this.pGuid = IntPtr.Zero;
			this.pPropTags = IntPtr.Zero;
			this.ppReturnedPropTags = IntPtr.Zero;
			this.ppNames = IntPtr.Zero;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x00039540 File Offset: 0x00038940
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			PropertyTag[] propertyTags = null;
			if (this.ppReturnedPropTags != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppReturnedPropTags, IntPtr.Zero);
			}
			if (this.ppNames != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppNames, IntPtr.Zero);
			}
			Guid? guid = null;
			if (this.pGuid != IntPtr.Zero)
			{
				Guid value = NspiHelper.ConvertGuidFromNative(this.pGuid);
				Guid? guid2 = new Guid?(value);
				guid = guid2;
			}
			if (this.pPropTags != IntPtr.Zero)
			{
				propertyTags = MarshalHelper.ConvertSPropTagArrayToPropertyTagArray(this.pPropTags);
			}
			base.AsyncDispatch.BeginGetNamesFromIDs(null, this.contextHandle, this.flags, guid, propertyTags, asyncCallback, this);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003962C File Offset: 0x00038A2C
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			PropertyTag[] array = null;
			NspiStatus result = NspiStatus.Success;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			SafeRpcMemoryHandle safeRpcMemoryHandle2 = null;
			try
			{
				array = null;
				result = base.AsyncDispatch.EndGetNamesFromIDs(asyncResult, out array, out safeRpcMemoryHandle);
				if (this.ppReturnedPropTags != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle2 = MarshalHelper.ConvertPropertyTagArrayToSPropTagArray(array, true);
					if (safeRpcMemoryHandle2 != null)
					{
						IntPtr val = safeRpcMemoryHandle2.Detach();
						Marshal.WriteIntPtr(this.ppReturnedPropTags, val);
					}
				}
				if (this.ppNames != IntPtr.Zero && safeRpcMemoryHandle != null)
				{
					IntPtr val2 = safeRpcMemoryHandle.Detach();
					Marshal.WriteIntPtr(this.ppNames, val2);
				}
			}
			finally
			{
				if (safeRpcMemoryHandle2 != null)
				{
					((IDisposable)safeRpcMemoryHandle2).Dispose();
				}
				if (safeRpcMemoryHandle != null)
				{
					((IDisposable)safeRpcMemoryHandle).Dispose();
				}
			}
			return (int)result;
		}

		// Token: 0x04000EEA RID: 3818
		private IntPtr contextHandle;

		// Token: 0x04000EEB RID: 3819
		private NspiGetNamesFromIDsFlags flags;

		// Token: 0x04000EEC RID: 3820
		private IntPtr pGuid;

		// Token: 0x04000EED RID: 3821
		private IntPtr pPropTags;

		// Token: 0x04000EEE RID: 3822
		private IntPtr ppReturnedPropTags;

		// Token: 0x04000EEF RID: 3823
		private IntPtr ppNames;
	}
}
