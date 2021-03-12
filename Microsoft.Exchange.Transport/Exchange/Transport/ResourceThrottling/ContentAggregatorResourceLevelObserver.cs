using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceThrottling
{
	// Token: 0x02000012 RID: 18
	internal class ContentAggregatorResourceLevelObserver : IResourceLevelObserver
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public ContentAggregatorResourceLevelObserver(IStartableTransportComponent contentAggregator)
		{
			ArgumentValidator.ThrowIfNull("contentAggregator", contentAggregator);
			this.contentAggregator = contentAggregator;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D38 File Offset: 0x00000F38
		public virtual void HandleResourceChange(IEnumerable<ResourceUse> allResourceUses, IEnumerable<ResourceUse> changedResourceUses, IEnumerable<ResourceUse> rawResourceUses)
		{
			ArgumentValidator.ThrowIfNull("allResourceUses", allResourceUses);
			ArgumentValidator.ThrowIfNull("changedResourceUses", changedResourceUses);
			ArgumentValidator.ThrowIfNull("rawResourceUses", rawResourceUses);
			UseLevel useLevel = ResourceHelper.TryGetCurrentUseLevel(rawResourceUses, this.submissionQueueResource, UseLevel.Low);
			UseLevel useLevel2 = ResourceHelper.TryGetCurrentUseLevel(rawResourceUses, this.versionBucketsResource, UseLevel.Low);
			if (useLevel != UseLevel.Low || useLevel2 != UseLevel.Low)
			{
				if (!this.paused)
				{
					this.contentAggregator.Pause();
					this.paused = true;
					return;
				}
			}
			else if (this.paused)
			{
				this.contentAggregator.Continue();
				this.paused = false;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002DBD File Offset: 0x00000FBD
		public string Name
		{
			get
			{
				return "ContentAggregator";
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002DC4 File Offset: 0x00000FC4
		public bool Paused
		{
			get
			{
				return this.paused;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002DCC File Offset: 0x00000FCC
		public string SubStatus
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x04000029 RID: 41
		internal const string ResourceObserverName = "ContentAggregator";

		// Token: 0x0400002A RID: 42
		private IStartableTransportComponent contentAggregator;

		// Token: 0x0400002B RID: 43
		private readonly ResourceIdentifier submissionQueueResource = new ResourceIdentifier("QueueLength", "SubmissionQueue");

		// Token: 0x0400002C RID: 44
		private readonly ResourceIdentifier versionBucketsResource = new ResourceIdentifier("UsedVersionBuckets", "");

		// Token: 0x0400002D RID: 45
		private bool paused;
	}
}
