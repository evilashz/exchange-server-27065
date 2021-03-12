using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000852 RID: 2130
	public class GetRemoteAccountPolicyCommand : SyntheticCommandWithPipelineInput<RemoteAccountPolicy, RemoteAccountPolicy>
	{
		// Token: 0x060069FE RID: 27134 RVA: 0x000A0F75 File Offset: 0x0009F175
		private GetRemoteAccountPolicyCommand() : base("Get-RemoteAccountPolicy")
		{
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x000A0F82 File Offset: 0x0009F182
		public GetRemoteAccountPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006A00 RID: 27136 RVA: 0x000A0F91 File Offset: 0x0009F191
		public virtual GetRemoteAccountPolicyCommand SetParameters(GetRemoteAccountPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006A01 RID: 27137 RVA: 0x000A0F9B File Offset: 0x0009F19B
		public virtual GetRemoteAccountPolicyCommand SetParameters(GetRemoteAccountPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000853 RID: 2131
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004513 RID: 17683
			// (set) Token: 0x06006A02 RID: 27138 RVA: 0x000A0FA5 File Offset: 0x0009F1A5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004514 RID: 17684
			// (set) Token: 0x06006A03 RID: 27139 RVA: 0x000A0FC3 File Offset: 0x0009F1C3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004515 RID: 17685
			// (set) Token: 0x06006A04 RID: 27140 RVA: 0x000A0FD6 File Offset: 0x0009F1D6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004516 RID: 17686
			// (set) Token: 0x06006A05 RID: 27141 RVA: 0x000A0FEE File Offset: 0x0009F1EE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004517 RID: 17687
			// (set) Token: 0x06006A06 RID: 27142 RVA: 0x000A1006 File Offset: 0x0009F206
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004518 RID: 17688
			// (set) Token: 0x06006A07 RID: 27143 RVA: 0x000A101E File Offset: 0x0009F21E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000854 RID: 2132
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004519 RID: 17689
			// (set) Token: 0x06006A09 RID: 27145 RVA: 0x000A103E File Offset: 0x0009F23E
			public virtual RemoteAccountPolicyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700451A RID: 17690
			// (set) Token: 0x06006A0A RID: 27146 RVA: 0x000A1051 File Offset: 0x0009F251
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700451B RID: 17691
			// (set) Token: 0x06006A0B RID: 27147 RVA: 0x000A106F File Offset: 0x0009F26F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700451C RID: 17692
			// (set) Token: 0x06006A0C RID: 27148 RVA: 0x000A1082 File Offset: 0x0009F282
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700451D RID: 17693
			// (set) Token: 0x06006A0D RID: 27149 RVA: 0x000A109A File Offset: 0x0009F29A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700451E RID: 17694
			// (set) Token: 0x06006A0E RID: 27150 RVA: 0x000A10B2 File Offset: 0x0009F2B2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700451F RID: 17695
			// (set) Token: 0x06006A0F RID: 27151 RVA: 0x000A10CA File Offset: 0x0009F2CA
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
