using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.MessagingPolicies.AddressRewrite
{
	// Token: 0x02000022 RID: 34
	internal sealed class AgentInbound : SmtpReceiveAgent
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000559D File Offset: 0x0000379D
		internal AgentInbound(SmtpServer server)
		{
			this.server = server;
			this.currentConfig = Configuration.Current;
			base.OnRcptCommand += this.RewriteRecipient;
			base.OnEndOfHeaders += this.RewriteP2Addresses;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000055DC File Offset: 0x000037DC
		private void RewriteRecipient(ReceiveCommandEventSource source, RcptCommandEventArgs args)
		{
			if (this.currentConfig == null)
			{
				ExTraceGlobals.AddressRewritingTracer.TraceError((long)this.GetHashCode(), "Rejecting recipient as we have no configuration");
				source.RejectCommand(SmtpResponse.ServiceUnavailable);
				return;
			}
			MailItem mailItem = args.MailItem;
			if (args.SmtpSession.AuthenticationSource != AuthenticationSource.Anonymous && RewriteHelper.IsSenderInternal(mailItem, this.server))
			{
				ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "RCPT TO:InboundAddressRewrite skipped as sender was internal, and the message is therefore going Outbound");
				return;
			}
			ExTraceGlobals.AddressRewritingTracer.TraceDebug<RoutingAddress>((long)this.GetHashCode(), "Processing RCPT TO: {0} for message", args.RecipientAddress);
			string text = this.currentConfig.RewriteInbound(args.RecipientAddress);
			if (!string.IsNullOrEmpty(text))
			{
				ExTraceGlobals.AddressRewritingTracer.TraceDebug<RoutingAddress, string>((long)this.GetHashCode(), "Rewriting RCPT TO: {0} -> {1}", args.RecipientAddress, text);
				args.RecipientAddress = RoutingAddress.Parse(text);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000056AC File Offset: 0x000038AC
		private void RewriteP2Addresses(ReceiveMessageEventSource source, EndOfHeadersEventArgs args)
		{
			if (ExTraceGlobals.AddressRewritingTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				if (args.MailItem.Message != null && args.MailItem.Message.MessageId != null)
				{
					ExTraceGlobals.AddressRewritingTracer.TraceDebug<string>((long)this.GetHashCode(), "MessageId: {0}, Address Rewriting this Message", args.MailItem.Message.MessageId);
				}
				else
				{
					ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "MessageId: <null>, Address Rewriting this Message");
				}
			}
			if (args.SmtpSession.AuthenticationSource != AuthenticationSource.Anonymous && RewriteHelper.IsSenderInternal(args.MailItem, this.server))
			{
				ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "EOH:InboundAddressRewrite skipped as sender was internal, and the message is therefore going Outbound");
				return;
			}
			ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Rewriting P2 addresses for message");
			HeaderList headers = args.Headers;
			try
			{
				ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Rewriting the To: header");
				RewriteHelper.RewriteHeader(headers.FindFirst(HeaderId.To), this.currentConfig, MapTable.MapEntryType.External);
				ExTraceGlobals.AddressRewritingTracer.TraceDebug((long)this.GetHashCode(), "Rewriting the Cc: header");
				RewriteHelper.RewriteHeader(headers.FindFirst(HeaderId.Cc), this.currentConfig, MapTable.MapEntryType.External);
			}
			catch (ExchangeDataException ex)
			{
				ExTraceGlobals.AddressRewritingTracer.TraceError<string>((long)this.GetHashCode(), "Unable to rewrite message headers during Inbound address-rewriting. The message may be malformed. Exception message: {0}", ex.ToString());
				source.RejectMessage(SmtpResponse.InvalidContent);
			}
		}

		// Token: 0x04000072 RID: 114
		private SmtpServer server;

		// Token: 0x04000073 RID: 115
		private Configuration currentConfig;
	}
}
