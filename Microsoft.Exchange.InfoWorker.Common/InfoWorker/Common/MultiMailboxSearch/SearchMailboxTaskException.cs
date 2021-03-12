using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C2 RID: 450
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class SearchMailboxTaskException : MultiMailboxSearchException
	{
		// Token: 0x06000C2F RID: 3119 RVA: 0x0003530F File Offset: 0x0003350F
		public SearchMailboxTaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00035318 File Offset: 0x00033518
		public SearchMailboxTaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00035322 File Offset: 0x00033522
		protected SearchMailboxTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
