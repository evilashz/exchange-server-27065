using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006B8 RID: 1720
	public class GetGlobalLocatorServiceTenantCommand : SyntheticCommandWithPipelineInputNoOutput<SmtpDomain>
	{
		// Token: 0x06005A89 RID: 23177 RVA: 0x0008D30A File Offset: 0x0008B50A
		private GetGlobalLocatorServiceTenantCommand() : base("Get-GlobalLocatorServiceTenant")
		{
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x0008D317 File Offset: 0x0008B517
		public GetGlobalLocatorServiceTenantCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x0008D326 File Offset: 0x0008B526
		public virtual GetGlobalLocatorServiceTenantCommand SetParameters(GetGlobalLocatorServiceTenantCommand.DomainNameParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x0008D330 File Offset: 0x0008B530
		public virtual GetGlobalLocatorServiceTenantCommand SetParameters(GetGlobalLocatorServiceTenantCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005A8D RID: 23181 RVA: 0x0008D33A File Offset: 0x0008B53A
		public virtual GetGlobalLocatorServiceTenantCommand SetParameters(GetGlobalLocatorServiceTenantCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006B9 RID: 1721
		public class DomainNameParameterSetParameters : ParametersBase
		{
			// Token: 0x170038D2 RID: 14546
			// (set) Token: 0x06005A8E RID: 23182 RVA: 0x0008D344 File Offset: 0x0008B544
			public virtual SmtpDomain DomainName
			{
				set
				{
					base.PowerSharpParameters["DomainName"] = value;
				}
			}

			// Token: 0x170038D3 RID: 14547
			// (set) Token: 0x06005A8F RID: 23183 RVA: 0x0008D357 File Offset: 0x0008B557
			public virtual SwitchParameter ShowDomainNames
			{
				set
				{
					base.PowerSharpParameters["ShowDomainNames"] = value;
				}
			}

			// Token: 0x170038D4 RID: 14548
			// (set) Token: 0x06005A90 RID: 23184 RVA: 0x0008D36F File Offset: 0x0008B56F
			public virtual SwitchParameter UseOfflineGLS
			{
				set
				{
					base.PowerSharpParameters["UseOfflineGLS"] = value;
				}
			}

			// Token: 0x170038D5 RID: 14549
			// (set) Token: 0x06005A91 RID: 23185 RVA: 0x0008D387 File Offset: 0x0008B587
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038D6 RID: 14550
			// (set) Token: 0x06005A92 RID: 23186 RVA: 0x0008D39F File Offset: 0x0008B59F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038D7 RID: 14551
			// (set) Token: 0x06005A93 RID: 23187 RVA: 0x0008D3B7 File Offset: 0x0008B5B7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038D8 RID: 14552
			// (set) Token: 0x06005A94 RID: 23188 RVA: 0x0008D3CF File Offset: 0x0008B5CF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020006BA RID: 1722
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x170038D9 RID: 14553
			// (set) Token: 0x06005A96 RID: 23190 RVA: 0x0008D3EF File Offset: 0x0008B5EF
			public virtual SwitchParameter ShowDomainNames
			{
				set
				{
					base.PowerSharpParameters["ShowDomainNames"] = value;
				}
			}

			// Token: 0x170038DA RID: 14554
			// (set) Token: 0x06005A97 RID: 23191 RVA: 0x0008D407 File Offset: 0x0008B607
			public virtual SwitchParameter UseOfflineGLS
			{
				set
				{
					base.PowerSharpParameters["UseOfflineGLS"] = value;
				}
			}

			// Token: 0x170038DB RID: 14555
			// (set) Token: 0x06005A98 RID: 23192 RVA: 0x0008D41F File Offset: 0x0008B61F
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170038DC RID: 14556
			// (set) Token: 0x06005A99 RID: 23193 RVA: 0x0008D437 File Offset: 0x0008B637
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038DD RID: 14557
			// (set) Token: 0x06005A9A RID: 23194 RVA: 0x0008D44F File Offset: 0x0008B64F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038DE RID: 14558
			// (set) Token: 0x06005A9B RID: 23195 RVA: 0x0008D467 File Offset: 0x0008B667
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038DF RID: 14559
			// (set) Token: 0x06005A9C RID: 23196 RVA: 0x0008D47F File Offset: 0x0008B67F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020006BB RID: 1723
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x170038E0 RID: 14560
			// (set) Token: 0x06005A9E RID: 23198 RVA: 0x0008D49F File Offset: 0x0008B69F
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170038E1 RID: 14561
			// (set) Token: 0x06005A9F RID: 23199 RVA: 0x0008D4B7 File Offset: 0x0008B6B7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038E2 RID: 14562
			// (set) Token: 0x06005AA0 RID: 23200 RVA: 0x0008D4CF File Offset: 0x0008B6CF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038E3 RID: 14563
			// (set) Token: 0x06005AA1 RID: 23201 RVA: 0x0008D4E7 File Offset: 0x0008B6E7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038E4 RID: 14564
			// (set) Token: 0x06005AA2 RID: 23202 RVA: 0x0008D4FF File Offset: 0x0008B6FF
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
