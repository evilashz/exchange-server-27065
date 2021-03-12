using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001C1 RID: 449
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class MultiMailboxSearchException : LocalizedException
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x000352F2 File Offset: 0x000334F2
		public MultiMailboxSearchException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000352FB File Offset: 0x000334FB
		public MultiMailboxSearchException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00035305 File Offset: 0x00033505
		protected MultiMailboxSearchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
