using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000C3 RID: 195
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UMReportRawCountersType
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x0001FF89 File Offset: 0x0001E189
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x0001FF91 File Offset: 0x0001E191
		public long AutoAttendantCalls
		{
			get
			{
				return this.autoAttendantCallsField;
			}
			set
			{
				this.autoAttendantCallsField = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x0001FF9A File Offset: 0x0001E19A
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x0001FFA2 File Offset: 0x0001E1A2
		public long FailedCalls
		{
			get
			{
				return this.failedCallsField;
			}
			set
			{
				this.failedCallsField = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001FFAB File Offset: 0x0001E1AB
		// (set) Token: 0x0600097C RID: 2428 RVA: 0x0001FFB3 File Offset: 0x0001E1B3
		public long FaxCalls
		{
			get
			{
				return this.faxCallsField;
			}
			set
			{
				this.faxCallsField = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001FFBC File Offset: 0x0001E1BC
		// (set) Token: 0x0600097E RID: 2430 RVA: 0x0001FFC4 File Offset: 0x0001E1C4
		public long MissedCalls
		{
			get
			{
				return this.missedCallsField;
			}
			set
			{
				this.missedCallsField = value;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001FFCD File Offset: 0x0001E1CD
		// (set) Token: 0x06000980 RID: 2432 RVA: 0x0001FFD5 File Offset: 0x0001E1D5
		public long OtherCalls
		{
			get
			{
				return this.otherCallsField;
			}
			set
			{
				this.otherCallsField = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0001FFDE File Offset: 0x0001E1DE
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x0001FFE6 File Offset: 0x0001E1E6
		public long OutboundCalls
		{
			get
			{
				return this.outboundCallsField;
			}
			set
			{
				this.outboundCallsField = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0001FFEF File Offset: 0x0001E1EF
		// (set) Token: 0x06000984 RID: 2436 RVA: 0x0001FFF7 File Offset: 0x0001E1F7
		public long SubscriberAccessCalls
		{
			get
			{
				return this.subscriberAccessCallsField;
			}
			set
			{
				this.subscriberAccessCallsField = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00020000 File Offset: 0x0001E200
		// (set) Token: 0x06000986 RID: 2438 RVA: 0x00020008 File Offset: 0x0001E208
		public long VoiceMailCalls
		{
			get
			{
				return this.voiceMailCallsField;
			}
			set
			{
				this.voiceMailCallsField = value;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00020011 File Offset: 0x0001E211
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x00020019 File Offset: 0x0001E219
		public long TotalCalls
		{
			get
			{
				return this.totalCallsField;
			}
			set
			{
				this.totalCallsField = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00020022 File Offset: 0x0001E222
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x0002002A File Offset: 0x0001E22A
		public DateTime Date
		{
			get
			{
				return this.dateField;
			}
			set
			{
				this.dateField = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00020033 File Offset: 0x0001E233
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x0002003B File Offset: 0x0001E23B
		public UMReportAudioMetricsAverageCountersType AudioMetricsAverages
		{
			get
			{
				return this.audioMetricsAveragesField;
			}
			set
			{
				this.audioMetricsAveragesField = value;
			}
		}

		// Token: 0x04000577 RID: 1399
		private long autoAttendantCallsField;

		// Token: 0x04000578 RID: 1400
		private long failedCallsField;

		// Token: 0x04000579 RID: 1401
		private long faxCallsField;

		// Token: 0x0400057A RID: 1402
		private long missedCallsField;

		// Token: 0x0400057B RID: 1403
		private long otherCallsField;

		// Token: 0x0400057C RID: 1404
		private long outboundCallsField;

		// Token: 0x0400057D RID: 1405
		private long subscriberAccessCallsField;

		// Token: 0x0400057E RID: 1406
		private long voiceMailCallsField;

		// Token: 0x0400057F RID: 1407
		private long totalCallsField;

		// Token: 0x04000580 RID: 1408
		private DateTime dateField;

		// Token: 0x04000581 RID: 1409
		private UMReportAudioMetricsAverageCountersType audioMetricsAveragesField;
	}
}
