using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Net;
using System.Security;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Mapi;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics.CmdletInfra;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.MapiTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Provisioning.LoadBalancing;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000046 RID: 70
	public class NewMailboxBase : NewUserBase
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000E160 File Offset: 0x0000C360
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("Shared" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxShared(base.Name.ToString(), this.Shared.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if ("Room" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxResource(base.Name.ToString(), ExchangeResourceType.Room.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if ("Equipment" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxResource(base.Name.ToString(), ExchangeResourceType.Equipment.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if ("Linked" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxLinked(base.Name.ToString(), this.LinkedMasterAccount.ToString(), this.LinkedDomainController.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if ("LinkedWithSyncMailbox" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxLinkedWithSyncMailbox(base.Name.ToString(), this.DataObject.MasterAccountSid.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if ("WindowsLiveID" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxWithWindowsLiveId(base.Name.ToString(), base.WindowsLiveID.SmtpAddress.ToString(), base.RecipientContainerId.ToString());
				}
				if ("Arbitration" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxArbitration(base.Name.ToString(), this.database.ToString(), this.Arbitration.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if ("PublicFolder" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxPublicFolder(base.Name.ToString(), this.database.ToString(), this.PublicFolder.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if ("Discovery" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxDiscovery(base.Name.ToString(), this.database.ToString(), this.Discovery.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if ("AuditLog" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxAuditLog(base.Name.ToString(), this.database.ToString(), this.AuditLog.ToString(), this.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
				}
				if (this.Archive.IsPresent)
				{
					return Strings.ConfirmationMessageNewMailboxWithArchive(base.Name.ToString());
				}
				if ("RemoteArchive" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewMailboxWithRemoteArchive(base.Name.ToString(), this.ArchiveDomain.ToString());
				}
				return Strings.ConfirmationMessageNewMailboxUser(base.Name.ToString(), this.DataObject.UserPrincipalName.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000E4FE File Offset: 0x0000C6FE
		protected virtual bool runUMSteps
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000E501 File Offset: 0x0000C701
		private ActiveManager ActiveManager
		{
			get
			{
				return RecipientTaskHelper.GetActiveManagerInstance();
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000E508 File Offset: 0x0000C708
		private bool IsCreatingResourceOrSharedMB
		{
			get
			{
				return "Room" == base.ParameterSetName || "LinkedRoomMailbox" == base.ParameterSetName || "EnableRoomMailboxAccount" == base.ParameterSetName || "Equipment" == base.ParameterSetName || base.ParameterSetName == "TeamMailboxIW" || base.ParameterSetName == "TeamMailboxITPro" || base.ParameterSetName == "GroupMailbox" || base.ParameterSetName == "Shared";
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		private bool IsCreatingLogonDisabledTypeMB
		{
			get
			{
				return "Linked" == base.ParameterSetName || this.IsCreatingResourceOrSharedMB || "DisabledUser" == base.ParameterSetName || "LinkedWithSyncMailbox" == base.ParameterSetName || "Discovery" == base.ParameterSetName || "PublicFolder" == base.ParameterSetName || "AuxMailbox" == base.ParameterSetName;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000E62C File Offset: 0x0000C82C
		protected virtual bool ShouldGenerateWindowsLiveID
		{
			get
			{
				return this.IsCreatingResourceOrSharedMB && base.WindowsLiveID == null && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.WindowsLiveID.Enabled && !base.CurrentOrganizationId.Equals(OrganizationId.ForestWideOrgId);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000E67E File Offset: 0x0000C87E
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000E686 File Offset: 0x0000C886
		[Parameter(Mandatory = false, ParameterSetName = "Discovery")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxIW")]
		[Parameter(Mandatory = false, ParameterSetName = "Arbitration")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = true, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = true, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "AuxMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoteArchive")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = true, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedWithSyncMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		public override SecureString Password
		{
			get
			{
				return base.Password;
			}
			set
			{
				base.Password = value;
				base.Fields["Password"] = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		[Parameter(Mandatory = false, ParameterSetName = "EnableRoomMailboxAccount")]
		public SecureString RoomMailboxPassword
		{
			get
			{
				return base.Password;
			}
			set
			{
				base.Password = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000E6B1 File Offset: 0x0000C8B1
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000E6C8 File Offset: 0x0000C8C8
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public MailboxProvisioningConstraint MailboxProvisioningConstraint
		{
			get
			{
				return (MailboxProvisioningConstraint)base.Fields[ADRecipientSchema.MailboxProvisioningConstraint];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxProvisioningConstraint] = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000E6DB File Offset: 0x0000C8DB
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000E6F2 File Offset: 0x0000C8F2
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
		{
			get
			{
				return (MultiValuedProperty<MailboxProvisioningConstraint>)base.Fields[ADRecipientSchema.MailboxProvisioningPreferences];
			}
			set
			{
				base.Fields[ADRecipientSchema.MailboxProvisioningPreferences] = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000E705 File Offset: 0x0000C905
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000E70D File Offset: 0x0000C90D
		[Parameter(Mandatory = true, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = true, ParameterSetName = "RemoteArchive")]
		[Parameter(Mandatory = true, ParameterSetName = "AuditLog")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxIW")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[Parameter(Mandatory = true, ParameterSetName = "Arbitration")]
		[Parameter(Mandatory = true, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "Discovery")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedWithSyncMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "EnableRoomMailboxAccount")]
		[Parameter(Mandatory = false, ParameterSetName = "AuxMailbox")]
		public override string UserPrincipalName
		{
			get
			{
				return base.UserPrincipalName;
			}
			set
			{
				base.UserPrincipalName = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000E716 File Offset: 0x0000C916
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000E72D File Offset: 0x0000C92D
		[Parameter(Mandatory = false)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields[ADMailboxRecipientSchema.Database];
			}
			set
			{
				base.Fields[ADMailboxRecipientSchema.Database] = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000E740 File Offset: 0x0000C940
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000E757 File Offset: 0x0000C957
		[Parameter(Mandatory = true, ParameterSetName = "Room")]
		[Parameter(Mandatory = true, ParameterSetName = "EnableRoomMailboxAccount")]
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000E76F File Offset: 0x0000C96F
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000E786 File Offset: 0x0000C986
		[Parameter(Mandatory = true, ParameterSetName = "LinkedRoomMailbox")]
		public SwitchParameter LinkedRoom
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

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000E79E File Offset: 0x0000C99E
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000E7C9 File Offset: 0x0000C9C9
		[Parameter(Mandatory = true, ParameterSetName = "EnableRoomMailboxAccount")]
		public bool EnableRoomMailboxAccount
		{
			get
			{
				return base.Fields["EnableRoomMailboxAccount"] != null && (bool)base.Fields["EnableRoomMailboxAccount"];
			}
			set
			{
				base.Fields["EnableRoomMailboxAccount"] = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000E7E1 File Offset: 0x0000C9E1
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
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

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000E810 File Offset: 0x0000CA10
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000E827 File Offset: 0x0000CA27
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000E83F File Offset: 0x0000CA3F
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000E865 File Offset: 0x0000CA65
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000E87D File Offset: 0x0000CA7D
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000E8A3 File Offset: 0x0000CAA3
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000E8BB File Offset: 0x0000CABB
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000E8E1 File Offset: 0x0000CAE1
		[Parameter(Mandatory = true, ParameterSetName = "DisabledUser")]
		public SwitchParameter AccountDisabled
		{
			get
			{
				return (SwitchParameter)(base.Fields["DisabledUser"] ?? false);
			}
			set
			{
				base.Fields["DisabledUser"] = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000E8F9 File Offset: 0x0000CAF9
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000E910 File Offset: 0x0000CB10
		[Parameter(Mandatory = true, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = true, ParameterSetName = "Linked")]
		public UserIdParameter LinkedMasterAccount
		{
			get
			{
				return (UserIdParameter)base.Fields[MailboxSchema.LinkedMasterAccount];
			}
			set
			{
				base.Fields[MailboxSchema.LinkedMasterAccount] = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000E923 File Offset: 0x0000CB23
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000E93A File Offset: 0x0000CB3A
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

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000E94D File Offset: 0x0000CB4D
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000E964 File Offset: 0x0000CB64
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000E977 File Offset: 0x0000CB77
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000E98E File Offset: 0x0000CB8E
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

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000E9A1 File Offset: 0x0000CBA1
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		[Parameter]
		public MailboxPolicyIdParameter ActiveSyncMailboxPolicy
		{
			get
			{
				return (MailboxPolicyIdParameter)base.Fields[ADUserSchema.ActiveSyncMailboxPolicy];
			}
			set
			{
				base.Fields[ADUserSchema.ActiveSyncMailboxPolicy] = value;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000E9CB File Offset: 0x0000CBCB
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000E9E2 File Offset: 0x0000CBE2
		[Parameter]
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000E9F5 File Offset: 0x0000CBF5
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000EA0C File Offset: 0x0000CC0C
		[Parameter]
		public ThrottlingPolicyIdParameter ThrottlingPolicy
		{
			get
			{
				return (ThrottlingPolicyIdParameter)base.Fields[MailboxSchema.ThrottlingPolicy];
			}
			set
			{
				base.Fields[MailboxSchema.ThrottlingPolicy] = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000EA1F File Offset: 0x0000CC1F
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000EA36 File Offset: 0x0000CC36
		[Parameter]
		public SharingPolicyIdParameter SharingPolicy
		{
			get
			{
				return (SharingPolicyIdParameter)base.Fields[MailboxSchema.SharingPolicy];
			}
			set
			{
				base.Fields[MailboxSchema.SharingPolicy] = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000EA49 File Offset: 0x0000CC49
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000EA60 File Offset: 0x0000CC60
		[Parameter]
		public RemoteAccountPolicyIdParameter RemoteAccountPolicy
		{
			get
			{
				return (RemoteAccountPolicyIdParameter)base.Fields[MailboxSchema.RemoteAccountPolicy];
			}
			set
			{
				base.Fields[MailboxSchema.RemoteAccountPolicy] = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000EA73 File Offset: 0x0000CC73
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000EA8A File Offset: 0x0000CC8A
		[Parameter]
		public bool RemotePowerShellEnabled
		{
			get
			{
				return (bool)base.Fields[MailboxSchema.RemotePowerShellEnabled];
			}
			set
			{
				base.Fields[MailboxSchema.RemotePowerShellEnabled] = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000EAA2 File Offset: 0x0000CCA2
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000EAB9 File Offset: 0x0000CCB9
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

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000EACC File Offset: 0x0000CCCC
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000EAE3 File Offset: 0x0000CCE3
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxIW")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedWithSyncMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		public override MailboxIdParameter ArbitrationMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields[ADRecipientSchema.ArbitrationMailbox];
			}
			set
			{
				base.Fields[ADRecipientSchema.ArbitrationMailbox] = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000EAF6 File Offset: 0x0000CCF6
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000EB0D File Offset: 0x0000CD0D
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxIW")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedWithSyncMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		public override MultiValuedProperty<ModeratorIDParameter> ModeratedBy
		{
			get
			{
				return (MultiValuedProperty<ModeratorIDParameter>)base.Fields[ADRecipientSchema.ModeratedBy];
			}
			set
			{
				base.Fields[ADRecipientSchema.ModeratedBy] = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000EB20 File Offset: 0x0000CD20
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000EB2D File Offset: 0x0000CD2D
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxIW")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedWithSyncMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		public override bool ModerationEnabled
		{
			get
			{
				return this.DataObject.ModerationEnabled;
			}
			set
			{
				this.DataObject.ModerationEnabled = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000EB3B File Offset: 0x0000CD3B
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000EB48 File Offset: 0x0000CD48
		[Parameter(Mandatory = false, ParameterSetName = "Equipment")]
		[Parameter(Mandatory = false, ParameterSetName = "RemovedMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID")]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxITPro")]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID")]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive")]
		[Parameter(Mandatory = false, ParameterSetName = "Linked")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedWithSyncMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "LinkedRoomMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "TeamMailboxIW")]
		[Parameter(Mandatory = false, ParameterSetName = "MailboxPlan")]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains")]
		[Parameter(Mandatory = false, ParameterSetName = "Room")]
		[Parameter(Mandatory = false, ParameterSetName = "User")]
		[Parameter(Mandatory = false, ParameterSetName = "Shared")]
		[Parameter(Mandatory = false, ParameterSetName = "GroupMailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "DisabledUser")]
		public override TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return this.DataObject.SendModerationNotifications;
			}
			set
			{
				this.DataObject.SendModerationNotifications = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000EB56 File Offset: 0x0000CD56
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000EB63 File Offset: 0x0000CD63
		[Parameter(Mandatory = false)]
		public bool QueryBaseDNRestrictionEnabled
		{
			get
			{
				return this.DataObject.QueryBaseDNRestrictionEnabled;
			}
			set
			{
				this.DataObject.QueryBaseDNRestrictionEnabled = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000EB71 File Offset: 0x0000CD71
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000EB88 File Offset: 0x0000CD88
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveCustomDomains", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "ImportLiveId", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "User", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "RemoteArchive", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesFederatedUser", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "WindowsLiveID", ValueFromPipeline = true)]
		[Parameter(Mandatory = true, ParameterSetName = "RemovedMailbox", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "MicrosoftOnlineServicesID", ValueFromPipeline = true)]
		[Parameter(Mandatory = false, ParameterSetName = "FederatedUser", ValueFromPipeline = true)]
		public RemovedMailboxIdParameter RemovedMailbox
		{
			get
			{
				return (RemovedMailboxIdParameter)base.Fields["RemovedMailbox"];
			}
			set
			{
				base.Fields["RemovedMailbox"] = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000EB9B File Offset: 0x0000CD9B
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000EBC1 File Offset: 0x0000CDC1
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000EBD9 File Offset: 0x0000CDD9
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000EBFF File Offset: 0x0000CDFF
		[Parameter(Mandatory = false, ParameterSetName = "AuxMailbox")]
		public SwitchParameter AuxMailbox
		{
			get
			{
				return (SwitchParameter)(base.Fields["AuxMailbox"] ?? false);
			}
			set
			{
				base.Fields["AuxMailbox"] = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000EC17 File Offset: 0x0000CE17
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000EC2E File Offset: 0x0000CE2E
		[Parameter(Mandatory = false)]
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

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000EC41 File Offset: 0x0000CE41
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000EC67 File Offset: 0x0000CE67
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

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000EC7F File Offset: 0x0000CE7F
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000EC96 File Offset: 0x0000CE96
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

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000ECA9 File Offset: 0x0000CEA9
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000ECCF File Offset: 0x0000CECF
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

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000ECE7 File Offset: 0x0000CEE7
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000ECFE File Offset: 0x0000CEFE
		[Parameter(Mandatory = false, ParameterSetName = "PublicFolder")]
		public bool IsExcludedFromServingHierarchy
		{
			get
			{
				return (bool)base.Fields[ADRecipientSchema.IsExcludedFromServingHierarchy];
			}
			set
			{
				base.Fields[ADRecipientSchema.IsExcludedFromServingHierarchy] = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000ED16 File Offset: 0x0000CF16
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000ED3C File Offset: 0x0000CF3C
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

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000ED54 File Offset: 0x0000CF54
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000ED8F File Offset: 0x0000CF8F
		[Parameter(Mandatory = false)]
		public Guid MailboxContainerGuid
		{
			get
			{
				if (this.DataObject.MailboxContainerGuid == null)
				{
					return Guid.Empty;
				}
				return this.DataObject.MailboxContainerGuid.Value;
			}
			set
			{
				this.DataObject.MailboxContainerGuid = new Guid?(value);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000EDA2 File Offset: 0x0000CFA2
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		[Parameter(Mandatory = false)]
		public SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForestWideDomainControllerAffinityByExecutingUser"] ?? false);
			}
			set
			{
				base.Fields["ForestWideDomainControllerAffinityByExecutingUser"] = value;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000EE06 File Offset: 0x0000D006
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

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000EE1E File Offset: 0x0000D01E
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000EE44 File Offset: 0x0000D044
		[Parameter(Mandatory = true, ParameterSetName = "AuditLog")]
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

		// Token: 0x06000386 RID: 902 RVA: 0x0000EE6B File Offset: 0x0000D06B
		internal static bool IsNonApprovalArbitrationMailboxName(string mailboxName)
		{
			return "SystemMailbox{e0dc1c29-89c3-4034-b678-e6c29d823ed9}".Equals(mailboxName, StringComparison.OrdinalIgnoreCase) || "MigrationMailbox{24B27736-B069-46f1-B482-D6D9EAC9B053}".Equals(mailboxName, StringComparison.OrdinalIgnoreCase) || "Migration.8f3e7716-2011-43e4-96b1-aba62d229136".Equals(mailboxName, StringComparison.OrdinalIgnoreCase) || "FederatedEmail.4c1f4d8b-8179-4148-93bf-00a95fa1e042".Equals(mailboxName, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		protected override void StampDefaultValues(ADUser dataObject)
		{
			base.StampDefaultValues(dataObject);
			dataObject.StampDefaultValues(RecipientType.UserMailbox);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000EF00 File Offset: 0x0000D100
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugStartInternalBeginProcessing);
			}
			base.InternalBeginProcessing();
			if ("PublicFolder" == base.ParameterSetName)
			{
				foreach (object obj in NewMailboxBase.InvalidPublicFolderParameters)
				{
					if (base.Fields.IsModified(obj))
					{
						string parameter = (obj is ADPropertyDefinition) ? ((ADPropertyDefinition)obj).Name : obj.ToString();
						base.WriteError(new TaskArgumentException(Strings.ErrorInvalidParameterForPublicFolderTasks(parameter, "PublicFolder")), ExchangeErrorCategory.Client, null);
					}
				}
			}
			if (base.CurrentOrganizationId != null && base.CurrentOrganizationId.ConfigurationUnit != null)
			{
				ExchangeConfigurationUnit exchangeConfigUnit = RecipientTaskHelper.GetExchangeConfigUnit(this.ConfigurationSession, base.CurrentOrganizationId);
				if (exchangeConfigUnit != null && exchangeConfigUnit.UseServicePlanAsCounterInstanceName)
				{
					base.CurrentTaskContext.Items["TenantNameForMonitoring"] = exchangeConfigUnit.ServicePlan;
				}
			}
			if (!("Linked" == base.ParameterSetName))
			{
				if (!("LinkedRoomMailbox" == base.ParameterSetName))
				{
					goto IL_185;
				}
			}
			try
			{
				NetworkCredential userForestCredential = (this.LinkedCredential == null) ? null : this.LinkedCredential.GetNetworkCredential();
				this.linkedUserSid = MailboxTaskHelper.GetAccountSidFromAnotherForest(this.LinkedMasterAccount, this.LinkedDomainController, userForestCredential, base.GlobalConfigSession, new MailboxTaskHelper.GetUniqueObject(base.GetDataObject<ADUser>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
			}
			catch (PSArgumentException exception)
			{
				base.ThrowNonLocalizedTerminatingError(exception, ExchangeErrorCategory.Client, this.LinkedCredential);
			}
			IL_185:
			if (this.RetentionPolicy != null)
			{
				if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
				{
					base.WriteError(new LocalizedException(Strings.ErrorLinkOpOnDehydratedTenant("RetentionPolicy")), ExchangeErrorCategory.Client, null);
				}
				RetentionPolicy retentionPolicy = (RetentionPolicy)base.GetDataObject<RetentionPolicy>(this.RetentionPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRetentionPolicyNotFound(this.RetentionPolicy.ToString())), new LocalizedString?(Strings.ErrorRetentionPolicyNotUnique(this.RetentionPolicy.ToString())), ExchangeErrorCategory.Client);
				this.retentionPolicyId = retentionPolicy.Id;
			}
			if (this.ActiveSyncMailboxPolicy != null)
			{
				MobileMailboxPolicy mobileMailboxPolicy = (MobileMailboxPolicy)base.GetDataObject<MobileMailboxPolicy>(this.ActiveSyncMailboxPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorMobileMailboxPolicyNotFound(this.ActiveSyncMailboxPolicy.ToString())), new LocalizedString?(Strings.ErrorMobileMailboxPolicyNotUnique(this.ActiveSyncMailboxPolicy.ToString())), ExchangeErrorCategory.Client);
				this.mobileMailboxPolicyId = (ADObjectId)mobileMailboxPolicy.Identity;
			}
			if (this.AddressBookPolicy != null)
			{
				AddressBookMailboxPolicy addressBookMailboxPolicy = (AddressBookMailboxPolicy)base.GetDataObject<AddressBookMailboxPolicy>(this.AddressBookPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorAddressBookMailboxPolicyNotFound(this.AddressBookPolicy.ToString())), new LocalizedString?(Strings.ErrorAddressBookMailboxPolicyNotUnique(this.AddressBookPolicy.ToString())), ExchangeErrorCategory.Client);
				this.addressbookMailboxPolicyId = (ADObjectId)addressBookMailboxPolicy.Identity;
			}
			if (this.ThrottlingPolicy != null)
			{
				if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
				{
					base.WriteError(new TaskArgumentException(Strings.ErrorLinkOpOnDehydratedTenant("ThrottlingPolicy")), ExchangeErrorCategory.Context, this.DataObject.Identity);
				}
				ThrottlingPolicy throttlingPolicy = (ThrottlingPolicy)base.GetDataObject<ThrottlingPolicy>(this.ThrottlingPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorThrottlingPolicyNotFound(this.ThrottlingPolicy.ToString())), new LocalizedString?(Strings.ErrorThrottlingPolicyNotUnique(this.ThrottlingPolicy.ToString())), ExchangeErrorCategory.Client);
				this.throttlingPolicyId = (ADObjectId)throttlingPolicy.Identity;
			}
			if (this.SharingPolicy != null)
			{
				if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
				{
					base.WriteError(new LocalizedException(Strings.ErrorLinkOpOnDehydratedTenant("SharingPolicy")), ExchangeErrorCategory.Client, null);
				}
				SharingPolicy sharingPolicy = (SharingPolicy)base.GetDataObject<SharingPolicy>(this.SharingPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorSharingPolicyNotFound(this.SharingPolicy.ToString())), new LocalizedString?(Strings.ErrorSharingPolicyNotUnique(this.SharingPolicy.ToString())), ExchangeErrorCategory.Client);
				this.sharingPolicyId = (ADObjectId)sharingPolicy.Identity;
			}
			if (this.RemoteAccountPolicy != null)
			{
				RemoteAccountPolicy remoteAccountPolicy = (RemoteAccountPolicy)base.GetDataObject<RemoteAccountPolicy>(this.RemoteAccountPolicy, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRemoteAccountPolicyNotFound(this.RemoteAccountPolicy.ToString())), new LocalizedString?(Strings.ErrorRemoteAccountPolicyNotUnique(this.RemoteAccountPolicy.ToString())), ExchangeErrorCategory.Client);
				this.remoteAccountPolicyId = (ADObjectId)remoteAccountPolicy.Identity;
			}
			IConfigurationSession tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId);
			if (this.PublicFolder)
			{
				MailboxTaskHelper.ValidatePublicFolderInformationWritable(tenantLocalConfigSession, this.HoldForMigration, new Task.ErrorLoggerDelegate(base.WriteError), this.Force);
			}
			if (this.RoleAssignmentPolicy == null)
			{
				if (!this.Arbitration && !this.Discovery && !this.PublicFolder && !this.AuditLog)
				{
					ADObjectId adobjectId = base.ProvisioningCache.TryAddAndGetOrganizationData<ADObjectId>(CannedProvisioningCacheKeys.DefaultRoleAssignmentPolicyId, base.CurrentOrganizationId, delegate()
					{
						RoleAssignmentPolicy roleAssignmentPolicy2 = RecipientTaskHelper.FindDefaultRoleAssignmentPolicy(tenantLocalConfigSession, new Task.ErrorLoggerDelegate(this.WriteError), Strings.ErrorDefaultRoleAssignmentPolicyNotUnique, Strings.ErrorDefaultRoleAssignmentPolicyNotFound);
						if (roleAssignmentPolicy2 != null)
						{
							return roleAssignmentPolicy2.Id;
						}
						return null;
					});
					if (adobjectId != null)
					{
						this.defaultRoleAssignmentPolicyId = adobjectId;
					}
				}
			}
			else
			{
				RoleAssignmentPolicy roleAssignmentPolicy = (RoleAssignmentPolicy)base.GetDataObject<RoleAssignmentPolicy>(this.RoleAssignmentPolicy, tenantLocalConfigSession, null, new LocalizedString?(Strings.ErrorRoleAssignmentPolicyNotFound(this.RoleAssignmentPolicy.ToString())), new LocalizedString?(Strings.ErrorRoleAssignmentPolicyNotUnique(this.RoleAssignmentPolicy.ToString())), ExchangeErrorCategory.Client);
				this.userSpecifiedRoleAssignmentPolicyId = (ADObjectId)roleAssignmentPolicy.Identity;
			}
			if (base.BypassLiveId && this.RemovedMailbox != null)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorBypassWLIDAndRemovedMailboxTogether), ExchangeErrorCategory.Client, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		protected override void InternalStateReset()
		{
			if (this.RemovedMailbox != null)
			{
				this.removedMailbox = MailboxTaskHelper.GetRemovedMailbox(base.DomainController, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, this.RemovedMailbox, new Task.ErrorLoggerDelegate(base.WriteError));
				if (this.removedMailbox.PreviousDatabase == null)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorRemovedMailboxDoesNotHaveDatabase(this.removedMailbox.Name)), ExchangeErrorCategory.Client, null);
				}
				DatabaseIdParameter obj = new DatabaseIdParameter(this.removedMailbox.PreviousDatabase);
				if (this.Database != null && !this.Database.Equals(obj))
				{
					base.WriteError(new UserInputInvalidException(Strings.ErrorRemovedMailboxCannotBeUsedWithDatabaseParameter(this.Database.ToString())), ExchangeErrorCategory.Client, null);
				}
				this.Database = obj;
				if (base.WindowsLiveID == null)
				{
					if (this.removedMailbox.WindowsLiveID.IsValidAddress)
					{
						base.WindowsLiveID = new WindowsLiveId(this.removedMailbox.WindowsLiveID.ToString());
					}
					else if (string.IsNullOrEmpty(this.UserPrincipalName))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorParameterRequired("UserPrincipalName")), ExchangeErrorCategory.Client, null);
					}
				}
				if (string.IsNullOrEmpty(base.SamAccountName))
				{
					base.SamAccountName = this.removedMailbox.SamAccountName;
				}
			}
			if (this.isDatabaseRequired)
			{
				if (this.Database != null)
				{
					bool throwOnError = this.RemovedMailbox == null;
					this.ValidateAndSetDatabase(this.Database, throwOnError, ExchangeErrorCategory.Client);
					this.isDatabaseRequired = (null == this.database);
				}
				else if (!base.IsProvisioningLayerAvailable)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorAutomaticProvisioningFailedToFindDatabase("Database")), ExchangeErrorCategory.ServerOperation, null);
				}
			}
			if (this.ArchiveDatabase != null)
			{
				this.ValidateAndSetArchiveDatabase(this.ArchiveDatabase, true, ExchangeErrorCategory.Client);
			}
			if (this.removedMailbox != null)
			{
				this.databaseOwnerServer = this.database.GetServer();
				this.mapiAdministrationSession = new MapiAdministrationSession(this.databaseOwnerServer.ExchangeLegacyDN, Fqdn.Parse(this.databaseOwnerServer.Fqdn));
			}
			bool flag = (this.IsCreatingResourceOrSharedMB || base.ParameterSetName == "PublicFolder") && base.WindowsLiveID == null;
			if (flag && string.IsNullOrEmpty(base.Alias))
			{
				base.Alias = RecipientTaskHelper.GenerateUniqueAlias(base.TenantGlobalCatalogSession, base.CurrentOrganizationId, string.IsNullOrEmpty(base.Name) ? base.SamAccountName : base.Name, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			if (string.IsNullOrEmpty(this.UserPrincipalName) && this.IsCreatingLogonDisabledTypeMB)
			{
				string text = flag ? base.Alias : base.Name;
				if (!WindowsLiveIDLocalPartConstraint.IsValidLocalPartOfWindowsLiveID(text))
				{
					CmdletLogger.SafeAppendGenericInfo(base.CurrentTaskContext.UniqueId, "GenerateDefaultUPN", text);
					text = "G" + Guid.NewGuid().ToString("N");
				}
				this.UserPrincipalName = RecipientTaskHelper.GenerateUniqueUserPrincipalName(base.TenantGlobalCatalogSession, text, this.ConfigurationSession.GetDefaultAcceptedDomain().DomainName.Domain, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			if (this.ShouldGenerateWindowsLiveID)
			{
				this.GenerateWindowsLiveID(base.Alias);
				if (this.Password == null)
				{
					this.Password = MailboxTaskUtilities.GetRandomPassword(base.Name, this.UserPrincipalName, 16);
					base.UserSpecifiedParameters["Password"] = this.Password;
				}
			}
			if (base.UserSpecifiedParameters["EnableRoomMailboxAccount"] != null && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.ValidateEnableRoomMailboxAccount.Enabled)
			{
				this.ValidateEnableRoomMailboxAccountParameter((bool)base.UserSpecifiedParameters["EnableRoomMailboxAccount"], (SecureString)base.UserSpecifiedParameters["RoomMailboxPassword"]);
			}
			base.InternalStateReset();
			if (base.SoftDeletedObject != null)
			{
				SmtpAddress windowsLiveID = this.DataObject.WindowsLiveID;
				NetID netID = this.DataObject.NetID;
				string name = this.DataObject.Name;
				string displayName = this.DataObject.DisplayName;
				ADObjectId mailboxPlan = this.DataObject.MailboxPlan;
				this.DataObject = SoftDeletedTaskHelper.GetSoftDeletedADUser(base.CurrentOrganizationId, (MailboxIdParameter)base.SoftDeletedObject, new Task.ErrorLoggerDelegate(base.WriteError));
				if (this.DataObject.WindowsLiveID != windowsLiveID)
				{
					this.DataObject.EmailAddressPolicyEnabled = false;
					this.DataObject.WindowsLiveID = windowsLiveID;
					this.DataObject.UserPrincipalName = windowsLiveID.ToString();
					this.DataObject.PrimarySmtpAddress = windowsLiveID;
				}
				if (this.DataObject.NetID != netID)
				{
					this.DataObject.NetID = netID;
				}
				if (!string.IsNullOrEmpty(name))
				{
					this.DataObject.Name = name;
				}
				this.DataObject.Name = SoftDeletedTaskHelper.GetUniqueNameForRecovery((IRecipientSession)base.DataSession, this.DataObject.Name, this.DataObject.Id);
				if (!string.IsNullOrEmpty(displayName))
				{
					this.DataObject.DisplayName = displayName;
				}
				if (!this.ValidateMailboxPlan(this.DataObject.MailboxPlan))
				{
					this.DataObject.MailboxPlan = mailboxPlan;
				}
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000FA20 File Offset: 0x0000DC20
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.PublicFolder)
			{
				if (!this.Force && !this.HoldForMigration && base.GlobalConfigSession.GetOrgContainer().DefaultPublicFolderMailbox.HierarchyMailboxGuid == Guid.Empty && MailboxTaskHelper.HasPublicFolderDatabases(new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<Server>), base.GlobalConfigSession))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorLegacyPublicFolderDatabaseExist), ExchangeErrorCategory.Client, null);
				}
				this.DisallowPublicFolderMailboxCreationDuringFinalization();
			}
			base.InternalValidate();
			if (this.database != null)
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets", LoggerHelper.CmdletPerfMonitors))
				{
					MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(base.SessionSettings, this.database, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (this.archiveDatabase != null)
			{
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets", LoggerHelper.CmdletPerfMonitors))
				{
					MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(base.SessionSettings, this.archiveDatabase, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (this.removedMailbox != null)
			{
				if (!this.removedMailbox.StoreMailboxExists)
				{
					if (this.removedMailbox.ExchangeGuid == Guid.Empty)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorRemovedMailboxDoesNotHaveMailboxGuid(this.removedMailbox.Name)), ExchangeErrorCategory.Client, null);
					}
					else
					{
						using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "MailboxTaskHelper.GetDeletedStoreMailbox", LoggerHelper.CmdletPerfMonitors))
						{
							StoreMailboxIdParameter identity = new StoreMailboxIdParameter(this.removedMailbox.ExchangeGuid);
							using (MailboxTaskHelper.GetDeletedStoreMailbox(this.mapiAdministrationSession, identity, MapiTaskHelper.ConvertDatabaseADObjectToDatabaseId(this.database), this.Database, new Task.ErrorLoggerDelegate(base.WriteError)))
							{
							}
						}
					}
				}
				if (((MailboxDatabase)this.database).Recovery)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMailboxResidesInRDB(this.removedMailbox.Name)), ExchangeErrorCategory.Client, null);
				}
				if (!this.databaseOwnerServer.IsE14OrLater)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMailboxDatabaseNotOnE14Server(this.Database.ToString())), ExchangeErrorCategory.Client, null);
				}
				if (this.archiveDatabase != null && !this.archiveDatabase.GetServer().IsE14Sp1OrLater)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMailboxDatabaseNotOnE14Sp1Server(this.ArchiveDatabase.ToString())), ExchangeErrorCategory.Client, null);
				}
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "MailboxTaskHelper.ValidateMailboxIsDisconnected", LoggerHelper.CmdletPerfMonitors))
				{
					MailboxTaskHelper.ValidateMailboxIsDisconnected(RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, this.removedMailbox.Id), this.removedMailbox.ExchangeGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				}
				using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "ConnectMailbox.CheckLegacyDNNotInUse", LoggerHelper.CmdletPerfMonitors))
				{
					ConnectMailbox.CheckLegacyDNNotInUse(MailboxId.Parse(this.removedMailbox.ExchangeGuid.ToString()), this.removedMailbox.LegacyExchangeDN, RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, this.removedMailbox.Id), new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			MailboxTaskHelper.ValidateRoomMailboxPasswordParameterCanOnlyBeUsedWithEnableRoomMailboxPassword(base.Fields.IsModified("RoomMailboxPassword"), base.Fields.IsModified("EnableRoomMailboxAccount"), new Task.ErrorLoggerDelegate(base.WriteError));
			if (base.ParameterSetName == "EnableRoomMailboxAccount")
			{
				this.ValidateEnableRoomMailboxAccountParameter(this.EnableRoomMailboxAccount, this.RoomMailboxPassword);
			}
			if (this.MailboxProvisioningPreferences != null)
			{
				MailboxTaskHelper.ValidateMailboxProvisioningConstraintEntries(this.MailboxProvisioningPreferences, base.DomainController, delegate(string message)
				{
					base.WriteVerbose(new LocalizedString(message));
				}, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			MailboxTaskHelper.EnsureUserSpecifiedDatabaseMatchesMailboxProvisioningConstraint(this.database, this.archiveDatabase, base.Fields, this.MailboxProvisioningConstraint, new Task.ErrorLoggerDelegate(base.WriteError), ADMailboxRecipientSchema.Database);
			TaskLogger.LogExit();
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0000FECC File Offset: 0x0000E0CC
		private void ValidateEnableRoomMailboxAccountParameter(bool enableRoomMailboxAccount, SecureString roomMailboxPassword)
		{
			if (enableRoomMailboxAccount && roomMailboxPassword == null)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorRoomPasswordMustBeSetWhenCreatingARoomADAccount), ExchangeErrorCategory.Client, null);
			}
			if (!enableRoomMailboxAccount && roomMailboxPassword != null)
			{
				base.WriteError(new TaskArgumentException(Strings.ErrorRoomMailboxPasswordCannotBeSetIfEnableRoomMailboxAccountIsFalse), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000FF08 File Offset: 0x0000E108
		private void DisallowPublicFolderMailboxCreationDuringFinalization()
		{
			if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Mrs.PublicFolderMailboxesMigration.Enabled && CommonUtils.IsPublicFolderMailboxesLockedForNewConnectionsFlagSet(base.CurrentOrganizationId))
			{
				base.WriteError(new RecipientTaskException(new LocalizedString(ServerStrings.PublicFolderMailboxesCannotBeCreatedDuringMigration)), ExchangeErrorCategory.Client, null);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000FF64 File Offset: 0x0000E164
		protected override void PrepareUserObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareUserObject(user);
			if (base.SoftDeletedObject == null)
			{
				if (this.databaseLocationInfo != null)
				{
					user.Database = this.database.Id;
					user.ServerLegacyDN = this.databaseLocationInfo.ServerLegacyDN;
					if (!RecipientTaskHelper.IsE14OrLater(this.databaseLocationInfo.ServerVersion))
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorMailboxDatabaseNotOnE14Server(this.database.ToString())), ExchangeErrorCategory.Client, null);
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
				if (base.Fields.IsModified(ADRecipientSchema.ThrottlingPolicy))
				{
					user.ThrottlingPolicy = this.throttlingPolicyId;
				}
				if (base.Fields.IsModified(ADUserSchema.SharingPolicy))
				{
					user.SharingPolicy = this.sharingPolicyId;
				}
				if (base.Fields.IsModified(ADRecipientSchema.IsExcludedFromServingHierarchy))
				{
					user.IsExcludedFromServingHierarchy = this.IsExcludedFromServingHierarchy;
				}
				if (base.Fields.IsChanged(ADRecipientSchema.MailboxProvisioningConstraint))
				{
					user.MailboxProvisioningConstraint = this.MailboxProvisioningConstraint;
				}
				if (base.Fields.IsChanged(ADRecipientSchema.MailboxProvisioningPreferences))
				{
					user.MailboxProvisioningPreferences = this.MailboxProvisioningPreferences;
				}
				if (base.Fields.IsModified(ADUserSchema.RemoteAccountPolicy))
				{
					user.RemoteAccountPolicy = this.remoteAccountPolicyId;
				}
				if (base.Fields.IsModified(ADRecipientSchema.RemotePowerShellEnabled))
				{
					user.RemotePowerShellEnabled = this.RemotePowerShellEnabled;
				}
				else
				{
					user.RemotePowerShellEnabled = true;
				}
				if (!user.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
				{
					if (this.userSpecifiedRoleAssignmentPolicyId != null)
					{
						user.RoleAssignmentPolicy = this.userSpecifiedRoleAssignmentPolicyId;
					}
					else if (user.RoleAssignmentPolicy == null && this.defaultRoleAssignmentPolicyId != null)
					{
						user.RoleAssignmentPolicy = this.defaultRoleAssignmentPolicyId;
					}
				}
				user.ShouldUseDefaultRetentionPolicy = true;
				user.ElcMailboxFlags |= ElcMailboxFlags.ElcV2;
				if (base.Fields.IsModified(ADUserSchema.RetentionPolicy))
				{
					user.RetentionPolicy = this.retentionPolicyId;
				}
				if (base.Fields.IsModified(ADUserSchema.ActiveSyncMailboxPolicy))
				{
					user.ActiveSyncMailboxPolicy = this.mobileMailboxPolicyId;
				}
				if (base.Fields.IsModified(ADRecipientSchema.AddressBookPolicy))
				{
					user.AddressBookPolicy = this.addressbookMailboxPolicyId;
				}
				user.ExchangeUserAccountControl = UserAccountControlFlags.None;
				if ("LinkedRoomMailbox" == base.ParameterSetName)
				{
					user.ResourceType = new ExchangeResourceType?(ExchangeResourceType.Room);
					user.MasterAccountSid = this.linkedUserSid;
				}
				else if ("AuxMailbox" == base.ParameterSetName)
				{
					AuxMailboxTaskHelper.AuxMailboxStampDefaultValues(user);
				}
				else if ("Linked" == base.ParameterSetName)
				{
					user.MasterAccountSid = this.linkedUserSid;
				}
				else if ("Shared" == base.ParameterSetName || "GroupMailbox" == base.ParameterSetName || "TeamMailboxIW" == base.ParameterSetName || "TeamMailboxITPro" == base.ParameterSetName)
				{
					user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
				}
				else if ("Room" == base.ParameterSetName || "EnableRoomMailboxAccount" == base.ParameterSetName)
				{
					user.ResourceType = new ExchangeResourceType?(ExchangeResourceType.Room);
					user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
				}
				else if ("Equipment" == base.ParameterSetName)
				{
					user.ResourceType = new ExchangeResourceType?(ExchangeResourceType.Equipment);
					user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.SelfSid, null);
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
					user.PersistedCapabilities.Add(Capability.OrganizationCapabilityPstProvider);
					ADObjectId childId;
					if (base.Organization == null)
					{
						childId = this.ConfigurationSession.GetOrgContainerId().GetChildId(ApprovalApplicationContainer.DefaultName);
					}
					else
					{
						childId = base.CurrentOrganizationId.ConfigurationUnit.GetChildId(ApprovalApplicationContainer.DefaultName);
					}
					if (this.ConfigurationSession.Read<ApprovalApplicationContainer>(childId) == null)
					{
						base.WriteError(new RecipientTaskException(Strings.ErrorRootContainerNotExist(childId.ToString())), ExchangeErrorCategory.Client, null);
					}
					if (!NewMailboxBase.IsNonApprovalArbitrationMailboxName(user.Name))
					{
						if (user.ManagedFolderMailboxPolicy == null && user.RetentionPolicy == null)
						{
							ADObjectId childId2;
							if (base.Organization == null)
							{
								childId2 = this.ConfigurationSession.GetOrgContainerId().GetChildId("Retention Policies Container").GetChildId("ArbitrationMailbox");
							}
							else
							{
								childId2 = base.CurrentOrganizationId.ConfigurationUnit.GetChildId("Retention Policies Container").GetChildId("ArbitrationMailbox");
							}
							RetentionPolicy retentionPolicy = this.ConfigurationSession.Read<RetentionPolicy>(childId2);
							if (retentionPolicy != null)
							{
								user.RetentionPolicy = retentionPolicy.Id;
							}
							else
							{
								this.WriteWarning(Strings.WarningArbitrationRetentionPolicyNotAvailable(childId2.ToString()));
							}
						}
						ApprovalApplication[] array = this.ConfigurationSession.Find<ApprovalApplication>(childId, QueryScope.SubTree, null, null, 0);
						List<ADObjectId> list = new List<ADObjectId>(array.Length);
						foreach (ApprovalApplication approvalApplication in array)
						{
							list.Add((ADObjectId)approvalApplication.Identity);
						}
						user[ADUserSchema.ApprovalApplications] = new MultiValuedProperty<ADObjectId>(list);
						if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SetDefaultProhibitSendReceiveQuota.Enabled)
						{
							user.ProhibitSendReceiveQuota = ByteQuantifiedSize.FromGB(10UL);
						}
					}
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
				else if ("MailboxPlan" == base.ParameterSetName)
				{
					user.MasterAccountSid = new SecurityIdentifier(WellKnownSidType.NullSid, null);
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
				else if (base.ParameterSetName == "Monitoring")
				{
					user.HiddenFromAddressListsEnabled = true;
				}
				MailboxTaskHelper.StampMailboxRecipientTypes(user, base.ParameterSetName);
			}
			if (base.WindowsLiveID != null && base.WindowsLiveID.SmtpAddress != SmtpAddress.Empty)
			{
				user.EmailAddressPolicyEnabled = false;
				SmtpProxyAddress item = new SmtpProxyAddress(base.WindowsLiveID.SmtpAddress.ToString(), false);
				if (!user.EmailAddresses.Contains(item))
				{
					user.EmailAddresses.Add(item);
				}
			}
			if (base.ParameterSetName == "Monitoring")
			{
				user.EmailAddresses.Add(ProxyAddress.Parse("SIP:" + user.UserPrincipalName));
			}
			if ((this.Arbitration.IsPresent || "MailboxPlan" == base.ParameterSetName || "Discovery" == base.ParameterSetName) && string.IsNullOrEmpty(user.SamAccountName))
			{
				user.SamAccountName = "SM_" + Guid.NewGuid().ToString("N").Substring(0, 17);
			}
			if ((this.Archive.IsPresent || this.archiveDatabase != null) && "RemoteArchive" == base.ParameterSetName)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorArchiveRemoteArchiveCannotBeSpecifiedTogether), ExchangeErrorCategory.Client, null);
			}
			if (this.Archive.IsPresent || "RemoteArchive" == base.ParameterSetName)
			{
				if ("RemoteArchive" == base.ParameterSetName && this.databaseLocationInfo.ServerVersion < Server.E15MinVersion)
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorMailboxDatabaseNotOnE15Server(this.database.ToString())), ExchangeErrorCategory.Client, null);
				}
				user.ArchiveGuid = Guid.NewGuid();
				user.ArchiveName = new MultiValuedProperty<string>(Strings.ArchiveNamePrefix + (string.IsNullOrEmpty(user.DisplayName) ? user.Name : user.DisplayName));
				if ("RemoteArchive" == base.ParameterSetName)
				{
					user.ArchiveDomain = this.ArchiveDomain;
					user.RemoteRecipientType |= RemoteRecipientType.ProvisionArchive;
				}
				else
				{
					if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SetActiveArchiveStatus.Enabled)
					{
						user.ArchiveStatus |= ArchiveStatusFlags.Active;
					}
					if (this.archiveDatabase == null)
					{
						user.ArchiveDatabase = user.Database;
					}
					else
					{
						MailboxTaskHelper.BlockLowerMajorVersionArchive(this.archiveDatabaseLocationInfo.ServerVersion, user.Database.DistinguishedName, this.archiveDatabase.DistinguishedName, this.archiveDatabase.ToString(), user.Database, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<MailboxDatabase>), base.GlobalConfigSession, this.ActiveManager, new Task.ErrorLoggerDelegate(base.WriteError));
						user.ArchiveDatabase = this.archiveDatabase.Id;
					}
				}
				MailboxTaskHelper.ApplyDefaultArchivePolicy(user, this.ConfigurationSession);
			}
			if (MailboxTaskHelper.SupportsMailboxReleaseVersioning(user))
			{
				user.MailboxRelease = this.databaseLocationInfo.MailboxRelease;
				if (this.Archive.IsPresent)
				{
					user.ArchiveRelease = this.archiveDatabaseLocationInfo.MailboxRelease;
				}
			}
			if (base.SoftDeletedObject != null)
			{
				SoftDeletedTaskHelper.UpdateShadowWhenSoftDeletedProperty((IRecipientSession)base.DataSession, this.ConfigurationSession, base.CurrentOrganizationId, this.DataObject);
				this.DataObject.RecipientSoftDeletedStatus = 0;
				this.DataObject.WhenSoftDeleted = null;
				this.DataObject.InternalOnly = false;
			}
			NewMailboxBase.CopyRemovedMailboxData(user, this.removedMailbox);
			TaskLogger.LogExit();
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00010A60 File Offset: 0x0000EC60
		protected override void StampChangesAfterSettingPassword()
		{
			if ("User" == base.ParameterSetName || "WindowsLiveID" == base.ParameterSetName || "WindowsLiveCustomDomains" == base.ParameterSetName || "ImportLiveId" == base.ParameterSetName || "FederatedUser" == base.ParameterSetName || "RemovedMailbox" == base.ParameterSetName || "RemoteArchive" == base.ParameterSetName || "MicrosoftOnlineServicesFederatedUser" == base.ParameterSetName || "MicrosoftOnlineServicesID" == base.ParameterSetName || ("EnableRoomMailboxAccount" == base.ParameterSetName && this.EnableRoomMailboxAccount))
			{
				this.DataObject.UserAccountControl = UserAccountControlFlags.NormalAccount;
				return;
			}
			if ("DisabledUser" == base.ParameterSetName || "PublicFolder" == base.ParameterSetName)
			{
				this.DataObject.UserAccountControl = (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.NormalAccount);
				this.DataObject.ExchangeUserAccountControl |= UserAccountControlFlags.AccountDisabled;
				return;
			}
			if ("Monitoring" == base.ParameterSetName)
			{
				this.DataObject.UserAccountControl = (UserAccountControlFlags.NormalAccount | UserAccountControlFlags.DoNotExpirePassword);
				return;
			}
			this.DataObject.UserAccountControl = (UserAccountControlFlags.AccountDisabled | UserAccountControlFlags.NormalAccount);
			this.DataObject.ExchangeUserAccountControl |= UserAccountControlFlags.AccountDisabled;
			if (!base.ResetPasswordOnNextLogon && (this.Password == null || this.Password.Length == 0) && "Arbitration" != base.ParameterSetName)
			{
				this.DataObject.UserAccountControl |= UserAccountControlFlags.DoNotExpirePassword;
			}
			MailboxTaskHelper.GrantPermissionToLinkedUserAccount(this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			if (base.IsVerboseOn)
			{
				base.WriteVerbose(Strings.VerboseSaveADSecurityDescriptor(this.DataObject.Id.ToString()));
			}
			this.DataObject.SaveSecurityDescriptor(((SecurityDescriptor)this.DataObject[ADObjectSchema.NTSecurityDescriptor]).ToRawSecurityDescriptor());
			if ("Discovery" == base.ParameterSetName || "MailboxPlan" == base.ParameterSetName)
			{
				this.DataObject.AcceptMessagesOnlyFrom.Add(this.DataObject.Id);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00010CC4 File Offset: 0x0000EEC4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "NewMailboxBase.InternalProcessRecord", LoggerHelper.CmdletPerfMonitors))
			{
				if (base.WindowsLiveID != null && MailboxTaskHelper.IsReservedLiveId(base.WindowsLiveID.SmtpAddress))
				{
					return;
				}
				if (this.runUMSteps && this.DataObject.UMEnabled)
				{
					Utils.DoUMEnablingSynchronousWork(this.DataObject);
				}
				if (base.ParameterSetName == "EnableRoomMailboxAccount" && this.EnableRoomMailboxAccount)
				{
					this.Password = this.RoomMailboxPassword;
				}
				if (this.DataObject.IsInLitigationHoldOrInplaceHold)
				{
					RecoverableItemsQuotaHelper.IncreaseRecoverableItemsQuotaIfNeeded(this.DataObject);
				}
				base.InternalProcessRecord();
				bool flag = false;
				if (this.PublicFolder || this.DataObject.RecipientTypeDetails == RecipientTypeDetails.TeamMailbox || this.DataObject.RecipientTypeDetails == RecipientTypeDetails.GroupMailbox || (this.DataObject.MailboxContainerGuid != null && this.DataObject.MailboxContainerGuid.Value != Guid.Empty))
				{
					IConfigurationSession tenantLocalConfigSession = RecipientTaskHelper.GetTenantLocalConfigSession(base.CurrentOrganizationId, base.ExecutingUserOrganizationId, base.RootOrgContainerId, false, this.DataObject.OriginatingServer, null);
					Organization orgContainer = tenantLocalConfigSession.GetOrgContainer();
					if (this.PublicFolder)
					{
						if (orgContainer.DefaultPublicFolderMailbox.HierarchyMailboxGuid == Guid.Empty)
						{
							orgContainer.DefaultPublicFolderMailbox = orgContainer.DefaultPublicFolderMailbox.Clone();
							orgContainer.DefaultPublicFolderMailbox.SetHierarchyMailbox(this.DataObject.ExchangeGuid, this.HoldForMigration ? PublicFolderInformation.HierarchyType.InTransitMailboxGuid : PublicFolderInformation.HierarchyType.MailboxGuid);
							tenantLocalConfigSession.Save(orgContainer);
						}
						else
						{
							this.DataObject.IsHierarchyReady = false;
							base.DataSession.Save(this.DataObject);
						}
					}
					if (this.databaseLocationInfo == null)
					{
						this.databaseLocationInfo = MailboxTaskHelper.GetDatabaseLocationInfo(this.database, this.ActiveManager, new Task.ErrorLoggerDelegate(base.WriteError));
					}
					MailboxTaskHelper.PrepopulateCacheForMailbox(this.database, this.databaseLocationInfo.ServerFqdn, this.DataObject.OrganizationId, this.DataObject.LegacyExchangeDN, this.DataObject.ExchangeGuid, tenantLocalConfigSession.LastUsedDc, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
					this.lastUsedDc = tenantLocalConfigSession.LastUsedDc;
					flag = true;
				}
				if (this.removedMailbox != null)
				{
					ConnectMailbox.UpdateSDAndRefreshMailbox(this.mapiAdministrationSession, this.DataObject, (MailboxDatabase)this.database, this.removedMailbox.ExchangeGuid, this.removedMailbox.LegacyExchangeDN, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
				}
				else if (base.SoftDeletedObject == null && this.databaseLocationInfo != null && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.PrePopulateCacheForMailboxBasedOnDatabase.Enabled)
				{
					bool flag2 = PhysicalResourceLoadBalancing.IsDatabaseInLocalSite(this.databaseLocationInfo, delegate(string message)
					{
						base.WriteVerbose(new LocalizedString(message));
					});
					string text = null;
					if (flag2)
					{
						text = this.DataObject.OriginatingServer;
					}
					else if (!this.isMailboxForcedReplicationDisabled)
					{
						string[] array = ((IRecipientSession)base.DataSession).ReplicateSingleObject(this.DataObject, new ADObjectId[]
						{
							this.databaseLocationInfo.ServerSite
						});
						if (array == null || array.Length == 0 || string.IsNullOrEmpty(array[0]))
						{
							this.WriteWarning(Strings.ErrorFailedToReplicateMailbox(this.DataObject.Identity.ToString(), this.databaseLocationInfo.ServerSite.ToString()));
						}
						else
						{
							text = array[0];
							base.WriteVerbose(Strings.VerboseSucceededReplicatingObject(this.DataObject.Identity.ToString(), text));
						}
					}
					if (text != null && !flag && this.DataObject.RecipientTypeDetails != RecipientTypeDetails.ArbitrationMailbox)
					{
						using (new CmdletMonitoredScope(base.CurrentTaskContext.UniqueId, "BizLogic", "MailboxTaskHelper.PrepopulateCacheForMailbox", LoggerHelper.CmdletPerfMonitors))
						{
							MailboxTaskHelper.PrepopulateCacheForMailbox(this.database, this.databaseLocationInfo.ServerFqdn, base.CurrentOrganizationId, this.DataObject.LegacyExchangeDN, this.DataObject.ExchangeGuid, text, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
						}
					}
				}
				this.DisposeMapiSession();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001119C File Offset: 0x0000F39C
		protected override void InternalStopProcessing()
		{
			base.InternalStopProcessing();
			this.DisposeMapiSession();
		}

		// Token: 0x06000391 RID: 913 RVA: 0x000111AC File Offset: 0x0000F3AC
		private static void CopyRemovedMailboxData(ADUser mailbox, RemovedMailbox removedMailbox)
		{
			if (removedMailbox != null && mailbox != null)
			{
				mailbox.ExchangeGuid = removedMailbox.ExchangeGuid;
				mailbox.LegacyExchangeDN = removedMailbox.LegacyExchangeDN;
				foreach (ProxyAddress proxyAddress in removedMailbox.EmailAddresses)
				{
					if (!proxyAddress.AddressString.Equals(removedMailbox.WindowsLiveID.ToString()) && !mailbox.EmailAddresses.Contains(proxyAddress))
					{
						mailbox.EmailAddresses.Add(proxyAddress);
					}
				}
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00011258 File Offset: 0x0000F458
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(Mailbox).FullName;
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001126C File Offset: 0x0000F46C
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.DisposeMapiSession();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001129C File Offset: 0x0000F49C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return Mailbox.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000112AC File Offset: 0x0000F4AC
		protected override void ValidateProvisionedProperties(IConfigurable dataObject)
		{
			if (this.isDatabaseRequired)
			{
				ADUser aduser = dataObject as ADUser;
				if (aduser != null && aduser.IsChanged(IADMailStorageSchema.Database))
				{
					if (aduser.DatabaseAndLocation == null)
					{
						this.ValidateAndSetDatabase(new DatabaseIdParameter(aduser.Database), false, ExchangeErrorCategory.ServerOperation);
						return;
					}
					MailboxDatabaseWithLocationInfo mailboxDatabaseWithLocationInfo = (MailboxDatabaseWithLocationInfo)aduser.DatabaseAndLocation;
					this.database = mailboxDatabaseWithLocationInfo.MailboxDatabase;
					this.databaseLocationInfo = mailboxDatabaseWithLocationInfo.DatabaseLocationInfo;
					aduser.DatabaseAndLocation = null;
					aduser.propertyBag.ResetChangeTracking(IADMailStorageSchema.DatabaseAndLocation);
					return;
				}
				else
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorParameterRequiredButNotProvisioned("Database")), ExchangeErrorCategory.ServerOperation, null);
				}
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00011354 File Offset: 0x0000F554
		private bool ValidateMailboxPlan(ADObjectId mailboxPlanId)
		{
			if (mailboxPlanId == null)
			{
				return false;
			}
			ADUser aduser = (ADUser)base.DataSession.Read<ADUser>(mailboxPlanId);
			return aduser != null && MailboxPlanRelease.NonCurrentRelease != (MailboxPlanRelease)aduser[ADRecipientSchema.MailboxPlanRelease];
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011392 File Offset: 0x0000F592
		private void ValidateAndSetDatabase(DatabaseIdParameter databaseId, bool throwOnError, ExchangeErrorCategory errorCategory)
		{
			this.InternalValidateAndSetArchiveDatabase(databaseId, Server.E15MinVersion, throwOnError, errorCategory, out this.database, out this.databaseLocationInfo);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000113AE File Offset: 0x0000F5AE
		private void ValidateAndSetArchiveDatabase(DatabaseIdParameter databaseId, bool throwOnError, ExchangeErrorCategory errorCategory)
		{
			this.InternalValidateAndSetArchiveDatabase(databaseId, Server.E15MinVersion, throwOnError, errorCategory, out this.archiveDatabase, out this.archiveDatabaseLocationInfo);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000113CC File Offset: 0x0000F5CC
		private void InternalValidateAndSetArchiveDatabase(DatabaseIdParameter databaseId, int minServerVersion, bool throwOnError, ExchangeErrorCategory errorCategory, out Database database, out DatabaseLocationInfo databaseLocationInfo)
		{
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugStartSetDatabase);
			}
			database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(databaseId, base.GlobalConfigSession, null, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(databaseId.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(databaseId.ToString())), errorCategory);
			databaseLocationInfo = MailboxTaskHelper.GetDatabaseLocationInfo(database, this.ActiveManager, new Task.ErrorLoggerDelegate(base.WriteError));
			LocalizedException ex = null;
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
					base.ThrowTerminatingError(ex, ExchangeErrorCategory.ServerOperation, null);
				}
				else
				{
					base.WriteError(ex, ExchangeErrorCategory.ServerOperation, null);
				}
			}
			if (base.IsDebugOn)
			{
				base.WriteDebug(Strings.DebugEndSetDatabase);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000114BC File Offset: 0x0000F6BC
		private void GenerateWindowsLiveID(string preferedLocalPart)
		{
			string domainPartOfUserPrincalName = RecipientTaskHelper.GetDomainPartOfUserPrincalName(this.UserPrincipalName);
			WindowsLiveId windowsLiveID;
			if (!WindowsLiveIDLocalPartConstraint.IsValidLocalPartOfWindowsLiveID(preferedLocalPart) || !WindowsLiveId.TryParse(string.Format("{0}@{1}", preferedLocalPart, domainPartOfUserPrincalName), out windowsLiveID))
			{
				windowsLiveID = new WindowsLiveId(string.Format("{0}@{1}", "G" + Guid.NewGuid().ToString("N"), domainPartOfUserPrincalName));
			}
			base.WindowsLiveID = windowsLiveID;
			base.UserSpecifiedParameters["WindowsLiveID"] = base.WindowsLiveID;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001153C File Offset: 0x0000F73C
		private void DisposeMapiSession()
		{
			if (this.mapiAdministrationSession != null)
			{
				this.mapiAdministrationSession.Dispose();
				this.mapiAdministrationSession = null;
			}
		}

		// Token: 0x040000A7 RID: 167
		private const string ForestWideDomainControllerAffinityByExecutingUserName = "ForestWideDomainControllerAffinityByExecutingUser";

		// Token: 0x040000A8 RID: 168
		public const string TenantNameForMonitoringKey = "TenantNameForMonitoring";

		// Token: 0x040000A9 RID: 169
		public const string Migration = "MigrationMailbox{24B27736-B069-46f1-B482-D6D9EAC9B053}";

		// Token: 0x040000AA RID: 170
		private const int RandomPasswordLengthForLiveID = 16;

		// Token: 0x040000AB RID: 171
		private static readonly object[] InvalidPublicFolderParameters = new object[]
		{
			"Archive",
			"AuxMailbox",
			ADUserSchema.ArchiveDatabase,
			ADUserSchema.PasswordLastSetRaw
		};

		// Token: 0x040000AC RID: 172
		private Database database;

		// Token: 0x040000AD RID: 173
		private Database archiveDatabase;

		// Token: 0x040000AE RID: 174
		private ADObjectId retentionPolicyId;

		// Token: 0x040000AF RID: 175
		private ADObjectId mobileMailboxPolicyId;

		// Token: 0x040000B0 RID: 176
		private SecurityIdentifier linkedUserSid;

		// Token: 0x040000B1 RID: 177
		private ADObjectId addressbookMailboxPolicyId;

		// Token: 0x040000B2 RID: 178
		private ADObjectId throttlingPolicyId;

		// Token: 0x040000B3 RID: 179
		private ADObjectId sharingPolicyId;

		// Token: 0x040000B4 RID: 180
		private ADObjectId remoteAccountPolicyId;

		// Token: 0x040000B5 RID: 181
		private ADObjectId userSpecifiedRoleAssignmentPolicyId;

		// Token: 0x040000B6 RID: 182
		private ADObjectId defaultRoleAssignmentPolicyId;

		// Token: 0x040000B7 RID: 183
		protected bool isDatabaseRequired = true;

		// Token: 0x040000B8 RID: 184
		protected DatabaseLocationInfo databaseLocationInfo;

		// Token: 0x040000B9 RID: 185
		private DatabaseLocationInfo archiveDatabaseLocationInfo;

		// Token: 0x040000BA RID: 186
		protected string lastUsedDc;

		// Token: 0x040000BB RID: 187
		private RemovedMailbox removedMailbox;

		// Token: 0x040000BC RID: 188
		private MapiAdministrationSession mapiAdministrationSession;

		// Token: 0x040000BD RID: 189
		private Server databaseOwnerServer;

		// Token: 0x040000BE RID: 190
		protected bool isMailboxForcedReplicationDisabled;
	}
}
