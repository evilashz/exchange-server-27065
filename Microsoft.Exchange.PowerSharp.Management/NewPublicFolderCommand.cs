using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000245 RID: 581
	public class NewPublicFolderCommand : SyntheticCommandWithPipelineInput<PublicFolder, PublicFolder>
	{
		// Token: 0x06002B78 RID: 11128 RVA: 0x000502E4 File Offset: 0x0004E4E4
		private NewPublicFolderCommand() : base("New-PublicFolder")
		{
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000502F1 File Offset: 0x0004E4F1
		public NewPublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x00050300 File Offset: 0x0004E500
		public virtual NewPublicFolderCommand SetParameters(NewPublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000246 RID: 582
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170012A7 RID: 4775
			// (set) Token: 0x06002B7B RID: 11131 RVA: 0x0005030A File Offset: 0x0004E50A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170012A8 RID: 4776
			// (set) Token: 0x06002B7C RID: 11132 RVA: 0x00050328 File Offset: 0x0004E528
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170012A9 RID: 4777
			// (set) Token: 0x06002B7D RID: 11133 RVA: 0x00050346 File Offset: 0x0004E546
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170012AA RID: 4778
			// (set) Token: 0x06002B7E RID: 11134 RVA: 0x00050359 File Offset: 0x0004E559
			public virtual string Path
			{
				set
				{
					base.PowerSharpParameters["Path"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x170012AB RID: 4779
			// (set) Token: 0x06002B7F RID: 11135 RVA: 0x00050377 File Offset: 0x0004E577
			public virtual CultureInfo EformsLocaleId
			{
				set
				{
					base.PowerSharpParameters["EformsLocaleId"] = value;
				}
			}

			// Token: 0x170012AC RID: 4780
			// (set) Token: 0x06002B80 RID: 11136 RVA: 0x0005038A File Offset: 0x0004E58A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170012AD RID: 4781
			// (set) Token: 0x06002B81 RID: 11137 RVA: 0x0005039D File Offset: 0x0004E59D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170012AE RID: 4782
			// (set) Token: 0x06002B82 RID: 11138 RVA: 0x000503B5 File Offset: 0x0004E5B5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170012AF RID: 4783
			// (set) Token: 0x06002B83 RID: 11139 RVA: 0x000503CD File Offset: 0x0004E5CD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170012B0 RID: 4784
			// (set) Token: 0x06002B84 RID: 11140 RVA: 0x000503E5 File Offset: 0x0004E5E5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170012B1 RID: 4785
			// (set) Token: 0x06002B85 RID: 11141 RVA: 0x000503FD File Offset: 0x0004E5FD
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
