using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002C RID: 44
	internal class JobSortKey : IComparable<JobSortKey>
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00009F49 File Offset: 0x00008149
		public JobSortKey(RequestPriority priority, JobSortFlags flags, DateTime lastUpdate, Guid requestGuid)
		{
			this.priority = priority;
			this.flags = flags;
			this.lastUpdate = lastUpdate;
			this.requestGuid = requestGuid;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009F70 File Offset: 0x00008170
		public int CompareTo(JobSortKey other)
		{
			if (this.priority != other.priority)
			{
				return other.priority.CompareTo(this.priority);
			}
			if (!this.flags.HasFlag(JobSortFlags.IsInteractive) && !other.flags.HasFlag(JobSortFlags.IsInteractive) && this.flags.HasFlag(JobSortFlags.HasReservations) != other.flags.HasFlag(JobSortFlags.HasReservations))
			{
				return other.flags.HasFlag(JobSortFlags.HasReservations).CompareTo(this.flags.HasFlag(JobSortFlags.HasReservations));
			}
			return JobSortKey.CompareTimeAndRequestGuid(this.lastUpdate, other.lastUpdate, this.requestGuid, other.requestGuid);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000A058 File Offset: 0x00008258
		private static int CompareTimeAndRequestGuid(DateTime first, DateTime second, Guid firstGuid, Guid secondGuid)
		{
			int num = first.CompareTo(second);
			if (num != 0)
			{
				return num;
			}
			return firstGuid.CompareTo(secondGuid);
		}

		// Token: 0x040000CC RID: 204
		private readonly RequestPriority priority;

		// Token: 0x040000CD RID: 205
		private readonly JobSortFlags flags;

		// Token: 0x040000CE RID: 206
		private readonly DateTime lastUpdate;

		// Token: 0x040000CF RID: 207
		private readonly Guid requestGuid;
	}
}
