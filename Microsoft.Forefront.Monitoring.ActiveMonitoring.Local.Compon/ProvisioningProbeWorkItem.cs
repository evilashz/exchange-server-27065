﻿using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001EC RID: 492
	public abstract class ProvisioningProbeWorkItem : ProbeWorkItem
	{
		// Token: 0x06000E9A RID: 3738 RVA: 0x000235E4 File Offset: 0x000217E4
		protected void AwaitCompletion(string actionName, TimeSpan interval, TimeSpan timeout, Func<bool> action)
		{
			DateTime utcNow = DateTime.UtcNow;
			TimeSpan timeSpan = DateTime.UtcNow - utcNow;
			while (timeSpan < timeout && !action())
			{
				Thread.Sleep(interval);
				timeSpan = DateTime.UtcNow - utcNow;
				this.TraceInformation("Waiting for action {0} to complete; already waited {1}.", new object[]
				{
					actionName,
					timeSpan
				});
			}
			if (timeSpan >= timeout)
			{
				throw new Exception(string.Format("Action {0} did not complete within the timeout window {1}...", actionName, timeout));
			}
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0002366C File Offset: 0x0002186C
		protected void TraceInformation(string message, params object[] messageArgs)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + "<Information>" + string.Format(message, messageArgs) + "</Information>";
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ProvisioningTracer, base.TraceContext, message, messageArgs, null, "TraceInformation", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Provisioning\\ProvisioningProbeWorkItem.cs", 59);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000236C0 File Offset: 0x000218C0
		protected void TraceError(string message, params object[] messageArgs)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + "<Error>" + string.Format(message, messageArgs) + "</Error>";
			WTFDiagnostics.TraceError(ExTraceGlobals.ProvisioningTracer, base.TraceContext, message, messageArgs, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Provisioning\\ProvisioningProbeWorkItem.cs", 74);
		}
	}
}
