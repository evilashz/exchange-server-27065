using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200062F RID: 1583
	public class GetExchangeServerCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x0600508F RID: 20623 RVA: 0x0007FA5F File Offset: 0x0007DC5F
		private GetExchangeServerCommand() : base("Get-ExchangeServer")
		{
		}

		// Token: 0x06005090 RID: 20624 RVA: 0x0007FA6C File Offset: 0x0007DC6C
		public GetExchangeServerCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005091 RID: 20625 RVA: 0x0007FA7B File Offset: 0x0007DC7B
		public virtual GetExchangeServerCommand SetParameters(GetExchangeServerCommand.DomainParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005092 RID: 20626 RVA: 0x0007FA85 File Offset: 0x0007DC85
		public virtual GetExchangeServerCommand SetParameters(GetExchangeServerCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005093 RID: 20627 RVA: 0x0007FA8F File Offset: 0x0007DC8F
		public virtual GetExchangeServerCommand SetParameters(GetExchangeServerCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000630 RID: 1584
		public class DomainParameters : ParametersBase
		{
			// Token: 0x17002FEA RID: 12266
			// (set) Token: 0x06005094 RID: 20628 RVA: 0x0007FA99 File Offset: 0x0007DC99
			public virtual Fqdn Domain
			{
				set
				{
					base.PowerSharpParameters["Domain"] = value;
				}
			}

			// Token: 0x17002FEB RID: 12267
			// (set) Token: 0x06005095 RID: 20629 RVA: 0x0007FAAC File Offset: 0x0007DCAC
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002FEC RID: 12268
			// (set) Token: 0x06005096 RID: 20630 RVA: 0x0007FAC4 File Offset: 0x0007DCC4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FED RID: 12269
			// (set) Token: 0x06005097 RID: 20631 RVA: 0x0007FAD7 File Offset: 0x0007DCD7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FEE RID: 12270
			// (set) Token: 0x06005098 RID: 20632 RVA: 0x0007FAEF File Offset: 0x0007DCEF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FEF RID: 12271
			// (set) Token: 0x06005099 RID: 20633 RVA: 0x0007FB07 File Offset: 0x0007DD07
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FF0 RID: 12272
			// (set) Token: 0x0600509A RID: 20634 RVA: 0x0007FB1F File Offset: 0x0007DD1F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000631 RID: 1585
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002FF1 RID: 12273
			// (set) Token: 0x0600509C RID: 20636 RVA: 0x0007FB3F File Offset: 0x0007DD3F
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002FF2 RID: 12274
			// (set) Token: 0x0600509D RID: 20637 RVA: 0x0007FB57 File Offset: 0x0007DD57
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FF3 RID: 12275
			// (set) Token: 0x0600509E RID: 20638 RVA: 0x0007FB6A File Offset: 0x0007DD6A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FF4 RID: 12276
			// (set) Token: 0x0600509F RID: 20639 RVA: 0x0007FB82 File Offset: 0x0007DD82
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FF5 RID: 12277
			// (set) Token: 0x060050A0 RID: 20640 RVA: 0x0007FB9A File Offset: 0x0007DD9A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FF6 RID: 12278
			// (set) Token: 0x060050A1 RID: 20641 RVA: 0x0007FBB2 File Offset: 0x0007DDB2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000632 RID: 1586
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002FF7 RID: 12279
			// (set) Token: 0x060050A3 RID: 20643 RVA: 0x0007FBD2 File Offset: 0x0007DDD2
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002FF8 RID: 12280
			// (set) Token: 0x060050A4 RID: 20644 RVA: 0x0007FBE5 File Offset: 0x0007DDE5
			public virtual SwitchParameter Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17002FF9 RID: 12281
			// (set) Token: 0x060050A5 RID: 20645 RVA: 0x0007FBFD File Offset: 0x0007DDFD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002FFA RID: 12282
			// (set) Token: 0x060050A6 RID: 20646 RVA: 0x0007FC10 File Offset: 0x0007DE10
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002FFB RID: 12283
			// (set) Token: 0x060050A7 RID: 20647 RVA: 0x0007FC28 File Offset: 0x0007DE28
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002FFC RID: 12284
			// (set) Token: 0x060050A8 RID: 20648 RVA: 0x0007FC40 File Offset: 0x0007DE40
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002FFD RID: 12285
			// (set) Token: 0x060050A9 RID: 20649 RVA: 0x0007FC58 File Offset: 0x0007DE58
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
