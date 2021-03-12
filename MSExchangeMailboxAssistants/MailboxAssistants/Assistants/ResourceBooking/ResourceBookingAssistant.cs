using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ResourceBooking;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.MailboxAssistants.Assistants.Calendar;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x0200011B RID: 283
	internal class ResourceBookingAssistant : EventBasedAssistant, IEventBasedAssistant, IAssistantBase
	{
		// Token: 0x06000B6F RID: 2927 RVA: 0x00049984 File Offset: 0x00047B84
		public ResourceBookingAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
			this.calProcessor = new CalendarProcessing(true);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0004999C File Offset: 0x00047B9C
		public bool IsEventInteresting(MapiEvent mapiEvent)
		{
			CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
			bool? flag = ResourceCheck.QuickCheckForAutomatedBooking(mapiEvent, cachedState);
			if (flag != null && !(flag == true))
			{
				return false;
			}
			if (mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && (mapiEvent.EventMask & MapiEventTypeFlags.NewMail) != (MapiEventTypeFlags)0)
			{
				ResourceBookingAssistant.Tracer.TraceDebug<object, MapiEvent>((long)this.GetHashCode(), "{0}: this event is interesting because is a new message: {1}", TraceContext.Get(), mapiEvent);
				return true;
			}
			return ResourceCheck.IsEventConfigChange(mapiEvent);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00049A1C File Offset: 0x00047C1C
		protected override void HandleEventInternal(MapiEvent mapiEvent, MailboxSession itemStore, StoreObject item, List<KeyValuePair<string, object>> customDataToLog)
		{
			ResourceBookingAssistantLogEntry resourceBookingAssistantLogEntry = new ResourceBookingAssistantLogEntry
			{
				MapiEventFlag = mapiEvent.EventMask.ToString(),
				MailboxGuid = itemStore.MailboxGuid,
				TenantGuid = itemStore.MailboxOwner.MailboxInfo.OrganizationId.GetTenantGuid(),
				DatabaseGuid = itemStore.MailboxOwner.MailboxInfo.GetDatabaseGuid()
			};
			try
			{
				string text = (item != null) ? item.ClassName : mapiEvent.ObjectClass;
				resourceBookingAssistantLogEntry.ObjectClass = text;
				if (itemStore.GetDefaultFolderId(DefaultFolderType.Calendar) == null)
				{
					ResourceBookingAssistant.Tracer.TraceDebug<object, Guid>((long)this.GetHashCode(), "{0}: Mailbox: {1} - Calendar folder does not exist. Skipping processing.", TraceContext.Get(), mapiEvent.MailboxGuid);
					resourceBookingAssistantLogEntry.IsCalendarFolderNotAvailable = true;
				}
				else
				{
					CachedState cachedState = AssistantsService.CachedObjectsList.GetCachedState(mapiEvent.MailboxGuid);
					bool flag = ResourceCheck.DetailedCheckForAutomatedBooking(mapiEvent, itemStore, item, cachedState);
					resourceBookingAssistantLogEntry.IsAutomatedBooking = flag;
					if (flag)
					{
						if (item == null)
						{
							ResourceBookingAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: HandleEvent was passed a null item.", new object[]
							{
								TraceContext.Get()
							});
						}
						else
						{
							ResourceBookingAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Automatic Booking is on.", new object[]
							{
								TraceContext.Get()
							});
							if (item is MeetingMessage)
							{
								MeetingMessage meetingMessage = item as MeetingMessage;
								resourceBookingAssistantLogEntry.MeetingSender = meetingMessage.Sender.EmailAddress;
								resourceBookingAssistantLogEntry.MeetingInternetMessageId = meetingMessage.InternetMessageId;
								if (meetingMessage.IsDelegated())
								{
									ResourceBookingAssistant.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Deleting delegated meeting message: ID={1}", TraceContext.Get(), item.Id.ToString());
									itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
									{
										meetingMessage.Id
									});
									RbaLog.LogEntry(itemStore, meetingMessage, EvaluationResults.Delete);
									resourceBookingAssistantLogEntry.IsDelegatedMeetingRequest = true;
									return;
								}
							}
							cachedState.LockForRead();
							CalendarConfiguration calendarConfiguration;
							try
							{
								calendarConfiguration = (cachedState.State[0] as CalendarConfiguration);
							}
							finally
							{
								cachedState.ReleaseReaderLock();
							}
							bool flag2 = false;
							try
							{
								if (item is MeetingRequest)
								{
									flag2 = (item as MeetingRequest).IsOrganizer();
								}
								else if (item is MeetingCancellation)
								{
									flag2 = (item as MeetingCancellation).IsOrganizer();
								}
							}
							catch (ObjectNotFoundException innerException)
							{
								ResourceBookingAssistant.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Object Not Found. ID={1}", TraceContext.Get(), item.Id.ToString());
								throw new TransientMailboxException(innerException);
							}
							resourceBookingAssistantLogEntry.IsOrganizer = flag2;
							if (!ObjectClass.IsOfClass(text, "IPM.Schedule.Meeting") || flag2)
							{
								if (calendarConfiguration.DeleteNonCalendarItems && (mapiEvent.EventMask & MapiEventTypeFlags.NewMail) != (MapiEventTypeFlags)0)
								{
									ResourceBookingAssistant.Tracer.TraceError((long)this.GetHashCode(), "{0}: Deleting a non-meeting message", new object[]
									{
										TraceContext.Get()
									});
									if (item is MessageItem)
									{
										RbaLog.LogEntry(itemStore, item as MessageItem, EvaluationResults.Delete);
									}
									itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
									{
										item.Id
									});
									resourceBookingAssistantLogEntry.DeleteNonMeetingMessage = true;
								}
							}
							else
							{
								ResourceBookingAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Message is the right class", new object[]
								{
									TraceContext.Get()
								});
								if (!(item is MeetingMessage))
								{
									ResourceBookingAssistant.Tracer.TraceError((long)this.GetHashCode(), "{0}: Unexpected: Message class matched, but is not the correct object type. Ignoring message.", new object[]
									{
										TraceContext.Get()
									});
								}
								else
								{
									ResourceBookingAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Message is MeetingMessage", new object[]
									{
										TraceContext.Get()
									});
									resourceBookingAssistantLogEntry.IsMeetingMessage = true;
									MeetingMessage meetingMessage = item as MeetingMessage;
									CalendarItemBase calendarItemBase = null;
									DateTime utcNow = DateTime.UtcNow;
									resourceBookingAssistantLogEntry.StartProcessingTime = utcNow;
									for (int i = 0; i < 2; i++)
									{
										IEnumerable<VersionedId> duplicates;
										Exception ex;
										bool calendarItem = CalendarAssistant.GetCalendarItem(meetingMessage, ResourceBookingAssistant.Tracer, ref calendarItemBase, false, out duplicates, out ex);
										resourceBookingAssistantLogEntry.AddExceptionToLog(ex);
										if (!calendarItem)
										{
											break;
										}
										if (calendarItemBase == null)
										{
											ResourceBookingAssistant.Tracer.TraceDebug<object, VersionedId>((long)this.GetHashCode(), "{0}: Original CalendarItem for message {1} is null", TraceContext.Get(), meetingMessage.Id);
										}
										StoreObjectValidationError[] array = meetingMessage.Validate();
										if (array != null && array.Length > 0)
										{
											ResourceBookingAssistant.Tracer.TraceError<object, StoreObjectValidationError>((long)this.GetHashCode(), "{0}: mtgMessage did not validate, {1}", TraceContext.Get(), array[0]);
											resourceBookingAssistantLogEntry.IsMeetingMessageInvalid = true;
											throw new SkipException(Strings.descSkipExceptionFailedValidateCalItem);
										}
										string text2 = string.Empty;
										try
										{
											this.calProcessor.ProcessMeetingMessage(itemStore, meetingMessage, ref calendarItemBase, calendarConfiguration, duplicates, false);
											text2 = meetingMessage.Id.ToString();
											resourceBookingAssistantLogEntry.IsMeetingMessageProcessed = true;
											resourceBookingAssistantLogEntry.MeetingMessageId = text2;
											bool flag3 = false;
											if (meetingMessage is MeetingRequest)
											{
												flag3 = (meetingMessage as MeetingRequest).IsOrganizer();
											}
											else if (meetingMessage is MeetingCancellation)
											{
												flag3 = (meetingMessage as MeetingCancellation).IsOrganizer();
											}
											else if (meetingMessage is MeetingResponse)
											{
												flag3 = true;
											}
											resourceBookingAssistantLogEntry.IsOrganizer = flag3;
											resourceBookingAssistantLogEntry.IsDelegatedMeetingRequest = meetingMessage.IsDelegated();
											if (calendarItemBase == null)
											{
												if (flag3)
												{
													RbaLog.LogEntry(itemStore, meetingMessage, EvaluationResults.IgnoreOrganizer);
												}
												else if (resourceBookingAssistantLogEntry.IsDelegatedMeetingRequest)
												{
													RbaLog.LogEntry(itemStore, meetingMessage, EvaluationResults.IgnoreDelegate);
												}
											}
											if (calendarItemBase == null && meetingMessage is MeetingCancellation)
											{
												ResourceBookingAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: Deleting a meeting cancellation without correlated calendar item found", new object[]
												{
													TraceContext.Get()
												});
												itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
												{
													meetingMessage.Id
												});
												RbaLog.LogEntry(itemStore, meetingMessage, EvaluationResults.Delete);
												resourceBookingAssistantLogEntry.DeleteCanceledMeeting = true;
											}
											if (calendarItemBase != null)
											{
												StoreObjectValidationError[] array2 = calendarItemBase.Validate();
												if (array2 != null && array2.Length > 0)
												{
													ResourceBookingAssistant.Tracer.TraceError<object, StoreObjectValidationError>((long)this.GetHashCode(), "{0}: calendar item did not validate, {1}", TraceContext.Get(), array2[0]);
													throw new SkipException(Strings.descSkipExceptionFailedValidateCalItem);
												}
												ResourceBookingAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: About to process request", new object[]
												{
													TraceContext.Get()
												});
												EvaluationResults evaluationResults = EvaluationResults.None;
												BookingRoles bookingRoles = BookingRoles.NoRole;
												ResourceBookingProcessing.ProcessRequest(mapiEvent, itemStore, meetingMessage, ref calendarItemBase, calendarConfiguration, out evaluationResults, out bookingRoles);
												resourceBookingAssistantLogEntry.IsResourceBookingRequestProcessed = true;
												resourceBookingAssistantLogEntry.EvaluationResult = evaluationResults.ToString();
												resourceBookingAssistantLogEntry.BookingRole = bookingRoles.ToString();
											}
											break;
										}
										catch (CorruptDataException ex2)
										{
											ResourceBookingAssistant.Tracer.TraceDebug<object, string, CorruptDataException>((long)this.GetHashCode(), "{0}: The calendar item found was corrupted, so we could not do Resource Booking processing for message ID={1}, skipping event. Exception={2}", TraceContext.Get(), text2, ex2);
											throw new SkipException(ex2);
										}
										catch (ADTransientException arg)
										{
											ResourceBookingAssistant.Tracer.TraceDebug<object, string, ADTransientException>((long)this.GetHashCode(), "{0}: There was an tranisent AD error processing the calendar item during Resource Booking processing for message ID={1}, cleaning things up and retrying. Exception={2}", TraceContext.Get(), text2, arg);
											throw;
										}
										catch (ObjectNotFoundException innerException2)
										{
											ResourceBookingAssistant.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Object Not Found. ID={1}", TraceContext.Get(), text2);
											throw new TransientMailboxException(innerException2);
										}
										catch (SaveConflictException ex3)
										{
											ResourceBookingAssistant.Tracer.TraceDebug<object, string, SaveConflictException>((long)this.GetHashCode(), "{0}: There was an error saving the calendar item during Resource Booking processing for message ID={1}, cleaning things up and retrying.Exception={2}", TraceContext.Get(), text2, ex3);
											resourceBookingAssistantLogEntry.AddExceptionToLog(ex3);
										}
										catch (OccurrenceCrossingBoundaryException ex4)
										{
											resourceBookingAssistantLogEntry.AddExceptionToLog(ex4);
											ResourceBookingAssistant.Tracer.TraceDebug<object, string>((long)this.GetHashCode(), "{0}: Found an overlapping occurrence while processing message ID={1}. Cleaning things up and retrying", TraceContext.Get(), text2);
											if (ex4.OccurrenceInfo == null)
											{
												ResourceBookingAssistant.Tracer.TraceError((long)this.GetHashCode(), "{0}: Unexpected: Handling OccurrenceCrossingBoundaryException, the OccurrenceInfo is null", new object[]
												{
													TraceContext.Get()
												});
												break;
											}
											VersionedId versionedId = ex4.OccurrenceInfo.VersionedId;
											AggregateOperationResult aggregateOperationResult = meetingMessage.Session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
											{
												versionedId
											});
											ResourceBookingAssistant.Tracer.TraceDebug<object, OperationResult>((long)this.GetHashCode(), "{0}: Deleting the occurrence when handling an OccurrenceCrossingBoundaryException, returned:{2}", TraceContext.Get(), aggregateOperationResult.OperationResult);
										}
										finally
										{
											ResourceBookingAssistant.Tracer.TraceDebug((long)this.GetHashCode(), "{0}: In finally block.", new object[]
											{
												TraceContext.Get()
											});
											if (calendarItemBase != null)
											{
												calendarItemBase.Dispose();
												calendarItemBase = null;
											}
											DateTime utcNow2 = DateTime.UtcNow;
											TimeSpan timeSpan = utcNow2.Subtract(utcNow);
											ResourceBookingPerfmon.AverageResourceBookingProcessingTime.IncrementBy((long)timeSpan.TotalMilliseconds);
											ResourceBookingPerfmon.AverageResourceBookingProcessingTimeBase.Increment();
											resourceBookingAssistantLogEntry.StartProcessingTime = utcNow;
											resourceBookingAssistantLogEntry.StopProcessingTime = utcNow2;
											ResourceBookingAssistant.TracerPfd.TracePfd<int, object, string>((long)this.GetHashCode(), "PFD IWR {0} {1}: Finished processing message. MeetingMessageID = {2}", 20247, TraceContext.Get(), text2);
										}
										i++;
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex5)
			{
				resourceBookingAssistantLogEntry.AddExceptionToLog(ex5);
				throw ex5;
			}
			finally
			{
				customDataToLog.AddRange(resourceBookingAssistantLogEntry.FormatCustomData());
			}
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0004A346 File Offset: 0x00048546
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0004A34E File Offset: 0x0004854E
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0004A356 File Offset: 0x00048556
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x0400071E RID: 1822
		private CalendarProcessing calProcessor;

		// Token: 0x0400071F RID: 1823
		private static readonly Trace Tracer = ExTraceGlobals.ResourceBookingAssistantTracer;

		// Token: 0x04000720 RID: 1824
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
