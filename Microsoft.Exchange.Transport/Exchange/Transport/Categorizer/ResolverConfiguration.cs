using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001F1 RID: 497
	internal class ResolverConfiguration
	{
		// Token: 0x06001636 RID: 5686 RVA: 0x0005AB04 File Offset: 0x00058D04
		public ResolverConfiguration(OrganizationId orgId, PerTenantTransportSettings transportSettings)
		{
			if (orgId == null)
			{
				throw new ArgumentNullException("orgId");
			}
			if (transportSettings == null)
			{
				throw new ArgumentNullException("transportSettings");
			}
			this.organizationId = orgId;
			this.transportSettings = transportSettings;
			this.InitializeAcceptedDomainsAndDefaultDomain();
			this.logPath = ResolverConfiguration.GetLogPath();
			this.privilegedSenders = ResolverConfiguration.GetPrivilegedSenders(this.organizationId, this.DefaultDomain, this.transportSettings);
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x0005AB75 File Offset: 0x00058D75
		public static double ResolverRetryInterval
		{
			get
			{
				return Components.TransportAppConfig.Resolver.ResolverRetryInterval;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0005AB86 File Offset: 0x00058D86
		public static double DeliverMoveMailboxRetryInterval
		{
			get
			{
				return Components.TransportAppConfig.Resolver.DeliverMoveMailboxRetryInterval;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001639 RID: 5689 RVA: 0x0005AB97 File Offset: 0x00058D97
		public static ResolverLogLevel ResolverLogLevel
		{
			get
			{
				return Components.TransportAppConfig.Resolver.ResolverLogLevel;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600163A RID: 5690 RVA: 0x0005ABA8 File Offset: 0x00058DA8
		public static int ExpansionSizeLimit
		{
			get
			{
				return Components.TransportAppConfig.Resolver.ExpansionSizeLimit;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x0600163B RID: 5691 RVA: 0x0005ABB9 File Offset: 0x00058DB9
		public static int BatchLookupRecipientCount
		{
			get
			{
				return Components.TransportAppConfig.Resolver.BatchLookupRecipientCount;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x0005ABCA File Offset: 0x00058DCA
		public static bool LargeDGLimitEnforcementEnabled
		{
			get
			{
				return Components.TransportAppConfig.Resolver.LargeDGLimitEnforcementEnabled;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0005ABDB File Offset: 0x00058DDB
		public static ByteQuantifiedSize LargeDGMaxMessageSize
		{
			get
			{
				return Components.TransportAppConfig.Resolver.LargeDGMaxMessageSize;
			}
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x0005ABEC File Offset: 0x00058DEC
		public static int LargeDGGroupCount
		{
			get
			{
				return Components.TransportAppConfig.Resolver.LargeDGGroupCount;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0005ABFD File Offset: 0x00058DFD
		public static int LargeDGGroupCountForUnRestrictedDG
		{
			get
			{
				return Components.TransportAppConfig.Resolver.LargeDGGroupCountForUnRestrictedDG;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001640 RID: 5696 RVA: 0x0005AC0E File Offset: 0x00058E0E
		public static string ServerDN
		{
			get
			{
				return Components.Configuration.LocalServer.TransportServer.Id.DistinguishedName;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0005AC29 File Offset: 0x00058E29
		public AcceptedDomainTable AcceptedDomains
		{
			get
			{
				return this.acceptedDomains;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0005AC31 File Offset: 0x00058E31
		public string DefaultDomain
		{
			get
			{
				return this.defaultDomain;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001643 RID: 5699 RVA: 0x0005AC39 File Offset: 0x00058E39
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return this.transportSettings.MaxReceiveSize;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0005AC46 File Offset: 0x00058E46
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				return this.transportSettings.MaxSendSize;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x0005AC53 File Offset: 0x00058E53
		public string LogPath
		{
			get
			{
				return this.logPath;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x0005AC5B File Offset: 0x00058E5B
		public IList<RoutingAddress> PrivilegedSenders
		{
			get
			{
				return this.privilegedSenders;
			}
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0005AC64 File Offset: 0x00058E64
		public static IList<RoutingAddress> GetPrivilegedSenders(OrganizationId orgId, string defaultDomain, PerTenantTransportSettings transportSettings)
		{
			IList<RoutingAddress> list = new List<RoutingAddress>(Components.RoutingComponent.MailRouter.ExternalPostmasterAddresses);
			RoutingAddress item;
			if (RoutingUtils.TryConvertToRoutingAddress(transportSettings.ExternalPostmasterAddress, out item) && !list.Contains(item))
			{
				list.Add(item);
			}
			RoutingAddress item2 = new RoutingAddress("postmaster", defaultDomain);
			if (!list.Contains(item2))
			{
				list.Add(item2);
			}
			RoutingAddress defaultExternalPostmasterAddress = DsnGenerator.GetDefaultExternalPostmasterAddress(orgId);
			if (!list.Contains(defaultExternalPostmasterAddress))
			{
				list.Add(defaultExternalPostmasterAddress);
			}
			return list;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0005ACDC File Offset: 0x00058EDC
		private static string GetLogPath()
		{
			string result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup"))
			{
				if (registryKey == null)
				{
					result = null;
				}
				else
				{
					string text = registryKey.GetValue("MsiInstallPath") as string;
					if (text != null)
					{
						try
						{
							return Path.Combine(text, "Logging\\Resolver");
						}
						catch (ArgumentException)
						{
							ExTraceGlobals.ResolverTracer.TraceError<string>(0L, "Cannot use log path '{0}'.", text);
						}
					}
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0005AD64 File Offset: 0x00058F64
		private void InitializeAcceptedDomainsAndDefaultDomain()
		{
			PerTenantAcceptedDomainTable acceptedDomainTable = Components.Configuration.GetAcceptedDomainTable(this.organizationId);
			this.acceptedDomains = acceptedDomainTable.AcceptedDomainTable;
			this.defaultDomain = this.acceptedDomains.DefaultDomainName;
			if (string.IsNullOrEmpty(this.defaultDomain))
			{
				ExTraceGlobals.ResolverTracer.TraceError<OrganizationId>(0L, "Cannot find default authoritative domain for organization {0}.", this.organizationId);
				throw new DefaultAuthoritativeDomainNotFoundException(this.organizationId);
			}
		}

		// Token: 0x04000AFF RID: 2815
		private OrganizationId organizationId;

		// Token: 0x04000B00 RID: 2816
		private PerTenantTransportSettings transportSettings;

		// Token: 0x04000B01 RID: 2817
		private AcceptedDomainTable acceptedDomains;

		// Token: 0x04000B02 RID: 2818
		private string logPath;

		// Token: 0x04000B03 RID: 2819
		private IList<RoutingAddress> privilegedSenders;

		// Token: 0x04000B04 RID: 2820
		private string defaultDomain;
	}
}
