using System;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	internal class ADRunspaceServerSettingsProvider
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0001C6C6 File Offset: 0x0001A8C6
		internal ADRunspaceServerSettingsProvider()
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		public void ReportServerDown(string partitionFqdn, string serverFqdn, ADServerRole role)
		{
			SyncWrapper<PartitionBasedADRunspaceServerSettingsProvider> syncWrapper;
			if (this.internalProviders.TryGetValue(partitionFqdn, out syncWrapper) && syncWrapper.Value != null)
			{
				syncWrapper.Value.ReportServerDown(serverFqdn, role);
			}
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001C714 File Offset: 0x0001A914
		public ADServerInfo GetConfigDc(string forestFqdn)
		{
			PartitionBasedADRunspaceServerSettingsProvider providerForPartition = this.GetProviderForPartition(forestFqdn);
			if (providerForPartition != null)
			{
				return providerForPartition.ConfigDC;
			}
			return null;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0001C734 File Offset: 0x0001A934
		internal bool IsServerKnown(Fqdn serverFqdn)
		{
			foreach (SyncWrapper<PartitionBasedADRunspaceServerSettingsProvider> syncWrapper in this.internalProviders.Values)
			{
				if (syncWrapper.Value != null && syncWrapper.Value.IsServerKnown(serverFqdn))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0001C79C File Offset: 0x0001A99C
		internal ADServerInfo GetGcFromToken(string partitionFqdn, string token, out bool isInLocalSite, bool forestWideAffinityRequested = false)
		{
			PartitionBasedADRunspaceServerSettingsProvider providerForPartition = this.GetProviderForPartition(partitionFqdn);
			if (providerForPartition != null)
			{
				ADServerInfo gcFromToken = providerForPartition.GetGcFromToken(token, out isInLocalSite, forestWideAffinityRequested);
				if (gcFromToken != null)
				{
					return gcFromToken;
				}
			}
			if (Globals.IsDatacenter)
			{
				throw new ADTransientException(DirectoryStrings.ExceptionADTopologyNoServersForPartition(partitionFqdn));
			}
			throw new ADTransientException(DirectoryStrings.ExceptionADTopologyHasNoAvailableServersInForest(partitionFqdn));
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
		internal ADServerInfo[] GetGcFromToken(string partitionFqdn, string token, int serverCountRequested, out bool isInLocalSite, bool forestWideAffinityRequested = false)
		{
			PartitionBasedADRunspaceServerSettingsProvider providerForPartition = this.GetProviderForPartition(partitionFqdn);
			if (providerForPartition != null)
			{
				ADServerInfo[] gcFromToken = providerForPartition.GetGcFromToken(token, serverCountRequested, out isInLocalSite, forestWideAffinityRequested);
				if (gcFromToken != null)
				{
					return gcFromToken;
				}
			}
			if (Globals.IsDatacenter)
			{
				throw new ADTransientException(DirectoryStrings.ExceptionADTopologyNoServersForPartition(partitionFqdn));
			}
			throw new ADTransientException(DirectoryStrings.ExceptionADTopologyHasNoAvailableServersInForest(partitionFqdn));
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001C82C File Offset: 0x0001AA2C
		internal static ADRunspaceServerSettingsProvider GetInstance()
		{
			if (ADRunspaceServerSettingsProvider.staticInstance == null)
			{
				ADRunspaceServerSettingsProvider value = new ADRunspaceServerSettingsProvider();
				Interlocked.CompareExchange<ADRunspaceServerSettingsProvider>(ref ADRunspaceServerSettingsProvider.staticInstance, value, null);
			}
			return ADRunspaceServerSettingsProvider.staticInstance;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001C860 File Offset: 0x0001AA60
		private PartitionBasedADRunspaceServerSettingsProvider GetProviderForPartition(string partitionFqdn)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("partitionFqdn", partitionFqdn);
			TopologyProvider instance = TopologyProvider.GetInstance();
			SyncWrapper<PartitionBasedADRunspaceServerSettingsProvider> orAdd = this.internalProviders.GetOrAdd(partitionFqdn, (string key) => new SyncWrapper<PartitionBasedADRunspaceServerSettingsProvider>());
			PartitionBasedADRunspaceServerSettingsProvider value = orAdd.Value;
			if (value != null && Globals.GetTickDifference(value.LastTopoRecheck, Environment.TickCount) < (ulong)((long)instance.TopoRecheckIntervalMsec))
			{
				ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string>((long)this.GetHashCode(), "ADRunspaceServerSettingsProvider {0} ignoring topology version recheck for partition.", partitionFqdn);
				return value;
			}
			lock (orAdd)
			{
				value = orAdd.Value;
				if (value == null || Globals.GetTickDifference(value.LastTopoRecheck, Environment.TickCount) > (ulong)((long)TopologyProvider.GetInstance().TopoRecheckIntervalMsec))
				{
					int topologyVersion = instance.GetTopologyVersion(partitionFqdn);
					if (value == null || value.TopologyVersion != topologyVersion)
					{
						ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, int>((long)this.GetHashCode(), "ADRunspaceServerSettingsProvider {0}. Creating new provider version {1}", partitionFqdn, topologyVersion);
						PartitionBasedADRunspaceServerSettingsProvider value2;
						if (PartitionBasedADRunspaceServerSettingsProvider.TryCreateNew(partitionFqdn, instance, value, out value2))
						{
							orAdd.Value = value2;
							orAdd.Value.TopologyVersion = topologyVersion;
							orAdd.Value.LastTopoRecheck = Environment.TickCount;
						}
					}
				}
			}
			return orAdd.Value;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0001C99C File Offset: 0x0001AB9C
		[Conditional("DEBUG")]
		private void DbgCheckConstructorCaller()
		{
			string text = Environment.StackTrace.ToString();
			text.Contains("ADRunspaceServerSettingsProvider.GetInstance()");
		}

		// Token: 0x04000220 RID: 544
		private static ADRunspaceServerSettingsProvider staticInstance;

		// Token: 0x04000221 RID: 545
		private ConcurrentDictionary<string, SyncWrapper<PartitionBasedADRunspaceServerSettingsProvider>> internalProviders = new ConcurrentDictionary<string, SyncWrapper<PartitionBasedADRunspaceServerSettingsProvider>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0200006F RID: 111
		[Serializable]
		internal class ADServerInfoState : IComparable
		{
			// Token: 0x0600050E RID: 1294 RVA: 0x0001C9C2 File Offset: 0x0001ABC2
			public ADServerInfoState(ADServerInfo serverInfo) : this(serverInfo, false)
			{
			}

			// Token: 0x0600050F RID: 1295 RVA: 0x0001C9CC File Offset: 0x0001ABCC
			public ADServerInfoState(ADServerInfo serverInfo, bool isLazySite)
			{
				this.serverInfo = serverInfo;
				this.isDown = false;
				this.isLazySite = isLazySite;
				if (isLazySite)
				{
					this.isInLocalSite = false;
					return;
				}
				this.isInLocalSite = ADRunspaceServerSettingsProvider.ADServerInfoState.IsDcInLocalSite(serverInfo);
			}

			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000510 RID: 1296 RVA: 0x0001CA00 File Offset: 0x0001AC00
			public ADServerInfo ServerInfo
			{
				get
				{
					return this.serverInfo;
				}
			}

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000511 RID: 1297 RVA: 0x0001CA08 File Offset: 0x0001AC08
			// (set) Token: 0x06000512 RID: 1298 RVA: 0x0001CA10 File Offset: 0x0001AC10
			public bool IsDown
			{
				get
				{
					return this.isDown;
				}
				set
				{
					this.isDown = value;
				}
			}

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000513 RID: 1299 RVA: 0x0001CA19 File Offset: 0x0001AC19
			public bool IsInLocalSite
			{
				get
				{
					if (this.isLazySite)
					{
						this.isInLocalSite = ADRunspaceServerSettingsProvider.ADServerInfoState.IsDcInLocalSite(this.serverInfo);
						this.isLazySite = false;
					}
					return this.isInLocalSite;
				}
			}

			// Token: 0x06000514 RID: 1300 RVA: 0x0001CA44 File Offset: 0x0001AC44
			private static bool IsDcInLocalSite(ADServerInfo serverInfo)
			{
				bool flag = false;
				if (!string.IsNullOrEmpty(serverInfo.PartitionFqdn) && !PartitionId.IsLocalForestPartition(serverInfo.PartitionFqdn))
				{
					ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, string>((long)serverInfo.GetHashCode(), "Server {0} is in forest {1} which is not local forest, return IsDcInLocalSite == false", serverInfo.Fqdn, serverInfo.PartitionFqdn);
					return false;
				}
				if (ADRunspaceServerSettingsProvider.ADServerInfoState.localSiteName == null)
				{
					try
					{
						ADRunspaceServerSettingsProvider.ADServerInfoState.localSiteName = NativeHelpers.GetSiteName(false);
						ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string>((long)serverInfo.GetHashCode(), "Setting the local site name to {0}", ADRunspaceServerSettingsProvider.ADServerInfoState.localSiteName);
					}
					catch (CannotGetSiteInfoException)
					{
						ExTraceGlobals.ServerSettingsProviderTracer.TraceError((long)serverInfo.GetHashCode(), "GetSiteName has thrown a CannotGetSiteInfoException");
					}
				}
				if (ADRunspaceServerSettingsProvider.ADServerInfoState.localSiteName != null)
				{
					if (Globals.IsMicrosoftHostedOnly && !string.IsNullOrEmpty(serverInfo.SiteName))
					{
						flag = string.Equals(serverInfo.SiteName, ADRunspaceServerSettingsProvider.ADServerInfoState.localSiteName, StringComparison.OrdinalIgnoreCase);
						ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug((long)serverInfo.GetHashCode(), "local site is: {0}, server {1} is in site {2}, return IsDcInLocalSite == {3}", new object[]
						{
							ADRunspaceServerSettingsProvider.ADServerInfoState.localSiteName,
							serverInfo.Fqdn,
							serverInfo.SiteName,
							flag
						});
						return flag;
					}
					try
					{
						StringCollection dcSiteCoverage = NativeHelpers.GetDcSiteCoverage(serverInfo.Fqdn);
						flag = dcSiteCoverage.Contains(ADRunspaceServerSettingsProvider.ADServerInfoState.localSiteName);
						ExTraceGlobals.ServerSettingsProviderTracer.TraceDebug<string, string>((long)serverInfo.GetHashCode(), "Server {0} {1} in the local site", serverInfo.Fqdn, flag ? "is" : "is not");
					}
					catch (CannotGetSiteInfoException)
					{
						ExTraceGlobals.ServerSettingsProviderTracer.TraceError((long)serverInfo.GetHashCode(), "GetDcSiteCoverage has thrown a CannotGetSiteInfoException");
					}
				}
				return flag;
			}

			// Token: 0x06000515 RID: 1301 RVA: 0x0001CBC8 File Offset: 0x0001ADC8
			public int CompareTo(object obj)
			{
				if (obj == null)
				{
					return 1;
				}
				ADRunspaceServerSettingsProvider.ADServerInfoState adserverInfoState = obj as ADRunspaceServerSettingsProvider.ADServerInfoState;
				if (adserverInfoState == null)
				{
					throw new ArgumentException("obj is not of the required type");
				}
				return string.Compare(this.ServerInfo.Fqdn, adserverInfoState.ServerInfo.Fqdn, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000223 RID: 547
			private static string localSiteName;

			// Token: 0x04000224 RID: 548
			private bool isLazySite;

			// Token: 0x04000225 RID: 549
			private ADServerInfo serverInfo;

			// Token: 0x04000226 RID: 550
			private bool isDown;

			// Token: 0x04000227 RID: 551
			private bool isInLocalSite;
		}
	}
}
