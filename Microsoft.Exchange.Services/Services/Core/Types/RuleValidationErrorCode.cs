using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000596 RID: 1430
	[XmlType(TypeName = "RuleValidationErrorCodeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public enum RuleValidationErrorCode
	{
		// Token: 0x040019AE RID: 6574
		ADOperationFailure,
		// Token: 0x040019AF RID: 6575
		ConnectedAccountNotFound,
		// Token: 0x040019B0 RID: 6576
		CreateWithRuleId,
		// Token: 0x040019B1 RID: 6577
		EmptyValueFound,
		// Token: 0x040019B2 RID: 6578
		DuplicatedPriority,
		// Token: 0x040019B3 RID: 6579
		DuplicatedOperationOnTheSameRule,
		// Token: 0x040019B4 RID: 6580
		FolderDoesNotExist,
		// Token: 0x040019B5 RID: 6581
		InvalidAddress,
		// Token: 0x040019B6 RID: 6582
		InvalidDateRange,
		// Token: 0x040019B7 RID: 6583
		InvalidFolderId,
		// Token: 0x040019B8 RID: 6584
		InvalidSizeRange,
		// Token: 0x040019B9 RID: 6585
		InvalidValue,
		// Token: 0x040019BA RID: 6586
		MessageClassificationNotFound,
		// Token: 0x040019BB RID: 6587
		MissingAction,
		// Token: 0x040019BC RID: 6588
		MissingParameter,
		// Token: 0x040019BD RID: 6589
		MissingRangeValue,
		// Token: 0x040019BE RID: 6590
		NotSettable,
		// Token: 0x040019BF RID: 6591
		RecipientDoesNotExist,
		// Token: 0x040019C0 RID: 6592
		RuleNotFound,
		// Token: 0x040019C1 RID: 6593
		SizeLessThanZero,
		// Token: 0x040019C2 RID: 6594
		StringValueTooBig,
		// Token: 0x040019C3 RID: 6595
		UnsupportedAddress,
		// Token: 0x040019C4 RID: 6596
		UnexpectedError,
		// Token: 0x040019C5 RID: 6597
		UnsupportedRule
	}
}
