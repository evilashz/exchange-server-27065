using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000013 RID: 19
	public abstract class Task : DisposableBase
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00004DFC File Offset: 0x00002FFC
		public static IDisposable SetInvokeTestHook(Action action)
		{
			return Task.invokeTestHook.SetTestHook(action);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00004E09 File Offset: 0x00003009
		public static void TestDisableTaskExecution()
		{
			Task.testDisabledInvoke = true;
			while (Task.invokeCount != 0)
			{
				Thread.Yield();
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00004E20 File Offset: 0x00003020
		public static void TestReenableTaskExecution()
		{
			Task.testDisabledInvoke = false;
		}

		// Token: 0x0600022E RID: 558
		public abstract void Start();

		// Token: 0x0600022F RID: 559
		public abstract void Stop();

		// Token: 0x06000230 RID: 560
		public abstract bool Finished();

		// Token: 0x06000231 RID: 561
		public abstract bool WaitForCompletion(TimeSpan delay);

		// Token: 0x06000232 RID: 562 RVA: 0x00004E28 File Offset: 0x00003028
		public bool WaitForCompletion()
		{
			return this.WaitForCompletion(Task.InfiniteDelay);
		}

		// Token: 0x040002DA RID: 730
		protected static readonly TimeSpan NoDelay = TimeSpan.Zero;

		// Token: 0x040002DB RID: 731
		protected static readonly TimeSpan InfiniteDelay = TimeSpan.FromMilliseconds(-1.0);

		// Token: 0x040002DC RID: 732
		protected static int invokeCount;

		// Token: 0x040002DD RID: 733
		protected static bool testDisabledInvoke;

		// Token: 0x040002DE RID: 734
		protected static Hookable<Action> invokeTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x040002DF RID: 735
		protected internal Action StartLockEnterTestHook;

		// Token: 0x040002E0 RID: 736
		protected internal Action StartLockEnteredTestHook;

		// Token: 0x040002E1 RID: 737
		protected internal Action StopLockEnterTestHook;

		// Token: 0x040002E2 RID: 738
		protected internal Action StopLockEnteredTestHook;

		// Token: 0x040002E3 RID: 739
		protected internal Action InvokeLock1EnterTestHook;

		// Token: 0x040002E4 RID: 740
		protected internal Action InvokeLock1EnteredTestHook;

		// Token: 0x040002E5 RID: 741
		protected internal Action InvokeLock2EnterTestHook;

		// Token: 0x040002E6 RID: 742
		protected internal Action InvokeLock2EnteredTestHook;

		// Token: 0x040002E7 RID: 743
		protected internal Action DisposeLock1EnterTestHook;

		// Token: 0x040002E8 RID: 744
		protected internal Action DisposeLock1EnteredTestHook;

		// Token: 0x040002E9 RID: 745
		protected internal Action DisposeLock2EnterTestHook;

		// Token: 0x040002EA RID: 746
		protected internal Action DisposeLock2EnteredTestHook;
	}
}
