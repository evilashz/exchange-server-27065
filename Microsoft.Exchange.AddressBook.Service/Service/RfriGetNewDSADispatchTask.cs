using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Nspi.Rfri;
using Microsoft.Exchange.Rpc;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000024 RID: 36
	internal sealed class RfriGetNewDSADispatchTask : RfriDispatchTask
	{
		// Token: 0x06000142 RID: 322 RVA: 0x000067E6 File Offset: 0x000049E6
		public RfriGetNewDSADispatchTask(CancelableAsyncCallback asyncCallback, object asyncState, ProtocolRequestInfo protocolRequestInfo, ClientBinding clientBinding, RfriContext context, RfriGetNewDSAFlags flags, string userDn) : base(asyncCallback, asyncState, protocolRequestInfo, clientBinding, context)
		{
			this.flags = flags;
			this.userDn = userDn;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00006810 File Offset: 0x00004A10
		public RfriStatus End(out string serverDn)
		{
			base.CheckDisposed();
			base.CheckCompletion();
			serverDn = this.returnServerDn;
			return base.Status;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000682C File Offset: 0x00004A2C
		protected override string TaskName
		{
			get
			{
				return "RfriGetNewDSA";
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00006833 File Offset: 0x00004A33
		protected override void InternalDebugTrace()
		{
			RfriDispatchTask.ReferralTracer.TraceDebug<string, RfriGetNewDSAFlags, string>((long)base.ContextHandle, "{0} params: flags={1}, userDn={2}", this.TaskName, this.flags, this.userDn);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00006888 File Offset: 0x00004A88
		protected override void InternalTaskExecute()
		{
			base.InternalTaskExecute();
			string localServerDn = null;
			base.RfriContextCallWrapper("GetNewDSA", () => this.Context.GetNewDSA(this.userDn, out localServerDn));
			if (base.Status == RfriStatus.Success)
			{
				this.returnServerDn = localServerDn;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000068DA File Offset: 0x00004ADA
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RfriGetNewDSADispatchTask>(this);
		}

		// Token: 0x040000BF RID: 191
		private const string Name = "RfriGetNewDSA";

		// Token: 0x040000C0 RID: 192
		private readonly RfriGetNewDSAFlags flags;

		// Token: 0x040000C1 RID: 193
		private readonly string userDn;

		// Token: 0x040000C2 RID: 194
		private string returnServerDn = string.Empty;
	}
}
