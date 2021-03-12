using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Logging.Search;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x0200028B RID: 651
	internal class CommonDiagnosticsLogTracer : TraceWrapper.ITraceWriter
	{
		// Token: 0x06001284 RID: 4740 RVA: 0x00055B60 File Offset: 0x00053D60
		public void Write(string message)
		{
			KeyValuePair<string, object>[] eventData = new KeyValuePair<string, object>[]
			{
				new KeyValuePair<string, object>("ThreadId", Environment.CurrentManagedThreadId),
				new KeyValuePair<string, object>("Message", message)
			};
			CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.Trace, eventData);
		}

		// Token: 0x04000C16 RID: 3094
		private const string ThreadId = "ThreadId";

		// Token: 0x04000C17 RID: 3095
		private const string Message = "Message";
	}
}
