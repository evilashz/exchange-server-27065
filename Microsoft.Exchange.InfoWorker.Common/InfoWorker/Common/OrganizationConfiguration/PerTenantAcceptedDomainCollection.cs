using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x0200014F RID: 335
	internal class PerTenantAcceptedDomainCollection : PerTenantConfigurationLoader<AcceptedDomain[]>
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000934 RID: 2356 RVA: 0x00027A20 File Offset: 0x00025C20
		// (set) Token: 0x06000935 RID: 2357 RVA: 0x00027A28 File Offset: 0x00025C28
		public AcceptedDomain[] AcceptedDomains
		{
			get
			{
				return base.Data;
			}
			internal set
			{
				this.data = value;
			}
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00027A31 File Offset: 0x00025C31
		public PerTenantAcceptedDomainCollection(OrganizationId organizationId) : base(organizationId)
		{
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00027A3A File Offset: 0x00025C3A
		public PerTenantAcceptedDomainCollection(OrganizationId organizationId, TimeSpan timeoutInterval) : base(organizationId, timeoutInterval)
		{
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00027A44 File Offset: 0x00025C44
		public override void Initialize()
		{
			base.Initialize(PerTenantAcceptedDomainCollection.notificationLock);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00027A51 File Offset: 0x00025C51
		protected override ADNotificationRequestCookie Register(IConfigurationSession session)
		{
			return ADNotificationAdapter.RegisterChangeNotification<AcceptedDomain>(this.organizationId.ConfigurationUnit ?? session.GetOrgContainerId(), new ADNotificationCallback(base.ChangeCallback), session);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00027A7C File Offset: 0x00025C7C
		protected override AcceptedDomain[] Read(IConfigurationSession session)
		{
			ADPagedReader<AcceptedDomain> adpagedReader = session.FindAllPaged<AcceptedDomain>();
			return adpagedReader.ReadAllPages();
		}

		// Token: 0x0400072A RID: 1834
		private static object notificationLock = new object();
	}
}
