using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001D4 RID: 468
	[Serializable]
	public class BlobRulePackage : ISerializable
	{
		// Token: 0x060013A3 RID: 5027 RVA: 0x0003B138 File Offset: 0x00039338
		public BlobRulePackage()
		{
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0003B140 File Offset: 0x00039340
		public BlobRulePackage(SerializationInfo info, StreamingContext context)
		{
			this.RuleIDs = (long[])info.GetValue("RuleID", typeof(long[]));
			this.Uris = (string[])info.GetValue("RuleUri", typeof(string[]));
			this.Scores = (int[])info.GetValue("RuleScore", typeof(int[]));
			this.IsActiveRules = (bool[])info.GetValue("IsActive", typeof(bool[]));
			this.MajorVerison = (int)info.GetValue("MajorVersion", typeof(int));
			this.MinorVerison = (int)info.GetValue("MinorVersion", typeof(int));
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0003B214 File Offset: 0x00039414
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("RuleID", this.RuleIDs);
			info.AddValue("IsActive", this.IsActiveRules);
			info.AddValue("RuleScore", this.Scores);
			info.AddValue("RuleUri", this.Uris);
			info.AddValue("MajorVersion", this.MajorVerison);
			info.AddValue("MinorVersion", this.MinorVerison);
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0003B287 File Offset: 0x00039487
		// (set) Token: 0x060013A7 RID: 5031 RVA: 0x0003B28F File Offset: 0x0003948F
		public int[] Scores { get; set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0003B298 File Offset: 0x00039498
		// (set) Token: 0x060013A9 RID: 5033 RVA: 0x0003B2A0 File Offset: 0x000394A0
		public long[] RuleIDs { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0003B2A9 File Offset: 0x000394A9
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x0003B2B1 File Offset: 0x000394B1
		public string[] Uris { get; set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0003B2BA File Offset: 0x000394BA
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x0003B2C2 File Offset: 0x000394C2
		public bool[] IsActiveRules { get; set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0003B2CB File Offset: 0x000394CB
		// (set) Token: 0x060013AF RID: 5039 RVA: 0x0003B2D3 File Offset: 0x000394D3
		public int MajorVerison { get; set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0003B2DC File Offset: 0x000394DC
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x0003B2E4 File Offset: 0x000394E4
		public int MinorVerison { get; set; }

		// Token: 0x020001D5 RID: 469
		internal static class UriRulePackageDataConstants
		{
			// Token: 0x04000975 RID: 2421
			internal const string RuleIDSerializationName = "RuleID";

			// Token: 0x04000976 RID: 2422
			internal const string RuleScoreSerializationName = "RuleScore";

			// Token: 0x04000977 RID: 2423
			internal const string RuleUriSerializationName = "RuleUri";

			// Token: 0x04000978 RID: 2424
			internal const string RuleIsActiveSerializationName = "IsActive";

			// Token: 0x04000979 RID: 2425
			internal const string MajorVersionSerializationName = "MajorVersion";

			// Token: 0x0400097A RID: 2426
			internal const string MinorVersionSerializationName = "MinorVersion";
		}
	}
}
