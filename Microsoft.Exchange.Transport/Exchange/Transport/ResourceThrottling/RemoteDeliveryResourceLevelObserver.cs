using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x0200003E RID: 62
	internal class RemoteDeliveryResourceLevelObserver : ResourceLevelObserver
	{
		// Token: 0x06000167 RID: 359 RVA: 0x00006D74 File Offset: 0x00004F74
		public RemoteDeliveryResourceLevelObserver(RemoteDeliveryComponent remoteDelivery, string databasePath, bool dehydrateMessagesUnderMemoryPressure) : base("RemoteDelivery", remoteDelivery, new List<ResourceIdentifier>
		{
			new ResourceIdentifier("UsedVersionBuckets", databasePath)
		})
		{
			ArgumentValidator.ThrowIfNull("remoteDelivery", remoteDelivery);
			ArgumentValidator.ThrowIfNullOrEmpty("databasePath", databasePath);
			this.remoteDelivery = remoteDelivery;
			this.dehydrateMessagesUnderMemoryPressure = dehydrateMessagesUnderMemoryPressure;
			this.versionBucketsResource = new ResourceIdentifier("UsedVersionBuckets", databasePath);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006E04 File Offset: 0x00005004
		public override void HandleResourceChange(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses)
		{
			ArgumentValidator.ThrowIfNull("allResourceUses", allResourceUses);
			ArgumentValidator.ThrowIfNull("changedResourceUses", changedResourceUses);
			ArgumentValidator.ThrowIfNull("rawResourceUses", rawResourceUses);
			if (this.dehydrateMessagesUnderMemoryPressure)
			{
				UseLevel useLevel = ResourceHelper.TryGetCurrentUseLevel(allResourceUses, this.privateBytesResource, UseLevel.Low);
				UseLevel useLevel2 = ResourceHelper.TryGetCurrentUseLevel(allResourceUses, this.versionBucketsResource, UseLevel.Low);
				UseLevel useLevel3 = ResourceHelper.TryGetCurrentUseLevel(rawResourceUses, this.privateBytesResource, UseLevel.Low);
				UseLevel useLevel4 = ResourceHelper.TryGetCurrentUseLevel(rawResourceUses, this.systemMemoryResource, UseLevel.Low);
				if ((useLevel3 != UseLevel.Low || useLevel != UseLevel.Low || useLevel4 != UseLevel.Low) && useLevel2 == UseLevel.Low)
				{
					this.remoteDelivery.CommitLazyAndDehydrateMessages();
				}
			}
			base.HandleResourceChange(allResourceUses, changedResourceUses, rawResourceUses);
		}

		// Token: 0x040000BD RID: 189
		internal const string ResourceObserverName = "RemoteDelivery";

		// Token: 0x040000BE RID: 190
		private readonly ResourceIdentifier privateBytesResource = new ResourceIdentifier("PrivateBytes", "");

		// Token: 0x040000BF RID: 191
		private readonly bool dehydrateMessagesUnderMemoryPressure;

		// Token: 0x040000C0 RID: 192
		private readonly RemoteDeliveryComponent remoteDelivery;

		// Token: 0x040000C1 RID: 193
		private readonly ResourceIdentifier systemMemoryResource = new ResourceIdentifier("SystemMemory", "");

		// Token: 0x040000C2 RID: 194
		private readonly ResourceIdentifier versionBucketsResource;
	}
}
