using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000149 RID: 329
	[XmlType(TypeName = "UMReportAudioMetricsAverageCountersType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "UMReportAudioMetricsAverageCounters", Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData")]
	public class UMReportAudioMetricsAverageCounters
	{
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00027AA9 File Offset: 0x00025CA9
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x00027AB1 File Offset: 0x00025CB1
		[XmlElement]
		[DataMember(Name = "NMOS")]
		public AudioMetricsAverage NMOS { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00027ABA File Offset: 0x00025CBA
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x00027AC2 File Offset: 0x00025CC2
		[DataMember(Name = "NMOSDegradation")]
		[XmlElement]
		public AudioMetricsAverage NMOSDegradation { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00027ACB File Offset: 0x00025CCB
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x00027AD3 File Offset: 0x00025CD3
		[DataMember(Name = "Jitter")]
		[XmlElement]
		public AudioMetricsAverage Jitter { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00027ADC File Offset: 0x00025CDC
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x00027AE4 File Offset: 0x00025CE4
		[XmlElement]
		[DataMember(Name = "PercentPacketLoss")]
		public AudioMetricsAverage PercentPacketLoss { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00027AED File Offset: 0x00025CED
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x00027AF5 File Offset: 0x00025CF5
		[DataMember(Name = "RoundTrip")]
		[XmlElement]
		public AudioMetricsAverage RoundTrip { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x00027AFE File Offset: 0x00025CFE
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x00027B06 File Offset: 0x00025D06
		[DataMember(Name = "BurstLossDuration")]
		[XmlElement]
		public AudioMetricsAverage BurstLossDuration { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00027B0F File Offset: 0x00025D0F
		// (set) Token: 0x06000AB5 RID: 2741 RVA: 0x00027B17 File Offset: 0x00025D17
		[DataMember(Name = "TotalAudioQualityCallsSampled")]
		[XmlElement]
		public ulong TotalAudioQualityCallsSampled { get; set; }

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00027B20 File Offset: 0x00025D20
		public UMReportAudioMetricsAverageCounters()
		{
			this.NMOS = new AudioMetricsAverage();
			this.NMOSDegradation = new AudioMetricsAverage();
			this.Jitter = new AudioMetricsAverage();
			this.PercentPacketLoss = new AudioMetricsAverage();
			this.RoundTrip = new AudioMetricsAverage();
			this.BurstLossDuration = new AudioMetricsAverage();
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00027B78 File Offset: 0x00025D78
		public UMReportAudioMetricsAverageCounters(UMReportAudioMetricsAverageCountersType ewsType)
		{
			this.NMOS = new AudioMetricsAverage(ewsType.NMOS);
			this.Jitter = new AudioMetricsAverage(ewsType.Jitter);
			this.NMOSDegradation = new AudioMetricsAverage(ewsType.NMOSDegradation);
			this.PercentPacketLoss = new AudioMetricsAverage(ewsType.PercentPacketLoss);
			this.RoundTrip = new AudioMetricsAverage(ewsType.RoundTrip);
			this.BurstLossDuration = new AudioMetricsAverage(ewsType.BurstLossDuration);
			this.TotalAudioQualityCallsSampled = (ulong)ewsType.TotalAudioQualityCallsSampled;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00027C00 File Offset: 0x00025E00
		public void AddAudioQualityMetrics(CDRData cdrData)
		{
			bool flag = false;
			this.AddAudioMetricToCounter(cdrData.AudioQualityMetrics.NMOS, this.NMOS, ref flag);
			this.AddAudioMetricToCounter(cdrData.AudioQualityMetrics.NMOSDegradation, this.NMOSDegradation, ref flag);
			this.AddAudioMetricToCounter(cdrData.AudioQualityMetrics.Jitter, this.Jitter, ref flag);
			this.AddAudioMetricToCounter(cdrData.AudioQualityMetrics.PacketLoss, this.PercentPacketLoss, ref flag);
			this.AddAudioMetricToCounter(cdrData.AudioQualityMetrics.RoundTrip, this.RoundTrip, ref flag);
			this.AddAudioMetricToCounter(cdrData.AudioQualityMetrics.BurstDuration, this.BurstLossDuration, ref flag);
			if (flag)
			{
				this.TotalAudioQualityCallsSampled += 1UL;
			}
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00027CB7 File Offset: 0x00025EB7
		private void AddAudioMetricToCounter(float metric, AudioMetricsAverage averageCounter, ref bool cdrSampledForAudioQuality)
		{
			if (metric != AudioQuality.UnknownValue)
			{
				averageCounter.Add((double)metric);
				cdrSampledForAudioQuality = true;
			}
		}
	}
}
