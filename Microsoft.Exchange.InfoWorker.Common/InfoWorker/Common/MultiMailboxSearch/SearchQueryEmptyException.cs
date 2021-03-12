using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C7 RID: 455
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchQueryEmptyException : MultiMailboxSearchException
	{
		// Token: 0x06000C3A RID: 3130 RVA: 0x000354C9 File Offset: 0x000336C9
		public SearchQueryEmptyException() : base(Strings.SearchQueryEmpty)
		{
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000354D6 File Offset: 0x000336D6
		protected SearchQueryEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
