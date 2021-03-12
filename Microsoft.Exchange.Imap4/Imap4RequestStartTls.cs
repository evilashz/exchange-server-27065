using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Imap4
{
	// Token: 0x02000041 RID: 65
	internal sealed class Imap4RequestStartTls : Imap4Request
	{
		// Token: 0x0600027E RID: 638 RVA: 0x000126B7 File Offset: 0x000108B7
		public Imap4RequestStartTls(Imap4ResponseFactory factory, string tag, string data) : base(factory, tag, data)
		{
			this.PerfCounterTotal = base.Imap4CountersInstance.PerfCounter_STARTTLS_Total;
			this.PerfCounterFailures = base.Imap4CountersInstance.PerfCounter_STARTTLS_Failures;
			base.AllowedStates = Imap4State.Nonauthenticated;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000126EB File Offset: 0x000108EB
		public override bool VerifyState()
		{
			return base.VerifyState() && !base.Factory.Session.IsTls && base.Factory.Session.VirtualServer.Certificate != null;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00012724 File Offset: 0x00010924
		public override ProtocolResponse Process()
		{
			base.Factory.Session.OkToIssueRead = false;
			base.Factory.Session.SendToClient(new StringResponseItem(base.Tag + " OK Begin TLS negotiation now.", new BaseSession.SendCompleteDelegate(this.StartSsl)));
			return null;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00012778 File Offset: 0x00010978
		private void StartSsl()
		{
			if (base.Factory.Session.StartSsl())
			{
				return;
			}
			ProtocolResponse protocolResponse = new Imap4Response(this, Imap4Response.Type.bad, "Could not establish TLS connection.\r\n* BYE Connection is closed. 15");
			protocolResponse.IsDisconnectResponse = true;
			base.Factory.Session.BeginShutdown(protocolResponse.DataToSend);
			protocolResponse.Dispose();
		}

		// Token: 0x040001EB RID: 491
		internal const string STARTTLSResponse = " OK Begin TLS negotiation now.";

		// Token: 0x040001EC RID: 492
		internal const string STARTTLSFailed = "Could not establish TLS connection.\r\n* BYE Connection is closed. 15";
	}
}
