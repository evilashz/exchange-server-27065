using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000063 RID: 99
	public class NotSupportedException : StoreException
	{
		// Token: 0x060007EA RID: 2026 RVA: 0x00045AE6 File Offset: 0x00043CE6
		public NotSupportedException(LID lid, string message) : base(lid, ErrorCodeValue.NotSupported, message)
		{
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00045AF5 File Offset: 0x00043CF5
		public NotSupportedException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.NotSupported, message, innerException)
		{
		}
	}
}
