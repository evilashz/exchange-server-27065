using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000060 RID: 96
	public class CorruptSearchScopeException : StoreException
	{
		// Token: 0x060007E4 RID: 2020 RVA: 0x00045A89 File Offset: 0x00043C89
		public CorruptSearchScopeException(LID lid, string message) : base(lid, ErrorCodeValue.CorruptSearchScope, message)
		{
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00045A98 File Offset: 0x00043C98
		public CorruptSearchScopeException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.CorruptSearchScope, message, innerException)
		{
		}
	}
}
