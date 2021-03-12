using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000482 RID: 1154
	public class GetMailboxCalendarFolderCommand : SyntheticCommandWithPipelineInput<MailboxCalendarFolder, MailboxCalendarFolder>
	{
		// Token: 0x0600415A RID: 16730 RVA: 0x0006C8DA File Offset: 0x0006AADA
		private GetMailboxCalendarFolderCommand() : base("Get-MailboxCalendarFolder")
		{
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x0006C8E7 File Offset: 0x0006AAE7
		public GetMailboxCalendarFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x0006C8F6 File Offset: 0x0006AAF6
		public virtual GetMailboxCalendarFolderCommand SetParameters(GetMailboxCalendarFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x0006C900 File Offset: 0x0006AB00
		public virtual GetMailboxCalendarFolderCommand SetParameters(GetMailboxCalendarFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000483 RID: 1155
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700240F RID: 9231
			// (set) Token: 0x0600415E RID: 16734 RVA: 0x0006C90A File Offset: 0x0006AB0A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002410 RID: 9232
			// (set) Token: 0x0600415F RID: 16735 RVA: 0x0006C928 File Offset: 0x0006AB28
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002411 RID: 9233
			// (set) Token: 0x06004160 RID: 16736 RVA: 0x0006C93B File Offset: 0x0006AB3B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002412 RID: 9234
			// (set) Token: 0x06004161 RID: 16737 RVA: 0x0006C953 File Offset: 0x0006AB53
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002413 RID: 9235
			// (set) Token: 0x06004162 RID: 16738 RVA: 0x0006C96B File Offset: 0x0006AB6B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002414 RID: 9236
			// (set) Token: 0x06004163 RID: 16739 RVA: 0x0006C983 File Offset: 0x0006AB83
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000484 RID: 1156
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002415 RID: 9237
			// (set) Token: 0x06004165 RID: 16741 RVA: 0x0006C9A3 File Offset: 0x0006ABA3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002416 RID: 9238
			// (set) Token: 0x06004166 RID: 16742 RVA: 0x0006C9B6 File Offset: 0x0006ABB6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002417 RID: 9239
			// (set) Token: 0x06004167 RID: 16743 RVA: 0x0006C9CE File Offset: 0x0006ABCE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002418 RID: 9240
			// (set) Token: 0x06004168 RID: 16744 RVA: 0x0006C9E6 File Offset: 0x0006ABE6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002419 RID: 9241
			// (set) Token: 0x06004169 RID: 16745 RVA: 0x0006C9FE File Offset: 0x0006ABFE
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
