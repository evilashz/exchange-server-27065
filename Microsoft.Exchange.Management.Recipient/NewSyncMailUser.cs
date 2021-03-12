using System;
using System.Linq;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000CC RID: 204
	[Cmdlet("New", "SyncMailUser", SupportsShouldProcess = true, DefaultParameterSetName = "DisabledUser")]
	public sealed class NewSyncMailUser : NewMailUserBase
	{
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00037CF8 File Offset: 0x00035EF8
		public NewSyncMailUser()
		{
			base.NumberofCalls = ProvisioningCounters.NumberOfNewSyncMailuserCalls;
			base.NumberofSuccessfulCalls = ProvisioningCounters.NumberOfSuccessfulNewSyncMailuserCalls;
			base.AverageTimeTaken = ProvisioningCounters.AverageNewSyncMailuserResponseTime;
			base.AverageBaseTimeTaken = ProvisioningCounters.AverageNewSyncMailuserResponseTimeBase;
			base.AverageTimeTakenWithCache = ProvisioningCounters.AverageNewSyncMailuserResponseTimeWithCache;
			base.AverageBaseTimeTakenWithCache = ProvisioningCounters.AverageNewSyncMailuserResponseTimeBaseWithCache;
			base.AverageTimeTakenWithoutCache = ProvisioningCounters.AverageNewSyncMailuserResponseTimeWithoutCache;
			base.AverageBaseTimeTakenWithoutCache = ProvisioningCounters.AverageNewSyncMailuserResponseTimeBaseWithoutCache;
			base.TotalResponseTime = ProvisioningCounters.TotalNewSyncMailuserResponseTime;
			base.CacheActivePercentage = ProvisioningCounters.NewSyncMailuserCacheActivePercentage;
			base.CacheActiveBasePercentage = ProvisioningCounters.NewSyncMailuserCacheActivePercentageBase;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00037D84 File Offset: 0x00035F84
		// (set) Token: 0x06000ED3 RID: 3795 RVA: 0x00037D9B File Offset: 0x00035F9B
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] BypassModerationFrom
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[MailEnabledRecipientSchema.BypassModerationFrom];
			}
			set
			{
				base.Fields[MailEnabledRecipientSchema.BypassModerationFrom] = value;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00037DAE File Offset: 0x00035FAE
		// (set) Token: 0x06000ED5 RID: 3797 RVA: 0x00037DC5 File Offset: 0x00035FC5
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] BypassModerationFromDLMembers
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[MailEnabledRecipientSchema.BypassModerationFromDLMembers];
			}
			set
			{
				base.Fields[MailEnabledRecipientSchema.BypassModerationFromDLMembers] = value;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00037DD8 File Offset: 0x00035FD8
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x00037DEF File Offset: 0x00035FEF
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] AcceptMessagesOnlyFrom
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[ADRecipientSchema.AcceptMessagesOnlyFrom];
			}
			set
			{
				base.Fields[ADRecipientSchema.AcceptMessagesOnlyFrom] = value;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00037E02 File Offset: 0x00036002
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x00037E19 File Offset: 0x00036019
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[ADRecipientSchema.AcceptMessagesOnlyFromDLMembers];
			}
			set
			{
				base.Fields[ADRecipientSchema.AcceptMessagesOnlyFromDLMembers] = value;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00037E2C File Offset: 0x0003602C
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x00037E43 File Offset: 0x00036043
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] RejectMessagesFrom
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[ADRecipientSchema.RejectMessagesFrom];
			}
			set
			{
				base.Fields[ADRecipientSchema.RejectMessagesFrom] = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00037E56 File Offset: 0x00036056
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x00037E6D File Offset: 0x0003606D
		[Parameter(Mandatory = false)]
		public DeliveryRecipientIdParameter[] RejectMessagesFromDLMembers
		{
			get
			{
				return (DeliveryRecipientIdParameter[])base.Fields[ADRecipientSchema.RejectMessagesFromDLMembers];
			}
			set
			{
				base.Fields[ADRecipientSchema.RejectMessagesFromDLMembers] = value;
			}
		}

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x00037E80 File Offset: 0x00036080
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x00037E8D File Offset: 0x0003608D
		[Parameter(Mandatory = false)]
		public Guid ExchangeGuid
		{
			get
			{
				return this.DataObject.ExchangeGuid;
			}
			set
			{
				this.DataObject.ExchangeGuid = value;
			}
		}

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00037E9B File Offset: 0x0003609B
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x00037EB2 File Offset: 0x000360B2
		[Parameter(Mandatory = false)]
		public RecipientIdParameter ForwardingAddress
		{
			get
			{
				return (RecipientIdParameter)base.Fields["ForwardingAddress"];
			}
			set
			{
				base.Fields["ForwardingAddress"] = value;
			}
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00037EC5 File Offset: 0x000360C5
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x00037ED2 File Offset: 0x000360D2
		[Parameter(Mandatory = false)]
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return this.DataObject.DeliverToMailboxAndForward;
			}
			set
			{
				this.DataObject.DeliverToMailboxAndForward = value;
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x00037EE0 File Offset: 0x000360E0
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x00037EED File Offset: 0x000360ED
		[Parameter(Mandatory = false)]
		public string AssistantName
		{
			get
			{
				return this.DataObject.AssistantName;
			}
			set
			{
				this.DataObject.AssistantName = value;
			}
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00037EFB File Offset: 0x000360FB
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x00037F03 File Offset: 0x00036103
		[Parameter(Mandatory = false)]
		public override MultiValuedProperty<string> MailTipTranslations
		{
			get
			{
				return base.MailTipTranslations;
			}
			set
			{
				base.MailTipTranslations = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x00037F0C File Offset: 0x0003610C
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x00037F19 File Offset: 0x00036119
		[Parameter(Mandatory = false)]
		public byte[] BlockedSendersHash
		{
			get
			{
				return this.DataObject.BlockedSendersHash;
			}
			set
			{
				this.DataObject.BlockedSendersHash = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00037F27 File Offset: 0x00036127
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x00037F34 File Offset: 0x00036134
		[Parameter(Mandatory = false)]
		public string CustomAttribute1
		{
			get
			{
				return this.DataObject.CustomAttribute1;
			}
			set
			{
				this.DataObject.CustomAttribute1 = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00037F42 File Offset: 0x00036142
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x00037F4F File Offset: 0x0003614F
		[Parameter(Mandatory = false)]
		public string CustomAttribute10
		{
			get
			{
				return this.DataObject.CustomAttribute10;
			}
			set
			{
				this.DataObject.CustomAttribute10 = value;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00037F5D File Offset: 0x0003615D
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x00037F6A File Offset: 0x0003616A
		[Parameter(Mandatory = false)]
		public string CustomAttribute11
		{
			get
			{
				return this.DataObject.CustomAttribute11;
			}
			set
			{
				this.DataObject.CustomAttribute11 = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00037F78 File Offset: 0x00036178
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x00037F85 File Offset: 0x00036185
		[Parameter(Mandatory = false)]
		public string CustomAttribute12
		{
			get
			{
				return this.DataObject.CustomAttribute12;
			}
			set
			{
				this.DataObject.CustomAttribute12 = value;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00037F93 File Offset: 0x00036193
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x00037FA0 File Offset: 0x000361A0
		[Parameter(Mandatory = false)]
		public string CustomAttribute13
		{
			get
			{
				return this.DataObject.CustomAttribute13;
			}
			set
			{
				this.DataObject.CustomAttribute13 = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00037FAE File Offset: 0x000361AE
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x00037FBB File Offset: 0x000361BB
		[Parameter(Mandatory = false)]
		public string CustomAttribute14
		{
			get
			{
				return this.DataObject.CustomAttribute14;
			}
			set
			{
				this.DataObject.CustomAttribute14 = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00037FC9 File Offset: 0x000361C9
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x00037FD6 File Offset: 0x000361D6
		[Parameter(Mandatory = false)]
		public string CustomAttribute15
		{
			get
			{
				return this.DataObject.CustomAttribute15;
			}
			set
			{
				this.DataObject.CustomAttribute15 = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00037FE4 File Offset: 0x000361E4
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x00037FF1 File Offset: 0x000361F1
		[Parameter(Mandatory = false)]
		public string CustomAttribute2
		{
			get
			{
				return this.DataObject.CustomAttribute2;
			}
			set
			{
				this.DataObject.CustomAttribute2 = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00037FFF File Offset: 0x000361FF
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x0003800C File Offset: 0x0003620C
		[Parameter(Mandatory = false)]
		public string CustomAttribute3
		{
			get
			{
				return this.DataObject.CustomAttribute3;
			}
			set
			{
				this.DataObject.CustomAttribute3 = value;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0003801A File Offset: 0x0003621A
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x00038027 File Offset: 0x00036227
		[Parameter(Mandatory = false)]
		public string CustomAttribute4
		{
			get
			{
				return this.DataObject.CustomAttribute4;
			}
			set
			{
				this.DataObject.CustomAttribute4 = value;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00038035 File Offset: 0x00036235
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x00038042 File Offset: 0x00036242
		[Parameter(Mandatory = false)]
		public string CustomAttribute5
		{
			get
			{
				return this.DataObject.CustomAttribute5;
			}
			set
			{
				this.DataObject.CustomAttribute5 = value;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00038050 File Offset: 0x00036250
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x0003805D File Offset: 0x0003625D
		[Parameter(Mandatory = false)]
		public string CustomAttribute6
		{
			get
			{
				return this.DataObject.CustomAttribute6;
			}
			set
			{
				this.DataObject.CustomAttribute6 = value;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0003806B File Offset: 0x0003626B
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x00038078 File Offset: 0x00036278
		[Parameter(Mandatory = false)]
		public string CustomAttribute7
		{
			get
			{
				return this.DataObject.CustomAttribute7;
			}
			set
			{
				this.DataObject.CustomAttribute7 = value;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00038086 File Offset: 0x00036286
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x00038093 File Offset: 0x00036293
		[Parameter(Mandatory = false)]
		public string CustomAttribute8
		{
			get
			{
				return this.DataObject.CustomAttribute8;
			}
			set
			{
				this.DataObject.CustomAttribute8 = value;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000380A1 File Offset: 0x000362A1
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x000380AE File Offset: 0x000362AE
		[Parameter(Mandatory = false)]
		public string CustomAttribute9
		{
			get
			{
				return this.DataObject.CustomAttribute9;
			}
			set
			{
				this.DataObject.CustomAttribute9 = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000380BC File Offset: 0x000362BC
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x000380C9 File Offset: 0x000362C9
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute1
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute1;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute1 = value;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x000380D7 File Offset: 0x000362D7
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x000380E4 File Offset: 0x000362E4
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute2
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute2;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute2 = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x000380F2 File Offset: 0x000362F2
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x000380FF File Offset: 0x000362FF
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute3
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute3;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute3 = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0003810D File Offset: 0x0003630D
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x0003811A File Offset: 0x0003631A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute4
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute4;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute4 = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00038128 File Offset: 0x00036328
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00038135 File Offset: 0x00036335
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute5
		{
			get
			{
				return this.DataObject.ExtensionCustomAttribute5;
			}
			set
			{
				this.DataObject.ExtensionCustomAttribute5 = value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00038143 File Offset: 0x00036343
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00038150 File Offset: 0x00036350
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return this.DataObject.EmailAddresses;
			}
			set
			{
				this.DataObject.EmailAddresses = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0003815E File Offset: 0x0003635E
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0003816B File Offset: 0x0003636B
		[Parameter(Mandatory = false)]
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return this.DataObject.HiddenFromAddressListsEnabled;
			}
			set
			{
				this.DataObject.HiddenFromAddressListsEnabled = value;
			}
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00038179 File Offset: 0x00036379
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00038190 File Offset: 0x00036390
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] GrantSendOnBehalfTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[ADRecipientSchema.GrantSendOnBehalfTo];
			}
			set
			{
				base.Fields[ADRecipientSchema.GrantSendOnBehalfTo] = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x000381A3 File Offset: 0x000363A3
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x000381B0 File Offset: 0x000363B0
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return this.DataObject.Notes;
			}
			set
			{
				this.DataObject.Notes = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x000381BE File Offset: 0x000363BE
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x000381CB File Offset: 0x000363CB
		[Parameter(Mandatory = false)]
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return this.DataObject.RecipientDisplayType;
			}
			set
			{
				this.DataObject.RecipientDisplayType = value;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x000381D9 File Offset: 0x000363D9
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x000381E6 File Offset: 0x000363E6
		[Parameter(Mandatory = false)]
		public byte[] SafeRecipientsHash
		{
			get
			{
				return this.DataObject.SafeRecipientsHash;
			}
			set
			{
				this.DataObject.SafeRecipientsHash = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x000381F4 File Offset: 0x000363F4
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x00038201 File Offset: 0x00036401
		[Parameter(Mandatory = false)]
		public byte[] SafeSendersHash
		{
			get
			{
				return this.DataObject.SafeSendersHash;
			}
			set
			{
				this.DataObject.SafeSendersHash = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0003820F File Offset: 0x0003640F
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x0003821C File Offset: 0x0003641C
		[Parameter(Mandatory = false)]
		public byte[] Picture
		{
			get
			{
				return this.DataObject.ThumbnailPhoto;
			}
			set
			{
				this.DataObject.ThumbnailPhoto = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0003822A File Offset: 0x0003642A
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x00038237 File Offset: 0x00036437
		[Parameter(Mandatory = false)]
		public UseMapiRichTextFormat UseMapiRichTextFormat
		{
			get
			{
				return this.DataObject.UseMapiRichTextFormat;
			}
			set
			{
				this.DataObject.UseMapiRichTextFormat = value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x00038245 File Offset: 0x00036445
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x00038252 File Offset: 0x00036452
		[Parameter(Mandatory = false)]
		public string DirSyncId
		{
			get
			{
				return this.DataObject.DirSyncId;
			}
			set
			{
				this.DataObject.DirSyncId = value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00038260 File Offset: 0x00036460
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0003826D File Offset: 0x0003646D
		[Parameter(Mandatory = false)]
		public string City
		{
			get
			{
				return this.DataObject.City;
			}
			set
			{
				this.DataObject.City = value;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0003827B File Offset: 0x0003647B
		// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00038288 File Offset: 0x00036488
		[Parameter(Mandatory = false)]
		public string Company
		{
			get
			{
				return this.DataObject.Company;
			}
			set
			{
				this.DataObject.Company = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06000F2A RID: 3882 RVA: 0x00038296 File Offset: 0x00036496
		// (set) Token: 0x06000F2B RID: 3883 RVA: 0x000382AD File Offset: 0x000364AD
		[Parameter(Mandatory = false)]
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)base.Fields[SyncMailUserSchema.CountryOrRegion];
			}
			set
			{
				base.Fields[SyncMailUserSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x000382C0 File Offset: 0x000364C0
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x000382CD File Offset: 0x000364CD
		[Parameter(Mandatory = false)]
		public string Co
		{
			get
			{
				return this.DataObject.Co;
			}
			set
			{
				this.DataObject.Co = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x000382DB File Offset: 0x000364DB
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x000382E8 File Offset: 0x000364E8
		[Parameter(Mandatory = false)]
		public string C
		{
			get
			{
				return this.DataObject.C;
			}
			set
			{
				this.DataObject.C = value;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x000382F6 File Offset: 0x000364F6
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x00038303 File Offset: 0x00036503
		[Parameter(Mandatory = false)]
		public int CountryCode
		{
			get
			{
				return this.DataObject.CountryCode;
			}
			set
			{
				this.DataObject.CountryCode = value;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x00038311 File Offset: 0x00036511
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0003831E File Offset: 0x0003651E
		[Parameter(Mandatory = false)]
		public string Department
		{
			get
			{
				return this.DataObject.Department;
			}
			set
			{
				this.DataObject.Department = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0003832C File Offset: 0x0003652C
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x00038339 File Offset: 0x00036539
		[Parameter(Mandatory = false)]
		public string Fax
		{
			get
			{
				return this.DataObject.Fax;
			}
			set
			{
				this.DataObject.Fax = value;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x00038347 File Offset: 0x00036547
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x00038354 File Offset: 0x00036554
		[Parameter(Mandatory = false)]
		public string HomePhone
		{
			get
			{
				return this.DataObject.HomePhone;
			}
			set
			{
				this.DataObject.HomePhone = value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x00038362 File Offset: 0x00036562
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x0003836F File Offset: 0x0003656F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserCertificate
		{
			get
			{
				return this.DataObject.UserCertificate;
			}
			set
			{
				this.DataObject.UserCertificate = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0003837D File Offset: 0x0003657D
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0003838A File Offset: 0x0003658A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserSMimeCertificate
		{
			get
			{
				return this.DataObject.UserSMIMECertificate;
			}
			set
			{
				this.DataObject.UserSMIMECertificate = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x00038398 File Offset: 0x00036598
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x000383AF File Offset: 0x000365AF
		[Parameter(Mandatory = false)]
		public UserContactIdParameter Manager
		{
			get
			{
				return (UserContactIdParameter)base.Fields["Manager"];
			}
			set
			{
				base.Fields["Manager"] = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x000383C2 File Offset: 0x000365C2
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x000383CF File Offset: 0x000365CF
		[Parameter(Mandatory = false)]
		public string MobilePhone
		{
			get
			{
				return this.DataObject.MobilePhone;
			}
			set
			{
				this.DataObject.MobilePhone = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x000383DD File Offset: 0x000365DD
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x000383EA File Offset: 0x000365EA
		[Parameter(Mandatory = false)]
		public string Office
		{
			get
			{
				return this.DataObject.Office;
			}
			set
			{
				this.DataObject.Office = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x000383F8 File Offset: 0x000365F8
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x00038405 File Offset: 0x00036605
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return this.DataObject.OtherFax;
			}
			set
			{
				this.DataObject.OtherFax = value;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00038413 File Offset: 0x00036613
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x00038420 File Offset: 0x00036620
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return this.DataObject.OtherHomePhone;
			}
			set
			{
				this.DataObject.OtherHomePhone = value;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0003842E File Offset: 0x0003662E
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x0003843B File Offset: 0x0003663B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return this.DataObject.OtherTelephone;
			}
			set
			{
				this.DataObject.OtherTelephone = value;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00038449 File Offset: 0x00036649
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00038456 File Offset: 0x00036656
		[Parameter(Mandatory = false)]
		public string Pager
		{
			get
			{
				return this.DataObject.Pager;
			}
			set
			{
				this.DataObject.Pager = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00038464 File Offset: 0x00036664
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x00038471 File Offset: 0x00036671
		[Parameter(Mandatory = false)]
		public string Phone
		{
			get
			{
				return this.DataObject.Phone;
			}
			set
			{
				this.DataObject.Phone = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0003847F File Offset: 0x0003667F
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0003848C File Offset: 0x0003668C
		[Parameter(Mandatory = false)]
		public string PostalCode
		{
			get
			{
				return this.DataObject.PostalCode;
			}
			set
			{
				this.DataObject.PostalCode = value;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0003849A File Offset: 0x0003669A
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x000384A7 File Offset: 0x000366A7
		[Parameter(Mandatory = false)]
		public string StateOrProvince
		{
			get
			{
				return this.DataObject.StateOrProvince;
			}
			set
			{
				this.DataObject.StateOrProvince = value;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x000384B5 File Offset: 0x000366B5
		// (set) Token: 0x06000F51 RID: 3921 RVA: 0x000384C2 File Offset: 0x000366C2
		[Parameter(Mandatory = false)]
		public string StreetAddress
		{
			get
			{
				return this.DataObject.StreetAddress;
			}
			set
			{
				this.DataObject.StreetAddress = value;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x000384D0 File Offset: 0x000366D0
		// (set) Token: 0x06000F53 RID: 3923 RVA: 0x000384DD File Offset: 0x000366DD
		[Parameter(Mandatory = false)]
		public string TelephoneAssistant
		{
			get
			{
				return this.DataObject.TelephoneAssistant;
			}
			set
			{
				this.DataObject.TelephoneAssistant = value;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x000384EB File Offset: 0x000366EB
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x000384F8 File Offset: 0x000366F8
		[Parameter(Mandatory = false)]
		public string Title
		{
			get
			{
				return this.DataObject.Title;
			}
			set
			{
				this.DataObject.Title = value;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00038506 File Offset: 0x00036706
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x00038513 File Offset: 0x00036713
		[Parameter(Mandatory = false)]
		public string WebPage
		{
			get
			{
				return this.DataObject.WebPage;
			}
			set
			{
				this.DataObject.WebPage = value;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00038521 File Offset: 0x00036721
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0003852E File Offset: 0x0003672E
		[Parameter(Mandatory = false)]
		public Guid ArchiveGuid
		{
			get
			{
				return this.DataObject.ArchiveGuid;
			}
			set
			{
				this.DataObject.ArchiveGuid = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0003853C File Offset: 0x0003673C
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x00038549 File Offset: 0x00036749
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ArchiveName
		{
			get
			{
				return this.DataObject.ArchiveName;
			}
			set
			{
				this.DataObject.ArchiveName = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00038557 File Offset: 0x00036757
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x00038564 File Offset: 0x00036764
		[Parameter(Mandatory = false)]
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return this.DataObject.MasterAccountSid;
			}
			set
			{
				this.DataObject.MasterAccountSid = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00038572 File Offset: 0x00036772
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0003857F File Offset: 0x0003677F
		[Parameter(Mandatory = false)]
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return this.DataObject.ReleaseTrack;
			}
			set
			{
				this.DataObject.ReleaseTrack = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0003858D File Offset: 0x0003678D
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x000385A4 File Offset: 0x000367A4
		[Parameter(Mandatory = false)]
		public MailboxPlanIdParameter IntendedMailboxPlanName
		{
			get
			{
				return (MailboxPlanIdParameter)base.Fields["IntendedMailboxPlan"];
			}
			set
			{
				base.Fields["IntendedMailboxPlan"] = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x000385B7 File Offset: 0x000367B7
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x000385C4 File Offset: 0x000367C4
		[Parameter(Mandatory = false)]
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return this.DataObject.RequireAllSendersAreAuthenticated;
			}
			set
			{
				this.DataObject.RequireAllSendersAreAuthenticated = value;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x000385D2 File Offset: 0x000367D2
		// (set) Token: 0x06000F65 RID: 3941 RVA: 0x000385DF File Offset: 0x000367DF
		[Parameter(Mandatory = false)]
		public ExchangeResourceType? ResourceType
		{
			get
			{
				return this.DataObject.ResourceType;
			}
			set
			{
				this.DataObject.ResourceType = value;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x000385ED File Offset: 0x000367ED
		// (set) Token: 0x06000F67 RID: 3943 RVA: 0x000385FA File Offset: 0x000367FA
		[Parameter(Mandatory = false)]
		public int? ResourceCapacity
		{
			get
			{
				return this.DataObject.ResourceCapacity;
			}
			set
			{
				this.DataObject.ResourceCapacity = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x00038608 File Offset: 0x00036808
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x0003861F File Offset: 0x0003681F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourceCustom
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields[SyncMailUserSchema.ResourceCustom];
			}
			set
			{
				base.Fields[SyncMailUserSchema.ResourceCustom] = value;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00038632 File Offset: 0x00036832
		// (set) Token: 0x06000F6B RID: 3947 RVA: 0x0003863F File Offset: 0x0003683F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourceMetaData
		{
			get
			{
				return this.DataObject.ResourceMetaData;
			}
			set
			{
				this.DataObject.ResourceMetaData = value;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x0003864D File Offset: 0x0003684D
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x0003865A File Offset: 0x0003685A
		[Parameter(Mandatory = false)]
		public string ResourcePropertiesDisplay
		{
			get
			{
				return this.DataObject.ResourcePropertiesDisplay;
			}
			set
			{
				this.DataObject.ResourcePropertiesDisplay = value;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x00038668 File Offset: 0x00036868
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x00038675 File Offset: 0x00036875
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourceSearchProperties
		{
			get
			{
				return this.DataObject.ResourceSearchProperties;
			}
			set
			{
				this.DataObject.ResourceSearchProperties = value;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00038683 File Offset: 0x00036883
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x00038690 File Offset: 0x00036890
		[Parameter(Mandatory = false)]
		public int? SeniorityIndex
		{
			get
			{
				return this.DataObject.HABSeniorityIndex;
			}
			set
			{
				this.DataObject.HABSeniorityIndex = value;
			}
		}

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0003869E File Offset: 0x0003689E
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x000386AB File Offset: 0x000368AB
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return this.DataObject.PhoneticDisplayName;
			}
			set
			{
				this.DataObject.PhoneticDisplayName = value;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x000386B9 File Offset: 0x000368B9
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x000386C6 File Offset: 0x000368C6
		[Parameter(Mandatory = false)]
		public bool IsCalculatedTargetAddress
		{
			get
			{
				return this.DataObject.IsCalculatedTargetAddress;
			}
			set
			{
				this.DataObject.IsCalculatedTargetAddress = value;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x000386D4 File Offset: 0x000368D4
		// (set) Token: 0x06000F77 RID: 3959 RVA: 0x000386E1 File Offset: 0x000368E1
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return this.DataObject.OnPremisesObjectId;
			}
			set
			{
				this.DataObject.OnPremisesObjectId = value;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x000386EF File Offset: 0x000368EF
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x000386FC File Offset: 0x000368FC
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return this.DataObject.IsDirSynced;
			}
			set
			{
				this.DataObject.IsDirSynced = value;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0003870A File Offset: 0x0003690A
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00038717 File Offset: 0x00036917
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return this.DataObject.DirSyncAuthorityMetadata;
			}
			set
			{
				this.DataObject.DirSyncAuthorityMetadata = value;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00038725 File Offset: 0x00036925
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x0003873C File Offset: 0x0003693C
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection SmtpAndX500Addresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields["SmtpAndX500Addresses"];
			}
			set
			{
				base.Fields["SmtpAndX500Addresses"] = value;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003874F File Offset: 0x0003694F
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x00038766 File Offset: 0x00036966
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection SipAddresses
		{
			get
			{
				return (ProxyAddressCollection)base.Fields["SipAddresses"];
			}
			set
			{
				base.Fields["SipAddresses"] = value;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00038779 File Offset: 0x00036979
		// (set) Token: 0x06000F81 RID: 3969 RVA: 0x0003879F File Offset: 0x0003699F
		[Parameter(Mandatory = false)]
		public SwitchParameter DoNotCheckAcceptedDomains
		{
			get
			{
				return (SwitchParameter)(base.Fields["DoNotCheckAcceptedDomains"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DoNotCheckAcceptedDomains"] = value;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06000F82 RID: 3970 RVA: 0x000387B7 File Offset: 0x000369B7
		// (set) Token: 0x06000F83 RID: 3971 RVA: 0x000387C4 File Offset: 0x000369C4
		[Parameter(Mandatory = false)]
		public RemoteRecipientType RemoteRecipientType
		{
			get
			{
				return this.DataObject.RemoteRecipientType;
			}
			set
			{
				this.DataObject.RemoteRecipientType = value;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x000387D2 File Offset: 0x000369D2
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x000387E9 File Offset: 0x000369E9
		[Parameter(Mandatory = false)]
		public string ValidationOrganization
		{
			get
			{
				return (string)base.Fields["ValidationOrganization"];
			}
			set
			{
				base.Fields["ValidationOrganization"] = value;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000387FC File Offset: 0x000369FC
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x00038813 File Offset: 0x00036A13
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> InPlaceHoldsRaw
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields[SyncMailUserSchema.InPlaceHoldsRaw];
			}
			set
			{
				base.Fields[SyncMailUserSchema.InPlaceHoldsRaw] = value;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00038826 File Offset: 0x00036A26
		// (set) Token: 0x06000F89 RID: 3977 RVA: 0x00038833 File Offset: 0x00036A33
		[Parameter(Mandatory = false)]
		public ElcMailboxFlags ElcMailboxFlags
		{
			get
			{
				return this.DataObject.ElcMailboxFlags;
			}
			set
			{
				this.DataObject.ElcMailboxFlags = value;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00038841 File Offset: 0x00036A41
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x0003884E File Offset: 0x00036A4E
		[Parameter(Mandatory = false)]
		public DateTime? StartDateForRetentionHold
		{
			get
			{
				return this.DataObject.StartDateForRetentionHold;
			}
			set
			{
				this.DataObject.StartDateForRetentionHold = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x0003885C File Offset: 0x00036A5C
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x00038869 File Offset: 0x00036A69
		[Parameter(Mandatory = false)]
		public DateTime? EndDateForRetentionHold
		{
			get
			{
				return this.DataObject.EndDateForRetentionHold;
			}
			set
			{
				this.DataObject.EndDateForRetentionHold = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00038877 File Offset: 0x00036A77
		// (set) Token: 0x06000F8F RID: 3983 RVA: 0x00038884 File Offset: 0x00036A84
		[Parameter(Mandatory = false)]
		public string RetentionComment
		{
			get
			{
				return this.DataObject.RetentionComment;
			}
			set
			{
				this.DataObject.RetentionComment = value;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00038892 File Offset: 0x00036A92
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x0003889F File Offset: 0x00036A9F
		[Parameter(Mandatory = false)]
		public string RetentionUrl
		{
			get
			{
				return this.DataObject.RetentionUrl;
			}
			set
			{
				this.DataObject.RetentionUrl = value;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x000388AD File Offset: 0x00036AAD
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x000388BA File Offset: 0x00036ABA
		[Parameter(Mandatory = false)]
		public bool MailboxAuditEnabled
		{
			get
			{
				return this.DataObject.MailboxAuditEnabled;
			}
			set
			{
				this.DataObject.MailboxAuditEnabled = value;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x000388C8 File Offset: 0x00036AC8
		// (set) Token: 0x06000F95 RID: 3989 RVA: 0x000388D5 File Offset: 0x00036AD5
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MailboxAuditLogAgeLimit
		{
			get
			{
				return this.DataObject.MailboxAuditLogAgeLimit;
			}
			set
			{
				this.DataObject.MailboxAuditLogAgeLimit = value;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x000388E3 File Offset: 0x00036AE3
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x000388F0 File Offset: 0x00036AF0
		[Parameter(Mandatory = false)]
		public MailboxAuditOperations AuditAdminOperations
		{
			get
			{
				return this.DataObject.AuditAdminOperations;
			}
			set
			{
				this.DataObject.AuditAdminOperations = value;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x000388FE File Offset: 0x00036AFE
		// (set) Token: 0x06000F99 RID: 3993 RVA: 0x0003890B File Offset: 0x00036B0B
		[Parameter(Mandatory = false)]
		public MailboxAuditOperations AuditDelegateAdminOperations
		{
			get
			{
				return this.DataObject.AuditDelegateAdminOperations;
			}
			set
			{
				this.DataObject.AuditDelegateAdminOperations = value;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x00038919 File Offset: 0x00036B19
		// (set) Token: 0x06000F9B RID: 3995 RVA: 0x00038926 File Offset: 0x00036B26
		[Parameter(Mandatory = false)]
		public MailboxAuditOperations AuditDelegateOperations
		{
			get
			{
				return this.DataObject.AuditDelegateOperations;
			}
			set
			{
				this.DataObject.AuditDelegateOperations = value;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00038934 File Offset: 0x00036B34
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x00038941 File Offset: 0x00036B41
		[Parameter(Mandatory = false)]
		public MailboxAuditOperations AuditOwnerOperations
		{
			get
			{
				return this.DataObject.AuditOwnerOperations;
			}
			set
			{
				this.DataObject.AuditOwnerOperations = value;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x0003894F File Offset: 0x00036B4F
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x0003895C File Offset: 0x00036B5C
		[Parameter(Mandatory = false)]
		public bool BypassAudit
		{
			get
			{
				return this.DataObject.BypassAudit;
			}
			set
			{
				this.DataObject.BypassAudit = value;
			}
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x0003896A File Offset: 0x00036B6A
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x00038977 File Offset: 0x00036B77
		[Parameter(Mandatory = false)]
		public string LitigationHoldOwner
		{
			get
			{
				return this.DataObject.LitigationHoldOwner;
			}
			set
			{
				this.DataObject.LitigationHoldOwner = value;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00038985 File Offset: 0x00036B85
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00038992 File Offset: 0x00036B92
		[Parameter(Mandatory = false)]
		public DateTime? LitigationHoldDate
		{
			get
			{
				return this.DataObject.LitigationHoldDate;
			}
			set
			{
				this.DataObject.LitigationHoldDate = value;
			}
		}

		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x000389A0 File Offset: 0x00036BA0
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x000389B7 File Offset: 0x00036BB7
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SiteMailboxOwners
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[SyncMailUserSchema.SiteMailboxOwners];
			}
			set
			{
				base.Fields[SyncMailUserSchema.SiteMailboxOwners] = value;
			}
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x000389CA File Offset: 0x00036BCA
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x000389E1 File Offset: 0x00036BE1
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SiteMailboxUsers
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[SyncMailUserSchema.SiteMailboxUsers];
			}
			set
			{
				base.Fields[SyncMailUserSchema.SiteMailboxUsers] = value;
			}
		}

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x000389F4 File Offset: 0x00036BF4
		// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x00038A01 File Offset: 0x00036C01
		[Parameter(Mandatory = false)]
		public DateTime? SiteMailboxClosedTime
		{
			get
			{
				return this.DataObject.TeamMailboxClosedTime;
			}
			set
			{
				this.DataObject.TeamMailboxClosedTime = value;
			}
		}

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00038A0F File Offset: 0x00036C0F
		// (set) Token: 0x06000FAB RID: 4011 RVA: 0x00038A1C File Offset: 0x00036C1C
		[Parameter(Mandatory = false)]
		public Uri SharePointUrl
		{
			get
			{
				return this.DataObject.SharePointUrl;
			}
			set
			{
				this.DataObject.SharePointUrl = value;
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00038A2A File Offset: 0x00036C2A
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x00038A41 File Offset: 0x00036C41
		[Parameter(Mandatory = false)]
		public SwitchParameter AccountDisabled
		{
			get
			{
				return (SwitchParameter)base.Fields[SyncMailUserSchema.AccountDisabled];
			}
			set
			{
				base.Fields[SyncMailUserSchema.AccountDisabled] = value;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00038A59 File Offset: 0x00036C59
		// (set) Token: 0x06000FAF RID: 4015 RVA: 0x00038A66 File Offset: 0x00036C66
		[Parameter(Mandatory = false)]
		public DateTime? StsRefreshTokensValidFrom
		{
			get
			{
				return this.DataObject.StsRefreshTokensValidFrom;
			}
			set
			{
				this.DataObject.StsRefreshTokensValidFrom = value;
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00038A74 File Offset: 0x00036C74
		protected override bool ShouldCheckAcceptedDomains()
		{
			return !this.DoNotCheckAcceptedDomains;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00038AD8 File Offset: 0x00036CD8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			if ((this.SmtpAndX500Addresses != null && this.SmtpAndX500Addresses.Count > 0) || (this.SipAddresses != null && this.SipAddresses.Count > 0))
			{
				this.DataObject.EmailAddresses = SyncTaskHelper.MergeAddresses(this.SmtpAndX500Addresses, this.SipAddresses);
			}
			base.InternalBeginProcessing();
			if (this.ValidationOrganization != null && !string.Equals(this.ValidationOrganization, base.CurrentOrganizationId.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				base.ThrowTerminatingError(new ValidationOrgCurrentOrgNotMatchException(this.ValidationOrganization, base.CurrentOrganizationId.ToString()), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsModified(SyncMailUserSchema.CountryOrRegion) && (this.DataObject.IsModified(SyncMailUserSchema.C) || this.DataObject.IsModified(SyncMailUserSchema.Co) || this.DataObject.IsModified(SyncMailUserSchema.CountryCode)))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorConflictCountryOrRegion), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsModified(SyncMailUserSchema.ResourceCustom) && (this.DataObject.IsModified(SyncMailUserSchema.ResourcePropertiesDisplay) || this.DataObject.IsModified(SyncMailUserSchema.ResourceSearchProperties)))
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorConflictResourceCustom), ExchangeErrorCategory.Client, null);
			}
			if (base.Fields.IsModified("Manager"))
			{
				this.manager = MailboxTaskHelper.LookupManager(this.Manager, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), ExchangeErrorCategory.Client, base.TenantGlobalCatalogSession);
			}
			if (base.Fields.IsModified("ForwardingAddress") && this.ForwardingAddress != null)
			{
				this.forwardingAddress = (ADRecipient)base.GetDataObject<ADRecipient>(this.ForwardingAddress, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.ForwardingAddress.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.ForwardingAddress.ToString())), ExchangeErrorCategory.Client);
			}
			if (base.Fields.IsModified("IntendedMailboxPlan"))
			{
				this.intendedMailboxPlanObject = null;
				if (this.IntendedMailboxPlanName != null)
				{
					this.intendedMailboxPlanObject = base.ProvisioningCache.TryAddAndGetOrganizationDictionaryValue<ADUser, string>(CannedProvisioningCacheKeys.CacheKeyMailboxPlanIdParameterId, base.CurrentOrganizationId, this.IntendedMailboxPlanName.RawIdentity, () => (ADUser)base.GetDataObject<ADUser>(this.IntendedMailboxPlanName, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxPlanNotFound(this.IntendedMailboxPlanName.ToString())), new LocalizedString?(Strings.ErrorMailboxPlanNotUnique(this.IntendedMailboxPlanName.ToString())), ExchangeErrorCategory.Client));
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.GrantSendOnBehalfTo) && this.GrantSendOnBehalfTo != null && this.GrantSendOnBehalfTo.Length != 0)
			{
				this.grantSendOnBehalfTo = new MultiValuedProperty<ADRecipient>();
				foreach (RecipientIdParameter recipientIdParameter in this.GrantSendOnBehalfTo)
				{
					ADRecipient item = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter.ToString())), ExchangeErrorCategory.Client);
					this.grantSendOnBehalfTo.Add(item);
				}
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFrom) && this.BypassModerationFrom != null)
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty;
				MultiValuedProperty<ADObjectId> multiValuedProperty2;
				this.bypassModerationFromRecipient = SetMailEnabledRecipientObjectTask<MailUserIdParameter, SyncMailUser, ADUser>.ResolveAndSeparateMessageDeliveryRestrictionRecipientIdentities(this.BypassModerationFrom, "BypassModerationFrom", base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), out multiValuedProperty, out multiValuedProperty2);
				if (multiValuedProperty != null && multiValuedProperty.Count > 0)
				{
					base.ThrowTerminatingError(new RecipientTaskException(Strings.ErrorIndividualRecipientNeeded(multiValuedProperty[0].ToString())), ExchangeErrorCategory.Client, null);
				}
				this.bypassModerationFrom = multiValuedProperty2;
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFromDLMembers) && this.BypassModerationFromDLMembers != null)
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty3;
				MultiValuedProperty<ADObjectId> multiValuedProperty4;
				this.bypassModerationFromDLMembersRecipient = SetMailEnabledRecipientObjectTask<MailUserIdParameter, SyncMailUser, ADUser>.ResolveAndSeparateMessageDeliveryRestrictionRecipientIdentities(this.BypassModerationFromDLMembers, "BypassModerationFromDLMembers", base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new Task.ErrorLoggerDelegate(base.WriteError), out multiValuedProperty3, out multiValuedProperty4);
				if (multiValuedProperty4 != null && multiValuedProperty4.Count > 0)
				{
					base.ThrowTerminatingError(new RecipientTaskException(Strings.ErrorGroupRecipientNeeded(multiValuedProperty4[0].ToString())), ExchangeErrorCategory.Client, null);
				}
				this.bypassModerationFromDLMembers = multiValuedProperty3;
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFrom) && this.AcceptMessagesOnlyFrom != null && this.AcceptMessagesOnlyFrom.Length != 0)
			{
				this.acceptMessagesOnlyFrom = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter in this.AcceptMessagesOnlyFrom)
				{
					ADRecipient item2 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter.ToString())), ExchangeErrorCategory.Client);
					this.acceptMessagesOnlyFrom.Add(item2);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers) && this.AcceptMessagesOnlyFromDLMembers != null && this.AcceptMessagesOnlyFromDLMembers.Length != 0)
			{
				this.acceptMessagesOnlyFromDLMembers = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter2 in this.AcceptMessagesOnlyFromDLMembers)
				{
					ADRecipient item3 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter2, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter2.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter2.ToString())), ExchangeErrorCategory.Client);
					this.acceptMessagesOnlyFromDLMembers.Add(item3);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFrom) && this.RejectMessagesFrom != null && this.RejectMessagesFrom.Length != 0)
			{
				this.rejectMessagesFrom = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter3 in this.RejectMessagesFrom)
				{
					ADRecipient item4 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter3, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter3.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter3.ToString())), ExchangeErrorCategory.Client);
					this.rejectMessagesFrom.Add(item4);
				}
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFromDLMembers) && this.RejectMessagesFromDLMembers != null && this.RejectMessagesFromDLMembers.Length != 0)
			{
				this.rejectMessagesFromDLMembers = new MultiValuedProperty<ADRecipient>();
				foreach (DeliveryRecipientIdParameter deliveryRecipientIdParameter4 in this.RejectMessagesFromDLMembers)
				{
					ADRecipient item5 = (ADRecipient)base.GetDataObject<ADRecipient>(deliveryRecipientIdParameter4, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(deliveryRecipientIdParameter4.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(deliveryRecipientIdParameter4.ToString())), ExchangeErrorCategory.Client);
					this.rejectMessagesFromDLMembers.Add(item5);
				}
			}
			if (base.Fields.IsModified(SyncMailUserSchema.SiteMailboxOwners))
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty5 = SetSyncMailUser.ResolveSiteMailboxOwnersReferenceParameter(this.SiteMailboxOwners, base.TenantGlobalCatalogSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADRecipient>), new WriteWarningDelegate(this.WriteWarning));
				if (multiValuedProperty5 != null && multiValuedProperty5.Count != 0)
				{
					this.siteMailboxOwners = multiValuedProperty5;
				}
			}
			if (base.Fields.IsModified(SyncMailUserSchema.SiteMailboxUsers) && this.SiteMailboxUsers != null && this.SiteMailboxUsers.Length != 0)
			{
				this.siteMailboxUsers = new MultiValuedProperty<ADObjectId>();
				foreach (RecipientIdParameter recipientIdParameter2 in this.SiteMailboxUsers)
				{
					ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(recipientIdParameter2, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(recipientIdParameter2.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(recipientIdParameter2.ToString())), ExchangeErrorCategory.Client);
					if (TeamMailboxMembershipHelper.IsUserQualifiedType(adrecipient))
					{
						this.siteMailboxUsers.Add((ADObjectId)adrecipient.Identity);
					}
					else
					{
						base.WriteError(new TaskInvalidOperationException(Strings.ErrorTeamMailboxUserNotResolved(adrecipient.Identity.ToString())), ExchangeErrorCategory.Client, adrecipient.Identity);
					}
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003929C File Offset: 0x0003749C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.manager != null)
			{
				RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, this.manager, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.forwardingAddress != null)
			{
				RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, this.forwardingAddress, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.intendedMailboxPlanObject != null)
			{
				RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, this.intendedMailboxPlanObject, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (this.grantSendOnBehalfTo != null)
			{
				foreach (ADRecipient recipient in this.grantSendOnBehalfTo)
				{
					RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, recipient, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (this.bypassModerationFromRecipient != null)
			{
				foreach (ADRecipient recipient2 in this.bypassModerationFromRecipient)
				{
					RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, recipient2, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (this.bypassModerationFromDLMembersRecipient != null)
			{
				foreach (ADRecipient recipient3 in this.bypassModerationFromDLMembersRecipient)
				{
					RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, recipient3, new Task.ErrorLoggerDelegate(base.WriteError));
				}
			}
			if (this.DataObject.IsModified(ADMailboxRecipientSchema.ExchangeGuid) && this.DataObject.ExchangeGuid != Guid.Empty && this.DataObject.IsModified(ADUserSchema.ArchiveGuid) && this.DataObject.ArchiveGuid != Guid.Empty && this.DataObject.ExchangeGuid == this.DataObject.ArchiveGuid)
			{
				base.WriteError(new RecipientTaskException(Strings.ErrorInvalidParameterValue("ExchangeGuid", this.DataObject.ExchangeGuid.ToString())), ExchangeErrorCategory.Client, this.DataObject.Identity);
			}
			if (this.DataObject.IsModified(ADMailboxRecipientSchema.ExchangeGuid) && this.ExchangeGuid != Guid.Empty)
			{
				RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(this.DataObject, ADMailboxRecipientSchema.ExchangeGuid, this.ExchangeGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			if (this.DataObject.IsModified(ADUserSchema.ArchiveGuid) && this.ArchiveGuid != Guid.Empty)
			{
				RecipientTaskHelper.IsExchangeGuidOrArchiveGuidUnique(this.DataObject, ADUserSchema.ArchiveGuid, this.ArchiveGuid, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), ExchangeErrorCategory.Client);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000395D8 File Offset: 0x000377D8
		protected override void PrepareRecipientObject(ADUser user)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(user);
			user.BypassModerationCheck = true;
			if (base.Fields.IsModified("Manager"))
			{
				user.Manager = ((this.manager == null) ? null : ((ADObjectId)this.manager.Identity));
			}
			if (base.Fields.IsModified("ForwardingAddress"))
			{
				user.ForwardingAddress = ((this.forwardingAddress == null) ? null : ((ADObjectId)this.forwardingAddress.Identity));
			}
			if (base.Fields.IsModified("IntendedMailboxPlan"))
			{
				user.IntendedMailboxPlan = ((this.intendedMailboxPlanObject == null) ? null : this.intendedMailboxPlanObject.Id);
			}
			if (base.Fields.IsModified("ReleaseTrack"))
			{
				user.ReleaseTrack = this.ReleaseTrack;
			}
			if (base.Fields.IsModified(ADRecipientSchema.GrantSendOnBehalfTo) && this.grantSendOnBehalfTo != null)
			{
				foreach (ADRecipient adrecipient in this.grantSendOnBehalfTo)
				{
					user.GrantSendOnBehalfTo.Add(adrecipient.Identity as ADObjectId);
				}
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFrom))
			{
				user.BypassModerationFrom = this.bypassModerationFrom;
			}
			if (base.Fields.IsModified(MailEnabledRecipientSchema.BypassModerationFromDLMembers))
			{
				user.BypassModerationFromDLMembers = this.bypassModerationFromDLMembers;
			}
			if (this.DataObject.IsModified(ADRecipientSchema.EmailAddresses))
			{
				user.EmailAddresses = SyncTaskHelper.FilterDuplicateEmailAddresses(base.TenantGlobalCatalogSession, this.EmailAddresses, this.DataObject, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFrom))
			{
				user.AcceptMessagesOnlyFrom = (from c in this.acceptMessagesOnlyFrom
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.AcceptMessagesOnlyFromDLMembers))
			{
				user.AcceptMessagesOnlyFromDLMembers = (from c in this.acceptMessagesOnlyFromDLMembers
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFrom))
			{
				user.RejectMessagesFrom = (from c in this.rejectMessagesFrom
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(ADRecipientSchema.RejectMessagesFromDLMembers))
			{
				user.RejectMessagesFromDLMembers = (from c in this.rejectMessagesFromDLMembers
				select c.Identity as ADObjectId).ToArray<ADObjectId>();
			}
			if (base.Fields.IsModified(SyncMailUserSchema.SiteMailboxOwners))
			{
				user.Owners = this.siteMailboxOwners;
			}
			if (base.Fields.IsModified(SyncMailUserSchema.SiteMailboxUsers))
			{
				user.DelegateListLink = this.siteMailboxUsers;
			}
			if (base.Fields.IsModified(SyncMailUserSchema.CountryOrRegion))
			{
				user.CountryOrRegion = this.CountryOrRegion;
			}
			if (base.Fields.IsModified(SyncMailUserSchema.ResourceCustom))
			{
				user.ResourceCustom = this.ResourceCustom;
			}
			if (base.Fields.IsModified(SyncMailUserSchema.InPlaceHoldsRaw))
			{
				user[ADRecipientSchema.InPlaceHoldsRaw] = this.InPlaceHoldsRaw;
			}
			if (base.Fields.IsModified(ADRecipientSchema.Certificate))
			{
				user.UserCertificate = this.UserCertificate;
			}
			if (base.Fields.IsModified(ADRecipientSchema.SMimeCertificate))
			{
				user.UserSMIMECertificate = this.UserSMimeCertificate;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000399A4 File Offset: 0x00037BA4
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				result.Identity
			});
			SyncMailUser syncMailUser = new SyncMailUser((ADUser)result);
			if (this.intendedMailboxPlanObject != null)
			{
				syncMailUser.IntendedMailboxPlanName = this.intendedMailboxPlanObject.DisplayName;
				syncMailUser.ResetChangeTracking();
			}
			base.WriteResult(syncMailUser);
			TaskLogger.LogExit();
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x000399FE File Offset: 0x00037BFE
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(SyncMailUser).FullName;
			}
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x00039A0F File Offset: 0x00037C0F
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return SyncMailUser.FromDataObject((ADUser)dataObject);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00039A1C File Offset: 0x00037C1C
		protected override void StampChangesAfterSettingPassword()
		{
			base.StampChangesAfterSettingPassword();
			if (base.Fields.IsModified(SyncMailUserSchema.AccountDisabled))
			{
				SyncTaskHelper.SetExchangeAccountDisabled(this.DataObject, this.AccountDisabled);
			}
		}

		// Token: 0x040002CB RID: 715
		private ADObject manager;

		// Token: 0x040002CC RID: 716
		private MultiValuedProperty<ADObjectId> bypassModerationFrom;

		// Token: 0x040002CD RID: 717
		private MultiValuedProperty<ADRecipient> bypassModerationFromRecipient;

		// Token: 0x040002CE RID: 718
		private MultiValuedProperty<ADObjectId> bypassModerationFromDLMembers;

		// Token: 0x040002CF RID: 719
		private MultiValuedProperty<ADRecipient> bypassModerationFromDLMembersRecipient;

		// Token: 0x040002D0 RID: 720
		private MultiValuedProperty<ADRecipient> acceptMessagesOnlyFrom;

		// Token: 0x040002D1 RID: 721
		private MultiValuedProperty<ADRecipient> acceptMessagesOnlyFromDLMembers;

		// Token: 0x040002D2 RID: 722
		private MultiValuedProperty<ADRecipient> rejectMessagesFrom;

		// Token: 0x040002D3 RID: 723
		private MultiValuedProperty<ADRecipient> rejectMessagesFromDLMembers;

		// Token: 0x040002D4 RID: 724
		private MultiValuedProperty<ADObjectId> siteMailboxOwners;

		// Token: 0x040002D5 RID: 725
		private MultiValuedProperty<ADObjectId> siteMailboxUsers;

		// Token: 0x040002D6 RID: 726
		private ADRecipient forwardingAddress;

		// Token: 0x040002D7 RID: 727
		private ADUser intendedMailboxPlanObject;

		// Token: 0x040002D8 RID: 728
		private MultiValuedProperty<ADRecipient> grantSendOnBehalfTo;
	}
}
