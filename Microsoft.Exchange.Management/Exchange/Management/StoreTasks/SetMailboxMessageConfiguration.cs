using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.SendAsDefaults;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007CA RID: 1994
	[Cmdlet("Set", "MailboxMessageConfiguration", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxMessageConfiguration : SetXsoObjectWithIdentityTaskBase<MailboxMessageConfiguration>
	{
		// Token: 0x06004600 RID: 17920 RVA: 0x0011FC48 File Offset: 0x0011DE48
		protected override void InternalValidate()
		{
			base.InternalValidate();
			base.VerifyIsWithinScopes((IRecipientSession)base.DataSession, this.DataObject, true, new DataAccessTask<ADUser>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
		}

		// Token: 0x17001526 RID: 5414
		// (get) Token: 0x06004601 RID: 17921 RVA: 0x0011FC74 File Offset: 0x0011DE74
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMailboxMessageConfiguration(this.Identity.ToString());
			}
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x0011FC88 File Offset: 0x0011DE88
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			XsoDictionaryDataProvider xsoDictionaryDataProvider = new XsoDictionaryDataProvider(principal, "Set-MailboxMessageConfiguration");
			this.mailboxSession = xsoDictionaryDataProvider.MailboxSession;
			return xsoDictionaryDataProvider;
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x0011FCB0 File Offset: 0x0011DEB0
		protected override void StampChangesOnXsoObject(IConfigurable dataObject)
		{
			base.StampChangesOnXsoObject(dataObject);
			MailboxMessageConfiguration mailboxMessageConfiguration = (MailboxMessageConfiguration)dataObject;
			if (mailboxMessageConfiguration.IsModified(MailboxMessageConfigurationSchema.SignatureHtml))
			{
				mailboxMessageConfiguration.SignatureHtml = TextConverterHelper.SanitizeHtml(mailboxMessageConfiguration.SignatureHtml);
				if (!mailboxMessageConfiguration.IsModified(MailboxMessageConfigurationSchema.SignatureText))
				{
					mailboxMessageConfiguration.SignatureText = TextConverterHelper.HtmlToText(mailboxMessageConfiguration.SignatureHtml, true);
				}
			}
			else if (mailboxMessageConfiguration.IsModified(MailboxMessageConfigurationSchema.SignatureText))
			{
				mailboxMessageConfiguration.SignatureHtml = TextConverterHelper.TextToHtml(mailboxMessageConfiguration.SignatureText);
			}
			if (SyncUtilities.IsDatacenterMode() && mailboxMessageConfiguration.IsModified(MailboxMessageConfigurationSchema.SendAddressDefault))
			{
				SendAsDefaultsManager sendAsDefaultsManager = new SendAsDefaultsManager();
				sendAsDefaultsManager.SaveSettingForOutlook(mailboxMessageConfiguration.SendAddressDefault, this.mailboxSession);
			}
			this.mailboxSession = null;
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x0011FD5B File Offset: 0x0011DF5B
		protected override bool IsKnownException(Exception exception)
		{
			return exception is ExchangeDataException || exception is StorageTransientException || exception is TextConvertersException || base.IsKnownException(exception);
		}

		// Token: 0x04002AF9 RID: 11001
		private MailboxSession mailboxSession;
	}
}
