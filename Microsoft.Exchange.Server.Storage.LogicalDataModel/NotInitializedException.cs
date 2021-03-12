using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000062 RID: 98
	public class NotInitializedException : StoreException
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00045AC7 File Offset: 0x00043CC7
		public NotInitializedException(LID lid, string message) : base(lid, ErrorCodeValue.NotInitialized, message)
		{
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00045AD6 File Offset: 0x00043CD6
		public NotInitializedException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.NotInitialized, message, innerException)
		{
		}
	}
}
