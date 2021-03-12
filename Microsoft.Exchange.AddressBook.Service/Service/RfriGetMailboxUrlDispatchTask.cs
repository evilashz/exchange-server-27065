using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000023 RID: 35
	internal sealed class RfriGetMailboxUrlDispatchTask : RfriDispatchTask
	{
		// Token: 0x0600013C RID: 316 RVA: 0x000066AA File Offset: 0x000048AA
		public RfriGetMailboxUrlDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriContext context, RfriGetMailboxUrlFlags flags, string hostname, string serverDn) : base(asyncCallback, asyncState, protocolRequestInfo, clientBinding, context)
		{
			this.flags = flags;
			this.hostname = hostname;
			this.serverDn = serverDn;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000066DC File Offset: 0x000048DC
		public RfriStatus End(out string serverUrl)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			serverUrl = this.returnServerUrl;
			return base.Status;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000066F8 File Offset: 0x000048F8
		protected override string TaskName
		{
			get
			{
				return "RfriGetMailboxUrlDispatch";
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00006700 File Offset: 0x00004900
		protected override void InternalDebugTrace()
		{
			RfriDispatchTask.ReferralTracer.TraceDebug((long)base.ContextHandle, "{0} params: flags={1}, hostname={2}, serverDn={3}", new object[]
			{
				this.TaskName,
				this.flags,
				this.hostname,
				this.serverDn
			});
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000678C File Offset: 0x0000498C
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			string localServerUrl = null;
			base.RfriContextCallWrapper("GetMailboxUrl", () => this.Context.GetMailboxUrl(this.hostname, this.serverDn, out localServerUrl));
			if (base.Status == RfriStatus.Success)
			{
				this.returnServerUrl = localServerUrl;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000067DE File Offset: 0x000049DE
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriGetMailboxUrlDispatchTask>(this);
		}

		// Token: 0x040000BA RID: 186
		private const string Name = "RfriGetMailboxUrlDispatch";

		// Token: 0x040000BB RID: 187
		private readonly RfriGetMailboxUrlFlags flags;

		// Token: 0x040000BC RID: 188
		private readonly string hostname;

		// Token: 0x040000BD RID: 189
		private readonly string serverDn;

		// Token: 0x040000BE RID: 190
		private string returnServerUrl = string.Empty;
	}
}
