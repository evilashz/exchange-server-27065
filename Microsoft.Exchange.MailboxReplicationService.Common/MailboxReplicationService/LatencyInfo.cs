using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000047 RID: 71
	[DataContract]
	[Serializable]
	public class LatencyInfo
	{
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00005DCD File Offset: 0x00003FCD
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00005DD5 File Offset: 0x00003FD5
		[DataMember(Name = "Current", IsRequired = false)]
		[XmlElement(ElementName = "Current")]
		public int Current { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00005DDE File Offset: 0x00003FDE
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00005DE6 File Offset: 0x00003FE6
		[XmlElement(ElementName = "Average")]
		[DataMember(Name = "Average", IsRequired = false)]
		public int Average { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00005DEF File Offset: 0x00003FEF
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00005DF7 File Offset: 0x00003FF7
		[XmlElement(ElementName = "NumberOfLatencySamplingCalls")]
		[DataMember(Name = "NumberOfLatencySamplingCalls", IsRequired = false)]
		public int NumberOfLatencySamplingCalls { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00005E00 File Offset: 0x00004000
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00005E08 File Offset: 0x00004008
		[XmlElement(ElementName = "TotalNumberOfRemoteCalls")]
		[DataMember(Name = "TotalNumberOfRemoteCalls", IsRequired = false)]
		public int TotalNumberOfRemoteCalls { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00005E11 File Offset: 0x00004011
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00005E19 File Offset: 0x00004019
		[XmlIgnore]
		[IgnoreDataMember]
		public TimeSpan TotalRemoteCallDuration { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00005E24 File Offset: 0x00004024
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00005E3F File Offset: 0x0000403F
		[XmlElement(ElementName = "TotalRemoteCallDurationTicks")]
		[DataMember(Name = "TotalRemoteCallDurationTicks", IsRequired = false)]
		public long TotalRemoteCallDurationTicks
		{
			get
			{
				return this.TotalRemoteCallDuration.Ticks;
			}
			set
			{
				this.TotalRemoteCallDuration = new TimeSpan(value);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00005E4D File Offset: 0x0000404D
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00005E55 File Offset: 0x00004055
		[XmlElement(ElementName = "Min")]
		[DataMember(Name = "Min", IsRequired = false)]
		public int Min { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00005E5E File Offset: 0x0000405E
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00005E66 File Offset: 0x00004066
		[DataMember(Name = "Max", IsRequired = false)]
		[XmlElement(ElementName = "Max")]
		public int Max { get; set; }

		// Token: 0x06000363 RID: 867 RVA: 0x00005E6F File Offset: 0x0000406F
		public LatencyInfo()
		{
			this.Min = int.MaxValue;
			this.Max = int.MinValue;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00005E90 File Offset: 0x00004090
		public static LatencyInfo operator +(LatencyInfo latencyInfo1, LatencyInfo latencyInfo2)
		{
			if (latencyInfo2.TotalNumberOfRemoteCalls == 0)
			{
				return latencyInfo1;
			}
			if (latencyInfo1.TotalNumberOfRemoteCalls == 0)
			{
				return latencyInfo2;
			}
			LatencyInfo latencyInfo3 = new LatencyInfo();
			latencyInfo3.NumberOfLatencySamplingCalls = latencyInfo1.NumberOfLatencySamplingCalls + latencyInfo2.NumberOfLatencySamplingCalls;
			latencyInfo3.Current = latencyInfo2.Current;
			latencyInfo3.TotalNumberOfRemoteCalls = latencyInfo1.TotalNumberOfRemoteCalls + latencyInfo2.TotalNumberOfRemoteCalls;
			latencyInfo3.TotalRemoteCallDuration = latencyInfo1.TotalRemoteCallDuration + latencyInfo2.TotalRemoteCallDuration;
			if (latencyInfo3.NumberOfLatencySamplingCalls != 0)
			{
				latencyInfo3.Average = (latencyInfo1.Average * latencyInfo1.NumberOfLatencySamplingCalls + latencyInfo2.Average * latencyInfo2.NumberOfLatencySamplingCalls) / latencyInfo3.NumberOfLatencySamplingCalls;
				latencyInfo3.Min = Math.Min(latencyInfo1.Min, latencyInfo2.Min);
				latencyInfo3.Max = Math.Max(latencyInfo1.Max, latencyInfo2.Max);
			}
			return latencyInfo3;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00005F60 File Offset: 0x00004160
		public void AddSample(int latency)
		{
			if (latency > this.Max)
			{
				this.Max = latency;
			}
			if (latency < this.Min)
			{
				this.Min = latency;
			}
			this.Current = latency;
			this.Average = (this.Average * this.NumberOfLatencySamplingCalls + latency) / (this.NumberOfLatencySamplingCalls + 1);
			this.NumberOfLatencySamplingCalls++;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00005FC0 File Offset: 0x000041C0
		public override string ToString()
		{
			return string.Format("Current: {0}ms, Avg:{1}ms, NumberOfLatencySamplingCalls:{2}, Min:{3}ms, Max: {4}ms, TotalNumberOfRemoteCalls:{5}, TotalRemoteCallDuration: {6}", new object[]
			{
				this.Current,
				this.Average,
				this.NumberOfLatencySamplingCalls,
				this.Min,
				this.Max,
				this.TotalNumberOfRemoteCalls,
				this.TotalRemoteCallDuration.TotalMilliseconds
			});
		}
	}
}
