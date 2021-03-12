using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000734 RID: 1844
	public class EnableAntispamUpdatesCommand : SyntheticCommand<object>
	{
		// Token: 0x06005ED6 RID: 24278 RVA: 0x00092AFA File Offset: 0x00090CFA
		private EnableAntispamUpdatesCommand() : base("Enable-AntispamUpdates")
		{
		}

		// Token: 0x06005ED7 RID: 24279 RVA: 0x00092B07 File Offset: 0x00090D07
		public EnableAntispamUpdatesCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005ED8 RID: 24280 RVA: 0x00092B16 File Offset: 0x00090D16
		public virtual EnableAntispamUpdatesCommand SetParameters(EnableAntispamUpdatesCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000735 RID: 1845
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17003C27 RID: 15399
			// (set) Token: 0x06005ED9 RID: 24281 RVA: 0x00092B20 File Offset: 0x00090D20
			public virtual ServerIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17003C28 RID: 15400
			// (set) Token: 0x06005EDA RID: 24282 RVA: 0x00092B33 File Offset: 0x00090D33
			public virtual bool SpamSignatureUpdatesEnabled
			{
				set
				{
					base.PowerSharpParameters["SpamSignatureUpdatesEnabled"] = value;
				}
			}

			// Token: 0x17003C29 RID: 15401
			// (set) Token: 0x06005EDB RID: 24283 RVA: 0x00092B4B File Offset: 0x00090D4B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17003C2A RID: 15402
			// (set) Token: 0x06005EDC RID: 24284 RVA: 0x00092B63 File Offset: 0x00090D63
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17003C2B RID: 15403
			// (set) Token: 0x06005EDD RID: 24285 RVA: 0x00092B7B File Offset: 0x00090D7B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17003C2C RID: 15404
			// (set) Token: 0x06005EDE RID: 24286 RVA: 0x00092B93 File Offset: 0x00090D93
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17003C2D RID: 15405
			// (set) Token: 0x06005EDF RID: 24287 RVA: 0x00092BAB File Offset: 0x00090DAB
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
