using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000204 RID: 516
	[Flags]
	internal enum PropertyFlags
	{
		// Token: 0x04000EBB RID: 3771
		None = 0,
		// Token: 0x04000EBC RID: 3772
		ReadOnly = 1,
		// Token: 0x04000EBD RID: 3773
		FilterOnly = 2,
		// Token: 0x04000EBE RID: 3774
		HasMimeEncoding = 4,
		// Token: 0x04000EBF RID: 3775
		Sortable = 8,
		// Token: 0x04000EC0 RID: 3776
		Streamable = 16,
		// Token: 0x04000EC1 RID: 3777
		SetIfNotChanged = 32,
		// Token: 0x04000EC2 RID: 3778
		TrackChange = 64,
		// Token: 0x04000EC3 RID: 3779
		UserDefined = 65535,
		// Token: 0x04000EC4 RID: 3780
		Multivalued = 65536,
		// Token: 0x04000EC5 RID: 3781
		Binary = 131072,
		// Token: 0x04000EC6 RID: 3782
		Transmittable = 262144,
		// Token: 0x04000EC7 RID: 3783
		Custom = 524288,
		// Token: 0x04000EC8 RID: 3784
		Automatic = 2147418112
	}
}
