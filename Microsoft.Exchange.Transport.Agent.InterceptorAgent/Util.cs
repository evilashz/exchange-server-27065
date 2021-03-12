using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000027 RID: 39
	public static class Util
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007742 File Offset: 0x00005942
		internal static ExEventLog EventLog
		{
			get
			{
				return Util.eventLogger;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00007749 File Offset: 0x00005949
		internal static InterceptorAgentPerfCountersInstance PerfCountersTotalInstance
		{
			get
			{
				return InterceptorAgentPerfCounters.GetInstance(Util.TotalInstanceName);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007758 File Offset: 0x00005958
		internal static InterceptorAgentPerfCountersInstance GetPerfCountersInstance(string instanceName, out bool existed)
		{
			string instanceName2 = Util.PerfCounterPrefix + instanceName;
			existed = InterceptorAgentPerfCounters.InstanceExists(instanceName2);
			return InterceptorAgentPerfCounters.GetInstance(instanceName2);
		}

		// Token: 0x040000CB RID: 203
		public static readonly string PerfCounterPrefix = Process.GetCurrentProcess().ProcessName + "_";

		// Token: 0x040000CC RID: 204
		public static readonly string TotalInstanceName = Util.PerfCounterPrefix + "Total";

		// Token: 0x040000CD RID: 205
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.InterceptorAgentTracer.Category, TransportEventLog.GetEventSource());
	}
}
