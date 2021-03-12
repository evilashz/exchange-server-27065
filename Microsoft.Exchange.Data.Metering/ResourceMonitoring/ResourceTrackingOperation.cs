using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000025 RID: 37
	internal sealed class ResourceTrackingOperation : Operation
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00007E4E File Offset: 0x0000604E
		public ResourceTrackingOperation(IResourceMeter resourceMeter, IExecutionInfo executionInfo, int maxTransientExceptions = 5) : base(ResourceTrackingOperation.GetDebugInfo(resourceMeter), ResourceTrackingOperation.GetResourceTrackingAction(resourceMeter), executionInfo, maxTransientExceptions)
		{
			this.resourceMeter = resourceMeter;
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00007E6B File Offset: 0x0000606B
		public IResourceMeter ResourceMeter
		{
			get
			{
				return this.resourceMeter;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007E73 File Offset: 0x00006073
		private static Action GetResourceTrackingAction(IResourceMeter resourceMeter)
		{
			ArgumentValidator.ThrowIfNull("resourceMeter", resourceMeter);
			return new Action(resourceMeter.Refresh);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00007E8D File Offset: 0x0000608D
		private static string GetDebugInfo(IResourceMeter resourceMeter)
		{
			ArgumentValidator.ThrowIfNull("resourceMeter", resourceMeter);
			return "Refresh method for " + resourceMeter.Resource;
		}

		// Token: 0x040000C7 RID: 199
		private const string DebugInfoPrefix = "Refresh method for ";

		// Token: 0x040000C8 RID: 200
		private readonly IResourceMeter resourceMeter;
	}
}
