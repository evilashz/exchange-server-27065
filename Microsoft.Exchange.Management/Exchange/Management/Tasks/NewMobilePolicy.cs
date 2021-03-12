using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000441 RID: 1089
	[Cmdlet("New", "MobileDeviceMailboxPolicy", SupportsShouldProcess = true)]
	public class NewMobilePolicy : NewMailboxPolicyBase<MobileMailboxPolicy>
	{
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x0009873A File Offset: 0x0009693A
		// (set) Token: 0x06002631 RID: 9777 RVA: 0x00098747 File Offset: 0x00096947
		[Parameter]
		public bool AttachmentsEnabled
		{
			internal get
			{
				return this.DataObject.AttachmentsEnabled;
			}
			set
			{
				this.DataObject.AttachmentsEnabled = value;
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x00098755 File Offset: 0x00096955
		// (set) Token: 0x06002633 RID: 9779 RVA: 0x00098762 File Offset: 0x00096962
		[Parameter]
		public bool PasswordEnabled
		{
			internal get
			{
				return this.DataObject.PasswordEnabled;
			}
			set
			{
				this.DataObject.PasswordEnabled = value;
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x00098770 File Offset: 0x00096970
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x0009877D File Offset: 0x0009697D
		[Parameter]
		public bool AlphanumericPasswordRequired
		{
			internal get
			{
				return this.DataObject.AlphanumericPasswordRequired;
			}
			set
			{
				this.DataObject.AlphanumericPasswordRequired = value;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002636 RID: 9782 RVA: 0x0009878B File Offset: 0x0009698B
		// (set) Token: 0x06002637 RID: 9783 RVA: 0x00098798 File Offset: 0x00096998
		[Parameter]
		public bool PasswordRecoveryEnabled
		{
			internal get
			{
				return this.DataObject.PasswordRecoveryEnabled;
			}
			set
			{
				this.DataObject.PasswordRecoveryEnabled = value;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x000987A6 File Offset: 0x000969A6
		// (set) Token: 0x06002639 RID: 9785 RVA: 0x000987B3 File Offset: 0x000969B3
		[Parameter]
		public bool DeviceEncryptionEnabled
		{
			internal get
			{
				return this.DataObject.RequireStorageCardEncryption;
			}
			set
			{
				this.DataObject.RequireStorageCardEncryption = value;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600263A RID: 9786 RVA: 0x000987C1 File Offset: 0x000969C1
		// (set) Token: 0x0600263B RID: 9787 RVA: 0x000987CE File Offset: 0x000969CE
		[Parameter]
		public int? MinPasswordLength
		{
			internal get
			{
				return this.DataObject.MinPasswordLength;
			}
			set
			{
				this.DataObject.MinPasswordLength = value;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600263C RID: 9788 RVA: 0x000987DC File Offset: 0x000969DC
		// (set) Token: 0x0600263D RID: 9789 RVA: 0x000987E9 File Offset: 0x000969E9
		[Parameter]
		public Unlimited<EnhancedTimeSpan> MaxInactivityTimeLock
		{
			internal get
			{
				return this.DataObject.MaxInactivityTimeLock;
			}
			set
			{
				this.DataObject.MaxInactivityTimeLock = value;
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x000987F7 File Offset: 0x000969F7
		// (set) Token: 0x0600263F RID: 9791 RVA: 0x00098804 File Offset: 0x00096A04
		[Parameter]
		public Unlimited<int> MaxPasswordFailedAttempts
		{
			internal get
			{
				return this.DataObject.MaxPasswordFailedAttempts;
			}
			set
			{
				this.DataObject.MaxPasswordFailedAttempts = value;
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002640 RID: 9792 RVA: 0x00098812 File Offset: 0x00096A12
		// (set) Token: 0x06002641 RID: 9793 RVA: 0x0009881F File Offset: 0x00096A1F
		[Parameter]
		public bool AllowNonProvisionableDevices
		{
			internal get
			{
				return this.DataObject.AllowNonProvisionableDevices;
			}
			set
			{
				this.DataObject.AllowNonProvisionableDevices = value;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06002642 RID: 9794 RVA: 0x0009882D File Offset: 0x00096A2D
		// (set) Token: 0x06002643 RID: 9795 RVA: 0x0009883A File Offset: 0x00096A3A
		[Parameter]
		public Unlimited<ByteQuantifiedSize> MaxAttachmentSize
		{
			internal get
			{
				return this.DataObject.MaxAttachmentSize;
			}
			set
			{
				this.DataObject.MaxAttachmentSize = value;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06002644 RID: 9796 RVA: 0x00098848 File Offset: 0x00096A48
		// (set) Token: 0x06002645 RID: 9797 RVA: 0x00098855 File Offset: 0x00096A55
		[Parameter]
		public bool AllowSimplePassword
		{
			internal get
			{
				return this.DataObject.AllowSimplePassword;
			}
			set
			{
				this.DataObject.AllowSimplePassword = value;
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x06002646 RID: 9798 RVA: 0x00098863 File Offset: 0x00096A63
		// (set) Token: 0x06002647 RID: 9799 RVA: 0x00098870 File Offset: 0x00096A70
		[Parameter]
		public Unlimited<EnhancedTimeSpan> PasswordExpiration
		{
			internal get
			{
				return this.DataObject.PasswordExpiration;
			}
			set
			{
				this.DataObject.PasswordExpiration = value;
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x06002648 RID: 9800 RVA: 0x0009887E File Offset: 0x00096A7E
		// (set) Token: 0x06002649 RID: 9801 RVA: 0x0009888B File Offset: 0x00096A8B
		[Parameter]
		public int PasswordHistory
		{
			internal get
			{
				return this.DataObject.PasswordHistory;
			}
			set
			{
				this.DataObject.PasswordHistory = value;
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600264A RID: 9802 RVA: 0x00098899 File Offset: 0x00096A99
		// (set) Token: 0x0600264B RID: 9803 RVA: 0x000988A6 File Offset: 0x00096AA6
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> DevicePolicyRefreshInterval
		{
			internal get
			{
				return this.DataObject.DevicePolicyRefreshInterval;
			}
			set
			{
				this.DataObject.DevicePolicyRefreshInterval = value;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x0600264C RID: 9804 RVA: 0x000988B4 File Offset: 0x00096AB4
		// (set) Token: 0x0600264D RID: 9805 RVA: 0x000988C1 File Offset: 0x00096AC1
		[Parameter]
		public bool WSSAccessEnabled
		{
			internal get
			{
				return this.DataObject.WSSAccessEnabled;
			}
			set
			{
				this.DataObject.WSSAccessEnabled = value;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x0600264E RID: 9806 RVA: 0x000988CF File Offset: 0x00096ACF
		// (set) Token: 0x0600264F RID: 9807 RVA: 0x000988DC File Offset: 0x00096ADC
		[Parameter]
		public bool UNCAccessEnabled
		{
			internal get
			{
				return this.DataObject.UNCAccessEnabled;
			}
			set
			{
				this.DataObject.UNCAccessEnabled = value;
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x000988EA File Offset: 0x00096AEA
		// (set) Token: 0x06002651 RID: 9809 RVA: 0x000988F7 File Offset: 0x00096AF7
		[Parameter]
		public bool IsDefault
		{
			internal get
			{
				return this.DataObject.IsDefault;
			}
			set
			{
				this.DataObject.IsDefault = value;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x00098905 File Offset: 0x00096B05
		// (set) Token: 0x06002653 RID: 9811 RVA: 0x00098912 File Offset: 0x00096B12
		[Parameter(Mandatory = false)]
		public bool AllowStorageCard
		{
			internal get
			{
				return this.DataObject.AllowStorageCard;
			}
			set
			{
				this.DataObject.AllowStorageCard = value;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002654 RID: 9812 RVA: 0x00098920 File Offset: 0x00096B20
		// (set) Token: 0x06002655 RID: 9813 RVA: 0x0009892D File Offset: 0x00096B2D
		[Parameter(Mandatory = false)]
		public bool AllowCamera
		{
			internal get
			{
				return this.DataObject.AllowCamera;
			}
			set
			{
				this.DataObject.AllowCamera = value;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002656 RID: 9814 RVA: 0x0009893B File Offset: 0x00096B3B
		// (set) Token: 0x06002657 RID: 9815 RVA: 0x00098948 File Offset: 0x00096B48
		[Parameter(Mandatory = false)]
		public bool RequireStorageCardEncryption
		{
			internal get
			{
				return this.DataObject.RequireStorageCardEncryption;
			}
			set
			{
				this.DataObject.RequireStorageCardEncryption = value;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002658 RID: 9816 RVA: 0x00098956 File Offset: 0x00096B56
		// (set) Token: 0x06002659 RID: 9817 RVA: 0x00098963 File Offset: 0x00096B63
		[Parameter(Mandatory = false)]
		public bool RequireDeviceEncryption
		{
			internal get
			{
				return this.DataObject.RequireDeviceEncryption;
			}
			set
			{
				this.DataObject.RequireDeviceEncryption = value;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600265A RID: 9818 RVA: 0x00098971 File Offset: 0x00096B71
		// (set) Token: 0x0600265B RID: 9819 RVA: 0x0009897E File Offset: 0x00096B7E
		[Parameter(Mandatory = false)]
		public bool AllowUnsignedApplications
		{
			internal get
			{
				return this.DataObject.AllowUnsignedApplications;
			}
			set
			{
				this.DataObject.AllowUnsignedApplications = value;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x0009898C File Offset: 0x00096B8C
		// (set) Token: 0x0600265D RID: 9821 RVA: 0x00098999 File Offset: 0x00096B99
		[Parameter(Mandatory = false)]
		public bool AllowUnsignedInstallationPackages
		{
			internal get
			{
				return this.DataObject.AllowUnsignedInstallationPackages;
			}
			set
			{
				this.DataObject.AllowUnsignedInstallationPackages = value;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x000989A7 File Offset: 0x00096BA7
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x000989B4 File Offset: 0x00096BB4
		[Parameter(Mandatory = false)]
		public int MinPasswordComplexCharacters
		{
			internal get
			{
				return this.DataObject.MinPasswordComplexCharacters;
			}
			set
			{
				this.DataObject.MinPasswordComplexCharacters = value;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06002660 RID: 9824 RVA: 0x000989C2 File Offset: 0x00096BC2
		// (set) Token: 0x06002661 RID: 9825 RVA: 0x000989CF File Offset: 0x00096BCF
		[Parameter(Mandatory = false)]
		public bool AllowWiFi
		{
			internal get
			{
				return this.DataObject.AllowWiFi;
			}
			set
			{
				this.DataObject.AllowWiFi = value;
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000989DD File Offset: 0x00096BDD
		// (set) Token: 0x06002663 RID: 9827 RVA: 0x000989EA File Offset: 0x00096BEA
		[Parameter(Mandatory = false)]
		public bool AllowTextMessaging
		{
			internal get
			{
				return this.DataObject.AllowTextMessaging;
			}
			set
			{
				this.DataObject.AllowTextMessaging = value;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x000989F8 File Offset: 0x00096BF8
		// (set) Token: 0x06002665 RID: 9829 RVA: 0x00098A05 File Offset: 0x00096C05
		[Parameter(Mandatory = false)]
		public bool AllowPOPIMAPEmail
		{
			internal get
			{
				return this.DataObject.AllowPOPIMAPEmail;
			}
			set
			{
				this.DataObject.AllowPOPIMAPEmail = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002666 RID: 9830 RVA: 0x00098A13 File Offset: 0x00096C13
		// (set) Token: 0x06002667 RID: 9831 RVA: 0x00098A20 File Offset: 0x00096C20
		[Parameter(Mandatory = false)]
		public BluetoothType AllowBluetooth
		{
			internal get
			{
				return this.DataObject.AllowBluetooth;
			}
			set
			{
				this.DataObject.AllowBluetooth = value;
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06002668 RID: 9832 RVA: 0x00098A2E File Offset: 0x00096C2E
		// (set) Token: 0x06002669 RID: 9833 RVA: 0x00098A3B File Offset: 0x00096C3B
		[Parameter(Mandatory = false)]
		public bool AllowIrDA
		{
			internal get
			{
				return this.DataObject.AllowIrDA;
			}
			set
			{
				this.DataObject.AllowIrDA = value;
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x00098A49 File Offset: 0x00096C49
		// (set) Token: 0x0600266B RID: 9835 RVA: 0x00098A56 File Offset: 0x00096C56
		[Parameter(Mandatory = false)]
		public bool RequireManualSyncWhenRoaming
		{
			internal get
			{
				return this.DataObject.RequireManualSyncWhenRoaming;
			}
			set
			{
				this.DataObject.RequireManualSyncWhenRoaming = value;
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x00098A64 File Offset: 0x00096C64
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x00098A71 File Offset: 0x00096C71
		[Parameter(Mandatory = false)]
		public bool AllowDesktopSync
		{
			internal get
			{
				return this.DataObject.AllowDesktopSync;
			}
			set
			{
				this.DataObject.AllowDesktopSync = value;
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600266E RID: 9838 RVA: 0x00098A7F File Offset: 0x00096C7F
		// (set) Token: 0x0600266F RID: 9839 RVA: 0x00098A8C File Offset: 0x00096C8C
		[Parameter(Mandatory = false)]
		public CalendarAgeFilterType MaxCalendarAgeFilter
		{
			internal get
			{
				return this.DataObject.MaxCalendarAgeFilter;
			}
			set
			{
				this.DataObject.MaxCalendarAgeFilter = value;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06002670 RID: 9840 RVA: 0x00098A9A File Offset: 0x00096C9A
		// (set) Token: 0x06002671 RID: 9841 RVA: 0x00098AA7 File Offset: 0x00096CA7
		[Parameter(Mandatory = false)]
		public bool AllowHTMLEmail
		{
			internal get
			{
				return this.DataObject.AllowHTMLEmail;
			}
			set
			{
				this.DataObject.AllowHTMLEmail = value;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06002672 RID: 9842 RVA: 0x00098AB5 File Offset: 0x00096CB5
		// (set) Token: 0x06002673 RID: 9843 RVA: 0x00098AC2 File Offset: 0x00096CC2
		[Parameter(Mandatory = false)]
		public EmailAgeFilterType MaxEmailAgeFilter
		{
			internal get
			{
				return this.DataObject.MaxEmailAgeFilter;
			}
			set
			{
				this.DataObject.MaxEmailAgeFilter = value;
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x00098AD0 File Offset: 0x00096CD0
		// (set) Token: 0x06002675 RID: 9845 RVA: 0x00098ADD File Offset: 0x00096CDD
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxEmailBodyTruncationSize
		{
			internal get
			{
				return this.DataObject.MaxEmailBodyTruncationSize;
			}
			set
			{
				this.DataObject.MaxEmailBodyTruncationSize = value;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x00098AEB File Offset: 0x00096CEB
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x00098AF8 File Offset: 0x00096CF8
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxEmailHTMLBodyTruncationSize
		{
			internal get
			{
				return this.DataObject.MaxEmailHTMLBodyTruncationSize;
			}
			set
			{
				this.DataObject.MaxEmailHTMLBodyTruncationSize = value;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06002678 RID: 9848 RVA: 0x00098B06 File Offset: 0x00096D06
		// (set) Token: 0x06002679 RID: 9849 RVA: 0x00098B13 File Offset: 0x00096D13
		[Parameter(Mandatory = false)]
		public bool RequireSignedSMIMEMessages
		{
			internal get
			{
				return this.DataObject.RequireSignedSMIMEMessages;
			}
			set
			{
				this.DataObject.RequireSignedSMIMEMessages = value;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600267A RID: 9850 RVA: 0x00098B21 File Offset: 0x00096D21
		// (set) Token: 0x0600267B RID: 9851 RVA: 0x00098B2E File Offset: 0x00096D2E
		[Parameter(Mandatory = false)]
		public bool RequireEncryptedSMIMEMessages
		{
			internal get
			{
				return this.DataObject.RequireEncryptedSMIMEMessages;
			}
			set
			{
				this.DataObject.RequireEncryptedSMIMEMessages = value;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x0600267C RID: 9852 RVA: 0x00098B3C File Offset: 0x00096D3C
		// (set) Token: 0x0600267D RID: 9853 RVA: 0x00098B49 File Offset: 0x00096D49
		[Parameter(Mandatory = false)]
		public SignedSMIMEAlgorithmType RequireSignedSMIMEAlgorithm
		{
			internal get
			{
				return this.DataObject.RequireSignedSMIMEAlgorithm;
			}
			set
			{
				this.DataObject.RequireSignedSMIMEAlgorithm = value;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x0600267E RID: 9854 RVA: 0x00098B57 File Offset: 0x00096D57
		// (set) Token: 0x0600267F RID: 9855 RVA: 0x00098B64 File Offset: 0x00096D64
		[Parameter(Mandatory = false)]
		public EncryptionSMIMEAlgorithmType RequireEncryptionSMIMEAlgorithm
		{
			internal get
			{
				return this.DataObject.RequireEncryptionSMIMEAlgorithm;
			}
			set
			{
				this.DataObject.RequireEncryptionSMIMEAlgorithm = value;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x06002680 RID: 9856 RVA: 0x00098B72 File Offset: 0x00096D72
		// (set) Token: 0x06002681 RID: 9857 RVA: 0x00098B7F File Offset: 0x00096D7F
		[Parameter(Mandatory = false)]
		public SMIMEEncryptionAlgorithmNegotiationType AllowSMIMEEncryptionAlgorithmNegotiation
		{
			internal get
			{
				return this.DataObject.AllowSMIMEEncryptionAlgorithmNegotiation;
			}
			set
			{
				this.DataObject.AllowSMIMEEncryptionAlgorithmNegotiation = value;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x06002682 RID: 9858 RVA: 0x00098B8D File Offset: 0x00096D8D
		// (set) Token: 0x06002683 RID: 9859 RVA: 0x00098B9A File Offset: 0x00096D9A
		[Parameter(Mandatory = false)]
		public bool AllowSMIMESoftCerts
		{
			internal get
			{
				return this.DataObject.AllowSMIMESoftCerts;
			}
			set
			{
				this.DataObject.AllowSMIMESoftCerts = value;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06002684 RID: 9860 RVA: 0x00098BA8 File Offset: 0x00096DA8
		// (set) Token: 0x06002685 RID: 9861 RVA: 0x00098BB5 File Offset: 0x00096DB5
		[Parameter(Mandatory = false)]
		public bool AllowBrowser
		{
			internal get
			{
				return this.DataObject.AllowBrowser;
			}
			set
			{
				this.DataObject.AllowBrowser = value;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06002686 RID: 9862 RVA: 0x00098BC3 File Offset: 0x00096DC3
		// (set) Token: 0x06002687 RID: 9863 RVA: 0x00098BD0 File Offset: 0x00096DD0
		[Parameter(Mandatory = false)]
		public bool AllowConsumerEmail
		{
			internal get
			{
				return this.DataObject.AllowConsumerEmail;
			}
			set
			{
				this.DataObject.AllowConsumerEmail = value;
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06002688 RID: 9864 RVA: 0x00098BDE File Offset: 0x00096DDE
		// (set) Token: 0x06002689 RID: 9865 RVA: 0x00098BEB File Offset: 0x00096DEB
		[Parameter(Mandatory = false)]
		public bool AllowRemoteDesktop
		{
			internal get
			{
				return this.DataObject.AllowRemoteDesktop;
			}
			set
			{
				this.DataObject.AllowRemoteDesktop = value;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x00098BF9 File Offset: 0x00096DF9
		// (set) Token: 0x0600268B RID: 9867 RVA: 0x00098C06 File Offset: 0x00096E06
		[Parameter(Mandatory = false)]
		public bool AllowInternetSharing
		{
			internal get
			{
				return this.DataObject.AllowInternetSharing;
			}
			set
			{
				this.DataObject.AllowInternetSharing = value;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x00098C14 File Offset: 0x00096E14
		// (set) Token: 0x0600268D RID: 9869 RVA: 0x00098C21 File Offset: 0x00096E21
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UnapprovedInROMApplicationList
		{
			internal get
			{
				return this.DataObject.UnapprovedInROMApplicationList;
			}
			set
			{
				this.DataObject.UnapprovedInROMApplicationList = value;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x00098C2F File Offset: 0x00096E2F
		// (set) Token: 0x0600268F RID: 9871 RVA: 0x00098C3C File Offset: 0x00096E3C
		[Parameter(Mandatory = false)]
		public ApprovedApplicationCollection ApprovedApplicationList
		{
			internal get
			{
				return this.DataObject.ApprovedApplicationList;
			}
			set
			{
				this.DataObject.ApprovedApplicationList = value;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x00098C4A File Offset: 0x00096E4A
		// (set) Token: 0x06002691 RID: 9873 RVA: 0x00098C57 File Offset: 0x00096E57
		[Parameter(Mandatory = false)]
		public bool AllowExternalDeviceManagement
		{
			internal get
			{
				return this.DataObject.AllowExternalDeviceManagement;
			}
			set
			{
				this.DataObject.AllowExternalDeviceManagement = value;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002692 RID: 9874 RVA: 0x00098C65 File Offset: 0x00096E65
		// (set) Token: 0x06002693 RID: 9875 RVA: 0x00098C72 File Offset: 0x00096E72
		[Parameter(Mandatory = false)]
		public MobileOTAUpdateModeType MobileOTAUpdateMode
		{
			internal get
			{
				return this.DataObject.MobileOTAUpdateMode;
			}
			set
			{
				this.DataObject.MobileOTAUpdateMode = value;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06002694 RID: 9876 RVA: 0x00098C80 File Offset: 0x00096E80
		// (set) Token: 0x06002695 RID: 9877 RVA: 0x00098C8D File Offset: 0x00096E8D
		[Parameter(Mandatory = false)]
		public bool AllowMobileOTAUpdate
		{
			internal get
			{
				return this.DataObject.AllowMobileOTAUpdate;
			}
			set
			{
				this.DataObject.AllowMobileOTAUpdate = value;
			}
		}

		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002696 RID: 9878 RVA: 0x00098C9B File Offset: 0x00096E9B
		// (set) Token: 0x06002697 RID: 9879 RVA: 0x00098CA8 File Offset: 0x00096EA8
		[Parameter(Mandatory = false)]
		public bool IrmEnabled
		{
			internal get
			{
				return this.DataObject.IrmEnabled;
			}
			set
			{
				this.DataObject.IrmEnabled = value;
			}
		}

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002698 RID: 9880 RVA: 0x00098CB6 File Offset: 0x00096EB6
		// (set) Token: 0x06002699 RID: 9881 RVA: 0x00098CC3 File Offset: 0x00096EC3
		[Parameter(Mandatory = false)]
		public bool AllowApplePushNotifications
		{
			internal get
			{
				return this.DataObject.AllowApplePushNotifications;
			}
			set
			{
				this.DataObject.AllowApplePushNotifications = value;
			}
		}

		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x0600269A RID: 9882 RVA: 0x00098CD1 File Offset: 0x00096ED1
		// (set) Token: 0x0600269B RID: 9883 RVA: 0x00098CDE File Offset: 0x00096EDE
		[Parameter(Mandatory = false)]
		public bool AllowMicrosoftPushNotifications
		{
			internal get
			{
				return this.DataObject.AllowMicrosoftPushNotifications;
			}
			set
			{
				this.DataObject.AllowMicrosoftPushNotifications = value;
			}
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x00098CEC File Offset: 0x00096EEC
		// (set) Token: 0x0600269D RID: 9885 RVA: 0x00098CF9 File Offset: 0x00096EF9
		[Parameter(Mandatory = false)]
		public bool AllowGooglePushNotifications
		{
			internal get
			{
				return this.DataObject.AllowGooglePushNotifications;
			}
			set
			{
				this.DataObject.AllowGooglePushNotifications = value;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x00098D07 File Offset: 0x00096F07
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.updateExistingDefaultPolicies)
				{
					return Strings.ConfirmationMessageNewMobileMailboxDefaultPolicy(base.Name.ToString());
				}
				return base.ConfirmationMessage;
			}
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x00098D28 File Offset: 0x00096F28
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.IsDefault)
			{
				this.existingDefaultPolicies = DefaultMobileMailboxPolicyUtility<MobileMailboxPolicy>.GetDefaultPolicies((IConfigurationSession)base.DataSession);
				if (this.existingDefaultPolicies.Count > 0)
				{
					this.updateExistingDefaultPolicies = true;
				}
			}
			if (!DefaultMobileMailboxPolicyUtility<MobileMailboxPolicy>.ValidateLength(this.DataObject.UnapprovedInROMApplicationList, 5120, 2048))
			{
				base.WriteError(new ArgumentException(Strings.MobileDevicePolicyApplicationListTooLong(5120, 2048), "UnapprovedInROMApplicationList"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (!DefaultMobileMailboxPolicyUtility<MobileMailboxPolicy>.ValidateLength(this.DataObject.ApprovedApplicationList, 7168, 2048))
			{
				base.WriteError(new ArgumentException(Strings.MobileDevicePolicyApplicationListTooLong(7168, 2048), "ApprovedApplicationList"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x00098E0C File Offset: 0x0009700C
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			if (this.updateExistingDefaultPolicies)
			{
				try
				{
					DefaultMailboxPolicyUtility<MobileMailboxPolicy>.ClearDefaultPolicies(base.DataSession as IConfigurationSession, this.existingDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
		}
	}
}
