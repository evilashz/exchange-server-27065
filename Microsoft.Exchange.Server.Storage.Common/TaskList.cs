using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200008E RID: 142
	public class TaskList : DisposableBase
	{
		// Token: 0x0600076F RID: 1903 RVA: 0x0001487E File Offset: 0x00012A7E
		public TaskList()
		{
			this.taskList = new LinkedList<Task>();
			this.taskListToDispose = this.taskList;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000148A8 File Offset: 0x00012AA8
		internal Task Find(Predicate<Task> match)
		{
			using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
			{
				if (this.taskList != null)
				{
					base.CheckDisposed();
					foreach (Task task in this.taskList)
					{
						if (match(task))
						{
							return task;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001493C File Offset: 0x00012B3C
		public LinkedListNode<Task> Add(Task task, bool autoStart)
		{
			LinkedListNode<Task> result = null;
			Action action = null;
			using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
			{
				if (this.taskList != null)
				{
					base.CheckDisposed();
					result = this.taskList.AddLast(task);
					action = new Action(task.Start);
					task = null;
				}
			}
			if (task != null)
			{
				task.Stop();
				task.Dispose();
			}
			else if (autoStart)
			{
				action();
			}
			return result;
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000149C4 File Offset: 0x00012BC4
		public bool Remove(LinkedListNode<Task> taskNode)
		{
			bool result;
			using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
			{
				if (this.taskList != null && taskNode.List != null)
				{
					base.CheckDisposed();
					taskNode.List.Remove(taskNode);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x00014A28 File Offset: 0x00012C28
		public bool Remove(Task t)
		{
			bool result;
			using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
			{
				if (this.taskList != null)
				{
					base.CheckDisposed();
					result = this.taskList.Remove(t);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00014A84 File Offset: 0x00012C84
		public void WaitAllForTestOnly()
		{
			using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
			{
				if (this.taskList != null)
				{
					base.CheckDisposed();
					foreach (Task task in this.taskList)
					{
						task.WaitForCompletion(TimeSpan.FromSeconds(1.0));
						task.Stop();
					}
				}
			}
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00014B24 File Offset: 0x00012D24
		public void StartAll()
		{
			using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
			{
				if (this.taskList != null)
				{
					base.CheckDisposed();
					foreach (Task task in this.taskList)
					{
						task.Start();
					}
				}
			}
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00014BB0 File Offset: 0x00012DB0
		public void StopAll()
		{
			this.InternalStopAll(false);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00014BB9 File Offset: 0x00012DB9
		public void StopAllAndPreventFurtherRegistration()
		{
			this.InternalStopAll(true);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00014BC4 File Offset: 0x00012DC4
		private void InternalStopAll(bool preventFurtherRegistration)
		{
			using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
			{
				if (this.taskList != null)
				{
					base.CheckDisposed();
					foreach (Task task in this.taskList)
					{
						task.Stop();
					}
					if (preventFurtherRegistration)
					{
						this.taskList = null;
					}
				}
			}
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00014C58 File Offset: 0x00012E58
		public void StopAndShutdown()
		{
			this.Shutdown(false);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00014C61 File Offset: 0x00012E61
		public void WaitAndShutdown()
		{
			this.Shutdown(true);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x00014C6C File Offset: 0x00012E6C
		private void Shutdown(bool waitForCompletion)
		{
			using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
			{
				if (this.taskList != null)
				{
					base.CheckDisposed();
					this.taskList = null;
				}
			}
			if (this.taskListToDispose != null)
			{
				for (;;)
				{
					Task value;
					using (LockManager.Lock(this.taskListLock, LockManager.LockType.TaskList))
					{
						if (this.taskListToDispose.Count == 0)
						{
							break;
						}
						value = this.taskListToDispose.First.Value;
						this.taskListToDispose.Remove(this.taskListToDispose.First);
					}
					if (waitForCompletion)
					{
						value.WaitForCompletion();
					}
					else
					{
						value.Stop();
					}
					value.Dispose();
				}
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00014D40 File Offset: 0x00012F40
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TaskList>(this);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x00014D48 File Offset: 0x00012F48
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				this.StopAndShutdown();
			}
		}

		// Token: 0x04000692 RID: 1682
		internal const int CleanupIntervalInSeconds = 15;

		// Token: 0x04000693 RID: 1683
		private LinkedList<Task> taskList;

		// Token: 0x04000694 RID: 1684
		private LinkedList<Task> taskListToDispose;

		// Token: 0x04000695 RID: 1685
		private object taskListLock = new object();
	}
}
