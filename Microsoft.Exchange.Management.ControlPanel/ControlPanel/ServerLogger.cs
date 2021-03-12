using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200021A RID: 538
	internal sealed class ServerLogger : ExtensibleLogger
	{
		// Token: 0x06002745 RID: 10053 RVA: 0x0007AF36 File Offset: 0x00079136
		private ServerLogger() : base(new ServerLogConfiguration())
		{
		}

		// Token: 0x04001FDB RID: 8155
		internal static readonly ServerLogger Instance = new ServerLogger();
	}
}
