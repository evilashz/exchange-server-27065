using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.StructuredQuery;

namespace Microsoft.Exchange.Data.Search.AqsParser
{
	// Token: 0x02000CFA RID: 3322
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TokenInfo
	{
		// Token: 0x17001E7D RID: 7805
		// (get) Token: 0x06007255 RID: 29269 RVA: 0x001F9C73 File Offset: 0x001F7E73
		// (set) Token: 0x06007256 RID: 29270 RVA: 0x001F9C7B File Offset: 0x001F7E7B
		internal int FirstChar { get; set; }

		// Token: 0x17001E7E RID: 7806
		// (get) Token: 0x06007257 RID: 29271 RVA: 0x001F9C84 File Offset: 0x001F7E84
		// (set) Token: 0x06007258 RID: 29272 RVA: 0x001F9C8C File Offset: 0x001F7E8C
		internal int Length { get; set; }

		// Token: 0x17001E7F RID: 7807
		// (get) Token: 0x06007259 RID: 29273 RVA: 0x001F9C95 File Offset: 0x001F7E95
		internal bool IsValid
		{
			get
			{
				return this.FirstChar != -1;
			}
		}

		// Token: 0x0600725A RID: 29274 RVA: 0x001F9CA3 File Offset: 0x001F7EA3
		internal TokenInfo() : this(-1, 0)
		{
		}

		// Token: 0x0600725B RID: 29275 RVA: 0x001F9CAD File Offset: 0x001F7EAD
		internal TokenInfo(int firstChar, int length)
		{
			this.FirstChar = firstChar;
			this.Length = length;
		}

		// Token: 0x0600725C RID: 29276 RVA: 0x001F9CC3 File Offset: 0x001F7EC3
		internal TokenInfo(TokenInfo tokenInfo) : this(tokenInfo.FirstChar, tokenInfo.Length)
		{
		}
	}
}
