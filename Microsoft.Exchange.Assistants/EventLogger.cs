using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000040 RID: 64
	internal sealed class EventLogger
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0000E688 File Offset: 0x0000C888
		public EventLogger(string serviceName)
		{
			this.serviceName = serviceName;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		public void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			object[] array = new object[messageArgs.Length + 1];
			array[0] = this.serviceName;
			Array.Copy(messageArgs, 0, array, 1, messageArgs.Length);
			this.logger.LogEvent(tuple, periodicKey, array);
		}

		// Token: 0x040001AC RID: 428
		private ExEventLog logger = new ExEventLog(Globals.ComponentGuid, "MSExchange Assistants");

		// Token: 0x040001AD RID: 429
		private string serviceName;
	}
}
