using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x0200003B RID: 59
	internal static class UcmaAudioLogging
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000AF5C File Offset: 0x0000915C
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000AF63 File Offset: 0x00009163
		public static bool CmdAndControlAudioLoggingEnabled { get; private set; } = UcmaAudioLogging.GetRegistryValue("CommandAndControlAudioLogging");

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000AF6B File Offset: 0x0000916B
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000AF72 File Offset: 0x00009172
		public static bool MobileSpeechRecoAudioLoggingEnabled { get; private set; } = UcmaAudioLogging.GetRegistryValue("MobileSpeechRecoAudioLogging");

		// Token: 0x0600028E RID: 654 RVA: 0x0000AF7C File Offset: 0x0000917C
		private static bool GetRegistryValue(string valueName)
		{
			bool flag = false;
			int num = 0;
			if (Utils.TryReadRegValue("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", valueName, out num) && num == 1)
			{
				flag = true;
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.UCMATracer, 0, "UCMA audio logging setting Name='{0}', Value='{1}', Enabled='{2}'", new object[]
			{
				valueName,
				num,
				flag
			});
			return flag;
		}

		// Token: 0x040000DD RID: 221
		private const string CmdAndControlAudioLoggingValueName = "CommandAndControlAudioLogging";

		// Token: 0x040000DE RID: 222
		private const string MobileSpeechRecoAudioLoggingValueName = "MobileSpeechRecoAudioLogging";
	}
}
