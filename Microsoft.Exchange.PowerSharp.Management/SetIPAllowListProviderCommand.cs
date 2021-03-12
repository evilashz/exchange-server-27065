using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200075A RID: 1882
	public class SetIPAllowListProviderCommand : SyntheticCommandWithPipelineInputNoOutput<IPAllowListProvider>
	{
		// Token: 0x06005FDD RID: 24541 RVA: 0x00093F07 File Offset: 0x00092107
		private SetIPAllowListProviderCommand() : base("Set-IPAllowListProvider")
		{
		}

		// Token: 0x06005FDE RID: 24542 RVA: 0x00093F14 File Offset: 0x00092114
		public SetIPAllowListProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005FDF RID: 24543 RVA: 0x00093F23 File Offset: 0x00092123
		public virtual SetIPAllowListProviderCommand SetParameters(SetIPAllowListProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005FE0 RID: 24544 RVA: 0x00093F2D File Offset: 0x0009212D
		public virtual SetIPAllowListProviderCommand SetParameters(SetIPAllowListProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200075B RID: 1883
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003CE2 RID: 15586
			// (set) Token: 0x06005FE1 RID: 24545 RVA: 0x00093F37 File Offset: 0x00092137
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CE3 RID: 15587
			// (set) Token: 0x06005FE2 RID: 24546 RVA: 0x00093F4A File Offset: 0x0009214A
			public virtual SmtpDomain LookupDomain
			{
				set
				{
					base.PowerSharpParameters["LookupDomain"] = value;
				}
			}

			// Token: 0x17003CE4 RID: 15588
			// (set) Token: 0x06005FE3 RID: 24547 RVA: 0x00093F5D File Offset: 0x0009215D
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003CE5 RID: 15589
			// (set) Token: 0x06005FE4 RID: 24548 RVA: 0x00093F75 File Offset: 0x00092175
			public virtual bool AnyMatch
			{
				set
				{
					base.PowerSharpParameters["AnyMatch"] = value;
				}
			}

			// Token: 0x17003CE6 RID: 15590
			// (set) Token: 0x06005FE5 RID: 24549 RVA: 0x00093F8D File Offset: 0x0009218D
			public virtual IPAddress BitmaskMatch
			{
				set
				{
					base.PowerSharpParameters["BitmaskMatch"] = value;
				}
			}

			// Token: 0x17003CE7 RID: 15591
			// (set) Token: 0x06005FE6 RID: 24550 RVA: 0x00093FA0 File Offset: 0x000921A0
			public virtual MultiValuedProperty<IPAddress> IPAddressesMatch
			{
				set
				{
					base.PowerSharpParameters["IPAddressesMatch"] = value;
				}
			}

			// Token: 0x17003CE8 RID: 15592
			// (set) Token: 0x06005FE7 RID: 24551 RVA: 0x00093FB3 File Offset: 0x000921B3
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003CE9 RID: 15593
			// (set) Token: 0x06005FE8 RID: 24552 RVA: 0x00093FCB File Offset: 0x000921CB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003CEA RID: 15594
			// (set) Token: 0x06005FE9 RID: 24553 RVA: 0x00093FDE File Offset: 0x000921DE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CEB RID: 15595
			// (set) Token: 0x06005FEA RID: 24554 RVA: 0x00093FF6 File Offset: 0x000921F6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CEC RID: 15596
			// (set) Token: 0x06005FEB RID: 24555 RVA: 0x0009400E File Offset: 0x0009220E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CED RID: 15597
			// (set) Token: 0x06005FEC RID: 24556 RVA: 0x00094026 File Offset: 0x00092226
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003CEE RID: 15598
			// (set) Token: 0x06005FED RID: 24557 RVA: 0x0009403E File Offset: 0x0009223E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200075C RID: 1884
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003CEF RID: 15599
			// (set) Token: 0x06005FEF RID: 24559 RVA: 0x0009405E File Offset: 0x0009225E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPAllowListProviderIdParameter(value) : null);
				}
			}

			// Token: 0x17003CF0 RID: 15600
			// (set) Token: 0x06005FF0 RID: 24560 RVA: 0x0009407C File Offset: 0x0009227C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CF1 RID: 15601
			// (set) Token: 0x06005FF1 RID: 24561 RVA: 0x0009408F File Offset: 0x0009228F
			public virtual SmtpDomain LookupDomain
			{
				set
				{
					base.PowerSharpParameters["LookupDomain"] = value;
				}
			}

			// Token: 0x17003CF2 RID: 15602
			// (set) Token: 0x06005FF2 RID: 24562 RVA: 0x000940A2 File Offset: 0x000922A2
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003CF3 RID: 15603
			// (set) Token: 0x06005FF3 RID: 24563 RVA: 0x000940BA File Offset: 0x000922BA
			public virtual bool AnyMatch
			{
				set
				{
					base.PowerSharpParameters["AnyMatch"] = value;
				}
			}

			// Token: 0x17003CF4 RID: 15604
			// (set) Token: 0x06005FF4 RID: 24564 RVA: 0x000940D2 File Offset: 0x000922D2
			public virtual IPAddress BitmaskMatch
			{
				set
				{
					base.PowerSharpParameters["BitmaskMatch"] = value;
				}
			}

			// Token: 0x17003CF5 RID: 15605
			// (set) Token: 0x06005FF5 RID: 24565 RVA: 0x000940E5 File Offset: 0x000922E5
			public virtual MultiValuedProperty<IPAddress> IPAddressesMatch
			{
				set
				{
					base.PowerSharpParameters["IPAddressesMatch"] = value;
				}
			}

			// Token: 0x17003CF6 RID: 15606
			// (set) Token: 0x06005FF6 RID: 24566 RVA: 0x000940F8 File Offset: 0x000922F8
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003CF7 RID: 15607
			// (set) Token: 0x06005FF7 RID: 24567 RVA: 0x00094110 File Offset: 0x00092310
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003CF8 RID: 15608
			// (set) Token: 0x06005FF8 RID: 24568 RVA: 0x00094123 File Offset: 0x00092323
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CF9 RID: 15609
			// (set) Token: 0x06005FF9 RID: 24569 RVA: 0x0009413B File Offset: 0x0009233B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CFA RID: 15610
			// (set) Token: 0x06005FFA RID: 24570 RVA: 0x00094153 File Offset: 0x00092353
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CFB RID: 15611
			// (set) Token: 0x06005FFB RID: 24571 RVA: 0x0009416B File Offset: 0x0009236B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003CFC RID: 15612
			// (set) Token: 0x06005FFC RID: 24572 RVA: 0x00094183 File Offset: 0x00092383
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
