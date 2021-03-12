using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x0200022A RID: 554
	internal sealed class Privilege
	{
		// Token: 0x0600200A RID: 8202 RVA: 0x00070948 File Offset: 0x0006EB48
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static Win32Native.LUID LuidFromPrivilege(string privilege)
		{
			Win32Native.LUID luid;
			luid.LowPart = 0U;
			luid.HighPart = 0U;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				Privilege.privilegeLock.AcquireReaderLock(-1);
				if (Privilege.luids.Contains(privilege))
				{
					luid = (Win32Native.LUID)Privilege.luids[privilege];
					Privilege.privilegeLock.ReleaseReaderLock();
				}
				else
				{
					Privilege.privilegeLock.ReleaseReaderLock();
					if (!Win32Native.LookupPrivilegeValue(null, privilege, ref luid))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 8)
						{
							throw new OutOfMemoryException();
						}
						if (lastWin32Error == 5)
						{
							throw new UnauthorizedAccessException();
						}
						if (lastWin32Error == 1313)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPrivilegeName", new object[]
							{
								privilege
							}));
						}
						throw new InvalidOperationException();
					}
					else
					{
						Privilege.privilegeLock.AcquireWriterLock(-1);
					}
				}
			}
			finally
			{
				if (Privilege.privilegeLock.IsReaderLockHeld)
				{
					Privilege.privilegeLock.ReleaseReaderLock();
				}
				if (Privilege.privilegeLock.IsWriterLockHeld)
				{
					if (!Privilege.luids.Contains(privilege))
					{
						Privilege.luids[privilege] = luid;
						Privilege.privileges[luid] = privilege;
					}
					Privilege.privilegeLock.ReleaseWriterLock();
				}
			}
			return luid;
		}

		// Token: 0x0600200B RID: 8203 RVA: 0x00070A74 File Offset: 0x0006EC74
		[SecurityCritical]
		public Privilege(string privilegeName)
		{
			if (privilegeName == null)
			{
				throw new ArgumentNullException("privilegeName");
			}
			this.luid = Privilege.LuidFromPrivilege(privilegeName);
		}

		// Token: 0x0600200C RID: 8204 RVA: 0x00070AA4 File Offset: 0x0006ECA4
		[SecuritySafeCritical]
		~Privilege()
		{
			if (this.needToRevert)
			{
				this.Revert();
			}
		}

		// Token: 0x0600200D RID: 8205 RVA: 0x00070AD8 File Offset: 0x0006ECD8
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Enable()
		{
			this.ToggleState(true);
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x0600200E RID: 8206 RVA: 0x00070AE1 File Offset: 0x0006ECE1
		public bool NeedToRevert
		{
			get
			{
				return this.needToRevert;
			}
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x00070AEC File Offset: 0x0006ECEC
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private void ToggleState(bool enable)
		{
			int num = 0;
			if (!this.currentThread.Equals(Thread.CurrentThread))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustBeSameThread"));
			}
			if (this.needToRevert)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustRevertPrivilege"));
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				try
				{
					this.tlsContents = (Thread.GetData(Privilege.tlsSlot) as Privilege.TlsContents);
					if (this.tlsContents == null)
					{
						this.tlsContents = new Privilege.TlsContents();
						Thread.SetData(Privilege.tlsSlot, this.tlsContents);
					}
					else
					{
						this.tlsContents.IncrementReferenceCount();
					}
					Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE = default(Win32Native.TOKEN_PRIVILEGE);
					token_PRIVILEGE.PrivilegeCount = 1U;
					token_PRIVILEGE.Privilege.Luid = this.luid;
					token_PRIVILEGE.Privilege.Attributes = (enable ? 2U : 0U);
					Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE2 = default(Win32Native.TOKEN_PRIVILEGE);
					uint num2 = 0U;
					if (!Win32Native.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf<Win32Native.TOKEN_PRIVILEGE>(token_PRIVILEGE2), ref token_PRIVILEGE2, ref num2))
					{
						num = Marshal.GetLastWin32Error();
					}
					else if (1300 == Marshal.GetLastWin32Error())
					{
						num = 1300;
					}
					else
					{
						this.initialState = ((token_PRIVILEGE2.Privilege.Attributes & 2U) > 0U);
						this.stateWasChanged = (this.initialState != enable);
						this.needToRevert = (this.tlsContents.IsImpersonating || this.stateWasChanged);
					}
				}
				finally
				{
					if (!this.needToRevert)
					{
						this.Reset();
					}
				}
			}
			if (num == 1300)
			{
				throw new PrivilegeNotHeldException(Privilege.privileges[this.luid] as string);
			}
			if (num == 8)
			{
				throw new OutOfMemoryException();
			}
			if (num == 5 || num == 1347)
			{
				throw new UnauthorizedAccessException();
			}
			if (num != 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002010 RID: 8208 RVA: 0x00070CD8 File Offset: 0x0006EED8
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void Revert()
		{
			int num = 0;
			if (!this.currentThread.Equals(Thread.CurrentThread))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MustBeSameThread"));
			}
			if (!this.NeedToRevert)
			{
				return;
			}
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				bool flag = true;
				try
				{
					if (this.stateWasChanged && (this.tlsContents.ReferenceCountValue > 1 || !this.tlsContents.IsImpersonating))
					{
						Win32Native.TOKEN_PRIVILEGE token_PRIVILEGE = default(Win32Native.TOKEN_PRIVILEGE);
						token_PRIVILEGE.PrivilegeCount = 1U;
						token_PRIVILEGE.Privilege.Luid = this.luid;
						token_PRIVILEGE.Privilege.Attributes = (this.initialState ? 2U : 0U);
						Win32Native.TOKEN_PRIVILEGE structure = default(Win32Native.TOKEN_PRIVILEGE);
						uint num2 = 0U;
						if (!Win32Native.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf<Win32Native.TOKEN_PRIVILEGE>(structure), ref structure, ref num2))
						{
							num = Marshal.GetLastWin32Error();
							flag = false;
						}
					}
				}
				finally
				{
					if (flag)
					{
						this.Reset();
					}
				}
			}
			if (num == 8)
			{
				throw new OutOfMemoryException();
			}
			if (num == 5)
			{
				throw new UnauthorizedAccessException();
			}
			if (num != 0)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x00070DF8 File Offset: 0x0006EFF8
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Reset()
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this.stateWasChanged = false;
				this.initialState = false;
				this.needToRevert = false;
				if (this.tlsContents != null && this.tlsContents.DecrementReferenceCount() == 0)
				{
					this.tlsContents = null;
					Thread.SetData(Privilege.tlsSlot, null);
				}
			}
		}

		// Token: 0x04000B65 RID: 2917
		private static LocalDataStoreSlot tlsSlot = Thread.AllocateDataSlot();

		// Token: 0x04000B66 RID: 2918
		private static Hashtable privileges = new Hashtable();

		// Token: 0x04000B67 RID: 2919
		private static Hashtable luids = new Hashtable();

		// Token: 0x04000B68 RID: 2920
		private static ReaderWriterLock privilegeLock = new ReaderWriterLock();

		// Token: 0x04000B69 RID: 2921
		private bool needToRevert;

		// Token: 0x04000B6A RID: 2922
		private bool initialState;

		// Token: 0x04000B6B RID: 2923
		private bool stateWasChanged;

		// Token: 0x04000B6C RID: 2924
		[SecurityCritical]
		private Win32Native.LUID luid;

		// Token: 0x04000B6D RID: 2925
		private readonly Thread currentThread = Thread.CurrentThread;

		// Token: 0x04000B6E RID: 2926
		private Privilege.TlsContents tlsContents;

		// Token: 0x04000B6F RID: 2927
		public const string CreateToken = "SeCreateTokenPrivilege";

		// Token: 0x04000B70 RID: 2928
		public const string AssignPrimaryToken = "SeAssignPrimaryTokenPrivilege";

		// Token: 0x04000B71 RID: 2929
		public const string LockMemory = "SeLockMemoryPrivilege";

		// Token: 0x04000B72 RID: 2930
		public const string IncreaseQuota = "SeIncreaseQuotaPrivilege";

		// Token: 0x04000B73 RID: 2931
		public const string UnsolicitedInput = "SeUnsolicitedInputPrivilege";

		// Token: 0x04000B74 RID: 2932
		public const string MachineAccount = "SeMachineAccountPrivilege";

		// Token: 0x04000B75 RID: 2933
		public const string TrustedComputingBase = "SeTcbPrivilege";

		// Token: 0x04000B76 RID: 2934
		public const string Security = "SeSecurityPrivilege";

		// Token: 0x04000B77 RID: 2935
		public const string TakeOwnership = "SeTakeOwnershipPrivilege";

		// Token: 0x04000B78 RID: 2936
		public const string LoadDriver = "SeLoadDriverPrivilege";

		// Token: 0x04000B79 RID: 2937
		public const string SystemProfile = "SeSystemProfilePrivilege";

		// Token: 0x04000B7A RID: 2938
		public const string SystemTime = "SeSystemtimePrivilege";

		// Token: 0x04000B7B RID: 2939
		public const string ProfileSingleProcess = "SeProfileSingleProcessPrivilege";

		// Token: 0x04000B7C RID: 2940
		public const string IncreaseBasePriority = "SeIncreaseBasePriorityPrivilege";

		// Token: 0x04000B7D RID: 2941
		public const string CreatePageFile = "SeCreatePagefilePrivilege";

		// Token: 0x04000B7E RID: 2942
		public const string CreatePermanent = "SeCreatePermanentPrivilege";

		// Token: 0x04000B7F RID: 2943
		public const string Backup = "SeBackupPrivilege";

		// Token: 0x04000B80 RID: 2944
		public const string Restore = "SeRestorePrivilege";

		// Token: 0x04000B81 RID: 2945
		public const string Shutdown = "SeShutdownPrivilege";

		// Token: 0x04000B82 RID: 2946
		public const string Debug = "SeDebugPrivilege";

		// Token: 0x04000B83 RID: 2947
		public const string Audit = "SeAuditPrivilege";

		// Token: 0x04000B84 RID: 2948
		public const string SystemEnvironment = "SeSystemEnvironmentPrivilege";

		// Token: 0x04000B85 RID: 2949
		public const string ChangeNotify = "SeChangeNotifyPrivilege";

		// Token: 0x04000B86 RID: 2950
		public const string RemoteShutdown = "SeRemoteShutdownPrivilege";

		// Token: 0x04000B87 RID: 2951
		public const string Undock = "SeUndockPrivilege";

		// Token: 0x04000B88 RID: 2952
		public const string SyncAgent = "SeSyncAgentPrivilege";

		// Token: 0x04000B89 RID: 2953
		public const string EnableDelegation = "SeEnableDelegationPrivilege";

		// Token: 0x04000B8A RID: 2954
		public const string ManageVolume = "SeManageVolumePrivilege";

		// Token: 0x04000B8B RID: 2955
		public const string Impersonate = "SeImpersonatePrivilege";

		// Token: 0x04000B8C RID: 2956
		public const string CreateGlobal = "SeCreateGlobalPrivilege";

		// Token: 0x04000B8D RID: 2957
		public const string TrustedCredentialManagerAccess = "SeTrustedCredManAccessPrivilege";

		// Token: 0x04000B8E RID: 2958
		public const string ReserveProcessor = "SeReserveProcessorPrivilege";

		// Token: 0x02000B05 RID: 2821
		private sealed class TlsContents : IDisposable
		{
			// Token: 0x06006A51 RID: 27217 RVA: 0x0016ECF4 File Offset: 0x0016CEF4
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			public TlsContents()
			{
				int num = 0;
				int num2 = 0;
				bool flag = true;
				if (Privilege.TlsContents.processHandle.IsInvalid)
				{
					object obj = Privilege.TlsContents.syncRoot;
					lock (obj)
					{
						if (Privilege.TlsContents.processHandle.IsInvalid)
						{
							SafeAccessTokenHandle safeAccessTokenHandle;
							if (!Win32Native.OpenProcessToken(Win32Native.GetCurrentProcess(), TokenAccessLevels.Duplicate, out safeAccessTokenHandle))
							{
								num2 = Marshal.GetLastWin32Error();
								flag = false;
							}
							Privilege.TlsContents.processHandle = safeAccessTokenHandle;
						}
					}
				}
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					try
					{
						SafeAccessTokenHandle safeAccessTokenHandle2 = this.threadHandle;
						num = Win32.OpenThreadToken(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, WinSecurityContext.Process, out this.threadHandle);
						num &= 2147024895;
						if (num != 0)
						{
							if (flag)
							{
								this.threadHandle = safeAccessTokenHandle2;
								if (num != 1008)
								{
									flag = false;
								}
								if (flag)
								{
									num = 0;
									if (!Win32Native.DuplicateTokenEx(Privilege.TlsContents.processHandle, TokenAccessLevels.Impersonate | TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, IntPtr.Zero, Win32Native.SECURITY_IMPERSONATION_LEVEL.Impersonation, System.Security.Principal.TokenType.TokenImpersonation, ref this.threadHandle))
									{
										num = Marshal.GetLastWin32Error();
										flag = false;
									}
								}
								if (flag)
								{
									num = Win32.SetThreadToken(this.threadHandle);
									num &= 2147024895;
									if (num != 0)
									{
										flag = false;
									}
								}
								if (flag)
								{
									this.isImpersonating = true;
								}
							}
							else
							{
								num = num2;
							}
						}
						else
						{
							flag = true;
						}
					}
					finally
					{
						if (!flag)
						{
							this.Dispose();
						}
					}
				}
				if (num == 8)
				{
					throw new OutOfMemoryException();
				}
				if (num == 5 || num == 1347)
				{
					throw new UnauthorizedAccessException();
				}
				if (num != 0)
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x06006A52 RID: 27218 RVA: 0x0016EE74 File Offset: 0x0016D074
			[SecuritySafeCritical]
			~TlsContents()
			{
				if (!this.disposed)
				{
					this.Dispose(false);
				}
			}

			// Token: 0x06006A53 RID: 27219 RVA: 0x0016EEAC File Offset: 0x0016D0AC
			[SecuritySafeCritical]
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06006A54 RID: 27220 RVA: 0x0016EEBB File Offset: 0x0016D0BB
			[SecurityCritical]
			private void Dispose(bool disposing)
			{
				if (this.disposed)
				{
					return;
				}
				if (disposing && this.threadHandle != null)
				{
					this.threadHandle.Dispose();
					this.threadHandle = null;
				}
				if (this.isImpersonating)
				{
					Win32.RevertToSelf();
				}
				this.disposed = true;
			}

			// Token: 0x06006A55 RID: 27221 RVA: 0x0016EEF8 File Offset: 0x0016D0F8
			public void IncrementReferenceCount()
			{
				this.referenceCount++;
			}

			// Token: 0x06006A56 RID: 27222 RVA: 0x0016EF08 File Offset: 0x0016D108
			[SecurityCritical]
			public int DecrementReferenceCount()
			{
				int num = this.referenceCount - 1;
				this.referenceCount = num;
				int num2 = num;
				if (num2 == 0)
				{
					this.Dispose();
				}
				return num2;
			}

			// Token: 0x1700120A RID: 4618
			// (get) Token: 0x06006A57 RID: 27223 RVA: 0x0016EF31 File Offset: 0x0016D131
			public int ReferenceCountValue
			{
				get
				{
					return this.referenceCount;
				}
			}

			// Token: 0x1700120B RID: 4619
			// (get) Token: 0x06006A58 RID: 27224 RVA: 0x0016EF39 File Offset: 0x0016D139
			public SafeAccessTokenHandle ThreadHandle
			{
				[SecurityCritical]
				get
				{
					return this.threadHandle;
				}
			}

			// Token: 0x1700120C RID: 4620
			// (get) Token: 0x06006A59 RID: 27225 RVA: 0x0016EF41 File Offset: 0x0016D141
			public bool IsImpersonating
			{
				get
				{
					return this.isImpersonating;
				}
			}

			// Token: 0x04003289 RID: 12937
			private bool disposed;

			// Token: 0x0400328A RID: 12938
			private int referenceCount = 1;

			// Token: 0x0400328B RID: 12939
			[SecurityCritical]
			private SafeAccessTokenHandle threadHandle = new SafeAccessTokenHandle(IntPtr.Zero);

			// Token: 0x0400328C RID: 12940
			private bool isImpersonating;

			// Token: 0x0400328D RID: 12941
			[SecurityCritical]
			private static volatile SafeAccessTokenHandle processHandle = new SafeAccessTokenHandle(IntPtr.Zero);

			// Token: 0x0400328E RID: 12942
			private static readonly object syncRoot = new object();
		}
	}
}
