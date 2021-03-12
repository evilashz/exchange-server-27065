using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Hygiene.Cache.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000E2 RID: 226
	internal class GlobalConfigSession
	{
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0001B670 File Offset: 0x00019870
		private IConfigDataProvider CompositeProvider
		{
			get
			{
				if (this.compositeProvider == null)
				{
					lock (this)
					{
						this.compositeProvider = (this.compositeProvider ?? ConfigDataProviderFactory.CacheFallbackDefault.Create(DatabaseType.Directory));
					}
				}
				return this.compositeProvider;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0001B6D0 File Offset: 0x000198D0
		private IConfigDataProvider WebStoreDataProvider
		{
			get
			{
				if (this.webStoreDataProvider == null)
				{
					lock (this)
					{
						this.webStoreDataProvider = (this.webStoreDataProvider ?? ConfigDataProviderFactory.Default.Create(DatabaseType.Directory));
					}
				}
				return this.webStoreDataProvider;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0001B730 File Offset: 0x00019930
		private IBloomFilterDataProvider BloomFilterProvider
		{
			get
			{
				return GlobalConfigSession.bloomFilterProvider.Value;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0001B73C File Offset: 0x0001993C
		public void Save(OnDemandQueryRequest reportRequest)
		{
			if (reportRequest == null)
			{
				throw new ArgumentNullException("reportRequest");
			}
			reportRequest[ADObjectSchema.OrganizationalUnitRoot] = GlobalConfigSession.onDemandReportsFixedTenantId;
			this.WebStoreDataProvider.Save(reportRequest);
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0001B770 File Offset: 0x00019970
		public bool IsRegionEmailOptout()
		{
			IEnumerable<RegionEmailFilter> regionEmailOptout = this.GetRegionEmailOptout();
			return regionEmailOptout.Any<RegionEmailFilter>() && regionEmailOptout.First<RegionEmailFilter>().Enabled;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0001B799 File Offset: 0x00019999
		public IEnumerable<RegionEmailFilter> GetRegionEmailOptout()
		{
			return this.WebStoreDataProvider.Find<RegionEmailFilter>(null, null, false, null).Cast<RegionEmailFilter>();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0001B894 File Offset: 0x00019A94
		public IEnumerable<OnDemandQueryRequest> FindReportRequestsByTenant(Guid tenantId, Guid? requestId = null, DateTime? submissionDateTimeStart = null)
		{
			QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, GlobalConfigSession.onDemandReportsFixedTenantId);
			if (requestId != null)
			{
				return (from OnDemandQueryRequest r in this.WebStoreDataProvider.Find<OnDemandQueryRequest>(QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					new ComparisonFilter(ComparisonOperator.Equal, OnDemandQueryRequestSchema.RequestId, requestId.Value)
				}), null, false, null)
				where submissionDateTimeStart == null || r.SubmissionTime >= submissionDateTimeStart.Value.Subtract(TimeSpan.FromSeconds(1.0))
				select r).Cache<OnDemandQueryRequest>();
			}
			return (from r in new ConfigDataProviderPagedReader<OnDemandQueryRequest>(this.WebStoreDataProvider, null, queryFilter, null, 500)
			where r.TenantId == tenantId && (requestId == null || r.RequestId == requestId) && (submissionDateTimeStart == null || r.SubmissionTime >= submissionDateTimeStart.Value.Subtract(TimeSpan.FromSeconds(1.0)))
			select r).Cache<OnDemandQueryRequest>();
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0001B9F8 File Offset: 0x00019BF8
		public IEnumerable<OnDemandQueryRequest> FindPagedReportRequests(IEnumerable<OnDemandQueryType> queryTypes, IEnumerable<OnDemandQueryRequestStatus> requestStatuses, ref string pageCookie, out bool complete, int pageSize = 100)
		{
			QueryFilter pagingQueryFilter = PagingHelper.GetPagingQueryFilter(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, GlobalConfigSession.onDemandReportsFixedTenantId), pageCookie);
			IEnumerable<OnDemandQueryRequest> result = (from OnDemandQueryRequest r in this.WebStoreDataProvider.FindPaged<OnDemandQueryRequest>(pagingQueryFilter, null, false, null, pageSize)
			where queryTypes.Any((OnDemandQueryType t) => t == r.QueryType) && requestStatuses.Any((OnDemandQueryRequestStatus s) => s == r.RequestStatus)
			select r).Cache<OnDemandQueryRequest>();
			pageCookie = PagingHelper.GetProcessedCookie(pagingQueryFilter, out complete);
			return result;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0001BA79 File Offset: 0x00019C79
		internal ADMiniDomain GetDomainByDomainNames(IEnumerable<string> domainNames)
		{
			if (domainNames != null && domainNames.Any<string>())
			{
				if (domainNames.All((string item) => !string.IsNullOrEmpty(item)))
				{
					return null;
				}
			}
			throw new ArgumentNullException("domainNames");
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0001BAB7 File Offset: 0x00019CB7
		internal void FindEnabledInboundConnectorsByIPAddressOrCertificate(string ipAddress, IEnumerable<string> certificateFqdns, out IEnumerable<TenantInboundConnector> certConnectors, out IEnumerable<TenantInboundConnector> ipConnectors)
		{
			if (string.IsNullOrEmpty(ipAddress) && (certificateFqdns == null || !certificateFqdns.Any<string>()))
			{
				throw new ArgumentException("Both ipAddress and certificateFqdns cannot be empty");
			}
			certConnectors = GlobalConfigSession.FindInboundConnectorsByCertificate(this.CompositeProvider, certificateFqdns, true);
			ipConnectors = GlobalConfigSession.FindInboundConnectorsByOutboundIp(this.CompositeProvider, ipAddress, true);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001BAF8 File Offset: 0x00019CF8
		internal bool IsRecipientValid(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentNullException("emailAddress");
			}
			return this.BloomFilterProvider.Check<UserExtendedPropertiesEmailAddress>(new ComparisonFilter(ComparisonOperator.Equal, UserExtendedPropertiesEmailAddress.UserEmailAddressProp, emailAddress.ToLower())) || this.BloomFilterProvider.Check<GroupExtendedPropertiesEmailAddress>(new ComparisonFilter(ComparisonOperator.Equal, GroupExtendedPropertiesEmailAddress.GroupEmailAddressProp, emailAddress.ToLower())) || this.BloomFilterProvider.Check<ContactExtendedPropertiesEmailAddress>(new ComparisonFilter(ComparisonOperator.Equal, ContactExtendedPropertiesEmailAddress.ContactEmailAddressProp, emailAddress.ToLower()));
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001BB79 File Offset: 0x00019D79
		public bool TryFindMatchingDomain(SmtpDomain inputDomain, out SmtpDomain bestMatch, out bool isExactMatch)
		{
			if (inputDomain == null)
			{
				throw new ArgumentNullException("inputDomain");
			}
			return GlobalConfigSession.TryFindMatchingDomainInternal(inputDomain, this.BloomFilterProvider, out bestMatch, out isExactMatch);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0001BB98 File Offset: 0x00019D98
		internal ADMiniUser GetUserByCertificate(string certificateSubjectName, string certificateIssuerName)
		{
			if (string.IsNullOrEmpty(certificateSubjectName))
			{
				throw new ArgumentNullException("certificateSubjectName");
			}
			if (string.IsNullOrEmpty(certificateIssuerName))
			{
				throw new ArgumentNullException("certificateIssuerName");
			}
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, UserCertificate.CertificateSubjectNameProp, certificateSubjectName),
				new ComparisonFilter(ComparisonOperator.Equal, UserCertificate.CertificateIssuerNameProp, certificateIssuerName)
			});
			return GlobalConfigSession.GetMiniUser(this.WebStoreDataProvider, filter);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0001BC04 File Offset: 0x00019E04
		internal IEnumerable<TenantIPInfo> FindTenantIPs(TenantIPCookie tenantIPCookie)
		{
			IEnumerable<TenantIPInfo> result;
			try
			{
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, DalHelper.StartTimeProp, tenantIPCookie.UpdateWatermark());
				IConfigurable[] source = this.WebStoreDataProvider.Find<TenantIPInfo>(filter, null, false, null);
				tenantIPCookie.CommitNewWatermark();
				result = source.Cast<TenantIPInfo>();
			}
			catch
			{
				tenantIPCookie.RevertToOldWatermark();
				throw;
			}
			return result;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0001BC64 File Offset: 0x00019E64
		internal IEnumerable<TenantIPInfo> FindPagedTenantIPs(ref string pageCookie, int pageSize)
		{
			List<TenantIPInfo> list = new List<TenantIPInfo>();
			string text = pageCookie ?? string.Empty;
			foreach (object propertyValue in ((IPartitionedDataProvider)this.WebStoreDataProvider).GetAllPhysicalPartitions())
			{
				QueryFilter baseQueryFilter = new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PhysicalInstanceKeyProp, propertyValue);
				QueryFilter pagingQueryFilter = PagingHelper.GetPagingQueryFilter(baseQueryFilter, text);
				list.AddRange(this.WebStoreDataProvider.FindPaged<TenantIPInfo>(pagingQueryFilter, null, false, null, pageSize));
				bool flag = true;
				text = PagingHelper.GetProcessedCookie(pagingQueryFilter, out flag);
			}
			pageCookie = text;
			return list;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0001BCEC File Offset: 0x00019EEC
		internal FfoTenant GetTenantByName(string tenantName)
		{
			ComparisonFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, tenantName);
			return (FfoTenant)this.WebStoreDataProvider.Find<FfoTenant>(filter, null, false, null).FirstOrDefault<IConfigurable>();
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0001BD24 File Offset: 0x00019F24
		internal IEnumerable<ProbeOrganizationInfo> GetProbeOrganizations(string featureTag)
		{
			if (string.IsNullOrEmpty(featureTag))
			{
				throw new ArgumentNullException("featureTag");
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.RawName, featureTag);
			return this.WebStoreDataProvider.Find<ProbeOrganizationInfo>(filter, null, false, null).Cast<ProbeOrganizationInfo>();
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0001BD68 File Offset: 0x00019F68
		internal IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			if (typeof(T) != typeof(ProbeOrganizationInfo) && typeof(T) != typeof(TenantConfigurationCacheEntry))
			{
				throw new ArgumentException(string.Format("The type {0} is not supported for global session Find<T>; please use a scoped DAL session.", typeof(T).Name));
			}
			return this.WebStoreDataProvider.Find<T>(filter, rootId, deepSearch, sortBy).ToArray<IConfigurable>();
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0001BDDF File Offset: 0x00019FDF
		internal void Save(IConfigurable configurable)
		{
			if (!(configurable is ProbeOrganizationInfo) && !(configurable is TenantConfigurationCacheEntry))
			{
				throw new ArgumentException(string.Format("The type {0} is not supported for the global session; please use a scoped DAL session instead.", configurable.GetType().ToString()));
			}
			this.WebStoreDataProvider.Save(configurable);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0001BE18 File Offset: 0x0001A018
		internal void Delete(IConfigurable configurable)
		{
			if (!(configurable is ProbeOrganizationInfo) && !(configurable is TenantConfigurationCacheEntry))
			{
				throw new ArgumentException(string.Format("The type {0} is not supported for the global session; please use a scoped DAL session instead.", configurable.GetType().ToString()));
			}
			this.WebStoreDataProvider.Delete(configurable);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001BE51 File Offset: 0x0001A051
		internal IEnumerable<TenantConfigurationCacheEntry> FindPinnedTenantConfigurationCacheEntries()
		{
			return this.Find<TenantConfigurationCacheEntry>(null, null, false, null).Cast<TenantConfigurationCacheEntry>();
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0001BE64 File Offset: 0x0001A064
		internal IPagedReader<TenantConfigurationCacheEntry> FindPagedTenantConfigurationCacheEntries(int pageSize = 100)
		{
			List<IPagedReader<TenantConfigurationCacheEntry>> list = new List<IPagedReader<TenantConfigurationCacheEntry>>();
			foreach (object propertyValue in ((IPartitionedDataProvider)this.WebStoreDataProvider).GetAllPhysicalPartitions())
			{
				list.Add(new ConfigDataProviderPagedReader<TenantConfigurationCacheEntry>(this.webStoreDataProvider, null, new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PhysicalInstanceKeyProp, propertyValue), null, pageSize));
			}
			return new CompositePagedReader<TenantConfigurationCacheEntry>(list.ToArray());
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001BEE4 File Offset: 0x0001A0E4
		internal IEnumerable<TenantInboundConnector> FindTenantInboundConnectorsForTenantIds(IEnumerable<Guid> tenantIds)
		{
			if (tenantIds == null)
			{
				throw new ArgumentNullException("tenantIds");
			}
			IEnumerable<Guid> enumerable = from tenantId in tenantIds.Distinct<Guid>()
			where tenantId != Guid.Empty
			select tenantId;
			if (enumerable.Count<Guid>() == 0)
			{
				throw new ArgumentException(HygieneDataStrings.ErrorEmptyList, "tenantIds");
			}
			Dictionary<object, List<Guid>> dictionary = DalHelper.SplitByPhysicalInstance<Guid>((IHashBucket)this.WebStoreDataProvider, enumerable, (Guid i) => i.ToString());
			List<TenantInboundConnector> list = new List<TenantInboundConnector>();
			foreach (object obj in dictionary.Keys)
			{
				list.AddRange(this.webStoreDataProvider.Find<TenantInboundConnector>(QueryFilter.AndTogether(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, DalHelper.TenantIds, new MultiValuedProperty<Guid>(dictionary[obj])),
					new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PhysicalInstanceKeyProp, obj)
				}), null, false, null).Cast<TenantInboundConnector>());
			}
			return list;
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001C2E0 File Offset: 0x0001A4E0
		private static IEnumerable<TenantInboundConnector> GetInboundConnectors(IConfigDataProvider dataProvider, ADPropertyDefinition propertyDefinition, IEnumerable<string> propertyValues, bool enabledOnly)
		{
			IEnumerable<ComparisonFilter> propertyNameFilters = from propertyValue in propertyValues
			select new ComparisonFilter(ComparisonOperator.Equal, propertyDefinition, propertyValue);
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				QueryFilter.OrTogether(propertyNameFilters.ToArray<QueryFilter>()),
				new ComparisonFilter(ComparisonOperator.Equal, DalHelper.CacheFailoverModeProp, CacheFailoverMode.BloomFilter)
			});
			IEnumerable<IConfigurable> inboundConnectors = dataProvider.Find<TenantInboundConnector>(filter, null, false, null);
			foreach (IConfigurable configurable in inboundConnectors)
			{
				TenantInboundConnector inboundConnector = (TenantInboundConnector)configurable;
				if (!enabledOnly || inboundConnector.Enabled)
				{
					FfoDirectorySession.FixDistinguishedName(inboundConnector, DalHelper.GetTenantDistinguishedName(inboundConnector.OrganizationalUnitRoot.ObjectGuid.ToString()), inboundConnector.OrganizationalUnitRoot.ObjectGuid, inboundConnector.Id.ObjectGuid, null);
					yield return inboundConnector;
				}
			}
			yield break;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0001C314 File Offset: 0x0001A514
		private static ADMiniUser GetMiniUser(IConfigDataProvider dataProvider, QueryFilter filter)
		{
			IConfigurable[] array = dataProvider.Find<ADMiniUser>(filter, null, false, null);
			if (array == null || array.Length == 0)
			{
				return null;
			}
			if (array.Length > 1)
			{
				throw new AmbiguousMatchException(string.Format("Found multiple entries for given query filter. QueryFilter: {0}", filter.ToString()));
			}
			return (ADMiniUser)array[0];
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0001C35C File Offset: 0x0001A55C
		internal static IEnumerable<TenantInboundConnector> FindInboundConnectorsByOutboundIp(IConfigDataProvider dataProvider, string ipAddress, bool enabledOnly)
		{
			if (string.IsNullOrEmpty(ipAddress))
			{
				return GlobalConfigSession.emptyInboundConnectorArray;
			}
			return GlobalConfigSession.GetInboundConnectors(dataProvider, TenantInboundConnectorSchema.RemoteIPRanges, new string[]
			{
				ipAddress
			}, enabledOnly).ToArray<TenantInboundConnector>();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0001C394 File Offset: 0x0001A594
		internal static IEnumerable<TenantInboundConnector> FindInboundConnectorsByCertificate(IConfigDataProvider dataProvider, IEnumerable<string> certificateFqdns, bool enabledOnly)
		{
			if (certificateFqdns == null || !certificateFqdns.Any<string>())
			{
				return GlobalConfigSession.emptyInboundConnectorArray;
			}
			HashSet<string> hashSet = null;
			try
			{
				hashSet = GlobalConfigSession.GetSearchableCertificates(certificateFqdns);
			}
			catch (Exception ex)
			{
				if (RetryHelper.IsSystemFatal(ex))
				{
					throw;
				}
			}
			return GlobalConfigSession.GetInboundConnectors(dataProvider, TenantInboundConnectorSchema.TlsSenderCertificateName, (hashSet != null && hashSet.Any<string>()) ? hashSet : certificateFqdns, enabledOnly).ToArray<TenantInboundConnector>();
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0001C3FC File Offset: 0x0001A5FC
		internal static bool TryFindMatchingDomainInternal(SmtpDomain inputDomain, IBloomFilterDataProvider dataProvider, out SmtpDomain bestMatch, out bool isExactMatch)
		{
			string text = inputDomain.Domain.ToLower();
			if (dataProvider.Check<AcceptedDomain>(new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.DomainName, text)))
			{
				bestMatch = inputDomain;
				isExactMatch = true;
				return true;
			}
			foreach (string text2 in GlobalConfigSession.ExpandSubdomains(text))
			{
				if (dataProvider.Check<AcceptedDomain>(new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.DomainName, "*." + text2)))
				{
					bestMatch = new SmtpDomain(text2);
					isExactMatch = false;
					return true;
				}
			}
			bestMatch = null;
			isExactMatch = false;
			return false;
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x0001C4A0 File Offset: 0x0001A6A0
		private static HashSet<string> GetSearchableCertificates(IEnumerable<string> certificateFqdns)
		{
			HashSet<string> hashSet = new HashSet<string>();
			if (certificateFqdns != null && certificateFqdns.Any<string>())
			{
				foreach (string text in certificateFqdns)
				{
					if (!string.IsNullOrWhiteSpace(text))
					{
						if (!hashSet.Contains(text))
						{
							hashSet.Add(text);
						}
						string text2 = string.Empty;
						string text3 = string.Empty;
						SmtpX509Identifier smtpX509Identifier;
						SmtpDomain smtpDomain;
						if (SmtpX509Identifier.TryParse(text, out smtpX509Identifier))
						{
							if (smtpX509Identifier != null && smtpX509Identifier.SubjectCommonName != null && smtpX509Identifier.SubjectCommonName.SmtpDomain != null)
							{
								text2 = smtpX509Identifier.SubjectCommonName.SmtpDomain.Domain;
							}
						}
						else if (SmtpDomain.TryParse(text, out smtpDomain) && smtpDomain != null)
						{
							text2 = smtpDomain.Domain;
						}
						if (!string.IsNullOrWhiteSpace(text2))
						{
							int num = -1;
							do
							{
								num = text2.IndexOf('.', num + 1);
								if (num != -1)
								{
									if (!string.IsNullOrWhiteSpace(text3))
									{
										string item = "*." + text3;
										if (!hashSet.Contains(item))
										{
											hashSet.Add(item);
										}
									}
									text3 = text2.Substring(num + 1);
								}
							}
							while (num != -1);
						}
					}
				}
			}
			return hashSet;
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x0001C5D8 File Offset: 0x0001A7D8
		internal IEnumerable<UserExtendedPropertiesEmailAddress> FindPagedUserExtendedPropertiesEmailAddress(ref string[] cookie, int pageSize, out bool isComplete)
		{
			return this.FindPagedGenericData<UserExtendedPropertiesEmailAddress>(ref cookie, pageSize, out isComplete);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x0001C5E3 File Offset: 0x0001A7E3
		internal IEnumerable<GroupExtendedPropertiesEmailAddress> FindPagedGroupExtendedPropertiesEmailAddress(ref string[] cookie, int pageSize, out bool isComplete)
		{
			return this.FindPagedGenericData<GroupExtendedPropertiesEmailAddress>(ref cookie, pageSize, out isComplete);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0001C5EE File Offset: 0x0001A7EE
		internal IEnumerable<ContactExtendedPropertiesEmailAddress> FindPagedContactExtendedPropertiesEmailAddress(ref string[] cookie, int pageSize, out bool isComplete)
		{
			return this.FindPagedGenericData<ContactExtendedPropertiesEmailAddress>(ref cookie, pageSize, out isComplete);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0001C5F9 File Offset: 0x0001A7F9
		internal IEnumerable<AcceptedDomain> FindPagedAcceptedDomains(ref string[] cookie, int pageSize, out bool isComplete)
		{
			return this.FindPagedGenericData<AcceptedDomain>(ref cookie, pageSize, out isComplete);
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001C724 File Offset: 0x0001A924
		internal static IEnumerable<string> ExpandSubdomains(string fqdn)
		{
			int dotIndex = fqdn.IndexOf('.');
			do
			{
				int nextIndex = fqdn.IndexOf('.', dotIndex + 1);
				if (nextIndex != -1)
				{
					yield return fqdn.Substring(dotIndex + 1);
				}
				dotIndex = nextIndex;
			}
			while (dotIndex != -1);
			yield break;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0001C744 File Offset: 0x0001A944
		private IEnumerable<T> FindPagedGenericData<T>(ref string[] cookie, int pageSize, out bool isComplete) where T : IConfigurable, new()
		{
			string text = "[end]";
			List<T> list = new List<T>();
			int[] array = ((IPartitionedDataProvider)this.WebStoreDataProvider).GetAllPhysicalPartitions().Cast<int>().ToArray<int>();
			string[] array2;
			if (cookie == null)
			{
				array2 = new string[array.Length];
			}
			else
			{
				array2 = cookie;
			}
			isComplete = true;
			foreach (int num in array)
			{
				bool flag = true;
				QueryFilter baseQueryFilter = new ComparisonFilter(ComparisonOperator.Equal, DalHelper.PhysicalInstanceKeyProp, num);
				string text2 = array2[num] ?? string.Empty;
				if (text2 != text)
				{
					QueryFilter pagingQueryFilter = PagingHelper.GetPagingQueryFilter(baseQueryFilter, text2);
					list.AddRange(this.WebStoreDataProvider.FindPaged<T>(pagingQueryFilter, null, false, null, pageSize));
					text2 = PagingHelper.GetProcessedCookie(pagingQueryFilter, out flag);
				}
				if (flag)
				{
					array2[num] = text;
				}
				else
				{
					array2[num] = text2;
					isComplete = false;
				}
			}
			cookie = array2;
			return list;
		}

		// Token: 0x04000497 RID: 1175
		public static readonly QueryFilter TenantInboundConnectorEnabledFilter = new ComparisonFilter(ComparisonOperator.Equal, TenantInboundConnectorSchema.Enabled, true);

		// Token: 0x04000498 RID: 1176
		private static Guid onDemandReportsFixedTenantId = new Guid("00D89CFE-7C72-4D91-B1FE-A8BBDF4DEE62");

		// Token: 0x04000499 RID: 1177
		private static TenantInboundConnector[] emptyInboundConnectorArray = new TenantInboundConnector[0];

		// Token: 0x0400049A RID: 1178
		private static Lazy<IBloomFilterDataProvider> bloomFilterProvider = new Lazy<IBloomFilterDataProvider>(() => BloomFilterProviderFactory.Default.Create(new Type[]
		{
			typeof(UserExtendedPropertiesEmailAddress),
			typeof(GroupExtendedPropertiesEmailAddress),
			typeof(ContactExtendedPropertiesEmailAddress),
			typeof(AcceptedDomain)
		}, CacheConfiguration.Instance.BloomFilterUpdateFrequency, CacheConfiguration.Instance.BloomFilterTracerTokensEnabled));

		// Token: 0x0400049B RID: 1179
		private IConfigDataProvider compositeProvider;

		// Token: 0x0400049C RID: 1180
		private IConfigDataProvider webStoreDataProvider;
	}
}
