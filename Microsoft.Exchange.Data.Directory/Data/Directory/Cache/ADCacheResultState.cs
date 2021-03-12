using System;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x020000AB RID: 171
	public enum ADCacheResultState : byte
	{
		// Token: 0x0400031E RID: 798
		Succeed,
		// Token: 0x0400031F RID: 799
		NotFound,
		// Token: 0x04000320 RID: 800
		CNFedObject,
		// Token: 0x04000321 RID: 801
		SoftDeletedObject,
		// Token: 0x04000322 RID: 802
		TenantIsBeingRelocated,
		// Token: 0x04000323 RID: 803
		OranizationIdMismatch,
		// Token: 0x04000324 RID: 804
		CacheModeIsNotRead,
		// Token: 0x04000325 RID: 805
		ExceptionHappened,
		// Token: 0x04000326 RID: 806
		PropertiesMissing,
		// Token: 0x04000327 RID: 807
		WrongForest,
		// Token: 0x04000328 RID: 808
		WritableSession = 32
	}
}
