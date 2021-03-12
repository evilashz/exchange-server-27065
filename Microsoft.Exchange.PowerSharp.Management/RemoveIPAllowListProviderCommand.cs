using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000757 RID: 1879
	public class RemoveIPAllowListProviderCommand : SyntheticCommandWithPipelineInput<IPAllowListProvider, IPAllowListProvider>
	{
		// Token: 0x06005FC8 RID: 24520 RVA: 0x00093D63 File Offset: 0x00091F63
		private RemoveIPAllowListProviderCommand() : base("Remove-IPAllowListProvider")
		{
		}

		// Token: 0x06005FC9 RID: 24521 RVA: 0x00093D70 File Offset: 0x00091F70
		public RemoveIPAllowListProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005FCA RID: 24522 RVA: 0x00093D7F File Offset: 0x00091F7F
		public virtual RemoveIPAllowListProviderCommand SetParameters(RemoveIPAllowListProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005FCB RID: 24523 RVA: 0x00093D89 File Offset: 0x00091F89
		public virtual RemoveIPAllowListProviderCommand SetParameters(RemoveIPAllowListProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000758 RID: 1880
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003CD3 RID: 15571
			// (set) Token: 0x06005FCC RID: 24524 RVA: 0x00093D93 File Offset: 0x00091F93
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CD4 RID: 15572
			// (set) Token: 0x06005FCD RID: 24525 RVA: 0x00093DA6 File Offset: 0x00091FA6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CD5 RID: 15573
			// (set) Token: 0x06005FCE RID: 24526 RVA: 0x00093DBE File Offset: 0x00091FBE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CD6 RID: 15574
			// (set) Token: 0x06005FCF RID: 24527 RVA: 0x00093DD6 File Offset: 0x00091FD6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CD7 RID: 15575
			// (set) Token: 0x06005FD0 RID: 24528 RVA: 0x00093DEE File Offset: 0x00091FEE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003CD8 RID: 15576
			// (set) Token: 0x06005FD1 RID: 24529 RVA: 0x00093E06 File Offset: 0x00092006
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003CD9 RID: 15577
			// (set) Token: 0x06005FD2 RID: 24530 RVA: 0x00093E1E File Offset: 0x0009201E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000759 RID: 1881
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003CDA RID: 15578
			// (set) Token: 0x06005FD4 RID: 24532 RVA: 0x00093E3E File Offset: 0x0009203E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new IPAllowListProviderIdParameter(value) : null);
				}
			}

			// Token: 0x17003CDB RID: 15579
			// (set) Token: 0x06005FD5 RID: 24533 RVA: 0x00093E5C File Offset: 0x0009205C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003CDC RID: 15580
			// (set) Token: 0x06005FD6 RID: 24534 RVA: 0x00093E6F File Offset: 0x0009206F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003CDD RID: 15581
			// (set) Token: 0x06005FD7 RID: 24535 RVA: 0x00093E87 File Offset: 0x00092087
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003CDE RID: 15582
			// (set) Token: 0x06005FD8 RID: 24536 RVA: 0x00093E9F File Offset: 0x0009209F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003CDF RID: 15583
			// (set) Token: 0x06005FD9 RID: 24537 RVA: 0x00093EB7 File Offset: 0x000920B7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003CE0 RID: 15584
			// (set) Token: 0x06005FDA RID: 24538 RVA: 0x00093ECF File Offset: 0x000920CF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003CE1 RID: 15585
			// (set) Token: 0x06005FDB RID: 24539 RVA: 0x00093EE7 File Offset: 0x000920E7
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
