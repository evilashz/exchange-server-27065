using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000022 RID: 34
	internal sealed class RfriGetFQDNFromLegacyDNDispatchTask : RfriDispatchTask
	{
		// Token: 0x06000136 RID: 310 RVA: 0x000065AE File Offset: 0x000047AE
		public RfriGetFQDNFromLegacyDNDispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriContext context, RfriGetFQDNFromLegacyDNFlags flags, string serverDn) : base(asyncCallback, asyncState, protocolRequestInfo, clientBinding, context)
		{
			this.flags = flags;
			this.serverDn = serverDn;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000065D8 File Offset: 0x000047D8
		public RfriStatus End(out string serverFqdn)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			serverFqdn = this.returnServerFqdn;
			return base.Status;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000138 RID: 312 RVA: 0x000065F4 File Offset: 0x000047F4
		protected override string TaskName
		{
			get
			{
				return "RfriGetFQDNFromLegacyDN";
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000065FB File Offset: 0x000047FB
		protected override void InternalDebugTrace()
		{
			RfriDispatchTask.ReferralTracer.TraceDebug<string, RfriGetFQDNFromLegacyDNFlags, string>((long)base.ContextHandle, "{0} params: flags={1}, serverDn={2}", this.TaskName, this.flags, this.serverDn);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00006650 File Offset: 0x00004850
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			string localServerFqdn = null;
			base.RfriContextCallWrapper("GetFQDNFromLegacyDN", () => this.Context.GetFQDNFromLegacyDN(this.serverDn, out localServerFqdn));
			if (base.Status == RfriStatus.Success)
			{
				this.returnServerFqdn = localServerFqdn;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000066A2 File Offset: 0x000048A2
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriGetFQDNFromLegacyDNDispatchTask>(this);
		}

		// Token: 0x040000B6 RID: 182
		private const string Name = "RfriGetFQDNFromLegacyDN";

		// Token: 0x040000B7 RID: 183
		private readonly RfriGetFQDNFromLegacyDNFlags flags;

		// Token: 0x040000B8 RID: 184
		private readonly string serverDn;

		// Token: 0x040000B9 RID: 185
		private string returnServerFqdn = string.Empty;
	}
}
