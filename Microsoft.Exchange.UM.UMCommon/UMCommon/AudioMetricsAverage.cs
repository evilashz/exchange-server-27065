using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.UM.TroubleshootingTool.Shared;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000144 RID: 324
	[DataContract(Name = "AudioMetricsAverage", Namespace = "http://schemas.microsoft.com/v1.0/UMReportAggregatedData")]
	[XmlType(TypeName = "AudioMetricsAverageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class AudioMetricsAverage
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x00026910 File Offset: 0x00024B10
		public AudioMetricsAverage()
		{
			this.TotalValue = 0.0;
			this.TotalCount = 0.0;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00026936 File Offset: 0x00024B36
		public AudioMetricsAverage(AudioMetricsAverageType audioMetricsAverageType)
		{
			this.TotalCount = audioMetricsAverageType.TotalCount;
			this.TotalValue = audioMetricsAverageType.TotalValue;
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00026956 File Offset: 0x00024B56
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x0002695E File Offset: 0x00024B5E
		[DataMember(Name = "TotalValue")]
		[XmlElement]
		public double TotalValue { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00026967 File Offset: 0x00024B67
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0002696F File Offset: 0x00024B6F
		[XmlElement]
		[DataMember(Name = "TotalCount")]
		public double TotalCount { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00026978 File Offset: 0x00024B78
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0002699F File Offset: 0x00024B9F
		[DataMember(Name = "Average")]
		public double Average
		{
			get
			{
				if (this.TotalCount == 0.0)
				{
					return (double)AudioQuality.UnknownValue;
				}
				return this.TotalValue / this.TotalCount;
			}
			set
			{
			}
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000269A1 File Offset: 0x00024BA1
		public void Add(double value)
		{
			this.TotalCount += 1.0;
			this.TotalValue += value;
		}
	}
}
