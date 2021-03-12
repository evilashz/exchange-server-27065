using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessageSecurity;

namespace Microsoft.Exchange.MessageSecurity
{
	// Token: 0x02000002 RID: 2
	internal static class Common
	{
		// Token: 0x04000001 RID: 1
		public static readonly ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.EdgeCredentialServiceTracer.Category, "MSExchange Message Security");
	}
}
