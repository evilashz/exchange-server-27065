using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020006DF RID: 1759
	public class ExportJournalRuleCollectionCommand : SyntheticCommand<object>
	{
		// Token: 0x06005BBA RID: 23482 RVA: 0x0008EA9C File Offset: 0x0008CC9C
		private ExportJournalRuleCollectionCommand() : base("Export-JournalRuleCollection")
		{
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x0008EAA9 File Offset: 0x0008CCA9
		public ExportJournalRuleCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x0008EAB8 File Offset: 0x0008CCB8
		public virtual ExportJournalRuleCollectionCommand SetParameters(ExportJournalRuleCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x0008EAC2 File Offset: 0x0008CCC2
		public virtual ExportJournalRuleCollectionCommand SetParameters(ExportJournalRuleCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020006E0 RID: 1760
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170039B5 RID: 14773
			// (set) Token: 0x06005BBE RID: 23486 RVA: 0x0008EACC File Offset: 0x0008CCCC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170039B6 RID: 14774
			// (set) Token: 0x06005BBF RID: 23487 RVA: 0x0008EAEA File Offset: 0x0008CCEA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039B7 RID: 14775
			// (set) Token: 0x06005BC0 RID: 23488 RVA: 0x0008EAFD File Offset: 0x0008CCFD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039B8 RID: 14776
			// (set) Token: 0x06005BC1 RID: 23489 RVA: 0x0008EB15 File Offset: 0x0008CD15
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039B9 RID: 14777
			// (set) Token: 0x06005BC2 RID: 23490 RVA: 0x0008EB2D File Offset: 0x0008CD2D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039BA RID: 14778
			// (set) Token: 0x06005BC3 RID: 23491 RVA: 0x0008EB45 File Offset: 0x0008CD45
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039BB RID: 14779
			// (set) Token: 0x06005BC4 RID: 23492 RVA: 0x0008EB5D File Offset: 0x0008CD5D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020006E1 RID: 1761
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170039BC RID: 14780
			// (set) Token: 0x06005BC6 RID: 23494 RVA: 0x0008EB7D File Offset: 0x0008CD7D
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RuleIdParameter(value) : null);
				}
			}

			// Token: 0x170039BD RID: 14781
			// (set) Token: 0x06005BC7 RID: 23495 RVA: 0x0008EB9B File Offset: 0x0008CD9B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170039BE RID: 14782
			// (set) Token: 0x06005BC8 RID: 23496 RVA: 0x0008EBB9 File Offset: 0x0008CDB9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170039BF RID: 14783
			// (set) Token: 0x06005BC9 RID: 23497 RVA: 0x0008EBCC File Offset: 0x0008CDCC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170039C0 RID: 14784
			// (set) Token: 0x06005BCA RID: 23498 RVA: 0x0008EBE4 File Offset: 0x0008CDE4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170039C1 RID: 14785
			// (set) Token: 0x06005BCB RID: 23499 RVA: 0x0008EBFC File Offset: 0x0008CDFC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170039C2 RID: 14786
			// (set) Token: 0x06005BCC RID: 23500 RVA: 0x0008EC14 File Offset: 0x0008CE14
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170039C3 RID: 14787
			// (set) Token: 0x06005BCD RID: 23501 RVA: 0x0008EC2C File Offset: 0x0008CE2C
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
