using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Nspi;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000019 RID: 25
	internal sealed class NspiQueryRowsDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x000057D8 File Offset: 0x000039D8
		public NspiQueryRowsDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiQueryRowsFlags flags, NspiState state, int[] mids, int rowCount, PropertyTag[] propertyTags) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.mids = mids;
			this.rowCount = rowCount;
			this.propertyTags = propertyTags;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005807 File Offset: 0x00003A07
		public NspiStatus End(out NspiState state, out PropertyValue[][] rowset)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			state = this.returnState;
			rowset = this.returnRowset;
			return base.Status;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000582B File Offset: 0x00003A2B
		protected override string TaskName
		{
			get
			{
				return "NspiQueryRows";
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005832 File Offset: 0x00003A32
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiQueryRowsFlags, int>((long)base.ContextHandle, "{0} params: flags={1}, rowCount={2}", this.TaskName, this.flags, this.rowCount);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000058BC File Offset: 0x00003ABC
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			PropTag[] mapiPropTags = ConvertHelper.ConvertToMapiPropTags(this.propertyTags);
			PropRowSet mapiRowset = null;
			base.NspiContextCallWrapper("QueryRows", () => this.Context.QueryRows(this.flags, this.NspiState, this.mids, this.rowCount, mapiPropTags, out mapiRowset));
			if (base.Status == NspiStatus.Success)
			{
				this.returnState = base.NspiState;
				this.returnRowset = ConvertHelper.ConvertFromMapiPropRowSet(mapiRowset, MarshalHelper.GetString8CodePage(base.NspiState));
				base.TraceNspiState();
				NspiDispatchTask.NspiTracer.TraceDebug<int>((long)base.ContextHandle, "Rows returned: {0}", (mapiRowset == null) ? 0 : mapiRowset.Rows.Count);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005972 File Offset: 0x00003B72
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiQueryRowsDispatchTask>(this);
		}

		// Token: 0x04000084 RID: 132
		private const string Name = "NspiQueryRows";

		// Token: 0x04000085 RID: 133
		private readonly NspiQueryRowsFlags flags;

		// Token: 0x04000086 RID: 134
		private readonly int[] mids;

		// Token: 0x04000087 RID: 135
		private readonly int rowCount;

		// Token: 0x04000088 RID: 136
		private readonly PropertyTag[] propertyTags;

		// Token: 0x04000089 RID: 137
		private NspiState returnState;

		// Token: 0x0400008A RID: 138
		private PropertyValue[][] returnRowset;
	}
}
