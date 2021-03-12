using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200087F RID: 2175
	[Cmdlet("Remove", "ClientAccessRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveClientAccessRule : RemoveSystemConfigurationObjectTask<ClientAccessRuleIdParameter, ADClientAccessRule>
	{
		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06004B6F RID: 19311 RVA: 0x0013831B File Offset: 0x0013651B
		// (set) Token: 0x06004B70 RID: 19312 RVA: 0x00138323 File Offset: 0x00136523
		[Parameter(Mandatory = false)]
		public SwitchParameter DatacenterAdminsOnly { get; set; }

		// Token: 0x06004B71 RID: 19313 RVA: 0x0013832C File Offset: 0x0013652C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!this.DatacenterAdminsOnly.IsPresent && base.DataObject.DatacenterAdminsOnly)
			{
				base.WriteError(new InvalidOperationException(RulesTasksStrings.ClientAccessRuleRemoveDatacenterAdminsOnlyError), ErrorCategory.InvalidOperation, base.DataObject.Identity);
			}
		}

		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06004B72 RID: 19314 RVA: 0x0013837D File Offset: 0x0013657D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return RulesTasksStrings.ConfirmationMessageRemoveClientAccessRule(this.Identity.ToString());
			}
		}
	}
}
