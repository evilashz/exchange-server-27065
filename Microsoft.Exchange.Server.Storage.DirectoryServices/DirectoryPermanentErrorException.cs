using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000006 RID: 6
	public class DirectoryPermanentErrorException : StoreException
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002283 File Offset: 0x00000483
		public DirectoryPermanentErrorException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.AdUnavailable, message, innerException)
		{
		}
	}
}
