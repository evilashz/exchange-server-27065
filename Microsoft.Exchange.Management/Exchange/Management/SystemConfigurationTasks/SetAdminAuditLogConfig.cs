using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000056 RID: 86
	[Cmdlet("Set", "AdminAuditLogConfig", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetAdminAuditLogConfig : SetMultitenancySingletonSystemConfigurationObjectTask<AdminAuditLogConfig>
	{
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00009B52 File Offset: 0x00007D52
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAdminAuditLogConfig(base.CurrentOrgContainerId.ToString());
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00009B64 File Offset: 0x00007D64
		protected override ObjectId RootId
		{
			get
			{
				return AdminAuditLogConfig.GetWellKnownParentLocation(base.CurrentOrgContainerId);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009B71 File Offset: 0x00007D71
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Static;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00009B83 File Offset: 0x00007D83
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00009B8B File Offset: 0x00007D8B
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00009B94 File Offset: 0x00007D94
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00009BBA File Offset: 0x00007DBA
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

		// Token: 0x06000232 RID: 562 RVA: 0x00009BD4 File Offset: 0x00007DD4
		protected override IConfigDataProvider CreateSession()
		{
			IConfigurationSession configurationSession = (IConfigurationSession)base.CreateSession();
			if (this.IgnoreDehydratedFlag)
			{
				configurationSession.SessionSettings.IsSharedConfigChecked = true;
			}
			configurationSession.SessionSettings.IncludeCNFObject = false;
			return configurationSession;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009C14 File Offset: 0x00007E14
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.DataObject.IsModified(ADObjectSchema.Name))
			{
				base.WriteError(new ArgumentException(Strings.ErrorCannotChangeName), ErrorCategory.InvalidArgument, null);
			}
			if (this.DataObject.IsModified(AdminAuditLogConfigSchema.AdminAuditLogExcludedCmdlets) && this.DataObject.AdminAuditLogExcludedCmdlets != null)
			{
				foreach (string a in this.DataObject.AdminAuditLogExcludedCmdlets)
				{
					if (string.Equals(a, "*", StringComparison.OrdinalIgnoreCase))
					{
						base.WriteError(new ArgumentException(Strings.ErrorInvalidExcludedCmdletPattern), ErrorCategory.InvalidArgument, null);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009D04 File Offset: 0x00007F04
		protected override void InternalProcessRecord()
		{
			if (this.DataObject.IsChanged(AdminAuditLogConfigSchema.AdminAuditLogAgeLimit) && !this.Force)
			{
				EnhancedTimeSpan t;
				if (this.DataObject.AdminAuditLogAgeLimit == EnhancedTimeSpan.Zero)
				{
					if (!base.ShouldContinue(Strings.ConfirmationMessageAdminAuditLogAgeLimitZero(base.CurrentOrgContainerId.ToString())))
					{
						return;
					}
				}
				else if (this.DataObject.TryGetOriginalValue<EnhancedTimeSpan>(AdminAuditLogConfigSchema.AdminAuditLogAgeLimit, out t))
				{
					EnhancedTimeSpan adminAuditLogAgeLimit = this.DataObject.AdminAuditLogAgeLimit;
					if (t > adminAuditLogAgeLimit && !base.ShouldContinue(Strings.ConfirmationMessageAdminAuditLogAgeLimitSmaller(base.CurrentOrgContainerId.ToString(), adminAuditLogAgeLimit.ToString())))
					{
						return;
					}
				}
			}
			if (this.IsObjectStateChanged())
			{
				this.WriteWarning(Strings.WarningSetAdminAuditLogConfigDelay(SetAdminAuditLogConfig.AuditConfigSettingsDelayTime.TotalMinutes));
			}
			if (AdminAuditLogHelper.ShouldIssueWarning(base.CurrentOrganizationId))
			{
				this.WriteWarning(Strings.WarningSetAdminAuditLogOnPreE15(base.CurrentOrganizationId.ToString()));
			}
			base.InternalProcessRecord();
			ProvisioningLayer.RefreshProvisioningBroker(this);
		}

		// Token: 0x0400013C RID: 316
		private static TimeSpan AuditConfigSettingsDelayTime = new TimeSpan(1, 0, 0);
	}
}
