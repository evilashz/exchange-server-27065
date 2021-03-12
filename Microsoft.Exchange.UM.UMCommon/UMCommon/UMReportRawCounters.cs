using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000148 RID: 328
	[XmlType(TypeName = "UMReportRawCountersType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "UMReportRawCounters", Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData")]
	public class UMReportRawCounters
	{
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002769B File Offset: 0x0002589B
		// (set) Token: 0x06000A8D RID: 2701 RVA: 0x000276A3 File Offset: 0x000258A3
		[DataMember(Name = "AutoAttendantCalls")]
		[XmlElement]
		public ulong AutoAttendantCalls { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x000276AC File Offset: 0x000258AC
		// (set) Token: 0x06000A8F RID: 2703 RVA: 0x000276B4 File Offset: 0x000258B4
		[DataMember(Name = "FailedCalls")]
		[XmlElement]
		public ulong FailedCalls { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x000276BD File Offset: 0x000258BD
		// (set) Token: 0x06000A91 RID: 2705 RVA: 0x000276C5 File Offset: 0x000258C5
		[DataMember(Name = "FaxCalls")]
		[XmlElement]
		public ulong FaxCalls { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x000276CE File Offset: 0x000258CE
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x000276D6 File Offset: 0x000258D6
		[DataMember(Name = "MissedCalls")]
		[XmlElement]
		public ulong MissedCalls { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000276DF File Offset: 0x000258DF
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x000276E7 File Offset: 0x000258E7
		[DataMember(Name = "OtherCalls")]
		[XmlElement]
		public ulong OtherCalls { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000276F0 File Offset: 0x000258F0
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x000276F8 File Offset: 0x000258F8
		[DataMember(Name = "OutboundCalls")]
		[XmlElement]
		public ulong OutboundCalls { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00027701 File Offset: 0x00025901
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x00027709 File Offset: 0x00025909
		[XmlElement]
		[DataMember(Name = "SubscriberAccessCalls")]
		public ulong SubscriberAccessCalls { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00027712 File Offset: 0x00025912
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0002771A File Offset: 0x0002591A
		[XmlElement]
		[DataMember(Name = "VoiceMailCalls")]
		public ulong VoiceMailCalls { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00027723 File Offset: 0x00025923
		// (set) Token: 0x06000A9D RID: 2717 RVA: 0x0002772B File Offset: 0x0002592B
		[XmlElement]
		[DataMember(Name = "TotalCalls")]
		public ulong TotalCalls { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A9E RID: 2718 RVA: 0x00027734 File Offset: 0x00025934
		// (set) Token: 0x06000A9F RID: 2719 RVA: 0x0002773C File Offset: 0x0002593C
		[DataMember(Name = "Date")]
		[XmlElement]
		public DateTime Date { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000AA0 RID: 2720 RVA: 0x00027745 File Offset: 0x00025945
		// (set) Token: 0x06000AA1 RID: 2721 RVA: 0x0002774D File Offset: 0x0002594D
		[DataMember(Name = "AudioMetricsAverages")]
		[XmlElement]
		public UMReportAudioMetricsAverageCounters AudioMetricsAverages { get; set; }

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00027758 File Offset: 0x00025958
		public UMReportRawCounters() : this(default(DateTime))
		{
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00027774 File Offset: 0x00025974
		public UMReportRawCounters(DateTime dateTime)
		{
			this.AutoAttendantCalls = 0UL;
			this.Date = dateTime;
			this.FailedCalls = 0UL;
			this.FaxCalls = 0UL;
			this.MissedCalls = 0UL;
			this.OutboundCalls = 0UL;
			this.OtherCalls = 0UL;
			this.SubscriberAccessCalls = 0UL;
			this.VoiceMailCalls = 0UL;
			this.TotalCalls = 0UL;
			this.AudioMetricsAverages = new UMReportAudioMetricsAverageCounters();
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000277E4 File Offset: 0x000259E4
		public void AddCDR(CDRData cdrData)
		{
			this.TotalCalls += 1UL;
			this.AudioMetricsAverages.AddAudioQualityMetrics(cdrData);
			if (!this.TryIncrementFailedCalls(cdrData))
			{
				string callType;
				switch (callType = cdrData.CallType)
				{
				case "AutoAttendant":
					this.AutoAttendantCalls += 1UL;
					return;
				case "CallAnsweringMissedCall":
					this.MissedCalls += 1UL;
					return;
				case "CallAnsweringVoiceMessage":
					this.VoiceMailCalls += 1UL;
					return;
				case "Fax":
					this.FaxCalls += 1UL;
					return;
				case "PlayOnPhone":
				case "PlayOnPhonePAAGreeting":
				case "FindMe":
					this.OutboundCalls += 1UL;
					return;
				case "SubscriberAccess":
					this.SubscriberAccessCalls += 1UL;
					return;
				case "PromptProvisioning":
				case "UnAuthenticatedPilotNumber":
				case "VirtualNumberCall":
					this.OtherCalls += 1UL;
					return;
				}
				this.OtherCalls += 1UL;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002798C File Offset: 0x00025B8C
		private bool TryIncrementFailedCalls(CDRData cdrData)
		{
			if (string.Equals(cdrData.CallType, "None", StringComparison.OrdinalIgnoreCase) || string.Equals(cdrData.DropCallReason, "SystemError", StringComparison.OrdinalIgnoreCase) || string.Equals(cdrData.OfferResult, "Reject", StringComparison.OrdinalIgnoreCase) || string.Equals(cdrData.DropCallReason, "OutboundFailedCall", StringComparison.OrdinalIgnoreCase))
			{
				this.FailedCalls += 1UL;
				return true;
			}
			return false;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000279F8 File Offset: 0x00025BF8
		public UMReportRawCounters(UMReportRawCountersType rawCountersType)
		{
			this.AutoAttendantCalls = (ulong)rawCountersType.AutoAttendantCalls;
			this.Date = rawCountersType.Date;
			this.FailedCalls = (ulong)rawCountersType.FailedCalls;
			this.FaxCalls = (ulong)rawCountersType.FaxCalls;
			this.MissedCalls = (ulong)rawCountersType.MissedCalls;
			this.OutboundCalls = (ulong)rawCountersType.OutboundCalls;
			this.OtherCalls = (ulong)rawCountersType.OtherCalls;
			this.SubscriberAccessCalls = (ulong)rawCountersType.SubscriberAccessCalls;
			this.VoiceMailCalls = (ulong)rawCountersType.VoiceMailCalls;
			this.TotalCalls = (ulong)rawCountersType.TotalCalls;
			this.AudioMetricsAverages = new UMReportAudioMetricsAverageCounters(rawCountersType.AudioMetricsAverages);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00027A94 File Offset: 0x00025C94
		[OnDeserialized]
		private void Initialize(StreamingContext context)
		{
			if (this.AudioMetricsAverages == null)
			{
				this.AudioMetricsAverages = new UMReportAudioMetricsAverageCounters();
			}
		}
	}
}
