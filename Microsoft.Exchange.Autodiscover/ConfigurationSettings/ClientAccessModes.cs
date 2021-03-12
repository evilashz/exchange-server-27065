using System;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000032 RID: 50
	[Flags]
	internal enum ClientAccessModes
	{
		// Token: 0x04000180 RID: 384
		None = 0,
		// Token: 0x04000181 RID: 385
		InternalAccess = 1,
		// Token: 0x04000182 RID: 386
		ExternalAccess = 2
	}
}
