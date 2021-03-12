using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000100 RID: 256
	internal class PolicyData : IPolicyData
	{
		// Token: 0x06000DF6 RID: 3574 RVA: 0x0004DF9C File Offset: 0x0004C19C
		public PolicyData(MobileMailboxPolicy policy) : this(policy, GlobalSettings.IrmEnabled)
		{
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0004DFAC File Offset: 0x0004C1AC
		public PolicyData(MobileMailboxPolicy policy, bool useIrmEnabledPolicySetting)
		{
			this.identity = policy.OriginalId;
			this.allowNonProvisionableDevices = policy.AllowNonProvisionableDevices;
			this.alphanumericDevicePasswordRequired = policy.AlphanumericPasswordRequired;
			this.attachmentsEnabled = policy.AttachmentsEnabled;
			this.requireStorageCardEncryption = policy.RequireStorageCardEncryption;
			this.devicePasswordEnabled = policy.PasswordEnabled;
			this.passwordRecoveryEnabled = policy.PasswordRecoveryEnabled;
			this.devicePolicyRefreshInterval = policy.DevicePolicyRefreshInterval;
			this.allowSimpleDevicePassword = policy.AllowSimplePassword;
			this.maxAttachmentSize = policy.MaxAttachmentSize;
			this.wssAccessEnabled = policy.WSSAccessEnabled;
			this.uncAccessEnabled = policy.UNCAccessEnabled;
			this.minDevicePasswordLength = policy.MinPasswordLength;
			this.maxInactivityTimeDeviceLock = policy.MaxInactivityTimeLock;
			this.maxDevicePasswordFailedAttempts = policy.MaxPasswordFailedAttempts;
			this.devicePasswordExpiration = policy.PasswordExpiration;
			this.devicePasswordHistory = policy.PasswordHistory;
			this.isDefault = policy.IsDefault;
			this.allowStorageCard = policy.AllowStorageCard;
			this.allowCamera = policy.AllowCamera;
			this.requireDeviceEncryption = policy.RequireDeviceEncryption;
			this.allowUnsignedApplications = policy.AllowUnsignedApplications;
			this.allowUnsignedInstallationPackages = policy.AllowUnsignedInstallationPackages;
			this.allowWiFi = policy.AllowWiFi;
			this.allowTextMessaging = policy.AllowTextMessaging;
			this.allowPOPIMAPEmail = policy.AllowPOPIMAPEmail;
			this.allowIrDA = policy.AllowIrDA;
			this.requireManualSyncWhenRoaming = policy.RequireManualSyncWhenRoaming;
			this.allowDesktopSync = policy.AllowDesktopSync;
			this.allowHTMLEmail = policy.AllowHTMLEmail;
			this.requireSignedSMIMEMessages = policy.RequireSignedSMIMEMessages;
			this.requireEncryptedSMIMEMessages = policy.RequireEncryptedSMIMEMessages;
			this.allowSMIMESoftCerts = policy.AllowSMIMESoftCerts;
			this.allowBrowser = policy.AllowBrowser;
			this.allowConsumerEmail = policy.AllowConsumerEmail;
			this.allowRemoteDesktop = policy.AllowRemoteDesktop;
			this.allowInternetSharing = policy.AllowInternetSharing;
			this.allowBluetooth = policy.AllowBluetooth;
			this.maxCalendarAgeFilter = policy.MaxCalendarAgeFilter;
			this.maxEmailAgeFilter = policy.MaxEmailAgeFilter;
			this.requireSignedSMIMEAlgorithm = policy.RequireSignedSMIMEAlgorithm;
			this.requireEncryptionSMIMEAlgorithm = policy.RequireEncryptionSMIMEAlgorithm;
			this.allowSMIMEEncryptionAlgorithmNegotiation = policy.AllowSMIMEEncryptionAlgorithmNegotiation;
			this.minDevicePasswordComplexCharacters = policy.MinPasswordComplexCharacters;
			this.maxEmailBodyTruncationSize = policy.MaxEmailBodyTruncationSize;
			this.maxEmailHTMLBodyTruncationSize = policy.MaxEmailHTMLBodyTruncationSize;
			this.unapprovedInROMApplicationList = policy.UnapprovedInROMApplicationList;
			this.approvedApplicationList = policy.ApprovedApplicationList;
			this.allowExternalDeviceManagement = policy.AllowExternalDeviceManagement;
			this.MobileOTAUpdateMode = policy.MobileOTAUpdateMode;
			this.AllowMobileOTAUpdate = policy.AllowMobileOTAUpdate;
			this.isIrmEnabled = (useIrmEnabledPolicySetting && policy.IrmEnabled);
			string policyXml = ProvisionCommandPhaseOne.BuildEASProvisionDoc(20, out this.preversion121DeviceCompatibility, this) + policy.AllowNonProvisionableDevices;
			this.preversion121HashCode = PolicyData.GetPolicyHashCode(policyXml);
			policyXml = ProvisionCommandPhaseOne.BuildEASProvisionDoc(121, out this.version121DeviceCompatibility, this) + policy.AllowNonProvisionableDevices;
			this.version121HashCode = PolicyData.GetPolicyHashCode(policyXml);
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0004E28B File Offset: 0x0004C48B
		public ADObjectId Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0004E293 File Offset: 0x0004C493
		public bool AllowNonProvisionableDevices
		{
			get
			{
				return this.allowNonProvisionableDevices;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0004E29B File Offset: 0x0004C49B
		public bool AlphanumericDevicePasswordRequired
		{
			get
			{
				return this.alphanumericDevicePasswordRequired;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x0004E2A3 File Offset: 0x0004C4A3
		public bool AttachmentsEnabled
		{
			get
			{
				return this.attachmentsEnabled;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0004E2AB File Offset: 0x0004C4AB
		public bool RequireStorageCardEncryption
		{
			get
			{
				return this.requireStorageCardEncryption;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0004E2B3 File Offset: 0x0004C4B3
		public bool DevicePasswordEnabled
		{
			get
			{
				return this.devicePasswordEnabled;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0004E2BB File Offset: 0x0004C4BB
		public bool PasswordRecoveryEnabled
		{
			get
			{
				return this.passwordRecoveryEnabled;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0004E2C3 File Offset: 0x0004C4C3
		public Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
		{
			get
			{
				return this.devicePolicyRefreshInterval;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0004E2CB File Offset: 0x0004C4CB
		public bool AllowSimpleDevicePassword
		{
			get
			{
				return this.allowSimpleDevicePassword;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06000E01 RID: 3585 RVA: 0x0004E2D3 File Offset: 0x0004C4D3
		public Unlimited<ByteQuantifiedSize> MaxAttachmentSize
		{
			get
			{
				return this.maxAttachmentSize;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0004E2DB File Offset: 0x0004C4DB
		public bool WSSAccessEnabled
		{
			get
			{
				return this.wssAccessEnabled;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06000E03 RID: 3587 RVA: 0x0004E2E3 File Offset: 0x0004C4E3
		public bool UNCAccessEnabled
		{
			get
			{
				return this.uncAccessEnabled;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0004E2EB File Offset: 0x0004C4EB
		public int? MinDevicePasswordLength
		{
			get
			{
				return this.minDevicePasswordLength;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06000E05 RID: 3589 RVA: 0x0004E2F3 File Offset: 0x0004C4F3
		public Unlimited<EnhancedTimeSpan> MaxInactivityTimeDeviceLock
		{
			get
			{
				return this.maxInactivityTimeDeviceLock;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0004E2FB File Offset: 0x0004C4FB
		public Unlimited<int> MaxDevicePasswordFailedAttempts
		{
			get
			{
				return this.maxDevicePasswordFailedAttempts;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06000E07 RID: 3591 RVA: 0x0004E303 File Offset: 0x0004C503
		public Unlimited<EnhancedTimeSpan> DevicePasswordExpiration
		{
			get
			{
				return this.devicePasswordExpiration;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0004E30B File Offset: 0x0004C50B
		public int DevicePasswordHistory
		{
			get
			{
				return this.devicePasswordHistory;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06000E09 RID: 3593 RVA: 0x0004E313 File Offset: 0x0004C513
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0004E31B File Offset: 0x0004C51B
		public bool AllowStorageCard
		{
			get
			{
				return this.allowStorageCard;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000E0B RID: 3595 RVA: 0x0004E323 File Offset: 0x0004C523
		public bool AllowCamera
		{
			get
			{
				return this.allowCamera;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0004E32B File Offset: 0x0004C52B
		public bool RequireDeviceEncryption
		{
			get
			{
				return this.requireDeviceEncryption;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0004E333 File Offset: 0x0004C533
		public bool AllowUnsignedApplications
		{
			get
			{
				return this.allowUnsignedApplications;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0004E33B File Offset: 0x0004C53B
		public bool AllowUnsignedInstallationPackages
		{
			get
			{
				return this.allowUnsignedInstallationPackages;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0004E343 File Offset: 0x0004C543
		public bool AllowWiFi
		{
			get
			{
				return this.allowWiFi;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0004E34B File Offset: 0x0004C54B
		public bool AllowTextMessaging
		{
			get
			{
				return this.allowTextMessaging;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0004E353 File Offset: 0x0004C553
		public bool AllowPOPIMAPEmail
		{
			get
			{
				return this.allowPOPIMAPEmail;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0004E35B File Offset: 0x0004C55B
		public bool AllowIrDA
		{
			get
			{
				return this.allowIrDA;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0004E363 File Offset: 0x0004C563
		public bool RequireManualSyncWhenRoaming
		{
			get
			{
				return this.requireManualSyncWhenRoaming;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0004E36B File Offset: 0x0004C56B
		public bool AllowDesktopSync
		{
			get
			{
				return this.allowDesktopSync;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0004E373 File Offset: 0x0004C573
		public bool AllowHTMLEmail
		{
			get
			{
				return this.allowHTMLEmail;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0004E37B File Offset: 0x0004C57B
		public bool RequireSignedSMIMEMessages
		{
			get
			{
				return this.requireSignedSMIMEMessages;
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0004E383 File Offset: 0x0004C583
		public bool RequireEncryptedSMIMEMessages
		{
			get
			{
				return this.requireEncryptedSMIMEMessages;
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0004E38B File Offset: 0x0004C58B
		public bool AllowSMIMESoftCerts
		{
			get
			{
				return this.allowSMIMESoftCerts;
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0004E393 File Offset: 0x0004C593
		public bool AllowBrowser
		{
			get
			{
				return this.allowBrowser;
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0004E39B File Offset: 0x0004C59B
		public bool AllowConsumerEmail
		{
			get
			{
				return this.allowConsumerEmail;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0004E3A3 File Offset: 0x0004C5A3
		public bool AllowRemoteDesktop
		{
			get
			{
				return this.allowRemoteDesktop;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0004E3AB File Offset: 0x0004C5AB
		public bool AllowInternetSharing
		{
			get
			{
				return this.allowInternetSharing;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06000E1D RID: 3613 RVA: 0x0004E3B3 File Offset: 0x0004C5B3
		public BluetoothType AllowBluetooth
		{
			get
			{
				return this.allowBluetooth;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0004E3BB File Offset: 0x0004C5BB
		public CalendarAgeFilterType MaxCalendarAgeFilter
		{
			get
			{
				return this.maxCalendarAgeFilter;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0004E3C3 File Offset: 0x0004C5C3
		public EmailAgeFilterType MaxEmailAgeFilter
		{
			get
			{
				return this.maxEmailAgeFilter;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0004E3CB File Offset: 0x0004C5CB
		public SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
		{
			get
			{
				return this.requireSignedSMIMEAlgorithm;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0004E3D3 File Offset: 0x0004C5D3
		public EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
		{
			get
			{
				return this.requireEncryptionSMIMEAlgorithm;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0004E3DB File Offset: 0x0004C5DB
		public SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
		{
			get
			{
				return this.allowSMIMEEncryptionAlgorithmNegotiation;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000E23 RID: 3619 RVA: 0x0004E3E3 File Offset: 0x0004C5E3
		public int MinDevicePasswordComplexCharacters
		{
			get
			{
				return this.minDevicePasswordComplexCharacters;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0004E3EB File Offset: 0x0004C5EB
		public Unlimited<int> MaxEmailBodyTruncationSize
		{
			get
			{
				return this.maxEmailBodyTruncationSize;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x0004E3F3 File Offset: 0x0004C5F3
		public Unlimited<int> MaxEmailHTMLBodyTruncationSize
		{
			get
			{
				return this.maxEmailHTMLBodyTruncationSize;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0004E3FB File Offset: 0x0004C5FB
		public MultiValuedProperty<string> UnapprovedInROMApplicationList
		{
			get
			{
				return this.unapprovedInROMApplicationList;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0004E403 File Offset: 0x0004C603
		public ApprovedApplicationCollection ApprovedApplicationList
		{
			get
			{
				return this.approvedApplicationList;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0004E40B File Offset: 0x0004C60B
		public bool AllowExternalDeviceManagement
		{
			get
			{
				return this.allowExternalDeviceManagement;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0004E413 File Offset: 0x0004C613
		public bool IsIrmEnabled
		{
			get
			{
				return this.isIrmEnabled;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0004E41B File Offset: 0x0004C61B
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x0004E423 File Offset: 0x0004C623
		public MobileOTAUpdateModeType MobileOTAUpdateMode { get; private set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0004E42C File Offset: 0x0004C62C
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0004E434 File Offset: 0x0004C634
		public bool AllowMobileOTAUpdate { get; private set; }

		// Token: 0x06000E2E RID: 3630 RVA: 0x0004E43D File Offset: 0x0004C63D
		public static int GetPolicyHashCode(string policyXml)
		{
			return policyXml.GetHashCode();
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0004E445 File Offset: 0x0004C645
		public bool GetVersionCompatibility(int version)
		{
			if (version >= 121)
			{
				return this.version121DeviceCompatibility;
			}
			return this.preversion121DeviceCompatibility;
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0004E459 File Offset: 0x0004C659
		public int GetHashCode(int version)
		{
			if (version >= 121)
			{
				return this.version121HashCode;
			}
			return this.preversion121HashCode;
		}

		// Token: 0x040008CE RID: 2254
		private readonly bool isDefault;

		// Token: 0x040008CF RID: 2255
		private int preversion121HashCode;

		// Token: 0x040008D0 RID: 2256
		private int version121HashCode;

		// Token: 0x040008D1 RID: 2257
		private bool preversion121DeviceCompatibility;

		// Token: 0x040008D2 RID: 2258
		private bool version121DeviceCompatibility;

		// Token: 0x040008D3 RID: 2259
		private ADObjectId identity;

		// Token: 0x040008D4 RID: 2260
		private bool allowNonProvisionableDevices;

		// Token: 0x040008D5 RID: 2261
		private bool alphanumericDevicePasswordRequired;

		// Token: 0x040008D6 RID: 2262
		private bool attachmentsEnabled;

		// Token: 0x040008D7 RID: 2263
		private bool requireStorageCardEncryption;

		// Token: 0x040008D8 RID: 2264
		private bool devicePasswordEnabled;

		// Token: 0x040008D9 RID: 2265
		private bool passwordRecoveryEnabled;

		// Token: 0x040008DA RID: 2266
		private Unlimited<EnhancedTimeSpan> devicePolicyRefreshInterval;

		// Token: 0x040008DB RID: 2267
		private bool allowSimpleDevicePassword;

		// Token: 0x040008DC RID: 2268
		private Unlimited<ByteQuantifiedSize> maxAttachmentSize;

		// Token: 0x040008DD RID: 2269
		private bool wssAccessEnabled;

		// Token: 0x040008DE RID: 2270
		private bool uncAccessEnabled;

		// Token: 0x040008DF RID: 2271
		private int? minDevicePasswordLength;

		// Token: 0x040008E0 RID: 2272
		private Unlimited<EnhancedTimeSpan> maxInactivityTimeDeviceLock;

		// Token: 0x040008E1 RID: 2273
		private Unlimited<int> maxDevicePasswordFailedAttempts;

		// Token: 0x040008E2 RID: 2274
		private Unlimited<EnhancedTimeSpan> devicePasswordExpiration;

		// Token: 0x040008E3 RID: 2275
		private int devicePasswordHistory;

		// Token: 0x040008E4 RID: 2276
		private bool allowStorageCard;

		// Token: 0x040008E5 RID: 2277
		private bool allowCamera;

		// Token: 0x040008E6 RID: 2278
		private bool requireDeviceEncryption;

		// Token: 0x040008E7 RID: 2279
		private bool allowUnsignedApplications;

		// Token: 0x040008E8 RID: 2280
		private bool allowUnsignedInstallationPackages;

		// Token: 0x040008E9 RID: 2281
		private bool allowWiFi;

		// Token: 0x040008EA RID: 2282
		private bool allowTextMessaging;

		// Token: 0x040008EB RID: 2283
		private bool allowPOPIMAPEmail;

		// Token: 0x040008EC RID: 2284
		private bool allowIrDA;

		// Token: 0x040008ED RID: 2285
		private bool requireManualSyncWhenRoaming;

		// Token: 0x040008EE RID: 2286
		private bool allowDesktopSync;

		// Token: 0x040008EF RID: 2287
		private bool allowHTMLEmail;

		// Token: 0x040008F0 RID: 2288
		private bool requireSignedSMIMEMessages;

		// Token: 0x040008F1 RID: 2289
		private bool requireEncryptedSMIMEMessages;

		// Token: 0x040008F2 RID: 2290
		private bool allowSMIMESoftCerts;

		// Token: 0x040008F3 RID: 2291
		private bool allowBrowser;

		// Token: 0x040008F4 RID: 2292
		private bool allowConsumerEmail;

		// Token: 0x040008F5 RID: 2293
		private bool allowRemoteDesktop;

		// Token: 0x040008F6 RID: 2294
		private bool allowInternetSharing;

		// Token: 0x040008F7 RID: 2295
		private BluetoothType allowBluetooth;

		// Token: 0x040008F8 RID: 2296
		private CalendarAgeFilterType maxCalendarAgeFilter;

		// Token: 0x040008F9 RID: 2297
		private EmailAgeFilterType maxEmailAgeFilter;

		// Token: 0x040008FA RID: 2298
		private SignedSMIMEAlgorithmType requireSignedSMIMEAlgorithm;

		// Token: 0x040008FB RID: 2299
		private EncryptionSMIMEAlgorithmType requireEncryptionSMIMEAlgorithm;

		// Token: 0x040008FC RID: 2300
		private SMIMEEncryptionAlgorithmNegotiationType allowSMIMEEncryptionAlgorithmNegotiation;

		// Token: 0x040008FD RID: 2301
		private int minDevicePasswordComplexCharacters;

		// Token: 0x040008FE RID: 2302
		private Unlimited<int> maxEmailBodyTruncationSize;

		// Token: 0x040008FF RID: 2303
		private Unlimited<int> maxEmailHTMLBodyTruncationSize;

		// Token: 0x04000900 RID: 2304
		private MultiValuedProperty<string> unapprovedInROMApplicationList;

		// Token: 0x04000901 RID: 2305
		private ApprovedApplicationCollection approvedApplicationList;

		// Token: 0x04000902 RID: 2306
		private bool allowExternalDeviceManagement;

		// Token: 0x04000903 RID: 2307
		private bool isIrmEnabled;
	}
}
