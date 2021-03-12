using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020005D5 RID: 1493
	public class RemoveDlpPolicyCommand : SyntheticCommandWithPipelineInput<ADComplianceProgram, ADComplianceProgram>
	{
		// Token: 0x06004D4D RID: 19789 RVA: 0x0007B845 File Offset: 0x00079A45
		private RemoveDlpPolicyCommand() : base("Remove-DlpPolicy")
		{
		}

		// Token: 0x06004D4E RID: 19790 RVA: 0x0007B852 File Offset: 0x00079A52
		public RemoveDlpPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004D4F RID: 19791 RVA: 0x0007B861 File Offset: 0x00079A61
		public virtual RemoveDlpPolicyCommand SetParameters(RemoveDlpPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004D50 RID: 19792 RVA: 0x0007B86B File Offset: 0x00079A6B
		public virtual RemoveDlpPolicyCommand SetParameters(RemoveDlpPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020005D6 RID: 1494
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002D5C RID: 11612
			// (set) Token: 0x06004D51 RID: 19793 RVA: 0x0007B875 File Offset: 0x00079A75
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D5D RID: 11613
			// (set) Token: 0x06004D52 RID: 19794 RVA: 0x0007B888 File Offset: 0x00079A88
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D5E RID: 11614
			// (set) Token: 0x06004D53 RID: 19795 RVA: 0x0007B8A0 File Offset: 0x00079AA0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D5F RID: 11615
			// (set) Token: 0x06004D54 RID: 19796 RVA: 0x0007B8B8 File Offset: 0x00079AB8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D60 RID: 11616
			// (set) Token: 0x06004D55 RID: 19797 RVA: 0x0007B8D0 File Offset: 0x00079AD0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D61 RID: 11617
			// (set) Token: 0x06004D56 RID: 19798 RVA: 0x0007B8E8 File Offset: 0x00079AE8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002D62 RID: 11618
			// (set) Token: 0x06004D57 RID: 19799 RVA: 0x0007B900 File Offset: 0x00079B00
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020005D7 RID: 1495
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002D63 RID: 11619
			// (set) Token: 0x06004D59 RID: 19801 RVA: 0x0007B920 File Offset: 0x00079B20
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DlpPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17002D64 RID: 11620
			// (set) Token: 0x06004D5A RID: 19802 RVA: 0x0007B93E File Offset: 0x00079B3E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002D65 RID: 11621
			// (set) Token: 0x06004D5B RID: 19803 RVA: 0x0007B951 File Offset: 0x00079B51
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002D66 RID: 11622
			// (set) Token: 0x06004D5C RID: 19804 RVA: 0x0007B969 File Offset: 0x00079B69
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002D67 RID: 11623
			// (set) Token: 0x06004D5D RID: 19805 RVA: 0x0007B981 File Offset: 0x00079B81
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002D68 RID: 11624
			// (set) Token: 0x06004D5E RID: 19806 RVA: 0x0007B999 File Offset: 0x00079B99
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002D69 RID: 11625
			// (set) Token: 0x06004D5F RID: 19807 RVA: 0x0007B9B1 File Offset: 0x00079BB1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002D6A RID: 11626
			// (set) Token: 0x06004D60 RID: 19808 RVA: 0x0007B9C9 File Offset: 0x00079BC9
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
