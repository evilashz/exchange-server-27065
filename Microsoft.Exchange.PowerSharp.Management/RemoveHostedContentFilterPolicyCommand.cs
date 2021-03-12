using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000714 RID: 1812
	public class RemoveHostedContentFilterPolicyCommand : SyntheticCommandWithPipelineInput<HostedContentFilterPolicy, HostedContentFilterPolicy>
	{
		// Token: 0x06005D82 RID: 23938 RVA: 0x00090F59 File Offset: 0x0008F159
		private RemoveHostedContentFilterPolicyCommand() : base("Remove-HostedContentFilterPolicy")
		{
		}

		// Token: 0x06005D83 RID: 23939 RVA: 0x00090F66 File Offset: 0x0008F166
		public RemoveHostedContentFilterPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005D84 RID: 23940 RVA: 0x00090F75 File Offset: 0x0008F175
		public virtual RemoveHostedContentFilterPolicyCommand SetParameters(RemoveHostedContentFilterPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005D85 RID: 23941 RVA: 0x00090F7F File Offset: 0x0008F17F
		public virtual RemoveHostedContentFilterPolicyCommand SetParameters(RemoveHostedContentFilterPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000715 RID: 1813
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003B13 RID: 15123
			// (set) Token: 0x06005D86 RID: 23942 RVA: 0x00090F89 File Offset: 0x0008F189
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003B14 RID: 15124
			// (set) Token: 0x06005D87 RID: 23943 RVA: 0x00090FA1 File Offset: 0x0008F1A1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B15 RID: 15125
			// (set) Token: 0x06005D88 RID: 23944 RVA: 0x00090FB4 File Offset: 0x0008F1B4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B16 RID: 15126
			// (set) Token: 0x06005D89 RID: 23945 RVA: 0x00090FCC File Offset: 0x0008F1CC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B17 RID: 15127
			// (set) Token: 0x06005D8A RID: 23946 RVA: 0x00090FE4 File Offset: 0x0008F1E4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B18 RID: 15128
			// (set) Token: 0x06005D8B RID: 23947 RVA: 0x00090FFC File Offset: 0x0008F1FC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003B19 RID: 15129
			// (set) Token: 0x06005D8C RID: 23948 RVA: 0x00091014 File Offset: 0x0008F214
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003B1A RID: 15130
			// (set) Token: 0x06005D8D RID: 23949 RVA: 0x0009102C File Offset: 0x0008F22C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000716 RID: 1814
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17003B1B RID: 15131
			// (set) Token: 0x06005D8F RID: 23951 RVA: 0x0009104C File Offset: 0x0008F24C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new HostedContentFilterPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17003B1C RID: 15132
			// (set) Token: 0x06005D90 RID: 23952 RVA: 0x0009106A File Offset: 0x0008F26A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17003B1D RID: 15133
			// (set) Token: 0x06005D91 RID: 23953 RVA: 0x00091082 File Offset: 0x0008F282
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17003B1E RID: 15134
			// (set) Token: 0x06005D92 RID: 23954 RVA: 0x00091095 File Offset: 0x0008F295
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003B1F RID: 15135
			// (set) Token: 0x06005D93 RID: 23955 RVA: 0x000910AD File Offset: 0x0008F2AD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003B20 RID: 15136
			// (set) Token: 0x06005D94 RID: 23956 RVA: 0x000910C5 File Offset: 0x0008F2C5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003B21 RID: 15137
			// (set) Token: 0x06005D95 RID: 23957 RVA: 0x000910DD File Offset: 0x0008F2DD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003B22 RID: 15138
			// (set) Token: 0x06005D96 RID: 23958 RVA: 0x000910F5 File Offset: 0x0008F2F5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17003B23 RID: 15139
			// (set) Token: 0x06005D97 RID: 23959 RVA: 0x0009110D File Offset: 0x0008F30D
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
