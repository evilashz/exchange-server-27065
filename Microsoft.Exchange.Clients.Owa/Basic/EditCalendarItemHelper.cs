using System;
using System.Collections;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000036 RID: 54
	internal static class EditCalendarItemHelper
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000C770 File Offset: 0x0000A970
		public static EditCalendarItemHelper.CalendarItemUpdateFlags UpdateCalendarItemValues(CalendarItemBase calendarItemBase, UserContext userContext, HttpRequest request, out string errorMessage)
		{
			errorMessage = null;
			EditCalendarItemHelper.CalendarItemUpdateFlags calendarItemUpdateFlags = EditCalendarItemHelper.CalendarItemUpdateFlags.None;
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			string formParameter = Utilities.GetFormParameter(request, "hidmr", false);
			bool flag = false;
			if (formParameter != null)
			{
				int num;
				if (int.TryParse(formParameter, out num))
				{
					flag = (num == 1);
				}
				if (calendarItemBase.IsMeeting != flag)
				{
					calendarItemBase.IsMeeting = flag;
					calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
				}
			}
			formParameter = Utilities.GetFormParameter(request, "txtsbj", false);
			if (formParameter != null && !string.Equals(formParameter, calendarItemBase.Subject))
			{
				try
				{
					calendarItemBase.Subject = formParameter;
				}
				catch (PropertyValidationException ex)
				{
					throw new OwaInvalidRequestException(ex.Message);
				}
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			formParameter = Utilities.GetFormParameter(request, "txtloc", false);
			if (formParameter != null && !string.Equals(calendarItemBase.Location, formParameter))
			{
				if (formParameter.Length > 255)
				{
					throw new OwaInvalidRequestException("Input Location string is too long.");
				}
				calendarItemBase.Location = formParameter;
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.LocationChanged;
			}
			bool flag2 = !flag || Utilities.GetFormParameter(request, "cbreqres", false) != null;
			if (CalendarItemBaseData.GetIsResponseRequested(calendarItemBase) != flag2)
			{
				calendarItemBase[ItemSchema.IsResponseRequested] = flag2;
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			bool flag3 = false;
			if (!string.IsNullOrEmpty(Utilities.GetFormParameter(request, "selSM", false)))
			{
				flag3 = (Utilities.GetFormParameter(request, "cballday", false) != null);
			}
			else
			{
				formParameter = Utilities.GetFormParameter(request, "hidade", false);
				int num2;
				if (formParameter != null && int.TryParse(formParameter, out num2))
				{
					flag3 = (num2 == 1);
				}
			}
			formParameter = Utilities.GetFormParameter(request, "selfb", false);
			if (formParameter != null)
			{
				BusyType busyType = (BusyType)int.Parse(formParameter);
				if (busyType != calendarItemBase.FreeBusyStatus)
				{
					calendarItemBase.FreeBusyStatus = busyType;
					calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
				}
			}
			Sensitivity sensitivity = (Utilities.GetFormParameter(request, "cbprivate", false) != null) ? Sensitivity.Private : Sensitivity.Normal;
			if ((calendarItemBase.CalendarItemType == CalendarItemType.Single || calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster) && calendarItemBase.Sensitivity != sensitivity)
			{
				calendarItemBase.Sensitivity = sensitivity;
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
			}
			calendarItemUpdateFlags |= EditCalendarItemHelper.UpdateImportance(calendarItemBase, request);
			formParameter = Utilities.GetFormParameter(request, "txtbdy", false);
			if (formParameter != null && EditCalendarItemHelper.BodyChanged(formParameter, calendarItemBase))
			{
				if (!string.IsNullOrEmpty(formParameter))
				{
					if (calendarItemBase.Body.Format == BodyFormat.TextHtml)
					{
						ItemUtility.SetItemBody(calendarItemBase, BodyFormat.TextHtml, formParameter);
						calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
					}
					else if (calendarItemBase.Body.Format == BodyFormat.TextPlain)
					{
						ItemUtility.SetItemBody(calendarItemBase, BodyFormat.TextPlain, formParameter);
						calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
					}
					else
					{
						if (calendarItemBase.Body.Format != BodyFormat.ApplicationRtf)
						{
							throw new ArgumentOutOfRangeException("calendarItemBase", "Unhandled body format type.");
						}
						ItemUtility.SetItemBody(calendarItemBase, BodyFormat.TextPlain, formParameter);
						calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
					}
				}
				else
				{
					ItemUtility.SetItemBody(calendarItemBase, BodyFormat.TextPlain, string.Empty);
					calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
				}
				bool flag4 = AttachmentUtility.PromoteInlineAttachments(calendarItemBase);
				if (flag4)
				{
					calendarItemBase.Load();
				}
			}
			bool flag5 = CalendarUtilities.CheckIsLocationGenerated(calendarItemBase);
			calendarItemUpdateFlags |= EditCalendarItemHelper.AddAttendees(calendarItemBase.AttendeeCollection, AttendeeType.Required, "txtto", userContext, request);
			calendarItemUpdateFlags |= EditCalendarItemHelper.AddAttendees(calendarItemBase.AttendeeCollection, AttendeeType.Optional, "txtcc", userContext, request);
			EditCalendarItemHelper.CalendarItemUpdateFlags calendarItemUpdateFlags2 = EditCalendarItemHelper.AddAttendees(calendarItemBase.AttendeeCollection, AttendeeType.Resource, "txtbcc", userContext, request);
			calendarItemUpdateFlags |= calendarItemUpdateFlags2;
			if ((flag5 || string.IsNullOrEmpty(calendarItemBase.Location)) && (calendarItemUpdateFlags2 & EditCalendarItemHelper.CalendarItemUpdateFlags.AttendeesChanged) != EditCalendarItemHelper.CalendarItemUpdateFlags.None && CalendarUtilities.GenerateAndSetLocation(calendarItemBase))
			{
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.LocationChanged;
			}
			Exception ex2 = null;
			try
			{
				if (flag3 != calendarItemBase.IsAllDayEvent)
				{
					calendarItemBase.IsAllDayEvent = flag3;
					calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.TimeChanged;
				}
				EditCalendarItemHelper.CalendarItemUpdateFlags calendarItemUpdateFlags3;
				if (EditCalendarItemHelper.ParseDateTimeRange(request, calendarItemBase, flag3, true, out errorMessage, out calendarItemUpdateFlags3, userContext))
				{
					calendarItemUpdateFlags |= calendarItemUpdateFlags3;
				}
			}
			catch (StoragePermanentException ex3)
			{
				ex2 = ex3;
			}
			catch (StorageTransientException ex4)
			{
				ex2 = ex4;
			}
			if (ex2 != null)
			{
				ErrorInformation exceptionHandlingInformation = Utilities.GetExceptionHandlingInformation(ex2, userContext.MailboxIdentity);
				errorMessage = exceptionHandlingInformation.Message;
			}
			return calendarItemUpdateFlags;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000CB04 File Offset: 0x0000AD04
		public static EditCalendarItemHelper.CalendarItemUpdateFlags UpdateImportance(CalendarItemBase calendarItemBase, HttpRequest request)
		{
			EditCalendarItemHelper.CalendarItemUpdateFlags calendarItemUpdateFlags = EditCalendarItemHelper.CalendarItemUpdateFlags.None;
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			string formParameter = Utilities.GetFormParameter(request, "hidmsgimp", false);
			if (!string.IsNullOrEmpty(formParameter))
			{
				int num;
				if (!int.TryParse(formParameter, out num))
				{
					throw new OwaInvalidRequestException("Invalid form parameter:importance");
				}
				Importance importance = (Importance)num;
				if (calendarItemBase.Importance != importance)
				{
					calendarItemBase.Importance = importance;
					calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.OtherChanged;
				}
			}
			return calendarItemUpdateFlags;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000CB74 File Offset: 0x0000AD74
		private static bool ParseDateTimeRange(HttpRequest request, CalendarItemBase calendarItem, bool isAllDayEvent, bool autoFixEndDate, out string errorMessage, out EditCalendarItemHelper.CalendarItemUpdateFlags updateFlags, UserContext userContext)
		{
			updateFlags = EditCalendarItemHelper.CalendarItemUpdateFlags.None;
			errorMessage = null;
			if (!string.IsNullOrEmpty(Utilities.GetFormParameter(request, "selSM", false)))
			{
				ExDateTime exDateTime = CalendarUtilities.ParseDateTimeFromForm(request, "selSY", "selSM", "selSD", isAllDayEvent ? null : "selST", userContext);
				ExDateTime exDateTime2 = CalendarUtilities.ParseDateTimeFromForm(request, "selEY", "selEM", "selED", isAllDayEvent ? null : "selET", userContext);
				if (isAllDayEvent)
				{
					exDateTime2 = exDateTime2.IncrementDays(1);
				}
				if (exDateTime > exDateTime2)
				{
					if (!autoFixEndDate)
					{
						errorMessage = LocalizedStrings.GetNonEncoded(107113300);
						return false;
					}
					exDateTime2 = exDateTime.AddMinutes(60.0);
				}
				if (!exDateTime.Equals(calendarItem.StartTime))
				{
					updateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.TimeChanged;
					calendarItem.StartTime = exDateTime;
				}
				if (!exDateTime2.Equals(calendarItem.EndTime))
				{
					updateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.TimeChanged;
					calendarItem.EndTime = exDateTime2;
				}
			}
			return true;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		public static EditCalendarItemHelper.CalendarItemUpdateFlags GetCalendarItem(UserContext userContext, StoreObjectId storeObjectId, StoreObjectId folderId, string changeKey, bool syncCalendarItem, out CalendarItemBase calendarItemBase)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			EditCalendarItemHelper.CalendarItemUpdateFlags result = EditCalendarItemHelper.CalendarItemUpdateFlags.None;
			CalendarItemBaseData calendarItemBaseData = EditCalendarItemHelper.GetUserContextData(userContext);
			if (calendarItemBaseData != null && !string.IsNullOrEmpty(calendarItemBaseData.ChangeKey) && string.IsNullOrEmpty(changeKey))
			{
				changeKey = calendarItemBaseData.ChangeKey;
			}
			calendarItemBase = null;
			if (storeObjectId != null)
			{
				if (!string.IsNullOrEmpty(changeKey))
				{
					calendarItemBase = Utilities.GetItem<CalendarItemBase>(userContext, storeObjectId, changeKey, new PropertyDefinition[0]);
				}
				else
				{
					calendarItemBase = Utilities.GetItem<CalendarItemBase>(userContext, storeObjectId, new PropertyDefinition[0]);
				}
			}
			if (calendarItemBase == null)
			{
				if (calendarItemBaseData != null && calendarItemBaseData.FolderId != null)
				{
					calendarItemBase = EditCalendarItemHelper.CreateDraft(userContext, calendarItemBaseData.FolderId);
				}
				else
				{
					calendarItemBase = EditCalendarItemHelper.CreateDraft(userContext, folderId);
				}
				if (calendarItemBaseData != null && calendarItemBaseData.IsMeeting != calendarItemBase.IsMeeting)
				{
					calendarItemBase.IsMeeting = calendarItemBaseData.IsMeeting;
				}
			}
			if (calendarItemBaseData != null && syncCalendarItem)
			{
				result = calendarItemBaseData.CopyTo(calendarItemBase);
			}
			else
			{
				if (calendarItemBaseData == null)
				{
					calendarItemBaseData = CalendarItemBaseData.Create(calendarItemBase);
				}
				EditCalendarItemHelper.SetUserContextData(userContext, calendarItemBaseData);
			}
			return result;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000CD45 File Offset: 0x0000AF45
		public static CalendarItemBaseData GetUserContextData(UserContext userContext)
		{
			return userContext.WorkingData as CalendarItemBaseData;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000CD52 File Offset: 0x0000AF52
		public static void SetUserContextData(UserContext userContext, CalendarItemBaseData calendarItemBaseData)
		{
			userContext.WorkingData = calendarItemBaseData;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000CD5B File Offset: 0x0000AF5B
		public static void ClearUserContextData(UserContext userContext)
		{
			userContext.WorkingData = null;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000CD64 File Offset: 0x0000AF64
		public static void CreateUserContextData(UserContext userContext, CalendarItemBase calendarItemBase)
		{
			userContext.WorkingData = CalendarItemBaseData.Create(calendarItemBase);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000CD74 File Offset: 0x0000AF74
		public static CalendarItem CreateDraft(UserContext userContext, StoreObjectId folderId)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			ExTraceGlobals.CalendarTracer.TraceDebug(0L, "Creating new calendar item.");
			CalendarItem calendarItem = null;
			calendarItem = CalendarItem.Create(userContext.MailboxSession, (folderId != null) ? folderId : userContext.CalendarFolderId);
			calendarItem[ItemSchema.ConversationIndexTracking] = true;
			if (calendarItem != null)
			{
				try
				{
					calendarItem[ItemSchema.IsResponseRequested] = true;
					calendarItem[ItemSchema.ReminderMinutesBeforeStart] = userContext.CalendarSettings.DefaultReminderTime;
				}
				catch
				{
					if (calendarItem != null)
					{
						calendarItem.Dispose();
						calendarItem = null;
					}
					throw;
				}
			}
			return calendarItem;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000CE1C File Offset: 0x0000B01C
		private static EditCalendarItemHelper.CalendarItemUpdateFlags AddAttendees(IAttendeeCollection attendees, AttendeeType attendeeType, string wellName, UserContext userContext, HttpRequest request)
		{
			EditCalendarItemHelper.CalendarItemUpdateFlags calendarItemUpdateFlags = EditCalendarItemHelper.CalendarItemUpdateFlags.None;
			bool flag = false;
			Participant participant = null;
			string formParameter = Utilities.GetFormParameter(request, wellName, false);
			if (string.IsNullOrEmpty(formParameter))
			{
				return calendarItemUpdateFlags;
			}
			ArrayList arrayList = new ArrayList();
			RecipientWell.ResolveRecipients(formParameter, arrayList, userContext, userContext.UserOptions.CheckNameInContactsFirst);
			for (int i = 0; i < arrayList.Count; i++)
			{
				RecipientWellNode recipientWellNode = (RecipientWellNode)arrayList[i];
				flag |= Utilities.CreateExchangeParticipant(out participant, recipientWellNode.DisplayName, recipientWellNode.RoutingAddress, recipientWellNode.RoutingType, recipientWellNode.AddressOrigin, recipientWellNode.StoreObjectId, recipientWellNode.EmailAddressIndex);
				if (participant != null)
				{
					attendees.Add(participant, attendeeType, null, null, false);
					calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.AttendeesChanged;
				}
			}
			if (flag)
			{
				calendarItemUpdateFlags |= EditCalendarItemHelper.CalendarItemUpdateFlags.HasUnresolvedAttendees;
			}
			return calendarItemUpdateFlags;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public static void SetCalendarItemFromQueryParams(CalendarItemBase calendarItemBase, HttpRequest request, UserContext userContext)
		{
			bool isMeeting = Utilities.GetQueryStringParameter(request, "mr", false) != null;
			ExDateTime exDateTime = ExDateTime.MinValue;
			ExDateTime endTime = ExDateTime.MinValue;
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			int num = RequestParser.TryGetIntValueFromQueryString(request, "yr", -1);
			int num2 = RequestParser.TryGetIntValueFromQueryString(request, "mn", -1);
			int num3 = RequestParser.TryGetIntValueFromQueryString(request, "dy", -1);
			if (num != -1 && num2 != -1 && num3 != -1)
			{
				exDateTime = new ExDateTime(userContext.TimeZone, num, num2, num3, localTime.Hour, localTime.Minute, 0);
			}
			if (exDateTime == ExDateTime.MinValue)
			{
				exDateTime = new ExDateTime(userContext.TimeZone, localTime.Year, localTime.Month, localTime.Day, localTime.Hour, localTime.Minute, 0);
			}
			if (exDateTime.Minute != 0 && exDateTime.Minute != 30)
			{
				exDateTime = exDateTime.AddMinutes((double)(30 - exDateTime.Minute % 30));
			}
			endTime = exDateTime.AddMinutes(60.0);
			calendarItemBase.StartTime = exDateTime;
			calendarItemBase.EndTime = endTime;
			calendarItemBase.IsMeeting = isMeeting;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000D008 File Offset: 0x0000B208
		internal static bool CancelCalendarItem(UserContext userContext, CalendarItemBase calendarItemBase, EditCalendarItemHelper.CancelRangeType cancelRange, ExDateTime cancelFromDateTime, out InfobarMessage infobarMessage)
		{
			bool result = true;
			infobarMessage = null;
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			try
			{
				if (calendarItem == null)
				{
					StoreObjectId masterStoreObjectId = CalendarUtilities.GetMasterStoreObjectId(calendarItemBase);
					calendarItem = Utilities.GetItem<CalendarItem>(userContext, masterStoreObjectId, new PropertyDefinition[0]);
					if (calendarItem != null)
					{
						calendarItem.OpenAsReadWrite();
					}
				}
				if (cancelRange == EditCalendarItemHelper.CancelRangeType.Occurrence || cancelRange == EditCalendarItemHelper.CancelRangeType.None)
				{
					EditCalendarItemHelper.CancelCalendarItem(userContext, calendarItemBase);
				}
				else if (cancelRange == EditCalendarItemHelper.CancelRangeType.All)
				{
					EditCalendarItemHelper.CancelCalendarItem(userContext, calendarItem);
				}
				else if (cancelRange == EditCalendarItemHelper.CancelRangeType.FromDate)
				{
					int cancelRecurrenceCount = CalendarItemUtilities.GetCancelRecurrenceCount(calendarItem, cancelFromDateTime);
					if (cancelRecurrenceCount == 0)
					{
						EditCalendarItemHelper.CancelCalendarItem(userContext, calendarItem);
					}
					else if (cancelRecurrenceCount > 0)
					{
						calendarItem.Recurrence = new Recurrence(calendarItem.Recurrence.Pattern, new EndDateRecurrenceRange(calendarItem.Recurrence.Range.StartDate, cancelFromDateTime.IncrementDays(-1)));
						if (calendarItem.IsMeeting && calendarItem.MeetingRequestWasSent)
						{
							result = CalendarUtilities.SendMeetingMessages(userContext, calendarItem, true, out infobarMessage);
						}
						else
						{
							result = CalendarUtilities.SaveCalendarItem(calendarItem, userContext, out infobarMessage);
						}
					}
				}
			}
			finally
			{
				if (calendarItem != null && calendarItem != calendarItemBase)
				{
					calendarItem.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		public static void CancelCalendarItem(UserContext userContext, CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase.IsMeeting && calendarItemBase.MeetingRequestWasSent && calendarItemBase.AttendeeCollection.Count != 0)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug(0L, "Sending calendarItem cancellation");
				Utilities.ValidateCalendarItemBaseStoreObject(calendarItemBase);
				using (MeetingCancellation meetingCancellation = calendarItemBase.CancelMeeting(null, null))
				{
					meetingCancellation.Send();
				}
			}
			ExTraceGlobals.CalendarTracer.TraceDebug(0L, "Deleting calendarItem due to cancellation");
			Utilities.DeleteItems(userContext, DeleteItemFlags.MoveToDeletedItems, new StoreId[]
			{
				calendarItemBase.Id
			});
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000D19C File Offset: 0x0000B39C
		public static StoreObjectId GetCalendarFolderId(HttpRequest request, UserContext userContext)
		{
			StoreObjectId storeObjectId = null;
			string queryStringParameter = Utilities.GetQueryStringParameter(request, "fId", false);
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				storeObjectId = Utilities.CreateStoreObjectId(userContext.MailboxSession, queryStringParameter);
			}
			else
			{
				CalendarModuleViewState calendarModuleViewState = userContext.LastClientViewState as CalendarModuleViewState;
				if (calendarModuleViewState != null)
				{
					storeObjectId = calendarModuleViewState.FolderId;
				}
			}
			if (storeObjectId == null)
			{
				storeObjectId = userContext.CalendarFolderId;
			}
			return storeObjectId;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		internal static InfobarMessage BuildCancellationPrompt(CalendarItemBase calendarItemBase, UserContext userContext)
		{
			bool flag = calendarItemBase.CalendarItemType != CalendarItemType.Single;
			bool flag2 = calendarItemBase.IsMeeting && calendarItemBase.MeetingRequestWasSent;
			if (!flag && !flag2)
			{
				throw new ArgumentException("The input calendarItemBase doesn't need a cancellation prompt.");
			}
			SanitizedHtmlString bodyHtml = null;
			SanitizedHtmlString messageHtml;
			if (flag)
			{
				if (calendarItemBase.IsMeeting)
				{
					messageHtml = SanitizedHtmlString.FromStringId(-2063563644);
				}
				else
				{
					messageHtml = SanitizedHtmlString.FromStringId(78467316);
				}
				CalendarModuleViewState calendarModuleViewState = userContext.LastClientViewState as CalendarModuleViewState;
				ExDateTime cancelFromDate;
				if (calendarModuleViewState != null)
				{
					cancelFromDate = calendarModuleViewState.DateTime;
				}
				else
				{
					cancelFromDate = DateTimeUtilities.GetLocalTime();
				}
				bodyHtml = EditCalendarItemHelper.BuildCancelRecurringTable(calendarItemBase.CalendarItemType, cancelFromDate);
			}
			else
			{
				messageHtml = SanitizedHtmlString.FromStringId(-240976491);
			}
			SanitizedHtmlString footerHtml;
			if (!calendarItemBase.IsMeeting)
			{
				footerHtml = SanitizedHtmlString.FromStringId(2058056132);
			}
			else if (!calendarItemBase.MeetingRequestWasSent)
			{
				footerHtml = SanitizedHtmlString.FromStringId(2133761911);
			}
			else
			{
				footerHtml = SanitizedHtmlString.FromStringId(-1210628592);
			}
			return InfobarMessage.CreatePromptHtml(messageHtml, bodyHtml, footerHtml);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		private static SanitizedHtmlString BuildCancelRecurringTable(CalendarItemType type, ExDateTime cancelFromDate)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>("<table class=\"caltm\" cellpadding=\"0\" cellspacing=\"0\">");
			if (type == CalendarItemType.Single)
			{
				throw new ArgumentException("Unhandled calendar item type.");
			}
			bool flag = true;
			if (type == CalendarItemType.Exception || type == CalendarItemType.Occurrence)
			{
				sanitizingStringBuilder.Append("<tr><td class=\"rb\"><input class=\"rb\" type=\"radio\" name=\"delprompt\" checked=\"checked\" id=\"delocc\" value=\"0\" />\n<label class=\"lb\" for=\"delocc\">");
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-673339501));
				sanitizingStringBuilder.Append("</label></td></tr>");
				flag = false;
			}
			sanitizingStringBuilder.Append("<tr><td><input class=\"rb\" type=\"radio\" name=\"delprompt\" ");
			sanitizingStringBuilder.Append(flag ? "checked " : string.Empty);
			sanitizingStringBuilder.Append("id=\"delfwd\" value=\"1\" />\n<label class=\"lb\" for=\"delfwd\">");
			sanitizingStringBuilder.Append(string.Format(LocalizedStrings.GetNonEncoded(-1062918861), string.Empty));
			sanitizingStringBuilder.Append("</label></td>");
			using (SanitizingStringWriter<OwaHtml> sanitizingStringWriter = new SanitizingStringWriter<OwaHtml>())
			{
				CalendarUtilities.RenderDateTimeTable(sanitizingStringWriter, "seldel", cancelFromDate, DateTimeUtilities.GetLocalTime().Year, null, string.Empty, "onSelDelTm(this);", null);
				sanitizingStringBuilder.Append("<td>");
				sanitizingStringBuilder.Append<SanitizedHtmlString>(sanitizingStringWriter.ToSanitizedString<SanitizedHtmlString>());
				sanitizingStringBuilder.Append("</td></tr>");
			}
			sanitizingStringBuilder.Append("<tr><td><input class=\"rb\" type=\"radio\" name=\"delprompt\" id=\"delall\" value=\"2\" />\n<label class=\"lb\" for=\"delall\">");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1631668395));
			sanitizingStringBuilder.Append("</label></td></tr></table>");
			return sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000D418 File Offset: 0x0000B618
		public static bool IsSendUpdateRequired(CalendarItemBase calendarItemBase, UserContext userContext)
		{
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			if (calendarItemBase.AttendeesChanged)
			{
				return true;
			}
			CalendarItemBase item = Utilities.GetItem<CalendarItemBase>(userContext, calendarItemBase.Id.ObjectId, new PropertyDefinition[0]);
			using (item)
			{
				if (!string.Equals(calendarItemBase.Location, item.Location))
				{
					return true;
				}
				if (calendarItemBase.StartTime != item.StartTime || calendarItemBase.EndTime != item.EndTime)
				{
					return true;
				}
				CalendarItem calendarItem = calendarItemBase as CalendarItem;
				CalendarItem calendarItem2 = item as CalendarItem;
				if (calendarItem != null && calendarItem2 != null)
				{
					if (calendarItem.Recurrence == null != (calendarItem2.Recurrence == null))
					{
						return true;
					}
					if (calendarItem.Recurrence != null && calendarItem2.Recurrence != null && !calendarItem.Recurrence.Equals(calendarItem2.Recurrence))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000D520 File Offset: 0x0000B720
		public static bool BodyChanged(string newBodyText, CalendarItemBase origionalCalendarItemBase)
		{
			int num = string.IsNullOrEmpty(newBodyText) ? 0 : newBodyText.Length;
			string partialTextBody = origionalCalendarItemBase.Body.GetPartialTextBody(num + 1);
			return !CalendarUtilities.StringsEqualNullEmpty(newBodyText, partialTextBody, StringComparison.CurrentCulture);
		}

		// Token: 0x040000F2 RID: 242
		public const string SubjectElementName = "txtsbj";

		// Token: 0x040000F3 RID: 243
		public const string LocationElementName = "txtloc";

		// Token: 0x040000F4 RID: 244
		public const string ImportanceElementName = "hidmsgimp";

		// Token: 0x040000F5 RID: 245
		public const string IsMeetingElementName = "hidmr";

		// Token: 0x040000F6 RID: 246
		public const string IsAllDayEventElementName = "hidade";

		// Token: 0x040000F7 RID: 247
		public const string TabElementName = "hidtab";

		// Token: 0x040000F8 RID: 248
		public const string RequiredRecipientsElementName = "txtto";

		// Token: 0x040000F9 RID: 249
		public const string OptionalRecipientsElementName = "txtcc";

		// Token: 0x040000FA RID: 250
		public const string ResourceRecipientsElementName = "txtbcc";

		// Token: 0x040000FB RID: 251
		public const string BodyElementName = "txtbdy";

		// Token: 0x040000FC RID: 252
		public const string RequestResponseElementName = "cbreqres";

		// Token: 0x040000FD RID: 253
		public const string IsPrivateElementName = "cbprivate";

		// Token: 0x040000FE RID: 254
		public const string AllDayEventElementName = "cballday";

		// Token: 0x040000FF RID: 255
		public const string ShowTimeAsElementName = "selfb";

		// Token: 0x04000100 RID: 256
		private const string StartYearElementName = "selSY";

		// Token: 0x04000101 RID: 257
		private const string StartMonthElementName = "selSM";

		// Token: 0x04000102 RID: 258
		private const string StartDayElementName = "selSD";

		// Token: 0x04000103 RID: 259
		private const string StartTimeElementName = "selST";

		// Token: 0x04000104 RID: 260
		private const string EndYearElementName = "selEY";

		// Token: 0x04000105 RID: 261
		private const string EndMonthElementName = "selEM";

		// Token: 0x04000106 RID: 262
		private const string EndDayElementName = "selED";

		// Token: 0x04000107 RID: 263
		private const string EndTimeElementName = "selET";

		// Token: 0x04000108 RID: 264
		public const string FolderIdQueryParameter = "fId";

		// Token: 0x04000109 RID: 265
		public const string MeetingRequestQueryParameter = "mr";

		// Token: 0x0400010A RID: 266
		public const string YearQueryParameter = "yr";

		// Token: 0x0400010B RID: 267
		public const string MonthQueryParameter = "mn";

		// Token: 0x0400010C RID: 268
		public const string DayQueryParameter = "dy";

		// Token: 0x0400010D RID: 269
		public const string DoSendConfirmQueryParameter = "sndpt";

		// Token: 0x0400010E RID: 270
		public const string ForceSendQueryParameter = "fsnd";

		// Token: 0x0400010F RID: 271
		public const string ActionName = "pfaan";

		// Token: 0x02000037 RID: 55
		[Flags]
		public enum CalendarItemUpdateFlags
		{
			// Token: 0x04000111 RID: 273
			None = 0,
			// Token: 0x04000112 RID: 274
			LocationChanged = 1,
			// Token: 0x04000113 RID: 275
			TimeChanged = 2,
			// Token: 0x04000114 RID: 276
			AttendeesChanged = 4,
			// Token: 0x04000115 RID: 277
			OtherChanged = 8,
			// Token: 0x04000116 RID: 278
			HasUnresolvedAttendees = 16
		}

		// Token: 0x02000038 RID: 56
		public enum CancelRangeType
		{
			// Token: 0x04000118 RID: 280
			None,
			// Token: 0x04000119 RID: 281
			Occurrence,
			// Token: 0x0400011A RID: 282
			FromDate,
			// Token: 0x0400011B RID: 283
			All
		}
	}
}
