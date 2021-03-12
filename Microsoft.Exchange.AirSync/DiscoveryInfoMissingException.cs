using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	internal class DiscoveryInfoMissingException : AirSyncPermanentException
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00026A6D File Offset: 0x00024C6D
		internal DiscoveryInfoMissingException(HttpStatusCode httpStatusCode, StatusCode airSyncStatusCode, LocalizedString message) : base(httpStatusCode, airSyncStatusCode, message, true)
		{
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00026A79 File Offset: 0x00024C79
		protected DiscoveryInfoMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			base.LogExceptionToEventLog = true;
		}
	}
}
