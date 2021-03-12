using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D9 RID: 473
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchTransientException : MultiMailboxSearchException
	{
		// Token: 0x06000C61 RID: 3169 RVA: 0x00035728 File Offset: 0x00033928
		public SearchTransientException(SearchType searchType, Exception innerException) : base(Strings.SearchTransientError((searchType == SearchType.Preview) ? "Preview" : "Statistics", innerException.Message), innerException)
		{
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0003574C File Offset: 0x0003394C
		protected SearchTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
