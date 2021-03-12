using System;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ContentAggregation;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.SendAsVerification;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim
{
	// Token: 0x020000D2 RID: 210
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PimAggregationSubscription : AggregationSubscription, ISendAsSource
	{
		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0001E5E3 File Offset: 0x0001C7E3
		public override SmtpAddress Email
		{
			get
			{
				return this.userEmailAddress;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0001E5EC File Offset: 0x0001C7EC
		public override string Domain
		{
			get
			{
				string text = this.userEmailAddress.ToString();
				if (string.IsNullOrEmpty(text))
				{
					return string.Empty;
				}
				return text.Substring(text.IndexOf("@") + 1);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0001E62C File Offset: 0x0001C82C
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0001E634 File Offset: 0x0001C834
		public string UserDisplayName
		{
			get
			{
				return this.userDisplayName;
			}
			set
			{
				this.userDisplayName = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0001E63D File Offset: 0x0001C83D
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0001E645 File Offset: 0x0001C845
		public SmtpAddress UserEmailAddress
		{
			get
			{
				return this.userEmailAddress;
			}
			set
			{
				this.userEmailAddress = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0001E64E File Offset: 0x0001C84E
		public Guid SourceGuid
		{
			get
			{
				return base.SubscriptionGuid;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001E656 File Offset: 0x0001C856
		public bool IsEnabled
		{
			get
			{
				return base.Status != AggregationStatus.Disabled && base.Status != AggregationStatus.Poisonous;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001E66F File Offset: 0x0001C86F
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0001E67C File Offset: 0x0001C87C
		public string LogonPassword
		{
			get
			{
				return SyncUtilities.SecureStringToString(this.LogonPasswordSecured);
			}
			set
			{
				this.LogonPasswordSecured = SyncUtilities.StringToSecureString(value);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0001E68A File Offset: 0x0001C88A
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0001E692 File Offset: 0x0001C892
		public virtual SecureString LogonPasswordSecured
		{
			get
			{
				return this.logonPasswordSecured;
			}
			set
			{
				this.logonPasswordSecured = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0001E69B File Offset: 0x0001C89B
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0001E6A3 File Offset: 0x0001C8A3
		public virtual SendAsState SendAsState
		{
			get
			{
				return this.sendAsState;
			}
			set
			{
				this.sendAsState = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0001E6B4 File Offset: 0x0001C8B4
		public virtual VerificationEmailState VerificationEmailState
		{
			get
			{
				return this.verificationEmailState;
			}
			set
			{
				this.verificationEmailState = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0001E6BD File Offset: 0x0001C8BD
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0001E6C5 File Offset: 0x0001C8C5
		public string VerificationEmailMessageId
		{
			get
			{
				return this.verificationEmailMessageId;
			}
			set
			{
				this.verificationEmailMessageId = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0001E6CE File Offset: 0x0001C8CE
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0001E6D6 File Offset: 0x0001C8D6
		public DateTime? VerificationEmailTimeStamp
		{
			get
			{
				return this.verificationEmailTimestamp;
			}
			set
			{
				this.verificationEmailTimestamp = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0001E6DF File Offset: 0x0001C8DF
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0001E6E7 File Offset: 0x0001C8E7
		public virtual string SendAsValidatedEmail
		{
			get
			{
				return this.sendAsValidatedEmail;
			}
			set
			{
				this.sendAsValidatedEmail = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0001E6F0 File Offset: 0x0001C8F0
		public virtual string VerifiedUserName
		{
			get
			{
				throw new NotSupportedException("Verified user name not supported for this subscription.  Type: " + base.SubscriptionType.ToString());
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x0001E711 File Offset: 0x0001C911
		public virtual string VerifiedIncomingServer
		{
			get
			{
				throw new NotSupportedException("Verified incoming server not supported for this subscription.  Type: " + base.SubscriptionType.ToString());
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0001E732 File Offset: 0x0001C932
		public virtual bool SendAsNeedsVerification
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x0001E735 File Offset: 0x0001C935
		public sealed override bool IsMirrored
		{
			get
			{
				return base.AggregationType == AggregationType.Mirrored;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0001E741 File Offset: 0x0001C941
		public override FolderSupport FolderSupport
		{
			get
			{
				throw new InvalidOperationException("PimAggregation is a base aggregation class, no FolderSupport information is available.");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0001E74D File Offset: 0x0001C94D
		public override ItemSupport ItemSupport
		{
			get
			{
				throw new InvalidOperationException("PimAggregation is a base aggregation class, no ItemSupport information is available.");
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0001E759 File Offset: 0x0001C959
		public override SyncQuirks SyncQuirks
		{
			get
			{
				throw new InvalidOperationException("PimAggregation is a base aggregation class, no SyncQuirks information is available.");
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001E765 File Offset: 0x0001C965
		public virtual bool PasswordRequired
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0001E768 File Offset: 0x0001C968
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0001E782 File Offset: 0x0001C982
		internal string EncryptedLogonPassword
		{
			get
			{
				string result;
				if (!this.TryEncryptPassword(out result))
				{
					return null;
				}
				return result;
			}
			set
			{
				this.LogonPasswordSecured = this.DecryptPassword(value);
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001E791 File Offset: 0x0001C991
		public virtual PimSubscriptionProxy CreateSubscriptionProxy()
		{
			return new PimSubscriptionProxy(this);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001E799 File Offset: 0x0001C999
		public IEmailSender CreateEmailSenderFor(ADUser subscriptionAdUser, ExchangePrincipal subscriptionExchangePrincipal)
		{
			if (this.SendAsNeedsVerification)
			{
				return new EmailSender(this, subscriptionAdUser, subscriptionExchangePrincipal, CommonLoggingHelper.SyncLogSession.OpenWithContext(Guid.Empty, this));
			}
			return EmailSender.NullEmailSender;
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001E7C4 File Offset: 0x0001C9C4
		public string GetUserAddressInRfc822SmtpFormat()
		{
			Participant participant = new Participant(this.UserDisplayName, this.UserEmailAddress.ToString(), "SMTP");
			return participant.ToString(AddressFormat.Rfc822Smtp);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001E800 File Offset: 0x0001CA00
		protected override void SetPropertiesToMessageObject(MessageItem message)
		{
			base.SetPropertiesToMessageObject(message);
			message[AggregationSubscriptionMessageSchema.SharingInitiatorName] = this.UserDisplayName;
			message[AggregationSubscriptionMessageSchema.SharingInitiatorSmtp] = this.UserEmailAddress.ToString();
			string value;
			if (this.LogonPasswordSecured != null && this.LogonPasswordSecured.Length > 0 && this.TryEncryptPassword(out value))
			{
				message[AggregationSubscriptionMessageSchema.SharingRemotePass] = value;
			}
			message[MessageItemSchema.SharingSendAsState] = this.sendAsState;
			message[MessageItemSchema.SharingSendAsValidatedEmail] = this.sendAsValidatedEmail;
			message[AggregationSubscriptionMessageSchema.SharingSendAsVerificationEmailState] = this.verificationEmailState;
			message[AggregationSubscriptionMessageSchema.SharingSendAsVerificationMessageId] = this.verificationEmailMessageId;
			if (this.verificationEmailTimestamp == null)
			{
				message[AggregationSubscriptionMessageSchema.SharingSendAsVerificationTimestamp] = SyncUtilities.ExZeroTime;
				return;
			}
			message[AggregationSubscriptionMessageSchema.SharingSendAsVerificationTimestamp] = new ExDateTime(ExTimeZone.UtcTimeZone, this.VerificationEmailTimeStamp.Value.ToUniversalTime());
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x0001E914 File Offset: 0x0001CB14
		protected override bool LoadMinimumInfo(MessageItem message)
		{
			bool result = base.LoadMinimumInfo(message);
			try
			{
				base.GetSmtpAddressProperty(message, AggregationSubscriptionMessageSchema.SharingInitiatorSmtp, out this.userEmailAddress);
			}
			catch (SyncPropertyValidationException ex)
			{
				this.userEmailAddress = SmtpAddress.Parse(PimAggregationSubscription.DefaultEmailAddress);
				base.SetPropertyLoadError(ex.Property, ex.Value);
				result = false;
			}
			try
			{
				base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingInitiatorName, false, new uint?(0U), new uint?(256U), out this.userDisplayName);
			}
			catch (SyncPropertyValidationException ex2)
			{
				this.userDisplayName = this.UserEmailAddress.ToString();
				base.SetPropertyLoadError(ex2.Property, ex2.Value);
				result = false;
			}
			return result;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001E9D8 File Offset: 0x0001CBD8
		protected override void LoadProperties(MessageItem message)
		{
			base.LoadProperties(message);
			if (!PropertyError.IsPropertyError(message.TryGetProperty(MessageItemSchema.SharingSendAsState)))
			{
				base.GetEnumProperty<SendAsState>(message, MessageItemSchema.SharingSendAsState, null, out this.sendAsState);
				base.GetStringProperty(message, MessageItemSchema.SharingSendAsValidatedEmail, true, null, null, out this.sendAsValidatedEmail);
			}
			bool flag = base.SubscriptionType == AggregationSubscriptionType.DeltaSyncMail || base.SubscriptionType == AggregationSubscriptionType.Facebook || base.SubscriptionType == AggregationSubscriptionType.LinkedIn;
			string text;
			base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingRemotePass, flag, flag, null, null, out text);
			if (string.IsNullOrEmpty(text))
			{
				this.LogonPasswordSecured = new SecureString();
				if (!flag)
				{
					CommonLoggingHelper.SyncLogSession.LogError((TSLID)158UL, ExTraceGlobals.SubscriptionManagerTracer, "Unexpected Password value for Subscription: ID {0}, Name {1}, Protocol {2}, Version {3}. The logon password is empty.", new object[]
					{
						base.SubscriptionGuid,
						base.Name,
						base.SubscriptionProtocolName,
						base.Version
					});
				}
			}
			else
			{
				this.EncryptedLogonPassword = text;
			}
			CommonLoggingHelper.SyncLogSession.LogDebugging((TSLID)79UL, ExTraceGlobals.SubscriptionManagerTracer, "Subscription credentials Loaded: ID {0}, Name {1}, Protocol {2}.", new object[]
			{
				base.SubscriptionGuid,
				base.Name,
				base.SubscriptionProtocolName
			});
			if (base.Version >= 2L)
			{
				base.GetEnumProperty<VerificationEmailState>(message, AggregationSubscriptionMessageSchema.SharingSendAsVerificationEmailState, null, out this.verificationEmailState);
			}
			if (base.Version >= 3L)
			{
				base.GetStringProperty(message, AggregationSubscriptionMessageSchema.SharingSendAsVerificationMessageId, true, null, null, out this.verificationEmailMessageId);
				ExDateTime? exDateTime;
				base.GetExDateTimeProperty(message, AggregationSubscriptionMessageSchema.SharingSendAsVerificationTimestamp, out exDateTime);
				this.verificationEmailTimestamp = (DateTime?)exDateTime;
			}
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001EBB9 File Offset: 0x0001CDB9
		protected override void SetMinimumInfoToMessageObject(MessageItem message)
		{
			base.SetMinimumInfoToMessageObject(message);
			message[AggregationSubscriptionMessageSchema.SharingInitiatorSmtp] = this.userEmailAddress.ToString();
			message[AggregationSubscriptionMessageSchema.SharingInitiatorName] = this.userDisplayName;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x0001EBEF File Offset: 0x0001CDEF
		protected override void Serialize(AggregationSubscription.SubscriptionSerializer subscriptionSerializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001EBF6 File Offset: 0x0001CDF6
		protected override void Deserialize(AggregationSubscription.SubscriptionDeserializer deserializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001EC00 File Offset: 0x0001CE00
		private SecureString DecryptPassword(string encryptedString)
		{
			SecureString result;
			Exception ex;
			if (this.TryEncryptedStringToSecureString(encryptedString, out result, out ex))
			{
				return result;
			}
			CommonLoggingHelper.SyncLogSession.LogError((TSLID)78UL, ExTraceGlobals.SubscriptionManagerTracer, "Failed to Decrypt Password due to error: {0}, for Subscription: ID {1}, Name {2}, Protocol {3}, Version {4}.", new object[]
			{
				ex,
				base.SubscriptionGuid,
				base.Name,
				base.SubscriptionProtocolName,
				base.Version
			});
			return new SecureString();
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001EC78 File Offset: 0x0001CE78
		private bool TryEncryptPassword(out string encryptedPassword)
		{
			Exception ex;
			if (this.TrySecureStringToEncryptedString(this.LogonPasswordSecured, out encryptedPassword, out ex))
			{
				return true;
			}
			CommonLoggingHelper.SyncLogSession.LogError((TSLID)77UL, ExTraceGlobals.SubscriptionCacheMessageTracer, "Failed to encrypt password. Subscription: '{0}', Name {1}, Protocol {2}. Error: {3}", new object[]
			{
				base.SubscriptionGuid,
				base.Name,
				base.SubscriptionProtocolName,
				ex
			});
			return false;
		}

		// Token: 0x0400035D RID: 861
		private const int MaxStringLength = 256;

		// Token: 0x0400035E RID: 862
		internal static readonly string DefaultEmailAddress = "missing@no-email.microsoft.com";

		// Token: 0x0400035F RID: 863
		private string userDisplayName;

		// Token: 0x04000360 RID: 864
		private SendAsState sendAsState;

		// Token: 0x04000361 RID: 865
		private VerificationEmailState verificationEmailState = VerificationEmailState.EmailNotSent;

		// Token: 0x04000362 RID: 866
		private string verificationEmailMessageId = string.Empty;

		// Token: 0x04000363 RID: 867
		private DateTime? verificationEmailTimestamp;

		// Token: 0x04000364 RID: 868
		private string sendAsValidatedEmail = string.Empty;

		// Token: 0x04000365 RID: 869
		private SmtpAddress userEmailAddress;

		// Token: 0x04000366 RID: 870
		[NonSerialized]
		private SecureString logonPasswordSecured;
	}
}
