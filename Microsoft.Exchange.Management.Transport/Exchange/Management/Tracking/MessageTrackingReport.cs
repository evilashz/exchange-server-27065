using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Tracking;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x020000A1 RID: 161
	[Serializable]
	public class MessageTrackingReport : MessageTrackingConfigurableObject
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x0001639D File Offset: 0x0001459D
		public MessageTrackingReport()
		{
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x000163A5 File Offset: 0x000145A5
		public MessageTrackingReportId MessageTrackingReportId
		{
			get
			{
				return (MessageTrackingReportId)this[MessageTrackingSharedResultSchema.MessageTrackingReportId];
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x000163B7 File Offset: 0x000145B7
		public DateTime SubmittedDateTime
		{
			get
			{
				return (DateTime)this[MessageTrackingSharedResultSchema.SubmittedDateTime];
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x000163C9 File Offset: 0x000145C9
		public string Subject
		{
			get
			{
				return (string)this[MessageTrackingSharedResultSchema.Subject];
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x000163DB File Offset: 0x000145DB
		public SmtpAddress FromAddress
		{
			get
			{
				return (SmtpAddress)this[MessageTrackingSharedResultSchema.FromAddress];
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000163ED File Offset: 0x000145ED
		public string FromDisplayName
		{
			get
			{
				return (string)this[MessageTrackingSharedResultSchema.FromDisplayName];
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x000163FF File Offset: 0x000145FF
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00016411 File Offset: 0x00014611
		public SmtpAddress[] RecipientAddresses
		{
			get
			{
				return (SmtpAddress[])this[MessageTrackingSharedResultSchema.RecipientAddresses];
			}
			internal set
			{
				this[MessageTrackingSharedResultSchema.RecipientAddresses] = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001641F File Offset: 0x0001461F
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00016431 File Offset: 0x00014631
		public string[] RecipientDisplayNames
		{
			get
			{
				return (string[])this[MessageTrackingSharedResultSchema.RecipientDisplayNames];
			}
			internal set
			{
				this[MessageTrackingSharedResultSchema.RecipientDisplayNames] = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001643F File Offset: 0x0001463F
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00016451 File Offset: 0x00014651
		public int DeliveredCount
		{
			get
			{
				return (int)this[MessageTrackingReportSchema.DeliveryCount];
			}
			internal set
			{
				this[MessageTrackingReportSchema.DeliveryCount] = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00016464 File Offset: 0x00014664
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00016476 File Offset: 0x00014676
		public int PendingCount
		{
			get
			{
				return (int)this[MessageTrackingReportSchema.PendingCount];
			}
			internal set
			{
				this[MessageTrackingReportSchema.PendingCount] = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00016489 File Offset: 0x00014689
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0001649B File Offset: 0x0001469B
		public int UnsuccessfulCount
		{
			get
			{
				return (int)this[MessageTrackingReportSchema.UnsuccessfulCount];
			}
			internal set
			{
				this[MessageTrackingReportSchema.UnsuccessfulCount] = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x000164AE File Offset: 0x000146AE
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x000164C0 File Offset: 0x000146C0
		public int TransferredCount
		{
			get
			{
				return (int)this[MessageTrackingReportSchema.TransferredCount];
			}
			internal set
			{
				this[MessageTrackingReportSchema.TransferredCount] = value;
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x000164D3 File Offset: 0x000146D3
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x000164E5 File Offset: 0x000146E5
		public RecipientTrackingEvent[] RecipientTrackingEvents
		{
			get
			{
				return (RecipientTrackingEvent[])this[MessageTrackingReportSchema.RecipientTrackingEvents];
			}
			internal set
			{
				this[MessageTrackingReportSchema.RecipientTrackingEvents] = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x000164F3 File Offset: 0x000146F3
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return MessageTrackingReport.schema;
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x000164FC File Offset: 0x000146FC
		internal static MessageTrackingReport Create(IConfigurationSession configurationSession, IRecipientSession recipientSession, MultiValuedProperty<CultureInfo> userLanguages, bool summaryEvents, bool returnHelpDeskMessages, bool trackingAsSender, MessageTrackingReport internalMessageTrackingReport, bool doNotResolve, bool isCompleteReport)
		{
			RecipientTrackingEvent[] recipientTrackingEvents = internalMessageTrackingReport.RecipientTrackingEvents;
			if (!doNotResolve && recipientTrackingEvents.Length > 256)
			{
				ExTraceGlobals.TaskTracer.TraceDebug<int, int>(0L, "Recipient events ({0}) are more than MaxDisplaynameLookupsAllowed ({1}), turning off display-names", recipientTrackingEvents.Length, 256);
				doNotResolve = true;
			}
			RecipientTrackingEvent[] array;
			if (summaryEvents)
			{
				array = MessageTrackingReport.GetRecipientEventsForSummaryReport(configurationSession, recipientSession, userLanguages, returnHelpDeskMessages, trackingAsSender, recipientTrackingEvents);
			}
			else
			{
				array = MessageTrackingReport.GetRecipientEventsForRecipientPathReport(configurationSession, recipientSession, userLanguages, returnHelpDeskMessages, trackingAsSender, recipientTrackingEvents, isCompleteReport);
			}
			if (array == null)
			{
				return null;
			}
			int capacity = summaryEvents ? array.Length : 1;
			BulkRecipientLookupCache bulkRecipientLookupCache = new BulkRecipientLookupCache(capacity);
			if (!doNotResolve)
			{
				RecipientTrackingEvent.FillDisplayNames(bulkRecipientLookupCache, array, recipientSession);
			}
			MessageTrackingReport messageTrackingReport = new MessageTrackingReport(internalMessageTrackingReport, array);
			if (summaryEvents)
			{
				messageTrackingReport.FillDisplayNames(bulkRecipientLookupCache, recipientSession);
			}
			messageTrackingReport.PrepareRecipientTrackingEvents(returnHelpDeskMessages, summaryEvents);
			return messageTrackingReport;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000165A4 File Offset: 0x000147A4
		private static RecipientTrackingEvent[] GetRecipientEventsForRecipientPathReport(IConfigurationSession configurationSession, IRecipientSession recipientSession, MultiValuedProperty<CultureInfo> userLanguages, bool returnHelpDeskMessages, bool trackingAsSender, RecipientTrackingEvent[] internalRecipientTrackingEvents, bool isCompleteReport)
		{
			List<RecipientTrackingEvent> list = new List<RecipientTrackingEvent>(internalRecipientTrackingEvents.Length);
			RecipientTrackingEvent recipientTrackingEvent = null;
			if (isCompleteReport)
			{
				recipientTrackingEvent = RecipientTrackingEvent.GetExplanatoryMessage(internalRecipientTrackingEvents);
			}
			bool flag = false;
			for (int i = 0; i < internalRecipientTrackingEvents.Length; i++)
			{
				bool flag2 = i == internalRecipientTrackingEvents.Length - 1;
				if (!MessageTrackingReport.ShouldHideEvent(flag2, internalRecipientTrackingEvents[i], returnHelpDeskMessages, trackingAsSender) && (!flag || internalRecipientTrackingEvents[i].EventDescription != EventDescription.FailedGeneral) && (internalRecipientTrackingEvents[i].EventDescription != EventDescription.TransferredToForeignOrg || flag2) && (internalRecipientTrackingEvents[i].EventDescription != EventDescription.FailedTransportRules || returnHelpDeskMessages || (!flag && (i + 1 >= internalRecipientTrackingEvents.Length || (internalRecipientTrackingEvents[i + 1].EventDescription != EventDescription.FailedTransportRules && internalRecipientTrackingEvents[i + 1].EventDescription != EventDescription.FailedGeneral)))))
				{
					RecipientTrackingEvent recipientTrackingEvent2 = RecipientTrackingEvent.Create(flag2, configurationSession, userLanguages, returnHelpDeskMessages, internalRecipientTrackingEvents[i]);
					if (recipientTrackingEvent2 != null)
					{
						if (internalRecipientTrackingEvents[i].EventDescription == EventDescription.FailedTransportRules || internalRecipientTrackingEvents[i].EventDescription == EventDescription.FailedGeneral)
						{
							flag = true;
						}
						list.Add(recipientTrackingEvent2);
					}
					else
					{
						ExTraceGlobals.TaskTracer.TraceDebug<string, SmtpAddress>(0L, "Event: {0} for recipient {1} not translateable for end-user. It will be dropped", Names<EventDescription>.Map[(int)internalRecipientTrackingEvents[i].EventDescription], internalRecipientTrackingEvents[i].RecipientAddress);
					}
				}
			}
			if (list.Count == 0)
			{
				return null;
			}
			if (recipientTrackingEvent != null)
			{
				list.Add(recipientTrackingEvent);
			}
			return list.ToArray();
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x000166DC File Offset: 0x000148DC
		private static RecipientTrackingEvent[] GetRecipientEventsForSummaryReport(IConfigurationSession configurationSession, IRecipientSession recipientSession, MultiValuedProperty<CultureInfo> userLanguages, bool returnHelpDeskMessages, bool trackingAsSender, RecipientTrackingEvent[] internalRecipientTrackingEvents)
		{
			Dictionary<string, RecipientTrackingEvent> dictionary = new Dictionary<string, RecipientTrackingEvent>(StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < internalRecipientTrackingEvents.Length; i++)
			{
				if (!MessageTrackingReport.ShouldHideEvent(true, internalRecipientTrackingEvents[i], returnHelpDeskMessages, trackingAsSender))
				{
					RecipientTrackingEvent recipientTrackingEvent = RecipientTrackingEvent.Create(true, configurationSession, userLanguages, returnHelpDeskMessages, internalRecipientTrackingEvents[i]);
					if (recipientTrackingEvent == null)
					{
						ExTraceGlobals.TaskTracer.TraceDebug<string, SmtpAddress>(0L, "Event: {0} not translateable for end-user for recipient: {1}, substituting with generic pending event", Names<EventDescription>.Map[(int)internalRecipientTrackingEvents[i].EventDescription], internalRecipientTrackingEvents[i].RecipientAddress);
						RecipientTrackingEvent internalRecipientTrackingEvent = new RecipientTrackingEvent(null, internalRecipientTrackingEvents[i].RecipientAddress, internalRecipientTrackingEvents[i].RecipientDisplayName, DeliveryStatus.Pending, EventType.Pending, EventDescription.Pending, null, internalRecipientTrackingEvents[i].Server, internalRecipientTrackingEvents[i].Date, internalRecipientTrackingEvents[i].InternalMessageId, null, internalRecipientTrackingEvents[i].HiddenRecipient, new bool?(internalRecipientTrackingEvents[i].BccRecipient), internalRecipientTrackingEvents[i].RootAddress, null, null);
						recipientTrackingEvent = RecipientTrackingEvent.Create(true, configurationSession, userLanguages, returnHelpDeskMessages, internalRecipientTrackingEvent);
						if (recipientTrackingEvent == null)
						{
							throw new InvalidOperationException("Generic pending event should always be creatable");
						}
					}
					RecipientTrackingEvent recipientTrackingEvent2;
					if (!dictionary.TryGetValue(recipientTrackingEvent.RecipientAddress.ToString(), out recipientTrackingEvent2) || recipientTrackingEvent2.Status != _DeliveryStatus.Delivered)
					{
						dictionary[recipientTrackingEvent.RecipientAddress.ToString()] = recipientTrackingEvent;
					}
				}
			}
			if (dictionary.Count == 0)
			{
				return null;
			}
			return dictionary.Values.ToArray<RecipientTrackingEvent>();
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00016828 File Offset: 0x00014A28
		private static bool ShouldHideEvent(bool isLastKnownStatus, RecipientTrackingEvent trackingEvent, bool returnHelpDeskMessages, bool trackingAsSender)
		{
			if (isLastKnownStatus && (trackingEvent.EventDescription == EventDescription.TransferredToPartnerOrg || trackingEvent.EventDescription == EventDescription.SmtpSendCrossForest))
			{
				return false;
			}
			if (trackingEvent.HiddenRecipient && trackingAsSender && !returnHelpDeskMessages)
			{
				ExTraceGlobals.TaskTracer.TraceDebug<SmtpAddress>(0L, "Should hide recipient {0}, as it is a hidden recipient and may not be shown to InfoWorker role", trackingEvent.RecipientAddress);
				return true;
			}
			return false;
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00016878 File Offset: 0x00014A78
		internal void IncrementEventTypeCount(_DeliveryStatus typeToIncrement)
		{
			switch (typeToIncrement)
			{
			case _DeliveryStatus.Unsuccessful:
				this.UnsuccessfulCount++;
				return;
			case _DeliveryStatus.Pending:
				this.PendingCount++;
				return;
			case _DeliveryStatus.Delivered:
			case _DeliveryStatus.Read:
				this.DeliveredCount++;
				return;
			case _DeliveryStatus.Transferred:
				this.TransferredCount++;
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x000168E4 File Offset: 0x00014AE4
		private MessageTrackingReport(MessageTrackingReport internalMessageTrackingReport, RecipientTrackingEvent[] recipientTrackingEvents)
		{
			this.internalMessageTrackingReport = internalMessageTrackingReport;
			this[MessageTrackingSharedResultSchema.MessageTrackingReportId] = new MessageTrackingReportId(internalMessageTrackingReport.MessageTrackingReportId);
			this[MessageTrackingSharedResultSchema.FromAddress] = internalMessageTrackingReport.FromAddress;
			this[MessageTrackingSharedResultSchema.FromDisplayName] = null;
			this[MessageTrackingSharedResultSchema.RecipientAddresses] = internalMessageTrackingReport.RecipientAddresses;
			this[MessageTrackingSharedResultSchema.RecipientDisplayNames] = null;
			this[MessageTrackingReportSchema.RecipientTrackingEvents] = recipientTrackingEvents;
			this[MessageTrackingSharedResultSchema.Subject] = internalMessageTrackingReport.Subject;
			this[MessageTrackingSharedResultSchema.SubmittedDateTime] = internalMessageTrackingReport.SubmittedDateTime;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00016998 File Offset: 0x00014B98
		private void FillDisplayNames(BulkRecipientLookupCache recipientNamesCache, IRecipientSession galSession)
		{
			string text = this.internalMessageTrackingReport.FromAddress.ToString();
			SmtpAddress[] recipientAddresses = this.internalMessageTrackingReport.RecipientAddresses;
			IEnumerable<string> addresses = (from address in recipientAddresses
			select address.ToString()).Concat(new string[]
			{
				text
			});
			IEnumerable<string> source = recipientNamesCache.Resolve(addresses, galSession);
			this[MessageTrackingSharedResultSchema.RecipientDisplayNames] = source.Take(recipientAddresses.Length).ToArray<string>();
			this[MessageTrackingSharedResultSchema.FromDisplayName] = source.Last<string>();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00016A38 File Offset: 0x00014C38
		private void PrepareRecipientTrackingEvents(bool returnHelpDeskMessages, bool summaryMode)
		{
			if (!returnHelpDeskMessages)
			{
				foreach (RecipientTrackingEvent recipientTrackingEvent in this.RecipientTrackingEvents)
				{
					if (recipientTrackingEvent.EventDescriptionEnum != EventDescription.Expanded)
					{
						recipientTrackingEvent.EventData = null;
					}
					recipientTrackingEvent.Server = null;
				}
				return;
			}
			if (!summaryMode)
			{
				bool flag = false;
				foreach (RecipientTrackingEvent recipientTrackingEvent2 in this.RecipientTrackingEvents)
				{
					if (recipientTrackingEvent2.EventDescriptionEnum == EventDescription.Delivered)
					{
						flag = true;
					}
					else if (flag)
					{
						recipientTrackingEvent2.Server = null;
					}
				}
			}
		}

		// Token: 0x04000205 RID: 517
		private const int MaxDisplaynameLookupsAllowed = 256;

		// Token: 0x04000206 RID: 518
		private static MessageTrackingReportSchema schema = ObjectSchema.GetInstance<MessageTrackingReportSchema>();

		// Token: 0x04000207 RID: 519
		private MessageTrackingReport internalMessageTrackingReport;
	}
}
