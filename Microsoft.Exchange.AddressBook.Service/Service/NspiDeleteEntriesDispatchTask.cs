using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200000D RID: 13
	internal sealed class NspiDeleteEntriesDispatchTask : NspiDispatchTask
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x0000495E File Offset: 0x00002B5E
		public NspiDeleteEntriesDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiDeleteEntriesFlags flags, int mid, byte[][] entryIds) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
			this.mid = mid;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000497B File Offset: 0x00002B7B
		public NspiStatus End()
		{
			base.CheckDisposed();
			base.CheckCompletion();
			return base.Status;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000498F File Offset: 0x00002B8F
		protected override string TaskName
		{
			get
			{
				return "NspiDeleteEntries";
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004996 File Offset: 0x00002B96
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiDeleteEntriesFlags, int>((long)base.ContextHandle, "{0} params: flags={1}, mid={2}", this.TaskName, this.flags, this.mid);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000049C7 File Offset: 0x00002BC7
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			base.NspiContextCallWrapper("DeleteEntries", () => NspiStatus.NotSupported);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000049F7 File Offset: 0x00002BF7
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiDeleteEntriesDispatchTask>(this);
		}

		// Token: 0x04000044 RID: 68
		private const string Name = "NspiDeleteEntries";

		// Token: 0x04000045 RID: 69
		private readonly NspiDeleteEntriesFlags flags;

		// Token: 0x04000046 RID: 70
		private readonly int mid;
	}
}
