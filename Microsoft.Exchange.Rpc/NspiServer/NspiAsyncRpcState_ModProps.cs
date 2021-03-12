using System;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.Rpc.NspiServer
{
	// Token: 0x02000312 RID: 786
	internal class NspiAsyncRpcState_ModProps : BaseAsyncRpcState<Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcState_ModProps,Microsoft::Exchange::Rpc::NspiServer::NspiAsyncRpcServer,Microsoft::Exchange::Rpc::INspiAsyncDispatch>
	{
		// Token: 0x06000EAC RID: 3756 RVA: 0x00038F2C File Offset: 0x0003832C
		public void Initialize(SafeRpcAsyncStateHandle asyncState, NspiAsyncRpcServer asyncServer, IntPtr contextHandle, NspiModPropsFlags flags, IntPtr pState, IntPtr pPropTags, IntPtr pRow)
		{
			base.InternalInitialize(asyncState, asyncServer);
			this.contextHandle = contextHandle;
			this.flags = flags;
			this.pState = pState;
			this.pPropTags = pPropTags;
			this.pRow = pRow;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00035C5C File Offset: 0x0003505C
		public override void InternalReset()
		{
			this.contextHandle = IntPtr.Zero;
			this.flags = NspiModPropsFlags.None;
			this.pState = IntPtr.Zero;
			this.pPropTags = IntPtr.Zero;
			this.pRow = IntPtr.Zero;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00038F68 File Offset: 0x00038368
		public override void InternalBegin(CancelableAsyncCallback asyncCallback)
		{
			NspiState state = null;
			PropertyTag[] propertyTags = null;
			PropertyValue[] row = null;
			if (this.pState != IntPtr.Zero)
			{
				state = new NspiState(this.pState);
			}
			if (this.pPropTags != IntPtr.Zero)
			{
				propertyTags = MarshalHelper.ConvertSPropTagArrayToPropertyTagArray(this.pPropTags);
			}
			if (this.pRow != IntPtr.Zero)
			{
				row = MarshalHelper.ConvertSRowToPropertyValueArray(this.pRow, MarshalHelper.GetString8CodePage(state));
			}
			base.AsyncDispatch.BeginModProps(null, this.contextHandle, this.flags, state, propertyTags, row, asyncCallback, this);
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x0003901C File Offset: 0x0003841C
		public override int InternalEnd(ICancelableAsyncResult asyncResult)
		{
			return (int)base.AsyncDispatch.EndModProps(asyncResult);
		}

		// Token: 0x04000E9B RID: 3739
		private IntPtr contextHandle;

		// Token: 0x04000E9C RID: 3740
		private NspiModPropsFlags flags;

		// Token: 0x04000E9D RID: 3741
		private IntPtr pState;

		// Token: 0x04000E9E RID: 3742
		private IntPtr pPropTags;

		// Token: 0x04000E9F RID: 3743
		private IntPtr pRow;
	}
}
