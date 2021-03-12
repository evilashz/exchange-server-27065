using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.RecipientTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000A8D RID: 2701
	public class SetPublicFolderMigrationRequestCommand : SyntheticCommandWithPipelineInputNoOutput<PublicFolderMigrationRequestIdParameter>
	{
		// Token: 0x060085BE RID: 34238 RVA: 0x000C55BA File Offset: 0x000C37BA
		private SetPublicFolderMigrationRequestCommand() : base("Set-PublicFolderMigrationRequest")
		{
		}

		// Token: 0x060085BF RID: 34239 RVA: 0x000C55C7 File Offset: 0x000C37C7
		public SetPublicFolderMigrationRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060085C0 RID: 34240 RVA: 0x000C55D6 File Offset: 0x000C37D6
		public virtual SetPublicFolderMigrationRequestCommand SetParameters(SetPublicFolderMigrationRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060085C1 RID: 34241 RVA: 0x000C55E0 File Offset: 0x000C37E0
		public virtual SetPublicFolderMigrationRequestCommand SetParameters(SetPublicFolderMigrationRequestCommand.MigrationOutlookAnywherePublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060085C2 RID: 34242 RVA: 0x000C55EA File Offset: 0x000C37EA
		public virtual SetPublicFolderMigrationRequestCommand SetParameters(SetPublicFolderMigrationRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x000C55F4 File Offset: 0x000C37F4
		public virtual SetPublicFolderMigrationRequestCommand SetParameters(SetPublicFolderMigrationRequestCommand.RehomeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000A8E RID: 2702
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005C5D RID: 23645
			// (set) Token: 0x060085C4 RID: 34244 RVA: 0x000C55FE File Offset: 0x000C37FE
			public virtual bool PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x17005C5E RID: 23646
			// (set) Token: 0x060085C5 RID: 34245 RVA: 0x000C5616 File Offset: 0x000C3816
			public virtual string RemoteMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17005C5F RID: 23647
			// (set) Token: 0x060085C6 RID: 34246 RVA: 0x000C5629 File Offset: 0x000C3829
			public virtual string RemoteMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x17005C60 RID: 23648
			// (set) Token: 0x060085C7 RID: 34247 RVA: 0x000C563C File Offset: 0x000C383C
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x17005C61 RID: 23649
			// (set) Token: 0x060085C8 RID: 34248 RVA: 0x000C564F File Offset: 0x000C384F
			public virtual AuthenticationMethod AuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["AuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005C62 RID: 23650
			// (set) Token: 0x060085C9 RID: 34249 RVA: 0x000C5667 File Offset: 0x000C3867
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005C63 RID: 23651
			// (set) Token: 0x060085CA RID: 34250 RVA: 0x000C5685 File Offset: 0x000C3885
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C64 RID: 23652
			// (set) Token: 0x060085CB RID: 34251 RVA: 0x000C5698 File Offset: 0x000C3898
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C65 RID: 23653
			// (set) Token: 0x060085CC RID: 34252 RVA: 0x000C56B0 File Offset: 0x000C38B0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C66 RID: 23654
			// (set) Token: 0x060085CD RID: 34253 RVA: 0x000C56C8 File Offset: 0x000C38C8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C67 RID: 23655
			// (set) Token: 0x060085CE RID: 34254 RVA: 0x000C56E0 File Offset: 0x000C38E0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C68 RID: 23656
			// (set) Token: 0x060085CF RID: 34255 RVA: 0x000C56F8 File Offset: 0x000C38F8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A8F RID: 2703
		public class MigrationOutlookAnywherePublicFolderParameters : ParametersBase
		{
			// Token: 0x17005C69 RID: 23657
			// (set) Token: 0x060085D1 RID: 34257 RVA: 0x000C5718 File Offset: 0x000C3918
			public virtual PSCredential RemoteCredential
			{
				set
				{
					base.PowerSharpParameters["RemoteCredential"] = value;
				}
			}

			// Token: 0x17005C6A RID: 23658
			// (set) Token: 0x060085D2 RID: 34258 RVA: 0x000C572B File Offset: 0x000C392B
			public virtual bool PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x17005C6B RID: 23659
			// (set) Token: 0x060085D3 RID: 34259 RVA: 0x000C5743 File Offset: 0x000C3943
			public virtual string RemoteMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17005C6C RID: 23660
			// (set) Token: 0x060085D4 RID: 34260 RVA: 0x000C5756 File Offset: 0x000C3956
			public virtual string RemoteMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x17005C6D RID: 23661
			// (set) Token: 0x060085D5 RID: 34261 RVA: 0x000C5769 File Offset: 0x000C3969
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x17005C6E RID: 23662
			// (set) Token: 0x060085D6 RID: 34262 RVA: 0x000C577C File Offset: 0x000C397C
			public virtual AuthenticationMethod AuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["AuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005C6F RID: 23663
			// (set) Token: 0x060085D7 RID: 34263 RVA: 0x000C5794 File Offset: 0x000C3994
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005C70 RID: 23664
			// (set) Token: 0x060085D8 RID: 34264 RVA: 0x000C57B2 File Offset: 0x000C39B2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C71 RID: 23665
			// (set) Token: 0x060085D9 RID: 34265 RVA: 0x000C57C5 File Offset: 0x000C39C5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C72 RID: 23666
			// (set) Token: 0x060085DA RID: 34266 RVA: 0x000C57DD File Offset: 0x000C39DD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C73 RID: 23667
			// (set) Token: 0x060085DB RID: 34267 RVA: 0x000C57F5 File Offset: 0x000C39F5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C74 RID: 23668
			// (set) Token: 0x060085DC RID: 34268 RVA: 0x000C580D File Offset: 0x000C3A0D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C75 RID: 23669
			// (set) Token: 0x060085DD RID: 34269 RVA: 0x000C5825 File Offset: 0x000C3A25
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A90 RID: 2704
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005C76 RID: 23670
			// (set) Token: 0x060085DF RID: 34271 RVA: 0x000C5845 File Offset: 0x000C3A45
			public virtual Unlimited<int> BadItemLimit
			{
				set
				{
					base.PowerSharpParameters["BadItemLimit"] = value;
				}
			}

			// Token: 0x17005C77 RID: 23671
			// (set) Token: 0x060085E0 RID: 34272 RVA: 0x000C585D File Offset: 0x000C3A5D
			public virtual Unlimited<int> LargeItemLimit
			{
				set
				{
					base.PowerSharpParameters["LargeItemLimit"] = value;
				}
			}

			// Token: 0x17005C78 RID: 23672
			// (set) Token: 0x060085E1 RID: 34273 RVA: 0x000C5875 File Offset: 0x000C3A75
			public virtual SwitchParameter AcceptLargeDataLoss
			{
				set
				{
					base.PowerSharpParameters["AcceptLargeDataLoss"] = value;
				}
			}

			// Token: 0x17005C79 RID: 23673
			// (set) Token: 0x060085E2 RID: 34274 RVA: 0x000C588D File Offset: 0x000C3A8D
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005C7A RID: 23674
			// (set) Token: 0x060085E3 RID: 34275 RVA: 0x000C58A0 File Offset: 0x000C3AA0
			public virtual RequestPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17005C7B RID: 23675
			// (set) Token: 0x060085E4 RID: 34276 RVA: 0x000C58B8 File Offset: 0x000C3AB8
			public virtual Unlimited<EnhancedTimeSpan> CompletedRequestAgeLimit
			{
				set
				{
					base.PowerSharpParameters["CompletedRequestAgeLimit"] = value;
				}
			}

			// Token: 0x17005C7C RID: 23676
			// (set) Token: 0x060085E5 RID: 34277 RVA: 0x000C58D0 File Offset: 0x000C3AD0
			public virtual SkippableMergeComponent SkipMerging
			{
				set
				{
					base.PowerSharpParameters["SkipMerging"] = value;
				}
			}

			// Token: 0x17005C7D RID: 23677
			// (set) Token: 0x060085E6 RID: 34278 RVA: 0x000C58E8 File Offset: 0x000C3AE8
			public virtual InternalMrsFlag InternalFlags
			{
				set
				{
					base.PowerSharpParameters["InternalFlags"] = value;
				}
			}

			// Token: 0x17005C7E RID: 23678
			// (set) Token: 0x060085E7 RID: 34279 RVA: 0x000C5900 File Offset: 0x000C3B00
			public virtual bool PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x17005C7F RID: 23679
			// (set) Token: 0x060085E8 RID: 34280 RVA: 0x000C5918 File Offset: 0x000C3B18
			public virtual string RemoteMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17005C80 RID: 23680
			// (set) Token: 0x060085E9 RID: 34281 RVA: 0x000C592B File Offset: 0x000C3B2B
			public virtual string RemoteMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x17005C81 RID: 23681
			// (set) Token: 0x060085EA RID: 34282 RVA: 0x000C593E File Offset: 0x000C3B3E
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x17005C82 RID: 23682
			// (set) Token: 0x060085EB RID: 34283 RVA: 0x000C5951 File Offset: 0x000C3B51
			public virtual AuthenticationMethod AuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["AuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005C83 RID: 23683
			// (set) Token: 0x060085EC RID: 34284 RVA: 0x000C5969 File Offset: 0x000C3B69
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005C84 RID: 23684
			// (set) Token: 0x060085ED RID: 34285 RVA: 0x000C5987 File Offset: 0x000C3B87
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C85 RID: 23685
			// (set) Token: 0x060085EE RID: 34286 RVA: 0x000C599A File Offset: 0x000C3B9A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C86 RID: 23686
			// (set) Token: 0x060085EF RID: 34287 RVA: 0x000C59B2 File Offset: 0x000C3BB2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C87 RID: 23687
			// (set) Token: 0x060085F0 RID: 34288 RVA: 0x000C59CA File Offset: 0x000C3BCA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C88 RID: 23688
			// (set) Token: 0x060085F1 RID: 34289 RVA: 0x000C59E2 File Offset: 0x000C3BE2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C89 RID: 23689
			// (set) Token: 0x060085F2 RID: 34290 RVA: 0x000C59FA File Offset: 0x000C3BFA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000A91 RID: 2705
		public class RehomeParameters : ParametersBase
		{
			// Token: 0x17005C8A RID: 23690
			// (set) Token: 0x060085F4 RID: 34292 RVA: 0x000C5A1A File Offset: 0x000C3C1A
			public virtual SwitchParameter RehomeRequest
			{
				set
				{
					base.PowerSharpParameters["RehomeRequest"] = value;
				}
			}

			// Token: 0x17005C8B RID: 23691
			// (set) Token: 0x060085F5 RID: 34293 RVA: 0x000C5A32 File Offset: 0x000C3C32
			public virtual bool PreventCompletion
			{
				set
				{
					base.PowerSharpParameters["PreventCompletion"] = value;
				}
			}

			// Token: 0x17005C8C RID: 23692
			// (set) Token: 0x060085F6 RID: 34294 RVA: 0x000C5A4A File Offset: 0x000C3C4A
			public virtual string RemoteMailboxLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxLegacyDN"] = value;
				}
			}

			// Token: 0x17005C8D RID: 23693
			// (set) Token: 0x060085F7 RID: 34295 RVA: 0x000C5A5D File Offset: 0x000C3C5D
			public virtual string RemoteMailboxServerLegacyDN
			{
				set
				{
					base.PowerSharpParameters["RemoteMailboxServerLegacyDN"] = value;
				}
			}

			// Token: 0x17005C8E RID: 23694
			// (set) Token: 0x060085F8 RID: 34296 RVA: 0x000C5A70 File Offset: 0x000C3C70
			public virtual Fqdn OutlookAnywhereHostName
			{
				set
				{
					base.PowerSharpParameters["OutlookAnywhereHostName"] = value;
				}
			}

			// Token: 0x17005C8F RID: 23695
			// (set) Token: 0x060085F9 RID: 34297 RVA: 0x000C5A83 File Offset: 0x000C3C83
			public virtual AuthenticationMethod AuthenticationMethod
			{
				set
				{
					base.PowerSharpParameters["AuthenticationMethod"] = value;
				}
			}

			// Token: 0x17005C90 RID: 23696
			// (set) Token: 0x060085FA RID: 34298 RVA: 0x000C5A9B File Offset: 0x000C3C9B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderMigrationRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005C91 RID: 23697
			// (set) Token: 0x060085FB RID: 34299 RVA: 0x000C5AB9 File Offset: 0x000C3CB9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005C92 RID: 23698
			// (set) Token: 0x060085FC RID: 34300 RVA: 0x000C5ACC File Offset: 0x000C3CCC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005C93 RID: 23699
			// (set) Token: 0x060085FD RID: 34301 RVA: 0x000C5AE4 File Offset: 0x000C3CE4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005C94 RID: 23700
			// (set) Token: 0x060085FE RID: 34302 RVA: 0x000C5AFC File Offset: 0x000C3CFC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005C95 RID: 23701
			// (set) Token: 0x060085FF RID: 34303 RVA: 0x000C5B14 File Offset: 0x000C3D14
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17005C96 RID: 23702
			// (set) Token: 0x06008600 RID: 34304 RVA: 0x000C5B2C File Offset: 0x000C3D2C
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
