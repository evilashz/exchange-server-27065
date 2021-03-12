using System;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000C7 RID: 199
	[Cmdlet("Undo", "SyncSoftDeletedMailbox", SupportsShouldProcess = true, DefaultParameterSetName = "SoftDeletedMailbox")]
	public sealed class UndoSyncSoftDeletedMailbox : NewMailboxOrSyncMailbox
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x00035D23 File Offset: 0x00033F23
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x00035D30 File Offset: 0x00033F30
		[Parameter(Mandatory = true, Position = 0, ParameterSetName = "SoftDeletedMailbox", ValueFromPipeline = true)]
		public new MailboxIdParameter SoftDeletedObject
		{
			get
			{
				return (MailboxIdParameter)base.SoftDeletedObject;
			}
			set
			{
				base.SoftDeletedObject = value;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x00035D39 File Offset: 0x00033F39
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x00035D41 File Offset: 0x00033F41
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailbox")]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x00035D4A File Offset: 0x00033F4A
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x00035D52 File Offset: 0x00033F52
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailbox")]
		public new WindowsLiveId WindowsLiveID
		{
			get
			{
				return base.WindowsLiveID;
			}
			set
			{
				base.WindowsLiveID = value;
			}
		}

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00035D5B File Offset: 0x00033F5B
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00035D63 File Offset: 0x00033F63
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailbox")]
		public override SecureString Password
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

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x00035D6C File Offset: 0x00033F6C
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00035D74 File Offset: 0x00033F74
		[Parameter(Mandatory = false, ParameterSetName = "SoftDeletedMailbox")]
		public new SwitchParameter BypassLiveId
		{
			get
			{
				return base.BypassLiveId;
			}
			set
			{
				base.BypassLiveId = value;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00035D7D File Offset: 0x00033F7D
		protected override bool AllowBypassLiveIdWithoutWlid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00035D80 File Offset: 0x00033F80
		private new SwitchParameter AccountDisabled
		{
			get
			{
				return base.AccountDisabled;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00035D88 File Offset: 0x00033F88
		private new MailboxPolicyIdParameter ActiveSyncMailboxPolicy
		{
			get
			{
				return base.ActiveSyncMailboxPolicy;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00035D90 File Offset: 0x00033F90
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x00035D98 File Offset: 0x00033F98
		private new AddressBookMailboxPolicyIdParameter AddressBookPolicy
		{
			get
			{
				return base.AddressBookPolicy;
			}
			set
			{
				base.AddressBookPolicy = value;
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x00035DA1 File Offset: 0x00033FA1
		private new string Alias
		{
			get
			{
				return base.Alias;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00035DA9 File Offset: 0x00033FA9
		private new SwitchParameter Arbitration
		{
			get
			{
				return base.Arbitration;
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00035DB1 File Offset: 0x00033FB1
		private new MailboxIdParameter ArbitrationMailbox
		{
			get
			{
				return base.ArbitrationMailbox;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00035DB9 File Offset: 0x00033FB9
		private new SwitchParameter Archive
		{
			get
			{
				return base.Archive;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x00035DC1 File Offset: 0x00033FC1
		private new DatabaseIdParameter ArchiveDatabase
		{
			get
			{
				return base.ArchiveDatabase;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00035DC9 File Offset: 0x00033FC9
		private new SmtpDomain ArchiveDomain
		{
			get
			{
				return base.ArchiveDomain;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x00035DD1 File Offset: 0x00033FD1
		private new DatabaseIdParameter Database
		{
			get
			{
				return base.Database;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00035DD9 File Offset: 0x00033FD9
		private new SwitchParameter Discovery
		{
			get
			{
				return base.Discovery;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00035DE1 File Offset: 0x00033FE1
		private new SwitchParameter Equipment
		{
			get
			{
				return base.Equipment;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00035DE9 File Offset: 0x00033FE9
		private new SwitchParameter EvictLiveId
		{
			get
			{
				return base.EvictLiveId;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00035DF1 File Offset: 0x00033FF1
		private new string ExternalDirectoryObjectId
		{
			get
			{
				return base.ExternalDirectoryObjectId;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00035DF9 File Offset: 0x00033FF9
		private new string FederatedIdentity
		{
			get
			{
				return base.FederatedIdentity;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00035E01 File Offset: 0x00034001
		private new string FirstName
		{
			get
			{
				return base.FirstName;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00035E09 File Offset: 0x00034009
		private new SwitchParameter Force
		{
			get
			{
				return base.Force;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00035E11 File Offset: 0x00034011
		private new SwitchParameter ForestWideDomainControllerAffinityByExecutingUser
		{
			get
			{
				return base.ForestWideDomainControllerAffinityByExecutingUser;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00035E19 File Offset: 0x00034019
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x00035E21 File Offset: 0x00034021
		private new SwitchParameter HoldForMigration
		{
			get
			{
				return base.HoldForMigration;
			}
			set
			{
				base.HoldForMigration = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x00035E2A File Offset: 0x0003402A
		private new string ImmutableId
		{
			get
			{
				return base.ImmutableId;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00035E32 File Offset: 0x00034032
		private new SwitchParameter ImportLiveId
		{
			get
			{
				return base.ImportLiveId;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x00035E3A File Offset: 0x0003403A
		private new string Initials
		{
			get
			{
				return base.Initials;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000DCD RID: 3533 RVA: 0x00035E42 File Offset: 0x00034042
		private new string LastName
		{
			get
			{
				return base.LastName;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00035E4A File Offset: 0x0003404A
		private new PSCredential LinkedCredential
		{
			get
			{
				return base.LinkedCredential;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00035E52 File Offset: 0x00034052
		private new string LinkedDomainController
		{
			get
			{
				return base.LinkedDomainController;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00035E5A File Offset: 0x0003405A
		private new UserIdParameter LinkedMasterAccount
		{
			get
			{
				return base.LinkedMasterAccount;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x00035E62 File Offset: 0x00034062
		private new MailboxPlanIdParameter MailboxPlan
		{
			get
			{
				return base.MailboxPlan;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00035E6A File Offset: 0x0003406A
		private new Guid MailboxContainerGuid
		{
			get
			{
				return base.MailboxContainerGuid;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00035E72 File Offset: 0x00034072
		private new WindowsLiveId MicrosoftOnlineServicesID
		{
			get
			{
				return base.MicrosoftOnlineServicesID;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00035E7A File Offset: 0x0003407A
		private new MultiValuedProperty<ModeratorIDParameter> ModeratedBy
		{
			get
			{
				return base.ModeratedBy;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00035E82 File Offset: 0x00034082
		private new bool ModerationEnabled
		{
			get
			{
				return base.ModerationEnabled;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00035E8A File Offset: 0x0003408A
		private new NetID NetID
		{
			get
			{
				return base.NetID;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00035E92 File Offset: 0x00034092
		private new OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return base.OrganizationalUnit;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00035E9A File Offset: 0x0003409A
		private new SwitchParameter OverrideRecipientQuotas
		{
			get
			{
				return base.OverrideRecipientQuotas;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00035EA2 File Offset: 0x000340A2
		private new SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return base.PrimarySmtpAddress;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00035EAA File Offset: 0x000340AA
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x00035EB2 File Offset: 0x000340B2
		private new SwitchParameter PublicFolder
		{
			get
			{
				return base.PublicFolder;
			}
			set
			{
				base.PublicFolder = value;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x00035EBB File Offset: 0x000340BB
		private new bool QueryBaseDNRestrictionEnabled
		{
			get
			{
				return base.QueryBaseDNRestrictionEnabled;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00035EC3 File Offset: 0x000340C3
		private new RemoteAccountPolicyIdParameter RemoteAccountPolicy
		{
			get
			{
				return base.RemoteAccountPolicy;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x00035ECB File Offset: 0x000340CB
		private new SwitchParameter RemoteArchive
		{
			get
			{
				return base.RemoteArchive;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00035ED3 File Offset: 0x000340D3
		private new bool RemotePowerShellEnabled
		{
			get
			{
				return base.RemotePowerShellEnabled;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00035EDB File Offset: 0x000340DB
		private new RemovedMailboxIdParameter RemovedMailbox
		{
			get
			{
				return base.RemovedMailbox;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00035EE3 File Offset: 0x000340E3
		private new bool ResetPasswordOnNextLogon
		{
			get
			{
				return base.ResetPasswordOnNextLogon;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00035EEB File Offset: 0x000340EB
		private new MailboxPolicyIdParameter RetentionPolicy
		{
			get
			{
				return base.RetentionPolicy;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00035EF3 File Offset: 0x000340F3
		private new MailboxPolicyIdParameter RoleAssignmentPolicy
		{
			get
			{
				return base.RoleAssignmentPolicy;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x00035EFB File Offset: 0x000340FB
		private new SwitchParameter Room
		{
			get
			{
				return base.Room;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000DE5 RID: 3557 RVA: 0x00035F03 File Offset: 0x00034103
		private new string SamAccountName
		{
			get
			{
				return base.SamAccountName;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x00035F0B File Offset: 0x0003410B
		private new TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return base.SendModerationNotifications;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000DE7 RID: 3559 RVA: 0x00035F13 File Offset: 0x00034113
		private new SwitchParameter Shared
		{
			get
			{
				return base.Shared;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00035F1B File Offset: 0x0003411B
		private new SharingPolicyIdParameter SharingPolicy
		{
			get
			{
				return base.SharingPolicy;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x00035F23 File Offset: 0x00034123
		private new bool SKUAssigned
		{
			get
			{
				return base.SKUAssigned;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00035F2B File Offset: 0x0003412B
		private new MultiValuedProperty<Capability> AddOnSKUCapability
		{
			get
			{
				return base.AddOnSKUCapability;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x00035F33 File Offset: 0x00034133
		private new Capability SKUCapability
		{
			get
			{
				return base.SKUCapability;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00035F3B File Offset: 0x0003413B
		private new SwitchParameter TargetAllMDBs
		{
			get
			{
				return base.TargetAllMDBs;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x00035F43 File Offset: 0x00034143
		private new ThrottlingPolicyIdParameter ThrottlingPolicy
		{
			get
			{
				return base.ThrottlingPolicy;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00035F4B File Offset: 0x0003414B
		private new CountryInfo UsageLocation
		{
			get
			{
				return base.UsageLocation;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000DEF RID: 3567 RVA: 0x00035F53 File Offset: 0x00034153
		private new SwitchParameter UseExistingLiveId
		{
			get
			{
				return base.UseExistingLiveId;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00035F5B File Offset: 0x0003415B
		private new string UserPrincipalName
		{
			get
			{
				return base.UserPrincipalName;
			}
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x00035F64 File Offset: 0x00034164
		public UndoSyncSoftDeletedMailbox()
		{
			base.NumberofCalls = ProvisioningCounters.NumberOfUndoSyncSoftDeletedMailboxCalls;
			base.NumberofSuccessfulCalls = ProvisioningCounters.NumberOfSuccessfulUndoSyncSoftDeletedMailboxCalls;
			base.AverageTimeTaken = ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTime;
			base.AverageBaseTimeTaken = ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeBase;
			base.AverageTimeTakenWithCache = ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeWithCache;
			base.AverageBaseTimeTakenWithCache = ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeBaseWithCache;
			base.AverageTimeTakenWithoutCache = ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeWithoutCache;
			base.AverageBaseTimeTakenWithoutCache = ProvisioningCounters.AverageUndoSyncSoftDeletedMailboxResponseTimeBaseWithoutCache;
			base.TotalResponseTime = ProvisioningCounters.TotalUndoSyncSoftDeletedMailboxResponseTime;
			base.CacheActivePercentage = ProvisioningCounters.UndoSyncSoftDeletedMailboxCacheActivePercentage;
			base.CacheActiveBasePercentage = ProvisioningCounters.UndoSyncSoftDeletedMailboxCacheActivePercentageBase;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x00035FF0 File Offset: 0x000341F0
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			ADUser aduser = (ADUser)result;
			if (this.mailboxPlanObject != null)
			{
				aduser.MailboxPlanName = this.mailboxPlanObject.DisplayName;
			}
			aduser.ResetChangeTracking();
			SyncMailbox result2 = new SyncMailbox(aduser);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003604C File Offset: 0x0003424C
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncMailbox.FromDataObject((ADUser)dataObject);
		}
	}
}
