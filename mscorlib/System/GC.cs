using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
	// Token: 0x020000E9 RID: 233
	[__DynamicallyInvokable]
	public static class GC
	{
		// Token: 0x06000E90 RID: 3728
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetGCLatencyMode();

		// Token: 0x06000E91 RID: 3729
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int SetGCLatencyMode(int newLatencyMode);

		// Token: 0x06000E92 RID: 3730
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int _StartNoGCRegion(long totalSize, bool lohSizeKnown, long lohSize, bool disallowFullBlockingGC);

		// Token: 0x06000E93 RID: 3731
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int _EndNoGCRegion();

		// Token: 0x06000E94 RID: 3732
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLOHCompactionMode();

		// Token: 0x06000E95 RID: 3733
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLOHCompactionMode(int newLOHCompactionyMode);

		// Token: 0x06000E96 RID: 3734
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGenerationWR(IntPtr handle);

		// Token: 0x06000E97 RID: 3735
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern long GetTotalMemory();

		// Token: 0x06000E98 RID: 3736
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _Collect(int generation, int mode);

		// Token: 0x06000E99 RID: 3737
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxGeneration();

		// Token: 0x06000E9A RID: 3738
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _CollectionCount(int generation, int getSpecialGCCount);

		// Token: 0x06000E9B RID: 3739
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsServerGC();

		// Token: 0x06000E9C RID: 3740
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _AddMemoryPressure(ulong bytesAllocated);

		// Token: 0x06000E9D RID: 3741
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _RemoveMemoryPressure(ulong bytesAllocated);

		// Token: 0x06000E9E RID: 3742 RVA: 0x0002CD0C File Offset: 0x0002AF0C
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void AddMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("pressure", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32"));
			}
			GC._AddMemoryPressure((ulong)bytesAllocated);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0002CD60 File Offset: 0x0002AF60
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void RemoveMemoryPressure(long bytesAllocated)
		{
			if (bytesAllocated <= 0L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_NeedPosNum"));
			}
			if (4 == IntPtr.Size && bytesAllocated > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("bytesAllocated", Environment.GetResourceString("ArgumentOutOfRange_MustBeNonNegInt32"));
			}
			GC._RemoveMemoryPressure((ulong)bytesAllocated);
		}

		// Token: 0x06000EA0 RID: 3744
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetGeneration(object obj);

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0002CDB3 File Offset: 0x0002AFB3
		[__DynamicallyInvokable]
		public static void Collect(int generation)
		{
			GC.Collect(generation, GCCollectionMode.Default);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0002CDBC File Offset: 0x0002AFBC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Collect()
		{
			GC._Collect(-1, 2);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0002CDC5 File Offset: 0x0002AFC5
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Collect(int generation, GCCollectionMode mode)
		{
			GC.Collect(generation, mode, true);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002CDCF File Offset: 0x0002AFCF
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking)
		{
			GC.Collect(generation, mode, blocking, false);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002CDDC File Offset: 0x0002AFDC
		[SecuritySafeCritical]
		public static void Collect(int generation, GCCollectionMode mode, bool blocking, bool compacting)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			if (mode < GCCollectionMode.Default || mode > GCCollectionMode.Optimized)
			{
				throw new ArgumentOutOfRangeException(Environment.GetResourceString("ArgumentOutOfRange_Enum"));
			}
			int num = 0;
			if (mode == GCCollectionMode.Optimized)
			{
				num |= 4;
			}
			if (compacting)
			{
				num |= 8;
			}
			if (blocking)
			{
				num |= 2;
			}
			else if (!compacting)
			{
				num |= 1;
			}
			GC._Collect(generation, num);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0002CE42 File Offset: 0x0002B042
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int CollectionCount(int generation)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			return GC._CollectionCount(generation, 0);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0002CE64 File Offset: 0x0002B064
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static int CollectionCount(int generation, bool getSpecialGCCount)
		{
			if (generation < 0)
			{
				throw new ArgumentOutOfRangeException("generation", Environment.GetResourceString("ArgumentOutOfRange_GenericPositive"));
			}
			return GC._CollectionCount(generation, getSpecialGCCount ? 1 : 0);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0002CE8C File Offset: 0x0002B08C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void KeepAlive(object obj)
		{
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0002CE90 File Offset: 0x0002B090
		[SecuritySafeCritical]
		public static int GetGeneration(WeakReference wo)
		{
			int generationWR = GC.GetGenerationWR(wo.m_handle);
			GC.KeepAlive(wo);
			return generationWR;
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0002CEB0 File Offset: 0x0002B0B0
		[__DynamicallyInvokable]
		public static int MaxGeneration
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				return GC.GetMaxGeneration();
			}
		}

		// Token: 0x06000EAB RID: 3755
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _WaitForPendingFinalizers();

		// Token: 0x06000EAC RID: 3756 RVA: 0x0002CEB7 File Offset: 0x0002B0B7
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void WaitForPendingFinalizers()
		{
			GC._WaitForPendingFinalizers();
		}

		// Token: 0x06000EAD RID: 3757
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _SuppressFinalize(object o);

		// Token: 0x06000EAE RID: 3758 RVA: 0x0002CEBE File Offset: 0x0002B0BE
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void SuppressFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._SuppressFinalize(obj);
		}

		// Token: 0x06000EAF RID: 3759
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ReRegisterForFinalize(object o);

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0002CED4 File Offset: 0x0002B0D4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void ReRegisterForFinalize(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			GC._ReRegisterForFinalize(obj);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static long GetTotalMemory(bool forceFullCollection)
		{
			long num = GC.GetTotalMemory();
			if (!forceFullCollection)
			{
				return num;
			}
			int num2 = 20;
			long num3 = num;
			float num4;
			do
			{
				GC.WaitForPendingFinalizers();
				GC.Collect();
				num = num3;
				num3 = GC.GetTotalMemory();
				num4 = (float)(num3 - num) / (float)num;
			}
			while (num2-- > 0 && (-0.05 >= (double)num4 || (double)num4 >= 0.05));
			return num3;
		}

		// Token: 0x06000EB2 RID: 3762
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long _GetAllocatedBytesForCurrentThread();

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0002CF46 File Offset: 0x0002B146
		[SecuritySafeCritical]
		public static long GetAllocatedBytesForCurrentThread()
		{
			return GC._GetAllocatedBytesForCurrentThread();
		}

		// Token: 0x06000EB4 RID: 3764
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _RegisterForFullGCNotification(int maxGenerationPercentage, int largeObjectHeapPercentage);

		// Token: 0x06000EB5 RID: 3765
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool _CancelFullGCNotification();

		// Token: 0x06000EB6 RID: 3766
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _WaitForFullGCApproach(int millisecondsTimeout);

		// Token: 0x06000EB7 RID: 3767
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int _WaitForFullGCComplete(int millisecondsTimeout);

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0002CF50 File Offset: 0x0002B150
		[SecurityCritical]
		public static void RegisterForFullGCNotification(int maxGenerationThreshold, int largeObjectHeapThreshold)
		{
			if (maxGenerationThreshold <= 0 || maxGenerationThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("maxGenerationThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), 1, 99));
			}
			if (largeObjectHeapThreshold <= 0 || largeObjectHeapThreshold >= 100)
			{
				throw new ArgumentOutOfRangeException("largeObjectHeapThreshold", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("ArgumentOutOfRange_Bounds_Lower_Upper"), 1, 99));
			}
			if (!GC._RegisterForFullGCNotification(maxGenerationThreshold, largeObjectHeapThreshold))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotWithConcurrentGC"));
			}
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0002CFE0 File Offset: 0x0002B1E0
		[SecurityCritical]
		public static void CancelFullGCNotification()
		{
			if (!GC._CancelFullGCNotification())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotWithConcurrentGC"));
			}
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0002CFF9 File Offset: 0x0002B1F9
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach()
		{
			return (GCNotificationStatus)GC._WaitForFullGCApproach(-1);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0002D001 File Offset: 0x0002B201
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCApproach(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return (GCNotificationStatus)GC._WaitForFullGCApproach(millisecondsTimeout);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0002D022 File Offset: 0x0002B222
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete()
		{
			return (GCNotificationStatus)GC._WaitForFullGCComplete(-1);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0002D02A File Offset: 0x0002B22A
		[SecurityCritical]
		public static GCNotificationStatus WaitForFullGCComplete(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return (GCNotificationStatus)GC._WaitForFullGCComplete(millisecondsTimeout);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0002D04C File Offset: 0x0002B24C
		[SecurityCritical]
		private static bool StartNoGCRegionWorker(long totalSize, bool hasLohSize, long lohSize, bool disallowFullBlockingGC)
		{
			GC.StartNoGCRegionStatus startNoGCRegionStatus = (GC.StartNoGCRegionStatus)GC._StartNoGCRegion(totalSize, hasLohSize, lohSize, disallowFullBlockingGC);
			if (startNoGCRegionStatus == GC.StartNoGCRegionStatus.AmountTooLarge)
			{
				throw new ArgumentOutOfRangeException("totalSize", "totalSize is too large. For more information about setting the maximum size, see \"Latency Modes\" in http://go.microsoft.com/fwlink/?LinkId=522706");
			}
			if (startNoGCRegionStatus == GC.StartNoGCRegionStatus.AlreadyInProgress)
			{
				throw new InvalidOperationException("The NoGCRegion mode was already in progress");
			}
			return startNoGCRegionStatus != GC.StartNoGCRegionStatus.NotEnoughMemory;
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0002D08D File Offset: 0x0002B28D
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, false);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0002D099 File Offset: 0x0002B299
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, false);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0002D0A4 File Offset: 0x0002B2A4
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, false, 0L, disallowFullBlockingGC);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0002D0B0 File Offset: 0x0002B2B0
		[SecurityCritical]
		public static bool TryStartNoGCRegion(long totalSize, long lohSize, bool disallowFullBlockingGC)
		{
			return GC.StartNoGCRegionWorker(totalSize, true, lohSize, disallowFullBlockingGC);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0002D0BC File Offset: 0x0002B2BC
		[SecurityCritical]
		private static GC.EndNoGCRegionStatus EndNoGCRegionWorker()
		{
			GC.EndNoGCRegionStatus endNoGCRegionStatus = (GC.EndNoGCRegionStatus)GC._EndNoGCRegion();
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.NotInProgress)
			{
				throw new InvalidOperationException("NoGCRegion mode must be set");
			}
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.GCInduced)
			{
				throw new InvalidOperationException("Garbage collection was induced in NoGCRegion mode");
			}
			if (endNoGCRegionStatus == GC.EndNoGCRegionStatus.AllocationExceeded)
			{
				throw new InvalidOperationException("Allocated memory exceeds specified memory for NoGCRegion mode");
			}
			return GC.EndNoGCRegionStatus.Succeeded;
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0002D0FD File Offset: 0x0002B2FD
		[SecurityCritical]
		public static void EndNoGCRegion()
		{
			GC.EndNoGCRegionWorker();
		}

		// Token: 0x02000ABA RID: 2746
		private enum StartNoGCRegionStatus
		{
			// Token: 0x040030A9 RID: 12457
			Succeeded,
			// Token: 0x040030AA RID: 12458
			NotEnoughMemory,
			// Token: 0x040030AB RID: 12459
			AmountTooLarge,
			// Token: 0x040030AC RID: 12460
			AlreadyInProgress
		}

		// Token: 0x02000ABB RID: 2747
		private enum EndNoGCRegionStatus
		{
			// Token: 0x040030AE RID: 12462
			Succeeded,
			// Token: 0x040030AF RID: 12463
			NotInProgress,
			// Token: 0x040030B0 RID: 12464
			GCInduced,
			// Token: 0x040030B1 RID: 12465
			AllocationExceeded
		}
	}
}
