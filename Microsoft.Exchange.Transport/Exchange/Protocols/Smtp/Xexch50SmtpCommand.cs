using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Categorizer;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200046A RID: 1130
	internal class Xexch50SmtpCommand : SmtpCommand
	{
		// Token: 0x06003439 RID: 13369 RVA: 0x000D3023 File Offset: 0x000D1223
		public Xexch50SmtpCommand(ISmtpSession session, RecipientCorrelator recipientCorrelator, IMailRouter mailRouter, ITransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration) : base(session, "XEXCH50", null, LatencyComponent.None)
		{
			this.recipientCorrelator = recipientCorrelator;
			this.mailRouter = mailRouter;
			this.transportAppConfig = transportAppConfig;
			this.transportConfiguration = transportConfiguration;
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000D3054 File Offset: 0x000D1254
		public AsyncReturnType RawDataReceived(byte[] data, int offset, int length)
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			int num = this.inboundBlobSize - this.inboundBlobBytesReceived;
			if (num <= 0)
			{
				throw new InvalidOperationException("Exch50 bytes remaining should be greater than 0, but it is: " + num);
			}
			int num2 = Math.Min(num, length);
			if (length != num2)
			{
				smtpInSession.PutBackReceivedBytes(length - num2);
			}
			Array.Copy(data, offset, this.inboundBlob, this.inboundBlobBytesReceived, num2);
			this.inboundBlobBytesReceived += num2;
			ExTraceGlobals.SmtpReceiveTracer.TraceDebug<int, int, int>((long)this.GetHashCode(), "Exch50 consuming {0} bytes out of {1} bytes, {2} total bytes received", num2, length, this.inboundBlobBytesReceived);
			if (this.inboundBlobBytesReceived == this.inboundBlobSize)
			{
				bool flag;
				try
				{
					InboundExch50 inboundExch = new InboundExch50(smtpInSession.TransportMailItem, smtpInSession.SmtpInServer, this.mailRouter, this.transportAppConfig);
					flag = inboundExch.ProcessExch50(this.inboundBlob, this.recipientCorrelator);
					smtpInSession.InboundExch50 = inboundExch;
				}
				catch (Exch50Exception arg)
				{
					ExTraceGlobals.SmtpReceiveTracer.TraceError<Exch50Exception>((long)this.GetHashCode(), "XEXCH50 parsing failed {0}", arg);
					flag = false;
				}
				if (flag)
				{
					base.ParsingStatus = ParsingStatus.Complete;
					base.SmtpResponse = SmtpResponse.Xexch50Success;
				}
				else
				{
					base.ParsingStatus = ParsingStatus.ProtocolError;
					base.SmtpResponse = SmtpResponse.Xexch50Error;
				}
			}
			return AsyncReturnType.Sync;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000D3190 File Offset: 0x000D1390
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.Xexch50InboundParseCommand);
			if ((smtpInSession.Connector.AuthMechanism & (AuthMechanisms.ExchangeServer | AuthMechanisms.ExternalAuthoritative)) == AuthMechanisms.None)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError((long)this.GetHashCode(), "Exch50 is not enabled on the connector");
				base.SmtpResponse = SmtpResponse.Xexch50NotEnabled;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.Xexch50NotEnabled);
				return;
			}
			if (!base.VerifyHelloReceived() || !base.VerifyMailFromReceived() || !base.VerifyRcptToReceived() || !base.VerifyNoOngoingBdat() || !base.VerifyXexch50NotReceived())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			if (!smtpInSession.SmtpInServer.TransportSettings.Xexch50Enabled)
			{
				base.SmtpResponse = SmtpResponse.Xexch50NotEnabled;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.Xexch50NotEnabled);
				return;
			}
			if (!this.CanRelayXEXCH50(smtpInSession))
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.Xexch50NotAuthorized);
				return;
			}
			if (base.IsEndOfCommand())
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError((long)this.GetHashCode(), "Invalid XEXCH50 arguments");
				base.SmtpResponse = SmtpResponse.Xexch50InvalidCommand;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			string nextArg = base.GetNextArg();
			string nextArg2 = base.GetNextArg();
			if (nextArg.Length == 0 || nextArg2.Length == 0 || !base.IsEndOfCommand())
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError((long)this.GetHashCode(), "Invalid XEXCH50 arguments");
				base.SmtpResponse = SmtpResponse.Xexch50InvalidCommand;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			int num;
			int.TryParse(nextArg, out num);
			int num2;
			int.TryParse(nextArg2, out num2);
			int num3 = this.recipientCorrelator.Count + 1;
			if (num > 4 && (long)num <= 67108864L && num2 == num3)
			{
				this.inboundBlobSize = num;
				base.ParsingStatus = ParsingStatus.MoreDataRequired;
				return;
			}
			base.SmtpResponse = SmtpResponse.Xexch50InvalidCommand;
			if (num <= 4 || num2 != num3)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError((long)this.GetHashCode(), "Invalid XEXCH50 arguments");
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			ExTraceGlobals.SmtpReceiveTracer.TraceError<int>((long)this.GetHashCode(), "XEXCH50 size too large - {0}", num);
			base.ParsingStatus = ParsingStatus.Error;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000D3378 File Offset: 0x000D1578
		internal override void InboundProcessCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.Xexch50InboundProcessCommand);
			if (base.ParsingStatus != ParsingStatus.MoreDataRequired)
			{
				return;
			}
			this.inboundBlob = new byte[this.inboundBlobSize];
			base.SmtpResponse = SmtpResponse.Xexch50SendBlob;
			smtpInSession.SetRawModeAfterCommandCompleted(new RawDataHandler(this.RawDataReceived));
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x000D33D1 File Offset: 0x000D15D1
		internal override void OutboundCreateCommand()
		{
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x000D33D4 File Offset: 0x000D15D4
		internal override void OutboundFormatCommand()
		{
			if (this.sentXexch50Command)
			{
				base.ProtocolCommand = this.outboundBlob;
				return;
			}
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			this.outboundBlob = OutboundExch50.GetExch50(smtpOutSession.RoutedMailItem, this.recipientCorrelator.Recipients, this.transportConfiguration, this.mailRouter);
			base.ProtocolCommandString = string.Format(CultureInfo.InvariantCulture, "XEXCH50 {0} {1}", new object[]
			{
				this.outboundBlob.Length,
				this.recipientCorrelator.Count + 1
			});
			ExTraceGlobals.SmtpSendTracer.TraceDebug<string>((long)this.GetHashCode(), "[EXCH50] OUT command: {0}", base.ProtocolCommandString);
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x000D3488 File Offset: 0x000D1688
		internal override void OutboundProcessResponse()
		{
			string statusCode = base.SmtpResponse.StatusCode;
			if (statusCode[0] != '2' && statusCode[0] != '3')
			{
				ExTraceGlobals.SmtpSendTracer.TraceError<SmtpResponse>((long)this.GetHashCode(), "XEXCH50 rejected : {0}", base.SmtpResponse);
				this.SetNextState();
				return;
			}
			if (this.sentXexch50Command)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "XEXCH50 blob accepted");
				this.SetNextState();
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "XEXCH50 command accepted");
			base.ParsingStatus = ParsingStatus.MoreDataRequired;
			this.sentXexch50Command = true;
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x000D3526 File Offset: 0x000D1726
		protected bool CanRelayXEXCH50(ISmtpInSession session)
		{
			if (!SmtpInSessionUtils.HasSMTPAcceptEXCH50Permission(session.Permissions))
			{
				base.SmtpResponse = SmtpResponse.Xexch50NotAuthorized;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return false;
			}
			return true;
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x000D354C File Offset: 0x000D174C
		private void SetNextState()
		{
			SmtpOutSession smtpOutSession = (SmtpOutSession)base.SmtpSession;
			if (!smtpOutSession.AdvertisedEhloOptions.Chunking)
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Setting NextState: DATA");
				smtpOutSession.NextState = SmtpOutSession.SessionState.Data;
				return;
			}
			if (smtpOutSession.HasMoreBlobsPending())
			{
				ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Setting NextState: XBdatBlob");
				smtpOutSession.NextState = SmtpOutSession.SessionState.XBdatBlob;
				return;
			}
			ExTraceGlobals.SmtpSendTracer.TraceDebug((long)this.GetHashCode(), "Setting NextState: BDAT");
			smtpOutSession.NextState = SmtpOutSession.SessionState.Bdat;
		}

		// Token: 0x04001A85 RID: 6789
		public const string CommandKeyword = "XEXCH50";

		// Token: 0x04001A86 RID: 6790
		private const uint MaxXexch50BlobSize = 67108864U;

		// Token: 0x04001A87 RID: 6791
		private int inboundBlobSize;

		// Token: 0x04001A88 RID: 6792
		private int inboundBlobBytesReceived;

		// Token: 0x04001A89 RID: 6793
		private byte[] inboundBlob;

		// Token: 0x04001A8A RID: 6794
		private bool sentXexch50Command;

		// Token: 0x04001A8B RID: 6795
		private byte[] outboundBlob;

		// Token: 0x04001A8C RID: 6796
		private readonly RecipientCorrelator recipientCorrelator;

		// Token: 0x04001A8D RID: 6797
		private readonly IMailRouter mailRouter;

		// Token: 0x04001A8E RID: 6798
		private readonly ITransportAppConfig transportAppConfig;

		// Token: 0x04001A8F RID: 6799
		private readonly ITransportConfiguration transportConfiguration;
	}
}
