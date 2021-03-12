using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200051C RID: 1308
	public class SetClassificationRuleCollectionCommand : SyntheticCommandWithPipelineInputNoOutput<TransportRule>
	{
		// Token: 0x06004688 RID: 18056 RVA: 0x00073061 File Offset: 0x00071261
		private SetClassificationRuleCollectionCommand() : base("Set-ClassificationRuleCollection")
		{
		}

		// Token: 0x06004689 RID: 18057 RVA: 0x0007306E File Offset: 0x0007126E
		public SetClassificationRuleCollectionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600468A RID: 18058 RVA: 0x0007307D File Offset: 0x0007127D
		public virtual SetClassificationRuleCollectionCommand SetParameters(SetClassificationRuleCollectionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x00073087 File Offset: 0x00071287
		public virtual SetClassificationRuleCollectionCommand SetParameters(SetClassificationRuleCollectionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200051D RID: 1309
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002809 RID: 10249
			// (set) Token: 0x0600468C RID: 18060 RVA: 0x00073091 File Offset: 0x00071291
			public virtual byte FileData
			{
				set
				{
					base.PowerSharpParameters["FileData"] = value;
				}
			}

			// Token: 0x1700280A RID: 10250
			// (set) Token: 0x0600468D RID: 18061 RVA: 0x000730A9 File Offset: 0x000712A9
			public virtual SwitchParameter OutOfBoxCollection
			{
				set
				{
					base.PowerSharpParameters["OutOfBoxCollection"] = value;
				}
			}

			// Token: 0x1700280B RID: 10251
			// (set) Token: 0x0600468E RID: 18062 RVA: 0x000730C1 File Offset: 0x000712C1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700280C RID: 10252
			// (set) Token: 0x0600468F RID: 18063 RVA: 0x000730DF File Offset: 0x000712DF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700280D RID: 10253
			// (set) Token: 0x06004690 RID: 18064 RVA: 0x000730F2 File Offset: 0x000712F2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700280E RID: 10254
			// (set) Token: 0x06004691 RID: 18065 RVA: 0x00073105 File Offset: 0x00071305
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700280F RID: 10255
			// (set) Token: 0x06004692 RID: 18066 RVA: 0x0007311D File Offset: 0x0007131D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002810 RID: 10256
			// (set) Token: 0x06004693 RID: 18067 RVA: 0x00073135 File Offset: 0x00071335
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002811 RID: 10257
			// (set) Token: 0x06004694 RID: 18068 RVA: 0x0007314D File Offset: 0x0007134D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002812 RID: 10258
			// (set) Token: 0x06004695 RID: 18069 RVA: 0x00073165 File Offset: 0x00071365
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002813 RID: 10259
			// (set) Token: 0x06004696 RID: 18070 RVA: 0x0007317D File Offset: 0x0007137D
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200051E RID: 1310
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002814 RID: 10260
			// (set) Token: 0x06004698 RID: 18072 RVA: 0x0007319D File Offset: 0x0007139D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17002815 RID: 10261
			// (set) Token: 0x06004699 RID: 18073 RVA: 0x000731BB File Offset: 0x000713BB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002816 RID: 10262
			// (set) Token: 0x0600469A RID: 18074 RVA: 0x000731CE File Offset: 0x000713CE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17002817 RID: 10263
			// (set) Token: 0x0600469B RID: 18075 RVA: 0x000731E1 File Offset: 0x000713E1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002818 RID: 10264
			// (set) Token: 0x0600469C RID: 18076 RVA: 0x000731F9 File Offset: 0x000713F9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002819 RID: 10265
			// (set) Token: 0x0600469D RID: 18077 RVA: 0x00073211 File Offset: 0x00071411
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700281A RID: 10266
			// (set) Token: 0x0600469E RID: 18078 RVA: 0x00073229 File Offset: 0x00071429
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700281B RID: 10267
			// (set) Token: 0x0600469F RID: 18079 RVA: 0x00073241 File Offset: 0x00071441
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700281C RID: 10268
			// (set) Token: 0x060046A0 RID: 18080 RVA: 0x00073259 File Offset: 0x00071459
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
