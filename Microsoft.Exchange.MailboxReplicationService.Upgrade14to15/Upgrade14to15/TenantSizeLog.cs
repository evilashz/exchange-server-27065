using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Win32;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x020000A9 RID: 169
	internal class TenantSizeLog : DisposableObjectLog<TenantSize>
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x00008699 File Offset: 0x00006899
		public TenantSizeLog(string logFilePrefix) : base(new TenantSizeLog.TenantSizeLogSchema(), new TenantSizeLog.TenantSizeLogConfiguration(logFilePrefix))
		{
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000086AC File Offset: 0x000068AC
		public void Write(TenantData tenantData, string error)
		{
			TenantSize objectToLog = new TenantSize
			{
				ExternalDirectoryOrganizationId = tenantData.TenantId,
				Name = tenantData.TenantName,
				Constraints = tenantData.Constraints,
				UpgradeConstraintsDisabled = tenantData.UpgradeConstraintsDisabled,
				UpgradeUnitsOverride = tenantData.UpgradeUnitsOverride,
				ServicePlan = tenantData.ServicePlan,
				ProgramId = tenantData.ProgramId,
				OfferId = tenantData.OfferId,
				AdminDisplayVersion = tenantData.Version,
				IsUpgradingOrganization = tenantData.IsUpgradingOrganization,
				IsPilotingOrganization = tenantData.IsPilotingOrganization,
				E14PrimaryMbxCount = tenantData.E14MbxData.PrimaryData.Count,
				E14PrimaryMbxSize = tenantData.E14MbxData.PrimaryData.Size,
				E14ArchiveMbxCount = tenantData.E14MbxData.ArchiveData.Count,
				E14ArchiveMbxSize = tenantData.E14MbxData.ArchiveData.Size,
				E15PrimaryMbxCount = tenantData.E15MbxData.PrimaryData.Count,
				E15PrimaryMbxSize = tenantData.E15MbxData.PrimaryData.Size,
				E15ArchiveMbxCount = tenantData.E15MbxData.ArchiveData.Count,
				E15ArchiveMbxSize = tenantData.E15MbxData.ArchiveData.Size,
				TotalPrimaryMbxCount = tenantData.TotalPrimaryMbxCount,
				TotalPrimaryMbxSize = tenantData.TotalPrimaryMbxSize,
				TotalArchiveMbxCount = tenantData.TotalArchiveMbxCount,
				TotalArchiveMbxSize = tenantData.TotalArchiveMbxSize,
				UploadedSize = tenantData.TenantSize,
				ValidationError = error
			};
			base.LogObject(objectToLog);
		}

		// Token: 0x020000AA RID: 170
		private class TenantSizeLogSchema : ObjectLogSchema
		{
			// Token: 0x170001E8 RID: 488
			// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000885A File Offset: 0x00006A5A
			public override string Software
			{
				get
				{
					return "Microsoft Exchange Tenant Data Collector";
				}
			}

			// Token: 0x170001E9 RID: 489
			// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00008861 File Offset: 0x00006A61
			public override string LogType
			{
				get
				{
					return "Tenant Size Log";
				}
			}

			// Token: 0x0400022F RID: 559
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> ExternalDirectoryOrganizationId = new ObjectLogSimplePropertyDefinition<TenantSize>("TenantId", (TenantSize d) => d.ExternalDirectoryOrganizationId.ToString());

			// Token: 0x04000230 RID: 560
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> Name = new ObjectLogSimplePropertyDefinition<TenantSize>("Name", (TenantSize d) => d.Name ?? string.Empty);

			// Token: 0x04000231 RID: 561
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> Constraints = new ObjectLogSimplePropertyDefinition<TenantSize>("Constraints", delegate(TenantSize d)
			{
				if (d.Constraints != null)
				{
					return string.Join(";", d.Constraints);
				}
				return string.Empty;
			});

			// Token: 0x04000232 RID: 562
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> UpgradeConstraintsDisabled = new ObjectLogSimplePropertyDefinition<TenantSize>("UpgradeConstraintsDisabled", (TenantSize d) => d.UpgradeConstraintsDisabled);

			// Token: 0x04000233 RID: 563
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> UpgradeUnitsOverride = new ObjectLogSimplePropertyDefinition<TenantSize>("UpgradeUnitsOverride", (TenantSize d) => d.UpgradeUnitsOverride);

			// Token: 0x04000234 RID: 564
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> ServicePlan = new ObjectLogSimplePropertyDefinition<TenantSize>("ServicePlan", (TenantSize d) => d.ServicePlan ?? string.Empty);

			// Token: 0x04000235 RID: 565
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> ProgramId = new ObjectLogSimplePropertyDefinition<TenantSize>("ProgramId", (TenantSize d) => d.ProgramId ?? string.Empty);

			// Token: 0x04000236 RID: 566
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> OfferId = new ObjectLogSimplePropertyDefinition<TenantSize>("OfferId", (TenantSize d) => d.OfferId ?? string.Empty);

			// Token: 0x04000237 RID: 567
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> AdminDisplayVersion = new ObjectLogSimplePropertyDefinition<TenantSize>("AdminDisplayVersion", delegate(TenantSize d)
			{
				if (!(d.AdminDisplayVersion == null))
				{
					return d.AdminDisplayVersion.ToString();
				}
				return string.Empty;
			});

			// Token: 0x04000238 RID: 568
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> IsUpgradingOrganization = new ObjectLogSimplePropertyDefinition<TenantSize>("IsUpgradingOrganization", (TenantSize d) => d.IsUpgradingOrganization);

			// Token: 0x04000239 RID: 569
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> IsPilotingOrganization = new ObjectLogSimplePropertyDefinition<TenantSize>("IsPilotingOrganization", (TenantSize d) => d.IsPilotingOrganization);

			// Token: 0x0400023A RID: 570
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> E14PrimaryMbxCount = new ObjectLogSimplePropertyDefinition<TenantSize>("E14PrimaryMbxCount", (TenantSize d) => d.E14PrimaryMbxCount);

			// Token: 0x0400023B RID: 571
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> E14PrimaryMbxSize = new ObjectLogSimplePropertyDefinition<TenantSize>("E14PrimaryMbxSize", (TenantSize d) => d.E14PrimaryMbxSize);

			// Token: 0x0400023C RID: 572
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> E14ArchiveMbxCount = new ObjectLogSimplePropertyDefinition<TenantSize>("E14ArchiveMbxCount", (TenantSize d) => d.E14ArchiveMbxCount);

			// Token: 0x0400023D RID: 573
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> E14ArchiveMbxSize = new ObjectLogSimplePropertyDefinition<TenantSize>("E14ArchiveMbxSize", (TenantSize d) => d.E14ArchiveMbxSize);

			// Token: 0x0400023E RID: 574
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> E15PrimaryMbxCount = new ObjectLogSimplePropertyDefinition<TenantSize>("E15PrimaryMbxCount", (TenantSize d) => d.E15PrimaryMbxCount);

			// Token: 0x0400023F RID: 575
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> E15PrimaryMbxSize = new ObjectLogSimplePropertyDefinition<TenantSize>("E15PrimaryMbxSize", (TenantSize d) => d.E15PrimaryMbxSize);

			// Token: 0x04000240 RID: 576
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> E15ArchiveMbxCount = new ObjectLogSimplePropertyDefinition<TenantSize>("E15ArchiveMbxCount", (TenantSize d) => d.E15ArchiveMbxCount);

			// Token: 0x04000241 RID: 577
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> E15ArchiveMbxSize = new ObjectLogSimplePropertyDefinition<TenantSize>("E15ArchiveMbxSize", (TenantSize d) => d.E15ArchiveMbxSize);

			// Token: 0x04000242 RID: 578
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> TotalPrimaryMbxCount = new ObjectLogSimplePropertyDefinition<TenantSize>("TotalPrimaryMbxCount", (TenantSize d) => d.TotalPrimaryMbxCount);

			// Token: 0x04000243 RID: 579
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> TotalPrimaryMbxSize = new ObjectLogSimplePropertyDefinition<TenantSize>("TotalPrimaryMbxSize", (TenantSize d) => d.TotalPrimaryMbxSize);

			// Token: 0x04000244 RID: 580
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> TotalArchiveMbxCount = new ObjectLogSimplePropertyDefinition<TenantSize>("TotalArchiveMbxCount", (TenantSize d) => d.TotalArchiveMbxCount);

			// Token: 0x04000245 RID: 581
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> TotalArchiveMbxSize = new ObjectLogSimplePropertyDefinition<TenantSize>("TotalArchiveMbxSize", (TenantSize d) => d.TotalArchiveMbxSize);

			// Token: 0x04000246 RID: 582
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> UploadedSize = new ObjectLogSimplePropertyDefinition<TenantSize>("UploadedSize", (TenantSize d) => d.UploadedSize);

			// Token: 0x04000247 RID: 583
			public static readonly ObjectLogSimplePropertyDefinition<TenantSize> ValidationError = new ObjectLogSimplePropertyDefinition<TenantSize>("ValidationError", (TenantSize d) => d.ValidationError);
		}

		// Token: 0x020000AB RID: 171
		private class TenantSizeLogConfiguration : ObjectLogConfiguration
		{
			// Token: 0x060004EE RID: 1262 RVA: 0x00008E71 File Offset: 0x00007071
			public TenantSizeLogConfiguration(string logFilePrefix)
			{
				this.logFilePrefix = logFilePrefix;
			}

			// Token: 0x170001EA RID: 490
			// (get) Token: 0x060004EF RID: 1263 RVA: 0x00008E80 File Offset: 0x00007080
			public override bool IsEnabled
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170001EB RID: 491
			// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00008E83 File Offset: 0x00007083
			public override TimeSpan MaxLogAge
			{
				get
				{
					return TimeSpan.FromDays(7.0);
				}
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00008E94 File Offset: 0x00007094
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
							return Path.Combine(path, "Logging\\TenantDataCollectorLogs");
						}
					}
					return null;
				}
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00008EF4 File Offset: 0x000070F4
			public override string LogComponentName
			{
				get
				{
					return "TenantDataCollector";
				}
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00008EFB File Offset: 0x000070FB
			public override string FilenamePrefix
			{
				get
				{
					return this.logFilePrefix;
				}
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00008F03 File Offset: 0x00007103
			public override long MaxLogDirSize
			{
				get
				{
					return 50000000L;
				}
			}

			// Token: 0x170001F0 RID: 496
			// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00008F0B File Offset: 0x0000710B
			public override long MaxLogFileSize
			{
				get
				{
					return 10000000L;
				}
			}

			// Token: 0x04000261 RID: 609
			private readonly string logFilePrefix;
		}
	}
}
