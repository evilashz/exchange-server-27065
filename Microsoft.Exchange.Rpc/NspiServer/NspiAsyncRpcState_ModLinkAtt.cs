using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000318 RID: 792
	internal class NspiAsyncRpcState_ModLinkAtt : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_ModLinkAtt,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x00039258 File Offset: 0x00038658
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiModLinkAttFlags flags, int propTag, int mid, IntPtr pEntryIds)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.propTag = propTag;
			this.mid = mid;
			this.pEntryIds = pEntryIds;
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x00035D30 File Offset: 0x00035130
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiModLinkAttFlags.None;
			this.propTag = 0;
			this.mid = 0;
			this.pEntryIds = IntPtr.Zero;
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x00039294 File Offset: 0x00038694
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			PropertyTag propertyTag = new PropertyTag((uint)this.propTag);
			byte[][] entryIds = NspiHelper.ConvertCountedEntryIdArrayFromNative(this.pEntryIds);
			base.AsyncDispatch.BeginModLinkAtt(null, this.contextHandle, this.flags, propertyTag, this.mid, entryIds, asyncCallback, this);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000392E8 File Offset: 0x000386E8
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			return (int)base.AsyncDispatch.EndModLinkAtt(asyncResult);
		}

		// Token: 0x04000EC5 RID: 3781
		private IntPtr contextHandle;

		// Token: 0x04000EC6 RID: 3782
		private NspiModLinkAttFlags flags;

		// Token: 0x04000EC7 RID: 3783
		private int propTag;

		// Token: 0x04000EC8 RID: 3784
		private int mid;

		// Token: 0x04000EC9 RID: 3785
		private IntPtr pEntryIds;
	}
}
