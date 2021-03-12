using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.CalendarSync;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.MailboxAssistants.Assistants;

namespace Microsoft.Exchange.MailboxAssistants.CalendarSync
{
	// Token: 0x020000BC RID: 188
	internal sealed class CalendarSyncAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x060007C1 RID: 1985 RVA: 0x000384C8 File Offset: 0x000366C8
		public CalendarSyncAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.perfCounters = new CalendarSyncPerformanceCountersInstance(databaseInfo.DatabaseName, CalendarSyncPerformanceCounters.TotalInstance);
			this.CalendarSyncAssistantHelper = new CalendarSyncAssistantHelper(this, this.perfCounters);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x000384FB File Offset: 0x000366FB
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000384FD File Offset: 0x000366FD
		protected override void OnShutdownInternal()
		{
			if (this.PerformanceCounters != null)
			{
				this.PerformanceCounters.Reset();
				this.PerformanceCounters.Close();
				this.PerformanceCounters.Remove();
				this.perfCounters = null;
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00038530 File Offset: 0x00036730
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			DateTime utcNow = DateTime.UtcNow;
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			CalendarSyncLogEntry calendarSyncLogEntry = new CalendarSyncLogEntry
			{
				ProcessingStartTime = utcNow,
				IsOnDemandJob = false,
				MailboxGuid = mailboxSession.MailboxGuid,
				IsArchive = mailboxSession.MailboxOwner.MailboxInfo.IsArchive,
				TenantGuid = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId.GetTenantGuid(),
				MailboxType = mailboxSession.MailboxOwner.RecipientTypeDetails
			};
			this.ThrowIfShuttingDown(mailboxSession, calendarSyncLogEntry, customDataToLog);
			try
			{
				CalendarSyncAssistant.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: CalendarSyncAssistant.InvokeInternal: starting to process mailbox: {1}.", TraceContext.Get(), mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
				ExTimeZone userTimeZone = TimeZoneHelper.GetUserTimeZone(mailboxSession);
				if (userTimeZone != null)
				{
					mailboxSession.ExTimeZone = userTimeZone;
					CalendarSyncAssistant.Tracer.TraceDebug<object, ExTimeZone>((long)this.GetHashCode(), "{0}: CalendarSyncAssistant.InvokeInternal: changed session to user configuration time zone: {1}.", TraceContext.Get(), userTimeZone);
				}
				else
				{
					CalendarSyncAssistant.Tracer.TraceDebug<object, ExTimeZone>((long)this.GetHashCode(), "{0}: CalendarSyncAssistant.InvokeInternal: failed to read user configuration time zone. Use time zone:{1}.", TraceContext.Get(), mailboxSession.ExTimeZone);
					SharingSyncAssistantLog.LogEntry(mailboxSession, "Failed to read user configuration time zone. Use default session time zone:{0}", new object[]
					{
						mailboxSession.ExTimeZone
					});
				}
				if (string.IsNullOrEmpty(invokeArgs.Parameters))
				{
					this.CalendarSyncAssistantHelper.ProcessMailbox(mailboxSession, invokeArgs.TimePerTask, invokeArgs.ActivityId, calendarSyncLogEntry);
				}
				else
				{
					if (this.PerformanceCounters != null)
					{
						this.PerformanceCounters.OnDemandRequests.Increment();
					}
					calendarSyncLogEntry.IsOnDemandJob = true;
					this.CalendarSyncAssistantHelper.ProcessFolder(mailboxSession, invokeArgs.Parameters, calendarSyncLogEntry);
				}
				CalendarSyncAssistant.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: CalendarSyncAssistant.InvokeInternal: successfully processed mailbox: {1}.", TraceContext.Get(), mailboxSession.MailboxOwner.MailboxInfo.DisplayName);
			}
			catch (Exception ex)
			{
				if (ex is AIException && ex.InnerException != null)
				{
					calendarSyncLogEntry.ExceptionType = ex.InnerException.GetType().FullName;
				}
				else
				{
					calendarSyncLogEntry.ExceptionType = ex.GetType().FullName;
				}
				throw;
			}
			finally
			{
				calendarSyncLogEntry.ProcessingEndTime = DateTime.UtcNow;
				calendarSyncLogEntry.TotalProcessingTime = (long)(calendarSyncLogEntry.ProcessingEndTime - utcNow).TotalMilliseconds;
				customDataToLog.AddRange(calendarSyncLogEntry.LogAllData(mailboxSession, invokeArgs.Parameters));
			}
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000387A4 File Offset: 0x000369A4
		private void ThrowIfShuttingDown(MailboxSession mailboxSession, CalendarSyncLogEntry logEntry, List<KeyValuePair<string, object>> customDataToLog)
		{
			if (base.Shutdown)
			{
				CalendarSyncAssistant.Tracer.TraceDebug<object, IExchangePrincipal>((long)this.GetHashCode(), "{0}: Shutdown called during processing of mailbox '{1}'.", TraceContext.Get(), mailboxSession.MailboxOwner);
				Exception ex = new ShutdownException();
				logEntry.ExceptionType = ex.GetType().FullName;
				logEntry.ProcessingEndTime = DateTime.UtcNow;
				logEntry.TotalProcessingTime = (long)(logEntry.ProcessingEndTime - logEntry.ProcessingStartTime).TotalMilliseconds;
				customDataToLog.AddRange(logEntry.LogAllData(mailboxSession, null));
				throw ex;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0003882C File Offset: 0x00036A2C
		private CalendarSyncPerformanceCountersInstance PerformanceCounters
		{
			get
			{
				return this.perfCounters;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00038834 File Offset: 0x00036A34
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0003883C File Offset: 0x00036A3C
		private CalendarSyncAssistantHelper CalendarSyncAssistantHelper { get; set; }

		// Token: 0x060007C9 RID: 1993 RVA: 0x00038851 File Offset: 0x00036A51
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00038859 File Offset: 0x00036A59
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00038861 File Offset: 0x00036A61
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x040005BA RID: 1466
		private static readonly Trace Tracer = ExTraceGlobals.CalendarSyncAssistantTracer;

		// Token: 0x040005BB RID: 1467
		private CalendarSyncPerformanceCountersInstance perfCounters;
	}
}
