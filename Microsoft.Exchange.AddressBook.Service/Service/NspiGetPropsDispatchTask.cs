using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000014 RID: 20
	internal sealed class NspiGetPropsDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000051A8 File Offset: 0x000033A8
		public NspiGetPropsDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiGetPropsFlags flags, NspiState state, PropertyTag[] propertyTags) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.propertyTags = propertyTags;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000051C7 File Offset: 0x000033C7
		public NspiStatus End(out int codePage, out PropertyValue[] row)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			codePage = this.returnCodePage;
			row = this.returnRow;
			return base.Status;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000051EB File Offset: 0x000033EB
		protected override string TaskName
		{
			get
			{
				return "NspiGetProps";
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000051F2 File Offset: 0x000033F2
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiGetPropsFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00005254 File Offset: 0x00003454
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			PropRow mapiRow = null;
			PropTag[] mapiPropTags = ConvertHelper.ConvertToMapiPropTags(this.propertyTags);
			base.NspiContextCallWrapper("GetProps", () => this.Context.GetProps(this.flags, this.NspiState, mapiPropTags, out mapiRow));
			if (base.Status == NspiStatus.Success || base.Status == NspiStatus.ErrorsReturned)
			{
				this.returnCodePage = base.NspiState.CodePage;
				this.returnRow = ConvertHelper.ConvertFromMapiPropRow(mapiRow, this.returnCodePage);
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000052E0 File Offset: 0x000034E0
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetPropsDispatchTask>(this);
		}

		// Token: 0x0400006A RID: 106
		private const string Name = "NspiGetProps";

		// Token: 0x0400006B RID: 107
		private readonly NspiGetPropsFlags flags;

		// Token: 0x0400006C RID: 108
		private readonly PropertyTag[] propertyTags;

		// Token: 0x0400006D RID: 109
		private int returnCodePage;

		// Token: 0x0400006E RID: 110
		private PropertyValue[] returnRow;
	}
}
