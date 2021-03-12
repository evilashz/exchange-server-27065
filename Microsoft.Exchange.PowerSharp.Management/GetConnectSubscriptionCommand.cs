using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E4F RID: 3663
	public class GetConnectSubscriptionCommand : SyntheticCommandWithPipelineInput<ConnectSubscriptionProxy, ConnectSubscriptionProxy>
	{
		// Token: 0x0600D93A RID: 55610 RVA: 0x001345FB File Offset: 0x001327FB
		private GetConnectSubscriptionCommand() : base("Get-ConnectSubscription")
		{
		}

		// Token: 0x0600D93B RID: 55611 RVA: 0x00134608 File Offset: 0x00132808
		public GetConnectSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D93C RID: 55612 RVA: 0x00134617 File Offset: 0x00132817
		public virtual GetConnectSubscriptionCommand SetParameters(GetConnectSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D93D RID: 55613 RVA: 0x00134621 File Offset: 0x00132821
		public virtual GetConnectSubscriptionCommand SetParameters(GetConnectSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E50 RID: 3664
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A855 RID: 43093
			// (set) Token: 0x0600D93E RID: 55614 RVA: 0x0013462B File Offset: 0x0013282B
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A856 RID: 43094
			// (set) Token: 0x0600D93F RID: 55615 RVA: 0x00134649 File Offset: 0x00132849
			public virtual SwitchParameter IncludeReport
			{
				set
				{
					base.PowerSharpParameters["IncludeReport"] = value;
				}
			}

			// Token: 0x1700A857 RID: 43095
			// (set) Token: 0x0600D940 RID: 55616 RVA: 0x00134661 File Offset: 0x00132861
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A858 RID: 43096
			// (set) Token: 0x0600D941 RID: 55617 RVA: 0x0013467F File Offset: 0x0013287F
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A859 RID: 43097
			// (set) Token: 0x0600D942 RID: 55618 RVA: 0x00134697 File Offset: 0x00132897
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A85A RID: 43098
			// (set) Token: 0x0600D943 RID: 55619 RVA: 0x001346AF File Offset: 0x001328AF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A85B RID: 43099
			// (set) Token: 0x0600D944 RID: 55620 RVA: 0x001346C2 File Offset: 0x001328C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A85C RID: 43100
			// (set) Token: 0x0600D945 RID: 55621 RVA: 0x001346DA File Offset: 0x001328DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A85D RID: 43101
			// (set) Token: 0x0600D946 RID: 55622 RVA: 0x001346F2 File Offset: 0x001328F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A85E RID: 43102
			// (set) Token: 0x0600D947 RID: 55623 RVA: 0x0013470A File Offset: 0x0013290A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A85F RID: 43103
			// (set) Token: 0x0600D948 RID: 55624 RVA: 0x00134722 File Offset: 0x00132922
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E51 RID: 3665
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A860 RID: 43104
			// (set) Token: 0x0600D94A RID: 55626 RVA: 0x00134742 File Offset: 0x00132942
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x1700A861 RID: 43105
			// (set) Token: 0x0600D94B RID: 55627 RVA: 0x0013475A File Offset: 0x0013295A
			public virtual AggregationType AggregationType
			{
				set
				{
					base.PowerSharpParameters["AggregationType"] = value;
				}
			}

			// Token: 0x1700A862 RID: 43106
			// (set) Token: 0x0600D94C RID: 55628 RVA: 0x00134772 File Offset: 0x00132972
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A863 RID: 43107
			// (set) Token: 0x0600D94D RID: 55629 RVA: 0x00134785 File Offset: 0x00132985
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A864 RID: 43108
			// (set) Token: 0x0600D94E RID: 55630 RVA: 0x0013479D File Offset: 0x0013299D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A865 RID: 43109
			// (set) Token: 0x0600D94F RID: 55631 RVA: 0x001347B5 File Offset: 0x001329B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A866 RID: 43110
			// (set) Token: 0x0600D950 RID: 55632 RVA: 0x001347CD File Offset: 0x001329CD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A867 RID: 43111
			// (set) Token: 0x0600D951 RID: 55633 RVA: 0x001347E5 File Offset: 0x001329E5
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
