using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000E0 RID: 224
	internal sealed class CalendarSettings
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0003A11C File Offset: 0x0003831C
		internal static CalendarSettings CreateForSession(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			CalendarSettings result;
			if (CalendarSettings.LoadFromMailbox(userContext.MailboxSession, userContext.CanActAsOwner, out result) != CalendarSettings.LoadResult.Success)
			{
				ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, "Unable to load Calendar Settings User Configuration object");
				result = new CalendarSettings(15, true, false, true, false);
			}
			return result;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0003A16C File Offset: 0x0003836C
		private static CalendarSettings.LoadResult LoadFromMailbox(MailboxSession mailboxSession, bool canActAsOwner, out CalendarSettings calendarSettings)
		{
			if (!canActAsOwner)
			{
				calendarSettings = null;
				return CalendarSettings.LoadResult.AccessDenied;
			}
			CalendarConfiguration calendarConfiguration;
			try
			{
				using (CalendarConfigurationDataProvider calendarConfigurationDataProvider = new CalendarConfigurationDataProvider(mailboxSession))
				{
					calendarConfiguration = (CalendarConfiguration)calendarConfigurationDataProvider.Read<CalendarConfiguration>(null);
				}
				if (calendarConfiguration == null)
				{
					string message = string.Format("Unable to load Calendar configuration object for mailbox {0}", mailboxSession.MailboxOwner.LegacyDn);
					ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, message);
					calendarSettings = null;
					return CalendarSettings.LoadResult.Corrupt;
				}
			}
			catch (StoragePermanentException ex)
			{
				string message2 = string.Format("Unable to load Calendar configuration object for mailbox. Exception {0}", ex.Message);
				ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, message2);
				calendarSettings = null;
				return CalendarSettings.LoadResult.Missing;
			}
			catch (StorageTransientException ex2)
			{
				string message3 = string.Format("Unable to load Calendar configuration object for mailbox. Exception {0}", ex2.Message);
				ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, message3);
				calendarSettings = null;
				return CalendarSettings.LoadResult.Missing;
			}
			calendarSettings = new CalendarSettings(CalendarSettings.ValidateReminderInterval(calendarConfiguration.DefaultReminderTime), calendarConfiguration.AddNewRequestsTentatively, calendarConfiguration.ProcessExternalMeetingMessages, calendarConfiguration.RemoveOldMeetingMessages, calendarConfiguration.RemoveForwardedMeetingNotifications);
			return CalendarSettings.LoadResult.Success;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0003A280 File Offset: 0x00038480
		internal bool CommitChanges(MailboxSession mailboxSession)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			try
			{
				using (CalendarConfigurationDataProvider calendarConfigurationDataProvider = new CalendarConfigurationDataProvider(mailboxSession))
				{
					CalendarConfiguration calendarConfiguration = (CalendarConfiguration)calendarConfigurationDataProvider.Read<CalendarConfiguration>(null);
					if (calendarConfiguration == null)
					{
						string message = string.Format("Unable to load Calendar configuration object for mailbox {0}", mailboxSession.MailboxOwner.LegacyDn);
						ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, message);
						calendarConfigurationDataProvider.Delete(new CalendarConfiguration());
						calendarConfiguration = (CalendarConfiguration)calendarConfigurationDataProvider.Read<CalendarConfiguration>(null);
						if (calendarConfiguration == null)
						{
							message = string.Format("Unable to re-create Calendar configuration object for mailbox {0}", mailboxSession.MailboxOwner.LegacyDn);
							ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, message);
							return false;
						}
					}
					calendarConfiguration.AddNewRequestsTentatively = this.addNewRequestsTentatively;
					calendarConfiguration.RemoveOldMeetingMessages = this.removeOldMeetingMessages;
					calendarConfiguration.RemoveForwardedMeetingNotifications = this.removeForwardedMeetingNotifications;
					calendarConfiguration.DefaultReminderTime = this.defaultReminderTime;
					calendarConfiguration.ProcessExternalMeetingMessages = this.processExternalMeetingMessages;
					calendarConfigurationDataProvider.Save(calendarConfiguration);
				}
			}
			catch (ObjectExistedException ex)
			{
				string message2 = string.Format("Unable to load Calendar configuration object for mailbox. Exception {0}", ex.Message);
				ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, message2);
				return false;
			}
			catch (StoragePermanentException ex2)
			{
				string message3 = string.Format("Unable to load Calendar configuration object for mailbox. Exception {0}", ex2.Message);
				ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, message3);
				return false;
			}
			catch (StorageTransientException ex3)
			{
				string message4 = string.Format("Unable to load Calendar configuration object for mailbox. Exception {0}", ex3.Message);
				ExTraceGlobals.CalendarCallTracer.TraceDebug(0L, message4);
				return false;
			}
			return true;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0003A41C File Offset: 0x0003861C
		private static int ValidateReminderInterval(int reminderInterval)
		{
			if (reminderInterval <= 5040000 && reminderInterval >= 0)
			{
				ExTraceGlobals.CalendarCallTracer.TraceDebug<int>(0L, "Returning original value: {0}", reminderInterval);
				return reminderInterval;
			}
			if (reminderInterval > 5040000)
			{
				ExTraceGlobals.CalendarCallTracer.TraceDebug<int>(0L, "Reminder interval exceeds maximum.  Returning max reminder interval value: {0}", 5040000);
				return 5040000;
			}
			ExTraceGlobals.CalendarCallTracer.TraceDebug<int>(0L, "Reminder interval is a negative integer.  Returning default value: {0}", 15);
			return 15;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0003A484 File Offset: 0x00038684
		private CalendarSettings(int defaultReminderTime, bool autoCreate, bool processExternal, bool removeOldMeetingMessages, bool removeForwardedMeetingNotifications)
		{
			this.defaultReminderTime = defaultReminderTime;
			this.addNewRequestsTentatively = autoCreate;
			this.processExternalMeetingMessages = processExternal;
			this.removeOldMeetingMessages = removeOldMeetingMessages;
			this.removeForwardedMeetingNotifications = removeForwardedMeetingNotifications;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0003A4D2 File Offset: 0x000386D2
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0003A4DA File Offset: 0x000386DA
		internal int DefaultReminderTime
		{
			get
			{
				return this.defaultReminderTime;
			}
			set
			{
				this.defaultReminderTime = CalendarSettings.ValidateReminderInterval(value);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0003A4E8 File Offset: 0x000386E8
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0003A4F0 File Offset: 0x000386F0
		internal bool AddNewRequestsTentatively
		{
			get
			{
				return this.addNewRequestsTentatively;
			}
			set
			{
				this.addNewRequestsTentatively = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0003A4F9 File Offset: 0x000386F9
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x0003A501 File Offset: 0x00038701
		internal bool ProcessExternalMeetingMessages
		{
			get
			{
				return this.processExternalMeetingMessages;
			}
			set
			{
				this.processExternalMeetingMessages = value;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0003A50A File Offset: 0x0003870A
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0003A512 File Offset: 0x00038712
		internal bool RemoveOldMeetingMessages
		{
			get
			{
				return this.removeOldMeetingMessages;
			}
			set
			{
				this.removeOldMeetingMessages = value;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0003A51B File Offset: 0x0003871B
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0003A523 File Offset: 0x00038723
		internal bool RemoveForwardedMeetingNotifications
		{
			get
			{
				return this.removeForwardedMeetingNotifications;
			}
			set
			{
				this.removeForwardedMeetingNotifications = value;
			}
		}

		// Token: 0x0400054C RID: 1356
		private const int DefaultDefaultReminderTime = 15;

		// Token: 0x0400054D RID: 1357
		private const bool DefaultAddNewRequestsTentatively = true;

		// Token: 0x0400054E RID: 1358
		private const bool DefaultProcessExternalMeetingMessages = false;

		// Token: 0x0400054F RID: 1359
		private const bool DefaultRemoveOldMeetingMessages = true;

		// Token: 0x04000550 RID: 1360
		private const bool DefaultRemoveForwardedMeetingNotification = false;

		// Token: 0x04000551 RID: 1361
		internal const int AllowedMaxReminderInterval = 5040000;

		// Token: 0x04000552 RID: 1362
		private int defaultReminderTime = 15;

		// Token: 0x04000553 RID: 1363
		private bool addNewRequestsTentatively = true;

		// Token: 0x04000554 RID: 1364
		private bool processExternalMeetingMessages;

		// Token: 0x04000555 RID: 1365
		private bool removeOldMeetingMessages = true;

		// Token: 0x04000556 RID: 1366
		private bool removeForwardedMeetingNotifications;

		// Token: 0x020000E1 RID: 225
		private enum LoadResult
		{
			// Token: 0x04000558 RID: 1368
			Success,
			// Token: 0x04000559 RID: 1369
			Missing,
			// Token: 0x0400055A RID: 1370
			AccessDenied,
			// Token: 0x0400055B RID: 1371
			Corrupt
		}
	}
}
