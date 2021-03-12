using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B1E RID: 2846
	public class EnableUMIPGatewayCommand : SyntheticCommandWithPipelineInput<UMIPGateway, UMIPGateway>
	{
		// Token: 0x06008B51 RID: 35665 RVA: 0x000CC9A9 File Offset: 0x000CABA9
		private EnableUMIPGatewayCommand() : base("Enable-UMIPGateway")
		{
		}

		// Token: 0x06008B52 RID: 35666 RVA: 0x000CC9B6 File Offset: 0x000CABB6
		public EnableUMIPGatewayCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008B53 RID: 35667 RVA: 0x000CC9C5 File Offset: 0x000CABC5
		public virtual EnableUMIPGatewayCommand SetParameters(EnableUMIPGatewayCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008B54 RID: 35668 RVA: 0x000CC9CF File Offset: 0x000CABCF
		public virtual EnableUMIPGatewayCommand SetParameters(EnableUMIPGatewayCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B1F RID: 2847
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170060CE RID: 24782
			// (set) Token: 0x06008B55 RID: 35669 RVA: 0x000CC9D9 File Offset: 0x000CABD9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060CF RID: 24783
			// (set) Token: 0x06008B56 RID: 35670 RVA: 0x000CC9EC File Offset: 0x000CABEC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060D0 RID: 24784
			// (set) Token: 0x06008B57 RID: 35671 RVA: 0x000CCA04 File Offset: 0x000CAC04
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060D1 RID: 24785
			// (set) Token: 0x06008B58 RID: 35672 RVA: 0x000CCA1C File Offset: 0x000CAC1C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060D2 RID: 24786
			// (set) Token: 0x06008B59 RID: 35673 RVA: 0x000CCA34 File Offset: 0x000CAC34
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060D3 RID: 24787
			// (set) Token: 0x06008B5A RID: 35674 RVA: 0x000CCA4C File Offset: 0x000CAC4C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B20 RID: 2848
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170060D4 RID: 24788
			// (set) Token: 0x06008B5C RID: 35676 RVA: 0x000CCA6C File Offset: 0x000CAC6C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new UMIPGatewayIdParameter(value) : null);
				}
			}

			// Token: 0x170060D5 RID: 24789
			// (set) Token: 0x06008B5D RID: 35677 RVA: 0x000CCA8A File Offset: 0x000CAC8A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170060D6 RID: 24790
			// (set) Token: 0x06008B5E RID: 35678 RVA: 0x000CCA9D File Offset: 0x000CAC9D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170060D7 RID: 24791
			// (set) Token: 0x06008B5F RID: 35679 RVA: 0x000CCAB5 File Offset: 0x000CACB5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170060D8 RID: 24792
			// (set) Token: 0x06008B60 RID: 35680 RVA: 0x000CCACD File Offset: 0x000CACCD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170060D9 RID: 24793
			// (set) Token: 0x06008B61 RID: 35681 RVA: 0x000CCAE5 File Offset: 0x000CACE5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170060DA RID: 24794
			// (set) Token: 0x06008B62 RID: 35682 RVA: 0x000CCAFD File Offset: 0x000CACFD
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
