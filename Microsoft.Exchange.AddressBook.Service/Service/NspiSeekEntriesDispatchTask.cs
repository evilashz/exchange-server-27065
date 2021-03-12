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
	// Token: 0x0200001D RID: 29
	internal sealed class NspiSeekEntriesDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x0600010C RID: 268 RVA: 0x00005DA4 File Offset: 0x00003FA4
		public NspiSeekEntriesDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiSeekEntriesFlags flags, NspiState state, PropertyValue? target, int[] restriction, PropertyTag[] propertyTags) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.target = target;
			this.restriction = restriction;
			this.propertyTags = propertyTags;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005DD3 File Offset: 0x00003FD3
		public NspiStatus End(out NspiState state, out PropertyValue[][] rowset)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			state = this.returnState;
			rowset = this.returnRowset;
			return base.Status;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005DF7 File Offset: 0x00003FF7
		protected override string TaskName
		{
			get
			{
				return "NspiSeekEntries";
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005DFE File Offset: 0x00003FFE
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiSeekEntriesFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005E64 File Offset: 0x00004064
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			if (this.target == null)
			{
				NspiDispatchTask.NspiTracer.TraceError((long)base.ContextHandle, "Target is null");
				throw new NspiException(NspiStatus.GeneralFailure, "Target is null");
			}
			PropValue mapiPropValue = ConvertHelper.ConvertToMapiPropValue(this.target.Value);
			PropTag[] mapiPropTags = ConvertHelper.ConvertToMapiPropTags(this.propertyTags);
			PropRowSet mapiRowset = null;
			base.NspiContextCallWrapper("SeekEntries", () => this.Context.SeekEntries(this.NspiState, mapiPropValue, this.restriction, mapiPropTags, out mapiRowset));
			if (base.Status == NspiStatus.Success)
			{
				this.returnState = base.NspiState;
				this.returnRowset = ConvertHelper.ConvertFromMapiPropRowSet(mapiRowset, MarshalHelper.GetString8CodePage(base.NspiState));
				base.TraceNspiState();
				NspiDispatchTask.NspiTracer.TraceDebug<int>((long)base.ContextHandle, "Rows returned: {0}", (mapiRowset == null) ? 0 : mapiRowset.Rows.Count);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005F69 File Offset: 0x00004169
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiSeekEntriesDispatchTask>(this);
		}

		// Token: 0x0400009E RID: 158
		private const string Name = "NspiSeekEntries";

		// Token: 0x0400009F RID: 159
		private readonly NspiSeekEntriesFlags flags;

		// Token: 0x040000A0 RID: 160
		private readonly PropertyValue? target;

		// Token: 0x040000A1 RID: 161
		private readonly int[] restriction;

		// Token: 0x040000A2 RID: 162
		private readonly PropertyTag[] propertyTags;

		// Token: 0x040000A3 RID: 163
		private NspiState returnState;

		// Token: 0x040000A4 RID: 164
		private PropertyValue[][] returnRowset;
	}
}
