using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000CFB RID: 3323
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ParserErrorInfo
	{
		// Token: 0x0600725D RID: 29277 RVA: 0x001F9CDC File Offset: 0x001F7EDC
		private static LocalizedString FormatErrorCode(ParserErrorCode errorCode)
		{
			switch (errorCode)
			{
			case ParserErrorCode.InvalidKindFormat:
				return ServerStrings.InvalidKindFormat;
			case ParserErrorCode.InvalidDateTimeFormat:
				return ServerStrings.InvalidDateTimeFormat;
			case ParserErrorCode.InvalidDateTimeRange:
				return ServerStrings.InvalidDateTimeRange;
			case ParserErrorCode.InvalidPropertyKey:
				return ServerStrings.InvalidPropertyKey;
			case ParserErrorCode.MissingPropertyValue:
				return ServerStrings.MissingPropertyValue;
			case ParserErrorCode.MissingOperand:
				return ServerStrings.MissingOperand;
			case ParserErrorCode.InvalidOperator:
				return ServerStrings.InvalidOperator;
			case ParserErrorCode.UnbalancedQuote:
				return ServerStrings.UnbalancedQuote;
			case ParserErrorCode.UnbalancedParenthesis:
				return ServerStrings.UnbalancedParenthesis;
			case ParserErrorCode.SuffixMatchNotSupported:
				return ServerStrings.SuffixMatchNotSupported;
			case ParserErrorCode.UnexpectedToken:
				return ServerStrings.UnexpectedToken;
			case ParserErrorCode.InvalidModifier:
				return ServerStrings.InvalidModifier;
			case ParserErrorCode.StructuredQueryException:
				return ServerStrings.StructuredQueryException;
			case ParserErrorCode.KqlParseException:
				return ServerStrings.KqlParseException;
			case ParserErrorCode.ParserError:
				return ServerStrings.InternalParserError;
			default:
				return default(LocalizedString);
			}
		}

		// Token: 0x17001E80 RID: 7808
		// (get) Token: 0x0600725E RID: 29278 RVA: 0x001F9D94 File Offset: 0x001F7F94
		// (set) Token: 0x0600725F RID: 29279 RVA: 0x001F9D9C File Offset: 0x001F7F9C
		internal ParserErrorCode ErrorCode { get; set; }

		// Token: 0x17001E81 RID: 7809
		// (get) Token: 0x06007260 RID: 29280 RVA: 0x001F9DA5 File Offset: 0x001F7FA5
		// (set) Token: 0x06007261 RID: 29281 RVA: 0x001F9DAD File Offset: 0x001F7FAD
		internal LocalizedString Message { get; set; }

		// Token: 0x17001E82 RID: 7810
		// (get) Token: 0x06007262 RID: 29282 RVA: 0x001F9DB6 File Offset: 0x001F7FB6
		// (set) Token: 0x06007263 RID: 29283 RVA: 0x001F9DBE File Offset: 0x001F7FBE
		internal TokenInfo ErrorToken { get; set; }

		// Token: 0x06007264 RID: 29284 RVA: 0x001F9DC7 File Offset: 0x001F7FC7
		internal ParserErrorInfo(ParserErrorCode errorCode) : this(errorCode, null)
		{
		}

		// Token: 0x06007265 RID: 29285 RVA: 0x001F9DD1 File Offset: 0x001F7FD1
		internal ParserErrorInfo(ParserErrorCode errorCode, TokenInfo errorToken) : this(errorCode, ParserErrorInfo.FormatErrorCode(errorCode), errorToken)
		{
		}

		// Token: 0x06007266 RID: 29286 RVA: 0x001F9DE1 File Offset: 0x001F7FE1
		internal ParserErrorInfo(ParserErrorCode errorCode, LocalizedString message, TokenInfo errorToken)
		{
			this.ErrorCode = errorCode;
			this.Message = message;
			this.ErrorToken = errorToken;
		}

		// Token: 0x06007267 RID: 29287 RVA: 0x001F9E00 File Offset: 0x001F8000
		public override string ToString()
		{
			if (this.ErrorToken == null || !this.ErrorToken.IsValid)
			{
				return this.Message;
			}
			if (this.ErrorToken.Length <= 0)
			{
				return this.Message + string.Format(" Error position: {0}", this.ErrorToken.FirstChar);
			}
			return this.Message + string.Format(" Error position: {0}, length: {1}", this.ErrorToken.FirstChar, this.ErrorToken.Length);
		}
	}
}
