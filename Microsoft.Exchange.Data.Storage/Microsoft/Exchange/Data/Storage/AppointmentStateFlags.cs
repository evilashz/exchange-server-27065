using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000209 RID: 521
	[Flags]
	internal enum AppointmentStateFlags
	{
		// Token: 0x04000EE1 RID: 3809
		None = 0,
		// Token: 0x04000EE2 RID: 3810
		Meeting = 1,
		// Token: 0x04000EE3 RID: 3811
		Received = 2,
		// Token: 0x04000EE4 RID: 3812
		Cancelled = 4,
		// Token: 0x04000EE5 RID: 3813
		Forward = 8
	}
}
