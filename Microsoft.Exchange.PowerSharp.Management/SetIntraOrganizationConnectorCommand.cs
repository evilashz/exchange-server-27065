using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006DC RID: 1756
	public class SetIntraOrganizationConnectorCommand : SyntheticCommandWithPipelineInputNoOutput<IntraOrganizationConnector>
	{
		// Token: 0x06005B9F RID: 23455 RVA: 0x0008E891 File Offset: 0x0008CA91
		private SetIntraOrganizationConnectorCommand() : base("Set-IntraOrganizationConnector")
		{
		}

		// Token: 0x06005BA0 RID: 23456 RVA: 0x0008E89E File Offset: 0x0008CA9E
		public SetIntraOrganizationConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x0008E8AD File Offset: 0x0008CAAD
		public virtual SetIntraOrganizationConnectorCommand SetParameters(SetIntraOrganizationConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x0008E8B7 File Offset: 0x0008CAB7
		public virtual SetIntraOrganizationConnectorCommand SetParameters(SetIntraOrganizationConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006DD RID: 1757
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170039A0 RID: 14752
			// (set) Token: 0x06005BA3 RID: 23459 RVA: 0x0008E8C1 File Offset: 0x0008CAC1
			public virtual MultiValuedProperty<SmtpDomain> TargetAddressDomains
			{
				set
				{
					base.PowerSharpParameters["TargetAddressDomains"] = value;
				}
			}

			// Token: 0x170039A1 RID: 14753
			// (set) Token: 0x06005BA4 RID: 23460 RVA: 0x0008E8D4 File Offset: 0x0008CAD4
			public virtual Uri DiscoveryEndpoint
			{
				set
				{
					base.PowerSharpParameters["DiscoveryEndpoint"] = value;
				}
			}

			// Token: 0x170039A2 RID: 14754
			// (set) Token: 0x06005BA5 RID: 23461 RVA: 0x0008E8E7 File Offset: 0x0008CAE7
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170039A3 RID: 14755
			// (set) Token: 0x06005BA6 RID: 23462 RVA: 0x0008E8FF File Offset: 0x0008CAFF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039A4 RID: 14756
			// (set) Token: 0x06005BA7 RID: 23463 RVA: 0x0008E912 File Offset: 0x0008CB12
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170039A5 RID: 14757
			// (set) Token: 0x06005BA8 RID: 23464 RVA: 0x0008E925 File Offset: 0x0008CB25
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039A6 RID: 14758
			// (set) Token: 0x06005BA9 RID: 23465 RVA: 0x0008E93D File Offset: 0x0008CB3D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039A7 RID: 14759
			// (set) Token: 0x06005BAA RID: 23466 RVA: 0x0008E955 File Offset: 0x0008CB55
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039A8 RID: 14760
			// (set) Token: 0x06005BAB RID: 23467 RVA: 0x0008E96D File Offset: 0x0008CB6D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039A9 RID: 14761
			// (set) Token: 0x06005BAC RID: 23468 RVA: 0x0008E985 File Offset: 0x0008CB85
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006DE RID: 1758
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170039AA RID: 14762
			// (set) Token: 0x06005BAE RID: 23470 RVA: 0x0008E9A5 File Offset: 0x0008CBA5
			public virtual IntraOrganizationConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170039AB RID: 14763
			// (set) Token: 0x06005BAF RID: 23471 RVA: 0x0008E9B8 File Offset: 0x0008CBB8
			public virtual MultiValuedProperty<SmtpDomain> TargetAddressDomains
			{
				set
				{
					base.PowerSharpParameters["TargetAddressDomains"] = value;
				}
			}

			// Token: 0x170039AC RID: 14764
			// (set) Token: 0x06005BB0 RID: 23472 RVA: 0x0008E9CB File Offset: 0x0008CBCB
			public virtual Uri DiscoveryEndpoint
			{
				set
				{
					base.PowerSharpParameters["DiscoveryEndpoint"] = value;
				}
			}

			// Token: 0x170039AD RID: 14765
			// (set) Token: 0x06005BB1 RID: 23473 RVA: 0x0008E9DE File Offset: 0x0008CBDE
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170039AE RID: 14766
			// (set) Token: 0x06005BB2 RID: 23474 RVA: 0x0008E9F6 File Offset: 0x0008CBF6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039AF RID: 14767
			// (set) Token: 0x06005BB3 RID: 23475 RVA: 0x0008EA09 File Offset: 0x0008CC09
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170039B0 RID: 14768
			// (set) Token: 0x06005BB4 RID: 23476 RVA: 0x0008EA1C File Offset: 0x0008CC1C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039B1 RID: 14769
			// (set) Token: 0x06005BB5 RID: 23477 RVA: 0x0008EA34 File Offset: 0x0008CC34
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039B2 RID: 14770
			// (set) Token: 0x06005BB6 RID: 23478 RVA: 0x0008EA4C File Offset: 0x0008CC4C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039B3 RID: 14771
			// (set) Token: 0x06005BB7 RID: 23479 RVA: 0x0008EA64 File Offset: 0x0008CC64
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039B4 RID: 14772
			// (set) Token: 0x06005BB8 RID: 23480 RVA: 0x0008EA7C File Offset: 0x0008CC7C
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
