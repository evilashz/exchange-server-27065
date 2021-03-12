using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.ShadowRedundancy;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000258 RID: 600
	internal class RoutingDependencies
	{
		// Token: 0x060019F6 RID: 6646 RVA: 0x0006A011 File Offset: 0x00068211
		public RoutingDependencies(TransportAppConfig appConfig, ITransportConfiguration transportConfig)
		{
			this.appConfig = appConfig;
			this.transportConfig = transportConfig;
		}

		// Token: 0x170006D4 RID: 1748
		// (set) Token: 0x060019F7 RID: 6647 RVA: 0x0006A027 File Offset: 0x00068227
		public ShadowRedundancyComponent ShadowRedundancy
		{
			set
			{
				this.shadowRedundancy = value;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (set) Token: 0x060019F8 RID: 6648 RVA: 0x0006A030 File Offset: 0x00068230
		public UnhealthyTargetFilterComponent UnhealthyTargetFilter
		{
			set
			{
				this.unhealthyTargetFilter = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (set) Token: 0x060019F9 RID: 6649 RVA: 0x0006A039 File Offset: 0x00068239
		public CategorizerComponent Categorizer
		{
			set
			{
				this.categorizer = value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x0006A042 File Offset: 0x00068242
		public virtual bool IsProcessShuttingDown
		{
			get
			{
				return Components.ShuttingDown;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x0006A049 File Offset: 0x00068249
		public virtual string LocalComputerFqdn
		{
			get
			{
				return RoutingDependencies.localComputerFqdn;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x0006A050 File Offset: 0x00068250
		public virtual LocalLongFullPath LogPath
		{
			get
			{
				RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
				return this.transportConfig.LocalServer.TransportServer.RoutingTableLogPath;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x060019FD RID: 6653 RVA: 0x0006A077 File Offset: 0x00068277
		public virtual Unlimited<ByteQuantifiedSize> MaxLogDirectorySize
		{
			get
			{
				RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
				return this.transportConfig.LocalServer.TransportServer.RoutingTableLogMaxDirectorySize;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060019FE RID: 6654 RVA: 0x0006A09E File Offset: 0x0006829E
		public virtual EnhancedTimeSpan MaxLogFileAge
		{
			get
			{
				RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
				return this.transportConfig.LocalServer.TransportServer.RoutingTableLogMaxAge;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x0006A0C5 File Offset: 0x000682C5
		public virtual ProcessTransportRole ProcessTransportRole
		{
			get
			{
				RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
				return this.transportConfig.ProcessTransportRole;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001A00 RID: 6656 RVA: 0x0006A0E4 File Offset: 0x000682E4
		public virtual bool ShadowRedundancyPromotionEnabled
		{
			get
			{
				RoutingUtils.ThrowIfMissingDependency(this.appConfig, "AppConfig");
				RoutingUtils.ThrowIfMissingDependency(this.shadowRedundancy, "Shadow Redundancy");
				return this.shadowRedundancy.ShadowRedundancyManager.Configuration.Enabled && this.appConfig.ShadowRedundancy.ShadowRedundancyPromotionEnabled;
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x0006A13A File Offset: 0x0006833A
		public virtual bool IsUnhealthyFqdn(string fqdn, ushort port)
		{
			RoutingUtils.ThrowIfMissingDependency(this.unhealthyTargetFilter, "Unhealthy Target Filter");
			return this.unhealthyTargetFilter.UnhealthyTargetFqdnFilter.IsUnhealthy(new FqdnPortPair(fqdn, port));
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x0006A163 File Offset: 0x00068363
		public virtual void RegisterForLocalServerChanges(ConfigurationUpdateHandler<TransportServerConfiguration> handler)
		{
			RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
			this.transportConfig.LocalServerChanged += handler;
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x0006A181 File Offset: 0x00068381
		public virtual void RegisterForServiceControlConfigUpdates(EventHandler handler)
		{
			Components.ConfigChanged += handler;
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x0006A189 File Offset: 0x00068389
		public virtual bool ShouldShadowMailItem(IReadOnlyMailItem mailItem)
		{
			RoutingUtils.ThrowIfMissingDependency(this.shadowRedundancy, "Shadow Redundancy");
			return this.shadowRedundancy.ShadowRedundancyManager.ShouldShadowMailItem(mailItem);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x0006A1AC File Offset: 0x000683AC
		public virtual bool TryDeencapsulate(RoutingAddress address, out ProxyAddress innerAddress)
		{
			return Resolver.TryDeencapsulate(address, out innerAddress);
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x0006A1B8 File Offset: 0x000683B8
		public virtual bool TryGetServerForDatabase(Guid databaseGuid, out string fqdn)
		{
			fqdn = null;
			ActiveManager cachingActiveManagerInstance = ActiveManager.GetCachingActiveManagerInstance();
			GetServerForDatabaseFlags getServerForDatabaseFlags = GetServerForDatabaseFlags.BasicQuery;
			if (this.appConfig.Routing.DagRoutingEnabled)
			{
				getServerForDatabaseFlags |= GetServerForDatabaseFlags.IgnoreAdSiteBoundary;
			}
			DatabaseLocationInfo databaseLocationInfo = null;
			Exception ex = null;
			try
			{
				databaseLocationInfo = cachingActiveManagerInstance.GetServerForDatabase(databaseGuid, getServerForDatabaseFlags);
			}
			catch (DatabaseNotFoundException ex2)
			{
				ex = ex2;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			catch (StorageTransientException ex4)
			{
				ex = ex4;
			}
			if (databaseLocationInfo == null || string.IsNullOrEmpty(databaseLocationInfo.ServerFqdn))
			{
				RoutingDiag.Tracer.TraceError<Guid, object>((long)this.GetHashCode(), "Failed to obtain active server information for database <{0}>; exception details: {1}", databaseGuid, ex ?? "<null>");
				return false;
			}
			fqdn = databaseLocationInfo.ServerFqdn;
			return true;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x0006A270 File Offset: 0x00068470
		public virtual void UnregisterFromLocalServerChanges(ConfigurationUpdateHandler<TransportServerConfiguration> handler)
		{
			RoutingUtils.ThrowIfMissingDependency(this.transportConfig, "Configuration");
			this.transportConfig.LocalServerChanged -= handler;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x0006A28E File Offset: 0x0006848E
		public virtual void UnregisterFromServiceControlConfigUpdates(EventHandler handler)
		{
			Components.ConfigChanged -= handler;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0006A296 File Offset: 0x00068496
		public virtual void BifurcateRecipientsAndDefer(TransportMailItem mailItem, ICollection<MailRecipient> recipientsToBeForked, TaskContext taskContext, SmtpResponse deferResponse, TimeSpan deferTime, DeferReason deferReason)
		{
			RoutingUtils.ThrowIfMissingDependency(this.categorizer, "Categorizer");
			this.categorizer.BifurcateRecipientsAndDefer(mailItem, recipientsToBeForked, taskContext, deferResponse, deferTime, deferReason);
		}

		// Token: 0x04000C63 RID: 3171
		private static readonly string localComputerFqdn = ComputerInformation.DnsFullyQualifiedDomainName;

		// Token: 0x04000C64 RID: 3172
		private TransportAppConfig appConfig;

		// Token: 0x04000C65 RID: 3173
		private ITransportConfiguration transportConfig;

		// Token: 0x04000C66 RID: 3174
		private ShadowRedundancyComponent shadowRedundancy;

		// Token: 0x04000C67 RID: 3175
		private UnhealthyTargetFilterComponent unhealthyTargetFilter;

		// Token: 0x04000C68 RID: 3176
		private CategorizerComponent categorizer;
	}
}
