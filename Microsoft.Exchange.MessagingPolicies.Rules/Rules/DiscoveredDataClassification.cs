using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200000F RID: 15
	public class DiscoveredDataClassification : DataClassification
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003B08 File Offset: 0x00001D08
		private DiscoveredDataClassification()
		{
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003B10 File Offset: 0x00001D10
		public DiscoveredDataClassification(string id, string name, uint recommendedMinimumConfidence, IEnumerable<DataClassificationSourceInfo> sourceInfos = null) : base(id)
		{
			this.ClassificationName = name;
			this.RecommendedMinimumConfidence = recommendedMinimumConfidence;
			if (sourceInfos != null)
			{
				this.MatchingSourceInfos = new List<DataClassificationSourceInfo>(sourceInfos);
				return;
			}
			this.MatchingSourceInfos = new List<DataClassificationSourceInfo>();
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003B44 File Offset: 0x00001D44
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003B4C File Offset: 0x00001D4C
		public string ClassificationName { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003B5D File Offset: 0x00001D5D
		public int TotalCount
		{
			get
			{
				if (!this.MatchingSourceInfos.Any<DataClassificationSourceInfo>())
				{
					return 0;
				}
				return this.MatchingSourceInfos.Sum((DataClassificationSourceInfo matchingSourceInfo) => matchingSourceInfo.Count);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003B9E File Offset: 0x00001D9E
		public uint MaxConfidenceLevel
		{
			get
			{
				if (!this.MatchingSourceInfos.Any<DataClassificationSourceInfo>())
				{
					return 0U;
				}
				return this.MatchingSourceInfos.Max((DataClassificationSourceInfo matchingSourceInfo) => matchingSourceInfo.ConfidenceLevel);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003BD7 File Offset: 0x00001DD7
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00003BDF File Offset: 0x00001DDF
		public uint RecommendedMinimumConfidence { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003BE8 File Offset: 0x00001DE8
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003BF0 File Offset: 0x00001DF0
		public List<DataClassificationSourceInfo> MatchingSourceInfos { get; set; }

		// Token: 0x06000077 RID: 119 RVA: 0x00003BFC File Offset: 0x00001DFC
		public static string SerializeToXml(IEnumerable<DiscoveredDataClassification> discoveredDataClassifications)
		{
			if (discoveredDataClassifications != null)
			{
				List<DiscoveredDataClassification> list = new List<DiscoveredDataClassification>();
				foreach (DiscoveredDataClassification item in discoveredDataClassifications)
				{
					list.Add(item);
				}
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DiscoveredDataClassification>));
				using (StringWriter stringWriter = new StringWriter())
				{
					xmlSerializer.Serialize(stringWriter, list);
					return stringWriter.ToString();
				}
			}
			return null;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003C94 File Offset: 0x00001E94
		public static IEnumerable<DiscoveredDataClassification> DeserializeFromXml(string xml)
		{
			if (!string.IsNullOrEmpty(xml))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<DiscoveredDataClassification>));
				using (StringReader stringReader = new StringReader(xml))
				{
					return (IEnumerable<DiscoveredDataClassification>)xmlSerializer.Deserialize(stringReader);
				}
			}
			return null;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public static string ToString(IEnumerable<DiscoveredDataClassification> discoveredDataClassifications)
		{
			if (discoveredDataClassifications == null)
			{
				return string.Empty;
			}
			return string.Join(";", from discoveredDataClassification in discoveredDataClassifications
			select discoveredDataClassification.ToString());
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003D34 File Offset: 0x00001F34
		public override string ToString()
		{
			string format = "Name:{0}/Id:{1}/Count:{2}/Confidence:{3}/MatchingSourceInfos:{4}.";
			object[] array = new object[5];
			array[0] = this.ClassificationName;
			array[1] = base.Id;
			array[2] = this.TotalCount;
			array[3] = this.MaxConfidenceLevel;
			array[4] = string.Join(";", from matchingSourceInfo in this.MatchingSourceInfos
			select matchingSourceInfo.ToString());
			return string.Format(format, array);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public IEnumerable<DataClassificationMatchLocation> Locations
		{
			get
			{
				List<DataClassificationMatchLocation> list = new List<DataClassificationMatchLocation>();
				foreach (DataClassificationSourceInfo dataClassificationSourceInfo in this.MatchingSourceInfos)
				{
					list.AddRange(dataClassificationSourceInfo.Locations);
				}
				return list;
			}
		}

		// Token: 0x040000A5 RID: 165
		private const string ToStringFormat = "Name:{0}/Id:{1}/Count:{2}/Confidence:{3}/MatchingSourceInfos:{4}.";

		// Token: 0x040000A6 RID: 166
		public static readonly string RecommendedWeightKey = "RecommendedWeight";
	}
}
