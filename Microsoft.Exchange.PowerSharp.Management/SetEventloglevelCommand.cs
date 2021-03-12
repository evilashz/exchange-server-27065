using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000107 RID: 263
	public class SetEventloglevelCommand : SyntheticCommandWithPipelineInputNoOutput<EventCategoryObject>
	{
		// Token: 0x06001EEF RID: 7919 RVA: 0x0003FDC5 File Offset: 0x0003DFC5
		private SetEventloglevelCommand() : base("Set-Eventloglevel")
		{
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0003FDD2 File Offset: 0x0003DFD2
		public SetEventloglevelCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0003FDE1 File Offset: 0x0003DFE1
		public virtual SetEventloglevelCommand SetParameters(SetEventloglevelCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0003FDEB File Offset: 0x0003DFEB
		public virtual SetEventloglevelCommand SetParameters(SetEventloglevelCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000108 RID: 264
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700089A RID: 2202
			// (set) Token: 0x06001EF3 RID: 7923 RVA: 0x0003FDF5 File Offset: 0x0003DFF5
			public virtual ECIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x1700089B RID: 2203
			// (set) Token: 0x06001EF4 RID: 7924 RVA: 0x0003FE08 File Offset: 0x0003E008
			public virtual ExEventLog.EventLevel Level
			{
				set
				{
					base.PowerSharpParameters["Level"] = value;
				}
			}

			// Token: 0x1700089C RID: 2204
			// (set) Token: 0x06001EF5 RID: 7925 RVA: 0x0003FE20 File Offset: 0x0003E020
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700089D RID: 2205
			// (set) Token: 0x06001EF6 RID: 7926 RVA: 0x0003FE38 File Offset: 0x0003E038
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700089E RID: 2206
			// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x0003FE50 File Offset: 0x0003E050
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700089F RID: 2207
			// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x0003FE68 File Offset: 0x0003E068
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170008A0 RID: 2208
			// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x0003FE80 File Offset: 0x0003E080
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000109 RID: 265
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170008A1 RID: 2209
			// (set) Token: 0x06001EFB RID: 7931 RVA: 0x0003FEA0 File Offset: 0x0003E0A0
			public virtual ExEventLog.EventLevel Level
			{
				set
				{
					base.PowerSharpParameters["Level"] = value;
				}
			}

			// Token: 0x170008A2 RID: 2210
			// (set) Token: 0x06001EFC RID: 7932 RVA: 0x0003FEB8 File Offset: 0x0003E0B8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170008A3 RID: 2211
			// (set) Token: 0x06001EFD RID: 7933 RVA: 0x0003FED0 File Offset: 0x0003E0D0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170008A4 RID: 2212
			// (set) Token: 0x06001EFE RID: 7934 RVA: 0x0003FEE8 File Offset: 0x0003E0E8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170008A5 RID: 2213
			// (set) Token: 0x06001EFF RID: 7935 RVA: 0x0003FF00 File Offset: 0x0003E100
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170008A6 RID: 2214
			// (set) Token: 0x06001F00 RID: 7936 RVA: 0x0003FF18 File Offset: 0x0003E118
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
