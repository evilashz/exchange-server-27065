using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000806 RID: 2054
	public class NewOnPremisesOrganizationCommand : SyntheticCommandWithPipelineInput<OnPremisesOrganization, OnPremisesOrganization>
	{
		// Token: 0x060065C6 RID: 26054 RVA: 0x0009B676 File Offset: 0x00099876
		private NewOnPremisesOrganizationCommand() : base("New-OnPremisesOrganization")
		{
		}

		// Token: 0x060065C7 RID: 26055 RVA: 0x0009B683 File Offset: 0x00099883
		public NewOnPremisesOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060065C8 RID: 26056 RVA: 0x0009B692 File Offset: 0x00099892
		public virtual NewOnPremisesOrganizationCommand SetParameters(NewOnPremisesOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000807 RID: 2055
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004173 RID: 16755
			// (set) Token: 0x060065C9 RID: 26057 RVA: 0x0009B69C File Offset: 0x0009989C
			public virtual Guid OrganizationGuid
			{
				set
				{
					base.PowerSharpParameters["OrganizationGuid"] = value;
				}
			}

			// Token: 0x17004174 RID: 16756
			// (set) Token: 0x060065CA RID: 26058 RVA: 0x0009B6B4 File Offset: 0x000998B4
			public virtual MultiValuedProperty<SmtpDomain> HybridDomains
			{
				set
				{
					base.PowerSharpParameters["HybridDomains"] = value;
				}
			}

			// Token: 0x17004175 RID: 16757
			// (set) Token: 0x060065CB RID: 26059 RVA: 0x0009B6C7 File Offset: 0x000998C7
			public virtual InboundConnectorIdParameter InboundConnector
			{
				set
				{
					base.PowerSharpParameters["InboundConnector"] = value;
				}
			}

			// Token: 0x17004176 RID: 16758
			// (set) Token: 0x060065CC RID: 26060 RVA: 0x0009B6DA File Offset: 0x000998DA
			public virtual OutboundConnectorIdParameter OutboundConnector
			{
				set
				{
					base.PowerSharpParameters["OutboundConnector"] = value;
				}
			}

			// Token: 0x17004177 RID: 16759
			// (set) Token: 0x060065CD RID: 26061 RVA: 0x0009B6ED File Offset: 0x000998ED
			public virtual string OrganizationName
			{
				set
				{
					base.PowerSharpParameters["OrganizationName"] = value;
				}
			}

			// Token: 0x17004178 RID: 16760
			// (set) Token: 0x060065CE RID: 26062 RVA: 0x0009B700 File Offset: 0x00099900
			public virtual OrganizationRelationshipIdParameter OrganizationRelationship
			{
				set
				{
					base.PowerSharpParameters["OrganizationRelationship"] = value;
				}
			}

			// Token: 0x17004179 RID: 16761
			// (set) Token: 0x060065CF RID: 26063 RVA: 0x0009B713 File Offset: 0x00099913
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700417A RID: 16762
			// (set) Token: 0x060065D0 RID: 26064 RVA: 0x0009B731 File Offset: 0x00099931
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700417B RID: 16763
			// (set) Token: 0x060065D1 RID: 26065 RVA: 0x0009B744 File Offset: 0x00099944
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700417C RID: 16764
			// (set) Token: 0x060065D2 RID: 26066 RVA: 0x0009B757 File Offset: 0x00099957
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700417D RID: 16765
			// (set) Token: 0x060065D3 RID: 26067 RVA: 0x0009B76F File Offset: 0x0009996F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700417E RID: 16766
			// (set) Token: 0x060065D4 RID: 26068 RVA: 0x0009B787 File Offset: 0x00099987
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700417F RID: 16767
			// (set) Token: 0x060065D5 RID: 26069 RVA: 0x0009B79F File Offset: 0x0009999F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004180 RID: 16768
			// (set) Token: 0x060065D6 RID: 26070 RVA: 0x0009B7B7 File Offset: 0x000999B7
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
