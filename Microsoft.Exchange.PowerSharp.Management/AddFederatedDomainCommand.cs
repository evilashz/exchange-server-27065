using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000675 RID: 1653
	public class AddFederatedDomainCommand : SyntheticCommandWithPipelineInput<FederatedOrganizationId, FederatedOrganizationId>
	{
		// Token: 0x06005872 RID: 22642 RVA: 0x0008A970 File Offset: 0x00088B70
		private AddFederatedDomainCommand() : base("Add-FederatedDomain")
		{
		}

		// Token: 0x06005873 RID: 22643 RVA: 0x0008A97D File Offset: 0x00088B7D
		public AddFederatedDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005874 RID: 22644 RVA: 0x0008A98C File Offset: 0x00088B8C
		public virtual AddFederatedDomainCommand SetParameters(AddFederatedDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005875 RID: 22645 RVA: 0x0008A996 File Offset: 0x00088B96
		public virtual AddFederatedDomainCommand SetParameters(AddFederatedDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000676 RID: 1654
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003741 RID: 14145
			// (set) Token: 0x06005876 RID: 22646 RVA: 0x0008A9A0 File Offset: 0x00088BA0
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003742 RID: 14146
			// (set) Token: 0x06005877 RID: 22647 RVA: 0x0008A9B3 File Offset: 0x00088BB3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003743 RID: 14147
			// (set) Token: 0x06005878 RID: 22648 RVA: 0x0008A9C6 File Offset: 0x00088BC6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003744 RID: 14148
			// (set) Token: 0x06005879 RID: 22649 RVA: 0x0008A9DE File Offset: 0x00088BDE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003745 RID: 14149
			// (set) Token: 0x0600587A RID: 22650 RVA: 0x0008A9F6 File Offset: 0x00088BF6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003746 RID: 14150
			// (set) Token: 0x0600587B RID: 22651 RVA: 0x0008AA0E File Offset: 0x00088C0E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003747 RID: 14151
			// (set) Token: 0x0600587C RID: 22652 RVA: 0x0008AA26 File Offset: 0x00088C26
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000677 RID: 1655
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003748 RID: 14152
			// (set) Token: 0x0600587E RID: 22654 RVA: 0x0008AA46 File Offset: 0x00088C46
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17003749 RID: 14153
			// (set) Token: 0x0600587F RID: 22655 RVA: 0x0008AA64 File Offset: 0x00088C64
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700374A RID: 14154
			// (set) Token: 0x06005880 RID: 22656 RVA: 0x0008AA77 File Offset: 0x00088C77
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700374B RID: 14155
			// (set) Token: 0x06005881 RID: 22657 RVA: 0x0008AA8A File Offset: 0x00088C8A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700374C RID: 14156
			// (set) Token: 0x06005882 RID: 22658 RVA: 0x0008AAA2 File Offset: 0x00088CA2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700374D RID: 14157
			// (set) Token: 0x06005883 RID: 22659 RVA: 0x0008AABA File Offset: 0x00088CBA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700374E RID: 14158
			// (set) Token: 0x06005884 RID: 22660 RVA: 0x0008AAD2 File Offset: 0x00088CD2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700374F RID: 14159
			// (set) Token: 0x06005885 RID: 22661 RVA: 0x0008AAEA File Offset: 0x00088CEA
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
