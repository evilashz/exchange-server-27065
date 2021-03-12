using System;
using System.Globalization;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000C98 RID: 3224
	public class NewMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<SwitchParameter>
	{
		// Token: 0x0600A293 RID: 41619 RVA: 0x000EB7D4 File Offset: 0x000E99D4
		private NewMailboxCommand() : base("New-Mailbox")
		{
		}

		// Token: 0x0600A294 RID: 41620 RVA: 0x000EB7E1 File Offset: 0x000E99E1
		public NewMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600A295 RID: 41621 RVA: 0x000EB7F0 File Offset: 0x000E99F0
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.RoomParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A296 RID: 41622 RVA: 0x000EB7FA File Offset: 0x000E99FA
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.LinkedRoomMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A297 RID: 41623 RVA: 0x000EB804 File Offset: 0x000E9A04
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A298 RID: 41624 RVA: 0x000EB80E File Offset: 0x000E9A0E
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.WindowsLiveCustomDomainsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A299 RID: 41625 RVA: 0x000EB818 File Offset: 0x000E9A18
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.ImportLiveIdParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A29A RID: 41626 RVA: 0x000EB822 File Offset: 0x000E9A22
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.RemovedMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A29B RID: 41627 RVA: 0x000EB82C File Offset: 0x000E9A2C
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.FederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A29C RID: 41628 RVA: 0x000EB836 File Offset: 0x000E9A36
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.MicrosoftOnlineServicesFederatedUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A29D RID: 41629 RVA: 0x000EB840 File Offset: 0x000E9A40
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.RemoteArchiveParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A29E RID: 41630 RVA: 0x000EB84A File Offset: 0x000E9A4A
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.MicrosoftOnlineServicesIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A29F RID: 41631 RVA: 0x000EB854 File Offset: 0x000E9A54
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.MailboxPlanParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A0 RID: 41632 RVA: 0x000EB85E File Offset: 0x000E9A5E
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.DisabledUserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A1 RID: 41633 RVA: 0x000EB868 File Offset: 0x000E9A68
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.UserParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A2 RID: 41634 RVA: 0x000EB872 File Offset: 0x000E9A72
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.WindowsLiveIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A3 RID: 41635 RVA: 0x000EB87C File Offset: 0x000E9A7C
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.DiscoveryParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A4 RID: 41636 RVA: 0x000EB886 File Offset: 0x000E9A86
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.TeamMailboxIWParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A5 RID: 41637 RVA: 0x000EB890 File Offset: 0x000E9A90
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.ArbitrationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A6 RID: 41638 RVA: 0x000EB89A File Offset: 0x000E9A9A
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.EquipmentParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A7 RID: 41639 RVA: 0x000EB8A4 File Offset: 0x000E9AA4
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.AuxMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A8 RID: 41640 RVA: 0x000EB8AE File Offset: 0x000E9AAE
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.TeamMailboxITProParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2A9 RID: 41641 RVA: 0x000EB8B8 File Offset: 0x000E9AB8
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.LinkedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2AA RID: 41642 RVA: 0x000EB8C2 File Offset: 0x000E9AC2
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.SharedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2AB RID: 41643 RVA: 0x000EB8CC File Offset: 0x000E9ACC
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.LinkedWithSyncMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2AC RID: 41644 RVA: 0x000EB8D6 File Offset: 0x000E9AD6
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.EnableRoomMailboxAccountParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2AD RID: 41645 RVA: 0x000EB8E0 File Offset: 0x000E9AE0
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.AuditLogParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2AE RID: 41646 RVA: 0x000EB8EA File Offset: 0x000E9AEA
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.GroupMailboxParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600A2AF RID: 41647 RVA: 0x000EB8F4 File Offset: 0x000E9AF4
		public virtual NewMailboxCommand SetParameters(NewMailboxCommand.PublicFolderParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000C99 RID: 3225
		public class RoomParameters : ParametersBase
		{
			// Token: 0x1700751C RID: 29980
			// (set) Token: 0x0600A2B0 RID: 41648 RVA: 0x000EB8FE File Offset: 0x000E9AFE
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700751D RID: 29981
			// (set) Token: 0x0600A2B1 RID: 41649 RVA: 0x000EB911 File Offset: 0x000E9B11
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x1700751E RID: 29982
			// (set) Token: 0x0600A2B2 RID: 41650 RVA: 0x000EB924 File Offset: 0x000E9B24
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x1700751F RID: 29983
			// (set) Token: 0x0600A2B3 RID: 41651 RVA: 0x000EB93C File Offset: 0x000E9B3C
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007520 RID: 29984
			// (set) Token: 0x0600A2B4 RID: 41652 RVA: 0x000EB94F File Offset: 0x000E9B4F
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007521 RID: 29985
			// (set) Token: 0x0600A2B5 RID: 41653 RVA: 0x000EB962 File Offset: 0x000E9B62
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x17007522 RID: 29986
			// (set) Token: 0x0600A2B6 RID: 41654 RVA: 0x000EB97A File Offset: 0x000E9B7A
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007523 RID: 29987
			// (set) Token: 0x0600A2B7 RID: 41655 RVA: 0x000EB998 File Offset: 0x000E9B98
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007524 RID: 29988
			// (set) Token: 0x0600A2B8 RID: 41656 RVA: 0x000EB9AB File Offset: 0x000E9BAB
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007525 RID: 29989
			// (set) Token: 0x0600A2B9 RID: 41657 RVA: 0x000EB9C3 File Offset: 0x000E9BC3
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007526 RID: 29990
			// (set) Token: 0x0600A2BA RID: 41658 RVA: 0x000EB9DB File Offset: 0x000E9BDB
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007527 RID: 29991
			// (set) Token: 0x0600A2BB RID: 41659 RVA: 0x000EB9EE File Offset: 0x000E9BEE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007528 RID: 29992
			// (set) Token: 0x0600A2BC RID: 41660 RVA: 0x000EBA01 File Offset: 0x000E9C01
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007529 RID: 29993
			// (set) Token: 0x0600A2BD RID: 41661 RVA: 0x000EBA19 File Offset: 0x000E9C19
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700752A RID: 29994
			// (set) Token: 0x0600A2BE RID: 41662 RVA: 0x000EBA2C File Offset: 0x000E9C2C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700752B RID: 29995
			// (set) Token: 0x0600A2BF RID: 41663 RVA: 0x000EBA3F File Offset: 0x000E9C3F
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700752C RID: 29996
			// (set) Token: 0x0600A2C0 RID: 41664 RVA: 0x000EBA52 File Offset: 0x000E9C52
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700752D RID: 29997
			// (set) Token: 0x0600A2C1 RID: 41665 RVA: 0x000EBA70 File Offset: 0x000E9C70
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700752E RID: 29998
			// (set) Token: 0x0600A2C2 RID: 41666 RVA: 0x000EBA8E File Offset: 0x000E9C8E
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700752F RID: 29999
			// (set) Token: 0x0600A2C3 RID: 41667 RVA: 0x000EBAAC File Offset: 0x000E9CAC
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007530 RID: 30000
			// (set) Token: 0x0600A2C4 RID: 41668 RVA: 0x000EBACA File Offset: 0x000E9CCA
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007531 RID: 30001
			// (set) Token: 0x0600A2C5 RID: 41669 RVA: 0x000EBAE8 File Offset: 0x000E9CE8
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007532 RID: 30002
			// (set) Token: 0x0600A2C6 RID: 41670 RVA: 0x000EBAFB File Offset: 0x000E9CFB
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007533 RID: 30003
			// (set) Token: 0x0600A2C7 RID: 41671 RVA: 0x000EBB13 File Offset: 0x000E9D13
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007534 RID: 30004
			// (set) Token: 0x0600A2C8 RID: 41672 RVA: 0x000EBB31 File Offset: 0x000E9D31
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007535 RID: 30005
			// (set) Token: 0x0600A2C9 RID: 41673 RVA: 0x000EBB49 File Offset: 0x000E9D49
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007536 RID: 30006
			// (set) Token: 0x0600A2CA RID: 41674 RVA: 0x000EBB61 File Offset: 0x000E9D61
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007537 RID: 30007
			// (set) Token: 0x0600A2CB RID: 41675 RVA: 0x000EBB74 File Offset: 0x000E9D74
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007538 RID: 30008
			// (set) Token: 0x0600A2CC RID: 41676 RVA: 0x000EBB8C File Offset: 0x000E9D8C
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007539 RID: 30009
			// (set) Token: 0x0600A2CD RID: 41677 RVA: 0x000EBBA4 File Offset: 0x000E9DA4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700753A RID: 30010
			// (set) Token: 0x0600A2CE RID: 41678 RVA: 0x000EBBBC File Offset: 0x000E9DBC
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700753B RID: 30011
			// (set) Token: 0x0600A2CF RID: 41679 RVA: 0x000EBBCF File Offset: 0x000E9DCF
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700753C RID: 30012
			// (set) Token: 0x0600A2D0 RID: 41680 RVA: 0x000EBBE2 File Offset: 0x000E9DE2
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700753D RID: 30013
			// (set) Token: 0x0600A2D1 RID: 41681 RVA: 0x000EBBF5 File Offset: 0x000E9DF5
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700753E RID: 30014
			// (set) Token: 0x0600A2D2 RID: 41682 RVA: 0x000EBC08 File Offset: 0x000E9E08
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700753F RID: 30015
			// (set) Token: 0x0600A2D3 RID: 41683 RVA: 0x000EBC20 File Offset: 0x000E9E20
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007540 RID: 30016
			// (set) Token: 0x0600A2D4 RID: 41684 RVA: 0x000EBC33 File Offset: 0x000E9E33
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007541 RID: 30017
			// (set) Token: 0x0600A2D5 RID: 41685 RVA: 0x000EBC46 File Offset: 0x000E9E46
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007542 RID: 30018
			// (set) Token: 0x0600A2D6 RID: 41686 RVA: 0x000EBC5E File Offset: 0x000E9E5E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007543 RID: 30019
			// (set) Token: 0x0600A2D7 RID: 41687 RVA: 0x000EBC71 File Offset: 0x000E9E71
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007544 RID: 30020
			// (set) Token: 0x0600A2D8 RID: 41688 RVA: 0x000EBC89 File Offset: 0x000E9E89
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007545 RID: 30021
			// (set) Token: 0x0600A2D9 RID: 41689 RVA: 0x000EBC9C File Offset: 0x000E9E9C
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007546 RID: 30022
			// (set) Token: 0x0600A2DA RID: 41690 RVA: 0x000EBCBA File Offset: 0x000E9EBA
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007547 RID: 30023
			// (set) Token: 0x0600A2DB RID: 41691 RVA: 0x000EBCCD File Offset: 0x000E9ECD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007548 RID: 30024
			// (set) Token: 0x0600A2DC RID: 41692 RVA: 0x000EBCEB File Offset: 0x000E9EEB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007549 RID: 30025
			// (set) Token: 0x0600A2DD RID: 41693 RVA: 0x000EBCFE File Offset: 0x000E9EFE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700754A RID: 30026
			// (set) Token: 0x0600A2DE RID: 41694 RVA: 0x000EBD16 File Offset: 0x000E9F16
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700754B RID: 30027
			// (set) Token: 0x0600A2DF RID: 41695 RVA: 0x000EBD2E File Offset: 0x000E9F2E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700754C RID: 30028
			// (set) Token: 0x0600A2E0 RID: 41696 RVA: 0x000EBD46 File Offset: 0x000E9F46
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700754D RID: 30029
			// (set) Token: 0x0600A2E1 RID: 41697 RVA: 0x000EBD5E File Offset: 0x000E9F5E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C9A RID: 3226
		public class LinkedRoomMailboxParameters : ParametersBase
		{
			// Token: 0x1700754E RID: 30030
			// (set) Token: 0x0600A2E3 RID: 41699 RVA: 0x000EBD7E File Offset: 0x000E9F7E
			public virtual string Office
			{
				set
				{
					base.PowerSharpParameters["Office"] = value;
				}
			}

			// Token: 0x1700754F RID: 30031
			// (set) Token: 0x0600A2E4 RID: 41700 RVA: 0x000EBD91 File Offset: 0x000E9F91
			public virtual string Phone
			{
				set
				{
					base.PowerSharpParameters["Phone"] = value;
				}
			}

			// Token: 0x17007550 RID: 30032
			// (set) Token: 0x0600A2E5 RID: 41701 RVA: 0x000EBDA4 File Offset: 0x000E9FA4
			public virtual int? ResourceCapacity
			{
				set
				{
					base.PowerSharpParameters["ResourceCapacity"] = value;
				}
			}

			// Token: 0x17007551 RID: 30033
			// (set) Token: 0x0600A2E6 RID: 41702 RVA: 0x000EBDBC File Offset: 0x000E9FBC
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007552 RID: 30034
			// (set) Token: 0x0600A2E7 RID: 41703 RVA: 0x000EBDCF File Offset: 0x000E9FCF
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007553 RID: 30035
			// (set) Token: 0x0600A2E8 RID: 41704 RVA: 0x000EBDE2 File Offset: 0x000E9FE2
			public virtual SwitchParameter LinkedRoom
			{
				set
				{
					base.PowerSharpParameters["LinkedRoom"] = value;
				}
			}

			// Token: 0x17007554 RID: 30036
			// (set) Token: 0x0600A2E9 RID: 41705 RVA: 0x000EBDFA File Offset: 0x000E9FFA
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x17007555 RID: 30037
			// (set) Token: 0x0600A2EA RID: 41706 RVA: 0x000EBE18 File Offset: 0x000EA018
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x17007556 RID: 30038
			// (set) Token: 0x0600A2EB RID: 41707 RVA: 0x000EBE2B File Offset: 0x000EA02B
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x17007557 RID: 30039
			// (set) Token: 0x0600A2EC RID: 41708 RVA: 0x000EBE3E File Offset: 0x000EA03E
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007558 RID: 30040
			// (set) Token: 0x0600A2ED RID: 41709 RVA: 0x000EBE5C File Offset: 0x000EA05C
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007559 RID: 30041
			// (set) Token: 0x0600A2EE RID: 41710 RVA: 0x000EBE6F File Offset: 0x000EA06F
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700755A RID: 30042
			// (set) Token: 0x0600A2EF RID: 41711 RVA: 0x000EBE87 File Offset: 0x000EA087
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700755B RID: 30043
			// (set) Token: 0x0600A2F0 RID: 41712 RVA: 0x000EBE9F File Offset: 0x000EA09F
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x1700755C RID: 30044
			// (set) Token: 0x0600A2F1 RID: 41713 RVA: 0x000EBEB2 File Offset: 0x000EA0B2
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700755D RID: 30045
			// (set) Token: 0x0600A2F2 RID: 41714 RVA: 0x000EBEC5 File Offset: 0x000EA0C5
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700755E RID: 30046
			// (set) Token: 0x0600A2F3 RID: 41715 RVA: 0x000EBEDD File Offset: 0x000EA0DD
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700755F RID: 30047
			// (set) Token: 0x0600A2F4 RID: 41716 RVA: 0x000EBEF0 File Offset: 0x000EA0F0
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007560 RID: 30048
			// (set) Token: 0x0600A2F5 RID: 41717 RVA: 0x000EBF03 File Offset: 0x000EA103
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007561 RID: 30049
			// (set) Token: 0x0600A2F6 RID: 41718 RVA: 0x000EBF16 File Offset: 0x000EA116
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007562 RID: 30050
			// (set) Token: 0x0600A2F7 RID: 41719 RVA: 0x000EBF34 File Offset: 0x000EA134
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007563 RID: 30051
			// (set) Token: 0x0600A2F8 RID: 41720 RVA: 0x000EBF52 File Offset: 0x000EA152
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007564 RID: 30052
			// (set) Token: 0x0600A2F9 RID: 41721 RVA: 0x000EBF70 File Offset: 0x000EA170
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007565 RID: 30053
			// (set) Token: 0x0600A2FA RID: 41722 RVA: 0x000EBF8E File Offset: 0x000EA18E
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007566 RID: 30054
			// (set) Token: 0x0600A2FB RID: 41723 RVA: 0x000EBFAC File Offset: 0x000EA1AC
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007567 RID: 30055
			// (set) Token: 0x0600A2FC RID: 41724 RVA: 0x000EBFBF File Offset: 0x000EA1BF
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007568 RID: 30056
			// (set) Token: 0x0600A2FD RID: 41725 RVA: 0x000EBFD7 File Offset: 0x000EA1D7
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007569 RID: 30057
			// (set) Token: 0x0600A2FE RID: 41726 RVA: 0x000EBFF5 File Offset: 0x000EA1F5
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700756A RID: 30058
			// (set) Token: 0x0600A2FF RID: 41727 RVA: 0x000EC00D File Offset: 0x000EA20D
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700756B RID: 30059
			// (set) Token: 0x0600A300 RID: 41728 RVA: 0x000EC025 File Offset: 0x000EA225
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700756C RID: 30060
			// (set) Token: 0x0600A301 RID: 41729 RVA: 0x000EC038 File Offset: 0x000EA238
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x1700756D RID: 30061
			// (set) Token: 0x0600A302 RID: 41730 RVA: 0x000EC050 File Offset: 0x000EA250
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x1700756E RID: 30062
			// (set) Token: 0x0600A303 RID: 41731 RVA: 0x000EC068 File Offset: 0x000EA268
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700756F RID: 30063
			// (set) Token: 0x0600A304 RID: 41732 RVA: 0x000EC080 File Offset: 0x000EA280
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007570 RID: 30064
			// (set) Token: 0x0600A305 RID: 41733 RVA: 0x000EC093 File Offset: 0x000EA293
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007571 RID: 30065
			// (set) Token: 0x0600A306 RID: 41734 RVA: 0x000EC0A6 File Offset: 0x000EA2A6
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007572 RID: 30066
			// (set) Token: 0x0600A307 RID: 41735 RVA: 0x000EC0B9 File Offset: 0x000EA2B9
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007573 RID: 30067
			// (set) Token: 0x0600A308 RID: 41736 RVA: 0x000EC0CC File Offset: 0x000EA2CC
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007574 RID: 30068
			// (set) Token: 0x0600A309 RID: 41737 RVA: 0x000EC0E4 File Offset: 0x000EA2E4
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007575 RID: 30069
			// (set) Token: 0x0600A30A RID: 41738 RVA: 0x000EC0F7 File Offset: 0x000EA2F7
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007576 RID: 30070
			// (set) Token: 0x0600A30B RID: 41739 RVA: 0x000EC10A File Offset: 0x000EA30A
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007577 RID: 30071
			// (set) Token: 0x0600A30C RID: 41740 RVA: 0x000EC122 File Offset: 0x000EA322
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007578 RID: 30072
			// (set) Token: 0x0600A30D RID: 41741 RVA: 0x000EC135 File Offset: 0x000EA335
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007579 RID: 30073
			// (set) Token: 0x0600A30E RID: 41742 RVA: 0x000EC14D File Offset: 0x000EA34D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700757A RID: 30074
			// (set) Token: 0x0600A30F RID: 41743 RVA: 0x000EC160 File Offset: 0x000EA360
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700757B RID: 30075
			// (set) Token: 0x0600A310 RID: 41744 RVA: 0x000EC17E File Offset: 0x000EA37E
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700757C RID: 30076
			// (set) Token: 0x0600A311 RID: 41745 RVA: 0x000EC191 File Offset: 0x000EA391
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700757D RID: 30077
			// (set) Token: 0x0600A312 RID: 41746 RVA: 0x000EC1AF File Offset: 0x000EA3AF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700757E RID: 30078
			// (set) Token: 0x0600A313 RID: 41747 RVA: 0x000EC1C2 File Offset: 0x000EA3C2
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700757F RID: 30079
			// (set) Token: 0x0600A314 RID: 41748 RVA: 0x000EC1DA File Offset: 0x000EA3DA
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007580 RID: 30080
			// (set) Token: 0x0600A315 RID: 41749 RVA: 0x000EC1F2 File Offset: 0x000EA3F2
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007581 RID: 30081
			// (set) Token: 0x0600A316 RID: 41750 RVA: 0x000EC20A File Offset: 0x000EA40A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007582 RID: 30082
			// (set) Token: 0x0600A317 RID: 41751 RVA: 0x000EC222 File Offset: 0x000EA422
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C9B RID: 3227
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17007583 RID: 30083
			// (set) Token: 0x0600A319 RID: 41753 RVA: 0x000EC242 File Offset: 0x000EA442
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007584 RID: 30084
			// (set) Token: 0x0600A31A RID: 41754 RVA: 0x000EC255 File Offset: 0x000EA455
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007585 RID: 30085
			// (set) Token: 0x0600A31B RID: 41755 RVA: 0x000EC268 File Offset: 0x000EA468
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007586 RID: 30086
			// (set) Token: 0x0600A31C RID: 41756 RVA: 0x000EC280 File Offset: 0x000EA480
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007587 RID: 30087
			// (set) Token: 0x0600A31D RID: 41757 RVA: 0x000EC293 File Offset: 0x000EA493
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007588 RID: 30088
			// (set) Token: 0x0600A31E RID: 41758 RVA: 0x000EC2A6 File Offset: 0x000EA4A6
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007589 RID: 30089
			// (set) Token: 0x0600A31F RID: 41759 RVA: 0x000EC2B9 File Offset: 0x000EA4B9
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700758A RID: 30090
			// (set) Token: 0x0600A320 RID: 41760 RVA: 0x000EC2D7 File Offset: 0x000EA4D7
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700758B RID: 30091
			// (set) Token: 0x0600A321 RID: 41761 RVA: 0x000EC2F5 File Offset: 0x000EA4F5
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700758C RID: 30092
			// (set) Token: 0x0600A322 RID: 41762 RVA: 0x000EC313 File Offset: 0x000EA513
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700758D RID: 30093
			// (set) Token: 0x0600A323 RID: 41763 RVA: 0x000EC331 File Offset: 0x000EA531
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700758E RID: 30094
			// (set) Token: 0x0600A324 RID: 41764 RVA: 0x000EC34F File Offset: 0x000EA54F
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700758F RID: 30095
			// (set) Token: 0x0600A325 RID: 41765 RVA: 0x000EC362 File Offset: 0x000EA562
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007590 RID: 30096
			// (set) Token: 0x0600A326 RID: 41766 RVA: 0x000EC37A File Offset: 0x000EA57A
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007591 RID: 30097
			// (set) Token: 0x0600A327 RID: 41767 RVA: 0x000EC398 File Offset: 0x000EA598
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007592 RID: 30098
			// (set) Token: 0x0600A328 RID: 41768 RVA: 0x000EC3B0 File Offset: 0x000EA5B0
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007593 RID: 30099
			// (set) Token: 0x0600A329 RID: 41769 RVA: 0x000EC3C8 File Offset: 0x000EA5C8
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007594 RID: 30100
			// (set) Token: 0x0600A32A RID: 41770 RVA: 0x000EC3DB File Offset: 0x000EA5DB
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007595 RID: 30101
			// (set) Token: 0x0600A32B RID: 41771 RVA: 0x000EC3F3 File Offset: 0x000EA5F3
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007596 RID: 30102
			// (set) Token: 0x0600A32C RID: 41772 RVA: 0x000EC40B File Offset: 0x000EA60B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007597 RID: 30103
			// (set) Token: 0x0600A32D RID: 41773 RVA: 0x000EC423 File Offset: 0x000EA623
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007598 RID: 30104
			// (set) Token: 0x0600A32E RID: 41774 RVA: 0x000EC436 File Offset: 0x000EA636
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007599 RID: 30105
			// (set) Token: 0x0600A32F RID: 41775 RVA: 0x000EC449 File Offset: 0x000EA649
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700759A RID: 30106
			// (set) Token: 0x0600A330 RID: 41776 RVA: 0x000EC45C File Offset: 0x000EA65C
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700759B RID: 30107
			// (set) Token: 0x0600A331 RID: 41777 RVA: 0x000EC46F File Offset: 0x000EA66F
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700759C RID: 30108
			// (set) Token: 0x0600A332 RID: 41778 RVA: 0x000EC487 File Offset: 0x000EA687
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700759D RID: 30109
			// (set) Token: 0x0600A333 RID: 41779 RVA: 0x000EC49A File Offset: 0x000EA69A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700759E RID: 30110
			// (set) Token: 0x0600A334 RID: 41780 RVA: 0x000EC4AD File Offset: 0x000EA6AD
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700759F RID: 30111
			// (set) Token: 0x0600A335 RID: 41781 RVA: 0x000EC4C5 File Offset: 0x000EA6C5
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170075A0 RID: 30112
			// (set) Token: 0x0600A336 RID: 41782 RVA: 0x000EC4D8 File Offset: 0x000EA6D8
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170075A1 RID: 30113
			// (set) Token: 0x0600A337 RID: 41783 RVA: 0x000EC4F0 File Offset: 0x000EA6F0
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170075A2 RID: 30114
			// (set) Token: 0x0600A338 RID: 41784 RVA: 0x000EC503 File Offset: 0x000EA703
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170075A3 RID: 30115
			// (set) Token: 0x0600A339 RID: 41785 RVA: 0x000EC521 File Offset: 0x000EA721
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170075A4 RID: 30116
			// (set) Token: 0x0600A33A RID: 41786 RVA: 0x000EC534 File Offset: 0x000EA734
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170075A5 RID: 30117
			// (set) Token: 0x0600A33B RID: 41787 RVA: 0x000EC552 File Offset: 0x000EA752
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170075A6 RID: 30118
			// (set) Token: 0x0600A33C RID: 41788 RVA: 0x000EC565 File Offset: 0x000EA765
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170075A7 RID: 30119
			// (set) Token: 0x0600A33D RID: 41789 RVA: 0x000EC57D File Offset: 0x000EA77D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170075A8 RID: 30120
			// (set) Token: 0x0600A33E RID: 41790 RVA: 0x000EC595 File Offset: 0x000EA795
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170075A9 RID: 30121
			// (set) Token: 0x0600A33F RID: 41791 RVA: 0x000EC5AD File Offset: 0x000EA7AD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170075AA RID: 30122
			// (set) Token: 0x0600A340 RID: 41792 RVA: 0x000EC5C5 File Offset: 0x000EA7C5
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C9C RID: 3228
		public class WindowsLiveCustomDomainsParameters : ParametersBase
		{
			// Token: 0x170075AB RID: 30123
			// (set) Token: 0x0600A342 RID: 41794 RVA: 0x000EC5E5 File Offset: 0x000EA7E5
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170075AC RID: 30124
			// (set) Token: 0x0600A343 RID: 41795 RVA: 0x000EC603 File Offset: 0x000EA803
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170075AD RID: 30125
			// (set) Token: 0x0600A344 RID: 41796 RVA: 0x000EC61B File Offset: 0x000EA81B
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170075AE RID: 30126
			// (set) Token: 0x0600A345 RID: 41797 RVA: 0x000EC62E File Offset: 0x000EA82E
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170075AF RID: 30127
			// (set) Token: 0x0600A346 RID: 41798 RVA: 0x000EC646 File Offset: 0x000EA846
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170075B0 RID: 30128
			// (set) Token: 0x0600A347 RID: 41799 RVA: 0x000EC659 File Offset: 0x000EA859
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170075B1 RID: 30129
			// (set) Token: 0x0600A348 RID: 41800 RVA: 0x000EC677 File Offset: 0x000EA877
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170075B2 RID: 30130
			// (set) Token: 0x0600A349 RID: 41801 RVA: 0x000EC68A File Offset: 0x000EA88A
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170075B3 RID: 30131
			// (set) Token: 0x0600A34A RID: 41802 RVA: 0x000EC6A2 File Offset: 0x000EA8A2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170075B4 RID: 30132
			// (set) Token: 0x0600A34B RID: 41803 RVA: 0x000EC6BA File Offset: 0x000EA8BA
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170075B5 RID: 30133
			// (set) Token: 0x0600A34C RID: 41804 RVA: 0x000EC6D8 File Offset: 0x000EA8D8
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170075B6 RID: 30134
			// (set) Token: 0x0600A34D RID: 41805 RVA: 0x000EC6EB File Offset: 0x000EA8EB
			public virtual SwitchParameter UseExistingLiveId
			{
				set
				{
					base.PowerSharpParameters["UseExistingLiveId"] = value;
				}
			}

			// Token: 0x170075B7 RID: 30135
			// (set) Token: 0x0600A34E RID: 41806 RVA: 0x000EC703 File Offset: 0x000EA903
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x170075B8 RID: 30136
			// (set) Token: 0x0600A34F RID: 41807 RVA: 0x000EC716 File Offset: 0x000EA916
			public virtual SwitchParameter BypassLiveId
			{
				set
				{
					base.PowerSharpParameters["BypassLiveId"] = value;
				}
			}

			// Token: 0x170075B9 RID: 30137
			// (set) Token: 0x0600A350 RID: 41808 RVA: 0x000EC72E File Offset: 0x000EA92E
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170075BA RID: 30138
			// (set) Token: 0x0600A351 RID: 41809 RVA: 0x000EC741 File Offset: 0x000EA941
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170075BB RID: 30139
			// (set) Token: 0x0600A352 RID: 41810 RVA: 0x000EC754 File Offset: 0x000EA954
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170075BC RID: 30140
			// (set) Token: 0x0600A353 RID: 41811 RVA: 0x000EC76C File Offset: 0x000EA96C
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170075BD RID: 30141
			// (set) Token: 0x0600A354 RID: 41812 RVA: 0x000EC77F File Offset: 0x000EA97F
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170075BE RID: 30142
			// (set) Token: 0x0600A355 RID: 41813 RVA: 0x000EC792 File Offset: 0x000EA992
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170075BF RID: 30143
			// (set) Token: 0x0600A356 RID: 41814 RVA: 0x000EC7A5 File Offset: 0x000EA9A5
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075C0 RID: 30144
			// (set) Token: 0x0600A357 RID: 41815 RVA: 0x000EC7C3 File Offset: 0x000EA9C3
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075C1 RID: 30145
			// (set) Token: 0x0600A358 RID: 41816 RVA: 0x000EC7E1 File Offset: 0x000EA9E1
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075C2 RID: 30146
			// (set) Token: 0x0600A359 RID: 41817 RVA: 0x000EC7FF File Offset: 0x000EA9FF
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075C3 RID: 30147
			// (set) Token: 0x0600A35A RID: 41818 RVA: 0x000EC81D File Offset: 0x000EAA1D
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075C4 RID: 30148
			// (set) Token: 0x0600A35B RID: 41819 RVA: 0x000EC83B File Offset: 0x000EAA3B
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170075C5 RID: 30149
			// (set) Token: 0x0600A35C RID: 41820 RVA: 0x000EC84E File Offset: 0x000EAA4E
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170075C6 RID: 30150
			// (set) Token: 0x0600A35D RID: 41821 RVA: 0x000EC866 File Offset: 0x000EAA66
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075C7 RID: 30151
			// (set) Token: 0x0600A35E RID: 41822 RVA: 0x000EC884 File Offset: 0x000EAA84
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170075C8 RID: 30152
			// (set) Token: 0x0600A35F RID: 41823 RVA: 0x000EC89C File Offset: 0x000EAA9C
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170075C9 RID: 30153
			// (set) Token: 0x0600A360 RID: 41824 RVA: 0x000EC8B4 File Offset: 0x000EAAB4
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170075CA RID: 30154
			// (set) Token: 0x0600A361 RID: 41825 RVA: 0x000EC8C7 File Offset: 0x000EAAC7
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170075CB RID: 30155
			// (set) Token: 0x0600A362 RID: 41826 RVA: 0x000EC8DF File Offset: 0x000EAADF
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170075CC RID: 30156
			// (set) Token: 0x0600A363 RID: 41827 RVA: 0x000EC8F7 File Offset: 0x000EAAF7
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170075CD RID: 30157
			// (set) Token: 0x0600A364 RID: 41828 RVA: 0x000EC90F File Offset: 0x000EAB0F
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170075CE RID: 30158
			// (set) Token: 0x0600A365 RID: 41829 RVA: 0x000EC922 File Offset: 0x000EAB22
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170075CF RID: 30159
			// (set) Token: 0x0600A366 RID: 41830 RVA: 0x000EC935 File Offset: 0x000EAB35
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170075D0 RID: 30160
			// (set) Token: 0x0600A367 RID: 41831 RVA: 0x000EC948 File Offset: 0x000EAB48
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170075D1 RID: 30161
			// (set) Token: 0x0600A368 RID: 41832 RVA: 0x000EC95B File Offset: 0x000EAB5B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170075D2 RID: 30162
			// (set) Token: 0x0600A369 RID: 41833 RVA: 0x000EC973 File Offset: 0x000EAB73
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170075D3 RID: 30163
			// (set) Token: 0x0600A36A RID: 41834 RVA: 0x000EC986 File Offset: 0x000EAB86
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170075D4 RID: 30164
			// (set) Token: 0x0600A36B RID: 41835 RVA: 0x000EC999 File Offset: 0x000EAB99
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170075D5 RID: 30165
			// (set) Token: 0x0600A36C RID: 41836 RVA: 0x000EC9B1 File Offset: 0x000EABB1
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170075D6 RID: 30166
			// (set) Token: 0x0600A36D RID: 41837 RVA: 0x000EC9C4 File Offset: 0x000EABC4
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170075D7 RID: 30167
			// (set) Token: 0x0600A36E RID: 41838 RVA: 0x000EC9DC File Offset: 0x000EABDC
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170075D8 RID: 30168
			// (set) Token: 0x0600A36F RID: 41839 RVA: 0x000EC9EF File Offset: 0x000EABEF
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170075D9 RID: 30169
			// (set) Token: 0x0600A370 RID: 41840 RVA: 0x000ECA0D File Offset: 0x000EAC0D
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170075DA RID: 30170
			// (set) Token: 0x0600A371 RID: 41841 RVA: 0x000ECA20 File Offset: 0x000EAC20
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170075DB RID: 30171
			// (set) Token: 0x0600A372 RID: 41842 RVA: 0x000ECA3E File Offset: 0x000EAC3E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170075DC RID: 30172
			// (set) Token: 0x0600A373 RID: 41843 RVA: 0x000ECA51 File Offset: 0x000EAC51
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170075DD RID: 30173
			// (set) Token: 0x0600A374 RID: 41844 RVA: 0x000ECA69 File Offset: 0x000EAC69
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170075DE RID: 30174
			// (set) Token: 0x0600A375 RID: 41845 RVA: 0x000ECA81 File Offset: 0x000EAC81
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170075DF RID: 30175
			// (set) Token: 0x0600A376 RID: 41846 RVA: 0x000ECA99 File Offset: 0x000EAC99
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170075E0 RID: 30176
			// (set) Token: 0x0600A377 RID: 41847 RVA: 0x000ECAB1 File Offset: 0x000EACB1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C9D RID: 3229
		public class ImportLiveIdParameters : ParametersBase
		{
			// Token: 0x170075E1 RID: 30177
			// (set) Token: 0x0600A379 RID: 41849 RVA: 0x000ECAD1 File Offset: 0x000EACD1
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170075E2 RID: 30178
			// (set) Token: 0x0600A37A RID: 41850 RVA: 0x000ECAEF File Offset: 0x000EACEF
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170075E3 RID: 30179
			// (set) Token: 0x0600A37B RID: 41851 RVA: 0x000ECB07 File Offset: 0x000EAD07
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170075E4 RID: 30180
			// (set) Token: 0x0600A37C RID: 41852 RVA: 0x000ECB1A File Offset: 0x000EAD1A
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170075E5 RID: 30181
			// (set) Token: 0x0600A37D RID: 41853 RVA: 0x000ECB32 File Offset: 0x000EAD32
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170075E6 RID: 30182
			// (set) Token: 0x0600A37E RID: 41854 RVA: 0x000ECB45 File Offset: 0x000EAD45
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170075E7 RID: 30183
			// (set) Token: 0x0600A37F RID: 41855 RVA: 0x000ECB63 File Offset: 0x000EAD63
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170075E8 RID: 30184
			// (set) Token: 0x0600A380 RID: 41856 RVA: 0x000ECB76 File Offset: 0x000EAD76
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170075E9 RID: 30185
			// (set) Token: 0x0600A381 RID: 41857 RVA: 0x000ECB8E File Offset: 0x000EAD8E
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170075EA RID: 30186
			// (set) Token: 0x0600A382 RID: 41858 RVA: 0x000ECBA6 File Offset: 0x000EADA6
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170075EB RID: 30187
			// (set) Token: 0x0600A383 RID: 41859 RVA: 0x000ECBC4 File Offset: 0x000EADC4
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170075EC RID: 30188
			// (set) Token: 0x0600A384 RID: 41860 RVA: 0x000ECBD7 File Offset: 0x000EADD7
			public virtual SwitchParameter ImportLiveId
			{
				set
				{
					base.PowerSharpParameters["ImportLiveId"] = value;
				}
			}

			// Token: 0x170075ED RID: 30189
			// (set) Token: 0x0600A385 RID: 41861 RVA: 0x000ECBEF File Offset: 0x000EADEF
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170075EE RID: 30190
			// (set) Token: 0x0600A386 RID: 41862 RVA: 0x000ECC02 File Offset: 0x000EAE02
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170075EF RID: 30191
			// (set) Token: 0x0600A387 RID: 41863 RVA: 0x000ECC15 File Offset: 0x000EAE15
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170075F0 RID: 30192
			// (set) Token: 0x0600A388 RID: 41864 RVA: 0x000ECC2D File Offset: 0x000EAE2D
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170075F1 RID: 30193
			// (set) Token: 0x0600A389 RID: 41865 RVA: 0x000ECC40 File Offset: 0x000EAE40
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170075F2 RID: 30194
			// (set) Token: 0x0600A38A RID: 41866 RVA: 0x000ECC53 File Offset: 0x000EAE53
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170075F3 RID: 30195
			// (set) Token: 0x0600A38B RID: 41867 RVA: 0x000ECC66 File Offset: 0x000EAE66
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075F4 RID: 30196
			// (set) Token: 0x0600A38C RID: 41868 RVA: 0x000ECC84 File Offset: 0x000EAE84
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075F5 RID: 30197
			// (set) Token: 0x0600A38D RID: 41869 RVA: 0x000ECCA2 File Offset: 0x000EAEA2
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075F6 RID: 30198
			// (set) Token: 0x0600A38E RID: 41870 RVA: 0x000ECCC0 File Offset: 0x000EAEC0
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075F7 RID: 30199
			// (set) Token: 0x0600A38F RID: 41871 RVA: 0x000ECCDE File Offset: 0x000EAEDE
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075F8 RID: 30200
			// (set) Token: 0x0600A390 RID: 41872 RVA: 0x000ECCFC File Offset: 0x000EAEFC
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170075F9 RID: 30201
			// (set) Token: 0x0600A391 RID: 41873 RVA: 0x000ECD0F File Offset: 0x000EAF0F
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170075FA RID: 30202
			// (set) Token: 0x0600A392 RID: 41874 RVA: 0x000ECD27 File Offset: 0x000EAF27
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170075FB RID: 30203
			// (set) Token: 0x0600A393 RID: 41875 RVA: 0x000ECD45 File Offset: 0x000EAF45
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170075FC RID: 30204
			// (set) Token: 0x0600A394 RID: 41876 RVA: 0x000ECD5D File Offset: 0x000EAF5D
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170075FD RID: 30205
			// (set) Token: 0x0600A395 RID: 41877 RVA: 0x000ECD75 File Offset: 0x000EAF75
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170075FE RID: 30206
			// (set) Token: 0x0600A396 RID: 41878 RVA: 0x000ECD88 File Offset: 0x000EAF88
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170075FF RID: 30207
			// (set) Token: 0x0600A397 RID: 41879 RVA: 0x000ECDA0 File Offset: 0x000EAFA0
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007600 RID: 30208
			// (set) Token: 0x0600A398 RID: 41880 RVA: 0x000ECDB8 File Offset: 0x000EAFB8
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007601 RID: 30209
			// (set) Token: 0x0600A399 RID: 41881 RVA: 0x000ECDD0 File Offset: 0x000EAFD0
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007602 RID: 30210
			// (set) Token: 0x0600A39A RID: 41882 RVA: 0x000ECDE3 File Offset: 0x000EAFE3
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007603 RID: 30211
			// (set) Token: 0x0600A39B RID: 41883 RVA: 0x000ECDF6 File Offset: 0x000EAFF6
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007604 RID: 30212
			// (set) Token: 0x0600A39C RID: 41884 RVA: 0x000ECE09 File Offset: 0x000EB009
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007605 RID: 30213
			// (set) Token: 0x0600A39D RID: 41885 RVA: 0x000ECE1C File Offset: 0x000EB01C
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007606 RID: 30214
			// (set) Token: 0x0600A39E RID: 41886 RVA: 0x000ECE34 File Offset: 0x000EB034
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007607 RID: 30215
			// (set) Token: 0x0600A39F RID: 41887 RVA: 0x000ECE47 File Offset: 0x000EB047
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007608 RID: 30216
			// (set) Token: 0x0600A3A0 RID: 41888 RVA: 0x000ECE5A File Offset: 0x000EB05A
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007609 RID: 30217
			// (set) Token: 0x0600A3A1 RID: 41889 RVA: 0x000ECE72 File Offset: 0x000EB072
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700760A RID: 30218
			// (set) Token: 0x0600A3A2 RID: 41890 RVA: 0x000ECE85 File Offset: 0x000EB085
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700760B RID: 30219
			// (set) Token: 0x0600A3A3 RID: 41891 RVA: 0x000ECE9D File Offset: 0x000EB09D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700760C RID: 30220
			// (set) Token: 0x0600A3A4 RID: 41892 RVA: 0x000ECEB0 File Offset: 0x000EB0B0
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700760D RID: 30221
			// (set) Token: 0x0600A3A5 RID: 41893 RVA: 0x000ECECE File Offset: 0x000EB0CE
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700760E RID: 30222
			// (set) Token: 0x0600A3A6 RID: 41894 RVA: 0x000ECEE1 File Offset: 0x000EB0E1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700760F RID: 30223
			// (set) Token: 0x0600A3A7 RID: 41895 RVA: 0x000ECEFF File Offset: 0x000EB0FF
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007610 RID: 30224
			// (set) Token: 0x0600A3A8 RID: 41896 RVA: 0x000ECF12 File Offset: 0x000EB112
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007611 RID: 30225
			// (set) Token: 0x0600A3A9 RID: 41897 RVA: 0x000ECF2A File Offset: 0x000EB12A
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007612 RID: 30226
			// (set) Token: 0x0600A3AA RID: 41898 RVA: 0x000ECF42 File Offset: 0x000EB142
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007613 RID: 30227
			// (set) Token: 0x0600A3AB RID: 41899 RVA: 0x000ECF5A File Offset: 0x000EB15A
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007614 RID: 30228
			// (set) Token: 0x0600A3AC RID: 41900 RVA: 0x000ECF72 File Offset: 0x000EB172
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C9E RID: 3230
		public class RemovedMailboxParameters : ParametersBase
		{
			// Token: 0x17007615 RID: 30229
			// (set) Token: 0x0600A3AE RID: 41902 RVA: 0x000ECF92 File Offset: 0x000EB192
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007616 RID: 30230
			// (set) Token: 0x0600A3AF RID: 41903 RVA: 0x000ECFB0 File Offset: 0x000EB1B0
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17007617 RID: 30231
			// (set) Token: 0x0600A3B0 RID: 41904 RVA: 0x000ECFC8 File Offset: 0x000EB1C8
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17007618 RID: 30232
			// (set) Token: 0x0600A3B1 RID: 41905 RVA: 0x000ECFDB File Offset: 0x000EB1DB
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17007619 RID: 30233
			// (set) Token: 0x0600A3B2 RID: 41906 RVA: 0x000ECFF3 File Offset: 0x000EB1F3
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700761A RID: 30234
			// (set) Token: 0x0600A3B3 RID: 41907 RVA: 0x000ED006 File Offset: 0x000EB206
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700761B RID: 30235
			// (set) Token: 0x0600A3B4 RID: 41908 RVA: 0x000ED024 File Offset: 0x000EB224
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700761C RID: 30236
			// (set) Token: 0x0600A3B5 RID: 41909 RVA: 0x000ED037 File Offset: 0x000EB237
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700761D RID: 30237
			// (set) Token: 0x0600A3B6 RID: 41910 RVA: 0x000ED04F File Offset: 0x000EB24F
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700761E RID: 30238
			// (set) Token: 0x0600A3B7 RID: 41911 RVA: 0x000ED067 File Offset: 0x000EB267
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700761F RID: 30239
			// (set) Token: 0x0600A3B8 RID: 41912 RVA: 0x000ED085 File Offset: 0x000EB285
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007620 RID: 30240
			// (set) Token: 0x0600A3B9 RID: 41913 RVA: 0x000ED098 File Offset: 0x000EB298
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007621 RID: 30241
			// (set) Token: 0x0600A3BA RID: 41914 RVA: 0x000ED0AB File Offset: 0x000EB2AB
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007622 RID: 30242
			// (set) Token: 0x0600A3BB RID: 41915 RVA: 0x000ED0C3 File Offset: 0x000EB2C3
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007623 RID: 30243
			// (set) Token: 0x0600A3BC RID: 41916 RVA: 0x000ED0D6 File Offset: 0x000EB2D6
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007624 RID: 30244
			// (set) Token: 0x0600A3BD RID: 41917 RVA: 0x000ED0E9 File Offset: 0x000EB2E9
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007625 RID: 30245
			// (set) Token: 0x0600A3BE RID: 41918 RVA: 0x000ED0FC File Offset: 0x000EB2FC
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007626 RID: 30246
			// (set) Token: 0x0600A3BF RID: 41919 RVA: 0x000ED11A File Offset: 0x000EB31A
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007627 RID: 30247
			// (set) Token: 0x0600A3C0 RID: 41920 RVA: 0x000ED138 File Offset: 0x000EB338
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007628 RID: 30248
			// (set) Token: 0x0600A3C1 RID: 41921 RVA: 0x000ED156 File Offset: 0x000EB356
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007629 RID: 30249
			// (set) Token: 0x0600A3C2 RID: 41922 RVA: 0x000ED174 File Offset: 0x000EB374
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700762A RID: 30250
			// (set) Token: 0x0600A3C3 RID: 41923 RVA: 0x000ED192 File Offset: 0x000EB392
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700762B RID: 30251
			// (set) Token: 0x0600A3C4 RID: 41924 RVA: 0x000ED1A5 File Offset: 0x000EB3A5
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700762C RID: 30252
			// (set) Token: 0x0600A3C5 RID: 41925 RVA: 0x000ED1BD File Offset: 0x000EB3BD
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700762D RID: 30253
			// (set) Token: 0x0600A3C6 RID: 41926 RVA: 0x000ED1DB File Offset: 0x000EB3DB
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700762E RID: 30254
			// (set) Token: 0x0600A3C7 RID: 41927 RVA: 0x000ED1F3 File Offset: 0x000EB3F3
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700762F RID: 30255
			// (set) Token: 0x0600A3C8 RID: 41928 RVA: 0x000ED20B File Offset: 0x000EB40B
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007630 RID: 30256
			// (set) Token: 0x0600A3C9 RID: 41929 RVA: 0x000ED21E File Offset: 0x000EB41E
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007631 RID: 30257
			// (set) Token: 0x0600A3CA RID: 41930 RVA: 0x000ED236 File Offset: 0x000EB436
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007632 RID: 30258
			// (set) Token: 0x0600A3CB RID: 41931 RVA: 0x000ED24E File Offset: 0x000EB44E
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007633 RID: 30259
			// (set) Token: 0x0600A3CC RID: 41932 RVA: 0x000ED266 File Offset: 0x000EB466
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007634 RID: 30260
			// (set) Token: 0x0600A3CD RID: 41933 RVA: 0x000ED279 File Offset: 0x000EB479
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007635 RID: 30261
			// (set) Token: 0x0600A3CE RID: 41934 RVA: 0x000ED28C File Offset: 0x000EB48C
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007636 RID: 30262
			// (set) Token: 0x0600A3CF RID: 41935 RVA: 0x000ED29F File Offset: 0x000EB49F
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007637 RID: 30263
			// (set) Token: 0x0600A3D0 RID: 41936 RVA: 0x000ED2B2 File Offset: 0x000EB4B2
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007638 RID: 30264
			// (set) Token: 0x0600A3D1 RID: 41937 RVA: 0x000ED2CA File Offset: 0x000EB4CA
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007639 RID: 30265
			// (set) Token: 0x0600A3D2 RID: 41938 RVA: 0x000ED2DD File Offset: 0x000EB4DD
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700763A RID: 30266
			// (set) Token: 0x0600A3D3 RID: 41939 RVA: 0x000ED2F0 File Offset: 0x000EB4F0
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700763B RID: 30267
			// (set) Token: 0x0600A3D4 RID: 41940 RVA: 0x000ED308 File Offset: 0x000EB508
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700763C RID: 30268
			// (set) Token: 0x0600A3D5 RID: 41941 RVA: 0x000ED31B File Offset: 0x000EB51B
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700763D RID: 30269
			// (set) Token: 0x0600A3D6 RID: 41942 RVA: 0x000ED333 File Offset: 0x000EB533
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700763E RID: 30270
			// (set) Token: 0x0600A3D7 RID: 41943 RVA: 0x000ED346 File Offset: 0x000EB546
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700763F RID: 30271
			// (set) Token: 0x0600A3D8 RID: 41944 RVA: 0x000ED364 File Offset: 0x000EB564
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007640 RID: 30272
			// (set) Token: 0x0600A3D9 RID: 41945 RVA: 0x000ED377 File Offset: 0x000EB577
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007641 RID: 30273
			// (set) Token: 0x0600A3DA RID: 41946 RVA: 0x000ED395 File Offset: 0x000EB595
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007642 RID: 30274
			// (set) Token: 0x0600A3DB RID: 41947 RVA: 0x000ED3A8 File Offset: 0x000EB5A8
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007643 RID: 30275
			// (set) Token: 0x0600A3DC RID: 41948 RVA: 0x000ED3C0 File Offset: 0x000EB5C0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007644 RID: 30276
			// (set) Token: 0x0600A3DD RID: 41949 RVA: 0x000ED3D8 File Offset: 0x000EB5D8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007645 RID: 30277
			// (set) Token: 0x0600A3DE RID: 41950 RVA: 0x000ED3F0 File Offset: 0x000EB5F0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007646 RID: 30278
			// (set) Token: 0x0600A3DF RID: 41951 RVA: 0x000ED408 File Offset: 0x000EB608
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000C9F RID: 3231
		public class FederatedUserParameters : ParametersBase
		{
			// Token: 0x17007647 RID: 30279
			// (set) Token: 0x0600A3E1 RID: 41953 RVA: 0x000ED428 File Offset: 0x000EB628
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007648 RID: 30280
			// (set) Token: 0x0600A3E2 RID: 41954 RVA: 0x000ED446 File Offset: 0x000EB646
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17007649 RID: 30281
			// (set) Token: 0x0600A3E3 RID: 41955 RVA: 0x000ED45E File Offset: 0x000EB65E
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700764A RID: 30282
			// (set) Token: 0x0600A3E4 RID: 41956 RVA: 0x000ED471 File Offset: 0x000EB671
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700764B RID: 30283
			// (set) Token: 0x0600A3E5 RID: 41957 RVA: 0x000ED489 File Offset: 0x000EB689
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700764C RID: 30284
			// (set) Token: 0x0600A3E6 RID: 41958 RVA: 0x000ED49C File Offset: 0x000EB69C
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700764D RID: 30285
			// (set) Token: 0x0600A3E7 RID: 41959 RVA: 0x000ED4BA File Offset: 0x000EB6BA
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700764E RID: 30286
			// (set) Token: 0x0600A3E8 RID: 41960 RVA: 0x000ED4CD File Offset: 0x000EB6CD
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700764F RID: 30287
			// (set) Token: 0x0600A3E9 RID: 41961 RVA: 0x000ED4E0 File Offset: 0x000EB6E0
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x17007650 RID: 30288
			// (set) Token: 0x0600A3EA RID: 41962 RVA: 0x000ED4F8 File Offset: 0x000EB6F8
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17007651 RID: 30289
			// (set) Token: 0x0600A3EB RID: 41963 RVA: 0x000ED50B File Offset: 0x000EB70B
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007652 RID: 30290
			// (set) Token: 0x0600A3EC RID: 41964 RVA: 0x000ED51E File Offset: 0x000EB71E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007653 RID: 30291
			// (set) Token: 0x0600A3ED RID: 41965 RVA: 0x000ED531 File Offset: 0x000EB731
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007654 RID: 30292
			// (set) Token: 0x0600A3EE RID: 41966 RVA: 0x000ED549 File Offset: 0x000EB749
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007655 RID: 30293
			// (set) Token: 0x0600A3EF RID: 41967 RVA: 0x000ED55C File Offset: 0x000EB75C
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007656 RID: 30294
			// (set) Token: 0x0600A3F0 RID: 41968 RVA: 0x000ED56F File Offset: 0x000EB76F
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007657 RID: 30295
			// (set) Token: 0x0600A3F1 RID: 41969 RVA: 0x000ED582 File Offset: 0x000EB782
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007658 RID: 30296
			// (set) Token: 0x0600A3F2 RID: 41970 RVA: 0x000ED5A0 File Offset: 0x000EB7A0
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007659 RID: 30297
			// (set) Token: 0x0600A3F3 RID: 41971 RVA: 0x000ED5BE File Offset: 0x000EB7BE
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700765A RID: 30298
			// (set) Token: 0x0600A3F4 RID: 41972 RVA: 0x000ED5DC File Offset: 0x000EB7DC
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700765B RID: 30299
			// (set) Token: 0x0600A3F5 RID: 41973 RVA: 0x000ED5FA File Offset: 0x000EB7FA
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700765C RID: 30300
			// (set) Token: 0x0600A3F6 RID: 41974 RVA: 0x000ED618 File Offset: 0x000EB818
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700765D RID: 30301
			// (set) Token: 0x0600A3F7 RID: 41975 RVA: 0x000ED62B File Offset: 0x000EB82B
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700765E RID: 30302
			// (set) Token: 0x0600A3F8 RID: 41976 RVA: 0x000ED643 File Offset: 0x000EB843
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700765F RID: 30303
			// (set) Token: 0x0600A3F9 RID: 41977 RVA: 0x000ED661 File Offset: 0x000EB861
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007660 RID: 30304
			// (set) Token: 0x0600A3FA RID: 41978 RVA: 0x000ED679 File Offset: 0x000EB879
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007661 RID: 30305
			// (set) Token: 0x0600A3FB RID: 41979 RVA: 0x000ED691 File Offset: 0x000EB891
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007662 RID: 30306
			// (set) Token: 0x0600A3FC RID: 41980 RVA: 0x000ED6A4 File Offset: 0x000EB8A4
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007663 RID: 30307
			// (set) Token: 0x0600A3FD RID: 41981 RVA: 0x000ED6BC File Offset: 0x000EB8BC
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007664 RID: 30308
			// (set) Token: 0x0600A3FE RID: 41982 RVA: 0x000ED6D4 File Offset: 0x000EB8D4
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007665 RID: 30309
			// (set) Token: 0x0600A3FF RID: 41983 RVA: 0x000ED6EC File Offset: 0x000EB8EC
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007666 RID: 30310
			// (set) Token: 0x0600A400 RID: 41984 RVA: 0x000ED6FF File Offset: 0x000EB8FF
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007667 RID: 30311
			// (set) Token: 0x0600A401 RID: 41985 RVA: 0x000ED712 File Offset: 0x000EB912
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007668 RID: 30312
			// (set) Token: 0x0600A402 RID: 41986 RVA: 0x000ED725 File Offset: 0x000EB925
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007669 RID: 30313
			// (set) Token: 0x0600A403 RID: 41987 RVA: 0x000ED738 File Offset: 0x000EB938
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700766A RID: 30314
			// (set) Token: 0x0600A404 RID: 41988 RVA: 0x000ED750 File Offset: 0x000EB950
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700766B RID: 30315
			// (set) Token: 0x0600A405 RID: 41989 RVA: 0x000ED763 File Offset: 0x000EB963
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700766C RID: 30316
			// (set) Token: 0x0600A406 RID: 41990 RVA: 0x000ED776 File Offset: 0x000EB976
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700766D RID: 30317
			// (set) Token: 0x0600A407 RID: 41991 RVA: 0x000ED78E File Offset: 0x000EB98E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700766E RID: 30318
			// (set) Token: 0x0600A408 RID: 41992 RVA: 0x000ED7A1 File Offset: 0x000EB9A1
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700766F RID: 30319
			// (set) Token: 0x0600A409 RID: 41993 RVA: 0x000ED7B9 File Offset: 0x000EB9B9
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007670 RID: 30320
			// (set) Token: 0x0600A40A RID: 41994 RVA: 0x000ED7CC File Offset: 0x000EB9CC
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007671 RID: 30321
			// (set) Token: 0x0600A40B RID: 41995 RVA: 0x000ED7EA File Offset: 0x000EB9EA
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007672 RID: 30322
			// (set) Token: 0x0600A40C RID: 41996 RVA: 0x000ED7FD File Offset: 0x000EB9FD
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007673 RID: 30323
			// (set) Token: 0x0600A40D RID: 41997 RVA: 0x000ED81B File Offset: 0x000EBA1B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007674 RID: 30324
			// (set) Token: 0x0600A40E RID: 41998 RVA: 0x000ED82E File Offset: 0x000EBA2E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007675 RID: 30325
			// (set) Token: 0x0600A40F RID: 41999 RVA: 0x000ED846 File Offset: 0x000EBA46
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007676 RID: 30326
			// (set) Token: 0x0600A410 RID: 42000 RVA: 0x000ED85E File Offset: 0x000EBA5E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007677 RID: 30327
			// (set) Token: 0x0600A411 RID: 42001 RVA: 0x000ED876 File Offset: 0x000EBA76
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007678 RID: 30328
			// (set) Token: 0x0600A412 RID: 42002 RVA: 0x000ED88E File Offset: 0x000EBA8E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA0 RID: 3232
		public class MicrosoftOnlineServicesFederatedUserParameters : ParametersBase
		{
			// Token: 0x17007679 RID: 30329
			// (set) Token: 0x0600A414 RID: 42004 RVA: 0x000ED8AE File Offset: 0x000EBAAE
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x1700767A RID: 30330
			// (set) Token: 0x0600A415 RID: 42005 RVA: 0x000ED8CC File Offset: 0x000EBACC
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700767B RID: 30331
			// (set) Token: 0x0600A416 RID: 42006 RVA: 0x000ED8E4 File Offset: 0x000EBAE4
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700767C RID: 30332
			// (set) Token: 0x0600A417 RID: 42007 RVA: 0x000ED8F7 File Offset: 0x000EBAF7
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700767D RID: 30333
			// (set) Token: 0x0600A418 RID: 42008 RVA: 0x000ED90F File Offset: 0x000EBB0F
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700767E RID: 30334
			// (set) Token: 0x0600A419 RID: 42009 RVA: 0x000ED922 File Offset: 0x000EBB22
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700767F RID: 30335
			// (set) Token: 0x0600A41A RID: 42010 RVA: 0x000ED940 File Offset: 0x000EBB40
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17007680 RID: 30336
			// (set) Token: 0x0600A41B RID: 42011 RVA: 0x000ED953 File Offset: 0x000EBB53
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x17007681 RID: 30337
			// (set) Token: 0x0600A41C RID: 42012 RVA: 0x000ED966 File Offset: 0x000EBB66
			public virtual string FederatedIdentity
			{
				set
				{
					base.PowerSharpParameters["FederatedIdentity"] = value;
				}
			}

			// Token: 0x17007682 RID: 30338
			// (set) Token: 0x0600A41D RID: 42013 RVA: 0x000ED979 File Offset: 0x000EBB79
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007683 RID: 30339
			// (set) Token: 0x0600A41E RID: 42014 RVA: 0x000ED98C File Offset: 0x000EBB8C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007684 RID: 30340
			// (set) Token: 0x0600A41F RID: 42015 RVA: 0x000ED99F File Offset: 0x000EBB9F
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007685 RID: 30341
			// (set) Token: 0x0600A420 RID: 42016 RVA: 0x000ED9B7 File Offset: 0x000EBBB7
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007686 RID: 30342
			// (set) Token: 0x0600A421 RID: 42017 RVA: 0x000ED9CA File Offset: 0x000EBBCA
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007687 RID: 30343
			// (set) Token: 0x0600A422 RID: 42018 RVA: 0x000ED9DD File Offset: 0x000EBBDD
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007688 RID: 30344
			// (set) Token: 0x0600A423 RID: 42019 RVA: 0x000ED9F0 File Offset: 0x000EBBF0
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007689 RID: 30345
			// (set) Token: 0x0600A424 RID: 42020 RVA: 0x000EDA0E File Offset: 0x000EBC0E
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700768A RID: 30346
			// (set) Token: 0x0600A425 RID: 42021 RVA: 0x000EDA2C File Offset: 0x000EBC2C
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700768B RID: 30347
			// (set) Token: 0x0600A426 RID: 42022 RVA: 0x000EDA4A File Offset: 0x000EBC4A
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700768C RID: 30348
			// (set) Token: 0x0600A427 RID: 42023 RVA: 0x000EDA68 File Offset: 0x000EBC68
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700768D RID: 30349
			// (set) Token: 0x0600A428 RID: 42024 RVA: 0x000EDA86 File Offset: 0x000EBC86
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700768E RID: 30350
			// (set) Token: 0x0600A429 RID: 42025 RVA: 0x000EDA99 File Offset: 0x000EBC99
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700768F RID: 30351
			// (set) Token: 0x0600A42A RID: 42026 RVA: 0x000EDAB1 File Offset: 0x000EBCB1
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007690 RID: 30352
			// (set) Token: 0x0600A42B RID: 42027 RVA: 0x000EDACF File Offset: 0x000EBCCF
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007691 RID: 30353
			// (set) Token: 0x0600A42C RID: 42028 RVA: 0x000EDAE7 File Offset: 0x000EBCE7
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007692 RID: 30354
			// (set) Token: 0x0600A42D RID: 42029 RVA: 0x000EDAFF File Offset: 0x000EBCFF
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007693 RID: 30355
			// (set) Token: 0x0600A42E RID: 42030 RVA: 0x000EDB12 File Offset: 0x000EBD12
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007694 RID: 30356
			// (set) Token: 0x0600A42F RID: 42031 RVA: 0x000EDB2A File Offset: 0x000EBD2A
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007695 RID: 30357
			// (set) Token: 0x0600A430 RID: 42032 RVA: 0x000EDB42 File Offset: 0x000EBD42
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007696 RID: 30358
			// (set) Token: 0x0600A431 RID: 42033 RVA: 0x000EDB5A File Offset: 0x000EBD5A
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007697 RID: 30359
			// (set) Token: 0x0600A432 RID: 42034 RVA: 0x000EDB6D File Offset: 0x000EBD6D
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007698 RID: 30360
			// (set) Token: 0x0600A433 RID: 42035 RVA: 0x000EDB80 File Offset: 0x000EBD80
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007699 RID: 30361
			// (set) Token: 0x0600A434 RID: 42036 RVA: 0x000EDB93 File Offset: 0x000EBD93
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700769A RID: 30362
			// (set) Token: 0x0600A435 RID: 42037 RVA: 0x000EDBA6 File Offset: 0x000EBDA6
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700769B RID: 30363
			// (set) Token: 0x0600A436 RID: 42038 RVA: 0x000EDBBE File Offset: 0x000EBDBE
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700769C RID: 30364
			// (set) Token: 0x0600A437 RID: 42039 RVA: 0x000EDBD1 File Offset: 0x000EBDD1
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700769D RID: 30365
			// (set) Token: 0x0600A438 RID: 42040 RVA: 0x000EDBE4 File Offset: 0x000EBDE4
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700769E RID: 30366
			// (set) Token: 0x0600A439 RID: 42041 RVA: 0x000EDBFC File Offset: 0x000EBDFC
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700769F RID: 30367
			// (set) Token: 0x0600A43A RID: 42042 RVA: 0x000EDC0F File Offset: 0x000EBE0F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170076A0 RID: 30368
			// (set) Token: 0x0600A43B RID: 42043 RVA: 0x000EDC27 File Offset: 0x000EBE27
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170076A1 RID: 30369
			// (set) Token: 0x0600A43C RID: 42044 RVA: 0x000EDC3A File Offset: 0x000EBE3A
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170076A2 RID: 30370
			// (set) Token: 0x0600A43D RID: 42045 RVA: 0x000EDC58 File Offset: 0x000EBE58
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170076A3 RID: 30371
			// (set) Token: 0x0600A43E RID: 42046 RVA: 0x000EDC6B File Offset: 0x000EBE6B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170076A4 RID: 30372
			// (set) Token: 0x0600A43F RID: 42047 RVA: 0x000EDC89 File Offset: 0x000EBE89
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170076A5 RID: 30373
			// (set) Token: 0x0600A440 RID: 42048 RVA: 0x000EDC9C File Offset: 0x000EBE9C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170076A6 RID: 30374
			// (set) Token: 0x0600A441 RID: 42049 RVA: 0x000EDCB4 File Offset: 0x000EBEB4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170076A7 RID: 30375
			// (set) Token: 0x0600A442 RID: 42050 RVA: 0x000EDCCC File Offset: 0x000EBECC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170076A8 RID: 30376
			// (set) Token: 0x0600A443 RID: 42051 RVA: 0x000EDCE4 File Offset: 0x000EBEE4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170076A9 RID: 30377
			// (set) Token: 0x0600A444 RID: 42052 RVA: 0x000EDCFC File Offset: 0x000EBEFC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA1 RID: 3233
		public class RemoteArchiveParameters : ParametersBase
		{
			// Token: 0x170076AA RID: 30378
			// (set) Token: 0x0600A446 RID: 42054 RVA: 0x000EDD1C File Offset: 0x000EBF1C
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170076AB RID: 30379
			// (set) Token: 0x0600A447 RID: 42055 RVA: 0x000EDD3A File Offset: 0x000EBF3A
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170076AC RID: 30380
			// (set) Token: 0x0600A448 RID: 42056 RVA: 0x000EDD52 File Offset: 0x000EBF52
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170076AD RID: 30381
			// (set) Token: 0x0600A449 RID: 42057 RVA: 0x000EDD65 File Offset: 0x000EBF65
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170076AE RID: 30382
			// (set) Token: 0x0600A44A RID: 42058 RVA: 0x000EDD7D File Offset: 0x000EBF7D
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170076AF RID: 30383
			// (set) Token: 0x0600A44B RID: 42059 RVA: 0x000EDD90 File Offset: 0x000EBF90
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170076B0 RID: 30384
			// (set) Token: 0x0600A44C RID: 42060 RVA: 0x000EDDA3 File Offset: 0x000EBFA3
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170076B1 RID: 30385
			// (set) Token: 0x0600A44D RID: 42061 RVA: 0x000EDDC1 File Offset: 0x000EBFC1
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170076B2 RID: 30386
			// (set) Token: 0x0600A44E RID: 42062 RVA: 0x000EDDD4 File Offset: 0x000EBFD4
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170076B3 RID: 30387
			// (set) Token: 0x0600A44F RID: 42063 RVA: 0x000EDDEC File Offset: 0x000EBFEC
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170076B4 RID: 30388
			// (set) Token: 0x0600A450 RID: 42064 RVA: 0x000EDE04 File Offset: 0x000EC004
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170076B5 RID: 30389
			// (set) Token: 0x0600A451 RID: 42065 RVA: 0x000EDE22 File Offset: 0x000EC022
			public virtual SwitchParameter RemoteArchive
			{
				set
				{
					base.PowerSharpParameters["RemoteArchive"] = value;
				}
			}

			// Token: 0x170076B6 RID: 30390
			// (set) Token: 0x0600A452 RID: 42066 RVA: 0x000EDE3A File Offset: 0x000EC03A
			public virtual SmtpDomain ArchiveDomain
			{
				set
				{
					base.PowerSharpParameters["ArchiveDomain"] = value;
				}
			}

			// Token: 0x170076B7 RID: 30391
			// (set) Token: 0x0600A453 RID: 42067 RVA: 0x000EDE4D File Offset: 0x000EC04D
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170076B8 RID: 30392
			// (set) Token: 0x0600A454 RID: 42068 RVA: 0x000EDE60 File Offset: 0x000EC060
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170076B9 RID: 30393
			// (set) Token: 0x0600A455 RID: 42069 RVA: 0x000EDE73 File Offset: 0x000EC073
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170076BA RID: 30394
			// (set) Token: 0x0600A456 RID: 42070 RVA: 0x000EDE8B File Offset: 0x000EC08B
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170076BB RID: 30395
			// (set) Token: 0x0600A457 RID: 42071 RVA: 0x000EDE9E File Offset: 0x000EC09E
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170076BC RID: 30396
			// (set) Token: 0x0600A458 RID: 42072 RVA: 0x000EDEB1 File Offset: 0x000EC0B1
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170076BD RID: 30397
			// (set) Token: 0x0600A459 RID: 42073 RVA: 0x000EDEC4 File Offset: 0x000EC0C4
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076BE RID: 30398
			// (set) Token: 0x0600A45A RID: 42074 RVA: 0x000EDEE2 File Offset: 0x000EC0E2
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076BF RID: 30399
			// (set) Token: 0x0600A45B RID: 42075 RVA: 0x000EDF00 File Offset: 0x000EC100
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076C0 RID: 30400
			// (set) Token: 0x0600A45C RID: 42076 RVA: 0x000EDF1E File Offset: 0x000EC11E
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076C1 RID: 30401
			// (set) Token: 0x0600A45D RID: 42077 RVA: 0x000EDF3C File Offset: 0x000EC13C
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076C2 RID: 30402
			// (set) Token: 0x0600A45E RID: 42078 RVA: 0x000EDF5A File Offset: 0x000EC15A
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170076C3 RID: 30403
			// (set) Token: 0x0600A45F RID: 42079 RVA: 0x000EDF6D File Offset: 0x000EC16D
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170076C4 RID: 30404
			// (set) Token: 0x0600A460 RID: 42080 RVA: 0x000EDF85 File Offset: 0x000EC185
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076C5 RID: 30405
			// (set) Token: 0x0600A461 RID: 42081 RVA: 0x000EDFA3 File Offset: 0x000EC1A3
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170076C6 RID: 30406
			// (set) Token: 0x0600A462 RID: 42082 RVA: 0x000EDFBB File Offset: 0x000EC1BB
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170076C7 RID: 30407
			// (set) Token: 0x0600A463 RID: 42083 RVA: 0x000EDFD3 File Offset: 0x000EC1D3
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170076C8 RID: 30408
			// (set) Token: 0x0600A464 RID: 42084 RVA: 0x000EDFE6 File Offset: 0x000EC1E6
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170076C9 RID: 30409
			// (set) Token: 0x0600A465 RID: 42085 RVA: 0x000EDFFE File Offset: 0x000EC1FE
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170076CA RID: 30410
			// (set) Token: 0x0600A466 RID: 42086 RVA: 0x000EE016 File Offset: 0x000EC216
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170076CB RID: 30411
			// (set) Token: 0x0600A467 RID: 42087 RVA: 0x000EE02E File Offset: 0x000EC22E
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170076CC RID: 30412
			// (set) Token: 0x0600A468 RID: 42088 RVA: 0x000EE041 File Offset: 0x000EC241
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170076CD RID: 30413
			// (set) Token: 0x0600A469 RID: 42089 RVA: 0x000EE054 File Offset: 0x000EC254
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170076CE RID: 30414
			// (set) Token: 0x0600A46A RID: 42090 RVA: 0x000EE067 File Offset: 0x000EC267
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170076CF RID: 30415
			// (set) Token: 0x0600A46B RID: 42091 RVA: 0x000EE07A File Offset: 0x000EC27A
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170076D0 RID: 30416
			// (set) Token: 0x0600A46C RID: 42092 RVA: 0x000EE092 File Offset: 0x000EC292
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170076D1 RID: 30417
			// (set) Token: 0x0600A46D RID: 42093 RVA: 0x000EE0A5 File Offset: 0x000EC2A5
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170076D2 RID: 30418
			// (set) Token: 0x0600A46E RID: 42094 RVA: 0x000EE0B8 File Offset: 0x000EC2B8
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170076D3 RID: 30419
			// (set) Token: 0x0600A46F RID: 42095 RVA: 0x000EE0D0 File Offset: 0x000EC2D0
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170076D4 RID: 30420
			// (set) Token: 0x0600A470 RID: 42096 RVA: 0x000EE0E3 File Offset: 0x000EC2E3
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170076D5 RID: 30421
			// (set) Token: 0x0600A471 RID: 42097 RVA: 0x000EE0FB File Offset: 0x000EC2FB
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170076D6 RID: 30422
			// (set) Token: 0x0600A472 RID: 42098 RVA: 0x000EE10E File Offset: 0x000EC30E
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170076D7 RID: 30423
			// (set) Token: 0x0600A473 RID: 42099 RVA: 0x000EE12C File Offset: 0x000EC32C
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170076D8 RID: 30424
			// (set) Token: 0x0600A474 RID: 42100 RVA: 0x000EE13F File Offset: 0x000EC33F
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170076D9 RID: 30425
			// (set) Token: 0x0600A475 RID: 42101 RVA: 0x000EE15D File Offset: 0x000EC35D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170076DA RID: 30426
			// (set) Token: 0x0600A476 RID: 42102 RVA: 0x000EE170 File Offset: 0x000EC370
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170076DB RID: 30427
			// (set) Token: 0x0600A477 RID: 42103 RVA: 0x000EE188 File Offset: 0x000EC388
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170076DC RID: 30428
			// (set) Token: 0x0600A478 RID: 42104 RVA: 0x000EE1A0 File Offset: 0x000EC3A0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170076DD RID: 30429
			// (set) Token: 0x0600A479 RID: 42105 RVA: 0x000EE1B8 File Offset: 0x000EC3B8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170076DE RID: 30430
			// (set) Token: 0x0600A47A RID: 42106 RVA: 0x000EE1D0 File Offset: 0x000EC3D0
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA2 RID: 3234
		public class MicrosoftOnlineServicesIDParameters : ParametersBase
		{
			// Token: 0x170076DF RID: 30431
			// (set) Token: 0x0600A47C RID: 42108 RVA: 0x000EE1F0 File Offset: 0x000EC3F0
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170076E0 RID: 30432
			// (set) Token: 0x0600A47D RID: 42109 RVA: 0x000EE20E File Offset: 0x000EC40E
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170076E1 RID: 30433
			// (set) Token: 0x0600A47E RID: 42110 RVA: 0x000EE226 File Offset: 0x000EC426
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170076E2 RID: 30434
			// (set) Token: 0x0600A47F RID: 42111 RVA: 0x000EE239 File Offset: 0x000EC439
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170076E3 RID: 30435
			// (set) Token: 0x0600A480 RID: 42112 RVA: 0x000EE251 File Offset: 0x000EC451
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170076E4 RID: 30436
			// (set) Token: 0x0600A481 RID: 42113 RVA: 0x000EE264 File Offset: 0x000EC464
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170076E5 RID: 30437
			// (set) Token: 0x0600A482 RID: 42114 RVA: 0x000EE277 File Offset: 0x000EC477
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170076E6 RID: 30438
			// (set) Token: 0x0600A483 RID: 42115 RVA: 0x000EE295 File Offset: 0x000EC495
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170076E7 RID: 30439
			// (set) Token: 0x0600A484 RID: 42116 RVA: 0x000EE2A8 File Offset: 0x000EC4A8
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170076E8 RID: 30440
			// (set) Token: 0x0600A485 RID: 42117 RVA: 0x000EE2C0 File Offset: 0x000EC4C0
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170076E9 RID: 30441
			// (set) Token: 0x0600A486 RID: 42118 RVA: 0x000EE2D8 File Offset: 0x000EC4D8
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170076EA RID: 30442
			// (set) Token: 0x0600A487 RID: 42119 RVA: 0x000EE2F6 File Offset: 0x000EC4F6
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x170076EB RID: 30443
			// (set) Token: 0x0600A488 RID: 42120 RVA: 0x000EE309 File Offset: 0x000EC509
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170076EC RID: 30444
			// (set) Token: 0x0600A489 RID: 42121 RVA: 0x000EE31C File Offset: 0x000EC51C
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170076ED RID: 30445
			// (set) Token: 0x0600A48A RID: 42122 RVA: 0x000EE32F File Offset: 0x000EC52F
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170076EE RID: 30446
			// (set) Token: 0x0600A48B RID: 42123 RVA: 0x000EE347 File Offset: 0x000EC547
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170076EF RID: 30447
			// (set) Token: 0x0600A48C RID: 42124 RVA: 0x000EE35A File Offset: 0x000EC55A
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170076F0 RID: 30448
			// (set) Token: 0x0600A48D RID: 42125 RVA: 0x000EE36D File Offset: 0x000EC56D
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170076F1 RID: 30449
			// (set) Token: 0x0600A48E RID: 42126 RVA: 0x000EE380 File Offset: 0x000EC580
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076F2 RID: 30450
			// (set) Token: 0x0600A48F RID: 42127 RVA: 0x000EE39E File Offset: 0x000EC59E
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076F3 RID: 30451
			// (set) Token: 0x0600A490 RID: 42128 RVA: 0x000EE3BC File Offset: 0x000EC5BC
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076F4 RID: 30452
			// (set) Token: 0x0600A491 RID: 42129 RVA: 0x000EE3DA File Offset: 0x000EC5DA
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076F5 RID: 30453
			// (set) Token: 0x0600A492 RID: 42130 RVA: 0x000EE3F8 File Offset: 0x000EC5F8
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076F6 RID: 30454
			// (set) Token: 0x0600A493 RID: 42131 RVA: 0x000EE416 File Offset: 0x000EC616
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170076F7 RID: 30455
			// (set) Token: 0x0600A494 RID: 42132 RVA: 0x000EE429 File Offset: 0x000EC629
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170076F8 RID: 30456
			// (set) Token: 0x0600A495 RID: 42133 RVA: 0x000EE441 File Offset: 0x000EC641
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170076F9 RID: 30457
			// (set) Token: 0x0600A496 RID: 42134 RVA: 0x000EE45F File Offset: 0x000EC65F
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170076FA RID: 30458
			// (set) Token: 0x0600A497 RID: 42135 RVA: 0x000EE477 File Offset: 0x000EC677
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170076FB RID: 30459
			// (set) Token: 0x0600A498 RID: 42136 RVA: 0x000EE48F File Offset: 0x000EC68F
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170076FC RID: 30460
			// (set) Token: 0x0600A499 RID: 42137 RVA: 0x000EE4A2 File Offset: 0x000EC6A2
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170076FD RID: 30461
			// (set) Token: 0x0600A49A RID: 42138 RVA: 0x000EE4BA File Offset: 0x000EC6BA
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170076FE RID: 30462
			// (set) Token: 0x0600A49B RID: 42139 RVA: 0x000EE4D2 File Offset: 0x000EC6D2
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170076FF RID: 30463
			// (set) Token: 0x0600A49C RID: 42140 RVA: 0x000EE4EA File Offset: 0x000EC6EA
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007700 RID: 30464
			// (set) Token: 0x0600A49D RID: 42141 RVA: 0x000EE4FD File Offset: 0x000EC6FD
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007701 RID: 30465
			// (set) Token: 0x0600A49E RID: 42142 RVA: 0x000EE510 File Offset: 0x000EC710
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007702 RID: 30466
			// (set) Token: 0x0600A49F RID: 42143 RVA: 0x000EE523 File Offset: 0x000EC723
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007703 RID: 30467
			// (set) Token: 0x0600A4A0 RID: 42144 RVA: 0x000EE536 File Offset: 0x000EC736
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007704 RID: 30468
			// (set) Token: 0x0600A4A1 RID: 42145 RVA: 0x000EE54E File Offset: 0x000EC74E
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007705 RID: 30469
			// (set) Token: 0x0600A4A2 RID: 42146 RVA: 0x000EE561 File Offset: 0x000EC761
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007706 RID: 30470
			// (set) Token: 0x0600A4A3 RID: 42147 RVA: 0x000EE574 File Offset: 0x000EC774
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007707 RID: 30471
			// (set) Token: 0x0600A4A4 RID: 42148 RVA: 0x000EE58C File Offset: 0x000EC78C
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007708 RID: 30472
			// (set) Token: 0x0600A4A5 RID: 42149 RVA: 0x000EE59F File Offset: 0x000EC79F
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007709 RID: 30473
			// (set) Token: 0x0600A4A6 RID: 42150 RVA: 0x000EE5B7 File Offset: 0x000EC7B7
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700770A RID: 30474
			// (set) Token: 0x0600A4A7 RID: 42151 RVA: 0x000EE5CA File Offset: 0x000EC7CA
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700770B RID: 30475
			// (set) Token: 0x0600A4A8 RID: 42152 RVA: 0x000EE5E8 File Offset: 0x000EC7E8
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700770C RID: 30476
			// (set) Token: 0x0600A4A9 RID: 42153 RVA: 0x000EE5FB File Offset: 0x000EC7FB
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700770D RID: 30477
			// (set) Token: 0x0600A4AA RID: 42154 RVA: 0x000EE619 File Offset: 0x000EC819
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700770E RID: 30478
			// (set) Token: 0x0600A4AB RID: 42155 RVA: 0x000EE62C File Offset: 0x000EC82C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700770F RID: 30479
			// (set) Token: 0x0600A4AC RID: 42156 RVA: 0x000EE644 File Offset: 0x000EC844
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007710 RID: 30480
			// (set) Token: 0x0600A4AD RID: 42157 RVA: 0x000EE65C File Offset: 0x000EC85C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007711 RID: 30481
			// (set) Token: 0x0600A4AE RID: 42158 RVA: 0x000EE674 File Offset: 0x000EC874
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007712 RID: 30482
			// (set) Token: 0x0600A4AF RID: 42159 RVA: 0x000EE68C File Offset: 0x000EC88C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA3 RID: 3235
		public class MailboxPlanParameters : ParametersBase
		{
			// Token: 0x17007713 RID: 30483
			// (set) Token: 0x0600A4B1 RID: 42161 RVA: 0x000EE6AC File Offset: 0x000EC8AC
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007714 RID: 30484
			// (set) Token: 0x0600A4B2 RID: 42162 RVA: 0x000EE6CA File Offset: 0x000EC8CA
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17007715 RID: 30485
			// (set) Token: 0x0600A4B3 RID: 42163 RVA: 0x000EE6E2 File Offset: 0x000EC8E2
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17007716 RID: 30486
			// (set) Token: 0x0600A4B4 RID: 42164 RVA: 0x000EE6F5 File Offset: 0x000EC8F5
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17007717 RID: 30487
			// (set) Token: 0x0600A4B5 RID: 42165 RVA: 0x000EE70D File Offset: 0x000EC90D
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007718 RID: 30488
			// (set) Token: 0x0600A4B6 RID: 42166 RVA: 0x000EE720 File Offset: 0x000EC920
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007719 RID: 30489
			// (set) Token: 0x0600A4B7 RID: 42167 RVA: 0x000EE73E File Offset: 0x000EC93E
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700771A RID: 30490
			// (set) Token: 0x0600A4B8 RID: 42168 RVA: 0x000EE751 File Offset: 0x000EC951
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700771B RID: 30491
			// (set) Token: 0x0600A4B9 RID: 42169 RVA: 0x000EE769 File Offset: 0x000EC969
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700771C RID: 30492
			// (set) Token: 0x0600A4BA RID: 42170 RVA: 0x000EE781 File Offset: 0x000EC981
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x1700771D RID: 30493
			// (set) Token: 0x0600A4BB RID: 42171 RVA: 0x000EE794 File Offset: 0x000EC994
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700771E RID: 30494
			// (set) Token: 0x0600A4BC RID: 42172 RVA: 0x000EE7A7 File Offset: 0x000EC9A7
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700771F RID: 30495
			// (set) Token: 0x0600A4BD RID: 42173 RVA: 0x000EE7BF File Offset: 0x000EC9BF
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007720 RID: 30496
			// (set) Token: 0x0600A4BE RID: 42174 RVA: 0x000EE7D2 File Offset: 0x000EC9D2
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007721 RID: 30497
			// (set) Token: 0x0600A4BF RID: 42175 RVA: 0x000EE7E5 File Offset: 0x000EC9E5
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007722 RID: 30498
			// (set) Token: 0x0600A4C0 RID: 42176 RVA: 0x000EE7F8 File Offset: 0x000EC9F8
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007723 RID: 30499
			// (set) Token: 0x0600A4C1 RID: 42177 RVA: 0x000EE816 File Offset: 0x000ECA16
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007724 RID: 30500
			// (set) Token: 0x0600A4C2 RID: 42178 RVA: 0x000EE834 File Offset: 0x000ECA34
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007725 RID: 30501
			// (set) Token: 0x0600A4C3 RID: 42179 RVA: 0x000EE852 File Offset: 0x000ECA52
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007726 RID: 30502
			// (set) Token: 0x0600A4C4 RID: 42180 RVA: 0x000EE870 File Offset: 0x000ECA70
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007727 RID: 30503
			// (set) Token: 0x0600A4C5 RID: 42181 RVA: 0x000EE88E File Offset: 0x000ECA8E
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007728 RID: 30504
			// (set) Token: 0x0600A4C6 RID: 42182 RVA: 0x000EE8A1 File Offset: 0x000ECAA1
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007729 RID: 30505
			// (set) Token: 0x0600A4C7 RID: 42183 RVA: 0x000EE8B9 File Offset: 0x000ECAB9
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700772A RID: 30506
			// (set) Token: 0x0600A4C8 RID: 42184 RVA: 0x000EE8D7 File Offset: 0x000ECAD7
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700772B RID: 30507
			// (set) Token: 0x0600A4C9 RID: 42185 RVA: 0x000EE8EF File Offset: 0x000ECAEF
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700772C RID: 30508
			// (set) Token: 0x0600A4CA RID: 42186 RVA: 0x000EE907 File Offset: 0x000ECB07
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700772D RID: 30509
			// (set) Token: 0x0600A4CB RID: 42187 RVA: 0x000EE91A File Offset: 0x000ECB1A
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x1700772E RID: 30510
			// (set) Token: 0x0600A4CC RID: 42188 RVA: 0x000EE932 File Offset: 0x000ECB32
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x1700772F RID: 30511
			// (set) Token: 0x0600A4CD RID: 42189 RVA: 0x000EE94A File Offset: 0x000ECB4A
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007730 RID: 30512
			// (set) Token: 0x0600A4CE RID: 42190 RVA: 0x000EE962 File Offset: 0x000ECB62
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007731 RID: 30513
			// (set) Token: 0x0600A4CF RID: 42191 RVA: 0x000EE975 File Offset: 0x000ECB75
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007732 RID: 30514
			// (set) Token: 0x0600A4D0 RID: 42192 RVA: 0x000EE988 File Offset: 0x000ECB88
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007733 RID: 30515
			// (set) Token: 0x0600A4D1 RID: 42193 RVA: 0x000EE99B File Offset: 0x000ECB9B
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007734 RID: 30516
			// (set) Token: 0x0600A4D2 RID: 42194 RVA: 0x000EE9AE File Offset: 0x000ECBAE
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007735 RID: 30517
			// (set) Token: 0x0600A4D3 RID: 42195 RVA: 0x000EE9C6 File Offset: 0x000ECBC6
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007736 RID: 30518
			// (set) Token: 0x0600A4D4 RID: 42196 RVA: 0x000EE9D9 File Offset: 0x000ECBD9
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007737 RID: 30519
			// (set) Token: 0x0600A4D5 RID: 42197 RVA: 0x000EE9EC File Offset: 0x000ECBEC
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007738 RID: 30520
			// (set) Token: 0x0600A4D6 RID: 42198 RVA: 0x000EEA04 File Offset: 0x000ECC04
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007739 RID: 30521
			// (set) Token: 0x0600A4D7 RID: 42199 RVA: 0x000EEA17 File Offset: 0x000ECC17
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700773A RID: 30522
			// (set) Token: 0x0600A4D8 RID: 42200 RVA: 0x000EEA2F File Offset: 0x000ECC2F
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700773B RID: 30523
			// (set) Token: 0x0600A4D9 RID: 42201 RVA: 0x000EEA42 File Offset: 0x000ECC42
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700773C RID: 30524
			// (set) Token: 0x0600A4DA RID: 42202 RVA: 0x000EEA60 File Offset: 0x000ECC60
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700773D RID: 30525
			// (set) Token: 0x0600A4DB RID: 42203 RVA: 0x000EEA73 File Offset: 0x000ECC73
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700773E RID: 30526
			// (set) Token: 0x0600A4DC RID: 42204 RVA: 0x000EEA91 File Offset: 0x000ECC91
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700773F RID: 30527
			// (set) Token: 0x0600A4DD RID: 42205 RVA: 0x000EEAA4 File Offset: 0x000ECCA4
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007740 RID: 30528
			// (set) Token: 0x0600A4DE RID: 42206 RVA: 0x000EEABC File Offset: 0x000ECCBC
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007741 RID: 30529
			// (set) Token: 0x0600A4DF RID: 42207 RVA: 0x000EEAD4 File Offset: 0x000ECCD4
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007742 RID: 30530
			// (set) Token: 0x0600A4E0 RID: 42208 RVA: 0x000EEAEC File Offset: 0x000ECCEC
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007743 RID: 30531
			// (set) Token: 0x0600A4E1 RID: 42209 RVA: 0x000EEB04 File Offset: 0x000ECD04
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA4 RID: 3236
		public class DisabledUserParameters : ParametersBase
		{
			// Token: 0x17007744 RID: 30532
			// (set) Token: 0x0600A4E3 RID: 42211 RVA: 0x000EEB24 File Offset: 0x000ECD24
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007745 RID: 30533
			// (set) Token: 0x0600A4E4 RID: 42212 RVA: 0x000EEB42 File Offset: 0x000ECD42
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x17007746 RID: 30534
			// (set) Token: 0x0600A4E5 RID: 42213 RVA: 0x000EEB5A File Offset: 0x000ECD5A
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x17007747 RID: 30535
			// (set) Token: 0x0600A4E6 RID: 42214 RVA: 0x000EEB6D File Offset: 0x000ECD6D
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x17007748 RID: 30536
			// (set) Token: 0x0600A4E7 RID: 42215 RVA: 0x000EEB85 File Offset: 0x000ECD85
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x17007749 RID: 30537
			// (set) Token: 0x0600A4E8 RID: 42216 RVA: 0x000EEB98 File Offset: 0x000ECD98
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700774A RID: 30538
			// (set) Token: 0x0600A4E9 RID: 42217 RVA: 0x000EEBAB File Offset: 0x000ECDAB
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700774B RID: 30539
			// (set) Token: 0x0600A4EA RID: 42218 RVA: 0x000EEBBE File Offset: 0x000ECDBE
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700774C RID: 30540
			// (set) Token: 0x0600A4EB RID: 42219 RVA: 0x000EEBD6 File Offset: 0x000ECDD6
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700774D RID: 30541
			// (set) Token: 0x0600A4EC RID: 42220 RVA: 0x000EEBF4 File Offset: 0x000ECDF4
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700774E RID: 30542
			// (set) Token: 0x0600A4ED RID: 42221 RVA: 0x000EEC07 File Offset: 0x000ECE07
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700774F RID: 30543
			// (set) Token: 0x0600A4EE RID: 42222 RVA: 0x000EEC1F File Offset: 0x000ECE1F
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007750 RID: 30544
			// (set) Token: 0x0600A4EF RID: 42223 RVA: 0x000EEC37 File Offset: 0x000ECE37
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007751 RID: 30545
			// (set) Token: 0x0600A4F0 RID: 42224 RVA: 0x000EEC4A File Offset: 0x000ECE4A
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007752 RID: 30546
			// (set) Token: 0x0600A4F1 RID: 42225 RVA: 0x000EEC5D File Offset: 0x000ECE5D
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007753 RID: 30547
			// (set) Token: 0x0600A4F2 RID: 42226 RVA: 0x000EEC75 File Offset: 0x000ECE75
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007754 RID: 30548
			// (set) Token: 0x0600A4F3 RID: 42227 RVA: 0x000EEC88 File Offset: 0x000ECE88
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007755 RID: 30549
			// (set) Token: 0x0600A4F4 RID: 42228 RVA: 0x000EEC9B File Offset: 0x000ECE9B
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007756 RID: 30550
			// (set) Token: 0x0600A4F5 RID: 42229 RVA: 0x000EECAE File Offset: 0x000ECEAE
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007757 RID: 30551
			// (set) Token: 0x0600A4F6 RID: 42230 RVA: 0x000EECCC File Offset: 0x000ECECC
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007758 RID: 30552
			// (set) Token: 0x0600A4F7 RID: 42231 RVA: 0x000EECEA File Offset: 0x000ECEEA
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007759 RID: 30553
			// (set) Token: 0x0600A4F8 RID: 42232 RVA: 0x000EED08 File Offset: 0x000ECF08
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700775A RID: 30554
			// (set) Token: 0x0600A4F9 RID: 42233 RVA: 0x000EED26 File Offset: 0x000ECF26
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700775B RID: 30555
			// (set) Token: 0x0600A4FA RID: 42234 RVA: 0x000EED44 File Offset: 0x000ECF44
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700775C RID: 30556
			// (set) Token: 0x0600A4FB RID: 42235 RVA: 0x000EED57 File Offset: 0x000ECF57
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700775D RID: 30557
			// (set) Token: 0x0600A4FC RID: 42236 RVA: 0x000EED6F File Offset: 0x000ECF6F
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700775E RID: 30558
			// (set) Token: 0x0600A4FD RID: 42237 RVA: 0x000EED8D File Offset: 0x000ECF8D
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700775F RID: 30559
			// (set) Token: 0x0600A4FE RID: 42238 RVA: 0x000EEDA5 File Offset: 0x000ECFA5
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007760 RID: 30560
			// (set) Token: 0x0600A4FF RID: 42239 RVA: 0x000EEDBD File Offset: 0x000ECFBD
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007761 RID: 30561
			// (set) Token: 0x0600A500 RID: 42240 RVA: 0x000EEDD0 File Offset: 0x000ECFD0
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007762 RID: 30562
			// (set) Token: 0x0600A501 RID: 42241 RVA: 0x000EEDE8 File Offset: 0x000ECFE8
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007763 RID: 30563
			// (set) Token: 0x0600A502 RID: 42242 RVA: 0x000EEE00 File Offset: 0x000ED000
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007764 RID: 30564
			// (set) Token: 0x0600A503 RID: 42243 RVA: 0x000EEE18 File Offset: 0x000ED018
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007765 RID: 30565
			// (set) Token: 0x0600A504 RID: 42244 RVA: 0x000EEE2B File Offset: 0x000ED02B
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007766 RID: 30566
			// (set) Token: 0x0600A505 RID: 42245 RVA: 0x000EEE3E File Offset: 0x000ED03E
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007767 RID: 30567
			// (set) Token: 0x0600A506 RID: 42246 RVA: 0x000EEE51 File Offset: 0x000ED051
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007768 RID: 30568
			// (set) Token: 0x0600A507 RID: 42247 RVA: 0x000EEE64 File Offset: 0x000ED064
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007769 RID: 30569
			// (set) Token: 0x0600A508 RID: 42248 RVA: 0x000EEE7C File Offset: 0x000ED07C
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700776A RID: 30570
			// (set) Token: 0x0600A509 RID: 42249 RVA: 0x000EEE8F File Offset: 0x000ED08F
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700776B RID: 30571
			// (set) Token: 0x0600A50A RID: 42250 RVA: 0x000EEEA2 File Offset: 0x000ED0A2
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700776C RID: 30572
			// (set) Token: 0x0600A50B RID: 42251 RVA: 0x000EEEBA File Offset: 0x000ED0BA
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700776D RID: 30573
			// (set) Token: 0x0600A50C RID: 42252 RVA: 0x000EEECD File Offset: 0x000ED0CD
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700776E RID: 30574
			// (set) Token: 0x0600A50D RID: 42253 RVA: 0x000EEEE5 File Offset: 0x000ED0E5
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700776F RID: 30575
			// (set) Token: 0x0600A50E RID: 42254 RVA: 0x000EEEF8 File Offset: 0x000ED0F8
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007770 RID: 30576
			// (set) Token: 0x0600A50F RID: 42255 RVA: 0x000EEF16 File Offset: 0x000ED116
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007771 RID: 30577
			// (set) Token: 0x0600A510 RID: 42256 RVA: 0x000EEF29 File Offset: 0x000ED129
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007772 RID: 30578
			// (set) Token: 0x0600A511 RID: 42257 RVA: 0x000EEF47 File Offset: 0x000ED147
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007773 RID: 30579
			// (set) Token: 0x0600A512 RID: 42258 RVA: 0x000EEF5A File Offset: 0x000ED15A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007774 RID: 30580
			// (set) Token: 0x0600A513 RID: 42259 RVA: 0x000EEF72 File Offset: 0x000ED172
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007775 RID: 30581
			// (set) Token: 0x0600A514 RID: 42260 RVA: 0x000EEF8A File Offset: 0x000ED18A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007776 RID: 30582
			// (set) Token: 0x0600A515 RID: 42261 RVA: 0x000EEFA2 File Offset: 0x000ED1A2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007777 RID: 30583
			// (set) Token: 0x0600A516 RID: 42262 RVA: 0x000EEFBA File Offset: 0x000ED1BA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA5 RID: 3237
		public class UserParameters : ParametersBase
		{
			// Token: 0x17007778 RID: 30584
			// (set) Token: 0x0600A518 RID: 42264 RVA: 0x000EEFDA File Offset: 0x000ED1DA
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x17007779 RID: 30585
			// (set) Token: 0x0600A519 RID: 42265 RVA: 0x000EEFF8 File Offset: 0x000ED1F8
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x1700777A RID: 30586
			// (set) Token: 0x0600A51A RID: 42266 RVA: 0x000EF010 File Offset: 0x000ED210
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x1700777B RID: 30587
			// (set) Token: 0x0600A51B RID: 42267 RVA: 0x000EF023 File Offset: 0x000ED223
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x1700777C RID: 30588
			// (set) Token: 0x0600A51C RID: 42268 RVA: 0x000EF03B File Offset: 0x000ED23B
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700777D RID: 30589
			// (set) Token: 0x0600A51D RID: 42269 RVA: 0x000EF04E File Offset: 0x000ED24E
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700777E RID: 30590
			// (set) Token: 0x0600A51E RID: 42270 RVA: 0x000EF061 File Offset: 0x000ED261
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700777F RID: 30591
			// (set) Token: 0x0600A51F RID: 42271 RVA: 0x000EF074 File Offset: 0x000ED274
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007780 RID: 30592
			// (set) Token: 0x0600A520 RID: 42272 RVA: 0x000EF092 File Offset: 0x000ED292
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007781 RID: 30593
			// (set) Token: 0x0600A521 RID: 42273 RVA: 0x000EF0A5 File Offset: 0x000ED2A5
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007782 RID: 30594
			// (set) Token: 0x0600A522 RID: 42274 RVA: 0x000EF0BD File Offset: 0x000ED2BD
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007783 RID: 30595
			// (set) Token: 0x0600A523 RID: 42275 RVA: 0x000EF0D5 File Offset: 0x000ED2D5
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007784 RID: 30596
			// (set) Token: 0x0600A524 RID: 42276 RVA: 0x000EF0F3 File Offset: 0x000ED2F3
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007785 RID: 30597
			// (set) Token: 0x0600A525 RID: 42277 RVA: 0x000EF106 File Offset: 0x000ED306
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007786 RID: 30598
			// (set) Token: 0x0600A526 RID: 42278 RVA: 0x000EF119 File Offset: 0x000ED319
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007787 RID: 30599
			// (set) Token: 0x0600A527 RID: 42279 RVA: 0x000EF131 File Offset: 0x000ED331
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007788 RID: 30600
			// (set) Token: 0x0600A528 RID: 42280 RVA: 0x000EF144 File Offset: 0x000ED344
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007789 RID: 30601
			// (set) Token: 0x0600A529 RID: 42281 RVA: 0x000EF157 File Offset: 0x000ED357
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700778A RID: 30602
			// (set) Token: 0x0600A52A RID: 42282 RVA: 0x000EF16A File Offset: 0x000ED36A
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700778B RID: 30603
			// (set) Token: 0x0600A52B RID: 42283 RVA: 0x000EF188 File Offset: 0x000ED388
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700778C RID: 30604
			// (set) Token: 0x0600A52C RID: 42284 RVA: 0x000EF1A6 File Offset: 0x000ED3A6
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700778D RID: 30605
			// (set) Token: 0x0600A52D RID: 42285 RVA: 0x000EF1C4 File Offset: 0x000ED3C4
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700778E RID: 30606
			// (set) Token: 0x0600A52E RID: 42286 RVA: 0x000EF1E2 File Offset: 0x000ED3E2
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700778F RID: 30607
			// (set) Token: 0x0600A52F RID: 42287 RVA: 0x000EF200 File Offset: 0x000ED400
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007790 RID: 30608
			// (set) Token: 0x0600A530 RID: 42288 RVA: 0x000EF213 File Offset: 0x000ED413
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007791 RID: 30609
			// (set) Token: 0x0600A531 RID: 42289 RVA: 0x000EF22B File Offset: 0x000ED42B
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007792 RID: 30610
			// (set) Token: 0x0600A532 RID: 42290 RVA: 0x000EF249 File Offset: 0x000ED449
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007793 RID: 30611
			// (set) Token: 0x0600A533 RID: 42291 RVA: 0x000EF261 File Offset: 0x000ED461
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007794 RID: 30612
			// (set) Token: 0x0600A534 RID: 42292 RVA: 0x000EF279 File Offset: 0x000ED479
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007795 RID: 30613
			// (set) Token: 0x0600A535 RID: 42293 RVA: 0x000EF28C File Offset: 0x000ED48C
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007796 RID: 30614
			// (set) Token: 0x0600A536 RID: 42294 RVA: 0x000EF2A4 File Offset: 0x000ED4A4
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007797 RID: 30615
			// (set) Token: 0x0600A537 RID: 42295 RVA: 0x000EF2BC File Offset: 0x000ED4BC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007798 RID: 30616
			// (set) Token: 0x0600A538 RID: 42296 RVA: 0x000EF2D4 File Offset: 0x000ED4D4
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007799 RID: 30617
			// (set) Token: 0x0600A539 RID: 42297 RVA: 0x000EF2E7 File Offset: 0x000ED4E7
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700779A RID: 30618
			// (set) Token: 0x0600A53A RID: 42298 RVA: 0x000EF2FA File Offset: 0x000ED4FA
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700779B RID: 30619
			// (set) Token: 0x0600A53B RID: 42299 RVA: 0x000EF30D File Offset: 0x000ED50D
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700779C RID: 30620
			// (set) Token: 0x0600A53C RID: 42300 RVA: 0x000EF320 File Offset: 0x000ED520
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700779D RID: 30621
			// (set) Token: 0x0600A53D RID: 42301 RVA: 0x000EF338 File Offset: 0x000ED538
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700779E RID: 30622
			// (set) Token: 0x0600A53E RID: 42302 RVA: 0x000EF34B File Offset: 0x000ED54B
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700779F RID: 30623
			// (set) Token: 0x0600A53F RID: 42303 RVA: 0x000EF35E File Offset: 0x000ED55E
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170077A0 RID: 30624
			// (set) Token: 0x0600A540 RID: 42304 RVA: 0x000EF376 File Offset: 0x000ED576
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170077A1 RID: 30625
			// (set) Token: 0x0600A541 RID: 42305 RVA: 0x000EF389 File Offset: 0x000ED589
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170077A2 RID: 30626
			// (set) Token: 0x0600A542 RID: 42306 RVA: 0x000EF3A1 File Offset: 0x000ED5A1
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170077A3 RID: 30627
			// (set) Token: 0x0600A543 RID: 42307 RVA: 0x000EF3B4 File Offset: 0x000ED5B4
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170077A4 RID: 30628
			// (set) Token: 0x0600A544 RID: 42308 RVA: 0x000EF3D2 File Offset: 0x000ED5D2
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170077A5 RID: 30629
			// (set) Token: 0x0600A545 RID: 42309 RVA: 0x000EF3E5 File Offset: 0x000ED5E5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170077A6 RID: 30630
			// (set) Token: 0x0600A546 RID: 42310 RVA: 0x000EF403 File Offset: 0x000ED603
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170077A7 RID: 30631
			// (set) Token: 0x0600A547 RID: 42311 RVA: 0x000EF416 File Offset: 0x000ED616
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170077A8 RID: 30632
			// (set) Token: 0x0600A548 RID: 42312 RVA: 0x000EF42E File Offset: 0x000ED62E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170077A9 RID: 30633
			// (set) Token: 0x0600A549 RID: 42313 RVA: 0x000EF446 File Offset: 0x000ED646
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170077AA RID: 30634
			// (set) Token: 0x0600A54A RID: 42314 RVA: 0x000EF45E File Offset: 0x000ED65E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170077AB RID: 30635
			// (set) Token: 0x0600A54B RID: 42315 RVA: 0x000EF476 File Offset: 0x000ED676
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA6 RID: 3238
		public class WindowsLiveIDParameters : ParametersBase
		{
			// Token: 0x170077AC RID: 30636
			// (set) Token: 0x0600A54D RID: 42317 RVA: 0x000EF496 File Offset: 0x000ED696
			public virtual string MailboxPlan
			{
				set
				{
					base.PowerSharpParameters["MailboxPlan"] = ((value != null) ? new MailboxPlanIdParameter(value) : null);
				}
			}

			// Token: 0x170077AD RID: 30637
			// (set) Token: 0x0600A54E RID: 42318 RVA: 0x000EF4B4 File Offset: 0x000ED6B4
			public virtual Capability SKUCapability
			{
				set
				{
					base.PowerSharpParameters["SKUCapability"] = value;
				}
			}

			// Token: 0x170077AE RID: 30638
			// (set) Token: 0x0600A54F RID: 42319 RVA: 0x000EF4CC File Offset: 0x000ED6CC
			public virtual MultiValuedProperty<Capability> AddOnSKUCapability
			{
				set
				{
					base.PowerSharpParameters["AddOnSKUCapability"] = value;
				}
			}

			// Token: 0x170077AF RID: 30639
			// (set) Token: 0x0600A550 RID: 42320 RVA: 0x000EF4DF File Offset: 0x000ED6DF
			public virtual bool SKUAssigned
			{
				set
				{
					base.PowerSharpParameters["SKUAssigned"] = value;
				}
			}

			// Token: 0x170077B0 RID: 30640
			// (set) Token: 0x0600A551 RID: 42321 RVA: 0x000EF4F7 File Offset: 0x000ED6F7
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x170077B1 RID: 30641
			// (set) Token: 0x0600A552 RID: 42322 RVA: 0x000EF50A File Offset: 0x000ED70A
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170077B2 RID: 30642
			// (set) Token: 0x0600A553 RID: 42323 RVA: 0x000EF51D File Offset: 0x000ED71D
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170077B3 RID: 30643
			// (set) Token: 0x0600A554 RID: 42324 RVA: 0x000EF53B File Offset: 0x000ED73B
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170077B4 RID: 30644
			// (set) Token: 0x0600A555 RID: 42325 RVA: 0x000EF54E File Offset: 0x000ED74E
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170077B5 RID: 30645
			// (set) Token: 0x0600A556 RID: 42326 RVA: 0x000EF566 File Offset: 0x000ED766
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170077B6 RID: 30646
			// (set) Token: 0x0600A557 RID: 42327 RVA: 0x000EF57E File Offset: 0x000ED77E
			public virtual string RemovedMailbox
			{
				set
				{
					base.PowerSharpParameters["RemovedMailbox"] = ((value != null) ? new RemovedMailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170077B7 RID: 30647
			// (set) Token: 0x0600A558 RID: 42328 RVA: 0x000EF59C File Offset: 0x000ED79C
			public virtual WindowsLiveId WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x170077B8 RID: 30648
			// (set) Token: 0x0600A559 RID: 42329 RVA: 0x000EF5AF File Offset: 0x000ED7AF
			public virtual SwitchParameter EvictLiveId
			{
				set
				{
					base.PowerSharpParameters["EvictLiveId"] = value;
				}
			}

			// Token: 0x170077B9 RID: 30649
			// (set) Token: 0x0600A55A RID: 42330 RVA: 0x000EF5C7 File Offset: 0x000ED7C7
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170077BA RID: 30650
			// (set) Token: 0x0600A55B RID: 42331 RVA: 0x000EF5DA File Offset: 0x000ED7DA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170077BB RID: 30651
			// (set) Token: 0x0600A55C RID: 42332 RVA: 0x000EF5ED File Offset: 0x000ED7ED
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170077BC RID: 30652
			// (set) Token: 0x0600A55D RID: 42333 RVA: 0x000EF605 File Offset: 0x000ED805
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170077BD RID: 30653
			// (set) Token: 0x0600A55E RID: 42334 RVA: 0x000EF618 File Offset: 0x000ED818
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170077BE RID: 30654
			// (set) Token: 0x0600A55F RID: 42335 RVA: 0x000EF62B File Offset: 0x000ED82B
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170077BF RID: 30655
			// (set) Token: 0x0600A560 RID: 42336 RVA: 0x000EF63E File Offset: 0x000ED83E
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077C0 RID: 30656
			// (set) Token: 0x0600A561 RID: 42337 RVA: 0x000EF65C File Offset: 0x000ED85C
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077C1 RID: 30657
			// (set) Token: 0x0600A562 RID: 42338 RVA: 0x000EF67A File Offset: 0x000ED87A
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077C2 RID: 30658
			// (set) Token: 0x0600A563 RID: 42339 RVA: 0x000EF698 File Offset: 0x000ED898
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077C3 RID: 30659
			// (set) Token: 0x0600A564 RID: 42340 RVA: 0x000EF6B6 File Offset: 0x000ED8B6
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077C4 RID: 30660
			// (set) Token: 0x0600A565 RID: 42341 RVA: 0x000EF6D4 File Offset: 0x000ED8D4
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170077C5 RID: 30661
			// (set) Token: 0x0600A566 RID: 42342 RVA: 0x000EF6E7 File Offset: 0x000ED8E7
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170077C6 RID: 30662
			// (set) Token: 0x0600A567 RID: 42343 RVA: 0x000EF6FF File Offset: 0x000ED8FF
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077C7 RID: 30663
			// (set) Token: 0x0600A568 RID: 42344 RVA: 0x000EF71D File Offset: 0x000ED91D
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170077C8 RID: 30664
			// (set) Token: 0x0600A569 RID: 42345 RVA: 0x000EF735 File Offset: 0x000ED935
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170077C9 RID: 30665
			// (set) Token: 0x0600A56A RID: 42346 RVA: 0x000EF74D File Offset: 0x000ED94D
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170077CA RID: 30666
			// (set) Token: 0x0600A56B RID: 42347 RVA: 0x000EF760 File Offset: 0x000ED960
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170077CB RID: 30667
			// (set) Token: 0x0600A56C RID: 42348 RVA: 0x000EF778 File Offset: 0x000ED978
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170077CC RID: 30668
			// (set) Token: 0x0600A56D RID: 42349 RVA: 0x000EF790 File Offset: 0x000ED990
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170077CD RID: 30669
			// (set) Token: 0x0600A56E RID: 42350 RVA: 0x000EF7A8 File Offset: 0x000ED9A8
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170077CE RID: 30670
			// (set) Token: 0x0600A56F RID: 42351 RVA: 0x000EF7BB File Offset: 0x000ED9BB
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170077CF RID: 30671
			// (set) Token: 0x0600A570 RID: 42352 RVA: 0x000EF7CE File Offset: 0x000ED9CE
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170077D0 RID: 30672
			// (set) Token: 0x0600A571 RID: 42353 RVA: 0x000EF7E1 File Offset: 0x000ED9E1
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170077D1 RID: 30673
			// (set) Token: 0x0600A572 RID: 42354 RVA: 0x000EF7F4 File Offset: 0x000ED9F4
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170077D2 RID: 30674
			// (set) Token: 0x0600A573 RID: 42355 RVA: 0x000EF80C File Offset: 0x000EDA0C
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170077D3 RID: 30675
			// (set) Token: 0x0600A574 RID: 42356 RVA: 0x000EF81F File Offset: 0x000EDA1F
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170077D4 RID: 30676
			// (set) Token: 0x0600A575 RID: 42357 RVA: 0x000EF832 File Offset: 0x000EDA32
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170077D5 RID: 30677
			// (set) Token: 0x0600A576 RID: 42358 RVA: 0x000EF84A File Offset: 0x000EDA4A
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170077D6 RID: 30678
			// (set) Token: 0x0600A577 RID: 42359 RVA: 0x000EF85D File Offset: 0x000EDA5D
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170077D7 RID: 30679
			// (set) Token: 0x0600A578 RID: 42360 RVA: 0x000EF875 File Offset: 0x000EDA75
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170077D8 RID: 30680
			// (set) Token: 0x0600A579 RID: 42361 RVA: 0x000EF888 File Offset: 0x000EDA88
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170077D9 RID: 30681
			// (set) Token: 0x0600A57A RID: 42362 RVA: 0x000EF8A6 File Offset: 0x000EDAA6
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170077DA RID: 30682
			// (set) Token: 0x0600A57B RID: 42363 RVA: 0x000EF8B9 File Offset: 0x000EDAB9
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170077DB RID: 30683
			// (set) Token: 0x0600A57C RID: 42364 RVA: 0x000EF8D7 File Offset: 0x000EDAD7
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170077DC RID: 30684
			// (set) Token: 0x0600A57D RID: 42365 RVA: 0x000EF8EA File Offset: 0x000EDAEA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170077DD RID: 30685
			// (set) Token: 0x0600A57E RID: 42366 RVA: 0x000EF902 File Offset: 0x000EDB02
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170077DE RID: 30686
			// (set) Token: 0x0600A57F RID: 42367 RVA: 0x000EF91A File Offset: 0x000EDB1A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170077DF RID: 30687
			// (set) Token: 0x0600A580 RID: 42368 RVA: 0x000EF932 File Offset: 0x000EDB32
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170077E0 RID: 30688
			// (set) Token: 0x0600A581 RID: 42369 RVA: 0x000EF94A File Offset: 0x000EDB4A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA7 RID: 3239
		public class DiscoveryParameters : ParametersBase
		{
			// Token: 0x170077E1 RID: 30689
			// (set) Token: 0x0600A583 RID: 42371 RVA: 0x000EF96A File Offset: 0x000EDB6A
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170077E2 RID: 30690
			// (set) Token: 0x0600A584 RID: 42372 RVA: 0x000EF97D File Offset: 0x000EDB7D
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170077E3 RID: 30691
			// (set) Token: 0x0600A585 RID: 42373 RVA: 0x000EF990 File Offset: 0x000EDB90
			public virtual SwitchParameter Discovery
			{
				set
				{
					base.PowerSharpParameters["Discovery"] = value;
				}
			}

			// Token: 0x170077E4 RID: 30692
			// (set) Token: 0x0600A586 RID: 42374 RVA: 0x000EF9A8 File Offset: 0x000EDBA8
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170077E5 RID: 30693
			// (set) Token: 0x0600A587 RID: 42375 RVA: 0x000EF9BB File Offset: 0x000EDBBB
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170077E6 RID: 30694
			// (set) Token: 0x0600A588 RID: 42376 RVA: 0x000EF9CE File Offset: 0x000EDBCE
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170077E7 RID: 30695
			// (set) Token: 0x0600A589 RID: 42377 RVA: 0x000EF9E6 File Offset: 0x000EDBE6
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170077E8 RID: 30696
			// (set) Token: 0x0600A58A RID: 42378 RVA: 0x000EF9F9 File Offset: 0x000EDBF9
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170077E9 RID: 30697
			// (set) Token: 0x0600A58B RID: 42379 RVA: 0x000EFA0C File Offset: 0x000EDC0C
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170077EA RID: 30698
			// (set) Token: 0x0600A58C RID: 42380 RVA: 0x000EFA1F File Offset: 0x000EDC1F
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077EB RID: 30699
			// (set) Token: 0x0600A58D RID: 42381 RVA: 0x000EFA3D File Offset: 0x000EDC3D
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077EC RID: 30700
			// (set) Token: 0x0600A58E RID: 42382 RVA: 0x000EFA5B File Offset: 0x000EDC5B
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077ED RID: 30701
			// (set) Token: 0x0600A58F RID: 42383 RVA: 0x000EFA79 File Offset: 0x000EDC79
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077EE RID: 30702
			// (set) Token: 0x0600A590 RID: 42384 RVA: 0x000EFA97 File Offset: 0x000EDC97
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077EF RID: 30703
			// (set) Token: 0x0600A591 RID: 42385 RVA: 0x000EFAB5 File Offset: 0x000EDCB5
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170077F0 RID: 30704
			// (set) Token: 0x0600A592 RID: 42386 RVA: 0x000EFAC8 File Offset: 0x000EDCC8
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170077F1 RID: 30705
			// (set) Token: 0x0600A593 RID: 42387 RVA: 0x000EFAE0 File Offset: 0x000EDCE0
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170077F2 RID: 30706
			// (set) Token: 0x0600A594 RID: 42388 RVA: 0x000EFAFE File Offset: 0x000EDCFE
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170077F3 RID: 30707
			// (set) Token: 0x0600A595 RID: 42389 RVA: 0x000EFB16 File Offset: 0x000EDD16
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170077F4 RID: 30708
			// (set) Token: 0x0600A596 RID: 42390 RVA: 0x000EFB2E File Offset: 0x000EDD2E
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170077F5 RID: 30709
			// (set) Token: 0x0600A597 RID: 42391 RVA: 0x000EFB41 File Offset: 0x000EDD41
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170077F6 RID: 30710
			// (set) Token: 0x0600A598 RID: 42392 RVA: 0x000EFB59 File Offset: 0x000EDD59
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170077F7 RID: 30711
			// (set) Token: 0x0600A599 RID: 42393 RVA: 0x000EFB71 File Offset: 0x000EDD71
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170077F8 RID: 30712
			// (set) Token: 0x0600A59A RID: 42394 RVA: 0x000EFB89 File Offset: 0x000EDD89
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170077F9 RID: 30713
			// (set) Token: 0x0600A59B RID: 42395 RVA: 0x000EFB9C File Offset: 0x000EDD9C
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170077FA RID: 30714
			// (set) Token: 0x0600A59C RID: 42396 RVA: 0x000EFBAF File Offset: 0x000EDDAF
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170077FB RID: 30715
			// (set) Token: 0x0600A59D RID: 42397 RVA: 0x000EFBC2 File Offset: 0x000EDDC2
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170077FC RID: 30716
			// (set) Token: 0x0600A59E RID: 42398 RVA: 0x000EFBD5 File Offset: 0x000EDDD5
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170077FD RID: 30717
			// (set) Token: 0x0600A59F RID: 42399 RVA: 0x000EFBED File Offset: 0x000EDDED
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170077FE RID: 30718
			// (set) Token: 0x0600A5A0 RID: 42400 RVA: 0x000EFC00 File Offset: 0x000EDE00
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170077FF RID: 30719
			// (set) Token: 0x0600A5A1 RID: 42401 RVA: 0x000EFC13 File Offset: 0x000EDE13
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007800 RID: 30720
			// (set) Token: 0x0600A5A2 RID: 42402 RVA: 0x000EFC2B File Offset: 0x000EDE2B
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007801 RID: 30721
			// (set) Token: 0x0600A5A3 RID: 42403 RVA: 0x000EFC3E File Offset: 0x000EDE3E
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007802 RID: 30722
			// (set) Token: 0x0600A5A4 RID: 42404 RVA: 0x000EFC56 File Offset: 0x000EDE56
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007803 RID: 30723
			// (set) Token: 0x0600A5A5 RID: 42405 RVA: 0x000EFC69 File Offset: 0x000EDE69
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007804 RID: 30724
			// (set) Token: 0x0600A5A6 RID: 42406 RVA: 0x000EFC87 File Offset: 0x000EDE87
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007805 RID: 30725
			// (set) Token: 0x0600A5A7 RID: 42407 RVA: 0x000EFC9A File Offset: 0x000EDE9A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007806 RID: 30726
			// (set) Token: 0x0600A5A8 RID: 42408 RVA: 0x000EFCB8 File Offset: 0x000EDEB8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007807 RID: 30727
			// (set) Token: 0x0600A5A9 RID: 42409 RVA: 0x000EFCCB File Offset: 0x000EDECB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007808 RID: 30728
			// (set) Token: 0x0600A5AA RID: 42410 RVA: 0x000EFCE3 File Offset: 0x000EDEE3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007809 RID: 30729
			// (set) Token: 0x0600A5AB RID: 42411 RVA: 0x000EFCFB File Offset: 0x000EDEFB
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700780A RID: 30730
			// (set) Token: 0x0600A5AC RID: 42412 RVA: 0x000EFD13 File Offset: 0x000EDF13
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700780B RID: 30731
			// (set) Token: 0x0600A5AD RID: 42413 RVA: 0x000EFD2B File Offset: 0x000EDF2B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA8 RID: 3240
		public class TeamMailboxIWParameters : ParametersBase
		{
			// Token: 0x1700780C RID: 30732
			// (set) Token: 0x0600A5AF RID: 42415 RVA: 0x000EFD4B File Offset: 0x000EDF4B
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700780D RID: 30733
			// (set) Token: 0x0600A5B0 RID: 42416 RVA: 0x000EFD5E File Offset: 0x000EDF5E
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700780E RID: 30734
			// (set) Token: 0x0600A5B1 RID: 42417 RVA: 0x000EFD71 File Offset: 0x000EDF71
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700780F RID: 30735
			// (set) Token: 0x0600A5B2 RID: 42418 RVA: 0x000EFD8F File Offset: 0x000EDF8F
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007810 RID: 30736
			// (set) Token: 0x0600A5B3 RID: 42419 RVA: 0x000EFDA2 File Offset: 0x000EDFA2
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007811 RID: 30737
			// (set) Token: 0x0600A5B4 RID: 42420 RVA: 0x000EFDBA File Offset: 0x000EDFBA
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007812 RID: 30738
			// (set) Token: 0x0600A5B5 RID: 42421 RVA: 0x000EFDD2 File Offset: 0x000EDFD2
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007813 RID: 30739
			// (set) Token: 0x0600A5B6 RID: 42422 RVA: 0x000EFDE5 File Offset: 0x000EDFE5
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007814 RID: 30740
			// (set) Token: 0x0600A5B7 RID: 42423 RVA: 0x000EFDF8 File Offset: 0x000EDFF8
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007815 RID: 30741
			// (set) Token: 0x0600A5B8 RID: 42424 RVA: 0x000EFE10 File Offset: 0x000EE010
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007816 RID: 30742
			// (set) Token: 0x0600A5B9 RID: 42425 RVA: 0x000EFE23 File Offset: 0x000EE023
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007817 RID: 30743
			// (set) Token: 0x0600A5BA RID: 42426 RVA: 0x000EFE36 File Offset: 0x000EE036
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007818 RID: 30744
			// (set) Token: 0x0600A5BB RID: 42427 RVA: 0x000EFE49 File Offset: 0x000EE049
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007819 RID: 30745
			// (set) Token: 0x0600A5BC RID: 42428 RVA: 0x000EFE67 File Offset: 0x000EE067
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700781A RID: 30746
			// (set) Token: 0x0600A5BD RID: 42429 RVA: 0x000EFE85 File Offset: 0x000EE085
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700781B RID: 30747
			// (set) Token: 0x0600A5BE RID: 42430 RVA: 0x000EFEA3 File Offset: 0x000EE0A3
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700781C RID: 30748
			// (set) Token: 0x0600A5BF RID: 42431 RVA: 0x000EFEC1 File Offset: 0x000EE0C1
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700781D RID: 30749
			// (set) Token: 0x0600A5C0 RID: 42432 RVA: 0x000EFEDF File Offset: 0x000EE0DF
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700781E RID: 30750
			// (set) Token: 0x0600A5C1 RID: 42433 RVA: 0x000EFEF2 File Offset: 0x000EE0F2
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700781F RID: 30751
			// (set) Token: 0x0600A5C2 RID: 42434 RVA: 0x000EFF0A File Offset: 0x000EE10A
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007820 RID: 30752
			// (set) Token: 0x0600A5C3 RID: 42435 RVA: 0x000EFF28 File Offset: 0x000EE128
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007821 RID: 30753
			// (set) Token: 0x0600A5C4 RID: 42436 RVA: 0x000EFF40 File Offset: 0x000EE140
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007822 RID: 30754
			// (set) Token: 0x0600A5C5 RID: 42437 RVA: 0x000EFF58 File Offset: 0x000EE158
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007823 RID: 30755
			// (set) Token: 0x0600A5C6 RID: 42438 RVA: 0x000EFF6B File Offset: 0x000EE16B
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007824 RID: 30756
			// (set) Token: 0x0600A5C7 RID: 42439 RVA: 0x000EFF83 File Offset: 0x000EE183
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007825 RID: 30757
			// (set) Token: 0x0600A5C8 RID: 42440 RVA: 0x000EFF9B File Offset: 0x000EE19B
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007826 RID: 30758
			// (set) Token: 0x0600A5C9 RID: 42441 RVA: 0x000EFFB3 File Offset: 0x000EE1B3
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007827 RID: 30759
			// (set) Token: 0x0600A5CA RID: 42442 RVA: 0x000EFFC6 File Offset: 0x000EE1C6
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007828 RID: 30760
			// (set) Token: 0x0600A5CB RID: 42443 RVA: 0x000EFFD9 File Offset: 0x000EE1D9
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007829 RID: 30761
			// (set) Token: 0x0600A5CC RID: 42444 RVA: 0x000EFFEC File Offset: 0x000EE1EC
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700782A RID: 30762
			// (set) Token: 0x0600A5CD RID: 42445 RVA: 0x000EFFFF File Offset: 0x000EE1FF
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700782B RID: 30763
			// (set) Token: 0x0600A5CE RID: 42446 RVA: 0x000F0017 File Offset: 0x000EE217
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700782C RID: 30764
			// (set) Token: 0x0600A5CF RID: 42447 RVA: 0x000F002A File Offset: 0x000EE22A
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700782D RID: 30765
			// (set) Token: 0x0600A5D0 RID: 42448 RVA: 0x000F003D File Offset: 0x000EE23D
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700782E RID: 30766
			// (set) Token: 0x0600A5D1 RID: 42449 RVA: 0x000F0055 File Offset: 0x000EE255
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700782F RID: 30767
			// (set) Token: 0x0600A5D2 RID: 42450 RVA: 0x000F0068 File Offset: 0x000EE268
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007830 RID: 30768
			// (set) Token: 0x0600A5D3 RID: 42451 RVA: 0x000F0080 File Offset: 0x000EE280
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007831 RID: 30769
			// (set) Token: 0x0600A5D4 RID: 42452 RVA: 0x000F0093 File Offset: 0x000EE293
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007832 RID: 30770
			// (set) Token: 0x0600A5D5 RID: 42453 RVA: 0x000F00B1 File Offset: 0x000EE2B1
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007833 RID: 30771
			// (set) Token: 0x0600A5D6 RID: 42454 RVA: 0x000F00C4 File Offset: 0x000EE2C4
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007834 RID: 30772
			// (set) Token: 0x0600A5D7 RID: 42455 RVA: 0x000F00E2 File Offset: 0x000EE2E2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007835 RID: 30773
			// (set) Token: 0x0600A5D8 RID: 42456 RVA: 0x000F00F5 File Offset: 0x000EE2F5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007836 RID: 30774
			// (set) Token: 0x0600A5D9 RID: 42457 RVA: 0x000F010D File Offset: 0x000EE30D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007837 RID: 30775
			// (set) Token: 0x0600A5DA RID: 42458 RVA: 0x000F0125 File Offset: 0x000EE325
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007838 RID: 30776
			// (set) Token: 0x0600A5DB RID: 42459 RVA: 0x000F013D File Offset: 0x000EE33D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007839 RID: 30777
			// (set) Token: 0x0600A5DC RID: 42460 RVA: 0x000F0155 File Offset: 0x000EE355
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CA9 RID: 3241
		public class ArbitrationParameters : ParametersBase
		{
			// Token: 0x1700783A RID: 30778
			// (set) Token: 0x0600A5DE RID: 42462 RVA: 0x000F0175 File Offset: 0x000EE375
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700783B RID: 30779
			// (set) Token: 0x0600A5DF RID: 42463 RVA: 0x000F0188 File Offset: 0x000EE388
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700783C RID: 30780
			// (set) Token: 0x0600A5E0 RID: 42464 RVA: 0x000F019B File Offset: 0x000EE39B
			public virtual SwitchParameter Arbitration
			{
				set
				{
					base.PowerSharpParameters["Arbitration"] = value;
				}
			}

			// Token: 0x1700783D RID: 30781
			// (set) Token: 0x0600A5E1 RID: 42465 RVA: 0x000F01B3 File Offset: 0x000EE3B3
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x1700783E RID: 30782
			// (set) Token: 0x0600A5E2 RID: 42466 RVA: 0x000F01C6 File Offset: 0x000EE3C6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700783F RID: 30783
			// (set) Token: 0x0600A5E3 RID: 42467 RVA: 0x000F01D9 File Offset: 0x000EE3D9
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007840 RID: 30784
			// (set) Token: 0x0600A5E4 RID: 42468 RVA: 0x000F01F1 File Offset: 0x000EE3F1
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007841 RID: 30785
			// (set) Token: 0x0600A5E5 RID: 42469 RVA: 0x000F0204 File Offset: 0x000EE404
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007842 RID: 30786
			// (set) Token: 0x0600A5E6 RID: 42470 RVA: 0x000F0217 File Offset: 0x000EE417
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007843 RID: 30787
			// (set) Token: 0x0600A5E7 RID: 42471 RVA: 0x000F022A File Offset: 0x000EE42A
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007844 RID: 30788
			// (set) Token: 0x0600A5E8 RID: 42472 RVA: 0x000F0248 File Offset: 0x000EE448
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007845 RID: 30789
			// (set) Token: 0x0600A5E9 RID: 42473 RVA: 0x000F0266 File Offset: 0x000EE466
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007846 RID: 30790
			// (set) Token: 0x0600A5EA RID: 42474 RVA: 0x000F0284 File Offset: 0x000EE484
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007847 RID: 30791
			// (set) Token: 0x0600A5EB RID: 42475 RVA: 0x000F02A2 File Offset: 0x000EE4A2
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007848 RID: 30792
			// (set) Token: 0x0600A5EC RID: 42476 RVA: 0x000F02C0 File Offset: 0x000EE4C0
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007849 RID: 30793
			// (set) Token: 0x0600A5ED RID: 42477 RVA: 0x000F02D3 File Offset: 0x000EE4D3
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700784A RID: 30794
			// (set) Token: 0x0600A5EE RID: 42478 RVA: 0x000F02EB File Offset: 0x000EE4EB
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700784B RID: 30795
			// (set) Token: 0x0600A5EF RID: 42479 RVA: 0x000F0309 File Offset: 0x000EE509
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700784C RID: 30796
			// (set) Token: 0x0600A5F0 RID: 42480 RVA: 0x000F0321 File Offset: 0x000EE521
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700784D RID: 30797
			// (set) Token: 0x0600A5F1 RID: 42481 RVA: 0x000F0339 File Offset: 0x000EE539
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700784E RID: 30798
			// (set) Token: 0x0600A5F2 RID: 42482 RVA: 0x000F034C File Offset: 0x000EE54C
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x1700784F RID: 30799
			// (set) Token: 0x0600A5F3 RID: 42483 RVA: 0x000F0364 File Offset: 0x000EE564
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007850 RID: 30800
			// (set) Token: 0x0600A5F4 RID: 42484 RVA: 0x000F037C File Offset: 0x000EE57C
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007851 RID: 30801
			// (set) Token: 0x0600A5F5 RID: 42485 RVA: 0x000F0394 File Offset: 0x000EE594
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007852 RID: 30802
			// (set) Token: 0x0600A5F6 RID: 42486 RVA: 0x000F03A7 File Offset: 0x000EE5A7
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007853 RID: 30803
			// (set) Token: 0x0600A5F7 RID: 42487 RVA: 0x000F03BA File Offset: 0x000EE5BA
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007854 RID: 30804
			// (set) Token: 0x0600A5F8 RID: 42488 RVA: 0x000F03CD File Offset: 0x000EE5CD
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007855 RID: 30805
			// (set) Token: 0x0600A5F9 RID: 42489 RVA: 0x000F03E0 File Offset: 0x000EE5E0
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007856 RID: 30806
			// (set) Token: 0x0600A5FA RID: 42490 RVA: 0x000F03F8 File Offset: 0x000EE5F8
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007857 RID: 30807
			// (set) Token: 0x0600A5FB RID: 42491 RVA: 0x000F040B File Offset: 0x000EE60B
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007858 RID: 30808
			// (set) Token: 0x0600A5FC RID: 42492 RVA: 0x000F041E File Offset: 0x000EE61E
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007859 RID: 30809
			// (set) Token: 0x0600A5FD RID: 42493 RVA: 0x000F0436 File Offset: 0x000EE636
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700785A RID: 30810
			// (set) Token: 0x0600A5FE RID: 42494 RVA: 0x000F0449 File Offset: 0x000EE649
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700785B RID: 30811
			// (set) Token: 0x0600A5FF RID: 42495 RVA: 0x000F0461 File Offset: 0x000EE661
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700785C RID: 30812
			// (set) Token: 0x0600A600 RID: 42496 RVA: 0x000F0474 File Offset: 0x000EE674
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700785D RID: 30813
			// (set) Token: 0x0600A601 RID: 42497 RVA: 0x000F0492 File Offset: 0x000EE692
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700785E RID: 30814
			// (set) Token: 0x0600A602 RID: 42498 RVA: 0x000F04A5 File Offset: 0x000EE6A5
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700785F RID: 30815
			// (set) Token: 0x0600A603 RID: 42499 RVA: 0x000F04C3 File Offset: 0x000EE6C3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007860 RID: 30816
			// (set) Token: 0x0600A604 RID: 42500 RVA: 0x000F04D6 File Offset: 0x000EE6D6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007861 RID: 30817
			// (set) Token: 0x0600A605 RID: 42501 RVA: 0x000F04EE File Offset: 0x000EE6EE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007862 RID: 30818
			// (set) Token: 0x0600A606 RID: 42502 RVA: 0x000F0506 File Offset: 0x000EE706
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007863 RID: 30819
			// (set) Token: 0x0600A607 RID: 42503 RVA: 0x000F051E File Offset: 0x000EE71E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007864 RID: 30820
			// (set) Token: 0x0600A608 RID: 42504 RVA: 0x000F0536 File Offset: 0x000EE736
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CAA RID: 3242
		public class EquipmentParameters : ParametersBase
		{
			// Token: 0x17007865 RID: 30821
			// (set) Token: 0x0600A60A RID: 42506 RVA: 0x000F0556 File Offset: 0x000EE756
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007866 RID: 30822
			// (set) Token: 0x0600A60B RID: 42507 RVA: 0x000F0569 File Offset: 0x000EE769
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007867 RID: 30823
			// (set) Token: 0x0600A60C RID: 42508 RVA: 0x000F057C File Offset: 0x000EE77C
			public virtual SwitchParameter Equipment
			{
				set
				{
					base.PowerSharpParameters["Equipment"] = value;
				}
			}

			// Token: 0x17007868 RID: 30824
			// (set) Token: 0x0600A60D RID: 42509 RVA: 0x000F0594 File Offset: 0x000EE794
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007869 RID: 30825
			// (set) Token: 0x0600A60E RID: 42510 RVA: 0x000F05B2 File Offset: 0x000EE7B2
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x1700786A RID: 30826
			// (set) Token: 0x0600A60F RID: 42511 RVA: 0x000F05C5 File Offset: 0x000EE7C5
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x1700786B RID: 30827
			// (set) Token: 0x0600A610 RID: 42512 RVA: 0x000F05DD File Offset: 0x000EE7DD
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x1700786C RID: 30828
			// (set) Token: 0x0600A611 RID: 42513 RVA: 0x000F05F5 File Offset: 0x000EE7F5
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x1700786D RID: 30829
			// (set) Token: 0x0600A612 RID: 42514 RVA: 0x000F0608 File Offset: 0x000EE808
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700786E RID: 30830
			// (set) Token: 0x0600A613 RID: 42515 RVA: 0x000F061B File Offset: 0x000EE81B
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700786F RID: 30831
			// (set) Token: 0x0600A614 RID: 42516 RVA: 0x000F0633 File Offset: 0x000EE833
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007870 RID: 30832
			// (set) Token: 0x0600A615 RID: 42517 RVA: 0x000F0646 File Offset: 0x000EE846
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007871 RID: 30833
			// (set) Token: 0x0600A616 RID: 42518 RVA: 0x000F0659 File Offset: 0x000EE859
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007872 RID: 30834
			// (set) Token: 0x0600A617 RID: 42519 RVA: 0x000F066C File Offset: 0x000EE86C
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007873 RID: 30835
			// (set) Token: 0x0600A618 RID: 42520 RVA: 0x000F068A File Offset: 0x000EE88A
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007874 RID: 30836
			// (set) Token: 0x0600A619 RID: 42521 RVA: 0x000F06A8 File Offset: 0x000EE8A8
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007875 RID: 30837
			// (set) Token: 0x0600A61A RID: 42522 RVA: 0x000F06C6 File Offset: 0x000EE8C6
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007876 RID: 30838
			// (set) Token: 0x0600A61B RID: 42523 RVA: 0x000F06E4 File Offset: 0x000EE8E4
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007877 RID: 30839
			// (set) Token: 0x0600A61C RID: 42524 RVA: 0x000F0702 File Offset: 0x000EE902
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007878 RID: 30840
			// (set) Token: 0x0600A61D RID: 42525 RVA: 0x000F0715 File Offset: 0x000EE915
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007879 RID: 30841
			// (set) Token: 0x0600A61E RID: 42526 RVA: 0x000F072D File Offset: 0x000EE92D
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700787A RID: 30842
			// (set) Token: 0x0600A61F RID: 42527 RVA: 0x000F074B File Offset: 0x000EE94B
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700787B RID: 30843
			// (set) Token: 0x0600A620 RID: 42528 RVA: 0x000F0763 File Offset: 0x000EE963
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x1700787C RID: 30844
			// (set) Token: 0x0600A621 RID: 42529 RVA: 0x000F077B File Offset: 0x000EE97B
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x1700787D RID: 30845
			// (set) Token: 0x0600A622 RID: 42530 RVA: 0x000F078E File Offset: 0x000EE98E
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x1700787E RID: 30846
			// (set) Token: 0x0600A623 RID: 42531 RVA: 0x000F07A6 File Offset: 0x000EE9A6
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x1700787F RID: 30847
			// (set) Token: 0x0600A624 RID: 42532 RVA: 0x000F07BE File Offset: 0x000EE9BE
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007880 RID: 30848
			// (set) Token: 0x0600A625 RID: 42533 RVA: 0x000F07D6 File Offset: 0x000EE9D6
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007881 RID: 30849
			// (set) Token: 0x0600A626 RID: 42534 RVA: 0x000F07E9 File Offset: 0x000EE9E9
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007882 RID: 30850
			// (set) Token: 0x0600A627 RID: 42535 RVA: 0x000F07FC File Offset: 0x000EE9FC
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007883 RID: 30851
			// (set) Token: 0x0600A628 RID: 42536 RVA: 0x000F080F File Offset: 0x000EEA0F
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007884 RID: 30852
			// (set) Token: 0x0600A629 RID: 42537 RVA: 0x000F0822 File Offset: 0x000EEA22
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007885 RID: 30853
			// (set) Token: 0x0600A62A RID: 42538 RVA: 0x000F083A File Offset: 0x000EEA3A
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007886 RID: 30854
			// (set) Token: 0x0600A62B RID: 42539 RVA: 0x000F084D File Offset: 0x000EEA4D
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007887 RID: 30855
			// (set) Token: 0x0600A62C RID: 42540 RVA: 0x000F0860 File Offset: 0x000EEA60
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007888 RID: 30856
			// (set) Token: 0x0600A62D RID: 42541 RVA: 0x000F0878 File Offset: 0x000EEA78
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007889 RID: 30857
			// (set) Token: 0x0600A62E RID: 42542 RVA: 0x000F088B File Offset: 0x000EEA8B
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700788A RID: 30858
			// (set) Token: 0x0600A62F RID: 42543 RVA: 0x000F08A3 File Offset: 0x000EEAA3
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700788B RID: 30859
			// (set) Token: 0x0600A630 RID: 42544 RVA: 0x000F08B6 File Offset: 0x000EEAB6
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700788C RID: 30860
			// (set) Token: 0x0600A631 RID: 42545 RVA: 0x000F08D4 File Offset: 0x000EEAD4
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700788D RID: 30861
			// (set) Token: 0x0600A632 RID: 42546 RVA: 0x000F08E7 File Offset: 0x000EEAE7
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700788E RID: 30862
			// (set) Token: 0x0600A633 RID: 42547 RVA: 0x000F0905 File Offset: 0x000EEB05
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700788F RID: 30863
			// (set) Token: 0x0600A634 RID: 42548 RVA: 0x000F0918 File Offset: 0x000EEB18
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007890 RID: 30864
			// (set) Token: 0x0600A635 RID: 42549 RVA: 0x000F0930 File Offset: 0x000EEB30
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007891 RID: 30865
			// (set) Token: 0x0600A636 RID: 42550 RVA: 0x000F0948 File Offset: 0x000EEB48
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007892 RID: 30866
			// (set) Token: 0x0600A637 RID: 42551 RVA: 0x000F0960 File Offset: 0x000EEB60
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007893 RID: 30867
			// (set) Token: 0x0600A638 RID: 42552 RVA: 0x000F0978 File Offset: 0x000EEB78
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CAB RID: 3243
		public class AuxMailboxParameters : ParametersBase
		{
			// Token: 0x17007894 RID: 30868
			// (set) Token: 0x0600A63A RID: 42554 RVA: 0x000F0998 File Offset: 0x000EEB98
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x17007895 RID: 30869
			// (set) Token: 0x0600A63B RID: 42555 RVA: 0x000F09AB File Offset: 0x000EEBAB
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007896 RID: 30870
			// (set) Token: 0x0600A63C RID: 42556 RVA: 0x000F09BE File Offset: 0x000EEBBE
			public virtual SwitchParameter AuxMailbox
			{
				set
				{
					base.PowerSharpParameters["AuxMailbox"] = value;
				}
			}

			// Token: 0x17007897 RID: 30871
			// (set) Token: 0x0600A63D RID: 42557 RVA: 0x000F09D6 File Offset: 0x000EEBD6
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007898 RID: 30872
			// (set) Token: 0x0600A63E RID: 42558 RVA: 0x000F09E9 File Offset: 0x000EEBE9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007899 RID: 30873
			// (set) Token: 0x0600A63F RID: 42559 RVA: 0x000F09FC File Offset: 0x000EEBFC
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x1700789A RID: 30874
			// (set) Token: 0x0600A640 RID: 42560 RVA: 0x000F0A14 File Offset: 0x000EEC14
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x1700789B RID: 30875
			// (set) Token: 0x0600A641 RID: 42561 RVA: 0x000F0A27 File Offset: 0x000EEC27
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700789C RID: 30876
			// (set) Token: 0x0600A642 RID: 42562 RVA: 0x000F0A3A File Offset: 0x000EEC3A
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700789D RID: 30877
			// (set) Token: 0x0600A643 RID: 42563 RVA: 0x000F0A4D File Offset: 0x000EEC4D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700789E RID: 30878
			// (set) Token: 0x0600A644 RID: 42564 RVA: 0x000F0A6B File Offset: 0x000EEC6B
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700789F RID: 30879
			// (set) Token: 0x0600A645 RID: 42565 RVA: 0x000F0A89 File Offset: 0x000EEC89
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078A0 RID: 30880
			// (set) Token: 0x0600A646 RID: 42566 RVA: 0x000F0AA7 File Offset: 0x000EECA7
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078A1 RID: 30881
			// (set) Token: 0x0600A647 RID: 42567 RVA: 0x000F0AC5 File Offset: 0x000EECC5
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078A2 RID: 30882
			// (set) Token: 0x0600A648 RID: 42568 RVA: 0x000F0AE3 File Offset: 0x000EECE3
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170078A3 RID: 30883
			// (set) Token: 0x0600A649 RID: 42569 RVA: 0x000F0AF6 File Offset: 0x000EECF6
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170078A4 RID: 30884
			// (set) Token: 0x0600A64A RID: 42570 RVA: 0x000F0B0E File Offset: 0x000EED0E
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078A5 RID: 30885
			// (set) Token: 0x0600A64B RID: 42571 RVA: 0x000F0B2C File Offset: 0x000EED2C
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170078A6 RID: 30886
			// (set) Token: 0x0600A64C RID: 42572 RVA: 0x000F0B44 File Offset: 0x000EED44
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170078A7 RID: 30887
			// (set) Token: 0x0600A64D RID: 42573 RVA: 0x000F0B5C File Offset: 0x000EED5C
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170078A8 RID: 30888
			// (set) Token: 0x0600A64E RID: 42574 RVA: 0x000F0B6F File Offset: 0x000EED6F
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170078A9 RID: 30889
			// (set) Token: 0x0600A64F RID: 42575 RVA: 0x000F0B87 File Offset: 0x000EED87
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170078AA RID: 30890
			// (set) Token: 0x0600A650 RID: 42576 RVA: 0x000F0B9F File Offset: 0x000EED9F
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170078AB RID: 30891
			// (set) Token: 0x0600A651 RID: 42577 RVA: 0x000F0BB7 File Offset: 0x000EEDB7
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170078AC RID: 30892
			// (set) Token: 0x0600A652 RID: 42578 RVA: 0x000F0BCA File Offset: 0x000EEDCA
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170078AD RID: 30893
			// (set) Token: 0x0600A653 RID: 42579 RVA: 0x000F0BDD File Offset: 0x000EEDDD
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170078AE RID: 30894
			// (set) Token: 0x0600A654 RID: 42580 RVA: 0x000F0BF0 File Offset: 0x000EEDF0
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170078AF RID: 30895
			// (set) Token: 0x0600A655 RID: 42581 RVA: 0x000F0C03 File Offset: 0x000EEE03
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170078B0 RID: 30896
			// (set) Token: 0x0600A656 RID: 42582 RVA: 0x000F0C1B File Offset: 0x000EEE1B
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170078B1 RID: 30897
			// (set) Token: 0x0600A657 RID: 42583 RVA: 0x000F0C2E File Offset: 0x000EEE2E
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170078B2 RID: 30898
			// (set) Token: 0x0600A658 RID: 42584 RVA: 0x000F0C41 File Offset: 0x000EEE41
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170078B3 RID: 30899
			// (set) Token: 0x0600A659 RID: 42585 RVA: 0x000F0C59 File Offset: 0x000EEE59
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170078B4 RID: 30900
			// (set) Token: 0x0600A65A RID: 42586 RVA: 0x000F0C6C File Offset: 0x000EEE6C
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170078B5 RID: 30901
			// (set) Token: 0x0600A65B RID: 42587 RVA: 0x000F0C84 File Offset: 0x000EEE84
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170078B6 RID: 30902
			// (set) Token: 0x0600A65C RID: 42588 RVA: 0x000F0C97 File Offset: 0x000EEE97
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170078B7 RID: 30903
			// (set) Token: 0x0600A65D RID: 42589 RVA: 0x000F0CB5 File Offset: 0x000EEEB5
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170078B8 RID: 30904
			// (set) Token: 0x0600A65E RID: 42590 RVA: 0x000F0CC8 File Offset: 0x000EEEC8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170078B9 RID: 30905
			// (set) Token: 0x0600A65F RID: 42591 RVA: 0x000F0CE6 File Offset: 0x000EEEE6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170078BA RID: 30906
			// (set) Token: 0x0600A660 RID: 42592 RVA: 0x000F0CF9 File Offset: 0x000EEEF9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170078BB RID: 30907
			// (set) Token: 0x0600A661 RID: 42593 RVA: 0x000F0D11 File Offset: 0x000EEF11
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170078BC RID: 30908
			// (set) Token: 0x0600A662 RID: 42594 RVA: 0x000F0D29 File Offset: 0x000EEF29
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170078BD RID: 30909
			// (set) Token: 0x0600A663 RID: 42595 RVA: 0x000F0D41 File Offset: 0x000EEF41
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170078BE RID: 30910
			// (set) Token: 0x0600A664 RID: 42596 RVA: 0x000F0D59 File Offset: 0x000EEF59
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CAC RID: 3244
		public class TeamMailboxITProParameters : ParametersBase
		{
			// Token: 0x170078BF RID: 30911
			// (set) Token: 0x0600A666 RID: 42598 RVA: 0x000F0D79 File Offset: 0x000EEF79
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170078C0 RID: 30912
			// (set) Token: 0x0600A667 RID: 42599 RVA: 0x000F0D8C File Offset: 0x000EEF8C
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170078C1 RID: 30913
			// (set) Token: 0x0600A668 RID: 42600 RVA: 0x000F0D9F File Offset: 0x000EEF9F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170078C2 RID: 30914
			// (set) Token: 0x0600A669 RID: 42601 RVA: 0x000F0DBD File Offset: 0x000EEFBD
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170078C3 RID: 30915
			// (set) Token: 0x0600A66A RID: 42602 RVA: 0x000F0DD0 File Offset: 0x000EEFD0
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170078C4 RID: 30916
			// (set) Token: 0x0600A66B RID: 42603 RVA: 0x000F0DE8 File Offset: 0x000EEFE8
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170078C5 RID: 30917
			// (set) Token: 0x0600A66C RID: 42604 RVA: 0x000F0E00 File Offset: 0x000EF000
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170078C6 RID: 30918
			// (set) Token: 0x0600A66D RID: 42605 RVA: 0x000F0E13 File Offset: 0x000EF013
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170078C7 RID: 30919
			// (set) Token: 0x0600A66E RID: 42606 RVA: 0x000F0E26 File Offset: 0x000EF026
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170078C8 RID: 30920
			// (set) Token: 0x0600A66F RID: 42607 RVA: 0x000F0E3E File Offset: 0x000EF03E
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170078C9 RID: 30921
			// (set) Token: 0x0600A670 RID: 42608 RVA: 0x000F0E51 File Offset: 0x000EF051
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170078CA RID: 30922
			// (set) Token: 0x0600A671 RID: 42609 RVA: 0x000F0E64 File Offset: 0x000EF064
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170078CB RID: 30923
			// (set) Token: 0x0600A672 RID: 42610 RVA: 0x000F0E77 File Offset: 0x000EF077
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078CC RID: 30924
			// (set) Token: 0x0600A673 RID: 42611 RVA: 0x000F0E95 File Offset: 0x000EF095
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078CD RID: 30925
			// (set) Token: 0x0600A674 RID: 42612 RVA: 0x000F0EB3 File Offset: 0x000EF0B3
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078CE RID: 30926
			// (set) Token: 0x0600A675 RID: 42613 RVA: 0x000F0ED1 File Offset: 0x000EF0D1
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078CF RID: 30927
			// (set) Token: 0x0600A676 RID: 42614 RVA: 0x000F0EEF File Offset: 0x000EF0EF
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078D0 RID: 30928
			// (set) Token: 0x0600A677 RID: 42615 RVA: 0x000F0F0D File Offset: 0x000EF10D
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170078D1 RID: 30929
			// (set) Token: 0x0600A678 RID: 42616 RVA: 0x000F0F20 File Offset: 0x000EF120
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170078D2 RID: 30930
			// (set) Token: 0x0600A679 RID: 42617 RVA: 0x000F0F38 File Offset: 0x000EF138
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078D3 RID: 30931
			// (set) Token: 0x0600A67A RID: 42618 RVA: 0x000F0F56 File Offset: 0x000EF156
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170078D4 RID: 30932
			// (set) Token: 0x0600A67B RID: 42619 RVA: 0x000F0F6E File Offset: 0x000EF16E
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170078D5 RID: 30933
			// (set) Token: 0x0600A67C RID: 42620 RVA: 0x000F0F86 File Offset: 0x000EF186
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170078D6 RID: 30934
			// (set) Token: 0x0600A67D RID: 42621 RVA: 0x000F0F99 File Offset: 0x000EF199
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170078D7 RID: 30935
			// (set) Token: 0x0600A67E RID: 42622 RVA: 0x000F0FB1 File Offset: 0x000EF1B1
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170078D8 RID: 30936
			// (set) Token: 0x0600A67F RID: 42623 RVA: 0x000F0FC9 File Offset: 0x000EF1C9
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170078D9 RID: 30937
			// (set) Token: 0x0600A680 RID: 42624 RVA: 0x000F0FE1 File Offset: 0x000EF1E1
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170078DA RID: 30938
			// (set) Token: 0x0600A681 RID: 42625 RVA: 0x000F0FF4 File Offset: 0x000EF1F4
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170078DB RID: 30939
			// (set) Token: 0x0600A682 RID: 42626 RVA: 0x000F1007 File Offset: 0x000EF207
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170078DC RID: 30940
			// (set) Token: 0x0600A683 RID: 42627 RVA: 0x000F101A File Offset: 0x000EF21A
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170078DD RID: 30941
			// (set) Token: 0x0600A684 RID: 42628 RVA: 0x000F102D File Offset: 0x000EF22D
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170078DE RID: 30942
			// (set) Token: 0x0600A685 RID: 42629 RVA: 0x000F1045 File Offset: 0x000EF245
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170078DF RID: 30943
			// (set) Token: 0x0600A686 RID: 42630 RVA: 0x000F1058 File Offset: 0x000EF258
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170078E0 RID: 30944
			// (set) Token: 0x0600A687 RID: 42631 RVA: 0x000F106B File Offset: 0x000EF26B
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170078E1 RID: 30945
			// (set) Token: 0x0600A688 RID: 42632 RVA: 0x000F1083 File Offset: 0x000EF283
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170078E2 RID: 30946
			// (set) Token: 0x0600A689 RID: 42633 RVA: 0x000F1096 File Offset: 0x000EF296
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170078E3 RID: 30947
			// (set) Token: 0x0600A68A RID: 42634 RVA: 0x000F10AE File Offset: 0x000EF2AE
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170078E4 RID: 30948
			// (set) Token: 0x0600A68B RID: 42635 RVA: 0x000F10C1 File Offset: 0x000EF2C1
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170078E5 RID: 30949
			// (set) Token: 0x0600A68C RID: 42636 RVA: 0x000F10DF File Offset: 0x000EF2DF
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170078E6 RID: 30950
			// (set) Token: 0x0600A68D RID: 42637 RVA: 0x000F10F2 File Offset: 0x000EF2F2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170078E7 RID: 30951
			// (set) Token: 0x0600A68E RID: 42638 RVA: 0x000F1110 File Offset: 0x000EF310
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170078E8 RID: 30952
			// (set) Token: 0x0600A68F RID: 42639 RVA: 0x000F1123 File Offset: 0x000EF323
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170078E9 RID: 30953
			// (set) Token: 0x0600A690 RID: 42640 RVA: 0x000F113B File Offset: 0x000EF33B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170078EA RID: 30954
			// (set) Token: 0x0600A691 RID: 42641 RVA: 0x000F1153 File Offset: 0x000EF353
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170078EB RID: 30955
			// (set) Token: 0x0600A692 RID: 42642 RVA: 0x000F116B File Offset: 0x000EF36B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170078EC RID: 30956
			// (set) Token: 0x0600A693 RID: 42643 RVA: 0x000F1183 File Offset: 0x000EF383
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CAD RID: 3245
		public class LinkedParameters : ParametersBase
		{
			// Token: 0x170078ED RID: 30957
			// (set) Token: 0x0600A695 RID: 42645 RVA: 0x000F11A3 File Offset: 0x000EF3A3
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x170078EE RID: 30958
			// (set) Token: 0x0600A696 RID: 42646 RVA: 0x000F11B6 File Offset: 0x000EF3B6
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170078EF RID: 30959
			// (set) Token: 0x0600A697 RID: 42647 RVA: 0x000F11C9 File Offset: 0x000EF3C9
			public virtual string LinkedMasterAccount
			{
				set
				{
					base.PowerSharpParameters["LinkedMasterAccount"] = ((value != null) ? new UserIdParameter(value) : null);
				}
			}

			// Token: 0x170078F0 RID: 30960
			// (set) Token: 0x0600A698 RID: 42648 RVA: 0x000F11E7 File Offset: 0x000EF3E7
			public virtual string LinkedDomainController
			{
				set
				{
					base.PowerSharpParameters["LinkedDomainController"] = value;
				}
			}

			// Token: 0x170078F1 RID: 30961
			// (set) Token: 0x0600A699 RID: 42649 RVA: 0x000F11FA File Offset: 0x000EF3FA
			public virtual PSCredential LinkedCredential
			{
				set
				{
					base.PowerSharpParameters["LinkedCredential"] = value;
				}
			}

			// Token: 0x170078F2 RID: 30962
			// (set) Token: 0x0600A69A RID: 42650 RVA: 0x000F120D File Offset: 0x000EF40D
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170078F3 RID: 30963
			// (set) Token: 0x0600A69B RID: 42651 RVA: 0x000F122B File Offset: 0x000EF42B
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170078F4 RID: 30964
			// (set) Token: 0x0600A69C RID: 42652 RVA: 0x000F123E File Offset: 0x000EF43E
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x170078F5 RID: 30965
			// (set) Token: 0x0600A69D RID: 42653 RVA: 0x000F1256 File Offset: 0x000EF456
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170078F6 RID: 30966
			// (set) Token: 0x0600A69E RID: 42654 RVA: 0x000F126E File Offset: 0x000EF46E
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170078F7 RID: 30967
			// (set) Token: 0x0600A69F RID: 42655 RVA: 0x000F1281 File Offset: 0x000EF481
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170078F8 RID: 30968
			// (set) Token: 0x0600A6A0 RID: 42656 RVA: 0x000F1294 File Offset: 0x000EF494
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170078F9 RID: 30969
			// (set) Token: 0x0600A6A1 RID: 42657 RVA: 0x000F12AC File Offset: 0x000EF4AC
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170078FA RID: 30970
			// (set) Token: 0x0600A6A2 RID: 42658 RVA: 0x000F12BF File Offset: 0x000EF4BF
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170078FB RID: 30971
			// (set) Token: 0x0600A6A3 RID: 42659 RVA: 0x000F12D2 File Offset: 0x000EF4D2
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170078FC RID: 30972
			// (set) Token: 0x0600A6A4 RID: 42660 RVA: 0x000F12E5 File Offset: 0x000EF4E5
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078FD RID: 30973
			// (set) Token: 0x0600A6A5 RID: 42661 RVA: 0x000F1303 File Offset: 0x000EF503
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078FE RID: 30974
			// (set) Token: 0x0600A6A6 RID: 42662 RVA: 0x000F1321 File Offset: 0x000EF521
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170078FF RID: 30975
			// (set) Token: 0x0600A6A7 RID: 42663 RVA: 0x000F133F File Offset: 0x000EF53F
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007900 RID: 30976
			// (set) Token: 0x0600A6A8 RID: 42664 RVA: 0x000F135D File Offset: 0x000EF55D
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007901 RID: 30977
			// (set) Token: 0x0600A6A9 RID: 42665 RVA: 0x000F137B File Offset: 0x000EF57B
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007902 RID: 30978
			// (set) Token: 0x0600A6AA RID: 42666 RVA: 0x000F138E File Offset: 0x000EF58E
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007903 RID: 30979
			// (set) Token: 0x0600A6AB RID: 42667 RVA: 0x000F13A6 File Offset: 0x000EF5A6
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007904 RID: 30980
			// (set) Token: 0x0600A6AC RID: 42668 RVA: 0x000F13C4 File Offset: 0x000EF5C4
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007905 RID: 30981
			// (set) Token: 0x0600A6AD RID: 42669 RVA: 0x000F13DC File Offset: 0x000EF5DC
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007906 RID: 30982
			// (set) Token: 0x0600A6AE RID: 42670 RVA: 0x000F13F4 File Offset: 0x000EF5F4
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007907 RID: 30983
			// (set) Token: 0x0600A6AF RID: 42671 RVA: 0x000F1407 File Offset: 0x000EF607
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007908 RID: 30984
			// (set) Token: 0x0600A6B0 RID: 42672 RVA: 0x000F141F File Offset: 0x000EF61F
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007909 RID: 30985
			// (set) Token: 0x0600A6B1 RID: 42673 RVA: 0x000F1437 File Offset: 0x000EF637
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700790A RID: 30986
			// (set) Token: 0x0600A6B2 RID: 42674 RVA: 0x000F144F File Offset: 0x000EF64F
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700790B RID: 30987
			// (set) Token: 0x0600A6B3 RID: 42675 RVA: 0x000F1462 File Offset: 0x000EF662
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700790C RID: 30988
			// (set) Token: 0x0600A6B4 RID: 42676 RVA: 0x000F1475 File Offset: 0x000EF675
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700790D RID: 30989
			// (set) Token: 0x0600A6B5 RID: 42677 RVA: 0x000F1488 File Offset: 0x000EF688
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700790E RID: 30990
			// (set) Token: 0x0600A6B6 RID: 42678 RVA: 0x000F149B File Offset: 0x000EF69B
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700790F RID: 30991
			// (set) Token: 0x0600A6B7 RID: 42679 RVA: 0x000F14B3 File Offset: 0x000EF6B3
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007910 RID: 30992
			// (set) Token: 0x0600A6B8 RID: 42680 RVA: 0x000F14C6 File Offset: 0x000EF6C6
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007911 RID: 30993
			// (set) Token: 0x0600A6B9 RID: 42681 RVA: 0x000F14D9 File Offset: 0x000EF6D9
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007912 RID: 30994
			// (set) Token: 0x0600A6BA RID: 42682 RVA: 0x000F14F1 File Offset: 0x000EF6F1
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007913 RID: 30995
			// (set) Token: 0x0600A6BB RID: 42683 RVA: 0x000F1504 File Offset: 0x000EF704
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007914 RID: 30996
			// (set) Token: 0x0600A6BC RID: 42684 RVA: 0x000F151C File Offset: 0x000EF71C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007915 RID: 30997
			// (set) Token: 0x0600A6BD RID: 42685 RVA: 0x000F152F File Offset: 0x000EF72F
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007916 RID: 30998
			// (set) Token: 0x0600A6BE RID: 42686 RVA: 0x000F154D File Offset: 0x000EF74D
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007917 RID: 30999
			// (set) Token: 0x0600A6BF RID: 42687 RVA: 0x000F1560 File Offset: 0x000EF760
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007918 RID: 31000
			// (set) Token: 0x0600A6C0 RID: 42688 RVA: 0x000F157E File Offset: 0x000EF77E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007919 RID: 31001
			// (set) Token: 0x0600A6C1 RID: 42689 RVA: 0x000F1591 File Offset: 0x000EF791
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700791A RID: 31002
			// (set) Token: 0x0600A6C2 RID: 42690 RVA: 0x000F15A9 File Offset: 0x000EF7A9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700791B RID: 31003
			// (set) Token: 0x0600A6C3 RID: 42691 RVA: 0x000F15C1 File Offset: 0x000EF7C1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700791C RID: 31004
			// (set) Token: 0x0600A6C4 RID: 42692 RVA: 0x000F15D9 File Offset: 0x000EF7D9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700791D RID: 31005
			// (set) Token: 0x0600A6C5 RID: 42693 RVA: 0x000F15F1 File Offset: 0x000EF7F1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CAE RID: 3246
		public class SharedParameters : ParametersBase
		{
			// Token: 0x1700791E RID: 31006
			// (set) Token: 0x0600A6C7 RID: 42695 RVA: 0x000F1611 File Offset: 0x000EF811
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700791F RID: 31007
			// (set) Token: 0x0600A6C8 RID: 42696 RVA: 0x000F1624 File Offset: 0x000EF824
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x17007920 RID: 31008
			// (set) Token: 0x0600A6C9 RID: 42697 RVA: 0x000F1637 File Offset: 0x000EF837
			public virtual SwitchParameter Shared
			{
				set
				{
					base.PowerSharpParameters["Shared"] = value;
				}
			}

			// Token: 0x17007921 RID: 31009
			// (set) Token: 0x0600A6CA RID: 42698 RVA: 0x000F164F File Offset: 0x000EF84F
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007922 RID: 31010
			// (set) Token: 0x0600A6CB RID: 42699 RVA: 0x000F166D File Offset: 0x000EF86D
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007923 RID: 31011
			// (set) Token: 0x0600A6CC RID: 42700 RVA: 0x000F1680 File Offset: 0x000EF880
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007924 RID: 31012
			// (set) Token: 0x0600A6CD RID: 42701 RVA: 0x000F1698 File Offset: 0x000EF898
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007925 RID: 31013
			// (set) Token: 0x0600A6CE RID: 42702 RVA: 0x000F16B0 File Offset: 0x000EF8B0
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007926 RID: 31014
			// (set) Token: 0x0600A6CF RID: 42703 RVA: 0x000F16C3 File Offset: 0x000EF8C3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007927 RID: 31015
			// (set) Token: 0x0600A6D0 RID: 42704 RVA: 0x000F16D6 File Offset: 0x000EF8D6
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007928 RID: 31016
			// (set) Token: 0x0600A6D1 RID: 42705 RVA: 0x000F16EE File Offset: 0x000EF8EE
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007929 RID: 31017
			// (set) Token: 0x0600A6D2 RID: 42706 RVA: 0x000F1701 File Offset: 0x000EF901
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x1700792A RID: 31018
			// (set) Token: 0x0600A6D3 RID: 42707 RVA: 0x000F1714 File Offset: 0x000EF914
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x1700792B RID: 31019
			// (set) Token: 0x0600A6D4 RID: 42708 RVA: 0x000F1727 File Offset: 0x000EF927
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700792C RID: 31020
			// (set) Token: 0x0600A6D5 RID: 42709 RVA: 0x000F1745 File Offset: 0x000EF945
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700792D RID: 31021
			// (set) Token: 0x0600A6D6 RID: 42710 RVA: 0x000F1763 File Offset: 0x000EF963
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700792E RID: 31022
			// (set) Token: 0x0600A6D7 RID: 42711 RVA: 0x000F1781 File Offset: 0x000EF981
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700792F RID: 31023
			// (set) Token: 0x0600A6D8 RID: 42712 RVA: 0x000F179F File Offset: 0x000EF99F
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007930 RID: 31024
			// (set) Token: 0x0600A6D9 RID: 42713 RVA: 0x000F17BD File Offset: 0x000EF9BD
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007931 RID: 31025
			// (set) Token: 0x0600A6DA RID: 42714 RVA: 0x000F17D0 File Offset: 0x000EF9D0
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007932 RID: 31026
			// (set) Token: 0x0600A6DB RID: 42715 RVA: 0x000F17E8 File Offset: 0x000EF9E8
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007933 RID: 31027
			// (set) Token: 0x0600A6DC RID: 42716 RVA: 0x000F1806 File Offset: 0x000EFA06
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007934 RID: 31028
			// (set) Token: 0x0600A6DD RID: 42717 RVA: 0x000F181E File Offset: 0x000EFA1E
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007935 RID: 31029
			// (set) Token: 0x0600A6DE RID: 42718 RVA: 0x000F1836 File Offset: 0x000EFA36
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007936 RID: 31030
			// (set) Token: 0x0600A6DF RID: 42719 RVA: 0x000F1849 File Offset: 0x000EFA49
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007937 RID: 31031
			// (set) Token: 0x0600A6E0 RID: 42720 RVA: 0x000F1861 File Offset: 0x000EFA61
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007938 RID: 31032
			// (set) Token: 0x0600A6E1 RID: 42721 RVA: 0x000F1879 File Offset: 0x000EFA79
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007939 RID: 31033
			// (set) Token: 0x0600A6E2 RID: 42722 RVA: 0x000F1891 File Offset: 0x000EFA91
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x1700793A RID: 31034
			// (set) Token: 0x0600A6E3 RID: 42723 RVA: 0x000F18A4 File Offset: 0x000EFAA4
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x1700793B RID: 31035
			// (set) Token: 0x0600A6E4 RID: 42724 RVA: 0x000F18B7 File Offset: 0x000EFAB7
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700793C RID: 31036
			// (set) Token: 0x0600A6E5 RID: 42725 RVA: 0x000F18CA File Offset: 0x000EFACA
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700793D RID: 31037
			// (set) Token: 0x0600A6E6 RID: 42726 RVA: 0x000F18DD File Offset: 0x000EFADD
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700793E RID: 31038
			// (set) Token: 0x0600A6E7 RID: 42727 RVA: 0x000F18F5 File Offset: 0x000EFAF5
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700793F RID: 31039
			// (set) Token: 0x0600A6E8 RID: 42728 RVA: 0x000F1908 File Offset: 0x000EFB08
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007940 RID: 31040
			// (set) Token: 0x0600A6E9 RID: 42729 RVA: 0x000F191B File Offset: 0x000EFB1B
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007941 RID: 31041
			// (set) Token: 0x0600A6EA RID: 42730 RVA: 0x000F1933 File Offset: 0x000EFB33
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007942 RID: 31042
			// (set) Token: 0x0600A6EB RID: 42731 RVA: 0x000F1946 File Offset: 0x000EFB46
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007943 RID: 31043
			// (set) Token: 0x0600A6EC RID: 42732 RVA: 0x000F195E File Offset: 0x000EFB5E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007944 RID: 31044
			// (set) Token: 0x0600A6ED RID: 42733 RVA: 0x000F1971 File Offset: 0x000EFB71
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007945 RID: 31045
			// (set) Token: 0x0600A6EE RID: 42734 RVA: 0x000F198F File Offset: 0x000EFB8F
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007946 RID: 31046
			// (set) Token: 0x0600A6EF RID: 42735 RVA: 0x000F19A2 File Offset: 0x000EFBA2
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007947 RID: 31047
			// (set) Token: 0x0600A6F0 RID: 42736 RVA: 0x000F19C0 File Offset: 0x000EFBC0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007948 RID: 31048
			// (set) Token: 0x0600A6F1 RID: 42737 RVA: 0x000F19D3 File Offset: 0x000EFBD3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007949 RID: 31049
			// (set) Token: 0x0600A6F2 RID: 42738 RVA: 0x000F19EB File Offset: 0x000EFBEB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700794A RID: 31050
			// (set) Token: 0x0600A6F3 RID: 42739 RVA: 0x000F1A03 File Offset: 0x000EFC03
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700794B RID: 31051
			// (set) Token: 0x0600A6F4 RID: 42740 RVA: 0x000F1A1B File Offset: 0x000EFC1B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700794C RID: 31052
			// (set) Token: 0x0600A6F5 RID: 42741 RVA: 0x000F1A33 File Offset: 0x000EFC33
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CAF RID: 3247
		public class LinkedWithSyncMailboxParameters : ParametersBase
		{
			// Token: 0x1700794D RID: 31053
			// (set) Token: 0x0600A6F7 RID: 42743 RVA: 0x000F1A53 File Offset: 0x000EFC53
			public virtual SecureString Password
			{
				set
				{
					base.PowerSharpParameters["Password"] = value;
				}
			}

			// Token: 0x1700794E RID: 31054
			// (set) Token: 0x0600A6F8 RID: 42744 RVA: 0x000F1A66 File Offset: 0x000EFC66
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700794F RID: 31055
			// (set) Token: 0x0600A6F9 RID: 42745 RVA: 0x000F1A79 File Offset: 0x000EFC79
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17007950 RID: 31056
			// (set) Token: 0x0600A6FA RID: 42746 RVA: 0x000F1A97 File Offset: 0x000EFC97
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x17007951 RID: 31057
			// (set) Token: 0x0600A6FB RID: 42747 RVA: 0x000F1AAA File Offset: 0x000EFCAA
			public virtual bool ModerationEnabled
			{
				set
				{
					base.PowerSharpParameters["ModerationEnabled"] = value;
				}
			}

			// Token: 0x17007952 RID: 31058
			// (set) Token: 0x0600A6FC RID: 42748 RVA: 0x000F1AC2 File Offset: 0x000EFCC2
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x17007953 RID: 31059
			// (set) Token: 0x0600A6FD RID: 42749 RVA: 0x000F1ADA File Offset: 0x000EFCDA
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007954 RID: 31060
			// (set) Token: 0x0600A6FE RID: 42750 RVA: 0x000F1AED File Offset: 0x000EFCED
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007955 RID: 31061
			// (set) Token: 0x0600A6FF RID: 42751 RVA: 0x000F1B00 File Offset: 0x000EFD00
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007956 RID: 31062
			// (set) Token: 0x0600A700 RID: 42752 RVA: 0x000F1B18 File Offset: 0x000EFD18
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007957 RID: 31063
			// (set) Token: 0x0600A701 RID: 42753 RVA: 0x000F1B2B File Offset: 0x000EFD2B
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007958 RID: 31064
			// (set) Token: 0x0600A702 RID: 42754 RVA: 0x000F1B3E File Offset: 0x000EFD3E
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007959 RID: 31065
			// (set) Token: 0x0600A703 RID: 42755 RVA: 0x000F1B51 File Offset: 0x000EFD51
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700795A RID: 31066
			// (set) Token: 0x0600A704 RID: 42756 RVA: 0x000F1B6F File Offset: 0x000EFD6F
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700795B RID: 31067
			// (set) Token: 0x0600A705 RID: 42757 RVA: 0x000F1B8D File Offset: 0x000EFD8D
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700795C RID: 31068
			// (set) Token: 0x0600A706 RID: 42758 RVA: 0x000F1BAB File Offset: 0x000EFDAB
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700795D RID: 31069
			// (set) Token: 0x0600A707 RID: 42759 RVA: 0x000F1BC9 File Offset: 0x000EFDC9
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700795E RID: 31070
			// (set) Token: 0x0600A708 RID: 42760 RVA: 0x000F1BE7 File Offset: 0x000EFDE7
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700795F RID: 31071
			// (set) Token: 0x0600A709 RID: 42761 RVA: 0x000F1BFA File Offset: 0x000EFDFA
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007960 RID: 31072
			// (set) Token: 0x0600A70A RID: 42762 RVA: 0x000F1C12 File Offset: 0x000EFE12
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007961 RID: 31073
			// (set) Token: 0x0600A70B RID: 42763 RVA: 0x000F1C30 File Offset: 0x000EFE30
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007962 RID: 31074
			// (set) Token: 0x0600A70C RID: 42764 RVA: 0x000F1C48 File Offset: 0x000EFE48
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007963 RID: 31075
			// (set) Token: 0x0600A70D RID: 42765 RVA: 0x000F1C60 File Offset: 0x000EFE60
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007964 RID: 31076
			// (set) Token: 0x0600A70E RID: 42766 RVA: 0x000F1C73 File Offset: 0x000EFE73
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007965 RID: 31077
			// (set) Token: 0x0600A70F RID: 42767 RVA: 0x000F1C8B File Offset: 0x000EFE8B
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007966 RID: 31078
			// (set) Token: 0x0600A710 RID: 42768 RVA: 0x000F1CA3 File Offset: 0x000EFEA3
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007967 RID: 31079
			// (set) Token: 0x0600A711 RID: 42769 RVA: 0x000F1CBB File Offset: 0x000EFEBB
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007968 RID: 31080
			// (set) Token: 0x0600A712 RID: 42770 RVA: 0x000F1CCE File Offset: 0x000EFECE
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007969 RID: 31081
			// (set) Token: 0x0600A713 RID: 42771 RVA: 0x000F1CE1 File Offset: 0x000EFEE1
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x1700796A RID: 31082
			// (set) Token: 0x0600A714 RID: 42772 RVA: 0x000F1CF4 File Offset: 0x000EFEF4
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x1700796B RID: 31083
			// (set) Token: 0x0600A715 RID: 42773 RVA: 0x000F1D07 File Offset: 0x000EFF07
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x1700796C RID: 31084
			// (set) Token: 0x0600A716 RID: 42774 RVA: 0x000F1D1F File Offset: 0x000EFF1F
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700796D RID: 31085
			// (set) Token: 0x0600A717 RID: 42775 RVA: 0x000F1D32 File Offset: 0x000EFF32
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700796E RID: 31086
			// (set) Token: 0x0600A718 RID: 42776 RVA: 0x000F1D45 File Offset: 0x000EFF45
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700796F RID: 31087
			// (set) Token: 0x0600A719 RID: 42777 RVA: 0x000F1D5D File Offset: 0x000EFF5D
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007970 RID: 31088
			// (set) Token: 0x0600A71A RID: 42778 RVA: 0x000F1D70 File Offset: 0x000EFF70
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007971 RID: 31089
			// (set) Token: 0x0600A71B RID: 42779 RVA: 0x000F1D88 File Offset: 0x000EFF88
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007972 RID: 31090
			// (set) Token: 0x0600A71C RID: 42780 RVA: 0x000F1D9B File Offset: 0x000EFF9B
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007973 RID: 31091
			// (set) Token: 0x0600A71D RID: 42781 RVA: 0x000F1DB9 File Offset: 0x000EFFB9
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007974 RID: 31092
			// (set) Token: 0x0600A71E RID: 42782 RVA: 0x000F1DCC File Offset: 0x000EFFCC
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007975 RID: 31093
			// (set) Token: 0x0600A71F RID: 42783 RVA: 0x000F1DEA File Offset: 0x000EFFEA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007976 RID: 31094
			// (set) Token: 0x0600A720 RID: 42784 RVA: 0x000F1DFD File Offset: 0x000EFFFD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007977 RID: 31095
			// (set) Token: 0x0600A721 RID: 42785 RVA: 0x000F1E15 File Offset: 0x000F0015
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007978 RID: 31096
			// (set) Token: 0x0600A722 RID: 42786 RVA: 0x000F1E2D File Offset: 0x000F002D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007979 RID: 31097
			// (set) Token: 0x0600A723 RID: 42787 RVA: 0x000F1E45 File Offset: 0x000F0045
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700797A RID: 31098
			// (set) Token: 0x0600A724 RID: 42788 RVA: 0x000F1E5D File Offset: 0x000F005D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CB0 RID: 3248
		public class EnableRoomMailboxAccountParameters : ParametersBase
		{
			// Token: 0x1700797B RID: 31099
			// (set) Token: 0x0600A726 RID: 42790 RVA: 0x000F1E7D File Offset: 0x000F007D
			public virtual SecureString RoomMailboxPassword
			{
				set
				{
					base.PowerSharpParameters["RoomMailboxPassword"] = value;
				}
			}

			// Token: 0x1700797C RID: 31100
			// (set) Token: 0x0600A727 RID: 42791 RVA: 0x000F1E90 File Offset: 0x000F0090
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x1700797D RID: 31101
			// (set) Token: 0x0600A728 RID: 42792 RVA: 0x000F1EA3 File Offset: 0x000F00A3
			public virtual SwitchParameter Room
			{
				set
				{
					base.PowerSharpParameters["Room"] = value;
				}
			}

			// Token: 0x1700797E RID: 31102
			// (set) Token: 0x0600A729 RID: 42793 RVA: 0x000F1EBB File Offset: 0x000F00BB
			public virtual bool EnableRoomMailboxAccount
			{
				set
				{
					base.PowerSharpParameters["EnableRoomMailboxAccount"] = value;
				}
			}

			// Token: 0x1700797F RID: 31103
			// (set) Token: 0x0600A72A RID: 42794 RVA: 0x000F1ED3 File Offset: 0x000F00D3
			public virtual WindowsLiveId MicrosoftOnlineServicesID
			{
				set
				{
					base.PowerSharpParameters["MicrosoftOnlineServicesID"] = value;
				}
			}

			// Token: 0x17007980 RID: 31104
			// (set) Token: 0x0600A72B RID: 42795 RVA: 0x000F1EE6 File Offset: 0x000F00E6
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007981 RID: 31105
			// (set) Token: 0x0600A72C RID: 42796 RVA: 0x000F1EF9 File Offset: 0x000F00F9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007982 RID: 31106
			// (set) Token: 0x0600A72D RID: 42797 RVA: 0x000F1F0C File Offset: 0x000F010C
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007983 RID: 31107
			// (set) Token: 0x0600A72E RID: 42798 RVA: 0x000F1F24 File Offset: 0x000F0124
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007984 RID: 31108
			// (set) Token: 0x0600A72F RID: 42799 RVA: 0x000F1F37 File Offset: 0x000F0137
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007985 RID: 31109
			// (set) Token: 0x0600A730 RID: 42800 RVA: 0x000F1F4A File Offset: 0x000F014A
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007986 RID: 31110
			// (set) Token: 0x0600A731 RID: 42801 RVA: 0x000F1F5D File Offset: 0x000F015D
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007987 RID: 31111
			// (set) Token: 0x0600A732 RID: 42802 RVA: 0x000F1F7B File Offset: 0x000F017B
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007988 RID: 31112
			// (set) Token: 0x0600A733 RID: 42803 RVA: 0x000F1F99 File Offset: 0x000F0199
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007989 RID: 31113
			// (set) Token: 0x0600A734 RID: 42804 RVA: 0x000F1FB7 File Offset: 0x000F01B7
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700798A RID: 31114
			// (set) Token: 0x0600A735 RID: 42805 RVA: 0x000F1FD5 File Offset: 0x000F01D5
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700798B RID: 31115
			// (set) Token: 0x0600A736 RID: 42806 RVA: 0x000F1FF3 File Offset: 0x000F01F3
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x1700798C RID: 31116
			// (set) Token: 0x0600A737 RID: 42807 RVA: 0x000F2006 File Offset: 0x000F0206
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x1700798D RID: 31117
			// (set) Token: 0x0600A738 RID: 42808 RVA: 0x000F201E File Offset: 0x000F021E
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x1700798E RID: 31118
			// (set) Token: 0x0600A739 RID: 42809 RVA: 0x000F203C File Offset: 0x000F023C
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x1700798F RID: 31119
			// (set) Token: 0x0600A73A RID: 42810 RVA: 0x000F2054 File Offset: 0x000F0254
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007990 RID: 31120
			// (set) Token: 0x0600A73B RID: 42811 RVA: 0x000F206C File Offset: 0x000F026C
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007991 RID: 31121
			// (set) Token: 0x0600A73C RID: 42812 RVA: 0x000F207F File Offset: 0x000F027F
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007992 RID: 31122
			// (set) Token: 0x0600A73D RID: 42813 RVA: 0x000F2097 File Offset: 0x000F0297
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007993 RID: 31123
			// (set) Token: 0x0600A73E RID: 42814 RVA: 0x000F20AF File Offset: 0x000F02AF
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007994 RID: 31124
			// (set) Token: 0x0600A73F RID: 42815 RVA: 0x000F20C7 File Offset: 0x000F02C7
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007995 RID: 31125
			// (set) Token: 0x0600A740 RID: 42816 RVA: 0x000F20DA File Offset: 0x000F02DA
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007996 RID: 31126
			// (set) Token: 0x0600A741 RID: 42817 RVA: 0x000F20ED File Offset: 0x000F02ED
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007997 RID: 31127
			// (set) Token: 0x0600A742 RID: 42818 RVA: 0x000F2100 File Offset: 0x000F0300
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007998 RID: 31128
			// (set) Token: 0x0600A743 RID: 42819 RVA: 0x000F2113 File Offset: 0x000F0313
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007999 RID: 31129
			// (set) Token: 0x0600A744 RID: 42820 RVA: 0x000F212B File Offset: 0x000F032B
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x1700799A RID: 31130
			// (set) Token: 0x0600A745 RID: 42821 RVA: 0x000F213E File Offset: 0x000F033E
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x1700799B RID: 31131
			// (set) Token: 0x0600A746 RID: 42822 RVA: 0x000F2151 File Offset: 0x000F0351
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x1700799C RID: 31132
			// (set) Token: 0x0600A747 RID: 42823 RVA: 0x000F2169 File Offset: 0x000F0369
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x1700799D RID: 31133
			// (set) Token: 0x0600A748 RID: 42824 RVA: 0x000F217C File Offset: 0x000F037C
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x1700799E RID: 31134
			// (set) Token: 0x0600A749 RID: 42825 RVA: 0x000F2194 File Offset: 0x000F0394
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700799F RID: 31135
			// (set) Token: 0x0600A74A RID: 42826 RVA: 0x000F21A7 File Offset: 0x000F03A7
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170079A0 RID: 31136
			// (set) Token: 0x0600A74B RID: 42827 RVA: 0x000F21C5 File Offset: 0x000F03C5
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170079A1 RID: 31137
			// (set) Token: 0x0600A74C RID: 42828 RVA: 0x000F21D8 File Offset: 0x000F03D8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170079A2 RID: 31138
			// (set) Token: 0x0600A74D RID: 42829 RVA: 0x000F21F6 File Offset: 0x000F03F6
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170079A3 RID: 31139
			// (set) Token: 0x0600A74E RID: 42830 RVA: 0x000F2209 File Offset: 0x000F0409
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170079A4 RID: 31140
			// (set) Token: 0x0600A74F RID: 42831 RVA: 0x000F2221 File Offset: 0x000F0421
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170079A5 RID: 31141
			// (set) Token: 0x0600A750 RID: 42832 RVA: 0x000F2239 File Offset: 0x000F0439
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170079A6 RID: 31142
			// (set) Token: 0x0600A751 RID: 42833 RVA: 0x000F2251 File Offset: 0x000F0451
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170079A7 RID: 31143
			// (set) Token: 0x0600A752 RID: 42834 RVA: 0x000F2269 File Offset: 0x000F0469
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CB1 RID: 3249
		public class AuditLogParameters : ParametersBase
		{
			// Token: 0x170079A8 RID: 31144
			// (set) Token: 0x0600A754 RID: 42836 RVA: 0x000F2289 File Offset: 0x000F0489
			public virtual string UserPrincipalName
			{
				set
				{
					base.PowerSharpParameters["UserPrincipalName"] = value;
				}
			}

			// Token: 0x170079A9 RID: 31145
			// (set) Token: 0x0600A755 RID: 42837 RVA: 0x000F229C File Offset: 0x000F049C
			public virtual SwitchParameter AuditLog
			{
				set
				{
					base.PowerSharpParameters["AuditLog"] = value;
				}
			}

			// Token: 0x170079AA RID: 31146
			// (set) Token: 0x0600A756 RID: 42838 RVA: 0x000F22B4 File Offset: 0x000F04B4
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170079AB RID: 31147
			// (set) Token: 0x0600A757 RID: 42839 RVA: 0x000F22C7 File Offset: 0x000F04C7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170079AC RID: 31148
			// (set) Token: 0x0600A758 RID: 42840 RVA: 0x000F22DA File Offset: 0x000F04DA
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170079AD RID: 31149
			// (set) Token: 0x0600A759 RID: 42841 RVA: 0x000F22F2 File Offset: 0x000F04F2
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170079AE RID: 31150
			// (set) Token: 0x0600A75A RID: 42842 RVA: 0x000F2305 File Offset: 0x000F0505
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170079AF RID: 31151
			// (set) Token: 0x0600A75B RID: 42843 RVA: 0x000F2318 File Offset: 0x000F0518
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170079B0 RID: 31152
			// (set) Token: 0x0600A75C RID: 42844 RVA: 0x000F232B File Offset: 0x000F052B
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079B1 RID: 31153
			// (set) Token: 0x0600A75D RID: 42845 RVA: 0x000F2349 File Offset: 0x000F0549
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079B2 RID: 31154
			// (set) Token: 0x0600A75E RID: 42846 RVA: 0x000F2367 File Offset: 0x000F0567
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079B3 RID: 31155
			// (set) Token: 0x0600A75F RID: 42847 RVA: 0x000F2385 File Offset: 0x000F0585
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079B4 RID: 31156
			// (set) Token: 0x0600A760 RID: 42848 RVA: 0x000F23A3 File Offset: 0x000F05A3
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079B5 RID: 31157
			// (set) Token: 0x0600A761 RID: 42849 RVA: 0x000F23C1 File Offset: 0x000F05C1
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170079B6 RID: 31158
			// (set) Token: 0x0600A762 RID: 42850 RVA: 0x000F23D4 File Offset: 0x000F05D4
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170079B7 RID: 31159
			// (set) Token: 0x0600A763 RID: 42851 RVA: 0x000F23EC File Offset: 0x000F05EC
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079B8 RID: 31160
			// (set) Token: 0x0600A764 RID: 42852 RVA: 0x000F240A File Offset: 0x000F060A
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170079B9 RID: 31161
			// (set) Token: 0x0600A765 RID: 42853 RVA: 0x000F2422 File Offset: 0x000F0622
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170079BA RID: 31162
			// (set) Token: 0x0600A766 RID: 42854 RVA: 0x000F243A File Offset: 0x000F063A
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170079BB RID: 31163
			// (set) Token: 0x0600A767 RID: 42855 RVA: 0x000F244D File Offset: 0x000F064D
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170079BC RID: 31164
			// (set) Token: 0x0600A768 RID: 42856 RVA: 0x000F2465 File Offset: 0x000F0665
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170079BD RID: 31165
			// (set) Token: 0x0600A769 RID: 42857 RVA: 0x000F247D File Offset: 0x000F067D
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170079BE RID: 31166
			// (set) Token: 0x0600A76A RID: 42858 RVA: 0x000F2495 File Offset: 0x000F0695
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170079BF RID: 31167
			// (set) Token: 0x0600A76B RID: 42859 RVA: 0x000F24A8 File Offset: 0x000F06A8
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170079C0 RID: 31168
			// (set) Token: 0x0600A76C RID: 42860 RVA: 0x000F24BB File Offset: 0x000F06BB
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170079C1 RID: 31169
			// (set) Token: 0x0600A76D RID: 42861 RVA: 0x000F24CE File Offset: 0x000F06CE
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170079C2 RID: 31170
			// (set) Token: 0x0600A76E RID: 42862 RVA: 0x000F24E1 File Offset: 0x000F06E1
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170079C3 RID: 31171
			// (set) Token: 0x0600A76F RID: 42863 RVA: 0x000F24F9 File Offset: 0x000F06F9
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170079C4 RID: 31172
			// (set) Token: 0x0600A770 RID: 42864 RVA: 0x000F250C File Offset: 0x000F070C
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170079C5 RID: 31173
			// (set) Token: 0x0600A771 RID: 42865 RVA: 0x000F251F File Offset: 0x000F071F
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170079C6 RID: 31174
			// (set) Token: 0x0600A772 RID: 42866 RVA: 0x000F2537 File Offset: 0x000F0737
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170079C7 RID: 31175
			// (set) Token: 0x0600A773 RID: 42867 RVA: 0x000F254A File Offset: 0x000F074A
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170079C8 RID: 31176
			// (set) Token: 0x0600A774 RID: 42868 RVA: 0x000F2562 File Offset: 0x000F0762
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170079C9 RID: 31177
			// (set) Token: 0x0600A775 RID: 42869 RVA: 0x000F2575 File Offset: 0x000F0775
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170079CA RID: 31178
			// (set) Token: 0x0600A776 RID: 42870 RVA: 0x000F2593 File Offset: 0x000F0793
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170079CB RID: 31179
			// (set) Token: 0x0600A777 RID: 42871 RVA: 0x000F25A6 File Offset: 0x000F07A6
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170079CC RID: 31180
			// (set) Token: 0x0600A778 RID: 42872 RVA: 0x000F25C4 File Offset: 0x000F07C4
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170079CD RID: 31181
			// (set) Token: 0x0600A779 RID: 42873 RVA: 0x000F25D7 File Offset: 0x000F07D7
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170079CE RID: 31182
			// (set) Token: 0x0600A77A RID: 42874 RVA: 0x000F25EF File Offset: 0x000F07EF
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170079CF RID: 31183
			// (set) Token: 0x0600A77B RID: 42875 RVA: 0x000F2607 File Offset: 0x000F0807
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170079D0 RID: 31184
			// (set) Token: 0x0600A77C RID: 42876 RVA: 0x000F261F File Offset: 0x000F081F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170079D1 RID: 31185
			// (set) Token: 0x0600A77D RID: 42877 RVA: 0x000F2637 File Offset: 0x000F0837
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CB2 RID: 3250
		public class GroupMailboxParameters : ParametersBase
		{
			// Token: 0x170079D2 RID: 31186
			// (set) Token: 0x0600A77F RID: 42879 RVA: 0x000F2657 File Offset: 0x000F0857
			public virtual string ArbitrationMailbox
			{
				set
				{
					base.PowerSharpParameters["ArbitrationMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x170079D3 RID: 31187
			// (set) Token: 0x0600A780 RID: 42880 RVA: 0x000F2675 File Offset: 0x000F0875
			public virtual MultiValuedProperty<ModeratorIDParameter> ModeratedBy
			{
				set
				{
					base.PowerSharpParameters["ModeratedBy"] = value;
				}
			}

			// Token: 0x170079D4 RID: 31188
			// (set) Token: 0x0600A781 RID: 42881 RVA: 0x000F2688 File Offset: 0x000F0888
			public virtual TransportModerationNotificationFlags SendModerationNotifications
			{
				set
				{
					base.PowerSharpParameters["SendModerationNotifications"] = value;
				}
			}

			// Token: 0x170079D5 RID: 31189
			// (set) Token: 0x0600A782 RID: 42882 RVA: 0x000F26A0 File Offset: 0x000F08A0
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x170079D6 RID: 31190
			// (set) Token: 0x0600A783 RID: 42883 RVA: 0x000F26B3 File Offset: 0x000F08B3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170079D7 RID: 31191
			// (set) Token: 0x0600A784 RID: 42884 RVA: 0x000F26C6 File Offset: 0x000F08C6
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x170079D8 RID: 31192
			// (set) Token: 0x0600A785 RID: 42885 RVA: 0x000F26DE File Offset: 0x000F08DE
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x170079D9 RID: 31193
			// (set) Token: 0x0600A786 RID: 42886 RVA: 0x000F26F1 File Offset: 0x000F08F1
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x170079DA RID: 31194
			// (set) Token: 0x0600A787 RID: 42887 RVA: 0x000F2704 File Offset: 0x000F0904
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x170079DB RID: 31195
			// (set) Token: 0x0600A788 RID: 42888 RVA: 0x000F2717 File Offset: 0x000F0917
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079DC RID: 31196
			// (set) Token: 0x0600A789 RID: 42889 RVA: 0x000F2735 File Offset: 0x000F0935
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079DD RID: 31197
			// (set) Token: 0x0600A78A RID: 42890 RVA: 0x000F2753 File Offset: 0x000F0953
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079DE RID: 31198
			// (set) Token: 0x0600A78B RID: 42891 RVA: 0x000F2771 File Offset: 0x000F0971
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079DF RID: 31199
			// (set) Token: 0x0600A78C RID: 42892 RVA: 0x000F278F File Offset: 0x000F098F
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079E0 RID: 31200
			// (set) Token: 0x0600A78D RID: 42893 RVA: 0x000F27AD File Offset: 0x000F09AD
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x170079E1 RID: 31201
			// (set) Token: 0x0600A78E RID: 42894 RVA: 0x000F27C0 File Offset: 0x000F09C0
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x170079E2 RID: 31202
			// (set) Token: 0x0600A78F RID: 42895 RVA: 0x000F27D8 File Offset: 0x000F09D8
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x170079E3 RID: 31203
			// (set) Token: 0x0600A790 RID: 42896 RVA: 0x000F27F6 File Offset: 0x000F09F6
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x170079E4 RID: 31204
			// (set) Token: 0x0600A791 RID: 42897 RVA: 0x000F280E File Offset: 0x000F0A0E
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x170079E5 RID: 31205
			// (set) Token: 0x0600A792 RID: 42898 RVA: 0x000F2826 File Offset: 0x000F0A26
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x170079E6 RID: 31206
			// (set) Token: 0x0600A793 RID: 42899 RVA: 0x000F2839 File Offset: 0x000F0A39
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x170079E7 RID: 31207
			// (set) Token: 0x0600A794 RID: 42900 RVA: 0x000F2851 File Offset: 0x000F0A51
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x170079E8 RID: 31208
			// (set) Token: 0x0600A795 RID: 42901 RVA: 0x000F2869 File Offset: 0x000F0A69
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x170079E9 RID: 31209
			// (set) Token: 0x0600A796 RID: 42902 RVA: 0x000F2881 File Offset: 0x000F0A81
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x170079EA RID: 31210
			// (set) Token: 0x0600A797 RID: 42903 RVA: 0x000F2894 File Offset: 0x000F0A94
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x170079EB RID: 31211
			// (set) Token: 0x0600A798 RID: 42904 RVA: 0x000F28A7 File Offset: 0x000F0AA7
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x170079EC RID: 31212
			// (set) Token: 0x0600A799 RID: 42905 RVA: 0x000F28BA File Offset: 0x000F0ABA
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x170079ED RID: 31213
			// (set) Token: 0x0600A79A RID: 42906 RVA: 0x000F28CD File Offset: 0x000F0ACD
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x170079EE RID: 31214
			// (set) Token: 0x0600A79B RID: 42907 RVA: 0x000F28E5 File Offset: 0x000F0AE5
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x170079EF RID: 31215
			// (set) Token: 0x0600A79C RID: 42908 RVA: 0x000F28F8 File Offset: 0x000F0AF8
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x170079F0 RID: 31216
			// (set) Token: 0x0600A79D RID: 42909 RVA: 0x000F290B File Offset: 0x000F0B0B
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x170079F1 RID: 31217
			// (set) Token: 0x0600A79E RID: 42910 RVA: 0x000F2923 File Offset: 0x000F0B23
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x170079F2 RID: 31218
			// (set) Token: 0x0600A79F RID: 42911 RVA: 0x000F2936 File Offset: 0x000F0B36
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x170079F3 RID: 31219
			// (set) Token: 0x0600A7A0 RID: 42912 RVA: 0x000F294E File Offset: 0x000F0B4E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x170079F4 RID: 31220
			// (set) Token: 0x0600A7A1 RID: 42913 RVA: 0x000F2961 File Offset: 0x000F0B61
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x170079F5 RID: 31221
			// (set) Token: 0x0600A7A2 RID: 42914 RVA: 0x000F297F File Offset: 0x000F0B7F
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x170079F6 RID: 31222
			// (set) Token: 0x0600A7A3 RID: 42915 RVA: 0x000F2992 File Offset: 0x000F0B92
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170079F7 RID: 31223
			// (set) Token: 0x0600A7A4 RID: 42916 RVA: 0x000F29B0 File Offset: 0x000F0BB0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170079F8 RID: 31224
			// (set) Token: 0x0600A7A5 RID: 42917 RVA: 0x000F29C3 File Offset: 0x000F0BC3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170079F9 RID: 31225
			// (set) Token: 0x0600A7A6 RID: 42918 RVA: 0x000F29DB File Offset: 0x000F0BDB
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170079FA RID: 31226
			// (set) Token: 0x0600A7A7 RID: 42919 RVA: 0x000F29F3 File Offset: 0x000F0BF3
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170079FB RID: 31227
			// (set) Token: 0x0600A7A8 RID: 42920 RVA: 0x000F2A0B File Offset: 0x000F0C0B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170079FC RID: 31228
			// (set) Token: 0x0600A7A9 RID: 42921 RVA: 0x000F2A23 File Offset: 0x000F0C23
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000CB3 RID: 3251
		public class PublicFolderParameters : ParametersBase
		{
			// Token: 0x170079FD RID: 31229
			// (set) Token: 0x0600A7AB RID: 42923 RVA: 0x000F2A43 File Offset: 0x000F0C43
			public virtual SwitchParameter PublicFolder
			{
				set
				{
					base.PowerSharpParameters["PublicFolder"] = value;
				}
			}

			// Token: 0x170079FE RID: 31230
			// (set) Token: 0x0600A7AC RID: 42924 RVA: 0x000F2A5B File Offset: 0x000F0C5B
			public virtual bool IsExcludedFromServingHierarchy
			{
				set
				{
					base.PowerSharpParameters["IsExcludedFromServingHierarchy"] = value;
				}
			}

			// Token: 0x170079FF RID: 31231
			// (set) Token: 0x0600A7AD RID: 42925 RVA: 0x000F2A73 File Offset: 0x000F0C73
			public virtual SwitchParameter HoldForMigration
			{
				set
				{
					base.PowerSharpParameters["HoldForMigration"] = value;
				}
			}

			// Token: 0x17007A00 RID: 31232
			// (set) Token: 0x0600A7AE RID: 42926 RVA: 0x000F2A8B File Offset: 0x000F0C8B
			public virtual NetID OriginalNetID
			{
				set
				{
					base.PowerSharpParameters["OriginalNetID"] = value;
				}
			}

			// Token: 0x17007A01 RID: 31233
			// (set) Token: 0x0600A7AF RID: 42927 RVA: 0x000F2A9E File Offset: 0x000F0C9E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17007A02 RID: 31234
			// (set) Token: 0x0600A7B0 RID: 42928 RVA: 0x000F2AB1 File Offset: 0x000F0CB1
			public virtual SwitchParameter TargetAllMDBs
			{
				set
				{
					base.PowerSharpParameters["TargetAllMDBs"] = value;
				}
			}

			// Token: 0x17007A03 RID: 31235
			// (set) Token: 0x0600A7B1 RID: 42929 RVA: 0x000F2AC9 File Offset: 0x000F0CC9
			public virtual MailboxProvisioningConstraint MailboxProvisioningConstraint
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningConstraint"] = value;
				}
			}

			// Token: 0x17007A04 RID: 31236
			// (set) Token: 0x0600A7B2 RID: 42930 RVA: 0x000F2ADC File Offset: 0x000F0CDC
			public virtual MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
			{
				set
				{
					base.PowerSharpParameters["MailboxProvisioningPreferences"] = value;
				}
			}

			// Token: 0x17007A05 RID: 31237
			// (set) Token: 0x0600A7B3 RID: 42931 RVA: 0x000F2AEF File Offset: 0x000F0CEF
			public virtual DatabaseIdParameter Database
			{
				set
				{
					base.PowerSharpParameters["Database"] = value;
				}
			}

			// Token: 0x17007A06 RID: 31238
			// (set) Token: 0x0600A7B4 RID: 42932 RVA: 0x000F2B02 File Offset: 0x000F0D02
			public virtual string RetentionPolicy
			{
				set
				{
					base.PowerSharpParameters["RetentionPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A07 RID: 31239
			// (set) Token: 0x0600A7B5 RID: 42933 RVA: 0x000F2B20 File Offset: 0x000F0D20
			public virtual string ActiveSyncMailboxPolicy
			{
				set
				{
					base.PowerSharpParameters["ActiveSyncMailboxPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A08 RID: 31240
			// (set) Token: 0x0600A7B6 RID: 42934 RVA: 0x000F2B3E File Offset: 0x000F0D3E
			public virtual string AddressBookPolicy
			{
				set
				{
					base.PowerSharpParameters["AddressBookPolicy"] = ((value != null) ? new AddressBookMailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A09 RID: 31241
			// (set) Token: 0x0600A7B7 RID: 42935 RVA: 0x000F2B5C File Offset: 0x000F0D5C
			public virtual string ThrottlingPolicy
			{
				set
				{
					base.PowerSharpParameters["ThrottlingPolicy"] = ((value != null) ? new ThrottlingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A0A RID: 31242
			// (set) Token: 0x0600A7B8 RID: 42936 RVA: 0x000F2B7A File Offset: 0x000F0D7A
			public virtual string SharingPolicy
			{
				set
				{
					base.PowerSharpParameters["SharingPolicy"] = ((value != null) ? new SharingPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A0B RID: 31243
			// (set) Token: 0x0600A7B9 RID: 42937 RVA: 0x000F2B98 File Offset: 0x000F0D98
			public virtual RemoteAccountPolicyIdParameter RemoteAccountPolicy
			{
				set
				{
					base.PowerSharpParameters["RemoteAccountPolicy"] = value;
				}
			}

			// Token: 0x17007A0C RID: 31244
			// (set) Token: 0x0600A7BA RID: 42938 RVA: 0x000F2BAB File Offset: 0x000F0DAB
			public virtual bool RemotePowerShellEnabled
			{
				set
				{
					base.PowerSharpParameters["RemotePowerShellEnabled"] = value;
				}
			}

			// Token: 0x17007A0D RID: 31245
			// (set) Token: 0x0600A7BB RID: 42939 RVA: 0x000F2BC3 File Offset: 0x000F0DC3
			public virtual string RoleAssignmentPolicy
			{
				set
				{
					base.PowerSharpParameters["RoleAssignmentPolicy"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17007A0E RID: 31246
			// (set) Token: 0x0600A7BC RID: 42940 RVA: 0x000F2BE1 File Offset: 0x000F0DE1
			public virtual bool QueryBaseDNRestrictionEnabled
			{
				set
				{
					base.PowerSharpParameters["QueryBaseDNRestrictionEnabled"] = value;
				}
			}

			// Token: 0x17007A0F RID: 31247
			// (set) Token: 0x0600A7BD RID: 42941 RVA: 0x000F2BF9 File Offset: 0x000F0DF9
			public virtual SwitchParameter Archive
			{
				set
				{
					base.PowerSharpParameters["Archive"] = value;
				}
			}

			// Token: 0x17007A10 RID: 31248
			// (set) Token: 0x0600A7BE RID: 42942 RVA: 0x000F2C11 File Offset: 0x000F0E11
			public virtual DatabaseIdParameter ArchiveDatabase
			{
				set
				{
					base.PowerSharpParameters["ArchiveDatabase"] = value;
				}
			}

			// Token: 0x17007A11 RID: 31249
			// (set) Token: 0x0600A7BF RID: 42943 RVA: 0x000F2C24 File Offset: 0x000F0E24
			public virtual Guid MailboxContainerGuid
			{
				set
				{
					base.PowerSharpParameters["MailboxContainerGuid"] = value;
				}
			}

			// Token: 0x17007A12 RID: 31250
			// (set) Token: 0x0600A7C0 RID: 42944 RVA: 0x000F2C3C File Offset: 0x000F0E3C
			public virtual SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
			{
				set
				{
					base.PowerSharpParameters["ForestWideDomainControllerAffinityByExecutingUser"] = value;
				}
			}

			// Token: 0x17007A13 RID: 31251
			// (set) Token: 0x0600A7C1 RID: 42945 RVA: 0x000F2C54 File Offset: 0x000F0E54
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17007A14 RID: 31252
			// (set) Token: 0x0600A7C2 RID: 42946 RVA: 0x000F2C6C File Offset: 0x000F0E6C
			public virtual string FirstName
			{
				set
				{
					base.PowerSharpParameters["FirstName"] = value;
				}
			}

			// Token: 0x17007A15 RID: 31253
			// (set) Token: 0x0600A7C3 RID: 42947 RVA: 0x000F2C7F File Offset: 0x000F0E7F
			public virtual string Initials
			{
				set
				{
					base.PowerSharpParameters["Initials"] = value;
				}
			}

			// Token: 0x17007A16 RID: 31254
			// (set) Token: 0x0600A7C4 RID: 42948 RVA: 0x000F2C92 File Offset: 0x000F0E92
			public virtual string LastName
			{
				set
				{
					base.PowerSharpParameters["LastName"] = value;
				}
			}

			// Token: 0x17007A17 RID: 31255
			// (set) Token: 0x0600A7C5 RID: 42949 RVA: 0x000F2CA5 File Offset: 0x000F0EA5
			public virtual string SamAccountName
			{
				set
				{
					base.PowerSharpParameters["SamAccountName"] = value;
				}
			}

			// Token: 0x17007A18 RID: 31256
			// (set) Token: 0x0600A7C6 RID: 42950 RVA: 0x000F2CB8 File Offset: 0x000F0EB8
			public virtual bool ResetPasswordOnNextLogon
			{
				set
				{
					base.PowerSharpParameters["ResetPasswordOnNextLogon"] = value;
				}
			}

			// Token: 0x17007A19 RID: 31257
			// (set) Token: 0x0600A7C7 RID: 42951 RVA: 0x000F2CD0 File Offset: 0x000F0ED0
			public virtual string ImmutableId
			{
				set
				{
					base.PowerSharpParameters["ImmutableId"] = value;
				}
			}

			// Token: 0x17007A1A RID: 31258
			// (set) Token: 0x0600A7C8 RID: 42952 RVA: 0x000F2CE3 File Offset: 0x000F0EE3
			public virtual MultiValuedProperty<CultureInfo> Languages
			{
				set
				{
					base.PowerSharpParameters["Languages"] = value;
				}
			}

			// Token: 0x17007A1B RID: 31259
			// (set) Token: 0x0600A7C9 RID: 42953 RVA: 0x000F2CF6 File Offset: 0x000F0EF6
			public virtual SwitchParameter OverrideRecipientQuotas
			{
				set
				{
					base.PowerSharpParameters["OverrideRecipientQuotas"] = value;
				}
			}

			// Token: 0x17007A1C RID: 31260
			// (set) Token: 0x0600A7CA RID: 42954 RVA: 0x000F2D0E File Offset: 0x000F0F0E
			public virtual string Alias
			{
				set
				{
					base.PowerSharpParameters["Alias"] = value;
				}
			}

			// Token: 0x17007A1D RID: 31261
			// (set) Token: 0x0600A7CB RID: 42955 RVA: 0x000F2D21 File Offset: 0x000F0F21
			public virtual SmtpAddress PrimarySmtpAddress
			{
				set
				{
					base.PowerSharpParameters["PrimarySmtpAddress"] = value;
				}
			}

			// Token: 0x17007A1E RID: 31262
			// (set) Token: 0x0600A7CC RID: 42956 RVA: 0x000F2D39 File Offset: 0x000F0F39
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x17007A1F RID: 31263
			// (set) Token: 0x0600A7CD RID: 42957 RVA: 0x000F2D4C File Offset: 0x000F0F4C
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17007A20 RID: 31264
			// (set) Token: 0x0600A7CE RID: 42958 RVA: 0x000F2D6A File Offset: 0x000F0F6A
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x17007A21 RID: 31265
			// (set) Token: 0x0600A7CF RID: 42959 RVA: 0x000F2D7D File Offset: 0x000F0F7D
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17007A22 RID: 31266
			// (set) Token: 0x0600A7D0 RID: 42960 RVA: 0x000F2D9B File Offset: 0x000F0F9B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17007A23 RID: 31267
			// (set) Token: 0x0600A7D1 RID: 42961 RVA: 0x000F2DAE File Offset: 0x000F0FAE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17007A24 RID: 31268
			// (set) Token: 0x0600A7D2 RID: 42962 RVA: 0x000F2DC6 File Offset: 0x000F0FC6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17007A25 RID: 31269
			// (set) Token: 0x0600A7D3 RID: 42963 RVA: 0x000F2DDE File Offset: 0x000F0FDE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17007A26 RID: 31270
			// (set) Token: 0x0600A7D4 RID: 42964 RVA: 0x000F2DF6 File Offset: 0x000F0FF6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17007A27 RID: 31271
			// (set) Token: 0x0600A7D5 RID: 42965 RVA: 0x000F2E0E File Offset: 0x000F100E
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
