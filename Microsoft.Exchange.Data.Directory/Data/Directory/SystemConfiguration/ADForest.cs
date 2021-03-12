using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000485 RID: 1157
	internal class ADForest
	{
		// Token: 0x0600347C RID: 13436 RVA: 0x000D0D40 File Offset: 0x000CEF40
		private ADForest(string domainController, string globalCatalog) : this(ADForest.LocalForestName, domainController, globalCatalog, null, true)
		{
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x000D0D54 File Offset: 0x000CEF54
		private ADForest(string fqdn, string domainController, string globalCatalog, NetworkCredential networkCredential, bool knownForest)
		{
			this.fqdn = fqdn;
			this.domainController = domainController;
			this.globalCatalog = globalCatalog;
			this.networkCredential = networkCredential;
			this.partitionId = (Datacenter.IsMicrosoftHostedOnly(true) ? new PartitionId(fqdn) : PartitionId.LocalForest);
			this.isKnownForest = knownForest;
			this.isLocalForest = ADForest.IsLocalForestFqdn(fqdn);
		}

		// Token: 0x17000F88 RID: 3976
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000D0DB3 File Offset: 0x000CEFB3
		private static string LocalForestName
		{
			get
			{
				if (ADForest.localForestName == null)
				{
					ADForest.localForestName = NativeHelpers.GetForestName();
				}
				return ADForest.localForestName;
			}
		}

		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000D0DCB File Offset: 0x000CEFCB
		private static ADForest LocalForestInstance
		{
			get
			{
				if (ADForest.localForestInstance == null)
				{
					ADForest.localForestInstance = new ADForest(null, null);
				}
				return ADForest.localForestInstance;
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000D0DE8 File Offset: 0x000CEFE8
		private static bool IsDcAlsoGcAndAvailable(string domainController, NetworkCredential credentials)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(domainController, true, ConsistencyMode.FullyConsistent, credentials, ADSessionSettings.FromRootOrgScopeSet(), 131, "IsDcAlsoGcAndAvailable", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADForest.cs");
			tenantOrTopologyConfigurationSession.UseGlobalCatalog = true;
			return tenantOrTopologyConfigurationSession.IsReadConnectionAvailable();
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000D0E28 File Offset: 0x000CF028
		private ITopologyConfigurationSession CreateConfigNcSession()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(this.domainController, true, ConsistencyMode.FullyConsistent, this.networkCredential, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.partitionId), 144, "CreateConfigNcSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADForest.cs");
			topologyConfigurationSession.UseGlobalCatalog = false;
			return topologyConfigurationSession;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000D0E70 File Offset: 0x000CF070
		private IConfigurationSession CreateDomainNcSession()
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(this.globalCatalog, true, ConsistencyMode.FullyConsistent, this.networkCredential, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(this.partitionId), 156, "CreateDomainNcSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADForest.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			tenantOrTopologyConfigurationSession.UseGlobalCatalog = true;
			tenantOrTopologyConfigurationSession.EnforceDefaultScope = false;
			return tenantOrTopologyConfigurationSession;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000D0EC6 File Offset: 0x000CF0C6
		internal static bool IsLocalForestFqdn(string fqdn)
		{
			return string.Equals(fqdn, ADForest.LocalForestName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000D0ED4 File Offset: 0x000CF0D4
		public static ADForest GetLocalForest()
		{
			return ADForest.GetLocalForest(null);
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x000D0EDC File Offset: 0x000CF0DC
		public static ADForest GetLocalForest(string domainController)
		{
			if (string.IsNullOrEmpty(domainController))
			{
				return ADForest.LocalForestInstance;
			}
			string text = null;
			if (ADForest.IsDcAlsoGcAndAvailable(domainController, null))
			{
				text = domainController;
			}
			return new ADForest(domainController, text);
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x000D0F0C File Offset: 0x000CF10C
		public static ADForest GetForest(string forestFqdn, NetworkCredential credentials)
		{
			if (string.IsNullOrEmpty(forestFqdn))
			{
				throw new ArgumentNullException("forestFqdn");
			}
			if (ADForest.IsLocalForestFqdn(forestFqdn))
			{
				if (credentials != null)
				{
					throw new ArgumentException("The use of credentials is not supported for the local forest '" + forestFqdn + "'");
				}
				return ADForest.GetLocalForest();
			}
			else
			{
				PartitionId partitionId = new PartitionId(forestFqdn);
				if (ADAccountPartitionLocator.IsKnownPartition(partitionId))
				{
					return ADForest.GetForest(partitionId);
				}
				ADServerInfo remoteServerFromDomainFqdn = TopologyProvider.GetInstance().GetRemoteServerFromDomainFqdn(forestFqdn, credentials);
				return new ADForest(forestFqdn, remoteServerFromDomainFqdn.Fqdn, null, credentials, false);
			}
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x000D0F88 File Offset: 0x000CF188
		public static ADForest GetForest(PartitionId partitionId)
		{
			if (null == partitionId)
			{
				throw new ArgumentNullException("partitionId");
			}
			if (ADForest.IsLocalForestFqdn(partitionId.ForestFQDN))
			{
				return ADForest.GetLocalForest();
			}
			IList<ADServerInfo> serversForRole = TopologyProvider.GetInstance().GetServersForRole(partitionId.ForestFQDN, new List<string>(0), ADServerRole.DomainController, 1, false);
			IList<ADServerInfo> serversForRole2 = TopologyProvider.GetInstance().GetServersForRole(partitionId.ForestFQDN, new List<string>(0), ADServerRole.GlobalCatalog, 1, false);
			if (serversForRole == null || serversForRole2 == null || serversForRole.Count == 0 || serversForRole2.Count == 0)
			{
				throw new ADOperationException(DirectoryStrings.CannotGetForestInfo(partitionId.ForestFQDN));
			}
			return new ADForest(partitionId.ForestFQDN, serversForRole[0].Fqdn, serversForRole2[0].Fqdn, null, true);
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x000D103C File Offset: 0x000CF23C
		public static ADDomain FindExternalDomain(string fqdn, NetworkCredential credential)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			if (credential == null)
			{
				throw new ArgumentNullException("credential");
			}
			if (string.IsNullOrEmpty(credential.UserName))
			{
				throw new ArgumentException("User name must be provided in the credential argument to perform this operation.");
			}
			ADServerInfo remoteServerFromDomainFqdn = TopologyProvider.GetInstance().GetRemoteServerFromDomainFqdn(fqdn, credential);
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(remoteServerFromDomainFqdn.Fqdn, true, ConsistencyMode.FullyConsistent, credential, ADSessionSettings.FromAccountPartitionRootOrgScopeSet(Datacenter.IsMicrosoftHostedOnly(true) ? new PartitionId(remoteServerFromDomainFqdn.Fqdn) : PartitionId.LocalForest), 316, "FindExternalDomain", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ADForest.cs");
			tenantOrTopologyConfigurationSession.UseConfigNC = false;
			tenantOrTopologyConfigurationSession.EnforceDefaultScope = false;
			ADDomain[] array = tenantOrTopologyConfigurationSession.Find<ADDomain>(new ADObjectId(NativeHelpers.DistinguishedNameFromCanonicalName(fqdn)), QueryScope.Base, null, null, 1);
			if (array == null || array.Length <= 0)
			{
				throw new ADExternalException(DirectoryStrings.ExceptionADTopologyNoSuchDomain(fqdn));
			}
			return array[0];
		}

		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x000D110D File Offset: 0x000CF30D
		public string Fqdn
		{
			get
			{
				return this.fqdn;
			}
		}

		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x000D1115 File Offset: 0x000CF315
		public bool IsLocalForest
		{
			get
			{
				return this.isLocalForest;
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000D111D File Offset: 0x000CF31D
		public ADCrossRef[] GetDomainPartitions()
		{
			return this.GetDomainPartitions(false);
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000D1126 File Offset: 0x000CF326
		public ADCrossRef[] GetDomainPartitions(bool topLevelOnly)
		{
			return this.GetDomainPartitions(topLevelOnly, new ExistsFilter(ADCrossRefSchema.DnsRoot));
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000D113C File Offset: 0x000CF33C
		private ADCrossRef[] GetDomainPartitions(bool topLevelOnly, QueryFilter userFilter)
		{
			ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "FindDomains with topLevelOnly = {0}", topLevelOnly ? "true" : "false");
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("GetDomainPartitions is not supported for forest " + this.Fqdn);
			}
			QueryFilter queryFilter = ADForest.domainFilter;
			if (topLevelOnly)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					new NotFilter(new ExistsFilter(ADCrossRefSchema.TrustParent))
				});
			}
			if (userFilter != null)
			{
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					userFilter
				});
			}
			IConfigurationSession configurationSession = this.CreateConfigNcSession();
			ADCrossRef[] array = configurationSession.Find<ADCrossRef>(null, QueryScope.SubTree, queryFilter, null, 0);
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug((long)this.GetHashCode(), "No CrossRef objects were found");
			}
			return array;
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000D1208 File Offset: 0x000CF408
		public ADCrossRef GetLocalDomainPartition()
		{
			string domainName = NativeHelpers.GetDomainName();
			return this.FindDomainPartitionByFqdn(domainName);
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000D1222 File Offset: 0x000CF422
		public ReadOnlyCollection<ADDomain> FindDomains()
		{
			return this.FindDomains(false);
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000D122B File Offset: 0x000CF42B
		public ReadOnlyCollection<ADDomain> FindTopLevelDomains()
		{
			return this.FindDomains(true);
		}

		// Token: 0x06003491 RID: 13457 RVA: 0x000D1234 File Offset: 0x000CF434
		private ReadOnlyCollection<ADDomain> FindDomains(bool topLevelOnly)
		{
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("FindDomains is not supported for forest " + this.Fqdn);
			}
			ADCrossRef[] domainPartitions = this.GetDomainPartitions(topLevelOnly);
			List<ADDomain> list = new List<ADDomain>();
			if (domainPartitions != null)
			{
				ADObjectId[] array = new ADObjectId[domainPartitions.Length];
				IConfigurationSession configurationSession = this.CreateDomainNcSession();
				for (int i = 0; i < domainPartitions.Length; i++)
				{
					array[i] = domainPartitions[i].NCName;
				}
				Result<ADDomain>[] array2 = configurationSession.ReadMultiple<ADDomain>(array);
				for (int j = 0; j < array2.Length; j++)
				{
					Result<ADDomain> result = array2[j];
					if (result.Data == null)
					{
						ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Domain not found: {0}", array[j].ToDNString());
					}
					else
					{
						ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Domain found: {0}", array[0].ToDNString());
						list.Add(result.Data);
					}
				}
			}
			return new ReadOnlyCollection<ADDomain>(list);
		}

		// Token: 0x06003492 RID: 13458 RVA: 0x000D132C File Offset: 0x000CF52C
		public ADDomain FindDomainByFqdn(string fqdn)
		{
			if (fqdn == null)
			{
				throw new ArgumentNullException("fqdn");
			}
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentException(DirectoryStrings.ExEmptyStringArgumentException("fqdn"), "fqdn");
			}
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("FindDomainByFqdn is not supported for forest " + this.Fqdn);
			}
			ADCrossRef adcrossRef = this.FindDomainPartitionByFqdn(fqdn);
			if (adcrossRef == null)
			{
				return null;
			}
			IConfigurationSession configurationSession = this.CreateDomainNcSession();
			ADDomain addomain = configurationSession.Read<ADDomain>(adcrossRef.NCName);
			if (addomain == null)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<ADObjectId>((long)this.GetHashCode(), "No Domain found at {0}", adcrossRef.NCName);
				return null;
			}
			ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Domain found: {0}", addomain.Name);
			return addomain;
		}

		// Token: 0x06003493 RID: 13459 RVA: 0x000D13E8 File Offset: 0x000CF5E8
		public ADCrossRef FindDomainPartitionByFqdn(string fqdn)
		{
			if (fqdn == null)
			{
				throw new ArgumentNullException("fqdn");
			}
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentException(DirectoryStrings.ExEmptyStringArgumentException("fqdn"), "fqdn");
			}
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("FindDomainPartitionByFqdn is not supported for forest " + this.Fqdn);
			}
			QueryFilter userFilter = new ComparisonFilter(ComparisonOperator.Equal, ADCrossRefSchema.DnsRoot, fqdn);
			ADCrossRef[] domainPartitions = this.GetDomainPartitions(false, userFilter);
			if (domainPartitions == null || domainPartitions.Length == 0)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "No CrossRef found using fqdn {0}", fqdn ?? "<null>");
				return null;
			}
			return domainPartitions[0];
		}

		// Token: 0x06003494 RID: 13460 RVA: 0x000D1488 File Offset: 0x000CF688
		public ADDomain FindLocalDomain()
		{
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("FindLocalDomain is not supported for forest " + this.Fqdn);
			}
			string domainName = NativeHelpers.GetDomainName();
			return this.FindDomainByFqdn(domainName);
		}

		// Token: 0x06003495 RID: 13461 RVA: 0x000D14C0 File Offset: 0x000CF6C0
		public ADDomain FindRootDomain()
		{
			return this.FindRootDomain(false);
		}

		// Token: 0x06003496 RID: 13462 RVA: 0x000D14CC File Offset: 0x000CF6CC
		public ADDomain FindRootDomain(bool readFromDc)
		{
			ExTraceGlobals.ADTopologyTracer.TraceDebug((long)this.GetHashCode(), "FindRootDomain");
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("FindRootDomain is not supported for forest " + this.Fqdn);
			}
			IConfigurationSession configurationSession;
			if (readFromDc)
			{
				configurationSession = this.CreateConfigNcSession();
				configurationSession.UseConfigNC = false;
			}
			else
			{
				configurationSession = this.CreateDomainNcSession();
			}
			ADObjectId rootDomainNamingContext = configurationSession.GetRootDomainNamingContext();
			if (rootDomainNamingContext == null)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug((long)this.GetHashCode(), "GetRootDomainNamingContext returned null");
				return null;
			}
			ADDomain[] array = null;
			try
			{
				array = configurationSession.Find<ADDomain>(rootDomainNamingContext, QueryScope.Base, null, null, 1);
			}
			catch (ADReferralException innerException)
			{
				if (!readFromDc || string.IsNullOrEmpty(this.domainController))
				{
					throw;
				}
				throw new ADOperationException(DirectoryStrings.ExceptionReferralWhenBoundToDomainController(rootDomainNamingContext.ToString(), this.domainController), innerException);
			}
			if (array == null || array.Length == 0)
			{
				ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "No domain found at {0}", rootDomainNamingContext.DistinguishedName);
				return null;
			}
			ExTraceGlobals.ADTopologyTracer.TraceDebug<string>((long)this.GetHashCode(), "Domain found: {0}", array[0].Name);
			return array[0];
		}

		// Token: 0x06003497 RID: 13463 RVA: 0x000D15E0 File Offset: 0x000CF7E0
		public ReadOnlyCollection<ADServer> FindAllGlobalCatalogs()
		{
			return this.FindAllGlobalCatalogs(true);
		}

		// Token: 0x06003498 RID: 13464 RVA: 0x000D15EC File Offset: 0x000CF7EC
		public ReadOnlyCollection<ADServer> FindAllGlobalCatalogs(bool includingRODC)
		{
			ITopologyConfigurationSession topologyConfigurationSession = this.CreateConfigNcSession();
			return topologyConfigurationSession.FindServerWithNtdsdsa(null, true, includingRODC);
		}

		// Token: 0x06003499 RID: 13465 RVA: 0x000D160C File Offset: 0x000CF80C
		public List<ADServer> FindAllGlobalCatalogsInLocalSite()
		{
			ITopologyConfigurationSession topologyConfigurationSession = this.CreateConfigNcSession();
			ADSite localSite = topologyConfigurationSession.GetLocalSite();
			List<ADServer> list = new List<ADServer>();
			if (localSite != null)
			{
				ReadOnlyCollection<ADServer> readOnlyCollection = this.FindAllGlobalCatalogs(false);
				foreach (ADServer adserver in readOnlyCollection)
				{
					if (adserver.Site.Equals(localSite.Id))
					{
						list.Add(adserver);
					}
				}
			}
			return list;
		}

		// Token: 0x0600349A RID: 13466 RVA: 0x000D1694 File Offset: 0x000CF894
		public ADDomainTrustInfo[] FindAllLocalDomainTrustedDomains()
		{
			return this.FindTrustedDomains(NativeHelpers.GetDomainName());
		}

		// Token: 0x0600349B RID: 13467 RVA: 0x000D16A4 File Offset: 0x000CF8A4
		public ADDomainTrustInfo[] FindTrustedDomains(string domainFqdn)
		{
			if (string.IsNullOrEmpty(domainFqdn))
			{
				throw new ArgumentNullException("domainFqdn");
			}
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("FindAllLocalDomainTrustedDomains is only supported for local forest and account forests");
			}
			if (this.FindDomainByFqdn(domainFqdn) == null)
			{
				throw new ADExternalException(DirectoryStrings.ExceptionADTopologyNoSuchDomain(domainFqdn));
			}
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new BitMaskAndFilter(ADDomainTrustInfoSchema.TrustDirection, 2UL),
				new ComparisonFilter(ComparisonOperator.Equal, ADDomainTrustInfoSchema.TrustType, TrustTypeFlag.Uplevel),
				new NotFilter(new BitMaskAndFilter(ADDomainTrustInfoSchema.TrustAttributes, 32UL)),
				new NotFilter(new BitMaskAndFilter(ADDomainTrustInfoSchema.TrustAttributes, 8UL))
			});
			return this.FindTrustRelationships(domainFqdn, filter);
		}

		// Token: 0x0600349C RID: 13468 RVA: 0x000D1750 File Offset: 0x000CF950
		public ADDomainTrustInfo[] FindAllTrustedForests()
		{
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("FindAllLocalDomainTrustedDomains is only supported for the local forest and account forest.");
			}
			return this.FindTrustRelationships(this.fqdn, ADForest.forestTrustFilter);
		}

		// Token: 0x0600349D RID: 13469 RVA: 0x000D1778 File Offset: 0x000CF978
		public ADDomainTrustInfo[] FindTrustRelationshipsForDomain(string trustedDomainFqnd)
		{
			if (!this.isKnownForest)
			{
				throw new NotSupportedException("FindAllLocalDomainTrustedDomains is only supported for the local forest and account forest.");
			}
			return this.FindTrustRelationships(this.fqdn, QueryFilter.AndTogether(new QueryFilter[]
			{
				ADForest.forestTrustFilter,
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, trustedDomainFqnd)
			}));
		}

		// Token: 0x0600349E RID: 13470 RVA: 0x000D17C8 File Offset: 0x000CF9C8
		private ADDomainTrustInfo[] FindTrustRelationships(string fqdn, QueryFilter filter)
		{
			ADObjectId rootId = new ADObjectId("CN=System," + NativeHelpers.DistinguishedNameFromCanonicalName(fqdn));
			IConfigurationSession configurationSession = this.CreateDomainNcSession();
			return configurationSession.Find<ADDomainTrustInfo>(rootId, QueryScope.SubTree, filter, null, 0);
		}

		// Token: 0x040023D0 RID: 9168
		private static readonly QueryFilter domainFilter = new BitMaskAndFilter(ADConfigurationObjectSchema.SystemFlags, 3UL);

		// Token: 0x040023D1 RID: 9169
		private static readonly QueryFilter forestTrustFilter = new AndFilter(new QueryFilter[]
		{
			new BitMaskAndFilter(ADDomainTrustInfoSchema.TrustDirection, 2UL),
			new ComparisonFilter(ComparisonOperator.Equal, ADDomainTrustInfoSchema.TrustType, TrustTypeFlag.Uplevel),
			new BitMaskAndFilter(ADDomainTrustInfoSchema.TrustAttributes, 8UL)
		});

		// Token: 0x040023D2 RID: 9170
		private readonly bool isKnownForest;

		// Token: 0x040023D3 RID: 9171
		private readonly bool isLocalForest;

		// Token: 0x040023D4 RID: 9172
		private static ADForest localForestInstance;

		// Token: 0x040023D5 RID: 9173
		private static string localForestName;

		// Token: 0x040023D6 RID: 9174
		private string fqdn;

		// Token: 0x040023D7 RID: 9175
		private string domainController;

		// Token: 0x040023D8 RID: 9176
		private string globalCatalog;

		// Token: 0x040023D9 RID: 9177
		private PartitionId partitionId;

		// Token: 0x040023DA RID: 9178
		private NetworkCredential networkCredential;
	}
}
