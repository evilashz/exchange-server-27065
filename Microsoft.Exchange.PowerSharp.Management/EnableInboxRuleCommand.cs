using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C48 RID: 3144
	public class EnableInboxRuleCommand : SyntheticCommandWithPipelineInputNoOutput<InboxRuleIdParameter>
	{
		// Token: 0x060099B2 RID: 39346 RVA: 0x000DF3E5 File Offset: 0x000DD5E5
		private EnableInboxRuleCommand() : base("Enable-InboxRule")
		{
		}

		// Token: 0x060099B3 RID: 39347 RVA: 0x000DF3F2 File Offset: 0x000DD5F2
		public EnableInboxRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060099B4 RID: 39348 RVA: 0x000DF401 File Offset: 0x000DD601
		public virtual EnableInboxRuleCommand SetParameters(EnableInboxRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060099B5 RID: 39349 RVA: 0x000DF40B File Offset: 0x000DD60B
		public virtual EnableInboxRuleCommand SetParameters(EnableInboxRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C49 RID: 3145
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006CDB RID: 27867
			// (set) Token: 0x060099B6 RID: 39350 RVA: 0x000DF415 File Offset: 0x000DD615
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006CDC RID: 27868
			// (set) Token: 0x060099B7 RID: 39351 RVA: 0x000DF42D File Offset: 0x000DD62D
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006CDD RID: 27869
			// (set) Token: 0x060099B8 RID: 39352 RVA: 0x000DF445 File Offset: 0x000DD645
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006CDE RID: 27870
			// (set) Token: 0x060099B9 RID: 39353 RVA: 0x000DF463 File Offset: 0x000DD663
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006CDF RID: 27871
			// (set) Token: 0x060099BA RID: 39354 RVA: 0x000DF476 File Offset: 0x000DD676
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006CE0 RID: 27872
			// (set) Token: 0x060099BB RID: 39355 RVA: 0x000DF48E File Offset: 0x000DD68E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006CE1 RID: 27873
			// (set) Token: 0x060099BC RID: 39356 RVA: 0x000DF4A6 File Offset: 0x000DD6A6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006CE2 RID: 27874
			// (set) Token: 0x060099BD RID: 39357 RVA: 0x000DF4BE File Offset: 0x000DD6BE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006CE3 RID: 27875
			// (set) Token: 0x060099BE RID: 39358 RVA: 0x000DF4D6 File Offset: 0x000DD6D6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C4A RID: 3146
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006CE4 RID: 27876
			// (set) Token: 0x060099C0 RID: 39360 RVA: 0x000DF4F6 File Offset: 0x000DD6F6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new InboxRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17006CE5 RID: 27877
			// (set) Token: 0x060099C1 RID: 39361 RVA: 0x000DF514 File Offset: 0x000DD714
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17006CE6 RID: 27878
			// (set) Token: 0x060099C2 RID: 39362 RVA: 0x000DF52C File Offset: 0x000DD72C
			public virtual SwitchParameter AlwaysDeleteOutlookRulesBlob
			{
				set
				{
					base.PowerSharpParameters["AlwaysDeleteOutlookRulesBlob"] = value;
				}
			}

			// Token: 0x17006CE7 RID: 27879
			// (set) Token: 0x060099C3 RID: 39363 RVA: 0x000DF544 File Offset: 0x000DD744
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006CE8 RID: 27880
			// (set) Token: 0x060099C4 RID: 39364 RVA: 0x000DF562 File Offset: 0x000DD762
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006CE9 RID: 27881
			// (set) Token: 0x060099C5 RID: 39365 RVA: 0x000DF575 File Offset: 0x000DD775
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006CEA RID: 27882
			// (set) Token: 0x060099C6 RID: 39366 RVA: 0x000DF58D File Offset: 0x000DD78D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006CEB RID: 27883
			// (set) Token: 0x060099C7 RID: 39367 RVA: 0x000DF5A5 File Offset: 0x000DD7A5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006CEC RID: 27884
			// (set) Token: 0x060099C8 RID: 39368 RVA: 0x000DF5BD File Offset: 0x000DD7BD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006CED RID: 27885
			// (set) Token: 0x060099C9 RID: 39369 RVA: 0x000DF5D5 File Offset: 0x000DD7D5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
