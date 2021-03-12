using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core.RuleTasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000880 RID: 2176
	[Cmdlet("Set", "ClientAccessRule", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetClientAccessRule : SetSystemConfigurationObjectTask<ClientAccessRuleIdParameter, ADClientAccessRule>
	{
		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06004B74 RID: 19316 RVA: 0x00138397 File Offset: 0x00136597
		// (set) Token: 0x06004B75 RID: 19317 RVA: 0x0013839F File Offset: 0x0013659F
		[Parameter(Mandatory = false)]
		public SwitchParameter DatacenterAdminsOnly { get; set; }

		// Token: 0x06004B76 RID: 19318 RVA: 0x001383A8 File Offset: 0x001365A8
		protected override void InternalProcessRecord()
		{
			if (!ClientAccessRulesStorageManager.IsADRuleValid(this.DataObject))
			{
				base.WriteError(new InvalidOperationException(RulesTasksStrings.ClientAccessRulesAuthenticationTypeInvalid), ErrorCategory.InvalidOperation, null);
			}
			if (this.DataObject.IsModified(ADObjectSchema.Name) || this.DataObject.IsModified(ADClientAccessRuleSchema.Priority))
			{
				List<ADClientAccessRule> list = new List<ADClientAccessRule>(ClientAccessRulesStorageManager.GetClientAccessRules((IConfigurationSession)base.DataSession));
				if (this.DataObject.IsModified(ADObjectSchema.Name))
				{
					foreach (ADClientAccessRule adclientAccessRule in list)
					{
						if (!adclientAccessRule.Identity.Equals(this.DataObject.Identity) && adclientAccessRule.Name.Equals(this.DataObject.Name))
						{
							base.WriteError(new InvalidOperationException(RulesTasksStrings.ClientAccessRulesNameAlreadyInUse), ErrorCategory.InvalidOperation, null);
						}
					}
				}
				if (this.DataObject.IsModified(ADClientAccessRuleSchema.Priority))
				{
					bool flag = false;
					ClientAccessRulesPriorityManager clientAccessRulesPriorityManager = new ClientAccessRulesPriorityManager(list);
					this.DataObject.InternalPriority = clientAccessRulesPriorityManager.GetInternalPriority(this.DataObject.Priority, this.DataObject, out flag);
					if (flag)
					{
						ClientAccessRulesStorageManager.SaveRules((IConfigurationSession)base.DataSession, clientAccessRulesPriorityManager.ADClientAccessRules);
					}
				}
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x00138508 File Offset: 0x00136708
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (!this.DatacenterAdminsOnly.IsPresent && this.DataObject.DatacenterAdminsOnly)
			{
				base.WriteError(new InvalidOperationException(RulesTasksStrings.ClientAccessRuleSetDatacenterAdminsOnlyError), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06004B78 RID: 19320 RVA: 0x00138559 File Offset: 0x00136759
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return RulesTasksStrings.ConfirmationMessageSetClientAccessRule(this.Identity.ToString());
			}
		}
	}
}
