using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000028 RID: 40
	internal class UsedDiskSpaceResourceMeter : ResourceMeter
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00008083 File Offset: 0x00006283
		public UsedDiskSpaceResourceMeter(string directoryName, PressureTransitions pressureTransitions) : this("UsedDiskSpace", directoryName, pressureTransitions)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008092 File Offset: 0x00006292
		protected UsedDiskSpaceResourceMeter(string resourceName, string directoryName, PressureTransitions pressureTransitions) : base(resourceName, directoryName, pressureTransitions)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("directoryName", directoryName);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000080B3 File Offset: 0x000062B3
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000080BB File Offset: 0x000062BB
		private protected long TotalNumberOfBytes { protected get; private set; }

		// Token: 0x06000196 RID: 406 RVA: 0x000080C4 File Offset: 0x000062C4
		protected override long GetCurrentPressure()
		{
			ulong num;
			ulong num2;
			ulong num3;
			if (this.GetSpace(out num, out num2, out num3))
			{
				this.TotalNumberOfBytes = (long)num2;
				return (long)((num2 - num) * 100UL / num2);
			}
			return 0L;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000080F2 File Offset: 0x000062F2
		protected bool GetSpace(out ulong freeBytesAvailable, out ulong totalNumberOfBytes, out ulong totalNumberOfFreeBytes)
		{
			return this.nativeMethods.GetDiskFreeSpaceEx(base.Resource.InstanceName, out freeBytesAvailable, out totalNumberOfBytes, out totalNumberOfFreeBytes);
		}

		// Token: 0x040000CF RID: 207
		private readonly INativeMethodsWrapper nativeMethods = NativeMethodsWrapperFactory.CreateNativeMethodsWrapper();
	}
}
