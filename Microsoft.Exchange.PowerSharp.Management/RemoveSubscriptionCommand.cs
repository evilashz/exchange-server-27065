using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E7C RID: 3708
	public class RemoveSubscriptionCommand : SyntheticCommandWithPipelineInput<PimSubscriptionProxy, PimSubscriptionProxy>
	{
		// Token: 0x0600DAAE RID: 55982 RVA: 0x001363B9 File Offset: 0x001345B9
		private RemoveSubscriptionCommand() : base("Remove-Subscription")
		{
		}

		// Token: 0x0600DAAF RID: 55983 RVA: 0x001363C6 File Offset: 0x001345C6
		public RemoveSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DAB0 RID: 55984 RVA: 0x001363D5 File Offset: 0x001345D5
		public virtual RemoveSubscriptionCommand SetParameters(RemoveSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DAB1 RID: 55985 RVA: 0x001363DF File Offset: 0x001345DF
		public virtual RemoveSubscriptionCommand SetParameters(RemoveSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E7D RID: 3709
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A96F RID: 43375
			// (set) Token: 0x0600DAB2 RID: 55986 RVA: 0x001363E9 File Offset: 0x001345E9
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A970 RID: 43376
			// (set) Token: 0x0600DAB3 RID: 55987 RVA: 0x00136407 File Offset: 0x00134607
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A971 RID: 43377
			// (set) Token: 0x0600DAB4 RID: 55988 RVA: 0x00136425 File Offset: 0x00134625
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A972 RID: 43378
			// (set) Token: 0x0600DAB5 RID: 55989 RVA: 0x00136438 File Offset: 0x00134638
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A973 RID: 43379
			// (set) Token: 0x0600DAB6 RID: 55990 RVA: 0x00136450 File Offset: 0x00134650
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A974 RID: 43380
			// (set) Token: 0x0600DAB7 RID: 55991 RVA: 0x00136468 File Offset: 0x00134668
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A975 RID: 43381
			// (set) Token: 0x0600DAB8 RID: 55992 RVA: 0x00136480 File Offset: 0x00134680
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A976 RID: 43382
			// (set) Token: 0x0600DAB9 RID: 55993 RVA: 0x00136498 File Offset: 0x00134698
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A977 RID: 43383
			// (set) Token: 0x0600DABA RID: 55994 RVA: 0x001364B0 File Offset: 0x001346B0
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E7E RID: 3710
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A978 RID: 43384
			// (set) Token: 0x0600DABC RID: 55996 RVA: 0x001364D0 File Offset: 0x001346D0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A979 RID: 43385
			// (set) Token: 0x0600DABD RID: 55997 RVA: 0x001364E3 File Offset: 0x001346E3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A97A RID: 43386
			// (set) Token: 0x0600DABE RID: 55998 RVA: 0x001364FB File Offset: 0x001346FB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A97B RID: 43387
			// (set) Token: 0x0600DABF RID: 55999 RVA: 0x00136513 File Offset: 0x00134713
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A97C RID: 43388
			// (set) Token: 0x0600DAC0 RID: 56000 RVA: 0x0013652B File Offset: 0x0013472B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A97D RID: 43389
			// (set) Token: 0x0600DAC1 RID: 56001 RVA: 0x00136543 File Offset: 0x00134743
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A97E RID: 43390
			// (set) Token: 0x0600DAC2 RID: 56002 RVA: 0x0013655B File Offset: 0x0013475B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
