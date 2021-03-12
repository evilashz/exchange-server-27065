using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006AE RID: 1710
	public class GetGlobalLocatorServiceMsaUserCommand : SyntheticCommandWithPipelineInputNoOutput<NetID>
	{
		// Token: 0x06005A4A RID: 23114 RVA: 0x0008CE3F File Offset: 0x0008B03F
		private GetGlobalLocatorServiceMsaUserCommand() : base("Get-GlobalLocatorServiceMsaUser")
		{
		}

		// Token: 0x06005A4B RID: 23115 RVA: 0x0008CE4C File Offset: 0x0008B04C
		public GetGlobalLocatorServiceMsaUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005A4C RID: 23116 RVA: 0x0008CE5B File Offset: 0x0008B05B
		public virtual GetGlobalLocatorServiceMsaUserCommand SetParameters(GetGlobalLocatorServiceMsaUserCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006AF RID: 1711
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x170038A7 RID: 14503
			// (set) Token: 0x06005A4D RID: 23117 RVA: 0x0008CE65 File Offset: 0x0008B065
			public virtual NetID MsaUserNetId
			{
				set
				{
					base.PowerSharpParameters["MsaUserNetId"] = value;
				}
			}

			// Token: 0x170038A8 RID: 14504
			// (set) Token: 0x06005A4E RID: 23118 RVA: 0x0008CE78 File Offset: 0x0008B078
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038A9 RID: 14505
			// (set) Token: 0x06005A4F RID: 23119 RVA: 0x0008CE90 File Offset: 0x0008B090
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038AA RID: 14506
			// (set) Token: 0x06005A50 RID: 23120 RVA: 0x0008CEA8 File Offset: 0x0008B0A8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038AB RID: 14507
			// (set) Token: 0x06005A51 RID: 23121 RVA: 0x0008CEC0 File Offset: 0x0008B0C0
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
