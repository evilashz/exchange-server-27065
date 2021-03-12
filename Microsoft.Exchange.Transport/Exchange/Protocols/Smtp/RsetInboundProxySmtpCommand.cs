using System;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004A4 RID: 1188
	internal class RsetInboundProxySmtpCommand : SmtpCommand
	{
		// Token: 0x060035BE RID: 13758 RVA: 0x000DD5CF File Offset: 0x000DB7CF
		public RsetInboundProxySmtpCommand(ISmtpSession session) : base(session, "RSET", "OnRsetCommand", LatencyComponent.None)
		{
			base.IsResponseBuffered = true;
			this.CommandEventArgs = new RsetCommandEventArgs();
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000DD5F5 File Offset: 0x000DB7F5
		internal override void InboundParseCommand()
		{
			throw new InvalidOperationException("This Rset command handler should never be called for an inbound session");
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000DD601 File Offset: 0x000DB801
		internal override void InboundProcessCommand()
		{
			throw new InvalidOperationException("This Rset command handler should never be called for an inbound session");
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000DD60D File Offset: 0x000DB80D
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000DD60F File Offset: 0x000DB80F
		internal override void OutboundFormatCommand()
		{
			base.ProtocolCommandString = "RSET";
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000DD61C File Offset: 0x000DB81C
		internal override void OutboundProcessResponse()
		{
			InboundProxySmtpOutSession inboundProxySmtpOutSession = (InboundProxySmtpOutSession)base.SmtpSession;
			if (inboundProxySmtpOutSession.AdvertisedEhloOptions.XProxyFrom)
			{
				inboundProxySmtpOutSession.NextState = SmtpOutSession.SessionState.XProxyFrom;
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Setting Next State: XProxyFrom");
				return;
			}
			if (!inboundProxySmtpOutSession.BetweenMessagesRset)
			{
				throw new InvalidOperationException("Error, unexpected call to RSET");
			}
			inboundProxySmtpOutSession.BetweenMessagesRset = false;
			if (inboundProxySmtpOutSession.RoutedMailItem == null)
			{
				throw new InvalidOperationException("Must call PrepareForNextMessage when issuing RSET between messages");
			}
			inboundProxySmtpOutSession.NextState = SmtpOutSession.SessionState.MessageStart;
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Setting Next State: MessageStart");
		}
	}
}
