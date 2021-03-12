using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x0200031A RID: 794
	internal class NspiAsyncRpcState_DeleteEntries : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_DeleteEntries,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x00039318 File Offset: 0x00038718
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiDeleteEntriesFlags flags, int mid, IntPtr pEntryIds)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.mid = mid;
			this.pEntryIds = pEntryIds;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x00035D68 File Offset: 0x00035168
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiDeleteEntriesFlags.None;
			this.mid = 0;
			this.pEntryIds = IntPtr.Zero;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003934C File Offset: 0x0003874C
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			byte[][] entryIds = NspiHelper.ConvertCountedEntryIdArrayFromNative(this.pEntryIds);
			base.AsyncDispatch.BeginDeleteEntries(null, this.contextHandle, this.flags, this.mid, entryIds, asyncCallback, this);
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00039394 File Offset: 0x00038794
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			return (int)base.AsyncDispatch.EndDeleteEntries(asyncResult);
		}

		// Token: 0x04000ED2 RID: 3794
		private IntPtr contextHandle;

		// Token: 0x04000ED3 RID: 3795
		private NspiDeleteEntriesFlags flags;

		// Token: 0x04000ED4 RID: 3796
		private int mid;

		// Token: 0x04000ED5 RID: 3797
		private IntPtr pEntryIds;
	}
}
