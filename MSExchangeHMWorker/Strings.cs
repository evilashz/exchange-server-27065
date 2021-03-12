using System;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.ActiveMonitoring
{
	// Token: 0x02000007 RID: 7
	internal static class Strings
	{
		// Token: 0x06000020 RID: 32 RVA: 0x0000359C File Offset: 0x0000179C
		public static LocalizedString StartedWithMaintenanceWorkBrokerOnly(bool monitoringIsOnline, bool recoveryActionsEnabledIsOnline)
		{
			return new LocalizedString("StartedWithMaintenanceWorkBrokerOnly", Strings.ResourceManager, new object[]
			{
				monitoringIsOnline,
				recoveryActionsEnabledIsOnline
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000035D4 File Offset: 0x000017D4
		public static LocalizedString StartedWithAllWorkBrokers(bool monitoringIsOnline, bool recoveryActionsEnabledIsOnline)
		{
			return new LocalizedString("StartedWithAllWorkBrokers", Strings.ResourceManager, new object[]
			{
				monitoringIsOnline,
				recoveryActionsEnabledIsOnline
			});
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000360C File Offset: 0x0000180C
		public static LocalizedString UsageText(string processName)
		{
			return new LocalizedString("UsageText", Strings.ResourceManager, new object[]
			{
				processName
			});
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003634 File Offset: 0x00001834
		public static LocalizedString StartedWithAllWorkBrokersExceptResponder(bool monitoringIsOnline, bool recoveryActionsEnabledIsOnline)
		{
			return new LocalizedString("StartedWithAllWorkBrokersExceptResponder", Strings.ResourceManager, new object[]
			{
				monitoringIsOnline,
				recoveryActionsEnabledIsOnline
			});
		}

		// Token: 0x0400003C RID: 60
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.ActiveMonitoring.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000008 RID: 8
		private enum ParamIDs
		{
			// Token: 0x0400003E RID: 62
			StartedWithMaintenanceWorkBrokerOnly,
			// Token: 0x0400003F RID: 63
			StartedWithAllWorkBrokers,
			// Token: 0x04000040 RID: 64
			UsageText,
			// Token: 0x04000041 RID: 65
			StartedWithAllWorkBrokersExceptResponder
		}
	}
}
