using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000010 RID: 16
	internal sealed class Pop3RequestQuit : Pop3Request
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000038AA File Offset: 0x00001AAA
		public Pop3RequestQuit(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_QUIT_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_QUIT_Failures;
			base.AllowedStates = (Pop3State.Nonauthenticated | Pop3State.User | Pop3State.Authenticated | Pop3State.AuthenticatedButFailed);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000038DE File Offset: 0x00001ADE
		public override bool NeedsStoreConnection
		{
			get
			{
				return base.Factory.SessionState == Pop3State.Authenticated;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000038F0 File Offset: 0x00001AF0
		public override ProtocolResponse Process()
		{
			int? itemsDeleted = null;
			if (base.Factory.SessionState == Pop3State.Authenticated)
			{
				itemsDeleted = new int?(base.Factory.DeleteMarkedMessages());
			}
			if (base.Session.LightLogSession != null)
			{
				base.Session.LightLogSession.ItemsDeleted = itemsDeleted;
			}
			ProtocolResponse protocolResponse = new Pop3Response(Pop3Response.Type.ok, "Microsoft Exchange Server 2013 POP3 server signing off.");
			protocolResponse.IsDisconnectResponse = true;
			base.Factory.SessionState = Pop3State.Disconnected;
			return protocolResponse;
		}

		// Token: 0x0400004B RID: 75
		internal const string QUITResponse = "Microsoft Exchange Server 2013 POP3 server signing off.";
	}
}
