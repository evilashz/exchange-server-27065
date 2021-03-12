using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000125 RID: 293
	internal interface IExtensibleLogger : IWorkloadLogger
	{
		// Token: 0x06000875 RID: 2165
		void LogEvent(ILogEvent logEvent);

		// Token: 0x06000876 RID: 2166
		void LogEvent(IEnumerable<ILogEvent> logEvents);
	}
}
