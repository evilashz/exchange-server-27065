using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000367 RID: 871
	public class NewManagementScopeCommand : SyntheticCommandWithPipelineInput<ManagementScope, ManagementScope>
	{
		// Token: 0x0600377D RID: 14205 RVA: 0x0005FDB4 File Offset: 0x0005DFB4
		private NewManagementScopeCommand() : base("New-ManagementScope")
		{
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x0005FDC1 File Offset: 0x0005DFC1
		public NewManagementScopeCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x0005FDD0 File Offset: 0x0005DFD0
		public virtual NewManagementScopeCommand SetParameters(NewManagementScopeCommand.RecipientFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x0005FDDA File Offset: 0x0005DFDA
		public virtual NewManagementScopeCommand SetParameters(NewManagementScopeCommand.ServerFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0005FDE4 File Offset: 0x0005DFE4
		public virtual NewManagementScopeCommand SetParameters(NewManagementScopeCommand.ServerListParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x0005FDEE File Offset: 0x0005DFEE
		public virtual NewManagementScopeCommand SetParameters(NewManagementScopeCommand.DatabaseListParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003783 RID: 14211 RVA: 0x0005FDF8 File Offset: 0x0005DFF8
		public virtual NewManagementScopeCommand SetParameters(NewManagementScopeCommand.DatabaseFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003784 RID: 14212 RVA: 0x0005FE02 File Offset: 0x0005E002
		public virtual NewManagementScopeCommand SetParameters(NewManagementScopeCommand.PartnerFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003785 RID: 14213 RVA: 0x0005FE0C File Offset: 0x0005E00C
		public virtual NewManagementScopeCommand SetParameters(NewManagementScopeCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000368 RID: 872
		public class RecipientFilterParameters : ParametersBase
		{
			// Token: 0x17001C68 RID: 7272
			// (set) Token: 0x06003786 RID: 14214 RVA: 0x0005FE16 File Offset: 0x0005E016
			public virtual string RecipientRestrictionFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientRestrictionFilter"] = value;
				}
			}

			// Token: 0x17001C69 RID: 7273
			// (set) Token: 0x06003787 RID: 14215 RVA: 0x0005FE29 File Offset: 0x0005E029
			public virtual string RecipientRoot
			{
				set
				{
					base.PowerSharpParameters["RecipientRoot"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17001C6A RID: 7274
			// (set) Token: 0x06003788 RID: 14216 RVA: 0x0005FE47 File Offset: 0x0005E047
			public virtual SwitchParameter Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x17001C6B RID: 7275
			// (set) Token: 0x06003789 RID: 14217 RVA: 0x0005FE5F File Offset: 0x0005E05F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C6C RID: 7276
			// (set) Token: 0x0600378A RID: 14218 RVA: 0x0005FE77 File Offset: 0x0005E077
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001C6D RID: 7277
			// (set) Token: 0x0600378B RID: 14219 RVA: 0x0005FE95 File Offset: 0x0005E095
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001C6E RID: 7278
			// (set) Token: 0x0600378C RID: 14220 RVA: 0x0005FEA8 File Offset: 0x0005E0A8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C6F RID: 7279
			// (set) Token: 0x0600378D RID: 14221 RVA: 0x0005FEBB File Offset: 0x0005E0BB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C70 RID: 7280
			// (set) Token: 0x0600378E RID: 14222 RVA: 0x0005FED3 File Offset: 0x0005E0D3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C71 RID: 7281
			// (set) Token: 0x0600378F RID: 14223 RVA: 0x0005FEEB File Offset: 0x0005E0EB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C72 RID: 7282
			// (set) Token: 0x06003790 RID: 14224 RVA: 0x0005FF03 File Offset: 0x0005E103
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C73 RID: 7283
			// (set) Token: 0x06003791 RID: 14225 RVA: 0x0005FF1B File Offset: 0x0005E11B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000369 RID: 873
		public class ServerFilterParameters : ParametersBase
		{
			// Token: 0x17001C74 RID: 7284
			// (set) Token: 0x06003793 RID: 14227 RVA: 0x0005FF3B File Offset: 0x0005E13B
			public virtual string ServerRestrictionFilter
			{
				set
				{
					base.PowerSharpParameters["ServerRestrictionFilter"] = value;
				}
			}

			// Token: 0x17001C75 RID: 7285
			// (set) Token: 0x06003794 RID: 14228 RVA: 0x0005FF4E File Offset: 0x0005E14E
			public virtual SwitchParameter Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x17001C76 RID: 7286
			// (set) Token: 0x06003795 RID: 14229 RVA: 0x0005FF66 File Offset: 0x0005E166
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C77 RID: 7287
			// (set) Token: 0x06003796 RID: 14230 RVA: 0x0005FF7E File Offset: 0x0005E17E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001C78 RID: 7288
			// (set) Token: 0x06003797 RID: 14231 RVA: 0x0005FF9C File Offset: 0x0005E19C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001C79 RID: 7289
			// (set) Token: 0x06003798 RID: 14232 RVA: 0x0005FFAF File Offset: 0x0005E1AF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C7A RID: 7290
			// (set) Token: 0x06003799 RID: 14233 RVA: 0x0005FFC2 File Offset: 0x0005E1C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C7B RID: 7291
			// (set) Token: 0x0600379A RID: 14234 RVA: 0x0005FFDA File Offset: 0x0005E1DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C7C RID: 7292
			// (set) Token: 0x0600379B RID: 14235 RVA: 0x0005FFF2 File Offset: 0x0005E1F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C7D RID: 7293
			// (set) Token: 0x0600379C RID: 14236 RVA: 0x0006000A File Offset: 0x0005E20A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C7E RID: 7294
			// (set) Token: 0x0600379D RID: 14237 RVA: 0x00060022 File Offset: 0x0005E222
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200036A RID: 874
		public class ServerListParameters : ParametersBase
		{
			// Token: 0x17001C7F RID: 7295
			// (set) Token: 0x0600379F RID: 14239 RVA: 0x00060042 File Offset: 0x0005E242
			public virtual ServerIdParameter ServerList
			{
				set
				{
					base.PowerSharpParameters["ServerList"] = value;
				}
			}

			// Token: 0x17001C80 RID: 7296
			// (set) Token: 0x060037A0 RID: 14240 RVA: 0x00060055 File Offset: 0x0005E255
			public virtual SwitchParameter Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x17001C81 RID: 7297
			// (set) Token: 0x060037A1 RID: 14241 RVA: 0x0006006D File Offset: 0x0005E26D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C82 RID: 7298
			// (set) Token: 0x060037A2 RID: 14242 RVA: 0x00060085 File Offset: 0x0005E285
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001C83 RID: 7299
			// (set) Token: 0x060037A3 RID: 14243 RVA: 0x000600A3 File Offset: 0x0005E2A3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001C84 RID: 7300
			// (set) Token: 0x060037A4 RID: 14244 RVA: 0x000600B6 File Offset: 0x0005E2B6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C85 RID: 7301
			// (set) Token: 0x060037A5 RID: 14245 RVA: 0x000600C9 File Offset: 0x0005E2C9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C86 RID: 7302
			// (set) Token: 0x060037A6 RID: 14246 RVA: 0x000600E1 File Offset: 0x0005E2E1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C87 RID: 7303
			// (set) Token: 0x060037A7 RID: 14247 RVA: 0x000600F9 File Offset: 0x0005E2F9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C88 RID: 7304
			// (set) Token: 0x060037A8 RID: 14248 RVA: 0x00060111 File Offset: 0x0005E311
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C89 RID: 7305
			// (set) Token: 0x060037A9 RID: 14249 RVA: 0x00060129 File Offset: 0x0005E329
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200036B RID: 875
		public class DatabaseListParameters : ParametersBase
		{
			// Token: 0x17001C8A RID: 7306
			// (set) Token: 0x060037AB RID: 14251 RVA: 0x00060149 File Offset: 0x0005E349
			public virtual DatabaseIdParameter DatabaseList
			{
				set
				{
					base.PowerSharpParameters["DatabaseList"] = value;
				}
			}

			// Token: 0x17001C8B RID: 7307
			// (set) Token: 0x060037AC RID: 14252 RVA: 0x0006015C File Offset: 0x0005E35C
			public virtual SwitchParameter Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x17001C8C RID: 7308
			// (set) Token: 0x060037AD RID: 14253 RVA: 0x00060174 File Offset: 0x0005E374
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C8D RID: 7309
			// (set) Token: 0x060037AE RID: 14254 RVA: 0x0006018C File Offset: 0x0005E38C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001C8E RID: 7310
			// (set) Token: 0x060037AF RID: 14255 RVA: 0x000601AA File Offset: 0x0005E3AA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001C8F RID: 7311
			// (set) Token: 0x060037B0 RID: 14256 RVA: 0x000601BD File Offset: 0x0005E3BD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C90 RID: 7312
			// (set) Token: 0x060037B1 RID: 14257 RVA: 0x000601D0 File Offset: 0x0005E3D0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C91 RID: 7313
			// (set) Token: 0x060037B2 RID: 14258 RVA: 0x000601E8 File Offset: 0x0005E3E8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C92 RID: 7314
			// (set) Token: 0x060037B3 RID: 14259 RVA: 0x00060200 File Offset: 0x0005E400
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C93 RID: 7315
			// (set) Token: 0x060037B4 RID: 14260 RVA: 0x00060218 File Offset: 0x0005E418
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C94 RID: 7316
			// (set) Token: 0x060037B5 RID: 14261 RVA: 0x00060230 File Offset: 0x0005E430
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200036C RID: 876
		public class DatabaseFilterParameters : ParametersBase
		{
			// Token: 0x17001C95 RID: 7317
			// (set) Token: 0x060037B7 RID: 14263 RVA: 0x00060250 File Offset: 0x0005E450
			public virtual string DatabaseRestrictionFilter
			{
				set
				{
					base.PowerSharpParameters["DatabaseRestrictionFilter"] = value;
				}
			}

			// Token: 0x17001C96 RID: 7318
			// (set) Token: 0x060037B8 RID: 14264 RVA: 0x00060263 File Offset: 0x0005E463
			public virtual SwitchParameter Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x17001C97 RID: 7319
			// (set) Token: 0x060037B9 RID: 14265 RVA: 0x0006027B File Offset: 0x0005E47B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17001C98 RID: 7320
			// (set) Token: 0x060037BA RID: 14266 RVA: 0x00060293 File Offset: 0x0005E493
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001C99 RID: 7321
			// (set) Token: 0x060037BB RID: 14267 RVA: 0x000602B1 File Offset: 0x0005E4B1
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001C9A RID: 7322
			// (set) Token: 0x060037BC RID: 14268 RVA: 0x000602C4 File Offset: 0x0005E4C4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001C9B RID: 7323
			// (set) Token: 0x060037BD RID: 14269 RVA: 0x000602D7 File Offset: 0x0005E4D7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001C9C RID: 7324
			// (set) Token: 0x060037BE RID: 14270 RVA: 0x000602EF File Offset: 0x0005E4EF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001C9D RID: 7325
			// (set) Token: 0x060037BF RID: 14271 RVA: 0x00060307 File Offset: 0x0005E507
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001C9E RID: 7326
			// (set) Token: 0x060037C0 RID: 14272 RVA: 0x0006031F File Offset: 0x0005E51F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001C9F RID: 7327
			// (set) Token: 0x060037C1 RID: 14273 RVA: 0x00060337 File Offset: 0x0005E537
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200036D RID: 877
		public class PartnerFilterParameters : ParametersBase
		{
			// Token: 0x17001CA0 RID: 7328
			// (set) Token: 0x060037C3 RID: 14275 RVA: 0x00060357 File Offset: 0x0005E557
			public virtual string PartnerDelegatedTenantRestrictionFilter
			{
				set
				{
					base.PowerSharpParameters["PartnerDelegatedTenantRestrictionFilter"] = value;
				}
			}

			// Token: 0x17001CA1 RID: 7329
			// (set) Token: 0x060037C4 RID: 14276 RVA: 0x0006036A File Offset: 0x0005E56A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001CA2 RID: 7330
			// (set) Token: 0x060037C5 RID: 14277 RVA: 0x00060388 File Offset: 0x0005E588
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001CA3 RID: 7331
			// (set) Token: 0x060037C6 RID: 14278 RVA: 0x0006039B File Offset: 0x0005E59B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CA4 RID: 7332
			// (set) Token: 0x060037C7 RID: 14279 RVA: 0x000603AE File Offset: 0x0005E5AE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CA5 RID: 7333
			// (set) Token: 0x060037C8 RID: 14280 RVA: 0x000603C6 File Offset: 0x0005E5C6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CA6 RID: 7334
			// (set) Token: 0x060037C9 RID: 14281 RVA: 0x000603DE File Offset: 0x0005E5DE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CA7 RID: 7335
			// (set) Token: 0x060037CA RID: 14282 RVA: 0x000603F6 File Offset: 0x0005E5F6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001CA8 RID: 7336
			// (set) Token: 0x060037CB RID: 14283 RVA: 0x0006040E File Offset: 0x0005E60E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x0200036E RID: 878
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001CA9 RID: 7337
			// (set) Token: 0x060037CD RID: 14285 RVA: 0x0006042E File Offset: 0x0005E62E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001CAA RID: 7338
			// (set) Token: 0x060037CE RID: 14286 RVA: 0x0006044C File Offset: 0x0005E64C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17001CAB RID: 7339
			// (set) Token: 0x060037CF RID: 14287 RVA: 0x0006045F File Offset: 0x0005E65F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001CAC RID: 7340
			// (set) Token: 0x060037D0 RID: 14288 RVA: 0x00060472 File Offset: 0x0005E672
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001CAD RID: 7341
			// (set) Token: 0x060037D1 RID: 14289 RVA: 0x0006048A File Offset: 0x0005E68A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001CAE RID: 7342
			// (set) Token: 0x060037D2 RID: 14290 RVA: 0x000604A2 File Offset: 0x0005E6A2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001CAF RID: 7343
			// (set) Token: 0x060037D3 RID: 14291 RVA: 0x000604BA File Offset: 0x0005E6BA
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17001CB0 RID: 7344
			// (set) Token: 0x060037D4 RID: 14292 RVA: 0x000604D2 File Offset: 0x0005E6D2
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
