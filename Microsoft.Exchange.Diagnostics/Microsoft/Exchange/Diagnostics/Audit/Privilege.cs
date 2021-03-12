using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics.Audit
{
	// Token: 0x0200019C RID: 412
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	internal sealed class Privilege : IDisposable
	{
		// Token: 0x06000B6D RID: 2925 RVA: 0x00029A02 File Offset: 0x00027C02
		public Privilege(string privilegeName)
		{
			if (string.IsNullOrEmpty(privilegeName))
			{
				throw new ArgumentNullException(DiagnosticsResources.InvalidPrivilegeName);
			}
			this.luid = Privilege.LuidFromPrivilege(privilegeName);
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00029A39 File Offset: 0x00027C39
		public bool NeedToRevert
		{
			get
			{
				return this.needToRevert;
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00029A44 File Offset: 0x00027C44
		public static void RunWithPrivilege(string privilege, bool enabled, PrivilegedHelper helper)
		{
			if (helper == null)
			{
				throw new ArgumentNullException();
			}
			using (Privilege privilege2 = new Privilege(privilege))
			{
				if (enabled)
				{
					privilege2.Enable();
				}
				else
				{
					privilege2.Disable();
				}
				helper();
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00029A94 File Offset: 0x00027C94
		public void Enable()
		{
			this.ToggleState(true);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00029A9D File Offset: 0x00027C9D
		public void Disable()
		{
			this.ToggleState(false);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00029AA6 File Offset: 0x00027CA6
		public void Dispose()
		{
			this.Revert();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00029AB0 File Offset: 0x00027CB0
		public void Revert()
		{
			int num = 0;
			if (!this.currentThread.Equals(Thread.CurrentThread))
			{
				throw new InvalidOperationException(DiagnosticsResources.WrongThread);
			}
			if (!this.NeedToRevert)
			{
				return;
			}
			bool flag = true;
			try
			{
				if (this.stateWasChanged && (this.tlsContents.ReferenceCountValue > 1 || !this.tlsContents.IsImpersonating))
				{
					NativeMethods.TOKEN_PRIVILEGE token_PRIVILEGE = default(NativeMethods.TOKEN_PRIVILEGE);
					token_PRIVILEGE.PrivilegeCount = 1U;
					token_PRIVILEGE.Privilege.Luid = this.luid;
					token_PRIVILEGE.Privilege.Attributes = (this.initialState ? 2U : 0U);
					NativeMethods.TOKEN_PRIVILEGE token_PRIVILEGE2 = default(NativeMethods.TOKEN_PRIVILEGE);
					uint num2 = 0U;
					if (!NativeMethods.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf(token_PRIVILEGE2), ref token_PRIVILEGE2, ref num2))
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
			if (num == 8)
			{
				throw new OutOfMemoryException();
			}
			if (num == 5)
			{
				throw new UnauthorizedAccessException(DiagnosticsResources.UnauthorizedAccess);
			}
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00029BC8 File Offset: 0x00027DC8
		private static NativeMethods.LUID LuidFromPrivilege(string privilege)
		{
			NativeMethods.LUID luid;
			luid.LowPart = 0U;
			luid.HighPart = 0U;
			try
			{
				Privilege.privilegeLock.AcquireReaderLock(-1);
				if (Privilege.luids.Contains(privilege))
				{
					luid = (NativeMethods.LUID)Privilege.luids[privilege];
					Privilege.privilegeLock.ReleaseReaderLock();
				}
				else
				{
					Privilege.privilegeLock.ReleaseReaderLock();
					if (!NativeMethods.LookupPrivilegeValue(null, privilege, ref luid))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						if (lastWin32Error == 8)
						{
							throw new OutOfMemoryException();
						}
						if (lastWin32Error == 5)
						{
							throw new UnauthorizedAccessException(DiagnosticsResources.UnauthorizedAccess);
						}
						if (lastWin32Error == 1313)
						{
							throw new ArgumentException(DiagnosticsResources.InvalidPrivilegeName, privilege.ToString());
						}
						throw new Win32Exception(lastWin32Error);
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

		// Token: 0x06000B75 RID: 2933 RVA: 0x00029CF4 File Offset: 0x00027EF4
		private void ToggleState(bool enable)
		{
			int num = 0;
			if (!this.currentThread.Equals(Thread.CurrentThread))
			{
				throw new InvalidOperationException(DiagnosticsResources.WrongThread);
			}
			if (this.NeedToRevert)
			{
				throw new InvalidOperationException(DiagnosticsResources.RevertPrivilege);
			}
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
				NativeMethods.TOKEN_PRIVILEGE token_PRIVILEGE = default(NativeMethods.TOKEN_PRIVILEGE);
				token_PRIVILEGE.PrivilegeCount = 1U;
				token_PRIVILEGE.Privilege.Luid = this.luid;
				token_PRIVILEGE.Privilege.Attributes = (enable ? 2U : 0U);
				NativeMethods.TOKEN_PRIVILEGE token_PRIVILEGE2 = default(NativeMethods.TOKEN_PRIVILEGE);
				uint num2 = 0U;
				if (!NativeMethods.AdjustTokenPrivileges(this.tlsContents.ThreadHandle, false, ref token_PRIVILEGE, (uint)Marshal.SizeOf(token_PRIVILEGE2), ref token_PRIVILEGE2, ref num2))
				{
					num = Marshal.GetLastWin32Error();
				}
				else if (1300 == Marshal.GetLastWin32Error())
				{
					num = 1300;
				}
				else
				{
					this.initialState = ((token_PRIVILEGE2.Privilege.Attributes & 2U) != 0U);
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
				throw new UnauthorizedAccessException(DiagnosticsResources.UnauthorizedAccess);
			}
			if (num != 0)
			{
				throw new Win32Exception(num);
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00029ED0 File Offset: 0x000280D0
		private void Reset()
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

		// Token: 0x04000850 RID: 2128
		internal const string Audit = "SeAuditPrivilege";

		// Token: 0x04000851 RID: 2129
		private static LocalDataStoreSlot tlsSlot = Thread.AllocateDataSlot();

		// Token: 0x04000852 RID: 2130
		private static HybridDictionary privileges = new HybridDictionary();

		// Token: 0x04000853 RID: 2131
		private static HybridDictionary luids = new HybridDictionary();

		// Token: 0x04000854 RID: 2132
		private static ReaderWriterLock privilegeLock = new ReaderWriterLock();

		// Token: 0x04000855 RID: 2133
		private readonly Thread currentThread = Thread.CurrentThread;

		// Token: 0x04000856 RID: 2134
		private bool needToRevert;

		// Token: 0x04000857 RID: 2135
		private bool initialState;

		// Token: 0x04000858 RID: 2136
		private bool stateWasChanged;

		// Token: 0x04000859 RID: 2137
		private NativeMethods.LUID luid;

		// Token: 0x0400085A RID: 2138
		private Privilege.TlsContents tlsContents;

		// Token: 0x0200019D RID: 413
		private sealed class TlsContents : DisposeTrackableBase
		{
			// Token: 0x06000B78 RID: 2936 RVA: 0x00029F38 File Offset: 0x00028138
			public TlsContents()
			{
				int num = 0;
				int num2 = 0;
				bool flag = true;
				if (Privilege.TlsContents.processHandle.IsInvalid)
				{
					lock (Privilege.TlsContents.syncRoot)
					{
						if (Privilege.TlsContents.processHandle.IsInvalid && !NativeMethods.OpenProcessToken(NativeMethods.GetCurrentProcess(), TokenAccessLevels.Duplicate, ref Privilege.TlsContents.processHandle))
						{
							num2 = Marshal.GetLastWin32Error();
							flag = false;
						}
					}
				}
				try
				{
					if (!NativeMethods.OpenThreadToken(NativeMethods.GetCurrentThread(), TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, true, out this.threadHandle))
					{
						if (flag)
						{
							num = Marshal.GetLastWin32Error();
							if (num != 1008)
							{
								flag = false;
							}
							if (flag)
							{
								num = 0;
								if (!NativeMethods.DuplicateTokenEx(Privilege.TlsContents.processHandle, TokenAccessLevels.Impersonate | TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, IntPtr.Zero, SecurityImpersonationLevel.Impersonation, TokenType.Impersonation, ref this.threadHandle))
								{
									num = Marshal.GetLastWin32Error();
									flag = false;
								}
							}
							if (flag && !NativeMethods.SetThreadToken(IntPtr.Zero, this.threadHandle))
							{
								num = Marshal.GetLastWin32Error();
								flag = false;
							}
							if (flag)
							{
								this.impersonating = true;
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
				if (num == 8)
				{
					throw new OutOfMemoryException();
				}
				if (num == 5 || num == 1347)
				{
					throw new UnauthorizedAccessException(DiagnosticsResources.UnauthorizedAccess);
				}
				if (num != 0)
				{
					throw new Win32Exception(num);
				}
			}

			// Token: 0x17000255 RID: 597
			// (get) Token: 0x06000B79 RID: 2937 RVA: 0x0002A08C File Offset: 0x0002828C
			public int ReferenceCountValue
			{
				get
				{
					return this.referenceCount;
				}
			}

			// Token: 0x17000256 RID: 598
			// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0002A094 File Offset: 0x00028294
			public SafeTokenHandle ThreadHandle
			{
				get
				{
					return this.threadHandle;
				}
			}

			// Token: 0x17000257 RID: 599
			// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0002A09C File Offset: 0x0002829C
			public bool IsImpersonating
			{
				get
				{
					return this.impersonating;
				}
			}

			// Token: 0x06000B7C RID: 2940 RVA: 0x0002A0A4 File Offset: 0x000282A4
			public void IncrementReferenceCount()
			{
				this.referenceCount++;
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x0002A0B4 File Offset: 0x000282B4
			public int DecrementReferenceCount()
			{
				int num = --this.referenceCount;
				if (num == 0)
				{
					this.Dispose();
				}
				return num;
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x0002A0DD File Offset: 0x000282DD
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<Privilege.TlsContents>(this);
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x0002A0E5 File Offset: 0x000282E5
			protected override void InternalDispose(bool isDisposing)
			{
				if (this.threadHandle != null && !this.threadHandle.IsInvalid)
				{
					this.threadHandle.Close();
					this.threadHandle = null;
				}
				if (this.impersonating)
				{
					NativeMethods.RevertToSelf();
				}
			}

			// Token: 0x0400085B RID: 2139
			private static readonly object syncRoot = new object();

			// Token: 0x0400085C RID: 2140
			private static SafeTokenHandle processHandle = SafeTokenHandle.InvalidHandle;

			// Token: 0x0400085D RID: 2141
			private int referenceCount = 1;

			// Token: 0x0400085E RID: 2142
			private SafeTokenHandle threadHandle = SafeTokenHandle.InvalidHandle;

			// Token: 0x0400085F RID: 2143
			private bool impersonating;
		}
	}
}
