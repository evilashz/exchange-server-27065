using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000157 RID: 343
	internal class PerTenantSmimeSettings : PerTenantConfigurationLoader<SmimeConfigurationContainer>
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00028784 File Offset: 0x00026984
		public SmimeConfigurationContainer Configuration
		{
			get
			{
				return base.Data;
			}
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0002878C File Offset: 0x0002698C
		public PerTenantSmimeSettings(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00028795 File Offset: 0x00026995
		public PerTenantSmimeSettings(OrganizationId organizationId, TimeSpan timeoutInterval) : base(organizationId, timeoutInterval)
		{
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002879F File Offset: 0x0002699F
		public override void Initialize()
		{
			base.Initialize(PerTenantSmimeSettings.notificationLock);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000287AC File Offset: 0x000269AC
		protected override ADNotificationRequestCookie Register(IConfigurationSession session)
		{
			return ADNotificationAdapter.RegisterChangeNotification<SmimeConfigurationContainer>(this.organizationId.ConfigurationUnit ?? session.GetOrgContainerId(), new ADNotificationCallback(base.ChangeCallback), session);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x000287D8 File Offset: 0x000269D8
		protected override SmimeConfigurationContainer Read(IConfigurationSession session)
		{
			SmimeConfigurationContainer[] array = session.Find<SmimeConfigurationContainer>(SmimeConfigurationContainer.GetWellKnownParentLocation(session.GetOrgContainerId()), QueryScope.SubTree, null, null, 1);
			if (array.Length == 0)
			{
				return new SmimeConfigurationContainer();
			}
			return array[0];
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00028809 File Offset: 0x00026A09
		public SmimeConfigurationContainer ReadSmimeConfig(IConfigurationSession session)
		{
			return this.Read(session);
		}

		// Token: 0x0400074C RID: 1868
		private static object notificationLock = new object();
	}
}
