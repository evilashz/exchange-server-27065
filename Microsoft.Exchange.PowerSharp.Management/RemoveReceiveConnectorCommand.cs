using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008C3 RID: 2243
	public class RemoveReceiveConnectorCommand : SyntheticCommandWithPipelineInput<ReceiveConnector, ReceiveConnector>
	{
		// Token: 0x0600703B RID: 28731 RVA: 0x000A9566 File Offset: 0x000A7766
		private RemoveReceiveConnectorCommand() : base("Remove-ReceiveConnector")
		{
		}

		// Token: 0x0600703C RID: 28732 RVA: 0x000A9573 File Offset: 0x000A7773
		public RemoveReceiveConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600703D RID: 28733 RVA: 0x000A9582 File Offset: 0x000A7782
		public virtual RemoveReceiveConnectorCommand SetParameters(RemoveReceiveConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600703E RID: 28734 RVA: 0x000A958C File Offset: 0x000A778C
		public virtual RemoveReceiveConnectorCommand SetParameters(RemoveReceiveConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008C4 RID: 2244
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004A6E RID: 19054
			// (set) Token: 0x0600703F RID: 28735 RVA: 0x000A9596 File Offset: 0x000A7796
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A6F RID: 19055
			// (set) Token: 0x06007040 RID: 28736 RVA: 0x000A95A9 File Offset: 0x000A77A9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A70 RID: 19056
			// (set) Token: 0x06007041 RID: 28737 RVA: 0x000A95C1 File Offset: 0x000A77C1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A71 RID: 19057
			// (set) Token: 0x06007042 RID: 28738 RVA: 0x000A95D9 File Offset: 0x000A77D9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A72 RID: 19058
			// (set) Token: 0x06007043 RID: 28739 RVA: 0x000A95F1 File Offset: 0x000A77F1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A73 RID: 19059
			// (set) Token: 0x06007044 RID: 28740 RVA: 0x000A9609 File Offset: 0x000A7809
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A74 RID: 19060
			// (set) Token: 0x06007045 RID: 28741 RVA: 0x000A9621 File Offset: 0x000A7821
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008C5 RID: 2245
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004A75 RID: 19061
			// (set) Token: 0x06007047 RID: 28743 RVA: 0x000A9641 File Offset: 0x000A7841
			public virtual ReceiveConnectorIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004A76 RID: 19062
			// (set) Token: 0x06007048 RID: 28744 RVA: 0x000A9654 File Offset: 0x000A7854
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A77 RID: 19063
			// (set) Token: 0x06007049 RID: 28745 RVA: 0x000A9667 File Offset: 0x000A7867
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A78 RID: 19064
			// (set) Token: 0x0600704A RID: 28746 RVA: 0x000A967F File Offset: 0x000A787F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A79 RID: 19065
			// (set) Token: 0x0600704B RID: 28747 RVA: 0x000A9697 File Offset: 0x000A7897
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A7A RID: 19066
			// (set) Token: 0x0600704C RID: 28748 RVA: 0x000A96AF File Offset: 0x000A78AF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A7B RID: 19067
			// (set) Token: 0x0600704D RID: 28749 RVA: 0x000A96C7 File Offset: 0x000A78C7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A7C RID: 19068
			// (set) Token: 0x0600704E RID: 28750 RVA: 0x000A96DF File Offset: 0x000A78DF
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
