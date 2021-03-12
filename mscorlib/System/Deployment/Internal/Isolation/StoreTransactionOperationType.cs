using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x0200067E RID: 1662
	internal enum StoreTransactionOperationType
	{
		// Token: 0x0400219F RID: 8607
		Invalid,
		// Token: 0x040021A0 RID: 8608
		SetCanonicalizationContext = 14,
		// Token: 0x040021A1 RID: 8609
		StageComponent = 20,
		// Token: 0x040021A2 RID: 8610
		PinDeployment,
		// Token: 0x040021A3 RID: 8611
		UnpinDeployment,
		// Token: 0x040021A4 RID: 8612
		StageComponentFile,
		// Token: 0x040021A5 RID: 8613
		InstallDeployment,
		// Token: 0x040021A6 RID: 8614
		UninstallDeployment,
		// Token: 0x040021A7 RID: 8615
		SetDeploymentMetadata,
		// Token: 0x040021A8 RID: 8616
		Scavenge
	}
}
