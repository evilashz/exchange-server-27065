using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.PolicySync
{
	// Token: 0x0200010B RID: 267
	public class PolicySyncProxy : IPolicySyncWebserviceClient, IDisposable
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x00015E18 File Offset: 0x00014018
		private PolicySyncProxy(EndpointAddress endpointAddress, X509Certificate2 certificate, string partnerName, ExecutionLog logProvider)
		{
			this.identity = new ServiceProxyIdentity(endpointAddress, certificate, partnerName);
			this.proxyPool = ServiceProxyPoolsManager<IPolicySyncWebserviceClient>.Instance.GetOrCreateProxyPool(this.identity, logProvider);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00015E46 File Offset: 0x00014046
		private PolicySyncProxy(EndpointAddress endpointAddress, ICredentials credentials, string partnerName, ExecutionLog logProvider)
		{
			this.identity = new ServiceProxyIdentity(endpointAddress, credentials, partnerName);
			this.proxyPool = ServiceProxyPoolsManager<IPolicySyncWebserviceClient>.Instance.GetOrCreateProxyPool(this.identity, logProvider);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00015E74 File Offset: 0x00014074
		public static PolicySyncProxy GetOrCreate(EndpointAddress endpointAddress, X509Certificate2 certificate, string partnerName, ExecutionLog logProvider)
		{
			ArgumentValidator.ThrowIfNull("endpointAddress", endpointAddress);
			ArgumentValidator.ThrowIfNull("certificate", certificate);
			ArgumentValidator.ThrowIfNull("logProvider", logProvider);
			ArgumentValidator.ThrowIfNullOrEmpty("partnerName", partnerName);
			return new PolicySyncProxy(endpointAddress, certificate, partnerName, logProvider);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00015EAB File Offset: 0x000140AB
		public static PolicySyncProxy GetOrCreate(EndpointAddress endpointAddress, ICredentials credentials, string partnerName, ExecutionLog logProvider)
		{
			ArgumentValidator.ThrowIfNull("endpointAddress", endpointAddress);
			ArgumentValidator.ThrowIfNull("credentials", credentials);
			ArgumentValidator.ThrowIfNull("logProvider", logProvider);
			ArgumentValidator.ThrowIfNullOrEmpty("partnerName", partnerName);
			return new PolicySyncProxy(endpointAddress, credentials, partnerName, logProvider);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00015F38 File Offset: 0x00014138
		public PolicyChange GetSingleTenantChanges(TenantCookie tenantCookie)
		{
			ArgumentValidator.ThrowIfNull("tenantCookie", tenantCookie);
			PolicyChangeBatch policyChangeBatch = null;
			TenantCookieCollection tenantCookies = new TenantCookieCollection(tenantCookie.Workload, tenantCookie.ObjectType);
			tenantCookies[tenantCookie.TenantId] = tenantCookie;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				policyChangeBatch = proxy.Client.GetChanges(new GetChangesRequest
				{
					CallerContext = SyncCallerContext.Create(this.identity.PartnerName),
					TenantCookies = tenantCookies
				});
			}, string.Format("sync GetSingleTenantChanges - tenantId: {0}.", tenantCookie.TenantId), 3);
			return PolicySyncProxy.GetPolicyChangeFromBatch(tenantCookie, policyChangeBatch);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x00016038 File Offset: 0x00014238
		public IAsyncResult BeginGetSingleTenantChanges(TenantCookie tenantCookie, AsyncCallback userCallback, object stateObject)
		{
			ArgumentValidator.ThrowIfNull("tenantCookie", tenantCookie);
			TenantCookieCollection tenantCookies = new TenantCookieCollection(tenantCookie.Workload, tenantCookie.ObjectType);
			tenantCookies[tenantCookie.TenantId] = tenantCookie;
			IAsyncResult result = null;
			this.proxyPool.CallServiceWithRetryAsyncBegin(delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				AsyncCallStateObject stateObject2 = new AsyncCallStateObject(stateObject, proxy, tenantCookie);
				result = proxy.Client.BeginGetChanges(new GetChangesRequest
				{
					CallerContext = SyncCallerContext.Create(this.identity.PartnerName),
					TenantCookies = tenantCookies
				}, userCallback, stateObject2);
			}, string.Format("async BeginGetSingleTenantChanges - tenantId: {0}.", tenantCookie.TenantId), 3);
			return result;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00016114 File Offset: 0x00014314
		public PolicyChange EndGetSingleTenantChanges(IAsyncResult asyncResult)
		{
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			PolicyChangeBatch policyChangeBatch = null;
			AsyncCallStateObject asyncCallStateObject = (AsyncCallStateObject)asyncResult.AsyncState;
			this.proxyPool.CallServiceWithRetryAsyncEnd(asyncCallStateObject.ProxyToUse, delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				policyChangeBatch = proxy.Client.EndGetChanges(asyncResult);
			}, "async EndGetSingleTenantChanges");
			return PolicySyncProxy.GetPolicyChangeFromBatch(asyncCallStateObject.TenantCookie, policyChangeBatch);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x000161AC File Offset: 0x000143AC
		public PolicyChangeBatch GetChanges(GetChangesRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			PolicyChangeBatch policyChangeBatch = null;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				policyChangeBatch = proxy.Client.GetChanges(request);
			}, "sync GetChanges", 3);
			return policyChangeBatch;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00016244 File Offset: 0x00014444
		public IAsyncResult BeginGetChanges(GetChangesRequest request, AsyncCallback userCallback, object stateObject)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			IAsyncResult result = null;
			this.proxyPool.CallServiceWithRetryAsyncBegin(delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				AsyncCallStateObject stateObject2 = new AsyncCallStateObject(stateObject, proxy, null);
				result = proxy.Client.BeginGetChanges(request, userCallback, stateObject2);
			}, "async BeginGetChanges", 3);
			return result;
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000162C8 File Offset: 0x000144C8
		public PolicyChangeBatch EndGetChanges(IAsyncResult asyncResult)
		{
			PolicyChangeBatch policyChangeBatch = null;
			AsyncCallStateObject asyncCallStateObject = (AsyncCallStateObject)asyncResult.AsyncState;
			this.proxyPool.CallServiceWithRetryAsyncEnd(asyncCallStateObject.ProxyToUse, delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				policyChangeBatch = proxy.Client.EndGetChanges(asyncResult);
			}, "async EndGetChanges");
			return policyChangeBatch;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001635C File Offset: 0x0001455C
		public PolicyConfigurationBase GetObject(SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects)
		{
			ArgumentValidator.ThrowIfNull("callerContext", callerContext);
			ArgumentValidator.ThrowIfNull("tenantId", tenantId);
			ArgumentValidator.ThrowIfNull("objectType", objectType);
			ArgumentValidator.ThrowIfNull("objectId", objectId);
			PolicyConfigurationBase policyConfigurationBase = null;
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				policyConfigurationBase = proxy.Client.GetObject(callerContext, tenantId, objectType, objectId, includeDeletedObjects);
			}, string.Format("sync GetObject - partnerName: {0}, tenantId: {1}, objectType: {2}, objectId: {3}, includeDeletedObjects: {4}.", new object[]
			{
				callerContext.PartnerName,
				tenantId,
				objectType,
				objectId,
				includeDeletedObjects
			}), 3);
			return policyConfigurationBase;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000164BC File Offset: 0x000146BC
		public IAsyncResult BeginGetObject(SyncCallerContext callerContext, Guid tenantId, ConfigurationObjectType objectType, Guid objectId, bool includeDeletedObjects, AsyncCallback userCallback, object stateObject)
		{
			ArgumentValidator.ThrowIfNull("callerContext", callerContext);
			ArgumentValidator.ThrowIfNull("tenantId", tenantId);
			ArgumentValidator.ThrowIfNull("objectType", objectType);
			ArgumentValidator.ThrowIfNull("objectId", objectId);
			IAsyncResult result = null;
			this.proxyPool.CallServiceWithRetryAsyncBegin(delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				AsyncCallStateObject stateObject2 = new AsyncCallStateObject(stateObject, proxy, null);
				result = proxy.Client.BeginGetObject(callerContext, tenantId, objectType, objectId, includeDeletedObjects, userCallback, stateObject2);
			}, string.Format("async BeginGetObject - partnerName: {0}, tenantId: {1}, objectType: {2}, objectId: {3}, includeDeletedObjects: {4}.", new object[]
			{
				callerContext.PartnerName,
				tenantId,
				objectType,
				objectId,
				includeDeletedObjects
			}), 3);
			return result;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x000165F4 File Offset: 0x000147F4
		public PolicyConfigurationBase EndGetObject(IAsyncResult asyncResult)
		{
			PolicyConfigurationBase policyConfigurationBase = null;
			AsyncCallStateObject asyncCallStateObject = (AsyncCallStateObject)asyncResult.AsyncState;
			this.proxyPool.CallServiceWithRetryAsyncEnd(asyncCallStateObject.ProxyToUse, delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				policyConfigurationBase = proxy.Client.EndGetObject(asyncResult);
			}, "async EndGetObject");
			return policyConfigurationBase;
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001666C File Offset: 0x0001486C
		public void PublishStatus(PublishStatusRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			this.proxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				proxy.Client.PublishStatus(request);
			}, "sync PublishStatus", 3);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x000166F8 File Offset: 0x000148F8
		public IAsyncResult BeginPublishStatus(PublishStatusRequest request, AsyncCallback userCallback, object stateObject)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			IAsyncResult result = null;
			this.proxyPool.CallServiceWithRetryAsyncBegin(delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				AsyncCallStateObject stateObject2 = new AsyncCallStateObject(stateObject, proxy, null);
				result = proxy.Client.BeginPublishStatus(request, userCallback, stateObject2);
			}, "async BeginPublishStatus", 3);
			return result;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00016778 File Offset: 0x00014978
		public void EndPublishStatus(IAsyncResult asyncResult)
		{
			AsyncCallStateObject asyncCallStateObject = (AsyncCallStateObject)asyncResult.AsyncState;
			this.proxyPool.CallServiceWithRetryAsyncEnd(asyncCallStateObject.ProxyToUse, delegate(IPooledServiceProxy<IPolicySyncWebserviceClient> proxy)
			{
				proxy.Client.EndPublishStatus(asyncResult);
			}, "async EndGetObject");
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x000167C5 File Offset: 0x000149C5
		public void SetMaxNumberOfProxiesInPool(uint maxNumberOfProxiesInPool)
		{
			ArgumentValidator.ThrowIfZero("maxNumberOfProxiesInPool", maxNumberOfProxiesInPool);
			this.proxyPool.MaxNumberOfClientProxies = maxNumberOfProxiesInPool;
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x000167DE File Offset: 0x000149DE
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x000167E8 File Offset: 0x000149E8
		private static PolicyChange GetPolicyChangeFromBatch(TenantCookie tenantCookie, PolicyChangeBatch policyChangeBatch)
		{
			PolicyChange policyChange = null;
			if (policyChangeBatch != null && tenantCookie != null)
			{
				policyChange = new PolicyChange();
				policyChange.Changes = policyChangeBatch.Changes;
				TenantCookie newCookie = null;
				if (policyChangeBatch.NewCookies != null)
				{
					policyChangeBatch.NewCookies.TryGetCookie(tenantCookie.TenantId, out newCookie);
				}
				policyChange.NewCookie = newCookie;
			}
			return policyChange;
		}

		// Token: 0x0400040F RID: 1039
		private readonly ServiceProxyPool<IPolicySyncWebserviceClient> proxyPool;

		// Token: 0x04000410 RID: 1040
		private readonly ServiceProxyIdentity identity;
	}
}
