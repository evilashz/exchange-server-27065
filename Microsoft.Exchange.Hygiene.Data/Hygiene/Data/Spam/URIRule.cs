using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x0200020F RID: 527
	internal class URIRule : RuleBase
	{
		// Token: 0x060015F5 RID: 5621 RVA: 0x00044868 File Offset: 0x00042A68
		public URIRule()
		{
			base.RuleType = new byte?(1);
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0004487C File Offset: 0x00042A7C
		// (set) Token: 0x060015F7 RID: 5623 RVA: 0x0004488E File Offset: 0x00042A8E
		public int Score
		{
			get
			{
				return (int)this[URIRule.ScoreProperty];
			}
			set
			{
				this[URIRule.ScoreProperty] = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x000448A1 File Offset: 0x00042AA1
		// (set) Token: 0x060015F9 RID: 5625 RVA: 0x000448B3 File Offset: 0x00042AB3
		public string URI
		{
			get
			{
				return (string)this[URIRule.URIProperty];
			}
			set
			{
				this[URIRule.URIProperty] = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x000448C1 File Offset: 0x00042AC1
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x000448D3 File Offset: 0x00042AD3
		public int? URITypeID
		{
			get
			{
				return (int?)this[URIRule.URITypeIDProperty];
			}
			set
			{
				this[URIRule.URITypeIDProperty] = value;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x000448E6 File Offset: 0x00042AE6
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x000448F8 File Offset: 0x00042AF8
		public bool Overridable
		{
			get
			{
				return (bool)this[URIRule.OverridableProperty];
			}
			set
			{
				this[URIRule.OverridableProperty] = value;
			}
		}

		// Token: 0x04000B08 RID: 2824
		public static readonly HygienePropertyDefinition ScoreProperty = new HygienePropertyDefinition("Score", typeof(int), int.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B09 RID: 2825
		public static readonly HygienePropertyDefinition URIProperty = new HygienePropertyDefinition("Uri", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B0A RID: 2826
		public static readonly HygienePropertyDefinition URITypeIDProperty = new HygienePropertyDefinition("UriTypeId", typeof(int?));

		// Token: 0x04000B0B RID: 2827
		public static readonly HygienePropertyDefinition OverridableProperty = new HygienePropertyDefinition("Overridable", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
