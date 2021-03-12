using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Data.Storage.RightsManagement
{
	// Token: 0x02000B3B RID: 2875
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RmsConfiguration
	{
		// Token: 0x060067EA RID: 26602 RVA: 0x001B77E7 File Offset: 0x001B59E7
		private RmsConfiguration()
		{
		}

		// Token: 0x17001C95 RID: 7317
		// (get) Token: 0x060067EB RID: 26603 RVA: 0x001B77EF File Offset: 0x001B59EF
		internal static RmsConfiguration Instance
		{
			get
			{
				return RmsConfiguration.instance;
			}
		}

		// Token: 0x060067EC RID: 26604 RVA: 0x001B77F8 File Offset: 0x001B59F8
		public void Load(bool enableOrgAndTemplateCache)
		{
			if (this.enabled)
			{
				return;
			}
			lock (this)
			{
				if (!this.enabled)
				{
					this.irmConfigCache = new TenantConfigurationCache<RmsConfiguration.PerTenantIRMConfiguration>(RmsConfiguration.CacheSizeInBytes, RmsConfiguration.CacheTimeout, RmsConfiguration.CacheTimeout, null, null);
					this.enableOrgAndTemplateCache = enableOrgAndTemplateCache;
					this.transportConfigCache = new TenantConfigurationCache<RmsConfiguration.PerTenantTransportSettings>(RmsConfiguration.CacheSizeInBytes, RmsConfiguration.CacheTimeout, RmsConfiguration.CacheTimeout, null, null);
					if (this.enableOrgAndTemplateCache)
					{
						this.irmOrgCache = new TenantConfigurationCache<RmsConfiguration.PerTenantOrganizationConfig>(RmsConfiguration.CacheSizeInBytes, RmsConfiguration.CacheTimeout, RmsConfiguration.CacheTimeout, null, null);
						this.templateCache = new TenantConfigurationCache<RmsConfiguration.PerTenantTemplateInfo>(RmsClientManager.AppSettings.TemplateCacheSizeInBytes, RmsClientManager.AppSettings.TemplateCacheExpirationInterval, TimeSpan.Zero, null, null);
					}
					else
					{
						this.firstOrgCache = new RmsConfiguration.FirstOrgServiceLocationsCache();
					}
					this.enabled = true;
				}
			}
		}

		// Token: 0x060067ED RID: 26605 RVA: 0x001B78E0 File Offset: 0x001B5AE0
		public bool GetTenantExternalDirectoryOrgId(OrganizationId orgId, out Guid externalDirectoryOrgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			externalDirectoryOrgId = Guid.Empty;
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				return false;
			}
			RmsConfiguration.PerTenantOrganizationConfig tenantOrganizationConfig = this.GetTenantOrganizationConfig(orgId);
			externalDirectoryOrgId = tenantOrganizationConfig.ExternalDirectoryOrgId;
			return true;
		}

		// Token: 0x060067EE RID: 26606 RVA: 0x001B7928 File Offset: 0x001B5B28
		public Uri GetTenantServiceLocation(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				return this.GetFirstOrgServiceLocation(ServiceType.Certification);
			}
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			if (tenantIrmConfig.ServiceLocation != null)
			{
				return tenantIrmConfig.ServiceLocation;
			}
			return null;
		}

		// Token: 0x060067EF RID: 26607 RVA: 0x001B7974 File Offset: 0x001B5B74
		public Uri GetTenantPublishingLocation(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				return this.GetFirstOrgServiceLocation(ServiceType.Publishing);
			}
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			if (tenantIrmConfig.PublishingLocation != null)
			{
				return tenantIrmConfig.PublishingLocation;
			}
			if (tenantIrmConfig.ServiceLocation != null)
			{
				return tenantIrmConfig.ServiceLocation;
			}
			return null;
		}

		// Token: 0x060067F0 RID: 26608 RVA: 0x001B79D4 File Offset: 0x001B5BD4
		public List<Uri> GetTenantLicensingLocations(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.LicensingLocations;
		}

		// Token: 0x060067F1 RID: 26609 RVA: 0x001B79FC File Offset: 0x001B5BFC
		public TransportDecryptionSetting GetTenantTransportDecryptionSetting(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.TransportDecryptionSetting;
		}

		// Token: 0x060067F2 RID: 26610 RVA: 0x001B7A24 File Offset: 0x001B5C24
		public bool IsJournalReportDecryptionEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.JournalReportDecryptionEnabled;
		}

		// Token: 0x060067F3 RID: 26611 RVA: 0x001B7A4C File Offset: 0x001B5C4C
		public bool IsExternalLicensingEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.ExternalLicensingEnabled;
		}

		// Token: 0x060067F4 RID: 26612 RVA: 0x001B7A74 File Offset: 0x001B5C74
		public bool IsInternalLicensingEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.InternalLicensingEnabled;
		}

		// Token: 0x060067F5 RID: 26613 RVA: 0x001B7A9C File Offset: 0x001B5C9C
		public bool IsSearchEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.SearchEnabled;
		}

		// Token: 0x060067F6 RID: 26614 RVA: 0x001B7AC4 File Offset: 0x001B5CC4
		public bool IsClientAccessServerEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.ClientAccessServerEnabled;
		}

		// Token: 0x060067F7 RID: 26615 RVA: 0x001B7AEC File Offset: 0x001B5CEC
		public bool IsInternalServerPreLicensingEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return (tenantIrmConfig.SearchEnabled || tenantIrmConfig.ClientAccessServerEnabled) && tenantIrmConfig.InternalLicensingEnabled;
		}

		// Token: 0x060067F8 RID: 26616 RVA: 0x001B7B24 File Offset: 0x001B5D24
		public bool IsExternalServerPreLicensingEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return (tenantIrmConfig.SearchEnabled || tenantIrmConfig.ClientAccessServerEnabled) && tenantIrmConfig.ExternalLicensingEnabled;
		}

		// Token: 0x060067F9 RID: 26617 RVA: 0x001B7B5C File Offset: 0x001B5D5C
		public bool IsInternetConfidentialEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.InternetConfidentialEnabled;
		}

		// Token: 0x060067FA RID: 26618 RVA: 0x001B7B84 File Offset: 0x001B5D84
		public bool IsEDiscoverySuperUserEnabledForTenant(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.EDiscoverySuperUserEnabled;
		}

		// Token: 0x060067FB RID: 26619 RVA: 0x001B7BAC File Offset: 0x001B5DAC
		public Uri GetTenantRMSOnlineKeySharingLocation(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				throw new InvalidOperationException("RMSOnlineKeySharingLocation is a datacenter-only property");
			}
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.RMSOnlineKeySharingLocation;
		}

		// Token: 0x060067FC RID: 26620 RVA: 0x001B7BEC File Offset: 0x001B5DEC
		public byte GetTenantServerCertificatesVersion(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration tenantIrmConfig = this.GetTenantIrmConfig(orgId);
			return tenantIrmConfig.ServerCertificatesVersion;
		}

		// Token: 0x060067FD RID: 26621 RVA: 0x001B7C14 File Offset: 0x001B5E14
		public string GetTenantFederatedMailbox(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			string result;
			try
			{
				RmsConfiguration.PerTenantTransportSettings value;
				if (RmsClientManager.ADSession != null)
				{
					value = this.transportConfigCache.GetValue(RmsClientManager.ADSession);
				}
				else
				{
					value = this.transportConfigCache.GetValue(orgId);
				}
				result = (string)value.OrgFederatedMailbox;
			}
			catch (ADTransientException innerException)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, innerException);
			}
			catch (ADOperationException innerException2)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, innerException2);
			}
			return result;
		}

		// Token: 0x060067FE RID: 26622 RVA: 0x001B7C98 File Offset: 0x001B5E98
		public bool AreRmsTemplatesInCache(OrganizationId organizationId)
		{
			return this.templateCache != null && this.templateCache.ContainsInCache(organizationId);
		}

		// Token: 0x060067FF RID: 26623 RVA: 0x001B7CB0 File Offset: 0x001B5EB0
		internal IEnumerable<RmsTemplate> GetRmsTemplates(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				throw new InvalidOperationException("GetRmsTemplates called with ForestWideOrgId");
			}
			if (!this.enableOrgAndTemplateCache)
			{
				throw new NotSupportedException("This function is not supported when offlineRms is not enabled");
			}
			RmsConfiguration.PerTenantTemplateInfo tenantTemplateConfig = this.GetTenantTemplateConfig(orgId);
			return tenantTemplateConfig.Templates;
		}

		// Token: 0x06006800 RID: 26624 RVA: 0x001B7D04 File Offset: 0x001B5F04
		internal RmsTemplate GetRmsTemplate(OrganizationId orgId, Guid templateId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			if (!this.enableOrgAndTemplateCache)
			{
				throw new NotSupportedException("This function is only supported in datacenter where offlineRms is enabled");
			}
			if (orgId == OrganizationId.ForestWideOrgId)
			{
				throw new InvalidOperationException("GetRmsTemplate called with ForestWideOrgId");
			}
			if (templateId == RmsTemplate.DoNotForward.Id)
			{
				return RmsTemplate.DoNotForward;
			}
			if (templateId == RmsTemplate.InternetConfidential.Id)
			{
				return RmsTemplate.InternetConfidential;
			}
			RmsConfiguration.PerTenantTemplateInfo tenantTemplateConfig = this.GetTenantTemplateConfig(orgId);
			RmsTemplate result;
			if (!tenantTemplateConfig.TryGetValue(templateId, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06006801 RID: 26625 RVA: 0x001B7D90 File Offset: 0x001B5F90
		private RmsConfiguration.PerTenantOrganizationConfig GetTenantOrganizationConfig(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantOrganizationConfig value;
			try
			{
				value = this.irmOrgCache.GetValue(orgId);
			}
			catch (ADTransientException innerException)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, innerException);
			}
			catch (ADOperationException innerException2)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, innerException2);
			}
			return value;
		}

		// Token: 0x06006802 RID: 26626 RVA: 0x001B7DF0 File Offset: 0x001B5FF0
		private RmsConfiguration.PerTenantIRMConfiguration GetTenantIrmConfig(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantIRMConfiguration result;
			try
			{
				RmsConfiguration.PerTenantIRMConfiguration value;
				if (RmsClientManager.ADSession != null)
				{
					value = this.irmConfigCache.GetValue(RmsClientManager.ADSession);
				}
				else
				{
					value = this.irmConfigCache.GetValue(orgId);
				}
				result = value;
			}
			catch (ADTransientException innerException)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, innerException);
			}
			catch (ADOperationException innerException2)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, innerException2);
			}
			return result;
		}

		// Token: 0x06006803 RID: 26627 RVA: 0x001B7E6C File Offset: 0x001B606C
		private RmsConfiguration.PerTenantTemplateInfo GetTenantTemplateConfig(OrganizationId orgId)
		{
			ArgumentValidator.ThrowIfNull("orgId", orgId);
			RmsConfiguration.PerTenantTemplateInfo value;
			try
			{
				value = this.templateCache.GetValue(orgId);
			}
			catch (ADTransientException innerException)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, innerException);
			}
			catch (ADOperationException innerException2)
			{
				throw new ExchangeConfigurationException(ServerStrings.FailedToReadConfiguration, innerException2);
			}
			return value;
		}

		// Token: 0x06006804 RID: 26628 RVA: 0x001B7ECC File Offset: 0x001B60CC
		private Uri GetFirstOrgServiceLocation(ServiceType serviceType)
		{
			if (this.enableOrgAndTemplateCache)
			{
				return null;
			}
			RmsConfiguration.FirstOrgServiceLocationsCache.FirstOrgServiceLocations firstOrgServiceLocations = this.firstOrgCache.GetFirstOrgServiceLocations();
			if (firstOrgServiceLocations == null)
			{
				return null;
			}
			switch (serviceType)
			{
			case ServiceType.Certification:
				return firstOrgServiceLocations.CertificationLocation;
			case ServiceType.Activation | ServiceType.Certification:
				goto IL_45;
			case ServiceType.Publishing:
				break;
			default:
				if (serviceType != ServiceType.ClientLicensor)
				{
					goto IL_45;
				}
				break;
			}
			return firstOrgServiceLocations.PublishingLocaton;
			IL_45:
			return null;
		}

		// Token: 0x04003B19 RID: 15129
		private static readonly Trace Tracer = ExTraceGlobals.RightsManagementTracer;

		// Token: 0x04003B1A RID: 15130
		private static readonly TimeSpan CacheTimeout = TimeSpan.FromHours(8.0);

		// Token: 0x04003B1B RID: 15131
		private static readonly long CacheSizeInBytes = (long)ByteQuantifiedSize.FromMB(1UL).ToBytes();

		// Token: 0x04003B1C RID: 15132
		private static readonly RmsConfiguration instance = new RmsConfiguration();

		// Token: 0x04003B1D RID: 15133
		private TenantConfigurationCache<RmsConfiguration.PerTenantIRMConfiguration> irmConfigCache;

		// Token: 0x04003B1E RID: 15134
		private TenantConfigurationCache<RmsConfiguration.PerTenantOrganizationConfig> irmOrgCache;

		// Token: 0x04003B1F RID: 15135
		private TenantConfigurationCache<RmsConfiguration.PerTenantTransportSettings> transportConfigCache;

		// Token: 0x04003B20 RID: 15136
		private TenantConfigurationCache<RmsConfiguration.PerTenantTemplateInfo> templateCache;

		// Token: 0x04003B21 RID: 15137
		private bool enabled;

		// Token: 0x04003B22 RID: 15138
		private RmsConfiguration.FirstOrgServiceLocationsCache firstOrgCache;

		// Token: 0x04003B23 RID: 15139
		private bool enableOrgAndTemplateCache;

		// Token: 0x02000B3C RID: 2876
		private sealed class FirstOrgServiceLocationsCache
		{
			// Token: 0x06006806 RID: 26630 RVA: 0x001B7F68 File Offset: 0x001B6168
			public FirstOrgServiceLocationsCache()
			{
				this.objHashCode = this.GetHashCode();
				this.Refresh(true);
			}

			// Token: 0x06006807 RID: 26631 RVA: 0x001B7F8E File Offset: 0x001B618E
			public RmsConfiguration.FirstOrgServiceLocationsCache.FirstOrgServiceLocations GetFirstOrgServiceLocations()
			{
				this.Refresh(false);
				return this.firstOrgServiceLocations;
			}

			// Token: 0x06006808 RID: 26632 RVA: 0x001B7FA0 File Offset: 0x001B61A0
			private void Refresh(bool throwExceptions)
			{
				if (DateTime.UtcNow < this.timeout)
				{
					return;
				}
				RmsConfiguration.Tracer.TraceDebug<bool>((long)this.objHashCode, "Refreshing the first org service locations cache. Initializing for the first time: {0}", throwExceptions);
				lock (this)
				{
					if (DateTime.UtcNow < this.timeout)
					{
						return;
					}
					RmsConfiguration.FirstOrgServiceLocationsCache.FirstOrgServiceLocations firstOrgServiceLocations = new RmsConfiguration.FirstOrgServiceLocationsCache.FirstOrgServiceLocations();
					try
					{
						firstOrgServiceLocations.CertificationLocation = DrmClientUtils.GetServiceLocation(SafeRightsManagementSessionHandle.InvalidHandle, ServiceType.Certification, ServiceLocation.Enterprise, null);
						firstOrgServiceLocations.PublishingLocaton = DrmClientUtils.GetServiceLocation(SafeRightsManagementSessionHandle.InvalidHandle, ServiceType.ClientLicensor, ServiceLocation.Enterprise, null);
						this.firstOrgServiceLocations = firstOrgServiceLocations;
					}
					catch (RightsManagementException ex)
					{
						RmsConfiguration.Tracer.TraceError<RightsManagementException>((long)this.objHashCode, "Failed to get the service locations. Error {0}", ex);
						if (ex.FailureCode != RightsManagementFailureCode.ServiceNotFound && throwExceptions)
						{
							throw;
						}
						RmsConfiguration.Tracer.TraceError((long)this.objHashCode, "Failed to refresh the first org cache");
					}
					if (this.firstOrgServiceLocations != null)
					{
						this.timeout = DateTime.UtcNow.Add(RmsConfiguration.FirstOrgServiceLocationsCache.ExpirationTime);
					}
				}
				RmsConfiguration.Tracer.TraceDebug<Uri, Uri, DateTime>((long)this.objHashCode, "Values for the first org cache. CertificationLocation: {0}, PublishingLocation: {1}, ExpirationTime: {2}", (this.firstOrgServiceLocations != null) ? this.firstOrgServiceLocations.CertificationLocation : null, (this.firstOrgServiceLocations != null) ? this.firstOrgServiceLocations.PublishingLocaton : null, this.timeout);
			}

			// Token: 0x04003B24 RID: 15140
			private static readonly TimeSpan ExpirationTime = TimeSpan.FromMinutes(15.0);

			// Token: 0x04003B25 RID: 15141
			private readonly int objHashCode;

			// Token: 0x04003B26 RID: 15142
			private DateTime timeout = DateTime.MinValue;

			// Token: 0x04003B27 RID: 15143
			private RmsConfiguration.FirstOrgServiceLocationsCache.FirstOrgServiceLocations firstOrgServiceLocations;

			// Token: 0x02000B3D RID: 2877
			public class FirstOrgServiceLocations
			{
				// Token: 0x04003B28 RID: 15144
				public Uri CertificationLocation;

				// Token: 0x04003B29 RID: 15145
				public Uri PublishingLocaton;
			}
		}

		// Token: 0x02000B3E RID: 2878
		private sealed class PerTenantIRMConfiguration : TenantConfigurationCacheableItem<IRMConfiguration>
		{
			// Token: 0x17001C96 RID: 7318
			// (get) Token: 0x0600680B RID: 26635 RVA: 0x001B8121 File Offset: 0x001B6321
			public override long ItemSize
			{
				get
				{
					return (long)this.estimatedSize;
				}
			}

			// Token: 0x0600680C RID: 26636 RVA: 0x001B812C File Offset: 0x001B632C
			public override void ReadData(IConfigurationSession session)
			{
				IRMConfiguration irmconfiguration = IRMConfiguration.Read(session);
				this.JournalReportDecryptionEnabled = irmconfiguration.JournalReportDecryptionEnabled;
				this.ClientAccessServerEnabled = irmconfiguration.ClientAccessServerEnabled;
				this.SearchEnabled = irmconfiguration.SearchEnabled;
				this.ExternalLicensingEnabled = irmconfiguration.ExternalLicensingEnabled;
				this.InternalLicensingEnabled = irmconfiguration.InternalLicensingEnabled;
				this.TransportDecryptionSetting = irmconfiguration.TransportDecryptionSetting;
				this.InternetConfidentialEnabled = irmconfiguration.InternetConfidentialEnabled;
				this.EDiscoverySuperUserEnabled = irmconfiguration.EDiscoverySuperUserEnabled;
				this.ServerCertificatesVersion = (byte)irmconfiguration.ServerCertificatesVersion;
				this.estimatedSize += 10;
				if (irmconfiguration.ServiceLocation != null)
				{
					this.ServiceLocation = irmconfiguration.ServiceLocation;
					this.estimatedSize += irmconfiguration.ServiceLocation.OriginalString.Length * 2;
				}
				if (irmconfiguration.PublishingLocation != null)
				{
					this.PublishingLocation = irmconfiguration.PublishingLocation;
					this.estimatedSize += irmconfiguration.PublishingLocation.OriginalString.Length * 2;
				}
				if (irmconfiguration.LicensingLocation != null && !MultiValuedPropertyBase.IsNullOrEmpty(irmconfiguration.LicensingLocation))
				{
					this.LicensingLocations = new List<Uri>(irmconfiguration.LicensingLocation.Count + 1);
					foreach (Uri uri in irmconfiguration.LicensingLocation)
					{
						if (uri != null)
						{
							this.LicensingLocations.Add(uri);
							this.estimatedSize += uri.OriginalString.Length * 2;
						}
					}
				}
				if (irmconfiguration.RMSOnlineKeySharingLocation != null)
				{
					this.RMSOnlineKeySharingLocation = irmconfiguration.RMSOnlineKeySharingLocation;
					this.estimatedSize += irmconfiguration.RMSOnlineKeySharingLocation.OriginalString.Length * 2;
				}
				if (base.OrganizationId == OrganizationId.ForestWideOrgId)
				{
					Uri firstOrgServiceLocation = RmsConfiguration.Instance.GetFirstOrgServiceLocation(ServiceType.ClientLicensor);
					if (firstOrgServiceLocation != null && !this.LicensingLocations.Contains(firstOrgServiceLocation))
					{
						this.LicensingLocations.Add(firstOrgServiceLocation);
						this.estimatedSize += firstOrgServiceLocation.OriginalString.Length * 2;
					}
				}
				else if (irmconfiguration.PublishingLocation != null && !this.LicensingLocations.Contains(irmconfiguration.PublishingLocation))
				{
					this.LicensingLocations.Add(irmconfiguration.PublishingLocation);
					this.estimatedSize += irmconfiguration.PublishingLocation.OriginalString.Length * 2;
				}
				else if (irmconfiguration.ServiceLocation != null && !this.LicensingLocations.Contains(irmconfiguration.ServiceLocation))
				{
					this.LicensingLocations.Add(irmconfiguration.ServiceLocation);
					this.estimatedSize += irmconfiguration.ServiceLocation.OriginalString.Length * 2;
				}
				if (base.OrganizationId == OrganizationId.ForestWideOrgId && (!this.InternalLicensingEnabled || RmsClientManager.FirstOrgTemplateCacheVersion != this.ServerCertificatesVersion))
				{
					RmsClientManager.TemplateCacheForFirstOrg = null;
				}
			}

			// Token: 0x04003B2A RID: 15146
			public Uri ServiceLocation;

			// Token: 0x04003B2B RID: 15147
			public Uri PublishingLocation;

			// Token: 0x04003B2C RID: 15148
			public List<Uri> LicensingLocations = new List<Uri>(1);

			// Token: 0x04003B2D RID: 15149
			public bool JournalReportDecryptionEnabled;

			// Token: 0x04003B2E RID: 15150
			public bool ExternalLicensingEnabled;

			// Token: 0x04003B2F RID: 15151
			public bool InternalLicensingEnabled;

			// Token: 0x04003B30 RID: 15152
			public bool SearchEnabled;

			// Token: 0x04003B31 RID: 15153
			public bool ClientAccessServerEnabled;

			// Token: 0x04003B32 RID: 15154
			public TransportDecryptionSetting TransportDecryptionSetting;

			// Token: 0x04003B33 RID: 15155
			public bool InternetConfidentialEnabled;

			// Token: 0x04003B34 RID: 15156
			public bool EDiscoverySuperUserEnabled;

			// Token: 0x04003B35 RID: 15157
			public Uri RMSOnlineKeySharingLocation;

			// Token: 0x04003B36 RID: 15158
			public byte ServerCertificatesVersion;

			// Token: 0x04003B37 RID: 15159
			private int estimatedSize;
		}

		// Token: 0x02000B3F RID: 2879
		private sealed class PerTenantOrganizationConfig : TenantConfigurationCacheableItem<ExchangeConfigurationUnit>
		{
			// Token: 0x17001C97 RID: 7319
			// (get) Token: 0x0600680E RID: 26638 RVA: 0x001B8450 File Offset: 0x001B6650
			public override long ItemSize
			{
				get
				{
					return 16L;
				}
			}

			// Token: 0x17001C98 RID: 7320
			// (get) Token: 0x0600680F RID: 26639 RVA: 0x001B8455 File Offset: 0x001B6655
			public Guid ExternalDirectoryOrgId
			{
				get
				{
					return this.externalDirectoryOrgId;
				}
			}

			// Token: 0x06006810 RID: 26640 RVA: 0x001B8460 File Offset: 0x001B6660
			public override void ReadData(IConfigurationSession session)
			{
				ExchangeConfigurationUnit[] array = session.Find<ExchangeConfigurationUnit>(null, QueryScope.SubTree, null, null, 0);
				if (array == null || array.Length == 0)
				{
					return;
				}
				ExchangeConfigurationUnit exchangeConfigurationUnit = array.FirstOrDefault<ExchangeConfigurationUnit>();
				if (exchangeConfigurationUnit != null)
				{
					this.externalDirectoryOrgId = Guid.Parse(exchangeConfigurationUnit.ExternalDirectoryOrganizationId);
				}
			}

			// Token: 0x04003B38 RID: 15160
			private Guid externalDirectoryOrgId;
		}

		// Token: 0x02000B40 RID: 2880
		private sealed class PerTenantTransportSettings : TenantConfigurationCacheableItem<TransportConfigContainer>
		{
			// Token: 0x17001C99 RID: 7321
			// (get) Token: 0x06006812 RID: 26642 RVA: 0x001B84A5 File Offset: 0x001B66A5
			public override long ItemSize
			{
				get
				{
					return (long)this.orgFederatedMailbox.Length;
				}
			}

			// Token: 0x17001C9A RID: 7322
			// (get) Token: 0x06006813 RID: 26643 RVA: 0x001B84B3 File Offset: 0x001B66B3
			public SmtpAddress OrgFederatedMailbox
			{
				get
				{
					return this.orgFederatedMailbox;
				}
			}

			// Token: 0x06006814 RID: 26644 RVA: 0x001B84BC File Offset: 0x001B66BC
			public override void ReadData(IConfigurationSession session)
			{
				TransportConfigContainer[] array = session.Find<TransportConfigContainer>(null, QueryScope.SubTree, null, null, 0);
				if (array == null || array.Length == 0)
				{
					return;
				}
				this.orgFederatedMailbox = array[0].OrganizationFederatedMailbox;
			}

			// Token: 0x04003B39 RID: 15161
			private SmtpAddress orgFederatedMailbox;
		}

		// Token: 0x02000B41 RID: 2881
		private sealed class PerTenantTemplateInfo : TenantConfigurationCacheableItem<RMSTrustedPublishingDomain>
		{
			// Token: 0x17001C9B RID: 7323
			// (get) Token: 0x06006816 RID: 26646 RVA: 0x001B84F4 File Offset: 0x001B66F4
			public override long ItemSize
			{
				get
				{
					return this.estimatedSize;
				}
			}

			// Token: 0x17001C9C RID: 7324
			// (get) Token: 0x06006817 RID: 26647 RVA: 0x001B84FC File Offset: 0x001B66FC
			public IEnumerable<RmsTemplate> Templates
			{
				get
				{
					return this.templates.Values;
				}
			}

			// Token: 0x06006818 RID: 26648 RVA: 0x001B8509 File Offset: 0x001B6709
			public bool TryGetValue(Guid templateId, out RmsTemplate template)
			{
				return this.templates.TryGetValue(templateId, out template);
			}

			// Token: 0x06006819 RID: 26649 RVA: 0x001B8518 File Offset: 0x001B6718
			public override void ReadData(IConfigurationSession session)
			{
				RMSTrustedPublishingDomain[] array = session.Find<RMSTrustedPublishingDomain>(null, QueryScope.SubTree, null, null, 0);
				if (array == null || array.Length == 0)
				{
					return;
				}
				foreach (RMSTrustedPublishingDomain rmstrustedPublishingDomain in array)
				{
					if (rmstrustedPublishingDomain.Default && !MultiValuedPropertyBase.IsNullOrEmpty(rmstrustedPublishingDomain.RMSTemplates))
					{
						foreach (string encodedTemplate in rmstrustedPublishingDomain.RMSTemplates)
						{
							string text = null;
							try
							{
								RmsTemplateType rmsTemplateType;
								text = RMUtil.DecompressTemplate(encodedTemplate, out rmsTemplateType);
								if (rmsTemplateType == RmsTemplateType.Distributed)
								{
									RmsTemplate rmsTemplate = RmsTemplate.CreateServerTemplateFromTemplateDefinition(text, rmsTemplateType);
									this.templates.Add(rmsTemplate.Id, rmsTemplate);
									this.estimatedSize += rmsTemplate.ItemSize + 16L;
								}
							}
							catch (FormatException arg)
							{
								RmsConfiguration.Tracer.TraceError<string, FormatException>((long)this.GetHashCode(), "Failed to read template {0}. Error {1}", text, arg);
							}
							catch (InvalidRpmsgFormatException arg2)
							{
								RmsConfiguration.Tracer.TraceError<string, InvalidRpmsgFormatException>((long)this.GetHashCode(), "Failed to read template {0}. Error {1}", text, arg2);
							}
							catch (RightsManagementException arg3)
							{
								RmsConfiguration.Tracer.TraceError<string, RightsManagementException>((long)this.GetHashCode(), "Failed to read template {0}. Error {1}", text, arg3);
							}
						}
					}
				}
			}

			// Token: 0x04003B3A RID: 15162
			private readonly Dictionary<Guid, RmsTemplate> templates = new Dictionary<Guid, RmsTemplate>();

			// Token: 0x04003B3B RID: 15163
			private long estimatedSize;
		}
	}
}
