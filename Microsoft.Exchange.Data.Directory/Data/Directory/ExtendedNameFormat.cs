using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200016E RID: 366
	internal enum ExtendedNameFormat
	{
		// Token: 0x0400090D RID: 2317
		NameUnknown,
		// Token: 0x0400090E RID: 2318
		NameFullyQualifiedDN,
		// Token: 0x0400090F RID: 2319
		NameSamCompatible,
		// Token: 0x04000910 RID: 2320
		NameDisplay,
		// Token: 0x04000911 RID: 2321
		NameUniqueId = 6,
		// Token: 0x04000912 RID: 2322
		NameCanonical,
		// Token: 0x04000913 RID: 2323
		NameUserPrincipal,
		// Token: 0x04000914 RID: 2324
		NameCanonicalEx,
		// Token: 0x04000915 RID: 2325
		NameServicePrincipal,
		// Token: 0x04000916 RID: 2326
		NameDnsDomain = 12
	}
}
