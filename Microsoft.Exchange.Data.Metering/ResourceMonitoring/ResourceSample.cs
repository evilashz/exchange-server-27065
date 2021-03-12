using System;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000021 RID: 33
	internal struct ResourceSample
	{
		// Token: 0x06000154 RID: 340 RVA: 0x000066FD File Offset: 0x000048FD
		public ResourceSample(UseLevel useLevel, long pressure)
		{
			this.useLevel = useLevel;
			this.pressure = pressure;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000670D File Offset: 0x0000490D
		public UseLevel UseLevel
		{
			get
			{
				return this.useLevel;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006715 File Offset: 0x00004915
		public long Pressure
		{
			get
			{
				return this.pressure;
			}
		}

		// Token: 0x0400009E RID: 158
		private readonly UseLevel useLevel;

		// Token: 0x0400009F RID: 159
		private readonly long pressure;
	}
}
