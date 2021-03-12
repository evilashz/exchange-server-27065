using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200013F RID: 319
	internal class CommonMessageTraceSchema
	{
		// Token: 0x04000623 RID: 1571
		internal static readonly HygienePropertyDefinition PhysicalInstanceKeyProp = DalHelper.PhysicalInstanceKeyProp;

		// Token: 0x04000624 RID: 1572
		internal static readonly HygienePropertyDefinition FssCopyIdProp = DalHelper.FssCopyIdProp;

		// Token: 0x04000625 RID: 1573
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = new HygienePropertyDefinition("OrganizationalUnitRoot", typeof(Guid));

		// Token: 0x04000626 RID: 1574
		internal static readonly HygienePropertyDefinition HashBucketProperty = DalHelper.HashBucketProp;

		// Token: 0x04000627 RID: 1575
		internal static readonly HygienePropertyDefinition EventIdProperty = new HygienePropertyDefinition("EventId", typeof(Guid));

		// Token: 0x04000628 RID: 1576
		internal static readonly HygienePropertyDefinition ClassificationIdProperty = new HygienePropertyDefinition("ClassificationId", typeof(Guid));

		// Token: 0x04000629 RID: 1577
		internal static readonly HygienePropertyDefinition RecipientIdProperty = new HygienePropertyDefinition("RecipientId", typeof(Guid));

		// Token: 0x0400062A RID: 1578
		internal static readonly HygienePropertyDefinition EventRuleIdProperty = new HygienePropertyDefinition("EventRuleId", typeof(Guid));

		// Token: 0x0400062B RID: 1579
		internal static readonly HygienePropertyDefinition EventRuleClassificationIdProperty = new HygienePropertyDefinition("EventRuleClassificationIdProperty", typeof(Guid));

		// Token: 0x0400062C RID: 1580
		internal static readonly HygienePropertyDefinition SourceItemIdProperty = new HygienePropertyDefinition("SourceItemId", typeof(Guid));

		// Token: 0x0400062D RID: 1581
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = new HygienePropertyDefinition("ExMessageId", typeof(Guid));

		// Token: 0x0400062E RID: 1582
		internal static readonly HygienePropertyDefinition EventHashKeyProperty = new HygienePropertyDefinition("EventHashKey", typeof(byte[]));

		// Token: 0x0400062F RID: 1583
		internal static readonly HygienePropertyDefinition EmailHashKeyProperty = new HygienePropertyDefinition("EmailHashKey", typeof(byte[]));

		// Token: 0x04000630 RID: 1584
		internal static readonly HygienePropertyDefinition EmailDomainHashKeyProperty = new HygienePropertyDefinition("EmailDomainHashKey", typeof(byte[]));

		// Token: 0x04000631 RID: 1585
		internal static readonly HygienePropertyDefinition IPHashKeyProperty = new HygienePropertyDefinition("IPHashKey", typeof(byte[]));

		// Token: 0x04000632 RID: 1586
		internal static readonly HygienePropertyDefinition RuleIdProperty = new HygienePropertyDefinition("RuleId", typeof(Guid));

		// Token: 0x04000633 RID: 1587
		internal static readonly HygienePropertyDefinition DataClassificationIdProperty = new HygienePropertyDefinition("DataClassificationId", typeof(Guid));
	}
}
