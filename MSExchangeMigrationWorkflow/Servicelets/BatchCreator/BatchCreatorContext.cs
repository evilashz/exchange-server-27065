using System;
using System.Threading;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.BatchCreator
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BatchCreatorContext : AnchorContext
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000029AD File Offset: 0x00000BAD
		public BatchCreatorContext() : base("BatchCreator", OrganizationCapability.Management, new BatchCreatorConfig())
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029C4 File Offset: 0x00000BC4
		public override CacheProcessorBase[] CreateCacheComponents(WaitHandle stopEvent)
		{
			return new CacheProcessorBase[]
			{
				new FirstOrgCacheScanner(this, stopEvent),
				new BatchCreatorScheduler(this, stopEvent)
			};
		}

		// Token: 0x04000017 RID: 23
		internal new const string ApplicationName = "BatchCreator";
	}
}
