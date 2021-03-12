using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200046A RID: 1130
	public class CompareTextMessagingVerificationCodeCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x06004088 RID: 16520 RVA: 0x0006B824 File Offset: 0x00069A24
		private CompareTextMessagingVerificationCodeCommand() : base("Compare-TextMessagingVerificationCode")
		{
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x0006B831 File Offset: 0x00069A31
		public CompareTextMessagingVerificationCodeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x0006B840 File Offset: 0x00069A40
		public virtual CompareTextMessagingVerificationCodeCommand SetParameters(CompareTextMessagingVerificationCodeCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x0006B84A File Offset: 0x00069A4A
		public virtual CompareTextMessagingVerificationCodeCommand SetParameters(CompareTextMessagingVerificationCodeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200046B RID: 1131
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700236D RID: 9069
			// (set) Token: 0x0600408C RID: 16524 RVA: 0x0006B854 File Offset: 0x00069A54
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700236E RID: 9070
			// (set) Token: 0x0600408D RID: 16525 RVA: 0x0006B872 File Offset: 0x00069A72
			public virtual string VerificationCode
			{
				set
				{
					base.PowerSharpParameters["VerificationCode"] = value;
				}
			}

			// Token: 0x1700236F RID: 9071
			// (set) Token: 0x0600408E RID: 16526 RVA: 0x0006B885 File Offset: 0x00069A85
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002370 RID: 9072
			// (set) Token: 0x0600408F RID: 16527 RVA: 0x0006B898 File Offset: 0x00069A98
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002371 RID: 9073
			// (set) Token: 0x06004090 RID: 16528 RVA: 0x0006B8B0 File Offset: 0x00069AB0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002372 RID: 9074
			// (set) Token: 0x06004091 RID: 16529 RVA: 0x0006B8C8 File Offset: 0x00069AC8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002373 RID: 9075
			// (set) Token: 0x06004092 RID: 16530 RVA: 0x0006B8E0 File Offset: 0x00069AE0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002374 RID: 9076
			// (set) Token: 0x06004093 RID: 16531 RVA: 0x0006B8F8 File Offset: 0x00069AF8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200046C RID: 1132
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002375 RID: 9077
			// (set) Token: 0x06004095 RID: 16533 RVA: 0x0006B918 File Offset: 0x00069B18
			public virtual string VerificationCode
			{
				set
				{
					base.PowerSharpParameters["VerificationCode"] = value;
				}
			}

			// Token: 0x17002376 RID: 9078
			// (set) Token: 0x06004096 RID: 16534 RVA: 0x0006B92B File Offset: 0x00069B2B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002377 RID: 9079
			// (set) Token: 0x06004097 RID: 16535 RVA: 0x0006B93E File Offset: 0x00069B3E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002378 RID: 9080
			// (set) Token: 0x06004098 RID: 16536 RVA: 0x0006B956 File Offset: 0x00069B56
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002379 RID: 9081
			// (set) Token: 0x06004099 RID: 16537 RVA: 0x0006B96E File Offset: 0x00069B6E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700237A RID: 9082
			// (set) Token: 0x0600409A RID: 16538 RVA: 0x0006B986 File Offset: 0x00069B86
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700237B RID: 9083
			// (set) Token: 0x0600409B RID: 16539 RVA: 0x0006B99E File Offset: 0x00069B9E
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
