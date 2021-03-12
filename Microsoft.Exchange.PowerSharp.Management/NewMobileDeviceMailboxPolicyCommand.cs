using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020001B3 RID: 435
	public class NewMobileDeviceMailboxPolicyCommand : SyntheticCommandWithPipelineInput<MobileMailboxPolicy, MobileMailboxPolicy>
	{
		// Token: 0x060024FA RID: 9466 RVA: 0x000478E6 File Offset: 0x00045AE6
		private NewMobileDeviceMailboxPolicyCommand() : base("New-MobileDeviceMailboxPolicy")
		{
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x000478F3 File Offset: 0x00045AF3
		public NewMobileDeviceMailboxPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00047902 File Offset: 0x00045B02
		public virtual NewMobileDeviceMailboxPolicyCommand SetParameters(NewMobileDeviceMailboxPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020001B4 RID: 436
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17000D4D RID: 3405
			// (set) Token: 0x060024FD RID: 9469 RVA: 0x0004790C File Offset: 0x00045B0C
			public virtual bool AttachmentsEnabled
			{
				set
				{
					base.PowerSharpParameters["AttachmentsEnabled"] = value;
				}
			}

			// Token: 0x17000D4E RID: 3406
			// (set) Token: 0x060024FE RID: 9470 RVA: 0x00047924 File Offset: 0x00045B24
			public virtual bool PasswordEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordEnabled"] = value;
				}
			}

			// Token: 0x17000D4F RID: 3407
			// (set) Token: 0x060024FF RID: 9471 RVA: 0x0004793C File Offset: 0x00045B3C
			public virtual bool AlphanumericPasswordRequired
			{
				set
				{
					base.PowerSharpParameters["AlphanumericPasswordRequired"] = value;
				}
			}

			// Token: 0x17000D50 RID: 3408
			// (set) Token: 0x06002500 RID: 9472 RVA: 0x00047954 File Offset: 0x00045B54
			public virtual bool PasswordRecoveryEnabled
			{
				set
				{
					base.PowerSharpParameters["PasswordRecoveryEnabled"] = value;
				}
			}

			// Token: 0x17000D51 RID: 3409
			// (set) Token: 0x06002501 RID: 9473 RVA: 0x0004796C File Offset: 0x00045B6C
			public virtual bool DeviceEncryptionEnabled
			{
				set
				{
					base.PowerSharpParameters["DeviceEncryptionEnabled"] = value;
				}
			}

			// Token: 0x17000D52 RID: 3410
			// (set) Token: 0x06002502 RID: 9474 RVA: 0x00047984 File Offset: 0x00045B84
			public virtual int? MinPasswordLength
			{
				set
				{
					base.PowerSharpParameters["MinPasswordLength"] = value;
				}
			}

			// Token: 0x17000D53 RID: 3411
			// (set) Token: 0x06002503 RID: 9475 RVA: 0x0004799C File Offset: 0x00045B9C
			public virtual Unlimited<EnhancedTimeSpan> MaxInactivityTimeLock
			{
				set
				{
					base.PowerSharpParameters["MaxInactivityTimeLock"] = value;
				}
			}

			// Token: 0x17000D54 RID: 3412
			// (set) Token: 0x06002504 RID: 9476 RVA: 0x000479B4 File Offset: 0x00045BB4
			public virtual Unlimited<int> MaxPasswordFailedAttempts
			{
				set
				{
					base.PowerSharpParameters["MaxPasswordFailedAttempts"] = value;
				}
			}

			// Token: 0x17000D55 RID: 3413
			// (set) Token: 0x06002505 RID: 9477 RVA: 0x000479CC File Offset: 0x00045BCC
			public virtual bool AllowNonProvisionableDevices
			{
				set
				{
					base.PowerSharpParameters["AllowNonProvisionableDevices"] = value;
				}
			}

			// Token: 0x17000D56 RID: 3414
			// (set) Token: 0x06002506 RID: 9478 RVA: 0x000479E4 File Offset: 0x00045BE4
			public virtual Unlimited<ByteQuantifiedSize> MaxAttachmentSize
			{
				set
				{
					base.PowerSharpParameters["MaxAttachmentSize"] = value;
				}
			}

			// Token: 0x17000D57 RID: 3415
			// (set) Token: 0x06002507 RID: 9479 RVA: 0x000479FC File Offset: 0x00045BFC
			public virtual bool AllowSimplePassword
			{
				set
				{
					base.PowerSharpParameters["AllowSimplePassword"] = value;
				}
			}

			// Token: 0x17000D58 RID: 3416
			// (set) Token: 0x06002508 RID: 9480 RVA: 0x00047A14 File Offset: 0x00045C14
			public virtual Unlimited<EnhancedTimeSpan> PasswordExpiration
			{
				set
				{
					base.PowerSharpParameters["PasswordExpiration"] = value;
				}
			}

			// Token: 0x17000D59 RID: 3417
			// (set) Token: 0x06002509 RID: 9481 RVA: 0x00047A2C File Offset: 0x00045C2C
			public virtual int PasswordHistory
			{
				set
				{
					base.PowerSharpParameters["PasswordHistory"] = value;
				}
			}

			// Token: 0x17000D5A RID: 3418
			// (set) Token: 0x0600250A RID: 9482 RVA: 0x00047A44 File Offset: 0x00045C44
			public virtual Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
			{
				set
				{
					base.PowerSharpParameters["DevicePolicyRefreshInterval"] = value;
				}
			}

			// Token: 0x17000D5B RID: 3419
			// (set) Token: 0x0600250B RID: 9483 RVA: 0x00047A5C File Offset: 0x00045C5C
			public virtual bool WSSAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["WSSAccessEnabled"] = value;
				}
			}

			// Token: 0x17000D5C RID: 3420
			// (set) Token: 0x0600250C RID: 9484 RVA: 0x00047A74 File Offset: 0x00045C74
			public virtual bool UNCAccessEnabled
			{
				set
				{
					base.PowerSharpParameters["UNCAccessEnabled"] = value;
				}
			}

			// Token: 0x17000D5D RID: 3421
			// (set) Token: 0x0600250D RID: 9485 RVA: 0x00047A8C File Offset: 0x00045C8C
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17000D5E RID: 3422
			// (set) Token: 0x0600250E RID: 9486 RVA: 0x00047AA4 File Offset: 0x00045CA4
			public virtual bool AllowStorageCard
			{
				set
				{
					base.PowerSharpParameters["AllowStorageCard"] = value;
				}
			}

			// Token: 0x17000D5F RID: 3423
			// (set) Token: 0x0600250F RID: 9487 RVA: 0x00047ABC File Offset: 0x00045CBC
			public virtual bool AllowCamera
			{
				set
				{
					base.PowerSharpParameters["AllowCamera"] = value;
				}
			}

			// Token: 0x17000D60 RID: 3424
			// (set) Token: 0x06002510 RID: 9488 RVA: 0x00047AD4 File Offset: 0x00045CD4
			public virtual bool RequireStorageCardEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireStorageCardEncryption"] = value;
				}
			}

			// Token: 0x17000D61 RID: 3425
			// (set) Token: 0x06002511 RID: 9489 RVA: 0x00047AEC File Offset: 0x00045CEC
			public virtual bool RequireDeviceEncryption
			{
				set
				{
					base.PowerSharpParameters["RequireDeviceEncryption"] = value;
				}
			}

			// Token: 0x17000D62 RID: 3426
			// (set) Token: 0x06002512 RID: 9490 RVA: 0x00047B04 File Offset: 0x00045D04
			public virtual bool AllowUnsignedApplications
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedApplications"] = value;
				}
			}

			// Token: 0x17000D63 RID: 3427
			// (set) Token: 0x06002513 RID: 9491 RVA: 0x00047B1C File Offset: 0x00045D1C
			public virtual bool AllowUnsignedInstallationPackages
			{
				set
				{
					base.PowerSharpParameters["AllowUnsignedInstallationPackages"] = value;
				}
			}

			// Token: 0x17000D64 RID: 3428
			// (set) Token: 0x06002514 RID: 9492 RVA: 0x00047B34 File Offset: 0x00045D34
			public virtual int MinPasswordComplexCharacters
			{
				set
				{
					base.PowerSharpParameters["MinPasswordComplexCharacters"] = value;
				}
			}

			// Token: 0x17000D65 RID: 3429
			// (set) Token: 0x06002515 RID: 9493 RVA: 0x00047B4C File Offset: 0x00045D4C
			public virtual bool AllowWiFi
			{
				set
				{
					base.PowerSharpParameters["AllowWiFi"] = value;
				}
			}

			// Token: 0x17000D66 RID: 3430
			// (set) Token: 0x06002516 RID: 9494 RVA: 0x00047B64 File Offset: 0x00045D64
			public virtual bool AllowTextMessaging
			{
				set
				{
					base.PowerSharpParameters["AllowTextMessaging"] = value;
				}
			}

			// Token: 0x17000D67 RID: 3431
			// (set) Token: 0x06002517 RID: 9495 RVA: 0x00047B7C File Offset: 0x00045D7C
			public virtual bool AllowPOPIMAPEmail
			{
				set
				{
					base.PowerSharpParameters["AllowPOPIMAPEmail"] = value;
				}
			}

			// Token: 0x17000D68 RID: 3432
			// (set) Token: 0x06002518 RID: 9496 RVA: 0x00047B94 File Offset: 0x00045D94
			public virtual BluetoothType AllowBluetooth
			{
				set
				{
					base.PowerSharpParameters["AllowBluetooth"] = value;
				}
			}

			// Token: 0x17000D69 RID: 3433
			// (set) Token: 0x06002519 RID: 9497 RVA: 0x00047BAC File Offset: 0x00045DAC
			public virtual bool AllowIrDA
			{
				set
				{
					base.PowerSharpParameters["AllowIrDA"] = value;
				}
			}

			// Token: 0x17000D6A RID: 3434
			// (set) Token: 0x0600251A RID: 9498 RVA: 0x00047BC4 File Offset: 0x00045DC4
			public virtual bool RequireManualSyncWhenRoaming
			{
				set
				{
					base.PowerSharpParameters["RequireManualSyncWhenRoaming"] = value;
				}
			}

			// Token: 0x17000D6B RID: 3435
			// (set) Token: 0x0600251B RID: 9499 RVA: 0x00047BDC File Offset: 0x00045DDC
			public virtual bool AllowDesktopSync
			{
				set
				{
					base.PowerSharpParameters["AllowDesktopSync"] = value;
				}
			}

			// Token: 0x17000D6C RID: 3436
			// (set) Token: 0x0600251C RID: 9500 RVA: 0x00047BF4 File Offset: 0x00045DF4
			public virtual CalendarAgeFilterType MaxCalendarAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxCalendarAgeFilter"] = value;
				}
			}

			// Token: 0x17000D6D RID: 3437
			// (set) Token: 0x0600251D RID: 9501 RVA: 0x00047C0C File Offset: 0x00045E0C
			public virtual bool AllowHTMLEmail
			{
				set
				{
					base.PowerSharpParameters["AllowHTMLEmail"] = value;
				}
			}

			// Token: 0x17000D6E RID: 3438
			// (set) Token: 0x0600251E RID: 9502 RVA: 0x00047C24 File Offset: 0x00045E24
			public virtual EmailAgeFilterType MaxEmailAgeFilter
			{
				set
				{
					base.PowerSharpParameters["MaxEmailAgeFilter"] = value;
				}
			}

			// Token: 0x17000D6F RID: 3439
			// (set) Token: 0x0600251F RID: 9503 RVA: 0x00047C3C File Offset: 0x00045E3C
			public virtual Unlimited<int> MaxEmailBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000D70 RID: 3440
			// (set) Token: 0x06002520 RID: 9504 RVA: 0x00047C54 File Offset: 0x00045E54
			public virtual Unlimited<int> MaxEmailHTMLBodyTruncationSize
			{
				set
				{
					base.PowerSharpParameters["MaxEmailHTMLBodyTruncationSize"] = value;
				}
			}

			// Token: 0x17000D71 RID: 3441
			// (set) Token: 0x06002521 RID: 9505 RVA: 0x00047C6C File Offset: 0x00045E6C
			public virtual bool RequireSignedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000D72 RID: 3442
			// (set) Token: 0x06002522 RID: 9506 RVA: 0x00047C84 File Offset: 0x00045E84
			public virtual bool RequireEncryptedSMIMEMessages
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptedSMIMEMessages"] = value;
				}
			}

			// Token: 0x17000D73 RID: 3443
			// (set) Token: 0x06002523 RID: 9507 RVA: 0x00047C9C File Offset: 0x00045E9C
			public virtual SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireSignedSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000D74 RID: 3444
			// (set) Token: 0x06002524 RID: 9508 RVA: 0x00047CB4 File Offset: 0x00045EB4
			public virtual EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
			{
				set
				{
					base.PowerSharpParameters["RequireEncryptionSMIMEAlgorithm"] = value;
				}
			}

			// Token: 0x17000D75 RID: 3445
			// (set) Token: 0x06002525 RID: 9509 RVA: 0x00047CCC File Offset: 0x00045ECC
			public virtual SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMEEncryptionAlgorithmNegotiation"] = value;
				}
			}

			// Token: 0x17000D76 RID: 3446
			// (set) Token: 0x06002526 RID: 9510 RVA: 0x00047CE4 File Offset: 0x00045EE4
			public virtual bool AllowSMIMESoftCerts
			{
				set
				{
					base.PowerSharpParameters["AllowSMIMESoftCerts"] = value;
				}
			}

			// Token: 0x17000D77 RID: 3447
			// (set) Token: 0x06002527 RID: 9511 RVA: 0x00047CFC File Offset: 0x00045EFC
			public virtual bool AllowBrowser
			{
				set
				{
					base.PowerSharpParameters["AllowBrowser"] = value;
				}
			}

			// Token: 0x17000D78 RID: 3448
			// (set) Token: 0x06002528 RID: 9512 RVA: 0x00047D14 File Offset: 0x00045F14
			public virtual bool AllowConsumerEmail
			{
				set
				{
					base.PowerSharpParameters["AllowConsumerEmail"] = value;
				}
			}

			// Token: 0x17000D79 RID: 3449
			// (set) Token: 0x06002529 RID: 9513 RVA: 0x00047D2C File Offset: 0x00045F2C
			public virtual bool AllowRemoteDesktop
			{
				set
				{
					base.PowerSharpParameters["AllowRemoteDesktop"] = value;
				}
			}

			// Token: 0x17000D7A RID: 3450
			// (set) Token: 0x0600252A RID: 9514 RVA: 0x00047D44 File Offset: 0x00045F44
			public virtual bool AllowInternetSharing
			{
				set
				{
					base.PowerSharpParameters["AllowInternetSharing"] = value;
				}
			}

			// Token: 0x17000D7B RID: 3451
			// (set) Token: 0x0600252B RID: 9515 RVA: 0x00047D5C File Offset: 0x00045F5C
			public virtual MultiValuedProperty<string> UnapprovedInROMApplicationList
			{
				set
				{
					base.PowerSharpParameters["UnapprovedInROMApplicationList"] = value;
				}
			}

			// Token: 0x17000D7C RID: 3452
			// (set) Token: 0x0600252C RID: 9516 RVA: 0x00047D6F File Offset: 0x00045F6F
			public virtual ApprovedApplicationCollection ApprovedApplicationList
			{
				set
				{
					base.PowerSharpParameters["ApprovedApplicationList"] = value;
				}
			}

			// Token: 0x17000D7D RID: 3453
			// (set) Token: 0x0600252D RID: 9517 RVA: 0x00047D82 File Offset: 0x00045F82
			public virtual bool AllowExternalDeviceManagement
			{
				set
				{
					base.PowerSharpParameters["AllowExternalDeviceManagement"] = value;
				}
			}

			// Token: 0x17000D7E RID: 3454
			// (set) Token: 0x0600252E RID: 9518 RVA: 0x00047D9A File Offset: 0x00045F9A
			public virtual MobileOTAUpdateModeType MobileOTAUpdateMode
			{
				set
				{
					base.PowerSharpParameters["MobileOTAUpdateMode"] = value;
				}
			}

			// Token: 0x17000D7F RID: 3455
			// (set) Token: 0x0600252F RID: 9519 RVA: 0x00047DB2 File Offset: 0x00045FB2
			public virtual bool AllowMobileOTAUpdate
			{
				set
				{
					base.PowerSharpParameters["AllowMobileOTAUpdate"] = value;
				}
			}

			// Token: 0x17000D80 RID: 3456
			// (set) Token: 0x06002530 RID: 9520 RVA: 0x00047DCA File Offset: 0x00045FCA
			public virtual bool IrmEnabled
			{
				set
				{
					base.PowerSharpParameters["IrmEnabled"] = value;
				}
			}

			// Token: 0x17000D81 RID: 3457
			// (set) Token: 0x06002531 RID: 9521 RVA: 0x00047DE2 File Offset: 0x00045FE2
			public virtual bool AllowApplePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowApplePushNotifications"] = value;
				}
			}

			// Token: 0x17000D82 RID: 3458
			// (set) Token: 0x06002532 RID: 9522 RVA: 0x00047DFA File Offset: 0x00045FFA
			public virtual bool AllowMicrosoftPushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowMicrosoftPushNotifications"] = value;
				}
			}

			// Token: 0x17000D83 RID: 3459
			// (set) Token: 0x06002533 RID: 9523 RVA: 0x00047E12 File Offset: 0x00046012
			public virtual bool AllowGooglePushNotifications
			{
				set
				{
					base.PowerSharpParameters["AllowGooglePushNotifications"] = value;
				}
			}

			// Token: 0x17000D84 RID: 3460
			// (set) Token: 0x06002534 RID: 9524 RVA: 0x00047E2A File Offset: 0x0004602A
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17000D85 RID: 3461
			// (set) Token: 0x06002535 RID: 9525 RVA: 0x00047E48 File Offset: 0x00046048
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17000D86 RID: 3462
			// (set) Token: 0x06002536 RID: 9526 RVA: 0x00047E5B File Offset: 0x0004605B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17000D87 RID: 3463
			// (set) Token: 0x06002537 RID: 9527 RVA: 0x00047E6E File Offset: 0x0004606E
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17000D88 RID: 3464
			// (set) Token: 0x06002538 RID: 9528 RVA: 0x00047E86 File Offset: 0x00046086
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17000D89 RID: 3465
			// (set) Token: 0x06002539 RID: 9529 RVA: 0x00047E9E File Offset: 0x0004609E
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17000D8A RID: 3466
			// (set) Token: 0x0600253A RID: 9530 RVA: 0x00047EB6 File Offset: 0x000460B6
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17000D8B RID: 3467
			// (set) Token: 0x0600253B RID: 9531 RVA: 0x00047ECE File Offset: 0x000460CE
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
