using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F6 RID: 758
	[DataContract]
	public class ActiveSyncMailboxPolicyObject : ActiveSyncMailboxPolicyRow
	{
		// Token: 0x06002D4F RID: 11599 RVA: 0x0008B115 File Offset: 0x00089315
		public ActiveSyncMailboxPolicyObject(ActiveSyncMailboxPolicy policy) : base(policy)
		{
		}

		// Token: 0x17001E2F RID: 7727
		// (get) Token: 0x06002D50 RID: 11600 RVA: 0x0008B11E File Offset: 0x0008931E
		// (set) Token: 0x06002D51 RID: 11601 RVA: 0x0008B12B File Offset: 0x0008932B
		[DataMember]
		public string Name
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E30 RID: 7728
		// (get) Token: 0x06002D52 RID: 11602 RVA: 0x0008B132 File Offset: 0x00089332
		// (set) Token: 0x06002D53 RID: 11603 RVA: 0x0008B13F File Offset: 0x0008933F
		[DataMember]
		public bool AllowNonProvisionableDevices
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowNonProvisionableDevices;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06002D54 RID: 11604 RVA: 0x0008B146 File Offset: 0x00089346
		// (set) Token: 0x06002D55 RID: 11605 RVA: 0x0008B165 File Offset: 0x00089365
		[DataMember]
		public string AllowNonProvisionableDevicesString
		{
			get
			{
				if (this.AllowNonProvisionableDevices)
				{
					return Strings.AllowNonProvisionable;
				}
				return Strings.DisallowNonProvisionable;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x06002D56 RID: 11606 RVA: 0x0008B16C File Offset: 0x0008936C
		// (set) Token: 0x06002D57 RID: 11607 RVA: 0x0008B1AB File Offset: 0x000893AB
		[DataMember]
		public string DisplayTitle
		{
			get
			{
				if (base.IsDefault)
				{
					return string.Format(Strings.DefaultEASPolicyDetailTitle, base.ActiveSyncMailboxPolicy.Name);
				}
				return string.Format(Strings.EASPolicyDetailTitle, base.ActiveSyncMailboxPolicy.Name);
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x0008B1B4 File Offset: 0x000893B4
		// (set) Token: 0x06002D59 RID: 11609 RVA: 0x0008B2FB File Offset: 0x000894FB
		[DataMember]
		public string PasswordRequirementsString
		{
			get
			{
				if (!this.DevicePasswordEnabled)
				{
					return Strings.PasswordNotRequired;
				}
				if (this.IsMaxInactivityTimeDeviceLockSet)
				{
					if (this.AlphanumericDevicePasswordRequired)
					{
						return string.Format(Strings.RequiredAlphaLockingPassword, base.ActiveSyncMailboxPolicy.MinDevicePasswordLength, base.ActiveSyncMailboxPolicy.MaxInactivityTimeDeviceLock.Value.TotalMinutes);
					}
					if (this.IsMinDevicePasswordLengthSet)
					{
						return string.Format(Strings.RequiredPinLockingPassword, base.ActiveSyncMailboxPolicy.MinDevicePasswordLength, base.ActiveSyncMailboxPolicy.MaxInactivityTimeDeviceLock.Value.TotalMinutes);
					}
					return string.Format(Strings.RequiredLockingPassword, base.ActiveSyncMailboxPolicy.MaxInactivityTimeDeviceLock.Value.TotalMinutes);
				}
				else
				{
					if (this.AlphanumericDevicePasswordRequired)
					{
						return string.Format(Strings.RequiredAlphaPassword, base.ActiveSyncMailboxPolicy.MinDevicePasswordLength);
					}
					if (this.IsMinDevicePasswordLengthSet)
					{
						return string.Format(Strings.RequiredPinPassword, base.ActiveSyncMailboxPolicy.MinDevicePasswordLength);
					}
					return Strings.PasswordRequired;
				}
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x0008B302 File Offset: 0x00089502
		// (set) Token: 0x06002D5B RID: 11611 RVA: 0x0008B30F File Offset: 0x0008950F
		[DataMember]
		public bool DevicePasswordEnabled
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.DevicePasswordEnabled;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001E35 RID: 7733
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x0008B316 File Offset: 0x00089516
		// (set) Token: 0x06002D5D RID: 11613 RVA: 0x0008B323 File Offset: 0x00089523
		[DataMember]
		public bool AllowSimpleDevicePassword
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowSimpleDevicePassword;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001E36 RID: 7734
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x0008B32A File Offset: 0x0008952A
		// (set) Token: 0x06002D5F RID: 11615 RVA: 0x0008B337 File Offset: 0x00089537
		[DataMember]
		public bool AlphanumericDevicePasswordRequired
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AlphanumericDevicePasswordRequired;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001E37 RID: 7735
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x0008B340 File Offset: 0x00089540
		// (set) Token: 0x06002D61 RID: 11617 RVA: 0x0008B360 File Offset: 0x00089560
		[DataMember]
		public bool IsMinDevicePasswordLengthSet
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.MinDevicePasswordLength != null;
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001E38 RID: 7736
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x0008B368 File Offset: 0x00089568
		// (set) Token: 0x06002D63 RID: 11619 RVA: 0x0008B39E File Offset: 0x0008959E
		[DataMember]
		public string MinDevicePasswordLength
		{
			get
			{
				if (this.IsMinDevicePasswordLengthSet)
				{
					return base.ActiveSyncMailboxPolicy.MinDevicePasswordLength.Value.ToString();
				}
				return "4";
			}
			private set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001E39 RID: 7737
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x0008B3A8 File Offset: 0x000895A8
		// (set) Token: 0x06002D65 RID: 11621 RVA: 0x0008B3CB File Offset: 0x000895CB
		[DataMember]
		public bool IsMaxDevicePasswordFailedAttemptsSet
		{
			get
			{
				return !base.ActiveSyncMailboxPolicy.MaxDevicePasswordFailedAttempts.IsUnlimited;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E3A RID: 7738
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x0008B3D4 File Offset: 0x000895D4
		// (set) Token: 0x06002D67 RID: 11623 RVA: 0x0008B40F File Offset: 0x0008960F
		[DataMember]
		public string MaxDevicePasswordFailedAttempts
		{
			get
			{
				if (this.IsMaxDevicePasswordFailedAttemptsSet)
				{
					return base.ActiveSyncMailboxPolicy.MaxDevicePasswordFailedAttempts.Value.ToString(CultureInfo.InvariantCulture);
				}
				return "8";
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E3B RID: 7739
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x0008B416 File Offset: 0x00089616
		// (set) Token: 0x06002D69 RID: 11625 RVA: 0x0008B445 File Offset: 0x00089645
		[DataMember]
		public string MaxDevicePasswordFailedAttemptsString
		{
			get
			{
				if (this.IsMaxDevicePasswordFailedAttemptsSet)
				{
					return string.Format(Strings.MaxDevicePasswordFailedAttempts, base.ActiveSyncMailboxPolicy.MaxDevicePasswordFailedAttempts);
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E3C RID: 7740
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x0008B44C File Offset: 0x0008964C
		// (set) Token: 0x06002D6B RID: 11627 RVA: 0x0008B46F File Offset: 0x0008966F
		[DataMember]
		public bool IsMaxInactivityTimeDeviceLockSet
		{
			get
			{
				return !base.ActiveSyncMailboxPolicy.MaxInactivityTimeDeviceLock.IsUnlimited;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E3D RID: 7741
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x0008B478 File Offset: 0x00089678
		// (set) Token: 0x06002D6D RID: 11629 RVA: 0x0008B4BB File Offset: 0x000896BB
		[DataMember]
		public string MaxInactivityTimeDeviceLock
		{
			get
			{
				if (this.IsMaxInactivityTimeDeviceLockSet)
				{
					return base.ActiveSyncMailboxPolicy.MaxInactivityTimeDeviceLock.Value.TotalMinutes.ToString(CultureInfo.InvariantCulture);
				}
				return "15";
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E3E RID: 7742
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x0008B4C2 File Offset: 0x000896C2
		// (set) Token: 0x06002D6F RID: 11631 RVA: 0x0008B4CF File Offset: 0x000896CF
		[DataMember]
		public bool RequireDeviceEncryption
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.RequireDeviceEncryption;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E3F RID: 7743
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x0008B4D6 File Offset: 0x000896D6
		// (set) Token: 0x06002D71 RID: 11633 RVA: 0x0008B4F5 File Offset: 0x000896F5
		[DataMember]
		public string RequireDeviceEncryptionString
		{
			get
			{
				if (this.RequireDeviceEncryption)
				{
					return Strings.DeviceEncryptionRequired;
				}
				return Strings.DeviceEncryptionNotRequired;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E40 RID: 7744
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x0008B4FC File Offset: 0x000896FC
		// (set) Token: 0x06002D73 RID: 11635 RVA: 0x0008B509 File Offset: 0x00089709
		[DataMember]
		public bool RequireStorageCardEncryption
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.RequireStorageCardEncryption;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E41 RID: 7745
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x0008B510 File Offset: 0x00089710
		// (set) Token: 0x06002D75 RID: 11637 RVA: 0x0008B534 File Offset: 0x00089734
		[DataMember]
		public string AllowStorageCardString
		{
			get
			{
				if (base.ActiveSyncMailboxPolicy.AllowStorageCard)
				{
					return Strings.StorageCardAllowed;
				}
				return Strings.StorageCardNotAllowed;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E42 RID: 7746
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x0008B53B File Offset: 0x0008973B
		// (set) Token: 0x06002D77 RID: 11639 RVA: 0x0008B55F File Offset: 0x0008975F
		[DataMember]
		public string AllowCameraString
		{
			get
			{
				if (base.ActiveSyncMailboxPolicy.AllowCamera)
				{
					return Strings.CameraAllowed;
				}
				return Strings.CameraNotAllowed;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E43 RID: 7747
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x0008B566 File Offset: 0x00089766
		// (set) Token: 0x06002D79 RID: 11641 RVA: 0x0008B58A File Offset: 0x0008978A
		[DataMember]
		public string AllowBrowserString
		{
			get
			{
				if (base.ActiveSyncMailboxPolicy.AllowBrowser)
				{
					return Strings.BrowserAllowed;
				}
				return Strings.BrowserNotAllowed;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E44 RID: 7748
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x0008B591 File Offset: 0x00089791
		// (set) Token: 0x06002D7B RID: 11643 RVA: 0x0008B5B5 File Offset: 0x000897B5
		[DataMember]
		public string RequireManualSyncWhenRoamingString
		{
			get
			{
				if (base.ActiveSyncMailboxPolicy.RequireManualSyncWhenRoaming)
				{
					return Strings.ManualRoamingSyncRequired;
				}
				return Strings.ManualRoamingSyncNotRequired;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E45 RID: 7749
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x0008B5BC File Offset: 0x000897BC
		// (set) Token: 0x06002D7D RID: 11645 RVA: 0x0008B5DF File Offset: 0x000897DF
		[DataMember]
		public bool IsDevicePolicyRefreshIntervalSet
		{
			get
			{
				return !base.ActiveSyncMailboxPolicy.DevicePolicyRefreshInterval.IsUnlimited;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E46 RID: 7750
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x0008B5E8 File Offset: 0x000897E8
		// (set) Token: 0x06002D7F RID: 11647 RVA: 0x0008B626 File Offset: 0x00089826
		[DataMember]
		public string DevicePolicyRefreshInterval
		{
			get
			{
				if (this.IsDevicePolicyRefreshIntervalSet)
				{
					return base.ActiveSyncMailboxPolicy.DevicePolicyRefreshInterval.Value.TotalHours.ToString();
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E47 RID: 7751
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x0008B62D File Offset: 0x0008982D
		// (set) Token: 0x06002D81 RID: 11649 RVA: 0x0008B63A File Offset: 0x0008983A
		[DataMember]
		public bool PasswordRecoveryEnabled
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.PasswordRecoveryEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E48 RID: 7752
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x0008B641 File Offset: 0x00089841
		// (set) Token: 0x06002D83 RID: 11651 RVA: 0x0008B658 File Offset: 0x00089858
		[DataMember]
		public int MinDevicePasswordComplexCharacters
		{
			get
			{
				if (this.AlphanumericDevicePasswordRequired)
				{
					return base.ActiveSyncMailboxPolicy.MinDevicePasswordComplexCharacters;
				}
				return 3;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E49 RID: 7753
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0008B660 File Offset: 0x00089860
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x0008B683 File Offset: 0x00089883
		[DataMember]
		public bool IsDevicePasswordExpirationSet
		{
			get
			{
				return !base.ActiveSyncMailboxPolicy.DevicePasswordExpiration.IsUnlimited;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E4A RID: 7754
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x0008B68C File Offset: 0x0008988C
		// (set) Token: 0x06002D87 RID: 11655 RVA: 0x0008B6CA File Offset: 0x000898CA
		[DataMember]
		public string DevicePasswordExpiration
		{
			get
			{
				if (this.IsDevicePasswordExpirationSet)
				{
					return base.ActiveSyncMailboxPolicy.DevicePasswordExpiration.Value.Days.ToString();
				}
				return "90";
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E4B RID: 7755
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x0008B6D1 File Offset: 0x000898D1
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x0008B6DE File Offset: 0x000898DE
		[DataMember]
		public int DevicePasswordHistory
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.DevicePasswordHistory;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E4C RID: 7756
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x0008B6E5 File Offset: 0x000898E5
		// (set) Token: 0x06002D8B RID: 11659 RVA: 0x0008B6FC File Offset: 0x000898FC
		[DataMember]
		public string MaxCalendarAgeFilter
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.MaxCalendarAgeFilter.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E4D RID: 7757
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x0008B703 File Offset: 0x00089903
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x0008B71A File Offset: 0x0008991A
		[DataMember]
		public string MaxEmailAgeFilter
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.MaxEmailAgeFilter.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E4E RID: 7758
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x0008B724 File Offset: 0x00089924
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x0008B747 File Offset: 0x00089947
		[DataMember]
		public bool IsMaxEmailBodyTruncationSizeSet
		{
			get
			{
				return !base.ActiveSyncMailboxPolicy.MaxEmailBodyTruncationSize.IsUnlimited;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E4F RID: 7759
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x0008B750 File Offset: 0x00089950
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x0008B786 File Offset: 0x00089986
		[DataMember]
		public string MaxEmailBodyTruncationSize
		{
			get
			{
				if (this.IsMaxEmailBodyTruncationSizeSet)
				{
					return base.ActiveSyncMailboxPolicy.MaxEmailBodyTruncationSize.Value.ToString();
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E50 RID: 7760
		// (get) Token: 0x06002D92 RID: 11666 RVA: 0x0008B78D File Offset: 0x0008998D
		// (set) Token: 0x06002D93 RID: 11667 RVA: 0x0008B79A File Offset: 0x0008999A
		[DataMember]
		public bool RequireManualSyncWhenRoaming
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.RequireManualSyncWhenRoaming;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E51 RID: 7761
		// (get) Token: 0x06002D94 RID: 11668 RVA: 0x0008B7A1 File Offset: 0x000899A1
		// (set) Token: 0x06002D95 RID: 11669 RVA: 0x0008B7AE File Offset: 0x000899AE
		[DataMember]
		public bool AllowHTMLEmail
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowHTMLEmail;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E52 RID: 7762
		// (get) Token: 0x06002D96 RID: 11670 RVA: 0x0008B7B5 File Offset: 0x000899B5
		// (set) Token: 0x06002D97 RID: 11671 RVA: 0x0008B7C2 File Offset: 0x000899C2
		[DataMember]
		public bool AttachmentsEnabled
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AttachmentsEnabled;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E53 RID: 7763
		// (get) Token: 0x06002D98 RID: 11672 RVA: 0x0008B7CC File Offset: 0x000899CC
		// (set) Token: 0x06002D99 RID: 11673 RVA: 0x0008B7EF File Offset: 0x000899EF
		[DataMember]
		public bool IsMaxAttachmentSizeSet
		{
			get
			{
				return !base.ActiveSyncMailboxPolicy.MaxAttachmentSize.IsUnlimited;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E54 RID: 7764
		// (get) Token: 0x06002D9A RID: 11674 RVA: 0x0008B7F8 File Offset: 0x000899F8
		// (set) Token: 0x06002D9B RID: 11675 RVA: 0x0008B836 File Offset: 0x00089A36
		[DataMember]
		public string MaxAttachmentSize
		{
			get
			{
				if (this.IsMaxAttachmentSizeSet)
				{
					return base.ActiveSyncMailboxPolicy.MaxAttachmentSize.Value.ToKB().ToString();
				}
				return string.Empty;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E55 RID: 7765
		// (get) Token: 0x06002D9C RID: 11676 RVA: 0x0008B83D File Offset: 0x00089A3D
		// (set) Token: 0x06002D9D RID: 11677 RVA: 0x0008B84A File Offset: 0x00089A4A
		[DataMember]
		public bool AllowTextMessaging
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowTextMessaging;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E56 RID: 7766
		// (get) Token: 0x06002D9E RID: 11678 RVA: 0x0008B851 File Offset: 0x00089A51
		// (set) Token: 0x06002D9F RID: 11679 RVA: 0x0008B85E File Offset: 0x00089A5E
		[DataMember]
		public bool AllowStorageCard
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowStorageCard;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E57 RID: 7767
		// (get) Token: 0x06002DA0 RID: 11680 RVA: 0x0008B865 File Offset: 0x00089A65
		// (set) Token: 0x06002DA1 RID: 11681 RVA: 0x0008B872 File Offset: 0x00089A72
		[DataMember]
		public bool AllowCamera
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowCamera;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E58 RID: 7768
		// (get) Token: 0x06002DA2 RID: 11682 RVA: 0x0008B879 File Offset: 0x00089A79
		// (set) Token: 0x06002DA3 RID: 11683 RVA: 0x0008B886 File Offset: 0x00089A86
		[DataMember]
		public bool AllowWiFi
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowWiFi;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E59 RID: 7769
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x0008B88D File Offset: 0x00089A8D
		// (set) Token: 0x06002DA5 RID: 11685 RVA: 0x0008B89A File Offset: 0x00089A9A
		[DataMember]
		public bool AllowIrDA
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowIrDA;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E5A RID: 7770
		// (get) Token: 0x06002DA6 RID: 11686 RVA: 0x0008B8A1 File Offset: 0x00089AA1
		// (set) Token: 0x06002DA7 RID: 11687 RVA: 0x0008B8AE File Offset: 0x00089AAE
		[DataMember]
		public bool AllowInternetSharing
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowInternetSharing;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E5B RID: 7771
		// (get) Token: 0x06002DA8 RID: 11688 RVA: 0x0008B8B5 File Offset: 0x00089AB5
		// (set) Token: 0x06002DA9 RID: 11689 RVA: 0x0008B8C2 File Offset: 0x00089AC2
		[DataMember]
		public bool AllowBrowser
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowBrowser;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E5C RID: 7772
		// (get) Token: 0x06002DAA RID: 11690 RVA: 0x0008B8C9 File Offset: 0x00089AC9
		// (set) Token: 0x06002DAB RID: 11691 RVA: 0x0008B8E0 File Offset: 0x00089AE0
		[DataMember]
		public string AllowBluetooth
		{
			get
			{
				return base.ActiveSyncMailboxPolicy.AllowBluetooth.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400224E RID: 8782
		internal const string DefaultMinDevicePasswordLength = "4";

		// Token: 0x0400224F RID: 8783
		internal const string DefaultMaxDevicePasswordFailedAttempts = "8";

		// Token: 0x04002250 RID: 8784
		internal const string DefaultMaxInactivityTimeDeviceLock = "15";

		// Token: 0x04002251 RID: 8785
		internal const int DefaultMinDevicePasswordComplexCharacters = 3;

		// Token: 0x04002252 RID: 8786
		internal const string DefaultDevicePasswordExpiration = "90";
	}
}
