using System;
using System.Globalization;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000011 RID: 17
	internal struct PressureTransitions
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000020D0 File Offset: 0x000002D0
		public PressureTransitions(long mediumToHigh, long highToMedium, long lowToMedium, long mediumToLow)
		{
			if (mediumToHigh <= highToMedium || highToMedium <= lowToMedium || lowToMedium <= mediumToLow)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The pressure transitions should define strict non-overlapping ranges : mediumToLow < lowToMedium < highToMedium < mediumToHigh : {0}, {1}, {2}, {3}", new object[]
				{
					mediumToLow,
					lowToMedium,
					highToMedium,
					mediumToHigh
				}));
			}
			this.mediumToHigh = mediumToHigh;
			this.highToMedium = highToMedium;
			this.lowToMedium = lowToMedium;
			this.mediumToLow = mediumToLow;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002149 File Offset: 0x00000349
		internal long MediumToHigh
		{
			get
			{
				return this.mediumToHigh;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002151 File Offset: 0x00000351
		internal long HighToMedium
		{
			get
			{
				return this.highToMedium;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002159 File Offset: 0x00000359
		internal long LowToMedium
		{
			get
			{
				return this.lowToMedium;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002161 File Offset: 0x00000361
		internal long MediumToLow
		{
			get
			{
				return this.mediumToLow;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000216C File Offset: 0x0000036C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "[PressureTransitions: MediumToHigh={0} HighToMedium={1} LowToMedium={2} MediumToLow={3}]", new object[]
			{
				this.MediumToHigh,
				this.HighToMedium,
				this.LowToMedium,
				this.MediumToLow
			});
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000021C8 File Offset: 0x000003C8
		internal UseLevel GetUseLevel(long pressure, UseLevel previousUseLevel)
		{
			if (pressure > this.mediumToHigh)
			{
				return UseLevel.High;
			}
			if (PressureTransitions.Between(pressure, this.highToMedium, this.mediumToHigh))
			{
				if (previousUseLevel == UseLevel.High)
				{
					return UseLevel.High;
				}
				return UseLevel.Medium;
			}
			else
			{
				if (PressureTransitions.Between(pressure, this.lowToMedium, this.highToMedium))
				{
					return UseLevel.Medium;
				}
				if (PressureTransitions.Between(pressure, this.mediumToLow, this.lowToMedium) && (previousUseLevel == UseLevel.Medium || previousUseLevel == UseLevel.High))
				{
					return UseLevel.Medium;
				}
				return UseLevel.Low;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002231 File Offset: 0x00000431
		private static bool Between(long number, long lowerLimit, long upperLimit)
		{
			return lowerLimit < number && number <= upperLimit;
		}

		// Token: 0x04000008 RID: 8
		private readonly long mediumToHigh;

		// Token: 0x04000009 RID: 9
		private readonly long highToMedium;

		// Token: 0x0400000A RID: 10
		private readonly long lowToMedium;

		// Token: 0x0400000B RID: 11
		private readonly long mediumToLow;
	}
}
