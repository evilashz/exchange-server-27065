using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020000B0 RID: 176
	public class GetProcessModuleCommand : SyntheticCommandWithPipelineInputNoOutput<int>
	{
		// Token: 0x06001A3A RID: 6714 RVA: 0x000399E4 File Offset: 0x00037BE4
		private GetProcessModuleCommand() : base("Get-ProcessModule")
		{
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x000399F1 File Offset: 0x00037BF1
		public GetProcessModuleCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00039A00 File Offset: 0x00037C00
		public virtual GetProcessModuleCommand SetParameters(GetProcessModuleCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020000B1 RID: 177
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000493 RID: 1171
			// (set) Token: 0x06001A3D RID: 6717 RVA: 0x00039A0A File Offset: 0x00037C0A
			public virtual int ProcessId
			{
				set
				{
					base.PowerSharpParameters["ProcessId"] = value;
				}
			}

			// Token: 0x17000494 RID: 1172
			// (set) Token: 0x06001A3E RID: 6718 RVA: 0x00039A22 File Offset: 0x00037C22
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000495 RID: 1173
			// (set) Token: 0x06001A3F RID: 6719 RVA: 0x00039A3A File Offset: 0x00037C3A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000496 RID: 1174
			// (set) Token: 0x06001A40 RID: 6720 RVA: 0x00039A52 File Offset: 0x00037C52
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000497 RID: 1175
			// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00039A6A File Offset: 0x00037C6A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
