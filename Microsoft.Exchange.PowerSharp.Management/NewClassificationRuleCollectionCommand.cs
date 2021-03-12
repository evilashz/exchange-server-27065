using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000515 RID: 1301
	public class NewClassificationRuleCollectionCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06004650 RID: 18000 RVA: 0x00072BEB File Offset: 0x00070DEB
		private NewClassificationRuleCollectionCommand() : base("New-ClassificationRuleCollection")
		{
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x00072BF8 File Offset: 0x00070DF8
		public NewClassificationRuleCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x00072C07 File Offset: 0x00070E07
		public virtual NewClassificationRuleCollectionCommand SetParameters(NewClassificationRuleCollectionCommand.ArbitraryCollectionParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x00072C11 File Offset: 0x00070E11
		public virtual NewClassificationRuleCollectionCommand SetParameters(NewClassificationRuleCollectionCommand.OutOfBoxInstallParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x00072C1B File Offset: 0x00070E1B
		public virtual NewClassificationRuleCollectionCommand SetParameters(NewClassificationRuleCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000516 RID: 1302
		public class ArbitraryCollectionParameters : ParametersBase
		{
			// Token: 0x170027DF RID: 10207
			// (set) Token: 0x06004655 RID: 18005 RVA: 0x00072C25 File Offset: 0x00070E25
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x170027E0 RID: 10208
			// (set) Token: 0x06004656 RID: 18006 RVA: 0x00072C3D File Offset: 0x00070E3D
			public virtual SwitchParameter OutOfBoxCollection
			{
				set
				{
					base.PowerSharpParameters["OutOfBoxCollection"] = value;
				}
			}

			// Token: 0x170027E1 RID: 10209
			// (set) Token: 0x06004657 RID: 18007 RVA: 0x00072C55 File Offset: 0x00070E55
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170027E2 RID: 10210
			// (set) Token: 0x06004658 RID: 18008 RVA: 0x00072C73 File Offset: 0x00070E73
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027E3 RID: 10211
			// (set) Token: 0x06004659 RID: 18009 RVA: 0x00072C86 File Offset: 0x00070E86
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027E4 RID: 10212
			// (set) Token: 0x0600465A RID: 18010 RVA: 0x00072C9E File Offset: 0x00070E9E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027E5 RID: 10213
			// (set) Token: 0x0600465B RID: 18011 RVA: 0x00072CB6 File Offset: 0x00070EB6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027E6 RID: 10214
			// (set) Token: 0x0600465C RID: 18012 RVA: 0x00072CCE File Offset: 0x00070ECE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027E7 RID: 10215
			// (set) Token: 0x0600465D RID: 18013 RVA: 0x00072CE6 File Offset: 0x00070EE6
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170027E8 RID: 10216
			// (set) Token: 0x0600465E RID: 18014 RVA: 0x00072CFE File Offset: 0x00070EFE
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000517 RID: 1303
		public class OutOfBoxInstallParameters : ParametersBase
		{
			// Token: 0x170027E9 RID: 10217
			// (set) Token: 0x06004660 RID: 18016 RVA: 0x00072D1E File Offset: 0x00070F1E
			public virtual SwitchParameter InstallDefaultCollection
			{
				set
				{
					base.PowerSharpParameters["InstallDefaultCollection"] = value;
				}
			}

			// Token: 0x170027EA RID: 10218
			// (set) Token: 0x06004661 RID: 18017 RVA: 0x00072D36 File Offset: 0x00070F36
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170027EB RID: 10219
			// (set) Token: 0x06004662 RID: 18018 RVA: 0x00072D54 File Offset: 0x00070F54
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027EC RID: 10220
			// (set) Token: 0x06004663 RID: 18019 RVA: 0x00072D67 File Offset: 0x00070F67
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027ED RID: 10221
			// (set) Token: 0x06004664 RID: 18020 RVA: 0x00072D7F File Offset: 0x00070F7F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027EE RID: 10222
			// (set) Token: 0x06004665 RID: 18021 RVA: 0x00072D97 File Offset: 0x00070F97
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027EF RID: 10223
			// (set) Token: 0x06004666 RID: 18022 RVA: 0x00072DAF File Offset: 0x00070FAF
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027F0 RID: 10224
			// (set) Token: 0x06004667 RID: 18023 RVA: 0x00072DC7 File Offset: 0x00070FC7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170027F1 RID: 10225
			// (set) Token: 0x06004668 RID: 18024 RVA: 0x00072DDF File Offset: 0x00070FDF
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000518 RID: 1304
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170027F2 RID: 10226
			// (set) Token: 0x0600466A RID: 18026 RVA: 0x00072DFF File Offset: 0x00070FFF
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170027F3 RID: 10227
			// (set) Token: 0x0600466B RID: 18027 RVA: 0x00072E1D File Offset: 0x0007101D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027F4 RID: 10228
			// (set) Token: 0x0600466C RID: 18028 RVA: 0x00072E30 File Offset: 0x00071030
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027F5 RID: 10229
			// (set) Token: 0x0600466D RID: 18029 RVA: 0x00072E48 File Offset: 0x00071048
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027F6 RID: 10230
			// (set) Token: 0x0600466E RID: 18030 RVA: 0x00072E60 File Offset: 0x00071060
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027F7 RID: 10231
			// (set) Token: 0x0600466F RID: 18031 RVA: 0x00072E78 File Offset: 0x00071078
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027F8 RID: 10232
			// (set) Token: 0x06004670 RID: 18032 RVA: 0x00072E90 File Offset: 0x00071090
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x170027F9 RID: 10233
			// (set) Token: 0x06004671 RID: 18033 RVA: 0x00072EA8 File Offset: 0x000710A8
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
