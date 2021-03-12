using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000061 RID: 97
	public class NestedSearchChainTooDeepException : StoreException
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x00045AA8 File Offset: 0x00043CA8
		public NestedSearchChainTooDeepException(LID lid, string message) : base(lid, ErrorCodeValue.NestedSearchChainTooDeep, message)
		{
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00045AB7 File Offset: 0x00043CB7
		public NestedSearchChainTooDeepException(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.NestedSearchChainTooDeep, message, innerException)
		{
		}
	}
}
