using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000614 RID: 1556
	[Serializable]
	public enum PropertyErrorCodeType
	{
		// Token: 0x04001C00 RID: 7168
		UnknownError,
		// Token: 0x04001C01 RID: 7169
		NotFound,
		// Token: 0x04001C02 RID: 7170
		NotEnoughMemory,
		// Token: 0x04001C03 RID: 7171
		NullValue,
		// Token: 0x04001C04 RID: 7172
		IncorrectValueType,
		// Token: 0x04001C05 RID: 7173
		SetCalculatedPropertyError,
		// Token: 0x04001C06 RID: 7174
		SetStoreComputedPropertyError,
		// Token: 0x04001C07 RID: 7175
		GetCalculatedPropertyError,
		// Token: 0x04001C08 RID: 7176
		NotSupported,
		// Token: 0x04001C09 RID: 7177
		CorruptedData,
		// Token: 0x04001C0A RID: 7178
		RequireStreamed,
		// Token: 0x04001C0B RID: 7179
		ConstraintViolation,
		// Token: 0x04001C0C RID: 7180
		MapiCallFailed,
		// Token: 0x04001C0D RID: 7181
		FolderNameConflict,
		// Token: 0x04001C0E RID: 7182
		TransientMapiCallFailed,
		// Token: 0x04001C0F RID: 7183
		FolderHasChanged,
		// Token: 0x04001C10 RID: 7184
		PropertyValueTruncated,
		// Token: 0x04001C11 RID: 7185
		AccessDenied,
		// Token: 0x04001C12 RID: 7186
		PropertyNotPromoted
	}
}
