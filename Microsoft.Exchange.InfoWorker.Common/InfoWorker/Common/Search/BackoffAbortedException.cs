using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x0200022B RID: 555
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class BackoffAbortedException : LocalizedException
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x00043F6D File Offset: 0x0004216D
		public BackoffAbortedException() : base(Strings.SearchAborted)
		{
		}
	}
}
