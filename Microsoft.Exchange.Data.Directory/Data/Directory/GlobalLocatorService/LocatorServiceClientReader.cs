using System;
using System.Collections.Generic;
using System.Linq;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000123 RID: 291
	internal class LocatorServiceClientReader : LocatorServiceClientAdapter, IGlobalLocatorServiceReader
	{
		// Token: 0x06000C14 RID: 3092 RVA: 0x00036DCB File Offset: 0x00034FCB
		internal static IGlobalLocatorServiceReader Create(GlsCallerId glsCallerId)
		{
			if (AppConfigGlsReader.AppConfigOverrideExists())
			{
				return new AppConfigGlsReader();
			}
			return new LocatorServiceClientReader(glsCallerId);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00036DE0 File Offset: 0x00034FE0
		internal static IGlobalLocatorServiceReader Create(GlsCallerId glsCallerId, LocatorService serviceProxy)
		{
			if (AppConfigGlsReader.AppConfigOverrideExists())
			{
				return new AppConfigGlsReader();
			}
			return new LocatorServiceClientReader(glsCallerId, serviceProxy);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00036DF6 File Offset: 0x00034FF6
		private LocatorServiceClientReader(GlsCallerId glsCallerId, GlsAPIReadFlag readFlag) : base(glsCallerId)
		{
			this.glsReadFlag = readFlag;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00036E06 File Offset: 0x00035006
		private LocatorServiceClientReader(GlsCallerId glsCallerId) : base(glsCallerId)
		{
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00036E0F File Offset: 0x0003500F
		private LocatorServiceClientReader(GlsCallerId glsCallerId, LocatorService serviceProxy) : base(glsCallerId, serviceProxy)
		{
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00036E40 File Offset: 0x00035040
		public bool TenantExists(Guid tenantId, Namespace[] ns)
		{
			FindTenantRequest request = LocatorServiceClientReader.ConstructTenantExistsRequest(tenantId, ns, this.glsReadFlag);
			LocatorService proxy = this.AcquireServiceProxy();
			FindTenantResponse findTenantResponse = GLSLogger.LoggingWrapper<FindTenantResponse>(this, tenantId.ToString(), proxy.GetHashCode().ToString(), () => proxy.FindTenant(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
			return findTenantResponse.TenantInfo != null;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00036EEC File Offset: 0x000350EC
		public bool DomainExists(SmtpDomain domain, Namespace[] ns)
		{
			FindDomainRequest request = LocatorServiceClientReader.ConstructDomainExistsRequest(domain, ns, this.glsReadFlag);
			LocatorService proxy = this.AcquireServiceProxy();
			FindDomainResponse findDomainResponse = GLSLogger.LoggingWrapper<FindDomainResponse>(this, domain.ToString(), proxy.GetHashCode().ToString(), () => proxy.FindDomain(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
			return findDomainResponse.DomainInfo != null;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00036F90 File Offset: 0x00035190
		public FindTenantResult FindTenant(Guid tenantId, TenantProperty[] tenantProperties)
		{
			FindTenantRequest request = LocatorServiceClientReader.ConstructFindTenantRequest(tenantId, tenantProperties, this.glsReadFlag);
			LocatorService proxy = this.AcquireServiceProxy();
			FindTenantResponse response = GLSLogger.LoggingWrapper<FindTenantResponse>(this, tenantId.ToString(), proxy.GetHashCode().ToString(), () => proxy.FindTenant(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
			return LocatorServiceClientReader.ConstructFindTenantResult(response);
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00037034 File Offset: 0x00035234
		public FindDomainResult FindDomain(SmtpDomain domain, DomainProperty[] domainProperties, TenantProperty[] tenantProperties)
		{
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(domain, domainProperties, tenantProperties, this.glsReadFlag);
			LocatorService proxy = this.AcquireServiceProxy();
			FindDomainResponse response = GLSLogger.LoggingWrapper<FindDomainResponse>(this, domain.ToString(), proxy.GetHashCode().ToString(), () => proxy.FindDomain(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
			return LocatorServiceClientReader.ConstructFindDomainResult(response);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000370D4 File Offset: 0x000352D4
		public FindDomainsResult FindDomains(SmtpDomain[] domains, DomainProperty[] domainProperties, TenantProperty[] tenantProperties)
		{
			FindDomainsRequest request = LocatorServiceClientReader.ConstructFindDomainsRequest(domains, domainProperties, tenantProperties, this.glsReadFlag);
			LocatorService proxy = this.AcquireServiceProxy();
			FindDomainsResponse response = GLSLogger.LoggingWrapper<FindDomainsResponse>(this, domains[0].ToString(), proxy.GetHashCode().ToString(), () => proxy.FindDomains(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
			return LocatorServiceClientReader.ConstructFindDomainsResult(response);
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00037150 File Offset: 0x00035350
		public IAsyncResult BeginTenantExists(Guid tenantId, Namespace[] ns, AsyncCallback callback, object asyncState)
		{
			FindTenantRequest findTenantRequest = LocatorServiceClientReader.ConstructTenantExistsRequest(tenantId, ns, this.glsReadFlag);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginFindTenant(this.requestIdentity, findTenantRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000371A4 File Offset: 0x000353A4
		public IAsyncResult BeginDomainExists(SmtpDomain domain, Namespace[] ns, AsyncCallback callback, object asyncState)
		{
			FindDomainRequest findDomainRequest = LocatorServiceClientReader.ConstructDomainExistsRequest(domain, ns, this.glsReadFlag);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginFindDomain(this.requestIdentity, findDomainRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000371F8 File Offset: 0x000353F8
		public IAsyncResult BeginFindTenant(Guid tenantId, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState)
		{
			FindTenantRequest findTenantRequest = LocatorServiceClientReader.ConstructFindTenantRequest(tenantId, tenantProperties, this.glsReadFlag);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginFindTenant(this.requestIdentity, findTenantRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0003724C File Offset: 0x0003544C
		public IAsyncResult BeginFindDomain(SmtpDomain domain, DomainProperty[] domainProperties, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState)
		{
			FindDomainRequest findDomainRequest = LocatorServiceClientReader.ConstructFindDomainRequest(domain, domainProperties, tenantProperties, this.glsReadFlag);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginFindDomain(this.requestIdentity, findDomainRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000372A0 File Offset: 0x000354A0
		public IAsyncResult BeginFindDomains(SmtpDomain[] domains, DomainProperty[] domainProperties, TenantProperty[] tenantProperties, AsyncCallback callback, object asyncState)
		{
			FindDomainsRequest findDomainsRequest = LocatorServiceClientReader.ConstructFindDomainsRequest(domains, domainProperties, tenantProperties, this.glsReadFlag);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginFindDomains(this.requestIdentity, findDomainsRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000372F4 File Offset: 0x000354F4
		public bool EndTenantExists(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			FindTenantResponse findTenantResponse = glsAsyncResult.ServiceProxy.EndFindTenant(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
			return findTenantResponse.TenantInfo != null;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00037338 File Offset: 0x00035538
		public bool EndDomainExists(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			FindDomainResponse findDomainResponse = glsAsyncResult.ServiceProxy.EndFindDomain(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
			return findDomainResponse.DomainInfo != null;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0003737C File Offset: 0x0003557C
		public FindTenantResult EndFindTenant(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			FindTenantResponse response = glsAsyncResult.ServiceProxy.EndFindTenant(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
			return LocatorServiceClientReader.ConstructFindTenantResult(response);
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000373BC File Offset: 0x000355BC
		public FindDomainResult EndFindDomain(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			FindDomainResponse response = glsAsyncResult.ServiceProxy.EndFindDomain(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
			return LocatorServiceClientReader.ConstructFindDomainResult(response);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000373FC File Offset: 0x000355FC
		public FindDomainsResult EndFindDomains(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			FindDomainsResponse response = glsAsyncResult.ServiceProxy.EndFindDomains(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
			return LocatorServiceClientReader.ConstructFindDomainsResult(response);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00037444 File Offset: 0x00035644
		internal static FindTenantRequest ConstructTenantExistsRequest(Guid tenantId, Namespace[] ns, GlsAPIReadFlag readFlag)
		{
			LocatorServiceClientAdapter.ThrowIfEmptyGuid(tenantId, "tenantId");
			LocatorServiceClientAdapter.ThrowIfInvalidNamespace(ns);
			FindTenantRequest findTenantRequest = new FindTenantRequest();
			findTenantRequest.ReadFlag = (int)readFlag;
			string[] propertyNames = (from item in ns
			select NamespaceUtil.NamespaceWildcard(item)).ToArray<string>();
			findTenantRequest.Tenant = new TenantQuery
			{
				TenantId = tenantId,
				PropertyNames = propertyNames
			};
			return findTenantRequest;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000374BC File Offset: 0x000356BC
		internal static FindDomainRequest ConstructDomainExistsRequest(SmtpDomain domain, Namespace[] ns, GlsAPIReadFlag readFlag)
		{
			LocatorServiceClientAdapter.ThrowIfNull(domain, "domain");
			LocatorServiceClientAdapter.ThrowIfInvalidNamespace(ns);
			FindDomainRequest findDomainRequest = new FindDomainRequest();
			findDomainRequest.ReadFlag = (int)readFlag;
			string[] propertyNames = (from item in ns
			select NamespaceUtil.NamespaceWildcard(item)).ToArray<string>();
			findDomainRequest.Domain = new DomainQuery
			{
				DomainName = domain.Domain,
				PropertyNames = propertyNames
			};
			return findDomainRequest;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00037534 File Offset: 0x00035734
		internal static FindTenantRequest ConstructFindTenantRequest(Guid tenantId, TenantProperty[] tenantProperties, GlsAPIReadFlag readFlag)
		{
			LocatorServiceClientAdapter.ThrowIfEmptyGuid(tenantId, "tenantId");
			LocatorServiceClientAdapter.ThrowIfNull(tenantProperties, "tenantProperties");
			return new FindTenantRequest
			{
				ReadFlag = (int)readFlag,
				Tenant = new TenantQuery
				{
					TenantId = tenantId,
					PropertyNames = LocatorServiceClientReader.GetPropertyNames(tenantProperties)
				}
			};
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x00037588 File Offset: 0x00035788
		internal static FindDomainRequest ConstructFindDomainRequest(SmtpDomain domain, DomainProperty[] domainProperties, TenantProperty[] tenantProperties, GlsAPIReadFlag readFlag)
		{
			LocatorServiceClientAdapter.ThrowIfNull(domain, "domain");
			LocatorServiceClientAdapter.ThrowIfNull(domainProperties, "domainProperties");
			LocatorServiceClientAdapter.ThrowIfNull(tenantProperties, "tenantProperties");
			return new FindDomainRequest
			{
				ReadFlag = (int)readFlag,
				Domain = new DomainQuery
				{
					DomainName = domain.Domain,
					PropertyNames = LocatorServiceClientReader.GetPropertyNames(domainProperties)
				},
				Tenant = new TenantQuery
				{
					PropertyNames = LocatorServiceClientReader.GetPropertyNames(tenantProperties)
				}
			};
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003760C File Offset: 0x0003580C
		internal static FindDomainsRequest ConstructFindDomainsRequest(IEnumerable<SmtpDomain> domains, DomainProperty[] domainProperties, TenantProperty[] tenantProperties, GlsAPIReadFlag readFlag)
		{
			LocatorServiceClientAdapter.ThrowIfNull(domains, "domains");
			LocatorServiceClientAdapter.ThrowIfNull(domainProperties, "domainProperties");
			LocatorServiceClientAdapter.ThrowIfNull(tenantProperties, "tenantProperties");
			FindDomainsRequest findDomainsRequest = new FindDomainsRequest();
			findDomainsRequest.ReadFlag = (int)readFlag;
			findDomainsRequest.DomainsName = (from domain in domains
			select domain.Domain).ToArray<string>();
			findDomainsRequest.DomainPropertyNames = LocatorServiceClientReader.GetPropertyNames(domainProperties);
			findDomainsRequest.TenantPropertyNames = LocatorServiceClientReader.GetPropertyNames(tenantProperties);
			return findDomainsRequest;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00037690 File Offset: 0x00035890
		internal static FindTenantResult ConstructFindTenantResult(FindTenantResponse response)
		{
			IDictionary<TenantProperty, PropertyValue> properties = (response.TenantInfo != null) ? LocatorServiceClientReader.ConstructTenantPropertyDictionary(response.TenantInfo.Properties) : new Dictionary<TenantProperty, PropertyValue>();
			return new FindTenantResult(properties);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x000376C4 File Offset: 0x000358C4
		internal static FindDomainResult ConstructFindDomainResult(FindDomainResponse response)
		{
			IDictionary<DomainProperty, PropertyValue> domainProperties = (response.DomainInfo != null && response.DomainInfo.Properties != null) ? LocatorServiceClientReader.ConstructDomainPropertyDictionary(response.DomainInfo.Properties) : new Dictionary<DomainProperty, PropertyValue>();
			IDictionary<TenantProperty, PropertyValue> tenantProperties = (response.TenantInfo != null && response.TenantInfo.Properties != null) ? LocatorServiceClientReader.ConstructTenantPropertyDictionary(response.TenantInfo.Properties) : new Dictionary<TenantProperty, PropertyValue>();
			Guid tenantId = (response.TenantInfo != null) ? response.TenantInfo.TenantId : Guid.Empty;
			return new FindDomainResult((response.DomainInfo == null) ? null : response.DomainInfo.DomainName, tenantId, tenantProperties, domainProperties);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003776C File Offset: 0x0003596C
		internal static FindDomainsResult ConstructFindDomainsResult(FindDomainsResponse response)
		{
			FindDomainResult[] findDomainResults = new FindDomainResult[0];
			if (response.DomainsResponse != null)
			{
				findDomainResults = (from findDomainResponse in response.DomainsResponse
				select LocatorServiceClientReader.ConstructFindDomainResult(findDomainResponse)).ToArray<FindDomainResult>();
			}
			return new FindDomainsResult(findDomainResults);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000377C4 File Offset: 0x000359C4
		private static string[] GetPropertyNames(GlsProperty[] properties)
		{
			return (from property in properties
			select property.Name).ToArray<string>();
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000377F0 File Offset: 0x000359F0
		internal static IDictionary<TenantProperty, PropertyValue> ConstructTenantPropertyDictionary(KeyValuePair<string, string>[] properties)
		{
			IDictionary<TenantProperty, PropertyValue> dictionary = new Dictionary<TenantProperty, PropertyValue>();
			foreach (KeyValuePair<string, string> keyValuePair in properties)
			{
				TenantProperty tenantProperty = TenantProperty.Get(keyValuePair.Key);
				PropertyValue value = PropertyValue.Create(keyValuePair.Value, tenantProperty);
				dictionary.Add(tenantProperty, value);
			}
			return dictionary;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0003784C File Offset: 0x00035A4C
		internal static IDictionary<DomainProperty, PropertyValue> ConstructDomainPropertyDictionary(KeyValuePair<string, string>[] properties)
		{
			IDictionary<DomainProperty, PropertyValue> dictionary = new Dictionary<DomainProperty, PropertyValue>();
			foreach (KeyValuePair<string, string> keyValuePair in properties)
			{
				DomainProperty domainProperty = DomainProperty.Get(keyValuePair.Key);
				PropertyValue value = PropertyValue.Create(keyValuePair.Value, domainProperty);
				dictionary.Add(domainProperty, value);
			}
			return dictionary;
		}

		// Token: 0x04000643 RID: 1603
		private readonly GlsAPIReadFlag glsReadFlag;
	}
}
