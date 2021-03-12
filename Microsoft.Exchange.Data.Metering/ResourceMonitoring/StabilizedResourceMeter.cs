using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000026 RID: 38
	internal class StabilizedResourceMeter : IResourceMeter
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00007EB0 File Offset: 0x000060B0
		public StabilizedResourceMeter(IResourceMeter rawResourceMeter, int sampleCount)
		{
			ArgumentValidator.ThrowIfNull("rawResourceMeter", rawResourceMeter);
			ArgumentValidator.ThrowIfInvalidValue<int>("sampleCount", sampleCount, (int count) => count > 1);
			this.rawResourceMeter = rawResourceMeter;
			this.rawResourceMeter.Refresh();
			ResourceSample initialSample = new ResourceSample(this.rawResourceMeter.ResourceUse.CurrentUseLevel, this.rawResourceMeter.Pressure);
			this.stabilizer = new ResourceSampleStabilizer(sampleCount, initialSample);
			this.pressure = this.rawResourceMeter.Pressure;
			this.resourceUse = new ResourceUse(this.rawResourceMeter.Resource, this.rawResourceMeter.ResourceUse.CurrentUseLevel, this.rawResourceMeter.ResourceUse.PreviousUseLevel);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007F7E File Offset: 0x0000617E
		public long Pressure
		{
			get
			{
				return this.pressure;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00007F86 File Offset: 0x00006186
		public PressureTransitions PressureTransitions
		{
			get
			{
				return this.rawResourceMeter.PressureTransitions;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007F93 File Offset: 0x00006193
		public ResourceIdentifier Resource
		{
			get
			{
				return this.rawResourceMeter.Resource;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00007FA0 File Offset: 0x000061A0
		public ResourceUse ResourceUse
		{
			get
			{
				return this.resourceUse;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00007FA8 File Offset: 0x000061A8
		public ResourceUse RawResourceUse
		{
			get
			{
				return this.rawResourceMeter.RawResourceUse;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00007FB8 File Offset: 0x000061B8
		public void Refresh()
		{
			this.rawResourceMeter.Refresh();
			ResourceSample sample = new ResourceSample(this.rawResourceMeter.ResourceUse.CurrentUseLevel, this.rawResourceMeter.Pressure);
			ResourceSample stabilizedSample = this.stabilizer.GetStabilizedSample(sample);
			ResourceUse resourceUse = new ResourceUse(this.Resource, stabilizedSample.UseLevel, this.resourceUse.CurrentUseLevel);
			this.resourceUse = resourceUse;
			this.pressure = stabilizedSample.Pressure;
		}

		// Token: 0x040000C9 RID: 201
		private readonly IResourceMeter rawResourceMeter;

		// Token: 0x040000CA RID: 202
		private readonly ResourceSampleStabilizer stabilizer;

		// Token: 0x040000CB RID: 203
		private ResourceUse resourceUse;

		// Token: 0x040000CC RID: 204
		private long pressure;
	}
}
