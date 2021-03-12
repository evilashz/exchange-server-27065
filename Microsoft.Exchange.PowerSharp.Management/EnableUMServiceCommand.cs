using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B27 RID: 2855
	public class EnableUMServiceCommand : SyntheticCommandWithPipelineInput<Server, Server>
	{
		// Token: 0x06008BB2 RID: 35762 RVA: 0x000CD179 File Offset: 0x000CB379
		private EnableUMServiceCommand() : base("Enable-UMService")
		{
		}

		// Token: 0x06008BB3 RID: 35763 RVA: 0x000CD186 File Offset: 0x000CB386
		public EnableUMServiceCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008BB4 RID: 35764 RVA: 0x000CD195 File Offset: 0x000CB395
		public virtual EnableUMServiceCommand SetParameters(EnableUMServiceCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06008BB5 RID: 35765 RVA: 0x000CD19F File Offset: 0x000CB39F
		public virtual EnableUMServiceCommand SetParameters(EnableUMServiceCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B28 RID: 2856
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700611D RID: 24861
			// (set) Token: 0x06008BB6 RID: 35766 RVA: 0x000CD1A9 File Offset: 0x000CB3A9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700611E RID: 24862
			// (set) Token: 0x06008BB7 RID: 35767 RVA: 0x000CD1BC File Offset: 0x000CB3BC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700611F RID: 24863
			// (set) Token: 0x06008BB8 RID: 35768 RVA: 0x000CD1D4 File Offset: 0x000CB3D4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006120 RID: 24864
			// (set) Token: 0x06008BB9 RID: 35769 RVA: 0x000CD1EC File Offset: 0x000CB3EC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006121 RID: 24865
			// (set) Token: 0x06008BBA RID: 35770 RVA: 0x000CD204 File Offset: 0x000CB404
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006122 RID: 24866
			// (set) Token: 0x06008BBB RID: 35771 RVA: 0x000CD21C File Offset: 0x000CB41C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000B29 RID: 2857
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006123 RID: 24867
			// (set) Token: 0x06008BBD RID: 35773 RVA: 0x000CD23C File Offset: 0x000CB43C
			public virtual UMServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17006124 RID: 24868
			// (set) Token: 0x06008BBE RID: 35774 RVA: 0x000CD24F File Offset: 0x000CB44F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006125 RID: 24869
			// (set) Token: 0x06008BBF RID: 35775 RVA: 0x000CD262 File Offset: 0x000CB462
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006126 RID: 24870
			// (set) Token: 0x06008BC0 RID: 35776 RVA: 0x000CD27A File Offset: 0x000CB47A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006127 RID: 24871
			// (set) Token: 0x06008BC1 RID: 35777 RVA: 0x000CD292 File Offset: 0x000CB492
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006128 RID: 24872
			// (set) Token: 0x06008BC2 RID: 35778 RVA: 0x000CD2AA File Offset: 0x000CB4AA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006129 RID: 24873
			// (set) Token: 0x06008BC3 RID: 35779 RVA: 0x000CD2C2 File Offset: 0x000CB4C2
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
