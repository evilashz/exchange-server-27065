using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000039 RID: 57
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExecuteContext : BaseObject
	{
		// Token: 0x06000171 RID: 369 RVA: 0x000059EC File Offset: 0x00003BEC
		public ExecuteContext(ITask task, ExecuteAsyncResult executeAsyncResult)
		{
			this.executeAsyncResult = executeAsyncResult;
			this.Push(task);
			this.taskResult = TaskResult.Undefined;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00005A1F File Offset: 0x00003C1F
		private ExecuteContext.TaskStateMachine Top
		{
			get
			{
				if (this.taskStack.Count <= 0)
				{
					return null;
				}
				return this.taskStack.Peek();
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005A3C File Offset: 0x00003C3C
		public void Begin()
		{
			this.Resume();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005A44 File Offset: 0x00003C44
		public TaskResult End()
		{
			this.executeAsyncResult.WaitForCompletion();
			if (this.exception != null)
			{
				throw new TaskException(this.exception);
			}
			return this.taskResult;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005A6B File Offset: 0x00003C6B
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ExecuteContext>(this);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005A74 File Offset: 0x00003C74
		protected override void InternalDispose()
		{
			foreach (ExecuteContext.TaskStateMachine taskStateMachine in this.taskStack)
			{
				taskStateMachine.Dispose();
			}
			base.InternalDispose();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005AD9 File Offset: 0x00003CD9
		private void Resume()
		{
			ThreadPool.QueueUserWorkItem(delegate(object state)
			{
				((ExecuteContext)state).Process();
			}, this);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005B00 File Offset: 0x00003D00
		private void Process()
		{
			lock (this.processLock)
			{
				if (this.isFinished)
				{
					return;
				}
				if (this.isProcessing)
				{
					this.continueProcessing = true;
					return;
				}
				this.isProcessing = true;
				goto IL_49;
			}
			try
			{
				for (;;)
				{
					IL_49:
					ExecuteContext.TaskStateMachine top = this.Top;
					while (top != null)
					{
						bool flag2 = false;
						ITask task = top.Step();
						if (task != null)
						{
							if (!object.ReferenceEquals(task, top.Task))
							{
								this.Push(task);
								top = this.Top;
							}
							else
							{
								flag2 = true;
							}
						}
						else
						{
							this.Pop(top);
							top = this.Top;
						}
						if (flag2)
						{
							break;
						}
					}
					lock (this.processLock)
					{
						this.isProcessing = false;
						if (top == null)
						{
							this.isFinished = true;
						}
						else if (this.continueProcessing)
						{
							this.isProcessing = true;
							this.continueProcessing = false;
							continue;
						}
					}
					break;
				}
			}
			catch (Exception ex)
			{
				this.exception = ex;
				this.isFinished = true;
			}
			if (this.isFinished)
			{
				this.executeAsyncResult.InvokeCallback();
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005C44 File Offset: 0x00003E44
		private void Push(ITask uninitializedTask)
		{
			ExecuteContext.TaskStateMachine item = new ExecuteContext.TaskStateMachine(uninitializedTask, new Action(this.Resume));
			this.taskStack.Push(item);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005C70 File Offset: 0x00003E70
		private void Pop(ExecuteContext.TaskStateMachine doneStateMachine)
		{
			using (ExecuteContext.TaskStateMachine taskStateMachine = this.taskStack.Pop())
			{
				this.taskResult = taskStateMachine.Task.Result;
			}
			doneStateMachine.Task.OnCompleted();
		}

		// Token: 0x040000BF RID: 191
		private readonly Stack<ExecuteContext.TaskStateMachine> taskStack = new Stack<ExecuteContext.TaskStateMachine>();

		// Token: 0x040000C0 RID: 192
		private readonly ExecuteAsyncResult executeAsyncResult;

		// Token: 0x040000C1 RID: 193
		private readonly object processLock = new object();

		// Token: 0x040000C2 RID: 194
		private bool continueProcessing;

		// Token: 0x040000C3 RID: 195
		private bool isProcessing;

		// Token: 0x040000C4 RID: 196
		private bool isFinished;

		// Token: 0x040000C5 RID: 197
		private TaskResult taskResult;

		// Token: 0x040000C6 RID: 198
		private Exception exception;

		// Token: 0x0200003A RID: 58
		[ClassAccessLevel(AccessLevel.MSInternal)]
		private sealed class TaskStateMachine : BaseObject
		{
			// Token: 0x0600017C RID: 380 RVA: 0x00005CC4 File Offset: 0x00003EC4
			public TaskStateMachine(ITask uninitializedTask, Action resumeDelegate)
			{
				Util.ThrowOnNullArgument(uninitializedTask, "task");
				Util.ThrowOnNullArgument(resumeDelegate, "resumeDelegate");
				this.task = uninitializedTask;
				uninitializedTask.Initialize(resumeDelegate);
				this.stateMachine = uninitializedTask.Process();
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x0600017D RID: 381 RVA: 0x00005CFC File Offset: 0x00003EFC
			public ITask Task
			{
				get
				{
					return this.task;
				}
			}

			// Token: 0x0600017E RID: 382 RVA: 0x00005D04 File Offset: 0x00003F04
			[DebuggerStepThrough]
			public ITask Step()
			{
				if (this.stateMachine.MoveNext())
				{
					return this.stateMachine.Current ?? this.task;
				}
				return null;
			}

			// Token: 0x0600017F RID: 383 RVA: 0x00005D2A File Offset: 0x00003F2A
			protected override DisposeTracker GetDisposeTracker()
			{
				return DisposeTracker.Get<ExecuteContext.TaskStateMachine>(this);
			}

			// Token: 0x06000180 RID: 384 RVA: 0x00005D32 File Offset: 0x00003F32
			protected override void InternalDispose()
			{
				Util.DisposeIfPresent(this.stateMachine);
				base.InternalDispose();
			}

			// Token: 0x040000C8 RID: 200
			private readonly ITask task;

			// Token: 0x040000C9 RID: 201
			private readonly IEnumerator<ITask> stateMachine;
		}
	}
}
