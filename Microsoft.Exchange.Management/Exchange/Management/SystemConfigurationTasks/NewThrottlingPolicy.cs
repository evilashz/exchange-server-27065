using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B22 RID: 2850
	[Cmdlet("New", "ThrottlingPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class NewThrottlingPolicy : NewMultitenancySystemConfigurationObjectTask<ThrottlingPolicy>
	{
		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x06006524 RID: 25892 RVA: 0x001A605C File Offset: 0x001A425C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewThrottlingPolicy(base.Name.ToString(), this.ThrottlingPolicyScope.ToString());
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x06006525 RID: 25893 RVA: 0x001A607E File Offset: 0x001A427E
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x06006526 RID: 25894 RVA: 0x001A6081 File Offset: 0x001A4281
		// (set) Token: 0x06006527 RID: 25895 RVA: 0x001A6089 File Offset: 0x001A4289
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x06006528 RID: 25896 RVA: 0x001A6092 File Offset: 0x001A4292
		// (set) Token: 0x06006529 RID: 25897 RVA: 0x001A609F File Offset: 0x001A429F
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? AnonymousMaxConcurrency
		{
			get
			{
				return this.DataObject.AnonymousMaxConcurrency;
			}
			set
			{
				this.DataObject.AnonymousMaxConcurrency = value;
			}
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x0600652A RID: 25898 RVA: 0x001A60AD File Offset: 0x001A42AD
		// (set) Token: 0x0600652B RID: 25899 RVA: 0x001A60BA File Offset: 0x001A42BA
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? AnonymousMaxBurst
		{
			get
			{
				return this.DataObject.AnonymousMaxBurst;
			}
			set
			{
				this.DataObject.AnonymousMaxBurst = value;
			}
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x001A60C8 File Offset: 0x001A42C8
		// (set) Token: 0x0600652D RID: 25901 RVA: 0x001A60D5 File Offset: 0x001A42D5
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? AnonymousRechargeRate
		{
			get
			{
				return this.DataObject.AnonymousRechargeRate;
			}
			set
			{
				this.DataObject.AnonymousRechargeRate = value;
			}
		}

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x0600652E RID: 25902 RVA: 0x001A60E3 File Offset: 0x001A42E3
		// (set) Token: 0x0600652F RID: 25903 RVA: 0x001A60F0 File Offset: 0x001A42F0
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? AnonymousCutoffBalance
		{
			get
			{
				return this.DataObject.AnonymousCutoffBalance;
			}
			set
			{
				this.DataObject.AnonymousCutoffBalance = value;
			}
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x06006530 RID: 25904 RVA: 0x001A60FE File Offset: 0x001A42FE
		// (set) Token: 0x06006531 RID: 25905 RVA: 0x001A610B File Offset: 0x001A430B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxConcurrency
		{
			get
			{
				return this.DataObject.EasMaxConcurrency;
			}
			set
			{
				this.DataObject.EasMaxConcurrency = value;
			}
		}

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x06006532 RID: 25906 RVA: 0x001A6119 File Offset: 0x001A4319
		// (set) Token: 0x06006533 RID: 25907 RVA: 0x001A6126 File Offset: 0x001A4326
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxBurst
		{
			get
			{
				return this.DataObject.EasMaxBurst;
			}
			set
			{
				this.DataObject.EasMaxBurst = value;
			}
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x06006534 RID: 25908 RVA: 0x001A6134 File Offset: 0x001A4334
		// (set) Token: 0x06006535 RID: 25909 RVA: 0x001A6141 File Offset: 0x001A4341
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasRechargeRate
		{
			get
			{
				return this.DataObject.EasRechargeRate;
			}
			set
			{
				this.DataObject.EasRechargeRate = value;
			}
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x06006536 RID: 25910 RVA: 0x001A614F File Offset: 0x001A434F
		// (set) Token: 0x06006537 RID: 25911 RVA: 0x001A615C File Offset: 0x001A435C
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasCutoffBalance
		{
			get
			{
				return this.DataObject.EasCutoffBalance;
			}
			set
			{
				this.DataObject.EasCutoffBalance = value;
			}
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x06006538 RID: 25912 RVA: 0x001A616A File Offset: 0x001A436A
		// (set) Token: 0x06006539 RID: 25913 RVA: 0x001A6177 File Offset: 0x001A4377
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxDevices
		{
			get
			{
				return this.DataObject.EasMaxDevices;
			}
			set
			{
				this.DataObject.EasMaxDevices = value;
			}
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x0600653A RID: 25914 RVA: 0x001A6185 File Offset: 0x001A4385
		// (set) Token: 0x0600653B RID: 25915 RVA: 0x001A6192 File Offset: 0x001A4392
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxDeviceDeletesPerMonth
		{
			get
			{
				return this.DataObject.EasMaxDeviceDeletesPerMonth;
			}
			set
			{
				this.DataObject.EasMaxDeviceDeletesPerMonth = value;
			}
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x0600653C RID: 25916 RVA: 0x001A61A0 File Offset: 0x001A43A0
		// (set) Token: 0x0600653D RID: 25917 RVA: 0x001A61AD File Offset: 0x001A43AD
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EasMaxInactivityForDeviceCleanup
		{
			get
			{
				return this.DataObject.EasMaxInactivityForDeviceCleanup;
			}
			set
			{
				this.DataObject.EasMaxInactivityForDeviceCleanup = value;
			}
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x0600653E RID: 25918 RVA: 0x001A61BB File Offset: 0x001A43BB
		// (set) Token: 0x0600653F RID: 25919 RVA: 0x001A61C8 File Offset: 0x001A43C8
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsMaxConcurrency
		{
			get
			{
				return this.DataObject.EwsMaxConcurrency;
			}
			set
			{
				this.DataObject.EwsMaxConcurrency = value;
				base.Fields[ThrottlingPolicySchema.EwsMaxConcurrency] = value;
			}
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x06006540 RID: 25920 RVA: 0x001A61EC File Offset: 0x001A43EC
		// (set) Token: 0x06006541 RID: 25921 RVA: 0x001A61F9 File Offset: 0x001A43F9
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsMaxBurst
		{
			get
			{
				return this.DataObject.EwsMaxBurst;
			}
			set
			{
				this.DataObject.EwsMaxBurst = value;
				base.Fields[ThrottlingPolicySchema.EwsMaxBurst] = value;
			}
		}

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x06006542 RID: 25922 RVA: 0x001A621D File Offset: 0x001A441D
		// (set) Token: 0x06006543 RID: 25923 RVA: 0x001A622A File Offset: 0x001A442A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsRechargeRate
		{
			get
			{
				return this.DataObject.EwsRechargeRate;
			}
			set
			{
				this.DataObject.EwsRechargeRate = value;
				base.Fields[ThrottlingPolicySchema.EwsRechargeRate] = value;
			}
		}

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x06006544 RID: 25924 RVA: 0x001A624E File Offset: 0x001A444E
		// (set) Token: 0x06006545 RID: 25925 RVA: 0x001A625B File Offset: 0x001A445B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsCutoffBalance
		{
			get
			{
				return this.DataObject.EwsCutoffBalance;
			}
			set
			{
				this.DataObject.EwsCutoffBalance = value;
				base.Fields[ThrottlingPolicySchema.EwsCutoffBalance] = value;
			}
		}

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x06006546 RID: 25926 RVA: 0x001A627F File Offset: 0x001A447F
		// (set) Token: 0x06006547 RID: 25927 RVA: 0x001A628C File Offset: 0x001A448C
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EwsMaxSubscriptions
		{
			get
			{
				return this.DataObject.EwsMaxSubscriptions;
			}
			set
			{
				this.DataObject.EwsMaxSubscriptions = value;
			}
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x06006548 RID: 25928 RVA: 0x001A629A File Offset: 0x001A449A
		// (set) Token: 0x06006549 RID: 25929 RVA: 0x001A62A7 File Offset: 0x001A44A7
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ImapMaxConcurrency
		{
			get
			{
				return this.DataObject.ImapMaxConcurrency;
			}
			set
			{
				this.DataObject.ImapMaxConcurrency = value;
				base.Fields[ThrottlingPolicySchema.ImapMaxConcurrency] = value;
			}
		}

		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x0600654A RID: 25930 RVA: 0x001A62CB File Offset: 0x001A44CB
		// (set) Token: 0x0600654B RID: 25931 RVA: 0x001A62D8 File Offset: 0x001A44D8
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ImapMaxBurst
		{
			get
			{
				return this.DataObject.ImapMaxBurst;
			}
			set
			{
				this.DataObject.ImapMaxBurst = value;
				base.Fields[ThrottlingPolicySchema.ImapMaxBurst] = value;
			}
		}

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x0600654C RID: 25932 RVA: 0x001A62FC File Offset: 0x001A44FC
		// (set) Token: 0x0600654D RID: 25933 RVA: 0x001A6309 File Offset: 0x001A4509
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ImapRechargeRate
		{
			get
			{
				return this.DataObject.ImapRechargeRate;
			}
			set
			{
				this.DataObject.ImapRechargeRate = value;
				base.Fields[ThrottlingPolicySchema.ImapRechargeRate] = value;
			}
		}

		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x0600654E RID: 25934 RVA: 0x001A632D File Offset: 0x001A452D
		// (set) Token: 0x0600654F RID: 25935 RVA: 0x001A633A File Offset: 0x001A453A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ImapCutoffBalance
		{
			get
			{
				return this.DataObject.ImapCutoffBalance;
			}
			set
			{
				this.DataObject.ImapCutoffBalance = value;
				base.Fields[ThrottlingPolicySchema.ImapCutoffBalance] = value;
			}
		}

		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x06006550 RID: 25936 RVA: 0x001A635E File Offset: 0x001A455E
		// (set) Token: 0x06006551 RID: 25937 RVA: 0x001A636B File Offset: 0x001A456B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxConcurrency
		{
			get
			{
				return this.DataObject.OutlookServiceMaxConcurrency;
			}
			set
			{
				this.DataObject.OutlookServiceMaxConcurrency = value;
				base.Fields[ThrottlingPolicySchema.OutlookServiceMaxConcurrency] = value;
			}
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x06006552 RID: 25938 RVA: 0x001A638F File Offset: 0x001A458F
		// (set) Token: 0x06006553 RID: 25939 RVA: 0x001A639C File Offset: 0x001A459C
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxBurst
		{
			get
			{
				return this.DataObject.OutlookServiceMaxBurst;
			}
			set
			{
				this.DataObject.OutlookServiceMaxBurst = value;
				base.Fields[ThrottlingPolicySchema.OutlookServiceMaxBurst] = value;
			}
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x06006554 RID: 25940 RVA: 0x001A63C0 File Offset: 0x001A45C0
		// (set) Token: 0x06006555 RID: 25941 RVA: 0x001A63CD File Offset: 0x001A45CD
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceRechargeRate
		{
			get
			{
				return this.DataObject.OutlookServiceRechargeRate;
			}
			set
			{
				this.DataObject.OutlookServiceRechargeRate = value;
				base.Fields[ThrottlingPolicySchema.OutlookServiceRechargeRate] = value;
			}
		}

		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x06006556 RID: 25942 RVA: 0x001A63F1 File Offset: 0x001A45F1
		// (set) Token: 0x06006557 RID: 25943 RVA: 0x001A63FE File Offset: 0x001A45FE
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceCutoffBalance
		{
			get
			{
				return this.DataObject.OutlookServiceCutoffBalance;
			}
			set
			{
				this.DataObject.OutlookServiceCutoffBalance = value;
				base.Fields[ThrottlingPolicySchema.OutlookServiceCutoffBalance] = value;
			}
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x06006558 RID: 25944 RVA: 0x001A6422 File Offset: 0x001A4622
		// (set) Token: 0x06006559 RID: 25945 RVA: 0x001A642F File Offset: 0x001A462F
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxSubscriptions
		{
			get
			{
				return this.DataObject.OutlookServiceMaxSubscriptions;
			}
			set
			{
				this.DataObject.OutlookServiceMaxSubscriptions = value;
			}
		}

		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x0600655A RID: 25946 RVA: 0x001A643D File Offset: 0x001A463D
		// (set) Token: 0x0600655B RID: 25947 RVA: 0x001A644A File Offset: 0x001A464A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerDevice
		{
			get
			{
				return this.DataObject.OutlookServiceMaxSocketConnectionsPerDevice;
			}
			set
			{
				this.DataObject.OutlookServiceMaxSocketConnectionsPerDevice = value;
			}
		}

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x0600655C RID: 25948 RVA: 0x001A6458 File Offset: 0x001A4658
		// (set) Token: 0x0600655D RID: 25949 RVA: 0x001A6465 File Offset: 0x001A4665
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerUser
		{
			get
			{
				return this.DataObject.OutlookServiceMaxSocketConnectionsPerUser;
			}
			set
			{
				this.DataObject.OutlookServiceMaxSocketConnectionsPerUser = value;
			}
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x0600655E RID: 25950 RVA: 0x001A6473 File Offset: 0x001A4673
		// (set) Token: 0x0600655F RID: 25951 RVA: 0x001A6480 File Offset: 0x001A4680
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaMaxConcurrency
		{
			get
			{
				return this.DataObject.OwaMaxConcurrency;
			}
			set
			{
				this.DataObject.OwaMaxConcurrency = value;
			}
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x06006560 RID: 25952 RVA: 0x001A648E File Offset: 0x001A468E
		// (set) Token: 0x06006561 RID: 25953 RVA: 0x001A649B File Offset: 0x001A469B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaMaxBurst
		{
			get
			{
				return this.DataObject.OwaMaxBurst;
			}
			set
			{
				this.DataObject.OwaMaxBurst = value;
			}
		}

		// Token: 0x17001EE8 RID: 7912
		// (get) Token: 0x06006562 RID: 25954 RVA: 0x001A64A9 File Offset: 0x001A46A9
		// (set) Token: 0x06006563 RID: 25955 RVA: 0x001A64B6 File Offset: 0x001A46B6
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaRechargeRate
		{
			get
			{
				return this.DataObject.OwaRechargeRate;
			}
			set
			{
				this.DataObject.OwaRechargeRate = value;
			}
		}

		// Token: 0x17001EE9 RID: 7913
		// (get) Token: 0x06006564 RID: 25956 RVA: 0x001A64C4 File Offset: 0x001A46C4
		// (set) Token: 0x06006565 RID: 25957 RVA: 0x001A64D1 File Offset: 0x001A46D1
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaCutoffBalance
		{
			get
			{
				return this.DataObject.OwaCutoffBalance;
			}
			set
			{
				this.DataObject.OwaCutoffBalance = value;
			}
		}

		// Token: 0x17001EEA RID: 7914
		// (get) Token: 0x06006566 RID: 25958 RVA: 0x001A64DF File Offset: 0x001A46DF
		// (set) Token: 0x06006567 RID: 25959 RVA: 0x001A64EC File Offset: 0x001A46EC
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaVoiceMaxConcurrency
		{
			get
			{
				return this.DataObject.OwaVoiceMaxConcurrency;
			}
			set
			{
				this.DataObject.OwaVoiceMaxConcurrency = value;
			}
		}

		// Token: 0x17001EEB RID: 7915
		// (get) Token: 0x06006568 RID: 25960 RVA: 0x001A64FA File Offset: 0x001A46FA
		// (set) Token: 0x06006569 RID: 25961 RVA: 0x001A6507 File Offset: 0x001A4707
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaVoiceMaxBurst
		{
			get
			{
				return this.DataObject.OwaVoiceMaxBurst;
			}
			set
			{
				this.DataObject.OwaVoiceMaxBurst = value;
			}
		}

		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x0600656A RID: 25962 RVA: 0x001A6515 File Offset: 0x001A4715
		// (set) Token: 0x0600656B RID: 25963 RVA: 0x001A6522 File Offset: 0x001A4722
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaVoiceRechargeRate
		{
			get
			{
				return this.DataObject.OwaVoiceRechargeRate;
			}
			set
			{
				this.DataObject.OwaVoiceRechargeRate = value;
			}
		}

		// Token: 0x17001EED RID: 7917
		// (get) Token: 0x0600656C RID: 25964 RVA: 0x001A6530 File Offset: 0x001A4730
		// (set) Token: 0x0600656D RID: 25965 RVA: 0x001A653D File Offset: 0x001A473D
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? OwaVoiceCutoffBalance
		{
			get
			{
				return this.DataObject.OwaVoiceCutoffBalance;
			}
			set
			{
				this.DataObject.OwaVoiceCutoffBalance = value;
			}
		}

		// Token: 0x17001EEE RID: 7918
		// (get) Token: 0x0600656E RID: 25966 RVA: 0x001A654B File Offset: 0x001A474B
		// (set) Token: 0x0600656F RID: 25967 RVA: 0x001A6558 File Offset: 0x001A4758
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionSenderMaxConcurrency
		{
			get
			{
				return this.DataObject.EncryptionSenderMaxConcurrency;
			}
			set
			{
				this.DataObject.EncryptionSenderMaxConcurrency = value;
			}
		}

		// Token: 0x17001EEF RID: 7919
		// (get) Token: 0x06006570 RID: 25968 RVA: 0x001A6566 File Offset: 0x001A4766
		// (set) Token: 0x06006571 RID: 25969 RVA: 0x001A6573 File Offset: 0x001A4773
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionSenderMaxBurst
		{
			get
			{
				return this.DataObject.EncryptionSenderMaxBurst;
			}
			set
			{
				this.DataObject.EncryptionSenderMaxBurst = value;
			}
		}

		// Token: 0x17001EF0 RID: 7920
		// (get) Token: 0x06006572 RID: 25970 RVA: 0x001A6581 File Offset: 0x001A4781
		// (set) Token: 0x06006573 RID: 25971 RVA: 0x001A658E File Offset: 0x001A478E
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionSenderRechargeRate
		{
			get
			{
				return this.DataObject.EncryptionSenderRechargeRate;
			}
			set
			{
				this.DataObject.EncryptionSenderRechargeRate = value;
			}
		}

		// Token: 0x17001EF1 RID: 7921
		// (get) Token: 0x06006574 RID: 25972 RVA: 0x001A659C File Offset: 0x001A479C
		// (set) Token: 0x06006575 RID: 25973 RVA: 0x001A65A9 File Offset: 0x001A47A9
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionSenderCutoffBalance
		{
			get
			{
				return this.DataObject.EncryptionSenderCutoffBalance;
			}
			set
			{
				this.DataObject.EncryptionSenderCutoffBalance = value;
			}
		}

		// Token: 0x17001EF2 RID: 7922
		// (get) Token: 0x06006576 RID: 25974 RVA: 0x001A65B7 File Offset: 0x001A47B7
		// (set) Token: 0x06006577 RID: 25975 RVA: 0x001A65C4 File Offset: 0x001A47C4
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionRecipientMaxConcurrency
		{
			get
			{
				return this.DataObject.EncryptionRecipientMaxConcurrency;
			}
			set
			{
				this.DataObject.EncryptionRecipientMaxConcurrency = value;
			}
		}

		// Token: 0x17001EF3 RID: 7923
		// (get) Token: 0x06006578 RID: 25976 RVA: 0x001A65D2 File Offset: 0x001A47D2
		// (set) Token: 0x06006579 RID: 25977 RVA: 0x001A65DF File Offset: 0x001A47DF
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionRecipientMaxBurst
		{
			get
			{
				return this.DataObject.EncryptionRecipientMaxBurst;
			}
			set
			{
				this.DataObject.EncryptionRecipientMaxBurst = value;
			}
		}

		// Token: 0x17001EF4 RID: 7924
		// (get) Token: 0x0600657A RID: 25978 RVA: 0x001A65ED File Offset: 0x001A47ED
		// (set) Token: 0x0600657B RID: 25979 RVA: 0x001A65FA File Offset: 0x001A47FA
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionRecipientRechargeRate
		{
			get
			{
				return this.DataObject.EncryptionRecipientRechargeRate;
			}
			set
			{
				this.DataObject.EncryptionRecipientRechargeRate = value;
			}
		}

		// Token: 0x17001EF5 RID: 7925
		// (get) Token: 0x0600657C RID: 25980 RVA: 0x001A6608 File Offset: 0x001A4808
		// (set) Token: 0x0600657D RID: 25981 RVA: 0x001A6615 File Offset: 0x001A4815
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? EncryptionRecipientCutoffBalance
		{
			get
			{
				return this.DataObject.EncryptionRecipientCutoffBalance;
			}
			set
			{
				this.DataObject.EncryptionRecipientCutoffBalance = value;
			}
		}

		// Token: 0x17001EF6 RID: 7926
		// (get) Token: 0x0600657E RID: 25982 RVA: 0x001A6623 File Offset: 0x001A4823
		// (set) Token: 0x0600657F RID: 25983 RVA: 0x001A6630 File Offset: 0x001A4830
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PopMaxConcurrency
		{
			get
			{
				return this.DataObject.PopMaxConcurrency;
			}
			set
			{
				this.DataObject.PopMaxConcurrency = value;
			}
		}

		// Token: 0x17001EF7 RID: 7927
		// (get) Token: 0x06006580 RID: 25984 RVA: 0x001A663E File Offset: 0x001A483E
		// (set) Token: 0x06006581 RID: 25985 RVA: 0x001A664B File Offset: 0x001A484B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PopMaxBurst
		{
			get
			{
				return this.DataObject.PopMaxBurst;
			}
			set
			{
				this.DataObject.PopMaxBurst = value;
			}
		}

		// Token: 0x17001EF8 RID: 7928
		// (get) Token: 0x06006582 RID: 25986 RVA: 0x001A6659 File Offset: 0x001A4859
		// (set) Token: 0x06006583 RID: 25987 RVA: 0x001A6666 File Offset: 0x001A4866
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PopRechargeRate
		{
			get
			{
				return this.DataObject.PopRechargeRate;
			}
			set
			{
				this.DataObject.PopRechargeRate = value;
			}
		}

		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x06006584 RID: 25988 RVA: 0x001A6674 File Offset: 0x001A4874
		// (set) Token: 0x06006585 RID: 25989 RVA: 0x001A6681 File Offset: 0x001A4881
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PopCutoffBalance
		{
			get
			{
				return this.DataObject.PopCutoffBalance;
			}
			set
			{
				this.DataObject.PopCutoffBalance = value;
			}
		}

		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x06006586 RID: 25990 RVA: 0x001A668F File Offset: 0x001A488F
		// (set) Token: 0x06006587 RID: 25991 RVA: 0x001A669C File Offset: 0x001A489C
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxConcurrency
		{
			get
			{
				return this.DataObject.PowerShellMaxConcurrency;
			}
			set
			{
				this.DataObject.PowerShellMaxConcurrency = value;
			}
		}

		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x06006588 RID: 25992 RVA: 0x001A66AA File Offset: 0x001A48AA
		// (set) Token: 0x06006589 RID: 25993 RVA: 0x001A66B7 File Offset: 0x001A48B7
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxBurst
		{
			get
			{
				return this.DataObject.PowerShellMaxBurst;
			}
			set
			{
				this.DataObject.PowerShellMaxBurst = value;
			}
		}

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x0600658A RID: 25994 RVA: 0x001A66C5 File Offset: 0x001A48C5
		// (set) Token: 0x0600658B RID: 25995 RVA: 0x001A66D2 File Offset: 0x001A48D2
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellRechargeRate
		{
			get
			{
				return this.DataObject.PowerShellRechargeRate;
			}
			set
			{
				this.DataObject.PowerShellRechargeRate = value;
			}
		}

		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x0600658C RID: 25996 RVA: 0x001A66E0 File Offset: 0x001A48E0
		// (set) Token: 0x0600658D RID: 25997 RVA: 0x001A66ED File Offset: 0x001A48ED
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellCutoffBalance
		{
			get
			{
				return this.DataObject.PowerShellCutoffBalance;
			}
			set
			{
				this.DataObject.PowerShellCutoffBalance = value;
			}
		}

		// Token: 0x17001EFE RID: 7934
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x001A66FB File Offset: 0x001A48FB
		// (set) Token: 0x0600658F RID: 25999 RVA: 0x001A6708 File Offset: 0x001A4908
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxTenantConcurrency
		{
			get
			{
				return this.DataObject.PowerShellMaxTenantConcurrency;
			}
			set
			{
				this.DataObject.PowerShellMaxTenantConcurrency = value;
				base.Fields[ThrottlingPolicySchema.PowerShellMaxTenantConcurrency] = value;
			}
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x06006590 RID: 26000 RVA: 0x001A672C File Offset: 0x001A492C
		// (set) Token: 0x06006591 RID: 26001 RVA: 0x001A6739 File Offset: 0x001A4939
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxOperations
		{
			get
			{
				return this.DataObject.PowerShellMaxOperations;
			}
			set
			{
				this.DataObject.PowerShellMaxOperations = value;
			}
		}

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x06006592 RID: 26002 RVA: 0x001A6747 File Offset: 0x001A4947
		// (set) Token: 0x06006593 RID: 26003 RVA: 0x001A6754 File Offset: 0x001A4954
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ExchangeMaxCmdlets
		{
			get
			{
				return this.DataObject.ExchangeMaxCmdlets;
			}
			set
			{
				this.DataObject.ExchangeMaxCmdlets = value;
			}
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x001A6762 File Offset: 0x001A4962
		// (set) Token: 0x06006595 RID: 26005 RVA: 0x001A676F File Offset: 0x001A496F
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxCmdletsTimePeriod
		{
			get
			{
				return this.DataObject.PowerShellMaxCmdletsTimePeriod;
			}
			set
			{
				this.DataObject.PowerShellMaxCmdletsTimePeriod = value;
			}
		}

		// Token: 0x17001F02 RID: 7938
		// (get) Token: 0x06006596 RID: 26006 RVA: 0x001A677D File Offset: 0x001A497D
		// (set) Token: 0x06006597 RID: 26007 RVA: 0x001A678A File Offset: 0x001A498A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxCmdletQueueDepth
		{
			get
			{
				return this.DataObject.PowerShellMaxCmdletQueueDepth;
			}
			set
			{
				this.DataObject.PowerShellMaxCmdletQueueDepth = value;
			}
		}

		// Token: 0x17001F03 RID: 7939
		// (get) Token: 0x06006598 RID: 26008 RVA: 0x001A6798 File Offset: 0x001A4998
		// (set) Token: 0x06006599 RID: 26009 RVA: 0x001A67A5 File Offset: 0x001A49A5
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxDestructiveCmdlets
		{
			get
			{
				return this.DataObject.PowerShellMaxDestructiveCmdlets;
			}
			set
			{
				this.DataObject.PowerShellMaxDestructiveCmdlets = value;
			}
		}

		// Token: 0x17001F04 RID: 7940
		// (get) Token: 0x0600659A RID: 26010 RVA: 0x001A67B3 File Offset: 0x001A49B3
		// (set) Token: 0x0600659B RID: 26011 RVA: 0x001A67C0 File Offset: 0x001A49C0
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxDestructiveCmdletsTimePeriod
		{
			get
			{
				return this.DataObject.PowerShellMaxDestructiveCmdletsTimePeriod;
			}
			set
			{
				this.DataObject.PowerShellMaxDestructiveCmdletsTimePeriod = value;
			}
		}

		// Token: 0x17001F05 RID: 7941
		// (get) Token: 0x0600659C RID: 26012 RVA: 0x001A67CE File Offset: 0x001A49CE
		// (set) Token: 0x0600659D RID: 26013 RVA: 0x001A67DB File Offset: 0x001A49DB
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxCmdlets
		{
			get
			{
				return this.DataObject.PowerShellMaxCmdlets;
			}
			set
			{
				this.DataObject.PowerShellMaxCmdlets = value;
			}
		}

		// Token: 0x17001F06 RID: 7942
		// (get) Token: 0x0600659E RID: 26014 RVA: 0x001A67E9 File Offset: 0x001A49E9
		// (set) Token: 0x0600659F RID: 26015 RVA: 0x001A67F6 File Offset: 0x001A49F6
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxRunspaces
		{
			get
			{
				return this.DataObject.PowerShellMaxRunspaces;
			}
			set
			{
				this.DataObject.PowerShellMaxRunspaces = value;
			}
		}

		// Token: 0x17001F07 RID: 7943
		// (get) Token: 0x060065A0 RID: 26016 RVA: 0x001A6804 File Offset: 0x001A4A04
		// (set) Token: 0x060065A1 RID: 26017 RVA: 0x001A6811 File Offset: 0x001A4A11
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxTenantRunspaces
		{
			get
			{
				return this.DataObject.PowerShellMaxTenantRunspaces;
			}
			set
			{
				this.DataObject.PowerShellMaxTenantRunspaces = value;
				base.Fields[ThrottlingPolicySchema.PowerShellMaxTenantRunspaces] = value;
			}
		}

		// Token: 0x17001F08 RID: 7944
		// (get) Token: 0x060065A2 RID: 26018 RVA: 0x001A6835 File Offset: 0x001A4A35
		// (set) Token: 0x060065A3 RID: 26019 RVA: 0x001A6842 File Offset: 0x001A4A42
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PowerShellMaxRunspacesTimePeriod
		{
			get
			{
				return this.DataObject.PowerShellMaxRunspacesTimePeriod;
			}
			set
			{
				this.DataObject.PowerShellMaxRunspacesTimePeriod = value;
			}
		}

		// Token: 0x17001F09 RID: 7945
		// (get) Token: 0x060065A4 RID: 26020 RVA: 0x001A6850 File Offset: 0x001A4A50
		// (set) Token: 0x060065A5 RID: 26021 RVA: 0x001A685D File Offset: 0x001A4A5D
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PswsMaxConcurrency
		{
			get
			{
				return this.DataObject.PswsMaxConcurrency;
			}
			set
			{
				this.DataObject.PswsMaxConcurrency = value;
			}
		}

		// Token: 0x17001F0A RID: 7946
		// (get) Token: 0x060065A6 RID: 26022 RVA: 0x001A686B File Offset: 0x001A4A6B
		// (set) Token: 0x060065A7 RID: 26023 RVA: 0x001A6878 File Offset: 0x001A4A78
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PswsMaxRequest
		{
			get
			{
				return this.DataObject.PswsMaxRequest;
			}
			set
			{
				this.DataObject.PswsMaxRequest = value;
			}
		}

		// Token: 0x17001F0B RID: 7947
		// (get) Token: 0x060065A8 RID: 26024 RVA: 0x001A6886 File Offset: 0x001A4A86
		// (set) Token: 0x060065A9 RID: 26025 RVA: 0x001A6893 File Offset: 0x001A4A93
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PswsMaxRequestTimePeriod
		{
			get
			{
				return this.DataObject.PswsMaxRequestTimePeriod;
			}
			set
			{
				this.DataObject.PswsMaxRequestTimePeriod = value;
			}
		}

		// Token: 0x17001F0C RID: 7948
		// (get) Token: 0x060065AA RID: 26026 RVA: 0x001A68A1 File Offset: 0x001A4AA1
		// (set) Token: 0x060065AB RID: 26027 RVA: 0x001A68AE File Offset: 0x001A4AAE
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RcaMaxConcurrency
		{
			get
			{
				return this.DataObject.RcaMaxConcurrency;
			}
			set
			{
				this.DataObject.RcaMaxConcurrency = value;
				base.Fields[ThrottlingPolicySchema.RcaMaxConcurrency] = value;
			}
		}

		// Token: 0x17001F0D RID: 7949
		// (get) Token: 0x060065AC RID: 26028 RVA: 0x001A68D2 File Offset: 0x001A4AD2
		// (set) Token: 0x060065AD RID: 26029 RVA: 0x001A68DF File Offset: 0x001A4ADF
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RcaMaxBurst
		{
			get
			{
				return this.DataObject.RcaMaxBurst;
			}
			set
			{
				this.DataObject.RcaMaxBurst = value;
				base.Fields[ThrottlingPolicySchema.RcaMaxBurst] = value;
			}
		}

		// Token: 0x17001F0E RID: 7950
		// (get) Token: 0x060065AE RID: 26030 RVA: 0x001A6903 File Offset: 0x001A4B03
		// (set) Token: 0x060065AF RID: 26031 RVA: 0x001A6910 File Offset: 0x001A4B10
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RcaRechargeRate
		{
			get
			{
				return this.DataObject.RcaRechargeRate;
			}
			set
			{
				this.DataObject.RcaRechargeRate = value;
				base.Fields[ThrottlingPolicySchema.RcaRechargeRate] = value;
			}
		}

		// Token: 0x17001F0F RID: 7951
		// (get) Token: 0x060065B0 RID: 26032 RVA: 0x001A6934 File Offset: 0x001A4B34
		// (set) Token: 0x060065B1 RID: 26033 RVA: 0x001A6941 File Offset: 0x001A4B41
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RcaCutoffBalance
		{
			get
			{
				return this.DataObject.RcaCutoffBalance;
			}
			set
			{
				this.DataObject.RcaCutoffBalance = value;
				base.Fields[ThrottlingPolicySchema.RcaCutoffBalance] = value;
			}
		}

		// Token: 0x17001F10 RID: 7952
		// (get) Token: 0x060065B2 RID: 26034 RVA: 0x001A6965 File Offset: 0x001A4B65
		// (set) Token: 0x060065B3 RID: 26035 RVA: 0x001A6972 File Offset: 0x001A4B72
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? CpaMaxConcurrency
		{
			get
			{
				return this.DataObject.CpaMaxConcurrency;
			}
			set
			{
				this.DataObject.CpaMaxConcurrency = value;
			}
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x060065B4 RID: 26036 RVA: 0x001A6980 File Offset: 0x001A4B80
		// (set) Token: 0x060065B5 RID: 26037 RVA: 0x001A698D File Offset: 0x001A4B8D
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? CpaMaxBurst
		{
			get
			{
				return this.DataObject.CpaMaxBurst;
			}
			set
			{
				this.DataObject.CpaMaxBurst = value;
			}
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x060065B6 RID: 26038 RVA: 0x001A699B File Offset: 0x001A4B9B
		// (set) Token: 0x060065B7 RID: 26039 RVA: 0x001A69A8 File Offset: 0x001A4BA8
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? CpaRechargeRate
		{
			get
			{
				return this.DataObject.CpaRechargeRate;
			}
			set
			{
				this.DataObject.CpaRechargeRate = value;
			}
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x060065B8 RID: 26040 RVA: 0x001A69B6 File Offset: 0x001A4BB6
		// (set) Token: 0x060065B9 RID: 26041 RVA: 0x001A69C3 File Offset: 0x001A4BC3
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? CpaCutoffBalance
		{
			get
			{
				return this.DataObject.CpaCutoffBalance;
			}
			set
			{
				this.DataObject.CpaCutoffBalance = value;
			}
		}

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x060065BA RID: 26042 RVA: 0x001A69D1 File Offset: 0x001A4BD1
		// (set) Token: 0x060065BB RID: 26043 RVA: 0x001A69DE File Offset: 0x001A4BDE
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? MessageRateLimit
		{
			get
			{
				return this.DataObject.MessageRateLimit;
			}
			set
			{
				this.DataObject.MessageRateLimit = value;
			}
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x060065BC RID: 26044 RVA: 0x001A69EC File Offset: 0x001A4BEC
		// (set) Token: 0x060065BD RID: 26045 RVA: 0x001A69F9 File Offset: 0x001A4BF9
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? RecipientRateLimit
		{
			get
			{
				return this.DataObject.RecipientRateLimit;
			}
			set
			{
				this.DataObject.RecipientRateLimit = value;
			}
		}

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x060065BE RID: 26046 RVA: 0x001A6A07 File Offset: 0x001A4C07
		// (set) Token: 0x060065BF RID: 26047 RVA: 0x001A6A14 File Offset: 0x001A4C14
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ForwardeeLimit
		{
			get
			{
				return this.DataObject.ForwardeeLimit;
			}
			set
			{
				this.DataObject.ForwardeeLimit = value;
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x060065C0 RID: 26048 RVA: 0x001A6A22 File Offset: 0x001A4C22
		// (set) Token: 0x060065C1 RID: 26049 RVA: 0x001A6A2F File Offset: 0x001A4C2F
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxConcurrency
		{
			get
			{
				return this.DataObject.DiscoveryMaxConcurrency;
			}
			set
			{
				this.DataObject.DiscoveryMaxConcurrency = value;
			}
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x060065C2 RID: 26050 RVA: 0x001A6A3D File Offset: 0x001A4C3D
		// (set) Token: 0x060065C3 RID: 26051 RVA: 0x001A6A4A File Offset: 0x001A4C4A
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxMailboxes
		{
			get
			{
				return this.DataObject.DiscoveryMaxMailboxes;
			}
			set
			{
				this.DataObject.DiscoveryMaxMailboxes = value;
			}
		}

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x060065C4 RID: 26052 RVA: 0x001A6A58 File Offset: 0x001A4C58
		// (set) Token: 0x060065C5 RID: 26053 RVA: 0x001A6A65 File Offset: 0x001A4C65
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxKeywords
		{
			get
			{
				return this.DataObject.DiscoveryMaxKeywords;
			}
			set
			{
				this.DataObject.DiscoveryMaxKeywords = value;
			}
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x060065C6 RID: 26054 RVA: 0x001A6A73 File Offset: 0x001A4C73
		// (set) Token: 0x060065C7 RID: 26055 RVA: 0x001A6A80 File Offset: 0x001A4C80
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxPreviewSearchMailboxes
		{
			get
			{
				return this.DataObject.DiscoveryMaxPreviewSearchMailboxes;
			}
			set
			{
				this.DataObject.DiscoveryMaxPreviewSearchMailboxes = value;
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x060065C8 RID: 26056 RVA: 0x001A6A8E File Offset: 0x001A4C8E
		// (set) Token: 0x060065C9 RID: 26057 RVA: 0x001A6A9B File Offset: 0x001A4C9B
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxStatsSearchMailboxes
		{
			get
			{
				return this.DataObject.DiscoveryMaxStatsSearchMailboxes;
			}
			set
			{
				this.DataObject.DiscoveryMaxStatsSearchMailboxes = value;
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x060065CA RID: 26058 RVA: 0x001A6AA9 File Offset: 0x001A4CA9
		// (set) Token: 0x060065CB RID: 26059 RVA: 0x001A6AB6 File Offset: 0x001A4CB6
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryPreviewSearchResultsPageSize
		{
			get
			{
				return this.DataObject.DiscoveryPreviewSearchResultsPageSize;
			}
			set
			{
				this.DataObject.DiscoveryPreviewSearchResultsPageSize = value;
			}
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x060065CC RID: 26060 RVA: 0x001A6AC4 File Offset: 0x001A4CC4
		// (set) Token: 0x060065CD RID: 26061 RVA: 0x001A6AD1 File Offset: 0x001A4CD1
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxKeywordsPerPage
		{
			get
			{
				return this.DataObject.DiscoveryMaxKeywordsPerPage;
			}
			set
			{
				this.DataObject.DiscoveryMaxKeywordsPerPage = value;
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x060065CE RID: 26062 RVA: 0x001A6ADF File Offset: 0x001A4CDF
		// (set) Token: 0x060065CF RID: 26063 RVA: 0x001A6AEC File Offset: 0x001A4CEC
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxRefinerResults
		{
			get
			{
				return this.DataObject.DiscoveryMaxRefinerResults;
			}
			set
			{
				this.DataObject.DiscoveryMaxRefinerResults = value;
			}
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x060065D0 RID: 26064 RVA: 0x001A6AFA File Offset: 0x001A4CFA
		// (set) Token: 0x060065D1 RID: 26065 RVA: 0x001A6B07 File Offset: 0x001A4D07
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoveryMaxSearchQueueDepth
		{
			get
			{
				return this.DataObject.DiscoveryMaxSearchQueueDepth;
			}
			set
			{
				this.DataObject.DiscoveryMaxSearchQueueDepth = value;
			}
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x060065D2 RID: 26066 RVA: 0x001A6B15 File Offset: 0x001A4D15
		// (set) Token: 0x060065D3 RID: 26067 RVA: 0x001A6B22 File Offset: 0x001A4D22
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? DiscoverySearchTimeoutPeriod
		{
			get
			{
				return this.DataObject.DiscoverySearchTimeoutPeriod;
			}
			set
			{
				this.DataObject.DiscoverySearchTimeoutPeriod = value;
			}
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x060065D4 RID: 26068 RVA: 0x001A6B30 File Offset: 0x001A4D30
		// (set) Token: 0x060065D5 RID: 26069 RVA: 0x001A6B3D File Offset: 0x001A4D3D
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ComplianceMaxExpansionDGRecipients
		{
			get
			{
				return this.DataObject.ComplianceMaxExpansionDGRecipients;
			}
			set
			{
				this.DataObject.ComplianceMaxExpansionDGRecipients = value;
			}
		}

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x060065D6 RID: 26070 RVA: 0x001A6B4B File Offset: 0x001A4D4B
		// (set) Token: 0x060065D7 RID: 26071 RVA: 0x001A6B58 File Offset: 0x001A4D58
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? ComplianceMaxExpansionNestedDGs
		{
			get
			{
				return this.DataObject.ComplianceMaxExpansionNestedDGs;
			}
			set
			{
				this.DataObject.ComplianceMaxExpansionNestedDGs = value;
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x060065D8 RID: 26072 RVA: 0x001A6B66 File Offset: 0x001A4D66
		// (set) Token: 0x060065D9 RID: 26073 RVA: 0x001A6B73 File Offset: 0x001A4D73
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationMaxConcurrency
		{
			get
			{
				return this.DataObject.PushNotificationMaxConcurrency;
			}
			set
			{
				this.DataObject.PushNotificationMaxConcurrency = value;
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x060065DA RID: 26074 RVA: 0x001A6B81 File Offset: 0x001A4D81
		// (set) Token: 0x060065DB RID: 26075 RVA: 0x001A6B8E File Offset: 0x001A4D8E
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationMaxBurst
		{
			get
			{
				return this.DataObject.PushNotificationMaxBurst;
			}
			set
			{
				this.DataObject.PushNotificationMaxBurst = value;
			}
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x060065DC RID: 26076 RVA: 0x001A6B9C File Offset: 0x001A4D9C
		// (set) Token: 0x060065DD RID: 26077 RVA: 0x001A6BA9 File Offset: 0x001A4DA9
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationRechargeRate
		{
			get
			{
				return this.DataObject.PushNotificationRechargeRate;
			}
			set
			{
				this.DataObject.PushNotificationRechargeRate = value;
			}
		}

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x060065DE RID: 26078 RVA: 0x001A6BB7 File Offset: 0x001A4DB7
		// (set) Token: 0x060065DF RID: 26079 RVA: 0x001A6BC4 File Offset: 0x001A4DC4
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationCutoffBalance
		{
			get
			{
				return this.DataObject.PushNotificationCutoffBalance;
			}
			set
			{
				this.DataObject.PushNotificationCutoffBalance = value;
			}
		}

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x060065E0 RID: 26080 RVA: 0x001A6BD2 File Offset: 0x001A4DD2
		// (set) Token: 0x060065E1 RID: 26081 RVA: 0x001A6BDF File Offset: 0x001A4DDF
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationMaxBurstPerDevice
		{
			get
			{
				return this.DataObject.PushNotificationMaxBurstPerDevice;
			}
			set
			{
				this.DataObject.PushNotificationMaxBurstPerDevice = value;
			}
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x060065E2 RID: 26082 RVA: 0x001A6BED File Offset: 0x001A4DED
		// (set) Token: 0x060065E3 RID: 26083 RVA: 0x001A6BFA File Offset: 0x001A4DFA
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationRechargeRatePerDevice
		{
			get
			{
				return this.DataObject.PushNotificationRechargeRatePerDevice;
			}
			set
			{
				this.DataObject.PushNotificationRechargeRatePerDevice = value;
			}
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x060065E4 RID: 26084 RVA: 0x001A6C08 File Offset: 0x001A4E08
		// (set) Token: 0x060065E5 RID: 26085 RVA: 0x001A6C15 File Offset: 0x001A4E15
		[Parameter(Mandatory = false)]
		public Unlimited<uint>? PushNotificationSamplingPeriodPerDevice
		{
			get
			{
				return this.DataObject.PushNotificationSamplingPeriodPerDevice;
			}
			set
			{
				this.DataObject.PushNotificationSamplingPeriodPerDevice = value;
			}
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x060065E6 RID: 26086 RVA: 0x001A6C23 File Offset: 0x001A4E23
		// (set) Token: 0x060065E7 RID: 26087 RVA: 0x001A6C35 File Offset: 0x001A4E35
		[Parameter(Mandatory = false)]
		public SwitchParameter IsServiceAccount
		{
			get
			{
				return this.DataObject.IsServiceAccount;
			}
			set
			{
				this.DataObject.IsServiceAccount = value.ToBool();
			}
		}

		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x060065E8 RID: 26088 RVA: 0x001A6C49 File Offset: 0x001A4E49
		// (set) Token: 0x060065E9 RID: 26089 RVA: 0x001A6C56 File Offset: 0x001A4E56
		[Parameter(Mandatory = false)]
		public ThrottlingPolicyScopeType ThrottlingPolicyScope
		{
			get
			{
				return this.DataObject.ThrottlingPolicyScope;
			}
			set
			{
				base.VerifyValues<ThrottlingPolicyScopeType>(NewThrottlingPolicy.AllowedThrottlingPolicyScopeTypes, value);
				this.DataObject.ThrottlingPolicyScope = value;
			}
		}

		// Token: 0x060065EA RID: 26090 RVA: 0x001A6C70 File Offset: 0x001A4E70
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ThrottlingPolicy throttlingPolicy = (ThrottlingPolicy)base.PrepareDataObject();
			IConfigurationSession session = base.DataSession as IConfigurationSession;
			throttlingPolicy.SetId(session, new ADObjectId("CN=Global Settings"), base.Name);
			if (this.IsServiceAccount)
			{
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.EwsMaxConcurrency, ThrottlingPolicyDefaults.ServiceAccountEwsMaxConcurrency);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.EwsMaxBurst, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.EwsRechargeRate, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.EwsCutoffBalance, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.ImapMaxConcurrency, ThrottlingPolicyDefaults.ServiceAccountImapMaxConcurrency);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.ImapMaxBurst, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.ImapRechargeRate, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.ImapCutoffBalance, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.OutlookServiceMaxConcurrency, ThrottlingPolicyDefaults.ServiceAccountOutlookServiceMaxConcurrency);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.OutlookServiceMaxBurst, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.OutlookServiceRechargeRate, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.OutlookServiceCutoffBalance, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.RcaMaxConcurrency, ThrottlingPolicyDefaults.ServiceAccountRcaMaxConcurrency);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.RcaMaxBurst, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.RcaRechargeRate, Unlimited<uint>.UnlimitedValue);
				this.StampServiceAccountValue(throttlingPolicy, ThrottlingPolicySchema.RcaCutoffBalance, Unlimited<uint>.UnlimitedValue);
			}
			TaskLogger.LogExit();
			return throttlingPolicy;
		}

		// Token: 0x060065EB RID: 26091 RVA: 0x001A6DD8 File Offset: 0x001A4FD8
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			base.InternalValidate();
			if (this.ExchangeMaxCmdlets != null || this.PowerShellMaxCmdlets != null || this.PowerShellMaxOperations != null)
			{
				IThrottlingPolicy effectiveThrottlingPolicy = this.DataObject.GetEffectiveThrottlingPolicy(false);
				Unlimited<uint> powershellMaxOperations = SetThrottlingPolicy.ConvertToUnlimitedNull((this.PowerShellMaxOperations == null) ? new Unlimited<uint>?(effectiveThrottlingPolicy.PowerShellMaxOperations) : this.PowerShellMaxOperations);
				Unlimited<uint> powershellMaxCmdlets = SetThrottlingPolicy.ConvertToUnlimitedNull((this.PowerShellMaxCmdlets == null) ? new Unlimited<uint>?(effectiveThrottlingPolicy.PowerShellMaxCmdlets) : this.PowerShellMaxCmdlets);
				Unlimited<uint> exchangeMaxCmdlets = SetThrottlingPolicy.ConvertToUnlimitedNull((this.ExchangeMaxCmdlets == null) ? new Unlimited<uint>?(effectiveThrottlingPolicy.ExchangeMaxCmdlets) : this.ExchangeMaxCmdlets);
				bool flag;
				bool flag2;
				SetThrottlingPolicy.VerifyMaxCmdlets(powershellMaxOperations, powershellMaxCmdlets, exchangeMaxCmdlets, out flag, out flag2);
				if (flag2)
				{
					base.WriteError(new LocalizedException(Strings.ErrorMaxCmdletsNotSupported(powershellMaxOperations.ToString(), powershellMaxCmdlets.ToString(), exchangeMaxCmdlets.ToString())), (ErrorCategory)1000, null);
				}
				else if (flag)
				{
					this.WriteWarning(Strings.WarningMaxCmdletsRatioNotSupported(powershellMaxCmdlets.ToString(), exchangeMaxCmdlets.ToString()));
				}
			}
			if (this.DataObject.ThrottlingPolicyScope == ThrottlingPolicyScopeType.Organization)
			{
				ThrottlingPolicy[] array = this.ConfigurationSession.FindOrganizationThrottlingPolicies(this.DataObject.OrganizationId);
				if (array != null && array.Length > 0)
				{
					base.WriteError(new LocalizedException(Strings.ErrorOrganizationThrottlingPolicyAlreadyExists(base.OrganizationId.ToString())), (ErrorCategory)1000, null);
				}
			}
			if (this.DataObject.ThrottlingPolicyScope != ThrottlingPolicyScopeType.Organization)
			{
				if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxTenantConcurrency))
				{
					base.WriteError(new LocalizedException(Strings.ErrorCannotSetPowerShellMaxTenantConcurrency), (ErrorCategory)1000, null);
				}
				if (base.Fields.Contains(ThrottlingPolicySchema.PowerShellMaxTenantRunspaces))
				{
					base.WriteError(new LocalizedException(Strings.ErrorCannotSetPowerShellMaxTenantRunspaces), (ErrorCategory)1000, null);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060065EC RID: 26092 RVA: 0x001A6FF4 File Offset: 0x001A51F4
		protected override void WriteResult(IConfigurable dataObject)
		{
			ThrottlingPolicy throttlingPolicy = (ThrottlingPolicy)dataObject;
			try
			{
				throttlingPolicy.ConvertToEffectiveThrottlingPolicy(false);
			}
			catch (GlobalThrottlingPolicyNotFoundException)
			{
				base.WriteError(new ManagementObjectNotFoundException(DirectoryStrings.GlobalThrottlingPolicyNotFoundException), ErrorCategory.ObjectNotFound, null);
			}
			catch (GlobalThrottlingPolicyAmbiguousException)
			{
				base.WriteError(new ManagementObjectAmbiguousException(DirectoryStrings.GlobalThrottlingPolicyAmbiguousException), ErrorCategory.InvalidResult, null);
			}
			throttlingPolicy.Diagnostics = null;
			base.WriteResult(throttlingPolicy);
		}

		// Token: 0x060065ED RID: 26093 RVA: 0x001A7068 File Offset: 0x001A5268
		private void StampServiceAccountValue(ThrottlingPolicy dataObject, ADPropertyDefinition key, Unlimited<uint> serviceAccountDefaultValue)
		{
			if (!base.Fields.Contains(key))
			{
				dataObject[key] = serviceAccountDefaultValue;
			}
		}

		// Token: 0x04003645 RID: 13893
		internal static readonly ThrottlingPolicyScopeType[] AllowedThrottlingPolicyScopeTypes = new ThrottlingPolicyScopeType[]
		{
			ThrottlingPolicyScopeType.Regular,
			ThrottlingPolicyScopeType.Organization
		};
	}
}
