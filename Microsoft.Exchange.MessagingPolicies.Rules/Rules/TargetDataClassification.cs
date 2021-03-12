using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000010 RID: 16
	public class TargetDataClassification : DataClassification
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003E24 File Offset: 0x00002024
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00003E2C File Offset: 0x0000202C
		public int MinCount { get; protected set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003E35 File Offset: 0x00002035
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00003E3D File Offset: 0x0000203D
		public int MaxCount { get; protected set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003E46 File Offset: 0x00002046
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00003E4E File Offset: 0x0000204E
		public int MinConfidence { get; protected set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003E57 File Offset: 0x00002057
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003E5F File Offset: 0x0000205F
		public int MaxConfidence { get; protected set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003E68 File Offset: 0x00002068
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003E70 File Offset: 0x00002070
		public string OpaqueData { get; protected set; }

		// Token: 0x0600008B RID: 139 RVA: 0x00003E79 File Offset: 0x00002079
		public TargetDataClassification(string id, int minCount, int maxCount, int minConfidence, int maxConfidence, string opaqueData) : base(id)
		{
			this.MinCount = minCount;
			this.MaxCount = maxCount;
			this.MinConfidence = minConfidence;
			this.MaxConfidence = maxConfidence;
			this.OpaqueData = opaqueData;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003EA8 File Offset: 0x000020A8
		internal TargetDataClassification(IEnumerable<KeyValuePair<string, string>> keyValueParameters)
		{
			base.Id = string.Empty;
			this.MinCount = 0;
			this.MaxCount = TargetDataClassification.IgnoreMaxCount;
			this.MinConfidence = TargetDataClassification.UseRecommendedMinConfidence;
			this.MaxConfidence = 0;
			foreach (KeyValuePair<string, string> keyValuePair in keyValueParameters)
			{
				if (string.Compare(keyValuePair.Key, TargetDataClassification.IdKey, true) == 0)
				{
					base.Id = keyValuePair.Value;
				}
				else if (string.Compare(keyValuePair.Key, TargetDataClassification.MinCountKey, true) == 0)
				{
					this.MinCount = Convert.ToInt32(keyValuePair.Value);
				}
				else if (string.Compare(keyValuePair.Key, TargetDataClassification.MaxCountKey, true) == 0)
				{
					this.MaxCount = Convert.ToInt32(keyValuePair.Value);
				}
				else if (string.Compare(keyValuePair.Key, TargetDataClassification.MinConfidenceKey, true) == 0)
				{
					this.MinConfidence = Convert.ToInt32(keyValuePair.Value);
				}
				else if (string.Compare(keyValuePair.Key, TargetDataClassification.MaxConfidenceKey, true) == 0)
				{
					this.MaxConfidence = Convert.ToInt32(keyValuePair.Value);
				}
				else if (string.Compare(keyValuePair.Key, TargetDataClassification.OpaqueDataKey, true) == 0)
				{
					this.OpaqueData = keyValuePair.Value;
				}
			}
			if (string.IsNullOrEmpty(base.Id) || this.MinCount < 1 || (this.MaxCount < 1 && this.MaxCount != TargetDataClassification.IgnoreMaxCount) || (this.MinConfidence < 1 && this.MinConfidence != TargetDataClassification.UseRecommendedMinConfidence) || this.MaxConfidence < 1 || this.MaxConfidence > TargetDataClassification.MaxAllowedConfidenceValue)
			{
				throw new ArgumentException(RulesStrings.InvalidKeyValueParameter("DataClassification"));
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004088 File Offset: 0x00002288
		internal ShortList<KeyValuePair<string, string>> ToKeyValueCollection()
		{
			return new ShortList<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>(TargetDataClassification.IdKey, base.Id),
				new KeyValuePair<string, string>(TargetDataClassification.MinCountKey, this.MinCount.ToString()),
				new KeyValuePair<string, string>(TargetDataClassification.MaxCountKey, this.MaxCount.ToString()),
				new KeyValuePair<string, string>(TargetDataClassification.MinConfidenceKey, this.MinConfidence.ToString()),
				new KeyValuePair<string, string>(TargetDataClassification.MaxConfidenceKey, this.MaxConfidence.ToString()),
				new KeyValuePair<string, string>(TargetDataClassification.OpaqueDataKey, this.OpaqueData)
			};
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004144 File Offset: 0x00002344
		internal bool Matches(DiscoveredDataClassification discovered)
		{
			return discovered.Id == base.Id && discovered.TotalCount >= this.MinCount && (this.MaxCount == TargetDataClassification.IgnoreMaxCount || discovered.TotalCount <= this.MaxCount) && ((this.MinConfidence == TargetDataClassification.UseRecommendedMinConfidence && discovered.MaxConfidenceLevel >= discovered.RecommendedMinimumConfidence) || (this.MinConfidence != TargetDataClassification.UseRecommendedMinConfidence && (ulong)discovered.MaxConfidenceLevel >= (ulong)((long)this.MinConfidence))) && (ulong)discovered.MaxConfidenceLevel <= (ulong)((long)this.MaxConfidence);
		}

		// Token: 0x040000AE RID: 174
		public static readonly int UseRecommendedMinConfidence = -1;

		// Token: 0x040000AF RID: 175
		public static readonly int IgnoreMaxCount = -1;

		// Token: 0x040000B0 RID: 176
		public static readonly int MaxAllowedConfidenceValue = 100;

		// Token: 0x040000B1 RID: 177
		public static readonly string IdKey = "id";

		// Token: 0x040000B2 RID: 178
		public static readonly string MinCountKey = "minCount";

		// Token: 0x040000B3 RID: 179
		public static readonly string MaxCountKey = "maxCount";

		// Token: 0x040000B4 RID: 180
		public static readonly string MinConfidenceKey = "minConfidence";

		// Token: 0x040000B5 RID: 181
		public static readonly string MaxConfidenceKey = "maxConfidence";

		// Token: 0x040000B6 RID: 182
		public static readonly string OpaqueDataKey = "opaqueData";
	}
}
