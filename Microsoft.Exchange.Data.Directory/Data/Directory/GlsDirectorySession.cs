using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Data.Directory.GlobalLocatorService;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;
using Microsoft.Exchange.Net;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200013B RID: 315
	internal class GlsDirectorySession : IGlobalDirectorySession
	{
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0003A208 File Offset: 0x00038408
		internal static GlsCacheServiceMode GlsCacheServiceMode
		{
			get
			{
				return ConfigBase<AdDriverConfigSchema>.GetConfig<GlsCacheServiceMode>("GlsCacheServiceMode");
			}
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0003A214 File Offset: 0x00038414
		private static bool TryGetOverrideTenant(Guid tenantGuid, out GlobalLocatorServiceTenant tenantOverride)
		{
			tenantOverride = null;
			if (GlsDirectorySession.glsTenantOverridesNextRefresh < DateTime.UtcNow)
			{
				GlsDirectorySession.glsTenantOverridesNextRefresh = DateTime.UtcNow + GlsDirectorySession.ADConfigurationSettingsRefreshPeriod;
				try
				{
					GlsDirectorySession.glsTenantOverrides = (ConfigBase<AdDriverConfigSchema>.GetConfig<GlsOverrideCollection>("GlsTenantOverrides") ?? new GlsOverrideCollection(null));
				}
				catch (ConfigurationSettingsException ex)
				{
					ExTraceGlobals.GLSTracer.TraceWarning<string>(0L, "Unable to refresh GLS overrides after getting configuration exception:{0}", ex.Message);
				}
			}
			return GlsDirectorySession.glsTenantOverrides.TryGetTenantOverride(tenantGuid, out tenantOverride);
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0003A29C File Offset: 0x0003849C
		private static DirectoryServiceProxyPool<LocatorService> ServiceProxyPool
		{
			get
			{
				if (GlsDirectorySession.serviceProxyPool == null)
				{
					lock (GlsDirectorySession.proxyPoolLockObj)
					{
						if (GlsDirectorySession.serviceProxyPool == null)
						{
							GlsDirectorySession.serviceProxyPool = GlsDirectorySession.CreateServiceProxyPool();
						}
					}
				}
				return GlsDirectorySession.serviceProxyPool;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0003A2F4 File Offset: 0x000384F4
		private static DirectoryServiceProxyPool<LocatorService> OfflineServiceProxyPool
		{
			get
			{
				if (GlsDirectorySession.offlineServiceProxyPool == null)
				{
					lock (GlsDirectorySession.proxyPoolLockObj)
					{
						if (GlsDirectorySession.offlineServiceProxyPool == null)
						{
							GlsDirectorySession.offlineServiceProxyPool = GlsDirectorySession.CreateOfflineServiceProxyPool();
						}
					}
				}
				return GlsDirectorySession.offlineServiceProxyPool;
			}
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0003A34C File Offset: 0x0003854C
		private static ServiceEndpoint LoadServiceEndpoint()
		{
			ServiceEndpoint endpoint = LocatorServiceClientConfiguration.Instance.Endpoint;
			if (endpoint != null)
			{
				return endpoint;
			}
			ServiceEndpoint endpoint2;
			try
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 291, "LoadServiceEndpoint", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\GlsDirectorySession.cs");
				ServiceEndpointContainer endpointContainer = topologyConfigurationSession.GetEndpointContainer();
				endpoint2 = endpointContainer.GetEndpoint("GlobalLocatorService");
			}
			catch (ADTransientException ex)
			{
				throw new GlsTransientException(ex.LocalizedString, ex);
			}
			catch (ADExternalException ex2)
			{
				throw new GlsPermanentException(ex2.LocalizedString, ex2);
			}
			return endpoint2;
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0003A3DC File Offset: 0x000385DC
		private static Exception GetTransientWrappedException(Exception wcfException, string targetInfo)
		{
			if (wcfException is TimeoutException)
			{
				return new GlsTransientException(DirectoryStrings.TimeoutGlsError(wcfException.Message), wcfException);
			}
			if (wcfException is EndpointNotFoundException)
			{
				return new GlsTransientException(DirectoryStrings.GlsEndpointNotFound(targetInfo, wcfException.ToString()), wcfException);
			}
			return new GlsTransientException(DirectoryStrings.TransientGlsError(wcfException.Message), wcfException);
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0003A42F File Offset: 0x0003862F
		private static Exception GetPermanentWrappedException(Exception wcfException, string targetInfo)
		{
			return new GlsPermanentException(DirectoryStrings.PermanentGlsError(wcfException.Message), wcfException);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003A444 File Offset: 0x00038644
		private static DirectoryServiceProxyPool<LocatorService> CreateServiceProxyPool()
		{
			List<WSHttpBinding> list = new List<WSHttpBinding>();
			foreach (TimeSpan sendTimeout in GlsDirectorySession.SendTimeouts)
			{
				WSHttpBinding wshttpBinding = new WSHttpBinding(SecurityMode.Transport)
				{
					ReceiveTimeout = TimeSpan.FromSeconds(30.0),
					SendTimeout = sendTimeout,
					MaxBufferPoolSize = 524288L,
					MaxReceivedMessageSize = 65536L
				};
				wshttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Certificate;
				list.Add(wshttpBinding);
			}
			ServiceEndpoint serviceEndpoint = GlsDirectorySession.LoadServiceEndpoint();
			GlsDirectorySession.endpointHostName = serviceEndpoint.Uri.Host;
			return DirectoryServiceProxyPool<LocatorService>.CreateDirectoryServiceProxyPool("GlobalLocatorService", serviceEndpoint, ExTraceGlobals.GLSTracer, 150, list, new GetWrappedExceptionDelegate(GlsDirectorySession.GetTransientWrappedException), new GetWrappedExceptionDelegate(GlsDirectorySession.GetPermanentWrappedException), DirectoryEventLogConstants.Tuple_CannotContactGLS, false);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0003A538 File Offset: 0x00038738
		private static DirectoryServiceProxyPool<LocatorService> CreateOfflineServiceProxyPool()
		{
			return DirectoryServiceProxyPool<LocatorService>.CreateDirectoryServiceProxyPool("GlsCacheService", new ServiceEndpoint(new Uri("net.pipe://localhost/GlsCacheService/service.svc")), ExTraceGlobals.GLSTracer, 150, new NetNamedPipeBinding(NetNamedPipeSecurityMode.None), new GetWrappedExceptionDelegate(GlsDirectorySession.GetTransientWrappedException), new GetWrappedExceptionDelegate(GlsDirectorySession.GetPermanentWrappedException), DirectoryEventLogConstants.Tuple_CannotContactGLS, false);
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0003A58C File Offset: 0x0003878C
		public static GlsOverrideFlag GetTenantOverrideStatus(string externalDirectoryOrganizationId)
		{
			GlsOverrideFlag glsOverrideFlag = GlsOverrideFlag.None;
			Guid guid;
			GlobalLocatorServiceTenant globalLocatorServiceTenant;
			if (!string.IsNullOrWhiteSpace(externalDirectoryOrganizationId) && Guid.TryParse(externalDirectoryOrganizationId, out guid) && GlsDirectorySession.TryGetOverrideTenant(guid, out globalLocatorServiceTenant))
			{
				glsOverrideFlag = GlsOverrideFlag.OverrideIsSet;
				GlobalLocatorServiceTenant globalLocatorServiceTenant2;
				if (new GlsDirectorySession().TryGetTenantInfoByOrgGuid(guid, out globalLocatorServiceTenant2, GlsDirectorySession.GlsCacheServiceMode, true))
				{
					if (string.Compare(globalLocatorServiceTenant2.ResourceForest, globalLocatorServiceTenant.ResourceForest, CultureInfo.InvariantCulture, CompareOptions.OrdinalIgnoreCase) != 0)
					{
						glsOverrideFlag |= (GlsOverrideFlag.GlsRecordMismatch | GlsOverrideFlag.ResourceForestMismatch);
					}
					if (string.Compare(globalLocatorServiceTenant2.AccountForest, globalLocatorServiceTenant.AccountForest, CultureInfo.InvariantCulture, CompareOptions.OrdinalIgnoreCase) != 0)
					{
						glsOverrideFlag |= (GlsOverrideFlag.GlsRecordMismatch | GlsOverrideFlag.AccountForestMismatch);
					}
				}
			}
			return glsOverrideFlag;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0003A612 File Offset: 0x00038812
		internal GlsDirectorySession(GlsCallerId glsCallerId, GlsAPIReadFlag globalLocaterServiceReadFlag)
		{
			this.glsCallerId = glsCallerId;
			this.glsReadFlag = globalLocaterServiceReadFlag;
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003A628 File Offset: 0x00038828
		internal GlsDirectorySession(GlsAPIReadFlag globalLocaterServiceReadFlag) : this(GlsCallerId.Exchange, globalLocaterServiceReadFlag)
		{
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0003A636 File Offset: 0x00038836
		internal GlsDirectorySession(GlsCallerId glsCallerId) : this(glsCallerId, GlsAPIReadFlag.Default)
		{
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0003A640 File Offset: 0x00038840
		internal GlsDirectorySession() : this(GlsCallerId.Exchange)
		{
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0003A650 File Offset: 0x00038850
		public string GetRedirectServer(string memberName)
		{
			string result;
			if (!this.TryGetRedirectServer(memberName, out result))
			{
				throw new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(memberName));
			}
			return result;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0003A678 File Offset: 0x00038878
		public bool TryGetRedirectServer(string memberName, out string fqdn)
		{
			string empty = string.Empty;
			bool result = this.TryGetRedirectServer(memberName, GlsDirectorySession.GlsCacheServiceMode, out empty);
			fqdn = empty;
			return result;
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0003A6B8 File Offset: 0x000388B8
		public bool TryGetRedirectServer(string memberName, GlsCacheServiceMode glsCacheServiceMode, out string fqdn)
		{
			fqdn = string.Empty;
			SmtpAddress smtpAddress = GlsDirectorySession.ParseMemberName(memberName);
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(new SmtpDomain(smtpAddress.Domain), GlsDirectorySession.noDomainProperties, GlsDirectorySession.resourceForestProperty, this.glsReadFlag);
			GlsLoggerContext glsLoggerContext;
			FindDomainResponse response = this.ExecuteWithRetry<FindDomainResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindDomain(requestIdentity, request), "TryGetRedirectServer", "FindDomain", smtpAddress.Domain, true, glsCacheServiceMode, out glsLoggerContext);
			FindDomainResult findDomainResult = this.ConstructFindDomainResult(response, glsLoggerContext, Namespace.Exo);
			if (findDomainResult == null)
			{
				return false;
			}
			fqdn = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOResourceForest).GetStringValue();
			return this.IsValidForestName(memberName, fqdn, glsLoggerContext);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0003A758 File Offset: 0x00038958
		public string GetRedirectServer(Guid orgGuid)
		{
			string result;
			if (!this.TryGetRedirectServer(orgGuid, out result))
			{
				throw new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(orgGuid.ToString()));
			}
			return result;
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0003A7A0 File Offset: 0x000389A0
		public bool TryGetRedirectServer(Guid orgGuid, out string fqdn)
		{
			fqdn = string.Empty;
			FindTenantRequest request = this.ConstructFindTenantRequest(orgGuid, GlsDirectorySession.resourceForestProperty);
			GlsLoggerContext glsLoggerContext;
			FindTenantResponse response = this.ExecuteWithRetry<FindTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindTenant(requestIdentity, request), "TryGetRedirectServer", "FindTenant", orgGuid, true, out glsLoggerContext);
			FindTenantResult findTenantResult = this.ConstructFindTenantResult(response, glsLoggerContext, Namespace.Exo);
			if (findTenantResult == null)
			{
				return false;
			}
			fqdn = findTenantResult.GetPropertyValue(TenantProperty.EXOResourceForest).GetStringValue();
			return this.IsValidForestName(orgGuid.ToString(), fqdn, glsLoggerContext);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0003A868 File Offset: 0x00038A68
		public IAsyncResult BeginGetNextHop(SmtpDomain domain, object clientAsyncState, AsyncCallback clientCallback)
		{
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(domain, GlsDirectorySession.noDomainProperties, GlsDirectorySession.exoNextHopProperty, this.glsReadFlag);
			return this.BeginExecuteWithRetry(delegate(LocatorService proxy, RequestIdentity requestIdentity, GlsAsyncResult asyncResult)
			{
				IAsyncResult internalAsyncResult = proxy.BeginFindDomain(requestIdentity, request, new AsyncCallback(this.OnAsyncRequestCompleted), asyncResult);
				asyncResult.InternalAsyncResult = internalAsyncResult;
			}, clientCallback, clientAsyncState, "BeginFindDomain", "FindDomain", domain, true);
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0003A8DC File Offset: 0x00038ADC
		public bool TryEndGetNextHop(IAsyncResult asyncResult, out string nextHop, out Guid tenantId)
		{
			nextHop = null;
			tenantId = Guid.Empty;
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)asyncResult;
			FindDomainResponse response = this.EndExecuteWithRetry<FindDomainResponse>(glsAsyncResult, "EndGetNextHop", (LocatorService proxy) => proxy.EndFindDomain(glsAsyncResult.InternalAsyncResult));
			FindDomainResult findDomainResult = this.ConstructFindDomainResult(response, glsAsyncResult.LoggerContext, Namespace.Exo);
			if (findDomainResult == null)
			{
				return false;
			}
			nextHop = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOSmtpNextHopDomain).GetStringValue();
			tenantId = findDomainResult.TenantId;
			return true;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0003A978 File Offset: 0x00038B78
		public bool DomainExists(string domainFqdn, Namespace[] namespaceArray)
		{
			SmtpDomain domain = this.ParseDomain(domainFqdn);
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(domain, GlsDirectorySession.ffoExoDomainProperties, GlsDirectorySession.noTenantProperties, this.glsReadFlag);
			GlsLoggerContext glsLoggerContext;
			FindDomainResponse response = this.ExecuteWithRetry<FindDomainResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindDomain(requestIdentity, request), "DomainExists", "FindDomain", domainFqdn, true, out glsLoggerContext);
			foreach (Namespace namespaceToCheck in namespaceArray)
			{
				FindDomainResult findDomainResult = this.ConstructFindDomainResult(response, glsLoggerContext, namespaceToCheck);
				if (findDomainResult != null && !string.IsNullOrEmpty(findDomainResult.Domain))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0003AA18 File Offset: 0x00038C18
		internal bool DomainExists(string domainFqdn)
		{
			return this.DomainExists(domainFqdn, new Namespace[]
			{
				this.glsCallerId.DefaultNamespace
			});
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0003AA44 File Offset: 0x00038C44
		public bool TryGetDomainFlag(string domainFqdn, GlsDomainFlags flag, out bool value)
		{
			SmtpDomain smtpDomain = this.ParseDomain(domainFqdn);
			BitVector32 bitVector;
			Guid guid;
			if (!this.TryGetExoDomainFlags(smtpDomain, out bitVector, out guid))
			{
				value = false;
				return false;
			}
			value = bitVector[(int)flag];
			return true;
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0003AA78 File Offset: 0x00038C78
		public void SetDomainFlag(string domainFqdn, GlsDomainFlags flag, bool value)
		{
			SmtpDomain smtpDomain = this.ParseDomain(domainFqdn);
			BitVector32 bitVector;
			Guid externalDirectoryOrganizationId;
			if (!this.TryGetExoDomainFlags(smtpDomain, out bitVector, out externalDirectoryOrganizationId))
			{
				throw new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(domainFqdn));
			}
			bitVector[(int)flag] = value;
			KeyValuePair<DomainProperty, PropertyValue> keyValuePair = new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ExoFlags, new PropertyValue(bitVector.Data));
			this.SaveDomain("SetDomainFlag", externalDirectoryOrganizationId, smtpDomain, DomainKeyType.UseExisting, new KeyValuePair<DomainProperty, PropertyValue>[]
			{
				keyValuePair
			});
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0003AAF0 File Offset: 0x00038CF0
		public bool TryGetTenantFlag(Guid externalDirectoryOrganizationId, GlsTenantFlags flag, out bool value)
		{
			BitVector32 bitVector;
			if (!this.TryGetExoTenantFlags(externalDirectoryOrganizationId, out bitVector))
			{
				value = false;
				return false;
			}
			value = bitVector[(int)flag];
			return true;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0003AB18 File Offset: 0x00038D18
		public void SetTenantFlag(Guid externalDirectoryOrganizationId, GlsTenantFlags tenantFlags, bool value)
		{
			BitVector32 bitVector;
			if (!this.TryGetExoTenantFlags(externalDirectoryOrganizationId, out bitVector))
			{
				throw new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(externalDirectoryOrganizationId.ToString()));
			}
			bitVector[(int)tenantFlags] = value;
			KeyValuePair<TenantProperty, PropertyValue> keyValuePair = new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOTenantFlags, new PropertyValue(bitVector.Data));
			this.SaveTenant("SetTenantFlag", externalDirectoryOrganizationId, new KeyValuePair<TenantProperty, PropertyValue>[]
			{
				keyValuePair
			});
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0003AB8A File Offset: 0x00038D8A
		public void AddTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN)
		{
			this.AddTenant(externalDirectoryOrganizationId, resourceForestFqdn, accountForestFqdn, smtpNextHopDomain, tenantFlags, tenantContainerCN, resourceForestFqdn);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0003AB9C File Offset: 0x00038D9C
		public void AddTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN, string primarySite)
		{
			if (tenantContainerCN != null && (tenantContainerCN == string.Empty || tenantContainerCN.Length > 64))
			{
				throw new ArgumentException(tenantContainerCN);
			}
			List<KeyValuePair<TenantProperty, PropertyValue>> list = new List<KeyValuePair<TenantProperty, PropertyValue>>();
			list.Add(new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOResourceForest, new PropertyValue(resourceForestFqdn)));
			list.Add(new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOAccountForest, new PropertyValue(accountForestFqdn)));
			list.Add(new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOSmtpNextHopDomain, new PropertyValue(smtpNextHopDomain)));
			list.Add(new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOPrimarySite, new PropertyValue(primarySite)));
			list.Add(new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOTenantFlags, new PropertyValue((int)tenantFlags)));
			if (tenantContainerCN != null)
			{
				list.Add(new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOTenantContainerCN, new PropertyValue(tenantContainerCN)));
			}
			this.SaveTenant("AddTenant", externalDirectoryOrganizationId, list.ToArray());
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0003AC72 File Offset: 0x00038E72
		public void AddMSAUser(string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId)
		{
			this.SaveMSAUser("AddUser", msaUserNetID, msaUserMemberName, externalDirectoryOrganizationId);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0003AC84 File Offset: 0x00038E84
		public void AddTenant(Guid externalDirectoryOrganizationId, CustomerType tenantType, string ffoRegion, string ffoVersion)
		{
			KeyValuePair<TenantProperty, PropertyValue> keyValuePair = new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.CustomerType, new PropertyValue((int)tenantType));
			KeyValuePair<TenantProperty, PropertyValue> keyValuePair2 = new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.Region, new PropertyValue(ffoRegion));
			KeyValuePair<TenantProperty, PropertyValue> keyValuePair3 = new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.Version, new PropertyValue(ffoVersion));
			this.SaveTenant("AddTenant", externalDirectoryOrganizationId, new KeyValuePair<TenantProperty, PropertyValue>[]
			{
				keyValuePair,
				keyValuePair2,
				keyValuePair3
			});
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0003AD03 File Offset: 0x00038F03
		public void UpdateTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN)
		{
			this.UpdateTenant(externalDirectoryOrganizationId, resourceForestFqdn, accountForestFqdn, smtpNextHopDomain, tenantFlags, tenantContainerCN, resourceForestFqdn);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003AD15 File Offset: 0x00038F15
		public void UpdateTenant(Guid externalDirectoryOrganizationId, string resourceForestFqdn, string accountForestFqdn, string smtpNextHopDomain, GlsTenantFlags tenantFlags, string tenantContainerCN, string primarySite)
		{
			this.AddTenant(externalDirectoryOrganizationId, resourceForestFqdn, accountForestFqdn, smtpNextHopDomain, tenantFlags, tenantContainerCN, primarySite);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003AD28 File Offset: 0x00038F28
		public void UpdateMSAUser(string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId)
		{
			this.SaveMSAUser("UpdateUser", msaUserNetID, msaUserMemberName, externalDirectoryOrganizationId);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0003AD50 File Offset: 0x00038F50
		public void RemoveTenant(Guid externalDirectoryOrganizationId)
		{
			DeleteTenantRequest request = LocatorServiceClientWriter.ConstructDeleteTenantRequest(externalDirectoryOrganizationId, this.glsCallerId.DefaultNamespace);
			this.ExecuteWriteWithRetry<DeleteTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.DeleteTenant(requestIdentity, request), "RemoveTenant", "DeleteTenant", externalDirectoryOrganizationId.ToString());
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0003ADBC File Offset: 0x00038FBC
		public void RemoveMSAUser(string msaUserNetID)
		{
			DeleteUserRequest request = this.ConstructDeleteUserRequest(msaUserNetID);
			this.ExecuteWriteWithRetry<DeleteUserResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.DeleteUser(requestIdentity, request), "RemoveUser", "DeleteUser", msaUserNetID);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0003AE14 File Offset: 0x00039014
		public bool TryGetTenantType(Guid externalDirectoryOrganizationId, out CustomerType tenantType)
		{
			FindTenantRequest request = this.ConstructFindTenantRequest(externalDirectoryOrganizationId, GlsDirectorySession.customerTypeProperty);
			GlsLoggerContext glsLoggerContext;
			FindTenantResponse response = this.ExecuteWithRetry<FindTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindTenant(requestIdentity, request), "TryGetTenantType", "FindTenant", externalDirectoryOrganizationId, true, out glsLoggerContext);
			FindTenantResult findTenantResult = this.ConstructFindTenantResult(response, glsLoggerContext, Namespace.Ffo);
			if (findTenantResult == null)
			{
				tenantType = CustomerType.None;
				return false;
			}
			tenantType = (CustomerType)findTenantResult.GetPropertyValue(TenantProperty.CustomerType).GetIntValue();
			return true;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0003AE84 File Offset: 0x00039084
		public bool TryGetTenantForestsByDomain(string domainFqdn, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string smtpNextHopDomain, out string tenantContainerCN)
		{
			bool flag;
			return this.TryGetTenantForestsByDomain(domainFqdn, out externalDirectoryOrganizationId, out resourceForestFqdn, out accountForestFqdn, out smtpNextHopDomain, out tenantContainerCN, out flag);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0003AEA4 File Offset: 0x000390A4
		public bool TryGetTenantForestsByDomain(string domainFqdn, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string smtpNextHopDomain, out string tenantContainerCN, out bool dataFromOfflineService)
		{
			GlobalLocatorServiceTenant globalLocatorServiceTenant;
			if (!this.TryGetTenantInfoByDomain(domainFqdn, out globalLocatorServiceTenant))
			{
				resourceForestFqdn = null;
				accountForestFqdn = null;
				externalDirectoryOrganizationId = Guid.Empty;
				smtpNextHopDomain = null;
				tenantContainerCN = null;
				dataFromOfflineService = false;
				return false;
			}
			resourceForestFqdn = globalLocatorServiceTenant.ResourceForest;
			accountForestFqdn = globalLocatorServiceTenant.AccountForest;
			smtpNextHopDomain = globalLocatorServiceTenant.SmtpNextHopDomain.Domain;
			tenantContainerCN = globalLocatorServiceTenant.TenantContainerCN;
			externalDirectoryOrganizationId = globalLocatorServiceTenant.ExternalDirectoryOrganizationId;
			dataFromOfflineService = globalLocatorServiceTenant.IsOfflineData;
			return true;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0003AF1A File Offset: 0x0003911A
		public bool TryGetTenantInfoByDomain(string domainFqdn, out GlobalLocatorServiceTenant glsTenant)
		{
			return this.TryGetTenantInfoByDomain(domainFqdn, out glsTenant, GlsDirectorySession.GlsCacheServiceMode);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0003AF40 File Offset: 0x00039140
		public bool TryGetTenantInfoByDomain(string domainFqdn, out GlobalLocatorServiceTenant glsTenant, GlsCacheServiceMode glsCacheServiceMode)
		{
			SmtpDomain domain = this.ParseDomain(domainFqdn);
			glsTenant = new GlobalLocatorServiceTenant();
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(domain, GlsDirectorySession.AllExoDomainProperties, GlsDirectorySession.AllExoTenantProperties, this.glsReadFlag);
			GlsLoggerContext glsLoggerContext;
			FindDomainResponse response = this.ExecuteWithRetry<FindDomainResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindDomain(requestIdentity, request), "TryGetTenantInfoByDomain", "FindDomain", domainFqdn, true, glsCacheServiceMode, out glsLoggerContext);
			FindDomainResult findDomainResult = this.ConstructFindDomainResult(response, glsLoggerContext, Namespace.Exo);
			if (findDomainResult == null || !findDomainResult.HasTenantProperties())
			{
				return false;
			}
			glsTenant.ResourceForest = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOResourceForest).GetStringValue();
			glsTenant.AccountForest = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOAccountForest).GetStringValue();
			glsTenant.SmtpNextHopDomain = new SmtpDomain(findDomainResult.GetTenantPropertyValue(TenantProperty.EXOSmtpNextHopDomain).GetStringValue());
			glsTenant.TenantContainerCN = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOTenantContainerCN).GetStringValue();
			glsTenant.ResumeCache = findDomainResult.GetTenantPropertyValue(TenantProperty.GlobalResumeCache).GetStringValue();
			glsTenant.PrimarySite = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOPrimarySite).GetStringValue();
			glsTenant.TenantFlags = (GlsTenantFlags)findDomainResult.GetTenantPropertyValue(TenantProperty.EXOTenantFlags).GetIntValue();
			glsTenant.ExternalDirectoryOrganizationId = findDomainResult.TenantId;
			glsTenant.IsOfflineData = this.IsDataReturnedFromOfflineService(glsLoggerContext);
			GlobalLocatorServiceTenant globalLocatorServiceTenant;
			if (GlsDirectorySession.TryGetOverrideTenant(glsTenant.ExternalDirectoryOrganizationId, out globalLocatorServiceTenant))
			{
				glsTenant = globalLocatorServiceTenant;
				return true;
			}
			return this.ValidateMandatoryTenantProperties(glsTenant, glsLoggerContext);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0003B09C File Offset: 0x0003929C
		public bool TryGetTenantForestsByOrgGuid(Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN)
		{
			bool flag;
			return this.TryGetTenantForestsByOrgGuid(externalDirectoryOrganizationId, out resourceForestFqdn, out accountForestFqdn, out tenantContainerCN, out flag);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0003B0B8 File Offset: 0x000392B8
		public bool TryGetTenantForestsByOrgGuid(Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN, out bool dataFromOfflineService)
		{
			GlobalLocatorServiceTenant globalLocatorServiceTenant;
			if (!this.TryGetTenantInfoByOrgGuid(externalDirectoryOrganizationId, out globalLocatorServiceTenant))
			{
				resourceForestFqdn = null;
				accountForestFqdn = null;
				tenantContainerCN = null;
				dataFromOfflineService = false;
				return false;
			}
			resourceForestFqdn = globalLocatorServiceTenant.ResourceForest;
			accountForestFqdn = globalLocatorServiceTenant.AccountForest;
			tenantContainerCN = globalLocatorServiceTenant.TenantContainerCN;
			dataFromOfflineService = globalLocatorServiceTenant.IsOfflineData;
			return true;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0003B103 File Offset: 0x00039303
		public bool TryGetTenantInfoByOrgGuid(Guid externalDirectoryOrganizationId, out GlobalLocatorServiceTenant glsTenant)
		{
			return this.TryGetTenantInfoByOrgGuid(externalDirectoryOrganizationId, out glsTenant, GlsDirectorySession.GlsCacheServiceMode);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0003B112 File Offset: 0x00039312
		public bool TryGetTenantInfoByOrgGuid(Guid externalDirectoryOrganizationId, out GlobalLocatorServiceTenant glsTenant, GlsCacheServiceMode glsCacheServiceMode)
		{
			return this.TryGetTenantInfoByOrgGuid(externalDirectoryOrganizationId, out glsTenant, glsCacheServiceMode, false);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0003B120 File Offset: 0x00039320
		public bool TryGetTenantForestsByMSAUserNetID(string userNetID, out Guid externalDirectoryOrganizationId, out string resourceForestFqdn, out string accountForestFqdn, out string tenantContainerCN)
		{
			GlobalLocatorServiceTenant globalLocatorServiceTenant;
			if (!this.TryGetTenantInfoByMSAUserNetID(userNetID, out globalLocatorServiceTenant))
			{
				resourceForestFqdn = null;
				accountForestFqdn = null;
				tenantContainerCN = null;
				externalDirectoryOrganizationId = Guid.Empty;
				return false;
			}
			externalDirectoryOrganizationId = globalLocatorServiceTenant.ExternalDirectoryOrganizationId;
			resourceForestFqdn = globalLocatorServiceTenant.ResourceForest;
			accountForestFqdn = globalLocatorServiceTenant.AccountForest;
			tenantContainerCN = globalLocatorServiceTenant.TenantContainerCN;
			return true;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0003B190 File Offset: 0x00039390
		public bool TryGetTenantInfoByMSAUserNetID(string msaUserNetID, out GlobalLocatorServiceTenant glsTenant)
		{
			glsTenant = new GlobalLocatorServiceTenant();
			FindUserRequest request = this.ConstructFindUserRequest(msaUserNetID, GlsDirectorySession.AllExoTenantProperties);
			GlsLoggerContext glsLoggerContext;
			FindUserResponse response = this.ExecuteWithRetry<FindUserResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindUser(requestIdentity, request), "TryGetTenantInfoByMSAUserNetID", "FindUser", msaUserNetID, true, out glsLoggerContext);
			FindUserResult findUserResult = this.ConstructFindUserResult(response, glsLoggerContext);
			if (findUserResult == null)
			{
				return false;
			}
			glsTenant.ResourceForest = findUserResult.GetTenantPropertyValue(TenantProperty.EXOResourceForest).GetStringValue();
			glsTenant.AccountForest = findUserResult.GetTenantPropertyValue(TenantProperty.EXOAccountForest).GetStringValue();
			glsTenant.TenantContainerCN = findUserResult.GetTenantPropertyValue(TenantProperty.EXOTenantContainerCN).GetStringValue();
			glsTenant.ResumeCache = findUserResult.GetTenantPropertyValue(TenantProperty.GlobalResumeCache).GetStringValue();
			glsTenant.PrimarySite = findUserResult.GetTenantPropertyValue(TenantProperty.EXOPrimarySite).GetStringValue();
			glsTenant.SmtpNextHopDomain = new SmtpDomain(findUserResult.GetTenantPropertyValue(TenantProperty.EXOSmtpNextHopDomain).GetStringValue());
			glsTenant.TenantFlags = (GlsTenantFlags)findUserResult.GetTenantPropertyValue(TenantProperty.EXOTenantFlags).GetIntValue();
			glsTenant.ExternalDirectoryOrganizationId = findUserResult.TenantId;
			return this.ValidateMandatoryTenantProperties(glsTenant, glsLoggerContext);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003B2A8 File Offset: 0x000394A8
		public void SetTenantEntryResumeCacheTime(Guid externalDirectoryOrganizationId, string dateTimeString)
		{
			if (string.IsNullOrEmpty(dateTimeString))
			{
				throw new ArgumentNullException(dateTimeString);
			}
			KeyValuePair<TenantProperty, PropertyValue>[] tenantProperties = new KeyValuePair<TenantProperty, PropertyValue>[]
			{
				new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.GlobalResumeCache, new PropertyValue(dateTimeString))
			};
			this.SaveTenant("SetTenantEntryResumeCacheTime", externalDirectoryOrganizationId, tenantProperties);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003B2F8 File Offset: 0x000394F8
		public void SetAccountForest(Guid externalDirectoryOrganizationId, string value, string tenantContainerCN = null)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(value);
			}
			KeyValuePair<TenantProperty, PropertyValue> keyValuePair = new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOAccountForest, new PropertyValue(value));
			KeyValuePair<TenantProperty, PropertyValue>[] tenantProperties;
			if (string.IsNullOrEmpty(tenantContainerCN))
			{
				tenantProperties = new KeyValuePair<TenantProperty, PropertyValue>[]
				{
					keyValuePair
				};
			}
			else
			{
				KeyValuePair<TenantProperty, PropertyValue> keyValuePair2 = new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOTenantContainerCN, new PropertyValue(tenantContainerCN));
				tenantProperties = new KeyValuePair<TenantProperty, PropertyValue>[]
				{
					keyValuePair,
					keyValuePair2
				};
			}
			this.SaveTenant("SetAccountForest", externalDirectoryOrganizationId, tenantProperties);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003B38C File Offset: 0x0003958C
		public void SetResourceForest(Guid externalDirectoryOrganizationId, string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(value);
			}
			KeyValuePair<TenantProperty, PropertyValue> keyValuePair = new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.EXOResourceForest, new PropertyValue(value));
			this.SaveTenant("SetResourceForest", externalDirectoryOrganizationId, new KeyValuePair<TenantProperty, PropertyValue>[]
			{
				keyValuePair
			});
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0003B3DC File Offset: 0x000395DC
		public void SetTenantVersion(Guid externalDirectoryOrganizationId, string newTenantVersion)
		{
			if (string.IsNullOrEmpty(newTenantVersion))
			{
				throw new ArgumentNullException(newTenantVersion);
			}
			KeyValuePair<TenantProperty, PropertyValue> keyValuePair = new KeyValuePair<TenantProperty, PropertyValue>(TenantProperty.Version, new PropertyValue(newTenantVersion));
			this.SaveTenant("SetTenantVersion", externalDirectoryOrganizationId, new KeyValuePair<TenantProperty, PropertyValue>[]
			{
				keyValuePair
			});
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0003B42B File Offset: 0x0003962B
		public bool TryGetTenantDomains(Guid externalDirectoryOrganizationId, out string[] acceptedDomainFqdns)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0003B432 File Offset: 0x00039632
		public bool TryGetTenantDomainFromDomainFqdn(string domainFqdn, out GlobalLocatorServiceDomain glsDomain)
		{
			return this.TryGetTenantDomainFromDomainFqdn(domainFqdn, out glsDomain, GlsDirectorySession.GlsCacheServiceMode);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003B441 File Offset: 0x00039641
		public bool TryGetTenantDomainFromDomainFqdn(string domainFqdn, out GlobalLocatorServiceDomain glsDomain, GlsCacheServiceMode glsCacheServiceMode)
		{
			return this.TryGetTenantDomainFromDomainFqdn(domainFqdn, out glsDomain, false, glsCacheServiceMode);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003B44D File Offset: 0x0003964D
		public bool TryGetTenantDomainFromDomainFqdn(string domainFqdn, out GlobalLocatorServiceDomain glsDomain, bool skipTenantCheck)
		{
			return this.TryGetTenantDomainFromDomainFqdn(domainFqdn, out glsDomain, skipTenantCheck, GlsDirectorySession.GlsCacheServiceMode);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003B474 File Offset: 0x00039674
		public bool TryGetTenantDomainFromDomainFqdn(string domainFqdn, out GlobalLocatorServiceDomain glsDomain, bool skipTenantCheck, GlsCacheServiceMode glsCacheServiceMode)
		{
			SmtpDomain domain = this.ParseDomain(domainFqdn);
			glsDomain = new GlobalLocatorServiceDomain();
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(domain, GlsDirectorySession.AllExoDomainProperties, GlsDirectorySession.AllExoTenantProperties, this.glsReadFlag);
			GlsLoggerContext glsLoggerContext;
			FindDomainResponse response = this.ExecuteWithRetry<FindDomainResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindDomain(requestIdentity, request), "TryGetTenantDomainFromDomainFqdn", "FindDomain", domainFqdn, true, glsCacheServiceMode, out glsLoggerContext);
			FindDomainResult findDomainResult = this.ConstructFindDomainResult(response, glsLoggerContext, Namespace.Exo, Namespace.Exo, skipTenantCheck);
			if (findDomainResult == null)
			{
				return false;
			}
			glsDomain.DomainName = new SmtpDomain(findDomainResult.Domain);
			glsDomain.DomainInUse = findDomainResult.GetDomainPropertyValue(DomainProperty.ExoDomainInUse).GetBoolValue();
			PropertyValue domainPropertyValue = findDomainResult.GetDomainPropertyValue(DomainProperty.ExoFlags);
			if (domainPropertyValue != null && Enum.IsDefined(typeof(GlsDomainFlags), domainPropertyValue.GetIntValue()))
			{
				glsDomain.DomainFlags = new GlsDomainFlags?((GlsDomainFlags)domainPropertyValue.GetIntValue());
			}
			else
			{
				glsDomain.DomainFlags = null;
			}
			glsDomain.ExternalDirectoryOrganizationId = findDomainResult.TenantId;
			return true;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0003B574 File Offset: 0x00039774
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain)
		{
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, isInitialDomain, false, false);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003B584 File Offset: 0x00039784
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, string ffoRegion, string ffoServiceVersion)
		{
			KeyValuePair<DomainProperty, PropertyValue> keyValuePair = new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.Region, new PropertyValue(ffoRegion));
			KeyValuePair<DomainProperty, PropertyValue> keyValuePair2 = new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ServiceVersion, new PropertyValue(ffoServiceVersion));
			KeyValuePair<DomainProperty, PropertyValue> keyValuePair3 = new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.DomainInUse, new PropertyValue(true));
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, isInitialDomain ? DomainKeyType.InitialDomain : DomainKeyType.CustomDomain, new KeyValuePair<DomainProperty, PropertyValue>[]
			{
				keyValuePair,
				keyValuePair2,
				keyValuePair3
			});
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003B608 File Offset: 0x00039808
		public void UpdateAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn)
		{
			SmtpDomain smtpDomain = this.ParseDomain(domainFqdn);
			BitVector32 bitVector;
			Guid guid;
			if (!this.TryGetExoDomainFlags(smtpDomain, out bitVector, out guid))
			{
				throw new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(domainFqdn));
			}
			if (!bitVector[1])
			{
				return;
			}
			bool nego2Enabled = false;
			bool oauth2ClientProfileEnabled = bitVector[2];
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, DomainKeyType.UseExisting, nego2Enabled, oauth2ClientProfileEnabled);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0003B659 File Offset: 0x00039859
		public void UpdateAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, KeyValuePair<DomainProperty, PropertyValue>[] properties)
		{
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, DomainKeyType.UseExisting, properties);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003B665 File Offset: 0x00039865
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, bool nego2Enabled, bool oauth2ClientProfileEnabled)
		{
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, isInitialDomain ? DomainKeyType.InitialDomain : DomainKeyType.CustomDomain, nego2Enabled, oauth2ClientProfileEnabled);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003B67A File Offset: 0x0003987A
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool isInitialDomain, KeyValuePair<DomainProperty, PropertyValue>[] properties)
		{
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, isInitialDomain ? DomainKeyType.InitialDomain : DomainKeyType.CustomDomain, properties);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003B6A4 File Offset: 0x000398A4
		public void GetFfoTenantSettingsByDomain(string domain, out Guid tenantId, out string region, out string version, out CustomerType tenantType)
		{
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(new SmtpDomain(domain), GlsDirectorySession.ffoDomainProperties, GlsDirectorySession.customerTypeProperty, this.glsReadFlag);
			GlsLoggerContext glsLoggerContext;
			FindDomainResponse response = this.ExecuteWithRetry<FindDomainResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindDomain(requestIdentity, request), "GetFfoTenantSettingsByDomain", "FindDomain", domain, true, out glsLoggerContext);
			FindDomainResult findDomainResult = this.ConstructFindDomainResult(response, glsLoggerContext, Namespace.Ffo);
			if (findDomainResult == null)
			{
				region = null;
				version = null;
				tenantId = Guid.Empty;
				tenantType = CustomerType.None;
				throw new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(domain));
			}
			tenantId = findDomainResult.TenantId;
			tenantType = (CustomerType)findDomainResult.GetTenantPropertyValue(TenantProperty.CustomerType).GetIntValue();
			region = findDomainResult.GetDomainPropertyValue(DomainProperty.Region).GetStringValue();
			version = findDomainResult.GetDomainPropertyValue(DomainProperty.ServiceVersion).GetStringValue();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003B784 File Offset: 0x00039984
		public string GetFfoTenantRegionByOrgGuid(Guid orgGuid)
		{
			string result = null;
			FindTenantRequest request = this.ConstructFindTenantRequest(orgGuid, GlsDirectorySession.ffoTenantRegionProperty);
			GlsLoggerContext glsLoggerContext;
			FindTenantResponse findTenantResponse = this.ExecuteWithRetry<FindTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindTenant(requestIdentity, request), "GetFfoTenantRegionByOrgGuid", "FindTenant", orgGuid, true, out glsLoggerContext);
			FindTenantResult findTenantResult = this.ConstructFindTenantResult(findTenantResponse, glsLoggerContext, Namespace.Ffo);
			if (findTenantResult == null && findTenantResponse.TenantInfo == null)
			{
				throw new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(orgGuid.ToString()));
			}
			if (findTenantResult != null)
			{
				result = findTenantResult.GetPropertyValue(TenantProperty.Region).GetStringValue();
			}
			return result;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003B830 File Offset: 0x00039A30
		public IEnumerable<string> GetDomainNamesProvisionedByEXO(IEnumerable<SmtpDomain> domains)
		{
			if (domains == null)
			{
				throw new ArgumentNullException("domains");
			}
			List<string> list = new List<string>(domains.Count<SmtpDomain>());
			FindDomainsRequest request = LocatorServiceClientReader.ConstructFindDomainsRequest(domains, GlsDirectorySession.exoDomainInUseProperty, GlsDirectorySession.noTenantProperties, this.glsReadFlag);
			GlsLoggerContext glsLoggerContext;
			FindDomainsResponse findDomainsResponse = this.ExecuteWithRetry<FindDomainsResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindDomains(requestIdentity, request), "GetDomainNamesProvisionedByEXO", "FindDomains", string.Join<SmtpDomain>(",", domains), true, out glsLoggerContext);
			foreach (FindDomainResponse findDomainResponse in findDomainsResponse.DomainsResponse)
			{
				findDomainResponse.TransactionID = findDomainsResponse.TransactionID;
				FindDomainResult findDomainResult = this.ConstructFindDomainResult(findDomainResponse, glsLoggerContext, Namespace.Exo);
				if (findDomainResult != null && findDomainResult.GetDomainPropertyValue(DomainProperty.ExoDomainInUse).GetBoolValue())
				{
					list.Add(findDomainResult.Domain);
				}
			}
			return list;
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003B91C File Offset: 0x00039B1C
		public bool TenantExists(Guid externalDirectoryOrganizationId, Namespace namespaceToCheck)
		{
			FindTenantRequest request = this.ConstructFindTenantRequest(externalDirectoryOrganizationId, GlsDirectorySession.ffoExoTenantProperties);
			GlsLoggerContext glsLoggerContext;
			FindTenantResponse response = this.ExecuteWithRetry<FindTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindTenant(requestIdentity, request), "TenantExists", "FindTenant", externalDirectoryOrganizationId, true, out glsLoggerContext);
			FindTenantResult findTenantResult = this.ConstructFindTenantResult(response, glsLoggerContext, namespaceToCheck);
			return findTenantResult != null;
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0003B990 File Offset: 0x00039B90
		public bool MSAUserExists(string msaUserNetID)
		{
			FindUserRequest request = this.ConstructFindUserRequest(msaUserNetID, GlsDirectorySession.AllExoTenantProperties);
			GlsLoggerContext glsLoggerContext;
			FindUserResponse response = this.ExecuteWithRetry<FindUserResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindUser(requestIdentity, request), "MSAUserExists", "FindUser", msaUserNetID, true, out glsLoggerContext);
			FindUserResult findUserResult = this.ConstructFindUserResult(response, glsLoggerContext);
			return null != findUserResult;
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003BA00 File Offset: 0x00039C00
		public bool TryGetMSAUserMemberName(string msaUserNetID, out string msaUserMemberName)
		{
			FindUserRequest request = this.ConstructFindUserRequest(msaUserNetID, GlsDirectorySession.AllExoTenantProperties);
			GlsLoggerContext glsLoggerContext;
			FindUserResponse response = this.ExecuteWithRetry<FindUserResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindUser(requestIdentity, request), "TryGetMSAUserMemberName", "FindUser", msaUserNetID, true, out glsLoggerContext);
			FindUserResult findUserResult = this.ConstructFindUserResult(response, glsLoggerContext);
			if (findUserResult == null)
			{
				msaUserMemberName = null;
				return false;
			}
			msaUserMemberName = findUserResult.MSAUserMemberName;
			return true;
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003BA78 File Offset: 0x00039C78
		public bool TryGetFfoTenantProvisioningProperties(Guid externalDirectoryOrganizationId, out string version, out CustomerType tenantType, out string region)
		{
			FindTenantRequest request = this.ConstructFindTenantRequest(externalDirectoryOrganizationId, GlsDirectorySession.ffoTenantProperties);
			if (GlsCallerId.IsForwardSyncCallerID(this.glsCallerId))
			{
				request.ReadFlag = 2;
			}
			GlsLoggerContext glsLoggerContext;
			FindTenantResponse response = this.ExecuteWithRetry<FindTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindTenant(requestIdentity, request), "TryGetFfoTenantProvisioningProperties", "FindTenant", externalDirectoryOrganizationId, true, out glsLoggerContext);
			FindTenantResult findTenantResult = this.ConstructFindTenantResult(response, glsLoggerContext, Namespace.Ffo);
			if (findTenantResult == null)
			{
				version = null;
				tenantType = CustomerType.None;
				region = null;
				return false;
			}
			tenantType = (CustomerType)findTenantResult.GetPropertyValue(TenantProperty.CustomerType).GetIntValue();
			version = findTenantResult.GetPropertyValue(TenantProperty.Version).GetStringValue();
			region = findTenantResult.GetPropertyValue(TenantProperty.Region).GetStringValue();
			return true;
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0003BB6C File Offset: 0x00039D6C
		public IAsyncResult BeginGetFfoTenantAttributionPropertiesByOrgId(Guid externalDirectoryOrganizationId, object clientAsyncState, AsyncCallback clientCallback)
		{
			FindTenantRequest request = this.ConstructFindTenantRequest(externalDirectoryOrganizationId, GlsDirectorySession.customerAttributionTenantProperties);
			return this.BeginExecuteWithRetry(delegate(LocatorService proxy, RequestIdentity requestIdentity, GlsAsyncResult asyncResult)
			{
				IAsyncResult internalAsyncResult = proxy.BeginFindTenant(requestIdentity, request, new AsyncCallback(this.OnAsyncRequestCompleted), asyncResult);
				asyncResult.InternalAsyncResult = internalAsyncResult;
			}, clientCallback, clientAsyncState, "BeginGetFfoTenantAttributionPropertiesByOrgId", "FindTenant", externalDirectoryOrganizationId, true);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0003BBD8 File Offset: 0x00039DD8
		public bool TryEndGetFfoTenantAttributionPropertiesByOrgId(IAsyncResult asyncResult, out string ffoRegion, out string exoNextHop, out CustomerType tenantType, out string exoResourceForest, out string exoAccountForest, out string exoTenantContainer)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)asyncResult;
			FindTenantResponse response = this.EndExecuteWithRetry<FindTenantResponse>(glsAsyncResult, "TryEndGetFfoTenantAttributionPropertiesByOrgId", (LocatorService proxy) => proxy.EndFindTenant(glsAsyncResult.InternalAsyncResult));
			FindTenantResult findTenantResult = this.ConstructFindTenantResult(response, glsAsyncResult.LoggerContext, Namespace.Ffo);
			if (findTenantResult == null)
			{
				ffoRegion = null;
				exoNextHop = null;
				tenantType = CustomerType.None;
				exoResourceForest = null;
				exoAccountForest = null;
				exoTenantContainer = null;
				return false;
			}
			ffoRegion = findTenantResult.GetPropertyValue(TenantProperty.Region).GetStringValue();
			exoNextHop = findTenantResult.GetPropertyValue(TenantProperty.EXOSmtpNextHopDomain).GetStringValue();
			tenantType = (CustomerType)findTenantResult.GetPropertyValue(TenantProperty.CustomerType).GetIntValue();
			exoResourceForest = findTenantResult.GetPropertyValue(TenantProperty.EXOResourceForest).GetStringValue();
			exoAccountForest = findTenantResult.GetPropertyValue(TenantProperty.EXOAccountForest).GetStringValue();
			exoTenantContainer = findTenantResult.GetPropertyValue(TenantProperty.EXOTenantContainerCN).GetStringValue();
			return true;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0003BCF4 File Offset: 0x00039EF4
		public IAsyncResult BeginGetFfoTenantAttributionPropertiesByDomain(SmtpDomain domain, object clientAsyncState, AsyncCallback clientCallback)
		{
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(domain, GlsDirectorySession.customerAttributionDomainProperties, GlsDirectorySession.customerAttributionTenantProperties, this.glsReadFlag);
			return this.BeginExecuteWithRetry(delegate(LocatorService proxy, RequestIdentity requestIdentity, GlsAsyncResult asyncResult)
			{
				IAsyncResult internalAsyncResult = proxy.BeginFindDomain(requestIdentity, request, new AsyncCallback(this.OnAsyncRequestCompleted), asyncResult);
				asyncResult.InternalAsyncResult = internalAsyncResult;
			}, clientCallback, clientAsyncState, "BeginGetFfoTenantAttributionPropertiesByDomain", "FindDomain", domain, true);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003BD68 File Offset: 0x00039F68
		public bool TryEndGetFfoTenantAttributionPropertiesByDomain(IAsyncResult asyncResult, out string ffoRegion, out string ffoVersion, out Guid externalDirectoryOrganizationId, out string exoNextHop, out CustomerType tenantType, out DomainIPv6State ipv6Enabled, out string exoResourceForest, out string exoAccountForest, out string exoTenantContainer)
		{
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)asyncResult;
			FindDomainResponse response = this.EndExecuteWithRetry<FindDomainResponse>(glsAsyncResult, "TryEndGetFfoTenantAttributionPropertiesByDomain", (LocatorService proxy) => proxy.EndFindDomain(glsAsyncResult.InternalAsyncResult));
			FindDomainResult findDomainResult = this.ConstructFindDomainResult(response, glsAsyncResult.LoggerContext, Namespace.IgnoreComparison, Namespace.Ffo, false);
			if (findDomainResult == null)
			{
				ffoRegion = null;
				ffoVersion = null;
				externalDirectoryOrganizationId = Guid.Empty;
				exoNextHop = null;
				tenantType = CustomerType.None;
				ipv6Enabled = DomainIPv6State.Unknown;
				exoResourceForest = null;
				exoAccountForest = null;
				exoTenantContainer = null;
				return false;
			}
			ffoRegion = findDomainResult.GetDomainPropertyValue(DomainProperty.Region).GetStringValue();
			ffoVersion = findDomainResult.GetDomainPropertyValue(DomainProperty.ServiceVersion).GetStringValue();
			externalDirectoryOrganizationId = findDomainResult.TenantId;
			exoNextHop = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOSmtpNextHopDomain).GetStringValue();
			tenantType = (CustomerType)findDomainResult.GetTenantPropertyValue(TenantProperty.CustomerType).GetIntValue();
			ipv6Enabled = (DomainIPv6State)findDomainResult.GetDomainPropertyValue(DomainProperty.IPv6).GetIntValue();
			exoResourceForest = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOResourceForest).GetStringValue();
			exoAccountForest = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOAccountForest).GetStringValue();
			exoTenantContainer = findDomainResult.GetTenantPropertyValue(TenantProperty.EXOTenantContainerCN).GetStringValue();
			return true;
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003BE90 File Offset: 0x0003A090
		private void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, DomainKeyType domainKeyType, bool nego2Enabled, bool oauth2ClientProfileEnabled)
		{
			BitVector32 bitVector = default(BitVector32);
			bitVector[1] = nego2Enabled;
			bitVector[2] = oauth2ClientProfileEnabled;
			KeyValuePair<DomainProperty, PropertyValue> keyValuePair = new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ExoFlags, new PropertyValue(bitVector.Data));
			KeyValuePair<DomainProperty, PropertyValue> keyValuePair2 = new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ExoDomainInUse, new PropertyValue(true));
			this.AddAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, domainKeyType, new KeyValuePair<DomainProperty, PropertyValue>[]
			{
				keyValuePair,
				keyValuePair2
			});
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003BF10 File Offset: 0x0003A110
		public void AddAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, DomainKeyType domainKeyType, KeyValuePair<DomainProperty, PropertyValue>[] properties)
		{
			SmtpDomain smtpDomain = this.ParseDomain(domainFqdn);
			this.SaveDomain("AddAcceptedDomain", externalDirectoryOrganizationId, smtpDomain, domainKeyType, properties);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003BF35 File Offset: 0x0003A135
		public void RemoveAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn)
		{
			this.RemoveAcceptedDomain(externalDirectoryOrganizationId, domainFqdn, false);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003BF58 File Offset: 0x0003A158
		public void RemoveAcceptedDomain(Guid externalDirectoryOrganizationId, string domainFqdn, bool skipTenantCheck)
		{
			SmtpDomain domain = this.ParseDomain(domainFqdn);
			DeleteDomainRequest request = LocatorServiceClientWriter.ConstructDeleteDomainRequest(domain, externalDirectoryOrganizationId, this.glsCallerId.DefaultNamespace, skipTenantCheck);
			this.ExecuteWriteWithRetry<DeleteDomainResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.DeleteDomain(requestIdentity, request), "RemoveAcceptedDomain", "DeleteDomain", domainFqdn);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0003BFAC File Offset: 0x0003A1AC
		public void SetDomainVersion(Guid externalDirectoryOrganizationId, string domainFqdn, string newDomainVersion)
		{
			if (string.IsNullOrEmpty(domainFqdn))
			{
				throw new ArgumentNullException(domainFqdn);
			}
			if (string.IsNullOrEmpty(newDomainVersion))
			{
				throw new ArgumentNullException(newDomainVersion);
			}
			SmtpDomain smtpDomain = this.ParseDomain(domainFqdn);
			KeyValuePair<DomainProperty, PropertyValue> keyValuePair = new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.ServiceVersion, new PropertyValue(newDomainVersion));
			KeyValuePair<DomainProperty, PropertyValue> keyValuePair2 = new KeyValuePair<DomainProperty, PropertyValue>(DomainProperty.DomainInUse, new PropertyValue(true));
			this.SaveDomain("SetDomainVersion", externalDirectoryOrganizationId, smtpDomain, DomainKeyType.UseExisting, new KeyValuePair<DomainProperty, PropertyValue>[]
			{
				keyValuePair,
				keyValuePair2
			});
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x0003C033 File Offset: 0x0003A233
		internal static SmtpAddress ParseMemberName(string memberName)
		{
			if (string.IsNullOrEmpty(memberName))
			{
				throw new ArgumentNullException("memberName");
			}
			if (!SmtpAddress.IsValidSmtpAddress(memberName))
			{
				throw new ArgumentException("memberName");
			}
			return SmtpAddress.Parse(memberName);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003C064 File Offset: 0x0003A264
		internal static void ThrowIfInvalidNetID(string netID, string parameterName)
		{
			NetID netID2;
			if (!NetID.TryParse(netID, out netID2))
			{
				throw new ArgumentException(parameterName);
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003C082 File Offset: 0x0003A282
		internal static void ThrowIfInvalidSmtpAddress(string smtpAddress, string parameterName)
		{
			if (!SmtpAddress.IsValidSmtpAddress(smtpAddress))
			{
				throw new ArgumentException(parameterName);
			}
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x0003C093 File Offset: 0x0003A293
		internal static void ThrowIfEmptyGuid(Guid argument, string parameterName)
		{
			if (argument == Guid.Empty)
			{
				throw new ArgumentException(parameterName);
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003C0A9 File Offset: 0x0003A2A9
		internal static void ThrowIfNull(object argument, string parameterName)
		{
			if (argument == null)
			{
				throw new ArgumentException(parameterName);
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003C0CC File Offset: 0x0003A2CC
		private bool TryGetTenantInfoByOrgGuid(Guid externalDirectoryOrganizationId, out GlobalLocatorServiceTenant glsTenant, GlsCacheServiceMode glsCacheServiceMode, bool skipOverrideCheck)
		{
			if (!skipOverrideCheck && GlsDirectorySession.TryGetOverrideTenant(externalDirectoryOrganizationId, out glsTenant))
			{
				return true;
			}
			glsTenant = new GlobalLocatorServiceTenant();
			FindTenantRequest request = this.ConstructFindTenantRequest(externalDirectoryOrganizationId, GlsDirectorySession.AllExoTenantProperties);
			GlsLoggerContext glsLoggerContext;
			FindTenantResponse response = this.ExecuteWithRetry<FindTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindTenant(requestIdentity, request), "TryGetTenantInfoByOrgGuid", "FindTenant", externalDirectoryOrganizationId, true, glsCacheServiceMode, out glsLoggerContext);
			FindTenantResult findTenantResult = this.ConstructFindTenantResult(response, glsLoggerContext, Namespace.Exo);
			if (findTenantResult == null)
			{
				return false;
			}
			glsTenant.ResourceForest = findTenantResult.GetPropertyValue(TenantProperty.EXOResourceForest).GetStringValue();
			glsTenant.AccountForest = findTenantResult.GetPropertyValue(TenantProperty.EXOAccountForest).GetStringValue();
			glsTenant.TenantContainerCN = findTenantResult.GetPropertyValue(TenantProperty.EXOTenantContainerCN).GetStringValue();
			glsTenant.ResumeCache = findTenantResult.GetPropertyValue(TenantProperty.GlobalResumeCache).GetStringValue();
			glsTenant.PrimarySite = findTenantResult.GetPropertyValue(TenantProperty.EXOPrimarySite).GetStringValue();
			glsTenant.SmtpNextHopDomain = new SmtpDomain(findTenantResult.GetPropertyValue(TenantProperty.EXOSmtpNextHopDomain).GetStringValue());
			glsTenant.TenantFlags = (GlsTenantFlags)findTenantResult.GetPropertyValue(TenantProperty.EXOTenantFlags).GetIntValue();
			glsTenant.ExternalDirectoryOrganizationId = externalDirectoryOrganizationId;
			glsTenant.IsOfflineData = this.IsDataReturnedFromOfflineService(glsLoggerContext);
			return this.ValidateMandatoryTenantProperties(glsTenant, glsLoggerContext);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0003C21C File Offset: 0x0003A41C
		private void SaveTenant(string operation, Guid externalDirectoryOrganizationId, KeyValuePair<TenantProperty, PropertyValue>[] tenantProperties)
		{
			SaveTenantRequest request = LocatorServiceClientWriter.ConstructSaveTenantRequest(externalDirectoryOrganizationId, tenantProperties);
			this.ExecuteWriteWithRetry<SaveTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.SaveTenant(requestIdentity, request), operation, "SaveTenant", externalDirectoryOrganizationId.ToString());
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003C27C File Offset: 0x0003A47C
		private void SaveMSAUser(string operation, string msaUserNetID, string msaUserMemberName, Guid externalDirectoryOrganizationId)
		{
			SaveUserRequest request = this.ConstructSaveUserRequest(msaUserNetID, msaUserMemberName, externalDirectoryOrganizationId);
			this.ExecuteWriteWithRetry<SaveUserResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.SaveUser(requestIdentity, request), operation, "SaveUser", msaUserNetID);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0003C2D0 File Offset: 0x0003A4D0
		private void SaveDomain(string operation, Guid externalDirectoryOrganizationId, SmtpDomain smtpDomain, DomainKeyType domainKeyType, KeyValuePair<DomainProperty, PropertyValue>[] domainProperties)
		{
			SaveDomainRequest request = LocatorServiceClientWriter.ConstructSaveDomainRequest(smtpDomain, null, externalDirectoryOrganizationId, domainProperties);
			request.DomainKeyType = domainKeyType;
			this.ExecuteWriteWithRetry<SaveDomainResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.SaveDomain(requestIdentity, request), operation, "SaveDomain", smtpDomain.ToString());
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003C338 File Offset: 0x0003A538
		private bool TryGetExoDomainFlags(SmtpDomain smtpDomain, out BitVector32 flags, out Guid tenantId)
		{
			FindDomainRequest request = LocatorServiceClientReader.ConstructFindDomainRequest(smtpDomain, GlsDirectorySession.exoDomainFlagsProperty, GlsDirectorySession.noTenantProperties, this.glsReadFlag);
			GlsLoggerContext glsLoggerContext;
			FindDomainResponse response = this.ExecuteWithRetry<FindDomainResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindDomain(requestIdentity, request), "TryGetExoDomainFlags", "FindDomain", smtpDomain.Domain, true, out glsLoggerContext);
			FindDomainResult findDomainResult = this.ConstructFindDomainResult(response, glsLoggerContext, Namespace.Exo);
			if (findDomainResult == null)
			{
				tenantId = Guid.Empty;
				flags = new BitVector32(0);
				return false;
			}
			flags = new BitVector32(findDomainResult.GetDomainPropertyValue(DomainProperty.ExoFlags).GetIntValue());
			tenantId = findDomainResult.TenantId;
			return true;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003C3F4 File Offset: 0x0003A5F4
		private bool TryGetExoTenantFlags(Guid tenantId, out BitVector32 flags)
		{
			FindTenantRequest request = this.ConstructFindTenantRequest(tenantId, GlsDirectorySession.exoTenantFlagsProperty);
			GlsLoggerContext glsLoggerContext;
			FindTenantResponse response = this.ExecuteWithRetry<FindTenantResponse>((LocatorService proxy, RequestIdentity requestIdentity) => proxy.FindTenant(requestIdentity, request), "TryGetExoTenantFlags", "FindTenant", tenantId, true, out glsLoggerContext);
			FindTenantResult findTenantResult = this.ConstructFindTenantResult(response, glsLoggerContext, Namespace.Exo);
			if (findTenantResult == null)
			{
				flags = new BitVector32(0);
				return false;
			}
			flags = new BitVector32(findTenantResult.GetPropertyValue(TenantProperty.EXOTenantFlags).GetIntValue());
			return true;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0003C478 File Offset: 0x0003A678
		private bool ValidateMandatoryTenantProperties(GlobalLocatorServiceTenant glsTenant, GlsLoggerContext loggerContext)
		{
			if (string.IsNullOrEmpty(glsTenant.ResourceForest) && string.IsNullOrEmpty(glsTenant.AccountForest))
			{
				GLSLogger.LogException(loggerContext, new GlsTenantNotFoundException(DirectoryStrings.TenantNotFoundInGlsError(glsTenant.ExternalDirectoryOrganizationId.ToString())));
				return false;
			}
			if (string.IsNullOrEmpty(glsTenant.ResourceForest) || string.IsNullOrEmpty(glsTenant.AccountForest))
			{
				GLSLogger.LogException(loggerContext, new GlsTenantNotFoundException(DirectoryStrings.InvalidTenantRecordInGls(glsTenant.ExternalDirectoryOrganizationId, glsTenant.ResourceForest, glsTenant.AccountForest)));
				return false;
			}
			return this.IsValidForestName(glsTenant.ExternalDirectoryOrganizationId.ToString(), glsTenant.ResourceForest, loggerContext) && this.IsValidForestName(glsTenant.ExternalDirectoryOrganizationId.ToString(), glsTenant.AccountForest, loggerContext);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003C54C File Offset: 0x0003A74C
		private bool IsValidForestName(string tenant, string forest, GlsLoggerContext loggerContext)
		{
			PartitionId partitionId;
			Exception ex;
			if (!PartitionId.TryParse(forest, out partitionId, out ex))
			{
				GLSLogger.LogException(loggerContext, new GlsPermanentException(DirectoryStrings.InvalidForestFqdnInGls(forest, tenant, ex.Message)));
				return false;
			}
			return true;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0003C580 File Offset: 0x0003A780
		private SmtpDomain ParseDomain(string domainFqdn)
		{
			if (string.IsNullOrEmpty(domainFqdn))
			{
				throw new ArgumentNullException("domainFqdn");
			}
			SmtpDomain result;
			if (!SmtpDomain.TryParse(domainFqdn, out result))
			{
				throw new ArgumentException("domainFqdn");
			}
			return result;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003C5B8 File Offset: 0x0003A7B8
		private void OnAsyncRequestCompleted(IAsyncResult internalAR)
		{
			GLSLogger.FaultInjectionDelayTraceForAsync();
			GlsAsyncResult glsAsyncResult = (GlsAsyncResult)internalAR.AsyncState;
			glsAsyncResult.InternalAsyncResult = internalAR;
			glsAsyncResult.IsCompleted = true;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003C5E4 File Offset: 0x0003A7E4
		private TResult ExecuteWriteWithRetry<TResult>(Func<LocatorService, RequestIdentity, TResult> method, string methodName, string glsApiName, object parameterValue) where TResult : ResponseBase
		{
			GlsLoggerContext context;
			TResult tresult = this.ExecuteWithRetry<TResult>(method, methodName, glsApiName, parameterValue, false, out context);
			GLSLogger.LogResponse(context, GLSLogger.StatusCode.WriteSuccess, tresult, null);
			return tresult;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003C610 File Offset: 0x0003A810
		private TResult ExecuteWithRetry<TResult>(Func<LocatorService, RequestIdentity, TResult> method, string methodName, string glsApiName, object parameterValue, bool isRead, out GlsLoggerContext glsLoggerContext) where TResult : ResponseBase
		{
			GlsLoggerContext glsLoggerContext2;
			TResult tresult = this.ExecuteWithRetry<TResult>(method, methodName, glsApiName, parameterValue, isRead, GlsDirectorySession.GlsCacheServiceMode, out glsLoggerContext2);
			glsLoggerContext = glsLoggerContext2;
			GLSLogger.LogResponse(glsLoggerContext2, isRead ? GLSLogger.StatusCode.Found : GLSLogger.StatusCode.WriteSuccess, tresult, null);
			return tresult;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0003C748 File Offset: 0x0003A948
		private TResult ExecuteWithRetry<TResult>(Func<LocatorService, RequestIdentity, TResult> method, string methodName, string glsApiName, object parameterValue, bool isRead, GlsCacheServiceMode glsCacheServiceMode, out GlsLoggerContext glsLoggerContext) where TResult : ResponseBase
		{
			TResult response = default(TResult);
			GlsLoggerContext loggerContext = null;
			Exception ex = null;
			string endpointHostNameForLogging;
			DirectoryServiceProxyPool<LocatorService> directoryServiceProxyPool;
			int num;
			switch (glsCacheServiceMode)
			{
			case GlsCacheServiceMode.CacheDisabled:
				directoryServiceProxyPool = GlsDirectorySession.ServiceProxyPool;
				num = 0;
				endpointHostNameForLogging = GlsDirectorySession.endpointHostName;
				break;
			case GlsCacheServiceMode.CacheAsExceptionFallback:
				directoryServiceProxyPool = GlsDirectorySession.ServiceProxyPool;
				num = 1;
				endpointHostNameForLogging = GlsDirectorySession.endpointHostName;
				break;
			default:
				if (glsCacheServiceMode != GlsCacheServiceMode.CacheOnly)
				{
					throw new ArgumentException("Unsupported mode");
				}
				directoryServiceProxyPool = GlsDirectorySession.OfflineServiceProxyPool;
				num = 0;
				endpointHostNameForLogging = "localhost";
				break;
			}
			do
			{
				try
				{
					directoryServiceProxyPool.CallServiceWithRetry(delegate(IPooledServiceProxy<LocatorService> proxy)
					{
						RequestIdentity uniqueRequestIdentity = this.GetUniqueRequestIdentity();
						loggerContext = new GlsLoggerContext(glsApiName, parameterValue, endpointHostNameForLogging, isRead, uniqueRequestIdentity.RequestTrackingGuid);
						try
						{
							GLSLogger.FaultInjectionTrace();
							response = method(proxy.Client, uniqueRequestIdentity);
							string text = GlsDirectorySession.ExtractMachineNameFromDiagnostics(response.Diagnostics);
							if (!string.IsNullOrEmpty(text) && !string.Equals(proxy.Tag, text))
							{
								proxy.Tag = text;
							}
							loggerContext.ConnectionId = proxy.Client.GetHashCode().ToString();
						}
						catch (Exception ex3)
						{
							loggerContext.ConnectionId = proxy.Client.GetHashCode().ToString();
							GLSLogger.LogException(loggerContext, ex3);
							throw;
						}
					}, methodName, 3);
				}
				catch (Exception ex2)
				{
					if (num > 0)
					{
						if (glsApiName.Equals("SaveDomain", StringComparison.OrdinalIgnoreCase) || glsApiName.Equals("SaveTenant", StringComparison.OrdinalIgnoreCase))
						{
							throw;
						}
						directoryServiceProxyPool = GlsDirectorySession.OfflineServiceProxyPool;
						ex = ex2;
						endpointHostNameForLogging = "localhost";
						ExTraceGlobals.GLSTracer.TraceWarning<string>(0L, "Falling back to offline GLS after getting exception from the pool:{0}", ex2.Message);
					}
					else
					{
						if (ex != null)
						{
							ExTraceGlobals.GLSTracer.TraceError<string>(0L, "Both online and offline GLS failed, rethrowing the original exception:{0}", ex.Message);
							throw ex;
						}
						throw;
					}
				}
			}
			while (num-- > 0);
			glsLoggerContext = loggerContext;
			return response;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003C9B4 File Offset: 0x0003ABB4
		private IAsyncResult BeginExecuteWithRetry(Action<LocatorService, RequestIdentity, GlsAsyncResult> method, AsyncCallback clientCallback, object clientAsyncState, string methodName, string glsApiName, object parameterValue, bool isRead)
		{
			Exception ex = null;
			GlsAsyncResult glsAsyncResult = new GlsAsyncResult(clientCallback, clientAsyncState, null, null);
			GlsCacheServiceMode glsCacheServiceMode = GlsDirectorySession.GlsCacheServiceMode;
			string endpointHostNameForLogging;
			DirectoryServiceProxyPool<LocatorService> directoryServiceProxyPool;
			int num;
			switch (glsCacheServiceMode)
			{
			case GlsCacheServiceMode.CacheDisabled:
				directoryServiceProxyPool = GlsDirectorySession.ServiceProxyPool;
				num = 0;
				glsAsyncResult.IsOfflineGls = false;
				endpointHostNameForLogging = GlsDirectorySession.endpointHostName;
				break;
			case GlsCacheServiceMode.CacheAsExceptionFallback:
				directoryServiceProxyPool = GlsDirectorySession.ServiceProxyPool;
				num = 1;
				glsAsyncResult.IsOfflineGls = false;
				endpointHostNameForLogging = GlsDirectorySession.endpointHostName;
				break;
			default:
				if (glsCacheServiceMode != GlsCacheServiceMode.CacheOnly)
				{
					throw new ArgumentException("GlsCacheServiceMode");
				}
				directoryServiceProxyPool = GlsDirectorySession.OfflineServiceProxyPool;
				num = 0;
				glsAsyncResult.IsOfflineGls = true;
				endpointHostNameForLogging = "localhost";
				break;
			}
			do
			{
				try
				{
					directoryServiceProxyPool.CallServiceWithRetryAsyncBegin(delegate(IPooledServiceProxy<LocatorService> proxy)
					{
						RequestIdentity uniqueRequestIdentity = this.GetUniqueRequestIdentity();
						GlsLoggerContext glsLoggerContext = new GlsLoggerContext(glsApiName, parameterValue, endpointHostNameForLogging, isRead, uniqueRequestIdentity.RequestTrackingGuid);
						glsAsyncResult.LoggerContext = glsLoggerContext;
						glsAsyncResult.PooledProxy = proxy;
						try
						{
							GLSLogger.FaultInjectionTrace();
							method(proxy.Client, uniqueRequestIdentity, glsAsyncResult);
							glsLoggerContext.ConnectionId = proxy.Client.GetHashCode().ToString();
						}
						catch (Exception ex3)
						{
							glsLoggerContext.ConnectionId = proxy.Client.GetHashCode().ToString();
							GLSLogger.LogException(glsLoggerContext, ex3);
							glsAsyncResult.LoggerContext = null;
							glsAsyncResult.PooledProxy = null;
							throw;
						}
					}, methodName, 3);
				}
				catch (Exception ex2)
				{
					if (num > 0)
					{
						directoryServiceProxyPool = GlsDirectorySession.OfflineServiceProxyPool;
						glsAsyncResult.IsOfflineGls = true;
						ex = ex2;
						endpointHostNameForLogging = "localhost";
						ExTraceGlobals.GLSTracer.TraceWarning<string>(0L, "Falling back to offline GLS after getting exception from the pool:{0}", ex2.Message);
					}
					else if (ex != null)
					{
						ExTraceGlobals.GLSTracer.TraceError<string>(0L, "Both online and offline GLS failed, rethrowing the original exception:{0}", ex.Message);
						glsAsyncResult.AsyncException = ex;
						glsAsyncResult.IsCompleted = true;
					}
					else
					{
						glsAsyncResult.AsyncException = ex2;
						glsAsyncResult.IsCompleted = true;
					}
				}
			}
			while (num-- > 0);
			return glsAsyncResult;
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003CC00 File Offset: 0x0003AE00
		private TResult EndExecuteWithRetry<TResult>(GlsAsyncResult glsAsyncResult, string methodName, Func<LocatorService, TResult> method) where TResult : ResponseBase
		{
			TResult response = default(TResult);
			if (!glsAsyncResult.IsCompleted)
			{
				glsAsyncResult.AsyncWaitHandle.WaitOne(GlsDirectorySession.AsyncWaitTimeout);
				if (!glsAsyncResult.IsCompleted)
				{
					Exception ex = new GlsTransientException(DirectoryStrings.AsyncTimeout((int)GlsDirectorySession.AsyncWaitTimeout.TotalSeconds), new TimeoutException());
					GLSLogger.LogException(glsAsyncResult.LoggerContext, ex);
					throw ex;
				}
			}
			glsAsyncResult.CheckExceptionAndEnd();
			DirectoryServiceProxyPool<LocatorService> directoryServiceProxyPool = glsAsyncResult.IsOfflineGls ? GlsDirectorySession.OfflineServiceProxyPool : GlsDirectorySession.ServiceProxyPool;
			directoryServiceProxyPool.CallServiceWithRetryAsyncEnd(glsAsyncResult.PooledProxy, delegate(IPooledServiceProxy<LocatorService> proxy)
			{
				try
				{
					GLSLogger.FaultInjectionTraceForAsync();
					response = method(glsAsyncResult.ServiceProxy);
					string text = GlsDirectorySession.ExtractMachineNameFromDiagnostics(response.Diagnostics);
					if (!string.IsNullOrEmpty(text) && !string.Equals(proxy.Tag, text))
					{
						proxy.Tag = text;
					}
				}
				catch (Exception ex2)
				{
					GLSLogger.LogException(glsAsyncResult.LoggerContext, ex2);
					throw;
				}
			}, methodName);
			return response;
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003CCD8 File Offset: 0x0003AED8
		private RequestIdentity GetUniqueRequestIdentity()
		{
			return new RequestIdentity
			{
				CallerId = this.glsCallerId.CallerIdString,
				TrackingGuid = this.glsCallerId.TrackingGuid,
				RequestTrackingGuid = Guid.NewGuid()
			};
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		private bool IsNotFound(string[] nonExistentNamespaces, Namespace namespaceToCheck)
		{
			if (nonExistentNamespaces == null || nonExistentNamespaces.Length == 0 || namespaceToCheck == Namespace.IgnoreComparison)
			{
				return false;
			}
			foreach (string text in nonExistentNamespaces)
			{
				if (text.Equals(NamespaceUtil.NamespaceToString(namespaceToCheck), StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003CD64 File Offset: 0x0003AF64
		private DeleteUserRequest ConstructDeleteUserRequest(string msaUserNetID)
		{
			GlsDirectorySession.ThrowIfInvalidNetID(msaUserNetID, "msaUserNetID");
			return new DeleteUserRequest
			{
				User = new UserQuery
				{
					UserKey = msaUserNetID
				}
			};
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003CD9C File Offset: 0x0003AF9C
		private FindTenantResult ConstructFindTenantResult(FindTenantResponse response, GlsLoggerContext glsLoggerContext, Namespace namespaceToCheck)
		{
			FindTenantResult result;
			try
			{
				GlsRawResponse glsRawResponse = new GlsRawResponse();
				glsRawResponse.Populate(response.TenantInfo);
				if (response.TenantInfo == null || this.IsNotFound(response.TenantInfo.NoneExistNamespaces, namespaceToCheck))
				{
					ADProviderPerf.IncrementNotFoundCounter();
					GLSLogger.LogResponse(glsLoggerContext, GLSLogger.StatusCode.NotFound, response, glsRawResponse);
					result = null;
				}
				else
				{
					IDictionary<TenantProperty, PropertyValue> properties = (response.TenantInfo != null && response.TenantInfo.Properties != null) ? LocatorServiceClientReader.ConstructTenantPropertyDictionary(response.TenantInfo.Properties) : new Dictionary<TenantProperty, PropertyValue>();
					GLSLogger.LogResponse(glsLoggerContext, GLSLogger.StatusCode.Found, response, glsRawResponse);
					result = new FindTenantResult(properties);
				}
			}
			catch (ArgumentException ex)
			{
				throw new GlsPermanentException(new LocalizedString(ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0003CE4C File Offset: 0x0003B04C
		private FindUserResult ConstructFindUserResult(FindUserResponse response, GlsLoggerContext glsLoggerContext)
		{
			FindUserResult result;
			try
			{
				GlsRawResponse glsRawResponse = new GlsRawResponse();
				glsRawResponse.Populate(response.UserInfo);
				glsRawResponse.Populate(response.TenantInfo);
				if (response.UserInfo == null)
				{
					ADProviderPerf.IncrementNotFoundCounter();
					GLSLogger.LogResponse(glsLoggerContext, GLSLogger.StatusCode.NotFound, response, glsRawResponse);
					result = null;
				}
				else
				{
					string msaUserMemberName = null;
					if (response.UserInfo != null)
					{
						string userKey = response.UserInfo.UserKey;
						msaUserMemberName = response.UserInfo.MSAUserName;
					}
					Guid tenantId = (response.TenantInfo != null) ? response.TenantInfo.TenantId : Guid.Empty;
					IDictionary<TenantProperty, PropertyValue> tenantProperties = (response.TenantInfo != null && response.TenantInfo.Properties != null) ? LocatorServiceClientReader.ConstructTenantPropertyDictionary(response.TenantInfo.Properties) : new Dictionary<TenantProperty, PropertyValue>();
					GLSLogger.LogResponse(glsLoggerContext, GLSLogger.StatusCode.Found, response, glsRawResponse);
					result = new FindUserResult(msaUserMemberName, tenantId, tenantProperties);
				}
			}
			catch (ArgumentException ex)
			{
				throw new GlsPermanentException(new LocalizedString(ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003CF3C File Offset: 0x0003B13C
		private FindDomainResult ConstructFindDomainResult(FindDomainResponse response, GlsLoggerContext glsLoggerContext, Namespace namespaceToCheck)
		{
			return this.ConstructFindDomainResult(response, glsLoggerContext, namespaceToCheck, namespaceToCheck, false);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003CF4C File Offset: 0x0003B14C
		private FindDomainResult ConstructFindDomainResult(FindDomainResponse response, GlsLoggerContext glsLoggerContext, Namespace domainNamespaceToCheck, Namespace tenantNamespaceToCheck, bool skipTenantCheck)
		{
			FindDomainResult result;
			try
			{
				GlsRawResponse glsRawResponse = new GlsRawResponse();
				glsRawResponse.Populate(response.TenantInfo);
				glsRawResponse.Populate(response.DomainInfo);
				if ((!skipTenantCheck && (response.TenantInfo == null || this.IsNotFound(response.TenantInfo.NoneExistNamespaces, tenantNamespaceToCheck))) || response.DomainInfo == null || this.IsNotFound(response.DomainInfo.NoneExistNamespaces, domainNamespaceToCheck))
				{
					ADProviderPerf.IncrementNotFoundCounter();
					GLSLogger.LogResponse(glsLoggerContext, GLSLogger.StatusCode.NotFound, response, glsRawResponse);
					result = null;
				}
				else
				{
					IDictionary<DomainProperty, PropertyValue> domainProperties = (response.DomainInfo != null && response.DomainInfo.Properties != null) ? LocatorServiceClientReader.ConstructDomainPropertyDictionary(response.DomainInfo.Properties) : new Dictionary<DomainProperty, PropertyValue>();
					IDictionary<TenantProperty, PropertyValue> tenantProperties = (response.TenantInfo != null && response.TenantInfo.Properties != null) ? LocatorServiceClientReader.ConstructTenantPropertyDictionary(response.TenantInfo.Properties) : new Dictionary<TenantProperty, PropertyValue>();
					Guid tenantId = (response.TenantInfo != null) ? response.TenantInfo.TenantId : Guid.Empty;
					GLSLogger.LogResponse(glsLoggerContext, GLSLogger.StatusCode.Found, response, glsRawResponse);
					result = new FindDomainResult((response.DomainInfo == null) ? null : response.DomainInfo.DomainName, tenantId, tenantProperties, domainProperties);
				}
			}
			catch (ArgumentException ex)
			{
				throw new GlsPermanentException(new LocalizedString(ex.Message), ex);
			}
			return result;
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003D0A0 File Offset: 0x0003B2A0
		private FindTenantRequest ConstructFindTenantRequest(Guid tenantId, TenantProperty[] tenantProperties)
		{
			return new FindTenantRequest
			{
				ReadFlag = (int)this.glsReadFlag,
				Tenant = new TenantQuery
				{
					TenantId = tenantId,
					PropertyNames = GlsDirectorySession.GetPropertyNames(tenantProperties)
				}
			};
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x0003D0E0 File Offset: 0x0003B2E0
		private FindUserRequest ConstructFindUserRequest(string userNetID, TenantProperty[] tenantProperties)
		{
			GlsDirectorySession.ThrowIfInvalidNetID(userNetID, "userNetID");
			GlsDirectorySession.ThrowIfNull(tenantProperties, "tenantProperties");
			return new FindUserRequest
			{
				ReadFlag = (int)this.glsReadFlag,
				User = new UserQuery
				{
					UserKey = userNetID
				},
				Tenant = new TenantQuery
				{
					PropertyNames = GlsDirectorySession.GetPropertyNames(tenantProperties)
				}
			};
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x0003D144 File Offset: 0x0003B344
		private FindDomainRequest ConstructFindDomainRequest(SmtpDomain domain, DomainProperty[] domainProperties, TenantProperty[] tenantProperties)
		{
			return new FindDomainRequest
			{
				ReadFlag = (int)this.glsReadFlag,
				Domain = new DomainQuery
				{
					DomainName = domain.Domain,
					PropertyNames = GlsDirectorySession.GetPropertyNames(domainProperties)
				},
				Tenant = new TenantQuery
				{
					PropertyNames = GlsDirectorySession.GetPropertyNames(tenantProperties)
				}
			};
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x0003D1AC File Offset: 0x0003B3AC
		private FindDomainsRequest ConstructFindDomainsRequest(IEnumerable<SmtpDomain> domains, DomainProperty[] domainProperties, TenantProperty[] tenantProperties)
		{
			FindDomainsRequest findDomainsRequest = new FindDomainsRequest();
			findDomainsRequest.ReadFlag = (int)this.glsReadFlag;
			findDomainsRequest.DomainsName = (from domain in domains
			select domain.Domain).ToArray<string>();
			findDomainsRequest.DomainPropertyNames = GlsDirectorySession.GetPropertyNames(domainProperties);
			findDomainsRequest.TenantPropertyNames = GlsDirectorySession.GetPropertyNames(tenantProperties);
			return findDomainsRequest;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0003D214 File Offset: 0x0003B414
		private SaveUserRequest ConstructSaveUserRequest(string msaUserNetID, string msaUserMemberName, Guid tenantId)
		{
			GlsDirectorySession.ThrowIfInvalidNetID(msaUserNetID, "msaUserNetID");
			GlsDirectorySession.ThrowIfInvalidSmtpAddress(msaUserMemberName, "msaUserMemberName");
			GlsDirectorySession.ThrowIfEmptyGuid(tenantId, "tenantId");
			return new SaveUserRequest
			{
				UserInfo = new UserInfo
				{
					UserKey = msaUserNetID,
					MSAUserName = msaUserMemberName
				},
				TenantId = tenantId
			};
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x0003D275 File Offset: 0x0003B475
		private static string[] GetPropertyNames(GlsProperty[] properties)
		{
			return (from property in properties
			select property.Name).ToArray<string>();
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x0003D2A0 File Offset: 0x0003B4A0
		private static string ExtractMachineNameFromDiagnostics(string diagnostics)
		{
			if (string.IsNullOrEmpty(diagnostics))
			{
				return string.Empty;
			}
			int num = diagnostics.IndexOf("<Machine>", StringComparison.OrdinalIgnoreCase);
			if (num < 0)
			{
				return string.Empty;
			}
			num += "<Machine>".Length;
			int num2 = diagnostics.IndexOf("</Machine>", num, StringComparison.OrdinalIgnoreCase) - 1;
			if (num2 < 0 || num2 <= num)
			{
				return string.Empty;
			}
			return diagnostics.Substring(num, num2 - num + 1);
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x0003D309 File Offset: 0x0003B509
		private bool IsDataReturnedFromOfflineService(GlsLoggerContext loggerContext)
		{
			return string.Equals(loggerContext.ResolveEndpointToIpAddress(false), "localhost", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x040006CC RID: 1740
		private const int MaxRetries = 3;

		// Token: 0x040006CD RID: 1741
		private const string GlsEndpoint = "GlobalLocatorService";

		// Token: 0x040006CE RID: 1742
		private const string GlsCacheEndpoint = "GlsCacheService";

		// Token: 0x040006CF RID: 1743
		private const string offlineGlsUrl = "net.pipe://localhost/GlsCacheService/service.svc";

		// Token: 0x040006D0 RID: 1744
		private const string MachineTagStart = "<Machine>";

		// Token: 0x040006D1 RID: 1745
		private const string MachineTagEnd = "</Machine>";

		// Token: 0x040006D2 RID: 1746
		private const int MaxNumberOfClientProxies = 150;

		// Token: 0x040006D3 RID: 1747
		private static readonly TimeSpan AsyncWaitTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x040006D4 RID: 1748
		private static readonly TimeSpan ADConfigurationSettingsRefreshPeriod = TimeSpan.FromMinutes(15.0);

		// Token: 0x040006D5 RID: 1749
		private static readonly List<TimeSpan> SendTimeouts = new List<TimeSpan>
		{
			TimeSpan.FromSeconds(15.0),
			TimeSpan.FromSeconds(7.0),
			TimeSpan.FromSeconds(3.0)
		};

		// Token: 0x040006D6 RID: 1750
		private static readonly TenantProperty[] accountResourceForestProperty = new TenantProperty[]
		{
			TenantProperty.EXOAccountForest,
			TenantProperty.EXOResourceForest,
			TenantProperty.EXOSmtpNextHopDomain,
			TenantProperty.EXOTenantContainerCN
		};

		// Token: 0x040006D7 RID: 1751
		private static readonly TenantProperty[] resourceForestProperty = new TenantProperty[]
		{
			TenantProperty.EXOResourceForest
		};

		// Token: 0x040006D8 RID: 1752
		private static readonly TenantProperty[] customerTypeProperty = new TenantProperty[]
		{
			TenantProperty.CustomerType
		};

		// Token: 0x040006D9 RID: 1753
		private static readonly TenantProperty[] customerAttributionTenantProperties = new TenantProperty[]
		{
			TenantProperty.CustomerType,
			TenantProperty.Region,
			TenantProperty.EXOSmtpNextHopDomain,
			TenantProperty.EXOAccountForest,
			TenantProperty.EXOTenantContainerCN,
			TenantProperty.EXOResourceForest
		};

		// Token: 0x040006DA RID: 1754
		private static readonly DomainProperty[] customerAttributionDomainProperties = new DomainProperty[]
		{
			DomainProperty.Region,
			DomainProperty.ServiceVersion,
			DomainProperty.IPv6
		};

		// Token: 0x040006DB RID: 1755
		private static readonly DomainProperty[] exoDomainFlagsProperty = new DomainProperty[]
		{
			DomainProperty.ExoFlags
		};

		// Token: 0x040006DC RID: 1756
		private static readonly DomainProperty[] exoDomainInUseProperty = new DomainProperty[]
		{
			DomainProperty.ExoDomainInUse
		};

		// Token: 0x040006DD RID: 1757
		private static readonly DomainProperty[] ffoExoDomainProperties = new DomainProperty[]
		{
			DomainProperty.Region,
			DomainProperty.ExoFlags
		};

		// Token: 0x040006DE RID: 1758
		private static readonly TenantProperty[] ffoExoTenantProperties = new TenantProperty[]
		{
			TenantProperty.EXOResourceForest,
			TenantProperty.CustomerType
		};

		// Token: 0x040006DF RID: 1759
		private static readonly TenantProperty[] exoTenantFlagsProperty = new TenantProperty[]
		{
			TenantProperty.EXOTenantFlags
		};

		// Token: 0x040006E0 RID: 1760
		private static readonly TenantProperty[] exoNextHopProperty = new TenantProperty[]
		{
			TenantProperty.EXOSmtpNextHopDomain
		};

		// Token: 0x040006E1 RID: 1761
		private static readonly DomainProperty[] ffoDomainProperties = new DomainProperty[]
		{
			DomainProperty.Region,
			DomainProperty.ServiceVersion,
			DomainProperty.IPv6
		};

		// Token: 0x040006E2 RID: 1762
		private static readonly TenantProperty[] ffoTenantProperties = new TenantProperty[]
		{
			TenantProperty.Version,
			TenantProperty.CustomerType,
			TenantProperty.Region
		};

		// Token: 0x040006E3 RID: 1763
		private static readonly TenantProperty[] ffoTenantRegionProperty = new TenantProperty[]
		{
			TenantProperty.Region
		};

		// Token: 0x040006E4 RID: 1764
		private static readonly DomainProperty[] noDomainProperties = GlsDirectorySession.exoDomainFlagsProperty;

		// Token: 0x040006E5 RID: 1765
		private static readonly TenantProperty[] noTenantProperties = GlsDirectorySession.resourceForestProperty;

		// Token: 0x040006E6 RID: 1766
		private static DirectoryServiceProxyPool<LocatorService> serviceProxyPool;

		// Token: 0x040006E7 RID: 1767
		private static DirectoryServiceProxyPool<LocatorService> offlineServiceProxyPool;

		// Token: 0x040006E8 RID: 1768
		private static GlsOverrideCollection glsTenantOverrides = new GlsOverrideCollection(null);

		// Token: 0x040006E9 RID: 1769
		private static DateTime glsTenantOverridesNextRefresh = DateTime.MinValue;

		// Token: 0x040006EA RID: 1770
		private static object proxyPoolLockObj = new object();

		// Token: 0x040006EB RID: 1771
		private readonly GlsCallerId glsCallerId;

		// Token: 0x040006EC RID: 1772
		private static string endpointHostName;

		// Token: 0x040006ED RID: 1773
		private readonly GlsAPIReadFlag glsReadFlag;

		// Token: 0x040006EE RID: 1774
		internal static readonly DomainProperty[] AllExoDomainProperties = new DomainProperty[]
		{
			DomainProperty.ExoDomainInUse,
			DomainProperty.ExoFlags
		};

		// Token: 0x040006EF RID: 1775
		internal static readonly TenantProperty[] AllExoTenantProperties = new TenantProperty[]
		{
			TenantProperty.EXOAccountForest,
			TenantProperty.EXOResourceForest,
			TenantProperty.EXOPrimarySite,
			TenantProperty.EXOSmtpNextHopDomain,
			TenantProperty.EXOTenantFlags,
			TenantProperty.EXOTenantContainerCN,
			TenantProperty.GlobalResumeCache
		};
	}
}
