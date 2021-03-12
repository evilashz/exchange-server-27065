using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x02000158 RID: 344
	internal class PerTenantTransportSettings : PerTenantConfigurationLoader<TransportConfigContainer>
	{
		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0002881E File Offset: 0x00026A1E
		public TransportConfigContainer Configuration
		{
			get
			{
				return base.Data;
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00028826 File Offset: 0x00026A26
		public PerTenantTransportSettings(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002882F File Offset: 0x00026A2F
		public PerTenantTransportSettings(OrganizationId organizationId, TimeSpan timeoutInterval) : base(organizationId, timeoutInterval)
		{
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00028839 File Offset: 0x00026A39
		public override void Initialize()
		{
			base.Initialize(PerTenantTransportSettings.notificationLock);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00028846 File Offset: 0x00026A46
		protected override ADNotificationRequestCookie Register(IConfigurationSession session)
		{
			return ADNotificationAdapter.RegisterChangeNotification<TransportConfigContainer>(this.organizationId.ConfigurationUnit ?? session.GetOrgContainerId(), new ADNotificationCallback(base.ChangeCallback), session);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00028870 File Offset: 0x00026A70
		protected override TransportConfigContainer Read(IConfigurationSession session)
		{
			TransportConfigContainer[] array = session.Find<TransportConfigContainer>(this.organizationId.ConfigurationUnit, QueryScope.SubTree, null, null, 0);
			if (array.Length == 0)
			{
				return null;
			}
			return array[0];
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0002889D File Offset: 0x00026A9D
		public TransportConfigContainer ReadTransportConfig(IConfigurationSession session)
		{
			return this.Read(session);
		}

		// Token: 0x0400074D RID: 1869
		private static object notificationLock = new object();
	}
}
