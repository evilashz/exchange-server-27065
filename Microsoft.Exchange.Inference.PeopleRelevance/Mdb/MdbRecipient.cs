using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal class MdbRecipient : IMessageRecipient, IEquatable<IMessageRecipient>
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000020D0 File Offset: 0x000002D0
		public MdbRecipient(Recipient recipient) : this(recipient.Participant)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000020E0 File Offset: 0x000002E0
		public MdbRecipient(Participant participant)
		{
			Util.ThrowOnNullArgument(participant, "participant");
			this.displayName = participant.DisplayName;
			this.emailAddress = participant.EmailAddress;
			this.routingType = participant.RoutingType;
			this.smtpAddress = participant.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress);
			if (string.IsNullOrEmpty(this.smtpAddress) && this.routingType == "SMTP")
			{
				this.smtpAddress = this.emailAddress;
			}
			this.sipUri = participant.GetValueOrDefault<string>(ParticipantSchema.SipUri);
			this.alias = participant.GetValueOrDefault<string>(ParticipantSchema.Alias);
			this.isDistributionList = MdbRecipient.IsDistributionListParticipant(participant);
			this.recipientDisplayType = participant.GetValueOrDefault<RecipientDisplayType>(ParticipantSchema.DisplayTypeEx);
			this.ComputeIdentity();
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021A4 File Offset: 0x000003A4
		public MdbRecipient(IExchangePrincipal principal, CultureInfo culture)
		{
			Util.ThrowOnNullArgument(principal, "principal");
			Participant participant = new Participant(principal);
			this.displayName = participant.DisplayName;
			this.emailAddress = participant.EmailAddress;
			this.routingType = participant.RoutingType;
			this.smtpAddress = participant.GetValueOrDefault<string>(ParticipantSchema.SmtpAddress);
			if (string.IsNullOrEmpty(this.smtpAddress) && this.routingType == "SMTP")
			{
				this.smtpAddress = this.emailAddress;
			}
			this.sipUri = participant.GetValueOrDefault<string>(ParticipantSchema.SipUri);
			this.alias = participant.GetValueOrDefault<string>(ParticipantSchema.Alias);
			this.isDistributionList = MdbRecipient.IsDistributionListParticipant(participant);
			this.recipientDisplayType = participant.GetValueOrDefault<RecipientDisplayType>(ParticipantSchema.DisplayTypeEx);
			this.organizationalId = principal.MailboxInfo.OrganizationId;
			this.cultureInfo = culture;
			this.ComputeIdentity();
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002288 File Offset: 0x00000488
		public MdbRecipient(IMessageRecipient recipient)
		{
			Util.ThrowOnNullArgument(recipient, "recipient");
			this.displayName = recipient.DisplayName;
			this.emailAddress = recipient.EmailAddress;
			this.routingType = recipient.RoutingType;
			this.smtpAddress = recipient.SmtpAddress;
			if (string.IsNullOrEmpty(this.smtpAddress) && this.routingType == "SMTP")
			{
				this.smtpAddress = this.emailAddress;
			}
			this.sipUri = recipient.SipUri;
			this.alias = recipient.Alias;
			this.isDistributionList = recipient.IsDistributionList;
			this.recipientDisplayType = recipient.RecipientDisplayType;
			this.ComputeIdentity();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002337 File Offset: 0x00000537
		public MdbRecipient(string smtpAddress, string emailAddress, string displayName)
		{
			this.smtpAddress = smtpAddress;
			this.emailAddress = emailAddress;
			this.displayName = displayName;
			this.routingType = "EX";
			this.isDistributionList = false;
			this.recipientDisplayType = RecipientDisplayType.MailboxUser;
			this.ComputeIdentity();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002373 File Offset: 0x00000573
		public IIdentity Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000237B File Offset: 0x0000057B
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002383 File Offset: 0x00000583
		public string EmailAddress
		{
			get
			{
				return this.emailAddress;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000238B File Offset: 0x0000058B
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002393 File Offset: 0x00000593
		public string RoutingType
		{
			get
			{
				return this.routingType;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000239B File Offset: 0x0000059B
		public string SipUri
		{
			get
			{
				return this.sipUri;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000023A3 File Offset: 0x000005A3
		public string Alias
		{
			get
			{
				return this.alias;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000023AB File Offset: 0x000005AB
		public OrganizationId OrganizationalId
		{
			get
			{
				return this.organizationalId;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000023B3 File Offset: 0x000005B3
		public CultureInfo CultureInformation
		{
			get
			{
				return this.cultureInfo;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023BB File Offset: 0x000005BB
		public bool IsDistributionList
		{
			get
			{
				return this.isDistributionList;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023C3 File Offset: 0x000005C3
		public RecipientDisplayType RecipientDisplayType
		{
			get
			{
				return this.recipientDisplayType;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023CC File Offset: 0x000005CC
		public static bool operator ==(MdbRecipient left, MdbRecipient right)
		{
			return left == right || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023F2 File Offset: 0x000005F2
		public static bool operator !=(MdbRecipient left, MdbRecipient right)
		{
			return !(left == right);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023FE File Offset: 0x000005FE
		public override bool Equals(object comparand)
		{
			return comparand is IMessageRecipient && this.Equals((IMessageRecipient)comparand);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002416 File Offset: 0x00000616
		public bool Equals(IMessageRecipient other)
		{
			return other != null && this.Identity.Equals(other.Identity);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000242E File Offset: 0x0000062E
		public override int GetHashCode()
		{
			return this.Identity.GetHashCode();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000243C File Offset: 0x0000063C
		public virtual void UpdateFromRecipient(IMessageRecipient recipient)
		{
			IIdentity identity = this.identity;
			this.displayName = recipient.DisplayName;
			this.emailAddress = recipient.EmailAddress;
			this.routingType = recipient.RoutingType;
			this.smtpAddress = recipient.SmtpAddress;
			this.sipUri = recipient.SipUri;
			this.alias = recipient.Alias;
			this.isDistributionList = recipient.IsDistributionList;
			this.recipientDisplayType = recipient.RecipientDisplayType;
			this.ComputeIdentity();
			Util.ThrowOnConditionFailed(identity.Equals(this.Identity), "Updates of a recipient are not allowed to change the recipient identity");
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024CC File Offset: 0x000006CC
		private static bool IsDistributionListParticipant(Participant participant)
		{
			bool? valueAsNullable = participant.GetValueAsNullable<bool>(ParticipantSchema.IsDistributionList);
			return (valueAsNullable != null && valueAsNullable.Value) || 0 == string.CompareOrdinal(participant.RoutingType, "MAPIPDL");
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000250C File Offset: 0x0000070C
		private void ComputeIdentity()
		{
			this.identity = new MdbRecipientIdentity(this.smtpAddress);
		}

		// Token: 0x04000006 RID: 6
		[NonSerialized]
		private readonly OrganizationId organizationalId;

		// Token: 0x04000007 RID: 7
		[NonSerialized]
		private readonly CultureInfo cultureInfo;

		// Token: 0x04000008 RID: 8
		private string smtpAddress;

		// Token: 0x04000009 RID: 9
		private string routingType;

		// Token: 0x0400000A RID: 10
		private bool isDistributionList;

		// Token: 0x0400000B RID: 11
		private string displayName;

		// Token: 0x0400000C RID: 12
		private string emailAddress;

		// Token: 0x0400000D RID: 13
		private string sipUri;

		// Token: 0x0400000E RID: 14
		private string alias;

		// Token: 0x0400000F RID: 15
		private RecipientDisplayType recipientDisplayType;

		// Token: 0x04000010 RID: 16
		private MdbRecipientIdentity identity;
	}
}
