using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020003EF RID: 1007
	public class RemoveRMSTrustedPublishingDomainCommand : SyntheticCommandWithPipelineInput<RMSTrustedPublishingDomain, RMSTrustedPublishingDomain>
	{
		// Token: 0x06003BE0 RID: 15328 RVA: 0x000657C1 File Offset: 0x000639C1
		private RemoveRMSTrustedPublishingDomainCommand() : base("Remove-RMSTrustedPublishingDomain")
		{
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x000657CE File Offset: 0x000639CE
		public RemoveRMSTrustedPublishingDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000657DD File Offset: 0x000639DD
		public virtual RemoveRMSTrustedPublishingDomainCommand SetParameters(RemoveRMSTrustedPublishingDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000657E7 File Offset: 0x000639E7
		public virtual RemoveRMSTrustedPublishingDomainCommand SetParameters(RemoveRMSTrustedPublishingDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020003F0 RID: 1008
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001FBB RID: 8123
			// (set) Token: 0x06003BE4 RID: 15332 RVA: 0x000657F1 File Offset: 0x000639F1
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001FBC RID: 8124
			// (set) Token: 0x06003BE5 RID: 15333 RVA: 0x00065809 File Offset: 0x00063A09
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001FBD RID: 8125
			// (set) Token: 0x06003BE6 RID: 15334 RVA: 0x0006581C File Offset: 0x00063A1C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001FBE RID: 8126
			// (set) Token: 0x06003BE7 RID: 15335 RVA: 0x00065834 File Offset: 0x00063A34
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001FBF RID: 8127
			// (set) Token: 0x06003BE8 RID: 15336 RVA: 0x0006584C File Offset: 0x00063A4C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001FC0 RID: 8128
			// (set) Token: 0x06003BE9 RID: 15337 RVA: 0x00065864 File Offset: 0x00063A64
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001FC1 RID: 8129
			// (set) Token: 0x06003BEA RID: 15338 RVA: 0x0006587C File Offset: 0x00063A7C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001FC2 RID: 8130
			// (set) Token: 0x06003BEB RID: 15339 RVA: 0x00065894 File Offset: 0x00063A94
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020003F1 RID: 1009
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17001FC3 RID: 8131
			// (set) Token: 0x06003BED RID: 15341 RVA: 0x000658B4 File Offset: 0x00063AB4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RmsTrustedPublishingDomainIdParameter(value) : null);
				}
			}

			// Token: 0x17001FC4 RID: 8132
			// (set) Token: 0x06003BEE RID: 15342 RVA: 0x000658D2 File Offset: 0x00063AD2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001FC5 RID: 8133
			// (set) Token: 0x06003BEF RID: 15343 RVA: 0x000658EA File Offset: 0x00063AEA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001FC6 RID: 8134
			// (set) Token: 0x06003BF0 RID: 15344 RVA: 0x000658FD File Offset: 0x00063AFD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001FC7 RID: 8135
			// (set) Token: 0x06003BF1 RID: 15345 RVA: 0x00065915 File Offset: 0x00063B15
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001FC8 RID: 8136
			// (set) Token: 0x06003BF2 RID: 15346 RVA: 0x0006592D File Offset: 0x00063B2D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001FC9 RID: 8137
			// (set) Token: 0x06003BF3 RID: 15347 RVA: 0x00065945 File Offset: 0x00063B45
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001FCA RID: 8138
			// (set) Token: 0x06003BF4 RID: 15348 RVA: 0x0006595D File Offset: 0x00063B5D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17001FCB RID: 8139
			// (set) Token: 0x06003BF5 RID: 15349 RVA: 0x00065975 File Offset: 0x00063B75
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
