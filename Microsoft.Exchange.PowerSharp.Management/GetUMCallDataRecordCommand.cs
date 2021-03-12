using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000B45 RID: 2885
	public class GetUMCallDataRecordCommand : SyntheticCommandWithPipelineInput<MailboxIdParameter, MailboxIdParameter>
	{
		// Token: 0x06008CA2 RID: 36002 RVA: 0x000CE469 File Offset: 0x000CC669
		private GetUMCallDataRecordCommand() : base("Get-UMCallDataRecord")
		{
		}

		// Token: 0x06008CA3 RID: 36003 RVA: 0x000CE476 File Offset: 0x000CC676
		public GetUMCallDataRecordCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008CA4 RID: 36004 RVA: 0x000CE485 File Offset: 0x000CC685
		public virtual GetUMCallDataRecordCommand SetParameters(GetUMCallDataRecordCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000B46 RID: 2886
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170061D1 RID: 25041
			// (set) Token: 0x06008CA5 RID: 36005 RVA: 0x000CE48F File Offset: 0x000CC68F
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170061D2 RID: 25042
			// (set) Token: 0x06008CA6 RID: 36006 RVA: 0x000CE4AD File Offset: 0x000CC6AD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170061D3 RID: 25043
			// (set) Token: 0x06008CA7 RID: 36007 RVA: 0x000CE4CB File Offset: 0x000CC6CB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170061D4 RID: 25044
			// (set) Token: 0x06008CA8 RID: 36008 RVA: 0x000CE4DE File Offset: 0x000CC6DE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170061D5 RID: 25045
			// (set) Token: 0x06008CA9 RID: 36009 RVA: 0x000CE4F6 File Offset: 0x000CC6F6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170061D6 RID: 25046
			// (set) Token: 0x06008CAA RID: 36010 RVA: 0x000CE50E File Offset: 0x000CC70E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170061D7 RID: 25047
			// (set) Token: 0x06008CAB RID: 36011 RVA: 0x000CE526 File Offset: 0x000CC726
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
