using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000156 RID: 342
	internal class PerTenantRemoteDomainCollection : PerTenantConfigurationLoader<DomainContentConfig[]>
	{
		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0002870B File Offset: 0x0002690B
		public DomainContentConfig[] RemoteDomains
		{
			get
			{
				return base.Data;
			}
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00028713 File Offset: 0x00026913
		public PerTenantRemoteDomainCollection(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x0002871C File Offset: 0x0002691C
		public PerTenantRemoteDomainCollection(OrganizationId organizationId, TimeSpan timeoutInterval) : base(organizationId, timeoutInterval)
		{
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028726 File Offset: 0x00026926
		public override void Initialize()
		{
			base.Initialize(PerTenantRemoteDomainCollection.notificationLock);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00028733 File Offset: 0x00026933
		protected override ADNotificationRequestCookie Register(IConfigurationSession session)
		{
			return ADNotificationAdapter.RegisterChangeNotification<DomainContentConfig>(this.organizationId.ConfigurationUnit ?? session.GetOrgContainerId(), new ADNotificationCallback(base.ChangeCallback), session);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0002875C File Offset: 0x0002695C
		protected override DomainContentConfig[] Read(IConfigurationSession session)
		{
			ADPagedReader<DomainContentConfig> adpagedReader = session.FindAllPaged<DomainContentConfig>();
			return adpagedReader.ReadAllPages();
		}

		// Token: 0x0400074B RID: 1867
		private static object notificationLock = new object();
	}
}
