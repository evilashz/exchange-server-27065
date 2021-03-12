using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200083B RID: 2107
	internal class ParticipantInformation
	{
		// Token: 0x06003CC3 RID: 15555 RVA: 0x000D63C0 File Offset: 0x000D45C0
		internal ParticipantInformation(string displayName, string routingType, string emailAddress, ParticipantOrigin participantOrigin, bool? demoted, string sipUri, bool? submitted) : this(displayName, routingType, emailAddress, participantOrigin, demoted, sipUri, submitted, null)
		{
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x000D63E8 File Offset: 0x000D45E8
		internal ParticipantInformation(string displayName, string routingType, string emailAddress, ParticipantOrigin participantOrigin, bool? demoted, string sipUri, bool? submitted, MailboxHelper.MailboxTypeType? mailboxType)
		{
			ExTraceGlobals.ParticipantLookupBatchingTracer.TraceDebug((long)this.GetHashCode(), "ParticipantInformation constructed - Hashcode = {0}; DisplayName = {1}; RoutingType = {2}; EmailAddress = {3}; ParticipantOrigin (type) = {4}; Demoted = {5}; SipUri = {6}; Submitted = {7}", new object[]
			{
				this.GetHashCode(),
				displayName,
				routingType,
				emailAddress,
				participantOrigin,
				(demoted == null) ? "<NULL>" : demoted.Value.ToString(),
				sipUri ?? string.Empty,
				(submitted == null) ? "<NULL>" : submitted.Value.ToString()
			});
			this.displayName = displayName;
			this.routingType = routingType;
			this.emailAddress = emailAddress;
			this.participantOrigin = participantOrigin;
			this.demoted = demoted;
			this.sipUri = sipUri;
			this.Submitted = submitted;
			this.mailboxType = mailboxType;
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003CC5 RID: 15557 RVA: 0x000D64C7 File Offset: 0x000D46C7
		internal string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003CC6 RID: 15558 RVA: 0x000D64CF File Offset: 0x000D46CF
		internal string RoutingType
		{
			get
			{
				return this.routingType;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003CC7 RID: 15559 RVA: 0x000D64D7 File Offset: 0x000D46D7
		internal string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x000D64DF File Offset: 0x000D46DF
		internal ParticipantOrigin Origin
		{
			get
			{
				return this.participantOrigin;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003CC9 RID: 15561 RVA: 0x000D64E7 File Offset: 0x000D46E7
		internal bool? Demoted
		{
			get
			{
				return this.demoted;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06003CCA RID: 15562 RVA: 0x000D64EF File Offset: 0x000D46EF
		internal string SipUri
		{
			get
			{
				return this.sipUri;
			}
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000D64F7 File Offset: 0x000D46F7
		// (set) Token: 0x06003CCC RID: 15564 RVA: 0x000D64FF File Offset: 0x000D46FF
		internal bool? Submitted { get; private set; }

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06003CCD RID: 15565 RVA: 0x000D6508 File Offset: 0x000D4708
		internal MailboxHelper.MailboxTypeType MailboxType
		{
			get
			{
				if (this.mailboxType == null)
				{
					this.mailboxType = new MailboxHelper.MailboxTypeType?(MailboxHelper.GetMailboxType(this.Origin, this.RoutingType));
				}
				return this.mailboxType.Value;
			}
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x000D6540 File Offset: 0x000D4740
		internal static ParticipantInformation CreateSmtpParticipant(IParticipant participant, IExchangePrincipal mailboxIndentity)
		{
			MailboxHelper.MailboxTypeType mailboxTypeType = MailboxHelper.ConvertToMailboxType(mailboxIndentity.RecipientType, mailboxIndentity.RecipientTypeDetails);
			RemoteUserMailboxPrincipal remoteUserMailboxPrincipal = CallContext.Current.AccessingPrincipal as RemoteUserMailboxPrincipal;
			if (remoteUserMailboxPrincipal != null)
			{
				return ParticipantInformation.CreateSmtpParticipant(participant, remoteUserMailboxPrincipal.DisplayName, remoteUserMailboxPrincipal.PrimarySmtpAddress.ToString(), mailboxTypeType);
			}
			return ParticipantInformation.CreateSmtpParticipant(participant, mailboxIndentity.MailboxInfo.DisplayName, mailboxIndentity.MailboxInfo.PrimarySmtpAddress.ToString(), mailboxTypeType);
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x000D65C0 File Offset: 0x000D47C0
		internal static ParticipantInformation Create(IParticipant participant, ExchangePrincipal mailboxIndentity)
		{
			string text = participant.SipUri;
			MailboxHelper.MailboxTypeType value = MailboxHelper.ConvertToMailboxType(mailboxIndentity.RecipientType, mailboxIndentity.RecipientTypeDetails);
			RemoteUserMailboxPrincipal remoteUserMailboxPrincipal = CallContext.Current.AccessingPrincipal as RemoteUserMailboxPrincipal;
			if (remoteUserMailboxPrincipal != null)
			{
				return new ParticipantInformation(remoteUserMailboxPrincipal.DisplayName, "SMTP", remoteUserMailboxPrincipal.PrimarySmtpAddress.ToString(), participant.Origin, null, text, new bool?(participant.Submitted), new MailboxHelper.MailboxTypeType?(value));
			}
			return new ParticipantInformation(mailboxIndentity.MailboxInfo.DisplayName, "SMTP", mailboxIndentity.MailboxInfo.PrimarySmtpAddress.ToString(), participant.Origin, null, text, new bool?(participant.Submitted), new MailboxHelper.MailboxTypeType?(value));
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x000D6694 File Offset: 0x000D4894
		internal static ParticipantInformation CreateSmtpParticipant(IParticipant participant, string displayName, string smtpAddress, MailboxHelper.MailboxTypeType mailboxType)
		{
			return new ParticipantInformation(displayName, "SMTP", smtpAddress, participant.Origin, null, participant.SipUri, new bool?(participant.Submitted), new MailboxHelper.MailboxTypeType?(mailboxType));
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x000D66D4 File Offset: 0x000D48D4
		internal static ParticipantInformation Create(IParticipant participant, MailboxHelper.MailboxTypeType mailboxType, bool? demoted = null)
		{
			if (participant.SmtpEmailAddress != null)
			{
				return new ParticipantInformation(participant.DisplayName, "SMTP", participant.SmtpEmailAddress, participant.Origin, new bool?(true), participant.SipUri, new bool?(participant.Submitted), new MailboxHelper.MailboxTypeType?(mailboxType));
			}
			return new ParticipantInformation(participant.DisplayName, participant.RoutingType, participant.EmailAddress, participant.Origin, demoted, participant.SipUri, new bool?(participant.Submitted), new MailboxHelper.MailboxTypeType?(mailboxType));
		}

		// Token: 0x04002184 RID: 8580
		private readonly string displayName;

		// Token: 0x04002185 RID: 8581
		private readonly string routingType;

		// Token: 0x04002186 RID: 8582
		private readonly string emailAddress;

		// Token: 0x04002187 RID: 8583
		private readonly ParticipantOrigin participantOrigin;

		// Token: 0x04002188 RID: 8584
		private readonly bool? demoted;

		// Token: 0x04002189 RID: 8585
		private readonly string sipUri;

		// Token: 0x0400218A RID: 8586
		private MailboxHelper.MailboxTypeType? mailboxType;
	}
}
