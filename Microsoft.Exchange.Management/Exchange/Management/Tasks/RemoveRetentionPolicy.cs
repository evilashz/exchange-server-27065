using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200044B RID: 1099
	[Cmdlet("remove", "RetentionPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRetentionPolicy : RemoveMailboxPolicyBase<RetentionPolicy>
	{
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00099E3C File Offset: 0x0009803C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRetentionPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x00099E4E File Offset: 0x0009804E
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x060026DF RID: 9951 RVA: 0x00099E51 File Offset: 0x00098051
		// (set) Token: 0x060026E0 RID: 9952 RVA: 0x00099E77 File Offset: 0x00098077
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

		// Token: 0x060026E1 RID: 9953 RVA: 0x00099E8F File Offset: 0x0009808F
		protected override bool HandleRemoveWithAssociatedUsers()
		{
			return this.Force || base.ShouldContinue(Strings.WarningRemovePolicyWithAssociatedUsers(base.DataObject.Name));
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x00099EB8 File Offset: 0x000980B8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
			{
				base.WriteError(new ArgumentException(Strings.ErrorWriteOpOnDehydratedTenant), ErrorCategory.InvalidArgument, base.DataObject.Identity);
			}
			if (base.DataObject.IsDefault || base.DataObject.IsDefaultArbitrationMailbox)
			{
				base.WriteError(new InvalidOperationException(Strings.RemovingDefaultPolicyIsNotSupported(this.Identity.ToString())), ErrorCategory.WriteError, base.DataObject);
			}
		}

		// Token: 0x060026E3 RID: 9955 RVA: 0x00099F3C File Offset: 0x0009813C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.DataObject != null && SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
