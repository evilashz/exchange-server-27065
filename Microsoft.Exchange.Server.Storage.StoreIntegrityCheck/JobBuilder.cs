using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x02000020 RID: 32
	public sealed class JobBuilder
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00005D98 File Offset: 0x00003F98
		private JobBuilder(Context context)
		{
			this.context = context;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005DA7 File Offset: 0x00003FA7
		public static JobBuilder Create(Context context)
		{
			return new JobBuilder(context);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005DB0 File Offset: 0x00003FB0
		public static Guid BuildAndSchedule(Context context, Guid mailboxGuid, IntegrityCheckRequestFlags flags, TaskId[] taskIds, StorePropTag[] propTags, ref Properties[] propertiesRows)
		{
			Guid result = Guid.Empty;
			bool flag = (flags & IntegrityCheckRequestFlags.DetectOnly) != IntegrityCheckRequestFlags.None;
			JobSource source = JobSource.OnDemand;
			JobPriority jobPriority = JobPriority.Normal;
			if ((flags & IntegrityCheckRequestFlags.Maintenance) != IntegrityCheckRequestFlags.None)
			{
				source = JobSource.Maintenance;
				jobPriority = JobPriority.Low;
			}
			else if ((flags & IntegrityCheckRequestFlags.Force) != IntegrityCheckRequestFlags.None)
			{
				jobPriority = JobPriority.High;
			}
			if (jobPriority == JobPriority.Low && InMemoryJobStorage.Instance(context.Database).IsFull)
			{
				return Guid.Empty;
			}
			IList<IntegrityCheckJob> list = JobBuilder.Create(context).ScopeToMailbox(mailboxGuid).CheckCorruptions(taskIds).FromSource(source).WithPriority(jobPriority).DetectOnly(flag).Build();
			if (list != null && list.Count > 0)
			{
				if (propTags != null)
				{
					propertiesRows = new Properties[list.Count];
				}
				for (int i = 0; i < list.Count; i++)
				{
					InMemoryJobStorage.Instance(context.Database).AddJob(list[i]);
					JobScheduler.Instance(context.Database).ScheduleJob(list[i]);
					if (propTags != null)
					{
						propertiesRows[i] = list[i].GetProperties(propTags);
					}
				}
				result = list[0].RequestGuid;
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005ECB File Offset: 0x000040CB
		public JobBuilder ScopeToMailbox(Guid mailboxGuid)
		{
			this.mailboxGuid = mailboxGuid;
			return this;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005ED5 File Offset: 0x000040D5
		public JobBuilder CheckCorruptions(IList<TaskId> taskIds)
		{
			this.taskIds = taskIds;
			return this;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00005EDF File Offset: 0x000040DF
		public JobBuilder FromSource(JobSource source)
		{
			if (!Enum.IsDefined(typeof(JobSource), source))
			{
				throw new StoreException((LID)54364U, ErrorCodeValue.InvalidParameter, "Invalid job source");
			}
			this.jobSource = source;
			return this;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005F1A File Offset: 0x0000411A
		public JobBuilder WithPriority(JobPriority priority)
		{
			if (!Enum.IsDefined(typeof(JobPriority), priority))
			{
				throw new StoreException((LID)42076U, ErrorCodeValue.InvalidParameter, "Invalid job priority");
			}
			this.jobPriority = priority;
			return this;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005F55 File Offset: 0x00004155
		public JobBuilder DetectOnly(bool detectOnly)
		{
			this.detectOnly = detectOnly;
			return this;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005FD8 File Offset: 0x000041D8
		public IList<IntegrityCheckJob> Build()
		{
			List<KeyValuePair<int, Guid>> mailboxes = null;
			MailboxTable mailboxTable = DatabaseSchema.MailboxTable(this.context.Database);
			MailboxPropValueGetter mailboxPropValueGetter = new MailboxPropValueGetter(this.context);
			ErrorCode errorCode = mailboxPropValueGetter.Execute(this.mailboxGuid, new Column[]
			{
				mailboxTable.MailboxNumber,
				mailboxTable.MailboxGuid
			}, delegate(Reader reader)
			{
				int @int = reader.GetInt32(mailboxTable.MailboxNumber);
				Guid? nullableGuid = reader.GetNullableGuid(mailboxTable.MailboxGuid);
				if (nullableGuid != null)
				{
					if (mailboxes == null)
					{
						mailboxes = new List<KeyValuePair<int, Guid>>();
					}
					mailboxes.Add(new KeyValuePair<int, Guid>(@int, nullableGuid.Value));
				}
				return ErrorCode.NoError;
			}, () => true);
			if (ErrorCode.NoError != errorCode)
			{
				throw new StoreException((LID)33884U, errorCode, "Request expansion failed");
			}
			if (mailboxes == null || mailboxes.Count == 0)
			{
				throw new StoreException((LID)58460U, ErrorCodeValue.UnknownUser, "No mailbox found");
			}
			Guid requestGuid = Guid.NewGuid();
			DateTime utcNow = DateTime.UtcNow;
			List<IntegrityCheckJob> list = new List<IntegrityCheckJob>(mailboxes.Count);
			foreach (KeyValuePair<int, Guid> keyValuePair in mailboxes)
			{
				foreach (TaskId taskId in this.taskIds)
				{
					list.Add(new IntegrityCheckJob(Guid.NewGuid(), requestGuid, keyValuePair.Key, keyValuePair.Value, this.detectOnly, taskId, utcNow, this.jobSource, this.jobPriority));
				}
			}
			return list;
		}

		// Token: 0x04000077 RID: 119
		private readonly Context context;

		// Token: 0x04000078 RID: 120
		private Guid mailboxGuid;

		// Token: 0x04000079 RID: 121
		private bool detectOnly;

		// Token: 0x0400007A RID: 122
		private IList<TaskId> taskIds;

		// Token: 0x0400007B RID: 123
		private JobSource jobSource;

		// Token: 0x0400007C RID: 124
		private JobPriority jobPriority;
	}
}
