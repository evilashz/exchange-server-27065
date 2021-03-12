using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008C6 RID: 2246
	public class RemoveSendConnectorCommand : SyntheticCommandWithPipelineInput<SmtpSendConnectorConfig, SmtpSendConnectorConfig>
	{
		// Token: 0x06007050 RID: 28752 RVA: 0x000A96FF File Offset: 0x000A78FF
		private RemoveSendConnectorCommand() : base("Remove-SendConnector")
		{
		}

		// Token: 0x06007051 RID: 28753 RVA: 0x000A970C File Offset: 0x000A790C
		public RemoveSendConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06007052 RID: 28754 RVA: 0x000A971B File Offset: 0x000A791B
		public virtual RemoveSendConnectorCommand SetParameters(RemoveSendConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06007053 RID: 28755 RVA: 0x000A9725 File Offset: 0x000A7925
		public virtual RemoveSendConnectorCommand SetParameters(RemoveSendConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008C7 RID: 2247
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004A7D RID: 19069
			// (set) Token: 0x06007054 RID: 28756 RVA: 0x000A972F File Offset: 0x000A792F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A7E RID: 19070
			// (set) Token: 0x06007055 RID: 28757 RVA: 0x000A9742 File Offset: 0x000A7942
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A7F RID: 19071
			// (set) Token: 0x06007056 RID: 28758 RVA: 0x000A975A File Offset: 0x000A795A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A80 RID: 19072
			// (set) Token: 0x06007057 RID: 28759 RVA: 0x000A9772 File Offset: 0x000A7972
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A81 RID: 19073
			// (set) Token: 0x06007058 RID: 28760 RVA: 0x000A978A File Offset: 0x000A798A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A82 RID: 19074
			// (set) Token: 0x06007059 RID: 28761 RVA: 0x000A97A2 File Offset: 0x000A79A2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A83 RID: 19075
			// (set) Token: 0x0600705A RID: 28762 RVA: 0x000A97BA File Offset: 0x000A79BA
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008C8 RID: 2248
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004A84 RID: 19076
			// (set) Token: 0x0600705C RID: 28764 RVA: 0x000A97DA File Offset: 0x000A79DA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SendConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x17004A85 RID: 19077
			// (set) Token: 0x0600705D RID: 28765 RVA: 0x000A97F8 File Offset: 0x000A79F8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A86 RID: 19078
			// (set) Token: 0x0600705E RID: 28766 RVA: 0x000A980B File Offset: 0x000A7A0B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A87 RID: 19079
			// (set) Token: 0x0600705F RID: 28767 RVA: 0x000A9823 File Offset: 0x000A7A23
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A88 RID: 19080
			// (set) Token: 0x06007060 RID: 28768 RVA: 0x000A983B File Offset: 0x000A7A3B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A89 RID: 19081
			// (set) Token: 0x06007061 RID: 28769 RVA: 0x000A9853 File Offset: 0x000A7A53
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A8A RID: 19082
			// (set) Token: 0x06007062 RID: 28770 RVA: 0x000A986B File Offset: 0x000A7A6B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A8B RID: 19083
			// (set) Token: 0x06007063 RID: 28771 RVA: 0x000A9883 File Offset: 0x000A7A83
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
