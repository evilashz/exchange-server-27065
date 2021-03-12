using System;
using System.Management.Automation;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000086 RID: 134
	public class SendComplianceMessageCommand : SyntheticCommandWithPipelineInput<bool, bool>
	{
		// Token: 0x060018AF RID: 6319 RVA: 0x00037A80 File Offset: 0x00035C80
		private SendComplianceMessageCommand() : base("Send-ComplianceMessage")
		{
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00037A8D File Offset: 0x00035C8D
		public SendComplianceMessageCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00037A9C File Offset: 0x00035C9C
		public virtual SendComplianceMessageCommand SetParameters(SendComplianceMessageCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000087 RID: 135
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700035C RID: 860
			// (set) Token: 0x060018B2 RID: 6322 RVA: 0x00037AA6 File Offset: 0x00035CA6
			public virtual byte SerializedComplianceMessage
			{
				set
				{
					base.PowerSharpParameters["SerializedComplianceMessage"] = value;
				}
			}

			// Token: 0x1700035D RID: 861
			// (set) Token: 0x060018B3 RID: 6323 RVA: 0x00037ABE File Offset: 0x00035CBE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700035E RID: 862
			// (set) Token: 0x060018B4 RID: 6324 RVA: 0x00037AD6 File Offset: 0x00035CD6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700035F RID: 863
			// (set) Token: 0x060018B5 RID: 6325 RVA: 0x00037AEE File Offset: 0x00035CEE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000360 RID: 864
			// (set) Token: 0x060018B6 RID: 6326 RVA: 0x00037B06 File Offset: 0x00035D06
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
