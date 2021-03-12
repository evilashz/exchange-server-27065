using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000021 RID: 33
	internal sealed class RfriGetAddressBookUrlDispatchTask : RfriDispatchTask
	{
		// Token: 0x06000130 RID: 304 RVA: 0x0000649D File Offset: 0x0000469D
		public RfriGetAddressBookUrlDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriContext context, RfriGetAddressBookUrlFlags flags, string hostname, string userDn) : base(asyncCallback, asyncState, protocolRequestInfo, clientBinding, context)
		{
			this.flags = flags;
			this.hostname = hostname;
			this.userDn = userDn;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000064CF File Offset: 0x000046CF
		public RfriStatus End(out string serverUrl)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			serverUrl = this.returnServerUrl;
			return base.Status;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000064EB File Offset: 0x000046EB
		protected override string TaskName
		{
			get
			{
				return "RfriGetAddressBookUrlDispatch";
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000064F2 File Offset: 0x000046F2
		protected override void InternalDebugTrace()
		{
			RfriDispatchTask.ReferralTracer.TraceDebug<string, RfriGetAddressBookUrlFlags, string>((long)base.ContextHandle, "{0} params: flags={1}, userDn={2}", this.TaskName, this.flags, this.userDn);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006554 File Offset: 0x00004754
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			string localServerUrl = null;
			base.RfriContextCallWrapper("GetAddressBookUrl", () => this.Context.GetAddressBookUrl(this.hostname, this.userDn, out localServerUrl));
			if (base.Status == RfriStatus.Success)
			{
				this.returnServerUrl = localServerUrl;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000065A6 File Offset: 0x000047A6
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriGetAddressBookUrlDispatchTask>(this);
		}

		// Token: 0x040000B1 RID: 177
		private const string Name = "RfriGetAddressBookUrlDispatch";

		// Token: 0x040000B2 RID: 178
		private readonly RfriGetAddressBookUrlFlags flags;

		// Token: 0x040000B3 RID: 179
		private readonly string hostname;

		// Token: 0x040000B4 RID: 180
		private readonly string userDn;

		// Token: 0x040000B5 RID: 181
		private string returnServerUrl = string.Empty;
	}
}
