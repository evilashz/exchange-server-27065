using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000229 RID: 553
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchMailboxException : LocalizedException
	{
		// Token: 0x06000F2F RID: 3887 RVA: 0x00043F42 File Offset: 0x00042142
		public SearchMailboxException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x00043F4B File Offset: 0x0004214B
		public SearchMailboxException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x00043F55 File Offset: 0x00042155
		protected SearchMailboxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
