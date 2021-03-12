using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x0200001D RID: 29
	internal sealed class PrivateBytesResourceMeter : ResourceMeter
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00006388 File Offset: 0x00004588
		public PrivateBytesResourceMeter(PressureTransitions pressureTransitions, ulong totalPhysicalMemory) : base("PrivateBytes", string.Empty, pressureTransitions)
		{
			ArgumentValidator.ThrowIfInvalidValue<ulong>("totalPhysicalMemory", totalPhysicalMemory, (ulong memory) => memory > 0UL);
			this.totalPhysicalMemory = totalPhysicalMemory;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000063E0 File Offset: 0x000045E0
		protected override long GetCurrentPressure()
		{
			ulong num;
			if (this.nativeMethods.GetProcessPrivateBytes(out num))
			{
				return (long)(num * 100UL / this.totalPhysicalMemory);
			}
			return 0L;
		}

		// Token: 0x04000084 RID: 132
		private readonly INativeMethodsWrapper nativeMethods = NativeMethodsWrapperFactory.CreateNativeMethodsWrapper();

		// Token: 0x04000085 RID: 133
		private readonly ulong totalPhysicalMemory;
	}
}
