using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C39 RID: 3129
	public class GetGroupMailboxCommand : SyntheticCommand<object>
	{
		// Token: 0x060098BE RID: 39102 RVA: 0x000DDF4E File Offset: 0x000DC14E
		private GetGroupMailboxCommand() : base("Get-GroupMailbox")
		{
		}

		// Token: 0x060098BF RID: 39103 RVA: 0x000DDF5B File Offset: 0x000DC15B
		public GetGroupMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060098C0 RID: 39104 RVA: 0x000DDF6A File Offset: 0x000DC16A
		public virtual GetGroupMailboxCommand SetParameters(GetGroupMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060098C1 RID: 39105 RVA: 0x000DDF74 File Offset: 0x000DC174
		public virtual GetGroupMailboxCommand SetParameters(GetGroupMailboxCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060098C2 RID: 39106 RVA: 0x000DDF7E File Offset: 0x000DC17E
		public virtual GetGroupMailboxCommand SetParameters(GetGroupMailboxCommand.ServerSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060098C3 RID: 39107 RVA: 0x000DDF88 File Offset: 0x000DC188
		public virtual GetGroupMailboxCommand SetParameters(GetGroupMailboxCommand.DatabaseSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060098C4 RID: 39108 RVA: 0x000DDF92 File Offset: 0x000DC192
		public virtual GetGroupMailboxCommand SetParameters(GetGroupMailboxCommand.AnrSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C3A RID: 3130
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006C05 RID: 27653
			// (set) Token: 0x060098C5 RID: 39109 RVA: 0x000DDF9C File Offset: 0x000DC19C
			public virtual SwitchParameter IncludeMembers
			{
				set
				{
					base.PowerSharpParameters["IncludeMembers"] = value;
				}
			}

			// Token: 0x17006C06 RID: 27654
			// (set) Token: 0x060098C6 RID: 39110 RVA: 0x000DDFB4 File Offset: 0x000DC1B4
			public virtual SwitchParameter IncludePermissionsVersion
			{
				set
				{
					base.PowerSharpParameters["IncludePermissionsVersion"] = value;
				}
			}

			// Token: 0x17006C07 RID: 27655
			// (set) Token: 0x060098C7 RID: 39111 RVA: 0x000DDFCC File Offset: 0x000DC1CC
			public virtual SwitchParameter IncludeMemberSyncStatus
			{
				set
				{
					base.PowerSharpParameters["IncludeMemberSyncStatus"] = value;
				}
			}

			// Token: 0x17006C08 RID: 27656
			// (set) Token: 0x060098C8 RID: 39112 RVA: 0x000DDFE4 File Offset: 0x000DC1E4
			public virtual SwitchParameter IncludeMailboxUrls
			{
				set
				{
					base.PowerSharpParameters["IncludeMailboxUrls"] = value;
				}
			}

			// Token: 0x17006C09 RID: 27657
			// (set) Token: 0x060098C9 RID: 39113 RVA: 0x000DDFFC File Offset: 0x000DC1FC
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17006C0A RID: 27658
			// (set) Token: 0x060098CA RID: 39114 RVA: 0x000DE014 File Offset: 0x000DC214
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17006C0B RID: 27659
			// (set) Token: 0x060098CB RID: 39115 RVA: 0x000DE02C File Offset: 0x000DC22C
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006C0C RID: 27660
			// (set) Token: 0x060098CC RID: 39116 RVA: 0x000DE044 File Offset: 0x000DC244
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006C0D RID: 27661
			// (set) Token: 0x060098CD RID: 39117 RVA: 0x000DE057 File Offset: 0x000DC257
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006C0E RID: 27662
			// (set) Token: 0x060098CE RID: 39118 RVA: 0x000DE075 File Offset: 0x000DC275
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006C0F RID: 27663
			// (set) Token: 0x060098CF RID: 39119 RVA: 0x000DE088 File Offset: 0x000DC288
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006C10 RID: 27664
			// (set) Token: 0x060098D0 RID: 39120 RVA: 0x000DE09B File Offset: 0x000DC29B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006C11 RID: 27665
			// (set) Token: 0x060098D1 RID: 39121 RVA: 0x000DE0B9 File Offset: 0x000DC2B9
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006C12 RID: 27666
			// (set) Token: 0x060098D2 RID: 39122 RVA: 0x000DE0D1 File Offset: 0x000DC2D1
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006C13 RID: 27667
			// (set) Token: 0x060098D3 RID: 39123 RVA: 0x000DE0E9 File Offset: 0x000DC2E9
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006C14 RID: 27668
			// (set) Token: 0x060098D4 RID: 39124 RVA: 0x000DE101 File Offset: 0x000DC301
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006C15 RID: 27669
			// (set) Token: 0x060098D5 RID: 39125 RVA: 0x000DE114 File Offset: 0x000DC314
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006C16 RID: 27670
			// (set) Token: 0x060098D6 RID: 39126 RVA: 0x000DE12C File Offset: 0x000DC32C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006C17 RID: 27671
			// (set) Token: 0x060098D7 RID: 39127 RVA: 0x000DE144 File Offset: 0x000DC344
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006C18 RID: 27672
			// (set) Token: 0x060098D8 RID: 39128 RVA: 0x000DE15C File Offset: 0x000DC35C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C3B RID: 3131
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006C19 RID: 27673
			// (set) Token: 0x060098DA RID: 39130 RVA: 0x000DE17C File Offset: 0x000DC37C
			public virtual string ExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ExecutingUser"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x17006C1A RID: 27674
			// (set) Token: 0x060098DB RID: 39131 RVA: 0x000DE19A File Offset: 0x000DC39A
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006C1B RID: 27675
			// (set) Token: 0x060098DC RID: 39132 RVA: 0x000DE1B8 File Offset: 0x000DC3B8
			public virtual SwitchParameter IncludeMembers
			{
				set
				{
					base.PowerSharpParameters["IncludeMembers"] = value;
				}
			}

			// Token: 0x17006C1C RID: 27676
			// (set) Token: 0x060098DD RID: 39133 RVA: 0x000DE1D0 File Offset: 0x000DC3D0
			public virtual SwitchParameter IncludePermissionsVersion
			{
				set
				{
					base.PowerSharpParameters["IncludePermissionsVersion"] = value;
				}
			}

			// Token: 0x17006C1D RID: 27677
			// (set) Token: 0x060098DE RID: 39134 RVA: 0x000DE1E8 File Offset: 0x000DC3E8
			public virtual SwitchParameter IncludeMemberSyncStatus
			{
				set
				{
					base.PowerSharpParameters["IncludeMemberSyncStatus"] = value;
				}
			}

			// Token: 0x17006C1E RID: 27678
			// (set) Token: 0x060098DF RID: 39135 RVA: 0x000DE200 File Offset: 0x000DC400
			public virtual SwitchParameter IncludeMailboxUrls
			{
				set
				{
					base.PowerSharpParameters["IncludeMailboxUrls"] = value;
				}
			}

			// Token: 0x17006C1F RID: 27679
			// (set) Token: 0x060098E0 RID: 39136 RVA: 0x000DE218 File Offset: 0x000DC418
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17006C20 RID: 27680
			// (set) Token: 0x060098E1 RID: 39137 RVA: 0x000DE230 File Offset: 0x000DC430
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17006C21 RID: 27681
			// (set) Token: 0x060098E2 RID: 39138 RVA: 0x000DE248 File Offset: 0x000DC448
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006C22 RID: 27682
			// (set) Token: 0x060098E3 RID: 39139 RVA: 0x000DE260 File Offset: 0x000DC460
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006C23 RID: 27683
			// (set) Token: 0x060098E4 RID: 39140 RVA: 0x000DE273 File Offset: 0x000DC473
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006C24 RID: 27684
			// (set) Token: 0x060098E5 RID: 39141 RVA: 0x000DE291 File Offset: 0x000DC491
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006C25 RID: 27685
			// (set) Token: 0x060098E6 RID: 39142 RVA: 0x000DE2A4 File Offset: 0x000DC4A4
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006C26 RID: 27686
			// (set) Token: 0x060098E7 RID: 39143 RVA: 0x000DE2B7 File Offset: 0x000DC4B7
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006C27 RID: 27687
			// (set) Token: 0x060098E8 RID: 39144 RVA: 0x000DE2D5 File Offset: 0x000DC4D5
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006C28 RID: 27688
			// (set) Token: 0x060098E9 RID: 39145 RVA: 0x000DE2ED File Offset: 0x000DC4ED
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006C29 RID: 27689
			// (set) Token: 0x060098EA RID: 39146 RVA: 0x000DE305 File Offset: 0x000DC505
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006C2A RID: 27690
			// (set) Token: 0x060098EB RID: 39147 RVA: 0x000DE31D File Offset: 0x000DC51D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006C2B RID: 27691
			// (set) Token: 0x060098EC RID: 39148 RVA: 0x000DE330 File Offset: 0x000DC530
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006C2C RID: 27692
			// (set) Token: 0x060098ED RID: 39149 RVA: 0x000DE348 File Offset: 0x000DC548
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006C2D RID: 27693
			// (set) Token: 0x060098EE RID: 39150 RVA: 0x000DE360 File Offset: 0x000DC560
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006C2E RID: 27694
			// (set) Token: 0x060098EF RID: 39151 RVA: 0x000DE378 File Offset: 0x000DC578
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C3C RID: 3132
		public class ServerSetParameters : ParametersBase
		{
			// Token: 0x17006C2F RID: 27695
			// (set) Token: 0x060098F1 RID: 39153 RVA: 0x000DE398 File Offset: 0x000DC598
			public virtual ServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17006C30 RID: 27696
			// (set) Token: 0x060098F2 RID: 39154 RVA: 0x000DE3AB File Offset: 0x000DC5AB
			public virtual SwitchParameter IncludeMembers
			{
				set
				{
					base.PowerSharpParameters["IncludeMembers"] = value;
				}
			}

			// Token: 0x17006C31 RID: 27697
			// (set) Token: 0x060098F3 RID: 39155 RVA: 0x000DE3C3 File Offset: 0x000DC5C3
			public virtual SwitchParameter IncludePermissionsVersion
			{
				set
				{
					base.PowerSharpParameters["IncludePermissionsVersion"] = value;
				}
			}

			// Token: 0x17006C32 RID: 27698
			// (set) Token: 0x060098F4 RID: 39156 RVA: 0x000DE3DB File Offset: 0x000DC5DB
			public virtual SwitchParameter IncludeMemberSyncStatus
			{
				set
				{
					base.PowerSharpParameters["IncludeMemberSyncStatus"] = value;
				}
			}

			// Token: 0x17006C33 RID: 27699
			// (set) Token: 0x060098F5 RID: 39157 RVA: 0x000DE3F3 File Offset: 0x000DC5F3
			public virtual SwitchParameter IncludeMailboxUrls
			{
				set
				{
					base.PowerSharpParameters["IncludeMailboxUrls"] = value;
				}
			}

			// Token: 0x17006C34 RID: 27700
			// (set) Token: 0x060098F6 RID: 39158 RVA: 0x000DE40B File Offset: 0x000DC60B
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17006C35 RID: 27701
			// (set) Token: 0x060098F7 RID: 39159 RVA: 0x000DE423 File Offset: 0x000DC623
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17006C36 RID: 27702
			// (set) Token: 0x060098F8 RID: 39160 RVA: 0x000DE43B File Offset: 0x000DC63B
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006C37 RID: 27703
			// (set) Token: 0x060098F9 RID: 39161 RVA: 0x000DE453 File Offset: 0x000DC653
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006C38 RID: 27704
			// (set) Token: 0x060098FA RID: 39162 RVA: 0x000DE466 File Offset: 0x000DC666
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006C39 RID: 27705
			// (set) Token: 0x060098FB RID: 39163 RVA: 0x000DE484 File Offset: 0x000DC684
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006C3A RID: 27706
			// (set) Token: 0x060098FC RID: 39164 RVA: 0x000DE497 File Offset: 0x000DC697
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006C3B RID: 27707
			// (set) Token: 0x060098FD RID: 39165 RVA: 0x000DE4AA File Offset: 0x000DC6AA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006C3C RID: 27708
			// (set) Token: 0x060098FE RID: 39166 RVA: 0x000DE4C8 File Offset: 0x000DC6C8
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006C3D RID: 27709
			// (set) Token: 0x060098FF RID: 39167 RVA: 0x000DE4E0 File Offset: 0x000DC6E0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006C3E RID: 27710
			// (set) Token: 0x06009900 RID: 39168 RVA: 0x000DE4F8 File Offset: 0x000DC6F8
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006C3F RID: 27711
			// (set) Token: 0x06009901 RID: 39169 RVA: 0x000DE510 File Offset: 0x000DC710
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006C40 RID: 27712
			// (set) Token: 0x06009902 RID: 39170 RVA: 0x000DE523 File Offset: 0x000DC723
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006C41 RID: 27713
			// (set) Token: 0x06009903 RID: 39171 RVA: 0x000DE53B File Offset: 0x000DC73B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006C42 RID: 27714
			// (set) Token: 0x06009904 RID: 39172 RVA: 0x000DE553 File Offset: 0x000DC753
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006C43 RID: 27715
			// (set) Token: 0x06009905 RID: 39173 RVA: 0x000DE56B File Offset: 0x000DC76B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C3D RID: 3133
		public class DatabaseSetParameters : ParametersBase
		{
			// Token: 0x17006C44 RID: 27716
			// (set) Token: 0x06009907 RID: 39175 RVA: 0x000DE58B File Offset: 0x000DC78B
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17006C45 RID: 27717
			// (set) Token: 0x06009908 RID: 39176 RVA: 0x000DE59E File Offset: 0x000DC79E
			public virtual SwitchParameter IncludeMembers
			{
				set
				{
					base.PowerSharpParameters["IncludeMembers"] = value;
				}
			}

			// Token: 0x17006C46 RID: 27718
			// (set) Token: 0x06009909 RID: 39177 RVA: 0x000DE5B6 File Offset: 0x000DC7B6
			public virtual SwitchParameter IncludePermissionsVersion
			{
				set
				{
					base.PowerSharpParameters["IncludePermissionsVersion"] = value;
				}
			}

			// Token: 0x17006C47 RID: 27719
			// (set) Token: 0x0600990A RID: 39178 RVA: 0x000DE5CE File Offset: 0x000DC7CE
			public virtual SwitchParameter IncludeMemberSyncStatus
			{
				set
				{
					base.PowerSharpParameters["IncludeMemberSyncStatus"] = value;
				}
			}

			// Token: 0x17006C48 RID: 27720
			// (set) Token: 0x0600990B RID: 39179 RVA: 0x000DE5E6 File Offset: 0x000DC7E6
			public virtual SwitchParameter IncludeMailboxUrls
			{
				set
				{
					base.PowerSharpParameters["IncludeMailboxUrls"] = value;
				}
			}

			// Token: 0x17006C49 RID: 27721
			// (set) Token: 0x0600990C RID: 39180 RVA: 0x000DE5FE File Offset: 0x000DC7FE
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17006C4A RID: 27722
			// (set) Token: 0x0600990D RID: 39181 RVA: 0x000DE616 File Offset: 0x000DC816
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17006C4B RID: 27723
			// (set) Token: 0x0600990E RID: 39182 RVA: 0x000DE62E File Offset: 0x000DC82E
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006C4C RID: 27724
			// (set) Token: 0x0600990F RID: 39183 RVA: 0x000DE646 File Offset: 0x000DC846
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006C4D RID: 27725
			// (set) Token: 0x06009910 RID: 39184 RVA: 0x000DE659 File Offset: 0x000DC859
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006C4E RID: 27726
			// (set) Token: 0x06009911 RID: 39185 RVA: 0x000DE677 File Offset: 0x000DC877
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006C4F RID: 27727
			// (set) Token: 0x06009912 RID: 39186 RVA: 0x000DE68A File Offset: 0x000DC88A
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006C50 RID: 27728
			// (set) Token: 0x06009913 RID: 39187 RVA: 0x000DE69D File Offset: 0x000DC89D
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006C51 RID: 27729
			// (set) Token: 0x06009914 RID: 39188 RVA: 0x000DE6BB File Offset: 0x000DC8BB
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006C52 RID: 27730
			// (set) Token: 0x06009915 RID: 39189 RVA: 0x000DE6D3 File Offset: 0x000DC8D3
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006C53 RID: 27731
			// (set) Token: 0x06009916 RID: 39190 RVA: 0x000DE6EB File Offset: 0x000DC8EB
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006C54 RID: 27732
			// (set) Token: 0x06009917 RID: 39191 RVA: 0x000DE703 File Offset: 0x000DC903
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006C55 RID: 27733
			// (set) Token: 0x06009918 RID: 39192 RVA: 0x000DE716 File Offset: 0x000DC916
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006C56 RID: 27734
			// (set) Token: 0x06009919 RID: 39193 RVA: 0x000DE72E File Offset: 0x000DC92E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006C57 RID: 27735
			// (set) Token: 0x0600991A RID: 39194 RVA: 0x000DE746 File Offset: 0x000DC946
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006C58 RID: 27736
			// (set) Token: 0x0600991B RID: 39195 RVA: 0x000DE75E File Offset: 0x000DC95E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000C3E RID: 3134
		public class AnrSetParameters : ParametersBase
		{
			// Token: 0x17006C59 RID: 27737
			// (set) Token: 0x0600991D RID: 39197 RVA: 0x000DE77E File Offset: 0x000DC97E
			public virtual string Anr
			{
				set
				{
					base.PowerSharpParameters["Anr"] = value;
				}
			}

			// Token: 0x17006C5A RID: 27738
			// (set) Token: 0x0600991E RID: 39198 RVA: 0x000DE791 File Offset: 0x000DC991
			public virtual SwitchParameter IncludeMembers
			{
				set
				{
					base.PowerSharpParameters["IncludeMembers"] = value;
				}
			}

			// Token: 0x17006C5B RID: 27739
			// (set) Token: 0x0600991F RID: 39199 RVA: 0x000DE7A9 File Offset: 0x000DC9A9
			public virtual SwitchParameter IncludePermissionsVersion
			{
				set
				{
					base.PowerSharpParameters["IncludePermissionsVersion"] = value;
				}
			}

			// Token: 0x17006C5C RID: 27740
			// (set) Token: 0x06009920 RID: 39200 RVA: 0x000DE7C1 File Offset: 0x000DC9C1
			public virtual SwitchParameter IncludeMemberSyncStatus
			{
				set
				{
					base.PowerSharpParameters["IncludeMemberSyncStatus"] = value;
				}
			}

			// Token: 0x17006C5D RID: 27741
			// (set) Token: 0x06009921 RID: 39201 RVA: 0x000DE7D9 File Offset: 0x000DC9D9
			public virtual SwitchParameter IncludeMailboxUrls
			{
				set
				{
					base.PowerSharpParameters["IncludeMailboxUrls"] = value;
				}
			}

			// Token: 0x17006C5E RID: 27742
			// (set) Token: 0x06009922 RID: 39202 RVA: 0x000DE7F1 File Offset: 0x000DC9F1
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17006C5F RID: 27743
			// (set) Token: 0x06009923 RID: 39203 RVA: 0x000DE809 File Offset: 0x000DCA09
			public virtual SwitchParameter IncludeInactiveMailbox
			{
				set
				{
					base.PowerSharpParameters["IncludeInactiveMailbox"] = value;
				}
			}

			// Token: 0x17006C60 RID: 27744
			// (set) Token: 0x06009924 RID: 39204 RVA: 0x000DE821 File Offset: 0x000DCA21
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x17006C61 RID: 27745
			// (set) Token: 0x06009925 RID: 39205 RVA: 0x000DE839 File Offset: 0x000DCA39
			public virtual string Filter
			{
				set
				{
					base.PowerSharpParameters["Filter"] = value;
				}
			}

			// Token: 0x17006C62 RID: 27746
			// (set) Token: 0x06009926 RID: 39206 RVA: 0x000DE84C File Offset: 0x000DCA4C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17006C63 RID: 27747
			// (set) Token: 0x06009927 RID: 39207 RVA: 0x000DE86A File Offset: 0x000DCA6A
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17006C64 RID: 27748
			// (set) Token: 0x06009928 RID: 39208 RVA: 0x000DE87D File Offset: 0x000DCA7D
			public virtual string SortBy
			{
				set
				{
					base.PowerSharpParameters["SortBy"] = value;
				}
			}

			// Token: 0x17006C65 RID: 27749
			// (set) Token: 0x06009929 RID: 39209 RVA: 0x000DE890 File Offset: 0x000DCA90
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17006C66 RID: 27750
			// (set) Token: 0x0600992A RID: 39210 RVA: 0x000DE8AE File Offset: 0x000DCAAE
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006C67 RID: 27751
			// (set) Token: 0x0600992B RID: 39211 RVA: 0x000DE8C6 File Offset: 0x000DCAC6
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17006C68 RID: 27752
			// (set) Token: 0x0600992C RID: 39212 RVA: 0x000DE8DE File Offset: 0x000DCADE
			public virtual SwitchParameter ReadFromDomainController
			{
				set
				{
					base.PowerSharpParameters["ReadFromDomainController"] = value;
				}
			}

			// Token: 0x17006C69 RID: 27753
			// (set) Token: 0x0600992D RID: 39213 RVA: 0x000DE8F6 File Offset: 0x000DCAF6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006C6A RID: 27754
			// (set) Token: 0x0600992E RID: 39214 RVA: 0x000DE909 File Offset: 0x000DCB09
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006C6B RID: 27755
			// (set) Token: 0x0600992F RID: 39215 RVA: 0x000DE921 File Offset: 0x000DCB21
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006C6C RID: 27756
			// (set) Token: 0x06009930 RID: 39216 RVA: 0x000DE939 File Offset: 0x000DCB39
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006C6D RID: 27757
			// (set) Token: 0x06009931 RID: 39217 RVA: 0x000DE951 File Offset: 0x000DCB51
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
