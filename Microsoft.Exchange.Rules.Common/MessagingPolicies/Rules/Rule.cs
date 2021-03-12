using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000028 RID: 40
	public class Rule
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00003A89 File Offset: 0x00001C89
		public Rule(string name) : this(name, null)
		{
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003A94 File Offset: 0x00001C94
		public Rule(string name, Condition condition)
		{
			this.name = name;
			this.condition = condition;
			this.actions = new ShortList<Action>();
			this.Enabled = RuleState.Enabled;
			this.SubType = RuleSubType.None;
			this.isTooAdvancedToParse = false;
			this.expiryDate = null;
			this.activationDate = null;
			this.Mode = RuleMode.Enforce;
			this.ErrorAction = RuleErrorAction.Ignore;
			this.WhenChangedUTC = null;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003B15 File Offset: 0x00001D15
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00003B1D File Offset: 0x00001D1D
		public Condition Condition
		{
			get
			{
				return this.condition;
			}
			set
			{
				this.condition = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003B26 File Offset: 0x00001D26
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003B2E File Offset: 0x00001D2E
		public ShortList<Action> Actions
		{
			get
			{
				return this.actions;
			}
			set
			{
				this.actions = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003B37 File Offset: 0x00001D37
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003B3F File Offset: 0x00001D3F
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003B48 File Offset: 0x00001D48
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003B50 File Offset: 0x00001D50
		public Guid ImmutableId
		{
			get
			{
				return this.immutableId;
			}
			set
			{
				this.immutableId = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003B59 File Offset: 0x00001D59
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003B61 File Offset: 0x00001D61
		public DateTime? WhenChangedUTC
		{
			get
			{
				return this.whenChangedUTC;
			}
			set
			{
				this.whenChangedUTC = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003B6A File Offset: 0x00001D6A
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003B72 File Offset: 0x00001D72
		public string Comments
		{
			get
			{
				return this.comments;
			}
			set
			{
				this.comments = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003B7B File Offset: 0x00001D7B
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003B83 File Offset: 0x00001D83
		public RuleState Enabled { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003B8C File Offset: 0x00001D8C
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003B94 File Offset: 0x00001D94
		public RuleSubType SubType { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003B9D File Offset: 0x00001D9D
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003BA5 File Offset: 0x00001DA5
		public bool IsTooAdvancedToParse
		{
			get
			{
				return this.isTooAdvancedToParse;
			}
			set
			{
				this.isTooAdvancedToParse = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003BAE File Offset: 0x00001DAE
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003BB6 File Offset: 0x00001DB6
		public DateTime? ExpiryDate
		{
			get
			{
				return this.expiryDate;
			}
			set
			{
				this.expiryDate = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003BBF File Offset: 0x00001DBF
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003BC7 File Offset: 0x00001DC7
		public DateTime? ActivationDate
		{
			get
			{
				return this.activationDate;
			}
			set
			{
				this.activationDate = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public virtual Version MinimumVersion
		{
			get
			{
				if (this.ActivationDate != null)
				{
					return Rule.ActivationAndExpiryDateVersion;
				}
				if (this.ExpiryDate != null)
				{
					return Rule.ActivationAndExpiryDateVersion;
				}
				if (this.tags.Any<KeyValuePair<string, List<RuleTag>>>())
				{
					return Rule.RuleTagsVersion;
				}
				return Rule.BaseVersion;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00003C21 File Offset: 0x00001E21
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00003C29 File Offset: 0x00001E29
		public RuleMode Mode { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003C32 File Offset: 0x00001E32
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x00003C3A File Offset: 0x00001E3A
		public RuleErrorAction ErrorAction { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00003C43 File Offset: 0x00001E43
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00003C4B File Offset: 0x00001E4B
		public int ConditionAndActionSize { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00003C54 File Offset: 0x00001E54
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00003C5C File Offset: 0x00001E5C
		public bool SupportGetEstimatedSize { get; set; }

		// Token: 0x060000D9 RID: 217 RVA: 0x00003C68 File Offset: 0x00001E68
		public virtual int GetEstimatedSize()
		{
			if (!this.SupportGetEstimatedSize)
			{
				throw new InvalidOperationException("GetEstimatedSize currently is only supported when the rule is parsed from XML data");
			}
			int num = 0;
			num += 4;
			num += 4;
			num += 4;
			num += 4;
			if (!string.IsNullOrEmpty(this.name))
			{
				num += this.name.Length * 2;
				num += 18;
			}
			if (!string.IsNullOrEmpty(this.comments))
			{
				num += this.comments.Length * 2;
				num += 18;
			}
			num += 16;
			num += 24;
			num += 2;
			if (this.tags != null)
			{
				num += 18;
				foreach (KeyValuePair<string, List<RuleTag>> keyValuePair in this.tags)
				{
					num += keyValuePair.Key.Length * 2;
					foreach (RuleTag ruleTag in keyValuePair.Value)
					{
						num += ruleTag.Size;
					}
				}
			}
			return num + 18 + this.ConditionAndActionSize;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003DA5 File Offset: 0x00001FA5
		public IEnumerable<RuleTag> GetTags()
		{
			return this.tags.SelectMany((KeyValuePair<string, List<RuleTag>> tagItem) => tagItem.Value);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003DD0 File Offset: 0x00001FD0
		public IEnumerable<RuleTag> GetTags(string tagType)
		{
			List<RuleTag> result;
			if (this.tags.TryGetValue(tagType, out result))
			{
				return result;
			}
			return new List<RuleTag>();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003DF4 File Offset: 0x00001FF4
		public void AddTag(RuleTag tag)
		{
			List<RuleTag> list;
			if (this.tags.TryGetValue(tag.TagType, out list))
			{
				list.Add(tag);
				return;
			}
			this.tags.Add(tag.TagType, new List<RuleTag>
			{
				tag
			});
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003E3D File Offset: 0x0000203D
		public void RemoveAllTags(string tagType)
		{
			this.tags.Remove(tagType);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003E4C File Offset: 0x0000204C
		public void GetSupplementalData(SupplementalData data)
		{
			if (this.condition != null)
			{
				this.condition.GetSupplementalData(data);
			}
		}

		// Token: 0x0400003F RID: 63
		private Condition condition;

		// Token: 0x04000040 RID: 64
		private ShortList<Action> actions;

		// Token: 0x04000041 RID: 65
		private string name;

		// Token: 0x04000042 RID: 66
		private Guid immutableId;

		// Token: 0x04000043 RID: 67
		private string comments;

		// Token: 0x04000044 RID: 68
		private DateTime? activationDate;

		// Token: 0x04000045 RID: 69
		private bool isTooAdvancedToParse;

		// Token: 0x04000046 RID: 70
		private DateTime? expiryDate;

		// Token: 0x04000047 RID: 71
		private DateTime? whenChangedUTC;

		// Token: 0x04000048 RID: 72
		private readonly Dictionary<string, List<RuleTag>> tags = new Dictionary<string, List<RuleTag>>();

		// Token: 0x04000049 RID: 73
		public static readonly Version BaseVersion = new Version("14.00.0000.000");

		// Token: 0x0400004A RID: 74
		public static readonly Version RuleTagsVersion = new Version("15.00.0001.000");

		// Token: 0x0400004B RID: 75
		public static readonly Version ActivationAndExpiryDateVersion = new Version("15.00.0002.000");

		// Token: 0x0400004C RID: 76
		public static readonly Version BaseVersion15 = new Version("15.00.0000.000");

		// Token: 0x0400004D RID: 77
		public static readonly Version HighestHonoredVersion = new Version("15.00.0015.00");
	}
}
