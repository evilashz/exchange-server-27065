using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000012 RID: 18
	public class DataClassificationSourceInfo
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004342 File Offset: 0x00002542
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000434A File Offset: 0x0000254A
		public int SourceId { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004353 File Offset: 0x00002553
		// (set) Token: 0x0600009E RID: 158 RVA: 0x0000435B File Offset: 0x0000255B
		public string SourceName { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004364 File Offset: 0x00002564
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000436C File Offset: 0x0000256C
		public string TopLevelSourceName { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004375 File Offset: 0x00002575
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000437D File Offset: 0x0000257D
		public int Count { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004386 File Offset: 0x00002586
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x0000438E File Offset: 0x0000258E
		public uint ConfidenceLevel { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004397 File Offset: 0x00002597
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000439F File Offset: 0x0000259F
		[XmlIgnore]
		public List<DataClassificationMatchLocation> Locations { get; set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x000043A8 File Offset: 0x000025A8
		private DataClassificationSourceInfo()
		{
			this.Locations = new List<DataClassificationMatchLocation>();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000043BC File Offset: 0x000025BC
		public DataClassificationSourceInfo(int sourceId, string sourceName, string topLevelSourceName, int count, uint confidence)
		{
			if (string.IsNullOrEmpty(sourceName))
			{
				throw new ArgumentNullException("sourceName");
			}
			if (string.IsNullOrEmpty(topLevelSourceName))
			{
				throw new ArgumentNullException("topLevelSourceName");
			}
			this.SourceId = sourceId;
			this.SourceName = sourceName;
			this.TopLevelSourceName = topLevelSourceName;
			this.Count = count;
			this.ConfidenceLevel = confidence;
			this.Locations = new List<DataClassificationMatchLocation>();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000442D File Offset: 0x0000262D
		public static string ToString(IEnumerable<DataClassificationSourceInfo> dataClassificationSourceInfos)
		{
			if (dataClassificationSourceInfos == null)
			{
				return string.Empty;
			}
			return string.Join(";", from dataClassificationSourceInfo in dataClassificationSourceInfos
			select dataClassificationSourceInfo.ToString());
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004468 File Offset: 0x00002668
		public override string ToString()
		{
			return string.Format("Name:{0}/Id:{1}/TopLevelName:{2}/Count:{3}/Confidence:{4}.", new object[]
			{
				this.SourceName,
				this.SourceId,
				this.TopLevelSourceName,
				this.Count,
				this.ConfidenceLevel
			});
		}

		// Token: 0x040000C1 RID: 193
		private const string ToStringFormat = "Name:{0}/Id:{1}/TopLevelName:{2}/Count:{3}/Confidence:{4}.";
	}
}
