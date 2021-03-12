using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000005 RID: 5
	internal sealed class AlertingTracer
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002341 File Offset: 0x00000541
		public AlertingTracer(Trace tracer, string application)
		{
			this.tracer = tracer;
			this.application = application;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002357 File Offset: 0x00000557
		// (set) Token: 0x06000013 RID: 19 RVA: 0x0000235E File Offset: 0x0000055E
		internal static bool Enabled
		{
			get
			{
				return AlertingTracer.enabled;
			}
			set
			{
				AlertingTracer.enabled = value;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002368 File Offset: 0x00000568
		internal void TraceError(int traceId, string formatString, params object[] parameters)
		{
			try
			{
				if (AlertingTracer.Enabled)
				{
					string text = (parameters == null || parameters.Length == 0) ? formatString : string.Format(formatString, parameters);
					if (this.tracer != null)
					{
						this.tracer.TraceError((long)traceId, text);
					}
					SystemProbe.Trace(this.application, SystemProbe.Status.Fail, text, new object[0]);
					new EventNotificationItem(ExchangeComponent.Transport.Name, ExchangeComponent.Transport.Name, null, text, ResultSeverityLevel.Error).Publish(false);
				}
			}
			catch (FormatException)
			{
				SystemProbe.Trace(this.application, SystemProbe.Status.Fail, "Error logging", new object[0]);
			}
		}

		// Token: 0x04000005 RID: 5
		private static bool enabled = true;

		// Token: 0x04000006 RID: 6
		private readonly Trace tracer;

		// Token: 0x04000007 RID: 7
		private readonly string application;
	}
}
