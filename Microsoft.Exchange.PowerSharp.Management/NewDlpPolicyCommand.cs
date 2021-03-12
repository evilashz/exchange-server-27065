using System;
using System.Collections;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005D0 RID: 1488
	public class NewDlpPolicyCommand : SyntheticCommandWithPipelineInput<ADComplianceProgram, ADComplianceProgram>
	{
		// Token: 0x06004D20 RID: 19744 RVA: 0x0007B4BA File Offset: 0x000796BA
		private NewDlpPolicyCommand() : base("New-DlpPolicy")
		{
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x0007B4C7 File Offset: 0x000796C7
		public NewDlpPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x0007B4D6 File Offset: 0x000796D6
		public virtual NewDlpPolicyCommand SetParameters(NewDlpPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005D1 RID: 1489
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D39 RID: 11577
			// (set) Token: 0x06004D23 RID: 19747 RVA: 0x0007B4E0 File Offset: 0x000796E0
			public virtual string Template
			{
				set
				{
					base.PowerSharpParameters["Template"] = value;
				}
			}

			// Token: 0x17002D3A RID: 11578
			// (set) Token: 0x06004D24 RID: 19748 RVA: 0x0007B4F3 File Offset: 0x000796F3
			public virtual RuleState State
			{
				set
				{
					base.PowerSharpParameters["State"] = value;
				}
			}

			// Token: 0x17002D3B RID: 11579
			// (set) Token: 0x06004D25 RID: 19749 RVA: 0x0007B50B File Offset: 0x0007970B
			public virtual RuleMode Mode
			{
				set
				{
					base.PowerSharpParameters["Mode"] = value;
				}
			}

			// Token: 0x17002D3C RID: 11580
			// (set) Token: 0x06004D26 RID: 19750 RVA: 0x0007B523 File Offset: 0x00079723
			public virtual byte TemplateData
			{
				set
				{
					base.PowerSharpParameters["TemplateData"] = value;
				}
			}

			// Token: 0x17002D3D RID: 11581
			// (set) Token: 0x06004D27 RID: 19751 RVA: 0x0007B53B File Offset: 0x0007973B
			public virtual Hashtable Parameters
			{
				set
				{
					base.PowerSharpParameters["Parameters"] = value;
				}
			}

			// Token: 0x17002D3E RID: 11582
			// (set) Token: 0x06004D28 RID: 19752 RVA: 0x0007B54E File Offset: 0x0007974E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002D3F RID: 11583
			// (set) Token: 0x06004D29 RID: 19753 RVA: 0x0007B561 File Offset: 0x00079761
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17002D40 RID: 11584
			// (set) Token: 0x06004D2A RID: 19754 RVA: 0x0007B574 File Offset: 0x00079774
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002D41 RID: 11585
			// (set) Token: 0x06004D2B RID: 19755 RVA: 0x0007B592 File Offset: 0x00079792
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D42 RID: 11586
			// (set) Token: 0x06004D2C RID: 19756 RVA: 0x0007B5A5 File Offset: 0x000797A5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D43 RID: 11587
			// (set) Token: 0x06004D2D RID: 19757 RVA: 0x0007B5BD File Offset: 0x000797BD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D44 RID: 11588
			// (set) Token: 0x06004D2E RID: 19758 RVA: 0x0007B5D5 File Offset: 0x000797D5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D45 RID: 11589
			// (set) Token: 0x06004D2F RID: 19759 RVA: 0x0007B5ED File Offset: 0x000797ED
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D46 RID: 11590
			// (set) Token: 0x06004D30 RID: 19760 RVA: 0x0007B605 File Offset: 0x00079805
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
