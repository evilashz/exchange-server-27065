using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020004D5 RID: 1237
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public sealed class Mutex : WaitHandle
	{
		// Token: 0x06003B5A RID: 15194 RVA: 0x000DFA48 File Offset: 0x000DDC48
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public Mutex(bool initiallyOwned, string name, out bool createdNew) : this(initiallyOwned, name, out createdNew, null)
		{
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000DFA54 File Offset: 0x000DDC54
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public unsafe Mutex(bool initiallyOwned, string name, out bool createdNew, MutexSecurity mutexSecurity)
		{
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", new object[]
				{
					name
				}));
			}
			Win32Native.SECURITY_ATTRIBUTES security_ATTRIBUTES = null;
			if (mutexSecurity != null)
			{
				security_ATTRIBUTES = new Win32Native.SECURITY_ATTRIBUTES();
				security_ATTRIBUTES.nLength = Marshal.SizeOf<Win32Native.SECURITY_ATTRIBUTES>(security_ATTRIBUTES);
				byte[] securityDescriptorBinaryForm = mutexSecurity.GetSecurityDescriptorBinaryForm();
				byte* ptr = stackalloc byte[checked(unchecked((UIntPtr)securityDescriptorBinaryForm.Length) * 1)];
				Buffer.Memcpy(ptr, 0, securityDescriptorBinaryForm, 0, securityDescriptorBinaryForm.Length);
				security_ATTRIBUTES.pSecurityDescriptor = ptr;
			}
			this.CreateMutexWithGuaranteedCleanup(initiallyOwned, name, out createdNew, security_ATTRIBUTES);
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000DFAD7 File Offset: 0x000DDCD7
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal Mutex(bool initiallyOwned, string name, out bool createdNew, Win32Native.SECURITY_ATTRIBUTES secAttrs)
		{
			if (name != null && 260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", new object[]
				{
					name
				}));
			}
			this.CreateMutexWithGuaranteedCleanup(initiallyOwned, name, out createdNew, secAttrs);
		}

		// Token: 0x06003B5D RID: 15197 RVA: 0x000DFB14 File Offset: 0x000DDD14
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void CreateMutexWithGuaranteedCleanup(bool initiallyOwned, string name, out bool createdNew, Win32Native.SECURITY_ATTRIBUTES secAttrs)
		{
			RuntimeHelpers.CleanupCode backoutCode = new RuntimeHelpers.CleanupCode(this.MutexCleanupCode);
			Mutex.MutexCleanupInfo mutexCleanupInfo = new Mutex.MutexCleanupInfo(null, false);
			Mutex.MutexTryCodeHelper mutexTryCodeHelper = new Mutex.MutexTryCodeHelper(initiallyOwned, mutexCleanupInfo, name, secAttrs, this);
			RuntimeHelpers.TryCode code = new RuntimeHelpers.TryCode(mutexTryCodeHelper.MutexTryCode);
			RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(code, backoutCode, mutexCleanupInfo);
			createdNew = mutexTryCodeHelper.m_newMutex;
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000DFB60 File Offset: 0x000DDD60
		[SecurityCritical]
		[PrePrepareMethod]
		private void MutexCleanupCode(object userData, bool exceptionThrown)
		{
			Mutex.MutexCleanupInfo mutexCleanupInfo = (Mutex.MutexCleanupInfo)userData;
			if (!this.hasThreadAffinity)
			{
				if (mutexCleanupInfo.mutexHandle != null && !mutexCleanupInfo.mutexHandle.IsInvalid)
				{
					if (mutexCleanupInfo.inCriticalRegion)
					{
						Win32Native.ReleaseMutex(mutexCleanupInfo.mutexHandle);
					}
					mutexCleanupInfo.mutexHandle.Dispose();
				}
				if (mutexCleanupInfo.inCriticalRegion)
				{
					Thread.EndCriticalRegion();
					Thread.EndThreadAffinity();
				}
			}
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x000DFBC2 File Offset: 0x000DDDC2
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public Mutex(bool initiallyOwned, string name) : this(initiallyOwned, name, out Mutex.dummyBool)
		{
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x000DFBD1 File Offset: 0x000DDDD1
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public Mutex(bool initiallyOwned) : this(initiallyOwned, null, out Mutex.dummyBool)
		{
		}

		// Token: 0x06003B61 RID: 15201 RVA: 0x000DFBE0 File Offset: 0x000DDDE0
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public Mutex() : this(false, null, out Mutex.dummyBool)
		{
		}

		// Token: 0x06003B62 RID: 15202 RVA: 0x000DFBEF File Offset: 0x000DDDEF
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private Mutex(SafeWaitHandle handle)
		{
			base.SetHandleInternal(handle);
			this.hasThreadAffinity = true;
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x000DFC05 File Offset: 0x000DDE05
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static Mutex OpenExisting(string name)
		{
			return Mutex.OpenExisting(name, MutexRights.Modify | MutexRights.Synchronize);
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x000DFC14 File Offset: 0x000DDE14
		[SecurityCritical]
		public static Mutex OpenExisting(string name, MutexRights rights)
		{
			Mutex result;
			switch (Mutex.OpenExistingWorker(name, rights, out result))
			{
			case WaitHandle.OpenExistingResult.NameNotFound:
				throw new WaitHandleCannotBeOpenedException();
			case WaitHandle.OpenExistingResult.PathNotFound:
				__Error.WinIOError(3, name);
				return result;
			case WaitHandle.OpenExistingResult.NameInvalid:
				throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
				{
					name
				}));
			default:
				return result;
			}
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x000DFC6B File Offset: 0x000DDE6B
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static bool TryOpenExisting(string name, out Mutex result)
		{
			return Mutex.OpenExistingWorker(name, MutexRights.Modify | MutexRights.Synchronize, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x000DFC7C File Offset: 0x000DDE7C
		[SecurityCritical]
		public static bool TryOpenExisting(string name, MutexRights rights, out Mutex result)
		{
			return Mutex.OpenExistingWorker(name, rights, out result) == WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000DFC8C File Offset: 0x000DDE8C
		[SecurityCritical]
		private static WaitHandle.OpenExistingResult OpenExistingWorker(string name, MutexRights rights, out Mutex result)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", Environment.GetResourceString("ArgumentNull_WithParamName"));
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "name");
			}
			if (260 < name.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WaitHandleNameTooLong", new object[]
				{
					name
				}));
			}
			result = null;
			SafeWaitHandle safeWaitHandle = Win32Native.OpenMutex((int)rights, false, name);
			if (safeWaitHandle.IsInvalid)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (2 == lastWin32Error || 123 == lastWin32Error)
				{
					return WaitHandle.OpenExistingResult.NameNotFound;
				}
				if (3 == lastWin32Error)
				{
					return WaitHandle.OpenExistingResult.PathNotFound;
				}
				if (name != null && name.Length != 0 && 6 == lastWin32Error)
				{
					return WaitHandle.OpenExistingResult.NameInvalid;
				}
				__Error.WinIOError(lastWin32Error, name);
			}
			result = new Mutex(safeWaitHandle);
			return WaitHandle.OpenExistingResult.Success;
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000DFD43 File Offset: 0x000DDF43
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[__DynamicallyInvokable]
		public void ReleaseMutex()
		{
			if (Win32Native.ReleaseMutex(this.safeWaitHandle))
			{
				Thread.EndCriticalRegion();
				Thread.EndThreadAffinity();
				return;
			}
			throw new ApplicationException(Environment.GetResourceString("Arg_SynchronizationLockException"));
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x000DFD70 File Offset: 0x000DDF70
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static int CreateMutexHandle(bool initiallyOwned, string name, Win32Native.SECURITY_ATTRIBUTES securityAttribute, out SafeWaitHandle mutexHandle)
		{
			bool flag = false;
			int num;
			do
			{
				mutexHandle = Win32Native.CreateMutex(securityAttribute, initiallyOwned, name);
				num = Marshal.GetLastWin32Error();
				if (!mutexHandle.IsInvalid || num != 5)
				{
					return num;
				}
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					try
					{
					}
					finally
					{
						Thread.BeginThreadAffinity();
						flag = true;
					}
					mutexHandle = Win32Native.OpenMutex(1048577, false, name);
					if (!mutexHandle.IsInvalid)
					{
						num = 183;
					}
					else
					{
						num = Marshal.GetLastWin32Error();
					}
				}
				finally
				{
					if (flag)
					{
						Thread.EndThreadAffinity();
					}
				}
			}
			while (num == 2);
			if (num == 0)
			{
				num = 183;
			}
			return num;
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x000DFE08 File Offset: 0x000DE008
		[SecuritySafeCritical]
		public MutexSecurity GetAccessControl()
		{
			return new MutexSecurity(this.safeWaitHandle, AccessControlSections.Access | AccessControlSections.Owner | AccessControlSections.Group);
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x000DFE19 File Offset: 0x000DE019
		[SecuritySafeCritical]
		public void SetAccessControl(MutexSecurity mutexSecurity)
		{
			if (mutexSecurity == null)
			{
				throw new ArgumentNullException("mutexSecurity");
			}
			mutexSecurity.Persist(this.safeWaitHandle);
		}

		// Token: 0x040018E8 RID: 6376
		private static bool dummyBool;

		// Token: 0x02000BBA RID: 3002
		internal class MutexTryCodeHelper
		{
			// Token: 0x06006E36 RID: 28214 RVA: 0x0017AF1F File Offset: 0x0017911F
			[SecurityCritical]
			[PrePrepareMethod]
			internal MutexTryCodeHelper(bool initiallyOwned, Mutex.MutexCleanupInfo cleanupInfo, string name, Win32Native.SECURITY_ATTRIBUTES secAttrs, Mutex mutex)
			{
				this.m_initiallyOwned = initiallyOwned;
				this.m_cleanupInfo = cleanupInfo;
				this.m_name = name;
				this.m_secAttrs = secAttrs;
				this.m_mutex = mutex;
			}

			// Token: 0x06006E37 RID: 28215 RVA: 0x0017AF4C File Offset: 0x0017914C
			[SecurityCritical]
			[PrePrepareMethod]
			internal void MutexTryCode(object userData)
			{
				SafeWaitHandle safeWaitHandle = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (this.m_initiallyOwned)
					{
						this.m_cleanupInfo.inCriticalRegion = true;
						Thread.BeginThreadAffinity();
						Thread.BeginCriticalRegion();
					}
				}
				int num = 0;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					num = Mutex.CreateMutexHandle(this.m_initiallyOwned, this.m_name, this.m_secAttrs, out safeWaitHandle);
				}
				if (safeWaitHandle.IsInvalid)
				{
					safeWaitHandle.SetHandleAsInvalid();
					if (this.m_name != null && this.m_name.Length != 0 && 6 == num)
					{
						throw new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
						{
							this.m_name
						}));
					}
					__Error.WinIOError(num, this.m_name);
				}
				this.m_newMutex = (num != 183);
				this.m_mutex.SetHandleInternal(safeWaitHandle);
				this.m_mutex.hasThreadAffinity = true;
			}

			// Token: 0x04003531 RID: 13617
			private bool m_initiallyOwned;

			// Token: 0x04003532 RID: 13618
			private Mutex.MutexCleanupInfo m_cleanupInfo;

			// Token: 0x04003533 RID: 13619
			internal bool m_newMutex;

			// Token: 0x04003534 RID: 13620
			private string m_name;

			// Token: 0x04003535 RID: 13621
			[SecurityCritical]
			private Win32Native.SECURITY_ATTRIBUTES m_secAttrs;

			// Token: 0x04003536 RID: 13622
			private Mutex m_mutex;
		}

		// Token: 0x02000BBB RID: 3003
		internal class MutexCleanupInfo
		{
			// Token: 0x06006E38 RID: 28216 RVA: 0x0017B03C File Offset: 0x0017923C
			[SecurityCritical]
			internal MutexCleanupInfo(SafeWaitHandle mutexHandle, bool inCriticalRegion)
			{
				this.mutexHandle = mutexHandle;
				this.inCriticalRegion = inCriticalRegion;
			}

			// Token: 0x04003537 RID: 13623
			[SecurityCritical]
			internal SafeWaitHandle mutexHandle;

			// Token: 0x04003538 RID: 13624
			internal bool inCriticalRegion;
		}
	}
}
