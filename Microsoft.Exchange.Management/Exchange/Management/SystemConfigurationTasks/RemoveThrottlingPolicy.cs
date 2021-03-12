using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B23 RID: 2851
	[Cmdlet("Remove", "ThrottlingPolicy", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveThrottlingPolicy : RemoveSystemConfigurationObjectTask<ThrottlingPolicyIdParameter, ThrottlingPolicy>
	{
		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x060065F0 RID: 26096 RVA: 0x001A70AE File Offset: 0x001A52AE
		// (set) Token: 0x060065F1 RID: 26097 RVA: 0x001A70D4 File Offset: 0x001A52D4
		[Parameter]
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

		// Token: 0x060065F2 RID: 26098 RVA: 0x001A70EC File Offset: 0x001A52EC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (base.DataObject.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Global)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorCannotDeleteGlobalThrottlingPolicy), ErrorCategory.InvalidOperation, base.DataObject.Identity);
				return;
			}
			if (!this.Force)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.PartiallyConsistent, base.TenantGlobalCatalogSession.SessionSettings, 74, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\throttling\\RemoveThrottlingPolicy.cs");
				if (tenantOrRootOrgRecipientSession.IsThrottlingPolicyInUse(base.DataObject.Id))
				{
					base.WriteError(new CannotRemoveAssociatedThrottlingPolicyException(base.DataObject.Id.DistinguishedName), ErrorCategory.InvalidOperation, null);
					return;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x060065F3 RID: 26099 RVA: 0x001A71A3 File Offset: 0x001A53A3
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveThrottlingPolicy(this.Identity.ToString(), base.DataObject.ThrottlingPolicyScope.ToString());
			}
		}

		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x060065F4 RID: 26100 RVA: 0x001A71CA File Offset: 0x001A53CA
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}
	}
}
