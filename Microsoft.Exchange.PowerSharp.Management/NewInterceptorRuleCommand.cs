using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Transport.Agent.InterceptorAgent;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008A5 RID: 2213
	public class NewInterceptorRuleCommand : SyntheticCommandWithPipelineInput<InterceptorRule, InterceptorRule>
	{
		// Token: 0x06006DAA RID: 28074 RVA: 0x000A5D7E File Offset: 0x000A3F7E
		private NewInterceptorRuleCommand() : base("New-InterceptorRule")
		{
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x000A5D8B File Offset: 0x000A3F8B
		public NewInterceptorRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006DAC RID: 28076 RVA: 0x000A5D9A File Offset: 0x000A3F9A
		public virtual NewInterceptorRuleCommand SetParameters(NewInterceptorRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008A6 RID: 2214
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004819 RID: 18457
			// (set) Token: 0x06006DAD RID: 28077 RVA: 0x000A5DA4 File Offset: 0x000A3FA4
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700481A RID: 18458
			// (set) Token: 0x06006DAE RID: 28078 RVA: 0x000A5DB7 File Offset: 0x000A3FB7
			public virtual InterceptorAgentRuleBehavior Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x1700481B RID: 18459
			// (set) Token: 0x06006DAF RID: 28079 RVA: 0x000A5DCF File Offset: 0x000A3FCF
			public virtual InterceptorAgentEvent Event
			{
				set
				{
					base.PowerSharpParameters["Event"] = value;
				}
			}

			// Token: 0x1700481C RID: 18460
			// (set) Token: 0x06006DB0 RID: 28080 RVA: 0x000A5DE7 File Offset: 0x000A3FE7
			public virtual string Condition
			{
				set
				{
					base.PowerSharpParameters["Condition"] = value;
				}
			}

			// Token: 0x1700481D RID: 18461
			// (set) Token: 0x06006DB1 RID: 28081 RVA: 0x000A5DFA File Offset: 0x000A3FFA
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x1700481E RID: 18462
			// (set) Token: 0x06006DB2 RID: 28082 RVA: 0x000A5E0D File Offset: 0x000A400D
			public virtual MultiValuedProperty<ServerIdParameter> Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x1700481F RID: 18463
			// (set) Token: 0x06006DB3 RID: 28083 RVA: 0x000A5E20 File Offset: 0x000A4020
			public virtual MultiValuedProperty<DatabaseAvailabilityGroupIdParameter> Dag
			{
				set
				{
					base.PowerSharpParameters["Dag"] = value;
				}
			}

			// Token: 0x17004820 RID: 18464
			// (set) Token: 0x06006DB4 RID: 28084 RVA: 0x000A5E33 File Offset: 0x000A4033
			public virtual MultiValuedProperty<AdSiteIdParameter> Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = value;
				}
			}

			// Token: 0x17004821 RID: 18465
			// (set) Token: 0x06006DB5 RID: 28085 RVA: 0x000A5E46 File Offset: 0x000A4046
			public virtual EnhancedTimeSpan TimeInterval
			{
				set
				{
					base.PowerSharpParameters["TimeInterval"] = value;
				}
			}

			// Token: 0x17004822 RID: 18466
			// (set) Token: 0x06006DB6 RID: 28086 RVA: 0x000A5E5E File Offset: 0x000A405E
			public virtual string CustomResponseString
			{
				set
				{
					base.PowerSharpParameters["CustomResponseString"] = value;
				}
			}

			// Token: 0x17004823 RID: 18467
			// (set) Token: 0x06006DB7 RID: 28087 RVA: 0x000A5E71 File Offset: 0x000A4071
			public virtual string CustomResponseCode
			{
				set
				{
					base.PowerSharpParameters["CustomResponseCode"] = value;
				}
			}

			// Token: 0x17004824 RID: 18468
			// (set) Token: 0x06006DB8 RID: 28088 RVA: 0x000A5E84 File Offset: 0x000A4084
			public virtual DateTime ExpireTime
			{
				set
				{
					base.PowerSharpParameters["ExpireTime"] = value;
				}
			}

			// Token: 0x17004825 RID: 18469
			// (set) Token: 0x06006DB9 RID: 28089 RVA: 0x000A5E9C File Offset: 0x000A409C
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x17004826 RID: 18470
			// (set) Token: 0x06006DBA RID: 28090 RVA: 0x000A5EAF File Offset: 0x000A40AF
			public virtual SourceType Source
			{
				set
				{
					base.PowerSharpParameters["Source"] = value;
				}
			}

			// Token: 0x17004827 RID: 18471
			// (set) Token: 0x06006DBB RID: 28091 RVA: 0x000A5EC7 File Offset: 0x000A40C7
			public virtual string CreatedBy
			{
				set
				{
					base.PowerSharpParameters["CreatedBy"] = value;
				}
			}

			// Token: 0x17004828 RID: 18472
			// (set) Token: 0x06006DBC RID: 28092 RVA: 0x000A5EDA File Offset: 0x000A40DA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004829 RID: 18473
			// (set) Token: 0x06006DBD RID: 28093 RVA: 0x000A5EED File Offset: 0x000A40ED
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700482A RID: 18474
			// (set) Token: 0x06006DBE RID: 28094 RVA: 0x000A5F05 File Offset: 0x000A4105
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700482B RID: 18475
			// (set) Token: 0x06006DBF RID: 28095 RVA: 0x000A5F1D File Offset: 0x000A411D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700482C RID: 18476
			// (set) Token: 0x06006DC0 RID: 28096 RVA: 0x000A5F35 File Offset: 0x000A4135
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700482D RID: 18477
			// (set) Token: 0x06006DC1 RID: 28097 RVA: 0x000A5F4D File Offset: 0x000A414D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700482E RID: 18478
			// (set) Token: 0x06006DC2 RID: 28098 RVA: 0x000A5F65 File Offset: 0x000A4165
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
