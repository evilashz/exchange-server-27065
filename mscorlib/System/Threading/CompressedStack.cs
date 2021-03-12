using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004C4 RID: 1220
	[Serializable]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000DDF06 File Offset: 0x000DC106
		// (set) Token: 0x06003A8F RID: 14991 RVA: 0x000DDF0E File Offset: 0x000DC10E
		internal bool CanSkipEvaluation
		{
			get
			{
				return this.m_canSkipEvaluation;
			}
			private set
			{
				this.m_canSkipEvaluation = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000DDF17 File Offset: 0x000DC117
		internal PermissionListSet PLS
		{
			get
			{
				return this.m_pls;
			}
		}

		// Token: 0x06003A91 RID: 14993 RVA: 0x000DDF21 File Offset: 0x000DC121
		[SecurityCritical]
		internal CompressedStack(SafeCompressedStackHandle csHandle)
		{
			this.m_csHandle = csHandle;
		}

		// Token: 0x06003A92 RID: 14994 RVA: 0x000DDF32 File Offset: 0x000DC132
		[SecurityCritical]
		private CompressedStack(SafeCompressedStackHandle csHandle, PermissionListSet pls)
		{
			this.m_csHandle = csHandle;
			this.m_pls = pls;
		}

		// Token: 0x06003A93 RID: 14995 RVA: 0x000DDF4C File Offset: 0x000DC14C
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.CompleteConstruction(null);
			info.AddValue("PLS", this.m_pls);
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000DDF76 File Offset: 0x000DC176
		private CompressedStack(SerializationInfo info, StreamingContext context)
		{
			this.m_pls = (PermissionListSet)info.GetValue("PLS", typeof(PermissionListSet));
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x000DDFA0 File Offset: 0x000DC1A0
		// (set) Token: 0x06003A96 RID: 14998 RVA: 0x000DDFAA File Offset: 0x000DC1AA
		internal SafeCompressedStackHandle CompressedStackHandle
		{
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this.m_csHandle;
			}
			[SecurityCritical]
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			private set
			{
				this.m_csHandle = value;
			}
		}

		// Token: 0x06003A97 RID: 14999 RVA: 0x000DDFB8 File Offset: 0x000DC1B8
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static CompressedStack GetCompressedStack()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return CompressedStack.GetCompressedStack(ref stackCrawlMark);
		}

		// Token: 0x06003A98 RID: 15000 RVA: 0x000DDFD0 File Offset: 0x000DC1D0
		[SecurityCritical]
		internal static CompressedStack GetCompressedStack(ref StackCrawlMark stackMark)
		{
			CompressedStack innerCS = null;
			CompressedStack compressedStack;
			if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
			{
				compressedStack = new CompressedStack(null);
				compressedStack.CanSkipEvaluation = true;
			}
			else if (CodeAccessSecurityEngine.AllDomainsHomogeneousWithNoStackModifiers())
			{
				compressedStack = new CompressedStack(CompressedStack.GetDelayedCompressedStack(ref stackMark, false));
				compressedStack.m_pls = PermissionListSet.CreateCompressedState_HG();
			}
			else
			{
				compressedStack = new CompressedStack(null);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					compressedStack.CompressedStackHandle = CompressedStack.GetDelayedCompressedStack(ref stackMark, true);
					if (compressedStack.CompressedStackHandle != null && CompressedStack.IsImmediateCompletionCandidate(compressedStack.CompressedStackHandle, out innerCS))
					{
						try
						{
							compressedStack.CompleteConstruction(innerCS);
						}
						finally
						{
							CompressedStack.DestroyDCSList(compressedStack.CompressedStackHandle);
						}
					}
				}
			}
			return compressedStack;
		}

		// Token: 0x06003A99 RID: 15001 RVA: 0x000DE080 File Offset: 0x000DC280
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static CompressedStack Capture()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return CompressedStack.GetCompressedStack(ref stackCrawlMark);
		}

		// Token: 0x06003A9A RID: 15002 RVA: 0x000DE098 File Offset: 0x000DC298
		[SecurityCritical]
		public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
		{
			if (compressedStack == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_NamedParamNull"), "compressedStack");
			}
			if (CompressedStack.cleanupCode == null)
			{
				CompressedStack.tryCode = new RuntimeHelpers.TryCode(CompressedStack.runTryCode);
				CompressedStack.cleanupCode = new RuntimeHelpers.CleanupCode(CompressedStack.runFinallyCode);
			}
			CompressedStack.CompressedStackRunData userData = new CompressedStack.CompressedStackRunData(compressedStack, callback, state);
			RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(CompressedStack.tryCode, CompressedStack.cleanupCode, userData);
		}

		// Token: 0x06003A9B RID: 15003 RVA: 0x000DE10C File Offset: 0x000DC30C
		[SecurityCritical]
		internal static void runTryCode(object userData)
		{
			CompressedStack.CompressedStackRunData compressedStackRunData = (CompressedStack.CompressedStackRunData)userData;
			compressedStackRunData.cssw = CompressedStack.SetCompressedStack(compressedStackRunData.cs, CompressedStack.GetCompressedStackThread());
			compressedStackRunData.callBack(compressedStackRunData.state);
		}

		// Token: 0x06003A9C RID: 15004 RVA: 0x000DE148 File Offset: 0x000DC348
		[SecurityCritical]
		[PrePrepareMethod]
		internal static void runFinallyCode(object userData, bool exceptionThrown)
		{
			CompressedStack.CompressedStackRunData compressedStackRunData = (CompressedStack.CompressedStackRunData)userData;
			compressedStackRunData.cssw.Undo();
		}

		// Token: 0x06003A9D RID: 15005 RVA: 0x000DE168 File Offset: 0x000DC368
		[SecurityCritical]
		[HandleProcessCorruptedStateExceptions]
		internal static CompressedStackSwitcher SetCompressedStack(CompressedStack cs, CompressedStack prevCS)
		{
			CompressedStackSwitcher result = default(CompressedStackSwitcher);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					CompressedStack.SetCompressedStackThread(cs);
					result.prev_CS = prevCS;
					result.curr_CS = cs;
					result.prev_ADStack = CompressedStack.SetAppDomainStack(cs);
				}
			}
			catch
			{
				result.UndoNoThrow();
				throw;
			}
			return result;
		}

		// Token: 0x06003A9E RID: 15006 RVA: 0x000DE1D8 File Offset: 0x000DC3D8
		[SecuritySafeCritical]
		[ComVisible(false)]
		public CompressedStack CreateCopy()
		{
			return new CompressedStack(this.m_csHandle, this.m_pls);
		}

		// Token: 0x06003A9F RID: 15007 RVA: 0x000DE1EF File Offset: 0x000DC3EF
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static IntPtr SetAppDomainStack(CompressedStack cs)
		{
			return Thread.CurrentThread.SetAppDomainStack((cs == null) ? null : cs.CompressedStackHandle);
		}

		// Token: 0x06003AA0 RID: 15008 RVA: 0x000DE207 File Offset: 0x000DC407
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		internal static void RestoreAppDomainStack(IntPtr appDomainStack)
		{
			Thread.CurrentThread.RestoreAppDomainStack(appDomainStack);
		}

		// Token: 0x06003AA1 RID: 15009 RVA: 0x000DE214 File Offset: 0x000DC414
		[SecurityCritical]
		internal static CompressedStack GetCompressedStackThread()
		{
			return Thread.CurrentThread.GetExecutionContextReader().SecurityContext.CompressedStack;
		}

		// Token: 0x06003AA2 RID: 15010 RVA: 0x000DE23C File Offset: 0x000DC43C
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal static void SetCompressedStackThread(CompressedStack cs)
		{
			Thread currentThread = Thread.CurrentThread;
			if (currentThread.GetExecutionContextReader().SecurityContext.CompressedStack != cs)
			{
				ExecutionContext mutableExecutionContext = currentThread.GetMutableExecutionContext();
				if (mutableExecutionContext.SecurityContext != null)
				{
					mutableExecutionContext.SecurityContext.CompressedStack = cs;
					return;
				}
				if (cs != null)
				{
					mutableExecutionContext.SecurityContext = new SecurityContext
					{
						CompressedStack = cs
					};
				}
			}
		}

		// Token: 0x06003AA3 RID: 15011 RVA: 0x000DE29E File Offset: 0x000DC49E
		[SecurityCritical]
		internal bool CheckDemand(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
		{
			this.CompleteConstruction(null);
			if (this.PLS == null)
			{
				return false;
			}
			this.PLS.CheckDemand(demand, permToken, rmh);
			return false;
		}

		// Token: 0x06003AA4 RID: 15012 RVA: 0x000DE2C1 File Offset: 0x000DC4C1
		[SecurityCritical]
		internal bool CheckDemandNoHalt(CodeAccessPermission demand, PermissionToken permToken, RuntimeMethodHandleInternal rmh)
		{
			this.CompleteConstruction(null);
			return this.PLS == null || this.PLS.CheckDemand(demand, permToken, rmh);
		}

		// Token: 0x06003AA5 RID: 15013 RVA: 0x000DE2E2 File Offset: 0x000DC4E2
		[SecurityCritical]
		internal bool CheckSetDemand(PermissionSet pset, RuntimeMethodHandleInternal rmh)
		{
			this.CompleteConstruction(null);
			return this.PLS != null && this.PLS.CheckSetDemand(pset, rmh);
		}

		// Token: 0x06003AA6 RID: 15014 RVA: 0x000DE302 File Offset: 0x000DC502
		[SecurityCritical]
		internal bool CheckSetDemandWithModificationNoHalt(PermissionSet pset, out PermissionSet alteredDemandSet, RuntimeMethodHandleInternal rmh)
		{
			alteredDemandSet = null;
			this.CompleteConstruction(null);
			return this.PLS == null || this.PLS.CheckSetDemandWithModification(pset, out alteredDemandSet, rmh);
		}

		// Token: 0x06003AA7 RID: 15015 RVA: 0x000DE326 File Offset: 0x000DC526
		[SecurityCritical]
		internal void DemandFlagsOrGrantSet(int flags, PermissionSet grantSet)
		{
			this.CompleteConstruction(null);
			if (this.PLS == null)
			{
				return;
			}
			this.PLS.DemandFlagsOrGrantSet(flags, grantSet);
		}

		// Token: 0x06003AA8 RID: 15016 RVA: 0x000DE345 File Offset: 0x000DC545
		[SecurityCritical]
		internal void GetZoneAndOrigin(ArrayList zoneList, ArrayList originList, PermissionToken zoneToken, PermissionToken originToken)
		{
			this.CompleteConstruction(null);
			if (this.PLS != null)
			{
				this.PLS.GetZoneAndOrigin(zoneList, originList, zoneToken, originToken);
			}
		}

		// Token: 0x06003AA9 RID: 15017 RVA: 0x000DE368 File Offset: 0x000DC568
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		internal void CompleteConstruction(CompressedStack innerCS)
		{
			if (this.PLS != null)
			{
				return;
			}
			PermissionListSet pls = PermissionListSet.CreateCompressedState(this, innerCS);
			lock (this)
			{
				if (this.PLS == null)
				{
					this.m_pls = pls;
				}
			}
		}

		// Token: 0x06003AAA RID: 15018
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern SafeCompressedStackHandle GetDelayedCompressedStack(ref StackCrawlMark stackMark, bool walkStack);

		// Token: 0x06003AAB RID: 15019
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyDelayedCompressedStack(IntPtr unmanagedCompressedStack);

		// Token: 0x06003AAC RID: 15020
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyDCSList(SafeCompressedStackHandle compressedStack);

		// Token: 0x06003AAD RID: 15021
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetDCSCount(SafeCompressedStackHandle compressedStack);

		// Token: 0x06003AAE RID: 15022
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsImmediateCompletionCandidate(SafeCompressedStackHandle compressedStack, out CompressedStack innerCS);

		// Token: 0x06003AAF RID: 15023
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern DomainCompressedStack GetDomainCompressedStack(SafeCompressedStackHandle compressedStack, int index);

		// Token: 0x06003AB0 RID: 15024
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetHomogeneousPLS(PermissionListSet hgPLS);

		// Token: 0x040018BD RID: 6333
		private volatile PermissionListSet m_pls;

		// Token: 0x040018BE RID: 6334
		[SecurityCritical]
		private volatile SafeCompressedStackHandle m_csHandle;

		// Token: 0x040018BF RID: 6335
		private bool m_canSkipEvaluation;

		// Token: 0x040018C0 RID: 6336
		internal static volatile RuntimeHelpers.TryCode tryCode;

		// Token: 0x040018C1 RID: 6337
		internal static volatile RuntimeHelpers.CleanupCode cleanupCode;

		// Token: 0x02000BB6 RID: 2998
		internal class CompressedStackRunData
		{
			// Token: 0x06006E27 RID: 28199 RVA: 0x0017AD94 File Offset: 0x00178F94
			internal CompressedStackRunData(CompressedStack cs, ContextCallback cb, object state)
			{
				this.cs = cs;
				this.callBack = cb;
				this.state = state;
				this.cssw = default(CompressedStackSwitcher);
			}

			// Token: 0x04003523 RID: 13603
			internal CompressedStack cs;

			// Token: 0x04003524 RID: 13604
			internal ContextCallback callBack;

			// Token: 0x04003525 RID: 13605
			internal object state;

			// Token: 0x04003526 RID: 13606
			internal CompressedStackSwitcher cssw;
		}
	}
}
