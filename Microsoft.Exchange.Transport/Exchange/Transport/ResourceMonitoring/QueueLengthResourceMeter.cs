using System;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x02000322 RID: 802
	internal sealed class QueueLengthResourceMeter : ResourceMeter
	{
		// Token: 0x060022B2 RID: 8882 RVA: 0x000837C9 File Offset: 0x000819C9
		public QueueLengthResourceMeter(IMeterableQueue meterableQueue, PressureTransitions pressureTransitions) : base("QueueLength", QueueLengthResourceMeter.GetQueueName(meterableQueue), pressureTransitions)
		{
			this.meterableQueue = meterableQueue;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000837E4 File Offset: 0x000819E4
		protected override long GetCurrentPressure()
		{
			return this.meterableQueue.Length;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000837F1 File Offset: 0x000819F1
		private static string GetQueueName(IMeterableQueue meterableQueue)
		{
			ArgumentValidator.ThrowIfNull("meterableQueue", meterableQueue);
			return meterableQueue.Name;
		}

		// Token: 0x04001222 RID: 4642
		private readonly IMeterableQueue meterableQueue;
	}
}
