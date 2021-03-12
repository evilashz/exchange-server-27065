using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200086F RID: 2159
	public class GetThrottlingPolicyCommand : SyntheticCommandWithPipelineInput<ThrottlingPolicy, ThrottlingPolicy>
	{
		// Token: 0x06006AE2 RID: 27362 RVA: 0x000A2122 File Offset: 0x000A0322
		private GetThrottlingPolicyCommand() : base("Get-ThrottlingPolicy")
		{
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x000A212F File Offset: 0x000A032F
		public GetThrottlingPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x000A213E File Offset: 0x000A033E
		public virtual GetThrottlingPolicyCommand SetParameters(GetThrottlingPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x000A2148 File Offset: 0x000A0348
		public virtual GetThrottlingPolicyCommand SetParameters(GetThrottlingPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000870 RID: 2160
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170045BD RID: 17853
			// (set) Token: 0x06006AE6 RID: 27366 RVA: 0x000A2152 File Offset: 0x000A0352
			public virtual ThrottlingPolicyScopeType ThrottlingPolicyScope
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicyScope"] = value;
				}
			}

			// Token: 0x170045BE RID: 17854
			// (set) Token: 0x06006AE7 RID: 27367 RVA: 0x000A216A File Offset: 0x000A036A
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170045BF RID: 17855
			// (set) Token: 0x06006AE8 RID: 27368 RVA: 0x000A2182 File Offset: 0x000A0382
			public virtual SwitchParameter Explicit
			{
				set
				{
					base.PowerSharpParameters["Explicit"] = value;
				}
			}

			// Token: 0x170045C0 RID: 17856
			// (set) Token: 0x06006AE9 RID: 27369 RVA: 0x000A219A File Offset: 0x000A039A
			public virtual SwitchParameter Diagnostics
			{
				set
				{
					base.PowerSharpParameters["Diagnostics"] = value;
				}
			}

			// Token: 0x170045C1 RID: 17857
			// (set) Token: 0x06006AEA RID: 27370 RVA: 0x000A21B2 File Offset: 0x000A03B2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170045C2 RID: 17858
			// (set) Token: 0x06006AEB RID: 27371 RVA: 0x000A21D0 File Offset: 0x000A03D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170045C3 RID: 17859
			// (set) Token: 0x06006AEC RID: 27372 RVA: 0x000A21E3 File Offset: 0x000A03E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170045C4 RID: 17860
			// (set) Token: 0x06006AED RID: 27373 RVA: 0x000A21FB File Offset: 0x000A03FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170045C5 RID: 17861
			// (set) Token: 0x06006AEE RID: 27374 RVA: 0x000A2213 File Offset: 0x000A0413
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170045C6 RID: 17862
			// (set) Token: 0x06006AEF RID: 27375 RVA: 0x000A222B File Offset: 0x000A042B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000871 RID: 2161
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170045C7 RID: 17863
			// (set) Token: 0x06006AF1 RID: 27377 RVA: 0x000A224B File Offset: 0x000A044B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170045C8 RID: 17864
			// (set) Token: 0x06006AF2 RID: 27378 RVA: 0x000A2269 File Offset: 0x000A0469
			public virtual ThrottlingPolicyScopeType ThrottlingPolicyScope
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicyScope"] = value;
				}
			}

			// Token: 0x170045C9 RID: 17865
			// (set) Token: 0x06006AF3 RID: 27379 RVA: 0x000A2281 File Offset: 0x000A0481
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170045CA RID: 17866
			// (set) Token: 0x06006AF4 RID: 27380 RVA: 0x000A2299 File Offset: 0x000A0499
			public virtual SwitchParameter Explicit
			{
				set
				{
					base.PowerSharpParameters["Explicit"] = value;
				}
			}

			// Token: 0x170045CB RID: 17867
			// (set) Token: 0x06006AF5 RID: 27381 RVA: 0x000A22B1 File Offset: 0x000A04B1
			public virtual SwitchParameter Diagnostics
			{
				set
				{
					base.PowerSharpParameters["Diagnostics"] = value;
				}
			}

			// Token: 0x170045CC RID: 17868
			// (set) Token: 0x06006AF6 RID: 27382 RVA: 0x000A22C9 File Offset: 0x000A04C9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170045CD RID: 17869
			// (set) Token: 0x06006AF7 RID: 27383 RVA: 0x000A22E7 File Offset: 0x000A04E7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170045CE RID: 17870
			// (set) Token: 0x06006AF8 RID: 27384 RVA: 0x000A22FA File Offset: 0x000A04FA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170045CF RID: 17871
			// (set) Token: 0x06006AF9 RID: 27385 RVA: 0x000A2312 File Offset: 0x000A0512
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170045D0 RID: 17872
			// (set) Token: 0x06006AFA RID: 27386 RVA: 0x000A232A File Offset: 0x000A052A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170045D1 RID: 17873
			// (set) Token: 0x06006AFB RID: 27387 RVA: 0x000A2342 File Offset: 0x000A0542
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
