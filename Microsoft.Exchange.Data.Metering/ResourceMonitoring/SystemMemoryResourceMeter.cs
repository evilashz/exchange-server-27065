using System;
using System.ComponentModel;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000027 RID: 39
	internal class SystemMemoryResourceMeter : ResourceMeter
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00008031 File Offset: 0x00006231
		public SystemMemoryResourceMeter(PressureTransitions pressureTransitions) : base("SystemMemory", string.Empty, pressureTransitions)
		{
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00008050 File Offset: 0x00006250
		protected override long GetCurrentPressure()
		{
			uint num;
			if (this.nativeMethods.GetSystemMemoryUsePercentage(out num))
			{
				return (long)((ulong)num);
			}
			throw new TransientException(new LocalizedString("Failed to get the system memory usage."), new Win32Exception());
		}

		// Token: 0x040000CE RID: 206
		private readonly INativeMethodsWrapper nativeMethods = NativeMethodsWrapperFactory.CreateNativeMethodsWrapper();
	}
}
