using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Availability;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x0200095E RID: 2398
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetUserAvailabilityInternal
	{
		// Token: 0x0600450A RID: 17674 RVA: 0x000F047A File Offset: 0x000EE67A
		public GetUserAvailabilityInternal(MailboxSession mailboxSession, GetUserAvailabilityRequest request, GetUserAvailabilityResponse response)
		{
			this.request = request;
			this.response = response;
			this.mailboxSession = mailboxSession;
			OwsLogRegistry.Register(GetUserAvailabilityInternal.GetUserAvailabilityActionName, typeof(AvailabilityServiceMetadata), new Type[0]);
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x000F04B4 File Offset: 0x000EE6B4
		public GetUserAvailabilityInternalJsonResponse Execute()
		{
			GetUserAvailabilityInternalResponse getUserAvailabilityInternalResponse = new GetUserAvailabilityInternalResponse();
			getUserAvailabilityInternalResponse.Responses = new UserAvailabilityInternalResponse[this.response.FreeBusyResponseArray.Length];
			for (int i = 0; i < this.response.FreeBusyResponseArray.Length; i++)
			{
				FreeBusyResponse freeBusyResponse = this.response.FreeBusyResponseArray[i];
				UserAvailabilityInternalResponse userAvailabilityInternalResponse = new UserAvailabilityInternalResponse();
				userAvailabilityInternalResponse.ResponseMessage = freeBusyResponse.ResponseMessage;
				getUserAvailabilityInternalResponse.Responses[i] = userAvailabilityInternalResponse;
				string smtpAddressFromMailboxData = GetUserAvailabilityInternal.GetSmtpAddressFromMailboxData(this.request.MailboxDataArray[i]);
				if (userAvailabilityInternalResponse.ResponseMessage.ResponseClass != ResponseClass.Success)
				{
					getUserAvailabilityInternalResponse.Responses[i].CalendarView = new UserAvailabilityCalendarView();
					getUserAvailabilityInternalResponse.Responses[i].CalendarView.FreeBusyViewType = FreeBusyViewType.None.ToString();
					ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug<MailboxData, string>((long)this.GetHashCode(), "Failed to get freebusy response for user:{0}. Message:{1}", this.request.MailboxDataArray[i], userAvailabilityInternalResponse.ResponseMessage.MessageText);
				}
				else
				{
					FolderId parentFolderId = new FolderId(smtpAddressFromMailboxData, string.Empty);
					if (freeBusyResponse.FreeBusyView.MergedFreeBusy != null)
					{
						freeBusyResponse.FreeBusyView.MergedFreeBusy = this.UpdateMergedFreeBusy(freeBusyResponse.FreeBusyView.MergedFreeBusy);
					}
					userAvailabilityInternalResponse.CalendarView = this.ConvertFreeBusyView(freeBusyResponse.FreeBusyView, parentFolderId);
				}
			}
			return new GetUserAvailabilityInternalJsonResponse
			{
				Body = getUserAvailabilityInternalResponse
			};
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x000F060A File Offset: 0x000EE80A
		private static string GetSmtpAddressFromMailboxData(MailboxData mailboxData)
		{
			if (mailboxData == null || mailboxData.Email == null || mailboxData.Email.Address == null || !SmtpAddress.IsValidSmtpAddress(mailboxData.Email.Address))
			{
				return null;
			}
			return mailboxData.Email.Address;
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x000F0644 File Offset: 0x000EE844
		private string UpdateMergedFreeBusy(string mergedFreeBusy)
		{
			string result = mergedFreeBusy;
			if (mergedFreeBusy == null)
			{
				ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug((long)this.GetHashCode(), "No MergeFreeBusy data was returned.");
				return null;
			}
			this.SetFreeBusyDayLightBasedValue(new ExDateTime(EWSSettings.RequestTimeZone, this.request.FreeBusyViewOptions.TimeWindow.StartTime), new ExDateTime(EWSSettings.RequestTimeZone, this.request.FreeBusyViewOptions.TimeWindow.EndTime), EWSSettings.RequestTimeZone, this.request.FreeBusyViewOptions.MergedFreeBusyIntervalInMinutes, ref result);
			return result;
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x000F06CC File Offset: 0x000EE8CC
		private UserAvailabilityCalendarView ConvertFreeBusyView(FreeBusyView freeBusyView, FolderId parentFolderId)
		{
			UserAvailabilityCalendarView userAvailabilityCalendarView = new UserAvailabilityCalendarView();
			userAvailabilityCalendarView.WorkingHours = this.ConvertWorkingHours(freeBusyView.WorkingHours);
			userAvailabilityCalendarView.MergedFreeBusy = freeBusyView.MergedFreeBusy;
			userAvailabilityCalendarView.FreeBusyViewType = freeBusyView.FreeBusyViewTypeString;
			if (freeBusyView.CalendarEventArray == null)
			{
				userAvailabilityCalendarView.Items = new EwsCalendarItemType[0];
			}
			else
			{
				userAvailabilityCalendarView.Items = this.ConvertCalendarEvents(freeBusyView.CalendarEventArray, parentFolderId);
			}
			return userAvailabilityCalendarView;
		}

		// Token: 0x0600450F RID: 17679 RVA: 0x000F0734 File Offset: 0x000EE934
		private WorkingHoursType ConvertWorkingHours(WorkingHours freeBusyWorkingHours)
		{
			if (freeBusyWorkingHours == null)
			{
				return new WorkingHoursType(0, 0, 0, this.mailboxSession.ExTimeZone, this.mailboxSession.ExTimeZone);
			}
			return new WorkingHoursType(freeBusyWorkingHours.StartTimeInMinutes, freeBusyWorkingHours.EndTimeInMinutes, (int)freeBusyWorkingHours.DaysOfWeek, this.mailboxSession.ExTimeZone, freeBusyWorkingHours.ExTimeZone);
		}

		// Token: 0x06004510 RID: 17680 RVA: 0x000F078C File Offset: 0x000EE98C
		private EwsCalendarItemType[] ConvertCalendarEvents(CalendarEvent[] calendarEvents, FolderId parentFolderId)
		{
			EwsCalendarItemType[] array = new EwsCalendarItemType[calendarEvents.Length];
			for (int i = 0; i < calendarEvents.Length; i++)
			{
				CalendarEvent calendarEvent = calendarEvents[i];
				EwsCalendarItemType ewsCalendarItemType = array[i] = new EwsCalendarItemType();
				ewsCalendarItemType.LegacyFreeBusyStatus = calendarEvent.BusyType;
				ewsCalendarItemType.Start = calendarEvent.StartTimeString;
				ewsCalendarItemType.End = calendarEvent.EndTimeString;
				ewsCalendarItemType.ParentFolderId = parentFolderId;
				ewsCalendarItemType.EffectiveRights = new EffectiveRightsType();
				if (calendarEvent.GlobalObjectId != null)
				{
					ewsCalendarItemType.UID = new GlobalObjectId(calendarEvent.GlobalObjectId).Uid;
				}
				ewsCalendarItemType.ItemId = new Microsoft.Exchange.Services.Core.Types.ItemId(Guid.NewGuid().ToString(), string.Empty);
				if (calendarEvent.CalendarEventDetails == null)
				{
					ewsCalendarItemType.Subject = this.ConvertFreeBusyStatusToSubject(calendarEvent.BusyType);
				}
				else
				{
					CalendarEventDetails calendarEventDetails = calendarEvent.CalendarEventDetails;
					if (calendarEventDetails.IsPrivate)
					{
						ewsCalendarItemType.Sensitivity = SensitivityType.Private;
						if (!ewsCalendarItemType.EffectiveRights.Read)
						{
							ewsCalendarItemType.Subject = ClientStrings.PrivateAppointmentSubject.ToString(this.mailboxSession.Culture);
							goto IL_1E1;
						}
					}
					else
					{
						ewsCalendarItemType.Sensitivity = SensitivityType.Normal;
					}
					ewsCalendarItemType.Subject = calendarEventDetails.Subject;
					ewsCalendarItemType.EnhancedLocation = new EnhancedLocationType
					{
						DisplayName = calendarEventDetails.Location
					};
					ewsCalendarItemType.IsMeeting = new bool?(calendarEventDetails.IsMeeting);
					ewsCalendarItemType.ReminderIsSet = new bool?(calendarEventDetails.IsReminderSet);
					ewsCalendarItemType.IsAllDayEvent = new bool?(calendarEvent.StartTime.TimeOfDay.TotalSeconds == 0.0 && calendarEvent.EndTime.TimeOfDay.TotalSeconds == 0.0 && calendarEvent.StartTime < calendarEvent.EndTime);
					if (calendarEventDetails.IsRecurring)
					{
						ewsCalendarItemType.CalendarItemType = (calendarEventDetails.IsException ? CalendarItemTypeType.Exception : CalendarItemTypeType.Occurrence);
					}
					else
					{
						ewsCalendarItemType.CalendarItemType = CalendarItemTypeType.Single;
					}
				}
				IL_1E1:;
			}
			return array;
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x000F0988 File Offset: 0x000EEB88
		private string ConvertFreeBusyStatusToSubject(Microsoft.Exchange.InfoWorker.Common.Availability.BusyType busyType)
		{
			switch (busyType)
			{
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Free:
				return ClientStrings.Free.ToString(this.mailboxSession.Culture);
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Tentative:
				return ClientStrings.Tentative.ToString(this.mailboxSession.Culture);
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.Busy:
				return ClientStrings.Busy.ToString(this.mailboxSession.Culture);
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.OOF:
				return ClientStrings.OOF.ToString(this.mailboxSession.Culture);
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.WorkingElsewhere:
				return ClientStrings.WorkingElsewhere.ToString(this.mailboxSession.Culture);
			case Microsoft.Exchange.InfoWorker.Common.Availability.BusyType.NoData:
				return ClientStrings.NoDataAvailable.ToString(this.mailboxSession.Culture);
			default:
				ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug((long)this.GetHashCode(), "Unable to convert FreeBusy status to String. Returning empty string.");
				return string.Empty;
			}
		}

		// Token: 0x06004512 RID: 17682 RVA: 0x000F0A70 File Offset: 0x000EEC70
		private void SetFreeBusyDayLightBasedValue(ExDateTime startDate, ExDateTime endDate, ExTimeZone timeZone, int intervalInMinutes, ref string freeBusyData)
		{
			if (string.IsNullOrEmpty(freeBusyData))
			{
				throw new ArgumentNullException("freeBusyData", "FreeBusyData cannot be null or Empty");
			}
			ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug<string>((long)this.GetHashCode(), "GetUserAvailabilityInternal.SetFreeBusyDayLightBasedValue - Original MergedFreeBusy: {0}", freeBusyData);
			GetUserAvailabilityInternal.DayLightsTransition startToEndDayLightsTransitionValue = this.GetStartToEndDayLightsTransitionValue(startDate, endDate, timeZone);
			if (startToEndDayLightsTransitionValue == GetUserAvailabilityInternal.DayLightsTransition.NoTransition)
			{
				return;
			}
			ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug<GetUserAvailabilityInternal.DayLightsTransition>((long)this.GetHashCode(), "SetFreeBusyDayLightBasedValue - TransitionType: {0}.", startToEndDayLightsTransitionValue);
			int num;
			int num2;
			this.SetDayLightsSavingIndices(startDate, timeZone, startToEndDayLightsTransitionValue, intervalInMinutes, out num, out num2);
			if (startToEndDayLightsTransitionValue == GetUserAvailabilityInternal.DayLightsTransition.TransitionFromStandardToDayLights)
			{
				StringBuilder stringBuilder = new StringBuilder(freeBusyData.Length);
				stringBuilder.Append(freeBusyData.Substring(0, num));
				for (int i = 0; i < num2; i++)
				{
					stringBuilder.Append('0');
				}
				stringBuilder.Append(freeBusyData.Substring(num));
				freeBusyData = stringBuilder.ToString();
			}
			else if (startToEndDayLightsTransitionValue == GetUserAvailabilityInternal.DayLightsTransition.TransitionFromDayLightsToStandard)
			{
				StringBuilder stringBuilder2 = new StringBuilder(freeBusyData.Length);
				stringBuilder2.Append(freeBusyData.Substring(0, num));
				stringBuilder2.Append(freeBusyData.Substring(num + num2));
				freeBusyData = stringBuilder2.ToString();
			}
			ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug<string>((long)this.GetHashCode(), "SetFreeBusyDayLightBasedValue - New MergedFreeBusy: {0}.", freeBusyData);
		}

		// Token: 0x06004513 RID: 17683 RVA: 0x000F0B98 File Offset: 0x000EED98
		private void SetDayLightsSavingIndices(ExDateTime startDateFreeBusyWindow, ExTimeZone timeZone, GetUserAvailabilityInternal.DayLightsTransition transition, int intervalInMinutes, out int transitionIndex, out int dayLightIndexSpan)
		{
			ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug((long)this.GetHashCode(), "GetUserAvailabilityInternal.SetDayLightsSavingIndices");
			transitionIndex = 0;
			dayLightIndexSpan = 0;
			if (transition != GetUserAvailabilityInternal.DayLightsTransition.NoTransition)
			{
				DaylightTime daylightChanges = timeZone.GetDaylightChanges(startDateFreeBusyWindow.Year);
				TimeSpan timeSpan;
				if ((daylightChanges.Start < daylightChanges.End && startDateFreeBusyWindow > (ExDateTime)daylightChanges.Start) || (daylightChanges.Start > daylightChanges.End && startDateFreeBusyWindow < (ExDateTime)daylightChanges.End))
				{
					timeSpan = (ExDateTime)daylightChanges.End - startDateFreeBusyWindow;
				}
				else
				{
					timeSpan = (ExDateTime)daylightChanges.Start - startDateFreeBusyWindow;
				}
				transitionIndex = (timeSpan.Days * 60 * 24 + timeSpan.Hours * 60 + timeSpan.Minutes) / intervalInMinutes;
				dayLightIndexSpan = (daylightChanges.Delta.Hours * 60 + daylightChanges.Delta.Minutes) / intervalInMinutes;
			}
		}

		// Token: 0x06004514 RID: 17684 RVA: 0x000F0C98 File Offset: 0x000EEE98
		private GetUserAvailabilityInternal.DayLightsTransition GetStartToEndDayLightsTransitionValue(ExDateTime startDateFreeBusyWindow, ExDateTime endDateFreeBusyWindow, ExTimeZone timeZone)
		{
			ExTraceGlobals.GetUserAvailabilityInternalCallTracer.TraceDebug((long)this.GetHashCode(), "GetUserAvailabilityInternal.GetTransitionValue");
			bool flag = timeZone.IsDaylightSavingTime(startDateFreeBusyWindow);
			bool flag2 = timeZone.IsDaylightSavingTime(endDateFreeBusyWindow);
			if ((flag && flag2) || (!flag && !flag2))
			{
				return GetUserAvailabilityInternal.DayLightsTransition.NoTransition;
			}
			if (flag && !flag2)
			{
				return GetUserAvailabilityInternal.DayLightsTransition.TransitionFromDayLightsToStandard;
			}
			if (!flag && flag2)
			{
				return GetUserAvailabilityInternal.DayLightsTransition.TransitionFromStandardToDayLights;
			}
			return GetUserAvailabilityInternal.DayLightsTransition.NoTransition;
		}

		// Token: 0x04002829 RID: 10281
		private static readonly string GetUserAvailabilityActionName = typeof(GetUserAvailabilityInternal).Name;

		// Token: 0x0400282A RID: 10282
		private GetUserAvailabilityRequest request;

		// Token: 0x0400282B RID: 10283
		private GetUserAvailabilityResponse response;

		// Token: 0x0400282C RID: 10284
		private MailboxSession mailboxSession;

		// Token: 0x0200095F RID: 2399
		private enum DayLightsTransition
		{
			// Token: 0x0400282E RID: 10286
			NoTransition,
			// Token: 0x0400282F RID: 10287
			TransitionFromStandardToDayLights,
			// Token: 0x04002830 RID: 10288
			TransitionFromDayLightsToStandard
		}
	}
}
