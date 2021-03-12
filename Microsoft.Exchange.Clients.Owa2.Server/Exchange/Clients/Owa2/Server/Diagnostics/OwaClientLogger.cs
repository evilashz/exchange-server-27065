using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x0200044C RID: 1100
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class OwaClientLogger : ExtensibleLogger
	{
		// Token: 0x06002512 RID: 9490 RVA: 0x00086317 File Offset: 0x00084517
		public OwaClientLogger() : base(new OwaClientLogConfiguration(), OwaClientLogger.GetEscapeLineBreaksConfigValue())
		{
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x00086329 File Offset: 0x00084529
		public static void Initialize()
		{
			if (OwaClientLogger.instance == null)
			{
				OwaClientLogger.instance = new OwaClientLogger();
			}
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x0008633C File Offset: 0x0008453C
		public static void TestInitialize(IExtensibleLogger logger)
		{
			OwaClientLogger.instance = logger;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x00086344 File Offset: 0x00084544
		public static void AppendToLog(ILogEvent logEvent)
		{
			if (OwaClientLogger.instance != null)
			{
				OwaClientLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x00086358 File Offset: 0x00084558
		public static void AppendToLog(IEnumerable<ILogEvent> logEvent)
		{
			if (OwaClientLogger.instance != null)
			{
				OwaClientLogger.instance.LogEvent(logEvent);
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x0008636C File Offset: 0x0008456C
		private static bool GetEscapeLineBreaksConfigValue()
		{
			return AppConfigLoader.GetConfigBoolValue("Test_OWAClientLogEscapeLineBreaks", LogRowFormatter.DefaultEscapeLineBreaks);
		}

		// Token: 0x04001502 RID: 5378
		private static IExtensibleLogger instance;
	}
}
