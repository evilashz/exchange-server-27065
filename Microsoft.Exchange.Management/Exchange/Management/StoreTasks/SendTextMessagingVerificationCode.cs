using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
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
	// Token: 0x020007B9 RID: 1977
	[Cmdlet("Send", "TextMessagingVerificationCode", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SendTextMessagingVerificationCode : RecipientObjectActionTask<MailboxIdParameter, ADUser>
	{
		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x06004585 RID: 17797 RVA: 0x0011D81C File Offset: 0x0011BA1C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSendTextMessagingVerificationCode(this.Identity.ToString());
			}
		}

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x06004587 RID: 17799 RVA: 0x0011D838 File Offset: 0x0011BA38
		// (set) Token: 0x06004588 RID: 17800 RVA: 0x0011D878 File Offset: 0x0011BA78
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

		// Token: 0x06004589 RID: 17801 RVA: 0x0011D884 File Offset: 0x0011BA84
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			using (VersionedXmlDataProvider versionedXmlDataProvider = new VersionedXmlDataProvider(XsoStoreDataProviderBase.GetExchangePrincipalWithAdSessionSettingsForOrg(base.SessionSettings.CurrentOrganizationId, this.DataObject), (base.ExchangeRunspaceConfig == null) ? null : base.ExchangeRunspaceConfig.SecurityAccessToken, "Send-TextMessagingVerificationCode"))
			{
				TextMessagingAccount textMessagingAccount = (TextMessagingAccount)versionedXmlDataProvider.Read<TextMessagingAccount>(this.DataObject.Identity);
				IList<PossibleRecipient> effectivePossibleRecipients = textMessagingAccount.TextMessagingSettings.MachineToPersonMessagingPolicies.EffectivePossibleRecipients;
				if (effectivePossibleRecipients.Count == 0)
				{
					base.WriteError(new NotificationPhoneNumberAbsentException(this.Identity.ToString()), ErrorCategory.InvalidData, this.Identity);
				}
				PossibleRecipient possibleRecipient = effectivePossibleRecipients[0];
				DateTime utcNow = DateTime.UtcNow;
				if (3 <= PossibleRecipient.CountTimesSince(possibleRecipient.PasscodeSentTimeHistory, utcNow - TimeSpan.FromMinutes(30.0), false))
				{
					this.WriteWarning(new VerificationCodeSentTooManyTimesException(textMessagingAccount.NotificationPhoneNumber.ToString()).LocalizedString);
					TaskLogger.LogExit();
					return;
				}
				if (PossibleRecipient.CountTimesSince(possibleRecipient.PasscodeSentTimeHistory, utcNow - TimeSpan.FromDays(1.0), true) == 0)
				{
					if (DateTime.UtcNow > SendTextMessagingVerificationCode.timeCreatingRandom + TimeSpan.FromSeconds((double)SendTextMessagingVerificationCode.timeSpan))
					{
						SendTextMessagingVerificationCode.random = new Random();
						SendTextMessagingVerificationCode.timeCreatingRandom = DateTime.UtcNow;
						SendTextMessagingVerificationCode.timeSpan = new Random().Next(60);
					}
					possibleRecipient.SetPasscode(SendTextMessagingVerificationCode.random.Next(999999).ToString("000000"));
				}
				possibleRecipient.PasscodeSentTimeHistory.Add(utcNow);
				try
				{
					TextMessagingHelper.SendSystemTextMessage(versionedXmlDataProvider.MailboxSession, textMessagingAccount.NotificationPhoneNumber, Strings.PasscodeInformation(possibleRecipient.Passcode).ToString(textMessagingAccount.NotificationPreferredCulture ?? TextMessagingHelper.GetSupportedUserCulture(this.DataObject)), false);
				}
				catch (SendAsDeniedException innerException)
				{
					base.WriteError(new TextMessageInsufficientPermissionException(innerException), ErrorCategory.InvalidOperation, this.Identity);
				}
				versionedXmlDataProvider.Save(textMessagingAccount);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600458A RID: 17802 RVA: 0x0011DAAC File Offset: 0x0011BCAC
		protected override bool IsKnownException(Exception exception)
		{
			return exception is StoragePermanentException || base.IsKnownException(exception);
		}

		// Token: 0x04002ADD RID: 10973
		private static Random random = new Random();

		// Token: 0x04002ADE RID: 10974
		private static DateTime timeCreatingRandom = DateTime.UtcNow;

		// Token: 0x04002ADF RID: 10975
		private static int timeSpan = new Random().Next(60);
	}
}
