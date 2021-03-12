using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000049 RID: 73
	[DataContract]
	[Serializable]
	public class ProviderInfo : XMLSerializableBase
	{
		// Token: 0x0600036E RID: 878 RVA: 0x0000609D File Offset: 0x0000429D
		public ProviderInfo()
		{
			this.Durations = new List<DurationInfo>();
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600036F RID: 879 RVA: 0x000060B0 File Offset: 0x000042B0
		// (set) Token: 0x06000370 RID: 880 RVA: 0x000060B8 File Offset: 0x000042B8
		[XmlElement(ElementName = "Durations")]
		[DataMember(Name = "Duration", IsRequired = false)]
		public List<DurationInfo> Durations { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000371 RID: 881 RVA: 0x000060C4 File Offset: 0x000042C4
		public TimeSpan TotalDuration
		{
			get
			{
				long num = 0L;
				foreach (DurationInfo durationInfo in this.Durations)
				{
					num += durationInfo.DurationTicks;
				}
				return new TimeSpan(num);
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00006124 File Offset: 0x00004324
		public static ProviderInfo operator +(ProviderInfo providerInfo1, ProviderInfo providerInfo2)
		{
			ProviderInfo providerInfo3 = new ProviderInfo();
			providerInfo3.Durations.AddRange(providerInfo1.Durations);
			Dictionary<string, DurationInfo> dictionary = new Dictionary<string, DurationInfo>();
			for (int i = 0; i < providerInfo1.Durations.Count; i++)
			{
				dictionary.Add(providerInfo1.Durations[i].Name, providerInfo1.Durations[i]);
			}
			SortedList<string, DurationInfo> sortedList = new SortedList<string, DurationInfo>(dictionary);
			foreach (DurationInfo durationInfo in providerInfo2.Durations)
			{
				DurationInfo durationInfo2;
				if (sortedList.TryGetValue(durationInfo.Name, out durationInfo2))
				{
					durationInfo2.Duration += durationInfo.Duration;
				}
				else
				{
					providerInfo3.Durations.Add(durationInfo);
				}
			}
			return providerInfo3;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000620C File Offset: 0x0000440C
		public override string ToString()
		{
			return "Total Execution Time: " + this.TotalDuration.ToString();
		}
	}
}
