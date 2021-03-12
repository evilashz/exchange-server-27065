using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccess
{
	// Token: 0x02000048 RID: 72
	public interface IExecutionPlanner
	{
		// Token: 0x06000355 RID: 853
		void AppendToTraceContentBuilder(TraceContentBuilder cb, int indentLevel, string title);
	}
}
