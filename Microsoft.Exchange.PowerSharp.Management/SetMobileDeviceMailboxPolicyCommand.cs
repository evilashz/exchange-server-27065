using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001BB RID: 443
	public class SetMobileDeviceMailboxPolicyCommand : SyntheticCommandWithPipelineInputNoOutput<MobileMailboxPolicy>
	{
		// Token: 0x06002567 RID: 9575 RVA: 0x00048242 File Offset: 0x00046442
		private SetMobileDeviceMailboxPolicyCommand() : base("Set-MobileDeviceMailboxPolicy")
		{
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x0004824F File Offset: 0x0004644F
		public SetMobileDeviceMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0004825E File Offset: 0x0004645E
		public virtual SetMobileDeviceMailboxPolicyCommand SetParameters(SetMobileDeviceMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x00048268 File Offset: 0x00046468
		public virtual SetMobileDeviceMailboxPolicyCommand SetParameters(SetMobileDeviceMailboxPolicyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001BC RID: 444
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000DAA RID: 3498
			// (set) Token: 0x0600256B RID: 9579 RVA: 0x00048272 File Offset: 0x00046472
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000DAB RID: 3499
			// (set) Token: 0x0600256C RID: 9580 RVA: 0x00048285 File Offset: 0x00046485
			public virtual bool AllowNonProvisionableDevices
			{
				set
				{
					base.PowerSharpParameters["AllowNonProvisionableDevices"] = value;
				}
			}

			// Token: 0x17000DAC RID: 3500
			// (set) Token: 0x0600256D RID: 9581 RVA: 0x0004829D File Offset: 0x0004649D
			public virtual bool AlphanumericPasswordRequired
			{
				set
				{
					base.PowerSharpParameters["AlphanumericPasswordRequired"] = value;
				}
			}

			// Token: 0x17000DAD RID: 3501
			// (set) Token: 0x0600256E RID: 9582 RVA: 0x000482B5 File Offset: 0x000464B5
			public virtual bool AttachmentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AttachmentsEnabled"] = value;
				}
			}

			// Token: 0x17000DAE RID: 3502
			// (set) Token: 0x0600256F RID: 9583 RVA: 0x000482CD File Offset: 0x000464CD
			public virtual bool DeviceEncryptionEnabled
			{
				set
				{
					base.PowerSharpParameters["DeviceEncryptionEnabled"] = value;
				}
			}

			// Token: 0x17000DAF RID: 3503
			// (set) Token: 0x06002570 RID: 9584 RVA: 0x000482E5 File Offset: 0x000464E5
			public virtual bool RequireStorageCardEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireStorageCardEncryption"] = value;
				}
			}

			// Token: 0x17000DB0 RID: 3504
			// (set) Token: 0x06002571 RID: 9585 RVA: 0x000482FD File Offset: 0x000464FD
			public virtual bool PasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordEnabled"] = value;
				}
			}

			// Token: 0x17000DB1 RID: 3505
			// (set) Token: 0x06002572 RID: 9586 RVA: 0x00048315 File Offset: 0x00046515
			public virtual bool PasswordRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17000DB2 RID: 3506
			// (set) Token: 0x06002573 RID: 9587 RVA: 0x0004832D File Offset: 0x0004652D
			public virtual Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
			{
				set
				{
					base.PowerSharpParameters["DevicePolicyRefreshInterval"] = value;
				}
			}

			// Token: 0x17000DB3 RID: 3507
			// (set) Token: 0x06002574 RID: 9588 RVA: 0x00048345 File Offset: 0x00046545
			public virtual bool AllowSimplePassword
			{
				set
				{
					base.PowerSharpParameters["AllowSimplePassword"] = value;
				}
			}

			// Token: 0x17000DB4 RID: 3508
			// (set) Token: 0x06002575 RID: 9589 RVA: 0x0004835D File Offset: 0x0004655D
			public virtual Unlimited<ByteQuantifiedSize> MaxAttachmentSize
			{
				set
				{
					base.PowerSharpParameters["MaxAttachmentSize"] = value;
				}
			}

			// Token: 0x17000DB5 RID: 3509
			// (set) Token: 0x06002576 RID: 9590 RVA: 0x00048375 File Offset: 0x00046575
			public virtual bool WSSAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessEnabled"] = value;
				}
			}

			// Token: 0x17000DB6 RID: 3510
			// (set) Token: 0x06002577 RID: 9591 RVA: 0x0004838D File Offset: 0x0004658D
			public virtual bool UNCAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessEnabled"] = value;
				}
			}

			// Token: 0x17000DB7 RID: 3511
			// (set) Token: 0x06002578 RID: 9592 RVA: 0x000483A5 File Offset: 0x000465A5
			public virtual int? MinPasswordLength
			{
				set
				{
					base.PowerSharpParameters["MinPasswordLength"] = value;
				}
			}

			// Token: 0x17000DB8 RID: 3512
			// (set) Token: 0x06002579 RID: 9593 RVA: 0x000483BD File Offset: 0x000465BD
			public virtual Unlimited<EnhancedTimeSpan> MaxInactivityTimeLock
			{
				set
				{
					base.PowerSharpParameters["MaxInactivityTimeLock"] = value;
				}
			}

			// Token: 0x17000DB9 RID: 3513
			// (set) Token: 0x0600257A RID: 9594 RVA: 0x000483D5 File Offset: 0x000465D5
			public virtual Unlimited<int> MaxPasswordFailedAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxPasswordFailedAttempts"] = value;
				}
			}

			// Token: 0x17000DBA RID: 3514
			// (set) Token: 0x0600257B RID: 9595 RVA: 0x000483ED File Offset: 0x000465ED
			public virtual Unlimited<EnhancedTimeSpan> PasswordExpiration
			{
				set
				{
					base.PowerSharpParameters["PasswordExpiration"] = value;
				}
			}

			// Token: 0x17000DBB RID: 3515
			// (set) Token: 0x0600257C RID: 9596 RVA: 0x00048405 File Offset: 0x00046605
			public virtual int PasswordHistory
			{
				set
				{
					base.PowerSharpParameters["PasswordHistory"] = value;
				}
			}

			// Token: 0x17000DBC RID: 3516
			// (set) Token: 0x0600257D RID: 9597 RVA: 0x0004841D File Offset: 0x0004661D
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000DBD RID: 3517
			// (set) Token: 0x0600257E RID: 9598 RVA: 0x00048435 File Offset: 0x00046635
			public virtual bool AllowApplePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowApplePushNotifications"] = value;
				}
			}

			// Token: 0x17000DBE RID: 3518
			// (set) Token: 0x0600257F RID: 9599 RVA: 0x0004844D File Offset: 0x0004664D
			public virtual bool AllowMicrosoftPushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowMicrosoftPushNotifications"] = value;
				}
			}

			// Token: 0x17000DBF RID: 3519
			// (set) Token: 0x06002580 RID: 9600 RVA: 0x00048465 File Offset: 0x00046665
			public virtual bool AllowGooglePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowGooglePushNotifications"] = value;
				}
			}

			// Token: 0x17000DC0 RID: 3520
			// (set) Token: 0x06002581 RID: 9601 RVA: 0x0004847D File Offset: 0x0004667D
			public virtual bool AllowStorageCard
			{
				set
				{
					base.PowerSharpParameters["AllowStorageCard"] = value;
				}
			}

			// Token: 0x17000DC1 RID: 3521
			// (set) Token: 0x06002582 RID: 9602 RVA: 0x00048495 File Offset: 0x00046695
			public virtual bool AllowCamera
			{
				set
				{
					base.PowerSharpParameters["AllowCamera"] = value;
				}
			}

			// Token: 0x17000DC2 RID: 3522
			// (set) Token: 0x06002583 RID: 9603 RVA: 0x000484AD File Offset: 0x000466AD
			public virtual bool RequireDeviceEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireDeviceEncryption"] = value;
				}
			}

			// Token: 0x17000DC3 RID: 3523
			// (set) Token: 0x06002584 RID: 9604 RVA: 0x000484C5 File Offset: 0x000466C5
			public virtual bool AllowUnsignedApplications
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedApplications"] = value;
				}
			}

			// Token: 0x17000DC4 RID: 3524
			// (set) Token: 0x06002585 RID: 9605 RVA: 0x000484DD File Offset: 0x000466DD
			public virtual bool AllowUnsignedInstallationPackages
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedInstallationPackages"] = value;
				}
			}

			// Token: 0x17000DC5 RID: 3525
			// (set) Token: 0x06002586 RID: 9606 RVA: 0x000484F5 File Offset: 0x000466F5
			public virtual bool AllowWiFi
			{
				set
				{
					base.PowerSharpParameters["AllowWiFi"] = value;
				}
			}

			// Token: 0x17000DC6 RID: 3526
			// (set) Token: 0x06002587 RID: 9607 RVA: 0x0004850D File Offset: 0x0004670D
			public virtual bool AllowTextMessaging
			{
				set
				{
					base.PowerSharpParameters["AllowTextMessaging"] = value;
				}
			}

			// Token: 0x17000DC7 RID: 3527
			// (set) Token: 0x06002588 RID: 9608 RVA: 0x00048525 File Offset: 0x00046725
			public virtual bool AllowPOPIMAPEmail
			{
				set
				{
					base.PowerSharpParameters["AllowPOPIMAPEmail"] = value;
				}
			}

			// Token: 0x17000DC8 RID: 3528
			// (set) Token: 0x06002589 RID: 9609 RVA: 0x0004853D File Offset: 0x0004673D
			public virtual bool AllowIrDA
			{
				set
				{
					base.PowerSharpParameters["AllowIrDA"] = value;
				}
			}

			// Token: 0x17000DC9 RID: 3529
			// (set) Token: 0x0600258A RID: 9610 RVA: 0x00048555 File Offset: 0x00046755
			public virtual bool RequireManualSyncWhenRoaming
			{
				set
				{
					base.PowerSharpParameters["RequireManualSyncWhenRoaming"] = value;
				}
			}

			// Token: 0x17000DCA RID: 3530
			// (set) Token: 0x0600258B RID: 9611 RVA: 0x0004856D File Offset: 0x0004676D
			public virtual bool AllowDesktopSync
			{
				set
				{
					base.PowerSharpParameters["AllowDesktopSync"] = value;
				}
			}

			// Token: 0x17000DCB RID: 3531
			// (set) Token: 0x0600258C RID: 9612 RVA: 0x00048585 File Offset: 0x00046785
			public virtual bool AllowHTMLEmail
			{
				set
				{
					base.PowerSharpParameters["AllowHTMLEmail"] = value;
				}
			}

			// Token: 0x17000DCC RID: 3532
			// (set) Token: 0x0600258D RID: 9613 RVA: 0x0004859D File Offset: 0x0004679D
			public virtual bool RequireSignedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000DCD RID: 3533
			// (set) Token: 0x0600258E RID: 9614 RVA: 0x000485B5 File Offset: 0x000467B5
			public virtual bool RequireEncryptedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000DCE RID: 3534
			// (set) Token: 0x0600258F RID: 9615 RVA: 0x000485CD File Offset: 0x000467CD
			public virtual bool AllowSMIMESoftCerts
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMESoftCerts"] = value;
				}
			}

			// Token: 0x17000DCF RID: 3535
			// (set) Token: 0x06002590 RID: 9616 RVA: 0x000485E5 File Offset: 0x000467E5
			public virtual bool AllowBrowser
			{
				set
				{
					base.PowerSharpParameters["AllowBrowser"] = value;
				}
			}

			// Token: 0x17000DD0 RID: 3536
			// (set) Token: 0x06002591 RID: 9617 RVA: 0x000485FD File Offset: 0x000467FD
			public virtual bool AllowConsumerEmail
			{
				set
				{
					base.PowerSharpParameters["AllowConsumerEmail"] = value;
				}
			}

			// Token: 0x17000DD1 RID: 3537
			// (set) Token: 0x06002592 RID: 9618 RVA: 0x00048615 File Offset: 0x00046815
			public virtual bool AllowRemoteDesktop
			{
				set
				{
					base.PowerSharpParameters["AllowRemoteDesktop"] = value;
				}
			}

			// Token: 0x17000DD2 RID: 3538
			// (set) Token: 0x06002593 RID: 9619 RVA: 0x0004862D File Offset: 0x0004682D
			public virtual bool AllowInternetSharing
			{
				set
				{
					base.PowerSharpParameters["AllowInternetSharing"] = value;
				}
			}

			// Token: 0x17000DD3 RID: 3539
			// (set) Token: 0x06002594 RID: 9620 RVA: 0x00048645 File Offset: 0x00046845
			public virtual BluetoothType AllowBluetooth
			{
				set
				{
					base.PowerSharpParameters["AllowBluetooth"] = value;
				}
			}

			// Token: 0x17000DD4 RID: 3540
			// (set) Token: 0x06002595 RID: 9621 RVA: 0x0004865D File Offset: 0x0004685D
			public virtual CalendarAgeFilterType MaxCalendarAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxCalendarAgeFilter"] = value;
				}
			}

			// Token: 0x17000DD5 RID: 3541
			// (set) Token: 0x06002596 RID: 9622 RVA: 0x00048675 File Offset: 0x00046875
			public virtual EmailAgeFilterType MaxEmailAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxEmailAgeFilter"] = value;
				}
			}

			// Token: 0x17000DD6 RID: 3542
			// (set) Token: 0x06002597 RID: 9623 RVA: 0x0004868D File Offset: 0x0004688D
			public virtual SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000DD7 RID: 3543
			// (set) Token: 0x06002598 RID: 9624 RVA: 0x000486A5 File Offset: 0x000468A5
			public virtual EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptionSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000DD8 RID: 3544
			// (set) Token: 0x06002599 RID: 9625 RVA: 0x000486BD File Offset: 0x000468BD
			public virtual SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMEEncryptionAlgorithmNegotiation"] = value;
				}
			}

			// Token: 0x17000DD9 RID: 3545
			// (set) Token: 0x0600259A RID: 9626 RVA: 0x000486D5 File Offset: 0x000468D5
			public virtual int MinPasswordComplexCharacters
			{
				set
				{
					base.PowerSharpParameters["MinPasswordComplexCharacters"] = value;
				}
			}

			// Token: 0x17000DDA RID: 3546
			// (set) Token: 0x0600259B RID: 9627 RVA: 0x000486ED File Offset: 0x000468ED
			public virtual Unlimited<int> MaxEmailBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000DDB RID: 3547
			// (set) Token: 0x0600259C RID: 9628 RVA: 0x00048705 File Offset: 0x00046905
			public virtual Unlimited<int> MaxEmailHTMLBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailHTMLBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000DDC RID: 3548
			// (set) Token: 0x0600259D RID: 9629 RVA: 0x0004871D File Offset: 0x0004691D
			public virtual MultiValuedProperty<string> UnapprovedInROMApplicationList
			{
				set
				{
					base.PowerSharpParameters["UnapprovedInROMApplicationList"] = value;
				}
			}

			// Token: 0x17000DDD RID: 3549
			// (set) Token: 0x0600259E RID: 9630 RVA: 0x00048730 File Offset: 0x00046930
			public virtual ApprovedApplicationCollection ApprovedApplicationList
			{
				set
				{
					base.PowerSharpParameters["ApprovedApplicationList"] = value;
				}
			}

			// Token: 0x17000DDE RID: 3550
			// (set) Token: 0x0600259F RID: 9631 RVA: 0x00048743 File Offset: 0x00046943
			public virtual bool AllowExternalDeviceManagement
			{
				set
				{
					base.PowerSharpParameters["AllowExternalDeviceManagement"] = value;
				}
			}

			// Token: 0x17000DDF RID: 3551
			// (set) Token: 0x060025A0 RID: 9632 RVA: 0x0004875B File Offset: 0x0004695B
			public virtual MobileOTAUpdateModeType MobileOTAUpdateMode
			{
				set
				{
					base.PowerSharpParameters["MobileOTAUpdateMode"] = value;
				}
			}

			// Token: 0x17000DE0 RID: 3552
			// (set) Token: 0x060025A1 RID: 9633 RVA: 0x00048773 File Offset: 0x00046973
			public virtual bool AllowMobileOTAUpdate
			{
				set
				{
					base.PowerSharpParameters["AllowMobileOTAUpdate"] = value;
				}
			}

			// Token: 0x17000DE1 RID: 3553
			// (set) Token: 0x060025A2 RID: 9634 RVA: 0x0004878B File Offset: 0x0004698B
			public virtual bool IrmEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmEnabled"] = value;
				}
			}

			// Token: 0x17000DE2 RID: 3554
			// (set) Token: 0x060025A3 RID: 9635 RVA: 0x000487A3 File Offset: 0x000469A3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000DE3 RID: 3555
			// (set) Token: 0x060025A4 RID: 9636 RVA: 0x000487B6 File Offset: 0x000469B6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000DE4 RID: 3556
			// (set) Token: 0x060025A5 RID: 9637 RVA: 0x000487CE File Offset: 0x000469CE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000DE5 RID: 3557
			// (set) Token: 0x060025A6 RID: 9638 RVA: 0x000487E6 File Offset: 0x000469E6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000DE6 RID: 3558
			// (set) Token: 0x060025A7 RID: 9639 RVA: 0x000487FE File Offset: 0x000469FE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000DE7 RID: 3559
			// (set) Token: 0x060025A8 RID: 9640 RVA: 0x00048816 File Offset: 0x00046A16
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020001BD RID: 445
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17000DE8 RID: 3560
			// (set) Token: 0x060025AA RID: 9642 RVA: 0x00048836 File Offset: 0x00046A36
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxPolicyIdParameter(value) : null);
				}
			}

			// Token: 0x17000DE9 RID: 3561
			// (set) Token: 0x060025AB RID: 9643 RVA: 0x00048854 File Offset: 0x00046A54
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000DEA RID: 3562
			// (set) Token: 0x060025AC RID: 9644 RVA: 0x00048867 File Offset: 0x00046A67
			public virtual bool AllowNonProvisionableDevices
			{
				set
				{
					base.PowerSharpParameters["AllowNonProvisionableDevices"] = value;
				}
			}

			// Token: 0x17000DEB RID: 3563
			// (set) Token: 0x060025AD RID: 9645 RVA: 0x0004887F File Offset: 0x00046A7F
			public virtual bool AlphanumericPasswordRequired
			{
				set
				{
					base.PowerSharpParameters["AlphanumericPasswordRequired"] = value;
				}
			}

			// Token: 0x17000DEC RID: 3564
			// (set) Token: 0x060025AE RID: 9646 RVA: 0x00048897 File Offset: 0x00046A97
			public virtual bool AttachmentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AttachmentsEnabled"] = value;
				}
			}

			// Token: 0x17000DED RID: 3565
			// (set) Token: 0x060025AF RID: 9647 RVA: 0x000488AF File Offset: 0x00046AAF
			public virtual bool DeviceEncryptionEnabled
			{
				set
				{
					base.PowerSharpParameters["DeviceEncryptionEnabled"] = value;
				}
			}

			// Token: 0x17000DEE RID: 3566
			// (set) Token: 0x060025B0 RID: 9648 RVA: 0x000488C7 File Offset: 0x00046AC7
			public virtual bool RequireStorageCardEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireStorageCardEncryption"] = value;
				}
			}

			// Token: 0x17000DEF RID: 3567
			// (set) Token: 0x060025B1 RID: 9649 RVA: 0x000488DF File Offset: 0x00046ADF
			public virtual bool PasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordEnabled"] = value;
				}
			}

			// Token: 0x17000DF0 RID: 3568
			// (set) Token: 0x060025B2 RID: 9650 RVA: 0x000488F7 File Offset: 0x00046AF7
			public virtual bool PasswordRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17000DF1 RID: 3569
			// (set) Token: 0x060025B3 RID: 9651 RVA: 0x0004890F File Offset: 0x00046B0F
			public virtual Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
			{
				set
				{
					base.PowerSharpParameters["DevicePolicyRefreshInterval"] = value;
				}
			}

			// Token: 0x17000DF2 RID: 3570
			// (set) Token: 0x060025B4 RID: 9652 RVA: 0x00048927 File Offset: 0x00046B27
			public virtual bool AllowSimplePassword
			{
				set
				{
					base.PowerSharpParameters["AllowSimplePassword"] = value;
				}
			}

			// Token: 0x17000DF3 RID: 3571
			// (set) Token: 0x060025B5 RID: 9653 RVA: 0x0004893F File Offset: 0x00046B3F
			public virtual Unlimited<ByteQuantifiedSize> MaxAttachmentSize
			{
				set
				{
					base.PowerSharpParameters["MaxAttachmentSize"] = value;
				}
			}

			// Token: 0x17000DF4 RID: 3572
			// (set) Token: 0x060025B6 RID: 9654 RVA: 0x00048957 File Offset: 0x00046B57
			public virtual bool WSSAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessEnabled"] = value;
				}
			}

			// Token: 0x17000DF5 RID: 3573
			// (set) Token: 0x060025B7 RID: 9655 RVA: 0x0004896F File Offset: 0x00046B6F
			public virtual bool UNCAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessEnabled"] = value;
				}
			}

			// Token: 0x17000DF6 RID: 3574
			// (set) Token: 0x060025B8 RID: 9656 RVA: 0x00048987 File Offset: 0x00046B87
			public virtual int? MinPasswordLength
			{
				set
				{
					base.PowerSharpParameters["MinPasswordLength"] = value;
				}
			}

			// Token: 0x17000DF7 RID: 3575
			// (set) Token: 0x060025B9 RID: 9657 RVA: 0x0004899F File Offset: 0x00046B9F
			public virtual Unlimited<EnhancedTimeSpan> MaxInactivityTimeLock
			{
				set
				{
					base.PowerSharpParameters["MaxInactivityTimeLock"] = value;
				}
			}

			// Token: 0x17000DF8 RID: 3576
			// (set) Token: 0x060025BA RID: 9658 RVA: 0x000489B7 File Offset: 0x00046BB7
			public virtual Unlimited<int> MaxPasswordFailedAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxPasswordFailedAttempts"] = value;
				}
			}

			// Token: 0x17000DF9 RID: 3577
			// (set) Token: 0x060025BB RID: 9659 RVA: 0x000489CF File Offset: 0x00046BCF
			public virtual Unlimited<EnhancedTimeSpan> PasswordExpiration
			{
				set
				{
					base.PowerSharpParameters["PasswordExpiration"] = value;
				}
			}

			// Token: 0x17000DFA RID: 3578
			// (set) Token: 0x060025BC RID: 9660 RVA: 0x000489E7 File Offset: 0x00046BE7
			public virtual int PasswordHistory
			{
				set
				{
					base.PowerSharpParameters["PasswordHistory"] = value;
				}
			}

			// Token: 0x17000DFB RID: 3579
			// (set) Token: 0x060025BD RID: 9661 RVA: 0x000489FF File Offset: 0x00046BFF
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000DFC RID: 3580
			// (set) Token: 0x060025BE RID: 9662 RVA: 0x00048A17 File Offset: 0x00046C17
			public virtual bool AllowApplePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowApplePushNotifications"] = value;
				}
			}

			// Token: 0x17000DFD RID: 3581
			// (set) Token: 0x060025BF RID: 9663 RVA: 0x00048A2F File Offset: 0x00046C2F
			public virtual bool AllowMicrosoftPushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowMicrosoftPushNotifications"] = value;
				}
			}

			// Token: 0x17000DFE RID: 3582
			// (set) Token: 0x060025C0 RID: 9664 RVA: 0x00048A47 File Offset: 0x00046C47
			public virtual bool AllowGooglePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowGooglePushNotifications"] = value;
				}
			}

			// Token: 0x17000DFF RID: 3583
			// (set) Token: 0x060025C1 RID: 9665 RVA: 0x00048A5F File Offset: 0x00046C5F
			public virtual bool AllowStorageCard
			{
				set
				{
					base.PowerSharpParameters["AllowStorageCard"] = value;
				}
			}

			// Token: 0x17000E00 RID: 3584
			// (set) Token: 0x060025C2 RID: 9666 RVA: 0x00048A77 File Offset: 0x00046C77
			public virtual bool AllowCamera
			{
				set
				{
					base.PowerSharpParameters["AllowCamera"] = value;
				}
			}

			// Token: 0x17000E01 RID: 3585
			// (set) Token: 0x060025C3 RID: 9667 RVA: 0x00048A8F File Offset: 0x00046C8F
			public virtual bool RequireDeviceEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireDeviceEncryption"] = value;
				}
			}

			// Token: 0x17000E02 RID: 3586
			// (set) Token: 0x060025C4 RID: 9668 RVA: 0x00048AA7 File Offset: 0x00046CA7
			public virtual bool AllowUnsignedApplications
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedApplications"] = value;
				}
			}

			// Token: 0x17000E03 RID: 3587
			// (set) Token: 0x060025C5 RID: 9669 RVA: 0x00048ABF File Offset: 0x00046CBF
			public virtual bool AllowUnsignedInstallationPackages
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedInstallationPackages"] = value;
				}
			}

			// Token: 0x17000E04 RID: 3588
			// (set) Token: 0x060025C6 RID: 9670 RVA: 0x00048AD7 File Offset: 0x00046CD7
			public virtual bool AllowWiFi
			{
				set
				{
					base.PowerSharpParameters["AllowWiFi"] = value;
				}
			}

			// Token: 0x17000E05 RID: 3589
			// (set) Token: 0x060025C7 RID: 9671 RVA: 0x00048AEF File Offset: 0x00046CEF
			public virtual bool AllowTextMessaging
			{
				set
				{
					base.PowerSharpParameters["AllowTextMessaging"] = value;
				}
			}

			// Token: 0x17000E06 RID: 3590
			// (set) Token: 0x060025C8 RID: 9672 RVA: 0x00048B07 File Offset: 0x00046D07
			public virtual bool AllowPOPIMAPEmail
			{
				set
				{
					base.PowerSharpParameters["AllowPOPIMAPEmail"] = value;
				}
			}

			// Token: 0x17000E07 RID: 3591
			// (set) Token: 0x060025C9 RID: 9673 RVA: 0x00048B1F File Offset: 0x00046D1F
			public virtual bool AllowIrDA
			{
				set
				{
					base.PowerSharpParameters["AllowIrDA"] = value;
				}
			}

			// Token: 0x17000E08 RID: 3592
			// (set) Token: 0x060025CA RID: 9674 RVA: 0x00048B37 File Offset: 0x00046D37
			public virtual bool RequireManualSyncWhenRoaming
			{
				set
				{
					base.PowerSharpParameters["RequireManualSyncWhenRoaming"] = value;
				}
			}

			// Token: 0x17000E09 RID: 3593
			// (set) Token: 0x060025CB RID: 9675 RVA: 0x00048B4F File Offset: 0x00046D4F
			public virtual bool AllowDesktopSync
			{
				set
				{
					base.PowerSharpParameters["AllowDesktopSync"] = value;
				}
			}

			// Token: 0x17000E0A RID: 3594
			// (set) Token: 0x060025CC RID: 9676 RVA: 0x00048B67 File Offset: 0x00046D67
			public virtual bool AllowHTMLEmail
			{
				set
				{
					base.PowerSharpParameters["AllowHTMLEmail"] = value;
				}
			}

			// Token: 0x17000E0B RID: 3595
			// (set) Token: 0x060025CD RID: 9677 RVA: 0x00048B7F File Offset: 0x00046D7F
			public virtual bool RequireSignedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000E0C RID: 3596
			// (set) Token: 0x060025CE RID: 9678 RVA: 0x00048B97 File Offset: 0x00046D97
			public virtual bool RequireEncryptedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000E0D RID: 3597
			// (set) Token: 0x060025CF RID: 9679 RVA: 0x00048BAF File Offset: 0x00046DAF
			public virtual bool AllowSMIMESoftCerts
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMESoftCerts"] = value;
				}
			}

			// Token: 0x17000E0E RID: 3598
			// (set) Token: 0x060025D0 RID: 9680 RVA: 0x00048BC7 File Offset: 0x00046DC7
			public virtual bool AllowBrowser
			{
				set
				{
					base.PowerSharpParameters["AllowBrowser"] = value;
				}
			}

			// Token: 0x17000E0F RID: 3599
			// (set) Token: 0x060025D1 RID: 9681 RVA: 0x00048BDF File Offset: 0x00046DDF
			public virtual bool AllowConsumerEmail
			{
				set
				{
					base.PowerSharpParameters["AllowConsumerEmail"] = value;
				}
			}

			// Token: 0x17000E10 RID: 3600
			// (set) Token: 0x060025D2 RID: 9682 RVA: 0x00048BF7 File Offset: 0x00046DF7
			public virtual bool AllowRemoteDesktop
			{
				set
				{
					base.PowerSharpParameters["AllowRemoteDesktop"] = value;
				}
			}

			// Token: 0x17000E11 RID: 3601
			// (set) Token: 0x060025D3 RID: 9683 RVA: 0x00048C0F File Offset: 0x00046E0F
			public virtual bool AllowInternetSharing
			{
				set
				{
					base.PowerSharpParameters["AllowInternetSharing"] = value;
				}
			}

			// Token: 0x17000E12 RID: 3602
			// (set) Token: 0x060025D4 RID: 9684 RVA: 0x00048C27 File Offset: 0x00046E27
			public virtual BluetoothType AllowBluetooth
			{
				set
				{
					base.PowerSharpParameters["AllowBluetooth"] = value;
				}
			}

			// Token: 0x17000E13 RID: 3603
			// (set) Token: 0x060025D5 RID: 9685 RVA: 0x00048C3F File Offset: 0x00046E3F
			public virtual CalendarAgeFilterType MaxCalendarAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxCalendarAgeFilter"] = value;
				}
			}

			// Token: 0x17000E14 RID: 3604
			// (set) Token: 0x060025D6 RID: 9686 RVA: 0x00048C57 File Offset: 0x00046E57
			public virtual EmailAgeFilterType MaxEmailAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxEmailAgeFilter"] = value;
				}
			}

			// Token: 0x17000E15 RID: 3605
			// (set) Token: 0x060025D7 RID: 9687 RVA: 0x00048C6F File Offset: 0x00046E6F
			public virtual SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000E16 RID: 3606
			// (set) Token: 0x060025D8 RID: 9688 RVA: 0x00048C87 File Offset: 0x00046E87
			public virtual EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptionSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000E17 RID: 3607
			// (set) Token: 0x060025D9 RID: 9689 RVA: 0x00048C9F File Offset: 0x00046E9F
			public virtual SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMEEncryptionAlgorithmNegotiation"] = value;
				}
			}

			// Token: 0x17000E18 RID: 3608
			// (set) Token: 0x060025DA RID: 9690 RVA: 0x00048CB7 File Offset: 0x00046EB7
			public virtual int MinPasswordComplexCharacters
			{
				set
				{
					base.PowerSharpParameters["MinPasswordComplexCharacters"] = value;
				}
			}

			// Token: 0x17000E19 RID: 3609
			// (set) Token: 0x060025DB RID: 9691 RVA: 0x00048CCF File Offset: 0x00046ECF
			public virtual Unlimited<int> MaxEmailBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000E1A RID: 3610
			// (set) Token: 0x060025DC RID: 9692 RVA: 0x00048CE7 File Offset: 0x00046EE7
			public virtual Unlimited<int> MaxEmailHTMLBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailHTMLBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000E1B RID: 3611
			// (set) Token: 0x060025DD RID: 9693 RVA: 0x00048CFF File Offset: 0x00046EFF
			public virtual MultiValuedProperty<string> UnapprovedInROMApplicationList
			{
				set
				{
					base.PowerSharpParameters["UnapprovedInROMApplicationList"] = value;
				}
			}

			// Token: 0x17000E1C RID: 3612
			// (set) Token: 0x060025DE RID: 9694 RVA: 0x00048D12 File Offset: 0x00046F12
			public virtual ApprovedApplicationCollection ApprovedApplicationList
			{
				set
				{
					base.PowerSharpParameters["ApprovedApplicationList"] = value;
				}
			}

			// Token: 0x17000E1D RID: 3613
			// (set) Token: 0x060025DF RID: 9695 RVA: 0x00048D25 File Offset: 0x00046F25
			public virtual bool AllowExternalDeviceManagement
			{
				set
				{
					base.PowerSharpParameters["AllowExternalDeviceManagement"] = value;
				}
			}

			// Token: 0x17000E1E RID: 3614
			// (set) Token: 0x060025E0 RID: 9696 RVA: 0x00048D3D File Offset: 0x00046F3D
			public virtual MobileOTAUpdateModeType MobileOTAUpdateMode
			{
				set
				{
					base.PowerSharpParameters["MobileOTAUpdateMode"] = value;
				}
			}

			// Token: 0x17000E1F RID: 3615
			// (set) Token: 0x060025E1 RID: 9697 RVA: 0x00048D55 File Offset: 0x00046F55
			public virtual bool AllowMobileOTAUpdate
			{
				set
				{
					base.PowerSharpParameters["AllowMobileOTAUpdate"] = value;
				}
			}

			// Token: 0x17000E20 RID: 3616
			// (set) Token: 0x060025E2 RID: 9698 RVA: 0x00048D6D File Offset: 0x00046F6D
			public virtual bool IrmEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmEnabled"] = value;
				}
			}

			// Token: 0x17000E21 RID: 3617
			// (set) Token: 0x060025E3 RID: 9699 RVA: 0x00048D85 File Offset: 0x00046F85
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000E22 RID: 3618
			// (set) Token: 0x060025E4 RID: 9700 RVA: 0x00048D98 File Offset: 0x00046F98
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000E23 RID: 3619
			// (set) Token: 0x060025E5 RID: 9701 RVA: 0x00048DB0 File Offset: 0x00046FB0
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000E24 RID: 3620
			// (set) Token: 0x060025E6 RID: 9702 RVA: 0x00048DC8 File Offset: 0x00046FC8
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000E25 RID: 3621
			// (set) Token: 0x060025E7 RID: 9703 RVA: 0x00048DE0 File Offset: 0x00046FE0
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000E26 RID: 3622
			// (set) Token: 0x060025E8 RID: 9704 RVA: 0x00048DF8 File Offset: 0x00046FF8
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
