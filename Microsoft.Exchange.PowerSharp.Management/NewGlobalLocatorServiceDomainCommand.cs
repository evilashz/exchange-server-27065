using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006CA RID: 1738
	public class NewGlobalLocatorServiceDomainCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x06005B21 RID: 23329 RVA: 0x0008DECF File Offset: 0x0008C0CF
		private NewGlobalLocatorServiceDomainCommand() : base("New-GlobalLocatorServiceDomain")
		{
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x0008DEDC File Offset: 0x0008C0DC
		public NewGlobalLocatorServiceDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x0008DEEB File Offset: 0x0008C0EB
		public virtual NewGlobalLocatorServiceDomainCommand SetParameters(NewGlobalLocatorServiceDomainCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x0008DEF5 File Offset: 0x0008C0F5
		public virtual NewGlobalLocatorServiceDomainCommand SetParameters(NewGlobalLocatorServiceDomainCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x0008DEFF File Offset: 0x0008C0FF
		public virtual NewGlobalLocatorServiceDomainCommand SetParameters(NewGlobalLocatorServiceDomainCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006CB RID: 1739
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003946 RID: 14662
			// (set) Token: 0x06005B26 RID: 23334 RVA: 0x0008DF09 File Offset: 0x0008C109
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003947 RID: 14663
			// (set) Token: 0x06005B27 RID: 23335 RVA: 0x0008DF1C File Offset: 0x0008C11C
			public virtual GlsDomainFlags DomainFlags
			{
				set
				{
					base.PowerSharpParameters["DomainFlags"] = value;
				}
			}

			// Token: 0x17003948 RID: 14664
			// (set) Token: 0x06005B28 RID: 23336 RVA: 0x0008DF34 File Offset: 0x0008C134
			public virtual DomainKeyType DomainType
			{
				set
				{
					base.PowerSharpParameters["DomainType"] = value;
				}
			}

			// Token: 0x17003949 RID: 14665
			// (set) Token: 0x06005B29 RID: 23337 RVA: 0x0008DF4C File Offset: 0x0008C14C
			public virtual bool DomainInUse
			{
				set
				{
					base.PowerSharpParameters["DomainInUse"] = value;
				}
			}

			// Token: 0x1700394A RID: 14666
			// (set) Token: 0x06005B2A RID: 23338 RVA: 0x0008DF64 File Offset: 0x0008C164
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700394B RID: 14667
			// (set) Token: 0x06005B2B RID: 23339 RVA: 0x0008DF7C File Offset: 0x0008C17C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700394C RID: 14668
			// (set) Token: 0x06005B2C RID: 23340 RVA: 0x0008DF94 File Offset: 0x0008C194
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700394D RID: 14669
			// (set) Token: 0x06005B2D RID: 23341 RVA: 0x0008DFAC File Offset: 0x0008C1AC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700394E RID: 14670
			// (set) Token: 0x06005B2E RID: 23342 RVA: 0x0008DFC4 File Offset: 0x0008C1C4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006CC RID: 1740
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x1700394F RID: 14671
			// (set) Token: 0x06005B30 RID: 23344 RVA: 0x0008DFE4 File Offset: 0x0008C1E4
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x17003950 RID: 14672
			// (set) Token: 0x06005B31 RID: 23345 RVA: 0x0008DFFC File Offset: 0x0008C1FC
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x17003951 RID: 14673
			// (set) Token: 0x06005B32 RID: 23346 RVA: 0x0008E00F File Offset: 0x0008C20F
			public virtual GlsDomainFlags DomainFlags
			{
				set
				{
					base.PowerSharpParameters["DomainFlags"] = value;
				}
			}

			// Token: 0x17003952 RID: 14674
			// (set) Token: 0x06005B33 RID: 23347 RVA: 0x0008E027 File Offset: 0x0008C227
			public virtual DomainKeyType DomainType
			{
				set
				{
					base.PowerSharpParameters["DomainType"] = value;
				}
			}

			// Token: 0x17003953 RID: 14675
			// (set) Token: 0x06005B34 RID: 23348 RVA: 0x0008E03F File Offset: 0x0008C23F
			public virtual bool DomainInUse
			{
				set
				{
					base.PowerSharpParameters["DomainInUse"] = value;
				}
			}

			// Token: 0x17003954 RID: 14676
			// (set) Token: 0x06005B35 RID: 23349 RVA: 0x0008E057 File Offset: 0x0008C257
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003955 RID: 14677
			// (set) Token: 0x06005B36 RID: 23350 RVA: 0x0008E06F File Offset: 0x0008C26F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003956 RID: 14678
			// (set) Token: 0x06005B37 RID: 23351 RVA: 0x0008E087 File Offset: 0x0008C287
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003957 RID: 14679
			// (set) Token: 0x06005B38 RID: 23352 RVA: 0x0008E09F File Offset: 0x0008C29F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003958 RID: 14680
			// (set) Token: 0x06005B39 RID: 23353 RVA: 0x0008E0B7 File Offset: 0x0008C2B7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006CD RID: 1741
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x17003959 RID: 14681
			// (set) Token: 0x06005B3B RID: 23355 RVA: 0x0008E0D7 File Offset: 0x0008C2D7
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x1700395A RID: 14682
			// (set) Token: 0x06005B3C RID: 23356 RVA: 0x0008E0EF File Offset: 0x0008C2EF
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700395B RID: 14683
			// (set) Token: 0x06005B3D RID: 23357 RVA: 0x0008E102 File Offset: 0x0008C302
			public virtual GlsDomainFlags DomainFlags
			{
				set
				{
					base.PowerSharpParameters["DomainFlags"] = value;
				}
			}

			// Token: 0x1700395C RID: 14684
			// (set) Token: 0x06005B3E RID: 23358 RVA: 0x0008E11A File Offset: 0x0008C31A
			public virtual DomainKeyType DomainType
			{
				set
				{
					base.PowerSharpParameters["DomainType"] = value;
				}
			}

			// Token: 0x1700395D RID: 14685
			// (set) Token: 0x06005B3F RID: 23359 RVA: 0x0008E132 File Offset: 0x0008C332
			public virtual bool DomainInUse
			{
				set
				{
					base.PowerSharpParameters["DomainInUse"] = value;
				}
			}

			// Token: 0x1700395E RID: 14686
			// (set) Token: 0x06005B40 RID: 23360 RVA: 0x0008E14A File Offset: 0x0008C34A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700395F RID: 14687
			// (set) Token: 0x06005B41 RID: 23361 RVA: 0x0008E162 File Offset: 0x0008C362
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003960 RID: 14688
			// (set) Token: 0x06005B42 RID: 23362 RVA: 0x0008E17A File Offset: 0x0008C37A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003961 RID: 14689
			// (set) Token: 0x06005B43 RID: 23363 RVA: 0x0008E192 File Offset: 0x0008C392
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003962 RID: 14690
			// (set) Token: 0x06005B44 RID: 23364 RVA: 0x0008E1AA File Offset: 0x0008C3AA
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
