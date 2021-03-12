using System;
using System.Net;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000247 RID: 583
	[Serializable]
	internal class SearchTimeOutException : AirSyncPermanentException
	{
		// Token: 0x06001547 RID: 5447 RVA: 0x0007C865 File Offset: 0x0007AA65
		internal SearchTimeOutException() : base(HttpStatusCode.ServiceUnavailable, StatusCode.Sync_NotificationGUID, null, false)
		{
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x0007C876 File Offset: 0x0007AA76
		protected SearchTimeOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
