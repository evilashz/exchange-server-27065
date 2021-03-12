using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001A4 RID: 420
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UMReportRawCountersType
	{
		// Token: 0x040009C9 RID: 2505
		public long AutoAttendantCalls;

		// Token: 0x040009CA RID: 2506
		public long FailedCalls;

		// Token: 0x040009CB RID: 2507
		public long FaxCalls;

		// Token: 0x040009CC RID: 2508
		public long MissedCalls;

		// Token: 0x040009CD RID: 2509
		public long OtherCalls;

		// Token: 0x040009CE RID: 2510
		public long OutboundCalls;

		// Token: 0x040009CF RID: 2511
		public long SubscriberAccessCalls;

		// Token: 0x040009D0 RID: 2512
		public long VoiceMailCalls;

		// Token: 0x040009D1 RID: 2513
		public long TotalCalls;

		// Token: 0x040009D2 RID: 2514
		public DateTime Date;

		// Token: 0x040009D3 RID: 2515
		public UMReportAudioMetricsAverageCountersType AudioMetricsAverages;
	}
}
