using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B0 RID: 1200
	[DataContract]
	public class UMCallDataRecordRow : UMCallBaseRow
	{
		// Token: 0x06003B61 RID: 15201 RVA: 0x000B36E2 File Offset: 0x000B18E2
		public UMCallDataRecordRow(UMCallDataRecord report) : base(report)
		{
			this.UMCallDataRecord = report;
		}

		// Token: 0x17002373 RID: 9075
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000B36F2 File Offset: 0x000B18F2
		// (set) Token: 0x06003B63 RID: 15203 RVA: 0x000B36FA File Offset: 0x000B18FA
		private UMCallDataRecord UMCallDataRecord { get; set; }

		// Token: 0x17002374 RID: 9076
		// (get) Token: 0x06003B64 RID: 15204 RVA: 0x000B3703 File Offset: 0x000B1903
		// (set) Token: 0x06003B65 RID: 15205 RVA: 0x000B371A File Offset: 0x000B191A
		[DataMember]
		public string Date
		{
			get
			{
				return ((ExDateTime)this.UMCallDataRecord.Date).ToUserDateTimeString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002375 RID: 9077
		// (get) Token: 0x06003B66 RID: 15206 RVA: 0x000B3724 File Offset: 0x000B1924
		// (set) Token: 0x06003B67 RID: 15207 RVA: 0x000B374A File Offset: 0x000B194A
		[DataMember]
		public string Duration
		{
			get
			{
				return this.UMCallDataRecord.Duration.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002376 RID: 9078
		// (get) Token: 0x06003B68 RID: 15208 RVA: 0x000B3751 File Offset: 0x000B1951
		// (set) Token: 0x06003B69 RID: 15209 RVA: 0x000B3763 File Offset: 0x000B1963
		[DataMember]
		public string AudioCodec
		{
			get
			{
				return UMUtils.FormatAudioQualityMetricDisplay(this.UMCallDataRecord.AudioCodec);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002377 RID: 9079
		// (get) Token: 0x06003B6A RID: 15210 RVA: 0x000B376A File Offset: 0x000B196A
		// (set) Token: 0x06003B6B RID: 15211 RVA: 0x000B3777 File Offset: 0x000B1977
		[DataMember]
		public string DialPlan
		{
			get
			{
				return this.UMCallDataRecord.DialPlan;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002378 RID: 9080
		// (get) Token: 0x06003B6C RID: 15212 RVA: 0x000B377E File Offset: 0x000B197E
		// (set) Token: 0x06003B6D RID: 15213 RVA: 0x000B3791 File Offset: 0x000B1991
		[DataMember]
		public string CallType
		{
			get
			{
				return this.GetLocalizedCallType(this.UMCallDataRecord.CallType);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002379 RID: 9081
		// (get) Token: 0x06003B6E RID: 15214 RVA: 0x000B3798 File Offset: 0x000B1998
		// (set) Token: 0x06003B6F RID: 15215 RVA: 0x000B37A5 File Offset: 0x000B19A5
		[DataMember]
		public string CallingNumber
		{
			get
			{
				return this.UMCallDataRecord.CallingNumber;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700237A RID: 9082
		// (get) Token: 0x06003B70 RID: 15216 RVA: 0x000B37AC File Offset: 0x000B19AC
		// (set) Token: 0x06003B71 RID: 15217 RVA: 0x000B37B9 File Offset: 0x000B19B9
		[DataMember]
		public string CalledNumber
		{
			get
			{
				return this.UMCallDataRecord.CalledNumber;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700237B RID: 9083
		// (get) Token: 0x06003B72 RID: 15218 RVA: 0x000B37C0 File Offset: 0x000B19C0
		// (set) Token: 0x06003B73 RID: 15219 RVA: 0x000B37CD File Offset: 0x000B19CD
		[DataMember]
		public string Gateway
		{
			get
			{
				return this.UMCallDataRecord.Gateway;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x06003B74 RID: 15220 RVA: 0x000B37D4 File Offset: 0x000B19D4
		// (set) Token: 0x06003B75 RID: 15221 RVA: 0x000B37E1 File Offset: 0x000B19E1
		[DataMember]
		public string UserMailboxName
		{
			get
			{
				return this.UMCallDataRecord.UserMailboxName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000B37E8 File Offset: 0x000B19E8
		private string GetLocalizedCallType(string callType)
		{
			switch (callType)
			{
			case "CallAnsweringMissedCall":
				return Strings.UMReportingCAMissedCall;
			case "CallAnsweringVoiceMessage":
				return Strings.UMReportingCAVoiceMessage;
			case "FindMe":
				return Strings.UMReportingFindMe;
			case "PlayOnPhone":
				return Strings.UMReportingPlayOnPhone;
			case "PlayOnPhonePAAGreeting":
				return Strings.UMReportingPlayOnPhonePAAGreeting;
			case "PromptProvisioning":
				return Strings.UMReportingPromptProvisioning;
			case "SubscriberAccess":
				return Strings.UMReportingSubscriberAccess;
			case "UnAuthenticatedPilotNumber":
				return Strings.UMReportingUnAuthenticatedPilotNumber;
			case "VirtualNumberCall":
				return Strings.UMReportingVirtualNumberCall;
			}
			return callType;
		}
	}
}
