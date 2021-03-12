using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004CD RID: 1229
	internal class HostExecutionContextSwitcher
	{
		// Token: 0x06003B1C RID: 15132 RVA: 0x000DF518 File Offset: 0x000DD718
		[SecurityCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static void Undo(object switcherObject)
		{
			if (switcherObject == null)
			{
				return;
			}
			HostExecutionContextManager currentHostExecutionContextManager = HostExecutionContextManager.GetCurrentHostExecutionContextManager();
			if (currentHostExecutionContextManager != null)
			{
				currentHostExecutionContextManager.Revert(switcherObject);
			}
		}

		// Token: 0x040018DD RID: 6365
		internal ExecutionContext executionContext;

		// Token: 0x040018DE RID: 6366
		internal HostExecutionContext previousHostContext;

		// Token: 0x040018DF RID: 6367
		internal HostExecutionContext currentHostContext;
	}
}
