using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200073B RID: 1851
	[ProvisioningObjectTag("MailUser")]
	[Serializable]
	public class MailUser : MailEnabledOrgPerson, IExternalAndEmailAddresses
	{
		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x06005987 RID: 22919 RVA: 0x0013C439 File Offset: 0x0013A639
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return MailUser.schema;
			}
		}

		// Token: 0x06005988 RID: 22920 RVA: 0x0013C440 File Offset: 0x0013A640
		public MailUser()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x0013C453 File Offset: 0x0013A653
		public MailUser(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x0600598A RID: 22922 RVA: 0x0013C45C File Offset: 0x0013A65C
		internal static MailUser FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new MailUser(dataObject);
		}

		// Token: 0x17001EA8 RID: 7848
		// (get) Token: 0x0600598B RID: 22923 RVA: 0x0013C469 File Offset: 0x0013A669
		// (set) Token: 0x0600598C RID: 22924 RVA: 0x0013C47B File Offset: 0x0013A67B
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[MailUserSchema.DeliverToMailboxAndForward];
			}
			internal set
			{
				this[MailUserSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x0600598D RID: 22925 RVA: 0x0013C48E File Offset: 0x0013A68E
		// (set) Token: 0x0600598E RID: 22926 RVA: 0x0013C4A0 File Offset: 0x0013A6A0
		[Parameter(Mandatory = false)]
		public Guid ExchangeGuid
		{
			get
			{
				return (Guid)this[MailUserSchema.ExchangeGuid];
			}
			set
			{
				this[MailUserSchema.ExchangeGuid] = value;
			}
		}

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x0600598F RID: 22927 RVA: 0x0013C4B3 File Offset: 0x0013A6B3
		// (set) Token: 0x06005990 RID: 22928 RVA: 0x0013C4C5 File Offset: 0x0013A6C5
		[Parameter(Mandatory = false)]
		public Guid? MailboxContainerGuid
		{
			get
			{
				return (Guid?)this[MailUserSchema.MailboxContainerGuid];
			}
			set
			{
				this[MailUserSchema.MailboxContainerGuid] = value;
			}
		}

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x06005991 RID: 22929 RVA: 0x0013C4D8 File Offset: 0x0013A6D8
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<Guid> AggregatedMailboxGuids
		{
			get
			{
				return (MultiValuedProperty<Guid>)this[MailUserSchema.AggregatedMailboxGuids];
			}
		}

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x06005992 RID: 22930 RVA: 0x0013C4EA File Offset: 0x0013A6EA
		// (set) Token: 0x06005993 RID: 22931 RVA: 0x0013C4FC File Offset: 0x0013A6FC
		[Parameter(Mandatory = false)]
		public Guid ArchiveGuid
		{
			get
			{
				return (Guid)this[MailUserSchema.ArchiveGuid];
			}
			set
			{
				this[MailUserSchema.ArchiveGuid] = value;
			}
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x06005994 RID: 22932 RVA: 0x0013C50F File Offset: 0x0013A70F
		// (set) Token: 0x06005995 RID: 22933 RVA: 0x0013C521 File Offset: 0x0013A721
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ArchiveName
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailUserSchema.ArchiveName];
			}
			set
			{
				this[MailUserSchema.ArchiveName] = value;
			}
		}

		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x06005996 RID: 22934 RVA: 0x0013C52F File Offset: 0x0013A72F
		// (set) Token: 0x06005997 RID: 22935 RVA: 0x0013C541 File Offset: 0x0013A741
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ArchiveQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailUserSchema.ArchiveQuota];
			}
			set
			{
				this[MailUserSchema.ArchiveQuota] = value;
			}
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x06005998 RID: 22936 RVA: 0x0013C554 File Offset: 0x0013A754
		// (set) Token: 0x06005999 RID: 22937 RVA: 0x0013C566 File Offset: 0x0013A766
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ArchiveWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailUserSchema.ArchiveWarningQuota];
			}
			set
			{
				this[MailUserSchema.ArchiveWarningQuota] = value;
			}
		}

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x0600599A RID: 22938 RVA: 0x0013C579 File Offset: 0x0013A779
		// (set) Token: 0x0600599B RID: 22939 RVA: 0x0013C58B File Offset: 0x0013A78B
		public ADObjectId ForwardingAddress
		{
			get
			{
				return (ADObjectId)this[MailUserSchema.ForwardingAddress];
			}
			internal set
			{
				this[MailUserSchema.ForwardingAddress] = value;
			}
		}

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x0600599C RID: 22940 RVA: 0x0013C599 File Offset: 0x0013A799
		// (set) Token: 0x0600599D RID: 22941 RVA: 0x0013C5AB File Offset: 0x0013A7AB
		public ADObjectId ArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[MailUserSchema.ArchiveDatabase];
			}
			internal set
			{
				this[MailUserSchema.ArchiveDatabase] = value;
			}
		}

		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x0600599E RID: 22942 RVA: 0x0013C5B9 File Offset: 0x0013A7B9
		// (set) Token: 0x0600599F RID: 22943 RVA: 0x0013C5CB File Offset: 0x0013A7CB
		public ArchiveStatusFlags ArchiveStatus
		{
			get
			{
				return (ArchiveStatusFlags)this[MailUserSchema.ArchiveStatus];
			}
			internal set
			{
				this[MailUserSchema.ArchiveStatus] = value;
			}
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x060059A0 RID: 22944 RVA: 0x0013C5DE File Offset: 0x0013A7DE
		// (set) Token: 0x060059A1 RID: 22945 RVA: 0x0013C5F0 File Offset: 0x0013A7F0
		public ADObjectId DisabledArchiveDatabase
		{
			get
			{
				return (ADObjectId)this[MailUserSchema.DisabledArchiveDatabase];
			}
			internal set
			{
				this[MailUserSchema.DisabledArchiveDatabase] = value;
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x060059A2 RID: 22946 RVA: 0x0013C5FE File Offset: 0x0013A7FE
		public Guid DisabledArchiveGuid
		{
			get
			{
				return (Guid)this[MailUserSchema.DisabledArchiveGuid];
			}
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x060059A3 RID: 22947 RVA: 0x0013C610 File Offset: 0x0013A810
		public MailboxProvisioningConstraint MailboxProvisioningConstraint
		{
			get
			{
				return (MailboxProvisioningConstraint)this[MailUserSchema.MailboxProvisioningConstraint];
			}
		}

		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x060059A4 RID: 22948 RVA: 0x0013C622 File Offset: 0x0013A822
		public MultiValuedProperty<MailboxProvisioningConstraint> MailboxProvisioningPreferences
		{
			get
			{
				return (MultiValuedProperty<MailboxProvisioningConstraint>)this[MailUserSchema.MailboxProvisioningPreferences];
			}
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x060059A5 RID: 22949 RVA: 0x0013C634 File Offset: 0x0013A834
		public UserAccountControlFlags ExchangeUserAccountControl
		{
			get
			{
				return (UserAccountControlFlags)this[MailUserSchema.ExchangeUserAccountControl];
			}
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x060059A6 RID: 22950 RVA: 0x0013C646 File Offset: 0x0013A846
		// (set) Token: 0x060059A7 RID: 22951 RVA: 0x0013C658 File Offset: 0x0013A858
		[Parameter(Mandatory = false)]
		public ProxyAddress ExternalEmailAddress
		{
			get
			{
				return (ProxyAddress)this[MailUserSchema.ExternalEmailAddress];
			}
			set
			{
				this[MailUserSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x060059A8 RID: 22952 RVA: 0x0013C666 File Offset: 0x0013A866
		// (set) Token: 0x060059A9 RID: 22953 RVA: 0x0013C678 File Offset: 0x0013A878
		[Parameter(Mandatory = false)]
		public bool UsePreferMessageFormat
		{
			get
			{
				return (bool)this[MailUserSchema.UsePreferMessageFormat];
			}
			set
			{
				this[MailUserSchema.UsePreferMessageFormat] = value;
			}
		}

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x060059AA RID: 22954 RVA: 0x0013C68B File Offset: 0x0013A88B
		// (set) Token: 0x060059AB RID: 22955 RVA: 0x0013C69D File Offset: 0x0013A89D
		[Parameter(Mandatory = false)]
		public SmtpAddress JournalArchiveAddress
		{
			get
			{
				return (SmtpAddress)this[MailUserSchema.JournalArchiveAddress];
			}
			set
			{
				this[MailUserSchema.JournalArchiveAddress] = value;
			}
		}

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x0013C6B0 File Offset: 0x0013A8B0
		// (set) Token: 0x060059AD RID: 22957 RVA: 0x0013C6C2 File Offset: 0x0013A8C2
		[Parameter(Mandatory = false)]
		public MessageFormat MessageFormat
		{
			get
			{
				return (MessageFormat)this[MailUserSchema.MessageFormat];
			}
			set
			{
				this[MailUserSchema.MessageFormat] = value;
			}
		}

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x060059AE RID: 22958 RVA: 0x0013C6D5 File Offset: 0x0013A8D5
		// (set) Token: 0x060059AF RID: 22959 RVA: 0x0013C6E7 File Offset: 0x0013A8E7
		[Parameter(Mandatory = false)]
		public MessageBodyFormat MessageBodyFormat
		{
			get
			{
				return (MessageBodyFormat)this[MailUserSchema.MessageBodyFormat];
			}
			set
			{
				this[MailUserSchema.MessageBodyFormat] = value;
			}
		}

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x060059B0 RID: 22960 RVA: 0x0013C6FA File Offset: 0x0013A8FA
		// (set) Token: 0x060059B1 RID: 22961 RVA: 0x0013C70C File Offset: 0x0013A90C
		[Parameter(Mandatory = false)]
		public MacAttachmentFormat MacAttachmentFormat
		{
			get
			{
				return (MacAttachmentFormat)this[MailUserSchema.MacAttachmentFormat];
			}
			set
			{
				this[MailUserSchema.MacAttachmentFormat] = value;
			}
		}

		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x060059B2 RID: 22962 RVA: 0x0013C71F File Offset: 0x0013A91F
		public MultiValuedProperty<string> ProtocolSettings
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailUserSchema.ProtocolSettings];
			}
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x060059B3 RID: 22963 RVA: 0x0013C731 File Offset: 0x0013A931
		// (set) Token: 0x060059B4 RID: 22964 RVA: 0x0013C743 File Offset: 0x0013A943
		[Parameter(Mandatory = false)]
		public Unlimited<int> RecipientLimits
		{
			get
			{
				return (Unlimited<int>)this[MailUserSchema.RecipientLimits];
			}
			set
			{
				this[MailUserSchema.RecipientLimits] = value;
			}
		}

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x060059B5 RID: 22965 RVA: 0x0013C756 File Offset: 0x0013A956
		// (set) Token: 0x060059B6 RID: 22966 RVA: 0x0013C768 File Offset: 0x0013A968
		[Parameter(Mandatory = false)]
		public string SamAccountName
		{
			get
			{
				return (string)this[MailUserSchema.SamAccountName];
			}
			set
			{
				this[MailUserSchema.SamAccountName] = value;
			}
		}

		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x060059B7 RID: 22967 RVA: 0x0013C776 File Offset: 0x0013A976
		// (set) Token: 0x060059B8 RID: 22968 RVA: 0x0013C788 File Offset: 0x0013A988
		[Parameter(Mandatory = false)]
		public UseMapiRichTextFormat UseMapiRichTextFormat
		{
			get
			{
				return (UseMapiRichTextFormat)this[MailUserSchema.UseMapiRichTextFormat];
			}
			set
			{
				this[MailUserSchema.UseMapiRichTextFormat] = value;
			}
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x060059B9 RID: 22969 RVA: 0x0013C79B File Offset: 0x0013A99B
		// (set) Token: 0x060059BA RID: 22970 RVA: 0x0013C7AD File Offset: 0x0013A9AD
		[Parameter(Mandatory = false)]
		public string UserPrincipalName
		{
			get
			{
				return (string)this[MailUserSchema.UserPrincipalName];
			}
			set
			{
				this[MailUserSchema.UserPrincipalName] = value;
			}
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x060059BB RID: 22971 RVA: 0x0013C7BB File Offset: 0x0013A9BB
		internal NetID NetID
		{
			get
			{
				return (NetID)this[MailUserSchema.NetID];
			}
		}

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x060059BC RID: 22972 RVA: 0x0013C7CD File Offset: 0x0013A9CD
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x060059BD RID: 22973 RVA: 0x0013C7D4 File Offset: 0x0013A9D4
		// (set) Token: 0x060059BE RID: 22974 RVA: 0x0013C7E6 File Offset: 0x0013A9E6
		[Parameter(Mandatory = false)]
		public SmtpAddress WindowsLiveID
		{
			get
			{
				return (SmtpAddress)this[MailUserSchema.WindowsLiveID];
			}
			set
			{
				this[MailUserSchema.WindowsLiveID] = value;
			}
		}

		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x060059BF RID: 22975 RVA: 0x0013C7F9 File Offset: 0x0013A9F9
		// (set) Token: 0x060059C0 RID: 22976 RVA: 0x0013C801 File Offset: 0x0013AA01
		[Parameter(Mandatory = false)]
		public SmtpAddress MicrosoftOnlineServicesID
		{
			get
			{
				return this.WindowsLiveID;
			}
			set
			{
				this.WindowsLiveID = value;
			}
		}

		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x060059C1 RID: 22977 RVA: 0x0013C80A File Offset: 0x0013AA0A
		public ADObjectId MailboxMoveTargetMDB
		{
			get
			{
				return (ADObjectId)this[MailUserSchema.MailboxMoveTargetMDB];
			}
		}

		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x060059C2 RID: 22978 RVA: 0x0013C81C File Offset: 0x0013AA1C
		public ADObjectId MailboxMoveSourceMDB
		{
			get
			{
				return (ADObjectId)this[MailUserSchema.MailboxMoveSourceMDB];
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x0013C82E File Offset: 0x0013AA2E
		public RequestFlags MailboxMoveFlags
		{
			get
			{
				return (RequestFlags)this[MailUserSchema.MailboxMoveFlags];
			}
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x0013C840 File Offset: 0x0013AA40
		public string MailboxMoveRemoteHostName
		{
			get
			{
				return (string)this[MailUserSchema.MailboxMoveRemoteHostName];
			}
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x060059C5 RID: 22981 RVA: 0x0013C852 File Offset: 0x0013AA52
		public string MailboxMoveBatchName
		{
			get
			{
				return (string)this[MailUserSchema.MailboxMoveBatchName];
			}
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x0013C864 File Offset: 0x0013AA64
		public RequestStatus MailboxMoveStatus
		{
			get
			{
				return (RequestStatus)this[MailUserSchema.MailboxMoveStatus];
			}
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x060059C7 RID: 22983 RVA: 0x0013C876 File Offset: 0x0013AA76
		public string MailboxRelease
		{
			get
			{
				return (string)this[MailUserSchema.MailboxRelease];
			}
		}

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x060059C8 RID: 22984 RVA: 0x0013C888 File Offset: 0x0013AA88
		public string ArchiveRelease
		{
			get
			{
				return (string)this[MailUserSchema.ArchiveRelease];
			}
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x060059C9 RID: 22985 RVA: 0x0013C89A File Offset: 0x0013AA9A
		// (set) Token: 0x060059CA RID: 22986 RVA: 0x0013C8AC File Offset: 0x0013AAAC
		[Parameter(Mandatory = false)]
		public string ImmutableId
		{
			get
			{
				return (string)this[MailUserSchema.ImmutableId];
			}
			set
			{
				this[MailUserSchema.ImmutableId] = value;
			}
		}

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x060059CB RID: 22987 RVA: 0x0013C8BA File Offset: 0x0013AABA
		public MultiValuedProperty<Capability> PersistedCapabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)this[MailUserSchema.PersistedCapabilities];
			}
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x060059CC RID: 22988 RVA: 0x0013C8CC File Offset: 0x0013AACC
		// (set) Token: 0x060059CD RID: 22989 RVA: 0x0013C8DE File Offset: 0x0013AADE
		[Parameter(Mandatory = false)]
		public bool? SKUAssigned
		{
			get
			{
				return (bool?)this[MailUserSchema.SKUAssigned];
			}
			set
			{
				this[MailUserSchema.SKUAssigned] = value;
			}
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x060059CE RID: 22990 RVA: 0x0013C8F1 File Offset: 0x0013AAF1
		// (set) Token: 0x060059CF RID: 22991 RVA: 0x0013C903 File Offset: 0x0013AB03
		[Parameter(Mandatory = false)]
		public bool ResetPasswordOnNextLogon
		{
			get
			{
				return (bool)this[MailUserSchema.ResetPasswordOnNextLogon];
			}
			set
			{
				this[MailUserSchema.ResetPasswordOnNextLogon] = value;
			}
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x060059D0 RID: 22992 RVA: 0x0013C916 File Offset: 0x0013AB16
		public DateTime? WhenMailboxCreated
		{
			get
			{
				return (DateTime?)this[MailUserSchema.WhenMailboxCreated];
			}
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x060059D1 RID: 22993 RVA: 0x0013C928 File Offset: 0x0013AB28
		// (set) Token: 0x060059D2 RID: 22994 RVA: 0x0013C93A File Offset: 0x0013AB3A
		[Parameter(Mandatory = false)]
		public bool LitigationHoldEnabled
		{
			get
			{
				return (bool)this[MailUserSchema.LitigationHoldEnabled];
			}
			set
			{
				this[MailUserSchema.LitigationHoldEnabled] = value;
			}
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x060059D3 RID: 22995 RVA: 0x0013C94D File Offset: 0x0013AB4D
		// (set) Token: 0x060059D4 RID: 22996 RVA: 0x0013C95F File Offset: 0x0013AB5F
		[Parameter(Mandatory = false)]
		public bool SingleItemRecoveryEnabled
		{
			get
			{
				return (bool)this[MailUserSchema.SingleItemRecoveryEnabled];
			}
			set
			{
				this[MailUserSchema.SingleItemRecoveryEnabled] = value;
			}
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x060059D5 RID: 22997 RVA: 0x0013C972 File Offset: 0x0013AB72
		// (set) Token: 0x060059D6 RID: 22998 RVA: 0x0013C984 File Offset: 0x0013AB84
		[Parameter(Mandatory = false)]
		public bool RetentionHoldEnabled
		{
			get
			{
				return (bool)this[MailUserSchema.ElcExpirationSuspensionEnabled];
			}
			set
			{
				this[MailUserSchema.ElcExpirationSuspensionEnabled] = value;
			}
		}

		// Token: 0x17001ED7 RID: 7895
		// (get) Token: 0x060059D7 RID: 22999 RVA: 0x0013C997 File Offset: 0x0013AB97
		// (set) Token: 0x060059D8 RID: 23000 RVA: 0x0013C9A9 File Offset: 0x0013ABA9
		[Parameter(Mandatory = false)]
		public DateTime? EndDateForRetentionHold
		{
			get
			{
				return (DateTime?)this[MailUserSchema.ElcExpirationSuspensionEndDate];
			}
			set
			{
				this[MailUserSchema.ElcExpirationSuspensionEndDate] = value;
			}
		}

		// Token: 0x17001ED8 RID: 7896
		// (get) Token: 0x060059D9 RID: 23001 RVA: 0x0013C9BC File Offset: 0x0013ABBC
		// (set) Token: 0x060059DA RID: 23002 RVA: 0x0013C9CE File Offset: 0x0013ABCE
		[Parameter(Mandatory = false)]
		public DateTime? StartDateForRetentionHold
		{
			get
			{
				return (DateTime?)this[MailUserSchema.ElcExpirationSuspensionStartDate];
			}
			set
			{
				this[MailUserSchema.ElcExpirationSuspensionStartDate] = value;
			}
		}

		// Token: 0x17001ED9 RID: 7897
		// (get) Token: 0x060059DB RID: 23003 RVA: 0x0013C9E1 File Offset: 0x0013ABE1
		// (set) Token: 0x060059DC RID: 23004 RVA: 0x0013C9F3 File Offset: 0x0013ABF3
		[Parameter(Mandatory = false)]
		public string RetentionComment
		{
			get
			{
				return (string)this[MailUserSchema.RetentionComment];
			}
			set
			{
				this[MailUserSchema.RetentionComment] = value;
			}
		}

		// Token: 0x17001EDA RID: 7898
		// (get) Token: 0x060059DD RID: 23005 RVA: 0x0013CA01 File Offset: 0x0013AC01
		// (set) Token: 0x060059DE RID: 23006 RVA: 0x0013CA13 File Offset: 0x0013AC13
		[Parameter(Mandatory = false)]
		public string RetentionUrl
		{
			get
			{
				return (string)this[MailUserSchema.RetentionUrl];
			}
			set
			{
				this[MailUserSchema.RetentionUrl] = value;
			}
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x060059DF RID: 23007 RVA: 0x0013CA21 File Offset: 0x0013AC21
		// (set) Token: 0x060059E0 RID: 23008 RVA: 0x0013CA33 File Offset: 0x0013AC33
		[Parameter(Mandatory = false)]
		public DateTime? LitigationHoldDate
		{
			get
			{
				return (DateTime?)this[MailUserSchema.LitigationHoldDate];
			}
			set
			{
				this[MailUserSchema.LitigationHoldDate] = value;
			}
		}

		// Token: 0x17001EDC RID: 7900
		// (get) Token: 0x060059E1 RID: 23009 RVA: 0x0013CA46 File Offset: 0x0013AC46
		// (set) Token: 0x060059E2 RID: 23010 RVA: 0x0013CA58 File Offset: 0x0013AC58
		[Parameter(Mandatory = false)]
		public string LitigationHoldOwner
		{
			get
			{
				return (string)this[MailUserSchema.LitigationHoldOwner];
			}
			set
			{
				this[MailUserSchema.LitigationHoldOwner] = value;
			}
		}

		// Token: 0x17001EDD RID: 7901
		// (get) Token: 0x060059E3 RID: 23011 RVA: 0x0013CA66 File Offset: 0x0013AC66
		// (set) Token: 0x060059E4 RID: 23012 RVA: 0x0013CA78 File Offset: 0x0013AC78
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan RetainDeletedItemsFor
		{
			get
			{
				return (EnhancedTimeSpan)this[MailUserSchema.RetainDeletedItemsFor];
			}
			set
			{
				this[MailUserSchema.RetainDeletedItemsFor] = value;
			}
		}

		// Token: 0x17001EDE RID: 7902
		// (get) Token: 0x060059E5 RID: 23013 RVA: 0x0013CA8B File Offset: 0x0013AC8B
		// (set) Token: 0x060059E6 RID: 23014 RVA: 0x0013CA9D File Offset: 0x0013AC9D
		[Parameter(Mandatory = false)]
		public bool CalendarVersionStoreDisabled
		{
			get
			{
				return (bool)this[MailUserSchema.CalendarVersionStoreDisabled];
			}
			set
			{
				this[MailUserSchema.CalendarVersionStoreDisabled] = value;
			}
		}

		// Token: 0x17001EDF RID: 7903
		// (get) Token: 0x060059E7 RID: 23015 RVA: 0x0013CAB0 File Offset: 0x0013ACB0
		// (set) Token: 0x060059E8 RID: 23016 RVA: 0x0013CAC2 File Offset: 0x0013ACC2
		[Parameter(Mandatory = false)]
		public CountryInfo UsageLocation
		{
			get
			{
				return (CountryInfo)this[MailUserSchema.UsageLocation];
			}
			set
			{
				this[MailUserSchema.UsageLocation] = value;
			}
		}

		// Token: 0x17001EE0 RID: 7904
		// (get) Token: 0x060059E9 RID: 23017 RVA: 0x0013CAD0 File Offset: 0x0013ACD0
		// (set) Token: 0x060059EA RID: 23018 RVA: 0x0013CAE2 File Offset: 0x0013ACE2
		public bool IsSoftDeletedByRemove
		{
			get
			{
				return (bool)this[MailboxSchema.IsSoftDeletedByRemove];
			}
			set
			{
				this[MailboxSchema.IsSoftDeletedByRemove] = value;
			}
		}

		// Token: 0x17001EE1 RID: 7905
		// (get) Token: 0x060059EB RID: 23019 RVA: 0x0013CAF5 File Offset: 0x0013ACF5
		// (set) Token: 0x060059EC RID: 23020 RVA: 0x0013CB07 File Offset: 0x0013AD07
		public bool IsSoftDeletedByDisable
		{
			get
			{
				return (bool)this[MailboxSchema.IsSoftDeletedByDisable];
			}
			set
			{
				this[MailboxSchema.IsSoftDeletedByDisable] = value;
			}
		}

		// Token: 0x17001EE2 RID: 7906
		// (get) Token: 0x060059ED RID: 23021 RVA: 0x0013CB1A File Offset: 0x0013AD1A
		// (set) Token: 0x060059EE RID: 23022 RVA: 0x0013CB2C File Offset: 0x0013AD2C
		public DateTime? WhenSoftDeleted
		{
			get
			{
				return (DateTime?)this[MailboxSchema.WhenSoftDeleted];
			}
			set
			{
				this[MailboxSchema.WhenSoftDeleted] = value;
			}
		}

		// Token: 0x17001EE3 RID: 7907
		// (get) Token: 0x060059EF RID: 23023 RVA: 0x0013CB3F File Offset: 0x0013AD3F
		// (set) Token: 0x060059F0 RID: 23024 RVA: 0x0013CB51 File Offset: 0x0013AD51
		public MultiValuedProperty<string> InPlaceHolds
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailboxSchema.InPlaceHolds];
			}
			set
			{
				this[MailboxSchema.InPlaceHolds] = value;
			}
		}

		// Token: 0x17001EE4 RID: 7908
		// (get) Token: 0x060059F1 RID: 23025 RVA: 0x0013CB5F File Offset: 0x0013AD5F
		// (set) Token: 0x060059F2 RID: 23026 RVA: 0x0013CB71 File Offset: 0x0013AD71
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> RecoverableItemsQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailUserSchema.RecoverableItemsQuota];
			}
			set
			{
				this[MailUserSchema.RecoverableItemsQuota] = value;
			}
		}

		// Token: 0x17001EE5 RID: 7909
		// (get) Token: 0x060059F3 RID: 23027 RVA: 0x0013CB84 File Offset: 0x0013AD84
		// (set) Token: 0x060059F4 RID: 23028 RVA: 0x0013CB96 File Offset: 0x0013AD96
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> RecoverableItemsWarningQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailUserSchema.RecoverableItemsWarningQuota];
			}
			set
			{
				this[MailUserSchema.RecoverableItemsWarningQuota] = value;
			}
		}

		// Token: 0x17001EE6 RID: 7910
		// (get) Token: 0x060059F5 RID: 23029 RVA: 0x0013CBA9 File Offset: 0x0013ADA9
		// (set) Token: 0x060059F6 RID: 23030 RVA: 0x0013CBBB File Offset: 0x0013ADBB
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MailUserSchema.UserCertificate];
			}
			set
			{
				this[MailUserSchema.UserCertificate] = value;
			}
		}

		// Token: 0x17001EE7 RID: 7911
		// (get) Token: 0x060059F7 RID: 23031 RVA: 0x0013CBC9 File Offset: 0x0013ADC9
		// (set) Token: 0x060059F8 RID: 23032 RVA: 0x0013CBDB File Offset: 0x0013ADDB
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<byte[]> UserSMimeCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MailUserSchema.UserSMimeCertificate];
			}
			set
			{
				this[MailUserSchema.UserSMimeCertificate] = value;
			}
		}

		// Token: 0x04003C55 RID: 15445
		private static MailUserSchema schema = ObjectSchema.GetInstance<MailUserSchema>();
	}
}
