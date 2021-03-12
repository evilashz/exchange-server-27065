using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000846 RID: 2118
	public class RemoveEmailAddressPolicyCommand : SyntheticCommandWithPipelineInput<EmailAddressPolicy, EmailAddressPolicy>
	{
		// Token: 0x0600697A RID: 27002 RVA: 0x000A055F File Offset: 0x0009E75F
		private RemoveEmailAddressPolicyCommand() : base("Remove-EmailAddressPolicy")
		{
		}

		// Token: 0x0600697B RID: 27003 RVA: 0x000A056C File Offset: 0x0009E76C
		public RemoveEmailAddressPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600697C RID: 27004 RVA: 0x000A057B File Offset: 0x0009E77B
		public virtual RemoveEmailAddressPolicyCommand SetParameters(RemoveEmailAddressPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600697D RID: 27005 RVA: 0x000A0585 File Offset: 0x0009E785
		public virtual RemoveEmailAddressPolicyCommand SetParameters(RemoveEmailAddressPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000847 RID: 2119
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170044A7 RID: 17575
			// (set) Token: 0x0600697E RID: 27006 RVA: 0x000A058F File Offset: 0x0009E78F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170044A8 RID: 17576
			// (set) Token: 0x0600697F RID: 27007 RVA: 0x000A05A2 File Offset: 0x0009E7A2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170044A9 RID: 17577
			// (set) Token: 0x06006980 RID: 27008 RVA: 0x000A05BA File Offset: 0x0009E7BA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170044AA RID: 17578
			// (set) Token: 0x06006981 RID: 27009 RVA: 0x000A05D2 File Offset: 0x0009E7D2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170044AB RID: 17579
			// (set) Token: 0x06006982 RID: 27010 RVA: 0x000A05EA File Offset: 0x0009E7EA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170044AC RID: 17580
			// (set) Token: 0x06006983 RID: 27011 RVA: 0x000A0602 File Offset: 0x0009E802
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170044AD RID: 17581
			// (set) Token: 0x06006984 RID: 27012 RVA: 0x000A061A File Offset: 0x0009E81A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000848 RID: 2120
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170044AE RID: 17582
			// (set) Token: 0x06006986 RID: 27014 RVA: 0x000A063A File Offset: 0x0009E83A
			public virtual EmailAddressPolicyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170044AF RID: 17583
			// (set) Token: 0x06006987 RID: 27015 RVA: 0x000A064D File Offset: 0x0009E84D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170044B0 RID: 17584
			// (set) Token: 0x06006988 RID: 27016 RVA: 0x000A0660 File Offset: 0x0009E860
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170044B1 RID: 17585
			// (set) Token: 0x06006989 RID: 27017 RVA: 0x000A0678 File Offset: 0x0009E878
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170044B2 RID: 17586
			// (set) Token: 0x0600698A RID: 27018 RVA: 0x000A0690 File Offset: 0x0009E890
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170044B3 RID: 17587
			// (set) Token: 0x0600698B RID: 27019 RVA: 0x000A06A8 File Offset: 0x0009E8A8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170044B4 RID: 17588
			// (set) Token: 0x0600698C RID: 27020 RVA: 0x000A06C0 File Offset: 0x0009E8C0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170044B5 RID: 17589
			// (set) Token: 0x0600698D RID: 27021 RVA: 0x000A06D8 File Offset: 0x0009E8D8
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
