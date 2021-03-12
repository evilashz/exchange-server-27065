using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D1 RID: 465
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class TooManyKeywordsException : DiscoverySearchPermanentException
	{
		// Token: 0x06000C51 RID: 3153 RVA: 0x00035632 File Offset: 0x00033832
		public TooManyKeywordsException(int keywordCount, int maxAllowedKeywordCount) : base(Strings.MaxAllowedKeywordsExceeded(keywordCount, maxAllowedKeywordCount))
		{
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00035641 File Offset: 0x00033841
		protected TooManyKeywordsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
