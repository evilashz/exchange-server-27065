using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004CC RID: 1228
	[__DynamicallyInvokable]
	public static class Interlocked
	{
		// Token: 0x06003B00 RID: 15104 RVA: 0x000DF497 File Offset: 0x000DD697
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Increment(ref int location)
		{
			return Interlocked.Add(ref location, 1);
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x000DF4A0 File Offset: 0x000DD6A0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static long Increment(ref long location)
		{
			return Interlocked.Add(ref location, 1L);
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x000DF4AA File Offset: 0x000DD6AA
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Decrement(ref int location)
		{
			return Interlocked.Add(ref location, -1);
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000DF4B3 File Offset: 0x000DD6B3
		[__DynamicallyInvokable]
		public static long Decrement(ref long location)
		{
			return Interlocked.Add(ref location, -1L);
		}

		// Token: 0x06003B04 RID: 15108
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Exchange(ref int location1, int value);

		// Token: 0x06003B05 RID: 15109
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Exchange(ref long location1, long value);

		// Token: 0x06003B06 RID: 15110
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exchange(ref float location1, float value);

		// Token: 0x06003B07 RID: 15111
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exchange(ref double location1, double value);

		// Token: 0x06003B08 RID: 15112
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object Exchange(ref object location1, object value);

		// Token: 0x06003B09 RID: 15113
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Exchange(ref IntPtr location1, IntPtr value);

		// Token: 0x06003B0A RID: 15114 RVA: 0x000DF4BD File Offset: 0x000DD6BD
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[ComVisible(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static T Exchange<T>(ref T location1, T value) where T : class
		{
			Interlocked._Exchange(__makeref(location1), __makeref(value));
			return value;
		}

		// Token: 0x06003B0B RID: 15115
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _Exchange(TypedReference location1, TypedReference value);

		// Token: 0x06003B0C RID: 15116
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CompareExchange(ref int location1, int value, int comparand);

		// Token: 0x06003B0D RID: 15117
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CompareExchange(ref long location1, long value, long comparand);

		// Token: 0x06003B0E RID: 15118
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CompareExchange(ref float location1, float value, float comparand);

		// Token: 0x06003B0F RID: 15119
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CompareExchange(ref double location1, double value, double comparand);

		// Token: 0x06003B10 RID: 15120
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object CompareExchange(ref object location1, object value, object comparand);

		// Token: 0x06003B11 RID: 15121
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand);

		// Token: 0x06003B12 RID: 15122 RVA: 0x000DF4D2 File Offset: 0x000DD6D2
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[ComVisible(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static T CompareExchange<T>(ref T location1, T value, T comparand) where T : class
		{
			Interlocked._CompareExchange(__makeref(location1), __makeref(value), comparand);
			return value;
		}

		// Token: 0x06003B13 RID: 15123
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _CompareExchange(TypedReference location1, TypedReference value, object comparand);

		// Token: 0x06003B14 RID: 15124
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CompareExchange(ref int location1, int value, int comparand, ref bool succeeded);

		// Token: 0x06003B15 RID: 15125
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ExchangeAdd(ref int location1, int value);

		// Token: 0x06003B16 RID: 15126
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long ExchangeAdd(ref long location1, long value);

		// Token: 0x06003B17 RID: 15127 RVA: 0x000DF4ED File Offset: 0x000DD6ED
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Add(ref int location1, int value)
		{
			return Interlocked.ExchangeAdd(ref location1, value) + value;
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x000DF4F8 File Offset: 0x000DD6F8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static long Add(ref long location1, long value)
		{
			return Interlocked.ExchangeAdd(ref location1, value) + value;
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000DF503 File Offset: 0x000DD703
		[__DynamicallyInvokable]
		public static long Read(ref long location)
		{
			return Interlocked.CompareExchange(ref location, 0L, 0L);
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000DF50F File Offset: 0x000DD70F
		[__DynamicallyInvokable]
		public static void MemoryBarrier()
		{
			Thread.MemoryBarrier();
		}

		// Token: 0x06003B1B RID: 15131
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SpeculationBarrier();
	}
}
