using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Agent.InterceptorAgent;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008D5 RID: 2261
	public class SetInterceptorRuleCommand : SyntheticCommandWithPipelineInputNoOutput<InterceptorAgentRule>
	{
		// Token: 0x06007103 RID: 28931 RVA: 0x000AA56E File Offset: 0x000A876E
		private SetInterceptorRuleCommand() : base("Set-InterceptorRule")
		{
		}

		// Token: 0x06007104 RID: 28932 RVA: 0x000AA57B File Offset: 0x000A877B
		public SetInterceptorRuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007105 RID: 28933 RVA: 0x000AA58A File Offset: 0x000A878A
		public virtual SetInterceptorRuleCommand SetParameters(SetInterceptorRuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x000AA594 File Offset: 0x000A8794
		public virtual SetInterceptorRuleCommand SetParameters(SetInterceptorRuleCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008D6 RID: 2262
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004B12 RID: 19218
			// (set) Token: 0x06007107 RID: 28935 RVA: 0x000AA59E File Offset: 0x000A879E
			public virtual InterceptorAgentRuleBehavior Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17004B13 RID: 19219
			// (set) Token: 0x06007108 RID: 28936 RVA: 0x000AA5B6 File Offset: 0x000A87B6
			public virtual InterceptorAgentEvent Event
			{
				set
				{
					base.PowerSharpParameters["Event"] = value;
				}
			}

			// Token: 0x17004B14 RID: 19220
			// (set) Token: 0x06007109 RID: 28937 RVA: 0x000AA5CE File Offset: 0x000A87CE
			public virtual string Condition
			{
				set
				{
					base.PowerSharpParameters["Condition"] = value;
				}
			}

			// Token: 0x17004B15 RID: 19221
			// (set) Token: 0x0600710A RID: 28938 RVA: 0x000AA5E1 File Offset: 0x000A87E1
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17004B16 RID: 19222
			// (set) Token: 0x0600710B RID: 28939 RVA: 0x000AA5F4 File Offset: 0x000A87F4
			public virtual MultiValuedProperty<ServerIdParameter> Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004B17 RID: 19223
			// (set) Token: 0x0600710C RID: 28940 RVA: 0x000AA607 File Offset: 0x000A8807
			public virtual MultiValuedProperty<DatabaseAvailabilityGroupIdParameter> Dag
			{
				set
				{
					base.PowerSharpParameters["Dag"] = value;
				}
			}

			// Token: 0x17004B18 RID: 19224
			// (set) Token: 0x0600710D RID: 28941 RVA: 0x000AA61A File Offset: 0x000A881A
			public virtual MultiValuedProperty<AdSiteIdParameter> Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = value;
				}
			}

			// Token: 0x17004B19 RID: 19225
			// (set) Token: 0x0600710E RID: 28942 RVA: 0x000AA62D File Offset: 0x000A882D
			public virtual EnhancedTimeSpan TimeInterval
			{
				set
				{
					base.PowerSharpParameters["TimeInterval"] = value;
				}
			}

			// Token: 0x17004B1A RID: 19226
			// (set) Token: 0x0600710F RID: 28943 RVA: 0x000AA645 File Offset: 0x000A8845
			public virtual string CustomResponseString
			{
				set
				{
					base.PowerSharpParameters["CustomResponseString"] = value;
				}
			}

			// Token: 0x17004B1B RID: 19227
			// (set) Token: 0x06007110 RID: 28944 RVA: 0x000AA658 File Offset: 0x000A8858
			public virtual string CustomResponseCode
			{
				set
				{
					base.PowerSharpParameters["CustomResponseCode"] = value;
				}
			}

			// Token: 0x17004B1C RID: 19228
			// (set) Token: 0x06007111 RID: 28945 RVA: 0x000AA66B File Offset: 0x000A886B
			public virtual DateTime ExpireTime
			{
				set
				{
					base.PowerSharpParameters["ExpireTime"] = value;
				}
			}

			// Token: 0x17004B1D RID: 19229
			// (set) Token: 0x06007112 RID: 28946 RVA: 0x000AA683 File Offset: 0x000A8883
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x17004B1E RID: 19230
			// (set) Token: 0x06007113 RID: 28947 RVA: 0x000AA696 File Offset: 0x000A8896
			public virtual SourceType Source
			{
				set
				{
					base.PowerSharpParameters["Source"] = value;
				}
			}

			// Token: 0x17004B1F RID: 19231
			// (set) Token: 0x06007114 RID: 28948 RVA: 0x000AA6AE File Offset: 0x000A88AE
			public virtual string CreatedBy
			{
				set
				{
					base.PowerSharpParameters["CreatedBy"] = value;
				}
			}

			// Token: 0x17004B20 RID: 19232
			// (set) Token: 0x06007115 RID: 28949 RVA: 0x000AA6C1 File Offset: 0x000A88C1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004B21 RID: 19233
			// (set) Token: 0x06007116 RID: 28950 RVA: 0x000AA6D4 File Offset: 0x000A88D4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004B22 RID: 19234
			// (set) Token: 0x06007117 RID: 28951 RVA: 0x000AA6EC File Offset: 0x000A88EC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004B23 RID: 19235
			// (set) Token: 0x06007118 RID: 28952 RVA: 0x000AA704 File Offset: 0x000A8904
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004B24 RID: 19236
			// (set) Token: 0x06007119 RID: 28953 RVA: 0x000AA71C File Offset: 0x000A891C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004B25 RID: 19237
			// (set) Token: 0x0600711A RID: 28954 RVA: 0x000AA734 File Offset: 0x000A8934
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020008D7 RID: 2263
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004B26 RID: 19238
			// (set) Token: 0x0600711C RID: 28956 RVA: 0x000AA754 File Offset: 0x000A8954
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new InterceptorRuleIdParameter(value) : null);
				}
			}

			// Token: 0x17004B27 RID: 19239
			// (set) Token: 0x0600711D RID: 28957 RVA: 0x000AA772 File Offset: 0x000A8972
			public virtual InterceptorAgentRuleBehavior Action
			{
				set
				{
					base.PowerSharpParameters["Action"] = value;
				}
			}

			// Token: 0x17004B28 RID: 19240
			// (set) Token: 0x0600711E RID: 28958 RVA: 0x000AA78A File Offset: 0x000A898A
			public virtual InterceptorAgentEvent Event
			{
				set
				{
					base.PowerSharpParameters["Event"] = value;
				}
			}

			// Token: 0x17004B29 RID: 19241
			// (set) Token: 0x0600711F RID: 28959 RVA: 0x000AA7A2 File Offset: 0x000A89A2
			public virtual string Condition
			{
				set
				{
					base.PowerSharpParameters["Condition"] = value;
				}
			}

			// Token: 0x17004B2A RID: 19242
			// (set) Token: 0x06007120 RID: 28960 RVA: 0x000AA7B5 File Offset: 0x000A89B5
			public virtual string Description
			{
				set
				{
					base.PowerSharpParameters["Description"] = value;
				}
			}

			// Token: 0x17004B2B RID: 19243
			// (set) Token: 0x06007121 RID: 28961 RVA: 0x000AA7C8 File Offset: 0x000A89C8
			public virtual MultiValuedProperty<ServerIdParameter> Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17004B2C RID: 19244
			// (set) Token: 0x06007122 RID: 28962 RVA: 0x000AA7DB File Offset: 0x000A89DB
			public virtual MultiValuedProperty<DatabaseAvailabilityGroupIdParameter> Dag
			{
				set
				{
					base.PowerSharpParameters["Dag"] = value;
				}
			}

			// Token: 0x17004B2D RID: 19245
			// (set) Token: 0x06007123 RID: 28963 RVA: 0x000AA7EE File Offset: 0x000A89EE
			public virtual MultiValuedProperty<AdSiteIdParameter> Site
			{
				set
				{
					base.PowerSharpParameters["Site"] = value;
				}
			}

			// Token: 0x17004B2E RID: 19246
			// (set) Token: 0x06007124 RID: 28964 RVA: 0x000AA801 File Offset: 0x000A8A01
			public virtual EnhancedTimeSpan TimeInterval
			{
				set
				{
					base.PowerSharpParameters["TimeInterval"] = value;
				}
			}

			// Token: 0x17004B2F RID: 19247
			// (set) Token: 0x06007125 RID: 28965 RVA: 0x000AA819 File Offset: 0x000A8A19
			public virtual string CustomResponseString
			{
				set
				{
					base.PowerSharpParameters["CustomResponseString"] = value;
				}
			}

			// Token: 0x17004B30 RID: 19248
			// (set) Token: 0x06007126 RID: 28966 RVA: 0x000AA82C File Offset: 0x000A8A2C
			public virtual string CustomResponseCode
			{
				set
				{
					base.PowerSharpParameters["CustomResponseCode"] = value;
				}
			}

			// Token: 0x17004B31 RID: 19249
			// (set) Token: 0x06007127 RID: 28967 RVA: 0x000AA83F File Offset: 0x000A8A3F
			public virtual DateTime ExpireTime
			{
				set
				{
					base.PowerSharpParameters["ExpireTime"] = value;
				}
			}

			// Token: 0x17004B32 RID: 19250
			// (set) Token: 0x06007128 RID: 28968 RVA: 0x000AA857 File Offset: 0x000A8A57
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = value;
				}
			}

			// Token: 0x17004B33 RID: 19251
			// (set) Token: 0x06007129 RID: 28969 RVA: 0x000AA86A File Offset: 0x000A8A6A
			public virtual SourceType Source
			{
				set
				{
					base.PowerSharpParameters["Source"] = value;
				}
			}

			// Token: 0x17004B34 RID: 19252
			// (set) Token: 0x0600712A RID: 28970 RVA: 0x000AA882 File Offset: 0x000A8A82
			public virtual string CreatedBy
			{
				set
				{
					base.PowerSharpParameters["CreatedBy"] = value;
				}
			}

			// Token: 0x17004B35 RID: 19253
			// (set) Token: 0x0600712B RID: 28971 RVA: 0x000AA895 File Offset: 0x000A8A95
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004B36 RID: 19254
			// (set) Token: 0x0600712C RID: 28972 RVA: 0x000AA8A8 File Offset: 0x000A8AA8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004B37 RID: 19255
			// (set) Token: 0x0600712D RID: 28973 RVA: 0x000AA8C0 File Offset: 0x000A8AC0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004B38 RID: 19256
			// (set) Token: 0x0600712E RID: 28974 RVA: 0x000AA8D8 File Offset: 0x000A8AD8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004B39 RID: 19257
			// (set) Token: 0x0600712F RID: 28975 RVA: 0x000AA8F0 File Offset: 0x000A8AF0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004B3A RID: 19258
			// (set) Token: 0x06007130 RID: 28976 RVA: 0x000AA908 File Offset: 0x000A8B08
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
