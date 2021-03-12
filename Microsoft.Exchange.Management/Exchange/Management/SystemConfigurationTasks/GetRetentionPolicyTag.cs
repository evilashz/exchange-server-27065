using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000316 RID: 790
	[Cmdlet("Get", "RetentionPolicyTag", DefaultParameterSetName = "Identity")]
	public sealed class GetRetentionPolicyTag : GetMultitenancySystemConfigurationObjectTask<RetentionPolicyTagIdParameter, RetentionPolicyTag>
	{
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001A86 RID: 6790 RVA: 0x000757BE File Offset: 0x000739BE
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x000757C1 File Offset: 0x000739C1
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x000757C9 File Offset: 0x000739C9
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x000757D2 File Offset: 0x000739D2
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x000757E4 File Offset: 0x000739E4
		// (set) Token: 0x06001A8B RID: 6795 RVA: 0x0007580A File Offset: 0x00073A0A
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSystemTags
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeSystemTags"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeSystemTags"] = value;
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001A8C RID: 6796 RVA: 0x00075822 File Offset: 0x00073A22
		// (set) Token: 0x06001A8D RID: 6797 RVA: 0x00075839 File Offset: 0x00073A39
		[Parameter(Mandatory = false, ParameterSetName = "ParameterSetMailboxTask")]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001A8E RID: 6798 RVA: 0x0007584C File Offset: 0x00073A4C
		// (set) Token: 0x06001A8F RID: 6799 RVA: 0x00075872 File Offset: 0x00073A72
		[Parameter(Mandatory = false, ParameterSetName = "ParameterSetMailboxTask")]
		public SwitchParameter OptionalInMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["OptionalInMailbox"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["OptionalInMailbox"] = value;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001A90 RID: 6800 RVA: 0x0007588A File Offset: 0x00073A8A
		// (set) Token: 0x06001A91 RID: 6801 RVA: 0x000758A1 File Offset: 0x00073AA1
		[Parameter(Mandatory = false)]
		public ElcFolderType[] Types
		{
			get
			{
				return (ElcFolderType[])base.Fields["Types"];
			}
			set
			{
				base.Fields["Types"] = value;
			}
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000758B4 File Offset: 0x00073AB4
		protected override ITaskModuleFactory CreateTaskModuleFactory()
		{
			return new GetTaskBaseModuleFactory();
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000758BC File Offset: 0x00073ABC
		protected override void WriteResult(IConfigurable dataObject)
		{
			RetentionPolicyTag retentionPolicyTag = (RetentionPolicyTag)dataObject;
			if (base.Fields.Contains("Types") && this.Types.Length > 0 && !this.Types.Contains(retentionPolicyTag.Type))
			{
				return;
			}
			if (this.Identity != null || !retentionPolicyTag.SystemTag || this.IncludeSystemTags)
			{
				ElcContentSettings[] array = retentionPolicyTag.GetELCContentSettings().ToArray<ElcContentSettings>();
				if (array == null || array.Length > 1)
				{
					this.WriteWarning(Strings.WarningRetentionPolicyTagCorrupted(retentionPolicyTag.Name));
				}
				PresentationRetentionPolicyTag dataObject2 = new PresentationRetentionPolicyTag(retentionPolicyTag, array.FirstOrDefault<ElcContentSettings>());
				base.WriteResult(dataObject2);
			}
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x00075958 File Offset: 0x00073B58
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.Mailbox != null)
			{
				try
				{
					using (StoreRetentionPolicyTagHelper storeRetentionPolicyTagHelper = StoreRetentionPolicyTagHelper.FromMailboxId(base.DomainController, this.Mailbox, base.CurrentOrganizationId))
					{
						if (storeRetentionPolicyTagHelper.Mailbox.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
						{
							base.WriteError(new InvalidOperationException(Strings.OptInNotSupportedForPre14Mailbox(ExchangeObjectVersion.Exchange2010.ToString(), storeRetentionPolicyTagHelper.Mailbox.Identity.ToString(), storeRetentionPolicyTagHelper.Mailbox.ExchangeVersion.ToString())), ErrorCategory.InvalidOperation, storeRetentionPolicyTagHelper.Mailbox.Identity);
						}
						IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
						configurationSession.SessionSettings.IsSharedConfigChecked = true;
						if (storeRetentionPolicyTagHelper.TagData != null && storeRetentionPolicyTagHelper.TagData.Count > 0)
						{
							foreach (Guid guid in storeRetentionPolicyTagHelper.TagData.Keys)
							{
								RetentionPolicyTag retentionTagFromGuid = this.GetRetentionTagFromGuid(guid, configurationSession);
								StoreTagData storeTagData = storeRetentionPolicyTagHelper.TagData[guid];
								if ((storeTagData.IsVisible || storeTagData.Tag.Type == ElcFolderType.All) && ((this.OptionalInMailbox && storeTagData.OptedInto) || !this.OptionalInMailbox) && retentionTagFromGuid != null)
								{
									this.WriteResult(retentionTagFromGuid);
								}
							}
						}
						if (!this.OptionalInMailbox && storeRetentionPolicyTagHelper.DefaultArchiveTagData != null && storeRetentionPolicyTagHelper.DefaultArchiveTagData.Count > 0)
						{
							foreach (Guid guid2 in storeRetentionPolicyTagHelper.DefaultArchiveTagData.Keys)
							{
								RetentionPolicyTag retentionTagFromGuid2 = this.GetRetentionTagFromGuid(guid2, configurationSession);
								if (retentionTagFromGuid2 != null)
								{
									StoreTagData storeTagData2 = storeRetentionPolicyTagHelper.DefaultArchiveTagData[guid2];
									if (storeTagData2.Tag.Type == ElcFolderType.All)
									{
										this.WriteResult(retentionTagFromGuid2);
									}
								}
							}
						}
					}
					goto IL_20A;
				}
				catch (ElcUserConfigurationException exception)
				{
					base.WriteError(exception, ErrorCategory.ResourceUnavailable, null);
					goto IL_20A;
				}
			}
			base.InternalProcessRecord();
			IL_20A:
			TaskLogger.LogExit();
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x00075BD8 File Offset: 0x00073DD8
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession;
			if (!this.IgnoreDehydratedFlag && SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
			{
				configurationSession = SharedConfiguration.CreateScopedToSharedConfigADSession(base.CurrentOrganizationId);
				return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, configurationSession.SessionSettings, 248, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Elc\\GetRetentionPolicyTag.cs");
			}
			if (!MobileDeviceTaskHelper.IsRunningUnderMyOptionsRole(this, base.TenantGlobalCatalogSession, base.SessionSettings))
			{
				configurationSession = (IConfigurationSession)base.CreateSession();
			}
			else
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 267, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Elc\\GetRetentionPolicyTag.cs");
			}
			return configurationSession;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00075C94 File Offset: 0x00073E94
		private RetentionPolicyTag GetRetentionTagFromGuid(Guid tagGuid, IConfigurationSession session)
		{
			OrFilter filter = new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, RetentionPolicyTagSchema.RetentionId, tagGuid),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, tagGuid)
			});
			IList<RetentionPolicyTag> list = session.Find<RetentionPolicyTag>(null, QueryScope.SubTree, filter, null, 1);
			if (list != null && list.Count > 0)
			{
				return list[0];
			}
			return null;
		}

		// Token: 0x04000B8C RID: 2956
		public const string ParameterSetMailboxTask = "ParameterSetMailboxTask";
	}
}
