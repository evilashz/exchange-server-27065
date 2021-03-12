using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.CalendarDiagnostics;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.Calendar;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Calendar
{
	// Token: 0x020000B5 RID: 181
	internal sealed class CalendarProcessing
	{
		// Token: 0x0600078E RID: 1934 RVA: 0x00035650 File Offset: 0x00033850
		public CalendarProcessing(bool honorProcessingConfiguration = true)
		{
			this.honorProcessingConfiguration = honorProcessingConfiguration;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x0003566A File Offset: 0x0003386A
		public void ProcessMeetingMessage(MailboxSession itemStore, MeetingMessage mtgMessage, ref CalendarItemBase originalCalItem, CalendarConfiguration mailboxConfig, IEnumerable<VersionedId> duplicates, bool performOmd)
		{
			this.ProcessMeetingMessage(itemStore, mtgMessage, ref originalCalItem, mailboxConfig, duplicates, performOmd, this.honorProcessingConfiguration);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00035684 File Offset: 0x00033884
		public void ProcessMeetingMessage(MailboxSession itemStore, MeetingMessage mtgMessage, ref CalendarItemBase originalCalItem, CalendarConfiguration mailboxConfig, IEnumerable<VersionedId> duplicates, bool performOmd, bool honorExternalMeetingMessageProcessingConfig)
		{
			bool isProcessed = mtgMessage.IsProcessed;
			bool flag = this.PreProcessMessage(itemStore, mtgMessage, isProcessed, mtgMessage.CalendarProcessed);
			bool flag2 = false;
			if (!flag)
			{
				flag2 = CalendarProcessing.IsAutoAcceptanceProcessingRequired(itemStore, mtgMessage, originalCalItem);
			}
			if (flag || flag2)
			{
				int num = mailboxConfig.DefaultReminderTime;
				if (num < 0 || num > 2629800)
				{
					num = CalendarProcessing.DefaultReminderMinutesBeforeStart;
				}
				int num2 = 3;
				int i = num2;
				if (this.honorProcessingConfiguration && originalCalItem == null && !mailboxConfig.AddNewRequestsTentatively && !mtgMessage.IsRepairUpdateMessage)
				{
					CalendarProcessing.ProcessingTracer.TraceDebug((long)mtgMessage.GetHashCode(), "{0}: Skipping processing because user settings for adding new items is false.", new object[]
					{
						TraceContext.Get()
					});
					return;
				}
				if (honorExternalMeetingMessageProcessingConfig && mtgMessage.SkipMessage(mailboxConfig.ProcessExternalMeetingMessages, originalCalItem))
				{
					CalendarProcessing.ProcessingTracer.TraceDebug<object, bool>((long)mtgMessage.GetHashCode(), "{0}: Skipping processing because user settings for processing external items is false. IsRepairUpdateMessage: {1}", TraceContext.Get(), mtgMessage.IsRepairUpdateMessage);
					return;
				}
				while (i > 0)
				{
					try
					{
						if (flag)
						{
							if (!this.DoProcessingLogic(itemStore, mtgMessage, ref originalCalItem, num, ref i))
							{
								this.TraceDebugAndPfd(mtgMessage.GetHashCode(), string.Format("{0}: completedProcessing = {1}", TraceContext.Get(), false));
								continue;
							}
							flag = false;
							this.TraceDebugAndPfd(mtgMessage.GetHashCode(), string.Format("{0}: completedProcessing = {1}", TraceContext.Get(), true));
							flag2 = CalendarProcessing.IsAutoAcceptanceProcessingRequired(itemStore, mtgMessage, originalCalItem);
						}
						if (flag2)
						{
							CalendarProcessing.AutoAcceptEvents(itemStore, originalCalItem);
							flag2 = false;
						}
						if (!flag && !flag2)
						{
							break;
						}
					}
					catch (ObjectExistedException exception)
					{
						this.TraceAndLogException(exception, itemStore, mtgMessage, i);
						i--;
					}
					catch (SaveConflictException ex)
					{
						this.TraceAndLogException(ex, itemStore, mtgMessage, i);
						i--;
						if (originalCalItem != null)
						{
							originalCalItem.Dispose();
							originalCalItem = null;
						}
						if (!CalendarAssistant.GetCalendarItem(mtgMessage, CalendarProcessing.UnexpectedPathTracer, ref originalCalItem, duplicates != null, out duplicates))
						{
							throw new SkipException(Strings.descSkipExceptionFailedToLoadCalItem, ex);
						}
						if (originalCalItem != null)
						{
							originalCalItem.OpenAsReadWrite();
						}
					}
					catch (QuotaExceededException exception2)
					{
						this.TraceAndLogException(exception2, itemStore, mtgMessage, i);
						i = -1;
					}
					catch (ObjectNotFoundException ex2)
					{
						this.TraceAndLogException(ex2, itemStore, mtgMessage, i);
						throw new SkipException(ex2);
					}
					catch (VirusDetectedException exception3)
					{
						this.TraceAndLogException(exception3, itemStore, mtgMessage, i);
						i = -1;
					}
					catch (VirusMessageDeletedException exception4)
					{
						this.TraceAndLogException(exception4, itemStore, mtgMessage, i);
						i = -1;
					}
					catch (DataValidationException ex3)
					{
						this.TraceAndLogException(ex3, itemStore, mtgMessage, i);
						throw new SkipException(ex3);
					}
					catch (RecurrenceException ex4)
					{
						this.TraceAndLogException(ex4, itemStore, mtgMessage, i);
						throw new SkipException(ex4);
					}
				}
				if (flag && i == 0)
				{
					string internetMessageId = mtgMessage.InternetMessageId;
					CalendarProcessing.ProcessingTracer.TraceDebug<object, string>((long)mtgMessage.GetHashCode(), "{0}: Attempting a third time to process message, but without the catch blocks: {1}", TraceContext.Get(), internetMessageId);
					this.DoProcessingLogic(itemStore, mtgMessage, ref originalCalItem, num, ref i);
				}
			}
			if (ObjectClass.IsMeetingCancellationSeries(mtgMessage.ClassName) || ObjectClass.IsMeetingRequestSeries(mtgMessage.ClassName))
			{
				this.TryUnparkInstanceMessages(itemStore, mtgMessage.GetValueOrDefault<string>(MeetingMessageSchema.SeriesId), mtgMessage.GetValueOrDefault<int>(MeetingMessageSchema.SeriesSequenceNumber));
			}
			if (performOmd)
			{
				this.OldMessageDeletion.PerformCleanUp(itemStore, mtgMessage, mailboxConfig, originalCalItem, duplicates);
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00035A34 File Offset: 0x00033C34
		public void ProcessMeetingInquiryMessage(CalendarVersionStoreGateway cvsGateway, MailboxSession itemStore, MeetingInquiryMessage message)
		{
			if (this.PreProcessMessage(itemStore, message, message.IsProcessed, message.CalendarProcessed))
			{
				MeetingInquiryAction processAction;
				try
				{
					itemStore.COWSettings.TemporaryDisableHold = true;
					processAction = message.Process(cvsGateway);
				}
				finally
				{
					itemStore.COWSettings.TemporaryDisableHold = false;
				}
				CalendarProcessing.LogHandler.UpdateItemContent(itemStore, (string currentContent) => string.Format("{0}{1}{2} MeetingInquiryMessage(GOID:{3}) {4}", new object[]
				{
					currentContent,
					Environment.NewLine,
					ExDateTime.UtcNow,
					message.GlobalObjectId,
					processAction
				}));
				this.TraceDebugAndPfd(message.GetHashCode(), string.Format("{0}: Completed processing of the inquiry message (Action: {1})", TraceContext.Get(), processAction));
			}
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00035B00 File Offset: 0x00033D00
		internal static bool CheckSaveResults(MessageItem message, ConflictResolutionResult saveResults, string internetMessageId)
		{
			if (saveResults.SaveStatus == SaveResult.Success)
			{
				return true;
			}
			ACRTraceString acrtraceString = new ACRTraceString(saveResults);
			if (saveResults.SaveStatus == SaveResult.SuccessWithConflictResolution)
			{
				CalendarProcessing.ProcessingTracer.TraceDebug<object, string, ACRTraceString>((long)message.GetHashCode(), "{0}: Success saving message, but conflict resolution was triggered. Message:{1} Properties in error: {2}", TraceContext.Get(), internetMessageId, acrtraceString);
				return true;
			}
			CalendarProcessing.ProcessingTracer.TraceDebug((long)message.GetHashCode(), "{0}: Error trying to save meeting message after final processing. Message:{1} SaveResults:{2} Properties in error: {3}", new object[]
			{
				TraceContext.Get(),
				internetMessageId,
				saveResults.SaveStatus,
				acrtraceString
			});
			return false;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00035B83 File Offset: 0x00033D83
		private static bool IsAutoAcceptanceProcessingRequired(MailboxSession session, MeetingMessage mtgMessage, CalendarItemBase calendarItem)
		{
			return calendarItem != null && mtgMessage is MeetingRequest && !calendarItem.IsOrganizer() && calendarItem.ResponseType != ResponseType.Accept && (session.IsGroupMailbox() || OldMessageDeletion.IsSelfForwardedEvent(mtgMessage, session));
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00035BB4 File Offset: 0x00033DB4
		private bool PreProcessMessage(MailboxSession itemStore, MessageItem message, bool processed, bool processedByCalendarAssistant)
		{
			if (itemStore == null)
			{
				throw new ArgumentNullException("itemStore");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			string subject = message.Subject;
			string internetMessageId = message.InternetMessageId;
			this.TraceDebugAndPfd(message.GetHashCode(), string.Format("{0}: About to process {1}", TraceContext.Get(), internetMessageId));
			CalendarProcessing.ProcessingTracer.TraceDebug<object, bool, bool>((long)message.GetHashCode(), "{0}: processed = {1}, processedByCalAssistant = {2}", TraceContext.Get(), processed, processedByCalendarAssistant);
			if (processed)
			{
				CalendarProcessing.ProcessingTracer.TraceDebug((long)message.GetHashCode(), "{0}: In if processed", new object[]
				{
					TraceContext.Get()
				});
				if (!processedByCalendarAssistant)
				{
					CalendarProcessing.ProcessingTracer.TraceDebug<object, string>((long)message.GetHashCode(), "{0}: Outlook processed flag is set to true on this meeting message: {1}", TraceContext.Get(), internetMessageId);
					CalendarAssistantPerformanceCounters.RacesLost.Increment();
				}
			}
			if (!processedByCalendarAssistant)
			{
				CalendarProcessing.ProcessingTracer.TraceDebug((long)message.GetHashCode(), "{0}: About to DoProcessingLogic", new object[]
				{
					TraceContext.Get()
				});
				return true;
			}
			if (message is MeetingForwardNotification)
			{
				CalendarProcessing.ProcessingTracer.TraceDebug<object, string>((long)message.GetHashCode(), "{0}: Already processed this meeting message, but it is a MFN: {1} so we need to check if we need to send an update", TraceContext.Get(), internetMessageId);
				return true;
			}
			CalendarProcessing.ProcessingTracer.TraceDebug<object, string>((long)message.GetHashCode(), "{0}: Already processed this meeting message, stopping: {1}", TraceContext.Get(), internetMessageId);
			return false;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00035CE8 File Offset: 0x00033EE8
		private bool DoProcessingLogic(MailboxSession itemStore, MeetingMessage mtgMessage, ref CalendarItemBase originalCalItem, int defaultReminderInMinutes, ref int retries)
		{
			string internetMessageId = mtgMessage.InternetMessageId;
			bool isProcessed = mtgMessage.IsProcessed;
			CalendarProcessing.ProcessingRequestTracer.TraceDebug((long)mtgMessage.GetHashCode(), "{0}: About to call OrganizerOrDelegateCheck", new object[]
			{
				TraceContext.Get()
			});
			bool flag;
			if (!CalendarProcessing.OrganizerOrDelegateCheck(mtgMessage, originalCalItem, out flag))
			{
				CalendarProcessing.ProcessingRequestTracer.TraceDebug((long)mtgMessage.GetHashCode(), "{0}: Not Organizer or delegate", new object[]
				{
					TraceContext.Get()
				});
				if (mtgMessage is MeetingRequest)
				{
					CalendarProcessing.ProcessingRequestTracer.TraceDebug<object, string>((long)mtgMessage.GetHashCode(), "{0}: Processing MeetingRequest: {1}", TraceContext.Get(), internetMessageId);
					this.ProcessMeetingRequest(itemStore, (MeetingRequest)mtgMessage, ref originalCalItem, internetMessageId, defaultReminderInMinutes);
					int num = (int)Utils.SafeGetProperty(mtgMessage, CalendarItemBaseSchema.AppointmentSequenceNumber, -1);
					if (isProcessed && num == 0)
					{
						((MeetingRequest)mtgMessage).MeetingRequestType = MeetingMessageType.NewMeetingRequest;
					}
				}
				else if (mtgMessage is MeetingResponse)
				{
					CalendarProcessing.ProcessingResponseTracer.TraceDebug<object, string>((long)mtgMessage.GetHashCode(), "{0}: Processing MeetingResponse: {1}", TraceContext.Get(), internetMessageId);
					this.ProcessMeetingResponse(itemStore, (MeetingResponse)mtgMessage, ref originalCalItem, internetMessageId);
				}
				else if (mtgMessage is MeetingCancellation)
				{
					CalendarProcessing.ProcessingCancellationTracer.TraceDebug<object, string>((long)mtgMessage.GetHashCode(), "{0}: Processing MeetingCancellation: {1}", TraceContext.Get(), internetMessageId);
					this.ProcessMeetingCancellation(itemStore, (MeetingCancellation)mtgMessage, ref originalCalItem, internetMessageId);
				}
				else if (mtgMessage is MeetingForwardNotification)
				{
					CalendarProcessing.ProcessingMeetingForwardNotificationTracer.TraceDebug<object, string>((long)mtgMessage.GetHashCode(), "{0}: Processing MeetingForwardNotification: {1}", TraceContext.Get(), internetMessageId);
					this.ProcessMeetingForwardNotification(itemStore, (MeetingForwardNotification)mtgMessage, ref originalCalItem, internetMessageId);
				}
			}
			else
			{
				int? num2 = (int?)Utils.SafeGetProperty(mtgMessage, MeetingMessageInstanceSchema.OriginalMeetingType, null);
				if (num2 != null)
				{
					mtgMessage[CalendarItemBaseSchema.MeetingRequestType] = (MeetingMessageType)num2.Value;
				}
			}
			if (flag)
			{
				mtgMessage.CalendarProcessed = true;
			}
			ConflictResolutionResult saveResults = mtgMessage.Save(SaveMode.ResolveConflicts);
			mtgMessage.Load();
			if (!CalendarProcessing.CheckSaveResults(mtgMessage, saveResults, internetMessageId))
			{
				retries--;
				return false;
			}
			return true;
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00035ED0 File Offset: 0x000340D0
		private void TryUnparkInstanceMessages(MailboxSession session, string seriesId, int seriesSequenceNumber)
		{
			string correlationId = ParkedMeetingMessage.GetCorrelationId(seriesId, seriesSequenceNumber);
			StoreObjectId defaultFolderId = session.GetDefaultFolderId(DefaultFolderType.ParkedMessages);
			if (defaultFolderId != null)
			{
				using (Folder folder = Folder.Bind(session, DefaultFolderType.ParkedMessages))
				{
					using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
					{
						new SortBy(ParkedMeetingMessageSchema.ParkedCorrelationId, SortOrder.Ascending),
						new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
					}, new PropertyDefinition[]
					{
						ParkedMeetingMessageSchema.ParkedCorrelationId,
						ItemSchema.Id,
						StoreObjectSchema.ItemClass
					}))
					{
						QueryFilter seekFilter = new AndFilter(new QueryFilter[]
						{
							new ComparisonFilter(ComparisonOperator.Equal, ParkedMeetingMessageSchema.ParkedCorrelationId, correlationId),
							new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.Parked.MeetingMessage")
						});
						queryResult.SeekToCondition(SeekReference.OriginBeginning, seekFilter, SeekToConditionFlags.AllowExtendedFilters);
						IStorePropertyBag[] propertyBags;
						do
						{
							propertyBags = queryResult.GetPropertyBags(50);
							foreach (IStorePropertyBag storePropertyBag in propertyBags)
							{
								string valueOrDefault = storePropertyBag.GetValueOrDefault<string>(ParkedMeetingMessageSchema.ParkedCorrelationId, null);
								string valueOrDefault2 = storePropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, null);
								if (!(correlationId == valueOrDefault) || !ObjectClass.IsParkedMeetingMessage(valueOrDefault2))
								{
									goto IL_10C;
								}
								this.UnparkMessage(session, storePropertyBag);
							}
						}
						while (propertyBags.Length > 0);
						IL_10C:;
					}
				}
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0003604C File Offset: 0x0003424C
		private void UnparkMessage(MailboxSession session, IStorePropertyBag rowPropertyBag)
		{
			VersionedId valueOrDefault = rowPropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
			try
			{
				using (ParkedMeetingMessage parkedMeetingMessage = ParkedMeetingMessage.Bind(session, valueOrDefault, new PropertyDefinition[0]))
				{
					IExchangePrincipal mailboxOwner = session.MailboxOwner;
					parkedMeetingMessage.Recipients.Clear();
					Participant participant = new Participant(mailboxOwner.MailboxInfo.DisplayName, mailboxOwner.LegacyDn, "EX");
					parkedMeetingMessage.Recipients.Add(participant);
					parkedMeetingMessage.From = participant;
					parkedMeetingMessage.SendWithoutSavingMessage(SubmitMessageFlags.None);
				}
			}
			catch (ObjectNotFoundException arg)
			{
				CalendarProcessing.ProcessingTracer.TraceWarning<VersionedId, ObjectNotFoundException>((long)this.GetHashCode(), "Cannot find parked message any more - skipping. Id: {0}, exception {1}", valueOrDefault, arg);
			}
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00036104 File Offset: 0x00034304
		private void TraceDebugAndPfd(int hashCode, string message)
		{
			CalendarProcessing.ProcessingTracer.TraceDebug((long)hashCode, message);
			CalendarProcessing.TracerPfd.TracePfd<string>((long)hashCode, "PFD IWC ", message);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00036128 File Offset: 0x00034328
		internal void TraceAndLogException(Exception exception, MailboxSession itemStore, MessageItem message, int retries)
		{
			string text = (exception is SaveConflictException) ? new ACRTraceString(((SaveConflictException)exception).ConflictResolutionResult).ToString() : string.Empty;
			CalendarProcessing.ProcessingTracer.TraceError((long)message.GetHashCode(), "{0}: Exception thrown when processing item: {1}, Retries = {2}, exception = {3}, extraData={4}", new object[]
			{
				TraceContext.Get(),
				message.InternetMessageId,
				retries,
				exception,
				text
			});
			CalendarAssistantPerformanceCounters.RequestsFailed.Increment();
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_ProcessingMeetingMessageFailure, null, new object[]
			{
				message.Subject,
				(itemStore != null && itemStore.MailboxOwner != null) ? itemStore.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString() : "NULL",
				exception
			});
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00036200 File Offset: 0x00034400
		private static bool OrganizerOrDelegateCheck(MeetingMessage mtgMessage, CalendarItemBase originalCalItem, out bool markAsProcessed)
		{
			bool result = false;
			bool flag = true;
			string text = (string)Utils.SafeGetProperty(mtgMessage, MessageItemSchema.ReceivedRepresentingEmailAddress, string.Empty);
			string text2 = (string)Utils.SafeGetProperty(mtgMessage, CalendarItemBaseSchema.OrganizerEmailAddress, string.Empty);
			string text3 = string.Empty;
			MailboxSession mailboxSession = mtgMessage.Session as MailboxSession;
			if (originalCalItem != null)
			{
				text3 = (string)Utils.SafeGetProperty(originalCalItem, CalendarItemBaseSchema.OrganizerEmailAddress, string.Empty);
			}
			CalendarProcessing.ProcessingRequestTracer.TraceDebug((long)mtgMessage.GetHashCode(), "{0}: OrganizerOrDelegateCheck(): ReceivedRepresenting={1} SentRepresenting={2} CalItemSentRepresenting={3} Mailbox LegDN={4}", new object[]
			{
				TraceContext.Get(),
				text,
				text2,
				text3,
				mailboxSession.MailboxOwner.LegacyDn
			});
			if (string.Compare(text3, text2, true) != 0)
			{
				CalendarProcessing.ProcessingRequestTracer.TraceDebug<object, string, string>((long)mtgMessage.GetHashCode(), "{0}: OrganizerOrDelegateCheck(): MReq SentRepresenting and CalItem SentRepresenting do NOT match. {1} != {2}", TraceContext.Get(), text2, text3);
			}
			if (!(mtgMessage is MeetingForwardNotification))
			{
				if (string.Compare(text, text2, true) == 0)
				{
					result = true;
				}
				if (mtgMessage.IsDelegated())
				{
					result = true;
				}
			}
			if (originalCalItem != null)
			{
				if (originalCalItem.IsOrganizer())
				{
					if (mtgMessage is MeetingRequest || mtgMessage is MeetingCancellation)
					{
						result = true;
					}
				}
				else if (mtgMessage is MeetingForwardNotification || mtgMessage is MeetingResponse)
				{
					result = true;
				}
			}
			else if (mtgMessage is MeetingRequest)
			{
				if (string.Compare(mailboxSession.MailboxOwner.LegacyDn, text2, true) == 0)
				{
					result = true;
				}
			}
			else
			{
				result = true;
				if (mtgMessage is MeetingResponse)
				{
					flag = false;
				}
			}
			markAsProcessed = flag;
			return result;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00036360 File Offset: 0x00034560
		private void ProcessMeetingRequest(MailboxSession itemStore, MeetingRequest mtgMessage, ref CalendarItemBase originalCalItem, string internetMessageId, int defaultReminderInMinutes)
		{
			try
			{
				if (mtgMessage.TryUpdateCalendarItem(ref originalCalItem, false))
				{
					MeetingMessageType meetingRequestType = mtgMessage.MeetingRequestType;
					if (originalCalItem != null)
					{
						if (originalCalItem.Id == null && MeetingMessageType.NewMeetingRequest == meetingRequestType)
						{
							int num = (int)Utils.SafeGetProperty(mtgMessage, ItemSchema.ReminderMinutesBeforeStart, defaultReminderInMinutes);
							if (num == 1525252321)
							{
								num = defaultReminderInMinutes;
								originalCalItem[ItemSchema.ReminderMinutesBeforeStart] = num;
							}
							if (num < 0 || num > 2629800)
							{
								originalCalItem[ItemSchema.ReminderMinutesBeforeStart] = defaultReminderInMinutes;
							}
							if (!originalCalItem.Reminder.IsSet)
							{
								originalCalItem.Reminder.MinutesBeforeStart = defaultReminderInMinutes;
								originalCalItem.Reminder.IsSet = true;
							}
						}
						originalCalItem.Validate();
						ConflictResolutionResult conflictResolutionResult = originalCalItem.Save(SaveMode.ResolveConflicts);
						originalCalItem.Load();
						if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
						{
							throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(originalCalItem.Id), conflictResolutionResult);
						}
					}
				}
				CalendarAssistantPerformanceCounters.MeetingRequests.Increment();
				CalendarProcessing.TracerPfd.TracePfd<int, object, string>((long)mtgMessage.GetHashCode(), "PFD IWC {0} {1}:completed Processing Meeting Request for {2}", 24727, TraceContext.Get(), internetMessageId);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new TransientException(Strings.descTransientErrorInRequest, innerException);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000364A4 File Offset: 0x000346A4
		private static void AutoAcceptEvents(MailboxSession session, CalendarItemBase originalCalItem)
		{
			if (originalCalItem != null && !originalCalItem.IsCancelled)
			{
				bool flag;
				if (session.IsGroupMailbox())
				{
					CalendarProcessing.ProcessingRequestTracer.TraceDebug<IExchangePrincipal>(0L, "Processing meeting request for group mailbox {0}", session.MailboxOwner);
					originalCalItem.Reminder.IsSet = false;
					flag = false;
				}
				else
				{
					CalendarProcessing.ProcessingRequestTracer.TraceDebug<IExchangePrincipal>(0L, "Processing sent to self meeting request. Mailbox owner: {0}", session.MailboxOwner);
					flag = originalCalItem.ResponseRequested;
				}
				using (MeetingResponse meetingResponse = originalCalItem.RespondToMeetingRequest(ResponseType.Accept, true, flag, null, null))
				{
					if (flag)
					{
						meetingResponse.Send();
					}
				}
				originalCalItem.Load();
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00036558 File Offset: 0x00034758
		private void ProcessMeetingResponse(MailboxSession itemStore, MeetingResponse mtgMessage, ref CalendarItemBase originalCalItem, string internetMessageId)
		{
			try
			{
				if (!mtgMessage.IsRepairUpdateMessage && originalCalItem != null && !originalCalItem.ResponseRequested)
				{
					CalendarProcessing.ProcessingResponseTracer.TraceDebug<object, string>((long)mtgMessage.GetHashCode(), "{0}: Skipping processing the MeetingResponse since the organizer has not requested a response: {1}", TraceContext.Get(), internetMessageId);
					CalendarProcessingSteps calendarProcessingSteps = CalendarProcessingSteps.PropsCheck | CalendarProcessingSteps.LookedForOutOfDate | CalendarProcessingSteps.UpdatedCalItem;
					mtgMessage.SetCalendarProcessingSteps(calendarProcessingSteps);
				}
				else if (mtgMessage.TryUpdateCalendarItem(ref originalCalItem, false))
				{
					mtgMessage.Load();
					try
					{
						itemStore.COWSettings.TemporaryDisableHold = true;
						ConflictResolutionResult conflictResolutionResult = originalCalItem.Save(SaveMode.ResolveConflicts);
						originalCalItem.Load();
						if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
						{
							throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(originalCalItem.Id), conflictResolutionResult);
						}
					}
					finally
					{
						itemStore.COWSettings.TemporaryDisableHold = false;
					}
				}
				CalendarAssistantPerformanceCounters.MeetingResponses.Increment();
				CalendarProcessing.TracerPfd.TracePfd<int, object, string>((long)mtgMessage.GetHashCode(), "PFD IWC {0} {1}:completed Processing Meeting Response for {2}", 20631, TraceContext.Get(), internetMessageId);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new TransientException(Strings.descTransientErrorInResponse, innerException);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00036658 File Offset: 0x00034858
		private void ProcessMeetingCancellation(MailboxSession itemStore, MeetingCancellation mtgMessage, ref CalendarItemBase originalCalItem, string internetMessageId)
		{
			try
			{
				if (mtgMessage.TryUpdateCalendarItem(ref originalCalItem, false))
				{
					mtgMessage.Load();
					ConflictResolutionResult conflictResolutionResult = originalCalItem.Save(SaveMode.ResolveConflicts);
					originalCalItem.Load();
					if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
					{
						throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(originalCalItem.Id), conflictResolutionResult);
					}
				}
				CalendarAssistantPerformanceCounters.MeetingCancellations.Increment();
				CalendarProcessing.TracerPfd.TracePfd<int, object, string>((long)mtgMessage.GetHashCode(), "PFD IWC {0} {1}:completed Processing Meeting Cancellation for {2}", 28823, TraceContext.Get(), internetMessageId);
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new TransientException(Strings.descTransientErrorInCancellation, innerException);
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000366F0 File Offset: 0x000348F0
		private void ProcessMeetingForwardNotification(MailboxSession itemStore, MeetingForwardNotification mfnMessage, ref CalendarItemBase originalCalItem, string internetMessageId)
		{
			try
			{
				if (!mfnMessage.CalendarProcessed)
				{
					if (!mfnMessage.TryUpdateCalendarItem(ref originalCalItem, false))
					{
						throw new SkipException(Strings.descSkipExceptionFailedUpdateFromMfn);
					}
					ConflictResolutionResult conflictResolutionResult = originalCalItem.Save(SaveMode.ResolveConflicts);
					originalCalItem.Load();
					if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
					{
						throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(originalCalItem.Id), conflictResolutionResult);
					}
				}
				mfnMessage.SendRumUpdate(ref originalCalItem);
				originalCalItem.Load();
			}
			catch (ObjectNotFoundException innerException)
			{
				throw new TransientException(Strings.descTransientErrorInMfn, innerException);
			}
		}

		// Token: 0x04000584 RID: 1412
		public const int MarkerForDefaultReminder = 1525252321;

		// Token: 0x04000585 RID: 1413
		public static DateTime MaxOutlookDateUtc = DateTime.SpecifyKind(new DateTime(4501, 1, 1), DateTimeKind.Utc);

		// Token: 0x04000586 RID: 1414
		public static int DefaultReminderMinutesBeforeStart = 15;

		// Token: 0x04000587 RID: 1415
		private static readonly Trace UnexpectedPathTracer = ExTraceGlobals.UnexpectedPathTracer;

		// Token: 0x04000588 RID: 1416
		private static readonly Trace ProcessingTracer = ExTraceGlobals.ProcessingTracer;

		// Token: 0x04000589 RID: 1417
		private static readonly Trace ProcessingRequestTracer = ExTraceGlobals.ProcessingRequestTracer;

		// Token: 0x0400058A RID: 1418
		private static readonly Trace ProcessingResponseTracer = ExTraceGlobals.ProcessingResponseTracer;

		// Token: 0x0400058B RID: 1419
		private static readonly Trace ProcessingCancellationTracer = ExTraceGlobals.ProcessingCancellationTracer;

		// Token: 0x0400058C RID: 1420
		private static readonly Trace ProcessingMeetingForwardNotificationTracer = ExTraceGlobals.ProcessingMeetingForwardNotificationTracer;

		// Token: 0x0400058D RID: 1421
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;

		// Token: 0x0400058E RID: 1422
		private static readonly SingleInstanceItemHandler LogHandler = new SingleInstanceItemHandler("IPM.Microsoft.CA.Log", DefaultFolderType.Configuration);

		// Token: 0x0400058F RID: 1423
		private readonly bool honorProcessingConfiguration;

		// Token: 0x04000590 RID: 1424
		internal readonly OldMessageDeletion OldMessageDeletion = new OldMessageDeletion();
	}
}
