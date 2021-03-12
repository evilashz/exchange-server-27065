using System;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x0200031C RID: 796
	internal class NspiAsyncRpcState_QueryColumns : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_QueryColumns,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000F15 RID: 3861 RVA: 0x000393C4 File Offset: 0x000387C4
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiQueryColumnsFlags flags, NspiQueryColumnsMapiFlags mapiFlags, IntPtr ppColumns)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.mapiFlags = mapiFlags;
			this.ppColumns = ppColumns;
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x00035D9C File Offset: 0x0003519C
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiQueryColumnsFlags.None;
			this.mapiFlags = NspiQueryColumnsMapiFlags.None;
			this.ppColumns = IntPtr.Zero;
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000393F8 File Offset: 0x000387F8
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			if (this.ppColumns != IntPtr.Zero)
			{
				Marshal.WriteIntPtr(this.ppColumns, IntPtr.Zero);
			}
			base.AsyncDispatch.BeginQueryColumns(null, this.contextHandle, this.flags, this.mapiFlags, asyncCallback, this);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00039458 File Offset: 0x00038858
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			PropertyTag[] array = null;
			NspiStatus result = NspiStatus.Success;
			SafeRpcMemoryHandle safeRpcMemoryHandle = null;
			try
			{
				array = null;
				result = base.AsyncDispatch.EndQueryColumns(asyncResult, out array);
				if (this.ppColumns != IntPtr.Zero && array != null)
				{
					safeRpcMemoryHandle = MarshalHelper.ConvertPropertyTagArrayToSPropTagArray(array, true);
					if (safeRpcMemoryHandle != null)
					{
						IntPtr val = safeRpcMemoryHandle.Detach();
						Marshal.WriteIntPtr(this.ppColumns, val);
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

		// Token: 0x04000EDE RID: 3806
		private IntPtr contextHandle;

		// Token: 0x04000EDF RID: 3807
		private NspiQueryColumnsFlags flags;

		// Token: 0x04000EE0 RID: 3808
		private NspiQueryColumnsMapiFlags mapiFlags;

		// Token: 0x04000EE1 RID: 3809
		private IntPtr ppColumns;
	}
}
