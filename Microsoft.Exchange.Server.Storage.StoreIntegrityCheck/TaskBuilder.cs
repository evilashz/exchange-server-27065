using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000036 RID: 54
	public class TaskBuilder
	{
		// Token: 0x06000113 RID: 275 RVA: 0x00008C70 File Offset: 0x00006E70
		private TaskBuilder(TaskId taskId)
		{
			this.taskId = taskId;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008C7F File Offset: 0x00006E7F
		public static TaskBuilder Create(TaskId taskId)
		{
			return new TaskBuilder(taskId);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008C87 File Offset: 0x00006E87
		public TaskBuilder TrackedBy(IJobExecutionTracker tracker)
		{
			this.tracker = tracker;
			return this;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008C94 File Offset: 0x00006E94
		public IIntegrityCheckTask Build()
		{
			TaskId taskId = this.taskId;
			if (taskId <= TaskId.MidsetDeleted)
			{
				switch (taskId)
				{
				case TaskId.SearchBacklinks:
					return new SearchBacklinksCheckTask(this.tracker);
				case TaskId.FolderView:
					break;
				case TaskId.AggregateCounts:
					return new AggregateCountsCheckTask(this.tracker);
				default:
					if (taskId == TaskId.MidsetDeleted)
					{
						return new MidsetDeletedCheckTask(this.tracker);
					}
					break;
				}
			}
			else
			{
				switch (taskId)
				{
				case TaskId.RuleMessageClass:
					return new RuleMessageClassCheckTask(this.tracker);
				case TaskId.RestrictionFolder:
					return new FolderTypeCheckTask(this.tracker);
				case TaskId.FolderACL:
					return new FolderAclCheckTask(this.tracker);
				case TaskId.UniqueMidIndex:
					return new UniqueMidIndexCheckTask(this.tracker);
				case TaskId.CorruptJunkRule:
					return new CorruptJunkRuleCheckTask(this.tracker);
				case TaskId.MissingSpecialFolders:
					return new MissingSpecialFoldersCheckTask(this.tracker);
				case TaskId.DropAllLazyIndexes:
					return new DropAllLazyIndexesTask(this.tracker);
				case TaskId.ImapId:
					return new ImapIdCheckTask(this.tracker);
				case TaskId.InMemoryFolderHierarchy:
					return new InMemoryFolderHierarchyCheckTask(this.tracker);
				case TaskId.DiscardFolderHierarchyCache:
					return new DiscardFolderHierarchyCacheTask(this.tracker);
				default:
					if (taskId == TaskId.ScheduledCheck)
					{
						return new ScheduledCheckTask(this.tracker);
					}
					break;
				}
			}
			return new NullIntegrityCheckTask(this.taskId, this.tracker);
		}

		// Token: 0x040000C4 RID: 196
		private readonly TaskId taskId;

		// Token: 0x040000C5 RID: 197
		private IJobExecutionTracker tracker;
	}
}
