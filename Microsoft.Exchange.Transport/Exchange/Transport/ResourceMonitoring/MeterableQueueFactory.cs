using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x02000314 RID: 788
	internal class MeterableQueueFactory
	{
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00080F2A File Offset: 0x0007F12A
		// (set) Token: 0x06002226 RID: 8742 RVA: 0x00080F31 File Offset: 0x0007F131
		internal static Func<string, MessageQueue, IMeterableQueue> CreateMeterableQueueFunc
		{
			get
			{
				return MeterableQueueFactory.createMeterableQueueFunc;
			}
			set
			{
				MeterableQueueFactory.createMeterableQueueFunc = value;
			}
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00080F39 File Offset: 0x0007F139
		internal static IMeterableQueue CreateMeterableQueue(string name, MessageQueue queue)
		{
			return MeterableQueueFactory.CreateMeterableQueueFunc(name, queue);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00080F47 File Offset: 0x0007F147
		private static IMeterableQueue CreateRealMeterableQueue(string name, MessageQueue queue)
		{
			ArgumentValidator.ThrowIfNull("queue", queue);
			return new MeterableQueue(name, queue);
		}

		// Token: 0x040011D7 RID: 4567
		private static Func<string, MessageQueue, IMeterableQueue> createMeterableQueueFunc = new Func<string, MessageQueue, IMeterableQueue>(MeterableQueueFactory.CreateRealMeterableQueue);
	}
}
