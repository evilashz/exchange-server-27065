using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000991 RID: 2449
	[Flags]
	internal enum ServiceType : uint
	{
		// Token: 0x04002D2C RID: 11564
		Activation = 1U,
		// Token: 0x04002D2D RID: 11565
		Certification = 2U,
		// Token: 0x04002D2E RID: 11566
		Publishing = 4U,
		// Token: 0x04002D2F RID: 11567
		ClientLicensor = 8U
	}
}
