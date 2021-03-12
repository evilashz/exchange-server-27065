using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200000C RID: 12
	internal sealed class NspiCompareDNTsDispatchTask : NspiStateDispatchTask
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00004818 File Offset: 0x00002A18
		public NspiCompareDNTsDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiCompareDNTsFlags flags, NspiState state, int mid1, int mid2) : base(asyncCallback, asyncState, protocolRequestInfo, context, state)
		{
			this.flags = flags;
			this.mid1 = mid1;
			this.mid2 = mid2;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000483F File Offset: 0x00002A3F
		public NspiStatus End(out int result)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			result = this.result;
			return base.Status;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000485B File Offset: 0x00002A5B
		protected override string TaskName
		{
			get
			{
				return "NspiCompareDNTs";
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004864 File Offset: 0x00002A64
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug((long)base.ContextHandle, "{0} params: flags={1}, mid1={2}, mid2={3}", new object[]
			{
				this.TaskName,
				this.flags,
				this.mid1,
				this.mid2
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004904 File Offset: 0x00002B04
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			int localResult = 0;
			base.NspiContextCallWrapper("CompareMids", () => this.Context.CompareMids(this.NspiState, this.mid1, this.mid2, out localResult));
			if (base.Status == NspiStatus.Success)
			{
				this.result = localResult;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004956 File Offset: 0x00002B56
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiCompareDNTsDispatchTask>(this);
		}

		// Token: 0x0400003F RID: 63
		private const string Name = "NspiCompareDNTs";

		// Token: 0x04000040 RID: 64
		private readonly NspiCompareDNTsFlags flags;

		// Token: 0x04000041 RID: 65
		private readonly int mid1;

		// Token: 0x04000042 RID: 66
		private readonly int mid2;

		// Token: 0x04000043 RID: 67
		private int result;
	}
}
