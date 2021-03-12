using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020001B0 RID: 432
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum RuleValidationErrorCodeType
	{
		// Token: 0x04000CB7 RID: 3255
		ADOperationFailure,
		// Token: 0x04000CB8 RID: 3256
		ConnectedAccountNotFound,
		// Token: 0x04000CB9 RID: 3257
		CreateWithRuleId,
		// Token: 0x04000CBA RID: 3258
		EmptyValueFound,
		// Token: 0x04000CBB RID: 3259
		DuplicatedPriority,
		// Token: 0x04000CBC RID: 3260
		DuplicatedOperationOnTheSameRule,
		// Token: 0x04000CBD RID: 3261
		FolderDoesNotExist,
		// Token: 0x04000CBE RID: 3262
		InvalidAddress,
		// Token: 0x04000CBF RID: 3263
		InvalidDateRange,
		// Token: 0x04000CC0 RID: 3264
		InvalidFolderId,
		// Token: 0x04000CC1 RID: 3265
		InvalidSizeRange,
		// Token: 0x04000CC2 RID: 3266
		InvalidValue,
		// Token: 0x04000CC3 RID: 3267
		MessageClassificationNotFound,
		// Token: 0x04000CC4 RID: 3268
		MissingAction,
		// Token: 0x04000CC5 RID: 3269
		MissingParameter,
		// Token: 0x04000CC6 RID: 3270
		MissingRangeValue,
		// Token: 0x04000CC7 RID: 3271
		NotSettable,
		// Token: 0x04000CC8 RID: 3272
		RecipientDoesNotExist,
		// Token: 0x04000CC9 RID: 3273
		RuleNotFound,
		// Token: 0x04000CCA RID: 3274
		SizeLessThanZero,
		// Token: 0x04000CCB RID: 3275
		StringValueTooBig,
		// Token: 0x04000CCC RID: 3276
		UnsupportedAddress,
		// Token: 0x04000CCD RID: 3277
		UnexpectedError,
		// Token: 0x04000CCE RID: 3278
		UnsupportedRule
	}
}
