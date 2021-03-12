using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200003B RID: 59
	internal class CrashProperties
	{
		// Token: 0x06000149 RID: 329 RVA: 0x00006558 File Offset: 0x00004758
		public CrashProperties(double crashCount, DateTime lastCrashTime)
		{
			this.crashCount = crashCount;
			this.lastCrashTime = lastCrashTime;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000656E File Offset: 0x0000476E
		internal double CrashCount
		{
			get
			{
				return this.crashCount;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00006576 File Offset: 0x00004776
		internal DateTime LastCrashTime
		{
			get
			{
				return this.lastCrashTime;
			}
		}

		// Token: 0x040000B0 RID: 176
		private readonly double crashCount;

		// Token: 0x040000B1 RID: 177
		private readonly DateTime lastCrashTime;
	}
}
