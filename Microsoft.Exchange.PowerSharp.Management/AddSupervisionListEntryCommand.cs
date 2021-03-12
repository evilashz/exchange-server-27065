using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E30 RID: 3632
	public class AddSupervisionListEntryCommand : SyntheticCommandWithPipelineInputNoOutput<RecipientIdParameter>
	{
		// Token: 0x0600D813 RID: 55315 RVA: 0x00132DB9 File Offset: 0x00130FB9
		private AddSupervisionListEntryCommand() : base("Add-SupervisionListEntry")
		{
		}

		// Token: 0x0600D814 RID: 55316 RVA: 0x00132DC6 File Offset: 0x00130FC6
		public AddSupervisionListEntryCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D815 RID: 55317 RVA: 0x00132DD5 File Offset: 0x00130FD5
		public virtual AddSupervisionListEntryCommand SetParameters(AddSupervisionListEntryCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D816 RID: 55318 RVA: 0x00132DDF File Offset: 0x00130FDF
		public virtual AddSupervisionListEntryCommand SetParameters(AddSupervisionListEntryCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E31 RID: 3633
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A76C RID: 42860
			// (set) Token: 0x0600D817 RID: 55319 RVA: 0x00132DE9 File Offset: 0x00130FE9
			public virtual string Entry
			{
				set
				{
					base.PowerSharpParameters["Entry"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A76D RID: 42861
			// (set) Token: 0x0600D818 RID: 55320 RVA: 0x00132E07 File Offset: 0x00131007
			public virtual string Tag
			{
				set
				{
					base.PowerSharpParameters["Tag"] = value;
				}
			}

			// Token: 0x1700A76E RID: 42862
			// (set) Token: 0x0600D819 RID: 55321 RVA: 0x00132E1A File Offset: 0x0013101A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A76F RID: 42863
			// (set) Token: 0x0600D81A RID: 55322 RVA: 0x00132E2D File Offset: 0x0013102D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A770 RID: 42864
			// (set) Token: 0x0600D81B RID: 55323 RVA: 0x00132E45 File Offset: 0x00131045
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A771 RID: 42865
			// (set) Token: 0x0600D81C RID: 55324 RVA: 0x00132E5D File Offset: 0x0013105D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A772 RID: 42866
			// (set) Token: 0x0600D81D RID: 55325 RVA: 0x00132E75 File Offset: 0x00131075
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A773 RID: 42867
			// (set) Token: 0x0600D81E RID: 55326 RVA: 0x00132E8D File Offset: 0x0013108D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E32 RID: 3634
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A774 RID: 42868
			// (set) Token: 0x0600D820 RID: 55328 RVA: 0x00132EAD File Offset: 0x001310AD
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A775 RID: 42869
			// (set) Token: 0x0600D821 RID: 55329 RVA: 0x00132ECB File Offset: 0x001310CB
			public virtual string Entry
			{
				set
				{
					base.PowerSharpParameters["Entry"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A776 RID: 42870
			// (set) Token: 0x0600D822 RID: 55330 RVA: 0x00132EE9 File Offset: 0x001310E9
			public virtual string Tag
			{
				set
				{
					base.PowerSharpParameters["Tag"] = value;
				}
			}

			// Token: 0x1700A777 RID: 42871
			// (set) Token: 0x0600D823 RID: 55331 RVA: 0x00132EFC File Offset: 0x001310FC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A778 RID: 42872
			// (set) Token: 0x0600D824 RID: 55332 RVA: 0x00132F0F File Offset: 0x0013110F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A779 RID: 42873
			// (set) Token: 0x0600D825 RID: 55333 RVA: 0x00132F27 File Offset: 0x00131127
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A77A RID: 42874
			// (set) Token: 0x0600D826 RID: 55334 RVA: 0x00132F3F File Offset: 0x0013113F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A77B RID: 42875
			// (set) Token: 0x0600D827 RID: 55335 RVA: 0x00132F57 File Offset: 0x00131157
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A77C RID: 42876
			// (set) Token: 0x0600D828 RID: 55336 RVA: 0x00132F6F File Offset: 0x0013116F
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
