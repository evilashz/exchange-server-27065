using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200084C RID: 2124
	public class GetResourceConfigCommand : SyntheticCommandWithPipelineInput<ResourceBookingConfig, ResourceBookingConfig>
	{
		// Token: 0x060069D8 RID: 27096 RVA: 0x000A0C97 File Offset: 0x0009EE97
		private GetResourceConfigCommand() : base("Get-ResourceConfig")
		{
		}

		// Token: 0x060069D9 RID: 27097 RVA: 0x000A0CA4 File Offset: 0x0009EEA4
		public GetResourceConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060069DA RID: 27098 RVA: 0x000A0CB3 File Offset: 0x0009EEB3
		public virtual GetResourceConfigCommand SetParameters(GetResourceConfigCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060069DB RID: 27099 RVA: 0x000A0CBD File Offset: 0x0009EEBD
		public virtual GetResourceConfigCommand SetParameters(GetResourceConfigCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200084D RID: 2125
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170044F9 RID: 17657
			// (set) Token: 0x060069DC RID: 27100 RVA: 0x000A0CC7 File Offset: 0x0009EEC7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170044FA RID: 17658
			// (set) Token: 0x060069DD RID: 27101 RVA: 0x000A0CE5 File Offset: 0x0009EEE5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170044FB RID: 17659
			// (set) Token: 0x060069DE RID: 27102 RVA: 0x000A0CF8 File Offset: 0x0009EEF8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170044FC RID: 17660
			// (set) Token: 0x060069DF RID: 27103 RVA: 0x000A0D10 File Offset: 0x0009EF10
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170044FD RID: 17661
			// (set) Token: 0x060069E0 RID: 27104 RVA: 0x000A0D28 File Offset: 0x0009EF28
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170044FE RID: 17662
			// (set) Token: 0x060069E1 RID: 27105 RVA: 0x000A0D40 File Offset: 0x0009EF40
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200084E RID: 2126
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170044FF RID: 17663
			// (set) Token: 0x060069E3 RID: 27107 RVA: 0x000A0D60 File Offset: 0x0009EF60
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004500 RID: 17664
			// (set) Token: 0x060069E4 RID: 27108 RVA: 0x000A0D73 File Offset: 0x0009EF73
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004501 RID: 17665
			// (set) Token: 0x060069E5 RID: 27109 RVA: 0x000A0D8B File Offset: 0x0009EF8B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004502 RID: 17666
			// (set) Token: 0x060069E6 RID: 27110 RVA: 0x000A0DA3 File Offset: 0x0009EFA3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004503 RID: 17667
			// (set) Token: 0x060069E7 RID: 27111 RVA: 0x000A0DBB File Offset: 0x0009EFBB
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
