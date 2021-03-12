using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000024 RID: 36
	[Flags]
	internal enum ConverterFlags
	{
		// Token: 0x040000E5 RID: 229
		None = 0,
		// Token: 0x040000E6 RID: 230
		IsEmbeddedMessage = 1,
		// Token: 0x040000E7 RID: 231
		IsStreamToStreamConversion = 2,
		// Token: 0x040000E8 RID: 232
		GenerateSkeleton = 4
	}
}
