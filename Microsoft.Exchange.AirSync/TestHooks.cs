using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.AAD;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000285 RID: 645
	internal static class TestHooks
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0008C8E2 File Offset: 0x0008AAE2
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x0008C8E9 File Offset: 0x0008AAE9
		public static Func<GlobalSettingsPropertyDefinition, string> GlobalSettings_GetAppSetting { get; set; }

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0008C8F1 File Offset: 0x0008AAF1
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x0008C8F8 File Offset: 0x0008AAF8
		public static Func<GlobalSettingsPropertyDefinition, object> GlobalSettings_GetFlightingSetting { get; set; }

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0008C900 File Offset: 0x0008AB00
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x0008C907 File Offset: 0x0008AB07
		public static Func<GlobalSettingsPropertyDefinition, string, object> GlobalSettings_GetRegistrySetting { get; set; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060017B9 RID: 6073 RVA: 0x0008C90F File Offset: 0x0008AB0F
		// (set) Token: 0x060017BA RID: 6074 RVA: 0x0008C916 File Offset: 0x0008AB16
		public static Func<GlobalSettingsPropertyDefinition, object> GlobalSettings_GetVDirSetting { get; set; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060017BB RID: 6075 RVA: 0x0008C91E File Offset: 0x0008AB1E
		// (set) Token: 0x060017BC RID: 6076 RVA: 0x0008C925 File Offset: 0x0008AB25
		public static Func<bool> GccUtils_AreStoredSecurityKeysValid { get; set; }

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x0008C92D File Offset: 0x0008AB2D
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x0008C934 File Offset: 0x0008AB34
		public static Action<ExEventLog.EventTuple, string, string[]> EventLog_LogEvent { get; set; }

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x0008C93C File Offset: 0x0008AB3C
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x0008C943 File Offset: 0x0008AB43
		public static Func<Participant, Participant> EmailAddressConverter_ADLookup { get; set; }

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060017C1 RID: 6081 RVA: 0x0008C94B File Offset: 0x0008AB4B
		// (set) Token: 0x060017C2 RID: 6082 RVA: 0x0008C952 File Offset: 0x0008AB52
		public static Func<OrganizationId, IAadClient> GraphApi_GetAadClient { get; set; }
	}
}
