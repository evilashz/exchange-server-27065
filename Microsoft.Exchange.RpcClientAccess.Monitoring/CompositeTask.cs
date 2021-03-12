using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CompositeTask : BaseTask
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00006EFC File Offset: 0x000050FC
		public CompositeTask(IContext context, params ITask[] tasks) : base(context, Strings.CompositeTaskTitle(tasks.Length), Strings.CompositeTaskDescription(tasks.Length), TaskType.Infrastructure, new ContextProperty[0])
		{
			this.tasks = tasks;
			base.Result = TaskResult.Success;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00007084 File Offset: 0x00005284
		protected override IEnumerator<ITask> Process()
		{
			foreach (ITask task in this.tasks)
			{
				Util.ThrowOnNullArgument(task, "task");
				yield return task;
				base.Result = task.Result;
				if (base.Result != TaskResult.Success)
				{
					break;
				}
			}
			yield break;
		}

		// Token: 0x040000ED RID: 237
		private readonly ITask[] tasks;
	}
}
