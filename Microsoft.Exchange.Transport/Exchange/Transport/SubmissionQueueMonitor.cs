using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000068 RID: 104
	internal sealed class SubmissionQueueMonitor : ResourceMonitor
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0000E53B File Offset: 0x0000C73B
		public SubmissionQueueMonitor(ResourceManagerConfiguration.ResourceMonitorConfiguration configuration) : base(string.Empty, configuration)
		{
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000E549 File Offset: 0x0000C749
		public override string ToString(ResourceUses resourceUses, int currentPressure)
		{
			return Strings.SubmissionQueueUses(currentPressure, ResourceManager.MapToLocalizedString(resourceUses), base.LowPressureLimit, base.MediumPressureLimit, base.HighPressureLimit);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000E570 File Offset: 0x0000C770
		protected override bool GetCurrentReading(out int currentReading)
		{
			SubmitMessageQueue submitMessageQueue = Components.CategorizerComponent.SubmitMessageQueue;
			if (submitMessageQueue != null && !submitMessageQueue.Suspended)
			{
				currentReading = submitMessageQueue.ActiveCountExcludingPriorityNone;
			}
			else
			{
				currentReading = 0;
			}
			return true;
		}
	}
}
