using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200005B RID: 91
	public class DatabaseCorruptionException : StoreException
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x000459EE File Offset: 0x00043BEE
		public DatabaseCorruptionException(LID lid, string message) : base(lid, ErrorCodeValue.CallFailed, message)
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000459FD File Offset: 0x00043BFD
		public DatabaseCorruptionException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.CallFailed, message, innerException)
		{
		}
	}
}
