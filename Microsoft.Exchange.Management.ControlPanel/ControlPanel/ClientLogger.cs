using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000217 RID: 535
	internal sealed class ClientLogger : ExtensibleLogger
	{
		// Token: 0x0600272F RID: 10031 RVA: 0x0007ACAC File Offset: 0x00078EAC
		private ClientLogger() : base(new ClientLogConfiguration())
		{
		}

		// Token: 0x04001FCE RID: 8142
		internal static readonly ClientLogger Instance = new ClientLogger();
	}
}
