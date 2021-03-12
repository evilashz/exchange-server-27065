using System;

namespace Microsoft.Exchange.VariantConfiguration.Reflection
{
	// Token: 0x02000114 RID: 276
	public sealed class VariantConfigurationMrsComponent : VariantConfigurationComponent
	{
		// Token: 0x06000CA1 RID: 3233 RVA: 0x0001E388 File Offset: 0x0001C588
		internal VariantConfigurationMrsComponent() : base("Mrs")
		{
			base.Add(new VariantConfigurationSection("Mrs.settings.ini", "MigrationMonitor", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Mrs.settings.ini", "PublicFolderMailboxesMigration", typeof(IFeature), true));
			base.Add(new VariantConfigurationSection("Mrs.settings.ini", "UseDefaultValueForCheckInitialProvisioningForMovesParameter", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Mrs.settings.ini", "SlowMRSDetector", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Mrs.settings.ini", "CheckProvisioningSettings", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Mrs.settings.ini", "TxSyncMrsImapExecute", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Mrs.settings.ini", "TxSyncMrsImapCopy", typeof(IFeature), false));
			base.Add(new VariantConfigurationSection("Mrs.settings.ini", "AutomaticMailboxLoadBalancing", typeof(IFeature), false));
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0001E4A0 File Offset: 0x0001C6A0
		public VariantConfigurationSection MigrationMonitor
		{
			get
			{
				return base["MigrationMonitor"];
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0001E4AD File Offset: 0x0001C6AD
		public VariantConfigurationSection PublicFolderMailboxesMigration
		{
			get
			{
				return base["PublicFolderMailboxesMigration"];
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0001E4BA File Offset: 0x0001C6BA
		public VariantConfigurationSection UseDefaultValueForCheckInitialProvisioningForMovesParameter
		{
			get
			{
				return base["UseDefaultValueForCheckInitialProvisioningForMovesParameter"];
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06000CA5 RID: 3237 RVA: 0x0001E4C7 File Offset: 0x0001C6C7
		public VariantConfigurationSection SlowMRSDetector
		{
			get
			{
				return base["SlowMRSDetector"];
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0001E4D4 File Offset: 0x0001C6D4
		public VariantConfigurationSection CheckProvisioningSettings
		{
			get
			{
				return base["CheckProvisioningSettings"];
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06000CA7 RID: 3239 RVA: 0x0001E4E1 File Offset: 0x0001C6E1
		public VariantConfigurationSection TxSyncMrsImapExecute
		{
			get
			{
				return base["TxSyncMrsImapExecute"];
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0001E4EE File Offset: 0x0001C6EE
		public VariantConfigurationSection TxSyncMrsImapCopy
		{
			get
			{
				return base["TxSyncMrsImapCopy"];
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06000CA9 RID: 3241 RVA: 0x0001E4FB File Offset: 0x0001C6FB
		public VariantConfigurationSection AutomaticMailboxLoadBalancing
		{
			get
			{
				return base["AutomaticMailboxLoadBalancing"];
			}
		}
	}
}
