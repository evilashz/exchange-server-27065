using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200084F RID: 2127
	public class SetResourceConfigCommand : SyntheticCommandWithPipelineInputNoOutput<ResourceBookingConfig>
	{
		// Token: 0x060069E9 RID: 27113 RVA: 0x000A0DDB File Offset: 0x0009EFDB
		private SetResourceConfigCommand() : base("Set-ResourceConfig")
		{
		}

		// Token: 0x060069EA RID: 27114 RVA: 0x000A0DE8 File Offset: 0x0009EFE8
		public SetResourceConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060069EB RID: 27115 RVA: 0x000A0DF7 File Offset: 0x0009EFF7
		public virtual SetResourceConfigCommand SetParameters(SetResourceConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060069EC RID: 27116 RVA: 0x000A0E01 File Offset: 0x0009F001
		public virtual SetResourceConfigCommand SetParameters(SetResourceConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000850 RID: 2128
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004504 RID: 17668
			// (set) Token: 0x060069ED RID: 27117 RVA: 0x000A0E0B File Offset: 0x0009F00B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004505 RID: 17669
			// (set) Token: 0x060069EE RID: 27118 RVA: 0x000A0E29 File Offset: 0x0009F029
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004506 RID: 17670
			// (set) Token: 0x060069EF RID: 27119 RVA: 0x000A0E3C File Offset: 0x0009F03C
			public virtual MultiValuedProperty<string> ResourcePropertySchema
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertySchema"] = value;
				}
			}

			// Token: 0x17004507 RID: 17671
			// (set) Token: 0x060069F0 RID: 27120 RVA: 0x000A0E4F File Offset: 0x0009F04F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004508 RID: 17672
			// (set) Token: 0x060069F1 RID: 27121 RVA: 0x000A0E67 File Offset: 0x0009F067
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004509 RID: 17673
			// (set) Token: 0x060069F2 RID: 27122 RVA: 0x000A0E7F File Offset: 0x0009F07F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700450A RID: 17674
			// (set) Token: 0x060069F3 RID: 27123 RVA: 0x000A0E97 File Offset: 0x0009F097
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700450B RID: 17675
			// (set) Token: 0x060069F4 RID: 27124 RVA: 0x000A0EAF File Offset: 0x0009F0AF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000851 RID: 2129
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700450C RID: 17676
			// (set) Token: 0x060069F6 RID: 27126 RVA: 0x000A0ECF File Offset: 0x0009F0CF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700450D RID: 17677
			// (set) Token: 0x060069F7 RID: 27127 RVA: 0x000A0EE2 File Offset: 0x0009F0E2
			public virtual MultiValuedProperty<string> ResourcePropertySchema
			{
				set
				{
					base.PowerSharpParameters["ResourcePropertySchema"] = value;
				}
			}

			// Token: 0x1700450E RID: 17678
			// (set) Token: 0x060069F8 RID: 27128 RVA: 0x000A0EF5 File Offset: 0x0009F0F5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700450F RID: 17679
			// (set) Token: 0x060069F9 RID: 27129 RVA: 0x000A0F0D File Offset: 0x0009F10D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004510 RID: 17680
			// (set) Token: 0x060069FA RID: 27130 RVA: 0x000A0F25 File Offset: 0x0009F125
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004511 RID: 17681
			// (set) Token: 0x060069FB RID: 27131 RVA: 0x000A0F3D File Offset: 0x0009F13D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004512 RID: 17682
			// (set) Token: 0x060069FC RID: 27132 RVA: 0x000A0F55 File Offset: 0x0009F155
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
