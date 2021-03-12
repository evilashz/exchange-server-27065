using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000C0 RID: 192
	internal abstract class UMAgentUtil
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x000205CC File Offset: 0x0001E7CC
		internal static bool TryGetADUser(Trace tracer, ADRecipientCache<TransportMiniRecipient> recipientCache, MailRecipient mailRecipient, out ADUser user)
		{
			user = null;
			ADRecipient adrecipient = null;
			if (UMAgentUtil.TryGetADRecipient(tracer, recipientCache, mailRecipient, out adrecipient))
			{
				user = (adrecipient as ADUser);
			}
			return null != user;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x000205FC File Offset: 0x0001E7FC
		internal static bool TryGetUMMailboxPolicy(Trace tracer, ADRecipientCache<TransportMiniRecipient> recipientCache, MailRecipient mailRecipient, out UMMailboxPolicy mailboxPolicy)
		{
			mailboxPolicy = null;
			ADUser dataObject = null;
			return UMAgentUtil.TryGetADUser(tracer, recipientCache, mailRecipient, out dataObject) && UMAgentUtil.TryGetUMMailboxPolicy(tracer, new UMMailbox(dataObject), out mailboxPolicy);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0002068C File Offset: 0x0001E88C
		internal static bool TryGetUMMailboxPolicy(Trace tracer, UMMailbox mailbox, out UMMailboxPolicy mailboxPolicy)
		{
			mailboxPolicy = null;
			if (!mailbox.UMEnabled || mailbox.UMMailboxPolicy == null)
			{
				tracer.TraceWarning<string>(0L, "Recipient is not UM enabled: {0}", mailbox.DistinguishedName);
				return false;
			}
			UMMailboxPolicy policy = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), mailbox.OrganizationId, null, false);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.IgnoreInvalid, sessionSettings, 117, "TryGetUMMailboxPolicy", "f:\\15.00.1497\\sources\\dev\\MailboxTransport\\src\\MailboxTransportDelivery\\StoreDriver\\agents\\UM\\UMAgentUtil.cs");
				policy = tenantOrTopologyConfigurationSession.Read<UMMailboxPolicy>(mailbox.UMMailboxPolicy);
			});
			if (policy == null)
			{
				tracer.TraceWarning<string, string>(0L, "Could not open mailbox policy {0} for {1}", mailbox.UMMailboxPolicy.DistinguishedName, mailbox.DistinguishedName);
				return false;
			}
			mailboxPolicy = policy;
			return true;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x000207A4 File Offset: 0x0001E9A4
		private static bool TryGetADRecipient(Trace tracer, ADRecipientCache<TransportMiniRecipient> recipientCache, MailRecipient mailRecipient, out ADRecipient recipient)
		{
			recipient = null;
			ProxyAddress proxyAddress = new SmtpProxyAddress((string)mailRecipient.Email, true);
			TransportMiniRecipient recipientEntry = recipientCache.FindAndCacheRecipient(proxyAddress).Data;
			if (recipientEntry == null)
			{
				tracer.TraceWarning<RoutingAddress>(0L, "Could not find recipient entry for {0}", mailRecipient.Email);
				return false;
			}
			ADRecipient tempRecipient = null;
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				SmtpAddress smtpAddress = new SmtpAddress(proxyAddress.AddressString);
				ADSessionSettings sessionSettings = ADSessionSettings.RootOrgOrSingleTenantFromAcceptedDomainAutoDetect(smtpAddress.Domain);
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, null, LcidMapper.DefaultLcid, true, ConsistencyMode.IgnoreInvalid, null, sessionSettings, 178, "TryGetADRecipient", "f:\\15.00.1497\\sources\\dev\\MailboxTransport\\src\\MailboxTransportDelivery\\StoreDriver\\agents\\UM\\UMAgentUtil.cs");
				tempRecipient = tenantOrRootOrgRecipientSession.Read(recipientEntry.Id);
			});
			if (tempRecipient == null)
			{
				tracer.TraceWarning<ADObjectId>(0L, "Could not read recipient object for {0}", recipientEntry.Id);
				return false;
			}
			recipient = tempRecipient;
			return true;
		}
	}
}
