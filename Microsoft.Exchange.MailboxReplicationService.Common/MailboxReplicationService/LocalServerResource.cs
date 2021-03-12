using System;
using System.Collections.Generic;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200026F RID: 623
	internal abstract class LocalServerResource : WlmResource
	{
		// Token: 0x06001F41 RID: 8001 RVA: 0x0004174A File Offset: 0x0003F94A
		public LocalServerResource(WorkloadType workloadType) : base(workloadType)
		{
			base.ResourceGuid = LocalServerResource.ResourceId;
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x0004175E File Offset: 0x0003F95E
		public override string ResourceName
		{
			get
			{
				return "LocalServer";
			}
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00041768 File Offset: 0x0003F968
		public override List<WlmResourceHealthMonitor> GetWlmResources()
		{
			return new List<WlmResourceHealthMonitor>(2)
			{
				new LocalCPUHealthMonitor(this),
				new ADReplicationHealthMonitor(this)
			};
		}

		// Token: 0x04000C9B RID: 3227
		public const string LocalServerResourceName = "LocalServer";

		// Token: 0x04000C9C RID: 3228
		internal static readonly Guid ResourceId = new Guid("17bd66db-d063-4641-b8ae-317c2b16c386");
	}
}
