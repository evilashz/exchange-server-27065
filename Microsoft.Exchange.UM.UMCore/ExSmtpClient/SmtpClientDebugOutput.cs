using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.ExSmtpClient
{
	// Token: 0x02000269 RID: 617
	internal class SmtpClientDebugOutput : ISmtpClientDebugOutput
	{
		// Token: 0x06001253 RID: 4691 RVA: 0x000510AA File Offset: 0x0004F2AA
		public void Output(Trace tracer, object context, string message, params object[] args)
		{
			CallIdTracer.TraceDebug(tracer, context, message, args);
		}
	}
}
