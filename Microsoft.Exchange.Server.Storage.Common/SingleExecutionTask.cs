using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000080 RID: 128
	public class SingleExecutionTask<T> : Task<T>
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x00013CA8 File Offset: 0x00011EA8
		public static SingleExecutionTask<T> CreateSingleExecutionTask(TaskList taskList, Task<T>.TaskCallback callback, T context, ThreadPriority threadPriority, int stackSize, TaskFlags taskFlags)
		{
			if (taskList == null)
			{
				return null;
			}
			LinkedListNode<Task> linkedListNode = null;
			bool flag = (byte)(taskFlags & TaskFlags.AutoStart) != 0;
			taskFlags &= ~TaskFlags.AutoStart;
			SingleExecutionTask<T> singleExecutionTask = new SingleExecutionTask<T>(taskList, callback, context, threadPriority, stackSize, taskFlags);
			try
			{
				linkedListNode = taskList.Add(singleExecutionTask, false);
			}
			catch (ObjectDisposedException exception)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
			}
			if (linkedListNode != null)
			{
				singleExecutionTask.taskListItem = linkedListNode;
				if (flag)
				{
					singleExecutionTask.Start();
				}
			}
			else
			{
				singleExecutionTask.Dispose();
				singleExecutionTask = null;
			}
			return singleExecutionTask;
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00013D28 File Offset: 0x00011F28
		public static Task<T> CreateSingleExecutionTask(TaskList taskList, Task<T>.TaskCallback callback, T context, bool autoStart)
		{
			return SingleExecutionTask<T>.CreateSingleExecutionTask(taskList, callback, context, ThreadPriority.Normal, 0, (TaskFlags)(6 | (autoStart ? 1 : 0)));
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00013D3E File Offset: 0x00011F3E
		private SingleExecutionTask(TaskList taskList, Task<T>.TaskCallback callback, T context, ThreadPriority threadPriority, int stackSize, TaskFlags taskFlags) : base(callback, context, threadPriority, stackSize, taskFlags)
		{
			this.taskList = taskList;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00013D55 File Offset: 0x00011F55
		public override void Start()
		{
			base.Start();
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00013D60 File Offset: 0x00011F60
		protected override void Invoke()
		{
			try
			{
				base.Invoke();
			}
			finally
			{
				LinkedListNode<Task> linkedListNode = null;
				using (LockManager.Lock(base.StateLock))
				{
					if (this.taskListItem != null)
					{
						linkedListNode = this.taskListItem;
						this.taskListItem = null;
					}
				}
				if (linkedListNode != null && this.taskList.Remove(linkedListNode))
				{
					base.Dispose();
				}
			}
		}

		// Token: 0x0400066B RID: 1643
		private LinkedListNode<Task> taskListItem;

		// Token: 0x0400066C RID: 1644
		private TaskList taskList;
	}
}
