using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x020004D4 RID: 1236
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public static class Monitor
	{
		// Token: 0x06003B41 RID: 15169
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Enter(object obj);

		// Token: 0x06003B42 RID: 15170 RVA: 0x000DF8D8 File Offset: 0x000DDAD8
		[__DynamicallyInvokable]
		public static void Enter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnter(obj, ref lockTaken);
		}

		// Token: 0x06003B43 RID: 15171 RVA: 0x000DF8EA File Offset: 0x000DDAEA
		private static void ThrowLockTakenException()
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_MustBeFalse"), "lockTaken");
		}

		// Token: 0x06003B44 RID: 15172
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReliableEnter(object obj, ref bool lockTaken);

		// Token: 0x06003B45 RID: 15173
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[__DynamicallyInvokable]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Exit(object obj);

		// Token: 0x06003B46 RID: 15174 RVA: 0x000DF900 File Offset: 0x000DDB00
		[__DynamicallyInvokable]
		public static bool TryEnter(object obj)
		{
			bool result = false;
			Monitor.TryEnter(obj, 0, ref result);
			return result;
		}

		// Token: 0x06003B47 RID: 15175 RVA: 0x000DF919 File Offset: 0x000DDB19
		[__DynamicallyInvokable]
		public static void TryEnter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, 0, ref lockTaken);
		}

		// Token: 0x06003B48 RID: 15176 RVA: 0x000DF92C File Offset: 0x000DDB2C
		[__DynamicallyInvokable]
		public static bool TryEnter(object obj, int millisecondsTimeout)
		{
			bool result = false;
			Monitor.TryEnter(obj, millisecondsTimeout, ref result);
			return result;
		}

		// Token: 0x06003B49 RID: 15177 RVA: 0x000DF948 File Offset: 0x000DDB48
		private static int MillisecondsTimeoutFromTimeSpan(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return (int)num;
		}

		// Token: 0x06003B4A RID: 15178 RVA: 0x000DF983 File Offset: 0x000DDB83
		[__DynamicallyInvokable]
		public static bool TryEnter(object obj, TimeSpan timeout)
		{
			return Monitor.TryEnter(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout));
		}

		// Token: 0x06003B4B RID: 15179 RVA: 0x000DF991 File Offset: 0x000DDB91
		[__DynamicallyInvokable]
		public static void TryEnter(object obj, int millisecondsTimeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, millisecondsTimeout, ref lockTaken);
		}

		// Token: 0x06003B4C RID: 15180 RVA: 0x000DF9A4 File Offset: 0x000DDBA4
		[__DynamicallyInvokable]
		public static void TryEnter(object obj, TimeSpan timeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), ref lockTaken);
		}

		// Token: 0x06003B4D RID: 15181
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReliableEnterTimeout(object obj, int timeout, ref bool lockTaken);

		// Token: 0x06003B4E RID: 15182 RVA: 0x000DF9BC File Offset: 0x000DDBBC
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static bool IsEntered(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.IsEnteredNative(obj);
		}

		// Token: 0x06003B4F RID: 15183
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEnteredNative(object obj);

		// Token: 0x06003B50 RID: 15184
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ObjWait(bool exitContext, int millisecondsTimeout, object obj);

		// Token: 0x06003B51 RID: 15185 RVA: 0x000DF9D2 File Offset: 0x000DDBD2
		[SecuritySafeCritical]
		public static bool Wait(object obj, int millisecondsTimeout, bool exitContext)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.ObjWait(exitContext, millisecondsTimeout, obj);
		}

		// Token: 0x06003B52 RID: 15186 RVA: 0x000DF9EA File Offset: 0x000DDBEA
		public static bool Wait(object obj, TimeSpan timeout, bool exitContext)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), exitContext);
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x000DF9F9 File Offset: 0x000DDBF9
		[__DynamicallyInvokable]
		public static bool Wait(object obj, int millisecondsTimeout)
		{
			return Monitor.Wait(obj, millisecondsTimeout, false);
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000DFA03 File Offset: 0x000DDC03
		[__DynamicallyInvokable]
		public static bool Wait(object obj, TimeSpan timeout)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), false);
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000DFA12 File Offset: 0x000DDC12
		[__DynamicallyInvokable]
		public static bool Wait(object obj)
		{
			return Monitor.Wait(obj, -1, false);
		}

		// Token: 0x06003B56 RID: 15190
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ObjPulse(object obj);

		// Token: 0x06003B57 RID: 15191 RVA: 0x000DFA1C File Offset: 0x000DDC1C
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void Pulse(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulse(obj);
		}

		// Token: 0x06003B58 RID: 15192
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ObjPulseAll(object obj);

		// Token: 0x06003B59 RID: 15193 RVA: 0x000DFA32 File Offset: 0x000DDC32
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static void PulseAll(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulseAll(obj);
		}
	}
}
