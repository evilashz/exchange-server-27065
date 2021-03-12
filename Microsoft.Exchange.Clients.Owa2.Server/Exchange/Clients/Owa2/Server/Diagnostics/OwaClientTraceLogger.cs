using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200044E RID: 1102
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaClientTraceLogger : ExtensibleLogger
	{
		// Token: 0x06002529 RID: 9513 RVA: 0x000864BB File Offset: 0x000846BB
		public OwaClientTraceLogger() : base(new OwaClientTraceLogConfiguration(), OwaClientTraceLogger.GetEscapeLineBreaksConfigValue())
		{
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x000864CD File Offset: 0x000846CD
		public static void Initialize()
		{
			if (OwaClientTraceLogger.instance == null)
			{
				OwaClientTraceLogger.instance = new OwaClientTraceLogger();
			}
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x000864E0 File Offset: 0x000846E0
		public static void AppendToLog(ILogEvent logEvent)
		{
			if (OwaClientTraceLogger.instance != null)
			{
				OwaClientTraceLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000864F4 File Offset: 0x000846F4
		public static void AppendToLog(IEnumerable<ILogEvent> logEvent)
		{
			if (OwaClientTraceLogger.instance != null)
			{
				OwaClientTraceLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00086508 File Offset: 0x00084708
		private static bool GetEscapeLineBreaksConfigValue()
		{
			return AppConfigLoader.GetConfigBoolValue("Test_OWAClientLogEscapeLineBreaks", LogRowFormatter.DefaultEscapeLineBreaks);
		}

		// Token: 0x0400150D RID: 5389
		private static OwaClientTraceLogger instance;
	}
}
