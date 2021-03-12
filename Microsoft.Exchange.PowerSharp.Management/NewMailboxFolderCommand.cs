using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200048D RID: 1165
	public class NewMailboxFolderCommand : SyntheticCommandWithPipelineInput<MailboxFolder, MailboxFolder>
	{
		// Token: 0x060041B1 RID: 16817 RVA: 0x0006CFCC File Offset: 0x0006B1CC
		private NewMailboxFolderCommand() : base("New-MailboxFolder")
		{
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x0006CFD9 File Offset: 0x0006B1D9
		public NewMailboxFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x0006CFE8 File Offset: 0x0006B1E8
		public virtual NewMailboxFolderCommand SetParameters(NewMailboxFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200048E RID: 1166
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002450 RID: 9296
			// (set) Token: 0x060041B4 RID: 16820 RVA: 0x0006CFF2 File Offset: 0x0006B1F2
			public virtual string Parent
			{
				set
				{
					base.PowerSharpParameters["Parent"] = ((value != null) ? new MailboxFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17002451 RID: 9297
			// (set) Token: 0x060041B5 RID: 16821 RVA: 0x0006D010 File Offset: 0x0006B210
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002452 RID: 9298
			// (set) Token: 0x060041B6 RID: 16822 RVA: 0x0006D023 File Offset: 0x0006B223
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002453 RID: 9299
			// (set) Token: 0x060041B7 RID: 16823 RVA: 0x0006D036 File Offset: 0x0006B236
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002454 RID: 9300
			// (set) Token: 0x060041B8 RID: 16824 RVA: 0x0006D04E File Offset: 0x0006B24E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002455 RID: 9301
			// (set) Token: 0x060041B9 RID: 16825 RVA: 0x0006D066 File Offset: 0x0006B266
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002456 RID: 9302
			// (set) Token: 0x060041BA RID: 16826 RVA: 0x0006D07E File Offset: 0x0006B27E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002457 RID: 9303
			// (set) Token: 0x060041BB RID: 16827 RVA: 0x0006D096 File Offset: 0x0006B296
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
