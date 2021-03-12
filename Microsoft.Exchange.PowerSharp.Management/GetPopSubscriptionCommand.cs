using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pop;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E58 RID: 3672
	public class GetPopSubscriptionCommand : SyntheticCommandWithPipelineInput<PopSubscriptionProxy, PopSubscriptionProxy>
	{
		// Token: 0x0600D985 RID: 55685 RVA: 0x00134C19 File Offset: 0x00132E19
		private GetPopSubscriptionCommand() : base("Get-PopSubscription")
		{
		}

		// Token: 0x0600D986 RID: 55686 RVA: 0x00134C26 File Offset: 0x00132E26
		public GetPopSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D987 RID: 55687 RVA: 0x00134C35 File Offset: 0x00132E35
		public virtual GetPopSubscriptionCommand SetParameters(GetPopSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D988 RID: 55688 RVA: 0x00134C3F File Offset: 0x00132E3F
		public virtual GetPopSubscriptionCommand SetParameters(GetPopSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E59 RID: 3673
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A88E RID: 43150
			// (set) Token: 0x0600D989 RID: 55689 RVA: 0x00134C49 File Offset: 0x00132E49
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A88F RID: 43151
			// (set) Token: 0x0600D98A RID: 55690 RVA: 0x00134C67 File Offset: 0x00132E67
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700A890 RID: 43152
			// (set) Token: 0x0600D98B RID: 55691 RVA: 0x00134C7F File Offset: 0x00132E7F
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A891 RID: 43153
			// (set) Token: 0x0600D98C RID: 55692 RVA: 0x00134C9D File Offset: 0x00132E9D
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A892 RID: 43154
			// (set) Token: 0x0600D98D RID: 55693 RVA: 0x00134CB5 File Offset: 0x00132EB5
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A893 RID: 43155
			// (set) Token: 0x0600D98E RID: 55694 RVA: 0x00134CCD File Offset: 0x00132ECD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A894 RID: 43156
			// (set) Token: 0x0600D98F RID: 55695 RVA: 0x00134CE0 File Offset: 0x00132EE0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A895 RID: 43157
			// (set) Token: 0x0600D990 RID: 55696 RVA: 0x00134CF8 File Offset: 0x00132EF8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A896 RID: 43158
			// (set) Token: 0x0600D991 RID: 55697 RVA: 0x00134D10 File Offset: 0x00132F10
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A897 RID: 43159
			// (set) Token: 0x0600D992 RID: 55698 RVA: 0x00134D28 File Offset: 0x00132F28
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A898 RID: 43160
			// (set) Token: 0x0600D993 RID: 55699 RVA: 0x00134D40 File Offset: 0x00132F40
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E5A RID: 3674
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A899 RID: 43161
			// (set) Token: 0x0600D995 RID: 55701 RVA: 0x00134D60 File Offset: 0x00132F60
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A89A RID: 43162
			// (set) Token: 0x0600D996 RID: 55702 RVA: 0x00134D78 File Offset: 0x00132F78
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A89B RID: 43163
			// (set) Token: 0x0600D997 RID: 55703 RVA: 0x00134D90 File Offset: 0x00132F90
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A89C RID: 43164
			// (set) Token: 0x0600D998 RID: 55704 RVA: 0x00134DA3 File Offset: 0x00132FA3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A89D RID: 43165
			// (set) Token: 0x0600D999 RID: 55705 RVA: 0x00134DBB File Offset: 0x00132FBB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A89E RID: 43166
			// (set) Token: 0x0600D99A RID: 55706 RVA: 0x00134DD3 File Offset: 0x00132FD3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A89F RID: 43167
			// (set) Token: 0x0600D99B RID: 55707 RVA: 0x00134DEB File Offset: 0x00132FEB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A8A0 RID: 43168
			// (set) Token: 0x0600D99C RID: 55708 RVA: 0x00134E03 File Offset: 0x00133003
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
