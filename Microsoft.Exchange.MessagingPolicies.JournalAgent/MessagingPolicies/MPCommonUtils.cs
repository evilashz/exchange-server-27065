using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.MessagingPolicies
{
	// Token: 0x02000003 RID: 3
	internal class MPCommonUtils
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002544 File Offset: 0x00000744
		public static string GetDecryptionTokenRecipient(MailItem mailItem, AcceptedDomainCollection acceptedDomains)
		{
			foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
			{
				AcceptedDomain acceptedDomain = acceptedDomains.Find(envelopeRecipient.Address);
				if (acceptedDomain != null && acceptedDomain.IsInCorporation && acceptedDomain.TenantId == mailItem.TenantId)
				{
					return (string)envelopeRecipient.Address;
				}
			}
			AcceptedDomain acceptedDomain2 = acceptedDomains.Find(mailItem.FromAddress);
			if (acceptedDomain2 != null && acceptedDomain2.IsInCorporation && acceptedDomain2.TenantId == mailItem.TenantId)
			{
				return (string)mailItem.FromAddress;
			}
			return null;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002604 File Offset: 0x00000804
		public static string GetDecryptionTokenRecipient(MailItem mailItem)
		{
			ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)mailItem;
			ITransportMailItemFacade transportMailItem = transportMailItemWrapperFacade.TransportMailItem;
			TransportMailItem transportMailItem2 = transportMailItem as TransportMailItem;
			OrganizationId organizationId = (transportMailItem2 == null) ? null : transportMailItem2.OrganizationId;
			if (organizationId != null)
			{
				PerTenantAcceptedDomainTable acceptedDomainTable = Components.Configuration.GetAcceptedDomainTable(organizationId);
				foreach (EnvelopeRecipient envelopeRecipient in mailItem.Recipients)
				{
					if (envelopeRecipient.Address != RoutingAddress.NullReversePath)
					{
						bool flag = acceptedDomainTable.AcceptedDomainTable.GetDomainEntry(SmtpDomain.Parse(envelopeRecipient.Address.DomainPart)) != null;
						if (flag)
						{
							return (string)envelopeRecipient.Address;
						}
					}
				}
				if (!(mailItem.FromAddress != RoutingAddress.NullReversePath))
				{
					goto IL_106;
				}
				bool flag2 = acceptedDomainTable.AcceptedDomainTable.GetDomainEntry(SmtpDomain.Parse(mailItem.FromAddress.DomainPart)) != null;
				if (flag2)
				{
					return (string)mailItem.FromAddress;
				}
			}
			IL_106:
			return null;
		}
	}
}
