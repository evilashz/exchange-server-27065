using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000088 RID: 136
	public class InvokeContactLinkingCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxIdParameter>
	{
		// Token: 0x060018B8 RID: 6328 RVA: 0x00037B26 File Offset: 0x00035D26
		private InvokeContactLinkingCommand() : base("Invoke-ContactLinking")
		{
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00037B33 File Offset: 0x00035D33
		public InvokeContactLinkingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00037B42 File Offset: 0x00035D42
		public virtual InvokeContactLinkingCommand SetParameters(InvokeContactLinkingCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000089 RID: 137
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000361 RID: 865
			// (set) Token: 0x060018BB RID: 6331 RVA: 0x00037B4C File Offset: 0x00035D4C
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000362 RID: 866
			// (set) Token: 0x060018BC RID: 6332 RVA: 0x00037B6A File Offset: 0x00035D6A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000363 RID: 867
			// (set) Token: 0x060018BD RID: 6333 RVA: 0x00037B7D File Offset: 0x00035D7D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000364 RID: 868
			// (set) Token: 0x060018BE RID: 6334 RVA: 0x00037B95 File Offset: 0x00035D95
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000365 RID: 869
			// (set) Token: 0x060018BF RID: 6335 RVA: 0x00037BAD File Offset: 0x00035DAD
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000366 RID: 870
			// (set) Token: 0x060018C0 RID: 6336 RVA: 0x00037BC5 File Offset: 0x00035DC5
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
