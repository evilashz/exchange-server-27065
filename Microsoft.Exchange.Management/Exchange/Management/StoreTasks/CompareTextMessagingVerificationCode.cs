using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.SQM;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.VersionedXml;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007B7 RID: 1975
	[Cmdlet("Compare", "TextMessagingVerificationCode", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class CompareTextMessagingVerificationCode : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x0011D57C File Offset: 0x0011B77C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageCompareTextMessagingVerificationCode(this.Identity.ToString());
			}
		}

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x0011D598 File Offset: 0x0011B798
		// (set) Token: 0x0600457E RID: 17790 RVA: 0x0011D5D8 File Offset: 0x0011B7D8
		[Parameter(ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0, ParameterSetName = "Identity")]
		public override MailboxIdParameter Identity
		{
			get
			{
				if (base.Identity != null)
				{
					return base.Identity;
				}
				ADObjectId adObjectId;
				if (!base.TryGetExecutingUserId(out adObjectId))
				{
					throw new ExecutingUserPropertyNotFoundException("executingUserid");
				}
				return base.Identity = new MailboxIdParameter(adObjectId);
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x0011D5E1 File Offset: 0x0011B7E1
		// (set) Token: 0x06004580 RID: 17792 RVA: 0x0011D5F8 File Offset: 0x0011B7F8
		[Parameter(Mandatory = true)]
		public string VerificationCode
		{
			get
			{
				return (string)base.Fields["VerificationCode"];
			}
			set
			{
				base.Fields["VerificationCode"] = value;
			}
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x0011D60C File Offset: 0x0011B80C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(XsoStoreDataProviderBase.GetExchangePrincipalWithAdSessionSettingsForOrg(base.SessionSettings.CurrentOrganizationId, this.DataObject), (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken, "Compare-TextMessagingVerificationCode"))
			{
				TextMessagingAccount textMessagingAccount = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(this.DataObject.Identity);
				IList<PossibleRecipient> effectivePossibleRecipients = textMessagingAccount.TextMessagingSettings.MachineToPersonMessagingPolicies.EffectivePossibleRecipients;
				if (effectivePossibleRecipients.Count == 0)
				{
					base.WriteError(new NotificationPhoneNumberAbsentException(this.Identity.ToString()), ErrorCategory.InvalidData, this.Identity);
				}
				PossibleRecipient possibleRecipient = effectivePossibleRecipients[0];
				bool flag = false;
				if (string.IsNullOrEmpty(possibleRecipient.Passcode))
				{
					base.WriteError(new VerificationCodeNeverSentException(this.Identity.ToString()), ErrorCategory.InvalidData, this.Identity);
				}
				DateTime utcNow = DateTime.UtcNow;
				if (6 <= PossibleRecipient.CountTimesSince(possibleRecipient.PasscodeVerificationFailedTimeHistory, utcNow - TimeSpan.FromDays(1.0), true))
				{
					base.WriteError(new VerificationCodeTooManyFailedException(), ErrorCategory.InvalidData, this.Identity);
				}
				if (string.Equals(possibleRecipient.Passcode, this.VerificationCode, StringComparison.InvariantCultureIgnoreCase))
				{
					possibleRecipient.SetAcknowledged(true);
					ADUser dataObject = this.DataObject;
					SmsSqmDataPointHelper.AddNotificationTurningOnDataPoint(SmsSqmSession.Instance, dataObject.Id, dataObject.LegacyExchangeDN, textMessagingAccount);
				}
				else
				{
					possibleRecipient.PasscodeVerificationFailedTimeHistory.Add(utcNow);
					flag = true;
				}
				versionedXmlDataProvider.Save(textMessagingAccount);
				if (flag)
				{
					base.WriteError(new VerificationCodeUnmatchException(this.VerificationCode), ErrorCategory.InvalidData, this.Identity);
				}
				else if (!textMessagingAccount.EasEnabled)
				{
					TextMessagingHelper.SendSystemTextMessage(versionedXmlDataProvider.MailboxSession, textMessagingAccount.NotificationPhoneNumber, Strings.CalendarNotificationConfirmation.ToString(textMessagingAccount.NotificationPreferredCulture ?? TextMessagingHelper.GetSupportedUserCulture(this.DataObject)), true);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x0011D7F4 File Offset: 0x0011B9F4
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x04002ADC RID: 10972
		private const string ParamVerificationCode = "VerificationCode";
	}
}
