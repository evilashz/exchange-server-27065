using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000C5 RID: 197
	internal class UpgradeHandlerSyncLog : DisposableObjectLog<TenantUpgradeData>
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x0000D2DB File Offset: 0x0000B4DB
		public UpgradeHandlerSyncLog(string logFilePrefix) : base(new UpgradeHandlerSyncLog.UpgradeHandlerSyncLogSchema(), new UpgradeHandlerSyncLog.UpgradeHandlerSyncLogConfiguration(logFilePrefix))
		{
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		public void Write(TenantOrganizationPresentationObjectWrapper tenant, RecipientWrapper user, string errorType, string errorDetails)
		{
			base.LogObject(new TenantUpgradeData
			{
				Tenant = tenant,
				PilotUser = user,
				ErrorType = errorType,
				ErrorDetails = errorDetails
			});
		}

		// Token: 0x020000C6 RID: 198
		private class UpgradeHandlerSyncLogSchema : ObjectLogSchema
		{
			// Token: 0x17000225 RID: 549
			// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000D32D File Offset: 0x0000B52D
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Upgrade Hanlder";
				}
			}

			// Token: 0x17000226 RID: 550
			// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0000D334 File Offset: 0x0000B534
			public override string LogType
			{
				get
				{
					return "Handler Log";
				}
			}

			// Token: 0x0400031A RID: 794
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> TenantId = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("TenantId", delegate(TenantUpgradeData d)
			{
				if (d.Tenant == null)
				{
					return string.Empty;
				}
				return d.Tenant.ExternalDirectoryOrganizationId;
			});

			// Token: 0x0400031B RID: 795
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> TenantName = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("TenantName", delegate(TenantUpgradeData d)
			{
				if (d.Tenant == null)
				{
					return string.Empty;
				}
				return d.Tenant.Name;
			});

			// Token: 0x0400031C RID: 796
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UserId = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UserId", delegate(TenantUpgradeData d)
			{
				if (d.PilotUser == null)
				{
					return string.Empty;
				}
				return d.PilotUser.Id.ObjectGuid.ToString();
			});

			// Token: 0x0400031D RID: 797
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeRequest = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeRequest", delegate(TenantUpgradeData d)
			{
				if (d.PilotUser != null)
				{
					return d.PilotUser.UpgradeRequest.ToString();
				}
				if (d.Tenant == null)
				{
					return string.Empty;
				}
				return d.Tenant.UpgradeRequest.ToString();
			});

			// Token: 0x0400031E RID: 798
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeStatus = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeStatus", delegate(TenantUpgradeData d)
			{
				if (d.PilotUser != null)
				{
					return d.PilotUser.UpgradeStatus.ToString();
				}
				if (d.Tenant == null)
				{
					return string.Empty;
				}
				return d.Tenant.UpgradeStatus.ToString();
			});

			// Token: 0x0400031F RID: 799
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeStageChangeTo = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeStageChangeTo", delegate(TenantUpgradeData d)
			{
				if (d.PilotUser != null)
				{
					return Convert.ToString(d.PilotUser.UpgradeStage);
				}
				if (d.Tenant == null)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeStage);
			});

			// Token: 0x04000320 RID: 800
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> Errortype = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("Errortype", (TenantUpgradeData d) => d.ErrorType);

			// Token: 0x04000321 RID: 801
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> ErrorDetails = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("ErrorDetails", (TenantUpgradeData d) => d.ErrorDetails);
		}

		// Token: 0x020000C7 RID: 199
		private class UpgradeHandlerSyncLogConfiguration : ObjectLogConfiguration
		{
			// Token: 0x06000603 RID: 1539 RVA: 0x0000D635 File Offset: 0x0000B835
			public UpgradeHandlerSyncLogConfiguration(string logFilePrefix)
			{
				this.logFilePrefix = logFilePrefix;
			}

			// Token: 0x17000227 RID: 551
			// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000D644 File Offset: 0x0000B844
			public override bool IsEnabled
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000228 RID: 552
			// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000D647 File Offset: 0x0000B847
			public override TimeSpan MaxLogAge
			{
				get
				{
					return TimeSpan.FromDays(7.0);
				}
			}

			// Token: 0x17000229 RID: 553
			// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000D658 File Offset: 0x0000B858
			public override string LoggingFolder
			{
				get
				{
					string name = "SOFTWARE\\Microsoft\\ExchangeServer\\V15\\Setup";
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
					{
						if (registryKey != null)
						{
							string path = registryKey.GetValue("MsiInstallPath").ToString();
							return Path.Combine(path, "Logging\\UpgradeHandlerLogs");
						}
					}
					return null;
				}
			}

			// Token: 0x1700022A RID: 554
			// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000D6B8 File Offset: 0x0000B8B8
			public override string LogComponentName
			{
				get
				{
					return "UpgradeHandler";
				}
			}

			// Token: 0x1700022B RID: 555
			// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000D6BF File Offset: 0x0000B8BF
			public override string FilenamePrefix
			{
				get
				{
					return this.logFilePrefix;
				}
			}

			// Token: 0x1700022C RID: 556
			// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000D6C7 File Offset: 0x0000B8C7
			public override long MaxLogDirSize
			{
				get
				{
					return 50000000L;
				}
			}

			// Token: 0x1700022D RID: 557
			// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000D6CF File Offset: 0x0000B8CF
			public override long MaxLogFileSize
			{
				get
				{
					return 500000L;
				}
			}

			// Token: 0x0400032A RID: 810
			private readonly string logFilePrefix;
		}
	}
}
