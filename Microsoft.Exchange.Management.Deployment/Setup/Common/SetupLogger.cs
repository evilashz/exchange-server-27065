using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000028 RID: 40
	internal static class SetupLogger
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00003450 File Offset: 0x00001650
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000345C File Offset: 0x0000165C
		public static bool IsPrereqLogging
		{
			get
			{
				return SetupLogger.Logger.IsPrereqLogging;
			}
			set
			{
				SetupLogger.Logger.IsPrereqLogging = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003469 File Offset: 0x00001669
		public static LocalizedString HalfAsterixLine
		{
			get
			{
				return SetupLogger.halfAsterixLine;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003470 File Offset: 0x00001670
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00003486 File Offset: 0x00001686
		public static ISetupLogger Logger
		{
			get
			{
				ISetupLogger result;
				if ((result = SetupLogger.logger) == null)
				{
					result = (SetupLogger.logger = new SetupLoggerImpl());
				}
				return result;
			}
			set
			{
				SetupLogger.logger = value;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000348E File Offset: 0x0000168E
		public static void StartSetupLogging()
		{
			SetupLogger.Logger.StartLogging();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000349A File Offset: 0x0000169A
		public static void StopSetupLogging()
		{
			SetupLogger.Logger.StopLogging();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000034A6 File Offset: 0x000016A6
		public static void Log(LocalizedString localizedString)
		{
			SetupLogger.Logger.Log(localizedString);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000034B3 File Offset: 0x000016B3
		public static void LogWarning(LocalizedString localizedString)
		{
			SetupLogger.Logger.LogWarning(localizedString);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000034C0 File Offset: 0x000016C0
		public static void LogError(Exception e)
		{
			SetupLogger.Logger.LogError(e);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000034CD File Offset: 0x000016CD
		public static void TraceEnter(params object[] arguments)
		{
			SetupLogger.Logger.TraceEnter(arguments);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000034DA File Offset: 0x000016DA
		public static void TraceExit()
		{
			SetupLogger.Logger.TraceExit();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000034E6 File Offset: 0x000016E6
		public static void IncreaseIndentation(LocalizedString tag)
		{
			SetupLogger.Logger.IncreaseIndentation(tag);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000034F3 File Offset: 0x000016F3
		public static void DecreaseIndentation()
		{
			SetupLogger.Logger.DecreaseIndentation();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000034FF File Offset: 0x000016FF
		public static void LogForDataMining(string task, DateTime startTime)
		{
			SetupLogger.Logger.LogForDataMining(task, startTime);
		}

		// Token: 0x04000088 RID: 136
		private static readonly LocalizedString halfAsterixLine = new LocalizedString("**************");

		// Token: 0x04000089 RID: 137
		private static ISetupLogger logger;
	}
}
