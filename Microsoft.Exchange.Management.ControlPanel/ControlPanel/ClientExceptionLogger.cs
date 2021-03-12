using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001B0 RID: 432
	internal sealed class ClientExceptionLogger : ExtensibleLogger
	{
		// Token: 0x060023B7 RID: 9143 RVA: 0x0006D569 File Offset: 0x0006B769
		private ClientExceptionLogger() : base(new ClientExceptionLogConfiguration())
		{
		}

		// Token: 0x04001E25 RID: 7717
		internal static readonly ClientExceptionLogger Instance = new ClientExceptionLogger();
	}
}
