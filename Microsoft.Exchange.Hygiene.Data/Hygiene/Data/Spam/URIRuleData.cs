using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000210 RID: 528
	[Serializable]
	public class URIRuleData : RuleDataBase
	{
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x00044998 File Offset: 0x00042B98
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x000449A0 File Offset: 0x00042BA0
		public string Uri { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x000449A9 File Offset: 0x00042BA9
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x000449B1 File Offset: 0x00042BB1
		public int UriType { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x000449BA File Offset: 0x00042BBA
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x000449C2 File Offset: 0x00042BC2
		public int Score { get; set; }

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x000449CB File Offset: 0x00042BCB
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x000449D3 File Offset: 0x00042BD3
		public bool Overridable { get; set; }

		// Token: 0x06001607 RID: 5639 RVA: 0x000449DC File Offset: 0x00042BDC
		public URIRuleData()
		{
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x000449E4 File Offset: 0x00042BE4
		public URIRuleData(SerializationInfo info, StreamingContext context)
		{
			this.Uri = (string)info.GetValue("Uri", typeof(string));
			this.UriType = (int)info.GetValue("UriType", typeof(int));
			this.Score = (int)info.GetValue("Score", typeof(int));
			this.Overridable = (bool)info.GetValue("Overridable", typeof(bool));
			base.RuleID = (long)info.GetValue("RuleID", typeof(long));
			base.GroupID = (long)info.GetValue("GroupID", typeof(long));
			base.IsActive = (bool)info.GetValue("IsActive", typeof(bool));
			base.IsPersistent = (bool)info.GetValue("IsPersistent", typeof(bool));
			base.Comment = (string)info.GetValue("Comment", typeof(string));
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00044B18 File Offset: 0x00042D18
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Uri", this.Uri);
			info.AddValue("UriType", this.UriType);
			info.AddValue("Score", this.Score);
			info.AddValue("Overridable", this.Overridable);
			info.AddValue("RuleID", base.RuleID);
			info.AddValue("GroupID", base.GroupID);
			info.AddValue("IsActive", base.IsActive);
			info.AddValue("IsPersistent", base.IsPersistent);
			info.AddValue("Comment", base.Comment);
		}

		// Token: 0x02000211 RID: 529
		internal static class URIRulesDataConstants
		{
			// Token: 0x04000B10 RID: 2832
			internal const string UriSerializationName = "Uri";

			// Token: 0x04000B11 RID: 2833
			internal const string UriTypeSerializationName = "UriType";

			// Token: 0x04000B12 RID: 2834
			internal const string ScoreSerializationName = "Score";

			// Token: 0x04000B13 RID: 2835
			internal const string OverridableSerializationName = "Overridable";

			// Token: 0x04000B14 RID: 2836
			internal const string RuleIDSerializationName = "RuleID";

			// Token: 0x04000B15 RID: 2837
			internal const string GroupIDSerializationName = "GroupID";

			// Token: 0x04000B16 RID: 2838
			internal const string IsActiveSerializationName = "IsActive";

			// Token: 0x04000B17 RID: 2839
			internal const string IsPersistentSerializationName = "IsPersistent";

			// Token: 0x04000B18 RID: 2840
			internal const string CommentSerializationName = "Comment";
		}
	}
}
