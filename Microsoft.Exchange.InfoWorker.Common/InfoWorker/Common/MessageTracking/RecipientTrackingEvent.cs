using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability.Proxy;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002D5 RID: 725
	[Serializable]
	internal sealed class RecipientTrackingEvent
	{
		// Token: 0x060014B6 RID: 5302 RVA: 0x000608F4 File Offset: 0x0005EAF4
		public static RecipientTrackingEvent Create(string domain, Microsoft.Exchange.SoapWebClient.EWS.RecipientTrackingEventType wsRecipientTrackingEvent)
		{
			return RecipientTrackingEvent.Create(domain, (wsRecipientTrackingEvent.Recipient == null) ? null : wsRecipientTrackingEvent.Recipient.EmailAddress, (wsRecipientTrackingEvent.Recipient == null) ? null : wsRecipientTrackingEvent.Recipient.Name, wsRecipientTrackingEvent.DeliveryStatus, wsRecipientTrackingEvent.EventDescription, wsRecipientTrackingEvent.EventData, wsRecipientTrackingEvent.Server, wsRecipientTrackingEvent.Date, wsRecipientTrackingEvent.InternalId, wsRecipientTrackingEvent.UniquePathId, wsRecipientTrackingEvent.HiddenRecipient, new bool?(wsRecipientTrackingEvent.BccRecipient), wsRecipientTrackingEvent.RootAddress, wsRecipientTrackingEvent.Properties);
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0006097C File Offset: 0x0005EB7C
		public static RecipientTrackingEvent Create(string domain, Microsoft.Exchange.InfoWorker.Common.Availability.Proxy.RecipientTrackingEventType rdRecipientTrackingEvent)
		{
			return RecipientTrackingEvent.Create(domain, (rdRecipientTrackingEvent.Recipient == null) ? null : rdRecipientTrackingEvent.Recipient.EmailAddress, (rdRecipientTrackingEvent.Recipient == null) ? null : rdRecipientTrackingEvent.Recipient.Name, rdRecipientTrackingEvent.DeliveryStatus, rdRecipientTrackingEvent.EventDescription, rdRecipientTrackingEvent.EventData, rdRecipientTrackingEvent.Server, rdRecipientTrackingEvent.Date, rdRecipientTrackingEvent.InternalId, rdRecipientTrackingEvent.UniquePathId, rdRecipientTrackingEvent.HiddenRecipient, new bool?(rdRecipientTrackingEvent.BccRecipient), rdRecipientTrackingEvent.RootAddress, MessageConverter.CopyTrackingProperties(rdRecipientTrackingEvent.Properties));
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x00060A08 File Offset: 0x0005EC08
		private static RecipientTrackingEvent Create(string domain, string recipientEmail, string recipientDisplayName, string deliveryStatusString, string eventDescriptionString, string[] eventData, string server, DateTime date, string internalIdString, string uniquePathId, bool hiddenRecipient, bool? bccRecipient, string rootAddress, Microsoft.Exchange.SoapWebClient.EWS.TrackingPropertyType[] properties)
		{
			if (string.IsNullOrEmpty(recipientEmail))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Null recipient address in WS-RecipientTrackingEvent: {0}", recipientEmail);
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "Null recipient in WS-response", new object[0]);
			}
			SmtpAddress smtpAddress = new SmtpAddress(recipientEmail);
			if (!smtpAddress.IsValidAddress)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<SmtpAddress>(0, "Corrupt recipient address in RD-RecipientTrackingEvent: {0}", smtpAddress);
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "Invalid recipient address {0} in WS-response", new object[]
				{
					smtpAddress
				});
			}
			recipientDisplayName = (recipientDisplayName ?? smtpAddress.ToString());
			DeliveryStatus deliveryStatus;
			if (!EnumValidator<DeliveryStatus>.TryParse(deliveryStatusString, EnumParseOptions.Default, out deliveryStatus))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(0, "Skipping event because of unknown delivery-status value in WS-RecipientTrackingEvent: {0}", deliveryStatusString);
				return null;
			}
			EventDescription eventDescription;
			if (!EnumValidator<EventDescription>.TryParse(eventDescriptionString, EnumParseOptions.Default, out eventDescription))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(0, "Skipping event because of unknown event-description in WS-RecipientTrackingEvent: {0}", eventDescriptionString);
				return null;
			}
			if (string.IsNullOrEmpty(internalIdString))
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Null or empty internalIdString in RD-RecipientTrackingEvent: {0}", internalIdString);
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "No InternalId {0} in WS-response", new object[0]);
			}
			long num = 0L;
			if (!long.TryParse(internalIdString, out num) || num < 0L)
			{
				TraceWrapper.SearchLibraryTracer.TraceError<string>(0, "Non-numeric or negative internalIdString in RD-RecipientTrackingEvent: {0}", internalIdString);
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "Invalid InternalId {0} in WS-response", new object[]
				{
					internalIdString
				});
			}
			TrackingExtendedProperties trackingExtendedProperties = TrackingExtendedProperties.CreateFromTrackingPropertyArray(properties);
			if (eventDescription == EventDescription.PendingModeration && !string.IsNullOrEmpty(trackingExtendedProperties.ArbitrationMailboxAddress) && !SmtpAddress.IsValidSmtpAddress(trackingExtendedProperties.ArbitrationMailboxAddress))
			{
				TraceWrapper.SearchLibraryTracer.TraceDebug<string>(0, "Arbitration address is in the extended proprties but it's invalid: {0}", trackingExtendedProperties.ArbitrationMailboxAddress);
				TrackingFatalException.RaiseED(ErrorCode.UnexpectedErrorPermanent, "Invalid ArbitrationMailboxAddress property {0} in WS-response", new object[]
				{
					trackingExtendedProperties.ArbitrationMailboxAddress
				});
			}
			return new RecipientTrackingEvent(domain, smtpAddress, recipientDisplayName, deliveryStatus, EventType.Pending, eventDescription, eventData, server, date, num, uniquePathId, hiddenRecipient, bccRecipient, rootAddress, true, trackingExtendedProperties);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00060BC8 File Offset: 0x0005EDC8
		public RecipientTrackingEvent(string domain, SmtpAddress recipientAddress, string recipientDisplayName, DeliveryStatus status, EventType eventType, EventDescription eventDescription, string[] eventData, string serverFqdn, DateTime date, long internalMessageId, string uniquePathId, bool hiddenRecipient, bool? bccRecipient, string rootAddress, string arbitrationMailboxAddress, string initMessageId) : this(domain, recipientAddress, recipientDisplayName, status, eventType, eventDescription, eventData, serverFqdn, date, internalMessageId, uniquePathId, hiddenRecipient, bccRecipient, rootAddress, false, new TrackingExtendedProperties(false, false, null, false, string.Empty, arbitrationMailboxAddress, initMessageId, false))
		{
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00060C10 File Offset: 0x0005EE10
		private RecipientTrackingEvent(string domain, SmtpAddress recipientAddress, string recipientDisplayName, DeliveryStatus status, EventType eventType, EventDescription eventDescription, string[] rawEventData, string serverFqdn, DateTime date, long internalMessageId, string uniquePathId, bool hiddenRecipient, bool? bccRecipient, string rootAddress, bool parseEventData, TrackingExtendedProperties trackingExtendedProperties)
		{
			this.domain = domain;
			this.recipientAddress = recipientAddress;
			this.recipientDisplayName = recipientDisplayName;
			this.status = status;
			this.eventType = eventType;
			this.eventDescription = eventDescription;
			this.server = serverFqdn;
			this.date = date;
			this.internalMessageId = internalMessageId;
			this.uniquePathId = uniquePathId;
			this.hiddenRecipient = hiddenRecipient;
			this.bccRecipient = bccRecipient;
			this.rootAddress = rootAddress;
			this.extendedProperties = trackingExtendedProperties;
			if (parseEventData)
			{
				VersionConverter.ConvertRawEventData(rawEventData, this);
				return;
			}
			this.eventData = rawEventData;
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00060CB0 File Offset: 0x0005EEB0
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x00060CB8 File Offset: 0x0005EEB8
		public DateTime Date
		{
			get
			{
				return this.date;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00060CC0 File Offset: 0x0005EEC0
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x00060CC8 File Offset: 0x0005EEC8
		public SmtpAddress RecipientAddress
		{
			get
			{
				return this.recipientAddress;
			}
			set
			{
				this.recipientAddress = value;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x00060CD1 File Offset: 0x0005EED1
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x00060CD9 File Offset: 0x0005EED9
		public string RecipientDisplayName
		{
			get
			{
				return this.recipientDisplayName;
			}
			set
			{
				this.recipientDisplayName = value;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00060CE2 File Offset: 0x0005EEE2
		public DeliveryStatus Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x00060CEA File Offset: 0x0005EEEA
		public EventType EventType
		{
			get
			{
				return this.eventType;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00060CF2 File Offset: 0x0005EEF2
		public EventDescription EventDescription
		{
			get
			{
				return this.eventDescription;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x00060CFA File Offset: 0x0005EEFA
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x00060D02 File Offset: 0x0005EF02
		public string[] EventData
		{
			get
			{
				return this.eventData;
			}
			internal set
			{
				this.eventData = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00060D0B File Offset: 0x0005EF0B
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x00060D13 File Offset: 0x0005EF13
		public string RootAddress
		{
			get
			{
				return this.rootAddress;
			}
			set
			{
				this.rootAddress = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x00060D1C File Offset: 0x0005EF1C
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x00060D24 File Offset: 0x0005EF24
		public long InternalMessageId
		{
			get
			{
				return this.internalMessageId;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x00060D2C File Offset: 0x0005EF2C
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x00060D34 File Offset: 0x0005EF34
		public string UniquePathId
		{
			get
			{
				return this.uniquePathId;
			}
			set
			{
				this.uniquePathId = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x00060D3D File Offset: 0x0005EF3D
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00060D45 File Offset: 0x0005EF45
		public bool HiddenRecipient
		{
			get
			{
				return this.hiddenRecipient;
			}
			set
			{
				this.hiddenRecipient = value;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x00060D4E File Offset: 0x0005EF4E
		// (set) Token: 0x060014CF RID: 5327 RVA: 0x00060D6A File Offset: 0x0005EF6A
		public bool BccRecipient
		{
			get
			{
				return this.bccRecipient == null || this.bccRecipient.Value;
			}
			set
			{
				this.bccRecipient = new bool?(value);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x00060D78 File Offset: 0x0005EF78
		public string ServerHint
		{
			get
			{
				if ((this.EventDescription == EventDescription.SmtpSend || this.EventDescription == EventDescription.SmtpSendCrossForest || this.EventDescription == EventDescription.SmtpSendCrossSite) && this.eventData.Length >= 2 && !string.IsNullOrEmpty(this.eventData[1]))
				{
					return this.eventData[1];
				}
				return null;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x00060DC7 File Offset: 0x0005EFC7
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x00060DCF File Offset: 0x0005EFCF
		public TrackingExtendedProperties ExtendedProperties
		{
			get
			{
				return this.extendedProperties;
			}
			set
			{
				this.extendedProperties = value;
			}
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00060DD8 File Offset: 0x0005EFD8
		internal RecipientTrackingEvent Clone()
		{
			return new RecipientTrackingEvent(this.domain, this.recipientAddress, this.recipientDisplayName, this.status, this.eventType, this.eventDescription, this.eventData, this.server, this.date, this.internalMessageId, this.uniquePathId, this.hiddenRecipient, this.bccRecipient, this.rootAddress, false, this.extendedProperties);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00060E45 File Offset: 0x0005F045
		internal void ConvertRecipientTrackingEvent(DeliveryStatus status, EventType eventType, EventDescription eventDescription)
		{
			this.status = status;
			this.eventType = eventType;
			this.eventDescription = eventDescription;
		}

		// Token: 0x04000D85 RID: 3461
		private string domain;

		// Token: 0x04000D86 RID: 3462
		private DateTime date;

		// Token: 0x04000D87 RID: 3463
		private SmtpAddress recipientAddress = SmtpAddress.Empty;

		// Token: 0x04000D88 RID: 3464
		private string recipientDisplayName;

		// Token: 0x04000D89 RID: 3465
		private DeliveryStatus status;

		// Token: 0x04000D8A RID: 3466
		private EventType eventType;

		// Token: 0x04000D8B RID: 3467
		private EventDescription eventDescription;

		// Token: 0x04000D8C RID: 3468
		private string[] eventData;

		// Token: 0x04000D8D RID: 3469
		private string server;

		// Token: 0x04000D8E RID: 3470
		private long internalMessageId;

		// Token: 0x04000D8F RID: 3471
		private string uniquePathId;

		// Token: 0x04000D90 RID: 3472
		private bool hiddenRecipient;

		// Token: 0x04000D91 RID: 3473
		private bool? bccRecipient;

		// Token: 0x04000D92 RID: 3474
		private string rootAddress;

		// Token: 0x04000D93 RID: 3475
		private TrackingExtendedProperties extendedProperties;
	}
}
