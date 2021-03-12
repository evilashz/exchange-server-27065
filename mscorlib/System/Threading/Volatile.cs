using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	// Token: 0x02000505 RID: 1285
	[__DynamicallyInvokable]
	public static class Volatile
	{
		// Token: 0x06003D44 RID: 15684 RVA: 0x000E3FBC File Offset: 0x000E21BC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static bool Read(ref bool location)
		{
			bool result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D45 RID: 15685 RVA: 0x000E3FD4 File Offset: 0x000E21D4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static sbyte Read(ref sbyte location)
		{
			sbyte result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D46 RID: 15686 RVA: 0x000E3FEC File Offset: 0x000E21EC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static byte Read(ref byte location)
		{
			byte result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D47 RID: 15687 RVA: 0x000E4004 File Offset: 0x000E2204
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static short Read(ref short location)
		{
			short result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x000E401C File Offset: 0x000E221C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static ushort Read(ref ushort location)
		{
			ushort result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x000E4034 File Offset: 0x000E2234
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static int Read(ref int location)
		{
			int result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x000E404C File Offset: 0x000E224C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static uint Read(ref uint location)
		{
			uint result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x000E4062 File Offset: 0x000E2262
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static long Read(ref long location)
		{
			return Interlocked.CompareExchange(ref location, 0L, 0L);
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000E4070 File Offset: 0x000E2270
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static ulong Read(ref ulong location)
		{
			fixed (ulong* ptr = &location)
			{
				return (ulong)Interlocked.CompareExchange(ref *(long*)ptr, 0L, 0L);
			}
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000E408C File Offset: 0x000E228C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr Read(ref IntPtr location)
		{
			IntPtr result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D4E RID: 15694 RVA: 0x000E40A4 File Offset: 0x000E22A4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static UIntPtr Read(ref UIntPtr location)
		{
			UIntPtr result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D4F RID: 15695 RVA: 0x000E40BC File Offset: 0x000E22BC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static float Read(ref float location)
		{
			float result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D50 RID: 15696 RVA: 0x000E40D2 File Offset: 0x000E22D2
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static double Read(ref double location)
		{
			return Interlocked.CompareExchange(ref location, 0.0, 0.0);
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x000E40EC File Offset: 0x000E22EC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static T Read<T>(ref T location) where T : class
		{
			T result = location;
			Thread.MemoryBarrier();
			return result;
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000E4106 File Offset: 0x000E2306
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref bool location, bool value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000E4110 File Offset: 0x000E2310
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref sbyte location, sbyte value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000E411A File Offset: 0x000E231A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref byte location, byte value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000E4124 File Offset: 0x000E2324
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref short location, short value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000E412E File Offset: 0x000E232E
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref ushort location, ushort value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000E4138 File Offset: 0x000E2338
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref int location, int value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000E4142 File Offset: 0x000E2342
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[__DynamicallyInvokable]
		public static void Write(ref uint location, uint value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000E414C File Offset: 0x000E234C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref long location, long value)
		{
			Interlocked.Exchange(ref location, value);
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000E4158 File Offset: 0x000E2358
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public unsafe static void Write(ref ulong location, ulong value)
		{
			fixed (ulong* ptr = &location)
			{
				Interlocked.Exchange(ref *(long*)ptr, (long)value);
			}
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000E4173 File Offset: 0x000E2373
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void Write(ref IntPtr location, IntPtr value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000E417D File Offset: 0x000E237D
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static void Write(ref UIntPtr location, UIntPtr value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000E4187 File Offset: 0x000E2387
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref float location, float value)
		{
			Thread.MemoryBarrier();
			location = value;
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x000E4191 File Offset: 0x000E2391
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		public static void Write(ref double location, double value)
		{
			Interlocked.Exchange(ref location, value);
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000E419B File Offset: 0x000E239B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Write<T>(ref T location, T value) where T : class
		{
			Thread.MemoryBarrier();
			location = value;
		}
	}
}
