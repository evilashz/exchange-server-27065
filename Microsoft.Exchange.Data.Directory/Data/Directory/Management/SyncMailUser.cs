using System;
using System.Globalization;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000765 RID: 1893
	[ProvisioningObjectTag("SyncMailUser")]
	[Serializable]
	public class SyncMailUser : MailUser
	{
		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x06005C7B RID: 23675 RVA: 0x0014145F File Offset: 0x0013F65F
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncMailUser.schema;
			}
		}

		// Token: 0x06005C7C RID: 23676 RVA: 0x00141466 File Offset: 0x0013F666
		public SyncMailUser()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x00141479 File Offset: 0x0013F679
		public SyncMailUser(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x00141482 File Offset: 0x0013F682
		internal new static SyncMailUser FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new SyncMailUser(dataObject);
		}

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x06005C7F RID: 23679 RVA: 0x0014148F File Offset: 0x0013F68F
		// (set) Token: 0x06005C80 RID: 23680 RVA: 0x00141497 File Offset: 0x0013F697
		public new MultiValuedProperty<ADObjectId> BypassModerationFrom
		{
			get
			{
				return base.BypassModerationFrom;
			}
			internal set
			{
				base.BypassModerationFrom = value;
			}
		}

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x06005C81 RID: 23681 RVA: 0x001414A0 File Offset: 0x0013F6A0
		// (set) Token: 0x06005C82 RID: 23682 RVA: 0x001414A8 File Offset: 0x0013F6A8
		public new MultiValuedProperty<ADObjectId> BypassModerationFromDLMembers
		{
			get
			{
				return base.BypassModerationFromDLMembers;
			}
			internal set
			{
				base.BypassModerationFromDLMembers = value;
			}
		}

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x06005C83 RID: 23683 RVA: 0x001414B1 File Offset: 0x0013F6B1
		// (set) Token: 0x06005C84 RID: 23684 RVA: 0x001414C3 File Offset: 0x0013F6C3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<CultureInfo> Languages
		{
			get
			{
				return (MultiValuedProperty<CultureInfo>)this[SyncMailUserSchema.Languages];
			}
			set
			{
				this[SyncMailUserSchema.Languages] = value;
			}
		}

		// Token: 0x1700204B RID: 8267
		// (get) Token: 0x06005C85 RID: 23685 RVA: 0x001414D1 File Offset: 0x0013F6D1
		// (set) Token: 0x06005C86 RID: 23686 RVA: 0x001414D9 File Offset: 0x0013F6D9
		[Parameter(Mandatory = false)]
		public new bool DeliverToMailboxAndForward
		{
			get
			{
				return base.DeliverToMailboxAndForward;
			}
			set
			{
				base.DeliverToMailboxAndForward = value;
			}
		}

		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x06005C87 RID: 23687 RVA: 0x001414E2 File Offset: 0x0013F6E2
		// (set) Token: 0x06005C88 RID: 23688 RVA: 0x001414EA File Offset: 0x0013F6EA
		public new ADObjectId ForwardingAddress
		{
			get
			{
				return base.ForwardingAddress;
			}
			set
			{
				base.ForwardingAddress = value;
			}
		}

		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x06005C89 RID: 23689 RVA: 0x001414F3 File Offset: 0x0013F6F3
		// (set) Token: 0x06005C8A RID: 23690 RVA: 0x00141505 File Offset: 0x0013F705
		[Parameter(Mandatory = false)]
		public string AssistantName
		{
			get
			{
				return (string)this[SyncMailUserSchema.AssistantName];
			}
			set
			{
				this[SyncMailUserSchema.AssistantName] = value;
			}
		}

		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x06005C8B RID: 23691 RVA: 0x00141513 File Offset: 0x0013F713
		// (set) Token: 0x06005C8C RID: 23692 RVA: 0x00141525 File Offset: 0x0013F725
		[Parameter(Mandatory = false)]
		public byte[] BlockedSendersHash
		{
			get
			{
				return (byte[])this[SyncMailUserSchema.BlockedSendersHash];
			}
			set
			{
				this[SyncMailUserSchema.BlockedSendersHash] = value;
			}
		}

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x06005C8D RID: 23693 RVA: 0x00141533 File Offset: 0x0013F733
		public MultiValuedProperty<byte[]> Certificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[SyncMailUserSchema.Certificate];
			}
		}

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x06005C8E RID: 23694 RVA: 0x00141545 File Offset: 0x0013F745
		// (set) Token: 0x06005C8F RID: 23695 RVA: 0x00141557 File Offset: 0x0013F757
		[Parameter(Mandatory = false)]
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return (SecurityIdentifier)this[SyncMailUserSchema.MasterAccountSid];
			}
			set
			{
				this[SyncMailUserSchema.MasterAccountSid] = value;
			}
		}

		// Token: 0x17002051 RID: 8273
		// (get) Token: 0x06005C90 RID: 23696 RVA: 0x00141565 File Offset: 0x0013F765
		// (set) Token: 0x06005C91 RID: 23697 RVA: 0x00141577 File Offset: 0x0013F777
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return (string)this[SyncMailUserSchema.Notes];
			}
			set
			{
				this[SyncMailUserSchema.Notes] = value;
			}
		}

		// Token: 0x17002052 RID: 8274
		// (get) Token: 0x06005C92 RID: 23698 RVA: 0x00141585 File Offset: 0x0013F785
		// (set) Token: 0x06005C93 RID: 23699 RVA: 0x00141597 File Offset: 0x0013F797
		[Parameter(Mandatory = false)]
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return (RecipientDisplayType?)this[SyncMailUserSchema.RecipientDisplayType];
			}
			set
			{
				this[SyncMailUserSchema.RecipientDisplayType] = value;
			}
		}

		// Token: 0x17002053 RID: 8275
		// (get) Token: 0x06005C94 RID: 23700 RVA: 0x001415AA File Offset: 0x0013F7AA
		// (set) Token: 0x06005C95 RID: 23701 RVA: 0x001415BC File Offset: 0x0013F7BC
		[Parameter(Mandatory = false)]
		public ExchangeResourceType? ResourceType
		{
			get
			{
				return (ExchangeResourceType?)this[ADRecipientSchema.ResourceType];
			}
			set
			{
				this[ADRecipientSchema.ResourceType] = value;
			}
		}

		// Token: 0x17002054 RID: 8276
		// (get) Token: 0x06005C96 RID: 23702 RVA: 0x001415CF File Offset: 0x0013F7CF
		// (set) Token: 0x06005C97 RID: 23703 RVA: 0x001415E1 File Offset: 0x0013F7E1
		[Parameter(Mandatory = false)]
		public byte[] SafeRecipientsHash
		{
			get
			{
				return (byte[])this[SyncMailUserSchema.SafeRecipientsHash];
			}
			set
			{
				this[SyncMailUserSchema.SafeRecipientsHash] = value;
			}
		}

		// Token: 0x17002055 RID: 8277
		// (get) Token: 0x06005C98 RID: 23704 RVA: 0x001415EF File Offset: 0x0013F7EF
		// (set) Token: 0x06005C99 RID: 23705 RVA: 0x00141601 File Offset: 0x0013F801
		[Parameter(Mandatory = false)]
		public byte[] SafeSendersHash
		{
			get
			{
				return (byte[])this[SyncMailUserSchema.SafeSendersHash];
			}
			set
			{
				this[SyncMailUserSchema.SafeSendersHash] = value;
			}
		}

		// Token: 0x17002056 RID: 8278
		// (get) Token: 0x06005C9A RID: 23706 RVA: 0x0014160F File Offset: 0x0013F80F
		public MultiValuedProperty<byte[]> SMimeCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[SyncMailUserSchema.SMimeCertificate];
			}
		}

		// Token: 0x17002057 RID: 8279
		// (get) Token: 0x06005C9B RID: 23707 RVA: 0x00141621 File Offset: 0x0013F821
		// (set) Token: 0x06005C9C RID: 23708 RVA: 0x00141633 File Offset: 0x0013F833
		[Parameter(Mandatory = false)]
		public byte[] Picture
		{
			get
			{
				return (byte[])this[SyncMailUserSchema.ThumbnailPhoto];
			}
			set
			{
				this[SyncMailUserSchema.ThumbnailPhoto] = value;
			}
		}

		// Token: 0x17002058 RID: 8280
		// (get) Token: 0x06005C9D RID: 23709 RVA: 0x00141641 File Offset: 0x0013F841
		// (set) Token: 0x06005C9E RID: 23710 RVA: 0x00141653 File Offset: 0x0013F853
		[Parameter(Mandatory = false)]
		public string DirSyncId
		{
			get
			{
				return (string)this[SyncMailUserSchema.DirSyncId];
			}
			set
			{
				this[SyncMailUserSchema.DirSyncId] = value;
			}
		}

		// Token: 0x17002059 RID: 8281
		// (get) Token: 0x06005C9F RID: 23711 RVA: 0x00141661 File Offset: 0x0013F861
		// (set) Token: 0x06005CA0 RID: 23712 RVA: 0x00141673 File Offset: 0x0013F873
		[Parameter(Mandatory = false)]
		public string City
		{
			get
			{
				return (string)this[SyncMailUserSchema.City];
			}
			set
			{
				this[SyncMailUserSchema.City] = value;
			}
		}

		// Token: 0x1700205A RID: 8282
		// (get) Token: 0x06005CA1 RID: 23713 RVA: 0x00141681 File Offset: 0x0013F881
		// (set) Token: 0x06005CA2 RID: 23714 RVA: 0x00141693 File Offset: 0x0013F893
		[Parameter(Mandatory = false)]
		public string Company
		{
			get
			{
				return (string)this[SyncMailUserSchema.Company];
			}
			set
			{
				this[SyncMailUserSchema.Company] = value;
			}
		}

		// Token: 0x1700205B RID: 8283
		// (get) Token: 0x06005CA3 RID: 23715 RVA: 0x001416A1 File Offset: 0x0013F8A1
		// (set) Token: 0x06005CA4 RID: 23716 RVA: 0x001416B3 File Offset: 0x0013F8B3
		[Parameter(Mandatory = false)]
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)this[SyncMailUserSchema.CountryOrRegion];
			}
			set
			{
				this[SyncMailUserSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x1700205C RID: 8284
		// (get) Token: 0x06005CA5 RID: 23717 RVA: 0x001416C1 File Offset: 0x0013F8C1
		// (set) Token: 0x06005CA6 RID: 23718 RVA: 0x001416D3 File Offset: 0x0013F8D3
		public string C
		{
			get
			{
				return (string)this[SyncMailUserSchema.C];
			}
			set
			{
				this[SyncMailUserSchema.C] = value;
			}
		}

		// Token: 0x1700205D RID: 8285
		// (get) Token: 0x06005CA7 RID: 23719 RVA: 0x001416E1 File Offset: 0x0013F8E1
		// (set) Token: 0x06005CA8 RID: 23720 RVA: 0x001416F3 File Offset: 0x0013F8F3
		public string Co
		{
			get
			{
				return (string)this[SyncMailUserSchema.Co];
			}
			set
			{
				this[SyncMailUserSchema.Co] = value;
			}
		}

		// Token: 0x1700205E RID: 8286
		// (get) Token: 0x06005CA9 RID: 23721 RVA: 0x00141701 File Offset: 0x0013F901
		// (set) Token: 0x06005CAA RID: 23722 RVA: 0x00141713 File Offset: 0x0013F913
		[Parameter(Mandatory = false)]
		public int CountryCode
		{
			get
			{
				return (int)this[SyncMailUserSchema.CountryCode];
			}
			set
			{
				this[SyncMailUserSchema.CountryCode] = value;
			}
		}

		// Token: 0x1700205F RID: 8287
		// (get) Token: 0x06005CAB RID: 23723 RVA: 0x00141726 File Offset: 0x0013F926
		// (set) Token: 0x06005CAC RID: 23724 RVA: 0x00141738 File Offset: 0x0013F938
		[Parameter(Mandatory = false)]
		public string Department
		{
			get
			{
				return (string)this[SyncMailUserSchema.Department];
			}
			set
			{
				this[SyncMailUserSchema.Department] = value;
			}
		}

		// Token: 0x17002060 RID: 8288
		// (get) Token: 0x06005CAD RID: 23725 RVA: 0x00141746 File Offset: 0x0013F946
		// (set) Token: 0x06005CAE RID: 23726 RVA: 0x00141758 File Offset: 0x0013F958
		[Parameter(Mandatory = false)]
		public string Fax
		{
			get
			{
				return (string)this[SyncMailUserSchema.Fax];
			}
			set
			{
				this[SyncMailUserSchema.Fax] = value;
			}
		}

		// Token: 0x17002061 RID: 8289
		// (get) Token: 0x06005CAF RID: 23727 RVA: 0x00141766 File Offset: 0x0013F966
		// (set) Token: 0x06005CB0 RID: 23728 RVA: 0x00141778 File Offset: 0x0013F978
		[Parameter(Mandatory = false)]
		public string FirstName
		{
			get
			{
				return (string)this[SyncMailUserSchema.FirstName];
			}
			set
			{
				this[SyncMailUserSchema.FirstName] = value;
			}
		}

		// Token: 0x17002062 RID: 8290
		// (get) Token: 0x06005CB1 RID: 23729 RVA: 0x00141786 File Offset: 0x0013F986
		// (set) Token: 0x06005CB2 RID: 23730 RVA: 0x00141798 File Offset: 0x0013F998
		[Parameter(Mandatory = false)]
		public string HomePhone
		{
			get
			{
				return (string)this[SyncMailUserSchema.HomePhone];
			}
			set
			{
				this[SyncMailUserSchema.HomePhone] = value;
			}
		}

		// Token: 0x17002063 RID: 8291
		// (get) Token: 0x06005CB3 RID: 23731 RVA: 0x001417A6 File Offset: 0x0013F9A6
		// (set) Token: 0x06005CB4 RID: 23732 RVA: 0x001417B8 File Offset: 0x0013F9B8
		[Parameter(Mandatory = false)]
		public string Initials
		{
			get
			{
				return (string)this[SyncMailUserSchema.Initials];
			}
			set
			{
				this[SyncMailUserSchema.Initials] = value;
			}
		}

		// Token: 0x17002064 RID: 8292
		// (get) Token: 0x06005CB5 RID: 23733 RVA: 0x001417C6 File Offset: 0x0013F9C6
		// (set) Token: 0x06005CB6 RID: 23734 RVA: 0x001417D8 File Offset: 0x0013F9D8
		[Parameter(Mandatory = false)]
		public string LastName
		{
			get
			{
				return (string)this[SyncMailUserSchema.LastName];
			}
			set
			{
				this[SyncMailUserSchema.LastName] = value;
			}
		}

		// Token: 0x17002065 RID: 8293
		// (get) Token: 0x06005CB7 RID: 23735 RVA: 0x001417E6 File Offset: 0x0013F9E6
		public ADObjectId Manager
		{
			get
			{
				return (ADObjectId)this[SyncMailUserSchema.Manager];
			}
		}

		// Token: 0x17002066 RID: 8294
		// (get) Token: 0x06005CB8 RID: 23736 RVA: 0x001417F8 File Offset: 0x0013F9F8
		// (set) Token: 0x06005CB9 RID: 23737 RVA: 0x0014180A File Offset: 0x0013FA0A
		[Parameter(Mandatory = false)]
		public string MobilePhone
		{
			get
			{
				return (string)this[SyncMailUserSchema.MobilePhone];
			}
			set
			{
				this[SyncMailUserSchema.MobilePhone] = value;
			}
		}

		// Token: 0x17002067 RID: 8295
		// (get) Token: 0x06005CBA RID: 23738 RVA: 0x00141818 File Offset: 0x0013FA18
		// (set) Token: 0x06005CBB RID: 23739 RVA: 0x0014182A File Offset: 0x0013FA2A
		[Parameter(Mandatory = false)]
		public string Office
		{
			get
			{
				return (string)this[SyncMailUserSchema.Office];
			}
			set
			{
				this[SyncMailUserSchema.Office] = value;
			}
		}

		// Token: 0x17002068 RID: 8296
		// (get) Token: 0x06005CBC RID: 23740 RVA: 0x00141838 File Offset: 0x0013FA38
		// (set) Token: 0x06005CBD RID: 23741 RVA: 0x0014184A File Offset: 0x0013FA4A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailUserSchema.OtherFax];
			}
			set
			{
				this[SyncMailUserSchema.OtherFax] = value;
			}
		}

		// Token: 0x17002069 RID: 8297
		// (get) Token: 0x06005CBE RID: 23742 RVA: 0x00141858 File Offset: 0x0013FA58
		// (set) Token: 0x06005CBF RID: 23743 RVA: 0x0014186A File Offset: 0x0013FA6A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailUserSchema.OtherHomePhone];
			}
			set
			{
				this[SyncMailUserSchema.OtherHomePhone] = value;
			}
		}

		// Token: 0x1700206A RID: 8298
		// (get) Token: 0x06005CC0 RID: 23744 RVA: 0x00141878 File Offset: 0x0013FA78
		// (set) Token: 0x06005CC1 RID: 23745 RVA: 0x0014188A File Offset: 0x0013FA8A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailUserSchema.OtherTelephone];
			}
			set
			{
				this[SyncMailUserSchema.OtherTelephone] = value;
			}
		}

		// Token: 0x1700206B RID: 8299
		// (get) Token: 0x06005CC2 RID: 23746 RVA: 0x00141898 File Offset: 0x0013FA98
		// (set) Token: 0x06005CC3 RID: 23747 RVA: 0x001418AA File Offset: 0x0013FAAA
		[Parameter(Mandatory = false)]
		public string Pager
		{
			get
			{
				return (string)this[SyncMailUserSchema.Pager];
			}
			set
			{
				this[SyncMailUserSchema.Pager] = value;
			}
		}

		// Token: 0x1700206C RID: 8300
		// (get) Token: 0x06005CC4 RID: 23748 RVA: 0x001418B8 File Offset: 0x0013FAB8
		// (set) Token: 0x06005CC5 RID: 23749 RVA: 0x001418CA File Offset: 0x0013FACA
		[Parameter(Mandatory = false)]
		public string Phone
		{
			get
			{
				return (string)this[SyncMailUserSchema.Phone];
			}
			set
			{
				this[SyncMailUserSchema.Phone] = value;
			}
		}

		// Token: 0x1700206D RID: 8301
		// (get) Token: 0x06005CC6 RID: 23750 RVA: 0x001418D8 File Offset: 0x0013FAD8
		// (set) Token: 0x06005CC7 RID: 23751 RVA: 0x001418EA File Offset: 0x0013FAEA
		[Parameter(Mandatory = false)]
		public string PostalCode
		{
			get
			{
				return (string)this[SyncMailUserSchema.PostalCode];
			}
			set
			{
				this[SyncMailUserSchema.PostalCode] = value;
			}
		}

		// Token: 0x1700206E RID: 8302
		// (get) Token: 0x06005CC8 RID: 23752 RVA: 0x001418F8 File Offset: 0x0013FAF8
		// (set) Token: 0x06005CC9 RID: 23753 RVA: 0x0014190A File Offset: 0x0013FB0A
		[Parameter(Mandatory = false)]
		public int? ResourceCapacity
		{
			get
			{
				return (int?)this[SyncMailUserSchema.ResourceCapacity];
			}
			set
			{
				this[SyncMailUserSchema.ResourceCapacity] = value;
			}
		}

		// Token: 0x1700206F RID: 8303
		// (get) Token: 0x06005CCA RID: 23754 RVA: 0x0014191D File Offset: 0x0013FB1D
		// (set) Token: 0x06005CCB RID: 23755 RVA: 0x0014192F File Offset: 0x0013FB2F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourceCustom
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailUserSchema.ResourceCustom];
			}
			set
			{
				this[SyncMailUserSchema.ResourceCustom] = value;
			}
		}

		// Token: 0x17002070 RID: 8304
		// (get) Token: 0x06005CCC RID: 23756 RVA: 0x0014193D File Offset: 0x0013FB3D
		// (set) Token: 0x06005CCD RID: 23757 RVA: 0x0014194F File Offset: 0x0013FB4F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourceMetaData
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailUserSchema.ResourceMetaData];
			}
			set
			{
				this[SyncMailUserSchema.ResourceMetaData] = value;
			}
		}

		// Token: 0x17002071 RID: 8305
		// (get) Token: 0x06005CCE RID: 23758 RVA: 0x0014195D File Offset: 0x0013FB5D
		// (set) Token: 0x06005CCF RID: 23759 RVA: 0x0014196F File Offset: 0x0013FB6F
		[Parameter(Mandatory = false)]
		public string ResourcePropertiesDisplay
		{
			get
			{
				return (string)this[SyncMailUserSchema.ResourcePropertiesDisplay];
			}
			set
			{
				this[SyncMailUserSchema.ResourcePropertiesDisplay] = value;
			}
		}

		// Token: 0x17002072 RID: 8306
		// (get) Token: 0x06005CD0 RID: 23760 RVA: 0x0014197D File Offset: 0x0013FB7D
		// (set) Token: 0x06005CD1 RID: 23761 RVA: 0x0014198F File Offset: 0x0013FB8F
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ResourceSearchProperties
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailUserSchema.ResourceSearchProperties];
			}
			set
			{
				this[SyncMailUserSchema.ResourceSearchProperties] = value;
			}
		}

		// Token: 0x17002073 RID: 8307
		// (get) Token: 0x06005CD2 RID: 23762 RVA: 0x0014199D File Offset: 0x0013FB9D
		// (set) Token: 0x06005CD3 RID: 23763 RVA: 0x001419AF File Offset: 0x0013FBAF
		[Parameter(Mandatory = false)]
		public string StateOrProvince
		{
			get
			{
				return (string)this[SyncMailUserSchema.StateOrProvince];
			}
			set
			{
				this[SyncMailUserSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x17002074 RID: 8308
		// (get) Token: 0x06005CD4 RID: 23764 RVA: 0x001419BD File Offset: 0x0013FBBD
		// (set) Token: 0x06005CD5 RID: 23765 RVA: 0x001419CF File Offset: 0x0013FBCF
		[Parameter(Mandatory = false)]
		public string StreetAddress
		{
			get
			{
				return (string)this[SyncMailUserSchema.StreetAddress];
			}
			set
			{
				this[SyncMailUserSchema.StreetAddress] = value;
			}
		}

		// Token: 0x17002075 RID: 8309
		// (get) Token: 0x06005CD6 RID: 23766 RVA: 0x001419DD File Offset: 0x0013FBDD
		// (set) Token: 0x06005CD7 RID: 23767 RVA: 0x001419EF File Offset: 0x0013FBEF
		[Parameter(Mandatory = false)]
		public string TelephoneAssistant
		{
			get
			{
				return (string)this[SyncMailUserSchema.TelephoneAssistant];
			}
			set
			{
				this[SyncMailUserSchema.TelephoneAssistant] = value;
			}
		}

		// Token: 0x17002076 RID: 8310
		// (get) Token: 0x06005CD8 RID: 23768 RVA: 0x001419FD File Offset: 0x0013FBFD
		// (set) Token: 0x06005CD9 RID: 23769 RVA: 0x00141A0F File Offset: 0x0013FC0F
		[Parameter(Mandatory = false)]
		public string Title
		{
			get
			{
				return (string)this[SyncMailUserSchema.Title];
			}
			set
			{
				this[SyncMailUserSchema.Title] = value;
			}
		}

		// Token: 0x17002077 RID: 8311
		// (get) Token: 0x06005CDA RID: 23770 RVA: 0x00141A1D File Offset: 0x0013FC1D
		// (set) Token: 0x06005CDB RID: 23771 RVA: 0x00141A2F File Offset: 0x0013FC2F
		[Parameter(Mandatory = false)]
		public string WebPage
		{
			get
			{
				return (string)this[SyncMailUserSchema.WebPage];
			}
			set
			{
				this[SyncMailUserSchema.WebPage] = value;
			}
		}

		// Token: 0x17002078 RID: 8312
		// (get) Token: 0x06005CDC RID: 23772 RVA: 0x00141A3D File Offset: 0x0013FC3D
		// (set) Token: 0x06005CDD RID: 23773 RVA: 0x00141A4F File Offset: 0x0013FC4F
		public string IntendedMailboxPlanName
		{
			get
			{
				return (string)this[SyncMailUserSchema.IntendedMailboxPlanName];
			}
			internal set
			{
				this[SyncMailUserSchema.IntendedMailboxPlanName] = value;
			}
		}

		// Token: 0x17002079 RID: 8313
		// (get) Token: 0x06005CDE RID: 23774 RVA: 0x00141A5D File Offset: 0x0013FC5D
		// (set) Token: 0x06005CDF RID: 23775 RVA: 0x00141A6F File Offset: 0x0013FC6F
		public ADObjectId IntendedMailboxPlan
		{
			get
			{
				return (ADObjectId)this[SyncMailUserSchema.IntendedMailboxPlan];
			}
			set
			{
				this[SyncMailUserSchema.IntendedMailboxPlan] = value;
			}
		}

		// Token: 0x1700207A RID: 8314
		// (get) Token: 0x06005CE0 RID: 23776 RVA: 0x00141A7D File Offset: 0x0013FC7D
		// (set) Token: 0x06005CE1 RID: 23777 RVA: 0x00141A8F File Offset: 0x0013FC8F
		[Parameter(Mandatory = false)]
		public int? SeniorityIndex
		{
			get
			{
				return (int?)this[SyncMailUserSchema.SeniorityIndex];
			}
			set
			{
				this[SyncMailUserSchema.SeniorityIndex] = value;
			}
		}

		// Token: 0x1700207B RID: 8315
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x00141AA2 File Offset: 0x0013FCA2
		// (set) Token: 0x06005CE3 RID: 23779 RVA: 0x00141AB4 File Offset: 0x0013FCB4
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[SyncMailUserSchema.PhoneticDisplayName];
			}
			set
			{
				this[SyncMailUserSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x1700207C RID: 8316
		// (get) Token: 0x06005CE4 RID: 23780 RVA: 0x00141AC2 File Offset: 0x0013FCC2
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[SyncMailUserSchema.Sid];
			}
		}

		// Token: 0x1700207D RID: 8317
		// (get) Token: 0x06005CE5 RID: 23781 RVA: 0x00141AD4 File Offset: 0x0013FCD4
		public MultiValuedProperty<SecurityIdentifier> SidHistory
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[SyncMailUserSchema.SidHistory];
			}
		}

		// Token: 0x1700207E RID: 8318
		// (get) Token: 0x06005CE6 RID: 23782 RVA: 0x00141AE6 File Offset: 0x0013FCE6
		// (set) Token: 0x06005CE7 RID: 23783 RVA: 0x00141AF8 File Offset: 0x0013FCF8
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)this[SyncMailUserSchema.ReleaseTrack];
			}
			set
			{
				this[SyncMailUserSchema.ReleaseTrack] = value;
			}
		}

		// Token: 0x1700207F RID: 8319
		// (get) Token: 0x06005CE8 RID: 23784 RVA: 0x00141B0B File Offset: 0x0013FD0B
		// (set) Token: 0x06005CE9 RID: 23785 RVA: 0x00141B1D File Offset: 0x0013FD1D
		public bool EndOfList
		{
			get
			{
				return (bool)this[SyncMailUserSchema.EndOfList];
			}
			internal set
			{
				this[SyncMailUserSchema.EndOfList] = value;
			}
		}

		// Token: 0x17002080 RID: 8320
		// (get) Token: 0x06005CEA RID: 23786 RVA: 0x00141B30 File Offset: 0x0013FD30
		// (set) Token: 0x06005CEB RID: 23787 RVA: 0x00141B42 File Offset: 0x0013FD42
		public byte[] Cookie
		{
			get
			{
				return (byte[])this[SyncMailUserSchema.Cookie];
			}
			internal set
			{
				this[SyncMailUserSchema.Cookie] = value;
			}
		}

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x06005CEC RID: 23788 RVA: 0x00141B50 File Offset: 0x0013FD50
		// (set) Token: 0x06005CED RID: 23789 RVA: 0x00141B62 File Offset: 0x0013FD62
		[Parameter(Mandatory = false)]
		public bool IsCalculatedTargetAddress
		{
			get
			{
				return (bool)this[SyncMailUserSchema.IsCalculatedTargetAddress];
			}
			set
			{
				this[SyncMailUserSchema.IsCalculatedTargetAddress] = value;
			}
		}

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x06005CEE RID: 23790 RVA: 0x00141B75 File Offset: 0x0013FD75
		// (set) Token: 0x06005CEF RID: 23791 RVA: 0x00141B87 File Offset: 0x0013FD87
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return (string)this[SyncMailUserSchema.OnPremisesObjectId];
			}
			set
			{
				this[SyncMailUserSchema.OnPremisesObjectId] = value;
			}
		}

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x06005CF0 RID: 23792 RVA: 0x00141B95 File Offset: 0x0013FD95
		// (set) Token: 0x06005CF1 RID: 23793 RVA: 0x00141BA7 File Offset: 0x0013FDA7
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return (bool)this[SyncMailUserSchema.IsDirSynced];
			}
			set
			{
				this[SyncMailUserSchema.IsDirSynced] = value;
			}
		}

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x06005CF2 RID: 23794 RVA: 0x00141BBA File Offset: 0x0013FDBA
		// (set) Token: 0x06005CF3 RID: 23795 RVA: 0x00141BCC File Offset: 0x0013FDCC
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailUserSchema.DirSyncAuthorityMetadata];
			}
			set
			{
				this[SyncMailUserSchema.DirSyncAuthorityMetadata] = value;
			}
		}

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x06005CF4 RID: 23796 RVA: 0x00141BDA File Offset: 0x0013FDDA
		// (set) Token: 0x06005CF5 RID: 23797 RVA: 0x00141BEC File Offset: 0x0013FDEC
		[Parameter(Mandatory = false)]
		public RemoteRecipientType RemoteRecipientType
		{
			get
			{
				return (RemoteRecipientType)this[SyncMailUserSchema.RemoteRecipientType];
			}
			set
			{
				this[SyncMailUserSchema.RemoteRecipientType] = value;
			}
		}

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x06005CF6 RID: 23798 RVA: 0x00141BFF File Offset: 0x0013FDFF
		// (set) Token: 0x06005CF7 RID: 23799 RVA: 0x00141C11 File Offset: 0x0013FE11
		[Parameter(Mandatory = false)]
		public bool ExcludedFromBackSync
		{
			get
			{
				return (bool)this[SyncMailUserSchema.ExcludedFromBackSync];
			}
			set
			{
				this[SyncMailUserSchema.ExcludedFromBackSync] = value;
			}
		}

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x06005CF8 RID: 23800 RVA: 0x00141C24 File Offset: 0x0013FE24
		// (set) Token: 0x06005CF9 RID: 23801 RVA: 0x00141C36 File Offset: 0x0013FE36
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> InPlaceHoldsRaw
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailUserSchema.InPlaceHoldsRaw];
			}
			set
			{
				this[SyncMailUserSchema.InPlaceHoldsRaw] = value;
			}
		}

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x06005CFA RID: 23802 RVA: 0x00141C44 File Offset: 0x0013FE44
		// (set) Token: 0x06005CFB RID: 23803 RVA: 0x00141C56 File Offset: 0x0013FE56
		[Parameter(Mandatory = false)]
		public ElcMailboxFlags ElcMailboxFlags
		{
			get
			{
				return (ElcMailboxFlags)this[SyncMailUserSchema.ElcMailboxFlags];
			}
			set
			{
				this[SyncMailUserSchema.ElcMailboxFlags] = value;
			}
		}

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x06005CFC RID: 23804 RVA: 0x00141C69 File Offset: 0x0013FE69
		// (set) Token: 0x06005CFD RID: 23805 RVA: 0x00141C7B File Offset: 0x0013FE7B
		[Parameter(Mandatory = false)]
		public bool MailboxAuditEnabled
		{
			get
			{
				return (bool)this[SyncMailUserSchema.AuditEnabled];
			}
			set
			{
				this[SyncMailUserSchema.AuditEnabled] = value;
			}
		}

		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x06005CFE RID: 23806 RVA: 0x00141C8E File Offset: 0x0013FE8E
		// (set) Token: 0x06005CFF RID: 23807 RVA: 0x00141CA0 File Offset: 0x0013FEA0
		[Parameter(Mandatory = false)]
		public EnhancedTimeSpan MailboxAuditLogAgeLimit
		{
			get
			{
				return (EnhancedTimeSpan)this[SyncMailUserSchema.AuditLogAgeLimit];
			}
			set
			{
				this[SyncMailUserSchema.AuditLogAgeLimit] = value;
			}
		}

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x06005D00 RID: 23808 RVA: 0x00141CB3 File Offset: 0x0013FEB3
		// (set) Token: 0x06005D01 RID: 23809 RVA: 0x00141CC5 File Offset: 0x0013FEC5
		[Parameter(Mandatory = false)]
		public MailboxAuditOperations AuditAdminOperations
		{
			get
			{
				return (MailboxAuditOperations)this[SyncMailUserSchema.AuditAdminFlags];
			}
			set
			{
				this[SyncMailUserSchema.AuditAdminFlags] = value;
			}
		}

		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x06005D02 RID: 23810 RVA: 0x00141CD8 File Offset: 0x0013FED8
		// (set) Token: 0x06005D03 RID: 23811 RVA: 0x00141CEA File Offset: 0x0013FEEA
		[Parameter(Mandatory = false)]
		public MailboxAuditOperations AuditDelegateAdminOperations
		{
			get
			{
				return (MailboxAuditOperations)this[SyncMailUserSchema.AuditDelegateAdminFlags];
			}
			set
			{
				this[SyncMailUserSchema.AuditDelegateAdminFlags] = value;
			}
		}

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x06005D04 RID: 23812 RVA: 0x00141CFD File Offset: 0x0013FEFD
		// (set) Token: 0x06005D05 RID: 23813 RVA: 0x00141D0F File Offset: 0x0013FF0F
		[Parameter(Mandatory = false)]
		public MailboxAuditOperations AuditDelegateOperations
		{
			get
			{
				return (MailboxAuditOperations)this[SyncMailUserSchema.AuditDelegateFlags];
			}
			set
			{
				this[SyncMailUserSchema.AuditDelegateFlags] = value;
			}
		}

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x06005D06 RID: 23814 RVA: 0x00141D22 File Offset: 0x0013FF22
		// (set) Token: 0x06005D07 RID: 23815 RVA: 0x00141D34 File Offset: 0x0013FF34
		[Parameter(Mandatory = false)]
		public MailboxAuditOperations AuditOwnerOperations
		{
			get
			{
				return (MailboxAuditOperations)this[SyncMailUserSchema.AuditOwnerFlags];
			}
			set
			{
				this[SyncMailUserSchema.AuditOwnerFlags] = value;
			}
		}

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x06005D08 RID: 23816 RVA: 0x00141D47 File Offset: 0x0013FF47
		// (set) Token: 0x06005D09 RID: 23817 RVA: 0x00141D59 File Offset: 0x0013FF59
		[Parameter(Mandatory = false)]
		public bool BypassAudit
		{
			get
			{
				return (bool)this[SyncMailUserSchema.AuditBypassEnabled];
			}
			set
			{
				this[SyncMailUserSchema.AuditBypassEnabled] = value;
			}
		}

		// Token: 0x17002090 RID: 8336
		// (get) Token: 0x06005D0A RID: 23818 RVA: 0x00141D6C File Offset: 0x0013FF6C
		// (set) Token: 0x06005D0B RID: 23819 RVA: 0x00141D7E File Offset: 0x0013FF7E
		public MultiValuedProperty<ADObjectId> SiteMailboxOwners
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SyncMailUserSchema.SiteMailboxOwners];
			}
			set
			{
				this[SyncMailUserSchema.SiteMailboxOwners] = value;
			}
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x06005D0C RID: 23820 RVA: 0x00141D8C File Offset: 0x0013FF8C
		// (set) Token: 0x06005D0D RID: 23821 RVA: 0x00141D9E File Offset: 0x0013FF9E
		public MultiValuedProperty<ADObjectId> SiteMailboxUsers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[SyncMailUserSchema.SiteMailboxUsers];
			}
			set
			{
				this[SyncMailUserSchema.SiteMailboxUsers] = value;
			}
		}

		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x06005D0E RID: 23822 RVA: 0x00141DAC File Offset: 0x0013FFAC
		// (set) Token: 0x06005D0F RID: 23823 RVA: 0x00141DBE File Offset: 0x0013FFBE
		[Parameter(Mandatory = false)]
		public DateTime? SiteMailboxClosedTime
		{
			get
			{
				return (DateTime?)this[SyncMailUserSchema.SiteMailboxClosedTime];
			}
			set
			{
				this[SyncMailUserSchema.SiteMailboxClosedTime] = value;
			}
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x06005D10 RID: 23824 RVA: 0x00141DD1 File Offset: 0x0013FFD1
		// (set) Token: 0x06005D11 RID: 23825 RVA: 0x00141DE3 File Offset: 0x0013FFE3
		[Parameter(Mandatory = false)]
		public Uri SharePointUrl
		{
			get
			{
				return (Uri)this[SyncMailUserSchema.SharePointUrl];
			}
			set
			{
				this[SyncMailUserSchema.SharePointUrl] = value;
			}
		}

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x06005D12 RID: 23826 RVA: 0x00141DF1 File Offset: 0x0013FFF1
		// (set) Token: 0x06005D13 RID: 23827 RVA: 0x00141E03 File Offset: 0x00140003
		[Parameter(Mandatory = false)]
		public bool AccountDisabled
		{
			get
			{
				return (bool)this[SyncMailUserSchema.AccountDisabled];
			}
			set
			{
				this[SyncMailUserSchema.AccountDisabled] = value;
			}
		}

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x06005D14 RID: 23828 RVA: 0x00141E16 File Offset: 0x00140016
		// (set) Token: 0x06005D15 RID: 23829 RVA: 0x00141E28 File Offset: 0x00140028
		[Parameter(Mandatory = false)]
		public DateTime? StsRefreshTokensValidFrom
		{
			get
			{
				return (DateTime?)this[SyncMailUserSchema.StsRefreshTokensValidFrom];
			}
			set
			{
				this[SyncUserSchema.StsRefreshTokensValidFrom] = value;
			}
		}

		// Token: 0x04003EF7 RID: 16119
		private static SyncMailUserSchema schema = ObjectSchema.GetInstance<SyncMailUserSchema>();
	}
}
