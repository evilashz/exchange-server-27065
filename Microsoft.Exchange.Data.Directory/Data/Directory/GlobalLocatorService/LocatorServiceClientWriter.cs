using System;
using System.Collections.Generic;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000125 RID: 293
	internal class LocatorServiceClientWriter : LocatorServiceClientAdapter, IGlobalLocatorServiceWriter
	{
		// Token: 0x06000C45 RID: 3141 RVA: 0x000378A8 File Offset: 0x00035AA8
		public static IGlobalLocatorServiceWriter Create(GlsCallerId glsCallerId)
		{
			return new LocatorServiceClientWriter(glsCallerId);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000378B0 File Offset: 0x00035AB0
		public static IGlobalLocatorServiceWriter Create(GlsCallerId glsCallerId, LocatorService serviceProxy)
		{
			return new LocatorServiceClientWriter(glsCallerId, serviceProxy);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000378B9 File Offset: 0x00035AB9
		private LocatorServiceClientWriter(GlsCallerId glsCallerId) : base(glsCallerId)
		{
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000378C2 File Offset: 0x00035AC2
		private LocatorServiceClientWriter(GlsCallerId glsCallerId, LocatorService serviceProxy) : base(glsCallerId, serviceProxy)
		{
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000378F4 File Offset: 0x00035AF4
		public void SaveTenant(Guid tenantId, KeyValuePair<TenantProperty, PropertyValue>[] properties)
		{
			SaveTenantRequest request = LocatorServiceClientWriter.ConstructSaveTenantRequest(tenantId, properties);
			LocatorService proxy = this.AcquireServiceProxy();
			GLSLogger.LoggingWrapper<SaveTenantResponse>(this, tenantId.ToString(), proxy.GetHashCode().ToString(), () => proxy.SaveTenant(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0003798C File Offset: 0x00035B8C
		public void SaveDomain(SmtpDomain domain, bool isInitialDomain, Guid tenantId, KeyValuePair<DomainProperty, PropertyValue>[] properties)
		{
			SaveDomainRequest request = LocatorServiceClientWriter.ConstructSaveDomainRequest(domain, null, tenantId, properties);
			request.DomainKeyType = (isInitialDomain ? DomainKeyType.InitialDomain : DomainKeyType.CustomDomain);
			LocatorService proxy = this.AcquireServiceProxy();
			GLSLogger.LoggingWrapper<SaveDomainResponse>(this, domain.ToString(), proxy.GetHashCode().ToString(), () => proxy.SaveDomain(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x00037A34 File Offset: 0x00035C34
		public void SaveDomain(SmtpDomain domain, string domainKey, Guid tenantId, KeyValuePair<DomainProperty, PropertyValue>[] properties)
		{
			SaveDomainRequest request = LocatorServiceClientWriter.ConstructSaveDomainRequest(domain, domainKey, tenantId, properties);
			LocatorService proxy = this.AcquireServiceProxy();
			GLSLogger.LoggingWrapper<SaveDomainResponse>(this, domain.ToString(), proxy.GetHashCode().ToString(), () => proxy.SaveDomain(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00037AC8 File Offset: 0x00035CC8
		public void DeleteTenant(Guid tenantId, Namespace ns)
		{
			DeleteTenantRequest request = LocatorServiceClientWriter.ConstructDeleteTenantRequest(tenantId, ns);
			LocatorService proxy = this.AcquireServiceProxy();
			GLSLogger.LoggingWrapper<DeleteTenantResponse>(this, tenantId.ToString(), proxy.GetHashCode().ToString(), () => proxy.DeleteTenant(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00037B60 File Offset: 0x00035D60
		public void DeleteDomain(SmtpDomain domain, Guid tenantId, Namespace ns)
		{
			DeleteDomainRequest request = LocatorServiceClientWriter.ConstructDeleteDomainRequest(domain, tenantId, ns);
			LocatorService proxy = this.AcquireServiceProxy();
			GLSLogger.LoggingWrapper<DeleteDomainResponse>(this, domain.ToString(), proxy.GetHashCode().ToString(), () => proxy.DeleteDomain(this.requestIdentity, request));
			base.ReleaseServiceProxy(proxy);
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00037BCC File Offset: 0x00035DCC
		public IAsyncResult BeginSaveTenant(Guid tenantId, KeyValuePair<TenantProperty, PropertyValue>[] properties, AsyncCallback callback, object asyncState)
		{
			SaveTenantRequest saveTenantRequest = LocatorServiceClientWriter.ConstructSaveTenantRequest(tenantId, properties);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginSaveTenant(this.requestIdentity, saveTenantRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00037C18 File Offset: 0x00035E18
		public IAsyncResult BeginSaveDomain(SmtpDomain domain, string domainKey, Guid tenantId, KeyValuePair<DomainProperty, PropertyValue>[] properties, AsyncCallback callback, object asyncState)
		{
			SaveDomainRequest saveDomainRequest = LocatorServiceClientWriter.ConstructSaveDomainRequest(domain, domainKey, tenantId, properties);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginSaveDomain(this.requestIdentity, saveDomainRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00037C68 File Offset: 0x00035E68
		public IAsyncResult BeginDeleteTenant(Guid tenantId, Namespace ns, AsyncCallback callback, object asyncState)
		{
			DeleteTenantRequest deleteTenantRequest = LocatorServiceClientWriter.ConstructDeleteTenantRequest(tenantId, ns);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginDeleteTenant(this.requestIdentity, deleteTenantRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00037CB4 File Offset: 0x00035EB4
		public IAsyncResult BeginDeleteDomain(SmtpDomain domain, Guid tenantId, Namespace ns, AsyncCallback callback, object asyncState)
		{
			DeleteDomainRequest deleteDomainRequest = LocatorServiceClientWriter.ConstructDeleteDomainRequest(domain, tenantId, ns);
			LocatorService locatorService = this.AcquireServiceProxy();
			IAsyncResult internalAsyncResult = locatorService.BeginDeleteDomain(this.requestIdentity, deleteDomainRequest, new AsyncCallback(LocatorServiceClientAdapter.OnWebServiceRequestCompleted), new GlsAsyncState(callback, asyncState, locatorService));
			return new GlsAsyncResult(callback, asyncState, locatorService, internalAsyncResult);
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00037D04 File Offset: 0x00035F04
		public void EndSaveTenant(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			glsAsyncResult.ServiceProxy.EndSaveTenant(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00037D3C File Offset: 0x00035F3C
		public void EndSaveDomain(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			glsAsyncResult.ServiceProxy.EndSaveDomain(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00037D74 File Offset: 0x00035F74
		public void EndDeleteTenant(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			glsAsyncResult.ServiceProxy.EndDeleteTenant(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00037DAC File Offset: 0x00035FAC
		public void EndDeleteDomain(IAsyncResult externalAR)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)externalAR;
			glsAsyncResult.ServiceProxy.EndDeleteDomain(glsAsyncResult.InternalAsyncResult);
			base.ReleaseServiceProxy(glsAsyncResult.ServiceProxy);
			glsAsyncResult.Dispose();
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00037DE4 File Offset: 0x00035FE4
		internal static SaveTenantRequest ConstructSaveTenantRequest(Guid tenantId, KeyValuePair<TenantProperty, PropertyValue>[] properties)
		{
			LocatorServiceClientAdapter.ThrowIfEmptyGuid(tenantId, "tenantId");
			LocatorServiceClientAdapter.ThrowIfNull(properties, "properties");
			foreach (KeyValuePair<TenantProperty, PropertyValue> keyValuePair in properties)
			{
				if (keyValuePair.Key.DataType != keyValuePair.Value.DataType)
				{
					throw new ArgumentException("key and value have different data types!", "properties");
				}
			}
			return new SaveTenantRequest
			{
				TenantInfo = new TenantInfo
				{
					TenantId = tenantId,
					Properties = LocatorServiceClientWriter.GetPropertyValues(properties)
				}
			};
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x00037E80 File Offset: 0x00036080
		internal static SaveDomainRequest ConstructSaveDomainRequest(SmtpDomain domain, string domainKey, Guid tenantId, KeyValuePair<DomainProperty, PropertyValue>[] properties)
		{
			LocatorServiceClientAdapter.ThrowIfEmptyGuid(tenantId, "tenantId");
			LocatorServiceClientAdapter.ThrowIfNull(domain, "domain");
			LocatorServiceClientAdapter.ThrowIfNull(properties, "properties");
			foreach (KeyValuePair<DomainProperty, PropertyValue> keyValuePair in properties)
			{
				if (keyValuePair.Key.DataType != keyValuePair.Value.DataType)
				{
					throw new ArgumentException("key and value have different data types!", "properties");
				}
			}
			return new SaveDomainRequest
			{
				TenantId = tenantId,
				DomainInfo = new DomainInfo
				{
					DomainName = domain.Domain,
					DomainKey = domainKey,
					Properties = LocatorServiceClientWriter.GetPropertyValues(properties)
				}
			};
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00037F3C File Offset: 0x0003613C
		internal static DeleteTenantRequest ConstructDeleteTenantRequest(Guid tenantId, Namespace ns)
		{
			LocatorServiceClientAdapter.ThrowIfEmptyGuid(tenantId, "tenantId");
			LocatorServiceClientAdapter.ThrowIfInvalidNamespace(ns);
			return new DeleteTenantRequest
			{
				Tenant = new TenantQuery
				{
					TenantId = tenantId,
					PropertyNames = new string[]
					{
						NamespaceUtil.NamespaceWildcard(ns)
					}
				}
			};
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00037F8C File Offset: 0x0003618C
		internal static DeleteDomainRequest ConstructDeleteDomainRequest(SmtpDomain domain, Guid tenantId, Namespace ns)
		{
			return LocatorServiceClientWriter.ConstructDeleteDomainRequest(domain, tenantId, ns, false);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00037F98 File Offset: 0x00036198
		internal static DeleteDomainRequest ConstructDeleteDomainRequest(SmtpDomain domain, Guid tenantId, Namespace ns, bool skipTenantCheck)
		{
			LocatorServiceClientAdapter.ThrowIfNull(domain, "domain");
			if (!skipTenantCheck)
			{
				LocatorServiceClientAdapter.ThrowIfEmptyGuid(tenantId, "tenantId");
			}
			LocatorServiceClientAdapter.ThrowIfInvalidNamespace(ns);
			return new DeleteDomainRequest
			{
				TenantId = tenantId,
				Domain = new DomainQuery
				{
					DomainName = domain.Domain,
					PropertyNames = new string[]
					{
						NamespaceUtil.NamespaceWildcard(ns)
					}
				}
			};
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00038004 File Offset: 0x00036204
		private static KeyValuePair<string, string>[] GetPropertyValues(KeyValuePair<TenantProperty, PropertyValue>[] properties)
		{
			KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[properties.Length];
			for (int i = 0; i < properties.Length; i++)
			{
				GlsProperty key = properties[i].Key;
				PropertyValue value = properties[i].Value;
				array[i] = new KeyValuePair<string, string>(key.Name, value.ToString());
			}
			return array;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00038060 File Offset: 0x00036260
		private static KeyValuePair<string, string>[] GetPropertyValues(KeyValuePair<DomainProperty, PropertyValue>[] properties)
		{
			KeyValuePair<string, string>[] array = new KeyValuePair<string, string>[properties.Length];
			for (int i = 0; i < properties.Length; i++)
			{
				GlsProperty key = properties[i].Key;
				PropertyValue value = properties[i].Value;
				array[i] = new KeyValuePair<string, string>(key.Name, value.ToString());
			}
			return array;
		}
	}
}
