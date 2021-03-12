using System;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001DC RID: 476
	internal class KesSpamSchema
	{
		// Token: 0x040009EC RID: 2540
		public static readonly HygienePropertyDefinition ActiveOnlyProperty = new HygienePropertyDefinition("f_ActiveOnly", typeof(bool?));

		// Token: 0x040009ED RID: 2541
		public static readonly HygienePropertyDefinition CommentProperty = new HygienePropertyDefinition("Comment", typeof(string));

		// Token: 0x040009EE RID: 2542
		public static readonly HygienePropertyDefinition ApprovalStatusIDProperty = new HygienePropertyDefinition("ApprovalStatusId", typeof(int?));

		// Token: 0x040009EF RID: 2543
		public static readonly HygienePropertyDefinition ApprovedByProperty = new HygienePropertyDefinition("ApprovedBy", typeof(string));

		// Token: 0x040009F0 RID: 2544
		public static readonly HygienePropertyDefinition DeletedByProperty = new HygienePropertyDefinition("DeletedBy", typeof(string));

		// Token: 0x040009F1 RID: 2545
		public static readonly HygienePropertyDefinition ModifiedByProperty = new HygienePropertyDefinition("ModifiedBy", typeof(string));

		// Token: 0x040009F2 RID: 2546
		public static readonly HygienePropertyDefinition DeactivatedByProperty = new HygienePropertyDefinition("DeactivatedBy", typeof(string));

		// Token: 0x040009F3 RID: 2547
		public static readonly HygienePropertyDefinition ApprovedDatetimeProperty = new HygienePropertyDefinition("ApprovedDatetime", typeof(DateTime?));

		// Token: 0x040009F4 RID: 2548
		public static readonly HygienePropertyDefinition IsUnifiedQueryProperty = new HygienePropertyDefinition("IsUnified", typeof(bool));
	}
}
