using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.MapiTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Provisioning.LoadBalancing;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000067 RID: 103
	[Cmdlet("enable", "Mailbox", SupportsShouldProcess = true, DefaultParameterSetName = "User")]
	public sealed class EnableMailbox : EnableRecipientObjectTask<UserIdParameter, ADUser>
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0001CB50 File Offset: 0x0001AD50
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Linked" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxLinked(this.Identity.ToString(), this.LinkedMasterAccount.ToString(), this.LinkedDomainController.ToString());
				}
				if ("Shared" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxShared(this.Identity.ToString(), this.Shared.ToString());
				}
				if ("Room" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxResource(this.Identity.ToString(), ExchangeResourceType.Room.ToString());
				}
				if ("Equipment" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxResource(this.Identity.ToString(), ExchangeResourceType.Equipment.ToString());
				}
				if ("Arbitration" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxArbitration(this.Identity.ToString());
				}
				if ("PublicFolder" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxPublicFolder(this.Identity.ToString());
				}
				if ("Discovery" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxDiscovery(this.Identity.ToString());
				}
				if ("Archive" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxWithArchive(this.Identity.ToString());
				}
				if ("RemoteArchive" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxWithRemoteArchive(this.Identity.ToString(), this.ArchiveDomain.ToString());
				}
				if ("LinkedRoomMailbox" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxLinkedAndResource(this.Identity.ToString(), this.LinkedMasterAccount.ToString(), this.LinkedDomainController.ToString(), ExchangeResourceType.Room.ToString());
				}
				if ("AuditLog" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageEnableMailboxAuditLog(this.Identity.ToString());
				}
				return Strings.ConfirmationMessageEnableMailboxUser(this.Identity.ToString());
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001CD5A File Offset: 0x0001AF5A
		private ActiveManager ActiveManager
		{
			get
			{
				return RecipientTaskHelper.GetActiveManagerInstance();
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001CD61 File Offset: 0x0001AF61
		// (set) Token: 0x060006E3 RID: 1763 RVA: 0x0001CD78 File Offset: 0x0001AF78
		[Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override UserIdParameter Identity
		{
			get
			{
				return (UserIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0001CD8B File Offset: 0x0001AF8B
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0001CDA2 File Offset: 0x0001AFA2
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "AuditLog")]
		[Parameter(Mandatory = false, ParameterSetName = "Arbitration")]
		[Parameter(Mandatory = false, ParameterSetName = "PublicFolder")]
		[Parameter(Mandatory = false, ParameterSetName = "Discovery")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001CDB5 File Offset: 0x0001AFB5
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001CDDB File Offset: 0x0001AFDB
		[Parameter(Mandatory = false, ParameterSetName = "AuditLog")]
		[Parameter(Mandatory = false, ParameterSetName = "Arbitration")]
		[Parameter(Mandatory = false, ParameterSetName = "Discovery")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public SwitchParameter TargetAllMDBs
		{
			get
			{
				return (SwitchParameter)(base.Fields["TargetAllMDBs"] ?? true);
			}
			set
			{
				base.Fields["TargetAllMDBs"] = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001CDF3 File Offset: 0x0001AFF3
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001CE19 File Offset: 0x0001B019
		[Parameter(Mandatory = true, ParameterSetName = "Arbitration")]
		public SwitchParameter Arbitration
		{
			get
			{
				return (SwitchParameter)(base.Fields["Arbitration"] ?? false);
			}
			set
			{
				base.Fields["Arbitration"] = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001CE31 File Offset: 0x0001B031
		// (set) Token: 0x060006EB RID: 1771 RVA: 0x0001CE57 File Offset: 0x0001B057
		[Parameter(Mandatory = true, ParameterSetName = "PublicFolder")]
		public SwitchParameter PublicFolder
		{
			get
			{
				return (SwitchParameter)(base.Fields["PublicFolder"] ?? false);
			}
			set
			{
				base.Fields["PublicFolder"] = value;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0001CE6F File Offset: 0x0001B06F
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0001CE95 File Offset: 0x0001B095
		[Parameter(Mandatory = false, ParameterSetName = "PublicFolder")]
		public SwitchParameter HoldForMigration
		{
			get
			{
				return (SwitchParameter)(base.Fields["HoldForMigration"] ?? false);
			}
			set
			{
				base.Fields["HoldForMigration"] = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060006EE RID: 1774 RVA: 0x0001CEAD File Offset: 0x0001B0AD
		// (set) Token: 0x060006EF RID: 1775 RVA: 0x0001CED3 File Offset: 0x0001B0D3
		[Parameter(Mandatory = true, ParameterSetName = "Discovery")]
		public SwitchParameter Discovery
		{
			get
			{
				return (SwitchParameter)(base.Fields["Discovery"] ?? false);
			}
			set
			{
				base.Fields["Discovery"] = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0001CEEB File Offset: 0x0001B0EB
		// (set) Token: 0x060006F1 RID: 1777 RVA: 0x0001CF02 File Offset: 0x0001B102
		[Parameter(Mandatory = true, ParameterSetName = "Room")]
		public SwitchParameter Room
		{
			get
			{
				return (SwitchParameter)base.Fields["Room"];
			}
			set
			{
				base.Fields["Room"] = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0001CF1A File Offset: 0x0001B11A
		// (set) Token: 0x060006F3 RID: 1779 RVA: 0x0001CF22 File Offset: 0x0001B122
		[Parameter(Mandatory = true, ParameterSetName = "LinkedRoomMailbox")]
		public SwitchParameter LinkedRoom
		{
			get
			{
				return this.Room;
			}
			set
			{
				this.Room = value;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0001CF2B File Offset: 0x0001B12B
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0001CF42 File Offset: 0x0001B142
		[Parameter(Mandatory = true, ParameterSetName = "Equipment")]
		public SwitchParameter Equipment
		{
			get
			{
				return (SwitchParameter)base.Fields["Equipment"];
			}
			set
			{
				base.Fields["Equipment"] = value;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001CF5A File Offset: 0x0001B15A
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001CF71 File Offset: 0x0001B171
		[Parameter(Mandatory = true, ParameterSetName = "Shared")]
		public SwitchParameter Shared
		{
			get
			{
				return (SwitchParameter)base.Fields["Shared"];
			}
			set
			{
				base.Fields["Shared"] = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060006F8 RID: 1784 RVA: 0x0001CF89 File Offset: 0x0001B189
		// (set) Token: 0x060006F9 RID: 1785 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		[Parameter(Mandatory = true, ParameterSetName = "Linked")]
		[Parameter(Mandatory = true, ParameterSetName = "LinkedRoomMailbox")]
		public UserIdParameter LinkedMasterAccount
		{
			get
			{
				return (UserIdParameter)base.Fields["LinkedMasterAccount"];
			}
			set
			{
				base.Fields["LinkedMasterAccount"] = value;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0001CFB3 File Offset: 0x0001B1B3
		// (set) Token: 0x060006FB RID: 1787 RVA: 0x0001CFCA File Offset: 0x0001B1CA
		[Parameter(Mandatory = true, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = true, ParameterSetName = "Linked")]
		public string LinkedDomainController
		{
			get
			{
				return (string)base.Fields["LinkedDomainController"];
			}
			set
			{
				base.Fields["LinkedDomainController"] = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0001CFDD File Offset: 0x0001B1DD
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		public PSCredential LinkedCredential
		{
			get
			{
				return (PSCredential)base.Fields["LinkedCredential"];
			}
			set
			{
				base.Fields["LinkedCredential"] = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060006FE RID: 1790 RVA: 0x0001D007 File Offset: 0x0001B207
		// (set) Token: 0x060006FF RID: 1791 RVA: 0x0001D01E File Offset: 0x0001B21E
		[Parameter(Mandatory = false)]
		public MailboxPolicyIdParameter RetentionPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields[MailboxSchema.RetentionPolicy];
			}
			set
			{
				base.Fields[MailboxSchema.RetentionPolicy] = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x0001D031 File Offset: 0x0001B231
		// (set) Token: 0x06000701 RID: 1793 RVA: 0x0001D057 File Offset: 0x0001B257
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? false);
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x0001D06F File Offset: 0x0001B26F
		// (set) Token: 0x06000703 RID: 1795 RVA: 0x0001D086 File Offset: 0x0001B286
		[Parameter(Mandatory = false)]
		public MailboxPolicyIdParameter ActiveSyncMailboxPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields["ActiveSyncMailboxPolicy"];
			}
			set
			{
				base.Fields["ActiveSyncMailboxPolicy"] = value;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0001D099 File Offset: 0x0001B299
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001D0B0 File Offset: 0x0001B2B0
		[Parameter(Mandatory = false)]
		public MailboxPolicyIdParameter RoleAssignmentPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields[MailboxSchema.RoleAssignmentPolicy];
			}
			set
			{
				base.Fields[MailboxSchema.RoleAssignmentPolicy] = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0001D0C3 File Offset: 0x0001B2C3
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x0001D0DA File Offset: 0x0001B2DA
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public AddressBookMailboxPolicyIdParameter AddressBookPolicy
		{
			get
			{
				return (AddressBookMailboxPolicyIdParameter)base.Fields[ADRecipientSchema.AddressBookPolicy];
			}
			set
			{
				base.Fields[ADRecipientSchema.AddressBookPolicy] = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0001D0ED File Offset: 0x0001B2ED
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x0001D104 File Offset: 0x0001B304
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public MailboxPlanIdParameter MailboxPlan
		{
			get
			{
				return (MailboxPlanIdParameter)base.Fields[ADRecipientSchema.MailboxPlan];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxPlan] = value;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x0001D117 File Offset: 0x0001B317
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x0001D13D File Offset: 0x0001B33D
		[Parameter(Mandatory = false, ParameterSetName = "Archive")]
		public SwitchParameter Archive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Archive"] ?? false);
			}
			set
			{
				base.Fields["Archive"] = value;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x0001D155 File Offset: 0x0001B355
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x0001D17A File Offset: 0x0001B37A
		[Parameter(Mandatory = false, ParameterSetName = "Archive")]
		[ValidateNotEmptyGuid]
		public Guid ArchiveGuid
		{
			get
			{
				return (Guid)(base.Fields[ADUserSchema.ArchiveGuid] ?? Guid.Empty);
			}
			set
			{
				base.Fields[ADUserSchema.ArchiveGuid] = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x0001D192 File Offset: 0x0001B392
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x0001D1A9 File Offset: 0x0001B3A9
		[Parameter(Mandatory = false, ParameterSetName = "Archive")]
		public DatabaseIdParameter ArchiveDatabase
		{
			get
			{
				return (DatabaseIdParameter)base.Fields[ADUserSchema.ArchiveDatabase];
			}
			set
			{
				base.Fields[ADUserSchema.ArchiveDatabase] = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0001D1BC File Offset: 0x0001B3BC
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0001D1D3 File Offset: 0x0001B3D3
		[Parameter(Mandatory = false, ParameterSetName = "Archive")]
		public MultiValuedProperty<string> ArchiveName
		{
			get
			{
				return base.Fields[ADUserSchema.ArchiveName] as MultiValuedProperty<string>;
			}
			set
			{
				base.Fields[ADUserSchema.ArchiveName] = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0001D1E6 File Offset: 0x0001B3E6
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0001D20C File Offset: 0x0001B40C
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		public SwitchParameter RemoteArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["RemoteArchive"] ?? false);
			}
			set
			{
				base.Fields["RemoteArchive"] = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001D224 File Offset: 0x0001B424
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0001D23B File Offset: 0x0001B43B
		[Parameter(Mandatory = true, ParameterSetName = "RemoteArchive")]
		public SmtpDomain ArchiveDomain
		{
			get
			{
				return base.Fields[ADUserSchema.ArchiveDomain] as SmtpDomain;
			}
			set
			{
				base.Fields[ADUserSchema.ArchiveDomain] = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001D24E File Offset: 0x0001B44E
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0001D256 File Offset: 0x0001B456
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public override Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
			set
			{
				base.SKUCapability = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001D25F File Offset: 0x0001B45F
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x0001D267 File Offset: 0x0001B467
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		public override MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
			set
			{
				base.AddOnSKUCapability = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001D270 File Offset: 0x0001B470
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x0001D278 File Offset: 0x0001B478
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public override bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
			set
			{
				base.SKUAssigned = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001D281 File Offset: 0x0001B481
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x0001D2A7 File Offset: 0x0001B4A7
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "Archive")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public SwitchParameter BypassModerationCheck
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassModerationCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassModerationCheck"] = value;
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001D2BF File Offset: 0x0001B4BF
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001D2D6 File Offset: 0x0001B4D6
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public bool? AccountDisabled
		{
			get
			{
				return (bool?)base.Fields["AccountDisabled"];
			}
			set
			{
				base.Fields["AccountDisabled"] = value;
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001D2EE File Offset: 0x0001B4EE
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001D2F6 File Offset: 0x0001B4F6
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		public override CountryInfo UsageLocation
		{
			get
			{
				return base.UsageLocation;
			}
			set
			{
				base.UsageLocation = value;
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001D2FF File Offset: 0x0001B4FF
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001D325 File Offset: 0x0001B525
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeSoftDeletedObjects
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeSoftDeletedMailbox"] ?? false);
			}
			set
			{
				base.Fields["IncludeSoftDeletedMailbox"] = value;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001D33D File Offset: 0x0001B53D
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0001D363 File Offset: 0x0001B563
		[Parameter(Mandatory = false, ParameterSetName = "AuditLog")]
		public SwitchParameter AuditLog
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuditLog"] ?? false);
			}
			set
			{
				base.Fields["AuditLog"] = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001D37B File Offset: 0x0001B57B
		protected override bool DelayProvisioning
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001D388 File Offset: 0x0001B588
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			base.CheckExclusiveParameters(new object[]
			{
				ADRecipientSchema.MailboxPlan,
				"SKUCapability"
			});
			if (!("Linked" == base.ParameterSetName))
			{
				if (!("LinkedRoomMailbox" == base.ParameterSetName))
				{
					goto IL_AD;
				}
			}
			try
			{
				NetworkCredential userForestCredential = (this.LinkedCredential == null) ? null : this.LinkedCredential.GetNetworkCredential();
				this.linkedUserSid = MailboxTaskHelper.GetAccountSidFromAnotherForest(this.LinkedMasterAccount, this.LinkedDomainController, userForestCredential, base.GlobalConfigSession, new MailboxTaskHelper.GetUniqueObject(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			}
			catch (PSArgumentException exception)
			{
				base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, this.LinkedCredential);
			}
			IL_AD:
			TaskLogger.LogExit();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001D458 File Offset: 0x0001B658
		protected override void InternalStateReset()
		{
			TaskLogger.LogEnter();
			base.InternalStateReset();
			if (this.IncludeSoftDeletedObjects.IsPresent)
			{
				base.SessionSettings.IncludeSoftDeletedObjects = true;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001D494 File Offset: 0x0001B694
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			if (this.MailboxPlan != null)
			{
				ADUser dataObject = (ADUser)base.GetDataObject<ADUser>(this.MailboxPlan, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(this.MailboxPlan.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(this.MailboxPlan.ToString())));
				MailboxTaskHelper.ValidateMailboxPlanRelease(dataObject, new Task.ErrorLoggerDelegate(base.WriteError));
				this.mailboxPlan = new MailboxPlan(dataObject);
			}
			if (this.RetentionPolicy != null)
			{
				if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
				{
					base.WriteError(new LocalizedException(Strings.ErrorLinkOpOnDehydratedTenant("RetentionPolicy")), ExchangeErrorCategory.Client, null);
				}
				RetentionPolicy retentionPolicy = (RetentionPolicy)base.GetDataObject<RetentionPolicy>(this.RetentionPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRetentionPolicyNotFound(this.RetentionPolicy.ToString())), new LocalizedString?(Strings.ErrorRetentionPolicyNotUnique(this.RetentionPolicy.ToString())));
				this.retentionPolicyId = retentionPolicy.Id;
			}
			if (this.ActiveSyncMailboxPolicy != null)
			{
				MobileMailboxPolicy mobileMailboxPolicy = (MobileMailboxPolicy)base.GetDataObject<MobileMailboxPolicy>(this.ActiveSyncMailboxPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorMobileMailboxPolicyNotFound(this.ActiveSyncMailboxPolicy.ToString())), new LocalizedString?(Strings.ErrorMobileMailboxPolicyNotUnique(this.ActiveSyncMailboxPolicy.ToString())));
				this.mobileMailboxPolicyId = (ADObjectId)mobileMailboxPolicy.Identity;
			}
			if (this.AddressBookPolicy != null)
			{
				AddressBookMailboxPolicy addressBookMailboxPolicy = (AddressBookMailboxPolicy)base.GetDataObject<AddressBookMailboxPolicy>(this.AddressBookPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorAddressBookMailboxPolicyNotFound(this.AddressBookPolicy.ToString())), new LocalizedString?(Strings.ErrorAddressBookMailboxPolicyNotUnique(this.AddressBookPolicy.ToString())), ExchangeErrorCategory.Client);
				this.addressBookPolicyId = (ADObjectId)addressBookMailboxPolicy.Identity;
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001D64C File Offset: 0x0001B84C
		internal override bool SkipPrepareDataObject()
		{
			return "Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001D678 File Offset: 0x0001B878
		protected override void PrepareRecipientObject(ref ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(ref user);
			if (this.BypassModerationCheck.IsPresent)
			{
				user.BypassModerationCheck = true;
			}
			if (this.AccountDisabled == true)
			{
				user.UserAccountControl |= UserAccountControlFlags.AccountDisabled;
			}
			else if (this.AccountDisabled == false)
			{
				user.UserAccountControl &= ~UserAccountControlFlags.AccountDisabled;
			}
			if (RecipientType.User != user.RecipientType)
			{
				if (("Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName) && user.RecipientType == RecipientType.UserMailbox)
				{
					if (user.MailboxMoveStatus != RequestStatus.None && user.MailboxMoveStatus != RequestStatus.Completed && user.MailboxMoveStatus != RequestStatus.CompletedWithWarning)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorMailboxBeingMoved(user.Name, user.MailboxMoveStatus.ToString())), ErrorCategory.InvalidArgument, user);
					}
					if (user.ManagedFolderMailboxPolicy != null)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorNoArchiveWithManagedFolder(user.Name)), ErrorCategory.InvalidData, null);
					}
					if (user.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorNoArchiveForPublicMailbox(user.Name)), ErrorCategory.InvalidArgument, null);
					}
					if ("RemoteArchive" == base.ParameterSetName)
					{
						Database database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(new DatabaseIdParameter(user.Database), base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(user.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(user.Database.ToString())));
						if (this.GetDatabaseLocationInfo(database).ServerVersion < Server.E15MinVersion)
						{
							base.WriteError(new RecipientTaskException(Strings.ErrorMailboxDatabaseNotOnE15Server(database.ToString())), ExchangeErrorCategory.Client, null);
						}
					}
					if (this.archiveDatabase != null)
					{
						MailboxTaskHelper.BlockLowerMajorVersionArchive(this.archiveDatabaseLocationInfo.ServerVersion, user.Database.DistinguishedName, this.archiveDatabase.DistinguishedName, this.archiveDatabase.ToString(), user.Database, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<MailboxDatabase>), base.GlobalConfigSession, this.ActiveManager, new Task.ErrorLoggerDelegate(base.WriteError));
					}
					this.CreateArchiveIfNecessary(user);
					TaskLogger.LogExit();
					return;
				}
				if (user.RecipientType == RecipientType.MailUser)
				{
					this.originalRecipientType = user.RecipientType;
					base.Alias = user.Alias;
				}
				else
				{
					RecipientIdParameter recipientIdParameter = new RecipientIdParameter((ADObjectId)user.Identity);
					base.WriteError(new RecipientTaskException(Strings.ErrorInvalidRecipientType(recipientIdParameter.ToString(), user.RecipientType.ToString())), ErrorCategory.InvalidArgument, recipientIdParameter);
				}
			}
			if ("Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorMailboxNotEnabled(this.Identity.ToString())), ErrorCategory.InvalidArgument, user);
			}
			if (!("User" == base.ParameterSetName) && !("WindowsLiveID" == base.ParameterSetName) && (user.UserAccountControl & UserAccountControlFlags.AccountDisabled) == UserAccountControlFlags.None)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorAccountEnabledForNonUserMailbox), ErrorCategory.InvalidArgument, user);
			}
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled && this.originalRecipientType == RecipientType.MailUser)
			{
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.WindowsLiveID.Enabled && user.WindowsLiveID.Equals(SmtpAddress.Empty))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorWindowsLiveIdRequired(user.Name)), ErrorCategory.InvalidData, null);
				}
				if (!RecipientTaskHelper.SMTPAddressCheckWithAcceptedDomain(this.ConfigurationSession, user.OrganizationId, new Task.ErrorLoggerDelegate(base.WriteError), base.ProvisioningCache))
				{
					this.StripInvalidSMTPAddresses(user);
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.WindowsLiveID.Enabled && !RecipientTaskHelper.IsAcceptedDomain(this.ConfigurationSession, user.OrganizationId, user.WindowsEmailAddress.Domain, base.ProvisioningCache))
					{
						user.WindowsEmailAddress = user.WindowsLiveID;
					}
				}
			}
			if ("PublicFolder" == base.ParameterSetName)
			{
				user.SetExchangeVersion(ADUser.GetMaximumSupportedExchangeObjectVersion(RecipientTypeDetails.PublicFolderMailbox, false));
			}
			else if ("Arbitration" == base.ParameterSetName)
			{
				user.SetExchangeVersion(ADUser.GetMaximumSupportedExchangeObjectVersion(RecipientTypeDetails.ArbitrationMailbox, false));
			}
			else if ("AuditLog" == base.ParameterSetName)
			{
				user.SetExchangeVersion(ADUser.GetMaximumSupportedExchangeObjectVersion(RecipientTypeDetails.AuditLogMailbox, false));
			}
			else
			{
				user.SetExchangeVersion(ADUser.GetMaximumSupportedExchangeObjectVersion(RecipientTypeDetails.UserMailbox, false));
			}
			if (user.IsChanged(ADObjectSchema.ExchangeVersion))
			{
				base.WriteVerbose(Strings.VerboseUpdatingVersion(user.Identity.ToString(), user.ExchangeVersion.ToString(), ExchangeObjectVersion.Exchange2010.ToString()));
				base.DataSession.Save(user);
				base.WriteVerbose(Strings.VerboseADOperationSucceeded(user.Identity.ToString()));
				user = (ADUser)base.DataSession.Read<ADUser>(user.Id);
			}
			List<PropertyDefinition> list;
			if (this.originalRecipientType != RecipientType.MailUser)
			{
				list = new List<PropertyDefinition>(RecipientConstants.DisableMailbox_PropertiesToReset);
				MailboxTaskHelper.RemovePersistentProperties(list);
			}
			else
			{
				list = new List<PropertyDefinition>(EnableMailbox.PropertiesToResetForMailUser);
			}
			MailboxTaskHelper.ClearExchangeProperties(user, list);
			if (this.DelayProvisioning && base.IsProvisioningLayerAvailable)
			{
				this.ProvisionDefaultValues(new ADUser
				{
					PersistedCapabilities = user.PersistedCapabilities,
					MailboxProvisioningConstraint = user.MailboxProvisioningConstraint
				}, user);
			}
			if ("RemoteArchive" != base.ParameterSetName && MailboxTaskHelper.SupportsMailboxReleaseVersioning(user))
			{
				if ("Archive" == base.ParameterSetName && this.archiveDatabaseLocationInfo != null)
				{
					user.ArchiveRelease = this.archiveDatabaseLocationInfo.MailboxRelease;
				}
				else
				{
					user.MailboxRelease = this.databaseLocationInfo.MailboxRelease;
				}
			}
			if (AppSettings.Current.DedicatedMailboxPlansEnabled || VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.LegacyRegCodeSupport.Enabled)
			{
				string dedicatedMailboxPlansCustomAttributeName = AppSettings.Current.DedicatedMailboxPlansCustomAttributeName;
				string customAttribute = ADRecipient.GetCustomAttribute(user, dedicatedMailboxPlansCustomAttributeName);
				if (!string.IsNullOrEmpty(customAttribute))
				{
					string text = null;
					MailboxProvisioningConstraint mailboxProvisioningConstraint = null;
					if (!ADRecipient.TryParseMailboxProvisioningData(customAttribute, out text, out mailboxProvisioningConstraint) && user.MailboxProvisioningConstraint == null && VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.LegacyRegCodeSupport.Enabled)
					{
						base.WriteError(new RecipientTaskException(Strings.Error_InvalidLegacyRegionCode(customAttribute)), ExchangeErrorCategory.Client, null);
					}
					if (AppSettings.Current.DedicatedMailboxPlansEnabled)
					{
						if (text != null)
						{
							ADUser aduser = this.FindMailboxPlanWithName(text, base.TenantGlobalCatalogSession);
							if (aduser != null)
							{
								user.MailboxPlan = aduser.Id;
								user.MailboxPlanObject = aduser;
							}
							else
							{
								this.WriteWarning(Strings.WarningDedicatedMailboxPlanNotFound(text));
							}
						}
						else
						{
							this.WriteWarning(Strings.WarningInvalidDedicatedMailboxPlanData(customAttribute));
						}
					}
				}
				else if (AppSettings.Current.DedicatedMailboxPlansEnabled)
				{
					this.WriteWarning(Strings.WarningDedicatedMailboxPlanDataNotFound(dedicatedMailboxPlansCustomAttributeName));
				}
			}
			if (user.MailboxPlan != null)
			{
				ADUser dataObject;
				if (user.MailboxPlanObject != null)
				{
					dataObject = user.MailboxPlanObject;
				}
				else
				{
					MailboxPlanIdParameter mailboxPlanIdParameter = new MailboxPlanIdParameter(user.MailboxPlan);
					dataObject = (ADUser)base.GetDataObject<ADUser>(mailboxPlanIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(mailboxPlanIdParameter.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(mailboxPlanIdParameter.ToString())));
				}
				this.mailboxPlan = new MailboxPlan(dataObject);
			}
			user.MailboxPlanObject = null;
			user.propertyBag.ResetChangeTracking(ADRecipientSchema.MailboxPlanObject);
			if (this.mailboxPlan != null)
			{
				ADUser aduser2 = new ADUser();
				aduser2.StampPersistableDefaultValues();
				aduser2.StampDefaultValues(RecipientType.UserMailbox);
				aduser2.ResetChangeTracking();
				aduser2.EnableSaveCalculatedValues();
				User.FromDataObject(aduser2).ApplyCloneableProperties(User.FromDataObject((ADUser)this.mailboxPlan.DataObject));
				aduser2.PersistedCapabilities = user.PersistedCapabilities;
				Mailbox.FromDataObject(aduser2).ApplyCloneableProperties(Mailbox.FromDataObject((ADUser)this.mailboxPlan.DataObject));
				CASMailbox.FromDataObject(aduser2).ApplyCloneableProperties(CASMailbox.FromDataObject((ADUser)this.mailboxPlan.DataObject));
				UMMailbox.FromDataObject(aduser2).ApplyCloneableProperties(UMMailbox.FromDataObject((ADUser)this.mailboxPlan.DataObject));
				RecipientTaskHelper.UpgradeArchiveQuotaOnArchiveAddOnSKU(aduser2, aduser2.PersistedCapabilities);
				user.CopyChangesFrom(aduser2);
				user.MailboxPlan = this.mailboxPlan.Id;
			}
			else
			{
				user.StampMailboxDefaultValues(false);
			}
			IConfigurationSession tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(user.OrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId);
			if (this.PublicFolder)
			{
				MailboxTaskHelper.ValidatePublicFolderInformationWritable(tenantLocalConfigSession, this.HoldForMigration, new Task.ErrorLoggerDelegate(base.WriteError), this.Force);
			}
			if (this.RoleAssignmentPolicy == null)
			{
				if (!this.Arbitration && !this.PublicFolder)
				{
					RoleAssignmentPolicy roleAssignmentPolicy = RecipientTaskHelper.FindDefaultRoleAssignmentPolicy(tenantLocalConfigSession, new Task.ErrorLoggerDelegate(base.WriteError), Strings.ErrorDefaultRoleAssignmentPolicyNotUnique, Strings.ErrorDefaultRoleAssignmentPolicyNotFound);
					if (roleAssignmentPolicy != null)
					{
						this.defaultRoleAssignmentPolicyId = (ADObjectId)roleAssignmentPolicy.Identity;
					}
				}
			}
			else
			{
				RoleAssignmentPolicy roleAssignmentPolicy2 = (RoleAssignmentPolicy)base.GetDataObject<RoleAssignmentPolicy>(this.RoleAssignmentPolicy, tenantLocalConfigSession, null, new LocalizedString?(Strings.ErrorRoleAssignmentPolicyNotFound(this.RoleAssignmentPolicy.ToString())), new LocalizedString?(Strings.ErrorRoleAssignmentPolicyNotUnique(this.RoleAssignmentPolicy.ToString())));
				this.userSpecifiedRoleAssignmentPolicyId = (ADObjectId)roleAssignmentPolicy2.Identity;
			}
			if (!RecipientTaskHelper.IsE15OrLater(this.databaseLocationInfo.ServerVersion))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorMailboxDatabaseNotOnE15Server(this.database.ToString())), ErrorCategory.InvalidData, null);
			}
			if (user.UseDatabaseQuotaDefaults == null)
			{
				user.UseDatabaseQuotaDefaults = new bool?(VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.UseDatabaseQuotaDefaults.Enabled);
			}
			bool flag = false;
			if ("LinkedRoomMailbox" == base.ParameterSetName)
			{
				user.MasterAccountSid = this.linkedUserSid;
				user.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(this.linkedUserSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				user.ResourceType = new ExchangeResourceType?(ExchangeResourceType.Room);
			}
			else if ("Linked" == base.ParameterSetName)
			{
				user.MasterAccountSid = this.linkedUserSid;
				user.LinkedMasterAccount = SecurityPrincipalIdParameter.GetFriendlyUserName(this.linkedUserSid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			else if ("Shared" == base.ParameterSetName)
			{
				user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
			}
			else if ("Room" == base.ParameterSetName)
			{
				user.ResourceType = new ExchangeResourceType?(ExchangeResourceType.Room);
				user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
			}
			else if ("Equipment" == base.ParameterSetName)
			{
				user.ResourceType = new ExchangeResourceType?(ExchangeResourceType.Equipment);
				user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
			}
			else if ("AuditLog" == base.ParameterSetName)
			{
				user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
				user.HiddenFromAddressListsEnabled = true;
				user.CalendarVersionStoreDisabled = true;
				user.UseDatabaseQuotaDefaults = new bool?(false);
				user.RecoverableItemsQuota = ByteQuantifiedSize.FromGB(50UL);
			}
			else if ("Discovery" == base.ParameterSetName)
			{
				user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
				user.ProhibitSendReceiveQuota = ByteQuantifiedSize.FromGB(50UL);
				user.ProhibitSendQuota = ByteQuantifiedSize.FromGB(50UL);
				user.CalendarVersionStoreDisabled = true;
				user.UseDatabaseQuotaDefaults = new bool?(false);
				user.HiddenFromAddressListsEnabled = true;
				user.MaxReceiveSize = ByteQuantifiedSize.FromMB(100UL);
				user.MaxSendSize = ByteQuantifiedSize.FromMB(100UL);
			}
			else if ("Arbitration" == base.ParameterSetName)
			{
				if (!user.IsModified(ADRecipientSchema.DisplayName))
				{
					user.DisplayName = Strings.ArbitrationMailboxDefaultDisplayName;
				}
				user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
				user.HiddenFromAddressListsEnabled = true;
				user.RequireAllSendersAreAuthenticated = true;
				user.ProhibitSendQuota = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				user.ProhibitSendReceiveQuota = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				user.IssueWarningQuota = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
				user.UseDatabaseQuotaDefaults = new bool?(false);
				ADObjectId childId;
				if (user.OrganizationId.ConfigurationUnit == null)
				{
					childId = this.ConfigurationSession.GetOrgContainerId().GetChildId(ApprovalApplicationContainer.DefaultName);
				}
				else
				{
					childId = user.OrganizationId.ConfigurationUnit.GetChildId(ApprovalApplicationContainer.DefaultName);
				}
				if (this.ConfigurationSession.Read<ApprovalApplicationContainer>(childId) == null)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorRootContainerNotExist(childId.ToString())), ErrorCategory.ObjectNotFound, null);
				}
				if (!NewMailboxBase.IsNonApprovalArbitrationMailboxName(user.Name))
				{
					if (user.ManagedFolderMailboxPolicy == null && user.RetentionPolicy == null)
					{
						ADObjectId childId2;
						if (user.OrganizationId.ConfigurationUnit == null)
						{
							childId2 = this.ConfigurationSession.GetOrgContainerId().GetChildId("Retention Policies Container").GetChildId("ArbitrationMailbox");
						}
						else
						{
							childId2 = user.OrganizationId.ConfigurationUnit.GetChildId("Retention Policies Container").GetChildId("ArbitrationMailbox");
						}
						RetentionPolicy retentionPolicy = this.ConfigurationSession.Read<RetentionPolicy>(childId2);
						if (retentionPolicy != null)
						{
							user.RetentionPolicy = retentionPolicy.Id;
							flag = true;
						}
						else
						{
							this.WriteWarning(Strings.WarningArbitrationRetentionPolicyNotAvailable(childId2.ToString()));
						}
					}
					ApprovalApplication[] array = this.ConfigurationSession.Find<ApprovalApplication>(childId, QueryScope.SubTree, null, null, 0);
					List<ADObjectId> list2 = new List<ADObjectId>(array.Length);
					foreach (ApprovalApplication approvalApplication in array)
					{
						list2.Add((ADObjectId)approvalApplication.Identity);
					}
					user[ADUserSchema.ApprovalApplications] = new MultiValuedProperty<ADObjectId>(list2);
				}
			}
			else if ("PublicFolder" == base.ParameterSetName)
			{
				user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
				user.HiddenFromAddressListsEnabled = true;
				if (!RecipientTaskHelper.IsE15OrLater(this.databaseLocationInfo.ServerVersion))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMailboxDatabaseNotOnE15Server(this.database.ToString())), ExchangeErrorCategory.Client, null);
				}
			}
			MailboxTaskHelper.StampMailboxRecipientTypes(user, base.ParameterSetName);
			user.Database = this.database.Id;
			user.ServerLegacyDN = this.databaseLocationInfo.ServerLegacyDN;
			if (!flag)
			{
				user.RetentionPolicy = this.retentionPolicyId;
			}
			user.ActiveSyncMailboxPolicy = this.mobileMailboxPolicyId;
			user.AddressBookPolicy = this.addressBookPolicyId;
			if (this.userSpecifiedRoleAssignmentPolicyId != null)
			{
				user.RoleAssignmentPolicy = this.userSpecifiedRoleAssignmentPolicyId;
			}
			else if (user.RoleAssignmentPolicy == null && this.defaultRoleAssignmentPolicyId != null)
			{
				user.RoleAssignmentPolicy = this.defaultRoleAssignmentPolicyId;
			}
			if (this.originalRecipientType != RecipientType.MailUser && user.WindowsLiveID != SmtpAddress.Empty)
			{
				user.EmailAddressPolicyEnabled = false;
				SmtpProxyAddress item = new SmtpProxyAddress(user.WindowsLiveID.ToString(), false);
				if (!user.EmailAddresses.Contains(item))
				{
					user.EmailAddresses.Add(item);
				}
			}
			if (!string.IsNullOrEmpty(base.Alias))
			{
				user.Alias = base.Alias;
			}
			MailboxTaskHelper.WriteWarningWhenMailboxIsUnlicensed(user, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			user.ShouldUseDefaultRetentionPolicy = true;
			user.ElcMailboxFlags |= ElcMailboxFlags.ElcV2;
			TaskLogger.LogExit();
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001E634 File Offset: 0x0001C834
		private void ReloadArchiveMailbox()
		{
			if (this.recoverArchive && this.DataObject.ArchiveDatabase != null)
			{
				MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(new DatabaseIdParameter(this.DataObject.ArchiveDatabase), base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.DataObject.ArchiveDatabase.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.DataObject.ArchiveDatabase.ToString())));
				using (MapiAdministrationSession adminSession = MapiTaskHelper.GetAdminSession(this.ActiveManager, this.DataObject.ArchiveDatabase.ObjectGuid))
				{
					ConnectMailbox.UpdateSDAndRefreshMailbox(adminSession, this.DataObject, mailboxDatabase, this.DataObject.ArchiveGuid, null, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				}
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001E720 File Offset: 0x0001C920
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			if ("Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName)
			{
				base.InternalProcessRecord();
				this.ReloadArchiveMailbox();
				this.WriteResult();
				TaskLogger.LogExit();
				return;
			}
			if (this.DataObject.UMEnabled)
			{
				Utils.DoUMEnablingSynchronousWork(this.DataObject);
			}
			bool flag = false;
			if ("Linked" == base.ParameterSetName || "Shared" == base.ParameterSetName || "Room" == base.ParameterSetName || "LinkedRoomMailbox" == base.ParameterSetName || "Equipment" == base.ParameterSetName)
			{
				MailboxTaskHelper.GrantPermissionToLinkedUserAccount(this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				flag = true;
			}
			else if ("Arbitration" == base.ParameterSetName)
			{
				MailboxTaskHelper.GrantPermissionToLinkedUserAccount(this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				flag = true;
			}
			else if ("Discovery" == base.ParameterSetName)
			{
				this.DataObject.AcceptMessagesOnlyFrom.Add(this.DataObject.Id);
			}
			else if ("PublicFolder" == base.ParameterSetName && this.PublicFolder)
			{
				IConfigurationSession tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId, false, null, null);
				Organization orgContainer = tenantLocalConfigSession.GetOrgContainer();
				if (orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid == Guid.Empty)
				{
					orgContainer.DefaultPublicFolderMailbox = orgContainer.DefaultPublicFolderMailbox.Clone();
					orgContainer.DefaultPublicFolderMailbox.SetHierarchyMailbox(this.DataObject.ExchangeGuid, this.HoldForMigration ? PublicFolderInformation.HierarchyType.InTransitMailboxGuid : PublicFolderInformation.HierarchyType.MailboxGuid);
					tenantLocalConfigSession.Save(orgContainer);
					MailboxTaskHelper.PrepopulateCacheForMailbox(this.database, this.databaseLocationInfo.ServerFqdn, this.DataObject.OrganizationId, this.DataObject.LegacyExchangeDN, this.DataObject.ExchangeGuid, tenantLocalConfigSession.LastUsedDc, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
				}
			}
			base.InternalProcessRecord();
			if (flag)
			{
				base.WriteVerbose(Strings.VerboseSaveADSecurityDescriptor(this.DataObject.Id.ToString()));
				try
				{
					this.DataObject.SaveSecurityDescriptor(((SecurityDescriptor)this.DataObject[ADObjectSchema.NTSecurityDescriptor]).ToRawSecurityDescriptor());
				}
				catch (ADOperationException ex)
				{
					TaskLogger.Trace("An exception is caught and ignored when enabling the mailbox '{0}'. Exception: {1}", new object[]
					{
						this.DataObject.Id.ToString(),
						ex.Message
					});
					this.WriteWarning(Strings.WarningNTSecurityDescriptorNotUpdated(this.DataObject.Id.ToString(), ex.Message));
				}
			}
			if (this.recoveredMailbox)
			{
				using (MapiAdministrationSession adminSession = MapiTaskHelper.GetAdminSession(this.ActiveManager, this.DataObject.Database.ObjectGuid))
				{
					ConnectMailbox.UpdateSDAndRefreshMailbox(adminSession, this.DataObject, (MailboxDatabase)this.database, this.DataObject.ExchangeGuid, base.ParameterSetName, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				}
				this.ReloadArchiveMailbox();
			}
			this.WriteResult();
			TaskLogger.LogExit();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001EAD0 File Offset: 0x0001CCD0
		protected override void InternalValidate()
		{
			if ("Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName)
			{
				if (this.ArchiveDatabase != null)
				{
					this.ValidateAndSetArchiveDatabase(this.ArchiveDatabase, true);
				}
				TaskLogger.Trace("EnableMailbox -Archive or -RemoteArchive, skip Database set and validation, Database will not be resolved.", new object[0]);
			}
			else if (this.Database != null)
			{
				this.ValidateAndSetDatabase(this.Database, true);
			}
			else if (!base.IsProvisioningLayerAvailable)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorAutomaticProvisioningFailedToFindDatabase("Database")), ErrorCategory.InvalidData, null);
			}
			base.InternalValidate();
			if (this.AuditLog)
			{
				if (this.DataObject.RecipientTypeDetails != RecipientTypeDetails.AuditLogMailbox)
				{
					if (this.DataObject.RecipientTypeDetails != RecipientTypeDetails.UserMailbox)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorNoAuditLogForNonUserMailbox(this.DataObject.Name)), ExchangeErrorCategory.Client, this.DataObject);
					}
					else if (this.DataObject.ArchiveGuid != Guid.Empty)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorNoAuditLogForArchive(this.DataObject.Name)), ExchangeErrorCategory.Client, this.DataObject);
					}
				}
			}
			else if (this.DataObject.RecipientTypeDetails == RecipientTypeDetails.AuditLogMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.RecipientNotFoundException(this.DataObject.Name)), ExchangeErrorCategory.Client, this.DataObject);
			}
			if (this.DataObject.IsModified(ADUserSchema.ArchiveGuid) && this.ArchiveGuid != Guid.Empty)
			{
				RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(this.DataObject, ADUserSchema.ArchiveGuid, this.ArchiveGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (("Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName) && this.DataObject.RecipientTypeDetails == RecipientTypeDetails.TeamMailbox)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorNoArchiveForTeamMailbox(this.DataObject.Name)), ExchangeErrorCategory.Client, this.DataObject);
			}
			if (this.DataObject.MailboxProvisioningConstraint != null)
			{
				MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(new MailboxProvisioningConstraint[]
				{
					this.DataObject.MailboxProvisioningConstraint
				}, base.DomainController, delegate(string message)
				{
					base.WriteVerbose(new LocalizedString(message));
				}, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.DataObject.MailboxProvisioningPreferences != null)
			{
				MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(this.DataObject.MailboxProvisioningPreferences, base.DomainController, delegate(string message)
				{
					base.WriteVerbose(new LocalizedString(message));
				}, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			MailboxTaskHelper.EnsureUserSpecifiedDatabaseMatchesMailboxProvisioningConstraint(this.database, this.archiveDatabase, base.Fields, this.DataObject.MailboxProvisioningConstraint, new Task.ErrorLoggerDelegate(base.WriteError), "Database");
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001EDC4 File Offset: 0x0001CFC4
		private void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Id
			});
			Mailbox sendToPipeline = new Mailbox(this.DataObject);
			base.WriteObject(sendToPipeline);
			TaskLogger.LogExit();
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001EE04 File Offset: 0x0001D004
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001EE0D File Offset: 0x0001D00D
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return Mailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001EE1C File Offset: 0x0001D01C
		protected override void ValidateProvisionedProperties(IConfigurable dataObject)
		{
			if (this.Database != null || "Archive" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName)
			{
				return;
			}
			this.recoveredMailbox = false;
			ADUser aduser = dataObject as ADUser;
			if (aduser == null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorParameterRequiredButNotProvisioned("Database")), ErrorCategory.InvalidData, null);
			}
			if (aduser.PreviousDatabase != null && Guid.Empty != aduser.PreviousExchangeGuid && MailboxTaskHelper.FindConnectedMailbox(RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, aduser.Id), aduser.PreviousExchangeGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose)) == null)
			{
				ADObjectId deletedObjectsContainer = this.ConfigurationSession.DeletedObjectsContainer;
				ADObjectId adobjectId = ADObjectIdResolutionHelper.ResolveDN(aduser.PreviousDatabase);
				if (adobjectId.Parent == null || adobjectId.Parent.Equals(deletedObjectsContainer))
				{
					aduser.PreviousDatabase = null;
					aduser.PreviousExchangeGuid = Guid.Empty;
				}
				else
				{
					using (MapiAdministrationSession adminSession = MapiTaskHelper.GetAdminSession(this.ActiveManager, adobjectId.ObjectGuid))
					{
						string mailboxLegacyDN = MapiTaskHelper.GetMailboxLegacyDN(adminSession, adobjectId, aduser.PreviousExchangeGuid);
						if (mailboxLegacyDN != null && ConnectMailbox.FindMailboxByLegacyDN(mailboxLegacyDN, RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, aduser.Id)) == null)
						{
							this.recoveredMailbox = true;
							aduser.Database = adobjectId;
							aduser.DatabaseAndLocation = null;
							aduser.ExchangeGuid = aduser.PreviousExchangeGuid;
							aduser.LegacyExchangeDN = mailboxLegacyDN;
							aduser.PreviousDatabase = null;
							aduser.PreviousExchangeGuid = Guid.Empty;
						}
					}
					if (this.recoveredMailbox)
					{
						this.recoverArchive = this.IsArchiveRecoverable(aduser);
						if (this.recoverArchive)
						{
							aduser.ArchiveGuid = aduser.DisabledArchiveGuid;
							aduser.ArchiveName = ((this.ArchiveName == null) ? new MultiValuedProperty<string>(Strings.ArchiveNamePrefix + aduser.DisplayName) : this.ArchiveName);
							aduser.ArchiveDatabase = aduser.DisabledArchiveDatabase;
						}
					}
				}
			}
			if (!aduser.IsChanged(IADMailStorageSchema.Database))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorParameterRequiredButNotProvisioned("Database")), ErrorCategory.InvalidData, null);
				return;
			}
			MailboxDatabaseWithLocationInfo mailboxDatabaseWithLocationInfo = aduser.DatabaseAndLocation as MailboxDatabaseWithLocationInfo;
			if (mailboxDatabaseWithLocationInfo == null)
			{
				this.ValidateAndSetDatabase(new DatabaseIdParameter(aduser.Database), false);
				return;
			}
			this.database = mailboxDatabaseWithLocationInfo.MailboxDatabase;
			this.databaseLocationInfo = mailboxDatabaseWithLocationInfo.DatabaseLocationInfo;
			aduser.DatabaseAndLocation = null;
			aduser.propertyBag.ResetChangeTracking(IADMailStorageSchema.DatabaseAndLocation);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001F090 File Offset: 0x0001D290
		private void ValidateAndSetDatabase(DatabaseIdParameter databaseId, bool throwOnError)
		{
			this.InternalValidateAndSetArchiveDatabase(databaseId, Server.E15MinVersion, throwOnError, out this.database, out this.databaseLocationInfo);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001F0AB File Offset: 0x0001D2AB
		private void ValidateAndSetArchiveDatabase(DatabaseIdParameter databaseId, bool throwOnError)
		{
			this.InternalValidateAndSetArchiveDatabase(databaseId, Server.E15MinVersion, throwOnError, out this.archiveDatabase, out this.archiveDatabaseLocationInfo);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001F0C8 File Offset: 0x0001D2C8
		private void InternalValidateAndSetArchiveDatabase(DatabaseIdParameter databaseId, int minServerVersion, bool throwOnError, out Database database, out DatabaseLocationInfo databaseLocationInfo)
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugStartSetDatabase);
			}
			database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(databaseId, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(databaseId.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(databaseId.ToString())));
			databaseLocationInfo = this.GetDatabaseLocationInfo(database);
			Exception ex = null;
			if (minServerVersion > databaseLocationInfo.ServerVersion)
			{
				ex = new RecipientTaskException(Strings.ErrorMailboxDatabaseNotOnE15Server(database.ToString()));
			}
			else if (((MailboxDatabase)database).Recovery)
			{
				ex = new RecipientTaskException(Strings.ErrorRecoveryDatabase(database.Name));
			}
			if (ex != null)
			{
				if (throwOnError)
				{
					base.ThrowTerminatingError(ex, (ErrorCategory)1001, null);
				}
				else
				{
					base.WriteError(ex, (ErrorCategory)1001, null);
				}
			}
			MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(base.SessionSettings, database, new Task.ErrorLoggerDelegate(base.WriteError));
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugEndSetDatabase);
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001F1C0 File Offset: 0x0001D3C0
		private DatabaseLocationInfo GetDatabaseLocationInfo(Database database)
		{
			try
			{
				return this.ActiveManager.GetServerForDatabase(database.Guid);
			}
			catch (ObjectNotFoundException exception)
			{
				base.WriteError(exception, (ErrorCategory)1000, null);
			}
			catch (ServerForDatabaseNotFoundException exception2)
			{
				base.WriteError(exception2, (ErrorCategory)1000, null);
			}
			return null;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001F220 File Offset: 0x0001D420
		private bool IsArchiveRecoverable(ADUser user)
		{
			bool result = false;
			if (this.archiveDatabase == null && user.DisabledArchiveGuid != Guid.Empty && (this.ArchiveGuid == Guid.Empty || this.ArchiveGuid == user.DisabledArchiveGuid))
			{
				result = ("RemoteArchive" == base.ParameterSetName || MailboxTaskHelper.IsArchiveRecoverable(user, this.ConfigurationSession, RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, user.Id)));
			}
			return result;
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001F2A8 File Offset: 0x0001D4A8
		private void CreateArchiveIfNecessary(ADUser user)
		{
			if (user.ArchiveGuid == Guid.Empty)
			{
				this.recoverArchive = this.IsArchiveRecoverable(user);
				user.ArchiveGuid = (this.recoverArchive ? user.DisabledArchiveGuid : ((this.ArchiveGuid == Guid.Empty) ? Guid.NewGuid() : this.ArchiveGuid));
				user.ArchiveName = ((this.ArchiveName == null) ? new MultiValuedProperty<string>(Strings.ArchiveNamePrefix + user.DisplayName) : this.ArchiveName);
				if ("RemoteArchive" == base.ParameterSetName)
				{
					user.ArchiveDomain = this.ArchiveDomain;
					user.RemoteRecipientType = ((user.RemoteRecipientType &= ~RemoteRecipientType.DeprovisionArchive) | RemoteRecipientType.ProvisionArchive);
				}
				else
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.CmdletInfra.SetActiveArchiveStatus.Enabled)
					{
						user.ArchiveStatus |= ArchiveStatusFlags.Active;
					}
					if (this.recoverArchive)
					{
						user.ArchiveDatabase = user.DisabledArchiveDatabase;
					}
					else if (this.archiveDatabase == null)
					{
						user.ArchiveDatabase = user.Database;
					}
					else
					{
						user.ArchiveDatabase = this.archiveDatabase.Id;
					}
				}
				MailboxTaskHelper.ApplyDefaultArchivePolicy(user, this.ConfigurationSession);
				return;
			}
			base.WriteError(new RecipientTaskException(Strings.ErrorArchiveAlreadyPresent(this.Identity.ToString())), (ErrorCategory)1003, null);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001F40C File Offset: 0x0001D60C
		private void StripInvalidSMTPAddresses(ADUser user)
		{
			ProxyAddressCollection emailAddresses = user.EmailAddresses;
			ProxyAddressCollection proxyAddressCollection = new ProxyAddressCollection();
			ProxyAddress proxyAddress = ProxyAddress.Parse(user.WindowsLiveID.ToString());
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (ProxyAddress proxyAddress2 in emailAddresses)
			{
				SmtpAddress smtpAddress = new SmtpAddress(proxyAddress2.AddressString);
				if (!(proxyAddress2 is SmtpProxyAddress) || !smtpAddress.IsValidAddress)
				{
					proxyAddressCollection.Add(proxyAddress2);
				}
				else if (RecipientTaskHelper.IsAcceptedDomain(this.ConfigurationSession, user.OrganizationId, smtpAddress.Domain, base.ProvisioningCache))
				{
					if (proxyAddress2.IsPrimaryAddress)
					{
						flag = true;
					}
					if (string.Compare(proxyAddress2.AddressString, user.WindowsLiveID.ToString(), StringComparison.InvariantCultureIgnoreCase) != 0)
					{
						proxyAddressCollection.Add(proxyAddress2);
						if (proxyAddress2.IsPrimaryAddress)
						{
							flag3 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				if (flag2)
				{
					if (flag3)
					{
						proxyAddressCollection.Add(proxyAddress);
					}
					else
					{
						proxyAddressCollection.Add(proxyAddress.ToPrimary());
					}
				}
			}
			else
			{
				proxyAddressCollection.Add(proxyAddress.ToPrimary());
			}
			user.EmailAddresses = proxyAddressCollection;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001F554 File Offset: 0x0001D754
		protected override void PrepareRecipientAlias(ADUser dataObject)
		{
			if (!string.IsNullOrEmpty(base.Alias))
			{
				dataObject.Alias = base.Alias;
				return;
			}
			dataObject.Alias = RecipientTaskHelper.GenerateUniqueAlias(base.TenantGlobalCatalogSession, dataObject.OrganizationId, string.IsNullOrEmpty(dataObject.UserPrincipalName) ? dataObject.SamAccountName : RecipientTaskHelper.GetLocalPartOfUserPrincalName(dataObject.UserPrincipalName), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001F5C0 File Offset: 0x0001D7C0
		private ADUser FindMailboxPlanWithName(string mailboxPlanName, IRecipientSession session)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				MailboxTaskHelper.mailboxPlanFilter,
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, mailboxPlanName)
			});
			bool includeSoftDeletedObjects = session.SessionSettings.IncludeSoftDeletedObjects;
			ADUser[] array = null;
			try
			{
				session.SessionSettings.IncludeSoftDeletedObjects = false;
				array = session.FindADUser(null, QueryScope.SubTree, filter, null, 1);
			}
			finally
			{
				session.SessionSettings.IncludeSoftDeletedObjects = includeSoftDeletedObjects;
			}
			if (array.Length == 1)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x04000191 RID: 401
		private static readonly PropertyDefinition[] PropertiesToResetForMailUser = new PropertyDefinition[]
		{
			ADMailboxRecipientSchema.ExchangeGuid,
			ADRecipientSchema.RawExternalEmailAddress,
			ADRecipientSchema.RecipientDisplayType,
			ADRecipientSchema.RecipientLimits,
			ADRecipientSchema.RecipientTypeDetails,
			ADMailboxRecipientSchema.RulesQuota,
			ADUserSchema.MailboxMoveTargetMDB,
			ADUserSchema.MailboxMoveSourceMDB,
			ADUserSchema.MailboxMoveFlags,
			ADUserSchema.MailboxMoveStatus,
			ADUserSchema.MailboxMoveRemoteHostName,
			ADUserSchema.MailboxMoveBatchName
		};

		// Token: 0x04000192 RID: 402
		private Database database;

		// Token: 0x04000193 RID: 403
		private Database archiveDatabase;

		// Token: 0x04000194 RID: 404
		private ADObjectId retentionPolicyId;

		// Token: 0x04000195 RID: 405
		private ADObjectId mobileMailboxPolicyId;

		// Token: 0x04000196 RID: 406
		private ADObjectId userSpecifiedRoleAssignmentPolicyId;

		// Token: 0x04000197 RID: 407
		private ADObjectId defaultRoleAssignmentPolicyId;

		// Token: 0x04000198 RID: 408
		private ADObjectId addressBookPolicyId;

		// Token: 0x04000199 RID: 409
		private SecurityIdentifier linkedUserSid;

		// Token: 0x0400019A RID: 410
		private DatabaseLocationInfo databaseLocationInfo;

		// Token: 0x0400019B RID: 411
		private DatabaseLocationInfo archiveDatabaseLocationInfo;

		// Token: 0x0400019C RID: 412
		private MailboxPlan mailboxPlan;

		// Token: 0x0400019D RID: 413
		private RecipientType originalRecipientType;

		// Token: 0x0400019E RID: 414
		public static readonly string DiscoveryMailboxUniqueName = "DiscoverySearchMailbox {D919BA05-46A6-415f-80AD-7E09334BB852}";

		// Token: 0x0400019F RID: 415
		public static readonly string DiscoveryMailboxDisplayName = Strings.DiscoveryMailboxDisplayName;

		// Token: 0x040001A0 RID: 416
		private bool recoveredMailbox;

		// Token: 0x040001A1 RID: 417
		private bool recoverArchive;
	}
}
