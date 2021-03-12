using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000450 RID: 1104
	[Cmdlet("Remove", "RoleAssignmentPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRoleAssignmentPolicy : RemoveMailboxPolicyBase<RoleAssignmentPolicy>
	{
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x0009AE0D File Offset: 0x0009900D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRBACPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x0009AE1F File Offset: 0x0009901F
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0009AE24 File Offset: 0x00099024
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.DataObject.IsDefault)
			{
				base.WriteError(new InvalidOperationException(Strings.RemovingDefaultPolicyIsNotSupported(this.Identity.ToString())), ErrorCategory.WriteError, base.DataObject);
			}
			if (RoleAssignmentPolicyHelper.RoleAssignmentsForPolicyExist((IConfigurationSession)base.DataSession, base.DataObject))
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorRemovingPolicyInUse(base.DataObject.Id.ToString())), ErrorCategory.WriteError, base.DataObject);
			}
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0009AEB1 File Offset: 0x000990B1
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			base.WriteError(new CannotDeleteAssociatedMailboxPolicyException(this.Identity.ToString()), ErrorCategory.WriteError, base.DataObject);
			return false;
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x0009AED2 File Offset: 0x000990D2
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.ResolveDataObject();
		}
	}
}
