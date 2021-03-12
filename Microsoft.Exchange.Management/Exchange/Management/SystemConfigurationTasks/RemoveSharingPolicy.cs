using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009F5 RID: 2549
	[Cmdlet("Remove", "SharingPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveSharingPolicy : RemoveSystemConfigurationObjectTask<SharingPolicyIdParameter, SharingPolicy>
	{
		// Token: 0x17001B46 RID: 6982
		// (get) Token: 0x06005B25 RID: 23333 RVA: 0x0017D4B6 File Offset: 0x0017B6B6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveSharingPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x06005B26 RID: 23334 RVA: 0x0017D4C8 File Offset: 0x0017B6C8
		protected override void InternalValidate()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			base.InternalValidate();
			this.ConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			FederatedOrganizationId federatedOrganizationId = this.ConfigurationSession.GetFederatedOrganizationId(base.DataObject.OrganizationId);
			if (base.DataObject.Id.Equals(federatedOrganizationId.DefaultSharingPolicyLink))
			{
				base.WriteError(new CannotRemoveDefaultSharingPolicy(), ErrorCategory.InvalidOperation, this.Identity);
			}
			IRecipientSession tenantOrRootOrgRecipientSession;
			if (this.ConfigurationSession.ConfigScope == ConfigScopes.TenantSubTree)
			{
				tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.ConfigurationSession.DomainController, null, this.ConfigurationSession.Lcid, true, ConsistencyMode.PartiallyConsistent, this.ConfigurationSession.NetworkCredential, this.ConfigurationSession.SessionSettings, this.ConfigurationSession.ConfigScope, 68, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Federation\\RemoveSharingPolicy.cs");
			}
			else
			{
				tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(this.ConfigurationSession.DomainController, null, this.ConfigurationSession.Lcid, true, ConsistencyMode.PartiallyConsistent, this.ConfigurationSession.NetworkCredential, this.ConfigurationSession.SessionSettings, 80, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\Federation\\RemoveSharingPolicy.cs");
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, IADMailStorageSchema.SharingPolicy, base.DataObject.Id);
			ADRecipient[] array = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, filter, null, 1);
			if (array.Length > 0)
			{
				base.WriteError(new CannotRemoveSharingPolicyWithUsersAssignedException(), ErrorCategory.InvalidOperation, this.Identity);
			}
		}

		// Token: 0x06005B27 RID: 23335 RVA: 0x0017D628 File Offset: 0x0017B828
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
