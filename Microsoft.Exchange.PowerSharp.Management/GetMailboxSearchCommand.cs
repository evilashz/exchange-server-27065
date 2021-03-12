using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000405 RID: 1029
	public class GetMailboxSearchCommand : SyntheticCommandWithPipelineInput<MailboxDiscoverySearch, MailboxDiscoverySearch>
	{
		// Token: 0x06003CD6 RID: 15574 RVA: 0x00066BE1 File Offset: 0x00064DE1
		private GetMailboxSearchCommand() : base("Get-MailboxSearch")
		{
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x00066BEE File Offset: 0x00064DEE
		public GetMailboxSearchCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x00066BFD File Offset: 0x00064DFD
		public virtual GetMailboxSearchCommand SetParameters(GetMailboxSearchCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x00066C07 File Offset: 0x00064E07
		public virtual GetMailboxSearchCommand SetParameters(GetMailboxSearchCommand.InPlaceHoldIdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x00066C11 File Offset: 0x00064E11
		public virtual GetMailboxSearchCommand SetParameters(GetMailboxSearchCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000406 RID: 1030
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002085 RID: 8325
			// (set) Token: 0x06003CDB RID: 15579 RVA: 0x00066C1B File Offset: 0x00064E1B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002086 RID: 8326
			// (set) Token: 0x06003CDC RID: 15580 RVA: 0x00066C39 File Offset: 0x00064E39
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002087 RID: 8327
			// (set) Token: 0x06003CDD RID: 15581 RVA: 0x00066C51 File Offset: 0x00064E51
			public virtual SwitchParameter ShowDeletionInProgressSearches
			{
				set
				{
					base.PowerSharpParameters["ShowDeletionInProgressSearches"] = value;
				}
			}

			// Token: 0x17002088 RID: 8328
			// (set) Token: 0x06003CDE RID: 15582 RVA: 0x00066C69 File Offset: 0x00064E69
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002089 RID: 8329
			// (set) Token: 0x06003CDF RID: 15583 RVA: 0x00066C7C File Offset: 0x00064E7C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700208A RID: 8330
			// (set) Token: 0x06003CE0 RID: 15584 RVA: 0x00066C94 File Offset: 0x00064E94
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700208B RID: 8331
			// (set) Token: 0x06003CE1 RID: 15585 RVA: 0x00066CAC File Offset: 0x00064EAC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700208C RID: 8332
			// (set) Token: 0x06003CE2 RID: 15586 RVA: 0x00066CC4 File Offset: 0x00064EC4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000407 RID: 1031
		public class InPlaceHoldIdentityParameters : ParametersBase
		{
			// Token: 0x1700208D RID: 8333
			// (set) Token: 0x06003CE4 RID: 15588 RVA: 0x00066CE4 File Offset: 0x00064EE4
			public virtual string InPlaceHoldIdentity
			{
				set
				{
					base.PowerSharpParameters["InPlaceHoldIdentity"] = value;
				}
			}

			// Token: 0x1700208E RID: 8334
			// (set) Token: 0x06003CE5 RID: 15589 RVA: 0x00066CF7 File Offset: 0x00064EF7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700208F RID: 8335
			// (set) Token: 0x06003CE6 RID: 15590 RVA: 0x00066D15 File Offset: 0x00064F15
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002090 RID: 8336
			// (set) Token: 0x06003CE7 RID: 15591 RVA: 0x00066D2D File Offset: 0x00064F2D
			public virtual SwitchParameter ShowDeletionInProgressSearches
			{
				set
				{
					base.PowerSharpParameters["ShowDeletionInProgressSearches"] = value;
				}
			}

			// Token: 0x17002091 RID: 8337
			// (set) Token: 0x06003CE8 RID: 15592 RVA: 0x00066D45 File Offset: 0x00064F45
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002092 RID: 8338
			// (set) Token: 0x06003CE9 RID: 15593 RVA: 0x00066D58 File Offset: 0x00064F58
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002093 RID: 8339
			// (set) Token: 0x06003CEA RID: 15594 RVA: 0x00066D70 File Offset: 0x00064F70
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002094 RID: 8340
			// (set) Token: 0x06003CEB RID: 15595 RVA: 0x00066D88 File Offset: 0x00064F88
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002095 RID: 8341
			// (set) Token: 0x06003CEC RID: 15596 RVA: 0x00066DA0 File Offset: 0x00064FA0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000408 RID: 1032
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002096 RID: 8342
			// (set) Token: 0x06003CEE RID: 15598 RVA: 0x00066DC0 File Offset: 0x00064FC0
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new EwsStoreObjectIdParameter(value) : null);
				}
			}

			// Token: 0x17002097 RID: 8343
			// (set) Token: 0x06003CEF RID: 15599 RVA: 0x00066DDE File Offset: 0x00064FDE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002098 RID: 8344
			// (set) Token: 0x06003CF0 RID: 15600 RVA: 0x00066DFC File Offset: 0x00064FFC
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17002099 RID: 8345
			// (set) Token: 0x06003CF1 RID: 15601 RVA: 0x00066E14 File Offset: 0x00065014
			public virtual SwitchParameter ShowDeletionInProgressSearches
			{
				set
				{
					base.PowerSharpParameters["ShowDeletionInProgressSearches"] = value;
				}
			}

			// Token: 0x1700209A RID: 8346
			// (set) Token: 0x06003CF2 RID: 15602 RVA: 0x00066E2C File Offset: 0x0006502C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700209B RID: 8347
			// (set) Token: 0x06003CF3 RID: 15603 RVA: 0x00066E3F File Offset: 0x0006503F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700209C RID: 8348
			// (set) Token: 0x06003CF4 RID: 15604 RVA: 0x00066E57 File Offset: 0x00065057
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700209D RID: 8349
			// (set) Token: 0x06003CF5 RID: 15605 RVA: 0x00066E6F File Offset: 0x0006506F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700209E RID: 8350
			// (set) Token: 0x06003CF6 RID: 15606 RVA: 0x00066E87 File Offset: 0x00065087
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
