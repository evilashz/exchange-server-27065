using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Shared.Providers
{
	// Token: 0x02000022 RID: 34
	internal static class ConfigurationProvider
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00006E8D File Offset: 0x0000508D
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00006EA5 File Offset: 0x000050A5
		public static IConfigurationProvider ConfigurationProviderInstance
		{
			get
			{
				if (ConfigurationProvider.configurationProvider == null)
				{
					ConfigurationProvider.configurationProvider = ADConfigurationProvider.Instance;
				}
				return ConfigurationProvider.configurationProvider;
			}
			set
			{
				ConfigurationProvider.configurationProvider = value;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00006EAD File Offset: 0x000050AD
		public static OutboundConversionOptions GetGlobalConversionOptions()
		{
			return ConfigurationProvider.ConfigurationProviderInstance.GetGlobalConversionOptions();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006EB9 File Offset: 0x000050B9
		public static string GetDefaultDomainName()
		{
			return ConfigurationProvider.ConfigurationProviderInstance.GetDefaultDomainName();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006EC5 File Offset: 0x000050C5
		public static bool TryGetDefaultDomainName(OrganizationId organizationId, out string domainName)
		{
			return ConfigurationProvider.ConfigurationProviderInstance.TryGetDefaultDomainName(organizationId, out domainName);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006ED3 File Offset: 0x000050D3
		public static void SendNDRForInvalidAddresses(IReadOnlyMailItem mailItemToSubmit, List<DsnRecipientInfo> invalidRecipients, DsnMailOutHandlerDelegate dsnMailOutHandler)
		{
			ConfigurationProvider.ConfigurationProviderInstance.SendNDRForInvalidAddresses(mailItemToSubmit, invalidRecipients, dsnMailOutHandler);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006EE2 File Offset: 0x000050E2
		public static void SendNDRForFailedSubmission(IReadOnlyMailItem ndrMailItem, SmtpResponse ndrReason, DsnMailOutHandlerDelegate dsnMailOutHandler)
		{
			ConfigurationProvider.ConfigurationProviderInstance.SendNDRForFailedSubmission(ndrMailItem, ndrReason, dsnMailOutHandler);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006EF1 File Offset: 0x000050F1
		public static string GetQuarantineMailbox()
		{
			return ConfigurationProvider.ConfigurationProviderInstance.GetQuarantineMailbox();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006EFD File Offset: 0x000050FD
		public static bool GetForwardingProhibitedFeatureStatus()
		{
			return ConfigurationProvider.ConfigurationProviderInstance.GetForwardingProhibitedFeatureStatus();
		}

		// Token: 0x04000071 RID: 113
		private static IConfigurationProvider configurationProvider;
	}
}
