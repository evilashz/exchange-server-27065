using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000090 RID: 144
	public class ResetContactLinkingCommand : SyntheticCommandWithPipelineInputNoOutput<MailboxIdParameter>
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x00037EF1 File Offset: 0x000360F1
		private ResetContactLinkingCommand() : base("Reset-ContactLinking")
		{
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00037EFE File Offset: 0x000360FE
		public ResetContactLinkingCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00037F0D File Offset: 0x0003610D
		public virtual ResetContactLinkingCommand SetParameters(ResetContactLinkingCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000091 RID: 145
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000382 RID: 898
			// (set) Token: 0x060018EC RID: 6380 RVA: 0x00037F17 File Offset: 0x00036117
			public virtual SwitchParameter IncludeUserApproved
			{
				set
				{
					base.PowerSharpParameters["IncludeUserApproved"] = value;
				}
			}

			// Token: 0x17000383 RID: 899
			// (set) Token: 0x060018ED RID: 6381 RVA: 0x00037F2F File Offset: 0x0003612F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17000384 RID: 900
			// (set) Token: 0x060018EE RID: 6382 RVA: 0x00037F4D File Offset: 0x0003614D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000385 RID: 901
			// (set) Token: 0x060018EF RID: 6383 RVA: 0x00037F60 File Offset: 0x00036160
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000386 RID: 902
			// (set) Token: 0x060018F0 RID: 6384 RVA: 0x00037F78 File Offset: 0x00036178
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000387 RID: 903
			// (set) Token: 0x060018F1 RID: 6385 RVA: 0x00037F90 File Offset: 0x00036190
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000388 RID: 904
			// (set) Token: 0x060018F2 RID: 6386 RVA: 0x00037FA8 File Offset: 0x000361A8
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
