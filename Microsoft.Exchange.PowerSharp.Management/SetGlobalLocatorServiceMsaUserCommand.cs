using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006B5 RID: 1717
	public class SetGlobalLocatorServiceMsaUserCommand : SyntheticCommandWithPipelineInputNoOutput<NetID>
	{
		// Token: 0x06005A73 RID: 23155 RVA: 0x0008D14F File Offset: 0x0008B34F
		private SetGlobalLocatorServiceMsaUserCommand() : base("Set-GlobalLocatorServiceMsaUser")
		{
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x0008D15C File Offset: 0x0008B35C
		public SetGlobalLocatorServiceMsaUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005A75 RID: 23157 RVA: 0x0008D16B File Offset: 0x0008B36B
		public virtual SetGlobalLocatorServiceMsaUserCommand SetParameters(SetGlobalLocatorServiceMsaUserCommand.MsaUserNetIDParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005A76 RID: 23158 RVA: 0x0008D175 File Offset: 0x0008B375
		public virtual SetGlobalLocatorServiceMsaUserCommand SetParameters(SetGlobalLocatorServiceMsaUserCommand.ExternalDirectoryOrganizationIdParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006B6 RID: 1718
		public class MsaUserNetIDParameterSetParameters : ParametersBase
		{
			// Token: 0x170038C2 RID: 14530
			// (set) Token: 0x06005A77 RID: 23159 RVA: 0x0008D17F File Offset: 0x0008B37F
			public virtual SmtpAddress MsaUserMemberName
			{
				set
				{
					base.PowerSharpParameters["MsaUserMemberName"] = value;
				}
			}

			// Token: 0x170038C3 RID: 14531
			// (set) Token: 0x06005A78 RID: 23160 RVA: 0x0008D197 File Offset: 0x0008B397
			public virtual NetID MsaUserNetId
			{
				set
				{
					base.PowerSharpParameters["MsaUserNetId"] = value;
				}
			}

			// Token: 0x170038C4 RID: 14532
			// (set) Token: 0x06005A79 RID: 23161 RVA: 0x0008D1AA File Offset: 0x0008B3AA
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170038C5 RID: 14533
			// (set) Token: 0x06005A7A RID: 23162 RVA: 0x0008D1C2 File Offset: 0x0008B3C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038C6 RID: 14534
			// (set) Token: 0x06005A7B RID: 23163 RVA: 0x0008D1DA File Offset: 0x0008B3DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038C7 RID: 14535
			// (set) Token: 0x06005A7C RID: 23164 RVA: 0x0008D1F2 File Offset: 0x0008B3F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038C8 RID: 14536
			// (set) Token: 0x06005A7D RID: 23165 RVA: 0x0008D20A File Offset: 0x0008B40A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170038C9 RID: 14537
			// (set) Token: 0x06005A7E RID: 23166 RVA: 0x0008D222 File Offset: 0x0008B422
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170038CA RID: 14538
			// (set) Token: 0x06005A7F RID: 23167 RVA: 0x0008D23A File Offset: 0x0008B43A
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x020006B7 RID: 1719
		public class ExternalDirectoryOrganizationIdParameterSetParameters : ParametersBase
		{
			// Token: 0x170038CB RID: 14539
			// (set) Token: 0x06005A81 RID: 23169 RVA: 0x0008D25A File Offset: 0x0008B45A
			public virtual Guid ExternalDirectoryOrganizationId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryOrganizationId"] = value;
				}
			}

			// Token: 0x170038CC RID: 14540
			// (set) Token: 0x06005A82 RID: 23170 RVA: 0x0008D272 File Offset: 0x0008B472
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170038CD RID: 14541
			// (set) Token: 0x06005A83 RID: 23171 RVA: 0x0008D28A File Offset: 0x0008B48A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170038CE RID: 14542
			// (set) Token: 0x06005A84 RID: 23172 RVA: 0x0008D2A2 File Offset: 0x0008B4A2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170038CF RID: 14543
			// (set) Token: 0x06005A85 RID: 23173 RVA: 0x0008D2BA File Offset: 0x0008B4BA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170038D0 RID: 14544
			// (set) Token: 0x06005A86 RID: 23174 RVA: 0x0008D2D2 File Offset: 0x0008B4D2
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170038D1 RID: 14545
			// (set) Token: 0x06005A87 RID: 23175 RVA: 0x0008D2EA File Offset: 0x0008B4EA
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
