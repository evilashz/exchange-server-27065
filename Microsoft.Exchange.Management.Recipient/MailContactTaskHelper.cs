using System;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x0200007B RID: 123
	internal static class MailContactTaskHelper
	{
		// Token: 0x060008F6 RID: 2294 RVA: 0x000269F0 File Offset: 0x00024BF0
		public static void ValidateExternalEmailAddress(ADContact contact, IConfigurationSession configurationSession, Task.ErrorLoggerDelegate writeError, ProvisioningCache provisioningCache)
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.ValidateExternalEmailAddressInAcceptedDomain.Enabled)
			{
				SmtpProxyAddress smtpProxyAddress = contact.ExternalEmailAddress as SmtpProxyAddress;
				if (smtpProxyAddress == null)
				{
					writeError(new RecipientTaskException(Strings.ErrorExternalEmailAddressNotSmtpAddress((contact.ExternalEmailAddress == null) ? "$null" : contact.ExternalEmailAddress.ToString())), ExchangeErrorCategory.Client, contact.Identity);
					return;
				}
				if (RecipientTaskHelper.SMTPAddressCheckWithAcceptedDomain(configurationSession, contact.OrganizationId, writeError, provisioningCache))
				{
					string domain = new SmtpAddress(smtpProxyAddress.SmtpAddress).Domain;
					if (RecipientTaskHelper.IsAcceptedDomain(configurationSession, contact.OrganizationId, domain, provisioningCache))
					{
						writeError(new RecipientTaskException(Strings.ErrorIsAcceptedDomain(domain)), ExchangeErrorCategory.Client, null);
					}
				}
				contact.EmailAddressPolicyEnabled = false;
				if (contact.PrimarySmtpAddress == SmtpAddress.Empty)
				{
					contact.PrimarySmtpAddress = new SmtpAddress(smtpProxyAddress.SmtpAddress);
				}
			}
		}
	}
}
