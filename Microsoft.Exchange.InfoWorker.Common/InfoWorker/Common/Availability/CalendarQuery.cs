using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.Security.Authorization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x020000EE RID: 238
	internal sealed class CalendarQuery : LocalQuery
	{
		// Token: 0x06000650 RID: 1616 RVA: 0x0001B944 File Offset: 0x00019B44
		internal CalendarQuery(ClientContext clientContext, FreeBusyViewOptions requestedFreeBusyView, bool defaultFreeBusyOnly, DateTime deadline) : base(clientContext, deadline)
		{
			this.requestedFreeBusyView = requestedFreeBusyView;
			this.defaultFreeBusyOnly = defaultFreeBusyOnly;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x0001B960 File Offset: 0x00019B60
		internal override BaseQueryResult GetData(BaseQuery query)
		{
			FreeBusyQuery freeBusyQuery = (FreeBusyQuery)query;
			Guid serverRequestId = Trace.TraceCasStart(CasTraceEventType.Availability);
			EmailAddress email = freeBusyQuery.Email;
			BaseQueryResult dataInternal = this.GetDataInternal(freeBusyQuery, email);
			this.TraceRequestStop(email, serverRequestId);
			return dataInternal;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0001B998 File Offset: 0x00019B98
		internal BaseQueryResult GetDataInternal(FreeBusyQuery freeBusyQuery, EmailAddress emailAddress)
		{
			BaseQueryResult result;
			try
			{
				TimeSpan t = this.deadline - DateTime.UtcNow;
				if (t <= TimeSpan.Zero)
				{
					result = this.HandleException(emailAddress, new TimeoutExpiredException("Opening-Mailbox-Session"));
				}
				else
				{
					using (MailboxSession mailboxSession = MailboxSession.OpenAsSystemService(freeBusyQuery.ExchangePrincipal, CultureInfo.InvariantCulture, "Client=AS"))
					{
						mailboxSession.AccountingObject = this.clientContext.Budget;
						this.clientContext.CheckOverBudget();
						Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability.ExTraceGlobals.FaultInjectionTracer.TraceTest(2204511549U);
						t = this.deadline - DateTime.UtcNow;
						if (t <= TimeSpan.Zero)
						{
							result = this.HandleException(emailAddress, new TimeoutExpiredException("Starting-Calendar-Query"));
						}
						else
						{
							result = this.InternalGetCalendarData(freeBusyQuery, mailboxSession);
						}
					}
				}
			}
			catch (MailboxInSiteFailoverException e)
			{
				result = this.HandleMailboxFailoverException(emailAddress, e);
			}
			catch (MailboxCrossSiteFailoverException e2)
			{
				result = this.HandleMailboxFailoverException(emailAddress, e2);
			}
			catch (VirusScanInProgressException e3)
			{
				LocalizedString message = Strings.descVirusScanInProgress(emailAddress.ToString());
				result = this.HandleException(emailAddress, e3, message);
			}
			catch (VirusDetectedException e4)
			{
				LocalizedString message2 = Strings.descVirusDetected(emailAddress.ToString());
				result = this.HandleException(emailAddress, e4, message2);
			}
			catch (AuthzException innerException)
			{
				result = this.HandleException(emailAddress, new Win32InteropException(innerException));
			}
			catch (LocalizedException e5)
			{
				result = this.HandleConnectionException(emailAddress, e5);
			}
			return result;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0001BB40 File Offset: 0x00019D40
		private static FreeBusyViewType GetReturnView(FreeBusyViewType requestedView, FreeBusyPermissionLevel freeBusyPermissionLevel)
		{
			FreeBusyViewType freeBusyViewType = FreeBusyViewType.None;
			switch (freeBusyPermissionLevel)
			{
			case FreeBusyPermissionLevel.None:
				freeBusyViewType = FreeBusyViewType.None;
				break;
			case FreeBusyPermissionLevel.Simple:
				freeBusyViewType = FreeBusyViewType.FreeBusy;
				break;
			case FreeBusyPermissionLevel.Detail:
			case FreeBusyPermissionLevel.Owner:
				freeBusyViewType = FreeBusyViewType.Detailed;
				break;
			}
			bool flag = FreeBusyViewOptions.IsMerged(requestedView);
			int num;
			int num2;
			if (flag)
			{
				num = (int)(requestedView | FreeBusyViewType.MergedOnly);
				num2 = (int)(freeBusyViewType | FreeBusyViewType.MergedOnly);
			}
			else
			{
				num = (int)requestedView;
				num2 = (int)freeBusyViewType;
			}
			int result;
			if (num <= num2)
			{
				result = num;
			}
			else
			{
				result = num2;
			}
			return (FreeBusyViewType)result;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0001BBA4 File Offset: 0x00019DA4
		private FreeBusyQueryResult HandleConnectionException(EmailAddress emailAddress, Exception e)
		{
			CalendarQuery.CalendarViewTracer.TraceError<object, EmailAddress, Exception>((long)this.GetHashCode(), "{0}: Database connection failed for mailbox {1} with exception {2}. The cache entry is being removed.", TraceContext.Get(), emailAddress, e);
			Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_MailboxLogonFailed, null, new object[]
			{
				Globals.ProcessId,
				emailAddress,
				e
			});
			PerformanceCounters.IntraSiteCalendarFailuresPerSecond.Increment();
			return new FreeBusyQueryResult(new MailboxLogonFailedException(e));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0001BC10 File Offset: 0x00019E10
		private FreeBusyQueryResult HandleMailboxFailoverException(EmailAddress emailAddress, Exception e)
		{
			CalendarQuery.CalendarViewTracer.TraceError<object, EmailAddress, Exception>((long)this.GetHashCode(), "{0}: Database connection failed for mailbox {1} with exception {2}. The mailbox is in failover.", TraceContext.Get(), emailAddress, e);
			Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_CalendarQueryFailed, null, new object[]
			{
				Globals.ProcessId,
				emailAddress,
				e
			});
			PerformanceCounters.IntraSiteCalendarFailuresPerSecond.Increment();
			return new FreeBusyQueryResult(new MailboxFailoverException(e));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001BC7C File Offset: 0x00019E7C
		private FreeBusyQueryResult HandleException(EmailAddress emailAddress, AvailabilityException e)
		{
			CalendarQuery.CalendarViewTracer.TraceError<object, AvailabilityException>((long)this.GetHashCode(), "{0}: Exception getting Calendar Data: {1}", TraceContext.Get(), e);
			Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_CalendarQueryFailed, null, new object[]
			{
				Globals.ProcessId,
				emailAddress,
				e
			});
			PerformanceCounters.IntraSiteCalendarFailuresPerSecond.Increment();
			return new FreeBusyQueryResult(e);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		private FreeBusyQueryResult HandleException(EmailAddress emailAddress, Exception e, LocalizedString message)
		{
			CalendarQuery.CalendarViewTracer.TraceError<object, LocalizedString>((long)this.GetHashCode(), "{0}: {1}", TraceContext.Get(), message);
			Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_CalendarQueryFailed, null, new object[]
			{
				Globals.ProcessId,
				emailAddress,
				e
			});
			PerformanceCounters.IntraSiteCalendarFailuresPerSecond.Increment();
			LocalizedException ex = new LocalizedException(message, e);
			ErrorHandler.SetErrorCodeIfNecessary(ex, ErrorConstants.FreeBusyGenerationFailed);
			return new FreeBusyQueryResult(ex);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x0001BD58 File Offset: 0x00019F58
		private FreeBusyQueryResult InternalGetCalendarData(FreeBusyQuery freeBusyQuery, MailboxSession session)
		{
			FreeBusyViewType freeBusyViewType = FreeBusyViewType.None;
			CalendarEvent[] calendarEventArray = null;
			string mergedFreeBusy = null;
			WorkingHours workingHours = null;
			EmailAddress email = freeBusyQuery.Email;
			StoreObjectId associatedFolderId = freeBusyQuery.RecipientData.AssociatedFolderId;
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.Calendar);
			if (defaultFolderId == null)
			{
				return this.HandleException(email, new NoCalendarException());
			}
			if (associatedFolderId != null && !associatedFolderId.Equals(defaultFolderId))
			{
				return this.HandleException(email, new NotDefaultCalendarException());
			}
			session.ExTimeZone = this.clientContext.TimeZone;
			using (CalendarFolder calendarFolder = CalendarFolder.Bind(session, DefaultFolderType.Calendar, CalendarQuery.CalendarFolderQueryProps))
			{
				TimeSpan t = this.deadline - DateTime.UtcNow;
				if (t <= TimeSpan.Zero)
				{
					return this.HandleException(email, new TimeoutExpiredException("Determine-Allowed-Access"));
				}
				FreeBusyPermissionLevel freeBusyPermissionLevel = FreeBusyPermission.DetermineAllowedAccess(this.clientContext, session, calendarFolder, freeBusyQuery, this.defaultFreeBusyOnly);
				CalendarQuery.CalendarViewTracer.TraceDebug((long)this.GetHashCode(), "{0}: AccessCheck returned {1} for user {2} on mailbox {3}", new object[]
				{
					TraceContext.Get(),
					freeBusyPermissionLevel,
					this.clientContext,
					email
				});
				if (freeBusyPermissionLevel == FreeBusyPermissionLevel.None)
				{
					CalendarQuery.CalendarViewTracer.TraceDebug<object, EmailAddress>((long)this.GetHashCode(), "{0}: No mailbox data will be returned for mailbox {1} since the granted access level to caller is None.", TraceContext.Get(), email);
					return new FreeBusyQueryResult(new NoFreeBusyAccessException(44348U));
				}
				t = this.deadline - DateTime.UtcNow;
				if (t <= TimeSpan.Zero)
				{
					return this.HandleException(email, new TimeoutExpiredException("Get-Calendar-View"));
				}
				freeBusyViewType = CalendarQuery.GetReturnView(this.requestedFreeBusyView.RequestedView, freeBusyPermissionLevel);
				ExDateTime windowStart = new ExDateTime(this.clientContext.TimeZone, this.requestedFreeBusyView.TimeWindow.StartTime);
				ExDateTime windowEnd = new ExDateTime(this.clientContext.TimeZone, this.requestedFreeBusyView.TimeWindow.EndTime);
				try
				{
					calendarEventArray = InternalCalendarQuery.GetCalendarEvents(email, calendarFolder, windowStart, windowEnd, freeBusyViewType, freeBusyPermissionLevel == FreeBusyPermissionLevel.Owner, this.clientContext.RequestSchemaVersion);
				}
				catch (ResultSetTooBigException e)
				{
					return this.HandleException(email, e);
				}
				if (FreeBusyViewOptions.IsMerged(freeBusyViewType))
				{
					t = this.deadline - DateTime.UtcNow;
					if (t <= TimeSpan.Zero)
					{
						return this.HandleException(email, new TimeoutExpiredException("Generate-Merged-FreeBusy"));
					}
					int mergedFreeBusyIntervalInMinutes = this.requestedFreeBusyView.MergedFreeBusyIntervalInMinutes;
					mergedFreeBusy = MergedFreeBusy.GenerateMergedFreeBusyString(this.clientContext.TimeZone, mergedFreeBusyIntervalInMinutes, windowStart, windowEnd, calendarEventArray, false, null, this.clientContext.RequestSchemaVersion);
				}
				t = this.deadline - DateTime.UtcNow;
				if (t <= TimeSpan.Zero)
				{
					return this.HandleException(email, new TimeoutExpiredException("Getting-Work-Hours"));
				}
				workingHours = this.GetWorkingHours(email, session, calendarFolder);
			}
			return new FreeBusyQueryResult(freeBusyViewType, calendarEventArray, mergedFreeBusy, workingHours);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0001C06C File Offset: 0x0001A26C
		private WorkingHours GetWorkingHours(EmailAddress emailAddress, MailboxSession session, CalendarFolder calendarFolder)
		{
			WorkingHours workingHours = null;
			try
			{
				workingHours = WorkingHours.LoadFrom(session, calendarFolder.Id.ObjectId);
				if (workingHours == null)
				{
					CalendarQuery.WorkHoursTracer.TraceDebug<object, EmailAddress>((long)this.GetHashCode(), "{0}: Work hours not found for mailbox {1}.", TraceContext.Get(), emailAddress);
				}
			}
			catch (WorkingHoursXmlMalformedException ex)
			{
				CalendarQuery.WorkHoursTracer.TraceError<object, EmailAddress>((long)this.GetHashCode(), "{0}: Unable to get working hours from mailbox {1} because it seems corrupted.", TraceContext.Get(), emailAddress);
				Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_WorkingHoursFailed, null, new object[]
				{
					Globals.ProcessId,
					emailAddress,
					ex
				});
			}
			catch (LocalizedException arg)
			{
				CalendarQuery.WorkHoursTracer.TraceError<object, EmailAddress, LocalizedException>((long)this.GetHashCode(), "{0}: Failed to get working hours from mailbox {1}, Error: {2}.", TraceContext.Get(), emailAddress, arg);
			}
			return workingHours;
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x0001C134 File Offset: 0x0001A334
		private void TraceRequestStop(EmailAddress emailAddress, Guid serverRequestId)
		{
			if (ETWTrace.ShouldTraceCasStop(serverRequestId))
			{
				string spOperationData = string.Format(CultureInfo.InvariantCulture, "emailAddress: {0}", new object[]
				{
					emailAddress
				});
				Trace.TraceCasStop(CasTraceEventType.Availability, serverRequestId, 0, 0, Query<AvailabilityQueryResult>.GetCurrentHttpRequestServerName(), TraceContext.Get(), "CalendarQuery::GetCalendarData", spOperationData, string.Empty);
			}
		}

		// Token: 0x040003A2 RID: 930
		private const CasTraceEventType AvailabilityTraceEventType = CasTraceEventType.Availability;

		// Token: 0x040003A3 RID: 931
		private static readonly Trace CalendarViewTracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.Availability.ExTraceGlobals.CalendarViewTracer;

		// Token: 0x040003A4 RID: 932
		private static readonly Trace WorkHoursTracer = Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common.ExTraceGlobals.WorkingHoursTracer;

		// Token: 0x040003A5 RID: 933
		private static readonly PropertyDefinition[] CalendarFolderQueryProps = new PropertyDefinition[]
		{
			CalendarFolderSchema.FreeBusySecurityDescriptor
		};

		// Token: 0x040003A6 RID: 934
		private FreeBusyViewOptions requestedFreeBusyView;

		// Token: 0x040003A7 RID: 935
		private bool defaultFreeBusyOnly;
	}
}
