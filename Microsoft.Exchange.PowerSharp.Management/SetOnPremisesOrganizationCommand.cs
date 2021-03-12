using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200080B RID: 2059
	public class SetOnPremisesOrganizationCommand : SyntheticCommandWithPipelineInputNoOutput<OnPremisesOrganization>
	{
		// Token: 0x060065ED RID: 26093 RVA: 0x0009B970 File Offset: 0x00099B70
		private SetOnPremisesOrganizationCommand() : base("Set-OnPremisesOrganization")
		{
		}

		// Token: 0x060065EE RID: 26094 RVA: 0x0009B97D File Offset: 0x00099B7D
		public SetOnPremisesOrganizationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060065EF RID: 26095 RVA: 0x0009B98C File Offset: 0x00099B8C
		public virtual SetOnPremisesOrganizationCommand SetParameters(SetOnPremisesOrganizationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060065F0 RID: 26096 RVA: 0x0009B996 File Offset: 0x00099B96
		public virtual SetOnPremisesOrganizationCommand SetParameters(SetOnPremisesOrganizationCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200080C RID: 2060
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004190 RID: 16784
			// (set) Token: 0x060065F1 RID: 26097 RVA: 0x0009B9A0 File Offset: 0x00099BA0
			public virtual InboundConnectorIdParameter InboundConnector
			{
				set
				{
					base.PowerSharpParameters["InboundConnector"] = value;
				}
			}

			// Token: 0x17004191 RID: 16785
			// (set) Token: 0x060065F2 RID: 26098 RVA: 0x0009B9B3 File Offset: 0x00099BB3
			public virtual OutboundConnectorIdParameter OutboundConnector
			{
				set
				{
					base.PowerSharpParameters["OutboundConnector"] = value;
				}
			}

			// Token: 0x17004192 RID: 16786
			// (set) Token: 0x060065F3 RID: 26099 RVA: 0x0009B9C6 File Offset: 0x00099BC6
			public virtual string OrganizationName
			{
				set
				{
					base.PowerSharpParameters["OrganizationName"] = value;
				}
			}

			// Token: 0x17004193 RID: 16787
			// (set) Token: 0x060065F4 RID: 26100 RVA: 0x0009B9D9 File Offset: 0x00099BD9
			public virtual OrganizationRelationshipIdParameter OrganizationRelationship
			{
				set
				{
					base.PowerSharpParameters["OrganizationRelationship"] = value;
				}
			}

			// Token: 0x17004194 RID: 16788
			// (set) Token: 0x060065F5 RID: 26101 RVA: 0x0009B9EC File Offset: 0x00099BEC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004195 RID: 16789
			// (set) Token: 0x060065F6 RID: 26102 RVA: 0x0009B9FF File Offset: 0x00099BFF
			public virtual MultiValuedProperty<SmtpDomain> HybridDomains
			{
				set
				{
					base.PowerSharpParameters["HybridDomains"] = value;
				}
			}

			// Token: 0x17004196 RID: 16790
			// (set) Token: 0x060065F7 RID: 26103 RVA: 0x0009BA12 File Offset: 0x00099C12
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004197 RID: 16791
			// (set) Token: 0x060065F8 RID: 26104 RVA: 0x0009BA25 File Offset: 0x00099C25
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004198 RID: 16792
			// (set) Token: 0x060065F9 RID: 26105 RVA: 0x0009BA3D File Offset: 0x00099C3D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004199 RID: 16793
			// (set) Token: 0x060065FA RID: 26106 RVA: 0x0009BA55 File Offset: 0x00099C55
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700419A RID: 16794
			// (set) Token: 0x060065FB RID: 26107 RVA: 0x0009BA6D File Offset: 0x00099C6D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700419B RID: 16795
			// (set) Token: 0x060065FC RID: 26108 RVA: 0x0009BA85 File Offset: 0x00099C85
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200080D RID: 2061
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700419C RID: 16796
			// (set) Token: 0x060065FE RID: 26110 RVA: 0x0009BAA5 File Offset: 0x00099CA5
			public virtual OnPremisesOrganizationIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700419D RID: 16797
			// (set) Token: 0x060065FF RID: 26111 RVA: 0x0009BAB8 File Offset: 0x00099CB8
			public virtual InboundConnectorIdParameter InboundConnector
			{
				set
				{
					base.PowerSharpParameters["InboundConnector"] = value;
				}
			}

			// Token: 0x1700419E RID: 16798
			// (set) Token: 0x06006600 RID: 26112 RVA: 0x0009BACB File Offset: 0x00099CCB
			public virtual OutboundConnectorIdParameter OutboundConnector
			{
				set
				{
					base.PowerSharpParameters["OutboundConnector"] = value;
				}
			}

			// Token: 0x1700419F RID: 16799
			// (set) Token: 0x06006601 RID: 26113 RVA: 0x0009BADE File Offset: 0x00099CDE
			public virtual string OrganizationName
			{
				set
				{
					base.PowerSharpParameters["OrganizationName"] = value;
				}
			}

			// Token: 0x170041A0 RID: 16800
			// (set) Token: 0x06006602 RID: 26114 RVA: 0x0009BAF1 File Offset: 0x00099CF1
			public virtual OrganizationRelationshipIdParameter OrganizationRelationship
			{
				set
				{
					base.PowerSharpParameters["OrganizationRelationship"] = value;
				}
			}

			// Token: 0x170041A1 RID: 16801
			// (set) Token: 0x06006603 RID: 26115 RVA: 0x0009BB04 File Offset: 0x00099D04
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170041A2 RID: 16802
			// (set) Token: 0x06006604 RID: 26116 RVA: 0x0009BB17 File Offset: 0x00099D17
			public virtual MultiValuedProperty<SmtpDomain> HybridDomains
			{
				set
				{
					base.PowerSharpParameters["HybridDomains"] = value;
				}
			}

			// Token: 0x170041A3 RID: 16803
			// (set) Token: 0x06006605 RID: 26117 RVA: 0x0009BB2A File Offset: 0x00099D2A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170041A4 RID: 16804
			// (set) Token: 0x06006606 RID: 26118 RVA: 0x0009BB3D File Offset: 0x00099D3D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170041A5 RID: 16805
			// (set) Token: 0x06006607 RID: 26119 RVA: 0x0009BB55 File Offset: 0x00099D55
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170041A6 RID: 16806
			// (set) Token: 0x06006608 RID: 26120 RVA: 0x0009BB6D File Offset: 0x00099D6D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170041A7 RID: 16807
			// (set) Token: 0x06006609 RID: 26121 RVA: 0x0009BB85 File Offset: 0x00099D85
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170041A8 RID: 16808
			// (set) Token: 0x0600660A RID: 26122 RVA: 0x0009BB9D File Offset: 0x00099D9D
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
