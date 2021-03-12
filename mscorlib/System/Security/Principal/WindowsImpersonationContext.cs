using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x02000304 RID: 772
	[ComVisible(true)]
	public class WindowsImpersonationContext : IDisposable
	{
		// Token: 0x060027C5 RID: 10181 RVA: 0x000921BC File Offset: 0x000903BC
		[SecurityCritical]
		private WindowsImpersonationContext()
		{
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000921D0 File Offset: 0x000903D0
		[SecurityCritical]
		internal WindowsImpersonationContext(SafeAccessTokenHandle safeTokenHandle, WindowsIdentity wi, bool isImpersonating, FrameSecurityDescriptor fsd)
		{
			if (safeTokenHandle.IsInvalid)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidImpersonationToken"));
			}
			if (isImpersonating)
			{
				if (!Win32Native.DuplicateHandle(Win32Native.GetCurrentProcess(), safeTokenHandle, Win32Native.GetCurrentProcess(), ref this.m_safeTokenHandle, 0U, true, 2U))
				{
					throw new SecurityException(Win32Native.GetMessage(Marshal.GetLastWin32Error()));
				}
				this.m_wi = wi;
			}
			this.m_fsd = fsd;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x00092244 File Offset: 0x00090444
		[SecuritySafeCritical]
		public void Undo()
		{
			if (this.m_safeTokenHandle.IsInvalid)
			{
				int num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
			}
			else
			{
				int num = Win32.RevertToSelf();
				if (num < 0)
				{
					Environment.FailFast(Win32Native.GetMessage(num));
				}
				num = Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
				if (num < 0)
				{
					throw new SecurityException(Win32Native.GetMessage(num));
				}
			}
			WindowsIdentity.UpdateThreadWI(this.m_wi);
			if (this.m_fsd != null)
			{
				this.m_fsd.SetTokenHandles(null, null);
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000922C8 File Offset: 0x000904C8
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[HandleProcessCorruptedStateExceptions]
		internal bool UndoNoThrow()
		{
			bool result = false;
			try
			{
				int num;
				if (this.m_safeTokenHandle.IsInvalid)
				{
					num = Win32.RevertToSelf();
					if (num < 0)
					{
						Environment.FailFast(Win32Native.GetMessage(num));
					}
				}
				else
				{
					num = Win32.RevertToSelf();
					if (num >= 0)
					{
						num = Win32.ImpersonateLoggedOnUser(this.m_safeTokenHandle);
					}
					else
					{
						Environment.FailFast(Win32Native.GetMessage(num));
					}
				}
				result = (num >= 0);
				if (this.m_fsd != null)
				{
					this.m_fsd.SetTokenHandles(null, null);
				}
			}
			catch (Exception exception)
			{
				if (!AppContextSwitches.UseLegacyExecutionContextBehaviorUponUndoFailure)
				{
					Environment.FailFast(Environment.GetResourceString("ExecutionContext_UndoFailed"), exception);
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0009236C File Offset: 0x0009056C
		[SecuritySafeCritical]
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.m_safeTokenHandle != null && !this.m_safeTokenHandle.IsClosed)
			{
				this.Undo();
				this.m_safeTokenHandle.Dispose();
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x00092397 File Offset: 0x00090597
		[ComVisible(false)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x04000FC4 RID: 4036
		[SecurityCritical]
		private SafeAccessTokenHandle m_safeTokenHandle = SafeAccessTokenHandle.InvalidHandle;

		// Token: 0x04000FC5 RID: 4037
		private WindowsIdentity m_wi;

		// Token: 0x04000FC6 RID: 4038
		private FrameSecurityDescriptor m_fsd;
	}
}
