using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200000A RID: 10
	public class BufferTooSmall : StoreException
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00004427 File Offset: 0x00002627
		public BufferTooSmall(LID lid, string message) : base(lid, ErrorCodeValue.BufferTooSmall, message)
		{
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00004436 File Offset: 0x00002636
		public BufferTooSmall(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.BufferTooSmall, message, innerException)
		{
		}
	}
}
