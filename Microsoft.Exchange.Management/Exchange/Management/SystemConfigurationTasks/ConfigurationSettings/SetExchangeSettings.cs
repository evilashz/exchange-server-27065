using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Cache;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.MailboxLoadBalance.Config;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Migration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks.ConfigurationSettings
{
	// Token: 0x02000940 RID: 2368
	[Cmdlet("Set", "ExchangeSettings", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "CreateSettingsGroup")]
	public sealed class SetExchangeSettings : SetTopologySystemConfigurationObjectTask<ExchangeSettingsIdParameter, ExchangeSettings>
	{
		// Token: 0x06005449 RID: 21577 RVA: 0x0015C324 File Offset: 0x0015A524
		static SetExchangeSettings()
		{
			SetExchangeSettings.AddRegisteredSchema(new MRSConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new ConfigurationADImpl.ADCacheConfigurationSchema());
			SetExchangeSettings.AddRegisteredSchema(new TenantRelocationConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new LoadBalanceADSettings());
			SetExchangeSettings.AddRegisteredSchema(new TenantDataCollectorConfig());
			SetExchangeSettings.AddRegisteredSchema(new UpgradeBatchCreatorConfig());
			SetExchangeSettings.AddRegisteredSchema(new UpgradeHandlerConfig());
			SetExchangeSettings.AddRegisteredSchema(new SlimTenantConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new TenantUpgradeConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new AdDriverConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new MRSRecurrentOperationConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new DirectoryTasksConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new ProvisioningTasksConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new PluggableDivergenceHandlerConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new MigrationServiceConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new ComplianceConfigSchema());
			SetExchangeSettings.AddRegisteredSchema(new OlcConfigSchema());
			SetExchangeSettings.SchemaAssemblyMap.Add("Store", new SetExchangeSettings.SchemaAssembly("Microsoft.Exchange.Server.Storage.Common.dll", "Microsoft.Exchange.Server.Storage.Common.StoreConfigSchema"));
			SetExchangeSettings.SchemaAssemblyMap.Add("MigrationMonitor", new SetExchangeSettings.SchemaAssembly("Microsoft.Exchange.MigrationMonitor.dll", "Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationMonitor+MigrationMonitorConfig"));
			SetExchangeSettings.SchemaAssemblyMap.Add("UpgradeInjector", new SetExchangeSettings.SchemaAssembly("Microsoft.Exchange.UpgradeInjector.dll", "Microsoft.Exchange.Servicelets.Upgrade.UpgradeInjector+UpgradeInjectorConfig"));
			SetExchangeSettings.SchemaAssemblyMap.Add("AuthAdmin", new SetExchangeSettings.SchemaAssembly("Microsoft.Exchange.AuthAdminServicelet.dll", "Microsoft.Exchange.Servicelets.AuthAdmin.AuthAdminContext+AuthAdminConfig"));
			SetExchangeSettings.SchemaAssemblyMap.Add("ServiceHost", new SetExchangeSettings.SchemaAssembly("Microsoft.Exchange.ServiceHost.exe", "Microsoft.Exchange.ServiceHost.ServiceHostConfigSchema"));
			SetExchangeSettings.SchemaAssemblyMap.Add("SlowMRSDetector", new SetExchangeSettings.SchemaAssembly("Microsoft.Exchange.MRS.SlowMRSDetector.dll", "Microsoft.Exchange.Servicelets.MRS.SlowMRSDetectorContext+SlowMRSDetectorConfig"));
			SetExchangeSettings.SchemaAssemblyMap.Add("DrumTesting", new SetExchangeSettings.SchemaAssembly("Microsoft.Exchange.DrumTesting.exe", "Microsoft.Exchange.DrumTesting.DrumConfigSchema"));
			SetExchangeSettings.SchemaAssemblyMap.Add("BatchCreator", new SetExchangeSettings.SchemaAssembly("MSExchangeMigrationWorkflow.exe", "Microsoft.Exchange.Servicelets.BatchCreator.BatchCreatorConfig"));
		}

		// Token: 0x17001924 RID: 6436
		// (get) Token: 0x0600544A RID: 21578 RVA: 0x0015C4E9 File Offset: 0x0015A6E9
		// (set) Token: 0x0600544B RID: 21579 RVA: 0x0015C50F File Offset: 0x0015A70F
		[Parameter(Mandatory = true, ParameterSetName = "CreateSettingsGroupGeneric")]
		[Parameter(Mandatory = true, ParameterSetName = "CreateSettingsGroup")]
		[Parameter(Mandatory = true, ParameterSetName = "CreateSettingsGroupAdvanced")]
		public SwitchParameter CreateSettingsGroup
		{
			get
			{
				return (SwitchParameter)(base.Fields["CreateSettingsGroup"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["CreateSettingsGroup"] = value;
			}
		}

		// Token: 0x17001925 RID: 6437
		// (get) Token: 0x0600544C RID: 21580 RVA: 0x0015C527 File Offset: 0x0015A727
		// (set) Token: 0x0600544D RID: 21581 RVA: 0x0015C54D File Offset: 0x0015A74D
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMultipleSettings")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSetting")]
		public SwitchParameter UpdateSetting
		{
			get
			{
				return (SwitchParameter)(base.Fields["UpdateSetting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UpdateSetting"] = value;
			}
		}

		// Token: 0x17001926 RID: 6438
		// (get) Token: 0x0600544E RID: 21582 RVA: 0x0015C565 File Offset: 0x0015A765
		// (set) Token: 0x0600544F RID: 21583 RVA: 0x0015C58B File Offset: 0x0015A78B
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSettingsGroup")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSettingsGroupAdvanced")]
		public SwitchParameter UpdateSettingsGroup
		{
			get
			{
				return (SwitchParameter)(base.Fields["UpdateSettingsGroup"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UpdateSettingsGroup"] = value;
			}
		}

		// Token: 0x17001927 RID: 6439
		// (get) Token: 0x06005450 RID: 21584 RVA: 0x0015C5A3 File Offset: 0x0015A7A3
		// (set) Token: 0x06005451 RID: 21585 RVA: 0x0015C5C9 File Offset: 0x0015A7C9
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMultipleSettings")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoveSetting")]
		public SwitchParameter RemoveSetting
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveSetting"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemoveSetting"] = value;
			}
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06005452 RID: 21586 RVA: 0x0015C5E1 File Offset: 0x0015A7E1
		// (set) Token: 0x06005453 RID: 21587 RVA: 0x0015C607 File Offset: 0x0015A807
		[Parameter(Mandatory = true, ParameterSetName = "RemoveSettingsGroup")]
		public SwitchParameter RemoveSettingsGroup
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveSettingsGroup"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemoveSettingsGroup"] = value;
			}
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06005454 RID: 21588 RVA: 0x0015C61F File Offset: 0x0015A81F
		// (set) Token: 0x06005455 RID: 21589 RVA: 0x0015C645 File Offset: 0x0015A845
		[Parameter(Mandatory = true, ParameterSetName = "AddScope")]
		public SwitchParameter AddScope
		{
			get
			{
				return (SwitchParameter)(base.Fields["AddScope"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["AddScope"] = value;
			}
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06005456 RID: 21590 RVA: 0x0015C65D File Offset: 0x0015A85D
		// (set) Token: 0x06005457 RID: 21591 RVA: 0x0015C683 File Offset: 0x0015A883
		[Parameter(Mandatory = true, ParameterSetName = "UpdateScope")]
		public SwitchParameter UpdateScope
		{
			get
			{
				return (SwitchParameter)(base.Fields["UpdateScope"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["UpdateScope"] = value;
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06005458 RID: 21592 RVA: 0x0015C69B File Offset: 0x0015A89B
		// (set) Token: 0x06005459 RID: 21593 RVA: 0x0015C6C1 File Offset: 0x0015A8C1
		[Parameter(Mandatory = true, ParameterSetName = "RemoveScope")]
		public SwitchParameter RemoveScope
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoveScope"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["RemoveScope"] = value;
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x0600545A RID: 21594 RVA: 0x0015C6D9 File Offset: 0x0015A8D9
		// (set) Token: 0x0600545B RID: 21595 RVA: 0x0015C6FF File Offset: 0x0015A8FF
		[Parameter(Mandatory = true, ParameterSetName = "ClearHistoryGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveSettingsGroup")]
		public SwitchParameter ClearHistory
		{
			get
			{
				return (SwitchParameter)(base.Fields["ClearHistory"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ClearHistory"] = value;
			}
		}

		// Token: 0x1700192D RID: 6445
		// (get) Token: 0x0600545C RID: 21596 RVA: 0x0015C717 File Offset: 0x0015A917
		// (set) Token: 0x0600545D RID: 21597 RVA: 0x0015C72E File Offset: 0x0015A92E
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public new ExchangeSettingsIdParameter Identity
		{
			get
			{
				return (ExchangeSettingsIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x1700192E RID: 6446
		// (get) Token: 0x0600545E RID: 21598 RVA: 0x0015C741 File Offset: 0x0015A941
		// (set) Token: 0x0600545F RID: 21599 RVA: 0x0015C761 File Offset: 0x0015A961
		[Parameter(Mandatory = false, ParameterSetName = "UpdateSetting")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateScope")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveScope")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateMultipleSettings")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveMultipleSettings")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveSetting")]
		[Parameter(Mandatory = false, ParameterSetName = "AddScope")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "ClearHistoryGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroupGeneric")]
		public string GroupName
		{
			get
			{
				return ((string)base.Fields["GroupName"]) ?? "default";
			}
			set
			{
				base.Fields["GroupName"] = value;
			}
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x0015C774 File Offset: 0x0015A974
		// (set) Token: 0x06005461 RID: 21601 RVA: 0x0015C7C3 File Offset: 0x0015A9C3
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroupGeneric")]
		[Parameter(Mandatory = false, ParameterSetName = "AddScope")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		public ExchangeSettingsScope Scope
		{
			get
			{
				if (base.Fields.Contains("Scope"))
				{
					return (ExchangeSettingsScope)base.Fields["Scope"];
				}
				if (base.Fields.Contains("GenericScopeName"))
				{
					return ExchangeSettingsScope.Generic;
				}
				return ExchangeSettingsScope.Forest;
			}
			set
			{
				base.Fields["Scope"] = value;
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x06005462 RID: 21602 RVA: 0x0015C7DB File Offset: 0x0015A9DB
		// (set) Token: 0x06005463 RID: 21603 RVA: 0x0015C7F2 File Offset: 0x0015A9F2
		[Parameter(Mandatory = false, ParameterSetName = "UpdateScope")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoveScope")]
		public Guid? ScopeId
		{
			get
			{
				return (Guid?)base.Fields["ScopeId"];
			}
			set
			{
				base.Fields["ScopeId"] = value;
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x0015C80A File Offset: 0x0015AA0A
		// (set) Token: 0x06005465 RID: 21605 RVA: 0x0015C82B File Offset: 0x0015AA2B
		[Parameter(Mandatory = false, ParameterSetName = "UpdateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroupGeneric")]
		public int Priority
		{
			get
			{
				return (int)(base.Fields["Priority"] ?? 0);
			}
			set
			{
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06005466 RID: 21606 RVA: 0x0015C843 File Offset: 0x0015AA43
		// (set) Token: 0x06005467 RID: 21607 RVA: 0x0015C85A File Offset: 0x0015AA5A
		[Parameter(Mandatory = false, ParameterSetName = "UpdateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroupGeneric")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		public DateTime? ExpirationDate
		{
			get
			{
				return (DateTime?)base.Fields["ExpirationDate"];
			}
			set
			{
				base.Fields["ExpirationDate"] = value;
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x06005468 RID: 21608 RVA: 0x0015C872 File Offset: 0x0015AA72
		// (set) Token: 0x06005469 RID: 21609 RVA: 0x0015C889 File Offset: 0x0015AA89
		[Parameter(Mandatory = false, ParameterSetName = "UpdateScope")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "AddScope")]
		public string MinVersion
		{
			get
			{
				return (string)base.Fields["MinVersion"];
			}
			set
			{
				base.Fields["MinVersion"] = value;
			}
		}

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x0600546A RID: 21610 RVA: 0x0015C89C File Offset: 0x0015AA9C
		// (set) Token: 0x0600546B RID: 21611 RVA: 0x0015C8B3 File Offset: 0x0015AAB3
		[Parameter(Mandatory = false, ParameterSetName = "AddScope")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateScope")]
		public string MaxVersion
		{
			get
			{
				return (string)base.Fields["MaxVersion"];
			}
			set
			{
				base.Fields["MaxVersion"] = value;
			}
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x0600546C RID: 21612 RVA: 0x0015C8C6 File Offset: 0x0015AAC6
		// (set) Token: 0x0600546D RID: 21613 RVA: 0x0015C8DD File Offset: 0x0015AADD
		[Parameter(Mandatory = false, ParameterSetName = "AddScope")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateScope")]
		public string NameMatch
		{
			get
			{
				return (string)base.Fields["NameMatch"];
			}
			set
			{
				base.Fields["NameMatch"] = value;
			}
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x0600546E RID: 21614 RVA: 0x0015C8F0 File Offset: 0x0015AAF0
		// (set) Token: 0x0600546F RID: 21615 RVA: 0x0015C907 File Offset: 0x0015AB07
		[Parameter(Mandatory = false, ParameterSetName = "UpdateScope")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "AddScope")]
		public Guid? GuidMatch
		{
			get
			{
				return (Guid?)base.Fields["GuidMatch"];
			}
			set
			{
				base.Fields["GuidMatch"] = value;
			}
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x06005470 RID: 21616 RVA: 0x0015C91F File Offset: 0x0015AB1F
		// (set) Token: 0x06005471 RID: 21617 RVA: 0x0015C936 File Offset: 0x0015AB36
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateScope")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroupGeneric")]
		[Parameter(Mandatory = false, ParameterSetName = "AddScope")]
		public string GenericScopeName
		{
			get
			{
				return (string)base.Fields["GenericScopeName"];
			}
			set
			{
				base.Fields["GenericScopeName"] = value;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x06005472 RID: 21618 RVA: 0x0015C949 File Offset: 0x0015AB49
		// (set) Token: 0x06005473 RID: 21619 RVA: 0x0015C960 File Offset: 0x0015AB60
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroupGeneric")]
		[Parameter(Mandatory = false, ParameterSetName = "UpdateScope")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false, ParameterSetName = "AddScope")]
		public string GenericScopeValue
		{
			get
			{
				return (string)base.Fields["GenericScopeValue"];
			}
			set
			{
				base.Fields["GenericScopeValue"] = value;
			}
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06005474 RID: 21620 RVA: 0x0015C973 File Offset: 0x0015AB73
		// (set) Token: 0x06005475 RID: 21621 RVA: 0x0015C98A File Offset: 0x0015AB8A
		[Parameter(Mandatory = false, ParameterSetName = "UpdateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		public string ScopeFilter
		{
			get
			{
				return (string)base.Fields["ScopeFilter"];
			}
			set
			{
				base.Fields["ScopeFilter"] = value;
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06005476 RID: 21622 RVA: 0x0015C99D File Offset: 0x0015AB9D
		// (set) Token: 0x06005477 RID: 21623 RVA: 0x0015C9B4 File Offset: 0x0015ABB4
		[Parameter(Mandatory = true, ParameterSetName = "RemoveSetting")]
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSetting")]
		public string ConfigName
		{
			get
			{
				return (string)base.Fields["ConfigName"];
			}
			set
			{
				base.Fields["ConfigName"] = value;
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06005478 RID: 21624 RVA: 0x0015C9C7 File Offset: 0x0015ABC7
		// (set) Token: 0x06005479 RID: 21625 RVA: 0x0015C9DE File Offset: 0x0015ABDE
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSetting")]
		public string ConfigValue
		{
			get
			{
				return (string)base.Fields["ConfigValue"];
			}
			set
			{
				base.Fields["ConfigValue"] = value;
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x0600547A RID: 21626 RVA: 0x0015C9F1 File Offset: 0x0015ABF1
		// (set) Token: 0x0600547B RID: 21627 RVA: 0x0015CA08 File Offset: 0x0015AC08
		[Parameter(Mandatory = true, ParameterSetName = "RemoveMultipleSettings")]
		[Parameter(Mandatory = true, ParameterSetName = "UpdateMultipleSettings")]
		public string[] ConfigPairs
		{
			get
			{
				return (string[])base.Fields["ConfigPairs"];
			}
			set
			{
				base.Fields["ConfigPairs"] = value;
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x0600547C RID: 21628 RVA: 0x0015CA1B File Offset: 0x0015AC1B
		// (set) Token: 0x0600547D RID: 21629 RVA: 0x0015CA32 File Offset: 0x0015AC32
		[Parameter(Mandatory = false, ParameterSetName = "EnableSettingsGroup")]
		public string EnableGroup
		{
			get
			{
				return (string)base.Fields["EnableGroup"];
			}
			set
			{
				base.Fields["EnableGroup"] = value;
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x0600547E RID: 21630 RVA: 0x0015CA45 File Offset: 0x0015AC45
		// (set) Token: 0x0600547F RID: 21631 RVA: 0x0015CA5C File Offset: 0x0015AC5C
		[Parameter(Mandatory = false, ParameterSetName = "EnableSettingsGroup")]
		public string DisableGroup
		{
			get
			{
				return (string)base.Fields["DisableGroup"];
			}
			set
			{
				base.Fields["DisableGroup"] = value;
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x06005480 RID: 21632 RVA: 0x0015CA6F File Offset: 0x0015AC6F
		// (set) Token: 0x06005481 RID: 21633 RVA: 0x0015CA95 File Offset: 0x0015AC95
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x06005482 RID: 21634 RVA: 0x0015CAAD File Offset: 0x0015ACAD
		// (set) Token: 0x06005483 RID: 21635 RVA: 0x0015CAD3 File Offset: 0x0015ACD3
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroup")]
		[Parameter(Mandatory = false, ParameterSetName = "CreateSettingsGroupGeneric")]
		public SwitchParameter Disable
		{
			get
			{
				return (SwitchParameter)(base.Fields["Disable"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Disable"] = value;
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x06005484 RID: 21636 RVA: 0x0015CAEB File Offset: 0x0015ACEB
		// (set) Token: 0x06005485 RID: 21637 RVA: 0x0015CB02 File Offset: 0x0015AD02
		[Parameter(Mandatory = true, ParameterSetName = "UpdateSettingsGroupAdvanced")]
		[Parameter(Mandatory = true, ParameterSetName = "CreateSettingsGroupAdvanced")]
		public string SettingsGroup
		{
			get
			{
				return (string)base.Fields["SettingsGroup"];
			}
			set
			{
				base.Fields["SettingsGroup"] = value;
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06005486 RID: 21638 RVA: 0x0015CB15 File Offset: 0x0015AD15
		// (set) Token: 0x06005487 RID: 21639 RVA: 0x0015CB2C File Offset: 0x0015AD2C
		[Parameter(Mandatory = true)]
		[ValidateNotNullOrEmpty]
		public string Reason
		{
			get
			{
				return (string)base.Fields["Reason"];
			}
			set
			{
				base.Fields["Reason"] = value;
			}
		}

		// Token: 0x17001943 RID: 6467
		// (get) Token: 0x06005488 RID: 21640 RVA: 0x0015CB3F File Offset: 0x0015AD3F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetExchangeSettings(this.DataObject.Name);
			}
		}

		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06005489 RID: 21641 RVA: 0x0015CB51 File Offset: 0x0015AD51
		// (set) Token: 0x0600548A RID: 21642 RVA: 0x0015CB59 File Offset: 0x0015AD59
		private SettingsGroup SelectedSettingsGroup { get; set; }

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x0600548B RID: 21643 RVA: 0x0015CB62 File Offset: 0x0015AD62
		// (set) Token: 0x0600548C RID: 21644 RVA: 0x0015CB6A File Offset: 0x0015AD6A
		private List<KeyValuePair<string, string>> ModifiedSettings { get; set; }

		// Token: 0x0600548D RID: 21645 RVA: 0x0015CB73 File Offset: 0x0015AD73
		internal static bool IsSchemaRegistered(string identity)
		{
			return SetExchangeSettings.RegisteredSchemas.ContainsKey(identity) || SetExchangeSettings.SchemaAssemblyMap.ContainsKey(identity);
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x0015CB90 File Offset: 0x0015AD90
		internal static ConfigSchemaBase GetRegisteredSchema(string identity, bool force, Task.TaskVerboseLoggingDelegate writeVerbose, Task.TaskErrorLoggingDelegate writeError)
		{
			ConfigSchemaBase configSchemaBase = null;
			if (force || SetExchangeSettings.RegisteredSchemas.TryGetValue(identity, out configSchemaBase))
			{
				return configSchemaBase;
			}
			SetExchangeSettings.SchemaAssembly schemaAssembly;
			if (!SetExchangeSettings.SchemaAssemblyMap.TryGetValue(identity, out schemaAssembly))
			{
				writeError(new ExchangeSettingsInvalidSchemaException(identity), ErrorCategory.InvalidOperation, null);
			}
			string text = Path.Combine(ConfigurationContext.Setup.InstallPath, "bin", schemaAssembly.ModuleName);
			writeVerbose(new LocalizedString(string.Format("Attempting to load schema for {0} from {1}", identity, text)));
			try
			{
				Assembly assembly = Assembly.LoadFrom(text);
				configSchemaBase = (ConfigSchemaBase)assembly.CreateInstance(schemaAssembly.TypeName, true, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null, null);
				if (configSchemaBase == null)
				{
					writeVerbose(new LocalizedString(string.Format("Assembly {0} found but type {1} could not get loaded", text, schemaAssembly.TypeName)));
					writeError(new ExchangeSettingsInvalidSchemaException(identity), ErrorCategory.InvalidOperation, null);
				}
				if (string.Compare(configSchemaBase.Name, identity, StringComparison.InvariantCulture) != 0)
				{
					writeVerbose(new LocalizedString(string.Format("identity used {0} does not match identity found on schema {1}", identity, configSchemaBase.Name)));
					writeError(new ExchangeSettingsInvalidSchemaException(identity), ErrorCategory.InvalidOperation, null);
				}
				SetExchangeSettings.AddRegisteredSchema(configSchemaBase);
				return configSchemaBase;
			}
			catch (FileNotFoundException)
			{
				writeError(new ExchangeSettingsAssemblyNotFoundException(identity, text, schemaAssembly.TypeName), ErrorCategory.InvalidOperation, null);
			}
			return null;
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x0015CCBC File Offset: 0x0015AEBC
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(ConfigurationSettingsException).IsInstanceOfType(exception);
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x0015CCDC File Offset: 0x0015AEDC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.GuidMatch != null && this.NameMatch != null)
			{
				base.WriteError(new RecipientTaskException(Strings.ExchangeSettingsGuidUsage), ExchangeErrorCategory.Client, this.DataObject);
			}
			this.PrivateValidate(SetExchangeSettings.GetRegisteredSchema(this.Identity.ToString(), this.Force, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskErrorLoggingDelegate(base.WriteError)));
			TaskLogger.LogExit();
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0015CD68 File Offset: 0x0015AF68
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.PrivateProcessRecord();
			int num = 102400;
			if (InternalExchangeSettingsSchema.ConfigurationXMLRaw.AllConstraints != null)
			{
				foreach (PropertyDefinitionConstraint propertyDefinitionConstraint in InternalExchangeSettingsSchema.ConfigurationXMLRaw.AllConstraints)
				{
					StringLengthConstraint stringLengthConstraint = propertyDefinitionConstraint as StringLengthConstraint;
					if (stringLengthConstraint != null)
					{
						num = stringLengthConstraint.MaxLength;
						break;
					}
				}
			}
			int length = this.DataObject.Xml.Serialize(false).Length;
			int num2 = num * 9 / 10;
			if (length >= num2)
			{
				this.WriteWarning(Strings.ExchangeSettingsWarningMaximumSize(length, num));
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0015CE28 File Offset: 0x0015B028
		private static void AddRegisteredSchema(ConfigSchemaBase schema)
		{
			SetExchangeSettings.RegisteredSchemas.Add(schema.Name, schema);
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x0015CEA4 File Offset: 0x0015B0A4
		private void PrivateValidate(ConfigSchemaBase schema)
		{
			if ("EnableSettingsGroup" == base.ParameterSetName)
			{
				HashSet<string> hashSet = new HashSet<string>();
				foreach (string text in new string[]
				{
					this.EnableGroup,
					this.DisableGroup
				})
				{
					if (!string.IsNullOrEmpty(text))
					{
						if (!this.DataObject.Settings.ContainsKey(text))
						{
							base.WriteError(new ExchangeSettingsGroupNotFoundException(text), ExchangeErrorCategory.Client, this.DataObject);
						}
						if (hashSet.Contains(text))
						{
							base.WriteError(new ExchangeSettingsGroupFoundMultipleTimesException(text), ExchangeErrorCategory.Client, this.DataObject);
						}
						hashSet.Add(text);
					}
				}
				if (hashSet.Count <= 0)
				{
					base.WriteError(new RecipientTaskException(Strings.ExchangeSettingsEnableUsage), ExchangeErrorCategory.Client, this.DataObject);
				}
				return;
			}
			if ("CreateSettingsGroupAdvanced" == base.ParameterSetName || "UpdateSettingsGroupAdvanced" == base.ParameterSetName)
			{
				this.SelectedSettingsGroup = XMLSerializableBase.Deserialize<SettingsGroup>(this.SettingsGroup, InternalExchangeSettingsSchema.ConfigurationXMLRaw);
				this.ValidateGroupName(this.SelectedSettingsGroup.Name, "CreateSettingsGroupAdvanced" == base.ParameterSetName);
				this.ValidatePriority(this.SelectedSettingsGroup.Priority);
				this.ValidateSettingsGroup(schema);
				return;
			}
			bool flag = "CreateSettingsGroup" == base.ParameterSetName || "CreateSettingsGroupGeneric" == base.ParameterSetName;
			this.ValidateGroupName(this.GroupName, flag);
			if (flag)
			{
				this.SelectedSettingsGroup = this.CreateNewSettingsGroup();
				if (!this.Disable)
				{
					this.SelectedSettingsGroup.Enabled = true;
				}
			}
			else if (this.GroupName == "default" && !this.DataObject.Settings.ContainsKey(this.GroupName) && ("UpdateSetting" == base.ParameterSetName || "UpdateMultipleSettings" == base.ParameterSetName))
			{
				base.WriteVerbose(new LocalizedString(string.Format("Creating default group for new settings", new object[0])));
				this.SelectedSettingsGroup = this.CreateNewSettingsGroup();
				this.SelectedSettingsGroup.Enabled = true;
				this.DataObject.AddSettingsGroup(this.SelectedSettingsGroup);
			}
			else if (!this.ClearHistory || this.IsFieldSet("GroupName"))
			{
				this.SelectedSettingsGroup = this.DataObject.GetSettingsGroupForModification(this.GroupName);
			}
			if (this.IsFieldSet("Priority"))
			{
				this.ValidatePriority(this.Priority);
				this.SelectedSettingsGroup.Priority = this.Priority;
			}
			else if (flag)
			{
				this.ValidatePriority(this.SelectedSettingsGroup.Priority);
			}
			if ("UpdateSettingsGroup" == base.ParameterSetName)
			{
				if (this.IsFieldSet("ScopeFilter"))
				{
					if (!this.SelectedSettingsGroup.HasExplicitScopeFilter && (this.SelectedSettingsGroup.Scopes.Count != 1 || !(this.SelectedSettingsGroup.Scopes[0] is SettingsForestScope)))
					{
						base.WriteError(new ExchangeSettingsCannotChangeScopeFilterOnDownlevelGroupException(this.GroupName), ExchangeErrorCategory.Client, this.DataObject);
					}
					this.SelectedSettingsGroup.ScopeFilter = this.ScopeFilter;
				}
				if (this.IsFieldSet("ExpirationDate"))
				{
					if (this.ExpirationDate != null && this.ExpirationDate.Value < DateTime.UtcNow)
					{
						this.WriteWarning(Strings.ExchangeSettingsExpirationDateIsInThePastWarning(this.ExpirationDate.Value.ToString()));
					}
					this.SelectedSettingsGroup.ExpirationDate = (this.ExpirationDate ?? DateTime.MinValue);
				}
			}
			if (this.ScopeId == null && ("RemoveScope" == base.ParameterSetName || "UpdateScope" == base.ParameterSetName))
			{
				if (this.SelectedSettingsGroup.Scopes.Count != 1)
				{
					base.WriteError(new ExchangeSettingsDefaultScopeNotFoundException(this.GroupName), ExchangeErrorCategory.Client, this.DataObject);
				}
				this.ScopeId = new Guid?(this.SelectedSettingsGroup.Scopes[0].ScopeId);
				base.WriteVerbose(new LocalizedString(string.Format("Using default scope {0}", this.ScopeId)));
			}
			if ("RemoveScope" == base.ParameterSetName)
			{
				if (this.SelectedSettingsGroup.HasExplicitScopeFilter)
				{
					base.WriteError(new ExchangeSettingsCannotChangeScopeOnScopeFilteredGroupException(this.GroupName), ExchangeErrorCategory.Client, this.DataObject);
				}
				int num = this.SelectedSettingsGroup.Scopes.RemoveAll((SettingsScope x) => x.ScopeId == this.ScopeId);
				if (num <= 0)
				{
					base.WriteError(new ExchangeSettingsScopeNotFoundException(this.GroupName, this.ScopeId.ToString()), ExchangeErrorCategory.Client, this.DataObject);
				}
				if (this.SelectedSettingsGroup.Scopes.Count <= 0)
				{
					this.SelectedSettingsGroup.Scopes.Add(new SettingsForestScope());
				}
			}
			else if ("AddScope" == base.ParameterSetName)
			{
				if (this.SelectedSettingsGroup.HasExplicitScopeFilter)
				{
					base.WriteError(new ExchangeSettingsCannotChangeScopeOnScopeFilteredGroupException(this.GroupName), ExchangeErrorCategory.Client, this.DataObject);
				}
				this.SelectedSettingsGroup.Scopes.Add(this.CreateDownlevelScope());
			}
			else if ("UpdateScope" == base.ParameterSetName)
			{
				if (this.SelectedSettingsGroup.HasExplicitScopeFilter)
				{
					base.WriteError(new ExchangeSettingsCannotChangeScopeOnScopeFilteredGroupException(this.GroupName), ExchangeErrorCategory.Client, this.DataObject);
				}
				SettingsScope settingsScope = this.SelectedSettingsGroup.Scopes.Find((SettingsScope x) => x.ScopeId == this.ScopeId);
				if (settingsScope == null)
				{
					base.WriteError(new ExchangeSettingsScopeNotFoundException(this.GroupName, this.ScopeId.ToString()), ExchangeErrorCategory.Client, this.DataObject);
				}
				this.WriteWarning(new LocalizedString("The use of Scopes is deprecated, use ScopeFilter instead."));
				if (settingsScope is SettingsForestScope)
				{
					base.WriteError(new ExchangeSettingsUpdateScopeForestException(this.GroupName, this.ScopeId.ToString()), ExchangeErrorCategory.Client, this.DataObject);
				}
				else if (settingsScope is SettingsGenericScope)
				{
					if (this.IsFieldSet("GenericScopeName"))
					{
						settingsScope.Restriction.SubType = this.GenericScopeName;
					}
					if (this.IsFieldSet("GenericScopeValue"))
					{
						settingsScope.Restriction.NameMatch = this.GenericScopeValue;
					}
				}
				else
				{
					if (this.IsFieldSet("NameMatch"))
					{
						settingsScope.Restriction.NameMatch = this.NameMatch;
					}
					if (this.IsFieldSet("GuidMatch") && this.GuidMatch != null)
					{
						settingsScope.Restriction.NameMatch = this.GuidMatch.ToString();
					}
					if (this.IsFieldSet("MinVersion"))
					{
						settingsScope.Restriction.MinVersion = this.MinVersion;
					}
					if (this.IsFieldSet("MaxVersion"))
					{
						settingsScope.Restriction.MaxVersion = this.MaxVersion;
					}
				}
			}
			if ("UpdateSettingsGroup" == base.ParameterSetName || "CreateSettingsGroup" == base.ParameterSetName || "AddScope" == base.ParameterSetName || "UpdateScope" == base.ParameterSetName || "CreateSettingsGroupGeneric" == base.ParameterSetName || "RemoveScope" == base.ParameterSetName)
			{
				this.ValidateSettingsGroup(schema);
			}
			if ("UpdateSetting" == base.ParameterSetName || "RemoveSetting" == base.ParameterSetName)
			{
				this.ModifiedSettings = new List<KeyValuePair<string, string>>(1);
				this.ModifiedSettings.Add(new KeyValuePair<string, string>(this.ConfigName, this.ConfigValue));
			}
			else if ("UpdateMultipleSettings" == base.ParameterSetName || "RemoveMultipleSettings" == base.ParameterSetName)
			{
				this.ModifiedSettings = new List<KeyValuePair<string, string>>(this.ConfigPairs.Length);
				foreach (string text2 in this.ConfigPairs)
				{
					string key = text2;
					string value = null;
					int num2 = text2.IndexOf('=');
					if (num2 < 0)
					{
						if ("UpdateMultipleSettings" == base.ParameterSetName)
						{
							base.WriteError(new ExchangeSettingsBadFormatOfConfigPairException(text2), ExchangeErrorCategory.Client, this.DataObject);
						}
					}
					else
					{
						key = text2.Substring(0, num2);
						value = text2.Substring(num2 + 1);
					}
					this.ModifiedSettings.Add(new KeyValuePair<string, string>(key, value));
				}
			}
			if (("UpdateSetting" == base.ParameterSetName || "UpdateMultipleSettings" == base.ParameterSetName) && !this.Force)
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.ModifiedSettings)
				{
					try
					{
						schema.ParseAndValidateConfigValue(keyValuePair.Key, keyValuePair.Value, null);
					}
					catch (ConfigurationSettingsException exception)
					{
						base.WriteError(exception, ExchangeErrorCategory.Client, this.DataObject);
					}
				}
			}
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x0015D858 File Offset: 0x0015BA58
		private bool PrivateProcessRecord()
		{
			this.SelectedSettingsGroup.Reason = this.Reason;
			this.SelectedSettingsGroup.UpdatedBy = base.ExecutingUserIdentityName;
			if ("EnableSettingsGroup" == base.ParameterSetName)
			{
				if (!string.IsNullOrEmpty(this.EnableGroup))
				{
					SettingsGroup settingsGroupForModification = this.DataObject.GetSettingsGroupForModification(this.EnableGroup);
					base.WriteVerbose(new LocalizedString(string.Format("Enabling group {0} from {1}", this.EnableGroup, settingsGroupForModification.Enabled)));
					settingsGroupForModification.Enabled = true;
					this.DataObject.UpdateSettingsGroup(settingsGroupForModification);
				}
				if (!string.IsNullOrEmpty(this.DisableGroup))
				{
					SettingsGroup settingsGroupForModification2 = this.DataObject.GetSettingsGroupForModification(this.DisableGroup);
					base.WriteVerbose(new LocalizedString(string.Format("Disabling group {0} from {1}", this.DisableGroup, settingsGroupForModification2.Enabled)));
					settingsGroupForModification2.Enabled = false;
					this.DataObject.UpdateSettingsGroup(settingsGroupForModification2);
				}
				return true;
			}
			if (this.ClearHistory && !this.IsFieldSet("GroupName"))
			{
				base.WriteVerbose(new LocalizedString(string.Format("clearing history for all groups", new object[0])));
				foreach (string name in this.DataObject.GroupNames)
				{
					this.DataObject.ClearHistorySettings(name);
				}
				return true;
			}
			if (this.CreateSettingsGroup)
			{
				base.WriteVerbose(new LocalizedString(string.Format("Creating group settings {0}", this.GroupName)));
				this.DataObject.AddSettingsGroup(this.SelectedSettingsGroup);
				return true;
			}
			if (this.RemoveSettingsGroup)
			{
				base.WriteVerbose(new LocalizedString(string.Format("Removing group settings {0}", this.GroupName)));
				this.DataObject.RemoveSettingsGroup(this.SelectedSettingsGroup, !this.ClearHistory);
				if (this.ClearHistory)
				{
					this.DataObject.ClearHistorySettings(this.GroupName);
				}
				return true;
			}
			if (this.UpdateSettingsGroup || this.AddScope || this.RemoveScope || this.UpdateScope)
			{
				base.WriteVerbose(new LocalizedString(string.Format("Updating group settings {0}", this.GroupName)));
				this.DataObject.UpdateSettingsGroup(this.SelectedSettingsGroup);
				return true;
			}
			if (this.ClearHistory)
			{
				base.WriteVerbose(new LocalizedString(string.Format("clearing group history for {0}", this.GroupName)));
				this.DataObject.ClearHistorySettings(this.GroupName);
				return true;
			}
			if (this.UpdateSetting || this.RemoveSetting)
			{
				bool flag = false;
				foreach (KeyValuePair<string, string> keyValuePair in this.ModifiedSettings)
				{
					if (this.UpdateSetting)
					{
						string text;
						if (this.SelectedSettingsGroup.TryGetValue(keyValuePair.Key, out text) && string.Equals(text, keyValuePair.Value, StringComparison.InvariantCulture))
						{
							this.WriteWarning(Strings.ExchangeSettingsExistingSettingNotUpdated(keyValuePair.Key, keyValuePair.Value, this.GroupName));
						}
						else
						{
							base.WriteVerbose(new LocalizedString(string.Format("Updating setting {0} from {1} to {2} for group {3}", new object[]
							{
								keyValuePair.Key,
								text,
								keyValuePair.Value,
								this.GroupName
							})));
							this.SelectedSettingsGroup[keyValuePair.Key] = keyValuePair.Value;
							flag = true;
						}
					}
					else if (!this.SelectedSettingsGroup.ContainsKey(keyValuePair.Key))
					{
						this.WriteWarning(Strings.ExchangeSettingsNonExistingSettingNotRemoved(keyValuePair.Key, this.GroupName));
					}
					else
					{
						base.WriteVerbose(new LocalizedString(string.Format("removing setting {0} for group {1}", keyValuePair.Key, this.GroupName)));
						this.SelectedSettingsGroup.Remove(keyValuePair.Key);
						flag = true;
					}
				}
				if (flag)
				{
					this.DataObject.UpdateSettingsGroup(this.SelectedSettingsGroup);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x0015DCC8 File Offset: 0x0015BEC8
		private void ValidateGroupName(string groupName, bool isNew)
		{
			if (this.DataObject.Settings.ContainsKey(groupName))
			{
				if (isNew)
				{
					base.WriteError(new ExchangeSettingsGroupAlreadyExistsException(groupName), ExchangeErrorCategory.Client, this.DataObject);
					return;
				}
			}
			else if (!isNew && groupName != "default")
			{
				base.WriteError(new ExchangeSettingsGroupNotFoundException(groupName), ExchangeErrorCategory.Client, this.DataObject);
			}
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x0015DD2C File Offset: 0x0015BF2C
		private void ValidatePriority(int priority)
		{
			if (!this.DataObject.IsPriorityInUse(priority, this.SelectedSettingsGroup.Name))
			{
				base.WriteError(new ExchangeSettingsPriorityIsNotUniqueException(this.SelectedSettingsGroup.Name, priority), ExchangeErrorCategory.Client, this.DataObject);
			}
			if (priority < 100)
			{
				this.WriteWarning(new LocalizedString(string.Format("The priority '{0}' is less than the default", priority)));
			}
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x0015DD94 File Offset: 0x0015BF94
		private void ValidateSettingsGroup(ConfigSchemaBase schema)
		{
			this.SelectedSettingsGroup.Validate(schema, new QueryParser.EvaluateVariableDelegate(base.GetVariableValue));
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x0015DDB0 File Offset: 0x0015BFB0
		private SettingsGroup CreateNewSettingsGroup()
		{
			SettingsGroup settingsGroup;
			if (string.IsNullOrEmpty(this.ScopeFilter) && this.Scope != ExchangeSettingsScope.Forest)
			{
				settingsGroup = new SettingsGroup(this.GroupName, this.CreateDownlevelScope());
			}
			else
			{
				if (this.IsFieldSet("Scope") && this.Scope != ExchangeSettingsScope.Forest)
				{
					base.WriteError(new ExchangeSettingsCannotChangeScopeOnScopeFilteredGroupException(this.GroupName), ExchangeErrorCategory.Client, this.DataObject);
				}
				int priority = this.Priority;
				if (!this.IsFieldSet("Priority"))
				{
					int num = -1;
					foreach (SettingsGroup settingsGroup2 in this.DataObject.Settings.Values)
					{
						if (settingsGroup2.Priority > num)
						{
							num = settingsGroup2.Priority;
						}
					}
					if (num == -1)
					{
						priority = 100;
					}
					else
					{
						priority = num + 10;
					}
				}
				settingsGroup = new SettingsGroup(this.GroupName, this.ScopeFilter, priority);
			}
			if (this.IsFieldSet("ExpirationDate"))
			{
				if (this.ExpirationDate != null && this.ExpirationDate.Value < DateTime.UtcNow)
				{
					this.WriteWarning(Strings.ExchangeSettingsExpirationDateIsInThePastWarning(this.ExpirationDate.Value.ToString()));
				}
				settingsGroup.ExpirationDate = (this.ExpirationDate ?? DateTime.MinValue);
			}
			return settingsGroup;
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x0015DF38 File Offset: 0x0015C138
		private SettingsScope CreateDownlevelScope()
		{
			this.WriteWarning(new LocalizedString("The use of Scopes is deprecated, use ScopeFilter instead."));
			ExchangeSettingsScope scope = this.Scope;
			if (scope <= ExchangeSettingsScope.Process)
			{
				if (scope <= ExchangeSettingsScope.Dag)
				{
					if (scope == ExchangeSettingsScope.Forest)
					{
						return new SettingsForestScope();
					}
					if (scope == ExchangeSettingsScope.Dag)
					{
						return new SettingsDagScope(this.GuidMatch);
					}
				}
				else if (scope != ExchangeSettingsScope.Server)
				{
					if (scope == ExchangeSettingsScope.Process)
					{
						return new SettingsProcessScope(this.NameMatch);
					}
				}
				else
				{
					if (this.GuidMatch != null)
					{
						return new SettingsServerScope(this.GuidMatch);
					}
					return new SettingsServerScope(this.NameMatch, this.MinVersion, this.MaxVersion);
				}
			}
			else if (scope <= ExchangeSettingsScope.Organization)
			{
				if (scope != ExchangeSettingsScope.Database)
				{
					if (scope == ExchangeSettingsScope.Organization)
					{
						return new SettingsOrganizationScope(this.NameMatch, this.MinVersion, this.MaxVersion);
					}
				}
				else
				{
					if (this.GuidMatch != null)
					{
						return new SettingsDatabaseScope(this.GuidMatch);
					}
					return new SettingsDatabaseScope(this.NameMatch, this.MinVersion, this.MaxVersion);
				}
			}
			else
			{
				if (scope == ExchangeSettingsScope.User)
				{
					return new SettingsUserScope(this.GuidMatch);
				}
				if (scope == ExchangeSettingsScope.Generic)
				{
					return new SettingsGenericScope(this.GenericScopeName, this.GenericScopeValue);
				}
			}
			throw new InvalidOperationException(string.Format("no support for scope {0}", this.Scope));
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0015E0A7 File Offset: 0x0015C2A7
		private bool IsFieldSet(string fieldName)
		{
			return base.Fields.IsChanged(fieldName) || base.Fields.IsModified(fieldName);
		}

		// Token: 0x04003114 RID: 12564
		private const int XmlMaxSizeDefault = 102400;

		// Token: 0x04003115 RID: 12565
		private const string ParameterSetCreateSettingsGroup = "CreateSettingsGroup";

		// Token: 0x04003116 RID: 12566
		private const string ParameterSetCreateSettingsGroupAdvanced = "CreateSettingsGroupAdvanced";

		// Token: 0x04003117 RID: 12567
		private const string ParameterSetRemoveSettingsGroup = "RemoveSettingsGroup";

		// Token: 0x04003118 RID: 12568
		private const string ParameterSetUpdateSettingsGroup = "UpdateSettingsGroup";

		// Token: 0x04003119 RID: 12569
		private const string ParameterSetUpdateSettingsGroupAdvanced = "UpdateSettingsGroupAdvanced";

		// Token: 0x0400311A RID: 12570
		private const string ParameterSetClearHistoryGroup = "ClearHistoryGroup";

		// Token: 0x0400311B RID: 12571
		private const string ParameterSetEnableSettingsGroup = "EnableSettingsGroup";

		// Token: 0x0400311C RID: 12572
		private const string ParameterSetUpdateSetting = "UpdateSetting";

		// Token: 0x0400311D RID: 12573
		private const string ParameterSetUpdateMultipleSettings = "UpdateMultipleSettings";

		// Token: 0x0400311E RID: 12574
		private const string ParameterSetRemoveMultipleSettings = "RemoveMultipleSettings";

		// Token: 0x0400311F RID: 12575
		private const string ParameterSetRemoveSetting = "RemoveSetting";

		// Token: 0x04003120 RID: 12576
		private const string ParameterSetRemoveScope = "RemoveScope";

		// Token: 0x04003121 RID: 12577
		private const string ParameterSetAddScope = "AddScope";

		// Token: 0x04003122 RID: 12578
		private const string ParameterSetUpdateScope = "UpdateScope";

		// Token: 0x04003123 RID: 12579
		private const string ParameterSetCreateSettingsGroupGeneric = "CreateSettingsGroupGeneric";

		// Token: 0x04003124 RID: 12580
		private const string ParameterGroupName = "GroupName";

		// Token: 0x04003125 RID: 12581
		private const string ParameterScope = "Scope";

		// Token: 0x04003126 RID: 12582
		private const string ParameterPriority = "Priority";

		// Token: 0x04003127 RID: 12583
		private const string ParameterExpirationDate = "ExpirationDate";

		// Token: 0x04003128 RID: 12584
		private const string ParameterNameMatch = "NameMatch";

		// Token: 0x04003129 RID: 12585
		private const string ParameterGuidMatch = "GuidMatch";

		// Token: 0x0400312A RID: 12586
		private const string ParameterMinVersion = "MinVersion";

		// Token: 0x0400312B RID: 12587
		private const string ParameterMaxVersion = "MaxVersion";

		// Token: 0x0400312C RID: 12588
		private const string ParameterGenericScopeName = "GenericScopeName";

		// Token: 0x0400312D RID: 12589
		private const string ParameterGenericScopeValue = "GenericScopeValue";

		// Token: 0x0400312E RID: 12590
		private const string ParameterScopeFilter = "ScopeFilter";

		// Token: 0x0400312F RID: 12591
		private const string ParameterSettingsGroup = "SettingsGroup";

		// Token: 0x04003130 RID: 12592
		private const string DefaultGroupName = "default";

		// Token: 0x04003131 RID: 12593
		private const ExchangeSettingsScope DefaultScope = ExchangeSettingsScope.Forest;

		// Token: 0x04003132 RID: 12594
		private static readonly IDictionary<string, ConfigSchemaBase> RegisteredSchemas = new ConcurrentDictionary<string, ConfigSchemaBase>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04003133 RID: 12595
		private static readonly IDictionary<string, SetExchangeSettings.SchemaAssembly> SchemaAssemblyMap = new ConcurrentDictionary<string, SetExchangeSettings.SchemaAssembly>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x02000941 RID: 2369
		private sealed class SchemaAssembly
		{
			// Token: 0x0600549E RID: 21662 RVA: 0x0015E0CD File Offset: 0x0015C2CD
			public SchemaAssembly(string moduleName, string typeName)
			{
				this.ModuleName = moduleName;
				this.TypeName = typeName;
			}

			// Token: 0x17001946 RID: 6470
			// (get) Token: 0x0600549F RID: 21663 RVA: 0x0015E0E3 File Offset: 0x0015C2E3
			// (set) Token: 0x060054A0 RID: 21664 RVA: 0x0015E0EB File Offset: 0x0015C2EB
			public string ModuleName { get; private set; }

			// Token: 0x17001947 RID: 6471
			// (get) Token: 0x060054A1 RID: 21665 RVA: 0x0015E0F4 File Offset: 0x0015C2F4
			// (set) Token: 0x060054A2 RID: 21666 RVA: 0x0015E0FC File Offset: 0x0015C2FC
			public string TypeName { get; private set; }
		}
	}
}
