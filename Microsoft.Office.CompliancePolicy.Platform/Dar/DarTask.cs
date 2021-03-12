using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Office.CompliancePolicy.Dar
{
	// Token: 0x02000058 RID: 88
	public abstract class DarTask
	{
		// Token: 0x06000209 RID: 521 RVA: 0x000065F8 File Offset: 0x000047F8
		protected DarTask()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00006624 File Offset: 0x00004824
		// (set) Token: 0x0600020B RID: 523 RVA: 0x0000662C File Offset: 0x0000482C
		public string Id { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00006635 File Offset: 0x00004835
		public virtual string CorrelationId
		{
			get
			{
				return this.Id;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000663D File Offset: 0x0000483D
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00006645 File Offset: 0x00004845
		public string Name { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600020F RID: 527
		public abstract string TaskType { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000664E File Offset: 0x0000484E
		// (set) Token: 0x06000211 RID: 529 RVA: 0x00006656 File Offset: 0x00004856
		public string TenantId { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000665F File Offset: 0x0000485F
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00006667 File Offset: 0x00004867
		public virtual DarTaskCategory Category { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00006670 File Offset: 0x00004870
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00006678 File Offset: 0x00004878
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int Priority { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006681 File Offset: 0x00004881
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00006689 File Offset: 0x00004889
		public TaskSynchronizationOption TaskSynchronizationOption { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006692 File Offset: 0x00004892
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000669A File Offset: 0x0000489A
		public string TaskSynchronizationKey { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000066A3 File Offset: 0x000048A3
		// (set) Token: 0x0600021B RID: 539 RVA: 0x000066AB File Offset: 0x000048AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DarTaskState PreviousTaskState { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000066B4 File Offset: 0x000048B4
		// (set) Token: 0x0600021D RID: 541 RVA: 0x000066BC File Offset: 0x000048BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DarTaskState TaskState
		{
			get
			{
				return this.taskState;
			}
			set
			{
				DarTaskState darTaskState = this.taskState;
				this.taskState = value;
				if (this.taskState != darTaskState)
				{
					DateTime utcNow = DateTime.UtcNow;
					switch (this.taskState)
					{
					case DarTaskState.Running:
						if (this.TaskExecutionStartTime == default(DateTime))
						{
							this.TaskExecutionStartTime = utcNow;
						}
						break;
					case DarTaskState.Completed:
					case DarTaskState.Failed:
					case DarTaskState.Cancelled:
						this.TaskCompletionTime = utcNow;
						break;
					}
					this.PreviousTaskState = darTaskState;
				}
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00006734 File Offset: 0x00004934
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000673C File Offset: 0x0000493C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DateTime TaskQueuedTime { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00006745 File Offset: 0x00004945
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000674D File Offset: 0x0000494D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DateTime MinTaskScheduleTime { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00006756 File Offset: 0x00004956
		// (set) Token: 0x06000223 RID: 547 RVA: 0x0000675E File Offset: 0x0000495E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DateTime TaskScheduledTime { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006767 File Offset: 0x00004967
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000676F File Offset: 0x0000496F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DateTime TaskExecutionStartTime { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00006778 File Offset: 0x00004978
		// (set) Token: 0x06000227 RID: 551 RVA: 0x00006780 File Offset: 0x00004980
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DateTime TaskCompletionTime { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00006789 File Offset: 0x00004989
		// (set) Token: 0x06000229 RID: 553 RVA: 0x00006791 File Offset: 0x00004991
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DateTime TaskLastExecutionTime { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000679A File Offset: 0x0000499A
		// (set) Token: 0x0600022B RID: 555 RVA: 0x000067A2 File Offset: 0x000049A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int TaskRetryTotalCount { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000067AB File Offset: 0x000049AB
		// (set) Token: 0x0600022D RID: 557 RVA: 0x000067B3 File Offset: 0x000049B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TimeSpan TaskRetryInterval { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600022E RID: 558 RVA: 0x000067BC File Offset: 0x000049BC
		// (set) Token: 0x0600022F RID: 559 RVA: 0x000067C4 File Offset: 0x000049C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int TaskRetryCurrentCount { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000067CD File Offset: 0x000049CD
		// (set) Token: 0x06000231 RID: 561 RVA: 0x000067D5 File Offset: 0x000049D5
		public string SerializedTaskData { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000067DE File Offset: 0x000049DE
		// (set) Token: 0x06000233 RID: 563 RVA: 0x000067E6 File Offset: 0x000049E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object WorkloadContext { get; set; }

		// Token: 0x06000234 RID: 564 RVA: 0x000067F0 File Offset: 0x000049F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InvokeTask(DarTaskManager darTaskManager)
		{
			DarTaskExecutionResult executionResult = DarTaskExecutionResult.Yielded;
			try
			{
				darTaskManager.ExecutionLog.LogInformation("DarTask", null, this.CorrelationId, string.Format("Invoking task {0}", this), new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTask0.ToString())
				});
				this.TaskState = DarTaskState.Running;
				this.TaskExecutionStartTime = DateTime.UtcNow;
				darTaskManager.UpdateTaskState(this);
				if (this.SerializedTaskData != null && !this.RestoreStateFromSerializedData(darTaskManager))
				{
					darTaskManager.ExecutionLog.LogInformation("DarTask", null, this.CorrelationId, string.Format("Restoring state from serialized data returned false for task {0}", this), new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTask1.ToString())
					});
					executionResult = DarTaskExecutionResult.Failed;
				}
				else
				{
					executionResult = this.Execute(darTaskManager);
				}
			}
			catch (Exception exception)
			{
				executionResult = DarTaskExecutionResult.Failed;
				darTaskManager.ExecutionLog.LogError("DarTask", null, this.CorrelationId, exception, string.Format("Task {0} threw exception.", this), new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTask2.ToString())
				});
			}
			finally
			{
				this.TaskLastExecutionTime = DateTime.UtcNow;
				this.SetTaskState(darTaskManager, executionResult);
				if (this.TaskState != DarTaskState.Cancelled && this.TaskState != DarTaskState.Failed)
				{
					if (this.TaskState != DarTaskState.Completed)
					{
						goto IL_1BE;
					}
				}
				try
				{
					this.CompleteTask(darTaskManager);
				}
				catch (Exception exception2)
				{
					executionResult = DarTaskExecutionResult.Failed;
					darTaskManager.ExecutionLog.LogError("DarTask", null, this.CorrelationId, exception2, string.Format("Task {0} completion code threw exception.", this), new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTask7.ToString())
					});
				}
				IL_1BE:
				this.OnExecuted(this.TaskState);
				this.SaveStateToSerializedData(darTaskManager);
				darTaskManager.UpdateTaskState(this, executionResult);
				darTaskManager.ExecutionLog.LogInformation("DarTask", null, this.CorrelationId, string.Format("Exiting task execution {0}", this), new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTask3.ToString())
				});
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006A74 File Offset: 0x00004C74
		public override bool Equals(object obj)
		{
			DarTask darTask = obj as DarTask;
			return darTask != null && this.Id == darTask.Id;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006A9E File Offset: 0x00004C9E
		public override int GetHashCode()
		{
			if (this.Id == null)
			{
				return 0;
			}
			return this.Id.GetHashCode();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006AB5 File Offset: 0x00004CB5
		public override string ToString()
		{
			return string.Format("{0} - {1}", this.TaskType, this.Id);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public virtual void SaveStateToSerializedData(DarTaskManager darTaskManager)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (PropertyInfo propertyInfo in base.GetType().GetProperties())
			{
				if (propertyInfo.GetCustomAttributes(typeof(SerializableTaskDataAttribute), false).Length > 0)
				{
					object value = propertyInfo.GetValue(this, null);
					if (value != null)
					{
						Type type = value.GetType();
						if (!type.IsValueType || !value.Equals(Activator.CreateInstance(type)))
						{
							dictionary.Add(propertyInfo.Name, value);
						}
					}
				}
			}
			if (dictionary.Count > 0)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this.GetSerializer().WriteObject(memoryStream, dictionary);
					this.SerializedTaskData = Encoding.UTF8.GetString(memoryStream.ToArray());
				}
			}
		}

		// Token: 0x06000239 RID: 569
		public abstract DarTaskExecutionResult Execute(DarTaskManager darTaskManager);

		// Token: 0x0600023A RID: 570 RVA: 0x00006BA8 File Offset: 0x00004DA8
		public virtual bool RestoreStateFromSerializedData(DarTaskManager darTaskManager)
		{
			bool result;
			try
			{
				if (!string.IsNullOrEmpty(this.SerializedTaskData))
				{
					Dictionary<string, object> dictionary = (Dictionary<string, object>)this.GetSerializer().ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(this.SerializedTaskData)));
					foreach (KeyValuePair<string, object> keyValuePair in dictionary)
					{
						base.GetType().GetProperty(keyValuePair.Key).SetValue(this, keyValuePair.Value, null);
					}
				}
				result = true;
			}
			catch (Exception exception)
			{
				darTaskManager.ExecutionLog.LogError("DarTask", null, this.CorrelationId, exception, string.Format("Could not restore task state from string \"{0}\"", this.SerializedTaskData), new KeyValuePair<string, object>[]
				{
					new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTask5.ToString())
				});
				result = false;
			}
			return result;
		}

		// Token: 0x0600023B RID: 571
		public abstract void CompleteTask(DarTaskManager darTaskManager);

		// Token: 0x0600023C RID: 572 RVA: 0x00006CB0 File Offset: 0x00004EB0
		internal bool ShouldContinue(DarTaskManager taskManager)
		{
			string arg;
			if (taskManager.ShouldContinue(this, out arg) == DarTaskExecutionCommand.ContinueExecution)
			{
				return true;
			}
			taskManager.ServiceProvider.ExecutionLog.LogInformation("DarTask", null, this.CorrelationId, string.Format("Stopping processing due to ShouldContinue: {0}", arg), new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTask6.ToString())
			});
			return false;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00006DEC File Offset: 0x00004FEC
		protected virtual IEnumerable<Type> GetKnownTypes()
		{
			yield return typeof(List<string>);
			yield break;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00006E09 File Offset: 0x00005009
		protected virtual void OnExecuted(DarTaskState executionState)
		{
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00006E0C File Offset: 0x0000500C
		private DataContractJsonSerializer GetSerializer()
		{
			return new DataContractJsonSerializer(typeof(Dictionary<string, object>), new DataContractJsonSerializerSettings
			{
				UseSimpleDictionaryFormat = true,
				KnownTypes = this.GetKnownTypes().ToArray<Type>()
			});
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006E48 File Offset: 0x00005048
		private void SetTaskState(DarTaskManager darTaskManager, DarTaskExecutionResult executionResult)
		{
			switch (executionResult)
			{
			case DarTaskExecutionResult.Completed:
				this.TaskState = DarTaskState.Completed;
				return;
			case DarTaskExecutionResult.Yielded:
				this.TaskState = DarTaskState.Ready;
				return;
			case DarTaskExecutionResult.Failed:
				this.TaskState = DarTaskState.Failed;
				return;
			case DarTaskExecutionResult.TransientError:
				this.TaskRetryCurrentCount++;
				if (this.TaskRetryCurrentCount > this.TaskRetryTotalCount)
				{
					darTaskManager.ExecutionLog.LogError("DarTask", null, this.CorrelationId, null, string.Format("{0} exceeded total retry count of {1}", this, this.TaskRetryTotalCount), new KeyValuePair<string, object>[]
					{
						new KeyValuePair<string, object>("Tag", DarExecutionLogClientIDs.DarTask4.ToString())
					});
					this.TaskState = DarTaskState.Failed;
					return;
				}
				this.MinTaskScheduleTime = DateTime.UtcNow.Add(this.TaskRetryInterval);
				this.TaskState = DarTaskState.Ready;
				return;
			default:
				return;
			}
		}

		// Token: 0x0400010F RID: 271
		private const string LoggingClientId = "DarTask";

		// Token: 0x04000110 RID: 272
		private DarTaskState taskState;
	}
}
