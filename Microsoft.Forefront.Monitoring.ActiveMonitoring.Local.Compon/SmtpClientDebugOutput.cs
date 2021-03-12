using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Net.ExSmtpClient;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000248 RID: 584
	internal class SmtpClientDebugOutput : ISmtpClientDebugOutput
	{
		// Token: 0x060013A8 RID: 5032 RVA: 0x0003A63D File Offset: 0x0003883D
		public void Output(Trace tracer, object context, string message, params object[] args)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.SMTPTracer, this.traceContext, message, args, null, "Output", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Smtp\\Probes\\SmtpClientDebugOutput.cs", 48);
		}

		// Token: 0x04000933 RID: 2355
		private TracingContext traceContext = new TracingContext();
	}
}
