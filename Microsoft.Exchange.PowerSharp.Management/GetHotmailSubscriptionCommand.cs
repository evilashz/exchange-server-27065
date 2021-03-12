using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E52 RID: 3666
	public class GetHotmailSubscriptionCommand : SyntheticCommandWithPipelineInput<HotmailSubscriptionProxy, HotmailSubscriptionProxy>
	{
		// Token: 0x0600D953 RID: 55635 RVA: 0x00134805 File Offset: 0x00132A05
		private GetHotmailSubscriptionCommand() : base("Get-HotmailSubscription")
		{
		}

		// Token: 0x0600D954 RID: 55636 RVA: 0x00134812 File Offset: 0x00132A12
		public GetHotmailSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D955 RID: 55637 RVA: 0x00134821 File Offset: 0x00132A21
		public virtual GetHotmailSubscriptionCommand SetParameters(GetHotmailSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D956 RID: 55638 RVA: 0x0013482B File Offset: 0x00132A2B
		public virtual GetHotmailSubscriptionCommand SetParameters(GetHotmailSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E53 RID: 3667
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A868 RID: 43112
			// (set) Token: 0x0600D957 RID: 55639 RVA: 0x00134835 File Offset: 0x00132A35
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A869 RID: 43113
			// (set) Token: 0x0600D958 RID: 55640 RVA: 0x00134853 File Offset: 0x00132A53
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700A86A RID: 43114
			// (set) Token: 0x0600D959 RID: 55641 RVA: 0x0013486B File Offset: 0x00132A6B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A86B RID: 43115
			// (set) Token: 0x0600D95A RID: 55642 RVA: 0x00134889 File Offset: 0x00132A89
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A86C RID: 43116
			// (set) Token: 0x0600D95B RID: 55643 RVA: 0x001348A1 File Offset: 0x00132AA1
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A86D RID: 43117
			// (set) Token: 0x0600D95C RID: 55644 RVA: 0x001348B9 File Offset: 0x00132AB9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A86E RID: 43118
			// (set) Token: 0x0600D95D RID: 55645 RVA: 0x001348CC File Offset: 0x00132ACC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A86F RID: 43119
			// (set) Token: 0x0600D95E RID: 55646 RVA: 0x001348E4 File Offset: 0x00132AE4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A870 RID: 43120
			// (set) Token: 0x0600D95F RID: 55647 RVA: 0x001348FC File Offset: 0x00132AFC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A871 RID: 43121
			// (set) Token: 0x0600D960 RID: 55648 RVA: 0x00134914 File Offset: 0x00132B14
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A872 RID: 43122
			// (set) Token: 0x0600D961 RID: 55649 RVA: 0x0013492C File Offset: 0x00132B2C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E54 RID: 3668
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A873 RID: 43123
			// (set) Token: 0x0600D963 RID: 55651 RVA: 0x0013494C File Offset: 0x00132B4C
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A874 RID: 43124
			// (set) Token: 0x0600D964 RID: 55652 RVA: 0x00134964 File Offset: 0x00132B64
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A875 RID: 43125
			// (set) Token: 0x0600D965 RID: 55653 RVA: 0x0013497C File Offset: 0x00132B7C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A876 RID: 43126
			// (set) Token: 0x0600D966 RID: 55654 RVA: 0x0013498F File Offset: 0x00132B8F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A877 RID: 43127
			// (set) Token: 0x0600D967 RID: 55655 RVA: 0x001349A7 File Offset: 0x00132BA7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A878 RID: 43128
			// (set) Token: 0x0600D968 RID: 55656 RVA: 0x001349BF File Offset: 0x00132BBF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A879 RID: 43129
			// (set) Token: 0x0600D969 RID: 55657 RVA: 0x001349D7 File Offset: 0x00132BD7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A87A RID: 43130
			// (set) Token: 0x0600D96A RID: 55658 RVA: 0x001349EF File Offset: 0x00132BEF
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
