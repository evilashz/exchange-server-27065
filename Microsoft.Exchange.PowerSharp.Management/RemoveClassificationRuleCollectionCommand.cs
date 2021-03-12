using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ClassificationDefinitions;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000519 RID: 1305
	public class RemoveClassificationRuleCollectionCommand : SyntheticCommandWithPipelineInput<TransportRule, TransportRule>
	{
		// Token: 0x06004673 RID: 18035 RVA: 0x00072EC8 File Offset: 0x000710C8
		private RemoveClassificationRuleCollectionCommand() : base("Remove-ClassificationRuleCollection")
		{
		}

		// Token: 0x06004674 RID: 18036 RVA: 0x00072ED5 File Offset: 0x000710D5
		public RemoveClassificationRuleCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x00072EE4 File Offset: 0x000710E4
		public virtual RemoveClassificationRuleCollectionCommand SetParameters(RemoveClassificationRuleCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004676 RID: 18038 RVA: 0x00072EEE File Offset: 0x000710EE
		public virtual RemoveClassificationRuleCollectionCommand SetParameters(RemoveClassificationRuleCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200051A RID: 1306
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170027FA RID: 10234
			// (set) Token: 0x06004677 RID: 18039 RVA: 0x00072EF8 File Offset: 0x000710F8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170027FB RID: 10235
			// (set) Token: 0x06004678 RID: 18040 RVA: 0x00072F0B File Offset: 0x0007110B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170027FC RID: 10236
			// (set) Token: 0x06004679 RID: 18041 RVA: 0x00072F23 File Offset: 0x00071123
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170027FD RID: 10237
			// (set) Token: 0x0600467A RID: 18042 RVA: 0x00072F3B File Offset: 0x0007113B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170027FE RID: 10238
			// (set) Token: 0x0600467B RID: 18043 RVA: 0x00072F53 File Offset: 0x00071153
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170027FF RID: 10239
			// (set) Token: 0x0600467C RID: 18044 RVA: 0x00072F6B File Offset: 0x0007116B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002800 RID: 10240
			// (set) Token: 0x0600467D RID: 18045 RVA: 0x00072F83 File Offset: 0x00071183
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200051B RID: 1307
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002801 RID: 10241
			// (set) Token: 0x0600467F RID: 18047 RVA: 0x00072FA3 File Offset: 0x000711A3
			public virtual ClassificationRuleCollectionIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002802 RID: 10242
			// (set) Token: 0x06004680 RID: 18048 RVA: 0x00072FB6 File Offset: 0x000711B6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002803 RID: 10243
			// (set) Token: 0x06004681 RID: 18049 RVA: 0x00072FC9 File Offset: 0x000711C9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002804 RID: 10244
			// (set) Token: 0x06004682 RID: 18050 RVA: 0x00072FE1 File Offset: 0x000711E1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002805 RID: 10245
			// (set) Token: 0x06004683 RID: 18051 RVA: 0x00072FF9 File Offset: 0x000711F9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002806 RID: 10246
			// (set) Token: 0x06004684 RID: 18052 RVA: 0x00073011 File Offset: 0x00071211
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002807 RID: 10247
			// (set) Token: 0x06004685 RID: 18053 RVA: 0x00073029 File Offset: 0x00071229
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002808 RID: 10248
			// (set) Token: 0x06004686 RID: 18054 RVA: 0x00073041 File Offset: 0x00071241
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
