using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x02000200 RID: 512
	internal class SpamRule : RuleBase
	{
		// Token: 0x06001572 RID: 5490 RVA: 0x00042FEB File Offset: 0x000411EB
		public SpamRule()
		{
			base.RuleType = new byte?(0);
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x00042FFF File Offset: 0x000411FF
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x00043011 File Offset: 0x00041211
		public string ConditionMatchPhrase
		{
			get
			{
				return (string)this[SpamRule.ConditionMatchPhraseProperty];
			}
			set
			{
				this[SpamRule.ConditionMatchPhraseProperty] = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x0004301F File Offset: 0x0004121F
		// (set) Token: 0x06001576 RID: 5494 RVA: 0x00043031 File Offset: 0x00041231
		public string ConditionNotMatchPhrase
		{
			get
			{
				return (string)this[SpamRule.ConditionNotMatchPhraseProperty];
			}
			set
			{
				this[SpamRule.ConditionNotMatchPhraseProperty] = value;
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0004303F File Offset: 0x0004123F
		// (set) Token: 0x06001578 RID: 5496 RVA: 0x00043051 File Offset: 0x00041251
		public int? AsfID
		{
			get
			{
				return (int?)this[SpamRule.AsfIDProperty];
			}
			set
			{
				this[SpamRule.AsfIDProperty] = value;
			}
		}

		// Token: 0x04000ACA RID: 2762
		public static readonly HygienePropertyDefinition ConditionMatchPhraseProperty = new HygienePropertyDefinition("nvc_ConditionMatchPhrase", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000ACB RID: 2763
		public static readonly HygienePropertyDefinition ConditionNotMatchPhraseProperty = new HygienePropertyDefinition("nvc_ConditionNotMatchPhrase", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000ACC RID: 2764
		public static readonly HygienePropertyDefinition AsfIDProperty = new HygienePropertyDefinition("AsfId", typeof(int?));
	}
}
