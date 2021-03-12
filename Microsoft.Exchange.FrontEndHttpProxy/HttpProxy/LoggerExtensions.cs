using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000042 RID: 66
	internal static class LoggerExtensions
	{
		// Token: 0x06000200 RID: 512 RVA: 0x0000B125 File Offset: 0x00009325
		internal static void SafeSet(this RequestDetailsLogger logger, Enum key, object value)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(logger, key, value);
		}
	}
}
