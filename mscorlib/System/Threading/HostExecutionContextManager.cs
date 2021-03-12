using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004D0 RID: 1232
	public class HostExecutionContextManager
	{
		// Token: 0x06003B29 RID: 15145
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HostSecurityManagerPresent();

		// Token: 0x06003B2A RID: 15146
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int ReleaseHostSecurityContext(IntPtr context);

		// Token: 0x06003B2B RID: 15147
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CloneHostSecurityContext(SafeHandle context, SafeHandle clonedContext);

		// Token: 0x06003B2C RID: 15148
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CaptureHostSecurityContext(SafeHandle capturedContext);

		// Token: 0x06003B2D RID: 15149
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SetHostSecurityContext(SafeHandle context, bool fReturnPrevious, SafeHandle prevContext);

		// Token: 0x06003B2E RID: 15150 RVA: 0x000DF610 File Offset: 0x000DD810
		[SecurityCritical]
		internal static bool CheckIfHosted()
		{
			if (!HostExecutionContextManager._fIsHostedChecked)
			{
				HostExecutionContextManager._fIsHosted = HostExecutionContextManager.HostSecurityManagerPresent();
				HostExecutionContextManager._fIsHostedChecked = true;
			}
			return HostExecutionContextManager._fIsHosted;
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000DF638 File Offset: 0x000DD838
		[SecuritySafeCritical]
		public virtual HostExecutionContext Capture()
		{
			HostExecutionContext result = null;
			if (HostExecutionContextManager.CheckIfHosted())
			{
				IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
				result = new HostExecutionContext(unknownSafeHandle);
				HostExecutionContextManager.CaptureHostSecurityContext(unknownSafeHandle);
			}
			return result;
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000DF664 File Offset: 0x000DD864
		[SecurityCritical]
		public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
		{
			if (hostExecutionContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			HostExecutionContextSwitcher hostExecutionContextSwitcher = new HostExecutionContextSwitcher();
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			hostExecutionContextSwitcher.executionContext = mutableExecutionContext;
			hostExecutionContextSwitcher.currentHostContext = hostExecutionContext;
			hostExecutionContextSwitcher.previousHostContext = null;
			if (HostExecutionContextManager.CheckIfHosted() && hostExecutionContext.State is IUnknownSafeHandle)
			{
				IUnknownSafeHandle unknownSafeHandle = new IUnknownSafeHandle();
				hostExecutionContextSwitcher.previousHostContext = new HostExecutionContext(unknownSafeHandle);
				IUnknownSafeHandle context = (IUnknownSafeHandle)hostExecutionContext.State;
				HostExecutionContextManager.SetHostSecurityContext(context, true, unknownSafeHandle);
			}
			mutableExecutionContext.HostExecutionContext = hostExecutionContext;
			return hostExecutionContextSwitcher;
		}

		// Token: 0x06003B31 RID: 15153 RVA: 0x000DF6F0 File Offset: 0x000DD8F0
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Revert(object previousState)
		{
			HostExecutionContextSwitcher hostExecutionContextSwitcher = previousState as HostExecutionContextSwitcher;
			if (hostExecutionContextSwitcher == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotOverrideSetWithoutRevert"));
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (mutableExecutionContext != hostExecutionContextSwitcher.executionContext)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
			}
			hostExecutionContextSwitcher.executionContext = null;
			HostExecutionContext hostExecutionContext = mutableExecutionContext.HostExecutionContext;
			if (hostExecutionContext != hostExecutionContextSwitcher.currentHostContext)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotUseSwitcherOtherThread"));
			}
			HostExecutionContext previousHostContext = hostExecutionContextSwitcher.previousHostContext;
			if (HostExecutionContextManager.CheckIfHosted() && previousHostContext != null && previousHostContext.State is IUnknownSafeHandle)
			{
				IUnknownSafeHandle context = (IUnknownSafeHandle)previousHostContext.State;
				HostExecutionContextManager.SetHostSecurityContext(context, false, null);
			}
			mutableExecutionContext.HostExecutionContext = previousHostContext;
		}

		// Token: 0x06003B32 RID: 15154 RVA: 0x000DF7A0 File Offset: 0x000DD9A0
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HostExecutionContext CaptureHostExecutionContext()
		{
			HostExecutionContext result = null;
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			if (currentHostExecutionContextManager != null)
			{
				result = currentHostExecutionContextManager.Capture();
			}
			return result;
		}

		// Token: 0x06003B33 RID: 15155 RVA: 0x000DF7C0 File Offset: 0x000DD9C0
		[SecurityCritical]
		internal static object SetHostExecutionContextInternal(HostExecutionContext hostContext)
		{
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			object result = null;
			if (currentHostExecutionContextManager != null)
			{
				result = currentHostExecutionContextManager.SetHostExecutionContext(hostContext);
			}
			return result;
		}

		// Token: 0x06003B34 RID: 15156 RVA: 0x000DF7E4 File Offset: 0x000DD9E4
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HostExecutionContextManager GetCurrentHostExecutionContextManager()
		{
			AppDomainManager currentAppDomainManager = AppDomainManager.CurrentAppDomainManager;
			if (currentAppDomainManager != null)
			{
				return currentAppDomainManager.HostExecutionContextManager;
			}
			return null;
		}

		// Token: 0x06003B35 RID: 15157 RVA: 0x000DF802 File Offset: 0x000DDA02
		internal static HostExecutionContextManager GetInternalHostExecutionContextManager()
		{
			if (HostExecutionContextManager._hostExecutionContextManager == null)
			{
				HostExecutionContextManager._hostExecutionContextManager = new HostExecutionContextManager();
			}
			return HostExecutionContextManager._hostExecutionContextManager;
		}

		// Token: 0x040018E1 RID: 6369
		private static volatile bool _fIsHostedChecked;

		// Token: 0x040018E2 RID: 6370
		private static volatile bool _fIsHosted;

		// Token: 0x040018E3 RID: 6371
		private static HostExecutionContextManager _hostExecutionContextManager;
	}
}
