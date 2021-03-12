using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage.ActivityLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F0E RID: 3854
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActivityLogger : IActivityLogger
	{
		// Token: 0x060084A9 RID: 33961 RVA: 0x002437C2 File Offset: 0x002419C2
		public static IActivityLogger Create(IMailboxSession mailboxSession)
		{
			if (!ActivityLogger.IsLoggingEnabled)
			{
				return null;
			}
			return new ActivityLogger(mailboxSession);
		}

		// Token: 0x060084AA RID: 33962 RVA: 0x002437D3 File Offset: 0x002419D3
		public static IActivityLogger Create()
		{
			return ActivityLogger.Create(null);
		}

		// Token: 0x060084AB RID: 33963 RVA: 0x002437DC File Offset: 0x002419DC
		private ActivityLogger(IMailboxSession mailboxSession)
		{
			MailboxSession mailboxSession2 = mailboxSession as MailboxSession;
			if (mailboxSession2 != null)
			{
				this.storeLogger = ActivityLogFactory.Current.Bind(mailboxSession2);
			}
			if (this.storeLogger != null)
			{
				this.storeEligibleActivities = (this.storeLogger.IsGroup() ? ActivityLogger.GroupLoggedToStoreActivityIds : ActivityLogger.LoggedToStoreActivityIds);
			}
		}

		// Token: 0x060084AC RID: 33964 RVA: 0x00243868 File Offset: 0x00241A68
		public void Log(IEnumerable<Activity> activities)
		{
			if (activities == null || !activities.Any<Activity>())
			{
				throw new ArgumentException("activities enumerable cannot be null or empty");
			}
			List<Activity> activitiesCache = (from activity in activities
			where activity != null
			select activity).ToList<Activity>();
			ActivityLogger.activityPerfCounters.ActivityLogsActivityCount.IncrementBy((long)activitiesCache.Count);
			if (this.storeLogger != null)
			{
				bool flag = ActivityLogHelper.CatchNonFatalExceptions(delegate
				{
					this.storeLogger.Append(this.GetStoreEligibleActivities(activitiesCache));
				}, null);
				if (flag)
				{
					ActivityLogger.activityPerfCounters.ActivityLogsStoreWriteExceptions.Increment();
				}
			}
			ActivityFileLogger.Instance.Log(activitiesCache);
		}

		// Token: 0x060084AD RID: 33965 RVA: 0x00243AE8 File Offset: 0x00241CE8
		private IEnumerable<Activity> GetStoreEligibleActivities(IEnumerable<Activity> activities)
		{
			foreach (Activity activity in activities)
			{
				if (this.storeEligibleActivities.Contains(activity.Id))
				{
					ActivityLogger.activityPerfCounters.ActivityLogsSelectedForStore.Increment();
					yield return activity;
				}
			}
			yield break;
		}

		// Token: 0x040058DE RID: 22750
		internal static readonly HashSet<ActivityId> LoggedToStoreActivityIds = new HashSet<ActivityId>
		{
			ActivityId.Categorize,
			ActivityId.ClutterGroupClosed,
			ActivityId.ClutterGroupOpened,
			ActivityId.Delete,
			ActivityId.Flag,
			ActivityId.FlagCleared,
			ActivityId.FlagComplete,
			ActivityId.Forward,
			ActivityId.Logon,
			ActivityId.MarkAsRead,
			ActivityId.MarkAsUnread,
			ActivityId.Move,
			ActivityId.NewMessage,
			ActivityId.ReadingPaneDisplayStart,
			ActivityId.ReadingPaneDisplayEnd,
			ActivityId.Reply,
			ActivityId.ReplyAll,
			ActivityId.MarkMessageAsClutter,
			ActivityId.MarkMessageAsNotClutter,
			ActivityId.MoveFromInbox,
			ActivityId.MoveToInbox,
			ActivityId.MoveFromClutter,
			ActivityId.MoveToClutter,
			ActivityId.DeleteFromInbox,
			ActivityId.DeleteFromClutter,
			ActivityId.RemoteSend,
			ActivityId.CreateCalendarEvent,
			ActivityId.UpdateCalendarEvent,
			ActivityId.ServerLogon
		};

		// Token: 0x040058DF RID: 22751
		internal static readonly HashSet<ActivityId> GroupLoggedToStoreActivityIds = new HashSet<ActivityId>
		{
			ActivityId.Forward,
			ActivityId.Like,
			ActivityId.ReadingPaneDisplayStart,
			ActivityId.ReadingPaneDisplayEnd,
			ActivityId.Reply,
			ActivityId.ReplyAll
		};

		// Token: 0x040058E0 RID: 22752
		private static readonly bool IsLoggingEnabled = ActivityLogHelper.IsActivityLoggingEnabled(false);

		// Token: 0x040058E1 RID: 22753
		private static MiddleTierStoragePerformanceCountersInstance activityPerfCounters = NamedPropMap.GetPerfCounters();

		// Token: 0x040058E2 RID: 22754
		private readonly HashSet<ActivityId> storeEligibleActivities;

		// Token: 0x040058E3 RID: 22755
		private readonly IActivityLog storeLogger;
	}
}
