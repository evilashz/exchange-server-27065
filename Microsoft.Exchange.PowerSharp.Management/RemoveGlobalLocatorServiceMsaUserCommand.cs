using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006B3 RID: 1715
	public class RemoveGlobalLocatorServiceMsaUserCommand : SyntheticCommandWithPipelineInputNoOutput<NetID>
	{
		// Token: 0x06005A68 RID: 23144 RVA: 0x0008D07E File Offset: 0x0008B27E
		private RemoveGlobalLocatorServiceMsaUserCommand() : base("Remove-GlobalLocatorServiceMsaUser")
		{
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x0008D08B File Offset: 0x0008B28B
		public RemoveGlobalLocatorServiceMsaUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x0008D09A File Offset: 0x0008B29A
		public virtual RemoveGlobalLocatorServiceMsaUserCommand SetParameters(RemoveGlobalLocatorServiceMsaUserCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006B4 RID: 1716
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x170038BB RID: 14523
			// (set) Token: 0x06005A6B RID: 23147 RVA: 0x0008D0A4 File Offset: 0x0008B2A4
			public virtual NetID MsaUserNetId
			{
				set
				{
					base.PowerSharpParameters["MsaUserNetId"] = value;
				}
			}

			// Token: 0x170038BC RID: 14524
			// (set) Token: 0x06005A6C RID: 23148 RVA: 0x0008D0B7 File Offset: 0x0008B2B7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038BD RID: 14525
			// (set) Token: 0x06005A6D RID: 23149 RVA: 0x0008D0CF File Offset: 0x0008B2CF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038BE RID: 14526
			// (set) Token: 0x06005A6E RID: 23150 RVA: 0x0008D0E7 File Offset: 0x0008B2E7
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038BF RID: 14527
			// (set) Token: 0x06005A6F RID: 23151 RVA: 0x0008D0FF File Offset: 0x0008B2FF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170038C0 RID: 14528
			// (set) Token: 0x06005A70 RID: 23152 RVA: 0x0008D117 File Offset: 0x0008B317
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170038C1 RID: 14529
			// (set) Token: 0x06005A71 RID: 23153 RVA: 0x0008D12F File Offset: 0x0008B32F
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
