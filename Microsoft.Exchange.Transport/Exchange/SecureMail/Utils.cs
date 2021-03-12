using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.SecureMail
{
	// Token: 0x0200051E RID: 1310
	internal static class Utils
	{
		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06003D3E RID: 15678 RVA: 0x000FFBED File Offset: 0x000FDDED
		internal static SecureMailTransportPerfCountersInstance SecureMailPerfCounters
		{
			get
			{
				return Utils.secureMailPerfCounters;
			}
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x000FFBF4 File Offset: 0x000FDDF4
		internal static void InitPerfCounters()
		{
			Utils.secureMailPerfCounters = SecureMailTransportPerfCounters.GetInstance("_Total");
		}

		// Token: 0x04001F28 RID: 7976
		internal const string DefaultPerfCountersInstance = "_Total";

		// Token: 0x04001F29 RID: 7977
		internal static ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.SecureMailTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04001F2A RID: 7978
		private static SecureMailTransportPerfCountersInstance secureMailPerfCounters;
	}
}
