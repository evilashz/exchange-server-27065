using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Configuration;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001E4 RID: 484
	internal class OneOffItem : RecipientItem
	{
		// Token: 0x060015C1 RID: 5569 RVA: 0x00058422 File Offset: 0x00056622
		public OneOffItem(MailRecipient recipient) : base(recipient)
		{
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0005842C File Offset: 0x0005662C
		public static bool IsLocalAddress(RoutingAddress smtpAddress, AcceptedDomainTable orgDomains, out ProxyAddressPrefix addressType)
		{
			ProxyAddress proxyAddress;
			if (Resolver.TryDeencapsulate(smtpAddress, out proxyAddress))
			{
				addressType = proxyAddress.Prefix;
				return proxyAddress.Prefix == ProxyAddressPrefix.LegacyDN || (proxyAddress.Prefix == ProxyAddressPrefix.X400 && OneOffItem.IsLocalX400Address(proxyAddress));
			}
			addressType = ProxyAddressPrefix.Smtp;
			return OneOffItem.IsLocalSmtpAddress(smtpAddress, orgDomains);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00058488 File Offset: 0x00056688
		public static bool IsLocalX400Address(ProxyAddress x400Proxy)
		{
			RoutingX400Address address;
			return RoutingX400Address.TryParse(x400Proxy.AddressString, out address) && Components.Configuration.X400AuthoritativeDomainTable.CheckAccepted(address);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x000584B8 File Offset: 0x000566B8
		public static bool IsLocalAddress(ProxyAddress proxyAddress, AcceptedDomainTable orgDomains)
		{
			if (proxyAddress.Prefix == ProxyAddressPrefix.LegacyDN)
			{
				return true;
			}
			if (proxyAddress.Prefix == ProxyAddressPrefix.X400)
			{
				return OneOffItem.IsLocalX400Address(proxyAddress);
			}
			return proxyAddress.Prefix == ProxyAddressPrefix.Smtp && OneOffItem.IsLocalSmtpAddress(new RoutingAddress(proxyAddress.AddressString), orgDomains);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00058517 File Offset: 0x00056717
		public static bool IsLocalSmtpAddress(RoutingAddress address, AcceptedDomainTable orgDomains)
		{
			if (orgDomains == null)
			{
				throw new ArgumentNullException("orgDomains");
			}
			return orgDomains.CheckAuthoritative(SmtpDomain.GetDomainPart(address));
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00058534 File Offset: 0x00056734
		public static bool IsAuthoritativeOrInternalRelaySmtpAddress(RoutingAddress smtpAddress, OrganizationId orgId)
		{
			PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
			return Components.Configuration.TryGetAcceptedDomainTable(orgId, out perTenantAcceptedDomainTable) && perTenantAcceptedDomainTable.AcceptedDomainTable.CheckInternal(SmtpDomain.GetDomainPart(smtpAddress));
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00058564 File Offset: 0x00056764
		public override void PreProcess(Expansion expansion)
		{
			AcceptedDomainTable acceptedDomains = expansion.Configuration.AcceptedDomains;
			ProxyAddressPrefix smtp = ProxyAddressPrefix.Smtp;
			if (OneOffItem.IsLocalAddress(base.Email, acceptedDomains, out smtp))
			{
				if (smtp == ProxyAddressPrefix.LegacyDN)
				{
					base.FailRecipient(AckReason.LocalRecipientExAddressUnknown);
				}
				else if (smtp == ProxyAddressPrefix.X400)
				{
					base.FailRecipient(AckReason.LocalRecipientX400AddressUnknown);
				}
				else
				{
					base.FailRecipient(AckReason.LocalRecipientAddressUnknown);
				}
				if (Resolver.PerfCounters != null)
				{
					Resolver.PerfCounters.UnresolvedOrgRecipientsTotal.Increment();
				}
			}
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000585EC File Offset: 0x000567EC
		public override void PostProcess(Expansion expansion)
		{
			ExTraceGlobals.ResolverTracer.TraceDebug<ResolverMessageType, RoutingAddress>(0L, "OneOffItem:Msg Type={0}, recipientAddress={1}", expansion.Message.Type, base.Email);
			OofRestriction.ExternalUserOofCheck(expansion, base.Recipient);
			MsgTypeRestriction.ExternalRecipientMessageTypeCheck(expansion, base.Recipient);
			if (!base.Recipient.IsProcessed)
			{
				this.CheckDeliveryRestrictions(expansion);
			}
		}
	}
}
