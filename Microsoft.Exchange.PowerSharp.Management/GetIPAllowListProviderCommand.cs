using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000750 RID: 1872
	public class GetIPAllowListProviderCommand : SyntheticCommandWithPipelineInput<IPAllowListProvider, IPAllowListProvider>
	{
		// Token: 0x06005F9D RID: 24477 RVA: 0x00093A31 File Offset: 0x00091C31
		private GetIPAllowListProviderCommand() : base("Get-IPAllowListProvider")
		{
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x00093A3E File Offset: 0x00091C3E
		public GetIPAllowListProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x00093A4D File Offset: 0x00091C4D
		public virtual GetIPAllowListProviderCommand SetParameters(GetIPAllowListProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x00093A57 File Offset: 0x00091C57
		public virtual GetIPAllowListProviderCommand SetParameters(GetIPAllowListProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000751 RID: 1873
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003CB6 RID: 15542
			// (set) Token: 0x06005FA1 RID: 24481 RVA: 0x00093A61 File Offset: 0x00091C61
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CB7 RID: 15543
			// (set) Token: 0x06005FA2 RID: 24482 RVA: 0x00093A74 File Offset: 0x00091C74
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CB8 RID: 15544
			// (set) Token: 0x06005FA3 RID: 24483 RVA: 0x00093A8C File Offset: 0x00091C8C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CB9 RID: 15545
			// (set) Token: 0x06005FA4 RID: 24484 RVA: 0x00093AA4 File Offset: 0x00091CA4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CBA RID: 15546
			// (set) Token: 0x06005FA5 RID: 24485 RVA: 0x00093ABC File Offset: 0x00091CBC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000752 RID: 1874
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003CBB RID: 15547
			// (set) Token: 0x06005FA7 RID: 24487 RVA: 0x00093ADC File Offset: 0x00091CDC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPAllowListProviderIdParameter(value) : null);
				}
			}

			// Token: 0x17003CBC RID: 15548
			// (set) Token: 0x06005FA8 RID: 24488 RVA: 0x00093AFA File Offset: 0x00091CFA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CBD RID: 15549
			// (set) Token: 0x06005FA9 RID: 24489 RVA: 0x00093B0D File Offset: 0x00091D0D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CBE RID: 15550
			// (set) Token: 0x06005FAA RID: 24490 RVA: 0x00093B25 File Offset: 0x00091D25
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CBF RID: 15551
			// (set) Token: 0x06005FAB RID: 24491 RVA: 0x00093B3D File Offset: 0x00091D3D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CC0 RID: 15552
			// (set) Token: 0x06005FAC RID: 24492 RVA: 0x00093B55 File Offset: 0x00091D55
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
