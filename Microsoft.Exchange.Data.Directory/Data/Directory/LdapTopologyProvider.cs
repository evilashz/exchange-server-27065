using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.TopologyDiscovery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000147 RID: 327
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LdapTopologyProvider : TopologyProvider
	{
		// Token: 0x06000DE5 RID: 3557 RVA: 0x00040F35 File Offset: 0x0003F135
		internal LdapTopologyProvider()
		{
			this.localServerFqdn = NativeHelpers.GetLocalComputerFqdn(false);
			this.topologies = new ConcurrentDictionary<string, LdapTopologyProvider.MiniTopology>(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00040F59 File Offset: 0x0003F159
		internal override TopologyMode TopologyMode
		{
			get
			{
				return TopologyMode.Ldap;
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00040F5C File Offset: 0x0003F15C
		public override IList<TopologyVersion> GetAllTopologyVersions()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00040F64 File Offset: 0x0003F164
		public override IList<TopologyVersion> GetTopologyVersions(IList<string> partitionFqdns)
		{
			if (partitionFqdns == null)
			{
				throw new ArgumentNullException("partitionFqdns");
			}
			ExTraceGlobals.TopologyProviderTracer.Information<int>((long)this.GetHashCode(), "GetTopologyVersions. Partitions {0}", partitionFqdns.Count);
			TopologyVersion[] array = new TopologyVersion[partitionFqdns.Count];
			LdapTopologyProvider.MiniTopology miniTopology = null;
			for (int i = 0; i < partitionFqdns.Count; i++)
			{
				TopologyProvider.EnforceNonEmptyPartition(partitionFqdns[i]);
				int version = 1;
				if (this.topologies.TryGetValue(partitionFqdns[i], out miniTopology))
				{
					version = miniTopology.Version;
				}
				array[i] = new TopologyVersion(partitionFqdns[i], version);
			}
			if (ExTraceGlobals.TopologyProviderTracer.IsTraceEnabled(TraceType.InfoTrace))
			{
				ExTraceGlobals.TopologyProviderTracer.Information<string>((long)this.GetHashCode(), "GetTopologyVersions. Partitions {0}", string.Join(",", (object[])array));
			}
			return array;
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0004102C File Offset: 0x0003F22C
		protected override IList<ADServerInfo> InternalGetServersForRole(string partitionFqdn, IList<string> currentlyUsedServers, ADServerRole role, int serversRequested, bool forestWideAffinityRequested = false)
		{
			LdapTopologyProvider.MiniTopology miniTopology = new LdapTopologyProvider.MiniTopology(partitionFqdn);
			miniTopology = this.topologies.GetOrAdd(partitionFqdn, miniTopology);
			if (ExTraceGlobals.TopologyProviderTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.TopologyProviderTracer.TraceDebug((long)this.GetHashCode(), "PartitionFqdn {0}. GetServersForRole {1}, {2} current: [{3}], need {4} servers", new object[]
				{
					partitionFqdn,
					role,
					currentlyUsedServers.Count,
					string.Join(",", currentlyUsedServers ?? Enumerable.Empty<string>()),
					serversRequested
				});
			}
			ADServerInfo adserverInfo = null;
			if (miniTopology.DCInfo == null)
			{
				adserverInfo = this.GetDirectoryServer(partitionFqdn, ADRole.DomainController);
				miniTopology.SetServerInfo(adserverInfo, ADServerRole.DomainController);
				miniTopology.IncrementTopologyVersion();
				adserverInfo = null;
			}
			switch (role)
			{
			case ADServerRole.GlobalCatalog:
				adserverInfo = miniTopology.GCInfo;
				if (adserverInfo == null)
				{
					adserverInfo = this.GetDirectoryServer(partitionFqdn, ADRole.GlobalCatalog);
					miniTopology.SetServerInfo(adserverInfo, role);
					miniTopology.IncrementTopologyVersion();
				}
				break;
			case ADServerRole.DomainController:
			case ADServerRole.ConfigurationDomainController:
				adserverInfo = ((ADServerRole.DomainController == role) ? miniTopology.DCInfo : miniTopology.CDCInfo);
				if (adserverInfo == null)
				{
					adserverInfo = this.GetDirectoryServer(partitionFqdn, ADRole.DomainController);
					miniTopology.SetServerInfo(adserverInfo, role);
					miniTopology.IncrementTopologyVersion();
				}
				break;
			}
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "PartitionFqdn {0}. GetServerForRole returning 1 server {1}", partitionFqdn, adserverInfo.FqdnPlusPort);
			ADProviderPerf.AddDCInstance(adserverInfo.Fqdn);
			ADServerInfo adserverInfo2 = (ADServerInfo)adserverInfo.Clone();
			adserverInfo2.Mapping = (adserverInfo2.Fqdn.Equals((currentlyUsedServers == null || currentlyUsedServers.Count == 0) ? string.Empty : currentlyUsedServers[0], StringComparison.OrdinalIgnoreCase) ? 0 : -1);
			return new List<ADServerInfo>
			{
				adserverInfo2
			};
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x000411C0 File Offset: 0x0003F3C0
		public override ADServerInfo GetServerFromDomainDN(string distinguishedName, NetworkCredential credential)
		{
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Need server from domain {0}. Credentials {1} NULL", distinguishedName, (credential == null) ? "are" : "are NOT");
			ADServerInfo remoteServerFromDomainFqdn = this.GetRemoteServerFromDomainFqdn(NativeHelpers.CanonicalNameFromDistinguishedName(distinguishedName), credential);
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string>((long)this.GetHashCode(), "GetServerFromDomainDN returning {0}", remoteServerFromDomainFqdn.FqdnPlusPort);
			return remoteServerFromDomainFqdn;
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0004121E File Offset: 0x0003F41E
		public override ADServerInfo GetRemoteServerFromDomainFqdn(string domainFqdn, NetworkCredential credential)
		{
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Need server from remote domain {0} {1} credentials.", domainFqdn, (credential == null) ? "without" : "with");
			return LdapTopologyProvider.FindDirectoryServerForForestOrDomain(domainFqdn, credential, false);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00041250 File Offset: 0x0003F450
		public override void SetConfigDC(string partitionFqdn, string serverName, int port)
		{
			base.SetConfigDC(partitionFqdn, serverName, port);
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, string, int>((long)this.GetHashCode(), "PartitionFqdn {0} setting Config DC to {1}:{2}", partitionFqdn, serverName, port);
			LdapTopologyProvider.MiniTopology miniTopology = new LdapTopologyProvider.MiniTopology(partitionFqdn);
			miniTopology = this.topologies.GetOrAdd(partitionFqdn, miniTopology);
			miniTopology.SetServerInfo(new ADServerInfo(serverName, partitionFqdn, port, null, 100, AuthType.Kerberos, true), ADServerRole.ConfigurationDomainController);
			miniTopology.IncrementTopologyVersion();
			ADProviderPerf.AddDCInstance(serverName);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000412B8 File Offset: 0x0003F4B8
		internal override ADServerInfo GetConfigDCInfo(string partitionFqdn, bool throwOnFailure)
		{
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string>((long)this.GetHashCode(), "PartitionFqdn {0}. Get Config DC.", partitionFqdn);
			try
			{
				IList<ADServerInfo> serversForRole = base.GetServersForRole(partitionFqdn, new List<string>(), ADServerRole.ConfigurationDomainController, 1, false);
				if (serversForRole.Count == 0)
				{
					throw new NoSuitableServerFoundException(DirectoryStrings.ErrorNoSuitableDC(this.localServerFqdn, partitionFqdn));
				}
				return serversForRole[0];
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.TopologyProviderTracer.TraceError<string, string>((long)this.GetHashCode(), "PartitionFqdn {0}. Could not get Config DC: {1}", partitionFqdn, ex.Message);
				if (throwOnFailure)
				{
					throw;
				}
			}
			return null;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00041348 File Offset: 0x0003F548
		public override void ReportServerDown(string partitionFqdn, string serverName, ADServerRole role)
		{
			if (string.IsNullOrEmpty(serverName))
			{
				ExTraceGlobals.TopologyProviderTracer.TraceWarning<string>((long)this.GetHashCode(), "PartitionFqdn {0}. ServerName is null or empty", partitionFqdn);
				return;
			}
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			ArgumentValidator.ThrowIfNullOrEmpty("serverName", serverName);
			ExTraceGlobals.TopologyProviderTracer.TraceWarning<string, string, ADServerRole>((long)this.GetHashCode(), "PartitionFqdn {0}. {1} is down for role {2}. If this server is part of topology we will bump topology version.", partitionFqdn, serverName, role);
			LdapTopologyProvider.MiniTopology miniTopology = null;
			if (!this.topologies.TryGetValue(partitionFqdn, out miniTopology))
			{
				ExTraceGlobals.TopologyProviderTracer.TraceWarning<string>((long)this.GetHashCode(), "PartitionFqdn {0} NOT FOUND.", partitionFqdn);
				return;
			}
			ADServerInfo dcinfo = miniTopology.DCInfo;
			ADServerInfo cdcinfo = miniTopology.CDCInfo;
			ADServerInfo gcinfo = miniTopology.GCInfo;
			ADServerRole adserverRole = ADServerRole.None;
			if (dcinfo != null && serverName.Equals(dcinfo.Fqdn, StringComparison.OrdinalIgnoreCase))
			{
				miniTopology.ResetServerInfoForRole(ADServerRole.DomainController);
				adserverRole |= ADServerRole.DomainController;
			}
			if (cdcinfo != null && serverName.Equals(cdcinfo.Fqdn, StringComparison.OrdinalIgnoreCase))
			{
				miniTopology.ResetServerInfoForRole(ADServerRole.ConfigurationDomainController);
				adserverRole |= ADServerRole.ConfigurationDomainController;
			}
			if (gcinfo != null && gcinfo.Fqdn.Equals(serverName, StringComparison.OrdinalIgnoreCase))
			{
				miniTopology.ResetServerInfoForRole(ADServerRole.GlobalCatalog);
				adserverRole |= ADServerRole.GlobalCatalog;
			}
			if (adserverRole != ADServerRole.None)
			{
				miniTopology.IncrementTopologyVersion();
			}
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, ADServerRole, int>((long)this.GetHashCode(), "PartitionFqdn {0}. Role(s) {1} were reported as down. Topology version change {2}.", partitionFqdn, adserverRole, miniTopology.Version);
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x0004146C File Offset: 0x0003F66C
		public override int TopoRecheckIntervalMsec
		{
			get
			{
				if (!ExEnvironment.IsTest)
				{
					return 1000;
				}
				return 0;
			}
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0004147C File Offset: 0x0003F67C
		protected override ADServerInfo GetDefaultServerInfo(string partitionFqdn)
		{
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string>((long)this.GetHashCode(), "PartitionFqdn {0}. GetDefaultServerInfo", partitionFqdn);
			ADServerInfo result;
			try
			{
				IList<ADServerInfo> serversForRole = base.GetServersForRole(partitionFqdn, new List<string>(), ADServerRole.DomainController, 1, false);
				if (serversForRole.Count == 0)
				{
					throw new NoSuitableServerFoundException(DirectoryStrings.ErrorNoSuitableDC(this.localServerFqdn, partitionFqdn));
				}
				result = serversForRole[0];
			}
			catch (ADTransientException ex)
			{
				ExTraceGlobals.TopologyProviderTracer.TraceError<string, string>((long)this.GetHashCode(), "PartitionFqdn {0}. Could not get default server info: {1}", partitionFqdn, ex.Message);
				throw;
			}
			return result;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00041508 File Offset: 0x0003F708
		private static ADServerInfo FindDirectoryServerForForestOrDomain(string domainOrForestFqdn, NetworkCredential credential, bool requireGCs = false)
		{
			StringCollection stringCollection = requireGCs ? NativeHelpers.FindAllGlobalCatalogs(domainOrForestFqdn, null) : NativeHelpers.FindAllDomainControllers(domainOrForestFqdn, null);
			LocalizedString value = LocalizedString.Empty;
			LocalizedString empty = LocalizedString.Empty;
			string writableNC = null;
			string siteName = null;
			foreach (string text in stringCollection)
			{
				if (SuitabilityVerifier.IsServerSuitableIgnoreExceptions(text, requireGCs, credential, out writableNC, out siteName, out empty))
				{
					if (!requireGCs)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_GET_DC_FROM_DOMAIN, domainOrForestFqdn, new object[]
						{
							domainOrForestFqdn,
							text
						});
					}
					return new ADServerInfo(text, domainOrForestFqdn, requireGCs ? 3268 : 389, writableNC, 100, AuthType.Kerberos, true)
					{
						SiteName = siteName
					};
				}
				ExTraceGlobals.TopologyProviderTracer.TraceError(0L, "{0} {1} {2} was found not suitable. Will try to find another {1} in the forest\\domain. Error message: {3}", new object[]
				{
					domainOrForestFqdn,
					requireGCs ? "Global Catalog" : "Domain Controller",
					text,
					empty
				});
				value = DirectoryStrings.AppendLocalizedStrings(value, empty);
			}
			if (requireGCs)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_ALL_GC_DOWN, string.Empty, new object[]
				{
					domainOrForestFqdn,
					string.Empty
				});
				throw new NoSuitableServerFoundException(DirectoryStrings.ErrorNoSuitableGCInForest(domainOrForestFqdn, value));
			}
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_DSC_EVENT_GET_DC_FROM_DOMAIN_FAILED, domainOrForestFqdn, new object[]
			{
				domainOrForestFqdn
			});
			throw new NoSuitableServerFoundException(DirectoryStrings.ErrorNoSuitableDCInDomain(domainOrForestFqdn, value));
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x000416AC File Offset: 0x0003F8AC
		private ADServerInfo GetDirectoryServer(string partitionFqdn, ADRole role)
		{
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, ADRole>((long)this.GetHashCode(), "GetDirectoryServer PartitionFqdn {0}. Role {1}", partitionFqdn, role);
			LocatorFlags locatorFlags = LocatorFlags.ForceRediscovery | LocatorFlags.DirectoryServicesRequired | LocatorFlags.ReturnDnsName;
			string text = partitionFqdn;
			if (ADRole.GlobalCatalog == role)
			{
				ADObjectId rootDomainNamingContext = base.GetRootDomainNamingContext(partitionFqdn);
				ADObjectId domainNamingContext = base.GetDomainNamingContext(partitionFqdn);
				if (!rootDomainNamingContext.DistinguishedName.Equals(domainNamingContext.DistinguishedName, StringComparison.OrdinalIgnoreCase))
				{
					text = NativeHelpers.CanonicalNameFromDistinguishedName(rootDomainNamingContext.DistinguishedName);
				}
				locatorFlags |= LocatorFlags.GCRequired;
			}
			ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, string, LocatorFlags>((long)this.GetHashCode(), "GetDirectoryServer. Partition Fqdn {0} Parent Domain {1}. Flags {2}", partitionFqdn, text, locatorFlags);
			ADServerInfo serverInfo = new ADServerInfo(null, text, (ADRole.GlobalCatalog == role) ? 3268 : 389, null, 100, AuthType.Kerberos, true);
			PooledLdapConnection pooledLdapConnection = null;
			ADServerInfo adserverInfo = null;
			try
			{
				pooledLdapConnection = LdapConnectionPool.CreateOneTimeConnection(null, serverInfo, locatorFlags);
				if (!string.IsNullOrEmpty(pooledLdapConnection.SessionOptions.HostName))
				{
					adserverInfo = pooledLdapConnection.ADServerInfo.CloneWithServerNameResolved(pooledLdapConnection.SessionOptions.HostName);
				}
				ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "GetDirectoryServer. Partition Fqdn {0}. Server {1}", partitionFqdn, pooledLdapConnection.SessionOptions.HostName ?? string.Empty);
			}
			finally
			{
				if (pooledLdapConnection != null)
				{
					pooledLdapConnection.ReturnToPool();
				}
			}
			string text2;
			LocalizedString localizedString;
			if (adserverInfo != null && SuitabilityVerifier.IsServerSuitableIgnoreExceptions(adserverInfo.Fqdn, ADRole.GlobalCatalog == role, null, out text2, out localizedString))
			{
				return adserverInfo;
			}
			return LdapTopologyProvider.FindDirectoryServerForForestOrDomain(text, null, ADRole.GlobalCatalog == role);
		}

		// Token: 0x04000729 RID: 1833
		private readonly string localServerFqdn;

		// Token: 0x0400072A RID: 1834
		private ConcurrentDictionary<string, LdapTopologyProvider.MiniTopology> topologies;

		// Token: 0x02000148 RID: 328
		internal class MiniTopology
		{
			// Token: 0x06000DF3 RID: 3571 RVA: 0x00041800 File Offset: 0x0003FA00
			public MiniTopology(string partitionFqdn)
			{
				this.PartitionFqdn = partitionFqdn;
				this.version = 0;
			}

			// Token: 0x17000275 RID: 629
			// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x00041816 File Offset: 0x0003FA16
			// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0004181E File Offset: 0x0003FA1E
			public string PartitionFqdn { get; private set; }

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x00041827 File Offset: 0x0003FA27
			public int Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x0004182F File Offset: 0x0003FA2F
			// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00041837 File Offset: 0x0003FA37
			public ADServerInfo DCInfo { get; private set; }

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00041840 File Offset: 0x0003FA40
			// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00041848 File Offset: 0x0003FA48
			public ADServerInfo GCInfo { get; private set; }

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00041851 File Offset: 0x0003FA51
			// (set) Token: 0x06000DFC RID: 3580 RVA: 0x00041859 File Offset: 0x0003FA59
			public ADServerInfo CDCInfo { get; private set; }

			// Token: 0x06000DFD RID: 3581 RVA: 0x00041862 File Offset: 0x0003FA62
			public void IncrementTopologyVersion()
			{
				Interlocked.Increment(ref this.version);
			}

			// Token: 0x06000DFE RID: 3582 RVA: 0x00041870 File Offset: 0x0003FA70
			public void ResetServerInfoForRole(ADServerRole role)
			{
				if (role == ADServerRole.None)
				{
					throw new ArgumentException("Invalid ADServerRole value. None");
				}
				switch (role)
				{
				case ADServerRole.GlobalCatalog:
					this.GCInfo = null;
					return;
				case ADServerRole.DomainController:
					this.DCInfo = null;
					return;
				case ADServerRole.ConfigurationDomainController:
					this.CDCInfo = null;
					return;
				}
				throw new NotSupportedException("Role Not Supported");
			}

			// Token: 0x06000DFF RID: 3583 RVA: 0x000418CC File Offset: 0x0003FACC
			public void SetServerInfo(ADServerInfo serverInfo, ADServerRole role)
			{
				ArgumentValidator.ThrowIfNull("serverInfo", serverInfo);
				if (role == ADServerRole.None)
				{
					throw new ArgumentException("Invalid ADServerRole value. None");
				}
				switch (role)
				{
				case ADServerRole.GlobalCatalog:
				{
					this.GCInfo = serverInfo;
					if (!Globals.IsDatacenter || (this.DCInfo != null && this.CDCInfo != null))
					{
						return;
					}
					ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}. GC {1} will be set as DC or CDC", this.PartitionFqdn, serverInfo.Fqdn);
					ADServerInfo adserverInfo = serverInfo.CloneAsRole(ADRole.DomainController);
					if (this.DCInfo == null)
					{
						this.DCInfo = adserverInfo;
					}
					if (this.CDCInfo == null)
					{
						this.CDCInfo = adserverInfo;
						return;
					}
					return;
				}
				case ADServerRole.DomainController:
				case ADServerRole.ConfigurationDomainController:
				{
					if (this.CDCInfo == null)
					{
						this.CDCInfo = serverInfo;
					}
					if (this.DCInfo == null)
					{
						this.DCInfo = serverInfo;
					}
					if (!Globals.IsDatacenter || this.GCInfo != null)
					{
						return;
					}
					ExTraceGlobals.TopologyProviderTracer.TraceDebug<string, string>((long)this.GetHashCode(), "{0}. DC {1} will be set as GC", this.PartitionFqdn, serverInfo.Fqdn);
					ADServerInfo gcinfo = serverInfo.CloneAsRole(ADRole.GlobalCatalog);
					if (this.GCInfo == null)
					{
						this.GCInfo = gcinfo;
						return;
					}
					return;
				}
				}
				throw new NotSupportedException("Role Not Supported");
			}

			// Token: 0x0400072B RID: 1835
			private int version;
		}
	}
}
