using System;
using System.Configuration;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A3 RID: 163
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class TenantDataCollectorConfig : AnchorConfig
	{
		// Token: 0x06000445 RID: 1093 RVA: 0x00006ADC File Offset: 0x00004CDC
		internal TenantDataCollectorConfig() : base("TenantDataCollector")
		{
			base.UpdateConfig<Uri>("WebServiceUri", UpgradeCommon.DefaultSymphonyWebserviceUri);
			base.UpdateConfig<string>("CertificateSubject", UpgradeCommon.DefaultSymphonyCertificateSubject);
			base.UpdateConfig<TimeSpan>("IdleRunDelay", TimeSpan.FromHours(24.0));
			base.UpdateConfig<TimeSpan>("ActiveRunDelay", TimeSpan.FromHours(24.0));
			base.UpdateConfig<string>("MonitoringComponentName", ExchangeComponent.MailboxMigration.Name);
			base.UpdateConfig<string>("CacheEntryPoisonNotificationReason", "TenantDataCollectorCacheEntryIsPoisonedNotification");
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00006B6B File Offset: 0x00004D6B
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00006B78 File Offset: 0x00004D78
		[ConfigurationProperty("WebServiceUri")]
		public Uri WebServiceUri
		{
			get
			{
				return this.InternalGetConfig<Uri>("WebServiceUri");
			}
			set
			{
				this.InternalSetConfig<Uri>(value, "WebServiceUri");
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00006B86 File Offset: 0x00004D86
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00006B93 File Offset: 0x00004D93
		[ConfigurationProperty("CertificateSubject")]
		public string CertificateSubject
		{
			get
			{
				return this.InternalGetConfig<string>("CertificateSubject");
			}
			set
			{
				this.InternalSetConfig<string>(value, "CertificateSubject");
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00006BA1 File Offset: 0x00004DA1
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00006BAE File Offset: 0x00004DAE
		[ConfigurationProperty("E14DataDirectory", DefaultValue = "C$\\Program Files\\Microsoft\\Exchange Server\\V14\\logging\\CompleteMailboxStats")]
		public string E14DataDirectory
		{
			get
			{
				return this.InternalGetConfig<string>("E14DataDirectory");
			}
			set
			{
				this.InternalSetConfig<string>(value, "E14DataDirectory");
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00006BBC File Offset: 0x00004DBC
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00006BC9 File Offset: 0x00004DC9
		[ConfigurationProperty("E15DataDirectory", DefaultValue = "C$\\Program Files\\Microsoft\\Exchange Server\\V15\\logging\\CompleteMailboxStats")]
		public string E15DataDirectory
		{
			get
			{
				return this.InternalGetConfig<string>("E15DataDirectory");
			}
			set
			{
				this.InternalSetConfig<string>(value, "E15DataDirectory");
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00006BD7 File Offset: 0x00004DD7
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00006BE4 File Offset: 0x00004DE4
		[ConfigurationProperty("UpgradeUnitsConversionFactor", DefaultValue = 50)]
		[IntegerValidator(MinValue = 0, MaxValue = 1000, ExcludeRange = false)]
		public int UpgradeUnitsConversionFactor
		{
			get
			{
				return this.InternalGetConfig<int>("UpgradeUnitsConversionFactor");
			}
			set
			{
				this.InternalSetConfig<int>(value, "UpgradeUnitsConversionFactor");
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00006BF2 File Offset: 0x00004DF2
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00006BFF File Offset: 0x00004DFF
		[ConfigurationProperty("CheckAllAccountPartitions", DefaultValue = false)]
		public bool CheckAllAccountPartitions
		{
			get
			{
				return this.InternalGetConfig<bool>("CheckAllAccountPartitions");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "CheckAllAccountPartitions");
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00006C0D File Offset: 0x00004E0D
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00006C1A File Offset: 0x00004E1A
		[ConfigurationProperty("ValidateMailboxVersions", DefaultValue = true)]
		public bool ValidateMailboxVersions
		{
			get
			{
				return this.InternalGetConfig<bool>("ValidateMailboxVersions");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "ValidateMailboxVersions");
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00006C28 File Offset: 0x00004E28
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00006C35 File Offset: 0x00004E35
		[ConfigurationProperty("UploadToSymphony", DefaultValue = true)]
		public bool UploadToSymphony
		{
			get
			{
				return this.InternalGetConfig<bool>("UploadToSymphony");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "UploadToSymphony");
			}
		}

		// Token: 0x040001E5 RID: 485
		private const string TenantDataCollectorCacheEntryIsPoisonedNotification = "TenantDataCollectorCacheEntryIsPoisonedNotification";
	}
}
