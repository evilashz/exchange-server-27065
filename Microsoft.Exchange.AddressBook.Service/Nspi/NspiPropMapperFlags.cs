using System;

namespace Microsoft.Exchange.AddressBook.Nspi
{
	// Token: 0x02000035 RID: 53
	[Flags]
	internal enum NspiPropMapperFlags
	{
		// Token: 0x04000140 RID: 320
		None = 0,
		// Token: 0x04000141 RID: 321
		UseEphemeralId = 1,
		// Token: 0x04000142 RID: 322
		SkipMissingProperties = 2,
		// Token: 0x04000143 RID: 323
		SkipObjects = 4,
		// Token: 0x04000144 RID: 324
		IncludeDisplayName = 8,
		// Token: 0x04000145 RID: 325
		IncludeHiddenFromAddressListsEnabled = 16,
		// Token: 0x04000146 RID: 326
		GetTemplateProps = 32
	}
}
