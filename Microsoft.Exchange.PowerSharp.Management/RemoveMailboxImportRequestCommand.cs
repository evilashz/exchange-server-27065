using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A25 RID: 2597
	public class RemoveMailboxImportRequestCommand : SyntheticCommandWithPipelineInput<MailboxImportRequestIdParameter, MailboxImportRequestIdParameter>
	{
		// Token: 0x060081CF RID: 33231 RVA: 0x000C04DC File Offset: 0x000BE6DC
		private RemoveMailboxImportRequestCommand() : base("Remove-MailboxImportRequest")
		{
		}

		// Token: 0x060081D0 RID: 33232 RVA: 0x000C04E9 File Offset: 0x000BE6E9
		public RemoveMailboxImportRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060081D1 RID: 33233 RVA: 0x000C04F8 File Offset: 0x000BE6F8
		public virtual RemoveMailboxImportRequestCommand SetParameters(RemoveMailboxImportRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x000C0502 File Offset: 0x000BE702
		public virtual RemoveMailboxImportRequestCommand SetParameters(RemoveMailboxImportRequestCommand.MigrationRequestQueueParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x000C050C File Offset: 0x000BE70C
		public virtual RemoveMailboxImportRequestCommand SetParameters(RemoveMailboxImportRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A26 RID: 2598
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700593E RID: 22846
			// (set) Token: 0x060081D4 RID: 33236 RVA: 0x000C0516 File Offset: 0x000BE716
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxImportRequestIdParameter(value) : null);
				}
			}

			// Token: 0x1700593F RID: 22847
			// (set) Token: 0x060081D5 RID: 33237 RVA: 0x000C0534 File Offset: 0x000BE734
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005940 RID: 22848
			// (set) Token: 0x060081D6 RID: 33238 RVA: 0x000C0547 File Offset: 0x000BE747
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005941 RID: 22849
			// (set) Token: 0x060081D7 RID: 33239 RVA: 0x000C055F File Offset: 0x000BE75F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005942 RID: 22850
			// (set) Token: 0x060081D8 RID: 33240 RVA: 0x000C0577 File Offset: 0x000BE777
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005943 RID: 22851
			// (set) Token: 0x060081D9 RID: 33241 RVA: 0x000C058F File Offset: 0x000BE78F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005944 RID: 22852
			// (set) Token: 0x060081DA RID: 33242 RVA: 0x000C05A7 File Offset: 0x000BE7A7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005945 RID: 22853
			// (set) Token: 0x060081DB RID: 33243 RVA: 0x000C05BF File Offset: 0x000BE7BF
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A27 RID: 2599
		public class MigrationRequestQueueParameters : ParametersBase
		{
			// Token: 0x17005946 RID: 22854
			// (set) Token: 0x060081DD RID: 33245 RVA: 0x000C05DF File Offset: 0x000BE7DF
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005947 RID: 22855
			// (set) Token: 0x060081DE RID: 33246 RVA: 0x000C05F2 File Offset: 0x000BE7F2
			public virtual Guid RequestGuid
			{
				set
				{
					base.PowerSharpParameters["RequestGuid"] = value;
				}
			}

			// Token: 0x17005948 RID: 22856
			// (set) Token: 0x060081DF RID: 33247 RVA: 0x000C060A File Offset: 0x000BE80A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005949 RID: 22857
			// (set) Token: 0x060081E0 RID: 33248 RVA: 0x000C061D File Offset: 0x000BE81D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700594A RID: 22858
			// (set) Token: 0x060081E1 RID: 33249 RVA: 0x000C0635 File Offset: 0x000BE835
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700594B RID: 22859
			// (set) Token: 0x060081E2 RID: 33250 RVA: 0x000C064D File Offset: 0x000BE84D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700594C RID: 22860
			// (set) Token: 0x060081E3 RID: 33251 RVA: 0x000C0665 File Offset: 0x000BE865
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700594D RID: 22861
			// (set) Token: 0x060081E4 RID: 33252 RVA: 0x000C067D File Offset: 0x000BE87D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700594E RID: 22862
			// (set) Token: 0x060081E5 RID: 33253 RVA: 0x000C0695 File Offset: 0x000BE895
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000A28 RID: 2600
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700594F RID: 22863
			// (set) Token: 0x060081E7 RID: 33255 RVA: 0x000C06B5 File Offset: 0x000BE8B5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005950 RID: 22864
			// (set) Token: 0x060081E8 RID: 33256 RVA: 0x000C06C8 File Offset: 0x000BE8C8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005951 RID: 22865
			// (set) Token: 0x060081E9 RID: 33257 RVA: 0x000C06E0 File Offset: 0x000BE8E0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005952 RID: 22866
			// (set) Token: 0x060081EA RID: 33258 RVA: 0x000C06F8 File Offset: 0x000BE8F8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005953 RID: 22867
			// (set) Token: 0x060081EB RID: 33259 RVA: 0x000C0710 File Offset: 0x000BE910
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005954 RID: 22868
			// (set) Token: 0x060081EC RID: 33260 RVA: 0x000C0728 File Offset: 0x000BE928
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17005955 RID: 22869
			// (set) Token: 0x060081ED RID: 33261 RVA: 0x000C0740 File Offset: 0x000BE940
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
