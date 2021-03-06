using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200015F RID: 351
	internal static class MeetingUtilities
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x000538B4 File Offset: 0x00051AB4
		public static void DeleteMeetingMessageCalendarItem(StoreObjectId meetingMessageId)
		{
			if (meetingMessageId == null)
			{
				throw new ArgumentNullException("meetingRequestId");
			}
			MeetingMessage meetingMessage = null;
			CalendarItemBase calendarItemBase = null;
			UserContext userContext = UserContextManager.GetUserContext();
			try
			{
				try
				{
					meetingMessage = (MeetingMessage)Item.Bind(userContext.MailboxSession, meetingMessageId, ItemBindOption.LoadRequiredPropertiesOnly);
				}
				catch (ObjectNotFoundException)
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug(0L, "Meeting request could not be found.");
					throw;
				}
				calendarItemBase = MeetingUtilities.TryGetCorrelatedItem(meetingMessage);
				if (calendarItemBase != null)
				{
					if (!calendarItemBase.IsOrganizer())
					{
						MeetingRequest meetingRequest = meetingMessage as MeetingRequest;
						if (meetingRequest != null)
						{
							if (meetingRequest.MeetingRequestType == MeetingMessageType.InformationalUpdate || meetingRequest.MeetingRequestType == MeetingMessageType.Outdated || calendarItemBase.ResponseType == ResponseType.Accept || calendarItemBase.ResponseType == ResponseType.Tentative)
							{
								return;
							}
						}
						else
						{
							if (!(meetingMessage is MeetingCancellation))
							{
								throw new OwaInvalidRequestException("Meeting message must be either a meeting invite or meeting cancellation");
							}
							if (meetingMessage.IsOutOfDate(calendarItemBase))
							{
								return;
							}
						}
						calendarItemBase.DeleteMeeting(DeleteItemFlags.MoveToDeletedItems);
					}
				}
			}
			finally
			{
				if (meetingMessage != null)
				{
					meetingMessage.Dispose();
					meetingMessage = null;
				}
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000539B0 File Offset: 0x00051BB0
		public static void DeleteCalendarItem(StoreObjectId calendarItemId, DeleteItemFlags deleteFlag)
		{
			if (calendarItemId == null)
			{
				throw new ArgumentNullException("calendarItemId");
			}
			UserContext userContext = UserContextManager.GetUserContext();
			using (CalendarItemBase calendarItemBase = (CalendarItemBase)Item.Bind(userContext.MailboxSession, calendarItemId, ItemBindOption.LoadRequiredPropertiesOnly))
			{
				calendarItemBase.DeleteMeeting(deleteFlag);
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x00053A08 File Offset: 0x00051C08
		public static void SaveItem(Item item, params PropertyDefinition[] properties)
		{
			ConflictResolutionResult conflictResolutionResult = item.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug(0L, "Saving item failed due to conflict resolution.");
				throw new OwaSaveConflictException(LocalizedStrings.GetNonEncoded(-482397486), conflictResolutionResult);
			}
			item.Load(properties);
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsUpdated.Increment();
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00053A64 File Offset: 0x00051C64
		public static CalendarItemBase TryGetCorrelatedItem(MeetingMessage meetingMessage)
		{
			CalendarItemBase result = null;
			try
			{
				ExTraceGlobals.CalendarTracer.TraceDebug<string>(0L, "Retreiving calendar item associated with meeting message. Value = '{0}'", (meetingMessage.Id != null) ? meetingMessage.Id.ObjectId.ToBase64String() : "null");
				result = meetingMessage.GetCorrelatedItem();
			}
			catch (CorrelationFailedException ex)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Can not retrieve calendar item associated with Meeting message.  Exception: {0}", ex.Message);
			}
			catch (CorruptDataException ex2)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Can not retrieve calendar item associated with Meeting message.  Exception: {0}", ex2.Message);
			}
			catch (ObjectDisposedException ex3)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Can not retrieve calendar item associated with Meeting message.  Exception: {0}", ex3.Message);
			}
			catch (StorageTransientException ex4)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Can not retrieve calendar item associated with Meeting message.  Exception: {0}", ex4.Message);
			}
			catch (InvalidOperationException ex5)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Can not retrieve calendar item associated with Meeting message.  Exception: {0}", ex5.Message);
			}
			return result;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00053B78 File Offset: 0x00051D78
		public static CalendarItemBase UpdateCalendarItem(MeetingRequest meetingRequest)
		{
			if (meetingRequest == null)
			{
				throw new ArgumentNullException("meetingRequest");
			}
			MeetingUtilities.ThrowIfMeetingResponseInvalid(meetingRequest);
			CalendarItemBase calendarItemBase = null;
			try
			{
				try
				{
					ExTraceGlobals.CalendarTracer.TraceDebug<string>(0L, "Retreiving calendar item associated with meeting request. Value = '{0}'", (meetingRequest.Id != null && meetingRequest.Id.ObjectId != null) ? meetingRequest.Id.ObjectId.ToBase64String() : "null");
					if (meetingRequest.IsOutOfDate())
					{
						throw new OwaOperationNotSupportedException(LocalizedStrings.GetNonEncoded(-1085726012));
					}
					calendarItemBase = meetingRequest.UpdateCalendarItem(true);
					if (calendarItemBase == null)
					{
						throw new OwaInvalidOperationException("meetingRequest.UpdateCalendarItem returns null.");
					}
					calendarItemBase.Load();
				}
				catch (CorrelationFailedException ex)
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Calendar item associated with meeting request could not be found.  Exception: {0}", ex.Message);
					throw;
				}
				catch (StoragePermanentException ex2)
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Calendar item associated with meeting request could not be found.  Exception: {0}", ex2.Message);
					throw;
				}
				catch (InvalidOperationException ex3)
				{
					if (ex3.Message == ServerStrings.ExOrganizerCannotCallUpdateCalendarItem)
					{
						throw new OwaOperationNotSupportedException(LocalizedStrings.GetNonEncoded(679811893));
					}
					throw;
				}
				try
				{
					MeetingUtilities.SaveItem(meetingRequest, new PropertyDefinition[]
					{
						MeetingMessageSchema.CalendarProcessed,
						StoreObjectSchema.ParentItemId
					});
				}
				catch (OwaSaveConflictException ex4)
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Calendar item associated with meeting request could not be saved.  Exception: {0}", ex4.Message);
					throw;
				}
			}
			catch
			{
				if (calendarItemBase != null)
				{
					calendarItemBase.Dispose();
					calendarItemBase = null;
				}
				throw;
			}
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.ItemsCreated.Increment();
			}
			return calendarItemBase;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00053D4C File Offset: 0x00051F4C
		public static CalendarItemBase TryPreProcessCalendarItem(MeetingMessage meetingMessage, UserContext userContext, bool doCalendarItemSave)
		{
			if (meetingMessage == null)
			{
				throw new ArgumentNullException("meetingMessage");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			CalendarItemBase calendarItemBase = null;
			meetingMessage.OpenAsReadWrite();
			try
			{
				ExTraceGlobals.CalendarTracer.TraceDebug<string>(0L, "Retreiving calendar item associated with meeting request. Value = '{0}'", (meetingMessage.Id != null) ? meetingMessage.Id.ObjectId.ToBase64String() : "null");
				calendarItemBase = meetingMessage.PreProcess(userContext.CalendarSettings.AddNewRequestsTentatively, userContext.CalendarSettings.ProcessExternalMeetingMessages, userContext.CalendarSettings.DefaultReminderTime);
				if (doCalendarItemSave && calendarItemBase != null)
				{
					Utilities.ValidateCalendarItemBaseStoreObject(calendarItemBase);
				}
				if (meetingMessage.IsDirty)
				{
					MeetingUtilities.SaveItem(meetingMessage, new PropertyDefinition[0]);
				}
			}
			catch (CorrelationFailedException ex)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Calendar item associated with meeting request could not be found.  Exception: {0}", ex.Message);
			}
			catch (CalendarProcessingException ex2)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Calendar item associated with meeting request could not be found.  Exception: {0}", ex2.Message);
			}
			catch (StoragePermanentException ex3)
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>(0L, "Calendar item associated with meeting request could not be found.  Exception: {0}", ex3.Message);
			}
			catch (ConnectionFailedTransientException innerException)
			{
				throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(995407892), innerException);
			}
			return calendarItemBase;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00053E90 File Offset: 0x00052090
		public static int CancelRecurrence(CalendarItem calendarItem, ExDateTime endRange)
		{
			if (endRange <= calendarItem.Recurrence.Range.StartDate)
			{
				return 0;
			}
			if (!(calendarItem.Recurrence.Range is NoEndRecurrenceRange))
			{
				OccurrenceInfo lastOccurrence = calendarItem.Recurrence.GetLastOccurrence();
				if (lastOccurrence != null && lastOccurrence.EndTime < endRange)
				{
					return int.MinValue;
				}
			}
			EndDateRecurrenceRange endDateRecurrenceRange = calendarItem.Recurrence.Range as EndDateRecurrenceRange;
			if (endDateRecurrenceRange != null && endDateRecurrenceRange.EndDate < endRange.Date)
			{
				return int.MinValue;
			}
			IList<OccurrenceInfo> occurrenceInfoList = calendarItem.Recurrence.GetOccurrenceInfoList(calendarItem.Recurrence.Range.StartDate, endRange);
			if (0 <= occurrenceInfoList.Count && (!(calendarItem.Recurrence.Range is NumberedRecurrenceRange) || occurrenceInfoList.Count < ((NumberedRecurrenceRange)calendarItem.Recurrence.Range).NumberOfOccurrences))
			{
				return occurrenceInfoList.Count;
			}
			return int.MinValue;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00053F7C File Offset: 0x0005217C
		public static bool CheckShouldDisableOccurrenceReminderUI(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			bool result = false;
			if (calendarItemBase.CalendarItemType != CalendarItemType.Single && calendarItemBase.CalendarItemType != CalendarItemType.RecurringMaster)
			{
				CalendarItemOccurrence calendarItemOccurrence = calendarItemBase as CalendarItemOccurrence;
				if (calendarItemOccurrence != null)
				{
					using (CalendarItem master = calendarItemOccurrence.GetMaster())
					{
						object obj = master.TryGetProperty(ItemSchema.ReminderIsSet);
						result = (obj is bool && !(bool)obj);
					}
				}
			}
			return result;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00053FFC File Offset: 0x000521FC
		public static MeetingResponse EditResponse(ResponseType responseType, MeetingRequest meetingRequest)
		{
			if (meetingRequest == null)
			{
				throw new ArgumentNullException("meetingRequest");
			}
			MeetingUtilities.ThrowIfMeetingResponseInvalid(meetingRequest);
			MeetingResponse result;
			using (CalendarItemBase calendarItemBase = MeetingUtilities.UpdateCalendarItem(meetingRequest))
			{
				result = MeetingUtilities.EditResponseInternal(responseType, calendarItemBase);
			}
			return result;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0005404C File Offset: 0x0005224C
		public static MeetingResponse EditResponse(ResponseType responseType, CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			MeetingUtilities.ThrowIfMeetingResponseInvalid(calendarItemBase);
			return MeetingUtilities.EditResponseInternal(responseType, calendarItemBase);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0005406C File Offset: 0x0005226C
		private static MeetingResponse EditResponseInternal(ResponseType responseType, CalendarItemBase calendarItemBase)
		{
			ExTraceGlobals.CalendarTracer.TraceDebug(0L, "Create meeting response and edit");
			calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(54431U, LastChangeAction.RespondToMeetingRequest);
			MeetingResponse meetingResponse = MeetingUtilities.CreateMeetingResponse(responseType, calendarItemBase, true);
			meetingResponse.Save(SaveMode.ResolveConflicts);
			MeetingUtilities.ProcessCalendarItemAfterResponse(responseType, calendarItemBase, true);
			return meetingResponse;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x000540B8 File Offset: 0x000522B8
		public static void NonEditResponse(ResponseType responseType, MeetingRequest meetingRequest, bool sendResponse)
		{
			if (meetingRequest == null)
			{
				throw new ArgumentNullException("meetingRequest");
			}
			MeetingUtilities.ThrowIfMeetingResponseInvalid(meetingRequest);
			using (CalendarItemBase calendarItemBase = MeetingUtilities.UpdateCalendarItem(meetingRequest))
			{
				MeetingUtilities.NonEditResponseInternal(responseType, calendarItemBase, sendResponse);
				MeetingUtilities.DeleteMeetingRequestAfterResponse(meetingRequest);
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0005410C File Offset: 0x0005230C
		public static void NonEditResponse(ResponseType responseType, CalendarItemBase calendarItemBase, bool sendResponse, MeetingRequest meetingRequest)
		{
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			MeetingUtilities.ThrowIfMeetingResponseInvalid(calendarItemBase);
			MeetingUtilities.NonEditResponseInternal(responseType, calendarItemBase, sendResponse);
			if (meetingRequest != null)
			{
				MeetingUtilities.DeleteMeetingRequestAfterResponse(meetingRequest);
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00054134 File Offset: 0x00052334
		private static void NonEditResponseInternal(ResponseType responseType, CalendarItemBase calendarItemBase, bool sendResponse)
		{
			ExTraceGlobals.CalendarTracer.TraceDebug(0L, "Process meeting response without editing");
			calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(42143U, LastChangeAction.RespondToMeetingRequest);
			using (MeetingResponse meetingResponse = MeetingUtilities.CreateMeetingResponse(responseType, calendarItemBase, sendResponse))
			{
				if (sendResponse)
				{
					meetingResponse.Send();
				}
			}
			MeetingUtilities.ProcessCalendarItemAfterResponse(responseType, calendarItemBase, sendResponse);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0005419C File Offset: 0x0005239C
		public static bool CheckOrganizer(string receivedRepresentingEmailAddress, string sentRepresentingEmailAddress, string mailboxOwnerLegacyDN)
		{
			if (mailboxOwnerLegacyDN == null)
			{
				throw new ArgumentNullException("mailboxOwnerLegacyDN");
			}
			if (string.IsNullOrEmpty(receivedRepresentingEmailAddress))
			{
				receivedRepresentingEmailAddress = mailboxOwnerLegacyDN;
			}
			ExTraceGlobals.CalendarDataTracer.TraceDebug<string, string, string>(0L, "CheckOrganizer(): Received representing={0} sent representing = {1} mailbox owner legacy DN = {2}", receivedRepresentingEmailAddress, sentRepresentingEmailAddress, mailboxOwnerLegacyDN);
			return string.Equals(receivedRepresentingEmailAddress, sentRepresentingEmailAddress, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000541D8 File Offset: 0x000523D8
		public static bool CheckDelegate(string receivedRepresentingEmailAddress, string mailboxOwnerLegacyDN)
		{
			if (mailboxOwnerLegacyDN == null)
			{
				throw new ArgumentNullException("mailboxOwnerLegacyDN");
			}
			ExTraceGlobals.CalendarDataTracer.TraceDebug<string, string>(0L, "CheckDelegate(): Received representing={0} mailbox owner legacy DN = {1}", (receivedRepresentingEmailAddress != null) ? receivedRepresentingEmailAddress : "<null>", mailboxOwnerLegacyDN);
			return !string.IsNullOrEmpty(receivedRepresentingEmailAddress) && !string.Equals(mailboxOwnerLegacyDN, receivedRepresentingEmailAddress, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00054228 File Offset: 0x00052428
		public static SanitizedHtmlString GetAttendeeResponseCountMessage(CalendarItemBase calendarItemBase)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (Attendee attendee in calendarItemBase.AttendeeCollection)
			{
				switch (attendee.ResponseType)
				{
				case ResponseType.Tentative:
					num2++;
					break;
				case ResponseType.Accept:
					num++;
					break;
				case ResponseType.Decline:
					num3++;
					break;
				}
			}
			SanitizedHtmlString result;
			if (num == 0 && num2 == 0 && num3 == 0)
			{
				result = SanitizedHtmlString.FromStringId(-518767563);
			}
			else
			{
				string response = MeetingUtilities.GetResponse(num, LocalizedStrings.GetNonEncoded(-1438005858), LocalizedStrings.GetNonEncoded(-171859085), Strings.MoreThenOneAttendeeAccepted);
				string response2 = MeetingUtilities.GetResponse(num2, LocalizedStrings.GetNonEncoded(-409971733), LocalizedStrings.GetNonEncoded(-1184021704), Strings.MoreThenOneAttendeeTentativelyAccepted);
				string response3 = MeetingUtilities.GetResponse(num3, LocalizedStrings.GetNonEncoded(161551623), LocalizedStrings.GetNonEncoded(1194482770), Strings.MoreThenOneAttendeeDeclined);
				result = SanitizedHtmlString.Format(LocalizedStrings.GetNonEncoded(-1080172631), new object[]
				{
					response,
					response2,
					response3
				});
			}
			return result;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00054358 File Offset: 0x00052558
		private static void ProcessCalendarItemAfterResponse(ResponseType responseType, CalendarItemBase calendarItemBase, bool intendToSendResponse)
		{
			UserContext userContext = UserContextManager.GetUserContext();
			if (responseType != ResponseType.Decline)
			{
				if (Utilities.IsItemInDefaultFolder(calendarItemBase, DefaultFolderType.DeletedItems))
				{
					StoreObjectId[] ids = new StoreObjectId[]
					{
						calendarItemBase.Id.ObjectId
					};
					userContext.MailboxSession.Move(userContext.CalendarFolderId, ids);
				}
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsUpdated.Increment();
					return;
				}
			}
			else
			{
				try
				{
					DeleteItemFlags deleteItemFlags = intendToSendResponse ? DeleteItemFlags.DeclineCalendarItemWithResponse : DeleteItemFlags.DeclineCalendarItemWithoutResponse;
					if (Utilities.IsItemInDefaultFolder(calendarItemBase, DefaultFolderType.DeletedItems))
					{
						deleteItemFlags |= DeleteItemFlags.SoftDelete;
					}
					else
					{
						deleteItemFlags |= DeleteItemFlags.MoveToDeletedItems;
					}
					Utilities.Delete(userContext, deleteItemFlags, new OwaStoreObjectId[]
					{
						OwaStoreObjectId.CreateFromStoreObject(calendarItemBase)
					});
				}
				catch (StoragePermanentException ex)
				{
					ExTraceGlobals.CalendarTracer.TraceDebug<string>(0L, "Exception on delete calendar item: {0}'", ex.Message);
					throw;
				}
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00054424 File Offset: 0x00052624
		private static MeetingResponse CreateMeetingResponse(ResponseType responseType, CalendarItemBase calendarItemBase, bool intendToSendResponse)
		{
			MeetingResponse meetingResponse = null;
			try
			{
				meetingResponse = calendarItemBase.RespondToMeetingRequest(responseType, true, intendToSendResponse, null, null);
				calendarItemBase.Load();
				if (Globals.ArePerfCountersEnabled)
				{
					OwaSingleCounters.ItemsCreated.Increment();
				}
				MeetingUtilities.UpdateMeetingResponseSubject(meetingResponse);
			}
			catch (SaveConflictException ex)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug<string>(0L, "Exception on RespondToMeetingRequest: {0}'", ex.Message);
				throw;
			}
			return meetingResponse;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0005449C File Offset: 0x0005269C
		internal static void DeleteMeetingRequestAfterResponse(MeetingRequest meetingRequest)
		{
			UserContext userContext = UserContextManager.GetUserContext();
			meetingRequest.Load();
			try
			{
				if (userContext.IsInMyMailbox(meetingRequest) && !Utilities.IsItemInDefaultFolder(meetingRequest, DefaultFolderType.DeletedItems))
				{
					Utilities.Delete(userContext, DeleteItemFlags.MoveToDeletedItems, new OwaStoreObjectId[]
					{
						OwaStoreObjectId.CreateFromStoreObject(meetingRequest)
					});
				}
			}
			catch (StoragePermanentException ex)
			{
				ExTraceGlobals.CalendarTracer.TraceDebug<string>(0L, "Provider exception on delete of meeting request.  {0}'", ex.Message);
				throw;
			}
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0005450C File Offset: 0x0005270C
		private static string GetResponse(int count, string zeroResponse, string oneResponse, string multipleResponses)
		{
			if (count == 0)
			{
				return zeroResponse;
			}
			if (count == 1)
			{
				return oneResponse;
			}
			return string.Format(multipleResponses, count);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00054528 File Offset: 0x00052728
		private static void UpdateMeetingResponseSubject(MeetingResponse meetingResponse)
		{
			string subject = meetingResponse.Subject;
			string text = (meetingResponse.TryGetProperty(ItemSchema.NormalizedSubject) as string) ?? string.Empty;
			if (subject.Length > 255)
			{
				int num = subject.Length - 255;
				meetingResponse[ItemSchema.NormalizedSubject] = text.Substring(0, text.Length - num);
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x0005458A File Offset: 0x0005278A
		public static void ThrowIfMeetingResponseInvalid(CalendarItemBase calendarItemBase)
		{
			MeetingUtilities.ThrowIfWebParts();
			if (calendarItemBase.IsOrganizer())
			{
				throw new OwaOperationNotSupportedException(LocalizedStrings.GetNonEncoded(679811893));
			}
			if (calendarItemBase.IsCancelled)
			{
				throw new OwaOperationNotSupportedException("Cannot respond to a cancelled meeting.");
			}
			MeetingUtilities.ThrowIfInArchiveMailbox(calendarItemBase);
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000545C4 File Offset: 0x000527C4
		public static void ThrowIfMeetingResponseInvalid(MeetingMessage meetingMessage)
		{
			MeetingUtilities.ThrowIfWebParts();
			MeetingRequest meetingRequest = meetingMessage as MeetingRequest;
			if (meetingRequest != null && meetingRequest.IsMailboxOwnerTheSender())
			{
				throw new OwaOperationNotSupportedException(LocalizedStrings.GetNonEncoded(679811893));
			}
			MeetingUtilities.ThrowIfInArchiveMailbox(meetingMessage);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00054600 File Offset: 0x00052800
		private static void ThrowIfWebParts()
		{
			UserContext userContext = UserContextManager.GetUserContext();
			if (userContext.IsWebPartRequest && userContext.MailboxSession.LogonType == LogonType.Delegated)
			{
				throw new OwaOperationNotSupportedException(LocalizedStrings.GetNonEncoded(236298055));
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00054639 File Offset: 0x00052839
		private static void ThrowIfInArchiveMailbox(Item item)
		{
			if (Utilities.IsInArchiveMailbox(item))
			{
				throw new OwaOperationNotSupportedException("Cannot respond to calendar item in archive folder.");
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00054650 File Offset: 0x00052850
		public static string GetReceivedOnBehalfOfDisplayName(MeetingMessage meetingMessage)
		{
			if (meetingMessage == null)
			{
				throw new ArgumentNullException("meetingMessage");
			}
			string result = LocalizedStrings.GetNonEncoded(-1018465893);
			if (meetingMessage.ReceivedRepresenting == null || string.IsNullOrEmpty(meetingMessage.ReceivedRepresenting.DisplayName))
			{
				result = LocalizedStrings.GetNonEncoded(-595380497);
			}
			else
			{
				result = meetingMessage.ReceivedRepresenting.DisplayName;
			}
			return result;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000546B0 File Offset: 0x000528B0
		public static bool IsEditCalendarItemOccurence(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			if (calendarItemBase.CalendarItemType != CalendarItemType.Single && calendarItemBase.CalendarItemType != CalendarItemType.RecurringMaster)
			{
				CalendarItemOccurrence calendarItemOccurrence = calendarItemBase as CalendarItemOccurrence;
				if (calendarItemOccurrence != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000546EC File Offset: 0x000528EC
		internal static bool IsCalendarItemEndTimeInPast(CalendarItemBase calendarItemBase)
		{
			if (calendarItemBase == null)
			{
				throw new ArgumentNullException("calendarItemBase");
			}
			ExDateTime localTime = DateTimeUtilities.GetLocalTime();
			bool result = false;
			if (calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				CalendarItem calendarItem = (CalendarItem)calendarItemBase;
				if (!(calendarItem.Recurrence.Range is NoEndRecurrenceRange))
				{
					OccurrenceInfo lastOccurrence = calendarItem.Recurrence.GetLastOccurrence();
					if (lastOccurrence != null && lastOccurrence.EndTime < localTime)
					{
						result = true;
					}
				}
			}
			else if (calendarItemBase.EndTime < localTime)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00054764 File Offset: 0x00052964
		internal static bool MeetingCancellationIsOutOfDate(MeetingCancellation meetingCancellation)
		{
			if (meetingCancellation == null)
			{
				throw new ArgumentNullException("meetingCancellation");
			}
			MeetingMessageType property = ItemUtility.GetProperty<MeetingMessageType>(meetingCancellation, CalendarItemBaseSchema.MeetingRequestType, MeetingMessageType.None);
			return property == MeetingMessageType.Outdated;
		}

		// Token: 0x0400089F RID: 2207
		public const int MaxLocationLength = 255;
	}
}
