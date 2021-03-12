using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000202 RID: 514
	internal enum PropertyErrorCode
	{
		// Token: 0x04000EA3 RID: 3747
		UnknownError,
		// Token: 0x04000EA4 RID: 3748
		NotFound,
		// Token: 0x04000EA5 RID: 3749
		NotEnoughMemory,
		// Token: 0x04000EA6 RID: 3750
		NullValue,
		// Token: 0x04000EA7 RID: 3751
		IncorrectValueType,
		// Token: 0x04000EA8 RID: 3752
		SetCalculatedPropertyError,
		// Token: 0x04000EA9 RID: 3753
		SetStoreComputedPropertyError,
		// Token: 0x04000EAA RID: 3754
		GetCalculatedPropertyError,
		// Token: 0x04000EAB RID: 3755
		NotSupported,
		// Token: 0x04000EAC RID: 3756
		CorruptedData,
		// Token: 0x04000EAD RID: 3757
		RequireStreamed,
		// Token: 0x04000EAE RID: 3758
		ConstraintViolation,
		// Token: 0x04000EAF RID: 3759
		MapiCallFailed,
		// Token: 0x04000EB0 RID: 3760
		FolderNameConflict,
		// Token: 0x04000EB1 RID: 3761
		TransientMapiCallFailed,
		// Token: 0x04000EB2 RID: 3762
		FolderHasChanged,
		// Token: 0x04000EB3 RID: 3763
		PropertyValueTruncated,
		// Token: 0x04000EB4 RID: 3764
		AccessDenied,
		// Token: 0x04000EB5 RID: 3765
		PropertyNotPromoted
	}
}
