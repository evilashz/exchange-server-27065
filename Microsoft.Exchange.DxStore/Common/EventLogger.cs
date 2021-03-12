using System;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200005C RID: 92
	internal static class EventLogger
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00009D8C File Offset: 0x00007F8C
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00009D93 File Offset: 0x00007F93
		public static IDxStoreEventLogger Instance { get; set; }

		// Token: 0x060003A5 RID: 933 RVA: 0x00009D9C File Offset: 0x00007F9C
		public static void LogErr(string formatString, params object[] args)
		{
			IDxStoreEventLogger instance = EventLogger.Instance;
			if (instance != null)
			{
				instance.Log(DxEventSeverity.Error, 0, formatString, args);
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00009DBC File Offset: 0x00007FBC
		public static void LogInfo(string formatString, params object[] args)
		{
			IDxStoreEventLogger instance = EventLogger.Instance;
			if (instance != null)
			{
				instance.Log(DxEventSeverity.Info, 0, formatString, args);
			}
		}
	}
}
