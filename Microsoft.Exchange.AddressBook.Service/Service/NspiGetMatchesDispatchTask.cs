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
	// Token: 0x02000011 RID: 17
	internal sealed class NspiGetMatchesDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00004D28 File Offset: 0x00002F28
		public NspiGetMatchesDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiGetMatchesFlags flags, NspiState state, int[] mids, int interfaceOptions, Restriction restriction, IntPtr pPropName, int maxRows, PropertyTag[] propertyTags) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.interfaceOptions = interfaceOptions;
			this.restriction = restriction;
			this.maxRows = maxRows;
			this.propertyTags = propertyTags;
			if (this.maxRows < 0 || this.maxRows > 100000)
			{
				this.maxRows = 100000;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004D8B File Offset: 0x00002F8B
		public NspiStatus End(out NspiState state, out int[] mids, out PropertyValue[][] rowset)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			state = this.returnState;
			mids = this.returnMids;
			rowset = this.returnRowset;
			return base.Status;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00004DB7 File Offset: 0x00002FB7
		protected override string TaskName
		{
			get
			{
				return "NspiGetMatches";
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004DC0 File Offset: 0x00002FC0
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug((long)base.ContextHandle, "{0} params: flags={1}, interfaceOptions={2}, maxRows={3}", new object[]
			{
				this.TaskName,
				this.flags,
				this.interfaceOptions,
				this.maxRows
			});
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004E68 File Offset: 0x00003068
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			Restriction mapiRestriction = null;
			if (this.restriction != null)
			{
				mapiRestriction = ConvertHelper.ConvertToMapiRestriction(this.restriction);
				if (mapiRestriction == null)
				{
					throw new NspiException(NspiStatus.TooComplex, "Restriction too complex");
				}
			}
			int[] midList = null;
			PropRowSet mapiRowset = null;
			PropTag[] mapiPropTags = ConvertHelper.ConvertToMapiPropTags(this.propertyTags);
			base.NspiContextCallWrapper("GetMatches", () => this.Context.GetMatches(this.NspiState, mapiRestriction, this.maxRows, mapiPropTags, out midList, out mapiRowset));
			if (base.Status == NspiStatus.Success)
			{
				this.returnState = base.NspiState;
				this.returnMids = midList;
				this.returnRowset = ConvertHelper.ConvertFromMapiPropRowSet(mapiRowset, MarshalHelper.GetString8CodePage(base.NspiState));
				base.TraceNspiState();
				NspiDispatchTask.NspiTracer.TraceDebug<int>((long)base.ContextHandle, "Rows returned: {0}", (mapiRowset == null) ? 0 : mapiRowset.Rows.Count);
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004F69 File Offset: 0x00003169
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiGetMatchesDispatchTask>(this);
		}

		// Token: 0x04000057 RID: 87
		private const string Name = "NspiGetMatches";

		// Token: 0x04000058 RID: 88
		private const int MaxRows = 100000;

		// Token: 0x04000059 RID: 89
		private readonly NspiGetMatchesFlags flags;

		// Token: 0x0400005A RID: 90
		private readonly int interfaceOptions;

		// Token: 0x0400005B RID: 91
		private readonly Restriction restriction;

		// Token: 0x0400005C RID: 92
		private readonly int maxRows;

		// Token: 0x0400005D RID: 93
		private readonly PropertyTag[] propertyTags;

		// Token: 0x0400005E RID: 94
		private NspiState returnState;

		// Token: 0x0400005F RID: 95
		private int[] returnMids;

		// Token: 0x04000060 RID: 96
		private PropertyValue[][] returnRowset;
	}
}
