using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security
{
	// Token: 0x020001EE RID: 494
	public sealed class SecurityContext : IDisposable
	{
		// Token: 0x06001DB1 RID: 7601 RVA: 0x00067A00 File Offset: 0x00065C00
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal SecurityContext()
		{
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x00067A08 File Offset: 0x00065C08
		internal static SecurityContext FullTrustSecurityContext
		{
			[SecurityCritical]
			get
			{
				if (SecurityContext._fullTrustSC == null)
				{
					SecurityContext._fullTrustSC = SecurityContext.CreateFullTrustSecurityContext();
				}
				return SecurityContext._fullTrustSC;
			}
		}

		// Token: 0x1700034C RID: 844
		// (set) Token: 0x06001DB3 RID: 7603 RVA: 0x00067A26 File Offset: 0x00065C26
		internal ExecutionContext ExecutionContext
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._executionContext = value;
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x00067A2F File Offset: 0x00065C2F
		// (set) Token: 0x06001DB5 RID: 7605 RVA: 0x00067A39 File Offset: 0x00065C39
		internal WindowsIdentity WindowsIdentity
		{
			get
			{
				return this._windowsIdentity;
			}
			set
			{
				this._windowsIdentity = value;
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06001DB6 RID: 7606 RVA: 0x00067A44 File Offset: 0x00065C44
		// (set) Token: 0x06001DB7 RID: 7607 RVA: 0x00067A4E File Offset: 0x00065C4E
		internal CompressedStack CompressedStack
		{
			get
			{
				return this._compressedStack;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				this._compressedStack = value;
			}
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x00067A59 File Offset: 0x00065C59
		public void Dispose()
		{
			if (this._windowsIdentity != null)
			{
				this._windowsIdentity.Dispose();
			}
		}

		// Token: 0x06001DB9 RID: 7609 RVA: 0x00067A72 File Offset: 0x00065C72
		[SecurityCritical]
		public static AsyncFlowControl SuppressFlow()
		{
			return SecurityContext.SuppressFlow(SecurityContextDisableFlow.All);
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00067A7E File Offset: 0x00065C7E
		[SecurityCritical]
		public static AsyncFlowControl SuppressFlowWindowsIdentity()
		{
			return SecurityContext.SuppressFlow(SecurityContextDisableFlow.WI);
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x00067A88 File Offset: 0x00065C88
		[SecurityCritical]
		internal static AsyncFlowControl SuppressFlow(SecurityContextDisableFlow flags)
		{
			if (SecurityContext.IsFlowSuppressed(flags))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotSupressFlowMultipleTimes"));
			}
			ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
			if (mutableExecutionContext.SecurityContext == null)
			{
				mutableExecutionContext.SecurityContext = new SecurityContext();
			}
			AsyncFlowControl result = default(AsyncFlowControl);
			result.Setup(flags);
			return result;
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00067ADC File Offset: 0x00065CDC
		[SecuritySafeCritical]
		public static void RestoreFlow()
		{
			SecurityContext securityContext = Thread.CurrentThread.GetMutableExecutionContext().SecurityContext;
			if (securityContext == null || securityContext._disableFlow == SecurityContextDisableFlow.Nothing)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CannotRestoreUnsupressedFlow"));
			}
			securityContext._disableFlow = SecurityContextDisableFlow.Nothing;
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x00067B1F File Offset: 0x00065D1F
		public static bool IsFlowSuppressed()
		{
			return SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.All);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x00067B2B File Offset: 0x00065D2B
		public static bool IsWindowsIdentityFlowSuppressed()
		{
			return SecurityContext._LegacyImpersonationPolicy || SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.WI);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00067B3C File Offset: 0x00065D3C
		[SecuritySafeCritical]
		internal static bool IsFlowSuppressed(SecurityContextDisableFlow flags)
		{
			return Thread.CurrentThread.GetExecutionContextReader().SecurityContext.IsFlowSuppressed(flags);
		}

		// Token: 0x06001DC0 RID: 7616 RVA: 0x00067B64 File Offset: 0x00065D64
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static void Run(SecurityContext securityContext, ContextCallback callback, object state)
		{
			if (securityContext == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullContext"));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
			if (!securityContext.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			securityContext.isNewCapture = false;
			ExecutionContext.Reader executionContextReader = Thread.CurrentThread.GetExecutionContextReader();
			if (SecurityContext.CurrentlyInDefaultFTSecurityContext(executionContextReader) && securityContext.IsDefaultFTSecurityContext())
			{
				callback(state);
				if (SecurityContext.GetCurrentWI(Thread.CurrentThread.GetExecutionContextReader()) != null)
				{
					WindowsIdentity.SafeRevertToSelf(ref stackCrawlMark);
					return;
				}
			}
			else
			{
				SecurityContext.RunInternal(securityContext, callback, state);
			}
		}

		// Token: 0x06001DC1 RID: 7617 RVA: 0x00067BF0 File Offset: 0x00065DF0
		[SecurityCritical]
		internal static void RunInternal(SecurityContext securityContext, ContextCallback callBack, object state)
		{
			if (SecurityContext.cleanupCode == null)
			{
				SecurityContext.tryCode = new RuntimeHelpers.TryCode(SecurityContext.runTryCode);
				SecurityContext.cleanupCode = new RuntimeHelpers.CleanupCode(SecurityContext.runFinallyCode);
			}
			SecurityContext.SecurityContextRunData userData = new SecurityContext.SecurityContextRunData(securityContext, callBack, state);
			RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(SecurityContext.tryCode, SecurityContext.cleanupCode, userData);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x00067C4C File Offset: 0x00065E4C
		[SecurityCritical]
		internal static void runTryCode(object userData)
		{
			SecurityContext.SecurityContextRunData securityContextRunData = (SecurityContext.SecurityContextRunData)userData;
			securityContextRunData.scsw = SecurityContext.SetSecurityContext(securityContextRunData.sc, Thread.CurrentThread.GetExecutionContextReader().SecurityContext, true);
			securityContextRunData.callBack(securityContextRunData.state);
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x00067C98 File Offset: 0x00065E98
		[SecurityCritical]
		[PrePrepareMethod]
		internal static void runFinallyCode(object userData, bool exceptionThrown)
		{
			SecurityContext.SecurityContextRunData securityContextRunData = (SecurityContext.SecurityContextRunData)userData;
			securityContextRunData.scsw.Undo();
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x00067CB8 File Offset: 0x00065EB8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static SecurityContextSwitcher SetSecurityContext(SecurityContext sc, SecurityContext.Reader prevSecurityContext, bool modifyCurrentExecutionContext)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return SecurityContext.SetSecurityContext(sc, prevSecurityContext, modifyCurrentExecutionContext, ref stackCrawlMark);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x00067CD4 File Offset: 0x00065ED4
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static SecurityContextSwitcher SetSecurityContext(SecurityContext sc, SecurityContext.Reader prevSecurityContext, bool modifyCurrentExecutionContext, ref StackCrawlMark stackMark)
		{
			SecurityContextDisableFlow disableFlow = sc._disableFlow;
			sc._disableFlow = SecurityContextDisableFlow.Nothing;
			SecurityContextSwitcher result = default(SecurityContextSwitcher);
			result.currSC = sc;
			result.prevSC = prevSecurityContext;
			if (modifyCurrentExecutionContext)
			{
				ExecutionContext mutableExecutionContext = Thread.CurrentThread.GetMutableExecutionContext();
				result.currEC = mutableExecutionContext;
				mutableExecutionContext.SecurityContext = sc;
			}
			if (sc != null)
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					result.wic = null;
					if (!SecurityContext._LegacyImpersonationPolicy)
					{
						if (sc.WindowsIdentity != null)
						{
							result.wic = sc.WindowsIdentity.Impersonate(ref stackMark);
						}
						else if ((disableFlow & SecurityContextDisableFlow.WI) == SecurityContextDisableFlow.Nothing && prevSecurityContext.WindowsIdentity != null)
						{
							result.wic = WindowsIdentity.SafeRevertToSelf(ref stackMark);
						}
					}
					result.cssw = CompressedStack.SetCompressedStack(sc.CompressedStack, prevSecurityContext.CompressedStack);
				}
				catch
				{
					result.UndoNoThrow();
					throw;
				}
			}
			return result;
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x00067DB0 File Offset: 0x00065FB0
		[SecuritySafeCritical]
		public SecurityContext CreateCopy()
		{
			if (!this.isNewCapture)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotNewCaptureContext"));
			}
			SecurityContext securityContext = new SecurityContext();
			securityContext.isNewCapture = true;
			securityContext._disableFlow = this._disableFlow;
			if (this.WindowsIdentity != null)
			{
				securityContext._windowsIdentity = new WindowsIdentity(this.WindowsIdentity.AccessToken);
			}
			if (this._compressedStack != null)
			{
				securityContext._compressedStack = this._compressedStack.CreateCopy();
			}
			return securityContext;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x00067E38 File Offset: 0x00066038
		[SecuritySafeCritical]
		internal SecurityContext CreateMutableCopy()
		{
			SecurityContext securityContext = new SecurityContext();
			securityContext._disableFlow = this._disableFlow;
			if (this.WindowsIdentity != null)
			{
				securityContext._windowsIdentity = new WindowsIdentity(this.WindowsIdentity.AccessToken);
			}
			if (this._compressedStack != null)
			{
				securityContext._compressedStack = this._compressedStack.CreateCopy();
			}
			return securityContext;
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x00067E9C File Offset: 0x0006609C
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static SecurityContext Capture()
		{
			if (SecurityContext.IsFlowSuppressed())
			{
				return null;
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			SecurityContext securityContext = SecurityContext.Capture(Thread.CurrentThread.GetExecutionContextReader(), ref stackCrawlMark);
			if (securityContext == null)
			{
				securityContext = SecurityContext.CreateFullTrustSecurityContext();
			}
			return securityContext;
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x00067ED0 File Offset: 0x000660D0
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static SecurityContext Capture(ExecutionContext.Reader currThreadEC, ref StackCrawlMark stackMark)
		{
			if (currThreadEC.SecurityContext.IsFlowSuppressed(SecurityContextDisableFlow.All))
			{
				return null;
			}
			if (SecurityContext.CurrentlyInDefaultFTSecurityContext(currThreadEC))
			{
				return null;
			}
			return SecurityContext.CaptureCore(currThreadEC, ref stackMark);
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00067F08 File Offset: 0x00066108
		[SecurityCritical]
		private static SecurityContext CaptureCore(ExecutionContext.Reader currThreadEC, ref StackCrawlMark stackMark)
		{
			SecurityContext securityContext = new SecurityContext();
			securityContext.isNewCapture = true;
			if (!SecurityContext.IsWindowsIdentityFlowSuppressed())
			{
				WindowsIdentity currentWI = SecurityContext.GetCurrentWI(currThreadEC);
				if (currentWI != null)
				{
					securityContext._windowsIdentity = new WindowsIdentity(currentWI.AccessToken);
				}
			}
			else
			{
				securityContext._disableFlow = SecurityContextDisableFlow.WI;
			}
			securityContext.CompressedStack = CompressedStack.GetCompressedStack(ref stackMark);
			return securityContext;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00067F60 File Offset: 0x00066160
		[SecurityCritical]
		internal static SecurityContext CreateFullTrustSecurityContext()
		{
			SecurityContext securityContext = new SecurityContext();
			securityContext.isNewCapture = true;
			if (SecurityContext.IsWindowsIdentityFlowSuppressed())
			{
				securityContext._disableFlow = SecurityContextDisableFlow.WI;
			}
			securityContext.CompressedStack = new CompressedStack(null);
			return securityContext;
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x00067F99 File Offset: 0x00066199
		internal static bool AlwaysFlowImpersonationPolicy
		{
			get
			{
				return SecurityContext._alwaysFlowImpersonationPolicy;
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00067FA0 File Offset: 0x000661A0
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static WindowsIdentity GetCurrentWI(ExecutionContext.Reader threadEC)
		{
			return SecurityContext.GetCurrentWI(threadEC, SecurityContext._alwaysFlowImpersonationPolicy);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00067FB0 File Offset: 0x000661B0
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static WindowsIdentity GetCurrentWI(ExecutionContext.Reader threadEC, bool cachedAlwaysFlowImpersonationPolicy)
		{
			if (cachedAlwaysFlowImpersonationPolicy)
			{
				return WindowsIdentity.GetCurrentInternal(TokenAccessLevels.MaximumAllowed, true);
			}
			return threadEC.SecurityContext.WindowsIdentity;
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00067FDC File Offset: 0x000661DC
		[SecurityCritical]
		internal static void RestoreCurrentWI(ExecutionContext.Reader currentEC, ExecutionContext.Reader prevEC, WindowsIdentity targetWI, bool cachedAlwaysFlowImpersonationPolicy)
		{
			if (cachedAlwaysFlowImpersonationPolicy || prevEC.SecurityContext.WindowsIdentity != targetWI)
			{
				SecurityContext.RestoreCurrentWIInternal(targetWI);
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00068004 File Offset: 0x00066204
		[SecurityCritical]
		private static void RestoreCurrentWIInternal(WindowsIdentity targetWI)
		{
			int num = Win32.RevertToSelf();
			if (num < 0)
			{
				Environment.FailFast(Win32Native.GetMessage(num));
			}
			if (targetWI != null)
			{
				SafeAccessTokenHandle accessToken = targetWI.AccessToken;
				if (accessToken != null && !accessToken.IsInvalid)
				{
					num = Win32.ImpersonateLoggedOnUser(accessToken);
					if (num < 0)
					{
						Environment.FailFast(Win32Native.GetMessage(num));
					}
				}
			}
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00068051 File Offset: 0x00066251
		[SecurityCritical]
		internal bool IsDefaultFTSecurityContext()
		{
			return this.WindowsIdentity == null && (this.CompressedStack == null || this.CompressedStack.CompressedStackHandle == null);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00068075 File Offset: 0x00066275
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool CurrentlyInDefaultFTSecurityContext(ExecutionContext.Reader threadEC)
		{
			return SecurityContext.IsDefaultThreadSecurityInfo() && SecurityContext.GetCurrentWI(threadEC) == null;
		}

		// Token: 0x06001DD3 RID: 7635
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern WindowsImpersonationFlowMode GetImpersonationFlowMode();

		// Token: 0x06001DD4 RID: 7636
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsDefaultThreadSecurityInfo();

		// Token: 0x04000A62 RID: 2658
		private static bool _LegacyImpersonationPolicy = SecurityContext.GetImpersonationFlowMode() == WindowsImpersonationFlowMode.IMP_NOFLOW;

		// Token: 0x04000A63 RID: 2659
		private static bool _alwaysFlowImpersonationPolicy = SecurityContext.GetImpersonationFlowMode() == WindowsImpersonationFlowMode.IMP_ALWAYSFLOW;

		// Token: 0x04000A64 RID: 2660
		private ExecutionContext _executionContext;

		// Token: 0x04000A65 RID: 2661
		private volatile WindowsIdentity _windowsIdentity;

		// Token: 0x04000A66 RID: 2662
		private volatile CompressedStack _compressedStack;

		// Token: 0x04000A67 RID: 2663
		private static volatile SecurityContext _fullTrustSC;

		// Token: 0x04000A68 RID: 2664
		internal volatile bool isNewCapture;

		// Token: 0x04000A69 RID: 2665
		internal volatile SecurityContextDisableFlow _disableFlow;

		// Token: 0x04000A6A RID: 2666
		internal static volatile RuntimeHelpers.TryCode tryCode;

		// Token: 0x04000A6B RID: 2667
		internal static volatile RuntimeHelpers.CleanupCode cleanupCode;

		// Token: 0x02000AFF RID: 2815
		internal struct Reader
		{
			// Token: 0x06006A43 RID: 27203 RVA: 0x0016EC2A File Offset: 0x0016CE2A
			public Reader(SecurityContext sc)
			{
				this.m_sc = sc;
			}

			// Token: 0x06006A44 RID: 27204 RVA: 0x0016EC33 File Offset: 0x0016CE33
			public SecurityContext DangerousGetRawSecurityContext()
			{
				return this.m_sc;
			}

			// Token: 0x17001207 RID: 4615
			// (get) Token: 0x06006A45 RID: 27205 RVA: 0x0016EC3B File Offset: 0x0016CE3B
			public bool IsNull
			{
				get
				{
					return this.m_sc == null;
				}
			}

			// Token: 0x06006A46 RID: 27206 RVA: 0x0016EC46 File Offset: 0x0016CE46
			public bool IsSame(SecurityContext sc)
			{
				return this.m_sc == sc;
			}

			// Token: 0x06006A47 RID: 27207 RVA: 0x0016EC51 File Offset: 0x0016CE51
			public bool IsSame(SecurityContext.Reader sc)
			{
				return this.m_sc == sc.m_sc;
			}

			// Token: 0x06006A48 RID: 27208 RVA: 0x0016EC61 File Offset: 0x0016CE61
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool IsFlowSuppressed(SecurityContextDisableFlow flags)
			{
				return this.m_sc != null && (this.m_sc._disableFlow & flags) == flags;
			}

			// Token: 0x17001208 RID: 4616
			// (get) Token: 0x06006A49 RID: 27209 RVA: 0x0016EC7F File Offset: 0x0016CE7F
			public CompressedStack CompressedStack
			{
				get
				{
					if (!this.IsNull)
					{
						return this.m_sc.CompressedStack;
					}
					return null;
				}
			}

			// Token: 0x17001209 RID: 4617
			// (get) Token: 0x06006A4A RID: 27210 RVA: 0x0016EC96 File Offset: 0x0016CE96
			public WindowsIdentity WindowsIdentity
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					if (!this.IsNull)
					{
						return this.m_sc.WindowsIdentity;
					}
					return null;
				}
			}

			// Token: 0x04003273 RID: 12915
			private SecurityContext m_sc;
		}

		// Token: 0x02000B00 RID: 2816
		internal class SecurityContextRunData
		{
			// Token: 0x06006A4B RID: 27211 RVA: 0x0016ECAD File Offset: 0x0016CEAD
			internal SecurityContextRunData(SecurityContext securityContext, ContextCallback cb, object state)
			{
				this.sc = securityContext;
				this.callBack = cb;
				this.state = state;
				this.scsw = default(SecurityContextSwitcher);
			}

			// Token: 0x04003274 RID: 12916
			internal SecurityContext sc;

			// Token: 0x04003275 RID: 12917
			internal ContextCallback callBack;

			// Token: 0x04003276 RID: 12918
			internal object state;

			// Token: 0x04003277 RID: 12919
			internal SecurityContextSwitcher scsw;
		}
	}
}
