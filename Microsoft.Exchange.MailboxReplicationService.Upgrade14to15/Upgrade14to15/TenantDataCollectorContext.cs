using System;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A4 RID: 164
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TenantDataCollectorContext : AnchorContext
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x00006C4F File Offset: 0x00004E4F
		internal TenantDataCollectorContext() : base("TenantDataCollector", OrganizationCapability.TenantUpgrade, TenantDataCollectorContext.AnchorConfig)
		{
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00006C63 File Offset: 0x00004E63
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00006C6A File Offset: 0x00004E6A
		public static AnchorConfig AnchorConfig { get; private set; } = new TenantDataCollectorConfig();

		// Token: 0x0600045A RID: 1114 RVA: 0x00006C74 File Offset: 0x00004E74
		public override CacheProcessorBase[] CreateCacheComponents(WaitHandle stopEvent)
		{
			SymphonyProxy symphonyProxyInstance = new SymphonyProxy();
			OrgOperationProxy orgOperationProxyInstance = new OrgOperationProxy();
			return new CacheProcessorBase[]
			{
				new FirstOrgCacheScanner(this, stopEvent),
				new TenantDataCollectorScheduler(this, orgOperationProxyInstance, symphonyProxyInstance, stopEvent)
			};
		}

		// Token: 0x040001E6 RID: 486
		public const string WebServiceUri = "WebServiceUri";

		// Token: 0x040001E7 RID: 487
		public const string CertificateSubject = "CertificateSubject";

		// Token: 0x040001E8 RID: 488
		public const string WorkLoadServiceName = "WorkloadService.svc";

		// Token: 0x040001E9 RID: 489
		public const string E14DataDirectory = "E14DataDirectory";

		// Token: 0x040001EA RID: 490
		public const string E15DataDirectory = "E15DataDirectory";

		// Token: 0x040001EB RID: 491
		public const string UpgradeUnitsConversionFactor = "UpgradeUnitsConversionFactor";

		// Token: 0x040001EC RID: 492
		public const string CheckAllAccountPartitions = "CheckAllAccountPartitions";

		// Token: 0x040001ED RID: 493
		public const string UploadToSymphony = "UploadToSymphony";

		// Token: 0x040001EE RID: 494
		public const string ValidateMailboxVersions = "ValidateMailboxVersions";

		// Token: 0x040001EF RID: 495
		internal const string TenantDataCollectorApplicationName = "TenantDataCollector";
	}
}
