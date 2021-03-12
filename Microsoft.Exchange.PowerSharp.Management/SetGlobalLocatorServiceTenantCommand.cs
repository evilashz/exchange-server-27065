using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006C4 RID: 1732
	public class SetGlobalLocatorServiceTenantCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x06005AEC RID: 23276 RVA: 0x0008DAB3 File Offset: 0x0008BCB3
		private SetGlobalLocatorServiceTenantCommand() : base("Set-GlobalLocatorServiceTenant")
		{
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x0008DAC0 File Offset: 0x0008BCC0
		public SetGlobalLocatorServiceTenantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x0008DACF File Offset: 0x0008BCCF
		public virtual SetGlobalLocatorServiceTenantCommand SetParameters(SetGlobalLocatorServiceTenantCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005AEF RID: 23279 RVA: 0x0008DAD9 File Offset: 0x0008BCD9
		public virtual SetGlobalLocatorServiceTenantCommand SetParameters(SetGlobalLocatorServiceTenantCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005AF0 RID: 23280 RVA: 0x0008DAE3 File Offset: 0x0008BCE3
		public virtual SetGlobalLocatorServiceTenantCommand SetParameters(SetGlobalLocatorServiceTenantCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006C5 RID: 1733
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x1700391D RID: 14621
			// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x0008DAED File Offset: 0x0008BCED
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x1700391E RID: 14622
			// (set) Token: 0x06005AF2 RID: 23282 RVA: 0x0008DB00 File Offset: 0x0008BD00
			public virtual string ResourceForest
			{
				set
				{
					base.PowerSharpParameters["ResourceForest"] = value;
				}
			}

			// Token: 0x1700391F RID: 14623
			// (set) Token: 0x06005AF3 RID: 23283 RVA: 0x0008DB13 File Offset: 0x0008BD13
			public virtual string AccountForest
			{
				set
				{
					base.PowerSharpParameters["AccountForest"] = value;
				}
			}

			// Token: 0x17003920 RID: 14624
			// (set) Token: 0x06005AF4 RID: 23284 RVA: 0x0008DB26 File Offset: 0x0008BD26
			public virtual string PrimarySite
			{
				set
				{
					base.PowerSharpParameters["PrimarySite"] = value;
				}
			}

			// Token: 0x17003921 RID: 14625
			// (set) Token: 0x06005AF5 RID: 23285 RVA: 0x0008DB39 File Offset: 0x0008BD39
			public virtual SmtpDomain SmtpNextHopDomain
			{
				set
				{
					base.PowerSharpParameters["SmtpNextHopDomain"] = value;
				}
			}

			// Token: 0x17003922 RID: 14626
			// (set) Token: 0x06005AF6 RID: 23286 RVA: 0x0008DB4C File Offset: 0x0008BD4C
			public virtual GlsTenantFlags TenantFlags
			{
				set
				{
					base.PowerSharpParameters["TenantFlags"] = value;
				}
			}

			// Token: 0x17003923 RID: 14627
			// (set) Token: 0x06005AF7 RID: 23287 RVA: 0x0008DB64 File Offset: 0x0008BD64
			public virtual string TenantContainerCN
			{
				set
				{
					base.PowerSharpParameters["TenantContainerCN"] = value;
				}
			}

			// Token: 0x17003924 RID: 14628
			// (set) Token: 0x06005AF8 RID: 23288 RVA: 0x0008DB77 File Offset: 0x0008BD77
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003925 RID: 14629
			// (set) Token: 0x06005AF9 RID: 23289 RVA: 0x0008DB8F File Offset: 0x0008BD8F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003926 RID: 14630
			// (set) Token: 0x06005AFA RID: 23290 RVA: 0x0008DBA7 File Offset: 0x0008BDA7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003927 RID: 14631
			// (set) Token: 0x06005AFB RID: 23291 RVA: 0x0008DBBF File Offset: 0x0008BDBF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003928 RID: 14632
			// (set) Token: 0x06005AFC RID: 23292 RVA: 0x0008DBD7 File Offset: 0x0008BDD7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003929 RID: 14633
			// (set) Token: 0x06005AFD RID: 23293 RVA: 0x0008DBEF File Offset: 0x0008BDEF
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020006C6 RID: 1734
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x1700392A RID: 14634
			// (set) Token: 0x06005AFF RID: 23295 RVA: 0x0008DC0F File Offset: 0x0008BE0F
			public virtual string ResourceForest
			{
				set
				{
					base.PowerSharpParameters["ResourceForest"] = value;
				}
			}

			// Token: 0x1700392B RID: 14635
			// (set) Token: 0x06005B00 RID: 23296 RVA: 0x0008DC22 File Offset: 0x0008BE22
			public virtual string AccountForest
			{
				set
				{
					base.PowerSharpParameters["AccountForest"] = value;
				}
			}

			// Token: 0x1700392C RID: 14636
			// (set) Token: 0x06005B01 RID: 23297 RVA: 0x0008DC35 File Offset: 0x0008BE35
			public virtual string PrimarySite
			{
				set
				{
					base.PowerSharpParameters["PrimarySite"] = value;
				}
			}

			// Token: 0x1700392D RID: 14637
			// (set) Token: 0x06005B02 RID: 23298 RVA: 0x0008DC48 File Offset: 0x0008BE48
			public virtual SmtpDomain SmtpNextHopDomain
			{
				set
				{
					base.PowerSharpParameters["SmtpNextHopDomain"] = value;
				}
			}

			// Token: 0x1700392E RID: 14638
			// (set) Token: 0x06005B03 RID: 23299 RVA: 0x0008DC5B File Offset: 0x0008BE5B
			public virtual GlsTenantFlags TenantFlags
			{
				set
				{
					base.PowerSharpParameters["TenantFlags"] = value;
				}
			}

			// Token: 0x1700392F RID: 14639
			// (set) Token: 0x06005B04 RID: 23300 RVA: 0x0008DC73 File Offset: 0x0008BE73
			public virtual string TenantContainerCN
			{
				set
				{
					base.PowerSharpParameters["TenantContainerCN"] = value;
				}
			}

			// Token: 0x17003930 RID: 14640
			// (set) Token: 0x06005B05 RID: 23301 RVA: 0x0008DC86 File Offset: 0x0008BE86
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x17003931 RID: 14641
			// (set) Token: 0x06005B06 RID: 23302 RVA: 0x0008DC9E File Offset: 0x0008BE9E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003932 RID: 14642
			// (set) Token: 0x06005B07 RID: 23303 RVA: 0x0008DCB6 File Offset: 0x0008BEB6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003933 RID: 14643
			// (set) Token: 0x06005B08 RID: 23304 RVA: 0x0008DCCE File Offset: 0x0008BECE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003934 RID: 14644
			// (set) Token: 0x06005B09 RID: 23305 RVA: 0x0008DCE6 File Offset: 0x0008BEE6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003935 RID: 14645
			// (set) Token: 0x06005B0A RID: 23306 RVA: 0x0008DCFE File Offset: 0x0008BEFE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003936 RID: 14646
			// (set) Token: 0x06005B0B RID: 23307 RVA: 0x0008DD16 File Offset: 0x0008BF16
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020006C7 RID: 1735
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x17003937 RID: 14647
			// (set) Token: 0x06005B0D RID: 23309 RVA: 0x0008DD36 File Offset: 0x0008BF36
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x17003938 RID: 14648
			// (set) Token: 0x06005B0E RID: 23310 RVA: 0x0008DD4E File Offset: 0x0008BF4E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003939 RID: 14649
			// (set) Token: 0x06005B0F RID: 23311 RVA: 0x0008DD66 File Offset: 0x0008BF66
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700393A RID: 14650
			// (set) Token: 0x06005B10 RID: 23312 RVA: 0x0008DD7E File Offset: 0x0008BF7E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700393B RID: 14651
			// (set) Token: 0x06005B11 RID: 23313 RVA: 0x0008DD96 File Offset: 0x0008BF96
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700393C RID: 14652
			// (set) Token: 0x06005B12 RID: 23314 RVA: 0x0008DDAE File Offset: 0x0008BFAE
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700393D RID: 14653
			// (set) Token: 0x06005B13 RID: 23315 RVA: 0x0008DDC6 File Offset: 0x0008BFC6
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
