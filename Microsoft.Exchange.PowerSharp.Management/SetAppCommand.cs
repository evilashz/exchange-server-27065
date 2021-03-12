using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Extension;
using Microsoft.Exchange.Management.Extension;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E4C RID: 3660
	public class SetAppCommand : SyntheticCommandWithPipelineInputNoOutput<App>
	{
		// Token: 0x0600D91B RID: 55579 RVA: 0x00134365 File Offset: 0x00132565
		private SetAppCommand() : base("Set-App")
		{
		}

		// Token: 0x0600D91C RID: 55580 RVA: 0x00134372 File Offset: 0x00132572
		public SetAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D91D RID: 55581 RVA: 0x00134381 File Offset: 0x00132581
		public virtual SetAppCommand SetParameters(SetAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D91E RID: 55582 RVA: 0x0013438B File Offset: 0x0013258B
		public virtual SetAppCommand SetParameters(SetAppCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E4D RID: 3661
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A83C RID: 43068
			// (set) Token: 0x0600D91F RID: 55583 RVA: 0x00134395 File Offset: 0x00132595
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A83D RID: 43069
			// (set) Token: 0x0600D920 RID: 55584 RVA: 0x001343B3 File Offset: 0x001325B3
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A83E RID: 43070
			// (set) Token: 0x0600D921 RID: 55585 RVA: 0x001343CB File Offset: 0x001325CB
			public virtual ClientExtensionProvidedTo ProvidedTo
			{
				set
				{
					base.PowerSharpParameters["ProvidedTo"] = value;
				}
			}

			// Token: 0x1700A83F RID: 43071
			// (set) Token: 0x0600D922 RID: 55586 RVA: 0x001343E3 File Offset: 0x001325E3
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
			{
				set
				{
					base.PowerSharpParameters["UserList"] = value;
				}
			}

			// Token: 0x1700A840 RID: 43072
			// (set) Token: 0x0600D923 RID: 55587 RVA: 0x001343F6 File Offset: 0x001325F6
			public virtual DefaultStateForUser DefaultStateForUser
			{
				set
				{
					base.PowerSharpParameters["DefaultStateForUser"] = value;
				}
			}

			// Token: 0x1700A841 RID: 43073
			// (set) Token: 0x0600D924 RID: 55588 RVA: 0x0013440E File Offset: 0x0013260E
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A842 RID: 43074
			// (set) Token: 0x0600D925 RID: 55589 RVA: 0x00134426 File Offset: 0x00132626
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A843 RID: 43075
			// (set) Token: 0x0600D926 RID: 55590 RVA: 0x00134439 File Offset: 0x00132639
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A844 RID: 43076
			// (set) Token: 0x0600D927 RID: 55591 RVA: 0x00134451 File Offset: 0x00132651
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A845 RID: 43077
			// (set) Token: 0x0600D928 RID: 55592 RVA: 0x00134469 File Offset: 0x00132669
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A846 RID: 43078
			// (set) Token: 0x0600D929 RID: 55593 RVA: 0x00134481 File Offset: 0x00132681
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A847 RID: 43079
			// (set) Token: 0x0600D92A RID: 55594 RVA: 0x00134499 File Offset: 0x00132699
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E4E RID: 3662
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A848 RID: 43080
			// (set) Token: 0x0600D92C RID: 55596 RVA: 0x001344B9 File Offset: 0x001326B9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AppIdParameter(value) : null);
				}
			}

			// Token: 0x1700A849 RID: 43081
			// (set) Token: 0x0600D92D RID: 55597 RVA: 0x001344D7 File Offset: 0x001326D7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A84A RID: 43082
			// (set) Token: 0x0600D92E RID: 55598 RVA: 0x001344F5 File Offset: 0x001326F5
			public virtual SwitchParameter OrganizationApp
			{
				set
				{
					base.PowerSharpParameters["OrganizationApp"] = value;
				}
			}

			// Token: 0x1700A84B RID: 43083
			// (set) Token: 0x0600D92F RID: 55599 RVA: 0x0013450D File Offset: 0x0013270D
			public virtual ClientExtensionProvidedTo ProvidedTo
			{
				set
				{
					base.PowerSharpParameters["ProvidedTo"] = value;
				}
			}

			// Token: 0x1700A84C RID: 43084
			// (set) Token: 0x0600D930 RID: 55600 RVA: 0x00134525 File Offset: 0x00132725
			public virtual MultiValuedProperty<RecipientWithAdUserIdParameter<RecipientIdParameter>> UserList
			{
				set
				{
					base.PowerSharpParameters["UserList"] = value;
				}
			}

			// Token: 0x1700A84D RID: 43085
			// (set) Token: 0x0600D931 RID: 55601 RVA: 0x00134538 File Offset: 0x00132738
			public virtual DefaultStateForUser DefaultStateForUser
			{
				set
				{
					base.PowerSharpParameters["DefaultStateForUser"] = value;
				}
			}

			// Token: 0x1700A84E RID: 43086
			// (set) Token: 0x0600D932 RID: 55602 RVA: 0x00134550 File Offset: 0x00132750
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A84F RID: 43087
			// (set) Token: 0x0600D933 RID: 55603 RVA: 0x00134568 File Offset: 0x00132768
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A850 RID: 43088
			// (set) Token: 0x0600D934 RID: 55604 RVA: 0x0013457B File Offset: 0x0013277B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A851 RID: 43089
			// (set) Token: 0x0600D935 RID: 55605 RVA: 0x00134593 File Offset: 0x00132793
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A852 RID: 43090
			// (set) Token: 0x0600D936 RID: 55606 RVA: 0x001345AB File Offset: 0x001327AB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A853 RID: 43091
			// (set) Token: 0x0600D937 RID: 55607 RVA: 0x001345C3 File Offset: 0x001327C3
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A854 RID: 43092
			// (set) Token: 0x0600D938 RID: 55608 RVA: 0x001345DB File Offset: 0x001327DB
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
