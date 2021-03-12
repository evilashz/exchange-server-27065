using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000078 RID: 120
	public class CannotRegisterNewReplidGuidMapping : StoreException
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0001CF23 File Offset: 0x0001B123
		public CannotRegisterNewReplidGuidMapping(LID lid, string message) : base(lid, ErrorCodeValue.CannotRegisterNewReplidGuidMapping, message)
		{
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001CF32 File Offset: 0x0001B132
		public CannotRegisterNewReplidGuidMapping(LID lid, string message, Exception innerException) : base(lid, ErrorCodeValue.CannotRegisterNewReplidGuidMapping, message, innerException)
		{
		}
	}
}
