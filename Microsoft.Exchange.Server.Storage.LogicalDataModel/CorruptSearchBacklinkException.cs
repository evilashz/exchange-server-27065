using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200005F RID: 95
	public class CorruptSearchBacklinkException : StoreException
	{
		// Token: 0x060007E2 RID: 2018 RVA: 0x00045A6A File Offset: 0x00043C6A
		public CorruptSearchBacklinkException(LID lid, string message) : base(lid, ErrorCodeValue.CorruptSearchBacklink, message)
		{
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00045A79 File Offset: 0x00043C79
		public CorruptSearchBacklinkException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.CorruptSearchBacklink, message, innerException)
		{
		}
	}
}
