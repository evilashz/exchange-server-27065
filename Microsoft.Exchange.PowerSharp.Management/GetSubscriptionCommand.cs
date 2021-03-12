using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E62 RID: 3682
	public class GetSubscriptionCommand : SyntheticCommandWithPipelineInput<PimSubscriptionProxy, PimSubscriptionProxy>
	{
		// Token: 0x0600D9CE RID: 55758 RVA: 0x001351DF File Offset: 0x001333DF
		private GetSubscriptionCommand() : base("Get-Subscription")
		{
		}

		// Token: 0x0600D9CF RID: 55759 RVA: 0x001351EC File Offset: 0x001333EC
		public GetSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D9D0 RID: 55760 RVA: 0x001351FB File Offset: 0x001333FB
		public virtual GetSubscriptionCommand SetParameters(GetSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D9D1 RID: 55761 RVA: 0x00135205 File Offset: 0x00133405
		public virtual GetSubscriptionCommand SetParameters(GetSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E63 RID: 3683
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A8C3 RID: 43203
			// (set) Token: 0x0600D9D2 RID: 55762 RVA: 0x0013520F File Offset: 0x0013340F
			public virtual AggregationSubscriptionType SubscriptionType
			{
				set
				{
					base.PowerSharpParameters["SubscriptionType"] = value;
				}
			}

			// Token: 0x1700A8C4 RID: 43204
			// (set) Token: 0x0600D9D3 RID: 55763 RVA: 0x00135227 File Offset: 0x00133427
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A8C5 RID: 43205
			// (set) Token: 0x0600D9D4 RID: 55764 RVA: 0x0013523F File Offset: 0x0013343F
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A8C6 RID: 43206
			// (set) Token: 0x0600D9D5 RID: 55765 RVA: 0x00135257 File Offset: 0x00133457
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8C7 RID: 43207
			// (set) Token: 0x0600D9D6 RID: 55766 RVA: 0x0013526A File Offset: 0x0013346A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8C8 RID: 43208
			// (set) Token: 0x0600D9D7 RID: 55767 RVA: 0x00135282 File Offset: 0x00133482
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8C9 RID: 43209
			// (set) Token: 0x0600D9D8 RID: 55768 RVA: 0x0013529A File Offset: 0x0013349A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8CA RID: 43210
			// (set) Token: 0x0600D9D9 RID: 55769 RVA: 0x001352B2 File Offset: 0x001334B2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8CB RID: 43211
			// (set) Token: 0x0600D9DA RID: 55770 RVA: 0x001352CA File Offset: 0x001334CA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E64 RID: 3684
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A8CC RID: 43212
			// (set) Token: 0x0600D9DC RID: 55772 RVA: 0x001352EA File Offset: 0x001334EA
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8CD RID: 43213
			// (set) Token: 0x0600D9DD RID: 55773 RVA: 0x00135308 File Offset: 0x00133508
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700A8CE RID: 43214
			// (set) Token: 0x0600D9DE RID: 55774 RVA: 0x00135320 File Offset: 0x00133520
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A8CF RID: 43215
			// (set) Token: 0x0600D9DF RID: 55775 RVA: 0x0013533E File Offset: 0x0013353E
			public virtual AggregationSubscriptionType SubscriptionType
			{
				set
				{
					base.PowerSharpParameters["SubscriptionType"] = value;
				}
			}

			// Token: 0x1700A8D0 RID: 43216
			// (set) Token: 0x0600D9E0 RID: 55776 RVA: 0x00135356 File Offset: 0x00133556
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A8D1 RID: 43217
			// (set) Token: 0x0600D9E1 RID: 55777 RVA: 0x0013536E File Offset: 0x0013356E
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A8D2 RID: 43218
			// (set) Token: 0x0600D9E2 RID: 55778 RVA: 0x00135386 File Offset: 0x00133586
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A8D3 RID: 43219
			// (set) Token: 0x0600D9E3 RID: 55779 RVA: 0x00135399 File Offset: 0x00133599
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A8D4 RID: 43220
			// (set) Token: 0x0600D9E4 RID: 55780 RVA: 0x001353B1 File Offset: 0x001335B1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A8D5 RID: 43221
			// (set) Token: 0x0600D9E5 RID: 55781 RVA: 0x001353C9 File Offset: 0x001335C9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A8D6 RID: 43222
			// (set) Token: 0x0600D9E6 RID: 55782 RVA: 0x001353E1 File Offset: 0x001335E1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8D7 RID: 43223
			// (set) Token: 0x0600D9E7 RID: 55783 RVA: 0x001353F9 File Offset: 0x001335F9
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
