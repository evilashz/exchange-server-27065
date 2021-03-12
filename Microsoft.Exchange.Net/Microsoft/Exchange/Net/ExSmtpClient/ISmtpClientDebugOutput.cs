using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x020006E4 RID: 1764
	internal interface ISmtpClientDebugOutput
	{
		// Token: 0x0600217D RID: 8573
		void Output(Trace tracer, object context, string message, params object[] args);
	}
}
