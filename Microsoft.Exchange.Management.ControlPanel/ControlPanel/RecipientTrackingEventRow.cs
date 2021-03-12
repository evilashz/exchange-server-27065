using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.Management.Tracking;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002DF RID: 735
	[DataContract]
	public class RecipientTrackingEventRow : BaseRow
	{
		// Token: 0x06002CD3 RID: 11475 RVA: 0x00089D8D File Offset: 0x00087F8D
		public RecipientTrackingEventRow(MessageTrackingReport messageTrackingReport) : base(messageTrackingReport)
		{
			this.MessageTrackingReport = messageTrackingReport;
			this.numberOfEvents = messageTrackingReport.RecipientTrackingEvents.Length;
		}

		// Token: 0x17001E05 RID: 7685
		// (get) Token: 0x06002CD4 RID: 11476 RVA: 0x00089DAB File Offset: 0x00087FAB
		// (set) Token: 0x06002CD5 RID: 11477 RVA: 0x00089DB3 File Offset: 0x00087FB3
		public MessageTrackingReport MessageTrackingReport { get; private set; }

		// Token: 0x17001E06 RID: 7686
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x00089DBC File Offset: 0x00087FBC
		// (set) Token: 0x06002CD7 RID: 11479 RVA: 0x00089DD7 File Offset: 0x00087FD7
		public string RecipientDisplayName
		{
			get
			{
				return this.MessageTrackingReport.RecipientTrackingEvents[this.numberOfEvents - 1].RecipientDisplayName;
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E07 RID: 7687
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x00089DE0 File Offset: 0x00087FE0
		// (set) Token: 0x06002CD9 RID: 11481 RVA: 0x00089E14 File Offset: 0x00088014
		public string RecipientEmail
		{
			get
			{
				return this.MessageTrackingReport.RecipientTrackingEvents[this.numberOfEvents - 1].RecipientAddress.ToString();
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E08 RID: 7688
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x00089E1B File Offset: 0x0008801B
		// (set) Token: 0x06002CDB RID: 11483 RVA: 0x00089E42 File Offset: 0x00088042
		[DataMember]
		public string RecipientDisplayNameAndEmail
		{
			get
			{
				return RtlUtil.ConvertToDecodedBidiString(this.RecipientDisplayName + " (" + this.RecipientEmail + ")", RtlUtil.IsRtl);
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E09 RID: 7689
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x00089E7A File Offset: 0x0008807A
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x00089E98 File Offset: 0x00088098
		[DataMember]
		public IEnumerable<TrackingEventRow> Events
		{
			get
			{
				return from trackingEvent in this.MessageTrackingReport.RecipientTrackingEvents
				select new TrackingEventRow((TrackingEventType)trackingEvent.EventType, trackingEvent.Date, this.GetLocalizedStringForEventType(trackingEvent.EventType), trackingEvent.EventDescription, trackingEvent.Server, trackingEvent.EventData);
			}
			protected set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x00089EA0 File Offset: 0x000880A0
		private string GetLocalizedStringForEventType(EventType eventType)
		{
			string empty = string.Empty;
			switch (eventType)
			{
			case EventType.SmtpReceive:
			case EventType.SmtpSend:
				break;
			case EventType.Fail:
				return OwaOptionStrings.MessageTrackingFailedEvent;
			case EventType.Deliver:
				return OwaOptionStrings.MessageTrackingDeliveredEvent;
			case EventType.Resolve:
			case EventType.Redirect:
				goto IL_76;
			case EventType.Expand:
				return OwaOptionStrings.MessageTrackingDLExpandedEvent;
			case EventType.Submit:
				return OwaOptionStrings.MessageTrackingSubmitEvent;
			default:
				if (eventType != EventType.Transferred)
				{
					goto IL_76;
				}
				break;
			}
			return OwaOptionStrings.MessageTrackingTransferredEvent;
			IL_76:
			return OwaOptionStrings.MessageTrackingPendingEvent;
		}

		// Token: 0x04002224 RID: 8740
		private int numberOfEvents;
	}
}
