using System;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x0200018C RID: 396
	internal class QuarantinedMessageRecipientSchema
	{
		// Token: 0x0400078C RID: 1932
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = QuarantinedMessageCommonSchema.OrganizationalUnitRootProperty;

		// Token: 0x0400078D RID: 1933
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = QuarantinedMessageCommonSchema.ExMessageIdProperty;

		// Token: 0x0400078E RID: 1934
		internal static readonly HygienePropertyDefinition EmailPrefixProperty = new HygienePropertyDefinition("EmailPrefix", typeof(string));

		// Token: 0x0400078F RID: 1935
		internal static readonly HygienePropertyDefinition EmailDomainProperty = new HygienePropertyDefinition("EmailDomain", typeof(string));

		// Token: 0x04000790 RID: 1936
		internal static readonly HygienePropertyDefinition QuarantinedProperty = QuarantinedMessageCommonSchema.QuarantinedProperty;

		// Token: 0x04000791 RID: 1937
		internal static readonly HygienePropertyDefinition NotifiedProperty = QuarantinedMessageCommonSchema.NotifiedProperty;

		// Token: 0x04000792 RID: 1938
		internal static readonly HygienePropertyDefinition ReportedProperty = QuarantinedMessageCommonSchema.ReportedProperty;

		// Token: 0x04000793 RID: 1939
		internal static readonly HygienePropertyDefinition ReleasedProperty = QuarantinedMessageCommonSchema.ReleasedProperty;

		// Token: 0x04000794 RID: 1940
		public static readonly HygienePropertyDefinition FssCopyIdProp = DalHelper.FssCopyIdProp;
	}
}
