using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200001C RID: 28
	internal sealed class NspiResortRestrictionDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x06000106 RID: 262 RVA: 0x00005C93 File Offset: 0x00003E93
		public NspiResortRestrictionDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiResortRestrictionFlags flags, NspiState state, int[] mids) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.mids = mids;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005CB2 File Offset: 0x00003EB2
		public NspiStatus End(out NspiState state, out int[] mids)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			state = this.returnState;
			mids = this.returnMids;
			return base.Status;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005CD6 File Offset: 0x00003ED6
		protected override string TaskName
		{
			get
			{
				return "NspiResortRestriction";
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005CDD File Offset: 0x00003EDD
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiResortRestrictionFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005D38 File Offset: 0x00003F38
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			int[] localMids = null;
			base.NspiContextCallWrapper("ResortRestriction", () => this.Context.ResortRestriction(this.NspiState, this.mids, out localMids));
			if (base.Status == NspiStatus.Success)
			{
				this.returnState = base.NspiState;
				this.returnMids = localMids;
				base.TraceNspiState();
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005D9C File Offset: 0x00003F9C
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiResortRestrictionDispatchTask>(this);
		}

		// Token: 0x04000099 RID: 153
		private const string Name = "NspiResortRestriction";

		// Token: 0x0400009A RID: 154
		private readonly NspiResortRestrictionFlags flags;

		// Token: 0x0400009B RID: 155
		private readonly int[] mids;

		// Token: 0x0400009C RID: 156
		private NspiState returnState;

		// Token: 0x0400009D RID: 157
		private int[] returnMids;
	}
}
