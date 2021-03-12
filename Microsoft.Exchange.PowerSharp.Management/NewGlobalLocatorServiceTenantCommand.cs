using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006BC RID: 1724
	public class NewGlobalLocatorServiceTenantCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x06005AA4 RID: 23204 RVA: 0x0008D51F File Offset: 0x0008B71F
		private NewGlobalLocatorServiceTenantCommand() : base("New-GlobalLocatorServiceTenant")
		{
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x0008D52C File Offset: 0x0008B72C
		public NewGlobalLocatorServiceTenantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x0008D53B File Offset: 0x0008B73B
		public virtual NewGlobalLocatorServiceTenantCommand SetParameters(NewGlobalLocatorServiceTenantCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x0008D545 File Offset: 0x0008B745
		public virtual NewGlobalLocatorServiceTenantCommand SetParameters(NewGlobalLocatorServiceTenantCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x0008D54F File Offset: 0x0008B74F
		public virtual NewGlobalLocatorServiceTenantCommand SetParameters(NewGlobalLocatorServiceTenantCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006BD RID: 1725
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170038E5 RID: 14565
			// (set) Token: 0x06005AA9 RID: 23209 RVA: 0x0008D559 File Offset: 0x0008B759
			public virtual string ResourceForest
			{
				set
				{
					base.PowerSharpParameters["ResourceForest"] = value;
				}
			}

			// Token: 0x170038E6 RID: 14566
			// (set) Token: 0x06005AAA RID: 23210 RVA: 0x0008D56C File Offset: 0x0008B76C
			public virtual string AccountForest
			{
				set
				{
					base.PowerSharpParameters["AccountForest"] = value;
				}
			}

			// Token: 0x170038E7 RID: 14567
			// (set) Token: 0x06005AAB RID: 23211 RVA: 0x0008D57F File Offset: 0x0008B77F
			public virtual string PrimarySite
			{
				set
				{
					base.PowerSharpParameters["PrimarySite"] = value;
				}
			}

			// Token: 0x170038E8 RID: 14568
			// (set) Token: 0x06005AAC RID: 23212 RVA: 0x0008D592 File Offset: 0x0008B792
			public virtual SmtpDomain SmtpNextHopDomain
			{
				set
				{
					base.PowerSharpParameters["SmtpNextHopDomain"] = value;
				}
			}

			// Token: 0x170038E9 RID: 14569
			// (set) Token: 0x06005AAD RID: 23213 RVA: 0x0008D5A5 File Offset: 0x0008B7A5
			public virtual GlsTenantFlags TenantFlags
			{
				set
				{
					base.PowerSharpParameters["TenantFlags"] = value;
				}
			}

			// Token: 0x170038EA RID: 14570
			// (set) Token: 0x06005AAE RID: 23214 RVA: 0x0008D5BD File Offset: 0x0008B7BD
			public virtual string TenantContainerCN
			{
				set
				{
					base.PowerSharpParameters["TenantContainerCN"] = value;
				}
			}

			// Token: 0x170038EB RID: 14571
			// (set) Token: 0x06005AAF RID: 23215 RVA: 0x0008D5D0 File Offset: 0x0008B7D0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038EC RID: 14572
			// (set) Token: 0x06005AB0 RID: 23216 RVA: 0x0008D5E8 File Offset: 0x0008B7E8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038ED RID: 14573
			// (set) Token: 0x06005AB1 RID: 23217 RVA: 0x0008D600 File Offset: 0x0008B800
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038EE RID: 14574
			// (set) Token: 0x06005AB2 RID: 23218 RVA: 0x0008D618 File Offset: 0x0008B818
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170038EF RID: 14575
			// (set) Token: 0x06005AB3 RID: 23219 RVA: 0x0008D630 File Offset: 0x0008B830
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006BE RID: 1726
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x170038F0 RID: 14576
			// (set) Token: 0x06005AB5 RID: 23221 RVA: 0x0008D650 File Offset: 0x0008B850
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170038F1 RID: 14577
			// (set) Token: 0x06005AB6 RID: 23222 RVA: 0x0008D668 File Offset: 0x0008B868
			public virtual string ResourceForest
			{
				set
				{
					base.PowerSharpParameters["ResourceForest"] = value;
				}
			}

			// Token: 0x170038F2 RID: 14578
			// (set) Token: 0x06005AB7 RID: 23223 RVA: 0x0008D67B File Offset: 0x0008B87B
			public virtual string AccountForest
			{
				set
				{
					base.PowerSharpParameters["AccountForest"] = value;
				}
			}

			// Token: 0x170038F3 RID: 14579
			// (set) Token: 0x06005AB8 RID: 23224 RVA: 0x0008D68E File Offset: 0x0008B88E
			public virtual string PrimarySite
			{
				set
				{
					base.PowerSharpParameters["PrimarySite"] = value;
				}
			}

			// Token: 0x170038F4 RID: 14580
			// (set) Token: 0x06005AB9 RID: 23225 RVA: 0x0008D6A1 File Offset: 0x0008B8A1
			public virtual SmtpDomain SmtpNextHopDomain
			{
				set
				{
					base.PowerSharpParameters["SmtpNextHopDomain"] = value;
				}
			}

			// Token: 0x170038F5 RID: 14581
			// (set) Token: 0x06005ABA RID: 23226 RVA: 0x0008D6B4 File Offset: 0x0008B8B4
			public virtual GlsTenantFlags TenantFlags
			{
				set
				{
					base.PowerSharpParameters["TenantFlags"] = value;
				}
			}

			// Token: 0x170038F6 RID: 14582
			// (set) Token: 0x06005ABB RID: 23227 RVA: 0x0008D6CC File Offset: 0x0008B8CC
			public virtual string TenantContainerCN
			{
				set
				{
					base.PowerSharpParameters["TenantContainerCN"] = value;
				}
			}

			// Token: 0x170038F7 RID: 14583
			// (set) Token: 0x06005ABC RID: 23228 RVA: 0x0008D6DF File Offset: 0x0008B8DF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038F8 RID: 14584
			// (set) Token: 0x06005ABD RID: 23229 RVA: 0x0008D6F7 File Offset: 0x0008B8F7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038F9 RID: 14585
			// (set) Token: 0x06005ABE RID: 23230 RVA: 0x0008D70F File Offset: 0x0008B90F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038FA RID: 14586
			// (set) Token: 0x06005ABF RID: 23231 RVA: 0x0008D727 File Offset: 0x0008B927
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170038FB RID: 14587
			// (set) Token: 0x06005AC0 RID: 23232 RVA: 0x0008D73F File Offset: 0x0008B93F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006BF RID: 1727
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x170038FC RID: 14588
			// (set) Token: 0x06005AC2 RID: 23234 RVA: 0x0008D75F File Offset: 0x0008B95F
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170038FD RID: 14589
			// (set) Token: 0x06005AC3 RID: 23235 RVA: 0x0008D777 File Offset: 0x0008B977
			public virtual string ResourceForest
			{
				set
				{
					base.PowerSharpParameters["ResourceForest"] = value;
				}
			}

			// Token: 0x170038FE RID: 14590
			// (set) Token: 0x06005AC4 RID: 23236 RVA: 0x0008D78A File Offset: 0x0008B98A
			public virtual string AccountForest
			{
				set
				{
					base.PowerSharpParameters["AccountForest"] = value;
				}
			}

			// Token: 0x170038FF RID: 14591
			// (set) Token: 0x06005AC5 RID: 23237 RVA: 0x0008D79D File Offset: 0x0008B99D
			public virtual string PrimarySite
			{
				set
				{
					base.PowerSharpParameters["PrimarySite"] = value;
				}
			}

			// Token: 0x17003900 RID: 14592
			// (set) Token: 0x06005AC6 RID: 23238 RVA: 0x0008D7B0 File Offset: 0x0008B9B0
			public virtual SmtpDomain SmtpNextHopDomain
			{
				set
				{
					base.PowerSharpParameters["SmtpNextHopDomain"] = value;
				}
			}

			// Token: 0x17003901 RID: 14593
			// (set) Token: 0x06005AC7 RID: 23239 RVA: 0x0008D7C3 File Offset: 0x0008B9C3
			public virtual GlsTenantFlags TenantFlags
			{
				set
				{
					base.PowerSharpParameters["TenantFlags"] = value;
				}
			}

			// Token: 0x17003902 RID: 14594
			// (set) Token: 0x06005AC8 RID: 23240 RVA: 0x0008D7DB File Offset: 0x0008B9DB
			public virtual string TenantContainerCN
			{
				set
				{
					base.PowerSharpParameters["TenantContainerCN"] = value;
				}
			}

			// Token: 0x17003903 RID: 14595
			// (set) Token: 0x06005AC9 RID: 23241 RVA: 0x0008D7EE File Offset: 0x0008B9EE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003904 RID: 14596
			// (set) Token: 0x06005ACA RID: 23242 RVA: 0x0008D806 File Offset: 0x0008BA06
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003905 RID: 14597
			// (set) Token: 0x06005ACB RID: 23243 RVA: 0x0008D81E File Offset: 0x0008BA1E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003906 RID: 14598
			// (set) Token: 0x06005ACC RID: 23244 RVA: 0x0008D836 File Offset: 0x0008BA36
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003907 RID: 14599
			// (set) Token: 0x06005ACD RID: 23245 RVA: 0x0008D84E File Offset: 0x0008BA4E
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
