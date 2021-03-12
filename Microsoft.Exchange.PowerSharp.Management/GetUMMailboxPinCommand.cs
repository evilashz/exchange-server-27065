using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B57 RID: 2903
	public class GetUMMailboxPinCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06008D12 RID: 36114 RVA: 0x000CED3D File Offset: 0x000CCF3D
		private GetUMMailboxPinCommand() : base("Get-UMMailboxPin")
		{
		}

		// Token: 0x06008D13 RID: 36115 RVA: 0x000CED4A File Offset: 0x000CCF4A
		public GetUMMailboxPinCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008D14 RID: 36116 RVA: 0x000CED59 File Offset: 0x000CCF59
		public virtual GetUMMailboxPinCommand SetParameters(GetUMMailboxPinCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008D15 RID: 36117 RVA: 0x000CED63 File Offset: 0x000CCF63
		public virtual GetUMMailboxPinCommand SetParameters(GetUMMailboxPinCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B58 RID: 2904
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700621D RID: 25117
			// (set) Token: 0x06008D16 RID: 36118 RVA: 0x000CED6D File Offset: 0x000CCF6D
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700621E RID: 25118
			// (set) Token: 0x06008D17 RID: 36119 RVA: 0x000CED85 File Offset: 0x000CCF85
			public virtual SwitchParameter IgnoreErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreErrors"] = value;
				}
			}

			// Token: 0x1700621F RID: 25119
			// (set) Token: 0x06008D18 RID: 36120 RVA: 0x000CED9D File Offset: 0x000CCF9D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006220 RID: 25120
			// (set) Token: 0x06008D19 RID: 36121 RVA: 0x000CEDBB File Offset: 0x000CCFBB
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x17006221 RID: 25121
			// (set) Token: 0x06008D1A RID: 36122 RVA: 0x000CEDCE File Offset: 0x000CCFCE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006222 RID: 25122
			// (set) Token: 0x06008D1B RID: 36123 RVA: 0x000CEDE6 File Offset: 0x000CCFE6
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006223 RID: 25123
			// (set) Token: 0x06008D1C RID: 36124 RVA: 0x000CEDFE File Offset: 0x000CCFFE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006224 RID: 25124
			// (set) Token: 0x06008D1D RID: 36125 RVA: 0x000CEE11 File Offset: 0x000CD011
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006225 RID: 25125
			// (set) Token: 0x06008D1E RID: 36126 RVA: 0x000CEE29 File Offset: 0x000CD029
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006226 RID: 25126
			// (set) Token: 0x06008D1F RID: 36127 RVA: 0x000CEE41 File Offset: 0x000CD041
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006227 RID: 25127
			// (set) Token: 0x06008D20 RID: 36128 RVA: 0x000CEE59 File Offset: 0x000CD059
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000B59 RID: 2905
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006228 RID: 25128
			// (set) Token: 0x06008D22 RID: 36130 RVA: 0x000CEE79 File Offset: 0x000CD079
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006229 RID: 25129
			// (set) Token: 0x06008D23 RID: 36131 RVA: 0x000CEE97 File Offset: 0x000CD097
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700622A RID: 25130
			// (set) Token: 0x06008D24 RID: 36132 RVA: 0x000CEEAF File Offset: 0x000CD0AF
			public virtual SwitchParameter IgnoreErrors
			{
				set
				{
					base.PowerSharpParameters["IgnoreErrors"] = value;
				}
			}

			// Token: 0x1700622B RID: 25131
			// (set) Token: 0x06008D25 RID: 36133 RVA: 0x000CEEC7 File Offset: 0x000CD0C7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700622C RID: 25132
			// (set) Token: 0x06008D26 RID: 36134 RVA: 0x000CEEE5 File Offset: 0x000CD0E5
			public virtual PSCredential Credential
			{
				set
				{
					base.PowerSharpParameters["Credential"] = value;
				}
			}

			// Token: 0x1700622D RID: 25133
			// (set) Token: 0x06008D27 RID: 36135 RVA: 0x000CEEF8 File Offset: 0x000CD0F8
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700622E RID: 25134
			// (set) Token: 0x06008D28 RID: 36136 RVA: 0x000CEF10 File Offset: 0x000CD110
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x1700622F RID: 25135
			// (set) Token: 0x06008D29 RID: 36137 RVA: 0x000CEF28 File Offset: 0x000CD128
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006230 RID: 25136
			// (set) Token: 0x06008D2A RID: 36138 RVA: 0x000CEF3B File Offset: 0x000CD13B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006231 RID: 25137
			// (set) Token: 0x06008D2B RID: 36139 RVA: 0x000CEF53 File Offset: 0x000CD153
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006232 RID: 25138
			// (set) Token: 0x06008D2C RID: 36140 RVA: 0x000CEF6B File Offset: 0x000CD16B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006233 RID: 25139
			// (set) Token: 0x06008D2D RID: 36141 RVA: 0x000CEF83 File Offset: 0x000CD183
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
