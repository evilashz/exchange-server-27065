using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000014 RID: 20
	internal sealed class Pop3RequestStls : Pop3Request
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003C43 File Offset: 0x00001E43
		public Pop3RequestStls(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_STLS_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_STLS_Failures;
			base.AllowedStates = Pop3State.Nonauthenticated;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003C76 File Offset: 0x00001E76
		public override bool VerifyState()
		{
			return base.VerifyState() && !base.Factory.Session.IsTls && base.Factory.Session.VirtualServer.Certificate != null;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003CAF File Offset: 0x00001EAF
		public override ProtocolResponse Process()
		{
			base.Factory.Session.OkToIssueRead = false;
			base.Factory.Session.SendToClient(new StringResponseItem("+OK Begin TLS negotiation.", new BaseSession.SendCompleteDelegate(this.StartSsl)));
			return null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003CEC File Offset: 0x00001EEC
		private void StartSsl()
		{
			if (base.Factory.Session.StartSsl())
			{
				return;
			}
			ProtocolResponse protocolResponse = new Pop3Response(Pop3Response.Type.err, "Server configuration error.\r\nConnection is closed. 22");
			protocolResponse.IsDisconnectResponse = true;
			base.Factory.Session.BeginShutdown(protocolResponse.DataToSend);
			protocolResponse.Dispose();
		}

		// Token: 0x04000050 RID: 80
		internal const string STLSResponse = "+OK Begin TLS negotiation.";

		// Token: 0x04000051 RID: 81
		internal const string STLSFailed = "Server configuration error.\r\nConnection is closed. 22";
	}
}
