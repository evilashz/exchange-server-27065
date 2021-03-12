using System;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000CF6 RID: 3318
	internal enum ParserErrorCode
	{
		// Token: 0x04004FA9 RID: 20393
		NoError,
		// Token: 0x04004FAA RID: 20394
		InvalidKindFormat,
		// Token: 0x04004FAB RID: 20395
		InvalidDateTimeFormat,
		// Token: 0x04004FAC RID: 20396
		InvalidDateTimeRange,
		// Token: 0x04004FAD RID: 20397
		InvalidPropertyKey,
		// Token: 0x04004FAE RID: 20398
		MissingPropertyValue,
		// Token: 0x04004FAF RID: 20399
		MissingOperand,
		// Token: 0x04004FB0 RID: 20400
		InvalidOperator,
		// Token: 0x04004FB1 RID: 20401
		UnbalancedQuote,
		// Token: 0x04004FB2 RID: 20402
		UnbalancedParenthesis,
		// Token: 0x04004FB3 RID: 20403
		SuffixMatchNotSupported,
		// Token: 0x04004FB4 RID: 20404
		UnexpectedToken,
		// Token: 0x04004FB5 RID: 20405
		InvalidModifier,
		// Token: 0x04004FB6 RID: 20406
		StructuredQueryException,
		// Token: 0x04004FB7 RID: 20407
		KqlParseException,
		// Token: 0x04004FB8 RID: 20408
		ParserError
	}
}
