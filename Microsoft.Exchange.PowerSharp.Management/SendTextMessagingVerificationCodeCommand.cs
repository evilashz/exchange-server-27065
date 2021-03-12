using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000470 RID: 1136
	public class SendTextMessagingVerificationCodeCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x060040B4 RID: 16564 RVA: 0x0006BB88 File Offset: 0x00069D88
		private SendTextMessagingVerificationCodeCommand() : base("Send-TextMessagingVerificationCode")
		{
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x0006BB95 File Offset: 0x00069D95
		public SendTextMessagingVerificationCodeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x0006BBA4 File Offset: 0x00069DA4
		public virtual SendTextMessagingVerificationCodeCommand SetParameters(SendTextMessagingVerificationCodeCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x0006BBAE File Offset: 0x00069DAE
		public virtual SendTextMessagingVerificationCodeCommand SetParameters(SendTextMessagingVerificationCodeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000471 RID: 1137
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700238D RID: 9101
			// (set) Token: 0x060040B8 RID: 16568 RVA: 0x0006BBB8 File Offset: 0x00069DB8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700238E RID: 9102
			// (set) Token: 0x060040B9 RID: 16569 RVA: 0x0006BBD6 File Offset: 0x00069DD6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700238F RID: 9103
			// (set) Token: 0x060040BA RID: 16570 RVA: 0x0006BBE9 File Offset: 0x00069DE9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002390 RID: 9104
			// (set) Token: 0x060040BB RID: 16571 RVA: 0x0006BC01 File Offset: 0x00069E01
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002391 RID: 9105
			// (set) Token: 0x060040BC RID: 16572 RVA: 0x0006BC19 File Offset: 0x00069E19
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002392 RID: 9106
			// (set) Token: 0x060040BD RID: 16573 RVA: 0x0006BC31 File Offset: 0x00069E31
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002393 RID: 9107
			// (set) Token: 0x060040BE RID: 16574 RVA: 0x0006BC49 File Offset: 0x00069E49
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000472 RID: 1138
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002394 RID: 9108
			// (set) Token: 0x060040C0 RID: 16576 RVA: 0x0006BC69 File Offset: 0x00069E69
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002395 RID: 9109
			// (set) Token: 0x060040C1 RID: 16577 RVA: 0x0006BC7C File Offset: 0x00069E7C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002396 RID: 9110
			// (set) Token: 0x060040C2 RID: 16578 RVA: 0x0006BC94 File Offset: 0x00069E94
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002397 RID: 9111
			// (set) Token: 0x060040C3 RID: 16579 RVA: 0x0006BCAC File Offset: 0x00069EAC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002398 RID: 9112
			// (set) Token: 0x060040C4 RID: 16580 RVA: 0x0006BCC4 File Offset: 0x00069EC4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002399 RID: 9113
			// (set) Token: 0x060040C5 RID: 16581 RVA: 0x0006BCDC File Offset: 0x00069EDC
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
