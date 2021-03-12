using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000506 RID: 1286
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public abstract class WaitHandle : MarshalByRefObject, IDisposable
	{
		// Token: 0x06003D60 RID: 15712 RVA: 0x000E41A9 File Offset: 0x000E23A9
		[SecuritySafeCritical]
		private static IntPtr GetInvalidHandle()
		{
			return Win32Native.INVALID_HANDLE_VALUE;
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000E41B0 File Offset: 0x000E23B0
		[__DynamicallyInvokable]
		protected WaitHandle()
		{
			this.Init();
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x000E41BE File Offset: 0x000E23BE
		[SecuritySafeCritical]
		private void Init()
		{
			this.safeWaitHandle = null;
			this.waitHandle = WaitHandle.InvalidHandle;
			this.hasThreadAffinity = false;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06003D63 RID: 15715 RVA: 0x000E41DB File Offset: 0x000E23DB
		// (set) Token: 0x06003D64 RID: 15716 RVA: 0x000E41FC File Offset: 0x000E23FC
		[Obsolete("Use the SafeWaitHandle property instead.")]
		public virtual IntPtr Handle
		{
			[SecuritySafeCritical]
			get
			{
				if (this.safeWaitHandle != null)
				{
					return this.safeWaitHandle.DangerousGetHandle();
				}
				return WaitHandle.InvalidHandle;
			}
			[SecurityCritical]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				if (value == WaitHandle.InvalidHandle)
				{
					if (this.safeWaitHandle != null)
					{
						this.safeWaitHandle.SetHandleAsInvalid();
						this.safeWaitHandle = null;
					}
				}
				else
				{
					this.safeWaitHandle = new SafeWaitHandle(value, true);
				}
				this.waitHandle = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06003D65 RID: 15717 RVA: 0x000E424E File Offset: 0x000E244E
		// (set) Token: 0x06003D66 RID: 15718 RVA: 0x000E4278 File Offset: 0x000E2478
		public SafeWaitHandle SafeWaitHandle
		{
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				if (this.safeWaitHandle == null)
				{
					this.safeWaitHandle = new SafeWaitHandle(WaitHandle.InvalidHandle, false);
				}
				return this.safeWaitHandle;
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (value == null)
					{
						this.safeWaitHandle = null;
						this.waitHandle = WaitHandle.InvalidHandle;
					}
					else
					{
						this.safeWaitHandle = value;
						this.waitHandle = this.safeWaitHandle.DangerousGetHandle();
					}
				}
			}
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x000E42D4 File Offset: 0x000E24D4
		[SecurityCritical]
		internal void SetHandleInternal(SafeWaitHandle handle)
		{
			this.safeWaitHandle = handle;
			this.waitHandle = handle.DangerousGetHandle();
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x000E42EB File Offset: 0x000E24EB
		public virtual bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return this.WaitOne((long)millisecondsTimeout, exitContext);
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x000E4310 File Offset: 0x000E2510
		public virtual bool WaitOne(TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return this.WaitOne(num, exitContext);
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x000E4351 File Offset: 0x000E2551
		[__DynamicallyInvokable]
		public virtual bool WaitOne()
		{
			return this.WaitOne(-1, false);
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x000E435B File Offset: 0x000E255B
		[__DynamicallyInvokable]
		public virtual bool WaitOne(int millisecondsTimeout)
		{
			return this.WaitOne(millisecondsTimeout, false);
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x000E4365 File Offset: 0x000E2565
		[__DynamicallyInvokable]
		public virtual bool WaitOne(TimeSpan timeout)
		{
			return this.WaitOne(timeout, false);
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x000E436F File Offset: 0x000E256F
		[SecuritySafeCritical]
		private bool WaitOne(long timeout, bool exitContext)
		{
			return WaitHandle.InternalWaitOne(this.safeWaitHandle, timeout, this.hasThreadAffinity, exitContext);
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000E4388 File Offset: 0x000E2588
		[SecurityCritical]
		internal static bool InternalWaitOne(SafeHandle waitableSafeHandle, long millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			if (waitableSafeHandle == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			int num = WaitHandle.WaitOneNative(waitableSafeHandle, (uint)millisecondsTimeout, hasThreadAffinity, exitContext);
			if (AppDomainPauseManager.IsPaused)
			{
				AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
			}
			if (num == 128)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			return num != 258;
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x000E43E0 File Offset: 0x000E25E0
		[SecurityCritical]
		internal bool WaitOneWithoutFAS()
		{
			if (this.safeWaitHandle == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_Generic"));
			}
			long num = -1L;
			int num2 = WaitHandle.WaitOneNative(this.safeWaitHandle, (uint)num, this.hasThreadAffinity, false);
			if (num2 == 128)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			return num2 != 258;
		}

		// Token: 0x06003D70 RID: 15728
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WaitOneNative(SafeHandle waitableSafeHandle, uint millisecondsTimeout, bool hasThreadAffinity, bool exitContext);

		// Token: 0x06003D71 RID: 15729
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WaitMultiple(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext, bool WaitAll);

		// Token: 0x06003D72 RID: 15730 RVA: 0x000E443C File Offset: 0x000E263C
		[SecuritySafeCritical]
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Waithandles"));
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentNullException(Environment.GetResourceString("Argument_EmptyWaithandleArray"));
			}
			if (waitHandles.Length > 64)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_MaxWaitHandles"));
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			WaitHandle[] array = new WaitHandle[waitHandles.Length];
			for (int i = 0; i < waitHandles.Length; i++)
			{
				WaitHandle waitHandle = waitHandles[i];
				if (waitHandle == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_ArrayElement"));
				}
				if (RemotingServices.IsTransparentProxy(waitHandle))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
				}
				array[i] = waitHandle;
			}
			int num = WaitHandle.WaitMultiple(array, millisecondsTimeout, exitContext, true);
			if (AppDomainPauseManager.IsPaused)
			{
				AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
			}
			if (128 <= num && 128 + array.Length > num)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			GC.KeepAlive(array);
			return num != 258;
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000E4530 File Offset: 0x000E2730
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return WaitHandle.WaitAll(waitHandles, (int)num, exitContext);
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000E4572 File Offset: 0x000E2772
		[__DynamicallyInvokable]
		public static bool WaitAll(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitAll(waitHandles, -1, true);
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000E457C File Offset: 0x000E277C
		[__DynamicallyInvokable]
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitAll(waitHandles, millisecondsTimeout, true);
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000E4586 File Offset: 0x000E2786
		[__DynamicallyInvokable]
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitAll(waitHandles, timeout, true);
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000E4590 File Offset: 0x000E2790
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Waithandles"));
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyWaithandleArray"));
			}
			if (64 < waitHandles.Length)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_MaxWaitHandles"));
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			WaitHandle[] array = new WaitHandle[waitHandles.Length];
			for (int i = 0; i < waitHandles.Length; i++)
			{
				WaitHandle waitHandle = waitHandles[i];
				if (waitHandle == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_ArrayElement"));
				}
				if (RemotingServices.IsTransparentProxy(waitHandle))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_WaitOnTransparentProxy"));
				}
				array[i] = waitHandle;
			}
			int num = WaitHandle.WaitMultiple(array, millisecondsTimeout, exitContext, false);
			if (AppDomainPauseManager.IsPaused)
			{
				AppDomainPauseManager.ResumeEvent.WaitOneWithoutFAS();
			}
			if (128 <= num && 128 + array.Length > num)
			{
				int num2 = num - 128;
				if (0 <= num2 && num2 < array.Length)
				{
					WaitHandle.ThrowAbandonedMutexException(num2, array[num2]);
				}
				else
				{
					WaitHandle.ThrowAbandonedMutexException();
				}
			}
			GC.KeepAlive(array);
			return num;
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x000E469C File Offset: 0x000E289C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return WaitHandle.WaitAny(waitHandles, (int)num, exitContext);
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x000E46DE File Offset: 0x000E28DE
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitAny(waitHandles, timeout, true);
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000E46E8 File Offset: 0x000E28E8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int WaitAny(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitAny(waitHandles, -1, true);
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000E46F2 File Offset: 0x000E28F2
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitAny(waitHandles, millisecondsTimeout, true);
		}

		// Token: 0x06003D7C RID: 15740
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SignalAndWaitOne(SafeWaitHandle waitHandleToSignal, SafeWaitHandle waitHandleToWaitOn, int millisecondsTimeout, bool hasThreadAffinity, bool exitContext);

		// Token: 0x06003D7D RID: 15741 RVA: 0x000E46FC File Offset: 0x000E28FC
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn)
		{
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, -1, false);
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x000E4708 File Offset: 0x000E2908
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, (int)num, exitContext);
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x000E474C File Offset: 0x000E294C
		[SecuritySafeCritical]
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, int millisecondsTimeout, bool exitContext)
		{
			if (toSignal == null)
			{
				throw new ArgumentNullException("toSignal");
			}
			if (toWaitOn == null)
			{
				throw new ArgumentNullException("toWaitOn");
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegOrNegative1"));
			}
			int num = WaitHandle.SignalAndWaitOne(toSignal.safeWaitHandle, toWaitOn.safeWaitHandle, millisecondsTimeout, toWaitOn.hasThreadAffinity, exitContext);
			if (2147483647 != num && toSignal.hasThreadAffinity)
			{
				Thread.EndCriticalRegion();
				Thread.EndThreadAffinity();
			}
			if (128 == num)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			if (298 == num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Threading.WaitHandleTooManyPosts"));
			}
			return num == 0;
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x000E47F1 File Offset: 0x000E29F1
		private static void ThrowAbandonedMutexException()
		{
			throw new AbandonedMutexException();
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x000E47F8 File Offset: 0x000E29F8
		private static void ThrowAbandonedMutexException(int location, WaitHandle handle)
		{
			throw new AbandonedMutexException(location, handle);
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x000E4801 File Offset: 0x000E2A01
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x000E4810 File Offset: 0x000E2A10
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool explicitDisposing)
		{
			if (this.safeWaitHandle != null)
			{
				this.safeWaitHandle.Close();
			}
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x000E4829 File Offset: 0x000E2A29
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400198E RID: 6542
		[__DynamicallyInvokable]
		public const int WaitTimeout = 258;

		// Token: 0x0400198F RID: 6543
		private const int MAX_WAITHANDLES = 64;

		// Token: 0x04001990 RID: 6544
		private IntPtr waitHandle;

		// Token: 0x04001991 RID: 6545
		[SecurityCritical]
		internal volatile SafeWaitHandle safeWaitHandle;

		// Token: 0x04001992 RID: 6546
		internal bool hasThreadAffinity;

		// Token: 0x04001993 RID: 6547
		protected static readonly IntPtr InvalidHandle = WaitHandle.GetInvalidHandle();

		// Token: 0x04001994 RID: 6548
		private const int WAIT_OBJECT_0 = 0;

		// Token: 0x04001995 RID: 6549
		private const int WAIT_ABANDONED = 128;

		// Token: 0x04001996 RID: 6550
		private const int WAIT_FAILED = 2147483647;

		// Token: 0x04001997 RID: 6551
		private const int ERROR_TOO_MANY_POSTS = 298;

		// Token: 0x02000BC3 RID: 3011
		internal enum OpenExistingResult
		{
			// Token: 0x04003557 RID: 13655
			Success,
			// Token: 0x04003558 RID: 13656
			NameNotFound,
			// Token: 0x04003559 RID: 13657
			PathNotFound,
			// Token: 0x0400355A RID: 13658
			NameInvalid
		}
	}
}
