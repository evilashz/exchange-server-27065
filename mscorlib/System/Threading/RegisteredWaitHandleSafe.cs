using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.Threading
{
	// Token: 0x020004F0 RID: 1264
	internal sealed class RegisteredWaitHandleSafe : CriticalFinalizerObject
	{
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06003CAD RID: 15533 RVA: 0x000E28A4 File Offset: 0x000E0AA4
		private static IntPtr InvalidHandle
		{
			[SecuritySafeCritical]
			get
			{
				return Win32Native.INVALID_HANDLE_VALUE;
			}
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x000E28AB File Offset: 0x000E0AAB
		internal RegisteredWaitHandleSafe()
		{
			this.registeredWaitHandle = RegisteredWaitHandleSafe.InvalidHandle;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x000E28BE File Offset: 0x000E0ABE
		internal IntPtr GetHandle()
		{
			return this.registeredWaitHandle;
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000E28C6 File Offset: 0x000E0AC6
		internal void SetHandle(IntPtr handle)
		{
			this.registeredWaitHandle = handle;
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x000E28D0 File Offset: 0x000E0AD0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void SetWaitObject(WaitHandle waitObject)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.m_internalWaitObject = waitObject;
				if (waitObject != null)
				{
					this.m_internalWaitObject.SafeWaitHandle.DangerousAddRef(ref this.bReleaseNeeded);
				}
			}
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x000E2918 File Offset: 0x000E0B18
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal bool Unregister(WaitHandle waitObject)
		{
			bool flag = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag2 = false;
				do
				{
					if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
					{
						flag2 = true;
						try
						{
							if (this.ValidHandle())
							{
								flag = RegisteredWaitHandleSafe.UnregisterWaitNative(this.GetHandle(), (waitObject == null) ? null : waitObject.SafeWaitHandle);
								if (flag)
								{
									if (this.bReleaseNeeded)
									{
										this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
										this.bReleaseNeeded = false;
									}
									this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
									this.m_internalWaitObject = null;
									GC.SuppressFinalize(this);
								}
							}
						}
						finally
						{
							this.m_lock = 0;
						}
					}
					Thread.SpinWait(1);
				}
				while (!flag2);
			}
			return flag;
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x000E29D4 File Offset: 0x000E0BD4
		private bool ValidHandle()
		{
			return this.registeredWaitHandle != RegisteredWaitHandleSafe.InvalidHandle && this.registeredWaitHandle != IntPtr.Zero;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x000E29FC File Offset: 0x000E0BFC
		[SecuritySafeCritical]
		~RegisteredWaitHandleSafe()
		{
			if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
			{
				try
				{
					if (this.ValidHandle())
					{
						RegisteredWaitHandleSafe.WaitHandleCleanupNative(this.registeredWaitHandle);
						if (this.bReleaseNeeded)
						{
							this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
							this.bReleaseNeeded = false;
						}
						this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
						this.m_internalWaitObject = null;
					}
				}
				finally
				{
					this.m_lock = 0;
				}
			}
		}

		// Token: 0x06003CB5 RID: 15541
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitHandleCleanupNative(IntPtr handle);

		// Token: 0x06003CB6 RID: 15542
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnregisterWaitNative(IntPtr handle, SafeHandle waitObject);

		// Token: 0x04001954 RID: 6484
		private IntPtr registeredWaitHandle;

		// Token: 0x04001955 RID: 6485
		private WaitHandle m_internalWaitObject;

		// Token: 0x04001956 RID: 6486
		private bool bReleaseNeeded;

		// Token: 0x04001957 RID: 6487
		private volatile int m_lock;
	}
}
