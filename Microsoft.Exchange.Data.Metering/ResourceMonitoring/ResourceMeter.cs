using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x0200001C RID: 28
	internal abstract class ResourceMeter : IResourceMeter
	{
		// Token: 0x0600013C RID: 316 RVA: 0x000062B4 File Offset: 0x000044B4
		protected ResourceMeter(string resourceName, string resourceInstanceName, PressureTransitions pressureTransitions)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("resourceName", resourceName);
			ArgumentValidator.ThrowIfNull("resourceInstanceName", resourceInstanceName);
			this.resourceIdentifier = new ResourceIdentifier(resourceName, resourceInstanceName);
			this.pressureTransitions = pressureTransitions;
			this.resourceUse = new ResourceUse(this.resourceIdentifier, UseLevel.Low, UseLevel.Low);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006304 File Offset: 0x00004504
		public ResourceIdentifier Resource
		{
			get
			{
				return this.resourceIdentifier;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000630C File Offset: 0x0000450C
		public long Pressure
		{
			get
			{
				return this.pressure;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006314 File Offset: 0x00004514
		public PressureTransitions PressureTransitions
		{
			get
			{
				return this.pressureTransitions;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000631C File Offset: 0x0000451C
		public ResourceUse ResourceUse
		{
			get
			{
				return this.resourceUse;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006324 File Offset: 0x00004524
		public ResourceUse RawResourceUse
		{
			get
			{
				return this.resourceUse;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000632C File Offset: 0x0000452C
		public void Refresh()
		{
			this.pressure = this.GetCurrentPressure();
			UseLevel useLevel = this.pressureTransitions.GetUseLevel(this.pressure, this.resourceUse.PreviousUseLevel);
			this.resourceUse = new ResourceUse(this.resourceIdentifier, useLevel, this.resourceUse.CurrentUseLevel);
		}

		// Token: 0x06000143 RID: 323
		protected abstract long GetCurrentPressure();

		// Token: 0x04000080 RID: 128
		private readonly ResourceIdentifier resourceIdentifier;

		// Token: 0x04000081 RID: 129
		private ResourceUse resourceUse;

		// Token: 0x04000082 RID: 130
		private long pressure;

		// Token: 0x04000083 RID: 131
		private PressureTransitions pressureTransitions;
	}
}
