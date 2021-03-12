using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200031E RID: 798
	public class GetManagementRoleAssignmentCommand : SyntheticCommandWithPipelineInput<ExchangeRoleAssignment, ExchangeRoleAssignment>
	{
		// Token: 0x06003441 RID: 13377 RVA: 0x0005B96B File Offset: 0x00059B6B
		private GetManagementRoleAssignmentCommand() : base("Get-ManagementRoleAssignment")
		{
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x0005B978 File Offset: 0x00059B78
		public GetManagementRoleAssignmentCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x0005B987 File Offset: 0x00059B87
		public virtual GetManagementRoleAssignmentCommand SetParameters(GetManagementRoleAssignmentCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x0005B991 File Offset: 0x00059B91
		public virtual GetManagementRoleAssignmentCommand SetParameters(GetManagementRoleAssignmentCommand.RoleAssigneeParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x0005B99B File Offset: 0x00059B9B
		public virtual GetManagementRoleAssignmentCommand SetParameters(GetManagementRoleAssignmentCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0200031F RID: 799
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x170019BE RID: 6590
			// (set) Token: 0x06003446 RID: 13382 RVA: 0x0005B9A5 File Offset: 0x00059BA5
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170019BF RID: 6591
			// (set) Token: 0x06003447 RID: 13383 RVA: 0x0005B9BD File Offset: 0x00059BBD
			public virtual RoleAssigneeType RoleAssigneeType
			{
				set
				{
					base.PowerSharpParameters["RoleAssigneeType"] = value;
				}
			}

			// Token: 0x170019C0 RID: 6592
			// (set) Token: 0x06003448 RID: 13384 RVA: 0x0005B9D5 File Offset: 0x00059BD5
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170019C1 RID: 6593
			// (set) Token: 0x06003449 RID: 13385 RVA: 0x0005B9ED File Offset: 0x00059BED
			public virtual bool Delegating
			{
				set
				{
					base.PowerSharpParameters["Delegating"] = value;
				}
			}

			// Token: 0x170019C2 RID: 6594
			// (set) Token: 0x0600344A RID: 13386 RVA: 0x0005BA05 File Offset: 0x00059C05
			public virtual bool Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x170019C3 RID: 6595
			// (set) Token: 0x0600344B RID: 13387 RVA: 0x0005BA1D File Offset: 0x00059C1D
			public virtual RecipientWriteScopeType RecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019C4 RID: 6596
			// (set) Token: 0x0600344C RID: 13388 RVA: 0x0005BA35 File Offset: 0x00059C35
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019C5 RID: 6597
			// (set) Token: 0x0600344D RID: 13389 RVA: 0x0005BA48 File Offset: 0x00059C48
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170019C6 RID: 6598
			// (set) Token: 0x0600344E RID: 13390 RVA: 0x0005BA66 File Offset: 0x00059C66
			public virtual ConfigWriteScopeType ConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019C7 RID: 6599
			// (set) Token: 0x0600344F RID: 13391 RVA: 0x0005BA7E File Offset: 0x00059C7E
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019C8 RID: 6600
			// (set) Token: 0x06003450 RID: 13392 RVA: 0x0005BA91 File Offset: 0x00059C91
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019C9 RID: 6601
			// (set) Token: 0x06003451 RID: 13393 RVA: 0x0005BAA4 File Offset: 0x00059CA4
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019CA RID: 6602
			// (set) Token: 0x06003452 RID: 13394 RVA: 0x0005BAB7 File Offset: 0x00059CB7
			public virtual SwitchParameter GetEffectiveUsers
			{
				set
				{
					base.PowerSharpParameters["GetEffectiveUsers"] = value;
				}
			}

			// Token: 0x170019CB RID: 6603
			// (set) Token: 0x06003453 RID: 13395 RVA: 0x0005BACF File Offset: 0x00059CCF
			public virtual string WritableRecipient
			{
				set
				{
					base.PowerSharpParameters["WritableRecipient"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170019CC RID: 6604
			// (set) Token: 0x06003454 RID: 13396 RVA: 0x0005BAED File Offset: 0x00059CED
			public virtual ServerIdParameter WritableServer
			{
				set
				{
					base.PowerSharpParameters["WritableServer"] = value;
				}
			}

			// Token: 0x170019CD RID: 6605
			// (set) Token: 0x06003455 RID: 13397 RVA: 0x0005BB00 File Offset: 0x00059D00
			public virtual DatabaseIdParameter WritableDatabase
			{
				set
				{
					base.PowerSharpParameters["WritableDatabase"] = value;
				}
			}

			// Token: 0x170019CE RID: 6606
			// (set) Token: 0x06003456 RID: 13398 RVA: 0x0005BB13 File Offset: 0x00059D13
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170019CF RID: 6607
			// (set) Token: 0x06003457 RID: 13399 RVA: 0x0005BB31 File Offset: 0x00059D31
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170019D0 RID: 6608
			// (set) Token: 0x06003458 RID: 13400 RVA: 0x0005BB44 File Offset: 0x00059D44
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170019D1 RID: 6609
			// (set) Token: 0x06003459 RID: 13401 RVA: 0x0005BB5C File Offset: 0x00059D5C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170019D2 RID: 6610
			// (set) Token: 0x0600345A RID: 13402 RVA: 0x0005BB74 File Offset: 0x00059D74
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170019D3 RID: 6611
			// (set) Token: 0x0600345B RID: 13403 RVA: 0x0005BB8C File Offset: 0x00059D8C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000320 RID: 800
		public class RoleAssigneeParameters : ParametersBase
		{
			// Token: 0x170019D4 RID: 6612
			// (set) Token: 0x0600345D RID: 13405 RVA: 0x0005BBAC File Offset: 0x00059DAC
			public virtual string Role
			{
				set
				{
					base.PowerSharpParameters["Role"] = ((value != null) ? new RoleIdParameter(value) : null);
				}
			}

			// Token: 0x170019D5 RID: 6613
			// (set) Token: 0x0600345E RID: 13406 RVA: 0x0005BBCA File Offset: 0x00059DCA
			public virtual string RoleAssignee
			{
				set
				{
					base.PowerSharpParameters["RoleAssignee"] = ((value != null) ? new RoleAssigneeIdParameter(value) : null);
				}
			}

			// Token: 0x170019D6 RID: 6614
			// (set) Token: 0x0600345F RID: 13407 RVA: 0x0005BBE8 File Offset: 0x00059DE8
			public virtual AssignmentMethod AssignmentMethod
			{
				set
				{
					base.PowerSharpParameters["AssignmentMethod"] = value;
				}
			}

			// Token: 0x170019D7 RID: 6615
			// (set) Token: 0x06003460 RID: 13408 RVA: 0x0005BC00 File Offset: 0x00059E00
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170019D8 RID: 6616
			// (set) Token: 0x06003461 RID: 13409 RVA: 0x0005BC18 File Offset: 0x00059E18
			public virtual RoleAssigneeType RoleAssigneeType
			{
				set
				{
					base.PowerSharpParameters["RoleAssigneeType"] = value;
				}
			}

			// Token: 0x170019D9 RID: 6617
			// (set) Token: 0x06003462 RID: 13410 RVA: 0x0005BC30 File Offset: 0x00059E30
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170019DA RID: 6618
			// (set) Token: 0x06003463 RID: 13411 RVA: 0x0005BC48 File Offset: 0x00059E48
			public virtual bool Delegating
			{
				set
				{
					base.PowerSharpParameters["Delegating"] = value;
				}
			}

			// Token: 0x170019DB RID: 6619
			// (set) Token: 0x06003464 RID: 13412 RVA: 0x0005BC60 File Offset: 0x00059E60
			public virtual bool Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x170019DC RID: 6620
			// (set) Token: 0x06003465 RID: 13413 RVA: 0x0005BC78 File Offset: 0x00059E78
			public virtual RecipientWriteScopeType RecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019DD RID: 6621
			// (set) Token: 0x06003466 RID: 13414 RVA: 0x0005BC90 File Offset: 0x00059E90
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019DE RID: 6622
			// (set) Token: 0x06003467 RID: 13415 RVA: 0x0005BCA3 File Offset: 0x00059EA3
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170019DF RID: 6623
			// (set) Token: 0x06003468 RID: 13416 RVA: 0x0005BCC1 File Offset: 0x00059EC1
			public virtual ConfigWriteScopeType ConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019E0 RID: 6624
			// (set) Token: 0x06003469 RID: 13417 RVA: 0x0005BCD9 File Offset: 0x00059ED9
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019E1 RID: 6625
			// (set) Token: 0x0600346A RID: 13418 RVA: 0x0005BCEC File Offset: 0x00059EEC
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019E2 RID: 6626
			// (set) Token: 0x0600346B RID: 13419 RVA: 0x0005BCFF File Offset: 0x00059EFF
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019E3 RID: 6627
			// (set) Token: 0x0600346C RID: 13420 RVA: 0x0005BD12 File Offset: 0x00059F12
			public virtual SwitchParameter GetEffectiveUsers
			{
				set
				{
					base.PowerSharpParameters["GetEffectiveUsers"] = value;
				}
			}

			// Token: 0x170019E4 RID: 6628
			// (set) Token: 0x0600346D RID: 13421 RVA: 0x0005BD2A File Offset: 0x00059F2A
			public virtual string WritableRecipient
			{
				set
				{
					base.PowerSharpParameters["WritableRecipient"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170019E5 RID: 6629
			// (set) Token: 0x0600346E RID: 13422 RVA: 0x0005BD48 File Offset: 0x00059F48
			public virtual ServerIdParameter WritableServer
			{
				set
				{
					base.PowerSharpParameters["WritableServer"] = value;
				}
			}

			// Token: 0x170019E6 RID: 6630
			// (set) Token: 0x0600346F RID: 13423 RVA: 0x0005BD5B File Offset: 0x00059F5B
			public virtual DatabaseIdParameter WritableDatabase
			{
				set
				{
					base.PowerSharpParameters["WritableDatabase"] = value;
				}
			}

			// Token: 0x170019E7 RID: 6631
			// (set) Token: 0x06003470 RID: 13424 RVA: 0x0005BD6E File Offset: 0x00059F6E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170019E8 RID: 6632
			// (set) Token: 0x06003471 RID: 13425 RVA: 0x0005BD8C File Offset: 0x00059F8C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170019E9 RID: 6633
			// (set) Token: 0x06003472 RID: 13426 RVA: 0x0005BD9F File Offset: 0x00059F9F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170019EA RID: 6634
			// (set) Token: 0x06003473 RID: 13427 RVA: 0x0005BDB7 File Offset: 0x00059FB7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170019EB RID: 6635
			// (set) Token: 0x06003474 RID: 13428 RVA: 0x0005BDCF File Offset: 0x00059FCF
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170019EC RID: 6636
			// (set) Token: 0x06003475 RID: 13429 RVA: 0x0005BDE7 File Offset: 0x00059FE7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000321 RID: 801
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x170019ED RID: 6637
			// (set) Token: 0x06003477 RID: 13431 RVA: 0x0005BE07 File Offset: 0x0005A007
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RoleAssignmentIdParameter(value) : null);
				}
			}

			// Token: 0x170019EE RID: 6638
			// (set) Token: 0x06003478 RID: 13432 RVA: 0x0005BE25 File Offset: 0x0005A025
			public virtual SwitchParameter IgnoreDehydratedFlag
			{
				set
				{
					base.PowerSharpParameters["IgnoreDehydratedFlag"] = value;
				}
			}

			// Token: 0x170019EF RID: 6639
			// (set) Token: 0x06003479 RID: 13433 RVA: 0x0005BE3D File Offset: 0x0005A03D
			public virtual RoleAssigneeType RoleAssigneeType
			{
				set
				{
					base.PowerSharpParameters["RoleAssigneeType"] = value;
				}
			}

			// Token: 0x170019F0 RID: 6640
			// (set) Token: 0x0600347A RID: 13434 RVA: 0x0005BE55 File Offset: 0x0005A055
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x170019F1 RID: 6641
			// (set) Token: 0x0600347B RID: 13435 RVA: 0x0005BE6D File Offset: 0x0005A06D
			public virtual bool Delegating
			{
				set
				{
					base.PowerSharpParameters["Delegating"] = value;
				}
			}

			// Token: 0x170019F2 RID: 6642
			// (set) Token: 0x0600347C RID: 13436 RVA: 0x0005BE85 File Offset: 0x0005A085
			public virtual bool Exclusive
			{
				set
				{
					base.PowerSharpParameters["Exclusive"] = value;
				}
			}

			// Token: 0x170019F3 RID: 6643
			// (set) Token: 0x0600347D RID: 13437 RVA: 0x0005BE9D File Offset: 0x0005A09D
			public virtual RecipientWriteScopeType RecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["RecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019F4 RID: 6644
			// (set) Token: 0x0600347E RID: 13438 RVA: 0x0005BEB5 File Offset: 0x0005A0B5
			public virtual ManagementScopeIdParameter CustomRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomRecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019F5 RID: 6645
			// (set) Token: 0x0600347F RID: 13439 RVA: 0x0005BEC8 File Offset: 0x0005A0C8
			public virtual string RecipientOrganizationalUnitScope
			{
				set
				{
					base.PowerSharpParameters["RecipientOrganizationalUnitScope"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170019F6 RID: 6646
			// (set) Token: 0x06003480 RID: 13440 RVA: 0x0005BEE6 File Offset: 0x0005A0E6
			public virtual ConfigWriteScopeType ConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019F7 RID: 6647
			// (set) Token: 0x06003481 RID: 13441 RVA: 0x0005BEFE File Offset: 0x0005A0FE
			public virtual ManagementScopeIdParameter CustomConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["CustomConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019F8 RID: 6648
			// (set) Token: 0x06003482 RID: 13442 RVA: 0x0005BF11 File Offset: 0x0005A111
			public virtual ManagementScopeIdParameter ExclusiveRecipientWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveRecipientWriteScope"] = value;
				}
			}

			// Token: 0x170019F9 RID: 6649
			// (set) Token: 0x06003483 RID: 13443 RVA: 0x0005BF24 File Offset: 0x0005A124
			public virtual ManagementScopeIdParameter ExclusiveConfigWriteScope
			{
				set
				{
					base.PowerSharpParameters["ExclusiveConfigWriteScope"] = value;
				}
			}

			// Token: 0x170019FA RID: 6650
			// (set) Token: 0x06003484 RID: 13444 RVA: 0x0005BF37 File Offset: 0x0005A137
			public virtual SwitchParameter GetEffectiveUsers
			{
				set
				{
					base.PowerSharpParameters["GetEffectiveUsers"] = value;
				}
			}

			// Token: 0x170019FB RID: 6651
			// (set) Token: 0x06003485 RID: 13445 RVA: 0x0005BF4F File Offset: 0x0005A14F
			public virtual string WritableRecipient
			{
				set
				{
					base.PowerSharpParameters["WritableRecipient"] = ((value != null) ? new GeneralRecipientIdParameter(value) : null);
				}
			}

			// Token: 0x170019FC RID: 6652
			// (set) Token: 0x06003486 RID: 13446 RVA: 0x0005BF6D File Offset: 0x0005A16D
			public virtual ServerIdParameter WritableServer
			{
				set
				{
					base.PowerSharpParameters["WritableServer"] = value;
				}
			}

			// Token: 0x170019FD RID: 6653
			// (set) Token: 0x06003487 RID: 13447 RVA: 0x0005BF80 File Offset: 0x0005A180
			public virtual DatabaseIdParameter WritableDatabase
			{
				set
				{
					base.PowerSharpParameters["WritableDatabase"] = value;
				}
			}

			// Token: 0x170019FE RID: 6654
			// (set) Token: 0x06003488 RID: 13448 RVA: 0x0005BF93 File Offset: 0x0005A193
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170019FF RID: 6655
			// (set) Token: 0x06003489 RID: 13449 RVA: 0x0005BFB1 File Offset: 0x0005A1B1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001A00 RID: 6656
			// (set) Token: 0x0600348A RID: 13450 RVA: 0x0005BFC4 File Offset: 0x0005A1C4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001A01 RID: 6657
			// (set) Token: 0x0600348B RID: 13451 RVA: 0x0005BFDC File Offset: 0x0005A1DC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001A02 RID: 6658
			// (set) Token: 0x0600348C RID: 13452 RVA: 0x0005BFF4 File Offset: 0x0005A1F4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001A03 RID: 6659
			// (set) Token: 0x0600348D RID: 13453 RVA: 0x0005C00C File Offset: 0x0005A20C
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
