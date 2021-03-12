using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000E7 RID: 231
	internal class HygieneFilterRuleFacadeSchema : ADObjectSchema
	{
		// Token: 0x040004B0 RID: 1200
		public static readonly HygienePropertyDefinition Enabled = new HygienePropertyDefinition("Enabled", typeof(bool), true, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004B1 RID: 1201
		public static readonly HygienePropertyDefinition Priority = new HygienePropertyDefinition("Priority", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040004B2 RID: 1202
		public static readonly HygienePropertyDefinition Comments = new HygienePropertyDefinition("Comments", typeof(string));

		// Token: 0x040004B3 RID: 1203
		public static readonly HygienePropertyDefinition SentToMemberOf = new HygienePropertyDefinition("SentToMemberOf", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040004B4 RID: 1204
		public static readonly HygienePropertyDefinition SentTo = new HygienePropertyDefinition("SentTo", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040004B5 RID: 1205
		public static readonly HygienePropertyDefinition RecipientDomainIs = new HygienePropertyDefinition("RecipientDomainIs", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040004B6 RID: 1206
		public static readonly HygienePropertyDefinition ExceptIfRecipientDomainIs = new HygienePropertyDefinition("ExceptIfRecipientDomainIs", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040004B7 RID: 1207
		public static readonly HygienePropertyDefinition ExceptIfSentTo = new HygienePropertyDefinition("ExceptIfSentTo", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);

		// Token: 0x040004B8 RID: 1208
		public static readonly HygienePropertyDefinition ExceptIfSentToMemberOf = new HygienePropertyDefinition("ExceptIfSentToMemberOf", typeof(string), null, ADPropertyDefinitionFlags.MultiValued);
	}
}
