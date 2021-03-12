using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.QueueViewer;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Tracking;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002AD RID: 685
	internal class GetMessageTrackingReportImpl
	{
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001318 RID: 4888 RVA: 0x0005745E File Offset: 0x0005565E
		public TrackingErrorCollection Errors
		{
			get
			{
				return this.directoryContext.Errors;
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0005746C File Offset: 0x0005566C
		internal GetMessageTrackingReportImpl(DirectoryContext directoryContext, SearchScope scope, MessageTrackingReportId messageTrackingReportId, LogCache logCache, ReportConstraints constraints)
		{
			TraceWrapper.SearchLibraryTracer.TraceDebug<MessageTrackingReportId>(this.GetHashCode(), "Getting report for: {0}", messageTrackingReportId);
			this.directoryContext = directoryContext;
			this.defaultDomain = ServerCache.Instance.GetDefaultDomain(this.directoryContext.OrganizationId);
			string fqdn = ServerCache.Instance.GetLocalServer().Fqdn;
			this.trackingDiscovery = new TrackingDiscovery(directoryContext);
			this.scope = scope;
			this.messageTrackingReportId = messageTrackingReportId;
			this.userCulture = Thread.CurrentThread.CurrentCulture;
			this.trackingContext = new TrackingContext(logCache ?? new LogCache(DateTime.MinValue, DateTime.MaxValue, this.directoryContext.TrackingBudget), this.directoryContext, this.messageTrackingReportId);
			this.trackingContext.ReportTemplate = constraints.ReportTemplate;
			if (constraints.ReportTemplate == ReportTemplate.RecipientPath)
			{
				this.trackingContext.SelectedRecipient = constraints.RecipientPathFilter[0].ToString();
			}
			this.findAdditionalRecords = new WSAdditionalRecords<FindParameters, GetMessageTrackingReportImpl.FindCachedItem>(new QueryMethod<FindParameters, GetMessageTrackingReportImpl.FindCachedItem>(this.FindMessageReceiveBasic));
			this.getAdditionalRecords = new WSAdditionalRecords<WSGetParameters, WSGetResult>(new QueryMethod<WSGetParameters, WSGetResult>(this.WSGetTrackingReportBasic));
			this.constraints = constraints;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000575B8 File Offset: 0x000557B8
		internal MessageTrackingReport Execute()
		{
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportExecuted.Increment();
			uint budgetUsed = this.directoryContext.TrackingBudget.BudgetUsed;
			RecipientEventData recipientEventData = null;
			TimeSpan elapsed = this.directoryContext.TrackingBudget.Elapsed;
			try
			{
				TrackingAuthority trackingAuthority = null;
				RecipientEventData eventsForSenderDomain = this.GetEventsForSenderDomain(out trackingAuthority);
				if (eventsForSenderDomain == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No events returned for sender domain", new object[0]);
					return null;
				}
				if (this.constraints.ReportTemplate == ReportTemplate.Summary)
				{
					ReferralEvaluator referralEvaluator = new ReferralEvaluator(this.directoryContext, new ReferralEvaluator.TryProcessReferralMethod(this.TryProcessReferral), new ReferralEvaluator.GetAuthorityAndRemapReferralMethod(this.GetAuthorityAndRemapReferral), string.Empty, this.scope);
					IEnumerable<List<RecipientTrackingEvent>> initialEvents = GetMessageTrackingReportImpl.ConvertSummaryEventsToPaths(eventsForSenderDomain.Events);
					referralEvaluator.Evaluate(initialEvents, trackingAuthority.TrackingAuthorityKind);
					List<RecipientTrackingEvent> leaves = referralEvaluator.GetLeaves();
					if (leaves != null && leaves.Count != 0)
					{
						recipientEventData = new RecipientEventData(leaves);
					}
				}
				else if (this.constraints.ReportTemplate == ReportTemplate.RecipientPath)
				{
					string text = this.constraints.RecipientPathFilter[0].ToString();
					ReferralEvaluator referralEvaluator2 = new ReferralEvaluator(this.directoryContext, new ReferralEvaluator.TryProcessReferralMethod(this.TryProcessReferral), new ReferralEvaluator.GetAuthorityAndRemapReferralMethod(this.GetAuthorityAndRemapReferral), text, this.scope);
					IEnumerable<List<RecipientTrackingEvent>> initialEvents2 = GetMessageTrackingReportImpl.ConvertRecipientPathModeEventsToPaths(eventsForSenderDomain);
					referralEvaluator2.Evaluate(initialEvents2, trackingAuthority.TrackingAuthorityKind);
					recipientEventData = referralEvaluator2.GetEventDataForRecipient(text);
				}
				if (recipientEventData == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No events after following referrals", new object[0]);
					return null;
				}
			}
			finally
			{
				long incrementValue = (long)(this.directoryContext.TrackingBudget.Elapsed - elapsed).TotalMilliseconds;
				InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportProcessingTime.IncrementBy(incrementValue);
				this.directoryContext.TrackingBudget.RestoreBudgetTo(budgetUsed);
			}
			return new MessageTrackingReport(this.messageTrackingReportId, this.submittedDateTime, this.subject, this.fromAddress, this.fromDisplayName, this.submissionRecipientAddresses, this.submissionRecipientDisplayNames, recipientEventData);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000577D8 File Offset: 0x000559D8
		private RecipientEventData GetEventsForSenderDomain(out TrackingAuthority authority)
		{
			bool flag = false;
			authority = null;
			try
			{
				authority = this.trackingDiscovery.FindLocationByDomainAndServer(this.messageTrackingReportId.Domain, this.messageTrackingReportId.Server, SmtpAddress.Empty, false, out flag);
			}
			catch (TrackingTransientException)
			{
			}
			catch (TrackingFatalException)
			{
			}
			if (authority == null)
			{
				return null;
			}
			if (!authority.IsAllowedScope(this.scope))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, string>(this.GetHashCode(), "ReportID's domain and server authority {0} is out of the current scope {1}", Names<TrackingAuthorityKind>.Map[(int)authority.TrackingAuthorityKind], Names<SearchScope>.Map[(int)this.scope]);
				return null;
			}
			if (authority.TrackingAuthorityKind == TrackingAuthorityKind.CurrentSite)
			{
				return this.RpcGetTrackingReport(this.messageTrackingReportId);
			}
			if (authority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteSiteInCurrentOrg || authority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteTrustedOrg || authority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteForest)
			{
				return this.GetEventsForRemoteSender(authority);
			}
			return null;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000578BC File Offset: 0x00055ABC
		private RecipientEventData GetEventsForRemoteSender(TrackingAuthority authority)
		{
			MessageTrackingReportType messageTrackingReportType = null;
			RecipientEventData recipientEventData = this.WSGetTrackingReport(this.messageTrackingReportId, (WebServiceTrackingAuthority)authority, out messageTrackingReportType);
			if (recipientEventData == null)
			{
				return null;
			}
			this.subject = messageTrackingReportType.Subject;
			EmailAddressType sender = messageTrackingReportType.Sender;
			if (sender != null)
			{
				this.fromAddress = new SmtpAddress?(SmtpAddress.Parse(messageTrackingReportType.Sender.EmailAddress));
				this.fromDisplayName = messageTrackingReportType.Sender.Name;
			}
			this.submittedDateTime = messageTrackingReportType.SubmitTime;
			EmailAddressType[] originalRecipients = messageTrackingReportType.OriginalRecipients;
			if (originalRecipients != null)
			{
				this.submissionRecipientAddresses = new SmtpAddress[originalRecipients.Length];
				this.submissionRecipientDisplayNames = new string[originalRecipients.Length];
				for (int i = 0; i < originalRecipients.Length; i++)
				{
					this.submissionRecipientAddresses[i] = SmtpAddress.Parse(originalRecipients[i].EmailAddress);
					this.submissionRecipientDisplayNames[i] = originalRecipients[i].Name;
				}
			}
			return recipientEventData;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000579A0 File Offset: 0x00055BA0
		private void GetRecipients(MessageTrackingLogEntry rootEvent)
		{
			if (this.constraints.ReportTemplate == ReportTemplate.RecipientPath)
			{
				return;
			}
			if (rootEvent.EventId == MessageTrackingEvent.SUBMIT && rootEvent.Source == MessageTrackingSource.STOREDRIVER)
			{
				if (rootEvent.RecipientAddresses == null || rootEvent.RecipientAddresses.Length == 0)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<int, string, string>(this.GetHashCode(), "SUBMIT had no recipients, eventId={0}, sender={1}, server={2}", (int)rootEvent.EventId, rootEvent.SenderAddress, rootEvent.Server);
					TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "No recipients for STOREDRIVER SUBMIT event from server {0}", new object[]
					{
						rootEvent.Server
					});
				}
				this.submissionRecipientAddresses = new SmtpAddress[rootEvent.RecipientAddresses.Length];
				this.submissionRecipientDisplayNames = rootEvent.RecipientAddresses;
				for (int i = 0; i < rootEvent.RecipientAddresses.Length; i++)
				{
					this.submissionRecipientAddresses[i] = SmtpAddress.Parse(rootEvent.RecipientAddresses[i]);
				}
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "The root event was not STOREDRIVER SUBMIT, it was: {0} {1}", Names<MessageTrackingSource>.Map[(int)rootEvent.Source], Names<MessageTrackingEvent>.Map[(int)rootEvent.EventId]);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00057AAC File Offset: 0x00055CAC
		private List<RecipientTrackingEvent> ConvertToRecipientTrackingEvent(List<MessageTrackingLogEntry> logEntries)
		{
			List<RecipientTrackingEvent> list = new List<RecipientTrackingEvent>(logEntries.Count);
			foreach (MessageTrackingLogEntry messageTrackingLogEntry in logEntries)
			{
				if (!messageTrackingLogEntry.IsEntryCompatible)
				{
					string exception = string.Format("Log entry on {0} for message-id {1} is not compatible with this server, skipping the rest of the entries", messageTrackingLogEntry.Server, messageTrackingLogEntry.MessageId);
					this.Errors.Add(ErrorCode.LogVersionIncompatible, messageTrackingLogEntry.Server, string.Empty, exception);
					break;
				}
				RecipientTrackingEvent recipientTrackingEvent = this.CreateRecipientTrackingEvent(messageTrackingLogEntry);
				if (recipientTrackingEvent != null)
				{
					list.Add(recipientTrackingEvent);
				}
			}
			return list;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00057B50 File Offset: 0x00055D50
		private RecipientTrackingEvent CreateRecipientTrackingEvent(MessageTrackingLogEntry logEntry)
		{
			GetMessageTrackingReportImpl.GetEventInfoMethod getEventInfoMethod = null;
			if (!this.directoryContext.IsTenantInScope(logEntry.TenantId))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, OrganizationId, string>(this.GetHashCode(), "row skipped for msg-id: {0}.  Getting recipient event for tenant {1}, but row is for {2}", logEntry.MessageId, this.directoryContext.OrganizationId, logEntry.TenantId);
				return null;
			}
			GetMessageTrackingReportImpl.EventInfo eventInfo;
			if (GetMessageTrackingReportImpl.rawEventToEventInfoGetter.TryGetValue(logEntry.EventId, out getEventInfoMethod))
			{
				eventInfo = getEventInfoMethod(logEntry, this);
			}
			else
			{
				GetMessageTrackingReportImpl.rawEventToEventInfo.TryGetValue(logEntry.EventId, out eventInfo);
			}
			if (eventInfo == null)
			{
				if (this.constraints.ReportTemplate != ReportTemplate.Summary)
				{
					return null;
				}
				eventInfo = GetMessageTrackingReportImpl.generalPendingEventInfo;
			}
			EventType eventType = eventInfo.EventType;
			DeliveryStatus deliveryStatus = eventInfo.DeliveryStatus;
			EventDescription eventDescription = eventInfo.EventDescription;
			if (logEntry.RecipientAddresses == null || logEntry.RecipientAddresses.Length == 0)
			{
				return null;
			}
			SmtpAddress recipientAddress = SmtpAddress.Parse(logEntry.RecipientAddress);
			string[] eventData = null;
			GetMessageTrackingReportImpl.GetEventDataMethod getEventDataMethod = null;
			if (GetMessageTrackingReportImpl.eventDataGetters.TryGetValue(eventDescription, out getEventDataMethod))
			{
				eventData = getEventDataMethod(logEntry, this);
			}
			string text = string.Empty;
			if (eventDescription == EventDescription.Submitted)
			{
				text = ((!string.IsNullOrEmpty(logEntry.ClientHostName)) ? logEntry.ClientHostName : logEntry.ClientIP);
			}
			return new RecipientTrackingEvent(this.defaultDomain, recipientAddress, logEntry.RecipientAddress, deliveryStatus, eventType, eventDescription, eventData, (!string.IsNullOrEmpty(text)) ? text : logEntry.Server, logEntry.Time, logEntry.InternalMessageId, null, logEntry.HiddenRecipient, logEntry.BccRecipient, logEntry.RootAddress, (string)logEntry.ArbitrationMailboxAddress, logEntry.InitMessageId);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00057CC8 File Offset: 0x00055EC8
		private void AppendRichTrackingDataIfNeeded(RecipientEventData recipientEventData, List<MessageTrackingLogEntry> rawEvents)
		{
			if (recipientEventData == null)
			{
				return;
			}
			List<RecipientTrackingEvent> events = recipientEventData.Events;
			if (events != null)
			{
				MessageTrackingLogEntry messageTrackingLogEntry = rawEvents[rawEvents.Count - 1];
				RecipientTrackingEvent recipEvent = events[events.Count - 1];
				if (messageTrackingLogEntry.EventId == MessageTrackingEvent.HAREDIRECT)
				{
					if (events.Count <= 1)
					{
						return;
					}
					messageTrackingLogEntry = rawEvents[rawEvents.Count - 2];
					recipEvent = events[events.Count - 2];
				}
				TrackedUser trackedUser = TrackedUser.Create(messageTrackingLogEntry.RecipientAddress, this.directoryContext.TenantGalSession);
				if (trackedUser == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Recipient: {0} lookup error, queue/inbox/read-status data may be incorrect (we will do our best)", messageTrackingLogEntry.RecipientAddress);
					trackedUser = TrackedUser.CreateUnresolved(messageTrackingLogEntry.RecipientAddress);
				}
				this.AppendQueueViewerDiagnosticIfNeeded(trackedUser, messageTrackingLogEntry, events);
				this.AppendInboxRuleEventIfNeeded(trackedUser, messageTrackingLogEntry, events);
				this.AppendInboxRuleForwardToDelegateEventIfNeeded(trackedUser, messageTrackingLogEntry, events);
				this.AppendRecipientReadStatusIfNeeded(trackedUser, recipEvent, events);
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00057DA0 File Offset: 0x00055FA0
		private void AppendQueueViewerDiagnosticIfNeeded(TrackedUser recipient, MessageTrackingLogEntry lastRawEvent, List<RecipientTrackingEvent> events)
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.MessageTracking.QueueViewerDiagnostics.Enabled)
			{
				return;
			}
			if (!this.constraints.ReturnQueueEvents || this.constraints.ReportTemplate != ReportTemplate.RecipientPath)
			{
				return;
			}
			if (lastRawEvent.EventId == MessageTrackingEvent.DELIVER || lastRawEvent.EventId == MessageTrackingEvent.POISONMESSAGE || lastRawEvent.EventId == MessageTrackingEvent.FAIL || lastRawEvent.EventId == MessageTrackingEvent.SEND || lastRawEvent.EventId == MessageTrackingEvent.INITMESSAGECREATED)
			{
				return;
			}
			if (lastRawEvent.InternalMessageId == 0L)
			{
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, int>(this.GetHashCode(), "Getting queue status for {0}. Last event was: {1}", lastRawEvent.RecipientAddress, (int)lastRawEvent.EventId);
			string text;
			DateTime? dateTime;
			string text2;
			bool flag;
			using (QueueClient queueClient = new QueueClient(lastRawEvent.Server, this.directoryContext))
			{
				RecipientStatus? recipientStatus;
				DateTime? dateTime2;
				if (!queueClient.GetRecipientStatus(lastRawEvent.InternalMessageId, lastRawEvent.MessageId, lastRawEvent.RecipientAddress, out recipientStatus, out text, out dateTime, out dateTime2, out text2, out flag))
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No status available", new object[0]);
					return;
				}
			}
			EventDescription eventDescription;
			string[] eventData;
			if (flag)
			{
				if (text == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "No last error for unreachable queue", new object[0]);
					return;
				}
				eventDescription = EventDescription.QueueRetryNoRetryTime;
				eventData = new string[]
				{
					text2,
					text
				};
			}
			else
			{
				if (text == null || dateTime == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Missing last-error, last-retry or next-retry for queue: {0}", text2);
					return;
				}
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Got queue diagnostic: {0}", text);
				eventData = new string[]
				{
					lastRawEvent.Server,
					GetMessageTrackingReportImpl.FormatDateTime(lastRawEvent.Time.ToLocalTime(), this.userCulture),
					GetMessageTrackingReportImpl.FormatDateTime(dateTime.Value.ToLocalTime(), this.userCulture),
					ExTimeZone.CurrentTimeZone.LocalizableDisplayName.ToString(this.userCulture),
					text
				};
				eventDescription = EventDescription.QueueRetry;
			}
			RecipientTrackingEvent item = new RecipientTrackingEvent(this.defaultDomain, recipient.SmtpAddress, recipient.DisplayName, DeliveryStatus.Pending, EventType.Pending, eventDescription, eventData, lastRawEvent.Server, lastRawEvent.Time, lastRawEvent.InternalMessageId, null, lastRawEvent.HiddenRecipient, lastRawEvent.BccRecipient, lastRawEvent.RootAddress, null, null);
			events.Add(item);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x00058000 File Offset: 0x00056200
		private void AppendInboxRuleEventIfNeeded(TrackedUser recipient, MessageTrackingLogEntry lastEntry, List<RecipientTrackingEvent> events)
		{
			if (this.constraints.ReportTemplate != ReportTemplate.RecipientPath || lastEntry.EventId != MessageTrackingEvent.DELIVER || string.IsNullOrEmpty(lastEntry.Folder))
			{
				return;
			}
			RecipientTrackingEvent item = new RecipientTrackingEvent(this.defaultDomain, recipient.SmtpAddress, recipient.DisplayName, DeliveryStatus.Delivered, EventType.Deliver, EventDescription.MovedToFolderByInboxRule, new string[]
			{
				lastEntry.Folder
			}, lastEntry.Server, lastEntry.Time, lastEntry.InternalMessageId, null, lastEntry.HiddenRecipient, lastEntry.BccRecipient, lastEntry.RootAddress, null, null);
			events.Add(item);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0005808C File Offset: 0x0005628C
		private void AppendInboxRuleForwardToDelegateEventIfNeeded(TrackedUser recipient, MessageTrackingLogEntry lastEntry, List<RecipientTrackingEvent> events)
		{
			if (this.constraints.ReportTemplate != ReportTemplate.RecipientPath || lastEntry.EventId != MessageTrackingEvent.PROCESS || !string.Equals("Mailbox Rules Agent.DelegateAccess", lastEntry.RecipientStatus, StringComparison.Ordinal))
			{
				return;
			}
			RecipientTrackingEvent item = new RecipientTrackingEvent(this.defaultDomain, recipient.SmtpAddress, recipient.DisplayName, DeliveryStatus.Delivered, EventType.Deliver, EventDescription.ForwardedToDelegateAndDeleted, null, lastEntry.Server, lastEntry.Time, lastEntry.InternalMessageId, null, lastEntry.HiddenRecipient, lastEntry.BccRecipient, lastEntry.RootAddress, null, null);
			events.Add(item);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00058110 File Offset: 0x00056310
		private void AppendRecipientReadStatusIfNeeded(TrackedUser trackedUser, RecipientTrackingEvent recipEvent, List<RecipientTrackingEvent> events)
		{
			if (this.constraints.ReportTemplate != ReportTemplate.RecipientPath || recipEvent.EventDescription != EventDescription.Delivered)
			{
				return;
			}
			if (!ServerCache.Instance.ReadStatusReportingEnabled(this.directoryContext) || !trackedUser.ReadStatusTrackingEnabled || trackedUser.ADUser == null || !trackedUser.IsMailbox)
			{
				return;
			}
			ExchangePrincipal mailboxOwner = null;
			try
			{
				mailboxOwner = ExchangePrincipal.FromADUser(this.directoryContext.TenantConfigSession.SessionSettings, trackedUser.ADUser, RemotingOptions.AllowCrossSite);
			}
			catch (ObjectNotFoundException arg)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress, ObjectNotFoundException>(this.GetHashCode(), "Cannot get ExchangePrincipal for recipient: {0}, will not get read status. Exception: {1}", recipEvent.RecipientAddress, arg);
				return;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress, string>(this.GetHashCode(), "Checking READ status for recipient: {0}, Message-Id: {1}", recipEvent.RecipientAddress, this.messageTrackingReportId.MessageId);
			try
			{
				using (MailboxSession mailboxSession = MailboxSession.OpenAsAdmin(mailboxOwner, CultureInfo.InvariantCulture, "Client=ELC;Action=MessageTracking"))
				{
					IStorePropertyBag[] array = AllItemsFolderHelper.FindItemsFromInternetId(mailboxSession, this.messageTrackingReportId.MessageId, GetMessageTrackingReportImpl.readStatusProperties);
					if (array == null || array.Length == 0)
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "No matching message found for recipient", new object[0]);
					}
					else
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug<int>(this.GetHashCode(), "Found {0} matching messages", array.Length);
						foreach (IStorePropertyBag storePropertyBag in array)
						{
							if (storePropertyBag.TryGetProperty(MessageItemSchema.TransportMessageHeaders) is PropertyError)
							{
								TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Skipping a sent item copy.", new object[0]);
							}
							else
							{
								object obj = storePropertyBag.TryGetProperty(MessageItemSchema.Flags);
								int num = (int)obj;
								TraceWrapper.SearchLibraryTracer.TraceDebug<int>(this.GetHashCode(), "PR_MESSAGE_FLAGS = {0:x}", num);
								if ((num & 1024) == 1024)
								{
									RecipientTrackingEvent item = new RecipientTrackingEvent(recipEvent.Domain, recipEvent.RecipientAddress, recipEvent.RecipientDisplayName, recipEvent.Status, recipEvent.EventType, EventDescription.Read, null, recipEvent.Server, DateTime.MinValue, 0L, null, recipEvent.HiddenRecipient, new bool?(recipEvent.BccRecipient), recipEvent.RootAddress, null, null);
									events.Add(item);
									return;
								}
							}
						}
						RecipientTrackingEvent item2 = new RecipientTrackingEvent(recipEvent.Domain, recipEvent.RecipientAddress, recipEvent.RecipientDisplayName, recipEvent.Status, recipEvent.EventType, EventDescription.NotRead, null, recipEvent.Server, DateTime.MinValue, 0L, null, recipEvent.HiddenRecipient, new bool?(recipEvent.BccRecipient), recipEvent.RootAddress, null, null);
						events.Add(item2);
					}
				}
			}
			catch (StoragePermanentException ex)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<StoragePermanentException>(this.GetHashCode(), "Store permanent exception: {0}", ex);
				this.Errors.Add(ErrorCode.ReadStatusError, string.Empty, string.Empty, ex.ToString());
			}
			catch (StorageTransientException ex2)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<StorageTransientException>(this.GetHashCode(), "Store transient exception: {0}", ex2);
				this.Errors.Add(ErrorCode.ReadStatusError, string.Empty, string.Empty, ex2.ToString());
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000584C4 File Offset: 0x000566C4
		private bool TryProcessReferral(RecipientTrackingEvent referralEvent, TrackingAuthority authority, out IEnumerable<List<RecipientTrackingEvent>> paths)
		{
			EventDescription eventDescription = referralEvent.EventDescription;
			paths = null;
			TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress, string, string>(this.GetHashCode(), "Processing referral for recipient: {0}, Event: {1}, authority kind {2}", referralEvent.RecipientAddress, Names<EventDescription>.Map[(int)eventDescription], Names<TrackingAuthorityKind>.Map[(int)authority.TrackingAuthorityKind]);
			MessageTrackingReportId receiveTrackingId = this.FindMessageReceive(referralEvent, authority);
			if (receiveTrackingId == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Did not find message entry point in remote tracking authority", new object[0]);
				return false;
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<MessageTrackingReportId>(this.GetHashCode(), "Found entrypoint in remote-tracking authority, ID: {0}", receiveTrackingId);
			RecipientEventData recipientEventData;
			if (receiveTrackingId == MessageTrackingReportId.LegacyExchange)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress>(this.GetHashCode(), "Converting Pending event into TransferredToLegacyExchangeServer event for recipient: {0}.", referralEvent.RecipientAddress);
				referralEvent.ConvertRecipientTrackingEvent(DeliveryStatus.Transferred, EventType.Transferred, EventDescription.TransferredToLegacyExchangeServer);
				recipientEventData = new RecipientEventData(new List<RecipientTrackingEvent>(0));
			}
			else
			{
				if (authority.TrackingAuthorityKind == TrackingAuthorityKind.Undefined)
				{
					bool serverNotFound = false;
					TrackingAuthority newAuthority = null;
					TrackingBaseException ex = this.TryExecuteTask(delegate
					{
						newAuthority = this.trackingDiscovery.FindLocationByDomainAndServer(receiveTrackingId.Domain, receiveTrackingId.Server, SmtpAddress.Empty, false, out serverNotFound);
					});
					if (ex != null)
					{
						TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Error in FindLocationByDomainAndServer during referral, aborting", new object[0]);
						return false;
					}
					if (serverNotFound)
					{
						string formatString = string.Format("Next hop server not found: {0}\\{1}", receiveTrackingId.Domain, receiveTrackingId.Server);
						TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), formatString, new object[0]);
						return false;
					}
					authority = newAuthority;
					TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Tracking authority modified: Undefined -> {0}", Names<TrackingAuthorityKind>.Map[(int)authority.TrackingAuthorityKind]);
				}
				recipientEventData = this.GetTrackingReport(receiveTrackingId, authority);
				if (recipientEventData == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Could not generate a tracking-report in remote-authority", new object[0]);
					return false;
				}
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Got tracking report", new object[0]);
			}
			if (this.constraints.ReportTemplate == ReportTemplate.Summary)
			{
				paths = GetMessageTrackingReportImpl.ConvertSummaryEventsToPaths(recipientEventData.Events);
			}
			else
			{
				paths = GetMessageTrackingReportImpl.ConvertRecipientPathModeEventsToPaths(recipientEventData);
			}
			return true;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x000586EC File Offset: 0x000568EC
		private TrackingAuthority GetAuthorityAndRemapReferral(ref RecipientTrackingEvent referralEvent)
		{
			EventDescription eventDescription = referralEvent.EventDescription;
			if ((eventDescription == EventDescription.SmtpSendCrossSite && referralEvent.EventData.Length != 2) || (eventDescription == EventDescription.SmtpSendCrossForest && referralEvent.EventData.Length != 3) || (eventDescription == EventDescription.TransferredToForeignOrg && referralEvent.EventData.Length != 1) || (eventDescription == EventDescription.SubmittedCrossSite && referralEvent.EventData.Length != 2))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string, int, string>(this.GetHashCode(), "Incorrect event-data count for {0}, there were {1} elements. Data is from server: {2}", Names<EventDescription>.Map[(int)eventDescription], referralEvent.EventData.Length, referralEvent.Server);
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "WS-Response Validation Error: Incorrect EventData count {0} in response for {1}", new object[]
				{
					referralEvent.EventData.Length,
					Names<EventDescription>.Map[(int)eventDescription]
				});
			}
			string text = null;
			string text2 = null;
			bool allowChildDomains = false;
			if (eventDescription == EventDescription.SmtpSendCrossSite)
			{
				if (referralEvent.EventData.Length != 2)
				{
					throw new InvalidOperationException();
				}
				text2 = referralEvent.Domain;
				text = referralEvent.EventData[1];
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Following cross-site SMTP send", new object[0]);
			}
			else if (eventDescription == EventDescription.SmtpSendCrossForest)
			{
				if (referralEvent.EventData.Length != 3)
				{
					throw new InvalidOperationException();
				}
				text2 = referralEvent.EventData[2];
				text = referralEvent.EventData[1];
				allowChildDomains = true;
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Following cross-forest SMTP send", new object[0]);
			}
			else if (eventDescription == EventDescription.TransferredToForeignOrg)
			{
				if (referralEvent.EventData.Length != 1)
				{
					throw new InvalidOperationException();
				}
				text2 = referralEvent.EventData[0];
				text = null;
				TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Following cross-org send", new object[0]);
			}
			else if (eventDescription == EventDescription.TransferredToPartnerOrg)
			{
				text = null;
				text2 = referralEvent.RecipientAddress.Domain;
			}
			else
			{
				if (eventDescription == EventDescription.SmtpSend)
				{
					return RemoteOrgTrackingAuthority.Instance;
				}
				if (eventDescription == EventDescription.PendingModeration)
				{
					if (string.IsNullOrEmpty(referralEvent.ExtendedProperties.ArbitrationMailboxAddress) || string.IsNullOrEmpty(referralEvent.ExtendedProperties.InitMessageId))
					{
						TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Arbitration address not available for PendingModeration event", new object[0]);
						return null;
					}
					return UndefinedTrackingAuthority.Instance;
				}
				else
				{
					if (eventDescription != EventDescription.SubmittedCrossSite)
					{
						return null;
					}
					text2 = referralEvent.Domain;
					text = referralEvent.EventData[1];
					TraceWrapper.SearchLibraryTracer.TraceDebug(this.GetHashCode(), "Following cross-site SD SUBMIT", new object[0]);
				}
			}
			TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(this.GetHashCode(), "NextHopServer={0}, NextHopDomain={1}", text, text2);
			TrackingAuthority trackingAuthority = null;
			bool flag = false;
			TrackingBaseException ex = null;
			try
			{
				trackingAuthority = this.trackingDiscovery.FindLocationByDomainAndServer(text2, text, referralEvent.RecipientAddress, allowChildDomains, out flag);
			}
			catch (TrackingTransientException ex2)
			{
				ex = ex2;
			}
			catch (TrackingFatalException ex3)
			{
				ex = ex3;
			}
			if ((eventDescription == EventDescription.SmtpSendCrossForest || eventDescription == EventDescription.TransferredToPartnerOrg) && ((trackingAuthority == null && flag) || trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.Undefined))
			{
				trackingAuthority = UndefinedTrackingAuthority.Instance;
			}
			else if (ex != null)
			{
				if (!ex.IsAlreadyLogged)
				{
					this.Errors.Errors.Add(ex.TrackingError);
				}
				return null;
			}
			this.RemapEventForAuthority(ref referralEvent, trackingAuthority, text2, text);
			if (trackingAuthority == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<SmtpAddress>(this.GetHashCode(), "Could not get a tracking authority for {0}", referralEvent.RecipientAddress);
				return null;
			}
			if (trackingAuthority.TrackingAuthorityKind != TrackingAuthorityKind.RemoteSiteInCurrentOrg && trackingAuthority.TrackingAuthorityKind != TrackingAuthorityKind.RemoteTrustedOrg && trackingAuthority.TrackingAuthorityKind != TrackingAuthorityKind.RemoteForest && trackingAuthority.TrackingAuthorityKind != TrackingAuthorityKind.CurrentSite && trackingAuthority.TrackingAuthorityKind != TrackingAuthorityKind.Undefined)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress, string>(this.GetHashCode(), "Cannot follow referral for {0}, authority kind is {1}", referralEvent.RecipientAddress, Names<TrackingAuthorityKind>.Map[(int)trackingAuthority.TrackingAuthorityKind]);
				return null;
			}
			return trackingAuthority;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00058A6C File Offset: 0x00056C6C
		private void RemapEventForAuthority(ref RecipientTrackingEvent referralEvent, TrackingAuthority trackingAuthority, string nextHopDomain, string nextHopServer)
		{
			if (referralEvent.EventDescription != EventDescription.TransferredToForeignOrg && referralEvent.EventDescription != EventDescription.TransferredToPartnerOrg && referralEvent.EventDescription != EventDescription.SmtpSendCrossForest && referralEvent.EventDescription != EventDescription.SmtpSendCrossSite && referralEvent.EventDescription != EventDescription.SmtpSend)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(this.GetHashCode(), "Referral event {0} does not need to be remapped", Names<EventDescription>.Map[(int)referralEvent.EventDescription]);
				return;
			}
			if (string.IsNullOrEmpty(nextHopServer))
			{
				nextHopServer = nextHopDomain;
			}
			if (trackingAuthority == null)
			{
				referralEvent.ConvertRecipientTrackingEvent(referralEvent.Status, referralEvent.EventType, EventDescription.TransferredToForeignOrg);
				referralEvent.EventData = new string[]
				{
					nextHopDomain
				};
				return;
			}
			if (trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.Undefined)
			{
				referralEvent.ConvertRecipientTrackingEvent(referralEvent.Status, referralEvent.EventType, EventDescription.SmtpSend);
				referralEvent.EventData = new string[]
				{
					referralEvent.Server,
					nextHopServer
				};
				return;
			}
			if (trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteForest)
			{
				referralEvent.ConvertRecipientTrackingEvent(referralEvent.Status, referralEvent.EventType, EventDescription.SmtpSendCrossForest);
				referralEvent.EventData = new string[]
				{
					referralEvent.Server,
					nextHopServer,
					nextHopDomain
				};
				return;
			}
			if (trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteTrustedOrg)
			{
				referralEvent.ConvertRecipientTrackingEvent(referralEvent.Status, referralEvent.EventType, EventDescription.TransferredToPartnerOrg);
				referralEvent.EventData = new string[0];
				return;
			}
			if (trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteSiteInCurrentOrg)
			{
				referralEvent.ConvertRecipientTrackingEvent(referralEvent.Status, referralEvent.EventType, EventDescription.SmtpSendCrossSite);
				referralEvent.EventData = new string[]
				{
					referralEvent.Server,
					nextHopServer
				};
				return;
			}
			if (trackingAuthority.TrackingAuthorityKind == TrackingAuthorityKind.CurrentSite)
			{
				referralEvent.ConvertRecipientTrackingEvent(referralEvent.Status, referralEvent.EventType, EventDescription.SmtpSend);
				referralEvent.EventData = new string[]
				{
					referralEvent.Server,
					nextHopServer
				};
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x00058C40 File Offset: 0x00056E40
		private RecipientEventData GetTrackingReport(MessageTrackingReportId reportId, TrackingAuthority authority)
		{
			if (authority.TrackingAuthorityKind == TrackingAuthorityKind.CurrentSite)
			{
				return this.RpcGetTrackingReport(reportId);
			}
			if (authority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteSiteInCurrentOrg || authority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteTrustedOrg || authority.TrackingAuthorityKind == TrackingAuthorityKind.RemoteForest)
			{
				MessageTrackingReportType messageTrackingReportType;
				return this.WSGetTrackingReport(reportId, (WebServiceTrackingAuthority)authority, out messageTrackingReportType);
			}
			return null;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00058C8C File Offset: 0x00056E8C
		private RecipientEventData RpcGetTrackingReport(MessageTrackingReportId reportId)
		{
			TrackingContext trackingContext = new TrackingContext(this.trackingContext.Cache, this.directoryContext, reportId);
			trackingContext.ReportTemplate = this.constraints.ReportTemplate;
			if (this.constraints.ReportTemplate == ReportTemplate.RecipientPath)
			{
				trackingContext.SelectedRecipient = this.constraints.RecipientPathFilter[0].ToString();
			}
			LogDataAnalyzer logDataAnalyzer = new LogDataAnalyzer(trackingContext);
			MessageTrackingLogEntry messageTrackingLogEntry;
			List<MessageTrackingLogEntry> list = logDataAnalyzer.AnalyzeLogData(reportId.MessageId, reportId.InternalMessageId, out messageTrackingLogEntry);
			List<RecipientTrackingEvent> list2 = null;
			if (list.Count > 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<int>(this.GetHashCode(), "Found {0} entries", list.Count);
				list2 = this.ConvertToRecipientTrackingEvent(list);
			}
			if (this.constraints.ReportTemplate == ReportTemplate.Summary && messageTrackingLogEntry != null)
			{
				this.subject = messageTrackingLogEntry.Subject;
				this.fromAddress = new SmtpAddress?(new SmtpAddress(messageTrackingLogEntry.SenderAddress));
				this.fromDisplayName = messageTrackingLogEntry.SenderAddress;
				this.submittedDateTime = messageTrackingLogEntry.Time;
				this.GetRecipients(messageTrackingLogEntry);
				return new RecipientEventData(list2);
			}
			List<List<RecipientTrackingEvent>> list3 = null;
			if (list2 != null && list2.Count > 0)
			{
				RecipientTrackingEvent recipientTrackingEvent = list2[list2.Count - 1];
				if ((recipientTrackingEvent.EventDescription == EventDescription.TransferredToForeignOrg || recipientTrackingEvent.EventDescription == EventDescription.SmtpSendCrossSite || recipientTrackingEvent.EventDescription == EventDescription.SmtpSendCrossForest || recipientTrackingEvent.EventDescription == EventDescription.TransferredToPartnerOrg) && list[list.Count - 1].EventId != MessageTrackingEvent.HAREDIRECT)
				{
					list3 = new List<List<RecipientTrackingEvent>>(1);
					list3.Add(list2);
					return new RecipientEventData(null, list3);
				}
				RecipientEventData recipientEventData = new RecipientEventData(list2);
				this.AppendRichTrackingDataIfNeeded(recipientEventData, list);
				return recipientEventData;
			}
			else
			{
				List<List<MessageTrackingLogEntry>> handedOffRecipPaths = logDataAnalyzer.GetHandedOffRecipPaths();
				if (handedOffRecipPaths != null)
				{
					list3 = new List<List<RecipientTrackingEvent>>(handedOffRecipPaths.Count);
					foreach (List<MessageTrackingLogEntry> logEntries in handedOffRecipPaths)
					{
						List<RecipientTrackingEvent> item = this.ConvertToRecipientTrackingEvent(logEntries);
						list3.Add(item);
					}
					return new RecipientEventData(null, list3);
				}
				return null;
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00058EA8 File Offset: 0x000570A8
		private RecipientEventData WSGetTrackingReport(MessageTrackingReportId reportId, WebServiceTrackingAuthority wsAuthority, out MessageTrackingReportType report)
		{
			report = null;
			WSGetParameters key = new WSGetParameters(reportId, wsAuthority);
			WSGetResult wsgetResult = this.getAdditionalRecords.FindAndCache(key, false);
			if (wsgetResult == null)
			{
				return null;
			}
			report = wsgetResult.Report;
			return wsgetResult.RecipientEventData;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00058EE4 File Offset: 0x000570E4
		private WSGetResult WSGetTrackingReportBasic(WSGetParameters getParams, WSGetResult currentCache, out KeyValuePair<WSGetParameters, WSGetResult>[] additionalRecords)
		{
			MessageTrackingReportId messageTrackingReportId = getParams.MessageTrackingReportId;
			WebServiceTrackingAuthority wsauthority = getParams.WSAuthority;
			additionalRecords = GetMessageTrackingReportImpl.EmptyGetRecords;
			IWebServiceBinding ewsBinding = wsauthority.GetEwsBinding(this.directoryContext);
			Exception ex = null;
			InternalGetMessageTrackingReportResponse internalGetMessageTrackingReportResponse = null;
			TraceWrapper.SearchLibraryTracer.TraceDebug<MessageTrackingReportId>(this.GetHashCode(), "WSGetTrackingReport: {0}", messageTrackingReportId);
			try
			{
				internalGetMessageTrackingReportResponse = ewsBinding.GetMessageTrackingReport(messageTrackingReportId.ToString(), this.constraints.ReportTemplate, this.constraints.RecipientPathFilter, wsauthority.AssociatedScope, this.constraints.ReturnQueueEvents, this.directoryContext.TrackingBudget);
			}
			catch (TrackingTransientException ex2)
			{
				ex = ex2;
			}
			catch (TrackingFatalException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<Exception>(this.GetHashCode(), "Error getting report-id: Exception: {0}", ex);
				return null;
			}
			if (internalGetMessageTrackingReportResponse == null || internalGetMessageTrackingReportResponse.Response == null || internalGetMessageTrackingReportResponse.Response.MessageTrackingReport == null || internalGetMessageTrackingReportResponse.RecipientTrackingEvents == null || internalGetMessageTrackingReportResponse.RecipientTrackingEvents.Count == 0)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<MessageTrackingReportId>(this.GetHashCode(), "Empty result finding report, report-id: {0}", this.messageTrackingReportId);
				return null;
			}
			MessageTrackingReportType messageTrackingReport = internalGetMessageTrackingReportResponse.Response.MessageTrackingReport;
			Dictionary<string, RecipientEventData> dictionary = RecipientEventData.DeserializeMultiple(internalGetMessageTrackingReportResponse.RecipientTrackingEvents);
			this.TraceRecipientData(dictionary);
			WSGetResult result = null;
			RecipientEventData recipientEventData;
			if (dictionary.TryGetValue(messageTrackingReportId.ToString(), out recipientEventData))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<int, int>(this.GetHashCode(), "SP1 returned mainRecipientEventData, eventCount={0}, handoffPathsCount={1}", (recipientEventData.Events == null) ? 0 : recipientEventData.Events.Count, (recipientEventData.HandoffPaths == null) ? 0 : recipientEventData.HandoffPaths.Count);
				dictionary.Remove(messageTrackingReportId.ToString());
			}
			else if (dictionary.TryGetValue(string.Empty, out recipientEventData))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<int, int>(this.GetHashCode(), "RTM returned mainRecipientEventData, eventCount={0}, handoffPathsCount={1}", (recipientEventData.Events == null) ? 0 : recipientEventData.Events.Count, (recipientEventData.HandoffPaths == null) ? 0 : recipientEventData.HandoffPaths.Count);
				dictionary.Remove(string.Empty);
			}
			else
			{
				TraceWrapper.SearchLibraryTracer.TraceError(0, "Only additional results were present in return data", new object[0]);
				recipientEventData = null;
			}
			if (recipientEventData != null)
			{
				result = new WSGetResult
				{
					RecipientEventData = recipientEventData,
					Report = messageTrackingReport
				};
			}
			if (dictionary.Count > 0)
			{
				int num = 0;
				additionalRecords = new KeyValuePair<WSGetParameters, WSGetResult>[dictionary.Count];
				foreach (KeyValuePair<string, RecipientEventData> keyValuePair in dictionary)
				{
					MessageTrackingReportId reportId;
					if (!MessageTrackingReportId.TryParse(keyValuePair.Key, out reportId))
					{
						TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Cannot parse report ID from additional records, skipping: {0}", keyValuePair.Key);
					}
					else
					{
						additionalRecords[num++] = new KeyValuePair<WSGetParameters, WSGetResult>(new WSGetParameters(reportId, wsauthority), new WSGetResult
						{
							RecipientEventData = keyValuePair.Value,
							Report = messageTrackingReport
						});
					}
				}
			}
			return result;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000591F8 File Offset: 0x000573F8
		private List<MessageTrackingSearchResult> LocalFindMessageReceiveBasic(FindParameters findParams, out bool wholeForestSearched)
		{
			RecipientTrackingEvent referralEvent = findParams.ReferralEvent;
			TrackingAuthority authority = findParams.Authority;
			bool flag = referralEvent.EventDescription == EventDescription.PendingModeration;
			wholeForestSearched = false;
			if (authority.TrackingAuthorityKind != TrackingAuthorityKind.CurrentSite && authority.TrackingAuthorityKind != TrackingAuthorityKind.Undefined)
			{
				throw new ArgumentException("Authority must be CurrentSite or Undefined, in local organization");
			}
			TrackedUser trackedUser = TrackedUser.Create(referralEvent.RecipientAddress.ToString(), this.directoryContext.TenantGalSession);
			if (trackedUser == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<SmtpAddress>(this.GetHashCode(), "ADUser object corrupted: {0}", referralEvent.RecipientAddress);
				this.Errors.Add(ErrorCode.InvalidADData, string.Empty, string.Format("Recipient mailbox not found for {0}", referralEvent.RecipientAddress), string.Empty);
				return null;
			}
			TrackedUser[] recipients = new TrackedUser[]
			{
				trackedUser
			};
			TrackedUser mailbox = null;
			if (flag)
			{
				mailbox = TrackedUser.Create(referralEvent.ExtendedProperties.ArbitrationMailboxAddress, this.directoryContext.TenantGalSession);
			}
			SearchScope searchScope = (SearchScope)Math.Min((int)authority.AssociatedScope, (int)this.scope);
			SearchMessageTrackingReportImpl searchMessageTrackingReportImpl = new SearchMessageTrackingReportImpl(this.directoryContext, searchScope, mailbox, null, referralEvent.ServerHint, recipients, this.trackingContext.Cache, null, flag ? referralEvent.ExtendedProperties.InitMessageId : this.messageTrackingReportId.MessageId, Unlimited<uint>.UnlimitedValue, false, false, true, flag);
			List<MessageTrackingSearchResult> list = searchMessageTrackingReportImpl.Execute();
			wholeForestSearched = searchMessageTrackingReportImpl.WholeForestSearchExecuted;
			if (list == null)
			{
				return null;
			}
			return list;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00059360 File Offset: 0x00057560
		private MessageTrackingReportId FindMessageReceive(RecipientTrackingEvent sendEvent, TrackingAuthority authority)
		{
			bool flag = sendEvent.EventDescription == EventDescription.PendingModeration;
			FindParameters key = new FindParameters(flag ? sendEvent.ExtendedProperties.InitMessageId : this.messageTrackingReportId.MessageId, sendEvent, authority);
			TraceWrapper.SearchLibraryTracer.TraceDebug<TrackingAuthority>(this.GetHashCode(), "Getting results from authority: {0}", authority);
			GetMessageTrackingReportImpl.FindCachedItem findCachedItem = this.findAdditionalRecords.FindAndCache(key, false);
			if (findCachedItem == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<MessageTrackingReportId>(this.GetHashCode(), "Null result finding message, report-id: {0}", this.messageTrackingReportId);
				return null;
			}
			MessageTrackingReportId messageTrackingReportId = this.FindReportIdWithMatchingRecipientInSearchResults(findCachedItem.Results, sendEvent.RecipientAddress);
			if (!flag && messageTrackingReportId == null && !findCachedItem.EntireForestSearched)
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<SmtpAddress, TrackingAuthority>(this.GetHashCode(), "No cached results for recipient {0}. Going to {1} again and bypass the WS cache to try and get the result.", sendEvent.RecipientAddress, authority);
				GetMessageTrackingReportImpl.FindCachedItem findCachedItem2 = this.findAdditionalRecords.FindAndCache(key, true);
				messageTrackingReportId = this.FindReportIdWithMatchingRecipientInSearchResults(findCachedItem2.Results, sendEvent.RecipientAddress);
			}
			return messageTrackingReportId;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x00059440 File Offset: 0x00057640
		private MessageTrackingReportId FindReportIdWithMatchingRecipientInSearchResults(IEnumerable<MessageTrackingSearchResult> searchResults, SmtpAddress recipientEmailAddress)
		{
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in searchResults)
			{
				if (messageTrackingSearchResult == null || messageTrackingSearchResult.RecipientAddresses == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Skipping result that is null or has no recipients.", new object[0]);
				}
				else
				{
					int num = Array.BinarySearch<SmtpAddress>(messageTrackingSearchResult.RecipientAddresses, recipientEmailAddress);
					if (num >= 0)
					{
						return messageTrackingSearchResult.MessageTrackingReportId;
					}
				}
			}
			return null;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000594C8 File Offset: 0x000576C8
		private GetMessageTrackingReportImpl.FindCachedItem FindMessageReceiveBasic(FindParameters findParams, GetMessageTrackingReportImpl.FindCachedItem currentCachedItem, out KeyValuePair<FindParameters, GetMessageTrackingReportImpl.FindCachedItem>[] additionalRecords)
		{
			additionalRecords = GetMessageTrackingReportImpl.EmptyFindRecords;
			if (currentCachedItem != null && (currentCachedItem.EntireForestSearched || currentCachedItem.AddressesSearched.Contains(findParams.ReferralEvent.RecipientAddress)))
			{
				return currentCachedItem;
			}
			IList<MessageTrackingSearchResult> list = null;
			bool flag = false;
			TrackingAuthorityKind trackingAuthorityKind = findParams.Authority.TrackingAuthorityKind;
			RecipientTrackingEvent referralEvent = findParams.ReferralEvent;
			if (trackingAuthorityKind == TrackingAuthorityKind.CurrentSite || trackingAuthorityKind == TrackingAuthorityKind.Undefined)
			{
				list = this.LocalFindMessageReceiveBasic(findParams, out flag);
			}
			else if (trackingAuthorityKind == TrackingAuthorityKind.RemoteSiteInCurrentOrg || trackingAuthorityKind == TrackingAuthorityKind.RemoteTrustedOrg || trackingAuthorityKind == TrackingAuthorityKind.RemoteForest)
			{
				list = this.WSFindMessageReceiveBasic(findParams, out flag);
			}
			if (list == null || list.Count == 0)
			{
				return currentCachedItem;
			}
			foreach (MessageTrackingSearchResult messageTrackingSearchResult in list)
			{
				Array.Sort<SmtpAddress>(messageTrackingSearchResult.RecipientAddresses);
			}
			if (currentCachedItem == null)
			{
				currentCachedItem = new GetMessageTrackingReportImpl.FindCachedItem();
			}
			if (flag)
			{
				currentCachedItem.EntireForestSearched = true;
				currentCachedItem.Results.Clear();
			}
			currentCachedItem.Results.InsertRange(0, list);
			currentCachedItem.AddressesSearched.Add(referralEvent.RecipientAddress);
			return currentCachedItem;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00059614 File Offset: 0x00057814
		private List<MessageTrackingSearchResult> WSFindMessageReceiveBasic(FindParameters findParams, out bool wholeForestSearched)
		{
			RecipientTrackingEvent referralEvent = findParams.ReferralEvent;
			WebServiceTrackingAuthority webServiceTrackingAuthority = (WebServiceTrackingAuthority)findParams.Authority;
			wholeForestSearched = false;
			IWebServiceBinding ewsBinding = webServiceTrackingAuthority.GetEwsBinding(this.directoryContext);
			new FindMessageTrackingReportRequestType();
			SmtpAddress? federatedDeliveryMailbox = null;
			if (referralEvent.EventDescription == EventDescription.TransferredToPartnerOrg && referralEvent.EventData != null && referralEvent.EventData.Length > 0)
			{
				federatedDeliveryMailbox = new SmtpAddress?(new SmtpAddress(referralEvent.EventData[0]));
				if (!federatedDeliveryMailbox.Value.IsValidAddress)
				{
					TraceWrapper.SearchLibraryTracer.TraceError(this.GetHashCode(), "Federated delivery email address invalid", new object[0]);
					TrackingFatalException.AddAndRaiseED(this.Errors, ErrorCode.UnexpectedErrorPermanent, "Referral event had invalid Federated Delivery email {0}", new object[]
					{
						federatedDeliveryMailbox.Value
					});
				}
			}
			Exception ex = null;
			FindMessageTrackingReportResponseMessageType findMessageTrackingReportResponseMessageType = null;
			try
			{
				findMessageTrackingReportResponseMessageType = ewsBinding.FindMessageTrackingReport(webServiceTrackingAuthority.Domain, null, new SmtpAddress?(referralEvent.RecipientAddress), referralEvent.ServerHint, federatedDeliveryMailbox, webServiceTrackingAuthority.AssociatedScope, this.messageTrackingReportId.MessageId, string.Empty, false, false, false, DateTime.MinValue, DateTime.MaxValue, this.directoryContext.TrackingBudget);
			}
			catch (TrackingTransientException ex2)
			{
				ex = ex2;
			}
			catch (TrackingFatalException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				if (GetMessageTrackingReportImpl.IsPageNotFoundAvailabilityException(ex))
				{
					return new List<MessageTrackingSearchResult>(1)
					{
						GetMessageTrackingReportImpl.CreateSearchResultWithLegacyExchangeReportId(referralEvent)
					};
				}
				TraceWrapper.SearchLibraryTracer.TraceError<MessageTrackingReportId, Exception>(this.GetHashCode(), "Error finding message, report-id: {0}, Exception: {1}", this.messageTrackingReportId, ex);
				return new List<MessageTrackingSearchResult>(0);
			}
			else
			{
				if (findMessageTrackingReportResponseMessageType == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<MessageTrackingReportId>(this.GetHashCode(), "Got null response when getting report-id:{0}", this.messageTrackingReportId);
					return new List<MessageTrackingSearchResult>(0);
				}
				wholeForestSearched = (string.IsNullOrEmpty(findMessageTrackingReportResponseMessageType.ExecutedSearchScope) || Names<SearchScope>.Map[1].Equals(findMessageTrackingReportResponseMessageType.ExecutedSearchScope, StringComparison.Ordinal));
				if (findMessageTrackingReportResponseMessageType.MessageTrackingSearchResults == null)
				{
					TraceWrapper.SearchLibraryTracer.TraceDebug<string, MessageTrackingReportId>(this.GetHashCode(), "No results found from EWS call to {0}, report-id: {1}", webServiceTrackingAuthority.Domain, this.messageTrackingReportId);
					return new List<MessageTrackingSearchResult>(0);
				}
				List<MessageTrackingSearchResult> searchResults = new List<MessageTrackingSearchResult>(findMessageTrackingReportResponseMessageType.MessageTrackingSearchResults.Length);
				FindMessageTrackingSearchResultType[] messageTrackingSearchResults = findMessageTrackingReportResponseMessageType.MessageTrackingSearchResults;
				for (int i = 0; i < messageTrackingSearchResults.Length; i++)
				{
					FindMessageTrackingSearchResultType wsResult = messageTrackingSearchResults[i];
					TrackingBaseException ex4 = this.TryExecuteTask(delegate
					{
						searchResults.Add(MessageTrackingSearchResult.Create(wsResult, ewsBinding.TargetInfoForDisplay));
					});
					if (ex4 != null)
					{
						TraceWrapper.SearchLibraryTracer.TraceError<string>(this.GetHashCode(), "Unable to convert ws result returned with id {0}", wsResult.MessageTrackingReportId);
					}
				}
				return searchResults;
			}
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x000598DC File Offset: 0x00057ADC
		private void TraceRecipientData(Dictionary<string, RecipientEventData> recipEventData)
		{
			if (this.directoryContext.DiagnosticsContext.VerboseDiagnostics || ExTraceGlobals.SearchLibraryTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				foreach (KeyValuePair<string, RecipientEventData> keyValuePair in recipEventData)
				{
					if (keyValuePair.Value.Events != null)
					{
						foreach (RecipientTrackingEvent recipientTrackingEvent in keyValuePair.Value.Events)
						{
							TraceWrapper.SearchLibraryTracer.TraceDebug<string, SmtpAddress>(0, "Event: {0}, {1}", keyValuePair.Key, recipientTrackingEvent.RecipientAddress);
						}
					}
					int num = 0;
					if (keyValuePair.Value.HandoffPaths != null)
					{
						foreach (List<RecipientTrackingEvent> list in keyValuePair.Value.HandoffPaths)
						{
							foreach (RecipientTrackingEvent recipientTrackingEvent2 in list)
							{
								TraceWrapper.SearchLibraryTracer.TraceDebug<string, int, SmtpAddress>(0, "HandoffPath: {0}, {1}, {2}", keyValuePair.Key, num, recipientTrackingEvent2.RecipientAddress);
							}
							num++;
						}
					}
				}
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00059A9C File Offset: 0x00057C9C
		private static bool IsPageNotFoundAvailabilityException(Exception e)
		{
			TrackingTransientException ex = e as TrackingTransientException;
			if (ex == null || ex.InnerException == null)
			{
				return false;
			}
			AvailabilityException ex2 = ex.InnerException as AvailabilityException;
			if (ex2 == null || ex2.InnerException == null)
			{
				return false;
			}
			WebException ex3 = ex2.InnerException as WebException;
			return ex3 != null && WebServiceBinding.IsPageNotFoundWebException(ex3);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00059AF0 File Offset: 0x00057CF0
		private static MessageTrackingSearchResult CreateSearchResultWithLegacyExchangeReportId(RecipientTrackingEvent sendEvent)
		{
			return new MessageTrackingSearchResult(MessageTrackingReportId.LegacyExchange, SmtpAddress.Empty, string.Empty, new SmtpAddress[]
			{
				sendEvent.RecipientAddress
			}, string.Empty, sendEvent.Date, string.Empty, string.Empty);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00059B40 File Offset: 0x00057D40
		private TrackingBaseException TryExecuteTask(Action action)
		{
			TrackingBaseException ex = null;
			try
			{
				action();
			}
			catch (TrackingFatalException ex2)
			{
				ex = ex2;
				if (!ex2.IsAlreadyLogged)
				{
					this.Errors.Errors.Add(ex2.TrackingError);
				}
			}
			catch (TrackingTransientException ex3)
			{
				ex = ex3;
				if (!ex3.IsAlreadyLogged)
				{
					this.Errors.Errors.Add(ex3.TrackingError);
				}
			}
			if (ex != null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<TrackingBaseException>(0, "TrackingException occurred: {0}", ex);
				return ex;
			}
			return null;
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00059BD0 File Offset: 0x00057DD0
		private static GetMessageTrackingReportImpl.EventInfo GetSendEventInfo(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			if (logEntry.Source == MessageTrackingSource.SMTP)
			{
				ServerInfo serverInfo = ServerCache.Instance.FindMailboxOrHubServer(logEntry.NextHopFqdnOrName, 32UL);
				if (serverInfo.Status == ServerStatus.NotFound)
				{
					string domain = new SmtpAddress(logEntry.RecipientAddress).Domain;
					if (!string.IsNullOrEmpty(domain) && implObject.trackingDiscovery.IsCrossForestDomain(domain))
					{
						return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpSend, DeliveryStatus.Pending, EventDescription.SmtpSendCrossForest);
					}
					if (!string.IsNullOrEmpty(domain) && ServerCache.Instance.IsRemoteTrustedOrg(implObject.directoryContext.OrganizationId, domain))
					{
						return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpSend, DeliveryStatus.Pending, EventDescription.TransferredToPartnerOrg);
					}
					return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpSend, DeliveryStatus.Transferred, EventDescription.TransferredToForeignOrg);
				}
				else
				{
					if (serverInfo.Status == ServerStatus.LegacyExchangeServer)
					{
						return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpSend, DeliveryStatus.Transferred, EventDescription.TransferredToLegacyExchangeServer);
					}
					EventDescription eventDescription = logEntry.IsNextHopCrossSite(implObject.directoryContext) ? EventDescription.SmtpSendCrossSite : EventDescription.SmtpSend;
					return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpSend, DeliveryStatus.Pending, eventDescription);
				}
			}
			else
			{
				if (logEntry.Source == MessageTrackingSource.GATEWAY)
				{
					return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpSend, DeliveryStatus.Transferred, EventDescription.TransferredToForeignOrg);
				}
				if (logEntry.Source == MessageTrackingSource.AGENT && Parse.IsSMSRecipient(logEntry.RecipientAddress))
				{
					return new GetMessageTrackingReportImpl.EventInfo(EventType.Transferred, DeliveryStatus.Transferred, EventDescription.TransferredToForeignOrg);
				}
				return null;
			}
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00059CD5 File Offset: 0x00057ED5
		private static GetMessageTrackingReportImpl.EventInfo GetReceiveEventInfo(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			if (logEntry.Source == MessageTrackingSource.SMTP)
			{
				return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpReceive, DeliveryStatus.Pending, EventDescription.SmtpReceive);
			}
			if (logEntry.Source == MessageTrackingSource.STOREDRIVER)
			{
				return new GetMessageTrackingReportImpl.EventInfo(EventType.Submit, DeliveryStatus.Pending, EventDescription.Submitted);
			}
			if (logEntry.Source == MessageTrackingSource.AGENT)
			{
				return new GetMessageTrackingReportImpl.EventInfo(EventType.Redirect, DeliveryStatus.Pending, EventDescription.RulesCc);
			}
			return null;
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00059D0E File Offset: 0x00057F0E
		private static GetMessageTrackingReportImpl.EventInfo GetFailEventInfo(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			if (logEntry.Source == MessageTrackingSource.AGENT && string.Equals(logEntry.SourceContext, "Transport Rule Agent", StringComparison.OrdinalIgnoreCase))
			{
				return new GetMessageTrackingReportImpl.EventInfo(EventType.Fail, DeliveryStatus.Unsuccessful, EventDescription.FailedTransportRules);
			}
			return new GetMessageTrackingReportImpl.EventInfo(EventType.Fail, DeliveryStatus.Unsuccessful, EventDescription.FailedGeneral);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00059D40 File Offset: 0x00057F40
		private static GetMessageTrackingReportImpl.EventInfo GetExpandedEventInfo(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			if (string.IsNullOrEmpty(logEntry.FederatedDeliveryAddress))
			{
				return new GetMessageTrackingReportImpl.EventInfo(EventType.Expand, DeliveryStatus.Pending, EventDescription.Expanded);
			}
			string domain = new SmtpAddress(logEntry.FederatedDeliveryAddress).Domain;
			bool flag = !string.IsNullOrEmpty(domain) && ServerCache.Instance.IsRemoteTrustedOrg(implObject.directoryContext.OrganizationId, domain);
			return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpSend, flag ? DeliveryStatus.Pending : DeliveryStatus.Transferred, flag ? EventDescription.TransferredToPartnerOrg : EventDescription.TransferredToForeignOrg);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00059DB0 File Offset: 0x00057FB0
		private static GetMessageTrackingReportImpl.EventInfo GetProcessedEventInfo(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			if (string.IsNullOrEmpty(logEntry.RecipientStatus))
			{
				return null;
			}
			if (logEntry.RecipientStatus.IndexOf("Approval Processing Agent", StringComparison.OrdinalIgnoreCase) >= 0 || logEntry.RecipientStatus.IndexOf("Mailbox Rules Agent", StringComparison.OrdinalIgnoreCase) >= 0)
			{
				return new GetMessageTrackingReportImpl.EventInfo(EventType.Deliver, DeliveryStatus.Delivered, EventDescription.Delivered);
			}
			return null;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00059DFE File Offset: 0x00057FFE
		private static GetMessageTrackingReportImpl.EventInfo GetSubmitEventInfo(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			return new GetMessageTrackingReportImpl.EventInfo(EventType.Submit, DeliveryStatus.Pending, logEntry.IsNextHopCrossSite(implObject.directoryContext) ? EventDescription.SubmittedCrossSite : EventDescription.Submitted);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00059E19 File Offset: 0x00058019
		private static GetMessageTrackingReportImpl.EventInfo GetHARedirectEventInfo(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			return new GetMessageTrackingReportImpl.EventInfo(EventType.SmtpSend, DeliveryStatus.Pending, logEntry.IsNextHopCrossSite(implObject.directoryContext) ? EventDescription.SmtpSendCrossSite : EventDescription.SmtpSend);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00059E38 File Offset: 0x00058038
		private static string[] GetResolvedEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			if (string.IsNullOrEmpty(logEntry.RelatedRecipientAddress) || string.IsNullOrEmpty(logEntry.RecipientAddress))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string, string>(implObject.GetHashCode(), "Either original [{0}] or final [{1}] addresses are not available, this is non-fatal, we will just display a simpler message", logEntry.RelatedRecipientAddress, logEntry.RecipientAddress);
				return null;
			}
			return new string[]
			{
				logEntry.RelatedRecipientAddress,
				logEntry.RecipientAddress
			};
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00059E9C File Offset: 0x0005809C
		private static string[] GetExpandEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			SmtpAddress arg = new SmtpAddress(logEntry.RelatedRecipientAddress);
			TrackedUser trackedUser = TrackedUser.Create(arg.ToString(), implObject.directoryContext.TenantGalSession);
			if (trackedUser == null)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<SmtpAddress>(implObject.GetHashCode(), "DL {0} was resolved but not able to be validated, this is non-fatal, display name of DL will be unavailable", arg);
				return new string[]
				{
					logEntry.RelatedRecipientAddress,
					logEntry.RelatedRecipientAddress
				};
			}
			return new string[]
			{
				trackedUser.SmtpAddress.ToString(),
				trackedUser.DisplayName
			};
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x00059F34 File Offset: 0x00058134
		private static string[] GetSmtpSendEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			string text;
			if (!string.IsNullOrEmpty(logEntry.NextHopFqdnOrName))
			{
				text = logEntry.NextHopFqdnOrName;
			}
			else
			{
				text = logEntry.ServerIP;
			}
			return new string[]
			{
				logEntry.Server,
				text
			};
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x00059F74 File Offset: 0x00058174
		private static string[] GetSmtpSendCrossForestEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			string text;
			if (!string.IsNullOrEmpty(logEntry.NextHopFqdnOrName))
			{
				text = logEntry.NextHopFqdnOrName;
			}
			else
			{
				text = logEntry.ServerIP;
			}
			return new string[]
			{
				logEntry.Server,
				text,
				SmtpAddress.Parse(logEntry.RecipientAddress).Domain
			};
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00059FCC File Offset: 0x000581CC
		private static string[] GetTransferredToForeignOrgEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			return new string[]
			{
				SmtpAddress.Parse(logEntry.RecipientAddress).Domain
			};
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00059FF8 File Offset: 0x000581F8
		private static string[] GetTransferredToPartnerOrgEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			if (logEntry.FederatedDeliveryAddress == null)
			{
				return null;
			}
			return new string[]
			{
				logEntry.FederatedDeliveryAddress
			};
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0005A020 File Offset: 0x00058220
		private static string[] TransferredToLegacyExchangeServer(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			string text;
			if (!string.IsNullOrEmpty(logEntry.NextHopFqdnOrName))
			{
				text = logEntry.NextHopFqdnOrName;
			}
			else
			{
				text = logEntry.ServerIP;
			}
			return new string[]
			{
				logEntry.Server,
				text
			};
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0005A060 File Offset: 0x00058260
		private static string[] GetSmtpReceiveEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			string text;
			if (!string.IsNullOrEmpty(logEntry.ClientHostName))
			{
				text = logEntry.ClientHostName;
			}
			else
			{
				text = logEntry.ClientIP;
			}
			return new string[]
			{
				logEntry.Server,
				text
			};
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0005A0A0 File Offset: 0x000582A0
		private static string[] GetFailEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			if (string.IsNullOrEmpty(logEntry.RecipientStatus))
			{
				return null;
			}
			if (logEntry.Source == MessageTrackingSource.SMTP && string.Equals(logEntry.SourceContext, "Content Filter Agent", StringComparison.OrdinalIgnoreCase))
			{
				return new string[]
				{
					logEntry.RecipientStatus,
					"Content Filter Agent"
				};
			}
			return new string[]
			{
				logEntry.RecipientStatus
			};
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0005A104 File Offset: 0x00058304
		private static string[] GetSubmitCrossSiteEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			string text;
			if (!string.IsNullOrEmpty(logEntry.NextHopFqdnOrName))
			{
				text = logEntry.NextHopFqdnOrName;
			}
			else
			{
				text = logEntry.ServerIP;
			}
			return new string[]
			{
				logEntry.Server,
				text
			};
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0005A144 File Offset: 0x00058344
		private static string[] GetSubmitEventData(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject)
		{
			return new string[]
			{
				logEntry.Server
			};
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0005A164 File Offset: 0x00058364
		private static string FormatDateTime(DateTime dateTime, CultureInfo culture)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentUICulture;
			}
			if (dateTime < culture.Calendar.MinSupportedDateTime)
			{
				dateTime = culture.Calendar.MinSupportedDateTime;
			}
			else if (dateTime > culture.Calendar.MaxSupportedDateTime)
			{
				dateTime = culture.Calendar.MaxSupportedDateTime;
			}
			return dateTime.ToString(culture);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0005A1C8 File Offset: 0x000583C8
		private static Dictionary<MessageTrackingEvent, GetMessageTrackingReportImpl.EventInfo> CreateRawEventToEventInfoMap()
		{
			Dictionary<MessageTrackingEvent, GetMessageTrackingReportImpl.EventInfo> dictionary = new Dictionary<MessageTrackingEvent, GetMessageTrackingReportImpl.EventInfo>();
			dictionary[MessageTrackingEvent.BADMAIL] = new GetMessageTrackingReportImpl.EventInfo(EventType.Fail, DeliveryStatus.Unsuccessful, EventDescription.FailedGeneral);
			dictionary[MessageTrackingEvent.DELIVER] = new GetMessageTrackingReportImpl.EventInfo(EventType.Deliver, DeliveryStatus.Delivered, EventDescription.Delivered);
			dictionary[MessageTrackingEvent.DSN] = new GetMessageTrackingReportImpl.EventInfo(EventType.Fail, DeliveryStatus.Unsuccessful, EventDescription.FailedGeneral);
			dictionary[MessageTrackingEvent.FAIL] = new GetMessageTrackingReportImpl.EventInfo(EventType.Fail, DeliveryStatus.Unsuccessful, EventDescription.FailedGeneral);
			dictionary[MessageTrackingEvent.INITMESSAGECREATED] = new GetMessageTrackingReportImpl.EventInfo(EventType.InitMessageCreated, DeliveryStatus.Pending, EventDescription.PendingModeration);
			dictionary[MessageTrackingEvent.MODERATORAPPROVE] = new GetMessageTrackingReportImpl.EventInfo(EventType.ModeratorApprove, DeliveryStatus.Pending, EventDescription.ApprovedModeration);
			dictionary[MessageTrackingEvent.MODERATORREJECT] = new GetMessageTrackingReportImpl.EventInfo(EventType.ModeratorRejected, DeliveryStatus.Unsuccessful, EventDescription.FailedModeration);
			dictionary[MessageTrackingEvent.POISONMESSAGE] = new GetMessageTrackingReportImpl.EventInfo(EventType.Fail, DeliveryStatus.Unsuccessful, EventDescription.FailedGeneral);
			dictionary[MessageTrackingEvent.REDIRECT] = new GetMessageTrackingReportImpl.EventInfo(EventType.Redirect, DeliveryStatus.Pending, EventDescription.Forwarded);
			dictionary[MessageTrackingEvent.RESOLVE] = new GetMessageTrackingReportImpl.EventInfo(EventType.Resolve, DeliveryStatus.Pending, EventDescription.Resolved);
			dictionary[MessageTrackingEvent.RESUBMIT] = new GetMessageTrackingReportImpl.EventInfo(EventType.Submit, DeliveryStatus.Pending, EventDescription.Submitted);
			dictionary[MessageTrackingEvent.DEFER] = new GetMessageTrackingReportImpl.EventInfo(EventType.Defer, DeliveryStatus.Pending, EventDescription.MessageDefer);
			dictionary[MessageTrackingEvent.DUPLICATEDELIVER] = new GetMessageTrackingReportImpl.EventInfo(EventType.Deliver, DeliveryStatus.Delivered, EventDescription.Delivered);
			dictionary[MessageTrackingEvent.MODERATIONEXPIRE] = new GetMessageTrackingReportImpl.EventInfo(EventType.Fail, DeliveryStatus.Unsuccessful, EventDescription.ExpiredWithNoModerationDecision);
			return dictionary;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0005A2C0 File Offset: 0x000584C0
		private static Dictionary<MessageTrackingEvent, GetMessageTrackingReportImpl.GetEventInfoMethod> CreateRawEventToEventInfoGetterMap()
		{
			Dictionary<MessageTrackingEvent, GetMessageTrackingReportImpl.GetEventInfoMethod> dictionary = new Dictionary<MessageTrackingEvent, GetMessageTrackingReportImpl.GetEventInfoMethod>();
			dictionary[MessageTrackingEvent.SEND] = new GetMessageTrackingReportImpl.GetEventInfoMethod(GetMessageTrackingReportImpl.GetSendEventInfo);
			dictionary[MessageTrackingEvent.RECEIVE] = new GetMessageTrackingReportImpl.GetEventInfoMethod(GetMessageTrackingReportImpl.GetReceiveEventInfo);
			dictionary[MessageTrackingEvent.FAIL] = new GetMessageTrackingReportImpl.GetEventInfoMethod(GetMessageTrackingReportImpl.GetFailEventInfo);
			dictionary[MessageTrackingEvent.EXPAND] = new GetMessageTrackingReportImpl.GetEventInfoMethod(GetMessageTrackingReportImpl.GetExpandedEventInfo);
			dictionary[MessageTrackingEvent.PROCESS] = new GetMessageTrackingReportImpl.GetEventInfoMethod(GetMessageTrackingReportImpl.GetProcessedEventInfo);
			dictionary[MessageTrackingEvent.SUBMIT] = new GetMessageTrackingReportImpl.GetEventInfoMethod(GetMessageTrackingReportImpl.GetSubmitEventInfo);
			dictionary[MessageTrackingEvent.HAREDIRECT] = new GetMessageTrackingReportImpl.GetEventInfoMethod(GetMessageTrackingReportImpl.GetHARedirectEventInfo);
			return dictionary;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0005A35C File Offset: 0x0005855C
		private static Dictionary<EventDescription, GetMessageTrackingReportImpl.GetEventDataMethod> CreateEventDataGetters()
		{
			Dictionary<EventDescription, GetMessageTrackingReportImpl.GetEventDataMethod> dictionary = new Dictionary<EventDescription, GetMessageTrackingReportImpl.GetEventDataMethod>();
			dictionary[EventDescription.Resolved] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetResolvedEventData);
			dictionary[EventDescription.Expanded] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetExpandEventData);
			dictionary[EventDescription.SmtpReceive] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetSmtpReceiveEventData);
			dictionary[EventDescription.TransferredToForeignOrg] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetTransferredToForeignOrgEventData);
			dictionary[EventDescription.TransferredToPartnerOrg] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetTransferredToPartnerOrgEventData);
			dictionary[EventDescription.TransferredToLegacyExchangeServer] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.TransferredToLegacyExchangeServer);
			dictionary[EventDescription.SmtpSend] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetSmtpSendEventData);
			dictionary[EventDescription.SmtpSendCrossSite] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetSmtpSendEventData);
			dictionary[EventDescription.SmtpSendCrossForest] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetSmtpSendCrossForestEventData);
			dictionary[EventDescription.FailedGeneral] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetFailEventData);
			dictionary[EventDescription.Submitted] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetSubmitEventData);
			dictionary[EventDescription.SubmittedCrossSite] = new GetMessageTrackingReportImpl.GetEventDataMethod(GetMessageTrackingReportImpl.GetSubmitCrossSiteEventData);
			return dictionary;
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0005A63C File Offset: 0x0005883C
		private static IEnumerable<List<RecipientTrackingEvent>> ConvertRecipientPathModeEventsToPaths(RecipientEventData recipEventData)
		{
			if (recipEventData.Events != null && recipEventData.Events.Count > 0)
			{
				yield return recipEventData.Events;
			}
			else if (recipEventData.HandoffPaths != null)
			{
				foreach (List<RecipientTrackingEvent> path in recipEventData.HandoffPaths)
				{
					yield return path;
				}
			}
			yield break;
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0005A804 File Offset: 0x00058A04
		private static IEnumerable<List<RecipientTrackingEvent>> ConvertSummaryEventsToPaths(List<RecipientTrackingEvent> recipEvents)
		{
			if (recipEvents != null)
			{
				foreach (RecipientTrackingEvent recipEvent in recipEvents)
				{
					yield return new List<RecipientTrackingEvent>(0)
					{
						recipEvent
					};
				}
			}
			yield break;
		}

		// Token: 0x04000CCD RID: 3277
		private static readonly KeyValuePair<FindParameters, GetMessageTrackingReportImpl.FindCachedItem>[] EmptyFindRecords = new KeyValuePair<FindParameters, GetMessageTrackingReportImpl.FindCachedItem>[0];

		// Token: 0x04000CCE RID: 3278
		private static readonly KeyValuePair<WSGetParameters, WSGetResult>[] EmptyGetRecords = new KeyValuePair<WSGetParameters, WSGetResult>[0];

		// Token: 0x04000CCF RID: 3279
		private static GetMessageTrackingReportImpl.EventInfo generalPendingEventInfo = new GetMessageTrackingReportImpl.EventInfo(EventType.Pending, DeliveryStatus.Pending, EventDescription.Pending);

		// Token: 0x04000CD0 RID: 3280
		private static Dictionary<MessageTrackingEvent, GetMessageTrackingReportImpl.EventInfo> rawEventToEventInfo = GetMessageTrackingReportImpl.CreateRawEventToEventInfoMap();

		// Token: 0x04000CD1 RID: 3281
		private static Dictionary<MessageTrackingEvent, GetMessageTrackingReportImpl.GetEventInfoMethod> rawEventToEventInfoGetter = GetMessageTrackingReportImpl.CreateRawEventToEventInfoGetterMap();

		// Token: 0x04000CD2 RID: 3282
		private static BitArray initialEventRows = MessageTrackingLogRow.GetColumnFilter(new MessageTrackingField[]
		{
			MessageTrackingField.SenderAddress,
			MessageTrackingField.RecipientAddress,
			MessageTrackingField.Timestamp,
			MessageTrackingField.MessageSubject
		});

		// Token: 0x04000CD3 RID: 3283
		private static Dictionary<EventDescription, GetMessageTrackingReportImpl.GetEventDataMethod> eventDataGetters = GetMessageTrackingReportImpl.CreateEventDataGetters();

		// Token: 0x04000CD4 RID: 3284
		private static PropertyDefinition[] readStatusProperties = new PropertyDefinition[]
		{
			MessageItemSchema.Flags,
			MessageItemSchema.TransportMessageHeaders
		};

		// Token: 0x04000CD5 RID: 3285
		private SearchScope scope;

		// Token: 0x04000CD6 RID: 3286
		private TrackingDiscovery trackingDiscovery;

		// Token: 0x04000CD7 RID: 3287
		private ReportConstraints constraints;

		// Token: 0x04000CD8 RID: 3288
		private TrackingContext trackingContext;

		// Token: 0x04000CD9 RID: 3289
		private MessageTrackingReportId messageTrackingReportId;

		// Token: 0x04000CDA RID: 3290
		private DirectoryContext directoryContext;

		// Token: 0x04000CDB RID: 3291
		private string defaultDomain;

		// Token: 0x04000CDC RID: 3292
		private CultureInfo userCulture;

		// Token: 0x04000CDD RID: 3293
		private string subject;

		// Token: 0x04000CDE RID: 3294
		private DateTime submittedDateTime;

		// Token: 0x04000CDF RID: 3295
		private SmtpAddress? fromAddress;

		// Token: 0x04000CE0 RID: 3296
		private string fromDisplayName;

		// Token: 0x04000CE1 RID: 3297
		private SmtpAddress[] submissionRecipientAddresses = new SmtpAddress[0];

		// Token: 0x04000CE2 RID: 3298
		private string[] submissionRecipientDisplayNames = new string[0];

		// Token: 0x04000CE3 RID: 3299
		private WSAdditionalRecords<FindParameters, GetMessageTrackingReportImpl.FindCachedItem> findAdditionalRecords;

		// Token: 0x04000CE4 RID: 3300
		private WSAdditionalRecords<WSGetParameters, WSGetResult> getAdditionalRecords;

		// Token: 0x020002AE RID: 686
		private class EventInfo
		{
			// Token: 0x0600134E RID: 4942 RVA: 0x0005A8B2 File Offset: 0x00058AB2
			public EventInfo(EventType eventType, DeliveryStatus deliveryStatus, EventDescription eventDescription)
			{
				this.eventType = eventType;
				this.deliveryStatus = deliveryStatus;
				this.eventDescription = eventDescription;
			}

			// Token: 0x170004CB RID: 1227
			// (get) Token: 0x0600134F RID: 4943 RVA: 0x0005A8CF File Offset: 0x00058ACF
			public EventType EventType
			{
				get
				{
					return this.eventType;
				}
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x06001350 RID: 4944 RVA: 0x0005A8D7 File Offset: 0x00058AD7
			public DeliveryStatus DeliveryStatus
			{
				get
				{
					return this.deliveryStatus;
				}
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x06001351 RID: 4945 RVA: 0x0005A8DF File Offset: 0x00058ADF
			public EventDescription EventDescription
			{
				get
				{
					return this.eventDescription;
				}
			}

			// Token: 0x04000CE5 RID: 3301
			private EventType eventType;

			// Token: 0x04000CE6 RID: 3302
			private DeliveryStatus deliveryStatus;

			// Token: 0x04000CE7 RID: 3303
			private EventDescription eventDescription;
		}

		// Token: 0x020002AF RID: 687
		internal class RecipientReferral
		{
			// Token: 0x06001352 RID: 4946 RVA: 0x0005A8E7 File Offset: 0x00058AE7
			public RecipientReferral(RecipientTrackingEvent recipientTrackingEvent, TrackingAuthority authority)
			{
				this.recipientTrackingEvent = recipientTrackingEvent;
				this.authority = authority;
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x06001353 RID: 4947 RVA: 0x0005A8FD File Offset: 0x00058AFD
			public RecipientTrackingEvent RecipientTrackingEvent
			{
				get
				{
					return this.recipientTrackingEvent;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x06001354 RID: 4948 RVA: 0x0005A905 File Offset: 0x00058B05
			public TrackingAuthority Authority
			{
				get
				{
					return this.authority;
				}
			}

			// Token: 0x04000CE8 RID: 3304
			private RecipientTrackingEvent recipientTrackingEvent;

			// Token: 0x04000CE9 RID: 3305
			private TrackingAuthority authority;
		}

		// Token: 0x020002B0 RID: 688
		// (Invoke) Token: 0x06001356 RID: 4950
		private delegate string[] GetEventDataMethod(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject);

		// Token: 0x020002B1 RID: 689
		// (Invoke) Token: 0x0600135A RID: 4954
		private delegate GetMessageTrackingReportImpl.EventInfo GetEventInfoMethod(MessageTrackingLogEntry logEntry, GetMessageTrackingReportImpl implObject);

		// Token: 0x020002B2 RID: 690
		internal class FindCachedItem
		{
			// Token: 0x170004D0 RID: 1232
			// (get) Token: 0x0600135D RID: 4957 RVA: 0x0005A90D File Offset: 0x00058B0D
			internal List<MessageTrackingSearchResult> Results
			{
				get
				{
					return this.results;
				}
			}

			// Token: 0x170004D1 RID: 1233
			// (get) Token: 0x0600135E RID: 4958 RVA: 0x0005A915 File Offset: 0x00058B15
			// (set) Token: 0x0600135F RID: 4959 RVA: 0x0005A91D File Offset: 0x00058B1D
			internal bool EntireForestSearched { get; set; }

			// Token: 0x170004D2 RID: 1234
			// (get) Token: 0x06001360 RID: 4960 RVA: 0x0005A926 File Offset: 0x00058B26
			internal HashSet<SmtpAddress> AddressesSearched
			{
				get
				{
					return this.addressesSearched;
				}
			}

			// Token: 0x06001361 RID: 4961 RVA: 0x0005A92E File Offset: 0x00058B2E
			internal FindCachedItem()
			{
			}

			// Token: 0x04000CEA RID: 3306
			private HashSet<SmtpAddress> addressesSearched = new HashSet<SmtpAddress>();

			// Token: 0x04000CEB RID: 3307
			private List<MessageTrackingSearchResult> results = new List<MessageTrackingSearchResult>();
		}
	}
}
