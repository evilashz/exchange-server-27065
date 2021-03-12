using System;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200031B RID: 795
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class MobileMailboxPolicy : MailboxPolicy
	{
		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x0009D0BF File Offset: 0x0009B2BF
		internal override ADObjectSchema Schema
		{
			get
			{
				return MobileMailboxPolicy.schema;
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x0009D0C6 File Offset: 0x0009B2C6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return MobileMailboxPolicy.mostDerivedClass;
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x0009D0CD File Offset: 0x0009B2CD
		internal override ADObjectId ParentPath
		{
			get
			{
				return MobileMailboxPolicy.parentPath;
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x0009D0D4 File Offset: 0x0009B2D4
		internal override bool CheckForAssociatedUsers()
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, base.Id.DistinguishedName),
				new ExistsFilter(MobileMailboxPolicySchema.AssociatedUsers)
			});
			MobileMailboxPolicy[] array = base.Session.Find<MobileMailboxPolicy>(null, QueryScope.SubTree, filter, null, 1);
			return array != null && array.Length > 0;
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x0009D131 File Offset: 0x0009B331
		// (set) Token: 0x06002477 RID: 9335 RVA: 0x0009D143 File Offset: 0x0009B343
		[Parameter(Mandatory = false)]
		public bool AllowNonProvisionableDevices
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowNonProvisionableDevices];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowNonProvisionableDevices] = value;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06002478 RID: 9336 RVA: 0x0009D156 File Offset: 0x0009B356
		// (set) Token: 0x06002479 RID: 9337 RVA: 0x0009D168 File Offset: 0x0009B368
		[Parameter(Mandatory = false)]
		public bool AlphanumericPasswordRequired
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AlphanumericPasswordRequired];
			}
			set
			{
				this[MobileMailboxPolicySchema.AlphanumericPasswordRequired] = value;
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x0009D17B File Offset: 0x0009B37B
		// (set) Token: 0x0600247B RID: 9339 RVA: 0x0009D18D File Offset: 0x0009B38D
		[Parameter(Mandatory = false)]
		public bool AttachmentsEnabled
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AttachmentsEnabled];
			}
			set
			{
				this[MobileMailboxPolicySchema.AttachmentsEnabled] = value;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x0009D1A0 File Offset: 0x0009B3A0
		// (set) Token: 0x0600247D RID: 9341 RVA: 0x0009D1A8 File Offset: 0x0009B3A8
		[Parameter(Mandatory = false)]
		public bool DeviceEncryptionEnabled
		{
			get
			{
				return this.RequireStorageCardEncryption;
			}
			set
			{
				this.RequireStorageCardEncryption = value;
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x0009D1B1 File Offset: 0x0009B3B1
		// (set) Token: 0x0600247F RID: 9343 RVA: 0x0009D1C3 File Offset: 0x0009B3C3
		[Parameter(Mandatory = false)]
		public bool RequireStorageCardEncryption
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.RequireStorageCardEncryption];
			}
			set
			{
				this[MobileMailboxPolicySchema.RequireStorageCardEncryption] = value;
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x0009D1D6 File Offset: 0x0009B3D6
		// (set) Token: 0x06002481 RID: 9345 RVA: 0x0009D1E8 File Offset: 0x0009B3E8
		[Parameter(Mandatory = false)]
		public bool PasswordEnabled
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.PasswordEnabled];
			}
			set
			{
				this[MobileMailboxPolicySchema.PasswordEnabled] = value;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x0009D1FB File Offset: 0x0009B3FB
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x0009D20D File Offset: 0x0009B40D
		[Parameter(Mandatory = false)]
		public bool PasswordRecoveryEnabled
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.PasswordRecoveryEnabled];
			}
			set
			{
				this[MobileMailboxPolicySchema.PasswordRecoveryEnabled] = value;
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x0009D220 File Offset: 0x0009B420
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x0009D232 File Offset: 0x0009B432
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[MobileMailboxPolicySchema.DevicePolicyRefreshInterval];
			}
			set
			{
				this[MobileMailboxPolicySchema.DevicePolicyRefreshInterval] = value;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x0009D245 File Offset: 0x0009B445
		// (set) Token: 0x06002487 RID: 9351 RVA: 0x0009D257 File Offset: 0x0009B457
		[Parameter(Mandatory = false)]
		public bool AllowSimplePassword
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowSimplePassword];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowSimplePassword] = value;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002488 RID: 9352 RVA: 0x0009D26A File Offset: 0x0009B46A
		// (set) Token: 0x06002489 RID: 9353 RVA: 0x0009D27C File Offset: 0x0009B47C
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MaxAttachmentSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MobileMailboxPolicySchema.MaxAttachmentSize];
			}
			set
			{
				this[MobileMailboxPolicySchema.MaxAttachmentSize] = value;
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x0009D28F File Offset: 0x0009B48F
		// (set) Token: 0x0600248B RID: 9355 RVA: 0x0009D2A1 File Offset: 0x0009B4A1
		[Parameter(Mandatory = false)]
		public bool WSSAccessEnabled
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.WSSAccessEnabled];
			}
			set
			{
				this[MobileMailboxPolicySchema.WSSAccessEnabled] = value;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600248C RID: 9356 RVA: 0x0009D2B4 File Offset: 0x0009B4B4
		// (set) Token: 0x0600248D RID: 9357 RVA: 0x0009D2C6 File Offset: 0x0009B4C6
		[Parameter(Mandatory = false)]
		public bool UNCAccessEnabled
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.UNCAccessEnabled];
			}
			set
			{
				this[MobileMailboxPolicySchema.UNCAccessEnabled] = value;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x0600248E RID: 9358 RVA: 0x0009D2D9 File Offset: 0x0009B4D9
		// (set) Token: 0x0600248F RID: 9359 RVA: 0x0009D2EB File Offset: 0x0009B4EB
		[Parameter(Mandatory = false)]
		public int? MinPasswordLength
		{
			get
			{
				return (int?)this[MobileMailboxPolicySchema.MinPasswordLength];
			}
			set
			{
				this[MobileMailboxPolicySchema.MinPasswordLength] = value;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06002490 RID: 9360 RVA: 0x0009D2FE File Offset: 0x0009B4FE
		// (set) Token: 0x06002491 RID: 9361 RVA: 0x0009D310 File Offset: 0x0009B510
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> MaxInactivityTimeLock
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[MobileMailboxPolicySchema.MaxInactivityTimeLock];
			}
			set
			{
				this[MobileMailboxPolicySchema.MaxInactivityTimeLock] = value;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06002492 RID: 9362 RVA: 0x0009D323 File Offset: 0x0009B523
		// (set) Token: 0x06002493 RID: 9363 RVA: 0x0009D335 File Offset: 0x0009B535
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxPasswordFailedAttempts
		{
			get
			{
				return (Unlimited<int>)this[MobileMailboxPolicySchema.MaxPasswordFailedAttempts];
			}
			set
			{
				this[MobileMailboxPolicySchema.MaxPasswordFailedAttempts] = value;
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06002494 RID: 9364 RVA: 0x0009D348 File Offset: 0x0009B548
		// (set) Token: 0x06002495 RID: 9365 RVA: 0x0009D35A File Offset: 0x0009B55A
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> PasswordExpiration
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[MobileMailboxPolicySchema.PasswordExpiration];
			}
			set
			{
				this[MobileMailboxPolicySchema.PasswordExpiration] = value;
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06002496 RID: 9366 RVA: 0x0009D36D File Offset: 0x0009B56D
		// (set) Token: 0x06002497 RID: 9367 RVA: 0x0009D37F File Offset: 0x0009B57F
		[Parameter(Mandatory = false)]
		public int PasswordHistory
		{
			get
			{
				return (int)this[MobileMailboxPolicySchema.PasswordHistory];
			}
			set
			{
				this[MobileMailboxPolicySchema.PasswordHistory] = value;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06002498 RID: 9368 RVA: 0x0009D392 File Offset: 0x0009B592
		// (set) Token: 0x06002499 RID: 9369 RVA: 0x0009D3A4 File Offset: 0x0009B5A4
		[Parameter(Mandatory = false)]
		public override bool IsDefault
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.IsDefault];
			}
			set
			{
				this[MobileMailboxPolicySchema.IsDefault] = value;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600249A RID: 9370 RVA: 0x0009D3B7 File Offset: 0x0009B5B7
		// (set) Token: 0x0600249B RID: 9371 RVA: 0x0009D3CC File Offset: 0x0009B5CC
		[Parameter(Mandatory = false)]
		public bool AllowApplePushNotifications
		{
			get
			{
				return !(bool)this[MobileMailboxPolicySchema.DenyApplePushNotifications];
			}
			set
			{
				this[MobileMailboxPolicySchema.DenyApplePushNotifications] = !value;
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x0600249C RID: 9372 RVA: 0x0009D3E2 File Offset: 0x0009B5E2
		// (set) Token: 0x0600249D RID: 9373 RVA: 0x0009D3F7 File Offset: 0x0009B5F7
		[Parameter(Mandatory = false)]
		public bool AllowMicrosoftPushNotifications
		{
			get
			{
				return !(bool)this[MobileMailboxPolicySchema.DenyMicrosoftPushNotifications];
			}
			set
			{
				this[MobileMailboxPolicySchema.DenyMicrosoftPushNotifications] = !value;
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600249E RID: 9374 RVA: 0x0009D40D File Offset: 0x0009B60D
		// (set) Token: 0x0600249F RID: 9375 RVA: 0x0009D422 File Offset: 0x0009B622
		[Parameter(Mandatory = false)]
		public bool AllowGooglePushNotifications
		{
			get
			{
				return !(bool)this[MobileMailboxPolicySchema.DenyGooglePushNotifications];
			}
			set
			{
				this[MobileMailboxPolicySchema.DenyGooglePushNotifications] = !value;
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060024A0 RID: 9376 RVA: 0x0009D438 File Offset: 0x0009B638
		// (set) Token: 0x060024A1 RID: 9377 RVA: 0x0009D44A File Offset: 0x0009B64A
		[Parameter(Mandatory = false)]
		public bool AllowStorageCard
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowStorageCard];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowStorageCard] = value;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060024A2 RID: 9378 RVA: 0x0009D45D File Offset: 0x0009B65D
		// (set) Token: 0x060024A3 RID: 9379 RVA: 0x0009D46F File Offset: 0x0009B66F
		[Parameter(Mandatory = false)]
		public bool AllowCamera
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowCamera];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowCamera] = value;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060024A4 RID: 9380 RVA: 0x0009D482 File Offset: 0x0009B682
		// (set) Token: 0x060024A5 RID: 9381 RVA: 0x0009D494 File Offset: 0x0009B694
		[Parameter(Mandatory = false)]
		public bool RequireDeviceEncryption
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.RequireDeviceEncryption];
			}
			set
			{
				this[MobileMailboxPolicySchema.RequireDeviceEncryption] = value;
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060024A6 RID: 9382 RVA: 0x0009D4A7 File Offset: 0x0009B6A7
		// (set) Token: 0x060024A7 RID: 9383 RVA: 0x0009D4B9 File Offset: 0x0009B6B9
		[Parameter(Mandatory = false)]
		public bool AllowUnsignedApplications
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowUnsignedApplications];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowUnsignedApplications] = value;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060024A8 RID: 9384 RVA: 0x0009D4CC File Offset: 0x0009B6CC
		// (set) Token: 0x060024A9 RID: 9385 RVA: 0x0009D4DE File Offset: 0x0009B6DE
		[Parameter(Mandatory = false)]
		public bool AllowUnsignedInstallationPackages
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowUnsignedInstallationPackages];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowUnsignedInstallationPackages] = value;
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060024AA RID: 9386 RVA: 0x0009D4F1 File Offset: 0x0009B6F1
		// (set) Token: 0x060024AB RID: 9387 RVA: 0x0009D503 File Offset: 0x0009B703
		[Parameter(Mandatory = false)]
		public bool AllowWiFi
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowWiFi];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowWiFi] = value;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060024AC RID: 9388 RVA: 0x0009D516 File Offset: 0x0009B716
		// (set) Token: 0x060024AD RID: 9389 RVA: 0x0009D528 File Offset: 0x0009B728
		[Parameter(Mandatory = false)]
		public bool AllowTextMessaging
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowTextMessaging];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowTextMessaging] = value;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060024AE RID: 9390 RVA: 0x0009D53B File Offset: 0x0009B73B
		// (set) Token: 0x060024AF RID: 9391 RVA: 0x0009D54D File Offset: 0x0009B74D
		[Parameter(Mandatory = false)]
		public bool AllowPOPIMAPEmail
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowPOPIMAPEmail];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowPOPIMAPEmail] = value;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x060024B0 RID: 9392 RVA: 0x0009D560 File Offset: 0x0009B760
		// (set) Token: 0x060024B1 RID: 9393 RVA: 0x0009D572 File Offset: 0x0009B772
		[Parameter(Mandatory = false)]
		public bool AllowIrDA
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowIrDA];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowIrDA] = value;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x060024B2 RID: 9394 RVA: 0x0009D585 File Offset: 0x0009B785
		// (set) Token: 0x060024B3 RID: 9395 RVA: 0x0009D597 File Offset: 0x0009B797
		[Parameter(Mandatory = false)]
		public bool RequireManualSyncWhenRoaming
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.RequireManualSyncWhenRoaming];
			}
			set
			{
				this[MobileMailboxPolicySchema.RequireManualSyncWhenRoaming] = value;
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x060024B4 RID: 9396 RVA: 0x0009D5AA File Offset: 0x0009B7AA
		// (set) Token: 0x060024B5 RID: 9397 RVA: 0x0009D5BC File Offset: 0x0009B7BC
		[Parameter(Mandatory = false)]
		public bool AllowDesktopSync
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowDesktopSync];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowDesktopSync] = value;
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x060024B6 RID: 9398 RVA: 0x0009D5CF File Offset: 0x0009B7CF
		// (set) Token: 0x060024B7 RID: 9399 RVA: 0x0009D5E1 File Offset: 0x0009B7E1
		[Parameter(Mandatory = false)]
		public bool AllowHTMLEmail
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowHTMLEmail];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowHTMLEmail] = value;
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x0009D5F4 File Offset: 0x0009B7F4
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x0009D606 File Offset: 0x0009B806
		[Parameter(Mandatory = false)]
		public bool RequireSignedSMIMEMessages
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.RequireSignedSMIMEMessages];
			}
			set
			{
				this[MobileMailboxPolicySchema.RequireSignedSMIMEMessages] = value;
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x0009D619 File Offset: 0x0009B819
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x0009D62B File Offset: 0x0009B82B
		[Parameter(Mandatory = false)]
		public bool RequireEncryptedSMIMEMessages
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.RequireEncryptedSMIMEMessages];
			}
			set
			{
				this[MobileMailboxPolicySchema.RequireEncryptedSMIMEMessages] = value;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x0009D63E File Offset: 0x0009B83E
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x0009D650 File Offset: 0x0009B850
		[Parameter(Mandatory = false)]
		public bool AllowSMIMESoftCerts
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowSMIMESoftCerts];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowSMIMESoftCerts] = value;
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x0009D663 File Offset: 0x0009B863
		// (set) Token: 0x060024BF RID: 9407 RVA: 0x0009D675 File Offset: 0x0009B875
		[Parameter(Mandatory = false)]
		public bool AllowBrowser
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowBrowser];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowBrowser] = value;
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x0009D688 File Offset: 0x0009B888
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x0009D69A File Offset: 0x0009B89A
		[Parameter(Mandatory = false)]
		public bool AllowConsumerEmail
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowConsumerEmail];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowConsumerEmail] = value;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x0009D6AD File Offset: 0x0009B8AD
		// (set) Token: 0x060024C3 RID: 9411 RVA: 0x0009D6BF File Offset: 0x0009B8BF
		[Parameter(Mandatory = false)]
		public bool AllowRemoteDesktop
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowRemoteDesktop];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowRemoteDesktop] = value;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x0009D6D2 File Offset: 0x0009B8D2
		// (set) Token: 0x060024C5 RID: 9413 RVA: 0x0009D6E4 File Offset: 0x0009B8E4
		[Parameter(Mandatory = false)]
		public bool AllowInternetSharing
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowInternetSharing];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowInternetSharing] = value;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x0009D6F7 File Offset: 0x0009B8F7
		// (set) Token: 0x060024C7 RID: 9415 RVA: 0x0009D709 File Offset: 0x0009B909
		[Parameter(Mandatory = false)]
		public BluetoothType AllowBluetooth
		{
			get
			{
				return (BluetoothType)this[MobileMailboxPolicySchema.AllowBluetooth];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowBluetooth] = value;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x0009D71C File Offset: 0x0009B91C
		// (set) Token: 0x060024C9 RID: 9417 RVA: 0x0009D72E File Offset: 0x0009B92E
		[Parameter(Mandatory = false)]
		public CalendarAgeFilterType MaxCalendarAgeFilter
		{
			get
			{
				return (CalendarAgeFilterType)this[MobileMailboxPolicySchema.MaxCalendarAgeFilter];
			}
			set
			{
				this[MobileMailboxPolicySchema.MaxCalendarAgeFilter] = value;
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x0009D741 File Offset: 0x0009B941
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x0009D753 File Offset: 0x0009B953
		[Parameter(Mandatory = false)]
		public EmailAgeFilterType MaxEmailAgeFilter
		{
			get
			{
				return (EmailAgeFilterType)this[MobileMailboxPolicySchema.MaxEmailAgeFilter];
			}
			set
			{
				this[MobileMailboxPolicySchema.MaxEmailAgeFilter] = value;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x0009D766 File Offset: 0x0009B966
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x0009D778 File Offset: 0x0009B978
		[Parameter(Mandatory = false)]
		public SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
		{
			get
			{
				return (SignedSMIMEAlgorithmType)this[MobileMailboxPolicySchema.RequireSignedSMIMEAlgorithm];
			}
			set
			{
				this[MobileMailboxPolicySchema.RequireSignedSMIMEAlgorithm] = value;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x0009D78B File Offset: 0x0009B98B
		// (set) Token: 0x060024CF RID: 9423 RVA: 0x0009D79D File Offset: 0x0009B99D
		[Parameter(Mandatory = false)]
		public EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
		{
			get
			{
				return (EncryptionSMIMEAlgorithmType)this[MobileMailboxPolicySchema.RequireEncryptionSMIMEAlgorithm];
			}
			set
			{
				this[MobileMailboxPolicySchema.RequireEncryptionSMIMEAlgorithm] = value;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x0009D7B0 File Offset: 0x0009B9B0
		// (set) Token: 0x060024D1 RID: 9425 RVA: 0x0009D7C2 File Offset: 0x0009B9C2
		[Parameter(Mandatory = false)]
		public SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
		{
			get
			{
				return (SMIMEEncryptionAlgorithmNegotiationType)this[MobileMailboxPolicySchema.AllowSMIMEEncryptionAlgorithmNegotiation];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowSMIMEEncryptionAlgorithmNegotiation] = value;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060024D2 RID: 9426 RVA: 0x0009D7D5 File Offset: 0x0009B9D5
		// (set) Token: 0x060024D3 RID: 9427 RVA: 0x0009D7E7 File Offset: 0x0009B9E7
		[Parameter(Mandatory = false)]
		public int MinPasswordComplexCharacters
		{
			get
			{
				return (int)this[MobileMailboxPolicySchema.MinPasswordComplexCharacters];
			}
			set
			{
				this[MobileMailboxPolicySchema.MinPasswordComplexCharacters] = value;
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060024D4 RID: 9428 RVA: 0x0009D7FA File Offset: 0x0009B9FA
		// (set) Token: 0x060024D5 RID: 9429 RVA: 0x0009D80C File Offset: 0x0009BA0C
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxEmailBodyTruncationSize
		{
			get
			{
				return (Unlimited<int>)this[MobileMailboxPolicySchema.MaxEmailBodyTruncationSize];
			}
			set
			{
				this[MobileMailboxPolicySchema.MaxEmailBodyTruncationSize] = value;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060024D6 RID: 9430 RVA: 0x0009D81F File Offset: 0x0009BA1F
		// (set) Token: 0x060024D7 RID: 9431 RVA: 0x0009D831 File Offset: 0x0009BA31
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxEmailHTMLBodyTruncationSize
		{
			get
			{
				return (Unlimited<int>)this[MobileMailboxPolicySchema.MaxEmailHTMLBodyTruncationSize];
			}
			set
			{
				this[MobileMailboxPolicySchema.MaxEmailHTMLBodyTruncationSize] = value;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x0009D844 File Offset: 0x0009BA44
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x0009D856 File Offset: 0x0009BA56
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UnapprovedInROMApplicationList
		{
			get
			{
				return (MultiValuedProperty<string>)this[MobileMailboxPolicySchema.UnapprovedInROMApplicationList];
			}
			set
			{
				this[MobileMailboxPolicySchema.UnapprovedInROMApplicationList] = value;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x0009D864 File Offset: 0x0009BA64
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x0009D876 File Offset: 0x0009BA76
		[Parameter(Mandatory = false)]
		public ApprovedApplicationCollection ApprovedApplicationList
		{
			get
			{
				return (ApprovedApplicationCollection)this[MobileMailboxPolicySchema.ADApprovedApplicationList];
			}
			set
			{
				this[MobileMailboxPolicySchema.ADApprovedApplicationList] = value;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x0009D884 File Offset: 0x0009BA84
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x0009D896 File Offset: 0x0009BA96
		[Parameter(Mandatory = false)]
		public bool AllowExternalDeviceManagement
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowExternalDeviceManagement];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowExternalDeviceManagement] = value;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x0009D8A9 File Offset: 0x0009BAA9
		// (set) Token: 0x060024DF RID: 9439 RVA: 0x0009D8BB File Offset: 0x0009BABB
		[Parameter(Mandatory = false)]
		public MobileOTAUpdateModeType MobileOTAUpdateMode
		{
			get
			{
				return (MobileOTAUpdateModeType)this[MobileMailboxPolicySchema.MobileOTAUpdateMode];
			}
			set
			{
				this[MobileMailboxPolicySchema.MobileOTAUpdateMode] = value;
			}
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x0009D8CE File Offset: 0x0009BACE
		// (set) Token: 0x060024E1 RID: 9441 RVA: 0x0009D8E0 File Offset: 0x0009BAE0
		[Parameter(Mandatory = false)]
		public bool AllowMobileOTAUpdate
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.AllowMobileOTAUpdate];
			}
			set
			{
				this[MobileMailboxPolicySchema.AllowMobileOTAUpdate] = value;
			}
		}

		// Token: 0x1700099B RID: 2459
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x0009D8F3 File Offset: 0x0009BAF3
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x0009D905 File Offset: 0x0009BB05
		[Parameter(Mandatory = false)]
		public bool IrmEnabled
		{
			get
			{
				return (bool)this[MobileMailboxPolicySchema.IrmEnabled];
			}
			set
			{
				this[MobileMailboxPolicySchema.IrmEnabled] = value;
			}
		}

		// Token: 0x040016CB RID: 5835
		private static MobileMailboxPolicySchema schema = ObjectSchema.GetInstance<MobileMailboxPolicySchema>();

		// Token: 0x040016CC RID: 5836
		private static string mostDerivedClass = "msExchMobileMailboxPolicy";

		// Token: 0x040016CD RID: 5837
		private static ADObjectId parentPath = new ADObjectId("CN=Mobile Mailbox Policies");
	}
}
