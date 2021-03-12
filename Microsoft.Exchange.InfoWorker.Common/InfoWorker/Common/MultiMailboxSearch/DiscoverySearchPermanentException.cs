using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x020001D0 RID: 464
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class DiscoverySearchPermanentException : MultiMailboxSearchException
	{
		// Token: 0x06000C4E RID: 3150 RVA: 0x00035615 File Offset: 0x00033815
		public DiscoverySearchPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0003561E File Offset: 0x0003381E
		public DiscoverySearchPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00035628 File Offset: 0x00033828
		protected DiscoverySearchPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
