using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Providers;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000465 RID: 1125
	public class NewMailMessageCommand : SyntheticCommandWithPipelineInput<MailMessage, MailMessage>
	{
		// Token: 0x0600405E RID: 16478 RVA: 0x0006B4DD File Offset: 0x000696DD
		private NewMailMessageCommand() : base("New-MailMessage")
		{
		}

		// Token: 0x0600405F RID: 16479 RVA: 0x0006B4EA File Offset: 0x000696EA
		public NewMailMessageCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004060 RID: 16480 RVA: 0x0006B4F9 File Offset: 0x000696F9
		public virtual NewMailMessageCommand SetParameters(NewMailMessageCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000466 RID: 1126
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700234D RID: 9037
			// (set) Token: 0x06004061 RID: 16481 RVA: 0x0006B503 File Offset: 0x00069703
			public virtual string Subject
			{
				set
				{
					base.PowerSharpParameters["Subject"] = value;
				}
			}

			// Token: 0x1700234E RID: 9038
			// (set) Token: 0x06004062 RID: 16482 RVA: 0x0006B516 File Offset: 0x00069716
			public virtual string Body
			{
				set
				{
					base.PowerSharpParameters["Body"] = value;
				}
			}

			// Token: 0x1700234F RID: 9039
			// (set) Token: 0x06004063 RID: 16483 RVA: 0x0006B529 File Offset: 0x00069729
			public virtual MailBodyFormat BodyFormat
			{
				set
				{
					base.PowerSharpParameters["BodyFormat"] = value;
				}
			}

			// Token: 0x17002350 RID: 9040
			// (set) Token: 0x06004064 RID: 16484 RVA: 0x0006B541 File Offset: 0x00069741
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002351 RID: 9041
			// (set) Token: 0x06004065 RID: 16485 RVA: 0x0006B554 File Offset: 0x00069754
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002352 RID: 9042
			// (set) Token: 0x06004066 RID: 16486 RVA: 0x0006B56C File Offset: 0x0006976C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002353 RID: 9043
			// (set) Token: 0x06004067 RID: 16487 RVA: 0x0006B584 File Offset: 0x00069784
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002354 RID: 9044
			// (set) Token: 0x06004068 RID: 16488 RVA: 0x0006B59C File Offset: 0x0006979C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002355 RID: 9045
			// (set) Token: 0x06004069 RID: 16489 RVA: 0x0006B5B4 File Offset: 0x000697B4
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
