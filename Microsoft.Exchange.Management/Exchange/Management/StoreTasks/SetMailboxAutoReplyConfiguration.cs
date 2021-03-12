using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.Management.StoreTasks
{
	// Token: 0x020007BF RID: 1983
	[Cmdlet("Set", "MailboxAutoReplyConfiguration", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetMailboxAutoReplyConfiguration : SetXsoObjectWithIdentityTaskBase<MailboxAutoReplyConfiguration>
	{
		// Token: 0x060045AB RID: 17835 RVA: 0x0011E5EC File Offset: 0x0011C7EC
		protected override void InternalValidate()
		{
			base.InternalValidate();
			base.VerifyIsWithinScopes((IRecipientSession)base.DataSession, this.DataObject, true, new DataAccessTask<ADUser>.ADObjectOutOfScopeString(Strings.ErrorCannotChangeMailboxOutOfWriteScope));
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x060045AC RID: 17836 RVA: 0x0011E618 File Offset: 0x0011C818
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageMailboxAutoReplyConfiguration(this.Identity.ToString());
			}
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x0011E62C File Offset: 0x0011C82C
		protected override void StampChangesOnXsoObject(IConfigurable dataObject)
		{
			base.StampChangesOnXsoObject(dataObject);
			MailboxAutoReplyConfiguration mailboxAutoReplyConfiguration = (MailboxAutoReplyConfiguration)dataObject;
			if (!string.IsNullOrEmpty(mailboxAutoReplyConfiguration.InternalMessage) && mailboxAutoReplyConfiguration.IsChanged(MailboxAutoReplyConfigurationSchema.InternalMessage))
			{
				mailboxAutoReplyConfiguration.InternalMessage = TextConverterHelper.SanitizeHtml(mailboxAutoReplyConfiguration.InternalMessage);
			}
			if (!string.IsNullOrEmpty(mailboxAutoReplyConfiguration.ExternalMessage) && mailboxAutoReplyConfiguration.IsChanged(MailboxAutoReplyConfigurationSchema.ExternalMessage))
			{
				mailboxAutoReplyConfiguration.ExternalMessage = TextConverterHelper.SanitizeHtml(mailboxAutoReplyConfiguration.ExternalMessage);
			}
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x0011E69D File Offset: 0x0011C89D
		internal override IConfigDataProvider CreateXsoMailboxDataProvider(ExchangePrincipal principal, ISecurityAccessToken userToken)
		{
			return new MailboxAutoReplyConfigurationDataProvider(principal, "Set-MailboxAutoReplyConfiguration");
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x0011E6AA File Offset: 0x0011C8AA
		protected override bool IsKnownException(Exception exception)
		{
			return exception is ExchangeDataException || exception is StorageTransientException || exception is TextConvertersException || exception is InvalidScheduledOofDuration || base.IsKnownException(exception);
		}
	}
}
