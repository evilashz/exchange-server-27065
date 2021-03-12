using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000694 RID: 1684
	public class RemoveFederationTrustCommand : SyntheticCommandWithPipelineInput<FederationTrust, FederationTrust>
	{
		// Token: 0x06005955 RID: 22869 RVA: 0x0008BAF0 File Offset: 0x00089CF0
		private RemoveFederationTrustCommand() : base("Remove-FederationTrust")
		{
		}

		// Token: 0x06005956 RID: 22870 RVA: 0x0008BAFD File Offset: 0x00089CFD
		public RemoveFederationTrustCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005957 RID: 22871 RVA: 0x0008BB0C File Offset: 0x00089D0C
		public virtual RemoveFederationTrustCommand SetParameters(RemoveFederationTrustCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005958 RID: 22872 RVA: 0x0008BB16 File Offset: 0x00089D16
		public virtual RemoveFederationTrustCommand SetParameters(RemoveFederationTrustCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000695 RID: 1685
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170037E6 RID: 14310
			// (set) Token: 0x06005959 RID: 22873 RVA: 0x0008BB20 File Offset: 0x00089D20
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037E7 RID: 14311
			// (set) Token: 0x0600595A RID: 22874 RVA: 0x0008BB33 File Offset: 0x00089D33
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037E8 RID: 14312
			// (set) Token: 0x0600595B RID: 22875 RVA: 0x0008BB4B File Offset: 0x00089D4B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037E9 RID: 14313
			// (set) Token: 0x0600595C RID: 22876 RVA: 0x0008BB63 File Offset: 0x00089D63
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037EA RID: 14314
			// (set) Token: 0x0600595D RID: 22877 RVA: 0x0008BB7B File Offset: 0x00089D7B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037EB RID: 14315
			// (set) Token: 0x0600595E RID: 22878 RVA: 0x0008BB93 File Offset: 0x00089D93
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170037EC RID: 14316
			// (set) Token: 0x0600595F RID: 22879 RVA: 0x0008BBAB File Offset: 0x00089DAB
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000696 RID: 1686
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170037ED RID: 14317
			// (set) Token: 0x06005961 RID: 22881 RVA: 0x0008BBCB File Offset: 0x00089DCB
			public virtual FederationTrustIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170037EE RID: 14318
			// (set) Token: 0x06005962 RID: 22882 RVA: 0x0008BBDE File Offset: 0x00089DDE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037EF RID: 14319
			// (set) Token: 0x06005963 RID: 22883 RVA: 0x0008BBF1 File Offset: 0x00089DF1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037F0 RID: 14320
			// (set) Token: 0x06005964 RID: 22884 RVA: 0x0008BC09 File Offset: 0x00089E09
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037F1 RID: 14321
			// (set) Token: 0x06005965 RID: 22885 RVA: 0x0008BC21 File Offset: 0x00089E21
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037F2 RID: 14322
			// (set) Token: 0x06005966 RID: 22886 RVA: 0x0008BC39 File Offset: 0x00089E39
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037F3 RID: 14323
			// (set) Token: 0x06005967 RID: 22887 RVA: 0x0008BC51 File Offset: 0x00089E51
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170037F4 RID: 14324
			// (set) Token: 0x06005968 RID: 22888 RVA: 0x0008BC69 File Offset: 0x00089E69
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
