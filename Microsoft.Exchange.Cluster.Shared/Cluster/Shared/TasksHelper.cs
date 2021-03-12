using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x020000AD RID: 173
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class TasksHelper
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x00017DAE File Offset: 0x00015FAE
		public static IEnumerable<TTask> GetSuccessful<TTask>(this IEnumerable<TTask> tasks) where TTask : Task
		{
			return from task in tasks
			where task.Status == TaskStatus.RanToCompletion
			select task;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00017DD1 File Offset: 0x00015FD1
		public static IEnumerable<TTask> GetFailed<TTask>(this IEnumerable<TTask> tasks) where TTask : Task
		{
			return from task in tasks
			where task.IsFaulted
			select task;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00017E17 File Offset: 0x00016017
		public static IEnumerable<TTask> GetTimedOut<TTask>(this IEnumerable<TTask> tasks) where TTask : Task
		{
			return from task in tasks
			where !task.IsCanceled && !task.IsCompleted && !task.IsFaulted
			select task;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00017E38 File Offset: 0x00016038
		public static IEnumerable<string> GetServerNamesFromTasks<T>(this IEnumerable<Task<Tuple<string, T>>> tasks)
		{
			return from task in tasks
			select task.Result.Item1;
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00017E4C File Offset: 0x0001604C
		public static string GetServerNamesStringFromTasks<T>(this IEnumerable<Task<Tuple<string, T>>> tasks, Func<IEnumerable<Task<Tuple<string, T>>>, IEnumerable<Task<Tuple<string, T>>>> tasksSelector = null)
		{
			IEnumerable<Task<Tuple<string, T>>> tasks2 = tasks;
			if (tasksSelector != null)
			{
				tasks2 = tasksSelector(tasks);
			}
			return string.Join(", ", tasks2.GetServerNamesFromTasks<T>());
		}
	}
}
