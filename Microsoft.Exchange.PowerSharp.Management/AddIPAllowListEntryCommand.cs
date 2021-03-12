using System;
using System.Management.Automation;
using System.Net;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000736 RID: 1846
	public class AddIPAllowListEntryCommand : SyntheticCommandWithPipelineInput<IPAllowListEntry, IPAllowListEntry>
	{
		// Token: 0x06005EE1 RID: 24289 RVA: 0x00092BCB File Offset: 0x00090DCB
		private AddIPAllowListEntryCommand() : base("Add-IPAllowListEntry")
		{
		}

		// Token: 0x06005EE2 RID: 24290 RVA: 0x00092BD8 File Offset: 0x00090DD8
		public AddIPAllowListEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005EE3 RID: 24291 RVA: 0x00092BE7 File Offset: 0x00090DE7
		public virtual AddIPAllowListEntryCommand SetParameters(AddIPAllowListEntryCommand.IPRangeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005EE4 RID: 24292 RVA: 0x00092BF1 File Offset: 0x00090DF1
		public virtual AddIPAllowListEntryCommand SetParameters(AddIPAllowListEntryCommand.IPAddressParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005EE5 RID: 24293 RVA: 0x00092BFB File Offset: 0x00090DFB
		public virtual AddIPAllowListEntryCommand SetParameters(AddIPAllowListEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000737 RID: 1847
		public class IPRangeParameters : ParametersBase
		{
			// Token: 0x17003C2E RID: 15406
			// (set) Token: 0x06005EE6 RID: 24294 RVA: 0x00092C05 File Offset: 0x00090E05
			public virtual IPRange IPRange
			{
				set
				{
					base.PowerSharpParameters["IPRange"] = value;
				}
			}

			// Token: 0x17003C2F RID: 15407
			// (set) Token: 0x06005EE7 RID: 24295 RVA: 0x00092C18 File Offset: 0x00090E18
			public virtual DateTime ExpirationTime
			{
				set
				{
					base.PowerSharpParameters["ExpirationTime"] = value;
				}
			}

			// Token: 0x17003C30 RID: 15408
			// (set) Token: 0x06005EE8 RID: 24296 RVA: 0x00092C30 File Offset: 0x00090E30
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17003C31 RID: 15409
			// (set) Token: 0x06005EE9 RID: 24297 RVA: 0x00092C43 File Offset: 0x00090E43
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C32 RID: 15410
			// (set) Token: 0x06005EEA RID: 24298 RVA: 0x00092C56 File Offset: 0x00090E56
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C33 RID: 15411
			// (set) Token: 0x06005EEB RID: 24299 RVA: 0x00092C6E File Offset: 0x00090E6E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C34 RID: 15412
			// (set) Token: 0x06005EEC RID: 24300 RVA: 0x00092C86 File Offset: 0x00090E86
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C35 RID: 15413
			// (set) Token: 0x06005EED RID: 24301 RVA: 0x00092C9E File Offset: 0x00090E9E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C36 RID: 15414
			// (set) Token: 0x06005EEE RID: 24302 RVA: 0x00092CB6 File Offset: 0x00090EB6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000738 RID: 1848
		public class IPAddressParameters : ParametersBase
		{
			// Token: 0x17003C37 RID: 15415
			// (set) Token: 0x06005EF0 RID: 24304 RVA: 0x00092CD6 File Offset: 0x00090ED6
			public virtual IPAddress IPAddress
			{
				set
				{
					base.PowerSharpParameters["IPAddress"] = value;
				}
			}

			// Token: 0x17003C38 RID: 15416
			// (set) Token: 0x06005EF1 RID: 24305 RVA: 0x00092CE9 File Offset: 0x00090EE9
			public virtual DateTime ExpirationTime
			{
				set
				{
					base.PowerSharpParameters["ExpirationTime"] = value;
				}
			}

			// Token: 0x17003C39 RID: 15417
			// (set) Token: 0x06005EF2 RID: 24306 RVA: 0x00092D01 File Offset: 0x00090F01
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17003C3A RID: 15418
			// (set) Token: 0x06005EF3 RID: 24307 RVA: 0x00092D14 File Offset: 0x00090F14
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C3B RID: 15419
			// (set) Token: 0x06005EF4 RID: 24308 RVA: 0x00092D27 File Offset: 0x00090F27
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C3C RID: 15420
			// (set) Token: 0x06005EF5 RID: 24309 RVA: 0x00092D3F File Offset: 0x00090F3F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C3D RID: 15421
			// (set) Token: 0x06005EF6 RID: 24310 RVA: 0x00092D57 File Offset: 0x00090F57
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C3E RID: 15422
			// (set) Token: 0x06005EF7 RID: 24311 RVA: 0x00092D6F File Offset: 0x00090F6F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C3F RID: 15423
			// (set) Token: 0x06005EF8 RID: 24312 RVA: 0x00092D87 File Offset: 0x00090F87
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000739 RID: 1849
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C40 RID: 15424
			// (set) Token: 0x06005EFA RID: 24314 RVA: 0x00092DA7 File Offset: 0x00090FA7
			public virtual DateTime ExpirationTime
			{
				set
				{
					base.PowerSharpParameters["ExpirationTime"] = value;
				}
			}

			// Token: 0x17003C41 RID: 15425
			// (set) Token: 0x06005EFB RID: 24315 RVA: 0x00092DBF File Offset: 0x00090FBF
			public virtual string Comment
			{
				set
				{
					base.PowerSharpParameters["Comment"] = value;
				}
			}

			// Token: 0x17003C42 RID: 15426
			// (set) Token: 0x06005EFC RID: 24316 RVA: 0x00092DD2 File Offset: 0x00090FD2
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17003C43 RID: 15427
			// (set) Token: 0x06005EFD RID: 24317 RVA: 0x00092DE5 File Offset: 0x00090FE5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C44 RID: 15428
			// (set) Token: 0x06005EFE RID: 24318 RVA: 0x00092DFD File Offset: 0x00090FFD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C45 RID: 15429
			// (set) Token: 0x06005EFF RID: 24319 RVA: 0x00092E15 File Offset: 0x00091015
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C46 RID: 15430
			// (set) Token: 0x06005F00 RID: 24320 RVA: 0x00092E2D File Offset: 0x0009102D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C47 RID: 15431
			// (set) Token: 0x06005F01 RID: 24321 RVA: 0x00092E45 File Offset: 0x00091045
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
