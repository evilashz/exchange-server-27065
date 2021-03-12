using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004B6 RID: 1206
	[DataContract]
	public class UMCallSummaryReportRow : UMCallBaseRow
	{
		// Token: 0x06003B8C RID: 15244 RVA: 0x000B3FC1 File Offset: 0x000B21C1
		public UMCallSummaryReportRow(UMCallSummaryReport report) : base(report)
		{
			this.UMCallSummaryReport = report;
		}

		// Token: 0x17002381 RID: 9089
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x000B3FD1 File Offset: 0x000B21D1
		// (set) Token: 0x06003B8E RID: 15246 RVA: 0x000B3FD9 File Offset: 0x000B21D9
		private UMCallSummaryReport UMCallSummaryReport { get; set; }

		// Token: 0x17002382 RID: 9090
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x000B3FE2 File Offset: 0x000B21E2
		// (set) Token: 0x06003B90 RID: 15248 RVA: 0x000B3FEF File Offset: 0x000B21EF
		[DataMember]
		public string Date
		{
			get
			{
				return this.UMCallSummaryReport.Date;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002383 RID: 9091
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000B3FF6 File Offset: 0x000B21F6
		// (set) Token: 0x06003B92 RID: 15250 RVA: 0x000B4009 File Offset: 0x000B2209
		[DataMember]
		public string AutoAttendant
		{
			get
			{
				return this.ConvertToPercentage(this.UMCallSummaryReport.AutoAttendant);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002384 RID: 9092
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x000B4010 File Offset: 0x000B2210
		// (set) Token: 0x06003B94 RID: 15252 RVA: 0x000B4023 File Offset: 0x000B2223
		[DataMember]
		public string Failed
		{
			get
			{
				return this.ConvertToPercentage(this.UMCallSummaryReport.FailedOrRejectedCalls);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002385 RID: 9093
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000B402A File Offset: 0x000B222A
		// (set) Token: 0x06003B96 RID: 15254 RVA: 0x000B403D File Offset: 0x000B223D
		[DataMember]
		public string Fax
		{
			get
			{
				return this.ConvertToPercentage(this.UMCallSummaryReport.Fax);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002386 RID: 9094
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x000B4044 File Offset: 0x000B2244
		// (set) Token: 0x06003B98 RID: 15256 RVA: 0x000B4057 File Offset: 0x000B2257
		[DataMember]
		public string MissedCalls
		{
			get
			{
				return this.ConvertToPercentage(this.UMCallSummaryReport.MissedCalls);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002387 RID: 9095
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x000B405E File Offset: 0x000B225E
		// (set) Token: 0x06003B9A RID: 15258 RVA: 0x000B4071 File Offset: 0x000B2271
		[DataMember]
		public string Outbound
		{
			get
			{
				return this.ConvertToPercentage(this.UMCallSummaryReport.Outbound);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002388 RID: 9096
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x000B4078 File Offset: 0x000B2278
		// (set) Token: 0x06003B9C RID: 15260 RVA: 0x000B408B File Offset: 0x000B228B
		[DataMember]
		public string Other
		{
			get
			{
				return this.ConvertToPercentage(this.UMCallSummaryReport.OtherCalls);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17002389 RID: 9097
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x000B4092 File Offset: 0x000B2292
		// (set) Token: 0x06003B9E RID: 15262 RVA: 0x000B40A5 File Offset: 0x000B22A5
		[DataMember]
		public string SubscriberAccess
		{
			get
			{
				return this.ConvertToPercentage(this.UMCallSummaryReport.SubscriberAccess);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700238A RID: 9098
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x000B40AC File Offset: 0x000B22AC
		// (set) Token: 0x06003BA0 RID: 15264 RVA: 0x000B40BF File Offset: 0x000B22BF
		[DataMember]
		public string VoiceMessages
		{
			get
			{
				return this.ConvertToPercentage(this.UMCallSummaryReport.VoiceMessages);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700238B RID: 9099
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x000B40C8 File Offset: 0x000B22C8
		// (set) Token: 0x06003BA2 RID: 15266 RVA: 0x000B40E8 File Offset: 0x000B22E8
		[DataMember]
		public string TotalCalls
		{
			get
			{
				return this.UMCallSummaryReport.TotalCalls.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700238C RID: 9100
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000B40F0 File Offset: 0x000B22F0
		// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x000B4110 File Offset: 0x000B2310
		[DataMember]
		public string TotalAudioQualityCallsSampled
		{
			get
			{
				return this.UMCallSummaryReport.TotalAudioQualityCallsSampled.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700238D RID: 9101
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x000B4117 File Offset: 0x000B2317
		// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x000B4141 File Offset: 0x000B2341
		[DataMember]
		public string UMDialPlanName
		{
			get
			{
				if (string.IsNullOrEmpty(this.UMCallSummaryReport.UMDialPlanName))
				{
					return Strings.AllDialplans;
				}
				return this.UMCallSummaryReport.UMDialPlanName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700238E RID: 9102
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000B4148 File Offset: 0x000B2348
		// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x000B4172 File Offset: 0x000B2372
		[DataMember]
		public string UMIPGatewayName
		{
			get
			{
				if (string.IsNullOrEmpty(this.UMCallSummaryReport.UMIPGatewayName))
				{
					return Strings.AllGateways;
				}
				return this.UMCallSummaryReport.UMIPGatewayName;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700238F RID: 9103
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x000B4179 File Offset: 0x000B2379
		// (set) Token: 0x06003BAA RID: 15274 RVA: 0x000B4181 File Offset: 0x000B2381
		[DataMember]
		public bool IsExportCallDataEnabled { get; set; }

		// Token: 0x17002390 RID: 9104
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x000B418A File Offset: 0x000B238A
		// (set) Token: 0x06003BAC RID: 15276 RVA: 0x000B4192 File Offset: 0x000B2392
		[DataMember]
		public string UMDialPlanID { get; set; }

		// Token: 0x17002391 RID: 9105
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000B419B File Offset: 0x000B239B
		// (set) Token: 0x06003BAE RID: 15278 RVA: 0x000B41A3 File Offset: 0x000B23A3
		[DataMember]
		public string UMIPGatewayID { get; set; }

		// Token: 0x06003BAF RID: 15279 RVA: 0x000B41AC File Offset: 0x000B23AC
		private string ConvertToPercentage(ulong val)
		{
			if (this.UMCallSummaryReport.TotalCalls > 0UL)
			{
				return (val / this.UMCallSummaryReport.TotalCalls).ToString("#0.0%");
			}
			return "-";
		}
	}
}
