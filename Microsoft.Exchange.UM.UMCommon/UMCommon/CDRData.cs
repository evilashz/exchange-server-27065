using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000145 RID: 325
	[DataContract(Name = "CDRData", Namespace = "http://schemas.microsoft.com/version1/CDRData")]
	[XmlType(TypeName = "CDRDataType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class CDRData
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x000269C7 File Offset: 0x00024BC7
		// (set) Token: 0x06000A3B RID: 2619 RVA: 0x000269CF File Offset: 0x00024BCF
		[DataMember(Name = "CallStartTime")]
		[XmlElement]
		public DateTime CallStartTime { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x000269D8 File Offset: 0x00024BD8
		// (set) Token: 0x06000A3D RID: 2621 RVA: 0x000269E0 File Offset: 0x00024BE0
		[XmlElement]
		[DataMember(Name = "CallType")]
		public string CallType { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000269E9 File Offset: 0x00024BE9
		// (set) Token: 0x06000A3F RID: 2623 RVA: 0x000269F1 File Offset: 0x00024BF1
		[DataMember(Name = "CallIdentity")]
		[XmlElement]
		public string CallIdentity { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000269FA File Offset: 0x00024BFA
		// (set) Token: 0x06000A41 RID: 2625 RVA: 0x00026A02 File Offset: 0x00024C02
		[XmlElement]
		[DataMember(Name = "ParentCallIdentity")]
		public string ParentCallIdentity { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00026A0B File Offset: 0x00024C0B
		// (set) Token: 0x06000A43 RID: 2627 RVA: 0x00026A13 File Offset: 0x00024C13
		[DataMember(Name = "UMServerName")]
		[XmlElement]
		public string UMServerName { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00026A1C File Offset: 0x00024C1C
		// (set) Token: 0x06000A45 RID: 2629 RVA: 0x00026A24 File Offset: 0x00024C24
		[XmlElement]
		[DataMember(Name = "DialPlanGuid")]
		public Guid DialPlanGuid { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x00026A2D File Offset: 0x00024C2D
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x00026A35 File Offset: 0x00024C35
		[DataMember(Name = "DialPlanName")]
		[XmlElement]
		public string DialPlanName { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00026A3E File Offset: 0x00024C3E
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00026A46 File Offset: 0x00024C46
		[DataMember(Name = "CallDuration")]
		[XmlElement]
		public int CallDuration { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00026A4F File Offset: 0x00024C4F
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00026A57 File Offset: 0x00024C57
		[DataMember(Name = "IPGatewayAddress")]
		[XmlElement]
		public string IPGatewayAddress { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00026A60 File Offset: 0x00024C60
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00026A68 File Offset: 0x00024C68
		[XmlElement]
		[DataMember(Name = "IPGatewayName")]
		public string IPGatewayName { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00026A71 File Offset: 0x00024C71
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00026A79 File Offset: 0x00024C79
		[DataMember(Name = "GatewayGuid")]
		[XmlElement]
		public Guid GatewayGuid { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00026A82 File Offset: 0x00024C82
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00026A8A File Offset: 0x00024C8A
		[DataMember(Name = "CalledPhoneNumber")]
		[XmlElement]
		public string CalledPhoneNumber { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x00026A93 File Offset: 0x00024C93
		// (set) Token: 0x06000A53 RID: 2643 RVA: 0x00026A9B File Offset: 0x00024C9B
		[XmlElement]
		[DataMember(Name = "CallerPhoneNumber")]
		public string CallerPhoneNumber { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00026AA4 File Offset: 0x00024CA4
		// (set) Token: 0x06000A55 RID: 2645 RVA: 0x00026AAC File Offset: 0x00024CAC
		[DataMember(Name = "OfferResult")]
		[XmlElement]
		public string OfferResult { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00026AB5 File Offset: 0x00024CB5
		// (set) Token: 0x06000A57 RID: 2647 RVA: 0x00026ABD File Offset: 0x00024CBD
		[XmlElement]
		[DataMember(Name = "DropCallReason")]
		public string DropCallReason { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00026AC6 File Offset: 0x00024CC6
		// (set) Token: 0x06000A59 RID: 2649 RVA: 0x00026ACE File Offset: 0x00024CCE
		[DataMember(Name = "ReasonForCall")]
		[XmlElement]
		public string ReasonForCall { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00026AD7 File Offset: 0x00024CD7
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00026ADF File Offset: 0x00024CDF
		[DataMember(Name = "TransferredNumber")]
		[XmlElement]
		public string TransferredNumber { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00026AE8 File Offset: 0x00024CE8
		// (set) Token: 0x06000A5D RID: 2653 RVA: 0x00026AF0 File Offset: 0x00024CF0
		[DataMember(Name = "DialedString")]
		[XmlElement]
		public string DialedString { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00026AF9 File Offset: 0x00024CF9
		// (set) Token: 0x06000A5F RID: 2655 RVA: 0x00026B01 File Offset: 0x00024D01
		[DataMember(Name = "CallerMailboxAlias")]
		[XmlElement]
		public string CallerMailboxAlias { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00026B0A File Offset: 0x00024D0A
		// (set) Token: 0x06000A61 RID: 2657 RVA: 0x00026B12 File Offset: 0x00024D12
		[XmlElement]
		[DataMember(Name = "CalleeMailboxAlias")]
		public string CalleeMailboxAlias { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00026B1B File Offset: 0x00024D1B
		// (set) Token: 0x06000A63 RID: 2659 RVA: 0x00026B23 File Offset: 0x00024D23
		[DataMember(Name = "CallerLegacyExchangeDN")]
		[XmlElement]
		public string CallerLegacyExchangeDN { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00026B2C File Offset: 0x00024D2C
		// (set) Token: 0x06000A65 RID: 2661 RVA: 0x00026B34 File Offset: 0x00024D34
		[DataMember(Name = "CalleeLegacyExchangeDN")]
		[XmlElement]
		public string CalleeLegacyExchangeDN { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00026B3D File Offset: 0x00024D3D
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x00026B45 File Offset: 0x00024D45
		[DataMember(Name = "AutoAttendantName")]
		[XmlElement]
		public string AutoAttendantName { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00026B4E File Offset: 0x00024D4E
		// (set) Token: 0x06000A69 RID: 2665 RVA: 0x00026B56 File Offset: 0x00024D56
		[DataMember(Name = "EDiscoveryUserObjectGuid")]
		[XmlIgnore]
		public Guid EDiscoveryUserObjectGuid { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00026B5F File Offset: 0x00024D5F
		// (set) Token: 0x06000A6B RID: 2667 RVA: 0x00026B67 File Offset: 0x00024D67
		[DataMember(Name = "AudioQualityMetrics")]
		[XmlElement]
		public AudioQuality AudioQualityMetrics { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00026B70 File Offset: 0x00024D70
		// (set) Token: 0x06000A6D RID: 2669 RVA: 0x00026B78 File Offset: 0x00024D78
		[DataMember(Name = "TenantGuid")]
		[XmlIgnore]
		public Guid TenantGuid { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00026B81 File Offset: 0x00024D81
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00026B89 File Offset: 0x00024D89
		[XmlElement]
		public DateTime CreationTime { get; set; }

		// Token: 0x06000A70 RID: 2672 RVA: 0x00026B94 File Offset: 0x00024D94
		public CDRData() : this(default(DateTime), null, null, null, null, Guid.Empty, null, 0, null, null, Guid.Empty, null, null, null, null, null, null, null, null, null, null, null, null, Guid.Empty, AudioQuality.CreateDefaultAudioQuality(), Guid.Empty)
		{
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x00026BE0 File Offset: 0x00024DE0
		public CDRData(DateTime callStartTime, string callType, string callId, string parentCallIdentity, string umServerName, Guid dialPlanGuid, string dialPlanName, int callDuration, string ipGatewayAddress, string ipGatewayName, Guid gatewayGuid, string calledPhoneNumber, string callerPhoneNumber, string offerResult, string dropCallReason, string reasonForCall, string transferredNumber, string dialedString, string callerMailboxAlias, string calleeMailboxAlias, string callerLegacyExchangeDN, string calleeLegacyExchangeDN, string autoAttendantName, Guid ediscoveryUserObjectGuid, AudioQuality audioQualitymetrics, Guid tenantGuid)
		{
			this.CallStartTime = callStartTime;
			this.CallType = callType;
			this.CallIdentity = callId;
			this.ParentCallIdentity = parentCallIdentity;
			this.UMServerName = umServerName;
			this.DialPlanGuid = dialPlanGuid;
			this.DialPlanName = dialPlanName;
			this.CallDuration = callDuration;
			this.IPGatewayAddress = ipGatewayAddress;
			this.IPGatewayName = ipGatewayName;
			this.GatewayGuid = gatewayGuid;
			this.CalledPhoneNumber = calledPhoneNumber;
			this.CallerPhoneNumber = callerPhoneNumber;
			this.OfferResult = offerResult;
			this.DropCallReason = dropCallReason;
			this.ReasonForCall = reasonForCall;
			this.TransferredNumber = transferredNumber;
			this.DialedString = dialedString;
			this.CallerMailboxAlias = callerMailboxAlias;
			this.CalleeMailboxAlias = calleeMailboxAlias;
			this.CallerLegacyExchangeDN = callerLegacyExchangeDN;
			this.CalleeLegacyExchangeDN = calleeLegacyExchangeDN;
			this.AutoAttendantName = autoAttendantName;
			this.EDiscoveryUserObjectGuid = ediscoveryUserObjectGuid;
			this.AudioQualityMetrics = audioQualitymetrics;
			this.TenantGuid = tenantGuid;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00026CC0 File Offset: 0x00024EC0
		public CDRDataType ToCDRDataType()
		{
			return new CDRDataType
			{
				CallStartTime = this.CallStartTime,
				CallType = this.CallType,
				CallIdentity = this.CallIdentity,
				ParentCallIdentity = this.ParentCallIdentity,
				UMServerName = this.UMServerName,
				DialPlanGuid = this.DialPlanGuid.ToString(),
				DialPlanName = this.DialPlanName,
				CallDuration = this.CallDuration,
				IPGatewayAddress = this.IPGatewayAddress,
				IPGatewayName = this.IPGatewayName,
				GatewayGuid = this.GatewayGuid.ToString(),
				CalledPhoneNumber = this.CalledPhoneNumber,
				CallerPhoneNumber = this.CallerPhoneNumber,
				OfferResult = this.OfferResult,
				DropCallReason = this.DropCallReason,
				ReasonForCall = this.ReasonForCall,
				TransferredNumber = this.TransferredNumber,
				DialedString = this.DialedString,
				CallerMailboxAlias = this.CallerMailboxAlias,
				CalleeMailboxAlias = this.CalleeMailboxAlias,
				CallerLegacyExchangeDN = this.CallerLegacyExchangeDN,
				CalleeLegacyExchangeDN = this.CalleeLegacyExchangeDN,
				AutoAttendantName = this.AutoAttendantName,
				AudioQualityMetrics = new AudioQualityType(),
				AudioQualityMetrics = 
				{
					AudioCodec = this.AudioQualityMetrics.AudioCodec,
					BurstDensity = this.AudioQualityMetrics.BurstDensity,
					BurstDuration = this.AudioQualityMetrics.BurstDuration,
					Jitter = this.AudioQualityMetrics.Jitter,
					NMOS = this.AudioQualityMetrics.NMOS,
					NMOSDegradation = this.AudioQualityMetrics.NMOSDegradation,
					NMOSDegradationJitter = this.AudioQualityMetrics.NMOSDegradationJitter,
					NMOSDegradationPacketLoss = this.AudioQualityMetrics.NMOSDegradationPacketLoss,
					PacketLoss = this.AudioQualityMetrics.PacketLoss,
					RoundTrip = this.AudioQualityMetrics.RoundTrip
				}
			};
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00026EEC File Offset: 0x000250EC
		public CDRData(CDRDataType ewsType)
		{
			this.CallStartTime = ewsType.CallStartTime;
			this.CallType = ewsType.CallType;
			this.CallIdentity = ewsType.CallIdentity;
			this.ParentCallIdentity = ewsType.ParentCallIdentity;
			this.UMServerName = ewsType.UMServerName;
			this.DialPlanGuid = new Guid(ewsType.DialPlanGuid);
			this.DialPlanName = ewsType.DialPlanName;
			this.CallDuration = ewsType.CallDuration;
			this.IPGatewayAddress = ewsType.IPGatewayAddress;
			this.IPGatewayName = ewsType.IPGatewayName;
			this.GatewayGuid = new Guid(ewsType.GatewayGuid);
			this.CalledPhoneNumber = ewsType.CalledPhoneNumber;
			this.CallerPhoneNumber = ewsType.CallerPhoneNumber;
			this.OfferResult = ewsType.OfferResult;
			this.DropCallReason = ewsType.DropCallReason;
			this.ReasonForCall = ewsType.ReasonForCall;
			this.TransferredNumber = ewsType.TransferredNumber;
			this.DialedString = ewsType.DialedString;
			this.CallerMailboxAlias = ewsType.CallerMailboxAlias;
			this.CalleeMailboxAlias = ewsType.CalleeMailboxAlias;
			this.CallerLegacyExchangeDN = ewsType.CallerLegacyExchangeDN;
			this.CalleeLegacyExchangeDN = ewsType.CalleeLegacyExchangeDN;
			this.AutoAttendantName = ewsType.AutoAttendantName;
			this.AudioQualityMetrics = AudioQuality.CreateDefaultAudioQuality();
			this.AudioQualityMetrics.AudioCodec = ewsType.AudioQualityMetrics.AudioCodec;
			this.AudioQualityMetrics.BurstDensity = ewsType.AudioQualityMetrics.BurstDensity;
			this.AudioQualityMetrics.BurstDuration = ewsType.AudioQualityMetrics.BurstDuration;
			this.AudioQualityMetrics.Jitter = ewsType.AudioQualityMetrics.Jitter;
			this.AudioQualityMetrics.NMOS = ewsType.AudioQualityMetrics.NMOS;
			this.AudioQualityMetrics.NMOSDegradation = ewsType.AudioQualityMetrics.NMOSDegradation;
			this.AudioQualityMetrics.NMOSDegradationJitter = ewsType.AudioQualityMetrics.NMOSDegradationJitter;
			this.AudioQualityMetrics.NMOSDegradationPacketLoss = ewsType.AudioQualityMetrics.NMOSDegradationPacketLoss;
			this.AudioQualityMetrics.PacketLoss = ewsType.AudioQualityMetrics.PacketLoss;
			this.AudioQualityMetrics.RoundTrip = ewsType.AudioQualityMetrics.RoundTrip;
			this.TenantGuid = Guid.Empty;
			this.EDiscoveryUserObjectGuid = Guid.Empty;
		}
	}
}
