using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000839 RID: 2105
	internal class SyncUser : SyncOrgPerson
	{
		// Token: 0x06006843 RID: 26691 RVA: 0x0016FCC4 File Offset: 0x0016DEC4
		public SyncUser(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x170024E7 RID: 9447
		// (get) Token: 0x06006844 RID: 26692 RVA: 0x0016FCCD File Offset: 0x0016DECD
		public override SyncObjectSchema Schema
		{
			get
			{
				return SyncUser.schema;
			}
		}

		// Token: 0x170024E8 RID: 9448
		// (get) Token: 0x06006845 RID: 26693 RVA: 0x0016FCD4 File Offset: 0x0016DED4
		internal override DirectoryObjectClass ObjectClass
		{
			get
			{
				return DirectoryObjectClass.User;
			}
		}

		// Token: 0x06006846 RID: 26694 RVA: 0x0016FCD8 File Offset: 0x0016DED8
		protected override DirectoryObject CreateDirectoryObject()
		{
			return new User();
		}

		// Token: 0x170024E9 RID: 9449
		// (get) Token: 0x06006847 RID: 26695 RVA: 0x0016FCDF File Offset: 0x0016DEDF
		// (set) Token: 0x06006848 RID: 26696 RVA: 0x0016FCF1 File Offset: 0x0016DEF1
		public SyncProperty<Guid> ArchiveGuid
		{
			get
			{
				return (SyncProperty<Guid>)base[SyncUserSchema.ArchiveGuid];
			}
			set
			{
				base[SyncUserSchema.ArchiveGuid] = value;
			}
		}

		// Token: 0x170024EA RID: 9450
		// (get) Token: 0x06006849 RID: 26697 RVA: 0x0016FCFF File Offset: 0x0016DEFF
		// (set) Token: 0x0600684A RID: 26698 RVA: 0x0016FD11 File Offset: 0x0016DF11
		public SyncProperty<MultiValuedProperty<string>> ArchiveName
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncUserSchema.ArchiveName];
			}
			set
			{
				base[SyncUserSchema.ArchiveName] = value;
			}
		}

		// Token: 0x170024EB RID: 9451
		// (get) Token: 0x0600684B RID: 26699 RVA: 0x0016FD1F File Offset: 0x0016DF1F
		// (set) Token: 0x0600684C RID: 26700 RVA: 0x0016FD31 File Offset: 0x0016DF31
		public SyncProperty<MultiValuedProperty<AssignedPlanValue>> AssignedPlan
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<AssignedPlanValue>>)base[SyncUserSchema.AssignedPlan];
			}
			set
			{
				base[SyncUserSchema.AssignedPlan] = value;
			}
		}

		// Token: 0x170024EC RID: 9452
		// (get) Token: 0x0600684D RID: 26701 RVA: 0x0016FD3F File Offset: 0x0016DF3F
		// (set) Token: 0x0600684E RID: 26702 RVA: 0x0016FD51 File Offset: 0x0016DF51
		public byte[] CloudMsExchBlockedSendersHash
		{
			get
			{
				return (byte[])base[SyncUserSchema.CloudMsExchBlockedSendersHash];
			}
			set
			{
				base[SyncUserSchema.CloudMsExchBlockedSendersHash] = value;
			}
		}

		// Token: 0x170024ED RID: 9453
		// (get) Token: 0x0600684F RID: 26703 RVA: 0x0016FD5F File Offset: 0x0016DF5F
		// (set) Token: 0x06006850 RID: 26704 RVA: 0x0016FD71 File Offset: 0x0016DF71
		public byte[] CloudMsExchSafeRecipientsHash
		{
			get
			{
				return (byte[])base[SyncUserSchema.CloudMsExchSafeRecipientsHash];
			}
			set
			{
				base[SyncUserSchema.CloudMsExchSafeRecipientsHash] = value;
			}
		}

		// Token: 0x170024EE RID: 9454
		// (get) Token: 0x06006851 RID: 26705 RVA: 0x0016FD7F File Offset: 0x0016DF7F
		// (set) Token: 0x06006852 RID: 26706 RVA: 0x0016FD91 File Offset: 0x0016DF91
		public byte[] CloudMsExchSafeSendersHash
		{
			get
			{
				return (byte[])base[SyncUserSchema.CloudMsExchSafeSendersHash];
			}
			set
			{
				base[SyncUserSchema.CloudMsExchSafeSendersHash] = value;
			}
		}

		// Token: 0x170024EF RID: 9455
		// (get) Token: 0x06006853 RID: 26707 RVA: 0x0016FD9F File Offset: 0x0016DF9F
		// (set) Token: 0x06006854 RID: 26708 RVA: 0x0016FDB1 File Offset: 0x0016DFB1
		public SyncProperty<bool> DeliverToMailboxAndForward
		{
			get
			{
				return (SyncProperty<bool>)base[SyncUserSchema.DeliverToMailboxAndForward];
			}
			set
			{
				base[SyncUserSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x170024F0 RID: 9456
		// (get) Token: 0x06006855 RID: 26709 RVA: 0x0016FDBF File Offset: 0x0016DFBF
		// (set) Token: 0x06006856 RID: 26710 RVA: 0x0016FDD1 File Offset: 0x0016DFD1
		public SyncProperty<Guid> ExchangeGuid
		{
			get
			{
				return (SyncProperty<Guid>)base[SyncUserSchema.ExchangeGuid];
			}
			set
			{
				base[SyncUserSchema.ExchangeGuid] = value;
			}
		}

		// Token: 0x170024F1 RID: 9457
		// (get) Token: 0x06006857 RID: 26711 RVA: 0x0016FDDF File Offset: 0x0016DFDF
		// (set) Token: 0x06006858 RID: 26712 RVA: 0x0016FDF1 File Offset: 0x0016DFF1
		public SyncProperty<SyncLink> Manager
		{
			get
			{
				return (SyncProperty<SyncLink>)base[SyncUserSchema.Manager];
			}
			set
			{
				base[SyncUserSchema.Manager] = value;
			}
		}

		// Token: 0x170024F2 RID: 9458
		// (get) Token: 0x06006859 RID: 26713 RVA: 0x0016FDFF File Offset: 0x0016DFFF
		// (set) Token: 0x0600685A RID: 26714 RVA: 0x0016FE11 File Offset: 0x0016E011
		public SyncProperty<NetID> NetID
		{
			get
			{
				return (SyncProperty<NetID>)base[SyncUserSchema.NetID];
			}
			set
			{
				base[SyncUserSchema.NetID] = value;
			}
		}

		// Token: 0x170024F3 RID: 9459
		// (get) Token: 0x0600685B RID: 26715 RVA: 0x0016FE1F File Offset: 0x0016E01F
		// (set) Token: 0x0600685C RID: 26716 RVA: 0x0016FE31 File Offset: 0x0016E031
		public SyncProperty<byte[]> Picture
		{
			get
			{
				return (SyncProperty<byte[]>)base[SyncUserSchema.Picture];
			}
			set
			{
				base[SyncUserSchema.Picture] = value;
			}
		}

		// Token: 0x170024F4 RID: 9460
		// (get) Token: 0x0600685D RID: 26717 RVA: 0x0016FE3F File Offset: 0x0016E03F
		public int RecipientSoftDeletedStatus
		{
			get
			{
				return (int)base[SyncUserSchema.RecipientSoftDeletedStatus];
			}
		}

		// Token: 0x170024F5 RID: 9461
		// (get) Token: 0x0600685E RID: 26718 RVA: 0x0016FE51 File Offset: 0x0016E051
		// (set) Token: 0x0600685F RID: 26719 RVA: 0x0016FE63 File Offset: 0x0016E063
		public SyncProperty<DateTime?> WhenSoftDeleted
		{
			get
			{
				return (SyncProperty<DateTime?>)base[SyncUserSchema.WhenSoftDeleted];
			}
			set
			{
				base[SyncUserSchema.WhenSoftDeleted] = value;
			}
		}

		// Token: 0x170024F6 RID: 9462
		// (get) Token: 0x06006860 RID: 26720 RVA: 0x0016FE71 File Offset: 0x0016E071
		// (set) Token: 0x06006861 RID: 26721 RVA: 0x0016FE83 File Offset: 0x0016E083
		public SyncProperty<DateTime> MSExchUserCreatedTimestamp
		{
			get
			{
				return (SyncProperty<DateTime>)base[SyncUserSchema.MSExchUserCreatedTimestamp];
			}
			set
			{
				base[SyncUserSchema.MSExchUserCreatedTimestamp] = value;
			}
		}

		// Token: 0x170024F7 RID: 9463
		// (get) Token: 0x06006862 RID: 26722 RVA: 0x0016FE91 File Offset: 0x0016E091
		// (set) Token: 0x06006863 RID: 26723 RVA: 0x0016FEA3 File Offset: 0x0016E0A3
		public SyncProperty<MultiValuedProperty<ProvisionedPlanValue>> ProvisionedPlan
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<ProvisionedPlanValue>>)base[SyncUserSchema.ProvisionedPlan];
			}
			set
			{
				base[SyncUserSchema.ProvisionedPlan] = value;
			}
		}

		// Token: 0x170024F8 RID: 9464
		// (get) Token: 0x06006864 RID: 26724 RVA: 0x0016FEB1 File Offset: 0x0016E0B1
		// (set) Token: 0x06006865 RID: 26725 RVA: 0x0016FEC3 File Offset: 0x0016E0C3
		public SyncProperty<int?> ResourceCapacity
		{
			get
			{
				return (SyncProperty<int?>)base[SyncUserSchema.ResourceCapacity];
			}
			set
			{
				base[SyncUserSchema.ResourceCapacity] = value;
			}
		}

		// Token: 0x170024F9 RID: 9465
		// (get) Token: 0x06006866 RID: 26726 RVA: 0x0016FED1 File Offset: 0x0016E0D1
		// (set) Token: 0x06006867 RID: 26727 RVA: 0x0016FEE3 File Offset: 0x0016E0E3
		public SyncProperty<string> ResourcePropertiesDisplay
		{
			get
			{
				return (SyncProperty<string>)base[SyncUserSchema.ResourcePropertiesDisplay];
			}
			set
			{
				base[SyncUserSchema.ResourcePropertiesDisplay] = value;
			}
		}

		// Token: 0x170024FA RID: 9466
		// (get) Token: 0x06006868 RID: 26728 RVA: 0x0016FEF1 File Offset: 0x0016E0F1
		// (set) Token: 0x06006869 RID: 26729 RVA: 0x0016FF03 File Offset: 0x0016E103
		public SyncProperty<MultiValuedProperty<string>> ResourceMetaData
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncUserSchema.ResourceMetaData];
			}
			set
			{
				base[SyncUserSchema.ResourceMetaData] = value;
			}
		}

		// Token: 0x170024FB RID: 9467
		// (get) Token: 0x0600686A RID: 26730 RVA: 0x0016FF11 File Offset: 0x0016E111
		// (set) Token: 0x0600686B RID: 26731 RVA: 0x0016FF23 File Offset: 0x0016E123
		public SyncProperty<MultiValuedProperty<string>> ResourceSearchProperties
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncUserSchema.ResourceSearchProperties];
			}
			set
			{
				base[SyncUserSchema.ResourceSearchProperties] = value;
			}
		}

		// Token: 0x170024FC RID: 9468
		// (get) Token: 0x0600686C RID: 26732 RVA: 0x0016FF31 File Offset: 0x0016E131
		// (set) Token: 0x0600686D RID: 26733 RVA: 0x0016FF43 File Offset: 0x0016E143
		public SyncProperty<MultiValuedProperty<ServiceInfoValue>> ServiceInfo
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<ServiceInfoValue>>)base[SyncUserSchema.ServiceInfo];
			}
			set
			{
				base[SyncUserSchema.ServiceInfo] = value;
			}
		}

		// Token: 0x170024FD RID: 9469
		// (get) Token: 0x0600686E RID: 26734 RVA: 0x0016FF51 File Offset: 0x0016E151
		// (set) Token: 0x0600686F RID: 26735 RVA: 0x0016FF63 File Offset: 0x0016E163
		public SyncProperty<SmtpAddress> WindowsLiveID
		{
			get
			{
				return (SyncProperty<SmtpAddress>)base[SyncUserSchema.WindowsLiveID];
			}
			set
			{
				base[SyncUserSchema.WindowsLiveID] = value;
			}
		}

		// Token: 0x170024FE RID: 9470
		// (get) Token: 0x06006870 RID: 26736 RVA: 0x0016FF71 File Offset: 0x0016E171
		// (set) Token: 0x06006871 RID: 26737 RVA: 0x0016FF83 File Offset: 0x0016E183
		public SyncProperty<RemoteRecipientType> RemoteRecipientType
		{
			get
			{
				return (SyncProperty<RemoteRecipientType>)base[SyncUserSchema.RemoteRecipientType];
			}
			set
			{
				base[SyncUserSchema.RemoteRecipientType] = value;
			}
		}

		// Token: 0x170024FF RID: 9471
		// (get) Token: 0x06006872 RID: 26738 RVA: 0x0016FF91 File Offset: 0x0016E191
		// (set) Token: 0x06006873 RID: 26739 RVA: 0x0016FFA3 File Offset: 0x0016E1A3
		public SyncProperty<CountryInfo> UsageLocation
		{
			get
			{
				return (SyncProperty<CountryInfo>)base[SyncUserSchema.UsageLocation];
			}
			set
			{
				base[SyncUserSchema.UsageLocation] = value;
			}
		}

		// Token: 0x17002500 RID: 9472
		// (get) Token: 0x06006874 RID: 26740 RVA: 0x0016FFB1 File Offset: 0x0016E1B1
		public SyncProperty<Capability> SKUCapability
		{
			get
			{
				return (SyncProperty<Capability>)base[SyncUserSchema.SKUCapability];
			}
		}

		// Token: 0x17002501 RID: 9473
		// (get) Token: 0x06006875 RID: 26741 RVA: 0x0016FFC3 File Offset: 0x0016E1C3
		public SyncProperty<MultiValuedProperty<Capability>> AddOnSKUCapability
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<Capability>>)base[SyncUserSchema.AddOnSKUCapability];
			}
		}

		// Token: 0x17002502 RID: 9474
		// (get) Token: 0x06006876 RID: 26742 RVA: 0x0016FFD5 File Offset: 0x0016E1D5
		public SyncProperty<AssignedCapabilityStatus?> SKUCapabilityStatus
		{
			get
			{
				return (SyncProperty<AssignedCapabilityStatus?>)base[SyncUserSchema.SKUCapabilityStatus];
			}
		}

		// Token: 0x17002503 RID: 9475
		// (get) Token: 0x06006877 RID: 26743 RVA: 0x0016FFE7 File Offset: 0x0016E1E7
		public SyncProperty<bool> SKUAssigned
		{
			get
			{
				return (SyncProperty<bool>)base[SyncUserSchema.SKUAssigned];
			}
		}

		// Token: 0x17002504 RID: 9476
		// (get) Token: 0x06006878 RID: 26744 RVA: 0x0016FFF9 File Offset: 0x0016E1F9
		// (set) Token: 0x06006879 RID: 26745 RVA: 0x0017000B File Offset: 0x0016E20B
		public SyncProperty<DirectoryPropertyReferenceAddressList> SiteMailboxOwners
		{
			get
			{
				return (SyncProperty<DirectoryPropertyReferenceAddressList>)base[SyncUserSchema.SiteMailboxOwners];
			}
			set
			{
				base[SyncUserSchema.SiteMailboxOwners] = value;
			}
		}

		// Token: 0x17002505 RID: 9477
		// (get) Token: 0x0600687A RID: 26746 RVA: 0x00170019 File Offset: 0x0016E219
		// (set) Token: 0x0600687B RID: 26747 RVA: 0x0017002B File Offset: 0x0016E22B
		public SyncProperty<MultiValuedProperty<SyncLink>> SiteMailboxUsers
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncUserSchema.SiteMailboxUsers];
			}
			set
			{
				base[SyncUserSchema.SiteMailboxUsers] = value;
			}
		}

		// Token: 0x17002506 RID: 9478
		// (get) Token: 0x0600687C RID: 26748 RVA: 0x00170039 File Offset: 0x0016E239
		// (set) Token: 0x0600687D RID: 26749 RVA: 0x0017004B File Offset: 0x0016E24B
		public SyncProperty<DateTime?> SiteMailboxClosedTime
		{
			get
			{
				return (SyncProperty<DateTime?>)base[SyncUserSchema.SiteMailboxClosedTime];
			}
			set
			{
				base[SyncUserSchema.SiteMailboxClosedTime] = value;
			}
		}

		// Token: 0x17002507 RID: 9479
		// (get) Token: 0x0600687E RID: 26750 RVA: 0x00170059 File Offset: 0x0016E259
		// (set) Token: 0x0600687F RID: 26751 RVA: 0x0017006B File Offset: 0x0016E26B
		public SyncProperty<Uri> SharePointUrl
		{
			get
			{
				return (SyncProperty<Uri>)base[SyncUserSchema.SharePointUrl];
			}
			set
			{
				base[SyncUserSchema.SharePointUrl] = value;
			}
		}

		// Token: 0x17002508 RID: 9480
		// (get) Token: 0x06006880 RID: 26752 RVA: 0x00170079 File Offset: 0x0016E279
		// (set) Token: 0x06006881 RID: 26753 RVA: 0x0017008B File Offset: 0x0016E28B
		public SyncProperty<MultiValuedProperty<string>> InPlaceHoldsRaw
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncUserSchema.InPlaceHoldsRaw];
			}
			set
			{
				base[SyncUserSchema.InPlaceHoldsRaw] = value;
			}
		}

		// Token: 0x17002509 RID: 9481
		// (get) Token: 0x06006882 RID: 26754 RVA: 0x0017009C File Offset: 0x0016E29C
		public bool HasValidLicense
		{
			get
			{
				return this.AssignedPlan.HasValue && this.AssignedPlan.Value != null && this.AssignedPlan.Value.Count > 0 && this.SKUCapability.HasValue && this.SKUCapability.Value != Capability.None;
			}
		}

		// Token: 0x1700250A RID: 9482
		// (get) Token: 0x06006883 RID: 26755 RVA: 0x001700F8 File Offset: 0x0016E2F8
		public bool IsLicenseDeletion
		{
			get
			{
				return this.HasValidLicense && this.SKUCapabilityStatus.HasValue && this.SKUCapabilityStatus.Value == AssignedCapabilityStatus.Deleted;
			}
		}

		// Token: 0x1700250B RID: 9483
		// (get) Token: 0x06006884 RID: 26756 RVA: 0x0017013B File Offset: 0x0016E33B
		public bool HasRemoteMailboxRequest
		{
			get
			{
				return this.RemoteRecipientType.HasValue && ((this.RemoteRecipientType.Value & Microsoft.Exchange.Data.Directory.Recipient.RemoteRecipientType.ProvisionMailbox) == Microsoft.Exchange.Data.Directory.Recipient.RemoteRecipientType.ProvisionMailbox || (this.RemoteRecipientType.Value & Microsoft.Exchange.Data.Directory.Recipient.RemoteRecipientType.DeprovisionMailbox) == Microsoft.Exchange.Data.Directory.Recipient.RemoteRecipientType.DeprovisionMailbox);
			}
		}

		// Token: 0x1700250C RID: 9484
		// (get) Token: 0x06006885 RID: 26757 RVA: 0x00170172 File Offset: 0x0016E372
		// (set) Token: 0x06006886 RID: 26758 RVA: 0x00170184 File Offset: 0x0016E384
		public SyncProperty<bool?> AccountEnabled
		{
			get
			{
				return (SyncProperty<bool?>)base[SyncUserSchema.AccountEnabled];
			}
			set
			{
				base[SyncUserSchema.AccountEnabled] = value;
			}
		}

		// Token: 0x1700250D RID: 9485
		// (get) Token: 0x06006887 RID: 26759 RVA: 0x00170192 File Offset: 0x0016E392
		// (set) Token: 0x06006888 RID: 26760 RVA: 0x001701A4 File Offset: 0x0016E3A4
		public SyncProperty<DateTime?> StsRefreshTokensValidFrom
		{
			get
			{
				return (SyncProperty<DateTime?>)base[SyncUserSchema.StsRefreshTokensValidFrom];
			}
			set
			{
				base[SyncUserSchema.AccountEnabled] = value;
			}
		}

		// Token: 0x06006889 RID: 26761 RVA: 0x001701B4 File Offset: 0x0016E3B4
		public static Capability GetExchangeCapability(XmlElement xmlCapability)
		{
			Capability result;
			try
			{
				result = (Capability)Enum.Parse(typeof(Capability), CapabilityHelper.TransformCapabilityString(xmlCapability.GetAttribute("MailboxPlan").Trim()), true);
			}
			catch (ArgumentException)
			{
				result = Capability.None;
			}
			return result;
		}

		// Token: 0x0600688A RID: 26762 RVA: 0x00170204 File Offset: 0x0016E404
		internal static object SKUCapabilityGetter(IPropertyBag propertyBag)
		{
			Capability capability = Capability.None;
			AssignedPlanValue effectiveRootServicePlan = SyncUser.GetEffectiveRootServicePlan(propertyBag);
			if (effectiveRootServicePlan != null)
			{
				capability = SyncUser.GetExchangeCapability(effectiveRootServicePlan.Capability);
			}
			return capability;
		}

		// Token: 0x0600688B RID: 26763 RVA: 0x0017022F File Offset: 0x0016E42F
		internal static object AddOnSKUCapabilityGetter(IPropertyBag propertyBag)
		{
			return SyncUser.GetEffectiveAddOnSKUCapabilities(propertyBag);
		}

		// Token: 0x0600688C RID: 26764 RVA: 0x00170238 File Offset: 0x0016E438
		internal static object SKUCapabilityStatusGetter(IPropertyBag propertyBag)
		{
			AssignedCapabilityStatus? assignedCapabilityStatus = null;
			AssignedPlanValue effectiveRootServicePlan = SyncUser.GetEffectiveRootServicePlan(propertyBag);
			if (effectiveRootServicePlan != null)
			{
				assignedCapabilityStatus = new AssignedCapabilityStatus?(effectiveRootServicePlan.CapabilityStatus);
			}
			return assignedCapabilityStatus;
		}

		// Token: 0x0600688D RID: 26765 RVA: 0x0017026C File Offset: 0x0016E46C
		internal static object SKUAssignedGetter(IPropertyBag propertyBag)
		{
			AssignedCapabilityStatus? assignedCapabilityStatus = (AssignedCapabilityStatus?)SyncUser.SKUCapabilityStatusGetter(propertyBag);
			return assignedCapabilityStatus != null && assignedCapabilityStatus == AssignedCapabilityStatus.Enabled;
		}

		// Token: 0x1700250E RID: 9486
		// (get) Token: 0x0600688E RID: 26766 RVA: 0x001702AC File Offset: 0x0016E4AC
		protected override SyncPropertyDefinition[] MinimumForwardSyncProperties
		{
			get
			{
				List<SyncPropertyDefinition> list = base.MinimumForwardSyncProperties.ToList<SyncPropertyDefinition>();
				list.AddRange(new SyncPropertyDefinition[]
				{
					SyncUserSchema.WindowsLiveID,
					SyncUserSchema.NetID
				});
				return list.ToArray();
			}
		}

		// Token: 0x0600688F RID: 26767 RVA: 0x0017030C File Offset: 0x0016E50C
		private static AssignedPlanValue GetEffectiveRootServicePlan(IPropertyBag propertyBag)
		{
			AssignedPlanValue assignedPlanValue = null;
			MultiValuedProperty<AssignedPlanValue> multiValuedProperty = (MultiValuedProperty<AssignedPlanValue>)propertyBag[SyncUserSchema.AssignedPlan];
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				IOrderedEnumerable<AssignedPlanValue> source = from ap in multiValuedProperty
				orderby ap.AssignedTimestamp descending
				select ap;
				foreach (AssignedPlanValue assignedPlanValue2 in from ap in source
				where ap.CapabilityStatus != AssignedCapabilityStatus.Deleted
				select ap)
				{
					if (CapabilityHelper.IsRootSKUCapability(SyncUser.GetExchangeCapability(assignedPlanValue2.Capability)))
					{
						assignedPlanValue = assignedPlanValue2;
						break;
					}
				}
				if (assignedPlanValue == null)
				{
					foreach (AssignedPlanValue assignedPlanValue3 in from ap in source
					where ap.CapabilityStatus == AssignedCapabilityStatus.Deleted
					select ap)
					{
						if (CapabilityHelper.IsRootSKUCapability(SyncUser.GetExchangeCapability(assignedPlanValue3.Capability)))
						{
							assignedPlanValue = assignedPlanValue3;
							break;
						}
					}
				}
			}
			return assignedPlanValue;
		}

		// Token: 0x06006890 RID: 26768 RVA: 0x00170448 File Offset: 0x0016E648
		private static MultiValuedProperty<Capability> GetEffectiveAddOnSKUCapabilities(IPropertyBag propertyBag)
		{
			MultiValuedProperty<AssignedPlanValue> multiValuedProperty = (MultiValuedProperty<AssignedPlanValue>)propertyBag[SyncUserSchema.AssignedPlan];
			MultiValuedProperty<Capability> multiValuedProperty2 = new MultiValuedProperty<Capability>();
			if (multiValuedProperty != null && multiValuedProperty.Count != 0)
			{
				foreach (AssignedPlanValue assignedPlanValue in multiValuedProperty)
				{
					Capability exchangeCapability = SyncUser.GetExchangeCapability(assignedPlanValue.Capability);
					if (CapabilityHelper.IsAddOnSKUCapability(exchangeCapability) && assignedPlanValue.CapabilityStatus != AssignedCapabilityStatus.Deleted)
					{
						multiValuedProperty2.Add(exchangeCapability);
					}
				}
			}
			return multiValuedProperty2;
		}

		// Token: 0x040044CE RID: 17614
		private static readonly SyncUserSchema schema = ObjectSchema.GetInstance<SyncUserSchema>();
	}
}
