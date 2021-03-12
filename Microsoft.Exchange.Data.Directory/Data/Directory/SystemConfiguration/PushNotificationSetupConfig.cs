using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000551 RID: 1361
	internal sealed class PushNotificationSetupConfig
	{
		// Token: 0x06003D44 RID: 15684 RVA: 0x000E920C File Offset: 0x000E740C
		public PushNotificationSetupConfig(PushNotificationApp[] installableBySetup, PushNotificationApp[] installableBySetupDedicated, string[] retiredBySetup, string[] retiredBySetupDedicated, string acsHierarchyNode, Dictionary<string, string> fallbackPartitionPerApp)
		{
			ArgumentValidator.ThrowIfNull("installableBySetup", installableBySetup);
			ArgumentValidator.ThrowIfNull("installableBySetupDedicated", installableBySetupDedicated);
			ArgumentValidator.ThrowIfNull("retiredBySetup", retiredBySetup);
			ArgumentValidator.ThrowIfNull("retiredBySetupDedicated", retiredBySetupDedicated);
			ArgumentValidator.ThrowIfNull("fallbackPartitionPerApp", fallbackPartitionPerApp);
			this.InstallableBySetup = installableBySetup;
			this.InstallableBySetupDedicated = installableBySetupDedicated;
			this.RetiredBySetup = retiredBySetup;
			this.RetiredBySetupDedicated = retiredBySetupDedicated;
			this.AcsHierarchyNode = acsHierarchyNode;
			this.FallbackPartitionMapping = fallbackPartitionPerApp;
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06003D45 RID: 15685 RVA: 0x000E9285 File Offset: 0x000E7485
		// (set) Token: 0x06003D46 RID: 15686 RVA: 0x000E928D File Offset: 0x000E748D
		public PushNotificationApp[] InstallableBySetup { get; private set; }

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000E9296 File Offset: 0x000E7496
		// (set) Token: 0x06003D48 RID: 15688 RVA: 0x000E929E File Offset: 0x000E749E
		public PushNotificationApp[] InstallableBySetupDedicated { get; private set; }

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06003D49 RID: 15689 RVA: 0x000E92A7 File Offset: 0x000E74A7
		// (set) Token: 0x06003D4A RID: 15690 RVA: 0x000E92AF File Offset: 0x000E74AF
		public string[] RetiredBySetup { get; private set; }

		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06003D4B RID: 15691 RVA: 0x000E92B8 File Offset: 0x000E74B8
		// (set) Token: 0x06003D4C RID: 15692 RVA: 0x000E92C0 File Offset: 0x000E74C0
		public string[] RetiredBySetupDedicated { get; private set; }

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06003D4D RID: 15693 RVA: 0x000E92C9 File Offset: 0x000E74C9
		// (set) Token: 0x06003D4E RID: 15694 RVA: 0x000E92D1 File Offset: 0x000E74D1
		public string AcsHierarchyNode { get; private set; }

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06003D4F RID: 15695 RVA: 0x000E92DA File Offset: 0x000E74DA
		// (set) Token: 0x06003D50 RID: 15696 RVA: 0x000E92E2 File Offset: 0x000E74E2
		public Dictionary<string, string> FallbackPartitionMapping { get; private set; }

		// Token: 0x04002963 RID: 10595
		public const string ExchangeOnlineDefaultAcsHierarchyNodeName = "exo";
	}
}
