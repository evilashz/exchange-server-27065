using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006E2 RID: 1762
	public class ImportJournalRuleCollectionCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06005BCF RID: 23503 RVA: 0x0008EC4C File Offset: 0x0008CE4C
		private ImportJournalRuleCollectionCommand() : base("Import-JournalRuleCollection")
		{
		}

		// Token: 0x06005BD0 RID: 23504 RVA: 0x0008EC59 File Offset: 0x0008CE59
		public ImportJournalRuleCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x0008EC68 File Offset: 0x0008CE68
		public virtual ImportJournalRuleCollectionCommand SetParameters(ImportJournalRuleCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x0008EC72 File Offset: 0x0008CE72
		public virtual ImportJournalRuleCollectionCommand SetParameters(ImportJournalRuleCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006E3 RID: 1763
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170039C4 RID: 14788
			// (set) Token: 0x06005BD3 RID: 23507 RVA: 0x0008EC7C File Offset: 0x0008CE7C
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x170039C5 RID: 14789
			// (set) Token: 0x06005BD4 RID: 23508 RVA: 0x0008EC94 File Offset: 0x0008CE94
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170039C6 RID: 14790
			// (set) Token: 0x06005BD5 RID: 23509 RVA: 0x0008ECB2 File Offset: 0x0008CEB2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039C7 RID: 14791
			// (set) Token: 0x06005BD6 RID: 23510 RVA: 0x0008ECC5 File Offset: 0x0008CEC5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039C8 RID: 14792
			// (set) Token: 0x06005BD7 RID: 23511 RVA: 0x0008ECDD File Offset: 0x0008CEDD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039C9 RID: 14793
			// (set) Token: 0x06005BD8 RID: 23512 RVA: 0x0008ECF5 File Offset: 0x0008CEF5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039CA RID: 14794
			// (set) Token: 0x06005BD9 RID: 23513 RVA: 0x0008ED0D File Offset: 0x0008CF0D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039CB RID: 14795
			// (set) Token: 0x06005BDA RID: 23514 RVA: 0x0008ED25 File Offset: 0x0008CF25
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006E4 RID: 1764
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170039CC RID: 14796
			// (set) Token: 0x06005BDC RID: 23516 RVA: 0x0008ED45 File Offset: 0x0008CF45
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x170039CD RID: 14797
			// (set) Token: 0x06005BDD RID: 23517 RVA: 0x0008ED63 File Offset: 0x0008CF63
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x170039CE RID: 14798
			// (set) Token: 0x06005BDE RID: 23518 RVA: 0x0008ED7B File Offset: 0x0008CF7B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170039CF RID: 14799
			// (set) Token: 0x06005BDF RID: 23519 RVA: 0x0008ED99 File Offset: 0x0008CF99
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039D0 RID: 14800
			// (set) Token: 0x06005BE0 RID: 23520 RVA: 0x0008EDAC File Offset: 0x0008CFAC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039D1 RID: 14801
			// (set) Token: 0x06005BE1 RID: 23521 RVA: 0x0008EDC4 File Offset: 0x0008CFC4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039D2 RID: 14802
			// (set) Token: 0x06005BE2 RID: 23522 RVA: 0x0008EDDC File Offset: 0x0008CFDC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039D3 RID: 14803
			// (set) Token: 0x06005BE3 RID: 23523 RVA: 0x0008EDF4 File Offset: 0x0008CFF4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039D4 RID: 14804
			// (set) Token: 0x06005BE4 RID: 23524 RVA: 0x0008EE0C File Offset: 0x0008D00C
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
