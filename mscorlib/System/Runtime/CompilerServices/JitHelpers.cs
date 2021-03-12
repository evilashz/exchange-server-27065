using System;
using System.Security;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008AA RID: 2218
	[FriendAccessAllowed]
	internal static class JitHelpers
	{
		// Token: 0x06005CAB RID: 23723 RVA: 0x00144DF4 File Offset: 0x00142FF4
		[SecurityCritical]
		internal static StringHandleOnStack GetStringHandleOnStack(ref string s)
		{
			return new StringHandleOnStack(JitHelpers.UnsafeCastToStackPointer<string>(ref s));
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x00144E01 File Offset: 0x00143001
		[SecurityCritical]
		internal static ObjectHandleOnStack GetObjectHandleOnStack<T>(ref T o) where T : class
		{
			return new ObjectHandleOnStack(JitHelpers.UnsafeCastToStackPointer<T>(ref o));
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x00144E0E File Offset: 0x0014300E
		[SecurityCritical]
		internal static StackCrawlMarkHandle GetStackCrawlMarkHandle(ref StackCrawlMark stackMark)
		{
			return new StackCrawlMarkHandle(JitHelpers.UnsafeCastToStackPointer<StackCrawlMark>(ref stackMark));
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x00144E1B File Offset: 0x0014301B
		[SecurityCritical]
		[FriendAccessAllowed]
		internal static T UnsafeCast<T>(object o) where T : class
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x00144E22 File Offset: 0x00143022
		internal static int UnsafeEnumCast<T>(T val) where T : struct
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x00144E29 File Offset: 0x00143029
		internal static long UnsafeEnumCastLong<T>(T val) where T : struct
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x00144E30 File Offset: 0x00143030
		[SecurityCritical]
		internal static IntPtr UnsafeCastToStackPointer<T>(ref T val)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06005CB2 RID: 23730
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UnsafeSetArrayElement(object[] target, int index, object element);

		// Token: 0x06005CB3 RID: 23731 RVA: 0x00144E37 File Offset: 0x00143037
		[SecurityCritical]
		internal static PinningHelper GetPinningHelper(object o)
		{
			return JitHelpers.UnsafeCast<PinningHelper>(o);
		}

		// Token: 0x0400297D RID: 10621
		internal const string QCall = "QCall";
	}
}
