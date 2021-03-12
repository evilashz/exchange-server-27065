using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x02000054 RID: 84
	internal enum RequestType
	{
		// Token: 0x04000138 RID: 312
		Local,
		// Token: 0x04000139 RID: 313
		IntraSite,
		// Token: 0x0400013A RID: 314
		CrossSite,
		// Token: 0x0400013B RID: 315
		CrossForest,
		// Token: 0x0400013C RID: 316
		FederatedCrossForest,
		// Token: 0x0400013D RID: 317
		PublicFolder
	}
}
