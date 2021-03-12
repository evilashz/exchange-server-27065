using System;
using System.Threading;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200002E RID: 46
	public class DeterministicTime
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000B4F4 File Offset: 0x000096F4
		public DateTime UtcNow
		{
			get
			{
				long num = DateTime.UtcNow.Ticks;
				long num2;
				do
				{
					num2 = this.previousTimeTicks;
					if (num <= num2)
					{
						num = num2 + 1L;
					}
				}
				while (num2 != Interlocked.CompareExchange(ref this.previousTimeTicks, num, num2));
				return new DateTime(num, DateTimeKind.Utc);
			}
		}

		// Token: 0x040004AD RID: 1197
		private long previousTimeTicks = DateTime.MinValue.Ticks;
	}
}
