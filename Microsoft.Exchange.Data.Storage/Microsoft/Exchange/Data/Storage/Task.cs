using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000844 RID: 2116
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class Task : Item, IToDoItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06004E9C RID: 20124 RVA: 0x00149161 File Offset: 0x00147361
		internal Task(ICoreItem coreItem) : base(coreItem, false)
		{
			ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::Constructor.");
			this.masterProperties = this.GetTaskMasterProperties();
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x00149199 File Offset: 0x00147399
		public static Task Create(StoreSession session, StoreId parentFolderId)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (parentFolderId == null)
			{
				throw new ArgumentNullException("parentFolderId");
			}
			return Task.CreateInternal(session, parentFolderId);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x001491BE File Offset: 0x001473BE
		public new static Task Bind(StoreSession session, StoreId id)
		{
			return Task.Bind(session, id, null);
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x001491C8 File Offset: 0x001473C8
		public new static Task Bind(StoreSession session, StoreId id, params PropertyDefinition[] propsToReturn)
		{
			return Task.Bind(session, id, (ICollection<PropertyDefinition>)propsToReturn);
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x001491D7 File Offset: 0x001473D7
		public new static Task Bind(StoreSession session, StoreId id, ICollection<PropertyDefinition> propsToReturn)
		{
			return Task.Bind(session, id, false, propsToReturn);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x001491E2 File Offset: 0x001473E2
		public static Task Bind(StoreSession session, StoreId id, bool suppressCreateOneOff)
		{
			return Task.Bind(session, id, suppressCreateOneOff, null);
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x001491F0 File Offset: 0x001473F0
		public static Task Bind(StoreSession session, StoreId id, bool suppressCreateOneOff, ICollection<PropertyDefinition> propsToReturn)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			Task task = ItemBuilder.ItemBind<Task>(session, id, TaskSchema.Instance, propsToReturn);
			if (suppressCreateOneOff)
			{
				ExTraceGlobals.TaskTracer.TraceDebug(0L, "Task::Bind. SuppressCreateOneOff.");
				task.SuppressCreateOneOff = true;
			}
			return task;
		}

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x06004EA3 RID: 20131 RVA: 0x00149243 File Offset: 0x00147443
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return TaskSchema.Instance;
			}
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x00149255 File Offset: 0x00147455
		public override void SetFlag(string flagRequest, ExDateTime? startDate, ExDateTime? dueDate)
		{
			this.CheckDisposed("SetFlag");
			throw new StoragePermanentException(ServerStrings.InvokingMethodNotSupported("Task", "SetFlag"));
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x00149276 File Offset: 0x00147476
		public override void CompleteFlag(ExDateTime? completeTime)
		{
			this.CheckDisposed("CompleteFlag");
			throw new StoragePermanentException(ServerStrings.InvokingMethodNotSupported("Task", "CompleteFlag"));
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x00149297 File Offset: 0x00147497
		public override void ClearFlag()
		{
			this.CheckDisposed("ClearFlag");
			throw new StoragePermanentException(ServerStrings.InvokingMethodNotSupported("Task", "ClearFlag"));
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x06004EA7 RID: 20135 RVA: 0x001492B8 File Offset: 0x001474B8
		// (set) Token: 0x06004EA8 RID: 20136 RVA: 0x001492C0 File Offset: 0x001474C0
		public override string Subject
		{
			get
			{
				return base.Subject;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Subject::set");
				}
				base.Subject = value;
			}
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06004EA9 RID: 20137 RVA: 0x001492D7 File Offset: 0x001474D7
		// (set) Token: 0x06004EAA RID: 20138 RVA: 0x001492F0 File Offset: 0x001474F0
		public ExDateTime? StartDate
		{
			get
			{
				this.CheckDisposed("StartDate::get");
				return base.GetValueAsNullable<ExDateTime>(InternalSchema.StartDate);
			}
			set
			{
				this.CheckDisposed("StartDate::set");
				if (this.StartDate == value)
				{
					return;
				}
				this.SetTaskDateInternal(InternalSchema.StartDate, value);
			}
		}

		// Token: 0x17001643 RID: 5699
		// (get) Token: 0x06004EAB RID: 20139 RVA: 0x00149352 File Offset: 0x00147552
		// (set) Token: 0x06004EAC RID: 20140 RVA: 0x0014936C File Offset: 0x0014756C
		public ExDateTime? DueDate
		{
			get
			{
				this.CheckDisposed("DueDate::get");
				return base.GetValueAsNullable<ExDateTime>(InternalSchema.DueDate);
			}
			set
			{
				this.CheckDisposed("DueDate::set");
				if (this.DueDate == value)
				{
					return;
				}
				this.SetTaskDateInternal(InternalSchema.DueDate, value);
			}
		}

		// Token: 0x17001644 RID: 5700
		// (get) Token: 0x06004EAD RID: 20141 RVA: 0x001493CE File Offset: 0x001475CE
		public ExDateTime? DoItTime
		{
			get
			{
				this.CheckDisposed("DoItTime::get");
				return new ExDateTime?(base.GetValueOrDefault<ExDateTime>(InternalSchema.DoItTime));
			}
		}

		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x001493EB File Offset: 0x001475EB
		// (set) Token: 0x06004EAF RID: 20143 RVA: 0x00149408 File Offset: 0x00147608
		public string InternetMessageId
		{
			get
			{
				this.CheckDisposed("InternetMessageId::get");
				return base.GetValueOrDefault<string>(InternalSchema.InternetMessageId, string.Empty);
			}
			set
			{
				this.CheckDisposed("InternetMessageId::set");
				base.CheckSetNull("Task::InternetMessageId", "InternetMessageId", value);
				this[InternalSchema.InternetMessageId] = value;
			}
		}

		// Token: 0x17001646 RID: 5702
		// (get) Token: 0x06004EB0 RID: 20144 RVA: 0x00149432 File Offset: 0x00147632
		// (set) Token: 0x06004EB1 RID: 20145 RVA: 0x00149460 File Offset: 0x00147660
		public Reminders<ModernReminder> ModernReminders
		{
			get
			{
				this.CheckDisposed("ModernReminders::get");
				if (this.modernReminders == null)
				{
					this.modernReminders = Reminders<ModernReminder>.Get(this, InternalSchema.ModernReminders);
				}
				return this.modernReminders;
			}
			set
			{
				this.CheckDisposed("ModernReminders::set");
				base.Load(new PropertyDefinition[]
				{
					InternalSchema.GlobalObjectId
				});
				if (base.GetValueOrDefault<byte[]>(InternalSchema.GlobalObjectId, null) == null)
				{
					GlobalObjectId globalObjectId = new GlobalObjectId();
					this[InternalSchema.GlobalObjectId] = globalObjectId.Bytes;
				}
				Reminders<ModernReminder>.Set(this, InternalSchema.ModernReminders, value);
				this.modernReminders = value;
			}
		}

		// Token: 0x17001647 RID: 5703
		// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x001494C8 File Offset: 0x001476C8
		// (set) Token: 0x06004EB3 RID: 20147 RVA: 0x001494F4 File Offset: 0x001476F4
		public RemindersState<ModernReminderState> ModernRemindersState
		{
			get
			{
				this.CheckDisposed("ModernRemindersState::get");
				if (this.modernRemindersState == null)
				{
					this.modernRemindersState = RemindersState<ModernReminderState>.Get(this, InternalSchema.ModernRemindersState);
				}
				return this.modernRemindersState;
			}
			set
			{
				this.CheckDisposed("ModernRemindersState::set");
				RemindersState<ModernReminderState>.Set(this, InternalSchema.ModernRemindersState, value);
				this.modernRemindersState = value;
			}
		}

		// Token: 0x17001648 RID: 5704
		// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x00149514 File Offset: 0x00147714
		// (set) Token: 0x06004EB5 RID: 20149 RVA: 0x00149540 File Offset: 0x00147740
		public Reminders<EventTimeBasedInboxReminder> EventTimeBasedInboxReminders
		{
			get
			{
				this.CheckDisposed("EventTimeBasedInboxReminders::get");
				if (this.eventTimeBasedInboxReminders == null)
				{
					this.eventTimeBasedInboxReminders = Reminders<EventTimeBasedInboxReminder>.Get(this, TaskSchema.EventTimeBasedInboxReminders);
				}
				return this.eventTimeBasedInboxReminders;
			}
			set
			{
				this.CheckDisposed("EventTimeBasedInboxReminders::set");
				Reminders<EventTimeBasedInboxReminder>.Set(this, TaskSchema.EventTimeBasedInboxReminders, value);
				this.eventTimeBasedInboxReminders = value;
			}
		}

		// Token: 0x17001649 RID: 5705
		// (get) Token: 0x06004EB6 RID: 20150 RVA: 0x00149560 File Offset: 0x00147760
		// (set) Token: 0x06004EB7 RID: 20151 RVA: 0x0014958C File Offset: 0x0014778C
		public RemindersState<EventTimeBasedInboxReminderState> EventTimeBasedInboxRemindersState
		{
			get
			{
				this.CheckDisposed("EventTimeBasedInboxRemindersState::get");
				if (this.eventTimeBasedInboxRemindersState == null)
				{
					this.eventTimeBasedInboxRemindersState = RemindersState<EventTimeBasedInboxReminderState>.Get(this, TaskSchema.EventTimeBasedInboxRemindersState);
				}
				return this.eventTimeBasedInboxRemindersState;
			}
			set
			{
				this.CheckDisposed("EventTimeBasedInboxRemindersState::set");
				RemindersState<EventTimeBasedInboxReminderState>.Set(this, TaskSchema.EventTimeBasedInboxRemindersState, value);
				this.eventTimeBasedInboxRemindersState = value;
			}
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x001495AC File Offset: 0x001477AC
		public GlobalObjectId GetGlobalObjectId()
		{
			base.Load(new PropertyDefinition[]
			{
				InternalSchema.GlobalObjectId
			});
			byte[] valueOrDefault = base.GetValueOrDefault<byte[]>(InternalSchema.GlobalObjectId, null);
			if (valueOrDefault == null)
			{
				return null;
			}
			return new GlobalObjectId(valueOrDefault);
		}

		// Token: 0x1700164A RID: 5706
		// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x001495E8 File Offset: 0x001477E8
		public TaskStatus Status
		{
			get
			{
				this.CheckDisposed("Status::get");
				return base.GetValueOrDefault<TaskStatus>(InternalSchema.TaskStatus);
			}
		}

		// Token: 0x1700164B RID: 5707
		// (get) Token: 0x06004EBA RID: 20154 RVA: 0x0014960D File Offset: 0x0014780D
		public LocalizedString StatusDescription
		{
			get
			{
				this.CheckDisposed("StatusDescription::get");
				return this.GenerateStatusDescription();
			}
		}

		// Token: 0x1700164C RID: 5708
		// (get) Token: 0x06004EBB RID: 20155 RVA: 0x00149620 File Offset: 0x00147820
		// (set) Token: 0x06004EBC RID: 20156 RVA: 0x00149638 File Offset: 0x00147838
		public double PercentComplete
		{
			get
			{
				this.CheckDisposed("PercentComplete::get");
				return base.GetValueOrDefault<double>(InternalSchema.PercentComplete);
			}
			set
			{
				this.CheckDisposed("PercentComplete::set");
				if (value.Equals(base.TryGetProperty(InternalSchema.PercentComplete)))
				{
					return;
				}
				if (value > 1.0 || value < 0.0)
				{
					ExTraceGlobals.TaskTracer.TraceError<double>((long)this.GetHashCode(), "Task::PercentComplete. PercentComplete is out of range. PercentComplete = {0}.", value);
					throw new ArgumentOutOfRangeException("PercentComplete");
				}
				if (value == 1.0)
				{
					throw new ArgumentException(ServerStrings.UseMethodInstead("SetStatusCompleted"));
				}
				if (value == 0.0)
				{
					ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::PercentComplete. PercentComplete = 0.0. Change status to NotStarted.");
					this[InternalSchema.TaskStatus] = TaskStatus.NotStarted;
					base.Delete(InternalSchema.CompleteDate);
				}
				else if (this.Status == TaskStatus.Completed || this.Status == TaskStatus.NotStarted)
				{
					ExTraceGlobals.TaskTracer.TraceDebug<TaskStatus>((long)this.GetHashCode(), "Task::PercentComplete. PercentComplete != 0. Change status to InProgress. Current Status = {0}.", this.Status);
					this[InternalSchema.TaskStatus] = TaskStatus.InProgress;
					base.Delete(InternalSchema.CompleteDate);
				}
				this[InternalSchema.PercentComplete] = value;
			}
		}

		// Token: 0x1700164D RID: 5709
		// (get) Token: 0x06004EBD RID: 20157 RVA: 0x0014975C File Offset: 0x0014795C
		// (set) Token: 0x06004EBE RID: 20158 RVA: 0x00149788 File Offset: 0x00147988
		public Recurrence Recurrence
		{
			get
			{
				this.CheckDisposed("Recurrence::get");
				object value = base.TryGetProperty(InternalSchema.TaskRecurrence);
				return Task.ParseRecurrence(this, value);
			}
			set
			{
				this.CheckDisposed("Recurrence::set");
				if (!Task.IsTaskRecurrenceSupported(value))
				{
					ExTraceGlobals.TaskTracer.TraceError<Recurrence>((long)this.GetHashCode(), "Task::Recurrent::set. The recurrence is not supported. Recurrence = {0}.", value);
					throw new NotSupportedException(ServerStrings.TaskRecurrenceNotSupported(value.Pattern.ToString(), value.Range.ToString()));
				}
				if (value == null)
				{
					ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::Recurrent::set. The recurrence is cleared.");
					base.Delete(InternalSchema.TaskRecurrence);
					this[InternalSchema.IsTaskRecurring] = false;
					this[InternalSchema.IconIndex] = IconIndex.BaseTask;
					return;
				}
				ExTraceGlobals.TaskTracer.TraceDebug<Recurrence>((long)this.GetHashCode(), "Task::Recurrent::set. Set a recurrence. Recurrence = {0}.", value);
				this[InternalSchema.TaskRecurrence] = this.ToRecurrenceBlob(value);
				this[InternalSchema.IsTaskRecurring] = true;
				this.adjustDatesBeforeSave = true;
				this[InternalSchema.IsOneOff] = false;
				this[InternalSchema.IconIndex] = IconIndex.TaskRecur;
			}
		}

		// Token: 0x1700164E RID: 5710
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x00149897 File Offset: 0x00147A97
		public bool IsRecurring
		{
			get
			{
				this.CheckDisposed("IsRecurring::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsTaskRecurring);
			}
		}

		// Token: 0x1700164F RID: 5711
		// (get) Token: 0x06004EC0 RID: 20160 RVA: 0x001498B0 File Offset: 0x00147AB0
		public ExDateTime? CompleteDate
		{
			get
			{
				this.CheckDisposed("CompleteDate::get");
				if (this.Status != TaskStatus.Completed)
				{
					return null;
				}
				return base.GetValueAsNullable<ExDateTime>(InternalSchema.CompleteDate);
			}
		}

		// Token: 0x17001650 RID: 5712
		// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x001498E8 File Offset: 0x00147AE8
		public bool IsLastOccurrence
		{
			get
			{
				this.CheckDisposed("IsLastInstance::get");
				if (!this.IsRecurring)
				{
					return true;
				}
				ExDateTime nextTaskInstance = this.GetNextTaskInstance();
				return Task.NoMoreInstance(nextTaskInstance);
			}
		}

		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x00149917 File Offset: 0x00147B17
		public bool IsComplete
		{
			get
			{
				this.CheckDisposed("IsComplete::get");
				return base.GetValueOrDefault<bool>(InternalSchema.IsComplete);
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x0014992F File Offset: 0x00147B2F
		// (set) Token: 0x06004EC4 RID: 20164 RVA: 0x00149942 File Offset: 0x00147B42
		public bool SuppressRecurrenceAdjustment
		{
			get
			{
				this.CheckDisposed("SuppressRecurrenceAdjustment::get");
				return this.suppressRecurrenceAdjustment;
			}
			set
			{
				this.CheckDisposed("SuppressRecurrenceAdjustment::set");
				if (this.suppressRecurrenceAdjustment != value)
				{
					this.suppressRecurrenceAdjustment = value;
				}
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06004EC5 RID: 20165 RVA: 0x0014995F File Offset: 0x00147B5F
		// (set) Token: 0x06004EC6 RID: 20166 RVA: 0x00149972 File Offset: 0x00147B72
		public bool SuppressCreateOneOff
		{
			get
			{
				this.CheckDisposed("SuppressCreateOneOff::get");
				return this.isSuppressCreateOneOff;
			}
			set
			{
				this.CheckDisposed("SuppressCreateOneOff::set");
				this.isSuppressCreateOneOff = value;
			}
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x00149986 File Offset: 0x00147B86
		private static bool IsTaskRecurrenceSupported(Recurrence recurrence)
		{
			if (recurrence == null)
			{
				return true;
			}
			if (recurrence.Pattern is RegeneratingPattern && recurrence.Range is EndDateRecurrenceRange)
			{
				ExTraceGlobals.TaskTracer.TraceDebug(0L, "Task::IsTaskRecurrenceSupported. We do not support combination of RegeneratingPattern and EndDateRecurrenceRange.");
				return false;
			}
			return true;
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x001499BB File Offset: 0x00147BBB
		private static void MakeTaskDateIntegrity(Task task, PropertyDefinition property, object value)
		{
			if (!property.Equals(InternalSchema.DueDate) || value != null)
			{
				return;
			}
			ExTraceGlobals.TaskTracer.TraceDebug(0L, "Task::MakeTaskDateIntegrity. We clear StartDate due to DueDate is set to Null.");
			task.Delete(InternalSchema.StartDate);
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x001499EC File Offset: 0x00147BEC
		private static TaskRecurrence ParseRecurrence(Task task, object value)
		{
			byte[] array = value as byte[];
			if (array == null)
			{
				return null;
			}
			return InternalRecurrence.InternalParseTask(array, task, task.PropertyBag.ExTimeZone, task.Session.ExTimeZone);
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x00149A24 File Offset: 0x00147C24
		private static bool IsTimeSpanWholeDays(TimeSpan period)
		{
			return period.TotalSeconds % 86400.0 == 0.0;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x00149A44 File Offset: 0x00147C44
		private static ExDateTime GetNextTaskInstance(ExDateTime? startDate, ExDateTime? dueDate, ExDateTime? completeDate, TaskRecurrence recurrence, ExTimeZone timezone)
		{
			ExTraceGlobals.TaskTracer.TraceDebug(0L, "Task::GetNextTaskInstance. StartDate = {0}, DueDate = {1}, recurrence = {2}, timezone = {3}.", new object[]
			{
				startDate,
				dueDate,
				recurrence,
				timezone
			});
			if (recurrence == null)
			{
				return ExDateTime.MaxValue;
			}
			ExDateTime exDateTime;
			if (recurrence.Pattern is RegeneratingPattern && completeDate != null)
			{
				exDateTime = completeDate.Value.Date;
			}
			else if (startDate != null)
			{
				exDateTime = startDate.Value;
			}
			else if (dueDate != null)
			{
				exDateTime = dueDate.Value;
			}
			else
			{
				exDateTime = ExDateTime.GetNow(timezone);
				if (exDateTime < recurrence.Range.StartDate)
				{
					exDateTime = recurrence.GetNextOccurrence(exDateTime);
				}
			}
			return recurrence.GetNextOccurrence(exDateTime);
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x00149B06 File Offset: 0x00147D06
		private static bool NoMoreInstance(ExDateTime instance)
		{
			return instance == ExDateTime.MaxValue;
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x00149B14 File Offset: 0x00147D14
		private static TimeSpan CalculateTimeDifference(ExDateTime? start, ExDateTime? due)
		{
			if (start == null || due == null)
			{
				return default(TimeSpan);
			}
			return due.Value.Date - start.Value.Date;
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x00149B60 File Offset: 0x00147D60
		private static Task CreateInternal(StoreSession session, StoreId parentFolderId)
		{
			Task task = null;
			bool flag = false;
			Task result;
			try
			{
				task = ItemBuilder.CreateNewItem<Task>(session, parentFolderId, ItemCreateInfo.TaskInfo);
				task.ClassName = "IPM.Task";
				task.SetStatusNotStarted();
				task[InternalSchema.IsComplete] = false;
				task[InternalSchema.IsTaskRecurring] = false;
				task[InternalSchema.TaskChangeCount] = 1;
				task[InternalSchema.IsDraft] = false;
				task.wasNewBeforeSave = true;
				flag = true;
				result = task;
			}
			finally
			{
				if (!flag && task != null)
				{
					task.Dispose();
					task = null;
				}
			}
			return result;
		}

		// Token: 0x06004ECF RID: 20175 RVA: 0x00149C00 File Offset: 0x00147E00
		public void SetStatusNotStarted()
		{
			this.CheckDisposed("SetStatusNotStarted");
			this.SetStatusInternal(TaskStatus.NotStarted);
		}

		// Token: 0x06004ED0 RID: 20176 RVA: 0x00149C14 File Offset: 0x00147E14
		public void SetStatusInProgress()
		{
			this.CheckDisposed("SetStatusInProgress");
			this.SetStatusInternal(TaskStatus.InProgress);
		}

		// Token: 0x06004ED1 RID: 20177 RVA: 0x00149C28 File Offset: 0x00147E28
		public void SetStatusWaitingOnOthers()
		{
			this.CheckDisposed("SetStatusWaitingOnOthers");
			this.SetStatusInternal(TaskStatus.WaitingOnOthers);
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x00149C3C File Offset: 0x00147E3C
		public void SetStatusDeferred()
		{
			this.CheckDisposed("SetStatusDeferred");
			this.SetStatusInternal(TaskStatus.Deferred);
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x00149C50 File Offset: 0x00147E50
		public void SetStatusCompleted(ExDateTime completeTime)
		{
			this.CheckDisposed("SetStatusCompleted");
			ExTimeZone exTimeZone = base.PropertyBag.ExTimeZone;
			if (exTimeZone == null || exTimeZone == ExTimeZone.UtcTimeZone)
			{
				throw new InvalidOperationException(ServerStrings.UseMethodInstead("SetCompleteTimesForUtcSession"));
			}
			this.SetStatusCompletedInternal(completeTime);
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x00149C9C File Offset: 0x00147E9C
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			if (this.AllowCreateOneOff())
			{
				ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::OnBeforeSave. Creating one-off and updating the master.");
				this.CreateOneOff();
				this.UpdateMaster();
			}
			else
			{
				if (this.adjustDatesBeforeSave && !this.suppressRecurrenceAdjustment)
				{
					this.AdjustStartAndDueDateAccordingToRecurrence();
				}
				this.updatedMasterProperties = this.GetTaskMasterProperties();
			}
			if (this.isCreateOneOff)
			{
				this.RestoreMaster();
			}
			this.OnBeforeSaveUpdateTaskDates();
		}

		// Token: 0x06004ED5 RID: 20181 RVA: 0x00149D14 File Offset: 0x00147F14
		public ConflictResolutionResult Save(SaveMode saveMode, out StoreObjectId idOneOff)
		{
			ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::Save. Save method. Version of idOneOff.");
			this.needOneOffId = true;
			ConflictResolutionResult result;
			try
			{
				ConflictResolutionResult conflictResolutionResult = base.Save(saveMode);
				if (this.oneoffId != null)
				{
					ExTraceGlobals.TaskTracer.TraceDebug<StoreObjectId>((long)this.GetHashCode(), "Task::Save. Save method. Getting idOneOff. idOneOff = {0}.", this.oneoffId);
					idOneOff = this.oneoffId.Clone();
				}
				else
				{
					ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::Save. Save method. Getting idOneOff. idOneOff = <Null>.");
					idOneOff = null;
				}
				result = conflictResolutionResult;
			}
			finally
			{
				this.oneoffId = null;
				this.needOneOffId = false;
			}
			return result;
		}

		// Token: 0x06004ED6 RID: 20182 RVA: 0x00149DB8 File Offset: 0x00147FB8
		protected override void OnAfterSave(ConflictResolutionResult acrResults)
		{
			base.OnAfterSave(acrResults);
			if (acrResults.SaveStatus != SaveResult.IrresolvableConflict)
			{
				if (this.updatedMasterProperties != null)
				{
					this.masterProperties = this.updatedMasterProperties;
					this.updatedMasterProperties = null;
				}
				if (this.itemOneOff != null)
				{
					this.SaveOneOff();
				}
				this.isCreateOneOff = false;
				this.wasNewBeforeSave = false;
				this.adjustDatesBeforeSave = false;
				return;
			}
			this.updatedMasterProperties = null;
			ExTraceGlobals.TaskTracer.TraceError<SaveResult>((long)this.GetHashCode(), "Task::OnAfterSave. Irresolvable save result. SaveStatus = {0}.", acrResults.SaveStatus);
		}

		// Token: 0x06004ED7 RID: 20183 RVA: 0x00149E38 File Offset: 0x00148038
		public void DeleteCurrentOccurrence()
		{
			this.CheckDisposed("DeleteCurrentOccurrence");
			if (!this.IsRecurring || this.Recurrence.Pattern is RegeneratingPattern)
			{
				ExTraceGlobals.TaskTracer.TraceError<bool, string>((long)this.GetHashCode(), "Task::DeleteCurrentOccurrence. Cannot delete the current occurrence. IsRecurring = {0}, RecurrencePattern = {1}.", this.IsRecurring, (this.Recurrence == null) ? "<null>" : this.Recurrence.Pattern.ToString());
				throw new InvalidOperationException();
			}
			ExDateTime nextTaskInstance = this.GetNextTaskInstance();
			if (Task.NoMoreInstance(nextTaskInstance))
			{
				ExTraceGlobals.TaskTracer.TraceError((long)this.GetHashCode(), "Task::DeleteCurrentOccurrence. Cannot delete the current occurrence. This is the last occurrence.");
				throw new InvalidOperationException(ServerStrings.UseMethodInstead("Delete"));
			}
			TimeSpan timeSpan = this.CalculateTimeDifference();
			if (this.StartDate != null)
			{
				this[InternalSchema.StartDate] = nextTaskInstance;
			}
			this[InternalSchema.DueDate] = nextTaskInstance.AddDays(timeSpan.TotalDays);
			this.SetStatusNotStarted();
		}

		// Token: 0x06004ED8 RID: 20184 RVA: 0x00149F34 File Offset: 0x00148134
		public void SetStartDatesForUtcSession(ExDateTime? localStartDate, ExDateTime? utcStartDate)
		{
			this.CheckDisposed("SetStartDatesForUtcSession");
			if (base.PropertyBag.ExTimeZone != null && base.PropertyBag.ExTimeZone != ExTimeZone.UtcTimeZone)
			{
				throw new InvalidOperationException(ServerStrings.CanUseApiOnlyWhenTimeZoneIsNull("SetStartDatesForUtcSession"));
			}
			this.StartDate = utcStartDate;
			this[InternalSchema.LocalStartDate] = localStartDate;
		}

		// Token: 0x06004ED9 RID: 20185 RVA: 0x00149F98 File Offset: 0x00148198
		public void SetDueDatesForUtcSession(ExDateTime? localDueDate, ExDateTime? utcDueDate)
		{
			this.CheckDisposed("SetDueDatesForUtcSession");
			if (base.PropertyBag.ExTimeZone != null && base.PropertyBag.ExTimeZone != ExTimeZone.UtcTimeZone)
			{
				throw new InvalidOperationException(ServerStrings.CanUseApiOnlyWhenTimeZoneIsNull("SetDueDatesForUtcSession"));
			}
			this.DueDate = utcDueDate;
			this[InternalSchema.LocalDueDate] = localDueDate;
		}

		// Token: 0x06004EDA RID: 20186 RVA: 0x00149FFC File Offset: 0x001481FC
		public void SetCompleteTimesForUtcSession(ExDateTime completeDate, ExDateTime? flagCompleteTime)
		{
			this.CheckDisposed("SetCompleteTimesForUtcSession");
			if (base.PropertyBag.ExTimeZone != null && base.PropertyBag.ExTimeZone != ExTimeZone.UtcTimeZone)
			{
				throw new InvalidOperationException(ServerStrings.CanUseApiOnlyWhenTimeZoneIsNull("SetCompleteTimesForUtcSession"));
			}
			ExDateTime value = TaskDate.PersistentLocalTime(new ExDateTime?(completeDate)).Value;
			ExDateTime? exDateTime = TaskDate.PersistentLocalTime(flagCompleteTime);
			this.SetStatusCompletedInternal(value);
			base.SetOrDeleteProperty(InternalSchema.FlagCompleteTime, exDateTime);
			base.SetOrDeleteProperty(InternalSchema.CompleteDate, value);
		}

		// Token: 0x06004EDB RID: 20187 RVA: 0x0014A08C File Offset: 0x0014828C
		public string GenerateWhen()
		{
			this.CheckDisposed("GenerateWhen");
			if (!this.IsRecurring)
			{
				return string.Empty;
			}
			return this.Recurrence.GenerateWhen(false).ToString(base.Session.InternalPreferedCulture);
		}

		// Token: 0x06004EDC RID: 20188 RVA: 0x0014A0D4 File Offset: 0x001482D4
		private void OnBeforeSaveUpdateTaskDates()
		{
			if (this.StartDate > this.DueDate)
			{
				ExTraceGlobals.TaskTracer.TraceDebug<ExDateTime?, ExDateTime?>(0L, "Task::ObjectUpdateTaskDates. DueDate is earlier than StartDate so we are changing DueDate. StartDate = {0}, DueDate = {1}.", this.StartDate, this.DueDate);
				TimeSpan timeSpan = Task.CalculateTimeDifference(this.OriginalStartDate, this.OriginalDueDate);
				this.DueDate = new ExDateTime?(this.StartDate.Value.AddDays(timeSpan.TotalDays));
			}
		}

		// Token: 0x06004EDD RID: 20189 RVA: 0x0014A170 File Offset: 0x00148370
		private void AdjustStartAndDueDateAccordingToRecurrence()
		{
			Recurrence recurrence = this.Recurrence;
			if (recurrence != null)
			{
				ExTraceGlobals.TaskTracer.TraceDebug<ExDateTime?, ExDateTime?>((long)this.GetHashCode(), "Task::AdjustStartAndDueDateAccordingToRecurrence. StartDate is {0}, DueDate is {1}.", this.StartDate, this.DueDate);
				if (this.StartDate != null)
				{
					TimeSpan? timeSpan = null;
					if (this.DueDate != null)
					{
						timeSpan = this.DueDate - this.StartDate;
					}
					this[InternalSchema.StartDate] = recurrence.Range.StartDate;
					if (timeSpan != null)
					{
						this[InternalSchema.DueDate] = (Task.IsTimeSpanWholeDays(timeSpan.Value) ? new ExDateTime?(recurrence.Range.StartDate.AddDays(timeSpan.Value.TotalDays)) : (recurrence.Range.StartDate + timeSpan));
					}
				}
				else
				{
					this[InternalSchema.DueDate] = recurrence.Range.StartDate;
				}
				ExTraceGlobals.TaskTracer.TraceDebug<ExDateTime?, ExDateTime?>((long)this.GetHashCode(), "Task::AdjustStartAndDueDateAccordingToRecurrence. New StartDate is {0}, new DueDate is {1}.", this.StartDate, this.DueDate);
			}
		}

		// Token: 0x06004EDE RID: 20190 RVA: 0x0014A304 File Offset: 0x00148504
		private void RestoreMaster()
		{
			int num = base.GetValueOrDefault<int>(InternalSchema.TaskChangeCount);
			ExTraceGlobals.TaskTracer.TraceDebug<int>((long)this.GetHashCode(), "Task::OnBeforeSave. Increment count. Count = {0}.", num);
			num++;
			this[InternalSchema.TaskChangeCount] = num;
			this[InternalSchema.IsOneOff] = false;
			this[InternalSchema.IconIndex] = IconIndex.TaskRecur;
			this[InternalSchema.IsTaskRecurring] = true;
			base.Delete(InternalSchema.FlagCompleteTime);
		}

		// Token: 0x06004EDF RID: 20191 RVA: 0x0014A38C File Offset: 0x0014858C
		private LocalizedString GenerateStatusDescription()
		{
			TaskStatus valueOrDefault = base.GetValueOrDefault<TaskStatus>(InternalSchema.TaskStatus, (TaskStatus)(-1));
			if (valueOrDefault < TaskStatus.NotStarted)
			{
				return LocalizedString.Empty;
			}
			switch (valueOrDefault)
			{
			case TaskStatus.NotStarted:
				return ClientStrings.TaskStatusNotStarted;
			case TaskStatus.InProgress:
				return ClientStrings.TaskStatusInProgress;
			case TaskStatus.Completed:
				return ClientStrings.TaskStatusCompleted;
			case TaskStatus.WaitingOnOthers:
				return ClientStrings.TaskStatusWaitOnOthers;
			case TaskStatus.Deferred:
				return ClientStrings.TaskStatusDeferred;
			default:
				return LocalizedString.Empty;
			}
		}

		// Token: 0x06004EE0 RID: 20192 RVA: 0x0014A3F4 File Offset: 0x001485F4
		private PropertyBag GetTaskMasterProperties()
		{
			MemoryPropertyBag memoryPropertyBag = new MemoryPropertyBag();
			IDirectPropertyBag directPropertyBag = memoryPropertyBag;
			foreach (NativeStorePropertyDefinition propertyDefinition in Task.TaskMasterProperties)
			{
				directPropertyBag.SetValue(propertyDefinition, base.TryGetProperty(propertyDefinition));
			}
			return memoryPropertyBag;
		}

		// Token: 0x06004EE1 RID: 20193 RVA: 0x0014A438 File Offset: 0x00148638
		private void SetStatusCompletedAsSingle(ExDateTime completeTime)
		{
			this[InternalSchema.TaskStatus] = TaskStatus.Completed;
			this[InternalSchema.FlagCompleteTime] = completeTime;
			this[InternalSchema.CompleteDate] = completeTime.Date;
			this[InternalSchema.PercentComplete] = 1.0;
			this[InternalSchema.IconIndex] = IconIndex.BaseTask;
			this[InternalSchema.IsTaskRecurring] = false;
			this[InternalSchema.FlagStatus] = FlagStatus.Complete;
			this[InternalSchema.ReminderIsSetInternal] = false;
		}

		// Token: 0x06004EE2 RID: 20194 RVA: 0x0014A4E0 File Offset: 0x001486E0
		private void SetStatusCompletedInternal(ExDateTime completeTime)
		{
			if (completeTime == ExDateTime.MaxValue || completeTime < Util.Date1601)
			{
				ExTraceGlobals.TaskTracer.TraceError<ExDateTime>((long)this.GetHashCode(), "Task::SetStatusCompletedInternal. completeTime is out of range. completeTime = {0}.", completeTime);
				throw new ArgumentOutOfRangeException("completeTime");
			}
			if (!this.IsRecurring)
			{
				this.SetStatusCompletedAsSingle(completeTime);
				return;
			}
			ExDateTime nextTaskInstance = this.GetNextTaskInstance(new ExDateTime?(completeTime));
			if (Task.NoMoreInstance(nextTaskInstance))
			{
				if (!(this.Recurrence.Pattern is RegeneratingPattern))
				{
					ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::SetStatusCompletedInternal. This is the last occurrence and the pattern is regular recurrence pattern. We are clearing the recurrence blob.");
					this.Recurrence = null;
				}
				this.SetStatusCompletedAsSingle(completeTime);
				this[InternalSchema.IsOneOff] = true;
				return;
			}
			if (this.SuppressCreateOneOff)
			{
				ExTraceGlobals.TaskTracer.TraceError((long)this.GetHashCode(), "Task::SetStatusCompletedInternal. The consumer cannot choose to suppress creating one-off yet calling this API.");
				throw new InvalidOperationException(ServerStrings.ExCannotMarkTaskCompletedWhenSuppressCreateOneOff);
			}
			this.SetCreateOneOff();
			this.SetStatusCompletedAsSingle(completeTime);
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x0014A5D4 File Offset: 0x001487D4
		private void SetStatusInternal(TaskStatus status)
		{
			ExTraceGlobals.TaskTracer.TraceDebug<TaskStatus, TaskStatus>((long)this.GetHashCode(), "Task::SetStatusInternal. Update status. Current = {0}, New = {1}.", this.Status, status);
			if (this.Status == TaskStatus.Completed)
			{
				this[InternalSchema.FlagStatus] = FlagStatus.NotFlagged;
			}
			this[InternalSchema.IsComplete] = false;
			this[InternalSchema.TaskStatus] = status;
			base.Delete(InternalSchema.CompleteDate);
			base.Delete(InternalSchema.FlagCompleteTime);
			switch (status)
			{
			case TaskStatus.NotStarted:
				this[InternalSchema.PercentComplete] = 0.0;
				return;
			case TaskStatus.InProgress:
			case TaskStatus.WaitingOnOthers:
			case TaskStatus.Deferred:
				if (this.PercentComplete == 1.0)
				{
					this[InternalSchema.PercentComplete] = 0.0;
					return;
				}
				return;
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x0014A6BC File Offset: 0x001488BC
		internal static bool IsTaskRecurrenceSupported(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			object obj = validatablePropertyBag.TryGetProperty(InternalSchema.TaskRecurrence);
			byte[] array = obj as byte[];
			if (array != null)
			{
				TaskRecurrence recurrence = InternalRecurrence.InternalParseTask(array, null, null, null);
				return Task.IsTaskRecurrenceSupported(recurrence);
			}
			return true;
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x0014A6F4 File Offset: 0x001488F4
		private void SetTaskDateInternal(PropertyDefinition property, ExDateTime? value)
		{
			ExTraceGlobals.TaskTracer.TraceDebug<PropertyDefinition, ExDateTime?>((long)this.GetHashCode(), "Task::SetTaskDateInternal. Change TaskDate. property = {0}, value = {1}.", property, value);
			base.SetOrDeleteProperty(property, value);
			Task.MakeTaskDateIntegrity(this, property, value);
			if (!this.IsRecurring || this.Recurrence.Pattern is RegeneratingPattern || this.SuppressCreateOneOff)
			{
				ExTraceGlobals.TaskTracer.TraceDebug<bool, bool>((long)this.GetHashCode(), "Task::SetTaskDateInternal. Change TaskDate on a single Task. IsRecurring = {0}, SuppressCreateOneOff = {1}.", this.IsRecurring, this.SuppressCreateOneOff);
				return;
			}
			ExDateTime nextTaskInstance = this.GetNextTaskInstance();
			if (Task.NoMoreInstance(nextTaskInstance))
			{
				ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::SetTaskDateInternal. This is the last occurrence.");
				base.Delete(InternalSchema.TaskRecurrence);
				this[InternalSchema.IsTaskRecurring] = false;
				this[InternalSchema.TaskResetReminder] = false;
				this[InternalSchema.IsOneOff] = true;
				return;
			}
			ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::SetTaskDateInternal. This is NOT the last occurence. We persist the changes on the master for the time being.");
			this.SetCreateOneOff();
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x0014A7F8 File Offset: 0x001489F8
		private byte[] ToRecurrenceBlob(Recurrence recurrence)
		{
			if (recurrence == null)
			{
				return null;
			}
			if (recurrence.Pattern == null)
			{
				throw new CorruptDataException(ServerStrings.ExPatternNotSet);
			}
			if (recurrence.Range == null)
			{
				throw new CorruptDataException(ServerStrings.ExRangeNotSet);
			}
			TimeSpan timeSpan = (this.StartDate == null) ? default(TimeSpan) : this.StartDate.Value.TimeOfDay;
			TimeSpan endOffset = timeSpan;
			InternalRecurrence internalRecurrence = new InternalRecurrence(recurrence.Pattern, recurrence.Range, this, base.PropertyBag.ExTimeZone, base.PropertyBag.ExTimeZone, timeSpan, endOffset, new ExDateTime?(recurrence.EndDate));
			return internalRecurrence.ToByteArray(true);
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x0014A8A4 File Offset: 0x00148AA4
		private void SetCreateOneOff()
		{
			if (this.OriginalRecurrence != null)
			{
				this.isCreateOneOff = true;
			}
		}

		// Token: 0x06004EE8 RID: 20200 RVA: 0x0014A8B8 File Offset: 0x00148AB8
		private bool AllowCreateOneOff()
		{
			bool flag = this.isCreateOneOff && !this.SuppressCreateOneOff && !this.wasNewBeforeSave;
			bool flag2 = this.StartDate != this.OriginalStartDate || this.DueDate != this.OriginalDueDate || (this.Status != this.OriginalStatus && this.Status == TaskStatus.Completed);
			ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::GetCreateOneOff. The condition of creating one-off. isCreateOneOff = {0}, SuppressCreateOneOff = {1}, wasNewBeforeSave = {2}, statusChanged = {3}.", new object[]
			{
				this.isCreateOneOff,
				this.SuppressCreateOneOff,
				this.wasNewBeforeSave,
				flag2
			});
			return flag && flag2;
		}

		// Token: 0x06004EE9 RID: 20201 RVA: 0x0014A9E4 File Offset: 0x00148BE4
		private void CreateOneOff()
		{
			ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::CreateOneOff. Create one-off.");
			this.itemOneOff = Task.Create(base.Session, base.ParentId);
			IDirectPropertyBag propertyBag = this.itemOneOff.PropertyBag;
			foreach (NativeStorePropertyDefinition propertyDefinition in Task.TaskMasterProperties)
			{
				object propertyValue = base.TryGetProperty(propertyDefinition);
				if (PropertyError.IsPropertyNotFound(propertyValue))
				{
					propertyBag.Delete(propertyDefinition);
				}
				else
				{
					propertyBag.SetValue(propertyDefinition, propertyValue);
				}
			}
			this.itemOneOff[InternalSchema.IsTaskRecurring] = false;
			this.itemOneOff[InternalSchema.IsOneOff] = true;
			this.itemOneOff[InternalSchema.IconIndex] = IconIndex.BaseTask;
			if (this.OriginalRecurrence != null && this.OriginalRecurrence.Pattern is RegeneratingPattern)
			{
				this.itemOneOff.Delete(InternalSchema.TaskRecurrence);
			}
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x0014AAD8 File Offset: 0x00148CD8
		private void SaveOneOff()
		{
			ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "Task::SaveOneOff. Saving one-off.");
			Microsoft.Exchange.Data.Storage.CoreObject.MapiCopyTo(base.MapiMessage, this.itemOneOff.MapiMessage, base.Session, this.itemOneOff.Session, CopyPropertiesFlags.None, CopySubObjects.Copy, new NativeStorePropertyDefinition[0]);
			this.itemOneOff.Save(SaveMode.NoConflictResolution);
			if (this.needOneOffId)
			{
				this.itemOneOff.Load(null);
				this.oneoffId = this.itemOneOff.Id.ObjectId;
			}
			this.itemOneOff.Dispose();
			this.itemOneOff = null;
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x0014AB74 File Offset: 0x00148D74
		private void UpdateMaster()
		{
			ExDateTime? completeDate = this.CompleteDate;
			this.SetStatusNotStarted();
			base.Delete(InternalSchema.IsOneOff);
			TimeSpan? timeSpan = null;
			ExDateTime nextTaskInstanceFromOriginal = this.GetNextTaskInstanceFromOriginal(completeDate);
			if (this.StartDate != null)
			{
				timeSpan = new TimeSpan?(nextTaskInstanceFromOriginal - this.StartDate.Value);
			}
			if (this.OriginalStartDate == null)
			{
				base.Delete(InternalSchema.StartDate);
			}
			else
			{
				this[InternalSchema.StartDate] = nextTaskInstanceFromOriginal;
			}
			TimeSpan timeSpan2 = Task.CalculateTimeDifference(this.OriginalStartDate, this.OriginalDueDate);
			this[InternalSchema.DueDate] = nextTaskInstanceFromOriginal.AddDays(timeSpan2.TotalDays);
			Recurrence recurrence = this.Recurrence;
			if (recurrence != null && recurrence.Range is NumberedRecurrenceRange)
			{
				int num = ((NumberedRecurrenceRange)recurrence.Range).NumberOfOccurrences - 1;
				ExTraceGlobals.TaskTracer.TraceDebug<int>((long)this.GetHashCode(), "Task::UpdateMaster. Update NumberedRecurrenceRange. updateNumberedOccurrence = {0}.", num);
				if (num <= 1)
				{
					this.Recurrence = null;
				}
				else
				{
					ExDateTime endDate = recurrence.EndDate;
					recurrence = new Recurrence(recurrence.Pattern, new NumberedRecurrenceRange(recurrence.Range.StartDate, num), new ExDateTime?(endDate));
					this.Recurrence = recurrence;
				}
			}
			this.nextInstanceOnMaster = null;
			if (!(base.TryGetProperty(InternalSchema.ActualWork) is PropertyError))
			{
				this[InternalSchema.ActualWork] = 0;
			}
			if (!(base.TryGetProperty(InternalSchema.TotalWork) is PropertyError))
			{
				this[InternalSchema.TotalWork] = 0;
			}
			bool flag = this.masterProperties.GetValueOrDefault<bool>(InternalSchema.ReminderIsSetInternal) && base.Reminder.DueBy != null;
			bool flag2 = base.GetValueOrDefault<bool>(InternalSchema.TaskResetReminder, false) && base.Reminder.DueBy != null;
			if (flag2)
			{
				this[InternalSchema.TaskResetReminder] = false;
			}
			if ((flag || flag2) && !base.Reminder.IsSet)
			{
				this[InternalSchema.ReminderIsSetInternal] = true;
				if (timeSpan != null)
				{
					base.Reminder.DueBy = new ExDateTime?(base.Reminder.DueBy.Value.AddDays((double)timeSpan.Value.Days));
					if (timeSpan.Value.Hours >= 23)
					{
						base.Reminder.DueBy = new ExDateTime?(base.Reminder.DueBy.Value.AddDays(1.0));
					}
					base.Reminder.Adjust();
				}
			}
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x0014AE4C File Offset: 0x0014904C
		private ExDateTime GetNextTaskInstance()
		{
			return this.GetNextTaskInstance(null);
		}

		// Token: 0x06004EED RID: 20205 RVA: 0x0014AE68 File Offset: 0x00149068
		private ExDateTime GetNextTaskInstance(ExDateTime? completeDate)
		{
			if (this.nextInstanceOnMaster != null)
			{
				return this.nextInstanceOnMaster.Value;
			}
			this.nextInstanceOnMaster = new ExDateTime?(Task.GetNextTaskInstance(this.StartDate, this.DueDate, completeDate, (TaskRecurrence)this.Recurrence, base.PropertyBag.ExTimeZone));
			return this.nextInstanceOnMaster.Value;
		}

		// Token: 0x06004EEE RID: 20206 RVA: 0x0014AECC File Offset: 0x001490CC
		private ExDateTime GetNextTaskInstanceFromOriginal(ExDateTime? completeDate)
		{
			return Task.GetNextTaskInstance(this.OriginalStartDate, this.OriginalDueDate, completeDate, this.OriginalRecurrence, base.PropertyBag.ExTimeZone);
		}

		// Token: 0x06004EEF RID: 20207 RVA: 0x0014AEF1 File Offset: 0x001490F1
		private TimeSpan CalculateTimeDifference()
		{
			return Task.CalculateTimeDifference(this.StartDate, this.DueDate);
		}

		// Token: 0x06004EF0 RID: 20208 RVA: 0x0014AF04 File Offset: 0x00149104
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.itemOneOff != null)
			{
				this.itemOneOff.Dispose();
				this.itemOneOff = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004EF1 RID: 20209 RVA: 0x0014AF2A File Offset: 0x0014912A
		internal static bool IsStartDateDefined(ValidationContext context, IValidatablePropertyBag validatablePropertyBag)
		{
			return !PropertyError.IsPropertyError(validatablePropertyBag.TryGetProperty(InternalSchema.StartDate));
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x0014AF3F File Offset: 0x0014913F
		private ExDateTime? OriginalStartDate
		{
			get
			{
				return this.masterProperties.GetValueAsNullable<ExDateTime>(InternalSchema.StartDate);
			}
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06004EF3 RID: 20211 RVA: 0x0014AF51 File Offset: 0x00149151
		private ExDateTime? OriginalDueDate
		{
			get
			{
				return this.masterProperties.GetValueAsNullable<ExDateTime>(InternalSchema.DueDate);
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x0014AF64 File Offset: 0x00149164
		private TaskRecurrence OriginalRecurrence
		{
			get
			{
				byte[] valueOrDefault = this.masterProperties.GetValueOrDefault<byte[]>(InternalSchema.TaskRecurrence);
				if (valueOrDefault == null)
				{
					return null;
				}
				return InternalRecurrence.InternalParseTask(valueOrDefault, this, base.PropertyBag.ExTimeZone, base.PropertyBag.ExTimeZone);
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06004EF5 RID: 20213 RVA: 0x0014AFA4 File Offset: 0x001491A4
		private TaskStatus OriginalStatus
		{
			get
			{
				return this.masterProperties.GetValueOrDefault<TaskStatus>(InternalSchema.TaskStatus);
			}
		}

		// Token: 0x06004EF6 RID: 20214 RVA: 0x0014AFB8 File Offset: 0x001491B8
		internal static void CoreObjectUpdateTaskStatus(CoreItem coreItem)
		{
			TaskStatus valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<TaskStatus>(InternalSchema.TaskStatus);
			bool valueOrDefault2 = coreItem.PropertyBag.GetValueOrDefault<bool>(InternalSchema.IsComplete);
			bool flag = valueOrDefault == TaskStatus.Completed;
			if (flag != valueOrDefault2)
			{
				coreItem.PropertyBag[InternalSchema.IsComplete] = flag;
			}
			if (valueOrDefault == TaskStatus.NotStarted)
			{
				coreItem.PropertyBag[InternalSchema.PercentComplete] = 0.0;
				return;
			}
			if (valueOrDefault == TaskStatus.Completed)
			{
				coreItem.PropertyBag[InternalSchema.PercentComplete] = 1.0;
				return;
			}
			if (PropertyError.IsPropertyError(coreItem.PropertyBag.TryGetProperty(InternalSchema.PercentComplete)))
			{
				coreItem.PropertyBag[InternalSchema.PercentComplete] = 0.0;
				return;
			}
			double num = (double)coreItem.PropertyBag.TryGetProperty(InternalSchema.PercentComplete);
			if (num >= 1.0 || num < 0.0)
			{
				coreItem.PropertyBag[InternalSchema.PercentComplete] = 0.0;
			}
		}

		// Token: 0x06004EF7 RID: 20215 RVA: 0x0014B0D0 File Offset: 0x001492D0
		internal static void CoreObjectUpdateRecurrence(CoreItem coreItem)
		{
			bool flag = coreItem.PropertyBag.GetValueOrDefault<bool>(InternalSchema.IsTaskRecurring);
			byte[] valueOrDefault = coreItem.PropertyBag.GetValueOrDefault<byte[]>(InternalSchema.TaskRecurrence);
			if (valueOrDefault != null)
			{
				ExTimeZone exTimeZone = Microsoft.Exchange.Data.Storage.CoreObject.GetPersistablePropertyBag(coreItem).ExTimeZone;
				try
				{
					TaskRecurrence taskRecurrence = InternalRecurrence.InternalParseTask(valueOrDefault, null, exTimeZone, exTimeZone);
					if (taskRecurrence.Pattern is RegeneratingPattern)
					{
						flag = true;
					}
					goto IL_AB;
				}
				catch (RecurrenceFormatException)
				{
					if (!coreItem.IsMoveUser)
					{
						throw;
					}
					VersionedId valueOrDefault2 = coreItem.PropertyBag.GetValueOrDefault<VersionedId>(InternalSchema.ItemId);
					ExTraceGlobals.TaskTracer.TraceWarning<VersionedId>(0L, "Task::CoreObjectUpdateRecurrence. Removing corrupted recurrence blob from task {0}", valueOrDefault2);
					coreItem.PropertyBag.Delete(InternalSchema.TaskRecurrence);
					coreItem.PropertyBag[InternalSchema.IsTaskRecurring] = false;
					flag = false;
					goto IL_AB;
				}
			}
			flag = false;
			IL_AB:
			coreItem.PropertyBag[InternalSchema.IsRecurring] = flag;
		}

		// Token: 0x06004EF8 RID: 20216 RVA: 0x0014B1B0 File Offset: 0x001493B0
		internal static void CoreObjectUpdateTaskDates(CoreItem coreItem)
		{
			ExDateTime? valueAsNullable = coreItem.PropertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.StartDate);
			ExDateTime? valueAsNullable2 = coreItem.PropertyBag.GetValueAsNullable<ExDateTime>(InternalSchema.DueDate);
			if (valueAsNullable != null && valueAsNullable2 == null)
			{
				ExTraceGlobals.TaskTracer.TraceDebug(0L, "Task::ObjectUpdateTaskDates. DueDate is missing so we are adding DueDate.");
				coreItem.PropertyBag[InternalSchema.DueDate] = valueAsNullable;
			}
		}

		// Token: 0x04002AE3 RID: 10979
		private static readonly NativeStorePropertyDefinition[] TaskMasterProperties = new NativeStorePropertyDefinition[]
		{
			InternalSchema.LocalStartDate,
			InternalSchema.LocalDueDate,
			InternalSchema.UtcStartDate,
			InternalSchema.UtcDueDate,
			InternalSchema.ReminderIsSetInternal,
			InternalSchema.TaskRecurrence,
			InternalSchema.TaskStatus,
			InternalSchema.TaskChangeCount,
			InternalSchema.ActualWork,
			InternalSchema.TotalWork,
			InternalSchema.CompleteDate
		};

		// Token: 0x04002AE4 RID: 10980
		private PropertyBag masterProperties;

		// Token: 0x04002AE5 RID: 10981
		private PropertyBag updatedMasterProperties;

		// Token: 0x04002AE6 RID: 10982
		private Task itemOneOff;

		// Token: 0x04002AE7 RID: 10983
		private StoreObjectId oneoffId;

		// Token: 0x04002AE8 RID: 10984
		private bool needOneOffId;

		// Token: 0x04002AE9 RID: 10985
		private bool isSuppressCreateOneOff;

		// Token: 0x04002AEA RID: 10986
		private bool isCreateOneOff;

		// Token: 0x04002AEB RID: 10987
		private ExDateTime? nextInstanceOnMaster = null;

		// Token: 0x04002AEC RID: 10988
		private bool suppressRecurrenceAdjustment;

		// Token: 0x04002AED RID: 10989
		private bool wasNewBeforeSave;

		// Token: 0x04002AEE RID: 10990
		private bool adjustDatesBeforeSave;

		// Token: 0x04002AEF RID: 10991
		private Reminders<ModernReminder> modernReminders;

		// Token: 0x04002AF0 RID: 10992
		private RemindersState<ModernReminderState> modernRemindersState;

		// Token: 0x04002AF1 RID: 10993
		private Reminders<EventTimeBasedInboxReminder> eventTimeBasedInboxReminders;

		// Token: 0x04002AF2 RID: 10994
		private RemindersState<EventTimeBasedInboxReminderState> eventTimeBasedInboxRemindersState;
	}
}
