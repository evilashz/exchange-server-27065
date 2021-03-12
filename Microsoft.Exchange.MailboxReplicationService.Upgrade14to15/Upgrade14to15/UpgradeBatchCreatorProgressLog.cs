using System;
using System.IO;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000BF RID: 191
	internal class UpgradeBatchCreatorProgressLog : ObjectLog<TenantUpgradeData>
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public UpgradeBatchCreatorProgressLog() : base(new UpgradeBatchCreatorProgressLog.UpgradeBatchCreatorProgressLogSchema(), new UpgradeBatchCreatorProgressLog.UpgradeBatchCreatorProgressLogConfiguration())
		{
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public static void Write(TenantOrganizationPresentationObjectWrapper tenant, string errorType, string errorDetails, int? upgradeE14MbxCountForCurrentStage = null, int? upgradeE14RequestCountForCurrentStage = null)
		{
			TenantUpgradeData objectToLog = default(TenantUpgradeData);
			objectToLog.Tenant = tenant;
			objectToLog.PilotUser = null;
			objectToLog.ErrorType = errorType;
			objectToLog.ErrorDetails = errorDetails;
			objectToLog.Tenant.UpgradeE14MbxCountForCurrentStage = upgradeE14MbxCountForCurrentStage;
			objectToLog.Tenant.UpgradeE14RequestCountForCurrentStage = upgradeE14RequestCountForCurrentStage;
			UpgradeBatchCreatorProgressLog.instance.LogObject(objectToLog);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0000B04F File Offset: 0x0000924F
		public static void FlushLog()
		{
			UpgradeBatchCreatorProgressLog.instance.Flush();
		}

		// Token: 0x040002CC RID: 716
		private static UpgradeBatchCreatorProgressLog instance = new UpgradeBatchCreatorProgressLog();

		// Token: 0x020000C0 RID: 192
		private class UpgradeBatchCreatorProgressLogSchema : ObjectLogSchema
		{
			// Token: 0x17000217 RID: 535
			// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0000B067 File Offset: 0x00009267
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Upgrade Batch Creator";
				}
			}

			// Token: 0x17000218 RID: 536
			// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000B06E File Offset: 0x0000926E
			public override string LogType
			{
				get
				{
					return "Batch Creator Log";
				}
			}

			// Token: 0x040002CD RID: 717
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> TenantId = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("TenantId", (TenantUpgradeData d) => d.Tenant.ExternalDirectoryOrganizationId);

			// Token: 0x040002CE RID: 718
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> TenantName = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("TenantName", (TenantUpgradeData d) => d.Tenant.Name);

			// Token: 0x040002CF RID: 719
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> TenantVersion = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("TenantVersion", (TenantUpgradeData d) => d.Tenant.AdminDisplayVersion.ToString());

			// Token: 0x040002D0 RID: 720
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UserId = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UserId", (TenantUpgradeData d) => string.Empty);

			// Token: 0x040002D1 RID: 721
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeRequest = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeRequest", (TenantUpgradeData d) => d.Tenant.UpgradeRequest.ToString());

			// Token: 0x040002D2 RID: 722
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeStatus = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeStatus", (TenantUpgradeData d) => d.Tenant.UpgradeStatus.ToString());

			// Token: 0x040002D3 RID: 723
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeStage = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeStage", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null)
				{
					return string.Empty;
				}
				return d.Tenant.UpgradeStage.Value.ToString();
			});

			// Token: 0x040002D4 RID: 724
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> IsUpgradingOrganization = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("IsUpgradingOrganization", (TenantUpgradeData d) => d.Tenant.IsUpgradingOrganization.ToString());

			// Token: 0x040002D5 RID: 725
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> IsUpgradeOperationInProgress = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("IsUpgradeOperationInProgress", (TenantUpgradeData d) => d.Tenant.IsUpgradeOperationInProgress.ToString());

			// Token: 0x040002D6 RID: 726
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> IsPilotingOrganization = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("IsPilotingOrganization", (TenantUpgradeData d) => d.Tenant.IsPilotingOrganization.ToString());

			// Token: 0x040002D7 RID: 727
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14SysMbxCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14SysMbxCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveArbitration)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14MbxCountForCurrentStage);
			});

			// Token: 0x040002D8 RID: 728
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14UserMbxCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14UserMbxCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveRegularUser)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14MbxCountForCurrentStage);
			});

			// Token: 0x040002D9 RID: 729
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14CloudOnlyMbxCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14CloudOnlyMbxCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveCloudOnlyArchive)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14MbxCountForCurrentStage);
			});

			// Token: 0x040002DA RID: 730
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14PilotMbxCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14PilotMbxCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveRegularPilot)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14MbxCountForCurrentStage);
			});

			// Token: 0x040002DB RID: 731
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14PilotCloudOnlyMbxCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14CloudOnlyArchivePilotMbxCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveCloudOnlyArchivePilot)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14MbxCountForCurrentStage);
			});

			// Token: 0x040002DC RID: 732
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14SysMoveCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14SysMoveCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveArbitration)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14RequestCountForCurrentStage);
			});

			// Token: 0x040002DD RID: 733
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14UserMoveCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14UserMoveCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveRegularUser)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14RequestCountForCurrentStage);
			});

			// Token: 0x040002DE RID: 734
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14CloudOnlyMoveCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14CloudOnlyArchiveMoveCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveCloudOnlyArchive)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14RequestCountForCurrentStage);
			});

			// Token: 0x040002DF RID: 735
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14PilotMoveCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14PilotMoveCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveRegularPilot)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14RequestCountForCurrentStage);
			});

			// Token: 0x040002E0 RID: 736
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> UpgradeE14PilotCloudOnlyMoveCount = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("UpgradeE14CloudOnlyArchivePilotMoveCount", delegate(TenantUpgradeData d)
			{
				if (d.Tenant.UpgradeStage == null || d.Tenant.UpgradeStage.Value != Microsoft.Exchange.Data.Directory.SystemConfiguration.UpgradeStage.MoveCloudOnlyArchivePilot)
				{
					return string.Empty;
				}
				return Convert.ToString(d.Tenant.UpgradeE14RequestCountForCurrentStage);
			});

			// Token: 0x040002E1 RID: 737
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> Errortype = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("Errortype", (TenantUpgradeData d) => d.ErrorType);

			// Token: 0x040002E2 RID: 738
			public static readonly ObjectLogSimplePropertyDefinition<TenantUpgradeData> ErrorDetails = new ObjectLogSimplePropertyDefinition<TenantUpgradeData>("ErrorDetails", (TenantUpgradeData d) => d.ErrorDetails);
		}

		// Token: 0x020000C1 RID: 193
		private class UpgradeBatchCreatorProgressLogConfiguration : ObjectLogConfiguration
		{
			// Token: 0x17000219 RID: 537
			// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000B8ED File Offset: 0x00009AED
			public override bool IsEnabled
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000B8F0 File Offset: 0x00009AF0
			public override TimeSpan MaxLogAge
			{
				get
				{
					return TimeSpan.FromDays(7.0);
				}
			}

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000B900 File Offset: 0x00009B00
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
							return Path.Combine(path, "Logging\\UpgradeBatchCreatorLogs");
						}
					}
					return null;
				}
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000B960 File Offset: 0x00009B60
			public override string LogComponentName
			{
				get
				{
					return "UpgradeBatchCreator";
				}
			}

			// Token: 0x1700021D RID: 541
			// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000B967 File Offset: 0x00009B67
			public override string FilenamePrefix
			{
				get
				{
					return "UpgradeBatchCreatorProgress";
				}
			}

			// Token: 0x1700021E RID: 542
			// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000B96E File Offset: 0x00009B6E
			public override long MaxLogDirSize
			{
				get
				{
					return 50000000L;
				}
			}

			// Token: 0x1700021F RID: 543
			// (get) Token: 0x060005D8 RID: 1496 RVA: 0x0000B976 File Offset: 0x00009B76
			public override long MaxLogFileSize
			{
				get
				{
					return 500000L;
				}
			}
		}
	}
}
