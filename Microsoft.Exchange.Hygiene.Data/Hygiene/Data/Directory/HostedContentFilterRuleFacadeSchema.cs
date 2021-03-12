using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000E4 RID: 228
	internal class HostedContentFilterRuleFacadeSchema : ADObjectSchema
	{
		// Token: 0x040004A4 RID: 1188
		public static readonly HygienePropertyDefinition Enabled = HygieneFilterRuleFacadeSchema.Enabled;

		// Token: 0x040004A5 RID: 1189
		public static readonly HygienePropertyDefinition Priority = HygieneFilterRuleFacadeSchema.Priority;

		// Token: 0x040004A6 RID: 1190
		public static readonly HygienePropertyDefinition Comments = HygieneFilterRuleFacadeSchema.Comments;

		// Token: 0x040004A7 RID: 1191
		public static readonly HygienePropertyDefinition SentToMemberOf = HygieneFilterRuleFacadeSchema.SentToMemberOf;

		// Token: 0x040004A8 RID: 1192
		public static readonly HygienePropertyDefinition SentTo = HygieneFilterRuleFacadeSchema.SentTo;

		// Token: 0x040004A9 RID: 1193
		public static readonly HygienePropertyDefinition RecipientDomainIs = HygieneFilterRuleFacadeSchema.RecipientDomainIs;

		// Token: 0x040004AA RID: 1194
		public static readonly HygienePropertyDefinition ExceptIfRecipientDomainIs = HygieneFilterRuleFacadeSchema.ExceptIfRecipientDomainIs;

		// Token: 0x040004AB RID: 1195
		public static readonly HygienePropertyDefinition ExceptIfSentTo = HygieneFilterRuleFacadeSchema.ExceptIfSentTo;

		// Token: 0x040004AC RID: 1196
		public static readonly HygienePropertyDefinition ExceptIfSentToMemberOf = HygieneFilterRuleFacadeSchema.ExceptIfSentToMemberOf;

		// Token: 0x040004AD RID: 1197
		public static readonly HygienePropertyDefinition HostedContentFilterPolicy = new HygienePropertyDefinition("HostedContentFilterPolicy", typeof(string));
	}
}
