using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.MailboxTransport.Shared.Providers
{
	// Token: 0x02000023 RID: 35
	internal class ADConfigurationProvider : IConfigurationProvider
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00006F09 File Offset: 0x00005109
		private ADConfigurationProvider()
		{
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00006F11 File Offset: 0x00005111
		public static ADConfigurationProvider Instance
		{
			get
			{
				return ADConfigurationProvider.instance;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00006F18 File Offset: 0x00005118
		public string GetDefaultDomainName()
		{
			return Components.Configuration.FirstOrgAcceptedDomainTable.DefaultDomainName;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00006F2C File Offset: 0x0000512C
		public bool TryGetDefaultDomainName(OrganizationId organizationId, out string domainName)
		{
			domainName = null;
			PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
			if (Components.Configuration.TryGetAcceptedDomainTable(organizationId, out perTenantAcceptedDomainTable))
			{
				domainName = perTenantAcceptedDomainTable.AcceptedDomainTable.DefaultDomain.DomainName.Domain;
			}
			return domainName != null;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006F6C File Offset: 0x0000516C
		public OutboundConversionOptions GetGlobalConversionOptions()
		{
			return new OutboundConversionOptions(new EmptyRecipientCache(), this.GetDefaultDomainName())
			{
				DsnMdnOptions = DsnMdnOptions.PropagateUserSettings,
				DsnHumanReadableWriter = Components.DsnGenerator.DsnHumanReadableWriter,
				Limits = 
				{
					MimeLimits = MimeLimits.Unlimited
				},
				LogDirectoryPath = Components.Configuration.LocalServer.ContentConversionTracingPath
			};
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006FC7 File Offset: 0x000051C7
		public void SendNDRForInvalidAddresses(IReadOnlyMailItem mailItem, List<DsnRecipientInfo> invalidRecipients, DsnMailOutHandlerDelegate dsnMailOutHandler)
		{
			Components.DsnGenerator.GenerateNDRForInvalidAddresses(false, mailItem, invalidRecipients, dsnMailOutHandler);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006FD8 File Offset: 0x000051D8
		public void SendNDRForFailedSubmission(IReadOnlyMailItem ndrMailItem, SmtpResponse ndrReason, DsnMailOutHandlerDelegate dsnMailOutHandler)
		{
			List<DsnRecipientInfo> list = new List<DsnRecipientInfo>();
			list.Add(DsnGenerator.CreateDsnRecipientInfo(null, (string)ndrMailItem.From, null, ndrReason));
			Components.DsnGenerator.GenerateNDRForInvalidAddresses(false, ndrMailItem, list, dsnMailOutHandler);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007012 File Offset: 0x00005212
		public string GetQuarantineMailbox()
		{
			if (Components.DsnGenerator.QuarantineConfig != null)
			{
				return Components.DsnGenerator.QuarantineConfig.Mailbox;
			}
			return null;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007031 File Offset: 0x00005231
		public bool GetForwardingProhibitedFeatureStatus()
		{
			return Components.TransportAppConfig.Resolver.EnableForwardingProhibitedFeature;
		}

		// Token: 0x04000072 RID: 114
		private static ADConfigurationProvider instance = new ADConfigurationProvider();
	}
}
