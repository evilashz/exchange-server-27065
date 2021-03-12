using System;
using System.Net;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200005A RID: 90
	[Serializable]
	internal class DataTooLargeException : AirSyncPermanentException
	{
		// Token: 0x06000512 RID: 1298 RVA: 0x0001E057 File Offset: 0x0001C257
		internal DataTooLargeException(StatusCode airSyncStatusCode) : base(HttpStatusCode.InternalServerError, airSyncStatusCode, null, false)
		{
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0001E067 File Offset: 0x0001C267
		protected DataTooLargeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
