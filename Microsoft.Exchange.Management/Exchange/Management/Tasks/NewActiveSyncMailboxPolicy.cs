using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000431 RID: 1073
	[Cmdlet("New", "ActiveSyncMailboxPolicy", SupportsShouldProcess = true)]
	public class NewActiveSyncMailboxPolicy : NewMailboxPolicyBase<ActiveSyncMailboxPolicy>
	{
		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x06002591 RID: 9617 RVA: 0x00097689 File Offset: 0x00095889
		// (set) Token: 0x06002592 RID: 9618 RVA: 0x00097696 File Offset: 0x00095896
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

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x06002593 RID: 9619 RVA: 0x000976A4 File Offset: 0x000958A4
		// (set) Token: 0x06002594 RID: 9620 RVA: 0x000976B1 File Offset: 0x000958B1
		[Parameter]
		public bool DevicePasswordEnabled
		{
			internal get
			{
				return this.DataObject.DevicePasswordEnabled;
			}
			set
			{
				this.DataObject.DevicePasswordEnabled = value;
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x000976BF File Offset: 0x000958BF
		// (set) Token: 0x06002596 RID: 9622 RVA: 0x000976CC File Offset: 0x000958CC
		[Parameter]
		public bool AlphanumericDevicePasswordRequired
		{
			internal get
			{
				return this.DataObject.AlphanumericDevicePasswordRequired;
			}
			set
			{
				this.DataObject.AlphanumericDevicePasswordRequired = value;
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x000976DA File Offset: 0x000958DA
		// (set) Token: 0x06002598 RID: 9624 RVA: 0x000976E7 File Offset: 0x000958E7
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

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x000976F5 File Offset: 0x000958F5
		// (set) Token: 0x0600259A RID: 9626 RVA: 0x00097702 File Offset: 0x00095902
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

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x00097710 File Offset: 0x00095910
		// (set) Token: 0x0600259C RID: 9628 RVA: 0x0009771D File Offset: 0x0009591D
		[Parameter]
		public int? MinDevicePasswordLength
		{
			internal get
			{
				return this.DataObject.MinDevicePasswordLength;
			}
			set
			{
				this.DataObject.MinDevicePasswordLength = value;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x0009772B File Offset: 0x0009592B
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x00097738 File Offset: 0x00095938
		[Parameter]
		public Unlimited<EnhancedTimeSpan> MaxInactivityTimeDeviceLock
		{
			internal get
			{
				return this.DataObject.MaxInactivityTimeDeviceLock;
			}
			set
			{
				this.DataObject.MaxInactivityTimeDeviceLock = value;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x00097746 File Offset: 0x00095946
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x00097753 File Offset: 0x00095953
		[Parameter]
		public Unlimited<int> MaxDevicePasswordFailedAttempts
		{
			internal get
			{
				return this.DataObject.MaxDevicePasswordFailedAttempts;
			}
			set
			{
				this.DataObject.MaxDevicePasswordFailedAttempts = value;
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x00097761 File Offset: 0x00095961
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x0009776E File Offset: 0x0009596E
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

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0009777C File Offset: 0x0009597C
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x00097789 File Offset: 0x00095989
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

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x00097797 File Offset: 0x00095997
		// (set) Token: 0x060025A6 RID: 9638 RVA: 0x000977A4 File Offset: 0x000959A4
		[Parameter]
		public bool AllowSimpleDevicePassword
		{
			internal get
			{
				return this.DataObject.AllowSimpleDevicePassword;
			}
			set
			{
				this.DataObject.AllowSimpleDevicePassword = value;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000977B2 File Offset: 0x000959B2
		// (set) Token: 0x060025A8 RID: 9640 RVA: 0x000977BF File Offset: 0x000959BF
		[Parameter]
		public Unlimited<EnhancedTimeSpan> DevicePasswordExpiration
		{
			internal get
			{
				return this.DataObject.DevicePasswordExpiration;
			}
			set
			{
				this.DataObject.DevicePasswordExpiration = value;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060025A9 RID: 9641 RVA: 0x000977CD File Offset: 0x000959CD
		// (set) Token: 0x060025AA RID: 9642 RVA: 0x000977DA File Offset: 0x000959DA
		[Parameter]
		public int DevicePasswordHistory
		{
			internal get
			{
				return this.DataObject.DevicePasswordHistory;
			}
			set
			{
				this.DataObject.DevicePasswordHistory = value;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x000977E8 File Offset: 0x000959E8
		// (set) Token: 0x060025AC RID: 9644 RVA: 0x000977F5 File Offset: 0x000959F5
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

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x00097803 File Offset: 0x00095A03
		// (set) Token: 0x060025AE RID: 9646 RVA: 0x00097810 File Offset: 0x00095A10
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

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x0009781E File Offset: 0x00095A1E
		// (set) Token: 0x060025B0 RID: 9648 RVA: 0x0009782B File Offset: 0x00095A2B
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

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x00097839 File Offset: 0x00095A39
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x00097846 File Offset: 0x00095A46
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

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x00097854 File Offset: 0x00095A54
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x00097861 File Offset: 0x00095A61
		[Parameter]
		public bool IsDefaultPolicy
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

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x0009786F File Offset: 0x00095A6F
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x0009787C File Offset: 0x00095A7C
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

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x0009788A File Offset: 0x00095A8A
		// (set) Token: 0x060025B8 RID: 9656 RVA: 0x00097897 File Offset: 0x00095A97
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

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x000978A5 File Offset: 0x00095AA5
		// (set) Token: 0x060025BA RID: 9658 RVA: 0x000978B2 File Offset: 0x00095AB2
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

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x060025BB RID: 9659 RVA: 0x000978C0 File Offset: 0x00095AC0
		// (set) Token: 0x060025BC RID: 9660 RVA: 0x000978CD File Offset: 0x00095ACD
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

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x060025BD RID: 9661 RVA: 0x000978DB File Offset: 0x00095ADB
		// (set) Token: 0x060025BE RID: 9662 RVA: 0x000978E8 File Offset: 0x00095AE8
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

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x000978F6 File Offset: 0x00095AF6
		// (set) Token: 0x060025C0 RID: 9664 RVA: 0x00097903 File Offset: 0x00095B03
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

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x060025C1 RID: 9665 RVA: 0x00097911 File Offset: 0x00095B11
		// (set) Token: 0x060025C2 RID: 9666 RVA: 0x0009791E File Offset: 0x00095B1E
		[Parameter(Mandatory = false)]
		public int MinDevicePasswordComplexCharacters
		{
			internal get
			{
				return this.DataObject.MinDevicePasswordComplexCharacters;
			}
			set
			{
				this.DataObject.MinDevicePasswordComplexCharacters = value;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x0009792C File Offset: 0x00095B2C
		// (set) Token: 0x060025C4 RID: 9668 RVA: 0x00097939 File Offset: 0x00095B39
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

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x00097947 File Offset: 0x00095B47
		// (set) Token: 0x060025C6 RID: 9670 RVA: 0x00097954 File Offset: 0x00095B54
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

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x00097962 File Offset: 0x00095B62
		// (set) Token: 0x060025C8 RID: 9672 RVA: 0x0009796F File Offset: 0x00095B6F
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

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x0009797D File Offset: 0x00095B7D
		// (set) Token: 0x060025CA RID: 9674 RVA: 0x0009798A File Offset: 0x00095B8A
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

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x00097998 File Offset: 0x00095B98
		// (set) Token: 0x060025CC RID: 9676 RVA: 0x000979A5 File Offset: 0x00095BA5
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

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000979B3 File Offset: 0x00095BB3
		// (set) Token: 0x060025CE RID: 9678 RVA: 0x000979C0 File Offset: 0x00095BC0
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

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x000979CE File Offset: 0x00095BCE
		// (set) Token: 0x060025D0 RID: 9680 RVA: 0x000979DB File Offset: 0x00095BDB
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

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x000979E9 File Offset: 0x00095BE9
		// (set) Token: 0x060025D2 RID: 9682 RVA: 0x000979F6 File Offset: 0x00095BF6
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

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00097A04 File Offset: 0x00095C04
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x00097A11 File Offset: 0x00095C11
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

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x00097A1F File Offset: 0x00095C1F
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x00097A2C File Offset: 0x00095C2C
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

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x060025D7 RID: 9687 RVA: 0x00097A3A File Offset: 0x00095C3A
		// (set) Token: 0x060025D8 RID: 9688 RVA: 0x00097A47 File Offset: 0x00095C47
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

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060025D9 RID: 9689 RVA: 0x00097A55 File Offset: 0x00095C55
		// (set) Token: 0x060025DA RID: 9690 RVA: 0x00097A62 File Offset: 0x00095C62
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

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060025DB RID: 9691 RVA: 0x00097A70 File Offset: 0x00095C70
		// (set) Token: 0x060025DC RID: 9692 RVA: 0x00097A7D File Offset: 0x00095C7D
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

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060025DD RID: 9693 RVA: 0x00097A8B File Offset: 0x00095C8B
		// (set) Token: 0x060025DE RID: 9694 RVA: 0x00097A98 File Offset: 0x00095C98
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

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060025DF RID: 9695 RVA: 0x00097AA6 File Offset: 0x00095CA6
		// (set) Token: 0x060025E0 RID: 9696 RVA: 0x00097AB3 File Offset: 0x00095CB3
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

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060025E1 RID: 9697 RVA: 0x00097AC1 File Offset: 0x00095CC1
		// (set) Token: 0x060025E2 RID: 9698 RVA: 0x00097ACE File Offset: 0x00095CCE
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

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x00097ADC File Offset: 0x00095CDC
		// (set) Token: 0x060025E4 RID: 9700 RVA: 0x00097AE9 File Offset: 0x00095CE9
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

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x00097AF7 File Offset: 0x00095CF7
		// (set) Token: 0x060025E6 RID: 9702 RVA: 0x00097B04 File Offset: 0x00095D04
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

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x00097B12 File Offset: 0x00095D12
		// (set) Token: 0x060025E8 RID: 9704 RVA: 0x00097B1F File Offset: 0x00095D1F
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

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x00097B2D File Offset: 0x00095D2D
		// (set) Token: 0x060025EA RID: 9706 RVA: 0x00097B3A File Offset: 0x00095D3A
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

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x060025EB RID: 9707 RVA: 0x00097B48 File Offset: 0x00095D48
		// (set) Token: 0x060025EC RID: 9708 RVA: 0x00097B55 File Offset: 0x00095D55
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

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x00097B63 File Offset: 0x00095D63
		// (set) Token: 0x060025EE RID: 9710 RVA: 0x00097B70 File Offset: 0x00095D70
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

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x00097B7E File Offset: 0x00095D7E
		// (set) Token: 0x060025F0 RID: 9712 RVA: 0x00097B8B File Offset: 0x00095D8B
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

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x00097B99 File Offset: 0x00095D99
		// (set) Token: 0x060025F2 RID: 9714 RVA: 0x00097BA6 File Offset: 0x00095DA6
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

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x060025F3 RID: 9715 RVA: 0x00097BB4 File Offset: 0x00095DB4
		// (set) Token: 0x060025F4 RID: 9716 RVA: 0x00097BC1 File Offset: 0x00095DC1
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

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x00097BCF File Offset: 0x00095DCF
		// (set) Token: 0x060025F6 RID: 9718 RVA: 0x00097BDC File Offset: 0x00095DDC
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

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x060025F7 RID: 9719 RVA: 0x00097BEA File Offset: 0x00095DEA
		// (set) Token: 0x060025F8 RID: 9720 RVA: 0x00097BF7 File Offset: 0x00095DF7
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

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x00097C05 File Offset: 0x00095E05
		// (set) Token: 0x060025FA RID: 9722 RVA: 0x00097C12 File Offset: 0x00095E12
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

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00097C20 File Offset: 0x00095E20
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x00097C2D File Offset: 0x00095E2D
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

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00097C3B File Offset: 0x00095E3B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.updateExistingDefaultPolicies)
				{
					return Strings.ConfirmationMessageNewActiveSyncMailboxDefaultPolicy(base.Name.ToString());
				}
				return base.ConfirmationMessage;
			}
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x00097C5C File Offset: 0x00095E5C
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.DataObject.IsDefault)
			{
				this.existingDefaultPolicies = DefaultMobileMailboxPolicyUtility<ActiveSyncMailboxPolicy>.GetDefaultPolicies((IConfigurationSession)base.DataSession);
				if (this.existingDefaultPolicies.Count > 0)
				{
					this.updateExistingDefaultPolicies = true;
				}
			}
			if (!DefaultMobileMailboxPolicyUtility<ActiveSyncMailboxPolicy>.ValidateLength(this.DataObject.UnapprovedInROMApplicationList, 5120, 2048))
			{
				base.WriteError(new ArgumentException(Strings.ActiveSyncPolicyApplicationListTooLong(5120, 2048), "UnapprovedInROMApplicationList"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (!DefaultMobileMailboxPolicyUtility<ActiveSyncMailboxPolicy>.ValidateLength(this.DataObject.ApprovedApplicationList, 7168, 2048))
			{
				base.WriteError(new ArgumentException(Strings.ActiveSyncPolicyApplicationListTooLong(7168, 2048), "ApprovedApplicationList"), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x00097D40 File Offset: 0x00095F40
		protected override void InternalProcessRecord()
		{
			this.WriteWarning(Strings.WarningCmdletIsDeprecated("New-ActiveSyncMailboxPolicy", "New-MobileDeviceMailboxPolicy"));
			base.InternalProcessRecord();
			if (this.updateExistingDefaultPolicies)
			{
				try
				{
					DefaultMailboxPolicyUtility<ActiveSyncMailboxPolicy>.ClearDefaultPolicies(base.DataSession as IConfigurationSession, this.existingDefaultPolicies);
				}
				catch (DataSourceTransientException exception)
				{
					base.WriteError(exception, ErrorCategory.ReadError, null);
				}
			}
		}
	}
}
