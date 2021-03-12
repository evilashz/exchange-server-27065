using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B06 RID: 2822
	[Cmdlet("Get", "EmailAddressPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetEmailAddressPolicy : GetMultitenancySystemConfigurationObjectTask<EmailAddressPolicyIdParameter, EmailAddressPolicy>
	{
		// Token: 0x17001E73 RID: 7795
		// (get) Token: 0x06006450 RID: 25680 RVA: 0x001A2CAC File Offset: 0x001A0EAC
		protected override QueryFilter InternalFilter
		{
			get
			{
				if (this.IncludeMailboxSettingOnlyPolicy.IsPresent)
				{
					return null;
				}
				return new ComparisonFilter(ComparisonOperator.Equal, EmailAddressPolicySchema.PolicyOptionListValue, EmailAddressPolicy.PolicyGuid.ToByteArray());
			}
		}

		// Token: 0x17001E74 RID: 7796
		// (get) Token: 0x06006451 RID: 25681 RVA: 0x001A2CE3 File Offset: 0x001A0EE3
		// (set) Token: 0x06006452 RID: 25682 RVA: 0x001A2D09 File Offset: 0x001A0F09
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeMailboxSettingOnlyPolicy
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeMailboxSettingOnlyPolicy"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeMailboxSettingOnlyPolicy"] = value;
			}
		}

		// Token: 0x17001E75 RID: 7797
		// (get) Token: 0x06006453 RID: 25683 RVA: 0x001A2D21 File Offset: 0x001A0F21
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001E76 RID: 7798
		// (get) Token: 0x06006454 RID: 25684 RVA: 0x001A2D24 File Offset: 0x001A0F24
		protected override ObjectId RootId
		{
			get
			{
				if (this.Identity == null)
				{
					return base.CurrentOrgContainerId.GetDescendantId(EmailAddressPolicy.RdnEapContainerToOrganization);
				}
				return null;
			}
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x001A2D40 File Offset: 0x001A0F40
		protected override void WriteResult(IConfigurable dataObject)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObject.Identity,
				dataObject
			});
			EmailAddressPolicy emailAddressPolicy = (EmailAddressPolicy)dataObject;
			if (!this.IncludeMailboxSettingOnlyPolicy.IsPresent && !emailAddressPolicy.HasEmailAddressSetting)
			{
				this.mailboxSettingOnlyPolicyIgnored = true;
				TaskLogger.LogExit();
				return;
			}
			OrganizationId organizationId = emailAddressPolicy.OrganizationId;
			if (this.domainValidator == null || !this.domainValidator.OrganizationId.Equals(organizationId))
			{
				this.domainValidator = new UpdateEmailAddressPolicy.ReadOnlyDomainValidator(organizationId, this.ConfigurationSession, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			this.domainValidator.Validate(emailAddressPolicy);
			base.WriteResult(dataObject);
			TaskLogger.LogExit();
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x001A2DEC File Offset: 0x001A0FEC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			this.mailboxSettingOnlyPolicyIgnored = false;
			base.InternalProcessRecord();
			if (this.Identity == null && !this.IncludeMailboxSettingOnlyPolicy.IsPresent)
			{
				try
				{
					this.mailboxSettingOnlyPolicyIgnored = true;
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.NotEqual, EmailAddressPolicySchema.PolicyOptionListValue, EmailAddressPolicy.PolicyGuid.ToByteArray());
					base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(EmailAddressPolicy), filter, this.RootId, this.DeepSearch));
					EmailAddressPolicy[] array = (base.DataSession as IConfigurationSession).Find<EmailAddressPolicy>((ADObjectId)this.RootId, this.DeepSearch ? QueryScope.SubTree : QueryScope.OneLevel, filter, null, 1);
					this.mailboxSettingOnlyPolicyIgnored = (0 != array.Length);
				}
				catch (DataSourceTransientException ex)
				{
					base.WriteVerbose(ex.LocalizedString);
				}
				catch (DataSourceOperationException ex2)
				{
					base.WriteVerbose(ex2.LocalizedString);
				}
			}
			if (this.mailboxSettingOnlyPolicyIgnored)
			{
				this.WriteWarning(Strings.WarningIgnoreMailboxSettingOnlyPolicy);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400361F RID: 13855
		private bool mailboxSettingOnlyPolicyIgnored;

		// Token: 0x04003620 RID: 13856
		private UpdateEmailAddressPolicy.ReadOnlyDomainValidator domainValidator;
	}
}
