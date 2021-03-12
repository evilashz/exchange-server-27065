using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000CFC RID: 3324
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Serializable]
	internal class ParserException : LocalizedException
	{
		// Token: 0x06007268 RID: 29288 RVA: 0x001F9EA1 File Offset: 0x001F80A1
		private static bool IsNullOrEmpty(List<ParserErrorInfo> list)
		{
			return list == null || list.Count <= 0;
		}

		// Token: 0x17001E83 RID: 7811
		// (get) Token: 0x06007269 RID: 29289 RVA: 0x001F9EB4 File Offset: 0x001F80B4
		internal new ParserErrorCode ErrorCode
		{
			get
			{
				if (!ParserException.IsNullOrEmpty(this.ParserErrors))
				{
					return this.ParserErrors[0].ErrorCode;
				}
				return ParserErrorCode.NoError;
			}
		}

		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x0600726A RID: 29290 RVA: 0x001F9ED6 File Offset: 0x001F80D6
		// (set) Token: 0x0600726B RID: 29291 RVA: 0x001F9EDE File Offset: 0x001F80DE
		internal List<ParserErrorInfo> ParserErrors { get; set; }

		// Token: 0x0600726C RID: 29292 RVA: 0x001F9EE7 File Offset: 0x001F80E7
		internal ParserException(ParserErrorInfo parserError) : base(parserError.Message)
		{
			this.ParserErrors = new List<ParserErrorInfo>();
			this.ParserErrors.Add(parserError);
		}

		// Token: 0x0600726D RID: 29293 RVA: 0x001F9F0C File Offset: 0x001F810C
		internal ParserException(ParserErrorInfo parserError, Exception innerException) : base(parserError.Message, innerException)
		{
			this.ParserErrors = new List<ParserErrorInfo>();
			this.ParserErrors.Add(parserError);
		}

		// Token: 0x0600726E RID: 29294 RVA: 0x001F9F34 File Offset: 0x001F8134
		internal ParserException(List<ParserErrorInfo> parserErrors) : base(ParserException.IsNullOrEmpty(parserErrors) ? default(LocalizedString) : parserErrors[0].Message)
		{
			if (ParserException.IsNullOrEmpty(parserErrors))
			{
				throw new ArgumentException("parserErrors");
			}
			this.ParserErrors = new List<ParserErrorInfo>();
			this.ParserErrors.AddRange(parserErrors);
		}
	}
}
