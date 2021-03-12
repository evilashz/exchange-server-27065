using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000070 RID: 112
	public class ParameterOverflow : StoreException
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x000109B7 File Offset: 0x0000EBB7
		public ParameterOverflow(LID lid, string message) : base(lid, ErrorCodeValue.ParameterOverflow, message)
		{
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x000109C6 File Offset: 0x0000EBC6
		public ParameterOverflow(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.ParameterOverflow, message, innerException)
		{
		}
	}
}
