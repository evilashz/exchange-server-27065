using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200013C RID: 316
	public class SetBposPlacementConfigurationCommand : SyntheticCommand<object>
	{
		// Token: 0x0600205A RID: 8282 RVA: 0x000419AC File Offset: 0x0003FBAC
		private SetBposPlacementConfigurationCommand() : base("Set-BposPlacementConfiguration")
		{
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x000419B9 File Offset: 0x0003FBB9
		public SetBposPlacementConfigurationCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x000419C8 File Offset: 0x0003FBC8
		public virtual SetBposPlacementConfigurationCommand SetParameters(SetBposPlacementConfigurationCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200013D RID: 317
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700099B RID: 2459
			// (set) Token: 0x0600205D RID: 8285 RVA: 0x000419D2 File Offset: 0x0003FBD2
			public virtual string Configuration
			{
				set
				{
					base.PowerSharpParameters["Configuration"] = value;
				}
			}

			// Token: 0x1700099C RID: 2460
			// (set) Token: 0x0600205E RID: 8286 RVA: 0x000419E5 File Offset: 0x0003FBE5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700099D RID: 2461
			// (set) Token: 0x0600205F RID: 8287 RVA: 0x000419FD File Offset: 0x0003FBFD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700099E RID: 2462
			// (set) Token: 0x06002060 RID: 8288 RVA: 0x00041A15 File Offset: 0x0003FC15
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700099F RID: 2463
			// (set) Token: 0x06002061 RID: 8289 RVA: 0x00041A2D File Offset: 0x0003FC2D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170009A0 RID: 2464
			// (set) Token: 0x06002062 RID: 8290 RVA: 0x00041A45 File Offset: 0x0003FC45
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
