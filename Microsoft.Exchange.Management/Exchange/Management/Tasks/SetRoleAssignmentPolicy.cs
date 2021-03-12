using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000452 RID: 1106
	[Cmdlet("Set", "RoleAssignmentPolicy", SupportsShouldProcess = true)]
	public sealed class SetRoleAssignmentPolicy : SetMailboxPolicyBase<RoleAssignmentPolicy>
	{
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002727 RID: 10023 RVA: 0x0009AFC9 File Offset: 0x000991C9
		private bool UpdateOtherDefaultPolicies
		{
			get
			{
				return this.otherDefaultPolicies != null && this.otherDefaultPolicies.Count > 0;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x0009AFE3 File Offset: 0x000991E3
		// (set) Token: 0x06002729 RID: 10025 RVA: 0x0009B009 File Offset: 0x00099209
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefault
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefault"] ?? false);
			}
			set
			{
				base.Fields["IsDefault"] = value;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x0600272A RID: 10026 RVA: 0x0009B021 File Offset: 0x00099221
		// (set) Token: 0x0600272B RID: 10027 RVA: 0x0009B038 File Offset: 0x00099238
		[Parameter]
		public string Description
		{
			get
			{
				return (string)base.Fields["Description"];
			}
			set
			{
				base.Fields["Description"] = value;
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x0600272C RID: 10028 RVA: 0x0009B04B File Offset: 0x0009924B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.UpdateOtherDefaultPolicies)
				{
					return Strings.ConfirmationMessageSwitchRBACPolicy(this.Identity.ToString());
				}
				return base.ConfirmationMessage;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x0009B06C File Offset: 0x0009926C
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x0009B070 File Offset: 0x00099270
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.IsDefault)
			{
				this.DataObject.IsDefault = true;
				QueryFilter extraFilter = new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, this.DataObject.Id.ObjectGuid);
				this.otherDefaultPolicies = RoleAssignmentPolicyHelper.GetDefaultPolicies((IConfigurationSession)base.DataSession, extraFilter);
			}
			else if (!this.IsDefault && base.Fields.IsChanged("IsDefault") && this.DataObject.IsDefault)
			{
				base.WriteError(new InvalidOperationException(Strings.ResettingIsDefaultIsNotSupported("IsDefault", "RoleAssignmentPolicy")), ErrorCategory.WriteError, this.DataObject);
			}
			if (base.Fields.IsChanged("Description"))
			{
				this.DataObject.Description = this.Description;
			}
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x0009B14C File Offset: 0x0009934C
		protected override void InternalProcessRecord()
		{
			if (this.UpdateOtherDefaultPolicies && !base.ShouldContinue(Strings.ConfirmationMessageSwitchMailboxPolicy("RoleAssignmentPolicy", this.Identity.ToString())))
			{
				return;
			}
			base.InternalProcessRecord();
			if (this.UpdateOtherDefaultPolicies)
			{
				try
				{
					RoleAssignmentPolicyHelper.ClearIsDefaultOnPolicies((IConfigurationSession)base.DataSession, this.otherDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x0009B1C4 File Offset: 0x000993C4
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.ResolveDataObject();
		}
	}
}
