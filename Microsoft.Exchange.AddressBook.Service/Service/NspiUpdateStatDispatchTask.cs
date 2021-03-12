using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200001F RID: 31
	internal sealed class NspiUpdateStatDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x06000118 RID: 280 RVA: 0x00006007 File Offset: 0x00004207
		public NspiUpdateStatDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiUpdateStatFlags flags, NspiState state, bool deltaRequested) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.deltaRequested = deltaRequested;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006032 File Offset: 0x00004232
		public NspiStatus End(out NspiState state, out int? delta)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			state = this.returnState;
			delta = this.returnedDelta;
			return base.Status;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000605A File Offset: 0x0000425A
		protected override string TaskName
		{
			get
			{
				return "NspiUpdateStat";
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006061 File Offset: 0x00004261
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiUpdateStatFlags, bool>((long)base.ContextHandle, "{0} params: flags={1}, deltaRequested={2}", this.TaskName, this.flags, this.deltaRequested);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000060CC File Offset: 0x000042CC
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			if (!this.deltaRequested)
			{
				base.NspiContextCallWrapper("UpdateStat", () => base.Context.UpdateStat(base.NspiState));
			}
			else
			{
				int localDelta = 0;
				base.NspiContextCallWrapper("UpdateStat", () => this.Context.UpdateStat(this.NspiState, out localDelta));
				if (base.Status == NspiStatus.Success)
				{
					this.returnedDelta = new int?(localDelta);
				}
			}
			if (base.Status == NspiStatus.Success)
			{
				this.returnState = base.NspiState;
				base.TraceNspiState();
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00006165 File Offset: 0x00004365
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiUpdateStatDispatchTask>(this);
		}

		// Token: 0x040000A7 RID: 167
		private const string Name = "NspiUpdateStat";

		// Token: 0x040000A8 RID: 168
		private readonly NspiUpdateStatFlags flags;

		// Token: 0x040000A9 RID: 169
		private readonly bool deltaRequested;

		// Token: 0x040000AA RID: 170
		private NspiState returnState;

		// Token: 0x040000AB RID: 171
		private int? returnedDelta = null;
	}
}
