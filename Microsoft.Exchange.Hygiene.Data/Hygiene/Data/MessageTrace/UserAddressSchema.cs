using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x020001B2 RID: 434
	internal class UserAddressSchema
	{
		// Token: 0x040008B6 RID: 2230
		internal static readonly HygienePropertyDefinition UserAddressIdProperty = new HygienePropertyDefinition("UserAddressId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008B7 RID: 2231
		internal static readonly HygienePropertyDefinition EmailDomainProperty = new HygienePropertyDefinition("EmailDomain", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008B8 RID: 2232
		internal static readonly HygienePropertyDefinition EmailPrefixProperty = new HygienePropertyDefinition("EmailPrefix", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x040008B9 RID: 2233
		internal static readonly HygienePropertyDefinition DigestFrequencyProperty = new HygienePropertyDefinition("DigestFreq", typeof(int?));

		// Token: 0x040008BA RID: 2234
		internal static readonly HygienePropertyDefinition LastNotifiedProperty = new HygienePropertyDefinition("LastNotifiedDateTime", typeof(DateTime?));

		// Token: 0x040008BB RID: 2235
		internal static readonly HygienePropertyDefinition BlockEsnProperty = new HygienePropertyDefinition("BlockEsn", typeof(bool?));
	}
}
