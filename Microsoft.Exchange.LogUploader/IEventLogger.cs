using System;
using Microsoft.Exchange.LogUploaderProxy;

namespace Microsoft.Exchange.LogUploader
{
	// Token: 0x02000010 RID: 16
	internal interface IEventLogger
	{
		// Token: 0x060000D5 RID: 213
		void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs);
	}
}
