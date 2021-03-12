using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000077 RID: 119
	public class CannotRegisterNewNamedPropertyMapping : StoreException
	{
		// Token: 0x06000487 RID: 1159 RVA: 0x0001CF04 File Offset: 0x0001B104
		public CannotRegisterNewNamedPropertyMapping(LID lid, string message) : base(lid, ErrorCodeValue.CannotRegisterNewNamedPropertyMapping, message)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001CF13 File Offset: 0x0001B113
		public CannotRegisterNewNamedPropertyMapping(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.CannotRegisterNewNamedPropertyMapping, message, innerException)
		{
		}
	}
}
