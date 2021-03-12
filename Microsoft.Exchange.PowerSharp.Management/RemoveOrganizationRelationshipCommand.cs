using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000697 RID: 1687
	public class RemoveOrganizationRelationshipCommand : SyntheticCommandWithPipelineInput<OrganizationRelationship, OrganizationRelationship>
	{
		// Token: 0x0600596A RID: 22890 RVA: 0x0008BC89 File Offset: 0x00089E89
		private RemoveOrganizationRelationshipCommand() : base("Remove-OrganizationRelationship")
		{
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x0008BC96 File Offset: 0x00089E96
		public RemoveOrganizationRelationshipCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600596C RID: 22892 RVA: 0x0008BCA5 File Offset: 0x00089EA5
		public virtual RemoveOrganizationRelationshipCommand SetParameters(RemoveOrganizationRelationshipCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600596D RID: 22893 RVA: 0x0008BCAF File Offset: 0x00089EAF
		public virtual RemoveOrganizationRelationshipCommand SetParameters(RemoveOrganizationRelationshipCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000698 RID: 1688
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170037F5 RID: 14325
			// (set) Token: 0x0600596E RID: 22894 RVA: 0x0008BCB9 File Offset: 0x00089EB9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037F6 RID: 14326
			// (set) Token: 0x0600596F RID: 22895 RVA: 0x0008BCCC File Offset: 0x00089ECC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037F7 RID: 14327
			// (set) Token: 0x06005970 RID: 22896 RVA: 0x0008BCE4 File Offset: 0x00089EE4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170037F8 RID: 14328
			// (set) Token: 0x06005971 RID: 22897 RVA: 0x0008BCFC File Offset: 0x00089EFC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170037F9 RID: 14329
			// (set) Token: 0x06005972 RID: 22898 RVA: 0x0008BD14 File Offset: 0x00089F14
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170037FA RID: 14330
			// (set) Token: 0x06005973 RID: 22899 RVA: 0x0008BD2C File Offset: 0x00089F2C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170037FB RID: 14331
			// (set) Token: 0x06005974 RID: 22900 RVA: 0x0008BD44 File Offset: 0x00089F44
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000699 RID: 1689
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170037FC RID: 14332
			// (set) Token: 0x06005976 RID: 22902 RVA: 0x0008BD64 File Offset: 0x00089F64
			public virtual OrganizationRelationshipIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x170037FD RID: 14333
			// (set) Token: 0x06005977 RID: 22903 RVA: 0x0008BD77 File Offset: 0x00089F77
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170037FE RID: 14334
			// (set) Token: 0x06005978 RID: 22904 RVA: 0x0008BD8A File Offset: 0x00089F8A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170037FF RID: 14335
			// (set) Token: 0x06005979 RID: 22905 RVA: 0x0008BDA2 File Offset: 0x00089FA2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003800 RID: 14336
			// (set) Token: 0x0600597A RID: 22906 RVA: 0x0008BDBA File Offset: 0x00089FBA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003801 RID: 14337
			// (set) Token: 0x0600597B RID: 22907 RVA: 0x0008BDD2 File Offset: 0x00089FD2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003802 RID: 14338
			// (set) Token: 0x0600597C RID: 22908 RVA: 0x0008BDEA File Offset: 0x00089FEA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003803 RID: 14339
			// (set) Token: 0x0600597D RID: 22909 RVA: 0x0008BE02 File Offset: 0x0008A002
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
