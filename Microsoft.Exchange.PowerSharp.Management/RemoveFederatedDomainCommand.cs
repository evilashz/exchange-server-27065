using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000691 RID: 1681
	public class RemoveFederatedDomainCommand : SyntheticCommandWithPipelineInput<FederatedOrganizationId, FederatedOrganizationId>
	{
		// Token: 0x0600593C RID: 22844 RVA: 0x0008B8F6 File Offset: 0x00089AF6
		private RemoveFederatedDomainCommand() : base("Remove-FederatedDomain")
		{
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x0008B903 File Offset: 0x00089B03
		public RemoveFederatedDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x0008B912 File Offset: 0x00089B12
		public virtual RemoveFederatedDomainCommand SetParameters(RemoveFederatedDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x0008B91C File Offset: 0x00089B1C
		public virtual RemoveFederatedDomainCommand SetParameters(RemoveFederatedDomainCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000692 RID: 1682
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170037D3 RID: 14291
			// (set) Token: 0x06005940 RID: 22848 RVA: 0x0008B926 File Offset: 0x00089B26
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170037D4 RID: 14292
			// (set) Token: 0x06005941 RID: 22849 RVA: 0x0008B939 File Offset: 0x00089B39
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170037D5 RID: 14293
			// (set) Token: 0x06005942 RID: 22850 RVA: 0x0008B951 File Offset: 0x00089B51
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037D6 RID: 14294
			// (set) Token: 0x06005943 RID: 22851 RVA: 0x0008B964 File Offset: 0x00089B64
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037D7 RID: 14295
			// (set) Token: 0x06005944 RID: 22852 RVA: 0x0008B97C File Offset: 0x00089B7C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037D8 RID: 14296
			// (set) Token: 0x06005945 RID: 22853 RVA: 0x0008B994 File Offset: 0x00089B94
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037D9 RID: 14297
			// (set) Token: 0x06005946 RID: 22854 RVA: 0x0008B9AC File Offset: 0x00089BAC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037DA RID: 14298
			// (set) Token: 0x06005947 RID: 22855 RVA: 0x0008B9C4 File Offset: 0x00089BC4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170037DB RID: 14299
			// (set) Token: 0x06005948 RID: 22856 RVA: 0x0008B9DC File Offset: 0x00089BDC
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000693 RID: 1683
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170037DC RID: 14300
			// (set) Token: 0x0600594A RID: 22858 RVA: 0x0008B9FC File Offset: 0x00089BFC
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170037DD RID: 14301
			// (set) Token: 0x0600594B RID: 22859 RVA: 0x0008BA1A File Offset: 0x00089C1A
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170037DE RID: 14302
			// (set) Token: 0x0600594C RID: 22860 RVA: 0x0008BA2D File Offset: 0x00089C2D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170037DF RID: 14303
			// (set) Token: 0x0600594D RID: 22861 RVA: 0x0008BA45 File Offset: 0x00089C45
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037E0 RID: 14304
			// (set) Token: 0x0600594E RID: 22862 RVA: 0x0008BA58 File Offset: 0x00089C58
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037E1 RID: 14305
			// (set) Token: 0x0600594F RID: 22863 RVA: 0x0008BA70 File Offset: 0x00089C70
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037E2 RID: 14306
			// (set) Token: 0x06005950 RID: 22864 RVA: 0x0008BA88 File Offset: 0x00089C88
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037E3 RID: 14307
			// (set) Token: 0x06005951 RID: 22865 RVA: 0x0008BAA0 File Offset: 0x00089CA0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037E4 RID: 14308
			// (set) Token: 0x06005952 RID: 22866 RVA: 0x0008BAB8 File Offset: 0x00089CB8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170037E5 RID: 14309
			// (set) Token: 0x06005953 RID: 22867 RVA: 0x0008BAD0 File Offset: 0x00089CD0
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
