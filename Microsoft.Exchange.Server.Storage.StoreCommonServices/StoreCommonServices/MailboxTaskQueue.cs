using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.Common;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000C0 RID: 192
	internal class MailboxTaskQueue : IComponentData
	{
		// Token: 0x06000804 RID: 2052 RVA: 0x0002739A File Offset: 0x0002559A
		private MailboxTaskQueue(StoreDatabase database)
		{
			this.database = database;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000273B4 File Offset: 0x000255B4
		bool IComponentData.DoCleanup(Context context)
		{
			bool result;
			using (LockManager.Lock(this.items, LockManager.LockType.LeafMonitorLock, context.Diagnostics))
			{
				result = (this.items.Count == 0);
			}
			return result;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00027408 File Offset: 0x00025608
		internal static IDisposable SetMailboxTaskScheduledTaskTestHook(Action action)
		{
			return MailboxTaskQueue.mailboxTaskScheduledTaskTestHook.SetTestHook(action);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00027415 File Offset: 0x00025615
		internal static void Initialize()
		{
			if (MailboxTaskQueue.mailboxStateSlot == -1)
			{
				MailboxTaskQueue.mailboxStateSlot = MailboxState.AllocateComponentDataSlot(false);
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002742C File Offset: 0x0002562C
		internal static void LaunchMailboxTask<TMailboxTaskContext>(Context context, MailboxTaskQueue.Priority priority, TaskTypeId taskTypeId, MailboxState mailboxState, SecurityIdentifier taskOwnerSid, ClientType taskOwnerClientType, CultureInfo taskOwnerCulture, MailboxTaskQueue.MailboxTaskDelegate mailboxTaskDelegate) where TMailboxTaskContext : MailboxTaskContext, new()
		{
			MailboxTaskQueue.LaunchMailboxTask<TMailboxTaskContext>(context, priority, taskTypeId, mailboxState, taskOwnerSid, taskOwnerClientType, taskOwnerCulture, null, null, null, mailboxTaskDelegate);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x000274B4 File Offset: 0x000256B4
		internal static void LaunchMailboxTask<TMailboxTaskContext>(Context context, MailboxTaskQueue.Priority priority, TaskTypeId taskTypeId, MailboxState mailboxState, SecurityIdentifier taskOwnerSid, ClientType taskOwnerClientType, CultureInfo taskOwnerCulture, Action<Context> onBeforeTaskStep, Action<Context> onInsideTaskStep, Action<Context> onAfterTaskStep, MailboxTaskQueue.MailboxTaskDelegate mailboxTaskDelegate) where TMailboxTaskContext : MailboxTaskContext, new()
		{
			MailboxTaskQueue mailboxTaskQueue = MailboxTaskQueue.GetMailboxTaskQueue(context, mailboxState);
			MailboxTaskQueue.MailboxTaskParameters mailboxTaskParameters = new MailboxTaskQueue.MailboxTaskParameters(context.Database, mailboxState.MailboxNumber, taskOwnerSid, taskOwnerClientType, taskOwnerCulture, onBeforeTaskStep, onInsideTaskStep, onAfterTaskStep, mailboxTaskDelegate);
			StoreDatabase database = context.Database;
			Guid userIdentity = context.UserIdentity;
			int mailboxNumber = mailboxState.MailboxNumber;
			Guid clientActivityId = context.Diagnostics.ClientActivityId;
			string clientComponentName = context.Diagnostics.ClientComponentName;
			string clientProtocolName = context.Diagnostics.ClientProtocolName;
			string clientActionString = context.Diagnostics.ClientActionString;
			Func<MailboxTaskContext> executionContextCreator = () => MailboxTaskContext.CreateTaskExecutionContext<TMailboxTaskContext>(taskTypeId, database, mailboxNumber, taskOwnerClientType, clientActivityId, clientComponentName, clientProtocolName, clientActionString, userIdentity, taskOwnerSid, taskOwnerCulture);
			mailboxTaskQueue.QueueTask(context, priority, taskTypeId, mailboxState, executionContextCreator, mailboxTaskParameters);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000275A0 File Offset: 0x000257A0
		internal static MailboxTaskQueue GetMailboxTaskQueueNoCreate(Context context, MailboxState mailboxState)
		{
			return (MailboxTaskQueue)mailboxState.GetComponentData(MailboxTaskQueue.mailboxStateSlot);
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x000275B4 File Offset: 0x000257B4
		internal static MailboxTaskQueue GetMailboxTaskQueue(Context context, MailboxState mailboxState)
		{
			MailboxTaskQueue mailboxTaskQueue = MailboxTaskQueue.GetMailboxTaskQueueNoCreate(context, mailboxState);
			if (mailboxTaskQueue == null)
			{
				mailboxTaskQueue = new MailboxTaskQueue(context.Database);
				MailboxTaskQueue mailboxTaskQueue2 = (MailboxTaskQueue)mailboxState.CompareExchangeComponentData(MailboxTaskQueue.mailboxStateSlot, null, mailboxTaskQueue);
				if (mailboxTaskQueue2 != null)
				{
					mailboxTaskQueue = mailboxTaskQueue2;
				}
			}
			return mailboxTaskQueue;
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x000275F4 File Offset: 0x000257F4
		internal void DrainQueue()
		{
			LinkedListNode<MailboxTaskQueue.QueueItem> linkedListNode;
			do
			{
				linkedListNode = null;
				using (LockManager.Lock(this.items, LockManager.LockType.LeafMonitorLock))
				{
					if (this.items.Count != 0)
					{
						linkedListNode = this.items.First;
						this.items.Remove(linkedListNode);
					}
				}
				if (linkedListNode != null)
				{
					linkedListNode.Value.Completed = true;
					linkedListNode.Value.Cleanup();
					linkedListNode.Value.Dispose();
				}
			}
			while (linkedListNode != null);
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00027680 File Offset: 0x00025880
		internal void ScheduleWorkerTaskIfNeeded()
		{
			bool flag = false;
			using (LockManager.Lock(this.items, LockManager.LockType.LeafMonitorLock))
			{
				if (this.items.Count == 0)
				{
					this.workerTaskScheduled = false;
					return;
				}
				if (this.workerTaskScheduled)
				{
					return;
				}
				this.workerTaskScheduled = true;
				flag = true;
			}
			if (flag)
			{
				Task<MailboxTaskQueue> task = SingleExecutionTask<MailboxTaskQueue>.CreateSingleExecutionTask(this.database.TaskList, new Task<MailboxTaskQueue>.TaskCallback(MailboxTaskQueue.WorkerTaskCallback), this, true);
				if (task != null)
				{
					FaultInjection.InjectFault(MailboxTaskQueue.mailboxTaskScheduledTaskTestHook);
					return;
				}
				this.workerTaskScheduled = false;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00027720 File Offset: 0x00025920
		private static void RunMailboxTaskStep(MailboxTaskContext mailboxTaskContext, MailboxTaskQueue.QueueItem queueItem, Func<bool> shouldTaskContinue)
		{
			bool flag = false;
			bool flag2 = true;
			Action action = null;
			ThreadManager.ThreadInfo threadInfo;
			using (ThreadManager.NewMethodFrame(queueItem.TaskParameters.MailboxTaskDelegate, out threadInfo))
			{
				try
				{
					if (queueItem.IsNew)
					{
						mailboxTaskContext.TaskDiagnostics.OnBeforeTask(RopSummaryCollector.GetRopSummaryCollector(mailboxTaskContext.Database));
						queueItem.IsNew = false;
					}
					mailboxTaskContext.TaskDiagnostics.OnBeginMailboxTaskQueueChunk();
					if (queueItem.TaskParameters.OnBeforeTaskStep != null)
					{
						queueItem.TaskParameters.OnBeforeTaskStep(mailboxTaskContext);
					}
					ErrorCode first = mailboxTaskContext.StartMailboxOperation();
					if (first != ErrorCode.NoError)
					{
						return;
					}
					threadInfo.MailboxGuid = mailboxTaskContext.Mailbox.MailboxGuid;
					if (queueItem.TaskParameters.OnInsideTaskStep != null)
					{
						queueItem.TaskParameters.OnInsideTaskStep(mailboxTaskContext);
					}
					DiagnosticContext.TraceLocation((LID)65328U);
					IEnumerator<MailboxTaskQueue.TaskStepResult> taskSteps = queueItem.GetTaskSteps(mailboxTaskContext, shouldTaskContinue);
					DiagnosticContext.TraceLocation((LID)40752U);
					if (taskSteps.MoveNext())
					{
						DiagnosticContext.TraceLocation((LID)57136U);
						MailboxTaskQueue.TaskStepResult taskStepResult = taskSteps.Current;
						action = taskStepResult.PostStepAction;
						flag2 = false;
					}
					DiagnosticContext.TraceLocation((LID)40368U);
					flag = true;
				}
				finally
				{
					DiagnosticContext.TraceLocation((LID)56752U);
					queueItem.Completed = flag2;
					bool flag3 = false;
					try
					{
						if (queueItem.Completed)
						{
							DiagnosticContext.TraceLocation((LID)44464U);
							queueItem.Dispose();
						}
						DiagnosticContext.TraceLocation((LID)60848U);
						flag3 = true;
					}
					finally
					{
						DiagnosticContext.TraceLocation((LID)36272U);
						bool flag4 = false;
						try
						{
							if (mailboxTaskContext.IsMailboxOperationStarted)
							{
								mailboxTaskContext.EndMailboxOperation(flag && flag3, false, !flag2);
							}
							if (queueItem.TaskParameters.OnAfterTaskStep != null)
							{
								queueItem.TaskParameters.OnAfterTaskStep(mailboxTaskContext);
							}
							DiagnosticContext.TraceLocation((LID)38320U);
							if (action != null)
							{
								DiagnosticContext.TraceLocation((LID)42416U);
								action();
							}
							mailboxTaskContext.TaskDiagnostics.OnEndMailboxTaskQueueChunk();
							if (queueItem.Completed)
							{
								mailboxTaskContext.TaskDiagnostics.OnTaskEnd();
							}
							flag4 = true;
						}
						finally
						{
							DiagnosticContext.TraceLocation((LID)58800U);
							if (!flag4 && !queueItem.Completed)
							{
								DiagnosticContext.TraceLocation((LID)54704U);
								queueItem.Completed = true;
								queueItem.Dispose();
							}
						}
					}
				}
			}
			DiagnosticContext.TraceLocation((LID)62896U);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000279E8 File Offset: 0x00025BE8
		private static void WorkerTaskCallback(TaskExecutionDiagnosticsProxy diagnosticsContext, MailboxTaskQueue mailboxTaskQueue, Func<bool> shouldCallbackContinue)
		{
			bool flag = false;
			try
			{
				mailboxTaskQueue.WorkerTaskCallback(diagnosticsContext, shouldCallbackContinue);
				flag = true;
			}
			finally
			{
				if (!flag && ExTraceGlobals.TasksTracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					ExTraceGlobals.TasksTracer.TraceError(34224L, ToStringHelper.GetAsString<byte[]>(DiagnosticContext.PackInfo()));
				}
				else if (ExTraceGlobals.TasksTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.TasksTracer.TraceDebug(52656L, ToStringHelper.GetAsString<byte[]>(DiagnosticContext.PackInfo()));
				}
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00027A68 File Offset: 0x00025C68
		private void QueueTask(Context context, MailboxTaskQueue.Priority priority, TaskTypeId taskTypeId, MailboxState mailboxState, Func<MailboxTaskContext> executionContextCreator, MailboxTaskQueue.MailboxTaskParameters mailboxTaskParameters)
		{
			if (mailboxState.Quarantined)
			{
				throw new StoreException((LID)47865U, ErrorCodeValue.MailboxQuarantined, string.Format("Unable to queue mailbox task {0} for mailbox number {1}", taskTypeId, mailboxState.MailboxNumber));
			}
			using (LockManager.Lock(this.items, LockManager.LockType.LeafMonitorLock, context.Diagnostics))
			{
				if (priority == MailboxTaskQueue.Priority.Low)
				{
					this.items.AddLast(new MailboxTaskQueue.QueueItem(context, taskTypeId, mailboxState, executionContextCreator, mailboxTaskParameters));
				}
				else if (priority == MailboxTaskQueue.Priority.High)
				{
					this.items.AddFirst(new MailboxTaskQueue.QueueItem(context, taskTypeId, mailboxState, executionContextCreator, mailboxTaskParameters));
				}
			}
			this.ScheduleWorkerTaskIfNeeded();
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00027B24 File Offset: 0x00025D24
		private void WorkerTaskCallback(TaskExecutionDiagnosticsProxy diagnosticsContext, Func<bool> shouldCallbackContinue)
		{
			LinkedListNode<MailboxTaskQueue.QueueItem> first;
			using (LockManager.Lock(this.items, LockManager.LockType.LeafMonitorLock))
			{
				first = this.items.First;
			}
			try
			{
				first.Value.TaskCallback(diagnosticsContext, first.Value, shouldCallbackContinue);
			}
			finally
			{
				using (LockManager.Lock(this.items, LockManager.LockType.LeafMonitorLock))
				{
					if (first.Value.Completed)
					{
						this.items.Remove(first);
					}
					this.workerTaskScheduled = false;
				}
				this.ScheduleWorkerTaskIfNeeded();
				DiagnosticContext.TraceLocation((LID)46512U);
			}
		}

		// Token: 0x04000498 RID: 1176
		private static int mailboxStateSlot = -1;

		// Token: 0x04000499 RID: 1177
		private static Hookable<Action> mailboxTaskScheduledTaskTestHook = Hookable<Action>.Create(true, null);

		// Token: 0x0400049A RID: 1178
		private readonly StoreDatabase database;

		// Token: 0x0400049B RID: 1179
		private LinkedList<MailboxTaskQueue.QueueItem> items = new LinkedList<MailboxTaskQueue.QueueItem>();

		// Token: 0x0400049C RID: 1180
		private bool workerTaskScheduled;

		// Token: 0x020000C1 RID: 193
		internal enum Priority
		{
			// Token: 0x0400049E RID: 1182
			Low,
			// Token: 0x0400049F RID: 1183
			High
		}

		// Token: 0x020000C2 RID: 194
		// (Invoke) Token: 0x06000814 RID: 2068
		public delegate IEnumerator<MailboxTaskQueue.TaskStepResult> MailboxTaskDelegate(MailboxTaskContext context, Func<bool> shouldTaskContinue);

		// Token: 0x020000C3 RID: 195
		internal struct TaskStepResult
		{
			// Token: 0x06000817 RID: 2071 RVA: 0x00027C08 File Offset: 0x00025E08
			private TaskStepResult(Action postStepAction)
			{
				this.postStepAction = postStepAction;
			}

			// Token: 0x1700020B RID: 523
			// (get) Token: 0x06000818 RID: 2072 RVA: 0x00027C11 File Offset: 0x00025E11
			internal Action PostStepAction
			{
				get
				{
					return this.postStepAction;
				}
			}

			// Token: 0x06000819 RID: 2073 RVA: 0x00027C19 File Offset: 0x00025E19
			internal static MailboxTaskQueue.TaskStepResult Result(Action postStepAction)
			{
				return new MailboxTaskQueue.TaskStepResult(postStepAction);
			}

			// Token: 0x040004A0 RID: 1184
			private Action postStepAction;
		}

		// Token: 0x020000C4 RID: 196
		internal class MailboxTaskParameters
		{
			// Token: 0x0600081A RID: 2074 RVA: 0x00027C24 File Offset: 0x00025E24
			public MailboxTaskParameters(StoreDatabase mailboxDatabase, int mailboxNumber, SecurityIdentifier userSid, ClientType clientType, CultureInfo culture, Action<Context> onBeforeTaskStep, Action<Context> onInsideTaskStep, Action<Context> onAfterTaskStep, MailboxTaskQueue.MailboxTaskDelegate mailboxTaskDelegate)
			{
				this.mailboxDatabase = mailboxDatabase;
				this.mailboxNumber = mailboxNumber;
				this.userSid = userSid;
				this.clientType = clientType;
				this.culture = culture;
				this.onBeforeTaskStep = onBeforeTaskStep;
				this.onInsideTaskStep = onInsideTaskStep;
				this.onAfterTaskStep = onAfterTaskStep;
				this.mailboxTaskDelegate = mailboxTaskDelegate;
			}

			// Token: 0x1700020C RID: 524
			// (get) Token: 0x0600081B RID: 2075 RVA: 0x00027C7C File Offset: 0x00025E7C
			public StoreDatabase MailboxDatabase
			{
				get
				{
					return this.mailboxDatabase;
				}
			}

			// Token: 0x1700020D RID: 525
			// (get) Token: 0x0600081C RID: 2076 RVA: 0x00027C84 File Offset: 0x00025E84
			public int MailboxNumber
			{
				get
				{
					return this.mailboxNumber;
				}
			}

			// Token: 0x1700020E RID: 526
			// (get) Token: 0x0600081D RID: 2077 RVA: 0x00027C8C File Offset: 0x00025E8C
			public SecurityIdentifier UserSid
			{
				get
				{
					return this.userSid;
				}
			}

			// Token: 0x1700020F RID: 527
			// (get) Token: 0x0600081E RID: 2078 RVA: 0x00027C94 File Offset: 0x00025E94
			public ClientType ClientType
			{
				get
				{
					return this.clientType;
				}
			}

			// Token: 0x17000210 RID: 528
			// (get) Token: 0x0600081F RID: 2079 RVA: 0x00027C9C File Offset: 0x00025E9C
			public CultureInfo Culture
			{
				get
				{
					return this.culture;
				}
			}

			// Token: 0x17000211 RID: 529
			// (get) Token: 0x06000820 RID: 2080 RVA: 0x00027CA4 File Offset: 0x00025EA4
			public Action<Context> OnBeforeTaskStep
			{
				get
				{
					return this.onBeforeTaskStep;
				}
			}

			// Token: 0x17000212 RID: 530
			// (get) Token: 0x06000821 RID: 2081 RVA: 0x00027CAC File Offset: 0x00025EAC
			public Action<Context> OnInsideTaskStep
			{
				get
				{
					return this.onInsideTaskStep;
				}
			}

			// Token: 0x17000213 RID: 531
			// (get) Token: 0x06000822 RID: 2082 RVA: 0x00027CB4 File Offset: 0x00025EB4
			public Action<Context> OnAfterTaskStep
			{
				get
				{
					return this.onAfterTaskStep;
				}
			}

			// Token: 0x17000214 RID: 532
			// (get) Token: 0x06000823 RID: 2083 RVA: 0x00027CBC File Offset: 0x00025EBC
			public MailboxTaskQueue.MailboxTaskDelegate MailboxTaskDelegate
			{
				get
				{
					return this.mailboxTaskDelegate;
				}
			}

			// Token: 0x040004A1 RID: 1185
			private readonly StoreDatabase mailboxDatabase;

			// Token: 0x040004A2 RID: 1186
			private readonly int mailboxNumber;

			// Token: 0x040004A3 RID: 1187
			private readonly SecurityIdentifier userSid;

			// Token: 0x040004A4 RID: 1188
			private readonly ClientType clientType;

			// Token: 0x040004A5 RID: 1189
			private readonly CultureInfo culture;

			// Token: 0x040004A6 RID: 1190
			private readonly Action<Context> onBeforeTaskStep;

			// Token: 0x040004A7 RID: 1191
			private readonly Action<Context> onInsideTaskStep;

			// Token: 0x040004A8 RID: 1192
			private readonly Action<Context> onAfterTaskStep;

			// Token: 0x040004A9 RID: 1193
			private readonly MailboxTaskQueue.MailboxTaskDelegate mailboxTaskDelegate;
		}

		// Token: 0x020000C5 RID: 197
		private class QueueItem : DisposableBase
		{
			// Token: 0x06000824 RID: 2084 RVA: 0x00027CC4 File Offset: 0x00025EC4
			internal QueueItem(Context context, TaskTypeId taskTypeId, MailboxState mailboxState, Func<MailboxTaskContext> executionContextCreator, MailboxTaskQueue.MailboxTaskParameters taskParameters)
			{
				this.executionContextCreator = executionContextCreator;
				Guid clientActivityId = context.Diagnostics.ClientActivityId;
				string clientComponentName = context.Diagnostics.ClientComponentName;
				string clientProtocolName = context.Diagnostics.ClientProtocolName;
				string clientActionString = context.Diagnostics.ClientActionString;
				this.taskCallback = TaskExecutionWrapper<MailboxTaskQueue.QueueItem>.WrapExecute<MailboxTaskContext>(new TaskDiagnosticInformation(taskTypeId, context.ClientType, context.Database.MdbGuid, mailboxState.MailboxGuid, clientActivityId, clientComponentName, clientProtocolName, clientActionString), new TaskExecutionWrapper<MailboxTaskQueue.QueueItem>.TaskCallback<MailboxTaskContext>(MailboxTaskQueue.RunMailboxTaskStep), new Func<MailboxTaskContext>(this.GetMailboxTaskExecutionContext), new Action<MailboxTaskContext>(this.ReleaseMailboxTaskExecutionContext), true);
				this.taskParameters = taskParameters;
				this.taskSteps = null;
				this.completed = false;
				this.isNew = true;
			}

			// Token: 0x17000215 RID: 533
			// (get) Token: 0x06000825 RID: 2085 RVA: 0x00027D7D File Offset: 0x00025F7D
			internal Task<MailboxTaskQueue.QueueItem>.TaskCallback TaskCallback
			{
				get
				{
					return this.taskCallback;
				}
			}

			// Token: 0x17000216 RID: 534
			// (get) Token: 0x06000826 RID: 2086 RVA: 0x00027D85 File Offset: 0x00025F85
			internal MailboxTaskQueue.MailboxTaskParameters TaskParameters
			{
				get
				{
					return this.taskParameters;
				}
			}

			// Token: 0x17000217 RID: 535
			// (get) Token: 0x06000827 RID: 2087 RVA: 0x00027D8D File Offset: 0x00025F8D
			// (set) Token: 0x06000828 RID: 2088 RVA: 0x00027D95 File Offset: 0x00025F95
			internal bool Completed
			{
				get
				{
					return this.completed;
				}
				set
				{
					this.completed = value;
				}
			}

			// Token: 0x17000218 RID: 536
			// (get) Token: 0x06000829 RID: 2089 RVA: 0x00027D9E File Offset: 0x00025F9E
			// (set) Token: 0x0600082A RID: 2090 RVA: 0x00027DA6 File Offset: 0x00025FA6
			internal bool IsNew
			{
				get
				{
					return this.isNew;
				}
				set
				{
					this.isNew = value;
				}
			}

			// Token: 0x0600082B RID: 2091 RVA: 0x00027DAF File Offset: 0x00025FAF
			internal void Cleanup()
			{
				if (this.currentMailboxTaskContext != null)
				{
					this.ReleaseMailboxTaskExecutionContext(this.currentMailboxTaskContext);
				}
			}

			// Token: 0x0600082C RID: 2092 RVA: 0x00027DC5 File Offset: 0x00025FC5
			protected override DisposeTracker InternalGetDisposeTracker()
			{
				return DisposeTracker.Get<MailboxTaskQueue.QueueItem>(this);
			}

			// Token: 0x0600082D RID: 2093 RVA: 0x00027DCD File Offset: 0x00025FCD
			protected override void InternalDispose(bool calledFromDispose)
			{
				if (calledFromDispose && this.taskSteps != null)
				{
					this.taskSteps.Dispose();
					this.taskSteps = null;
				}
			}

			// Token: 0x0600082E RID: 2094 RVA: 0x00027DEC File Offset: 0x00025FEC
			internal IEnumerator<MailboxTaskQueue.TaskStepResult> GetTaskSteps(MailboxTaskContext mailboxTaskContext, Func<bool> shouldTaskContinue)
			{
				this.currentShouldTaskContinueCallback = shouldTaskContinue;
				if (this.taskSteps == null)
				{
					this.taskSteps = this.taskParameters.MailboxTaskDelegate(mailboxTaskContext, new Func<bool>(this.ShouldTaskContinueCallback));
				}
				return this.taskSteps;
			}

			// Token: 0x0600082F RID: 2095 RVA: 0x00027E26 File Offset: 0x00026026
			private MailboxTaskContext GetMailboxTaskExecutionContext()
			{
				if (this.currentMailboxTaskContext == null)
				{
					this.currentMailboxTaskContext = this.executionContextCreator();
				}
				return this.currentMailboxTaskContext;
			}

			// Token: 0x06000830 RID: 2096 RVA: 0x00027E47 File Offset: 0x00026047
			private void ReleaseMailboxTaskExecutionContext(MailboxTaskContext mailboxTaskContext)
			{
				if (this.Completed)
				{
					this.currentMailboxTaskContext.Dispose();
					this.currentMailboxTaskContext = null;
				}
			}

			// Token: 0x06000831 RID: 2097 RVA: 0x00027E63 File Offset: 0x00026063
			private bool ShouldTaskContinueCallback()
			{
				return this.currentShouldTaskContinueCallback();
			}

			// Token: 0x040004AA RID: 1194
			private readonly Func<MailboxTaskContext> executionContextCreator;

			// Token: 0x040004AB RID: 1195
			private readonly Task<MailboxTaskQueue.QueueItem>.TaskCallback taskCallback;

			// Token: 0x040004AC RID: 1196
			private readonly MailboxTaskQueue.MailboxTaskParameters taskParameters;

			// Token: 0x040004AD RID: 1197
			private MailboxTaskContext currentMailboxTaskContext;

			// Token: 0x040004AE RID: 1198
			private Func<bool> currentShouldTaskContinueCallback;

			// Token: 0x040004AF RID: 1199
			private IEnumerator<MailboxTaskQueue.TaskStepResult> taskSteps;

			// Token: 0x040004B0 RID: 1200
			private bool completed;

			// Token: 0x040004B1 RID: 1201
			private bool isNew;
		}
	}
}
