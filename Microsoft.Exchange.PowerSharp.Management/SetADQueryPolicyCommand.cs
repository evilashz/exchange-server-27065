using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000E4 RID: 228
	public class SetADQueryPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<ADQueryPolicy>
	{
		// Token: 0x06001D84 RID: 7556 RVA: 0x0003E043 File Offset: 0x0003C243
		private SetADQueryPolicyCommand() : base("Set-ADQueryPolicy")
		{
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0003E050 File Offset: 0x0003C250
		public SetADQueryPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0003E05F File Offset: 0x0003C25F
		public virtual SetADQueryPolicyCommand SetParameters(SetADQueryPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0003E069 File Offset: 0x0003C269
		public virtual SetADQueryPolicyCommand SetParameters(SetADQueryPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000E5 RID: 229
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000775 RID: 1909
			// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0003E073 File Offset: 0x0003C273
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000776 RID: 1910
			// (set) Token: 0x06001D89 RID: 7561 RVA: 0x0003E086 File Offset: 0x0003C286
			public virtual int? MaxNotificationPerConnection
			{
				set
				{
					base.PowerSharpParameters["MaxNotificationPerConnection"] = value;
				}
			}

			// Token: 0x17000777 RID: 1911
			// (set) Token: 0x06001D8A RID: 7562 RVA: 0x0003E09E File Offset: 0x0003C29E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000778 RID: 1912
			// (set) Token: 0x06001D8B RID: 7563 RVA: 0x0003E0B1 File Offset: 0x0003C2B1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000779 RID: 1913
			// (set) Token: 0x06001D8C RID: 7564 RVA: 0x0003E0C9 File Offset: 0x0003C2C9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700077A RID: 1914
			// (set) Token: 0x06001D8D RID: 7565 RVA: 0x0003E0E1 File Offset: 0x0003C2E1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700077B RID: 1915
			// (set) Token: 0x06001D8E RID: 7566 RVA: 0x0003E0F9 File Offset: 0x0003C2F9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700077C RID: 1916
			// (set) Token: 0x06001D8F RID: 7567 RVA: 0x0003E111 File Offset: 0x0003C311
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020000E6 RID: 230
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700077D RID: 1917
			// (set) Token: 0x06001D91 RID: 7569 RVA: 0x0003E131 File Offset: 0x0003C331
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ADQueryPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700077E RID: 1918
			// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0003E14F File Offset: 0x0003C34F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700077F RID: 1919
			// (set) Token: 0x06001D93 RID: 7571 RVA: 0x0003E162 File Offset: 0x0003C362
			public virtual int? MaxNotificationPerConnection
			{
				set
				{
					base.PowerSharpParameters["MaxNotificationPerConnection"] = value;
				}
			}

			// Token: 0x17000780 RID: 1920
			// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0003E17A File Offset: 0x0003C37A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000781 RID: 1921
			// (set) Token: 0x06001D95 RID: 7573 RVA: 0x0003E18D File Offset: 0x0003C38D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000782 RID: 1922
			// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0003E1A5 File Offset: 0x0003C3A5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000783 RID: 1923
			// (set) Token: 0x06001D97 RID: 7575 RVA: 0x0003E1BD File Offset: 0x0003C3BD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000784 RID: 1924
			// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0003E1D5 File Offset: 0x0003C3D5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000785 RID: 1925
			// (set) Token: 0x06001D99 RID: 7577 RVA: 0x0003E1ED File Offset: 0x0003C3ED
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
