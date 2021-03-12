using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.FullTextIndex
{
	// Token: 0x0200000C RID: 12
	public class FullTextIndexException : StoreException
	{
		// Token: 0x06000092 RID: 146 RVA: 0x00004DEC File Offset: 0x00002FEC
		public FullTextIndexException(LID lid, ErrorCodeValue errorCode, string message) : base(lid, errorCode, message)
		{
		}
	}
}
