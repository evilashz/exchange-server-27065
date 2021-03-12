using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200001E RID: 30
	internal sealed class NspiUnbindDispatchTask : NspiDispatchTask
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00005F71 File Offset: 0x00004171
		public NspiUnbindDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, NspiContext context, NspiUnbindFlags flags) : base(asyncCallback, asyncState, protocolRequestInfo, context)
		{
			this.flags = flags;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005F86 File Offset: 0x00004186
		public NspiStatus End(out int contextHandle)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			if (base.Status == NspiStatus.Success)
			{
				base.IsContextRundown = true;
				contextHandle = 0;
			}
			else
			{
				contextHandle = base.ContextHandle;
			}
			if (base.Status == NspiStatus.Success)
			{
				return NspiStatus.UnbindSuccess;
			}
			return base.Status;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00005FC0 File Offset: 0x000041C0
		protected override string TaskName
		{
			get
			{
				return "NspiUnbind";
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005FC7 File Offset: 0x000041C7
		protected override void InternalDebugTrace()
		{
			NspiDispatchTask.NspiTracer.TraceDebug<string, NspiUnbindFlags>((long)base.ContextHandle, "{0} params: flags={1}", this.TaskName, this.flags);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005FEB File Offset: 0x000041EB
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			base.Context.Unbind(false);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005FFF File Offset: 0x000041FF
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<NspiUnbindDispatchTask>(this);
		}

		// Token: 0x040000A5 RID: 165
		private const string Name = "NspiUnbind";

		// Token: 0x040000A6 RID: 166
		private readonly NspiUnbindFlags flags;
	}
}
