using System;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.RightsManagementServices.Provider;

namespace Microsoft.Exchange.Data.Storage.OfflineRms
{
	// Token: 0x02000ABD RID: 2749
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ContextProvider : IContextProvider, IDisposable
	{
		// Token: 0x06006428 RID: 25640 RVA: 0x001A817C File Offset: 0x001A637C
		public ContextProvider(IPerTenantRMSTrustedPublishingDomainConfiguration perTenantconfig)
		{
			if (perTenantconfig == null)
			{
				throw new ArgumentNullException("perTenantconfig");
			}
			this.configProvider = new ConfigurationInformationProvider(perTenantconfig);
			this.privateKeyProvider = new TrustedPublishingDomainPrivateKeyProvider(null, perTenantconfig.PrivateKeys);
			this.globalConfigurationProvider = new GlobalConfigurationCacheProvider();
			this.directoryServiceProvider = new DirectoryServiceProvider();
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x001A81D4 File Offset: 0x001A63D4
		public ContextProvider(RmsClientManagerContext clientContext, PerTenantRMSTrustedPublishingDomainConfiguration perTenantconfig)
		{
			if (clientContext == null)
			{
				throw new ArgumentNullException("clientContext");
			}
			if (perTenantconfig == null)
			{
				throw new ArgumentNullException("perTenantconfig");
			}
			if (perTenantconfig.PrivateKeys == null)
			{
				throw new ArgumentNullException("perTenantconfig.PrivateKeys");
			}
			this.clientContext = clientContext;
			this.configProvider = new ConfigurationInformationProvider(perTenantconfig);
			this.privateKeyProvider = new TrustedPublishingDomainPrivateKeyProvider(clientContext, perTenantconfig.PrivateKeys);
			this.globalConfigurationProvider = new GlobalConfigurationCacheProvider();
			this.directoryServiceProvider = new DirectoryServiceProvider(clientContext);
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x0600642A RID: 25642 RVA: 0x001A8252 File Offset: 0x001A6452
		public IConfigurationInformationProvider ConfigurationInformation
		{
			get
			{
				return this.configProvider;
			}
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x0600642B RID: 25643 RVA: 0x001A825A File Offset: 0x001A645A
		public IGlobalConfigurationAndCacheProvider GlobalConfigurationAndCache
		{
			get
			{
				return this.globalConfigurationProvider;
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x0600642C RID: 25644 RVA: 0x001A8262 File Offset: 0x001A6462
		public ITrustedPublishingDomainPrivateKeyProvider TrustedPublishingDomainPrivateKey
		{
			get
			{
				return this.privateKeyProvider;
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x0600642D RID: 25645 RVA: 0x001A826A File Offset: 0x001A646A
		public IDirectoryServiceProvider DirectoryService
		{
			get
			{
				return this.directoryServiceProvider;
			}
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x001A8272 File Offset: 0x001A6472
		public void Dispose()
		{
			if (this.privateKeyProvider != null)
			{
				this.privateKeyProvider.Dispose();
			}
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x001A8288 File Offset: 0x001A6488
		public void Notify(EventNotificationEntry entry, EventNotificationPropertyBag propertyBag)
		{
			if (entry == 4)
			{
				string text = (propertyBag != null) ? propertyBag.ObjectGuid : string.Empty;
				StorageGlobals.EventLogger.LogEvent(this.clientContext.OrgId, StorageEventLogConstants.Tuple_UnknownTemplateInPublishingLicense, text, text);
				string notificationReason = string.Format("Exchange could not match the RMS template with Id {0} specified in the publishing license against templates configured for this tenant.", text);
				EventNotificationItem.Publish(ExchangeComponent.Rms.Name, "UnknownTemplateInPublishingLicense", null, notificationReason, ResultSeverityLevel.Warning, false);
			}
		}

		// Token: 0x040038D1 RID: 14545
		private readonly ConfigurationInformationProvider configProvider;

		// Token: 0x040038D2 RID: 14546
		private readonly TrustedPublishingDomainPrivateKeyProvider privateKeyProvider;

		// Token: 0x040038D3 RID: 14547
		private readonly IGlobalConfigurationAndCacheProvider globalConfigurationProvider;

		// Token: 0x040038D4 RID: 14548
		private readonly IDirectoryServiceProvider directoryServiceProvider;

		// Token: 0x040038D5 RID: 14549
		private readonly RmsClientManagerContext clientContext;
	}
}
