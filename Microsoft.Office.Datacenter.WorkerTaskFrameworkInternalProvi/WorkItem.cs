using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x02000016 RID: 22
	public abstract class WorkItem : MarshalByRefObject
	{
		// Token: 0x06000238 RID: 568 RVA: 0x00009376 File Offset: 0x00007576
		protected WorkItem()
		{
			this.cancellationTokenSource = new CancellationTokenSource();
			this.InstanceId = WorkItem.GenerateSerialId();
			this.tracingContext = new TracingContext(this);
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000239 RID: 569 RVA: 0x000093A0 File Offset: 0x000075A0
		public int Id
		{
			get
			{
				return this.definition.Id;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000093AD File Offset: 0x000075AD
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000093B5 File Offset: 0x000075B5
		public int InstanceId { get; private set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000093BE File Offset: 0x000075BE
		public TracingContext TraceContext
		{
			get
			{
				return this.tracingContext;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000093C6 File Offset: 0x000075C6
		internal DateTime DueTime
		{
			get
			{
				return this.whenDue;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600023E RID: 574 RVA: 0x000093CE File Offset: 0x000075CE
		internal bool IsCompleted
		{
			get
			{
				return this.result.ExecutionEndTime != DateTime.MaxValue;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600023F RID: 575 RVA: 0x000093E5 File Offset: 0x000075E5
		internal bool IsPending
		{
			get
			{
				return this.result.ExecutionStartTime == DateTime.MaxValue;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000093FC File Offset: 0x000075FC
		protected internal WorkBroker Broker
		{
			get
			{
				return this.broker;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00009404 File Offset: 0x00007604
		protected internal WorkDefinition Definition
		{
			get
			{
				return this.definition;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0000940C File Offset: 0x0000760C
		protected internal WorkItemResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009414 File Offset: 0x00007614
		public virtual void PopulateDefinition<TDefinition>(TDefinition definition, Dictionary<string, string> propertyBag) where TDefinition : WorkDefinition
		{
			throw new NotImplementedException(string.Format("PopulateDefinition is not implemented for WorkItem '{0}'", base.GetType()));
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000942C File Offset: 0x0000762C
		internal IEnumerable<PropertyInformation> GetPropertyInformation(IEnumerable<PropertyInformation> substitutePropInfo)
		{
			Dictionary<string, PropertyInformation> dictionary = new Dictionary<string, PropertyInformation>(StringComparer.OrdinalIgnoreCase);
			Type definitionType = this.GetDefinitionType();
			foreach (PropertyInfo propertyInfo in definitionType.GetProperties())
			{
				Attribute[] customAttributes = Attribute.GetCustomAttributes(propertyInfo);
				foreach (Attribute attribute in customAttributes)
				{
					if (attribute is PropertyInformationAttribute)
					{
						PropertyInformationAttribute propertyInformationAttribute = (PropertyInformationAttribute)attribute;
						dictionary.Add(propertyInfo.Name, new PropertyInformation(propertyInfo.Name, propertyInformationAttribute.Description, propertyInformationAttribute.IsMandatory));
					}
				}
			}
			if (substitutePropInfo != null)
			{
				foreach (PropertyInformation propertyInformation in substitutePropInfo)
				{
					dictionary[propertyInformation.Name] = propertyInformation;
				}
			}
			return dictionary.Values;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000951C File Offset: 0x0000771C
		internal virtual IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return null;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000951F File Offset: 0x0000771F
		internal void Initialize(WorkDefinition definition)
		{
			this.Initialize(definition, definition.CreateResult(), null);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00009530 File Offset: 0x00007730
		internal void Initialize(WorkDefinition definition, WorkItemResult result, WorkBroker broker)
		{
			this.definition = definition;
			this.result = result;
			this.broker = broker;
			this.tracingContext.Id = this.definition.Id;
			this.tracingContext.LId = this.definition.ExecutionId;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000096E8 File Offset: 0x000078E8
		internal Task StartExecuting(Action<WorkItemResult, TracingContext> continuation)
		{
			if (Settings.CalculateTimeoutFromBeginningOfExecution)
			{
				this.whenDue = DateTime.MaxValue;
			}
			else
			{
				this.whenDue = DateTime.UtcNow + TimeSpan.FromSeconds((double)this.definition.TimeoutSeconds);
			}
			CancellationToken cancellationToken = this.cancellationTokenSource.Token;
			this.workitemExecution = new Task(delegate()
			{
				CallContext.SetData("WTFTraceContext", this.TraceContext);
				WTFDiagnostics.TraceFunction(WTFLog.WorkItem, this.TraceContext, "[Workitem.StartExecuting]: Started workitemExecution task.", null, "StartExecuting", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 289);
				this.Execute(cancellationToken);
				WTFDiagnostics.TraceFunction(WTFLog.WorkItem, this.TraceContext, "[Workitem.StartExecuting]: Finished workitemExecution task. Child tasks might be executing still.", null, "StartExecuting", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 291);
			}, cancellationToken);
			TaskContinuationOptions taskContinuationOptions = TaskContinuationOptions.AttachedToParent;
			if (Settings.UseSynchronousContinuationForWorkitemResults)
			{
				taskContinuationOptions |= TaskContinuationOptions.ExecuteSynchronously;
			}
			Task task = this.workitemExecution.ContinueWith(delegate(Task t)
			{
				WTFDiagnostics.TraceFunction(WTFLog.WorkItem, this.TraceContext, "[Workitem.StartExecuting]: Started workitemExecution continuation.", null, "StartExecuting", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 313);
				if (t.Status == TaskStatus.RanToCompletion)
				{
					this.result.SetCompleted(ResultType.Succeeded);
				}
				else
				{
					this.result.SetCompleted(ResultType.Failed, t.Exception);
					WTFDiagnostics.TraceError<string, int, string>(WTFLog.WorkItem, this.TraceContext, "[WorkItem.StartExecuting]: '{0}' (Id={1}) error = {2}.", this.Definition.Name, this.Definition.Id, this.Result.Error, null, "StartExecuting", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 322);
				}
				this.CompleteExecution(continuation);
			}, cancellationToken, taskContinuationOptions, TaskScheduler.Current);
			this.workitemExecution.Start();
			return task;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000098F4 File Offset: 0x00007AF4
		internal Task StartCancel(int waitAmount, Action<WorkItemResult, TracingContext> continuation, bool forcedCancellationDueToControllerQuitting)
		{
			Task task = Task.Factory.StartNew(delegate()
			{
				WTFDiagnostics.TraceFunction(WTFLog.WorkItem, this.TraceContext, "[Workitem.StartCancel]: Started Cancel task.", null, "StartCancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 349);
				this.Cancel(waitAmount, forcedCancellationDueToControllerQuitting);
				WTFDiagnostics.TraceFunction(WTFLog.WorkItem, this.TraceContext, "[Workitem.StartCancel]: Finished Cancel task.", null, "StartCancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 351);
			}, TaskCreationOptions.LongRunning);
			return task.ContinueWith(delegate(Task t)
			{
				WTFDiagnostics.TraceFunction(WTFLog.WorkItem, this.TraceContext, "[Workitem.StartCancel]: Started Cancel continuation task.", null, "StartCancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 359);
				if (t.IsFaulted)
				{
					this.result.SetCompleted(ResultType.Failed, t.Exception);
					WTFDiagnostics.TraceError<string, int, string>(WTFLog.WorkItem, this.TraceContext, "[WorkItem.StartCancel]: '{0}' (Id={1}) cancellation error = {2}.", this.Definition.Name, this.Definition.Id, this.Result.Error, null, "StartCancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 364);
				}
				this.CompleteExecution(continuation);
			}, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x0600024A RID: 586
		protected abstract void DoWork(CancellationToken cancellationToken);

		// Token: 0x0600024B RID: 587 RVA: 0x00009950 File Offset: 0x00007B50
		protected virtual bool ShouldTakeWatsonOnTimeout()
		{
			return false;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00009953 File Offset: 0x00007B53
		private static int GenerateSerialId()
		{
			return Interlocked.Increment(ref WorkItem.serialId);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00009960 File Offset: 0x00007B60
		private void Cancel(int waitAmount, bool forcedCancellationDueToControllerQuitting)
		{
			TaskStatus status = this.workitemExecution.Status;
			WTFDiagnostics.TraceInformation<string, int, string>(WTFLog.WorkItem, this.TraceContext, "[WorkItem.Cancel]: Attempting to cancel WorkItem '{0}' (Id={1}). Its current status is {2}.", this.Definition.Name, this.Definition.Id, status.ToString(), null, "Cancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 416);
			if (!forcedCancellationDueToControllerQuitting && status == TaskStatus.Running && this.ShouldTakeWatsonOnTimeout())
			{
				string message = string.Format("[WorkItem.Cancel]: MaintenanceWorkItem '{0}' (Id={1}) timed out. Taking a Watson to figure out why.", this.Definition.Name, this.Definition.Id);
				Exception exception = new TimeoutException(message);
				WTFDiagnostics.SendWatson(exception);
			}
			this.cancellationTokenSource.Cancel();
			try
			{
				bool flag = this.workitemExecution.Wait(waitAmount);
				if (forcedCancellationDueToControllerQuitting)
				{
					this.result.SetCompleted(ResultType.Abandoned);
					WTFDiagnostics.TraceInformation(WTFLog.WorkItem, this.TraceContext, "[WorkItem.Cancel]: The controller is quitting, so marking the cancelled result as Abandoned.", null, "Cancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 441);
				}
				else if (flag)
				{
					string message2 = string.Format("[WorkItem.Cancel]: WorkItem ran too long but eventually ended WITHOUT throwing OperationCanceledException (TaskCanceledException). Status at time of cancellation attempt was {0}.", status.ToString());
					Exception e = new TimeoutException(message2);
					this.result.SetCompleted(ResultType.TimedOut, e);
					WTFDiagnostics.TraceInformation(WTFLog.WorkItem, this.TraceContext, message2, null, "Cancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 449);
				}
				else
				{
					string message3 = string.Format("[WorkItem.Cancel]: WorkItem ran too long and did not respond to cancellation token within the {0} second grace period. Marking it as poisoned. Status at time of cancellation attempt was {1}.", waitAmount / 1000, status.ToString());
					Exception e2 = new TimeoutException(message3);
					this.result.SetCompleted(ResultType.Poisoned, e2);
					WTFDiagnostics.TraceInformation(WTFLog.WorkItem, this.TraceContext, message3, null, "Cancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 457);
				}
			}
			catch (AggregateException ex)
			{
				ex = ex.Flatten();
				if (ex.InnerExceptions.Count != 1 || !(ex.InnerException is OperationCanceledException))
				{
					WTFDiagnostics.TraceError<AggregateException>(WTFLog.WorkItem, this.TraceContext, "[WorkItem.Cancel]: The workitem already finished with an error, so make sure the parent task gets it. Exception = {0}", ex, null, "Cancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 489);
					throw;
				}
				if (forcedCancellationDueToControllerQuitting)
				{
					this.result.SetCompleted(ResultType.Abandoned);
					WTFDiagnostics.TraceInformation<TaskStatus>(WTFLog.WorkItem, this.TraceContext, "[WorkItem.Cancel]: WorkItem acknowledged cancellation token by throwing OperationCanceledException (TaskCanceledException). The controller is quitting, so marking the cancelled result as Abandoned. Status of workitem before cancelling: {0}.", status, null, "Cancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 470);
				}
				else if (this.result.ExecutionStartTime == DateTime.MaxValue)
				{
					string message4 = string.Format("[WorkItem.Cancel]: WorkItem acknowledged cancellation token by throwing OperationCanceledException (TaskCanceledException). Setting result to Abandoned because this.result.ExecutionStartTime == DateTime.MaxValue. Status of workitem before cancelling: {0}. Exception message = {1}", status, ex.InnerException.ToString());
					this.result.SetCompleted(ResultType.Abandoned, new TaskCanceledException(message4));
					WTFDiagnostics.TraceError(WTFLog.WorkItem, this.TraceContext, message4, null, "Cancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 477);
				}
				else
				{
					string message5 = string.Format("[WorkItem.Cancel]: WorkItem acknowledged cancellation token by throwing OperationCanceledException (TaskCanceledException). Work item ran too long. Status of workitem before cancelling: {0}. Exception message = {1}", status, ex.InnerException.ToString());
					WTFDiagnostics.TraceError(WTFLog.WorkItem, this.TraceContext, message5, null, "Cancel", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 482);
					this.result.SetCompleted(ResultType.TimedOut, new TaskCanceledException(message5));
				}
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009C78 File Offset: 0x00007E78
		private void Execute(CancellationToken joinedToken)
		{
			this.result.ExecutionStartTime = DateTime.UtcNow;
			WTFDiagnostics.TraceInformation<string, int>(WTFLog.WorkItem, this.TraceContext, "[WorkItem.Execute]: '{0}' (Id={1}) started.", this.Definition.Name, this.Definition.Id, null, "Execute", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 508);
			if (Settings.CalculateTimeoutFromBeginningOfExecution)
			{
				this.whenDue = this.result.ExecutionStartTime + TimeSpan.FromSeconds((double)this.definition.TimeoutSeconds);
			}
			if (!joinedToken.IsCancellationRequested)
			{
				this.DoWork(joinedToken);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009D10 File Offset: 0x00007F10
		private void CompleteExecution(Action<WorkItemResult, TracingContext> continuation)
		{
			int num = Interlocked.Increment(ref this.completed);
			if (num == 1)
			{
				this.cancellationTokenSource.Dispose();
				WTFDiagnostics.TraceInformation<string, int, ResultType>(WTFLog.WorkItem, this.TraceContext, "[WorkItem.CompleteExecution]: '{0}' (Id={1}) result = {2}.", this.Definition.Name, this.Definition.Id, this.Result.ResultType, null, "CompleteExecution", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 547);
				continuation(this.result, this.TraceContext);
				WTFDiagnostics.TraceFunction(WTFLog.WorkItem, this.TraceContext, "[Workitem.CompleteExecution]: Finished executing the continuation task.", null, "CompleteExecution", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 549);
				return;
			}
			WTFDiagnostics.TraceInformation<string, int, ResultType>(WTFLog.WorkItem, this.TraceContext, "[WorkItem.CompleteExecution]: '{0}' (Id={1}) completed execution after cancellation started. Second result was skipped. result = {2}.", this.Definition.Name, this.Definition.Id, this.Result.ResultType, null, "CompleteExecution", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\Core\\WorkItem.cs", 555);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009E00 File Offset: 0x00008000
		private Type GetDefinitionType()
		{
			Type type = base.GetType();
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.Name.Equals("Definition") && (propertyInfo.PropertyType.Attributes & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
				{
					return propertyInfo.PropertyType;
				}
			}
			return null;
		}

		// Token: 0x040000D1 RID: 209
		private static int serialId;

		// Token: 0x040000D2 RID: 210
		private readonly CancellationTokenSource cancellationTokenSource;

		// Token: 0x040000D3 RID: 211
		private TracingContext tracingContext;

		// Token: 0x040000D4 RID: 212
		private WorkDefinition definition;

		// Token: 0x040000D5 RID: 213
		private WorkItemResult result;

		// Token: 0x040000D6 RID: 214
		private WorkBroker broker;

		// Token: 0x040000D7 RID: 215
		private Task workitemExecution;

		// Token: 0x040000D8 RID: 216
		private int completed;

		// Token: 0x040000D9 RID: 217
		private DateTime whenDue;
	}
}
