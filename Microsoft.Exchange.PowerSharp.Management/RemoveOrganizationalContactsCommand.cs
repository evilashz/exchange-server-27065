using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200008A RID: 138
	public class RemoveOrganizationalContactsCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxIdParameter>
	{
		// Token: 0x060018C2 RID: 6338 RVA: 0x00037BE5 File Offset: 0x00035DE5
		private RemoveOrganizationalContactsCommand() : base("Remove-OrganizationalContacts")
		{
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x00037BF2 File Offset: 0x00035DF2
		public RemoveOrganizationalContactsCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x00037C01 File Offset: 0x00035E01
		public virtual RemoveOrganizationalContactsCommand SetParameters(RemoveOrganizationalContactsCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200008B RID: 139
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000367 RID: 871
			// (set) Token: 0x060018C5 RID: 6341 RVA: 0x00037C0B File Offset: 0x00035E0B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000368 RID: 872
			// (set) Token: 0x060018C6 RID: 6342 RVA: 0x00037C29 File Offset: 0x00035E29
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000369 RID: 873
			// (set) Token: 0x060018C7 RID: 6343 RVA: 0x00037C3C File Offset: 0x00035E3C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700036A RID: 874
			// (set) Token: 0x060018C8 RID: 6344 RVA: 0x00037C54 File Offset: 0x00035E54
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700036B RID: 875
			// (set) Token: 0x060018C9 RID: 6345 RVA: 0x00037C6C File Offset: 0x00035E6C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700036C RID: 876
			// (set) Token: 0x060018CA RID: 6346 RVA: 0x00037C84 File Offset: 0x00035E84
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
