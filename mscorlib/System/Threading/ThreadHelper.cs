using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004E7 RID: 1255
	internal class ThreadHelper
	{
		// Token: 0x06003BFD RID: 15357 RVA: 0x000E174D File Offset: 0x000DF94D
		internal ThreadHelper(Delegate start)
		{
			this._start = start;
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x000E175C File Offset: 0x000DF95C
		internal void SetExecutionContextHelper(ExecutionContext ec)
		{
			this._executionContext = ec;
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x000E1768 File Offset: 0x000DF968
		[SecurityCritical]
		private static void ThreadStart_Context(object state)
		{
			ThreadHelper threadHelper = (ThreadHelper)state;
			if (threadHelper._start is ThreadStart)
			{
				((ThreadStart)threadHelper._start)();
				return;
			}
			((ParameterizedThreadStart)threadHelper._start)(threadHelper._startArg);
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x000E17B0 File Offset: 0x000DF9B0
		[SecurityCritical]
		internal void ThreadStart(object obj)
		{
			this._startArg = obj;
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ParameterizedThreadStart)this._start)(obj);
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x000E17E4 File Offset: 0x000DF9E4
		[SecurityCritical]
		internal void ThreadStart()
		{
			if (this._executionContext != null)
			{
				ExecutionContext.Run(this._executionContext, ThreadHelper._ccb, this);
				return;
			}
			((ThreadStart)this._start)();
		}

		// Token: 0x04001928 RID: 6440
		private Delegate _start;

		// Token: 0x04001929 RID: 6441
		private object _startArg;

		// Token: 0x0400192A RID: 6442
		private ExecutionContext _executionContext;

		// Token: 0x0400192B RID: 6443
		[SecurityCritical]
		internal static ContextCallback _ccb = new ContextCallback(ThreadHelper.ThreadStart_Context);
	}
}
