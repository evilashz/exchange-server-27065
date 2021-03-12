using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200001B RID: 27
	internal sealed class NspiResolveNamesWDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00005B07 File Offset: 0x00003D07
		public NspiResolveNamesWDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiResolveNamesFlags flags, NspiState state, PropertyTag[] propertyTags, string[] names) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.propertyTags = propertyTags;
			this.names = names;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005B2E File Offset: 0x00003D2E
		public NspiStatus End(out int codePage, out int[] mids, out PropertyValue[][] rowset)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			codePage = this.returnCodePage;
			mids = this.returnMids;
			rowset = this.returnRowset;
			return base.Status;
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005B5A File Offset: 0x00003D5A
		protected override string TaskName
		{
			get
			{
				return "NspiResolveNamesW";
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005B61 File Offset: 0x00003D61
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiResolveNamesFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005BC8 File Offset: 0x00003DC8
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

		// Token: 0x06000105 RID: 261 RVA: 0x00005C8B File Offset: 0x00003E8B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiResolveNamesWDispatchTask>(this);
		}

		// Token: 0x04000092 RID: 146
		private const string Name = "NspiResolveNamesW";

		// Token: 0x04000093 RID: 147
		private readonly NspiResolveNamesFlags flags;

		// Token: 0x04000094 RID: 148
		private readonly PropertyTag[] propertyTags;

		// Token: 0x04000095 RID: 149
		private readonly string[] names;

		// Token: 0x04000096 RID: 150
		private int returnCodePage;

		// Token: 0x04000097 RID: 151
		private int[] returnMids;

		// Token: 0x04000098 RID: 152
		private PropertyValue[][] returnRowset;
	}
}
