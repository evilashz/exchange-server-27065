using System;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000BB RID: 187
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeHandlerContext : AnchorContext
	{
		// Token: 0x06000592 RID: 1426 RVA: 0x00009EF7 File Offset: 0x000080F7
		internal UpgradeHandlerContext() : base("UpgradeHandler", OrganizationCapability.TenantUpgrade, UpgradeHandlerContext.AnchorConfig)
		{
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00009F0B File Offset: 0x0000810B
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00009F12 File Offset: 0x00008112
		public static AnchorConfig AnchorConfig { get; private set; } = new UpgradeHandlerConfig();

		// Token: 0x06000595 RID: 1429 RVA: 0x00009F1C File Offset: 0x0000811C
		public override CacheProcessorBase[] CreateCacheComponents(WaitHandle stopEvent)
		{
			SymphonyProxy symphonyProxyInstance = new SymphonyProxy();
			OrgOperationProxy orgOperationProxyInstance = new OrgOperationProxy();
			return new CacheProcessorBase[]
			{
				new FirstOrgCacheScanner(this, stopEvent),
				new UpgradeHandlerScheduler(this, orgOperationProxyInstance, symphonyProxyInstance, stopEvent)
			};
		}

		// Token: 0x040002B1 RID: 689
		public const string WebServiceUri = "WebServiceUri";

		// Token: 0x040002B2 RID: 690
		public const string CertificateSubject = "CertificateSubject";

		// Token: 0x040002B3 RID: 691
		public const string WorkLoadServiceName = "WorkloadService.svc";

		// Token: 0x040002B4 RID: 692
		public const string NumberOfSetMailboxAttempts = "NumberOfSetMailboxAttempts";

		// Token: 0x040002B5 RID: 693
		public const string SetMailboxAttemptIntervalSeconds = "SetMailboxAttemptIntervalSeconds";

		// Token: 0x040002B6 RID: 694
		internal const string UpgradeApplicationName = "UpgradeHandler";
	}
}
