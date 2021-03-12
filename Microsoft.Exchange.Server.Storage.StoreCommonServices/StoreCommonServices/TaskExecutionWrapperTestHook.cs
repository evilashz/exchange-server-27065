using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200014A RID: 330
	internal static class TaskExecutionWrapperTestHook
	{
		// Token: 0x06000CD7 RID: 3287 RVA: 0x00040B5A File Offset: 0x0003ED5A
		public static IDisposable Set(Action<TaskTypeId> testDelegate)
		{
			return TaskExecutionWrapperTestHook.testHook.SetTestHook(testDelegate);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x00040B67 File Offset: 0x0003ED67
		public static void Invoke(TaskTypeId taskTypeId)
		{
			if (TaskExecutionWrapperTestHook.testHook.Value != null)
			{
				TaskExecutionWrapperTestHook.testHook.Value(taskTypeId);
			}
		}

		// Token: 0x04000732 RID: 1842
		private static readonly Hookable<Action<TaskTypeId>> testHook = Hookable<Action<TaskTypeId>>.Create(true, null);
	}
}
