using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Transport.Agent.TrustedMail
{
	// Token: 0x02000009 RID: 9
	internal static class TrustedMailUtils
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00003368 File Offset: 0x00001568
		public static PerTenantRemoteDomainTable GetRemoteDomainTable(MailItem mailItem)
		{
			OrganizationId organizationId = TrustedMailUtils.GetOrganizationId(mailItem);
			PerTenantRemoteDomainTable result;
			if (Components.Configuration.TryGetRemoteDomainTable(organizationId, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003390 File Offset: 0x00001590
		public static RemoteDomainEntry GetRemoteDomainEntry(SmtpDomain domain, MailItem mailItem)
		{
			PerTenantRemoteDomainTable remoteDomainTable = TrustedMailUtils.GetRemoteDomainTable(mailItem);
			if (remoteDomainTable != null)
			{
				return remoteDomainTable.RemoteDomainTable.GetDomainEntry(domain);
			}
			return null;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000033B8 File Offset: 0x000015B8
		public static PerTenantAcceptedDomainTable GetAcceptedDomainTable(MailItem mailItem)
		{
			OrganizationId organizationId = TrustedMailUtils.GetOrganizationId(mailItem);
			PerTenantAcceptedDomainTable perTenantAcceptedDomainTable;
			if (Components.Configuration.TryGetAcceptedDomainTable(organizationId, out perTenantAcceptedDomainTable) && perTenantAcceptedDomainTable != null)
			{
				return perTenantAcceptedDomainTable;
			}
			return null;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000033E4 File Offset: 0x000015E4
		public static AcceptedDomainEntry GetAcceptedDomainEntry(SmtpDomain domain, MailItem mailItem)
		{
			PerTenantAcceptedDomainTable acceptedDomainTable = TrustedMailUtils.GetAcceptedDomainTable(mailItem);
			if (acceptedDomainTable != null)
			{
				return acceptedDomainTable.AcceptedDomainTable.GetDomainEntry(domain);
			}
			return null;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000340C File Offset: 0x0000160C
		public static bool HeadersPreservedOutbound(HeaderList headers)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Organization-Cross-Premises-Headers-Preserved");
			return null != header;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000342C File Offset: 0x0000162C
		public static bool PreserveHeadersOnEdge(HeaderList headers)
		{
			Header header = headers.FindFirst("X-MS-Exchange-Organization-PreserveHeadersOnEdge");
			return null != header;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000344C File Offset: 0x0000164C
		public static void StampHeader(HeaderList headerList, string headerName, string headerValue)
		{
			Header header = Header.Create(headerName);
			header.Value = headerValue;
			headerList.AppendChild(header);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003470 File Offset: 0x00001670
		public static void StampHeader(HeaderList headerList, string headerName)
		{
			Header newChild = Header.Create(headerName);
			headerList.AppendChild(newChild);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000348C File Offset: 0x0000168C
		public static OrganizationId GetOrganizationId(MailItem mailItem)
		{
			OrganizationId result = OrganizationId.ForestWideOrgId;
			if (mailItem != null && TrustedMailUtils.IsMultiTenancyEnabled)
			{
				ITransportMailItemWrapperFacade transportMailItemWrapperFacade = (ITransportMailItemWrapperFacade)mailItem;
				ITransportMailItemFacade transportMailItem = transportMailItemWrapperFacade.TransportMailItem;
				if (transportMailItem.OrganizationIdAsObject != null)
				{
					result = (transportMailItem.OrganizationIdAsObject as OrganizationId);
				}
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000034CC File Offset: 0x000016CC
		public static bool IsDomainMatchAcceptedDomain(int objHashCode, Trace tracer, TrustedMailUtils.GetAcceptedDomainEntryDelegate getAcceptedDomainEntry, SmtpDomain domain, MailItem mailItem)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			AcceptedDomainEntry acceptedDomainEntry = getAcceptedDomainEntry(domain, mailItem);
			if (acceptedDomainEntry != null)
			{
				return true;
			}
			tracer.TraceDebug<string>((long)objHashCode, "Accepted Domain query failed", domain.Domain);
			return false;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000350C File Offset: 0x0000170C
		public static bool IsOriginatingOrgDomainInboundTrustEnabled(int objHashCode, Trace tracer, TrustedMailUtils.GetRemoteDomainEntryDelegate getRemoteDomainEntry, TrustedMailUtils.GetAcceptedDomainEntryDelegate getAcceptedDomainEntry, SmtpDomain domain, MailItem mailItem, out bool isRemoteDomainMatch)
		{
			isRemoteDomainMatch = false;
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			if (TrustedMailUtils.IsDomainMatchAcceptedDomain(objHashCode, tracer, getAcceptedDomainEntry, domain, mailItem))
			{
				tracer.TraceDebug<string>((long)objHashCode, "Accepted domain present for originating domain {0} on message and the domain is treated as trusted", domain.Domain);
				return true;
			}
			if (TrustedMailUtils.IsRemoteDomainInboundTrustEnabled(objHashCode, tracer, getRemoteDomainEntry, domain, mailItem))
			{
				tracer.TraceDebug<string>((long)objHashCode, "Remote domain entry present for originating domain {0} on message and the domain is trusted", domain.Domain);
				isRemoteDomainMatch = true;
				return true;
			}
			tracer.TraceDebug<string>((long)objHashCode, "Accepted domain and Remote Domain Entry not present for originating domain {0} on message for the domain", domain.Domain);
			return false;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003590 File Offset: 0x00001790
		public static bool IsRemoteDomainInboundTrustEnabled(int objHashCode, Trace tracer, TrustedMailUtils.GetRemoteDomainEntryDelegate getRemoteDomainEntry, SmtpDomain domain, MailItem mailItem)
		{
			if (domain == null)
			{
				throw new ArgumentException("domain");
			}
			RemoteDomainEntry remoteDomainEntry = getRemoteDomainEntry(domain, mailItem);
			if (remoteDomainEntry == null)
			{
				tracer.TraceDebug<string>((long)objHashCode, "Remote domain entry not present for originating domain {0} on message and so domain is not trusted", domain.Domain);
				return false;
			}
			if (!remoteDomainEntry.TrustedMailInboundEnabled)
			{
				tracer.TraceDebug<string>((long)objHashCode, "Remote domain entry present for originating domain {0} on message but the domain is not trusted", domain.Domain);
				return false;
			}
			return true;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000035EC File Offset: 0x000017EC
		public static bool InboundTrustEnabledOnMail(int objHashCode, Trace tracer, IDictionary<string, object> mailItemProperties, TrustedMailUtils.GetRemoteDomainEntryDelegate getRemoteDomainEntry, TrustedMailUtils.GetAcceptedDomainEntryDelegate getAcceptedDomainEntry, SmtpDomain mailOriginatingDomain, MailItem mailItem, out bool isRemoteDomainMatch)
		{
			isRemoteDomainMatch = false;
			if (mailOriginatingDomain == null)
			{
				throw new ArgumentNullException("mailOriginatingDomain");
			}
			bool flag;
			if (TrustedMailUtils.TryGetInboundTrustProperty(mailItemProperties, out flag))
			{
				tracer.TraceDebug<bool>((long)objHashCode, "Message already had inbound trust property set to {0}", flag);
				return flag;
			}
			return TrustedMailUtils.IsOriginatingOrgDomainInboundTrustEnabled(objHashCode, tracer, getRemoteDomainEntry, getAcceptedDomainEntry, mailOriginatingDomain, mailItem, out isRemoteDomainMatch);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003638 File Offset: 0x00001838
		public static bool TryGetInboundTrustProperty(IDictionary<string, object> properties, out bool value)
		{
			value = false;
			object obj;
			if (properties != null && properties.TryGetValue("Microsoft.Exchange.Transport.InboundTrustEnabled", out obj))
			{
				bool? flag = obj as bool?;
				if (flag != null)
				{
					value = flag.Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000367C File Offset: 0x0000187C
		public static bool IsOutboundDomainTrusted(int objHashCode, Trace tracer, TrustedMailUtils.GetRemoteDomainEntryDelegate getRemoteDomainEntry, TrustedMailUtils.GetAcceptedDomainEntryDelegate getAcceptedDomainEntry, string domain, MailItem mailItem, EnvelopeRecipient recipient, bool? preserveCrossPremisesHeaders)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			SmtpDomain domain2;
			if (!SmtpDomain.TryParse(domain, out domain2))
			{
				throw new ArgumentException("Failed to Parse Doamin");
			}
			RemoteDomainEntry remoteDomainEntry = TrustedMailUtils.GetRemoteDomainEntry(objHashCode, tracer, getRemoteDomainEntry, domain2, mailItem);
			if (remoteDomainEntry != null && remoteDomainEntry.TrustedMailOutboundEnabled)
			{
				tracer.TraceDebug<string, string>((long)objHashCode, "Remote domain entry present for domain {0} on message {1}, the domain is trusted, and cross-premise header flag is enabled", domain, mailItem.Message.MessageId);
				return true;
			}
			if (!TrustedMailUtils.IsDomainMatchAcceptedDomain(objHashCode, tracer, getAcceptedDomainEntry, domain2, mailItem))
			{
				tracer.TraceDebug<string>((long)objHashCode, "Accepted domain is not present for domain {0} on message for the recipient domain", domain);
				return false;
			}
			tracer.TraceDebug<string, string>((long)objHashCode, "Accepted Domain present for domain {0} on message {1} and the domain is trusted", domain, mailItem.Message.MessageId);
			if (!TrustedMailUtils.CheckOutboundDeliveryTypeSmtpConnector)
			{
				if (preserveCrossPremisesHeaders == null)
				{
					preserveCrossPremisesHeaders = new bool?(TrustedMailUtils.CrossPremisesHeadersPreserved);
				}
				tracer.TraceDebug<string, string, bool>((long)objHashCode, "Accepted Domain present for domain {0} on message {1}, the domain is trusted.  Preserve cross-premise header flag is {2}", domain, mailItem.Message.MessageId, preserveCrossPremisesHeaders.Value);
				return preserveCrossPremisesHeaders.Value;
			}
			if (TrustedMailUtils.IsDeliveryTypeSmtpConnector(recipient))
			{
				tracer.TraceDebug<string, string>((long)objHashCode, "Accepted Domain present for domain {0} on message {1}, the domain is trusted.  Delivery type is SMTP connector", domain, mailItem.Message.MessageId);
				return true;
			}
			tracer.TraceDebug<string, string>((long)objHashCode, "Accepted Domain present for domain {0} on message {1}, the domain is trusted.  Delivery type is not SMTP connector", domain, mailItem.Message.MessageId);
			return false;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000037A8 File Offset: 0x000019A8
		public static RemoteDomainEntry GetRemoteDomainEntry(int objHashCode, Trace tracer, TrustedMailUtils.GetRemoteDomainEntryDelegate getRemoteDomainEntry, SmtpDomain domain, MailItem mailItem)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			return getRemoteDomainEntry(domain, mailItem);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000037D0 File Offset: 0x000019D0
		public static bool IsRemoteDomainOutboundTrustEnabled(int objHashCode, Trace tracer, TrustedMailUtils.GetRemoteDomainEntryDelegate getRemoteDomainEntry, SmtpDomain domain, MailItem mailItem)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			RemoteDomainEntry remoteDomainEntry = getRemoteDomainEntry(domain, mailItem);
			if (remoteDomainEntry == null)
			{
				tracer.TraceDebug<SmtpDomain, string>((long)objHashCode, "Remote domain entry not present for domain {0} on message {1} and so domain is not trusted", domain, mailItem.Message.MessageId);
				return false;
			}
			if (!remoteDomainEntry.TrustedMailOutboundEnabled)
			{
				tracer.TraceDebug<SmtpDomain, string>((long)objHashCode, "Remote domain entry present for domain {0} on message {1} and but the domain is not trusted", domain, mailItem.Message.MessageId);
				return false;
			}
			return true;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000383C File Offset: 0x00001A3C
		public static bool IsDeliveryTypeSmtpConnector(EnvelopeRecipient recipient)
		{
			MailRecipientWrapper mailRecipientWrapper = recipient as MailRecipientWrapper;
			return mailRecipientWrapper.MailRecipient.NextHop.NextHopType.IsSmtpConnectorDeliveryType;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000386C File Offset: 0x00001A6C
		public static bool IsDeliveryTypeSmtpRelayToEdge(EnvelopeRecipient recipient)
		{
			MailRecipientWrapper mailRecipientWrapper = recipient as MailRecipientWrapper;
			return mailRecipientWrapper.MailRecipient.NextHop.NextHopType.IsSmtpSmtpRelayToEdge;
		}

		// Token: 0x0400001B RID: 27
		public const string CrossPremisesHeadersPreservedHeader = "X-MS-Exchange-Organization-Cross-Premises-Headers-Preserved";

		// Token: 0x0400001C RID: 28
		public const string PreserveHeadersOnEdgeHeader = "X-MS-Exchange-Organization-PreserveHeadersOnEdge";

		// Token: 0x0400001D RID: 29
		private const string AcceptAnyRecipientOnPremisesName = "CentralizedMailControlAcceptAnyRecipientOnPremises";

		// Token: 0x0400001E RID: 30
		private const string StampOriginatorOrgForMsitConnectorName = "InboundTrustStampOriginatorOrgForMsitConnector";

		// Token: 0x0400001F RID: 31
		private const string TrustedMailAgentsEnabledName = "TrustedMailAgentsEnabled";

		// Token: 0x04000020 RID: 32
		public static readonly bool AcceptAnyRecipientOnPremises = TransportAppConfig.GetConfigBool("CentralizedMailControlAcceptAnyRecipientOnPremises", VariantConfiguration.InvariantNoFlightingSnapshot.MexAgents.TrustedMailAgents_AcceptAnyRecipientOnPremises.Enabled);

		// Token: 0x04000021 RID: 33
		public static readonly bool StampOriginatorOrgForMsitConnector = TransportAppConfig.GetConfigBool("InboundTrustStampOriginatorOrgForMsitConnector", VariantConfiguration.InvariantNoFlightingSnapshot.MexAgents.TrustedMailAgents_StampOriginatorOrgForMsitConnector.Enabled);

		// Token: 0x04000022 RID: 34
		public static readonly bool CrossPremisesHeadersPreserved = VariantConfiguration.InvariantNoFlightingSnapshot.MexAgents.TrustedMailAgents_CrossPremisesHeadersPreserved.Enabled;

		// Token: 0x04000023 RID: 35
		public static readonly bool HandleCrossPremisesProbeEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.MexAgents.TrustedMailAgents_HandleCrossPremisesProbe.Enabled;

		// Token: 0x04000024 RID: 36
		public static readonly bool CheckOutboundDeliveryTypeSmtpConnector = VariantConfiguration.InvariantNoFlightingSnapshot.MexAgents.TrustedMailAgents_CheckOutboundDeliveryTypeSmtpConnector.Enabled;

		// Token: 0x04000025 RID: 37
		public static readonly bool IsMultiTenancyEnabled = VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled;

		// Token: 0x04000026 RID: 38
		public static readonly bool TrustedMailAgentsEnabled = TransportAppConfig.GetConfigBool("TrustedMailAgentsEnabled", true);

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x0600004B RID: 75
		public delegate RemoteDomainEntry GetRemoteDomainEntryDelegate(SmtpDomain domain, MailItem mailItem);

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x0600004F RID: 79
		public delegate AcceptedDomainEntry GetAcceptedDomainEntryDelegate(SmtpDomain domain, MailItem mailItem);
	}
}
