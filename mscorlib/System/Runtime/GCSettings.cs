using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime
{
	// Token: 0x020006EA RID: 1770
	[__DynamicallyInvokable]
	public static class GCSettings
	{
		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x0600500B RID: 20491 RVA: 0x00119B7C File Offset: 0x00117D7C
		// (set) Token: 0x0600500C RID: 20492 RVA: 0x00119B83 File Offset: 0x00117D83
		[__DynamicallyInvokable]
		public static GCLatencyMode LatencyMode
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (GCLatencyMode)GC.GetGCLatencyMode();
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
			set
			{
				if (value < GCLatencyMode.Batch || value > GCLatencyMode.SustainedLowLatency)
				{
					throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
				}
				if (GC.SetGCLatencyMode((int)value) == 1)
				{
					throw new InvalidOperationException("The NoGCRegion mode is in progress. End it and then set a different mode.");
				}
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x0600500D RID: 20493 RVA: 0x00119BB1 File Offset: 0x00117DB1
		// (set) Token: 0x0600500E RID: 20494 RVA: 0x00119BB8 File Offset: 0x00117DB8
		[__DynamicallyInvokable]
		public static GCLargeObjectHeapCompactionMode LargeObjectHeapCompactionMode
		{
			[SecuritySafeCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			get
			{
				return (GCLargeObjectHeapCompactionMode)GC.GetLOHCompactionMode();
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[__DynamicallyInvokable]
			[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
			set
			{
				if (value < GCLargeObjectHeapCompactionMode.Default || value > GCLargeObjectHeapCompactionMode.CompactOnce)
				{
					throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
				}
				GC.SetLOHCompactionMode((int)value);
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x0600500F RID: 20495 RVA: 0x00119BD8 File Offset: 0x00117DD8
		[__DynamicallyInvokable]
		public static bool IsServerGC
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return GC.IsServerGC();
			}
		}

		// Token: 0x02000C2E RID: 3118
		private enum SetLatencyModeStatus
		{
			// Token: 0x040036D9 RID: 14041
			Succeeded,
			// Token: 0x040036DA RID: 14042
			NoGCInProgress
		}
	}
}
