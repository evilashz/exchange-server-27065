using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.DirectoryServices
{
	// Token: 0x02000007 RID: 7
	public class DirectoryTransientErrorException : StoreException
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002293 File Offset: 0x00000493
		public DirectoryTransientErrorException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.AdUnavailable, message, innerException)
		{
		}
	}
}
