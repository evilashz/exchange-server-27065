using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020004FB RID: 1275
	public class GetOutlookProviderCommand : SyntheticCommandWithPipelineInput<OutlookProvider, OutlookProvider>
	{
		// Token: 0x0600458B RID: 17803 RVA: 0x00071CB9 File Offset: 0x0006FEB9
		private GetOutlookProviderCommand() : base("Get-OutlookProvider")
		{
		}

		// Token: 0x0600458C RID: 17804 RVA: 0x00071CC6 File Offset: 0x0006FEC6
		public GetOutlookProviderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600458D RID: 17805 RVA: 0x00071CD5 File Offset: 0x0006FED5
		public virtual GetOutlookProviderCommand SetParameters(GetOutlookProviderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600458E RID: 17806 RVA: 0x00071CDF File Offset: 0x0006FEDF
		public virtual GetOutlookProviderCommand SetParameters(GetOutlookProviderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020004FC RID: 1276
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700274E RID: 10062
			// (set) Token: 0x0600458F RID: 17807 RVA: 0x00071CE9 File Offset: 0x0006FEE9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700274F RID: 10063
			// (set) Token: 0x06004590 RID: 17808 RVA: 0x00071D07 File Offset: 0x0006FF07
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002750 RID: 10064
			// (set) Token: 0x06004591 RID: 17809 RVA: 0x00071D1A File Offset: 0x0006FF1A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002751 RID: 10065
			// (set) Token: 0x06004592 RID: 17810 RVA: 0x00071D32 File Offset: 0x0006FF32
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002752 RID: 10066
			// (set) Token: 0x06004593 RID: 17811 RVA: 0x00071D4A File Offset: 0x0006FF4A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002753 RID: 10067
			// (set) Token: 0x06004594 RID: 17812 RVA: 0x00071D62 File Offset: 0x0006FF62
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x020004FD RID: 1277
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002754 RID: 10068
			// (set) Token: 0x06004596 RID: 17814 RVA: 0x00071D82 File Offset: 0x0006FF82
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OutlookProviderIdParameter(value) : null);
				}
			}

			// Token: 0x17002755 RID: 10069
			// (set) Token: 0x06004597 RID: 17815 RVA: 0x00071DA0 File Offset: 0x0006FFA0
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002756 RID: 10070
			// (set) Token: 0x06004598 RID: 17816 RVA: 0x00071DBE File Offset: 0x0006FFBE
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002757 RID: 10071
			// (set) Token: 0x06004599 RID: 17817 RVA: 0x00071DD1 File Offset: 0x0006FFD1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002758 RID: 10072
			// (set) Token: 0x0600459A RID: 17818 RVA: 0x00071DE9 File Offset: 0x0006FFE9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002759 RID: 10073
			// (set) Token: 0x0600459B RID: 17819 RVA: 0x00071E01 File Offset: 0x00070001
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700275A RID: 10074
			// (set) Token: 0x0600459C RID: 17820 RVA: 0x00071E19 File Offset: 0x00070019
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
