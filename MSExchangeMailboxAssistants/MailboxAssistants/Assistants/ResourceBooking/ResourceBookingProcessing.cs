using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.ResourceBooking;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ResourceBooking
{
	// Token: 0x0200011E RID: 286
	internal static class ResourceBookingProcessing
	{
		// Token: 0x06000B7C RID: 2940 RVA: 0x0004A5CC File Offset: 0x000487CC
		public static void ProcessRequest(MapiEvent mapiEvent, MailboxSession itemStore, MeetingMessage mtgMessage, ref CalendarItemBase calItem, CalendarConfiguration calendarConfig, out EvaluationResults evaluationResult, out BookingRoles bookingRole)
		{
			evaluationResult = EvaluationResults.None;
			bookingRole = BookingRoles.NoRole;
			if (mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && (mapiEvent.EventMask & MapiEventTypeFlags.NewMail) != (MapiEventTypeFlags)0)
			{
				string subject = mtgMessage.Subject;
				string emailAddress = mtgMessage.Sender.EmailAddress;
				string legacyDn = itemStore.MailboxOwner.LegacyDn;
				ResourceBookingProcessing.Tracer.TraceDebug<object, string, string>((long)itemStore.GetHashCode(), "{0}: Received Request from:{1} subject {2}", TraceContext.Get(), emailAddress, subject);
				ResourceBookingProcessing.TracerPfd.TracePfd((long)itemStore.GetHashCode(), "PFD IWR {0} {1}: Received Request from:{2} subject {3}", new object[]
				{
					20247,
					TraceContext.Get(),
					emailAddress,
					subject
				});
				if (!calItem.IsOrganizer())
				{
					if (mtgMessage is MeetingRequest)
					{
						ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: It's a meeting request.", new object[]
						{
							TraceContext.Get()
						});
						MeetingRequest mtgMessage2 = mtgMessage as MeetingRequest;
						evaluationResult = ResourceBookingProcessing.ProcessMeetingRequest(itemStore, mtgMessage2, ref calItem, calendarConfig, out bookingRole);
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_RBAProcessedMeetingMessage, null, new object[]
						{
							legacyDn,
							emailAddress,
							subject
						});
					}
					else if (mtgMessage is MeetingCancellation)
					{
						ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: It's a meeting cancellation.", new object[]
						{
							TraceContext.Get()
						});
						ResourceBookingProcessing.ProcessMeetingCancellation(itemStore, mtgMessage as MeetingCancellation, calItem, calendarConfig);
						Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_RBAProcessedMeetingCancelation, null, new object[]
						{
							legacyDn,
							emailAddress,
							subject
						});
					}
				}
				itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
				{
					mtgMessage.Id
				});
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0004A780 File Offset: 0x00048980
		private static EvaluationResults ProcessMeetingRequest(MailboxSession itemStore, MeetingMessage mtgMessage, ref CalendarItemBase calItem, CalendarConfiguration calendarConfig, out BookingRoles bookingRole)
		{
			ResourceBookingPerfmon.RequestsSubmitted.Increment();
			List<AdjacencyOrConflictInfo> list = null;
			bookingRole = BookingRoles.NoRole;
			ResponseTextGenerator responseTextGenerator = null;
			EvaluationResults evaluationResults;
			try
			{
				evaluationResults = ResourceBookingProcessing.Evaluate(itemStore, mtgMessage, calItem, calendarConfig, out responseTextGenerator, out list, out bookingRole);
			}
			catch (WorkingHoursXmlMalformedException)
			{
				ResourceBookingProcessing.Tracer.TraceError((long)itemStore.GetHashCode(), "{0}: The working hours configuration is corrupt. Warning the organizer.", new object[]
				{
					TraceContext.Get()
				});
				responseTextGenerator.GenerateMessageForCorruptWorkingHours();
				evaluationResults = EvaluationResults.Message;
			}
			if (evaluationResults == EvaluationResults.Message)
			{
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(calItem.Body.Format);
				replyForwardConfiguration.SubjectPrefix = responseTextGenerator.GetResponseSubjectPrefix();
				replyForwardConfiguration.AddBodyPrefix(responseTextGenerator.GetResponseBody(), BodyInjectionFormat.Html);
				using (MessageItem messageItem = calItem.CreateReply(itemStore.GetDefaultFolderId(DefaultFolderType.Outbox), replyForwardConfiguration))
				{
					messageItem.Recipients.Add(calItem.Organizer);
					messageItem.Send();
					return evaluationResults;
				}
			}
			if (evaluationResults == EvaluationResults.Delete)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Deleting.", new object[]
				{
					TraceContext.Get()
				});
				itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
				{
					mtgMessage.Id
				});
			}
			else if (evaluationResults == EvaluationResults.Accept)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Accepting.", new object[]
				{
					TraceContext.Get()
				});
				ResourceBookingProcessing.AcceptMeetingRequest(itemStore, mtgMessage, calItem, calendarConfig, responseTextGenerator);
				calItem.Load();
				mtgMessage.Load();
				bool forceBind = false;
				if (!calendarConfig.AllowConflicts && list != null && calItem is CalendarItem)
				{
					Recurrence recurrence = (calItem as CalendarItem).Recurrence;
					if (recurrence != null)
					{
						if (list.Count != 0)
						{
							forceBind = true;
						}
						foreach (AdjacencyOrConflictInfo adjacencyOrConflictInfo in list)
						{
							IList<OccurrenceInfo> occurrenceInfoList = recurrence.GetOccurrenceInfoList(adjacencyOrConflictInfo.OccurrenceInfo.StartTime, adjacencyOrConflictInfo.OccurrenceInfo.EndTime);
							List<AdjacencyOrConflictInfo> list2 = new List<AdjacencyOrConflictInfo>(1);
							list2.Add(adjacencyOrConflictInfo);
							foreach (OccurrenceInfo occurrenceInfo in occurrenceInfoList)
							{
								using (CalendarItemBase calendarItemBase = CalendarItemBase.Bind(itemStore, occurrenceInfo.VersionedId.ObjectId))
								{
									calendarItemBase.OpenAsReadWrite();
									if (calendarItemBase.ResponseType == ResponseType.Accept)
									{
										responseTextGenerator.GenerateSingleOutOfPolicyResponse(ConflictType.BookedSomeAccepted, calendarConfig.BookingWindowInDays, ResourceBookingProcessing.GetEndOfBookingWindowLocal(ResourceBookingProcessing.GetResourceTimeZone(itemStore), calendarConfig.BookingWindowInDays));
										ResourceBookingProcessing.DeclineMeetingRequest(itemStore, mtgMessage, calendarItemBase, responseTextGenerator);
									}
								}
							}
						}
					}
				}
				ResourceBookingProcessing.PostProcess(itemStore, mtgMessage, ref calItem, calendarConfig, forceBind);
			}
			else if (evaluationResults == EvaluationResults.Decline)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Decline.", new object[]
				{
					TraceContext.Get()
				});
				ResourceBookingProcessing.DeclineMeetingRequest(itemStore, mtgMessage, calItem, responseTextGenerator);
			}
			else if (evaluationResults == EvaluationResults.Tentative)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Tentative.", new object[]
				{
					TraceContext.Get()
				});
				ResourceBookingProcessing.TentativeMeetingRequest(itemStore, mtgMessage, calItem, calendarConfig, responseTextGenerator);
				ResourceBookingProcessing.PostProcess(itemStore, mtgMessage, ref calItem, calendarConfig, false);
				ResourceBookingProcessing.TracerPfd.TracePfd<int, object, VersionedId>((long)itemStore.GetHashCode(), "PFD IWR {0} {1} Processing completed on {2}.", 28439, TraceContext.Get(), mtgMessage.Id);
			}
			return evaluationResults;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0004AB10 File Offset: 0x00048D10
		private static EvaluationResults Evaluate(MailboxSession itemStore, MeetingMessage mtgMessage, CalendarItemBase calItem, CalendarConfiguration mailboxConfig, out ResponseTextGenerator responseMessage, out List<AdjacencyOrConflictInfo> conflictList, out BookingRoles role)
		{
			conflictList = null;
			responseMessage = null;
			role = BookingRoles.NoRole;
			if (mtgMessage.SkipMessage(mailboxConfig.ProcessExternalMeetingMessages, calItem))
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)mtgMessage.GetHashCode(), "{0}: Delete processing request since we are not allowed to process it.", new object[]
				{
					TraceContext.Get()
				});
				return EvaluationResults.Delete;
			}
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(itemStore.InternalMailboxOwner.MailboxInfo.OrganizationId), 346, "Evaluate", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\resourcebooking\\ResourceBookingProcessing.cs");
			Participant from = mtgMessage.From;
			Participant sender = mtgMessage.Sender;
			ADRecipient adrecipient = null;
			if (from != null)
			{
				ProxyAddress proxyAddress = ProxyAddress.Parse(from.RoutingType, from.EmailAddress);
				try
				{
					adrecipient = tenantOrRootOrgRecipientSession.FindByProxyAddress(proxyAddress);
				}
				catch (DataValidationException ex)
				{
					ResourceBookingProcessing.Tracer.TraceError<object, string>((long)itemStore.GetHashCode(), "{0}: DataValidationException hit while processing a message. {1}", TraceContext.Get(), ex.ToString());
					Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_RBAValidationException, null, new object[]
					{
						mtgMessage.Subject,
						ex.ToString()
					});
					throw new SkipException(ex.LocalizedString, ex);
				}
			}
			if (adrecipient == null && !mailboxConfig.ProcessExternalMeetingMessages)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Sender not in AD.", new object[]
				{
					TraceContext.Get()
				});
				return EvaluationResults.Delete;
			}
			ADRecipient adrecipient2 = null;
			if (sender != null && sender != from)
			{
				ProxyAddress proxyAddress2 = ProxyAddress.Parse(sender.RoutingType, sender.EmailAddress);
				adrecipient2 = tenantOrRootOrgRecipientSession.FindByProxyAddress(proxyAddress2);
				if (adrecipient2 != null && adrecipient != null && adrecipient2.Id != null && adrecipient2.Id.Equals(adrecipient.Id))
				{
					adrecipient2 = null;
				}
			}
			bool flag = false;
			bool flag2 = false;
			role = BookingPolicy.QueryRole(tenantOrRootOrgRecipientSession, adrecipient, mailboxConfig, out flag, out flag2);
			if (adrecipient2 != null)
			{
				bool flag3 = false;
				bool flag4 = false;
				BookingRoles bookingRoles = BookingPolicy.QueryRole(tenantOrRootOrgRecipientSession, adrecipient2, mailboxConfig, out flag3, out flag4);
				if (bookingRoles > role)
				{
					role = bookingRoles;
				}
				flag2 = (flag2 || flag4);
				flag = (flag && flag3);
			}
			ExDateTime endOfBookingWindowLocal = ResourceBookingProcessing.GetEndOfBookingWindowLocal(ResourceBookingProcessing.GetResourceTimeZone(itemStore), mailboxConfig.BookingWindowInDays);
			CheckConflict.CheckCalendarConflict(itemStore, calItem, new ExDateTime?(endOfBookingWindowLocal), out conflictList);
			responseMessage = new ResponseTextGenerator(ResourceBookingProcessing.GetMesssageCultureInfo(mtgMessage), calItem.StartTimeZone, calItem is CalendarItem && (calItem as CalendarItem).Recurrence != null, mailboxConfig.AddAdditionalResponse ? mailboxConfig.AdditionalResponse : null, mailboxConfig.EnableResponseDetails, mailboxConfig.OrganizerInfo, itemStore.MailboxOwner.MailboxInfo.DisplayName, ResourceBookingProcessing.AdjacencyOrConflictInfoToMeetingConflict(itemStore, conflictList));
			if (role == BookingRoles.NoRole)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Sender has no role.", new object[]
				{
					TraceContext.Get()
				});
				responseMessage.GenerateOutOfPolicyRoleNotAllowed();
				return EvaluationResults.Decline;
			}
			if (responseMessage.IsRecurrence)
			{
				CalendarItem calendarItem = calItem as CalendarItem;
				Recurrence recurrence;
				if (calendarItem != null && (recurrence = calendarItem.Recurrence) != null && !(recurrence.Range is NoEndRecurrenceRange))
				{
					OccurrenceInfo lastOccurrence = recurrence.GetLastOccurrence();
					if (lastOccurrence.EndTime <= ExDateTime.UtcNow)
					{
						ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Recurrence Request is in the past.", new object[]
						{
							TraceContext.Get()
						});
						responseMessage.GenerateDeclineMeetingPast();
						return EvaluationResults.Decline;
					}
				}
			}
			else if (calItem.EndTime <= ExDateTime.UtcNow)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Request is in the past.", new object[]
				{
					TraceContext.Get()
				});
				responseMessage.GenerateDeclineMeetingPast();
				return EvaluationResults.Decline;
			}
			bool flag5 = ResourceBookingProcessing.IsRequestInPolicy(itemStore, mtgMessage, calItem, mailboxConfig, responseMessage, conflictList);
			if (flag5)
			{
				if (role == BookingRoles.RequestInPolicy || (flag && !flag2))
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Sender has RequestInPolicy.", new object[]
					{
						TraceContext.Get()
					});
					responseMessage.GenerateInPolicyRoleNotAllowed();
					return EvaluationResults.Tentative;
				}
				if (!flag && !flag2)
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Sender cannot book in policy.", new object[]
					{
						TraceContext.Get()
					});
					responseMessage.GenerateInPolicyRoleNotAllowed();
					RbaLog.LogEntry(itemStore, mtgMessage, EvaluationResults.Decline);
					return EvaluationResults.Decline;
				}
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Can accept for sender.", new object[]
				{
					TraceContext.Get()
				});
				return EvaluationResults.Accept;
			}
			else
			{
				if (role == BookingRoles.RequestInPolicy || role == BookingRoles.BookInPolicy)
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Sender cannot book out of policy.");
					return EvaluationResults.Decline;
				}
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Defaulting to Tentative.", new object[]
				{
					TraceContext.Get()
				});
				return EvaluationResults.Tentative;
			}
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0004AFC8 File Offset: 0x000491C8
		private static bool IsRequestInPolicy(MailboxSession itemStore, MeetingMessage mtgMessage, CalendarItemBase calItem, CalendarConfiguration mailboxConfig, ResponseTextGenerator responseMessage, List<AdjacencyOrConflictInfo> conflictList)
		{
			Recurrence recurrence = null;
			OccurrenceInfo occurrenceInfo = null;
			IList<OccurrenceInfo> list = null;
			if (responseMessage.IsRecurrence)
			{
				CalendarItem calendarItem = calItem as CalendarItem;
				if (calendarItem != null && (recurrence = calendarItem.Recurrence) != null && !(recurrence.Range is NoEndRecurrenceRange))
				{
					occurrenceInfo = recurrence.GetLastOccurrence();
				}
			}
			ExDateTime endOfBookingWindowLocal = ResourceBookingProcessing.GetEndOfBookingWindowLocal(ResourceBookingProcessing.GetResourceTimeZone(itemStore), mailboxConfig.BookingWindowInDays);
			if (recurrence != null && !mailboxConfig.AllowRecurringMeetings)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: The resource does not allow recurring meetings.", new object[]
				{
					TraceContext.Get()
				});
				responseMessage.GenerateRecurringOutOfPolicyResponse(ConflictType.Recurring, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
				return false;
			}
			if (mailboxConfig.EnforceSchedulingHorizon)
			{
				if (recurrence != null)
				{
					if (occurrenceInfo == null)
					{
						ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Recurrence does not end. This resource does not allow such bookings", new object[]
						{
							TraceContext.Get()
						});
						responseMessage.GenerateRecurringOutOfPolicyResponse(ConflictType.EndDate, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
						return false;
					}
					if (occurrenceInfo != null && occurrenceInfo.EndTime > endOfBookingWindowLocal)
					{
						ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Recurrence ends is past the booking window. Meeting will be declined.", new object[]
						{
							TraceContext.Get()
						});
						responseMessage.GenerateRecurringOutOfPolicyResponse(ConflictType.EndDateWindow, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
						return false;
					}
				}
				else if (calItem.EndTime > endOfBookingWindowLocal)
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Meeting is a single instance that goes past the booking window", new object[]
					{
						TraceContext.Get()
					});
					responseMessage.GenerateSingleOutOfPolicyResponse(ConflictType.BookingWindow, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
					return false;
				}
			}
			else
			{
				if (recurrence != null && calItem.StartTime > endOfBookingWindowLocal)
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Not in policy. Recurrence starts after booking window.", new object[]
					{
						TraceContext.Get()
					});
					responseMessage.GenerateRecurringOutOfPolicyResponse(ConflictType.EndDateWindow, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
					return false;
				}
				if (recurrence == null && calItem.EndTime > endOfBookingWindowLocal)
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Not in policy. Single instance ends after the booking window.", new object[]
					{
						TraceContext.Get()
					});
					responseMessage.GenerateSingleOutOfPolicyResponse(ConflictType.BookingWindow, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
					return false;
				}
			}
			if (mailboxConfig.MaximumDurationInMinutes > 0)
			{
				bool flag = true;
				TimeSpan t = new TimeSpan(0, mailboxConfig.MaximumDurationInMinutes, 0);
				if (recurrence != null)
				{
					if (list == null)
					{
						list = recurrence.GetOccurrenceInfoList(ExDateTime.Now, ExTimeZone.CurrentTimeZone.ConvertDateTime(endOfBookingWindowLocal));
					}
					using (IEnumerator<OccurrenceInfo> enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							OccurrenceInfo occurrenceInfo2 = enumerator.Current;
							TimeSpan t2 = occurrenceInfo2.EndTime.Subtract(occurrenceInfo2.StartTime);
							if (t2 > t)
							{
								flag = false;
								break;
							}
						}
						goto IL_2CD;
					}
				}
				TimeSpan t3 = calItem.EndTime.Subtract(calItem.StartTime);
				if (t3 > t)
				{
					flag = false;
				}
				IL_2CD:
				if (!flag)
				{
					responseMessage.GenerateOutOfPolicyMeetingTooLong(mailboxConfig.MaximumDurationInMinutes);
					return false;
				}
			}
			if (mailboxConfig.ScheduleOnlyDuringWorkHours)
			{
				StoreObjectId defaultFolderId = itemStore.GetDefaultFolderId(DefaultFolderType.Calendar);
				WorkingHours workingHours = WorkingHours.LoadFrom(itemStore, defaultFolderId);
				if (workingHours != null)
				{
					bool flag2 = true;
					if (recurrence != null)
					{
						if (list == null)
						{
							list = recurrence.GetOccurrenceInfoList(ExDateTime.Now, ExTimeZone.CurrentTimeZone.ConvertDateTime(endOfBookingWindowLocal));
						}
						using (IEnumerator<OccurrenceInfo> enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								OccurrenceInfo occurrenceInfo3 = enumerator2.Current;
								if (!workingHours.InWorkingHours(occurrenceInfo3.StartTime, occurrenceInfo3.EndTime))
								{
									flag2 = false;
								}
							}
							goto IL_382;
						}
					}
					if (!workingHours.InWorkingHours(calItem.StartTime, calItem.EndTime))
					{
						flag2 = false;
					}
					IL_382:
					if (!flag2)
					{
						responseMessage.GenerateOutOfPolicyOutOfWorkingHours(workingHours);
						return false;
					}
				}
			}
			if (mailboxConfig.AllowConflicts || conflictList.Count == 0)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Defaulting to in policy.", new object[]
				{
					TraceContext.Get()
				});
				if (!responseMessage.IsRecurrence)
				{
					responseMessage.GenerateSingleInPolicyResponse();
				}
				else
				{
					responseMessage.GenerateRecurringInPolicyResponse((conflictList.Count == 0) ? RecurringAcceptType.Free : RecurringAcceptType.SomeBooked, occurrenceInfo, endOfBookingWindowLocal);
				}
				ResourceBookingProcessing.TracerPfd.TracePfd<int, object, VersionedId>((long)itemStore.GetHashCode(), "PFD IWR {0} {1}: The meeting:{2} is in policy ", 24343, TraceContext.Get(), mtgMessage.Id);
				return true;
			}
			if (!responseMessage.IsRecurrence)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Not in policy. Single instance with conflicts.", new object[]
				{
					TraceContext.Get()
				});
				responseMessage.GenerateSingleOutOfPolicyResponse(ConflictType.Booked, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
				return false;
			}
			if (mailboxConfig.ConflictPercentageAllowed <= 0 || mailboxConfig.MaximumConflictInstances <= 0)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Not in policy. Recurrence with too many conflicts.", new object[]
				{
					TraceContext.Get()
				});
				responseMessage.GenerateRecurringOutOfPolicyResponse(ConflictType.Booked, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
				return false;
			}
			if (conflictList.Count > mailboxConfig.MaximumConflictInstances)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Not in policy. Recurrence with more than MaximumConflictInstances.", new object[]
				{
					TraceContext.Get()
				});
				responseMessage.GenerateRecurringOutOfPolicyResponse(ConflictType.Booked, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
				return false;
			}
			if (list == null && recurrence != null)
			{
				list = recurrence.GetOccurrenceInfoList(ExDateTime.Now, ExTimeZone.CurrentTimeZone.ConvertDateTime(endOfBookingWindowLocal));
			}
			double num = (double)conflictList.Count / (double)list.Count * 100.0;
			if (num <= (double)mailboxConfig.ConflictPercentageAllowed && conflictList.Count <= mailboxConfig.MaximumConflictInstances)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: In policy. Recurrence with less than ConflictPercentage.", new object[]
				{
					TraceContext.Get()
				});
				responseMessage.GenerateRecurringInPolicyResponse(RecurringAcceptType.SomeBooked, occurrenceInfo, endOfBookingWindowLocal);
				return true;
			}
			ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Not in policy. Recurrence with more than Conflictpercentage.", new object[]
			{
				TraceContext.Get()
			});
			responseMessage.GenerateRecurringOutOfPolicyResponse(ConflictType.Booked, mailboxConfig.BookingWindowInDays, endOfBookingWindowLocal);
			return false;
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0004B5B4 File Offset: 0x000497B4
		private static ExTimeZone GetResourceTimeZone(MailboxSession mailboxSession)
		{
			ExTimeZone exTimeZone = null;
			try
			{
				StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.Calendar);
				WorkingHours workingHours = WorkingHours.LoadFrom(mailboxSession, defaultFolderId);
				if (workingHours != null)
				{
					exTimeZone = workingHours.TimeZone.TimeZone;
				}
			}
			catch (WorkingHoursXmlMalformedException)
			{
				ResourceBookingProcessing.Tracer.TraceError((long)mailboxSession.GetHashCode(), "{0}: The working hours configuration is corrupt.", new object[]
				{
					TraceContext.Get()
				});
			}
			if (exTimeZone == null)
			{
				exTimeZone = ExTimeZone.CurrentTimeZone;
			}
			return exTimeZone;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0004B628 File Offset: 0x00049828
		private static void PostProcess(MailboxSession itemStore, MeetingMessage mtgMessage, ref CalendarItemBase calItem, CalendarConfiguration mailboxConfig, bool forceBind = false)
		{
			if (forceBind)
			{
				StoreObjectId storeObjectId = calItem.StoreObjectId;
				calItem.Dispose();
				calItem = CalendarItemBase.Bind(itemStore, storeObjectId);
				calItem.OpenAsReadWrite();
			}
			else
			{
				calItem.Load();
			}
			if (calItem.FreeBusyStatus == Microsoft.Exchange.Data.Storage.BusyType.Tentative)
			{
				if (!mailboxConfig.TentativePendingApproval)
				{
					calItem.FreeBusyStatus = Microsoft.Exchange.Data.Storage.BusyType.Free;
				}
			}
			else
			{
				calItem.FreeBusyStatus = Microsoft.Exchange.Data.Storage.BusyType.Busy;
			}
			if (mailboxConfig.DisableReminders)
			{
				calItem[ItemSchema.ReminderIsSet] = false;
			}
			if (mailboxConfig.BookingWindowInDays > 0 && !mailboxConfig.EnforceSchedulingHorizon && calItem is CalendarItem && (calItem as CalendarItem).Recurrence != null)
			{
				CalendarItem calendarItem = calItem as CalendarItem;
				OccurrenceInfo occurrenceInfo = null;
				if (!(calendarItem.Recurrence.Range is NoEndRecurrenceRange))
				{
					occurrenceInfo = calendarItem.Recurrence.GetLastOccurrence();
				}
				ExTimeZone resourceTimeZone = ResourceBookingProcessing.GetResourceTimeZone(itemStore);
				ExDateTime exDateTime = ResourceBookingProcessing.GetEndOfBookingWindowLocal(resourceTimeZone, mailboxConfig.BookingWindowInDays).AddSeconds(-1.0);
				if (occurrenceInfo == null || occurrenceInfo.EndTime > exDateTime)
				{
					calendarItem.Recurrence = new Recurrence(calendarItem.Recurrence.Pattern, new EndDateRecurrenceRange(calendarItem.Recurrence.Range.StartDate, itemStore.ExTimeZone.ConvertDateTime(exDateTime)));
				}
			}
			if (mailboxConfig.DeleteAttachments)
			{
				calItem.AttachmentCollection.RemoveAll();
			}
			if (mailboxConfig.DeleteComments)
			{
				using (TextWriter textWriter = calItem.Body.OpenTextWriter(BodyFormat.TextPlain))
				{
					textWriter.Write(string.Empty);
				}
			}
			if (mailboxConfig.RemovePrivateProperty)
			{
				calItem.Sensitivity = Sensitivity.Normal;
			}
			if (mailboxConfig.DeleteSubject)
			{
				calItem.Subject = string.Empty;
			}
			if (mailboxConfig.AddOrganizerToSubject)
			{
				calItem.Subject = calItem.Organizer.DisplayName + " " + calItem.Subject;
			}
			ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Save calendar Item", new object[]
			{
				TraceContext.Get()
			});
			ConflictResolutionResult conflictResolutionResult = calItem.Save(SaveMode.ResolveConflicts);
			if (conflictResolutionResult != null && conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
			{
				throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(calItem.Id), conflictResolutionResult);
			}
			calItem.Load();
			ResourceBookingProcessing.TracerPfd.TracePfd<int, object, VersionedId>((long)itemStore.GetHashCode(), "PFD IWR {0} {1}: PostProcessing is completed on {2}.", 32535, TraceContext.Get(), mtgMessage.Id);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0004B894 File Offset: 0x00049A94
		private static void AcceptMeetingRequest(MailboxSession itemStore, MeetingMessage mtgMessage, CalendarItemBase calItem, CalendarConfiguration mailboxConfig, ResponseTextGenerator responseMessage)
		{
			ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: No Conflict. Accept", new object[]
			{
				TraceContext.Get()
			});
			Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_AAResourceBooked, null, new object[]
			{
				mtgMessage.Sender.EmailAddress.ToString(),
				itemStore.MailboxOwner.LegacyDn,
				((DateTime)calItem.StartTime).ToString(),
				((DateTime)calItem.EndTime).ToString()
			});
			if (ResourceBookingProcessing.CheckConditionToRespondRequest(itemStore, mtgMessage, calItem, responseMessage))
			{
				using (MeetingResponse meetingResponse = calItem.RespondToMeetingRequest(ResponseType.Accept, responseMessage.GetResponseSubjectPrefix(), null, null))
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Send Response.", new object[]
					{
						TraceContext.Get()
					});
					using (TextWriter textWriter = meetingResponse.Body.OpenTextWriter(BodyFormat.TextHtml))
					{
						textWriter.Write(responseMessage.GetResponseBody());
					}
					meetingResponse.Send();
				}
				ResourceBookingPerfmon.RequestsProcessed.Increment();
				ResourceBookingPerfmon.Accepted.Increment();
				RbaLog.LogEntry(itemStore, mtgMessage, EvaluationResults.Accept);
				ResourceBookingProcessing.TracerPfd.TracePfd<int, object, VersionedId>((long)itemStore.GetHashCode(), "PFD IWR {0} {1}: Accepted Meeting {2}", 18199, TraceContext.Get(), mtgMessage.Id);
			}
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0004BA38 File Offset: 0x00049C38
		private static void TentativeMeetingRequest(MailboxSession itemStore, MeetingMessage mtgMessage, CalendarItemBase calItem, CalendarConfiguration mailboxConfig, ResponseTextGenerator responseBody)
		{
			ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Tentative", new object[]
			{
				TraceContext.Get()
			});
			if (mailboxConfig.ForwardRequestsToDelegates)
			{
				ResourceBookingProcessing.CreateDelegateForward(itemStore, mtgMessage, responseBody);
				responseBody.DelegateCultureInfo = responseBody.CultureInfo;
			}
			if (ResourceBookingProcessing.CheckConditionToRespondRequest(itemStore, mtgMessage, calItem, responseBody))
			{
				using (MeetingResponse meetingResponse = calItem.RespondToMeetingRequest(ResponseType.Tentative))
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Send Response.", new object[]
					{
						TraceContext.Get()
					});
					responseBody.GenerateAcknowledgementResponse();
					using (TextWriter textWriter = meetingResponse.Body.OpenTextWriter(BodyFormat.TextHtml))
					{
						textWriter.Write(responseBody.GetResponseBody());
					}
					meetingResponse.Send();
				}
				ResourceBookingPerfmon.RequestsProcessed.Increment();
				RbaLog.LogEntry(itemStore, mtgMessage, EvaluationResults.Tentative);
				ResourceBookingProcessing.TracerPfd.TracePfd<int, object, VersionedId>((long)itemStore.GetHashCode(), "PFD IWR {0} {1}: Tentative response {2}", 26391, TraceContext.Get(), mtgMessage.Id);
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0004BB60 File Offset: 0x00049D60
		private static void CreateDelegateForward(MailboxSession itemStore, MeetingMessage mtgMessage, ResponseTextGenerator responseBody)
		{
			List<Participant> unsentResourceDelegates = ResourceBookingProcessing.GetUnsentResourceDelegates(itemStore);
			if (unsentResourceDelegates != null && unsentResourceDelegates.Count > 0)
			{
				foreach (Participant participant in unsentResourceDelegates)
				{
					if (participant.Origin is DirectoryParticipantOrigin)
					{
						DirectoryParticipantOrigin directoryParticipantOrigin = participant.Origin as DirectoryParticipantOrigin;
						IExchangePrincipal principal = directoryParticipantOrigin.Principal;
						if (principal.PreferredCultures == null || !principal.PreferredCultures.Any<CultureInfo>())
						{
							responseBody.DelegateCultureInfo = responseBody.CultureInfo;
						}
						else
						{
							responseBody.DelegateCultureInfo = principal.PreferredCultures.First<CultureInfo>();
						}
					}
					ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(mtgMessage.Body.Format);
					replyForwardConfiguration.ForwardCreationFlags = ForwardCreationFlags.ResourceDelegationMessage;
					replyForwardConfiguration.Culture = responseBody.DelegateCultureInfo;
					replyForwardConfiguration.AddBodyPrefix(responseBody.GetDelegateBody(), BodyInjectionFormat.Html);
					using (MessageItem messageItem = mtgMessage.CreateForward(itemStore.GetDefaultFolderId(DefaultFolderType.Outbox), replyForwardConfiguration))
					{
						messageItem.Recipients.Add(participant);
						messageItem[MessageItemSchema.ReceivedRepresentingEmailAddress] = itemStore.MailboxOwner.LegacyDn;
						messageItem[MessageItemSchema.ReceivedRepresentingAddressType] = "EX";
						messageItem.Send();
					}
				}
			}
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0004BCD8 File Offset: 0x00049ED8
		private static void DeclineMeetingRequest(MailboxSession itemStore, MeetingMessage mtgMessage, CalendarItemBase calItem, ResponseTextGenerator responseMessage)
		{
			ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Conflict. Decline", new object[]
			{
				TraceContext.Get()
			});
			string arg = mtgMessage.Id.ToString();
			StoreObjectId storeObjectId = null;
			if (calItem.Id != null)
			{
				storeObjectId = calItem.Id.ObjectId;
			}
			if (ResourceBookingProcessing.CheckConditionToRespondRequest(itemStore, mtgMessage, calItem, responseMessage))
			{
				using (MeetingResponse meetingResponse = calItem.RespondToMeetingRequest(ResponseType.Decline, responseMessage.GetResponseSubjectPrefix(), null, null))
				{
					using (TextWriter textWriter = meetingResponse.Body.OpenTextWriter(BodyFormat.TextHtml))
					{
						textWriter.Write(responseMessage.GetResponseBody());
					}
					meetingResponse.Send();
				}
				ResourceBookingPerfmon.RequestsProcessed.Increment();
				ResourceBookingPerfmon.Declined.Increment();
			}
			ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Delete the calendar item", new object[]
			{
				TraceContext.Get()
			});
			RbaLog.LogEntry(itemStore, mtgMessage, EvaluationResults.Decline);
			if (storeObjectId == null)
			{
				calItem.Load();
				storeObjectId = calItem.Id.ObjectId;
			}
			itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
			{
				storeObjectId
			});
			ResourceBookingProcessing.TracerPfd.TracePfd<int, object, string>((long)itemStore.GetHashCode(), "PFD IWR  {0} {1}: Decline the meeting request {1} ", 22295, TraceContext.Get(), arg);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0004BE50 File Offset: 0x0004A050
		private static void ProcessMeetingCancellation(MailboxSession itemStore, MeetingCancellation cancellation, CalendarItemBase calItem, CalendarConfiguration calendarConfig)
		{
			ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Cancelation.", new object[]
			{
				TraceContext.Get()
			});
			StoreId id = calItem.Id;
			StoreId id2 = cancellation.Id;
			BookingRoles bookingRoles = BookingRoles.NoRole;
			ResponseTextGenerator responseBody = null;
			List<AdjacencyOrConflictInfo> list = null;
			EvaluationResults evaluationResults;
			try
			{
				evaluationResults = ResourceBookingProcessing.Evaluate(itemStore, cancellation, calItem, calendarConfig, out responseBody, out list, out bookingRoles);
			}
			catch (WorkingHoursXmlMalformedException)
			{
				ResourceBookingProcessing.Tracer.TraceError((long)itemStore.GetHashCode(), "{0}: The working hours configuration is corrupt. Warning the organizer.", new object[]
				{
					TraceContext.Get()
				});
				evaluationResults = EvaluationResults.Message;
			}
			if (cancellation.TryUpdateCalendarItem(ref calItem, false))
			{
				ConflictResolutionResult conflictResolutionResult = calItem.Save(SaveMode.ResolveConflicts);
				if (conflictResolutionResult != null && conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
				{
					throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(id), conflictResolutionResult);
				}
			}
			if (evaluationResults == EvaluationResults.Tentative && calendarConfig.ForwardRequestsToDelegates)
			{
				ResourceBookingProcessing.CreateDelegateForward(itemStore, cancellation, responseBody);
			}
			itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
			{
				id
			});
			itemStore.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
			{
				id2
			});
			RbaLog.LogEntry(itemStore, cancellation, EvaluationResults.Cancel);
			ResourceBookingPerfmon.RequestsProcessed.Increment();
			ResourceBookingPerfmon.Cancelled.Increment();
			ResourceBookingProcessing.TracerPfd.TracePfd<int, object, StoreId>((long)itemStore.GetHashCode(), "PFD IWR {0} {1}: Cancelation successful {2}", 30487, TraceContext.Get(), id);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0004BFA0 File Offset: 0x0004A1A0
		private static CultureInfo GetMesssageCultureInfo(Item item)
		{
			CultureInfo cultureInfo = null;
			object obj = item.TryGetProperty(MessageItemSchema.MessageLocaleId);
			if (obj != null && obj is int)
			{
				int num = (int)obj;
				if (num != 0)
				{
					try
					{
						cultureInfo = new CultureInfo(num);
						cultureInfo = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
					}
					catch (ArgumentException)
					{
						cultureInfo = null;
					}
				}
			}
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			return cultureInfo;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0004C004 File Offset: 0x0004A204
		private static List<MeetingConflict> AdjacencyOrConflictInfoToMeetingConflict(MailboxSession session, List<AdjacencyOrConflictInfo> conflictInfo)
		{
			List<MeetingConflict> list = new List<MeetingConflict>();
			if (conflictInfo == null || conflictInfo.Count == 0)
			{
				return null;
			}
			foreach (AdjacencyOrConflictInfo adjacencyOrConflictInfo in conflictInfo)
			{
				if (adjacencyOrConflictInfo.OccurrenceInfo == null)
				{
					ResourceBookingProcessing.Tracer.TraceError<object, string>((long)session.GetHashCode(), "{0}: OccurrenceInfo is null for conflict {1}. Skipping this conflict.", TraceContext.Get(), (adjacencyOrConflictInfo.Subject == null) ? "Unknown" : adjacencyOrConflictInfo.Subject);
				}
				else
				{
					MeetingConflict meetingConflict = new MeetingConflict();
					meetingConflict.MeetingStartTimeLocal = DateTime.SpecifyKind((DateTime)adjacencyOrConflictInfo.OccurrenceInfo.StartTime, DateTimeKind.Local);
					meetingConflict.MeetingEndTimeLocal = DateTime.SpecifyKind((DateTime)adjacencyOrConflictInfo.OccurrenceInfo.EndTime, DateTimeKind.Local);
					CalendarItemBase calendarItemBase = null;
					try
					{
						calendarItemBase = CalendarItemBase.Bind(session, adjacencyOrConflictInfo.OccurrenceInfo.VersionedId);
						if (calendarItemBase.Organizer == null)
						{
							ResourceBookingProcessing.Tracer.TraceError<object, string>((long)session.GetHashCode(), "{0}: Meeting retrieved from conflict information does not have an organizer. Skipping this conflict.", TraceContext.Get(), (adjacencyOrConflictInfo.Subject == null) ? "Unknown" : adjacencyOrConflictInfo.Subject);
							continue;
						}
						ADRecipient adrecipient = null;
						if (!string.IsNullOrEmpty(calendarItemBase.Organizer.RoutingType))
						{
							if (string.Compare(calendarItemBase.Organizer.RoutingType, "EX", true) != 0)
							{
								goto IL_1F7;
							}
							try
							{
								IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(session.InternalMailboxOwner.MailboxInfo.OrganizationId), 1563, "AdjacencyOrConflictInfoToMeetingConflict", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\resourcebooking\\ResourceBookingProcessing.cs");
								adrecipient = tenantOrRootOrgRecipientSession.FindByLegacyExchangeDN(calendarItemBase.Organizer.EmailAddress);
								if (adrecipient != null)
								{
									meetingConflict.OrganizerDisplay = adrecipient.DisplayName;
									meetingConflict.OrganizerSmtp = adrecipient.PrimarySmtpAddress.ToString();
								}
								goto IL_1F7;
							}
							catch (NonUniqueRecipientException)
							{
								Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_RBANonUniqueLegacyDN, null, new object[]
								{
									calendarItemBase.Organizer.EmailAddress
								});
								goto IL_1F7;
							}
						}
						ResourceBookingProcessing.Tracer.TraceError((long)session.GetHashCode(), "{0}: Organizer does not have a Routing type.", new object[]
						{
							TraceContext.Get()
						});
						IL_1F7:
						if (adrecipient == null)
						{
							meetingConflict.OrganizerDisplay = calendarItemBase.Organizer.DisplayName;
							meetingConflict.OrganizerSmtp = calendarItemBase.Organizer.EmailAddress;
						}
					}
					finally
					{
						if (calendarItemBase != null)
						{
							calendarItemBase.Dispose();
						}
					}
					list.Add(meetingConflict);
				}
			}
			list.Sort();
			return list;
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0004C2D0 File Offset: 0x0004A4D0
		private static List<Participant> GetUnsentResourceDelegates(MailboxSession itemSession)
		{
			List<Participant> list = null;
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(itemSession.InternalMailboxOwner.MailboxInfo.OrganizationId), 1630, "GetUnsentResourceDelegates", "f:\\15.00.1497\\sources\\dev\\MailboxAssistants\\src\\assistants\\resourcebooking\\ResourceBookingProcessing.cs");
			ADRecipient adrecipient = tenantOrRootOrgRecipientSession.FindByLegacyExchangeDN(itemSession.MailboxOwnerLegacyDN);
			if (adrecipient != null)
			{
				List<ADObjectId> list2 = new List<ADObjectId>();
				foreach (ADObjectId item in adrecipient.GrantSendOnBehalfTo)
				{
					list2.Add(item);
				}
				if (list2.Count <= 0)
				{
					ResourceBookingProcessing.Tracer.TraceDebug((long)itemSession.GetHashCode(), "{0}: Mailbox has no delegates.", new object[]
					{
						TraceContext.Get()
					});
					return null;
				}
				Result<ADRecipient>[] array = tenantOrRootOrgRecipientSession.ReadMultiple(list2.ToArray());
				list = new List<Participant>();
				foreach (Result<ADRecipient> result in array)
				{
					if (result.Error == null)
					{
						ADUser aduser = result.Data as ADUser;
						ExchangePrincipal exchangePrincipal = null;
						if (aduser != null)
						{
							try
							{
								exchangePrincipal = ExchangePrincipal.FromADUser(aduser, null);
							}
							catch (UserHasNoMailboxException)
							{
								if (aduser.RecipientType == RecipientType.User)
								{
									goto IL_156;
								}
							}
						}
						list.Add((exchangePrincipal != null) ? new Participant(exchangePrincipal) : new Participant(result.Data.DisplayName, result.Data.LegacyExchangeDN, "EX"));
					}
					IL_156:;
				}
				MapiStore _ContainedMapiStore = itemSession.__ContainedMapiStore;
				using (MapiFolder inboxFolder = _ContainedMapiStore.GetInboxFolder())
				{
					Rule[] rules = inboxFolder.GetRules(new PropTag[0]);
					foreach (Rule rule in rules)
					{
						if ((rule.StateFlags & RuleStateFlags.Enabled) != (RuleStateFlags)0)
						{
							foreach (RuleAction ruleAction in rule.Actions)
							{
								if (ruleAction is RuleAction.Delegate)
								{
									RuleAction.Delegate @delegate = ruleAction as RuleAction.Delegate;
									foreach (AdrEntry adrEntry in @delegate.Recipients)
									{
										foreach (PropValue propValue in adrEntry.Values)
										{
											if (propValue.PropTag == PropTag.EmailAddress)
											{
												string dn = propValue.GetString();
												list.RemoveAll((Participant p) => string.Compare(p.EmailAddress, dn, true) == 0);
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0004C5B8 File Offset: 0x0004A7B8
		private static ExDateTime GetEndOfBookingWindowLocal(ExTimeZone resourceTz, int bookingWindowInDays)
		{
			ExDateTime exDateTime = ExDateTime.GetNow(resourceTz).Date.AddDays((double)(bookingWindowInDays + 1));
			return ExTimeZone.CurrentTimeZone.ConvertDateTime(exDateTime);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0004C5EC File Offset: 0x0004A7EC
		private static bool CheckConditionToRespondRequest(MailboxSession itemStore, MeetingMessage mtgMessage, CalendarItemBase calItem, ResponseTextGenerator responseMessage)
		{
			bool result = true;
			if (responseMessage.CultureInfo.IsNeutralCulture)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: Neutral culture encountered, we cannot send a response", new object[]
				{
					TraceContext.Get()
				});
				Globals.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_RBANeutralCultureEncountered, null, new object[]
				{
					itemStore.MailboxOwner.LegacyDn,
					responseMessage.CultureInfo.Name,
					mtgMessage.Sender.EmailAddress.ToString(),
					mtgMessage.Subject
				});
				ResourceBookingPerfmon.RequestsFailed.Increment();
				result = false;
			}
			else if (calItem.IsCancelled)
			{
				ResourceBookingProcessing.Tracer.TraceDebug((long)itemStore.GetHashCode(), "{0}: meeting is in Canceled state, we cannot send a response", new object[]
				{
					TraceContext.Get()
				});
				ResourceBookingPerfmon.RequestsFailed.Increment();
				result = false;
			}
			return result;
		}

		// Token: 0x0400072F RID: 1839
		private static readonly Trace Tracer = ExTraceGlobals.ResourceBookingProcessingTracer;

		// Token: 0x04000730 RID: 1840
		private static readonly Trace TracerPfd = ExTraceGlobals.PFDTracer;
	}
}
