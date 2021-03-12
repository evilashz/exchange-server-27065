using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008B4 RID: 2228
	public class RemoveDeliveryAgentConnectorCommand : SyntheticCommandWithPipelineInput<DeliveryAgentConnector, DeliveryAgentConnector>
	{
		// Token: 0x06006FD2 RID: 28626 RVA: 0x000A8D48 File Offset: 0x000A6F48
		private RemoveDeliveryAgentConnectorCommand() : base("Remove-DeliveryAgentConnector")
		{
		}

		// Token: 0x06006FD3 RID: 28627 RVA: 0x000A8D55 File Offset: 0x000A6F55
		public RemoveDeliveryAgentConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006FD4 RID: 28628 RVA: 0x000A8D64 File Offset: 0x000A6F64
		public virtual RemoveDeliveryAgentConnectorCommand SetParameters(RemoveDeliveryAgentConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006FD5 RID: 28629 RVA: 0x000A8D6E File Offset: 0x000A6F6E
		public virtual RemoveDeliveryAgentConnectorCommand SetParameters(RemoveDeliveryAgentConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008B5 RID: 2229
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004A23 RID: 18979
			// (set) Token: 0x06006FD6 RID: 28630 RVA: 0x000A8D78 File Offset: 0x000A6F78
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A24 RID: 18980
			// (set) Token: 0x06006FD7 RID: 28631 RVA: 0x000A8D8B File Offset: 0x000A6F8B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A25 RID: 18981
			// (set) Token: 0x06006FD8 RID: 28632 RVA: 0x000A8DA3 File Offset: 0x000A6FA3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A26 RID: 18982
			// (set) Token: 0x06006FD9 RID: 28633 RVA: 0x000A8DBB File Offset: 0x000A6FBB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A27 RID: 18983
			// (set) Token: 0x06006FDA RID: 28634 RVA: 0x000A8DD3 File Offset: 0x000A6FD3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A28 RID: 18984
			// (set) Token: 0x06006FDB RID: 28635 RVA: 0x000A8DEB File Offset: 0x000A6FEB
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A29 RID: 18985
			// (set) Token: 0x06006FDC RID: 28636 RVA: 0x000A8E03 File Offset: 0x000A7003
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008B6 RID: 2230
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004A2A RID: 18986
			// (set) Token: 0x06006FDE RID: 28638 RVA: 0x000A8E23 File Offset: 0x000A7023
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new DeliveryAgentConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x17004A2B RID: 18987
			// (set) Token: 0x06006FDF RID: 28639 RVA: 0x000A8E41 File Offset: 0x000A7041
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A2C RID: 18988
			// (set) Token: 0x06006FE0 RID: 28640 RVA: 0x000A8E54 File Offset: 0x000A7054
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A2D RID: 18989
			// (set) Token: 0x06006FE1 RID: 28641 RVA: 0x000A8E6C File Offset: 0x000A706C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A2E RID: 18990
			// (set) Token: 0x06006FE2 RID: 28642 RVA: 0x000A8E84 File Offset: 0x000A7084
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A2F RID: 18991
			// (set) Token: 0x06006FE3 RID: 28643 RVA: 0x000A8E9C File Offset: 0x000A709C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A30 RID: 18992
			// (set) Token: 0x06006FE4 RID: 28644 RVA: 0x000A8EB4 File Offset: 0x000A70B4
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A31 RID: 18993
			// (set) Token: 0x06006FE5 RID: 28645 RVA: 0x000A8ECC File Offset: 0x000A70CC
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
