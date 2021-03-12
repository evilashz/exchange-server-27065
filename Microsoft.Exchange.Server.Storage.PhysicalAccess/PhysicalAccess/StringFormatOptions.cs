using System;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000087 RID: 135
	[Flags]
	public enum StringFormatOptions
	{
		// Token: 0x040001CC RID: 460
		None = 0,
		// Token: 0x040001CD RID: 461
		IncludeDetails = 1,
		// Token: 0x040001CE RID: 462
		IncludeNestedObjectsId = 2,
		// Token: 0x040001CF RID: 463
		SkipParametersData = 4,
		// Token: 0x040001D0 RID: 464
		MultiLine = 8
	}
}
