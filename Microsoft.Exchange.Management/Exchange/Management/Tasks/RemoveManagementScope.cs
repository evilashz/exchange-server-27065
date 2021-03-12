using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200068A RID: 1674
	[Cmdlet("Remove", "ManagementScope", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveManagementScope : RemoveSystemConfigurationObjectTask<ManagementScopeIdParameter, ManagementScope>
	{
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x06003B50 RID: 15184 RVA: 0x000FC718 File Offset: 0x000FA918
		// (set) Token: 0x06003B51 RID: 15185 RVA: 0x000FC73E File Offset: 0x000FA93E
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

		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x06003B52 RID: 15186 RVA: 0x000FC756 File Offset: 0x000FA956
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveManagementScope(this.Identity.ToString());
			}
		}

		// Token: 0x06003B53 RID: 15187 RVA: 0x000FC768 File Offset: 0x000FA968
		protected override void InternalValidate()
		{
			base.InternalValidate();
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			if (configurationSession.ManagementScopeIsInUse(base.DataObject))
			{
				base.WriteError(new InvalidOperationException(Strings.ScopeIsInUse), ErrorCategory.InvalidOperation, this.Identity);
			}
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x000FC7B1 File Offset: 0x000FA9B1
		protected override IConfigurable ResolveDataObject()
		{
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			return base.ResolveDataObject();
		}

		// Token: 0x06003B55 RID: 15189 RVA: 0x000FC7D0 File Offset: 0x000FA9D0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
