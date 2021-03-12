using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.ResourceMonitoring
{
	// Token: 0x02000313 RID: 787
	internal class MeterableQueue : IMeterableQueue
	{
		// Token: 0x06002222 RID: 8738 RVA: 0x00080EE8 File Offset: 0x0007F0E8
		public MeterableQueue(string name, MessageQueue messageQueue)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("name", name);
			ArgumentValidator.ThrowIfNull("messageQueue", messageQueue);
			this.name = name;
			this.messageQueue = messageQueue;
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x00080F14 File Offset: 0x0007F114
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x00080F1C File Offset: 0x0007F11C
		public long Length
		{
			get
			{
				return (long)this.messageQueue.ActiveCountExcludingPriorityNone;
			}
		}

		// Token: 0x040011D5 RID: 4565
		private readonly string name;

		// Token: 0x040011D6 RID: 4566
		private readonly MessageQueue messageQueue;
	}
}
