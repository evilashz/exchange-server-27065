using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020008B7 RID: 2231
	public class RemoveForeignConnectorCommand : SyntheticCommandWithPipelineInput<ForeignConnector, ForeignConnector>
	{
		// Token: 0x06006FE7 RID: 28647 RVA: 0x000A8EEC File Offset: 0x000A70EC
		private RemoveForeignConnectorCommand() : base("Remove-ForeignConnector")
		{
		}

		// Token: 0x06006FE8 RID: 28648 RVA: 0x000A8EF9 File Offset: 0x000A70F9
		public RemoveForeignConnectorCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006FE9 RID: 28649 RVA: 0x000A8F08 File Offset: 0x000A7108
		public virtual RemoveForeignConnectorCommand SetParameters(RemoveForeignConnectorCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006FEA RID: 28650 RVA: 0x000A8F12 File Offset: 0x000A7112
		public virtual RemoveForeignConnectorCommand SetParameters(RemoveForeignConnectorCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020008B8 RID: 2232
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004A32 RID: 18994
			// (set) Token: 0x06006FEB RID: 28651 RVA: 0x000A8F1C File Offset: 0x000A711C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A33 RID: 18995
			// (set) Token: 0x06006FEC RID: 28652 RVA: 0x000A8F2F File Offset: 0x000A712F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A34 RID: 18996
			// (set) Token: 0x06006FED RID: 28653 RVA: 0x000A8F47 File Offset: 0x000A7147
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A35 RID: 18997
			// (set) Token: 0x06006FEE RID: 28654 RVA: 0x000A8F5F File Offset: 0x000A715F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A36 RID: 18998
			// (set) Token: 0x06006FEF RID: 28655 RVA: 0x000A8F77 File Offset: 0x000A7177
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A37 RID: 18999
			// (set) Token: 0x06006FF0 RID: 28656 RVA: 0x000A8F8F File Offset: 0x000A718F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A38 RID: 19000
			// (set) Token: 0x06006FF1 RID: 28657 RVA: 0x000A8FA7 File Offset: 0x000A71A7
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020008B9 RID: 2233
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004A39 RID: 19001
			// (set) Token: 0x06006FF3 RID: 28659 RVA: 0x000A8FC7 File Offset: 0x000A71C7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new ForeignConnectorIdParameter(value) : null);
				}
			}

			// Token: 0x17004A3A RID: 19002
			// (set) Token: 0x06006FF4 RID: 28660 RVA: 0x000A8FE5 File Offset: 0x000A71E5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004A3B RID: 19003
			// (set) Token: 0x06006FF5 RID: 28661 RVA: 0x000A8FF8 File Offset: 0x000A71F8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004A3C RID: 19004
			// (set) Token: 0x06006FF6 RID: 28662 RVA: 0x000A9010 File Offset: 0x000A7210
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004A3D RID: 19005
			// (set) Token: 0x06006FF7 RID: 28663 RVA: 0x000A9028 File Offset: 0x000A7228
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004A3E RID: 19006
			// (set) Token: 0x06006FF8 RID: 28664 RVA: 0x000A9040 File Offset: 0x000A7240
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004A3F RID: 19007
			// (set) Token: 0x06006FF9 RID: 28665 RVA: 0x000A9058 File Offset: 0x000A7258
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17004A40 RID: 19008
			// (set) Token: 0x06006FFA RID: 28666 RVA: 0x000A9070 File Offset: 0x000A7270
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
