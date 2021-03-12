using System;
using Microsoft.Exchange.AddressBook.Nspi;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000016 RID: 22
	internal sealed class NspiModLinkAttDispatchTask : NspiDispatchTask
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00005495 File Offset: 0x00003695
		public NspiModLinkAttDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiModLinkAttFlags flags, PropertyTag propertyTag, int mid, byte[][] rawEntryIds) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
			this.propertyTag = propertyTag;
			this.mid = mid;
			this.rawEntryIds = rawEntryIds;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000054C2 File Offset: 0x000036C2
		public NspiStatus End()
		{
			base.CheckDisposed();
			base.CheckCompletion();
			return base.Status;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000054D6 File Offset: 0x000036D6
		protected override string TaskName
		{
			get
			{
				return "NspiModLinkAtt";
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000054E0 File Offset: 0x000036E0
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug((long)base.ContextHandle, "{0} params: flags={1}, propertyTag={2}, mid={3}", new object[]
			{
				this.TaskName,
				this.flags,
				this.propertyTag,
				this.mid
			});
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000557C File Offset: 0x0000377C
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			EntryId[] entryIds = ConvertHelper.ConvertToMapiEntryIds(this.rawEntryIds);
			PropTag mapiPropTag = (PropTag)this.propertyTag;
			base.NspiContextCallWrapper("ModLinkAtt", () => this.Context.ModLinkAtt(this.flags, mapiPropTag, this.mid, entryIds));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000055D5 File Offset: 0x000037D5
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiModLinkAttDispatchTask>(this);
		}

		// Token: 0x04000077 RID: 119
		private const string Name = "NspiModLinkAtt";

		// Token: 0x04000078 RID: 120
		private readonly NspiModLinkAttFlags flags;

		// Token: 0x04000079 RID: 121
		private readonly PropertyTag propertyTag;

		// Token: 0x0400007A RID: 122
		private readonly int mid;

		// Token: 0x0400007B RID: 123
		private readonly byte[][] rawEntryIds;
	}
}
