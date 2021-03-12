using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Imap;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E55 RID: 3669
	public class GetImapSubscriptionCommand : SyntheticCommandWithPipelineInput<IMAPSubscriptionProxy, IMAPSubscriptionProxy>
	{
		// Token: 0x0600D96C RID: 55660 RVA: 0x00134A0F File Offset: 0x00132C0F
		private GetImapSubscriptionCommand() : base("Get-ImapSubscription")
		{
		}

		// Token: 0x0600D96D RID: 55661 RVA: 0x00134A1C File Offset: 0x00132C1C
		public GetImapSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D96E RID: 55662 RVA: 0x00134A2B File Offset: 0x00132C2B
		public virtual GetImapSubscriptionCommand SetParameters(GetImapSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D96F RID: 55663 RVA: 0x00134A35 File Offset: 0x00132C35
		public virtual GetImapSubscriptionCommand SetParameters(GetImapSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E56 RID: 3670
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A87B RID: 43131
			// (set) Token: 0x0600D970 RID: 55664 RVA: 0x00134A3F File Offset: 0x00132C3F
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A87C RID: 43132
			// (set) Token: 0x0600D971 RID: 55665 RVA: 0x00134A5D File Offset: 0x00132C5D
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700A87D RID: 43133
			// (set) Token: 0x0600D972 RID: 55666 RVA: 0x00134A75 File Offset: 0x00132C75
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A87E RID: 43134
			// (set) Token: 0x0600D973 RID: 55667 RVA: 0x00134A93 File Offset: 0x00132C93
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A87F RID: 43135
			// (set) Token: 0x0600D974 RID: 55668 RVA: 0x00134AAB File Offset: 0x00132CAB
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A880 RID: 43136
			// (set) Token: 0x0600D975 RID: 55669 RVA: 0x00134AC3 File Offset: 0x00132CC3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A881 RID: 43137
			// (set) Token: 0x0600D976 RID: 55670 RVA: 0x00134AD6 File Offset: 0x00132CD6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A882 RID: 43138
			// (set) Token: 0x0600D977 RID: 55671 RVA: 0x00134AEE File Offset: 0x00132CEE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A883 RID: 43139
			// (set) Token: 0x0600D978 RID: 55672 RVA: 0x00134B06 File Offset: 0x00132D06
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A884 RID: 43140
			// (set) Token: 0x0600D979 RID: 55673 RVA: 0x00134B1E File Offset: 0x00132D1E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A885 RID: 43141
			// (set) Token: 0x0600D97A RID: 55674 RVA: 0x00134B36 File Offset: 0x00132D36
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E57 RID: 3671
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A886 RID: 43142
			// (set) Token: 0x0600D97C RID: 55676 RVA: 0x00134B56 File Offset: 0x00132D56
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A887 RID: 43143
			// (set) Token: 0x0600D97D RID: 55677 RVA: 0x00134B6E File Offset: 0x00132D6E
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A888 RID: 43144
			// (set) Token: 0x0600D97E RID: 55678 RVA: 0x00134B86 File Offset: 0x00132D86
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A889 RID: 43145
			// (set) Token: 0x0600D97F RID: 55679 RVA: 0x00134B99 File Offset: 0x00132D99
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A88A RID: 43146
			// (set) Token: 0x0600D980 RID: 55680 RVA: 0x00134BB1 File Offset: 0x00132DB1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A88B RID: 43147
			// (set) Token: 0x0600D981 RID: 55681 RVA: 0x00134BC9 File Offset: 0x00132DC9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A88C RID: 43148
			// (set) Token: 0x0600D982 RID: 55682 RVA: 0x00134BE1 File Offset: 0x00132DE1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A88D RID: 43149
			// (set) Token: 0x0600D983 RID: 55683 RVA: 0x00134BF9 File Offset: 0x00132DF9
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
