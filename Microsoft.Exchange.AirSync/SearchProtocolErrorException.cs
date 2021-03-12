using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000246 RID: 582
	[Serializable]
	internal class SearchProtocolErrorException : AirSyncPermanentException
	{
		// Token: 0x06001545 RID: 5445 RVA: 0x0007C851 File Offset: 0x0007AA51
		internal SearchProtocolErrorException() : base(StatusCode.Sync_ProtocolVersionMismatch, false)
		{
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0007C85B File Offset: 0x0007AA5B
		protected SearchProtocolErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
