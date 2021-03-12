using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000381 RID: 897
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class BirthdayCalendar
	{
		// Token: 0x060027A6 RID: 10150 RVA: 0x0009E2EC File Offset: 0x0009C4EC
		public static CalendarFolder Bind(MailboxSession session)
		{
			return CalendarFolder.Bind(session, DefaultFolderType.BirthdayCalendar);
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x0009E2F6 File Offset: 0x0009C4F6
		public static bool UserHasBirthdayCalendarFolder(MailboxSession session)
		{
			return BirthdayCalendar.GetBirthdayCalendarFolderId(session) != null;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x0009E304 File Offset: 0x0009C504
		public static StoreObjectId GetBirthdayCalendarFolderId(MailboxSession session)
		{
			ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::GetBirthdayCalendarFolderId. GetDefaultFolderId. MailboxGuid:{0}", session.MailboxGuid);
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.BirthdayCalendar);
			ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<StoreObjectId, Guid>(0L, "BirthdayCalendar::GetBirthdayCalendarFolderId. FolderId: {0} MailboxGuid:{1}", defaultFolderId, session.MailboxGuid);
			return defaultFolderId;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0009E354 File Offset: 0x0009C554
		public static void EnableBirthdayCalendar(MailboxSession session)
		{
			if (!BirthdayCalendar.UserHasBirthdayCalendarFolder(session))
			{
				ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::EnableBirthdayCalendar CreateDefaultFolder. MailboxGuid:{0}", session.MailboxGuid);
				session.CreateDefaultFolder(DefaultFolderType.BirthdayCalendar);
				return;
			}
			ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::EnableBirthdayCalendar BindWithRecovery. MailboxGuid:{0}", session.MailboxGuid);
			BirthdayCalendar.BindWithRecovery(session, delegate(CalendarFolder birthdayCalendar)
			{
				BirthdayCalendar.SetBirthdayCalendarHiddenValue(birthdayCalendar, false);
			});
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0009E3C4 File Offset: 0x0009C5C4
		public static void DisableBirthdayCalendar(MailboxSession session)
		{
			ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::DisableBirthdayCalendar. MailboxGuid:{0}", session.MailboxGuid);
			StoreObjectId birthdayCalendarFolderId = BirthdayCalendar.GetBirthdayCalendarFolderId(session);
			if (birthdayCalendarFolderId == null)
			{
				ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::DisableBirthdayCalendar. Folder doesn't exist. MailboxGuid:{0}", session.MailboxGuid);
				return;
			}
			try
			{
				ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::DisableBirthdayCalendar. Set IsHidden. MailboxGuid:{0}", session.MailboxGuid);
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(session, birthdayCalendarFolderId))
				{
					BirthdayCalendar.SetBirthdayCalendarHiddenValue(calendarFolder, true);
				}
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::DisableBirthdayCalendar. ObjectNotFoundException. MailboxGuid:{0}", session.MailboxGuid);
			}
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x0009E474 File Offset: 0x0009C674
		public static void DeleteBirthdayCalendar(MailboxSession session)
		{
			ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::DeleteBirthdayCalendar. MailboxGuid:{0}", session.MailboxGuid);
			StoreObjectId storeObjectId = session.RefreshDefaultFolder(DefaultFolderType.BirthdayCalendar);
			ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid, StoreObjectId>(0L, "BirthdayCalendar::DeleteBirthdayCalendar. RefreshDefaultFolder. MailboxGuid:{0}. FolderId: {1}", session.MailboxGuid, storeObjectId);
			if (storeObjectId == null)
			{
				ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::DeleteBirthdayCalendar. Folder doesn't exist. MailboxGuid:{0}", session.MailboxGuid);
				return;
			}
			try
			{
				ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::DeleteBirthdayCalendar. Perform default folder delete. MailboxGuid:{0}", session.MailboxGuid);
				session.DeleteDefaultFolder(DefaultFolderType.BirthdayCalendar, DeleteItemFlags.HardDelete);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.BirthdayCalendarTracer.TraceError<Exception, string, Guid>(0L, "BirthdayCalendar::DeleteBirthdayCalendar. Unable to delete folder. Exception: {0}. Stack: {1}. MailboxGuid:{2}", ex, ex.StackTrace, session.MailboxGuid);
				throw;
			}
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x0009E528 File Offset: 0x0009C728
		private static void BindWithRecovery(MailboxSession session, Action<CalendarFolder> folderOperationDelegate)
		{
			ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::BindWithRecovery. MailboxGuid:{0}", session.MailboxGuid);
			try
			{
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(session, DefaultFolderType.BirthdayCalendar))
				{
					ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::BindWithRecovery. First bind was successful. MailboxGuid:{0}", session.MailboxGuid);
					folderOperationDelegate(calendarFolder);
				}
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::BindWithRecovery. ObjectNotFound. MailboxGuid:{0}", session.MailboxGuid);
				StoreObjectId folderId;
				if (!session.TryFixDefaultFolderId(DefaultFolderType.BirthdayCalendar, out folderId))
				{
					ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::BindWithRecovery. Fixup unsuccessful. MailboxGuid:{0}", session.MailboxGuid);
					throw;
				}
				ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<Guid>(0L, "BirthdayCalendar::BindWithRecovery. Fixup successful. MailboxGuid:{0}", session.MailboxGuid);
				using (CalendarFolder calendarFolder2 = CalendarFolder.Bind(session, folderId))
				{
					folderOperationDelegate(calendarFolder2);
				}
			}
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x0009E61C File Offset: 0x0009C81C
		private static void SetBirthdayCalendarHiddenValue(CalendarFolder birthdayCalendar, bool isHidden)
		{
			StoreSession session = birthdayCalendar.Session;
			ExTraceGlobals.BirthdayCalendarTracer.TraceDebug<bool, Guid>(0L, "BirthdayCalendar::SetBirthdayCalendarHiddenValue. IsHidden:{0}. MailboxGuid:{1}", isHidden, session.MailboxGuid);
			if (birthdayCalendar.IsHidden == isHidden)
			{
				return;
			}
			birthdayCalendar.IsHidden = isHidden;
			birthdayCalendar.Save(SaveMode.NoConflictResolution);
		}
	}
}
