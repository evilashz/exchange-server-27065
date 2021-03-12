using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000008 RID: 8
	internal sealed class ServerLogger : ExtensibleLogger
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000027AA File Offset: 0x000009AA
		private ServerLogger() : base(new ServerLogConfiguration())
		{
		}

		// Token: 0x0400002A RID: 42
		internal static readonly ServerLogger Instance = new ServerLogger();
	}
}
