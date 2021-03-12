using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000151 RID: 337
	internal class PerTenantOrganizationConfiguration : PerTenantConfigurationLoader<Organization>
	{
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00027D4E File Offset: 0x00025F4E
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00027D56 File Offset: 0x00025F56
		public Organization Configuration
		{
			get
			{
				return this.data;
			}
			internal set
			{
				this.data = value;
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00027D5F File Offset: 0x00025F5F
		public PerTenantOrganizationConfiguration(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00027D68 File Offset: 0x00025F68
		public override void Initialize()
		{
			base.Initialize(PerTenantOrganizationConfiguration.notificationLock);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00027D75 File Offset: 0x00025F75
		protected override ADNotificationRequestCookie Register(IConfigurationSession session)
		{
			return ADNotificationAdapter.RegisterChangeNotification<Organization>(this.organizationId.ConfigurationUnit ?? session.GetOrgContainerId(), new ADNotificationCallback(base.ChangeCallback), session);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00027D9E File Offset: 0x00025F9E
		protected override Organization Read(IConfigurationSession session)
		{
			return session.GetOrgContainer();
		}

		// Token: 0x04000733 RID: 1843
		private static object notificationLock = new object();
	}
}
