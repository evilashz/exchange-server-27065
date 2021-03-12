using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000464 RID: 1124
	internal class Rcpt2SmtpCommand : SmtpCommand
	{
		// Token: 0x06003411 RID: 13329 RVA: 0x000D2768 File Offset: 0x000D0968
		public Rcpt2SmtpCommand(ISmtpInSession smtpInSession) : base(smtpInSession, "RCPT2", "OnRcpt2Command", LatencyComponent.SmtpReceiveOnRcpt2Command)
		{
			ArgumentValidator.ThrowIfNull("smtpInSession", smtpInSession);
			this.rcpt2CommandEventArgs = new Rcpt2CommandEventArgs(smtpInSession.SessionSource);
			this.rcpt2CommandEventArgs.MailItem = smtpInSession.TransportMailItemWrapper;
			this.CommandEventArgs = this.rcpt2CommandEventArgs;
			base.IsResponseBuffered = true;
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000D27C8 File Offset: 0x000D09C8
		// (set) Token: 0x06003413 RID: 13331 RVA: 0x000D27D5 File Offset: 0x000D09D5
		internal Dictionary<string, string> ConsumerMailOptionalArguments
		{
			get
			{
				return this.rcpt2CommandEventArgs.ConsumerMailOptionalArguments;
			}
			set
			{
				this.rcpt2CommandEventArgs.ConsumerMailOptionalArguments = value;
			}
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x000D27E3 File Offset: 0x000D09E3
		// (set) Token: 0x06003415 RID: 13333 RVA: 0x000D27F0 File Offset: 0x000D09F0
		internal RoutingAddress RecipientAddress
		{
			get
			{
				return this.rcpt2CommandEventArgs.RecipientAddress;
			}
			set
			{
				this.rcpt2CommandEventArgs.RecipientAddress = value;
			}
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x000D2800 File Offset: 0x000D0A00
		internal override void InboundParseCommand()
		{
			ISmtpInSession smtpInSession = (ISmtpInSession)base.SmtpSession;
			smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.Rcpt2InboundParseCommand);
			if (!SmtpInSessionUtils.ShouldAllowConsumerMail(smtpInSession.Capabilities))
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.Rcpt2NotAuthorized);
				base.SmtpResponse = SmtpResponse.NotAuthorized;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return;
			}
			if (this.VerifyRcpt2ToAlreadyReceived())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.Rcpt2AlreadyReceived);
				return;
			}
			if (!base.VerifyRcptToReceived())
			{
				smtpInSession.DropBreadcrumb(SmtpInSessionBreadcrumbs.WrongSequence);
				return;
			}
			CommandContext commandContext = CommandContext.FromSmtpCommand(this);
			Rcpt2ParseOutput rcpt2ParseOutput;
			ParseResult parseResult;
			using (SmtpInSessionState smtpInSessionState = SmtpInSessionState.FromSmtpInSession(smtpInSession))
			{
				parseResult = Rcpt2SmtpCommandParser.Parse(commandContext, smtpInSessionState, smtpInSession.TransportMailItemWrapper.Recipients[0].Address, smtpInSession.IsDataRedactionNecessary, out rcpt2ParseOutput);
			}
			switch (parseResult.ParsingStatus)
			{
			case ParsingStatus.Complete:
				this.RecipientAddress = rcpt2ParseOutput.RecipientAddress;
				this.ConsumerMailOptionalArguments = rcpt2ParseOutput.ConsumerMailOptionalArguments;
				base.CurrentOffset = commandContext.Offset;
				smtpInSession.SeenRcpt2 = true;
				break;
			case ParsingStatus.IgnorableProtocolError:
				base.SmtpResponse = parseResult.SmtpResponse;
				smtpInSession.SeenRcpt2 = true;
				break;
			default:
				base.SmtpResponse = parseResult.SmtpResponse;
				break;
			}
			base.ParsingStatus = parseResult.ParsingStatus;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x000D2944 File Offset: 0x000D0B44
		internal override void InboundProcessCommand()
		{
			if (base.ParsingStatus != ParsingStatus.Complete)
			{
				return;
			}
			base.SmtpResponse = SmtpResponse.Rcpt2ToOk;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x000D295B File Offset: 0x000D0B5B
		internal override void OutboundCreateCommand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x000D2962 File Offset: 0x000D0B62
		internal override void OutboundFormatCommand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x000D2969 File Offset: 0x000D0B69
		internal override void OutboundProcessResponse()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x000D2970 File Offset: 0x000D0B70
		private bool VerifyRcpt2ToAlreadyReceived()
		{
			ISmtpInSession smtpInSession = base.SmtpSession as ISmtpInSession;
			if (smtpInSession != null && smtpInSession.TransportMailItem != null && smtpInSession.SeenRcpt2)
			{
				base.SmtpResponse = SmtpResponse.Rcpt2AlreadyReceived;
				base.ParsingStatus = ParsingStatus.ProtocolError;
				return true;
			}
			return false;
		}

		// Token: 0x04001A81 RID: 6785
		private readonly Rcpt2CommandEventArgs rcpt2CommandEventArgs;
	}
}
