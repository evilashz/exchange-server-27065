using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.BackgroundJobBackend
{
	// Token: 0x0200003E RID: 62
	internal class SaveTaskItemBatch : BackgroundJobBackendBase, IEnumerable<TaskItem>, IEnumerable
	{
		// Token: 0x06000207 RID: 519 RVA: 0x000079DF File Offset: 0x00005BDF
		public SaveTaskItemBatch()
		{
			this.batch = new List<TaskItem>(5);
			this.InsertedTasks = false;
			this[SaveTaskItemBatch.NextActiveJobIdProperty] = null;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007A06 File Offset: 0x00005C06
		public SaveTaskItemBatch(int initialCapacity)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException("initialCapacity < 0");
			}
			this.batch = new List<TaskItem>(initialCapacity);
			this.InsertedTasks = false;
			this[SaveTaskItemBatch.NextActiveJobIdProperty] = null;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007A3C File Offset: 0x00005C3C
		public SaveTaskItemBatch(List<TaskItem> taskItems, Guid backgroundJobId, DateTime lastScheduledTime, Guid? nextActiveJobId = null)
		{
			if (taskItems != null)
			{
				this.batch = taskItems;
			}
			else
			{
				this.batch = new List<TaskItem>();
			}
			this.BackgroundJobId = backgroundJobId;
			this.LastScheduledTime = lastScheduledTime;
			this.InsertedTasks = false;
			if (nextActiveJobId != null)
			{
				this.NextActiveJobId = nextActiveJobId.Value;
				return;
			}
			this[SaveTaskItemBatch.NextActiveJobIdProperty] = null;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00007A9E File Offset: 0x00005C9E
		public int TaskCount
		{
			get
			{
				return this.batch.Count;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00007AAB File Offset: 0x00005CAB
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00007ABD File Offset: 0x00005CBD
		public Guid BackgroundJobId
		{
			get
			{
				return (Guid)this[SaveTaskItemBatch.BackgroundJobIdProperty];
			}
			set
			{
				this[SaveTaskItemBatch.BackgroundJobIdProperty] = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00007AD0 File Offset: 0x00005CD0
		public Guid ActiveJobId
		{
			get
			{
				if (this.batch.Count == 0)
				{
					throw new ArgumentException("Unable to determine active job id because the batch of task items is empty.");
				}
				return this.batch[0].ActiveJobId;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00007AFB File Offset: 0x00005CFB
		public Guid ScheduleId
		{
			get
			{
				if (this.batch.Count == 0)
				{
					throw new ArgumentException("Unable to determine schedule id because the batch of task items is empty.");
				}
				return this.batch[0].ScheduleId;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00007B26 File Offset: 0x00005D26
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00007B38 File Offset: 0x00005D38
		public DateTime LastScheduledTime
		{
			get
			{
				return (DateTime)this[SaveTaskItemBatch.LastScheduledTimeProperty];
			}
			set
			{
				this[SaveTaskItemBatch.LastScheduledTimeProperty] = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00007B4B File Offset: 0x00005D4B
		public bool HasNextActiveJobId
		{
			get
			{
				return this[SaveTaskItemBatch.NextActiveJobIdProperty] != null;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00007B5E File Offset: 0x00005D5E
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00007B70 File Offset: 0x00005D70
		public Guid NextActiveJobId
		{
			get
			{
				return (Guid)this[SaveTaskItemBatch.NextActiveJobIdProperty];
			}
			set
			{
				this[SaveTaskItemBatch.NextActiveJobIdProperty] = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00007B83 File Offset: 0x00005D83
		// (set) Token: 0x06000215 RID: 533 RVA: 0x00007B95 File Offset: 0x00005D95
		public bool InsertedTasks
		{
			get
			{
				return (bool)this[SaveTaskItemBatch.InsertedTasksProperty];
			}
			set
			{
				this[SaveTaskItemBatch.InsertedTasksProperty] = value;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007BA8 File Offset: 0x00005DA8
		public void Clear()
		{
			this.batch.Clear();
			this.InsertedTasks = false;
			this.ClearNextActiveJobId();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007BC2 File Offset: 0x00005DC2
		public void ClearNextActiveJobId()
		{
			this[SaveTaskItemBatch.NextActiveJobIdProperty] = null;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public IEnumerator<TaskItem> GetEnumerator()
		{
			return this.batch.GetEnumerator();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007BE2 File Offset: 0x00005DE2
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007BEC File Offset: 0x00005DEC
		public void Add(TaskItem taskItem)
		{
			if (this.batch.Count > 0)
			{
				if (this.batch[0].ActiveJobId != taskItem.ActiveJobId)
				{
					throw new ArgumentException("Unable to add a task item to the batch because it is for a active job id.", "taskItem");
				}
				if (this.batch[0].ScheduleId != taskItem.ScheduleId)
				{
					throw new ArgumentException("Unable to add a task item to the batch because it is for a diffrent schedule id.", "taskItem");
				}
			}
			else
			{
				if (taskItem.ActiveJobId.Equals(Guid.Empty))
				{
					throw new ArgumentException("Unable to add task item to the batch because the ActiveJobId is not specified.", "taskItem");
				}
				if (taskItem.ScheduleId.Equals(Guid.Empty))
				{
					throw new ArgumentException("Unable to add task item to the batch because the ScheduleId is not specified.", "taskItem");
				}
			}
			this.batch.Add(taskItem);
		}

		// Token: 0x04000163 RID: 355
		internal static readonly BackgroundJobBackendPropertyDefinition BackgroundJobIdProperty = JobDefinition.BackgroundJobIdProperty;

		// Token: 0x04000164 RID: 356
		internal static readonly BackgroundJobBackendPropertyDefinition NextActiveJobIdProperty = ScheduleItemProperties.NextActiveJobIdProperty;

		// Token: 0x04000165 RID: 357
		internal static readonly BackgroundJobBackendPropertyDefinition LastScheduledTimeProperty = ScheduleItemProperties.LastScheduledTimeProperty;

		// Token: 0x04000166 RID: 358
		internal static readonly BackgroundJobBackendPropertyDefinition InsertedTasksProperty = new BackgroundJobBackendPropertyDefinition("InsertedTasks", typeof(bool), PropertyDefinitionFlags.Mandatory | PropertyDefinitionFlags.PersistDefaultValue, false);

		// Token: 0x04000167 RID: 359
		private List<TaskItem> batch;
	}
}
