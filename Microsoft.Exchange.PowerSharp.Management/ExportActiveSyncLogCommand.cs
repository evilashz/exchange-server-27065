using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000028 RID: 40
	public class ExportActiveSyncLogCommand : SyntheticCommandWithPipelineInputNoOutput<string>
	{
		// Token: 0x0600157A RID: 5498 RVA: 0x000339AF File Offset: 0x00031BAF
		private ExportActiveSyncLogCommand() : base("Export-ActiveSyncLog")
		{
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000339BC File Offset: 0x00031BBC
		public ExportActiveSyncLogCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000339CB File Offset: 0x00031BCB
		public virtual ExportActiveSyncLogCommand SetParameters(ExportActiveSyncLogCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000029 RID: 41
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170000E3 RID: 227
			// (set) Token: 0x0600157D RID: 5501 RVA: 0x000339D5 File Offset: 0x00031BD5
			public virtual string Filename
			{
				set
				{
					base.PowerSharpParameters["Filename"] = value;
				}
			}

			// Token: 0x170000E4 RID: 228
			// (set) Token: 0x0600157E RID: 5502 RVA: 0x000339E8 File Offset: 0x00031BE8
			public virtual DateTime StartDate
			{
				set
				{
					base.PowerSharpParameters["StartDate"] = value;
				}
			}

			// Token: 0x170000E5 RID: 229
			// (set) Token: 0x0600157F RID: 5503 RVA: 0x00033A00 File Offset: 0x00031C00
			public virtual DateTime EndDate
			{
				set
				{
					base.PowerSharpParameters["EndDate"] = value;
				}
			}

			// Token: 0x170000E6 RID: 230
			// (set) Token: 0x06001580 RID: 5504 RVA: 0x00033A18 File Offset: 0x00031C18
			public virtual SwitchParameter UseGMT
			{
				set
				{
					base.PowerSharpParameters["UseGMT"] = value;
				}
			}

			// Token: 0x170000E7 RID: 231
			// (set) Token: 0x06001581 RID: 5505 RVA: 0x00033A30 File Offset: 0x00031C30
			public virtual string OutputPrefix
			{
				set
				{
					base.PowerSharpParameters["OutputPrefix"] = value;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (set) Token: 0x06001582 RID: 5506 RVA: 0x00033A43 File Offset: 0x00031C43
			public virtual string OutputPath
			{
				set
				{
					base.PowerSharpParameters["OutputPath"] = value;
				}
			}

			// Token: 0x170000E9 RID: 233
			// (set) Token: 0x06001583 RID: 5507 RVA: 0x00033A56 File Offset: 0x00031C56
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170000EA RID: 234
			// (set) Token: 0x06001584 RID: 5508 RVA: 0x00033A6E File Offset: 0x00031C6E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170000EB RID: 235
			// (set) Token: 0x06001585 RID: 5509 RVA: 0x00033A86 File Offset: 0x00031C86
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170000EC RID: 236
			// (set) Token: 0x06001586 RID: 5510 RVA: 0x00033A9E File Offset: 0x00031C9E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170000ED RID: 237
			// (set) Token: 0x06001587 RID: 5511 RVA: 0x00033AB6 File Offset: 0x00031CB6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170000EE RID: 238
			// (set) Token: 0x06001588 RID: 5512 RVA: 0x00033ACE File Offset: 0x00031CCE
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
