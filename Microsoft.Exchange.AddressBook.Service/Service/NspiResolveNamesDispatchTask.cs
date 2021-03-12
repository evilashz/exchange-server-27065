using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200001A RID: 26
	internal sealed class NspiResolveNamesDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x060000FA RID: 250 RVA: 0x0000597A File Offset: 0x00003B7A
		public NspiResolveNamesDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiResolveNamesFlags flags, NspiState state, PropertyTag[] propertyTags, byte[][] names) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.propertyTags = propertyTags;
			this.names = names;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000059A1 File Offset: 0x00003BA1
		public NspiStatus End(out int codePage, out int[] mids, out PropertyValue[][] rowset)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			codePage = this.returnCodePage;
			mids = this.returnMids;
			rowset = this.returnRowset;
			return base.Status;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000059CD File Offset: 0x00003BCD
		protected override string TaskName
		{
			get
			{
				return "NspiResolveNames";
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000059D4 File Offset: 0x00003BD4
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiResolveNamesFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005A3C File Offset: 0x00003C3C
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			PropRowSet mapiRowset = null;
			int[] midList = null;
			PropTag[] mapiPropTags = ConvertHelper.ConvertToMapiPropTags(this.propertyTags);
			base.NspiContextCallWrapper("ResolveNames", () => this.Context.ResolveNames(this.NspiState, mapiPropTags, this.names, out midList, out mapiRowset));
			if (base.Status == NspiStatus.Success)
			{
				this.returnCodePage = base.NspiState.CodePage;
				this.returnMids = midList;
				this.returnRowset = ConvertHelper.ConvertFromMapiPropRowSet(mapiRowset, this.returnCodePage);
			}
			NspiDispatchTask.NspiTracer.TraceDebug<int>((long)base.ContextHandle, "Rows returned: {0}", (mapiRowset == null) ? 0 : mapiRowset.Rows.Count);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005AFF File Offset: 0x00003CFF
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiResolveNamesDispatchTask>(this);
		}

		// Token: 0x0400008B RID: 139
		private const string Name = "NspiResolveNames";

		// Token: 0x0400008C RID: 140
		private readonly NspiResolveNamesFlags flags;

		// Token: 0x0400008D RID: 141
		private readonly PropertyTag[] propertyTags;

		// Token: 0x0400008E RID: 142
		private readonly byte[][] names;

		// Token: 0x0400008F RID: 143
		private int returnCodePage;

		// Token: 0x04000090 RID: 144
		private int[] returnMids;

		// Token: 0x04000091 RID: 145
		private PropertyValue[][] returnRowset;
	}
}
