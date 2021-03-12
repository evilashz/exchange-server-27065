using System;
using System.Data;
using System.Data.SqlTypes;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000186 RID: 390
	internal class QuarantinedMessageCommonSchema
	{
		// Token: 0x0400073F RID: 1855
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonMessageTraceSchema.OrganizationalUnitRootProperty;

		// Token: 0x04000740 RID: 1856
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = CommonMessageTraceSchema.ExMessageIdProperty;

		// Token: 0x04000741 RID: 1857
		internal static readonly HygienePropertyDefinition EventIdProperty = CommonMessageTraceSchema.EventIdProperty;

		// Token: 0x04000742 RID: 1858
		internal static readonly HygienePropertyDefinition ClientMessageIdProperty = new HygienePropertyDefinition("ClientMessageId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000743 RID: 1859
		internal static readonly HygienePropertyDefinition ReceivedProperty = new HygienePropertyDefinition("Received", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000744 RID: 1860
		internal static readonly HygienePropertyDefinition SenderAddressProperty = new HygienePropertyDefinition("SenderAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000745 RID: 1861
		internal static readonly HygienePropertyDefinition RecipientAddressProperty = new HygienePropertyDefinition("RecipientAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000746 RID: 1862
		internal static readonly HygienePropertyDefinition MessageSubjectProperty = new HygienePropertyDefinition("MessageSubject", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000747 RID: 1863
		internal static readonly HygienePropertyDefinition MessageSizeProperty = new HygienePropertyDefinition("MessageSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000748 RID: 1864
		internal static readonly HygienePropertyDefinition MailDirectionProperty = new HygienePropertyDefinition("MailDirection", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000749 RID: 1865
		internal static readonly HygienePropertyDefinition QuarantineTypeProperty = new HygienePropertyDefinition("QuarantineType", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400074A RID: 1866
		internal static readonly HygienePropertyDefinition PartNameProperty = new HygienePropertyDefinition("PartName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400074B RID: 1867
		internal static readonly HygienePropertyDefinition MimeNameProperty = new HygienePropertyDefinition("MimeName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400074C RID: 1868
		internal static readonly HygienePropertyDefinition ExpiresProperty = new HygienePropertyDefinition("Expires", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400074D RID: 1869
		internal static readonly HygienePropertyDefinition NotifiedProperty = new HygienePropertyDefinition("Notified", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400074E RID: 1870
		internal static readonly HygienePropertyDefinition QuarantinedProperty = new HygienePropertyDefinition("Quarantined", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400074F RID: 1871
		internal static readonly HygienePropertyDefinition ReleasedProperty = new HygienePropertyDefinition("Released", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000750 RID: 1872
		internal static readonly HygienePropertyDefinition ReportedProperty = new HygienePropertyDefinition("Reported", typeof(bool), false, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000751 RID: 1873
		internal static readonly HygienePropertyDefinition StartDateQueryProperty = new HygienePropertyDefinition("StartDate", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000752 RID: 1874
		internal static readonly HygienePropertyDefinition EndDateQueryProperty = new HygienePropertyDefinition("EndDate", typeof(DateTime), SqlDateTime.MaxValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000753 RID: 1875
		internal static readonly HygienePropertyDefinition DomainListQueryProperty = new HygienePropertyDefinition("tvp_DomainList", typeof(DataTable));

		// Token: 0x04000754 RID: 1876
		internal static readonly HygienePropertyDefinition MailDirectionListQueryProperty = new HygienePropertyDefinition("tvp_MailDirectionList", typeof(DataTable));

		// Token: 0x04000755 RID: 1877
		internal static readonly HygienePropertyDefinition ClientMessageListQueryProperty = new HygienePropertyDefinition("tvp_ClientMessageIdList", typeof(DataTable));

		// Token: 0x04000756 RID: 1878
		internal static readonly HygienePropertyDefinition SenderAddressListQueryProperty = new HygienePropertyDefinition("tvp_SenderAddressList", typeof(DataTable));

		// Token: 0x04000757 RID: 1879
		internal static readonly HygienePropertyDefinition RecipientAddressListQueryProperty = new HygienePropertyDefinition("tvp_RecipientAddressList", typeof(DataTable));

		// Token: 0x04000758 RID: 1880
		internal static readonly HygienePropertyDefinition TransportRuleListQueryProperty = new HygienePropertyDefinition("tvp_TransportRuleList", typeof(DataTable));

		// Token: 0x04000759 RID: 1881
		internal static readonly HygienePropertyDefinition QuarantinedUserAddressListQueryProperty = new HygienePropertyDefinition("tvp_QuarantinedUserAddressList", typeof(DataTable));

		// Token: 0x0400075A RID: 1882
		internal static readonly HygienePropertyDefinition StartExpireDateQueryProperty = new HygienePropertyDefinition("StartExpireDate", typeof(DateTime), SqlDateTime.MinValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400075B RID: 1883
		internal static readonly HygienePropertyDefinition EndExpireDateQueryProperty = new HygienePropertyDefinition("EndExpireDate", typeof(DateTime), SqlDateTime.MaxValue.Value, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400075C RID: 1884
		internal static readonly HygienePropertyDefinition PageNumberQueryProperty = new HygienePropertyDefinition("PageNumber", typeof(int), 1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400075D RID: 1885
		internal static readonly HygienePropertyDefinition PageSizeQueryProperty = new HygienePropertyDefinition("PageSize", typeof(int), 100, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
