using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001AE RID: 430
	internal class UnsentSpamDigestMessageSchema
	{
		// Token: 0x040008A4 RID: 2212
		internal static readonly HygienePropertyDefinition ExMessageIdProperty = CommonMessageTraceSchema.ExMessageIdProperty;

		// Token: 0x040008A5 RID: 2213
		internal static readonly HygienePropertyDefinition OrganizationalUnitRootProperty = CommonMessageTraceSchema.OrganizationalUnitRootProperty;

		// Token: 0x040008A6 RID: 2214
		internal static readonly HygienePropertyDefinition SenderNameProperty = new HygienePropertyDefinition("SenderName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008A7 RID: 2215
		internal static readonly HygienePropertyDefinition FromEmailPrefixProperty = new HygienePropertyDefinition("FromEmailPrefix", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008A8 RID: 2216
		internal static readonly HygienePropertyDefinition FromEmailDomainProperty = new HygienePropertyDefinition("FromEmailDomain", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008A9 RID: 2217
		internal static readonly HygienePropertyDefinition RecipientNameProperty = new HygienePropertyDefinition("RecipientName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008AA RID: 2218
		internal static readonly HygienePropertyDefinition ToEmailPrefixProperty = new HygienePropertyDefinition("ToEmailPrefix", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008AB RID: 2219
		internal static readonly HygienePropertyDefinition ToEmailDomainProperty = new HygienePropertyDefinition("ToEmailDomain", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008AC RID: 2220
		internal static readonly HygienePropertyDefinition SubjectProperty = new HygienePropertyDefinition("Subject", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008AD RID: 2221
		internal static readonly HygienePropertyDefinition MessageSizeProperty = new HygienePropertyDefinition("MessageSize", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008AE RID: 2222
		internal static readonly HygienePropertyDefinition DateReceivedProperty = new HygienePropertyDefinition("DateReceived", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008AF RID: 2223
		internal static readonly HygienePropertyDefinition LastNotifiedProperty = new HygienePropertyDefinition("LastNotifiedDateTime", typeof(DateTime?));

		// Token: 0x040008B0 RID: 2224
		internal static readonly HygienePropertyDefinition UpperBoundaryQueryProperty = new HygienePropertyDefinition("UpperBoundaryDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008B1 RID: 2225
		internal static readonly HygienePropertyDefinition DefaultESNFrequencyQueryProperty = new HygienePropertyDefinition("DefaultESNFrequency", typeof(int), 3, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
