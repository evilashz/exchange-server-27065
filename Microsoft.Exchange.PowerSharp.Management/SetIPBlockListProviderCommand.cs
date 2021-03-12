using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200076D RID: 1901
	public class SetIPBlockListProviderCommand : SyntheticCommandWithPipelineInputNoOutput<IPBlockListProvider>
	{
		// Token: 0x06006064 RID: 24676 RVA: 0x00094955 File Offset: 0x00092B55
		private SetIPBlockListProviderCommand() : base("Set-IPBlockListProvider")
		{
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x00094962 File Offset: 0x00092B62
		public SetIPBlockListProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x00094971 File Offset: 0x00092B71
		public virtual SetIPBlockListProviderCommand SetParameters(SetIPBlockListProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x0009497B File Offset: 0x00092B7B
		public virtual SetIPBlockListProviderCommand SetParameters(SetIPBlockListProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200076E RID: 1902
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003D43 RID: 15683
			// (set) Token: 0x06006068 RID: 24680 RVA: 0x00094985 File Offset: 0x00092B85
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D44 RID: 15684
			// (set) Token: 0x06006069 RID: 24681 RVA: 0x00094998 File Offset: 0x00092B98
			public virtual AsciiString RejectionResponse
			{
				set
				{
					base.PowerSharpParameters["RejectionResponse"] = value;
				}
			}

			// Token: 0x17003D45 RID: 15685
			// (set) Token: 0x0600606A RID: 24682 RVA: 0x000949AB File Offset: 0x00092BAB
			public virtual SmtpDomain LookupDomain
			{
				set
				{
					base.PowerSharpParameters["LookupDomain"] = value;
				}
			}

			// Token: 0x17003D46 RID: 15686
			// (set) Token: 0x0600606B RID: 24683 RVA: 0x000949BE File Offset: 0x00092BBE
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003D47 RID: 15687
			// (set) Token: 0x0600606C RID: 24684 RVA: 0x000949D6 File Offset: 0x00092BD6
			public virtual bool AnyMatch
			{
				set
				{
					base.PowerSharpParameters["AnyMatch"] = value;
				}
			}

			// Token: 0x17003D48 RID: 15688
			// (set) Token: 0x0600606D RID: 24685 RVA: 0x000949EE File Offset: 0x00092BEE
			public virtual IPAddress BitmaskMatch
			{
				set
				{
					base.PowerSharpParameters["BitmaskMatch"] = value;
				}
			}

			// Token: 0x17003D49 RID: 15689
			// (set) Token: 0x0600606E RID: 24686 RVA: 0x00094A01 File Offset: 0x00092C01
			public virtual MultiValuedProperty<IPAddress> IPAddressesMatch
			{
				set
				{
					base.PowerSharpParameters["IPAddressesMatch"] = value;
				}
			}

			// Token: 0x17003D4A RID: 15690
			// (set) Token: 0x0600606F RID: 24687 RVA: 0x00094A14 File Offset: 0x00092C14
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003D4B RID: 15691
			// (set) Token: 0x06006070 RID: 24688 RVA: 0x00094A2C File Offset: 0x00092C2C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003D4C RID: 15692
			// (set) Token: 0x06006071 RID: 24689 RVA: 0x00094A3F File Offset: 0x00092C3F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D4D RID: 15693
			// (set) Token: 0x06006072 RID: 24690 RVA: 0x00094A57 File Offset: 0x00092C57
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D4E RID: 15694
			// (set) Token: 0x06006073 RID: 24691 RVA: 0x00094A6F File Offset: 0x00092C6F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D4F RID: 15695
			// (set) Token: 0x06006074 RID: 24692 RVA: 0x00094A87 File Offset: 0x00092C87
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003D50 RID: 15696
			// (set) Token: 0x06006075 RID: 24693 RVA: 0x00094A9F File Offset: 0x00092C9F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200076F RID: 1903
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003D51 RID: 15697
			// (set) Token: 0x06006077 RID: 24695 RVA: 0x00094ABF File Offset: 0x00092CBF
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPBlockListProviderIdParameter(value) : null);
				}
			}

			// Token: 0x17003D52 RID: 15698
			// (set) Token: 0x06006078 RID: 24696 RVA: 0x00094ADD File Offset: 0x00092CDD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003D53 RID: 15699
			// (set) Token: 0x06006079 RID: 24697 RVA: 0x00094AF0 File Offset: 0x00092CF0
			public virtual AsciiString RejectionResponse
			{
				set
				{
					base.PowerSharpParameters["RejectionResponse"] = value;
				}
			}

			// Token: 0x17003D54 RID: 15700
			// (set) Token: 0x0600607A RID: 24698 RVA: 0x00094B03 File Offset: 0x00092D03
			public virtual SmtpDomain LookupDomain
			{
				set
				{
					base.PowerSharpParameters["LookupDomain"] = value;
				}
			}

			// Token: 0x17003D55 RID: 15701
			// (set) Token: 0x0600607B RID: 24699 RVA: 0x00094B16 File Offset: 0x00092D16
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x17003D56 RID: 15702
			// (set) Token: 0x0600607C RID: 24700 RVA: 0x00094B2E File Offset: 0x00092D2E
			public virtual bool AnyMatch
			{
				set
				{
					base.PowerSharpParameters["AnyMatch"] = value;
				}
			}

			// Token: 0x17003D57 RID: 15703
			// (set) Token: 0x0600607D RID: 24701 RVA: 0x00094B46 File Offset: 0x00092D46
			public virtual IPAddress BitmaskMatch
			{
				set
				{
					base.PowerSharpParameters["BitmaskMatch"] = value;
				}
			}

			// Token: 0x17003D58 RID: 15704
			// (set) Token: 0x0600607E RID: 24702 RVA: 0x00094B59 File Offset: 0x00092D59
			public virtual MultiValuedProperty<IPAddress> IPAddressesMatch
			{
				set
				{
					base.PowerSharpParameters["IPAddressesMatch"] = value;
				}
			}

			// Token: 0x17003D59 RID: 15705
			// (set) Token: 0x0600607F RID: 24703 RVA: 0x00094B6C File Offset: 0x00092D6C
			public virtual int Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17003D5A RID: 15706
			// (set) Token: 0x06006080 RID: 24704 RVA: 0x00094B84 File Offset: 0x00092D84
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17003D5B RID: 15707
			// (set) Token: 0x06006081 RID: 24705 RVA: 0x00094B97 File Offset: 0x00092D97
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003D5C RID: 15708
			// (set) Token: 0x06006082 RID: 24706 RVA: 0x00094BAF File Offset: 0x00092DAF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003D5D RID: 15709
			// (set) Token: 0x06006083 RID: 24707 RVA: 0x00094BC7 File Offset: 0x00092DC7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003D5E RID: 15710
			// (set) Token: 0x06006084 RID: 24708 RVA: 0x00094BDF File Offset: 0x00092DDF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003D5F RID: 15711
			// (set) Token: 0x06006085 RID: 24709 RVA: 0x00094BF7 File Offset: 0x00092DF7
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
