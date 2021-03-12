using System;
using System.Configuration;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000BA RID: 186
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpgradeHandlerConfig : AnchorConfig
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x00009DF0 File Offset: 0x00007FF0
		internal UpgradeHandlerConfig() : base("UpgradeHandler")
		{
			base.UpdateConfig<Uri>("WebServiceUri", UpgradeCommon.DefaultSymphonyWebserviceUri);
			base.UpdateConfig<string>("CertificateSubject", UpgradeCommon.DefaultSymphonyCertificateSubject);
			base.UpdateConfig<TimeSpan>("IdleRunDelay", TimeSpan.FromMinutes(15.0));
			base.UpdateConfig<TimeSpan>("ScannerInitialTimeDelay", TimeSpan.FromMinutes(2.0));
			base.UpdateConfig<string>("MonitoringComponentName", ExchangeComponent.MailboxMigration.Name);
			base.UpdateConfig<string>("CacheEntryPoisonNotificationReason", "UpgradeHandlerCacheEntryIsPoisonedNotification");
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00009E7F File Offset: 0x0000807F
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00009E8C File Offset: 0x0000808C
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

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x00009E9A File Offset: 0x0000809A
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00009EA7 File Offset: 0x000080A7
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x00009EB5 File Offset: 0x000080B5
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x00009EC2 File Offset: 0x000080C2
		[IntegerValidator(MinValue = 1, MaxValue = 100, ExcludeRange = false)]
		[ConfigurationProperty("NumberOfSetMailboxAttempts", DefaultValue = 30)]
		public int NumberOfSetMailboxAttempts
		{
			get
			{
				return this.InternalGetConfig<int>("NumberOfSetMailboxAttempts");
			}
			set
			{
				this.InternalSetConfig<int>(value, "NumberOfSetMailboxAttempts");
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x00009ED0 File Offset: 0x000080D0
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x00009EDD File Offset: 0x000080DD
		[ConfigurationProperty("SetMailboxAttemptIntervalSeconds", DefaultValue = 1)]
		[IntegerValidator(MinValue = 1, MaxValue = 60, ExcludeRange = false)]
		public int SetMailboxAttemptIntervalSeconds
		{
			get
			{
				return this.InternalGetConfig<int>("SetMailboxAttemptIntervalSeconds");
			}
			set
			{
				this.InternalSetConfig<int>(value, "SetMailboxAttemptIntervalSeconds");
			}
		}

		// Token: 0x040002B0 RID: 688
		private const string UpgradeHandlerCacheEntryIsPoisonedNotification = "UpgradeHandlerCacheEntryIsPoisonedNotification";
	}
}
