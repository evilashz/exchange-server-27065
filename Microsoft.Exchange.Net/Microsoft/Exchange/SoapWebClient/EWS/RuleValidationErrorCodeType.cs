using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000291 RID: 657
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum RuleValidationErrorCodeType
	{
		// Token: 0x04001109 RID: 4361
		ADOperationFailure,
		// Token: 0x0400110A RID: 4362
		ConnectedAccountNotFound,
		// Token: 0x0400110B RID: 4363
		CreateWithRuleId,
		// Token: 0x0400110C RID: 4364
		EmptyValueFound,
		// Token: 0x0400110D RID: 4365
		DuplicatedPriority,
		// Token: 0x0400110E RID: 4366
		DuplicatedOperationOnTheSameRule,
		// Token: 0x0400110F RID: 4367
		FolderDoesNotExist,
		// Token: 0x04001110 RID: 4368
		InvalidAddress,
		// Token: 0x04001111 RID: 4369
		InvalidDateRange,
		// Token: 0x04001112 RID: 4370
		InvalidFolderId,
		// Token: 0x04001113 RID: 4371
		InvalidSizeRange,
		// Token: 0x04001114 RID: 4372
		InvalidValue,
		// Token: 0x04001115 RID: 4373
		MessageClassificationNotFound,
		// Token: 0x04001116 RID: 4374
		MissingAction,
		// Token: 0x04001117 RID: 4375
		MissingParameter,
		// Token: 0x04001118 RID: 4376
		MissingRangeValue,
		// Token: 0x04001119 RID: 4377
		NotSettable,
		// Token: 0x0400111A RID: 4378
		RecipientDoesNotExist,
		// Token: 0x0400111B RID: 4379
		RuleNotFound,
		// Token: 0x0400111C RID: 4380
		SizeLessThanZero,
		// Token: 0x0400111D RID: 4381
		StringValueTooBig,
		// Token: 0x0400111E RID: 4382
		UnsupportedAddress,
		// Token: 0x0400111F RID: 4383
		UnexpectedError,
		// Token: 0x04001120 RID: 4384
		UnsupportedRule
	}
}
