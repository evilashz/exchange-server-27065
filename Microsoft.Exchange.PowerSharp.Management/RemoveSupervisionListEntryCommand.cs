using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E36 RID: 3638
	public class RemoveSupervisionListEntryCommand : SyntheticCommandWithPipelineInputNoOutput<RecipientIdParameter>
	{
		// Token: 0x0600D843 RID: 55363 RVA: 0x0013317F File Offset: 0x0013137F
		private RemoveSupervisionListEntryCommand() : base("Remove-SupervisionListEntry")
		{
		}

		// Token: 0x0600D844 RID: 55364 RVA: 0x0013318C File Offset: 0x0013138C
		public RemoveSupervisionListEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D845 RID: 55365 RVA: 0x0013319B File Offset: 0x0013139B
		public virtual RemoveSupervisionListEntryCommand SetParameters(RemoveSupervisionListEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D846 RID: 55366 RVA: 0x001331A5 File Offset: 0x001313A5
		public virtual RemoveSupervisionListEntryCommand SetParameters(RemoveSupervisionListEntryCommand.RemoveOneParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D847 RID: 55367 RVA: 0x001331AF File Offset: 0x001313AF
		public virtual RemoveSupervisionListEntryCommand SetParameters(RemoveSupervisionListEntryCommand.RemoveAllParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E37 RID: 3639
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A790 RID: 42896
			// (set) Token: 0x0600D848 RID: 55368 RVA: 0x001331B9 File Offset: 0x001313B9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A791 RID: 42897
			// (set) Token: 0x0600D849 RID: 55369 RVA: 0x001331D7 File Offset: 0x001313D7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A792 RID: 42898
			// (set) Token: 0x0600D84A RID: 55370 RVA: 0x001331EA File Offset: 0x001313EA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A793 RID: 42899
			// (set) Token: 0x0600D84B RID: 55371 RVA: 0x00133202 File Offset: 0x00131402
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A794 RID: 42900
			// (set) Token: 0x0600D84C RID: 55372 RVA: 0x0013321A File Offset: 0x0013141A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A795 RID: 42901
			// (set) Token: 0x0600D84D RID: 55373 RVA: 0x00133232 File Offset: 0x00131432
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A796 RID: 42902
			// (set) Token: 0x0600D84E RID: 55374 RVA: 0x0013324A File Offset: 0x0013144A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A797 RID: 42903
			// (set) Token: 0x0600D84F RID: 55375 RVA: 0x00133262 File Offset: 0x00131462
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E38 RID: 3640
		public class RemoveOneParameters : ParametersBase
		{
			// Token: 0x1700A798 RID: 42904
			// (set) Token: 0x0600D851 RID: 55377 RVA: 0x00133282 File Offset: 0x00131482
			public virtual string Tag
			{
				set
				{
					base.PowerSharpParameters["Tag"] = value;
				}
			}

			// Token: 0x1700A799 RID: 42905
			// (set) Token: 0x0600D852 RID: 55378 RVA: 0x00133295 File Offset: 0x00131495
			public virtual string Entry
			{
				set
				{
					base.PowerSharpParameters["Entry"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A79A RID: 42906
			// (set) Token: 0x0600D853 RID: 55379 RVA: 0x001332B3 File Offset: 0x001314B3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A79B RID: 42907
			// (set) Token: 0x0600D854 RID: 55380 RVA: 0x001332D1 File Offset: 0x001314D1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A79C RID: 42908
			// (set) Token: 0x0600D855 RID: 55381 RVA: 0x001332E4 File Offset: 0x001314E4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A79D RID: 42909
			// (set) Token: 0x0600D856 RID: 55382 RVA: 0x001332FC File Offset: 0x001314FC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A79E RID: 42910
			// (set) Token: 0x0600D857 RID: 55383 RVA: 0x00133314 File Offset: 0x00131514
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A79F RID: 42911
			// (set) Token: 0x0600D858 RID: 55384 RVA: 0x0013332C File Offset: 0x0013152C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A7A0 RID: 42912
			// (set) Token: 0x0600D859 RID: 55385 RVA: 0x00133344 File Offset: 0x00131544
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A7A1 RID: 42913
			// (set) Token: 0x0600D85A RID: 55386 RVA: 0x0013335C File Offset: 0x0013155C
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000E39 RID: 3641
		public class RemoveAllParameters : ParametersBase
		{
			// Token: 0x1700A7A2 RID: 42914
			// (set) Token: 0x0600D85C RID: 55388 RVA: 0x0013337C File Offset: 0x0013157C
			public virtual SwitchParameter RemoveAll
			{
				set
				{
					base.PowerSharpParameters["RemoveAll"] = value;
				}
			}

			// Token: 0x1700A7A3 RID: 42915
			// (set) Token: 0x0600D85D RID: 55389 RVA: 0x00133394 File Offset: 0x00131594
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A7A4 RID: 42916
			// (set) Token: 0x0600D85E RID: 55390 RVA: 0x001333B2 File Offset: 0x001315B2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A7A5 RID: 42917
			// (set) Token: 0x0600D85F RID: 55391 RVA: 0x001333C5 File Offset: 0x001315C5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A7A6 RID: 42918
			// (set) Token: 0x0600D860 RID: 55392 RVA: 0x001333DD File Offset: 0x001315DD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A7A7 RID: 42919
			// (set) Token: 0x0600D861 RID: 55393 RVA: 0x001333F5 File Offset: 0x001315F5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A7A8 RID: 42920
			// (set) Token: 0x0600D862 RID: 55394 RVA: 0x0013340D File Offset: 0x0013160D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A7A9 RID: 42921
			// (set) Token: 0x0600D863 RID: 55395 RVA: 0x00133425 File Offset: 0x00131625
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700A7AA RID: 42922
			// (set) Token: 0x0600D864 RID: 55396 RVA: 0x0013343D File Offset: 0x0013163D
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
