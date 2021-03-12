using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001A5 RID: 421
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class UMReportAudioMetricsAverageCountersType
	{
		// Token: 0x040009D4 RID: 2516
		public AudioMetricsAverageType NMOS;

		// Token: 0x040009D5 RID: 2517
		public AudioMetricsAverageType NMOSDegradation;

		// Token: 0x040009D6 RID: 2518
		public AudioMetricsAverageType Jitter;

		// Token: 0x040009D7 RID: 2519
		public AudioMetricsAverageType PercentPacketLoss;

		// Token: 0x040009D8 RID: 2520
		public AudioMetricsAverageType RoundTrip;

		// Token: 0x040009D9 RID: 2521
		public AudioMetricsAverageType BurstLossDuration;

		// Token: 0x040009DA RID: 2522
		public long TotalAudioQualityCallsSampled;
	}
}
