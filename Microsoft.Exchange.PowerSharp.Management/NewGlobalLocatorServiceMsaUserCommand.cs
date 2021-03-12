using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006B0 RID: 1712
	public class NewGlobalLocatorServiceMsaUserCommand : SyntheticCommandWithPipelineInputNoOutput<Guid>
	{
		// Token: 0x06005A53 RID: 23123 RVA: 0x0008CEE0 File Offset: 0x0008B0E0
		private NewGlobalLocatorServiceMsaUserCommand() : base("New-GlobalLocatorServiceMsaUser")
		{
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x0008CEED File Offset: 0x0008B0ED
		public NewGlobalLocatorServiceMsaUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x0008CEFC File Offset: 0x0008B0FC
		public virtual NewGlobalLocatorServiceMsaUserCommand SetParameters(NewGlobalLocatorServiceMsaUserCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x0008CF06 File Offset: 0x0008B106
		public virtual NewGlobalLocatorServiceMsaUserCommand SetParameters(NewGlobalLocatorServiceMsaUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006B1 RID: 1713
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x170038AC RID: 14508
			// (set) Token: 0x06005A57 RID: 23127 RVA: 0x0008CF10 File Offset: 0x0008B110
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170038AD RID: 14509
			// (set) Token: 0x06005A58 RID: 23128 RVA: 0x0008CF28 File Offset: 0x0008B128
			public virtual SmtpAddress MsaUserMemberName
			{
				set
				{
					base.PowerSharpParameters["MsaUserMemberName"] = value;
				}
			}

			// Token: 0x170038AE RID: 14510
			// (set) Token: 0x06005A59 RID: 23129 RVA: 0x0008CF40 File Offset: 0x0008B140
			public virtual NetID MsaUserNetId
			{
				set
				{
					base.PowerSharpParameters["MsaUserNetId"] = value;
				}
			}

			// Token: 0x170038AF RID: 14511
			// (set) Token: 0x06005A5A RID: 23130 RVA: 0x0008CF53 File Offset: 0x0008B153
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038B0 RID: 14512
			// (set) Token: 0x06005A5B RID: 23131 RVA: 0x0008CF6B File Offset: 0x0008B16B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038B1 RID: 14513
			// (set) Token: 0x06005A5C RID: 23132 RVA: 0x0008CF83 File Offset: 0x0008B183
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038B2 RID: 14514
			// (set) Token: 0x06005A5D RID: 23133 RVA: 0x0008CF9B File Offset: 0x0008B19B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170038B3 RID: 14515
			// (set) Token: 0x06005A5E RID: 23134 RVA: 0x0008CFB3 File Offset: 0x0008B1B3
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006B2 RID: 1714
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170038B4 RID: 14516
			// (set) Token: 0x06005A60 RID: 23136 RVA: 0x0008CFD3 File Offset: 0x0008B1D3
			public virtual SmtpAddress MsaUserMemberName
			{
				set
				{
					base.PowerSharpParameters["MsaUserMemberName"] = value;
				}
			}

			// Token: 0x170038B5 RID: 14517
			// (set) Token: 0x06005A61 RID: 23137 RVA: 0x0008CFEB File Offset: 0x0008B1EB
			public virtual NetID MsaUserNetId
			{
				set
				{
					base.PowerSharpParameters["MsaUserNetId"] = value;
				}
			}

			// Token: 0x170038B6 RID: 14518
			// (set) Token: 0x06005A62 RID: 23138 RVA: 0x0008CFFE File Offset: 0x0008B1FE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038B7 RID: 14519
			// (set) Token: 0x06005A63 RID: 23139 RVA: 0x0008D016 File Offset: 0x0008B216
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038B8 RID: 14520
			// (set) Token: 0x06005A64 RID: 23140 RVA: 0x0008D02E File Offset: 0x0008B22E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038B9 RID: 14521
			// (set) Token: 0x06005A65 RID: 23141 RVA: 0x0008D046 File Offset: 0x0008B246
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170038BA RID: 14522
			// (set) Token: 0x06005A66 RID: 23142 RVA: 0x0008D05E File Offset: 0x0008B25E
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
