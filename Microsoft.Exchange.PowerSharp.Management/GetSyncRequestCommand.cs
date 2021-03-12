using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000AB1 RID: 2737
	public class GetSyncRequestCommand : SyntheticCommandWithPipelineInput<SyncRequest, SyncRequest>
	{
		// Token: 0x06008747 RID: 34631 RVA: 0x000C75B3 File Offset: 0x000C57B3
		private GetSyncRequestCommand() : base("Get-SyncRequest")
		{
		}

		// Token: 0x06008748 RID: 34632 RVA: 0x000C75C0 File Offset: 0x000C57C0
		public GetSyncRequestCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06008749 RID: 34633 RVA: 0x000C75CF File Offset: 0x000C57CF
		public virtual GetSyncRequestCommand SetParameters(GetSyncRequestCommand.FilteringParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600874A RID: 34634 RVA: 0x000C75D9 File Offset: 0x000C57D9
		public virtual GetSyncRequestCommand SetParameters(GetSyncRequestCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600874B RID: 34635 RVA: 0x000C75E3 File Offset: 0x000C57E3
		public virtual GetSyncRequestCommand SetParameters(GetSyncRequestCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000AB2 RID: 2738
		public class FilteringParameters : ParametersBase
		{
			// Token: 0x17005D9E RID: 23966
			// (set) Token: 0x0600874C RID: 34636 RVA: 0x000C75ED File Offset: 0x000C57ED
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxOrMailUserIdParameter(value) : null);
				}
			}

			// Token: 0x17005D9F RID: 23967
			// (set) Token: 0x0600874D RID: 34637 RVA: 0x000C760B File Offset: 0x000C580B
			public virtual RequestStatus Status
			{
				set
				{
					base.PowerSharpParameters["Status"] = value;
				}
			}

			// Token: 0x17005DA0 RID: 23968
			// (set) Token: 0x0600874E RID: 34638 RVA: 0x000C7623 File Offset: 0x000C5823
			public virtual string BatchName
			{
				set
				{
					base.PowerSharpParameters["BatchName"] = value;
				}
			}

			// Token: 0x17005DA1 RID: 23969
			// (set) Token: 0x0600874F RID: 34639 RVA: 0x000C7636 File Offset: 0x000C5836
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17005DA2 RID: 23970
			// (set) Token: 0x06008750 RID: 34640 RVA: 0x000C7649 File Offset: 0x000C5849
			public virtual bool Suspend
			{
				set
				{
					base.PowerSharpParameters["Suspend"] = value;
				}
			}

			// Token: 0x17005DA3 RID: 23971
			// (set) Token: 0x06008751 RID: 34641 RVA: 0x000C7661 File Offset: 0x000C5861
			public virtual bool HighPriority
			{
				set
				{
					base.PowerSharpParameters["HighPriority"] = value;
				}
			}

			// Token: 0x17005DA4 RID: 23972
			// (set) Token: 0x06008752 RID: 34642 RVA: 0x000C7679 File Offset: 0x000C5879
			public virtual DatabaseIdParameter RequestQueue
			{
				set
				{
					base.PowerSharpParameters["RequestQueue"] = value;
				}
			}

			// Token: 0x17005DA5 RID: 23973
			// (set) Token: 0x06008753 RID: 34643 RVA: 0x000C768C File Offset: 0x000C588C
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005DA6 RID: 23974
			// (set) Token: 0x06008754 RID: 34644 RVA: 0x000C76AA File Offset: 0x000C58AA
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005DA7 RID: 23975
			// (set) Token: 0x06008755 RID: 34645 RVA: 0x000C76BD File Offset: 0x000C58BD
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005DA8 RID: 23976
			// (set) Token: 0x06008756 RID: 34646 RVA: 0x000C76D0 File Offset: 0x000C58D0
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005DA9 RID: 23977
			// (set) Token: 0x06008757 RID: 34647 RVA: 0x000C76E8 File Offset: 0x000C58E8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005DAA RID: 23978
			// (set) Token: 0x06008758 RID: 34648 RVA: 0x000C7700 File Offset: 0x000C5900
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005DAB RID: 23979
			// (set) Token: 0x06008759 RID: 34649 RVA: 0x000C7718 File Offset: 0x000C5918
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005DAC RID: 23980
			// (set) Token: 0x0600875A RID: 34650 RVA: 0x000C7730 File Offset: 0x000C5930
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AB3 RID: 2739
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17005DAD RID: 23981
			// (set) Token: 0x0600875C RID: 34652 RVA: 0x000C7750 File Offset: 0x000C5950
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17005DAE RID: 23982
			// (set) Token: 0x0600875D RID: 34653 RVA: 0x000C776E File Offset: 0x000C596E
			public virtual AccountPartitionIdParameter AccountPartition
			{
				set
				{
					base.PowerSharpParameters["AccountPartition"] = value;
				}
			}

			// Token: 0x17005DAF RID: 23983
			// (set) Token: 0x0600875E RID: 34654 RVA: 0x000C7781 File Offset: 0x000C5981
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new SyncRequestIdParameter(value) : null);
				}
			}

			// Token: 0x17005DB0 RID: 23984
			// (set) Token: 0x0600875F RID: 34655 RVA: 0x000C779F File Offset: 0x000C599F
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005DB1 RID: 23985
			// (set) Token: 0x06008760 RID: 34656 RVA: 0x000C77B2 File Offset: 0x000C59B2
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005DB2 RID: 23986
			// (set) Token: 0x06008761 RID: 34657 RVA: 0x000C77CA File Offset: 0x000C59CA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005DB3 RID: 23987
			// (set) Token: 0x06008762 RID: 34658 RVA: 0x000C77E2 File Offset: 0x000C59E2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005DB4 RID: 23988
			// (set) Token: 0x06008763 RID: 34659 RVA: 0x000C77FA File Offset: 0x000C59FA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005DB5 RID: 23989
			// (set) Token: 0x06008764 RID: 34660 RVA: 0x000C7812 File Offset: 0x000C5A12
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000AB4 RID: 2740
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17005DB6 RID: 23990
			// (set) Token: 0x06008766 RID: 34662 RVA: 0x000C7832 File Offset: 0x000C5A32
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17005DB7 RID: 23991
			// (set) Token: 0x06008767 RID: 34663 RVA: 0x000C7845 File Offset: 0x000C5A45
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17005DB8 RID: 23992
			// (set) Token: 0x06008768 RID: 34664 RVA: 0x000C785D File Offset: 0x000C5A5D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17005DB9 RID: 23993
			// (set) Token: 0x06008769 RID: 34665 RVA: 0x000C7875 File Offset: 0x000C5A75
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17005DBA RID: 23994
			// (set) Token: 0x0600876A RID: 34666 RVA: 0x000C788D File Offset: 0x000C5A8D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17005DBB RID: 23995
			// (set) Token: 0x0600876B RID: 34667 RVA: 0x000C78A5 File Offset: 0x000C5AA5
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
