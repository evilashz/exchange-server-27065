using System;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000763 RID: 1891
	[ProvisioningObjectTag("SyncMailContact")]
	[Serializable]
	public class SyncMailContact : MailContact
	{
		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x06005C18 RID: 23576 RVA: 0x00140B63 File Offset: 0x0013ED63
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncMailContact.schema;
			}
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x00140B6A File Offset: 0x0013ED6A
		public SyncMailContact()
		{
			base.SetObjectClass("contact");
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x00140B7D File Offset: 0x0013ED7D
		public SyncMailContact(ADContact dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x00140B86 File Offset: 0x0013ED86
		internal new static SyncMailContact FromDataObject(ADContact dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new SyncMailContact(dataObject);
		}

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x00140B93 File Offset: 0x0013ED93
		// (set) Token: 0x06005C1D RID: 23581 RVA: 0x00140BA5 File Offset: 0x0013EDA5
		[Parameter(Mandatory = false)]
		public string AssistantName
		{
			get
			{
				return (string)this[SyncMailContactSchema.AssistantName];
			}
			set
			{
				this[SyncMailContactSchema.AssistantName] = value;
			}
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x06005C1E RID: 23582 RVA: 0x00140BB3 File Offset: 0x0013EDB3
		// (set) Token: 0x06005C1F RID: 23583 RVA: 0x00140BC5 File Offset: 0x0013EDC5
		[Parameter(Mandatory = false)]
		public byte[] BlockedSendersHash
		{
			get
			{
				return (byte[])this[SyncMailContactSchema.BlockedSendersHash];
			}
			set
			{
				this[SyncMailContactSchema.BlockedSendersHash] = value;
			}
		}

		// Token: 0x1700201A RID: 8218
		// (get) Token: 0x06005C20 RID: 23584 RVA: 0x00140BD3 File Offset: 0x0013EDD3
		// (set) Token: 0x06005C21 RID: 23585 RVA: 0x00140BDB File Offset: 0x0013EDDB
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

		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x06005C22 RID: 23586 RVA: 0x00140BE4 File Offset: 0x0013EDE4
		// (set) Token: 0x06005C23 RID: 23587 RVA: 0x00140BEC File Offset: 0x0013EDEC
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

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x06005C24 RID: 23588 RVA: 0x00140BF5 File Offset: 0x0013EDF5
		public string ImmutableId
		{
			get
			{
				return (string)this[SyncMailContactSchema.ImmutableId];
			}
		}

		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x06005C25 RID: 23589 RVA: 0x00140C07 File Offset: 0x0013EE07
		// (set) Token: 0x06005C26 RID: 23590 RVA: 0x00140C19 File Offset: 0x0013EE19
		[Parameter(Mandatory = false)]
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return (SecurityIdentifier)this[SyncMailContactSchema.MasterAccountSid];
			}
			set
			{
				this[SyncMailContactSchema.MasterAccountSid] = value;
			}
		}

		// Token: 0x1700201E RID: 8222
		// (get) Token: 0x06005C27 RID: 23591 RVA: 0x00140C27 File Offset: 0x0013EE27
		// (set) Token: 0x06005C28 RID: 23592 RVA: 0x00140C39 File Offset: 0x0013EE39
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return (string)this[SyncMailContactSchema.Notes];
			}
			set
			{
				this[SyncMailContactSchema.Notes] = value;
			}
		}

		// Token: 0x1700201F RID: 8223
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x00140C47 File Offset: 0x0013EE47
		// (set) Token: 0x06005C2A RID: 23594 RVA: 0x00140C59 File Offset: 0x0013EE59
		[Parameter(Mandatory = false)]
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return (RecipientDisplayType?)this[SyncMailContactSchema.RecipientDisplayType];
			}
			set
			{
				this[SyncMailContactSchema.RecipientDisplayType] = value;
			}
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x06005C2B RID: 23595 RVA: 0x00140C6C File Offset: 0x0013EE6C
		// (set) Token: 0x06005C2C RID: 23596 RVA: 0x00140C7E File Offset: 0x0013EE7E
		[Parameter(Mandatory = false)]
		public byte[] SafeRecipientsHash
		{
			get
			{
				return (byte[])this[SyncMailContactSchema.SafeRecipientsHash];
			}
			set
			{
				this[SyncMailContactSchema.SafeRecipientsHash] = value;
			}
		}

		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x06005C2D RID: 23597 RVA: 0x00140C8C File Offset: 0x0013EE8C
		// (set) Token: 0x06005C2E RID: 23598 RVA: 0x00140C9E File Offset: 0x0013EE9E
		[Parameter(Mandatory = false)]
		public byte[] SafeSendersHash
		{
			get
			{
				return (byte[])this[SyncMailContactSchema.SafeSendersHash];
			}
			set
			{
				this[SyncMailContactSchema.SafeSendersHash] = value;
			}
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x06005C2F RID: 23599 RVA: 0x00140CAC File Offset: 0x0013EEAC
		// (set) Token: 0x06005C30 RID: 23600 RVA: 0x00140CBE File Offset: 0x0013EEBE
		[Parameter(Mandatory = false)]
		public string DirSyncId
		{
			get
			{
				return (string)this[SyncMailContactSchema.DirSyncId];
			}
			set
			{
				this[SyncMailContactSchema.DirSyncId] = value;
			}
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x06005C31 RID: 23601 RVA: 0x00140CCC File Offset: 0x0013EECC
		// (set) Token: 0x06005C32 RID: 23602 RVA: 0x00140CDE File Offset: 0x0013EEDE
		[Parameter(Mandatory = false)]
		public string City
		{
			get
			{
				return (string)this[SyncMailContactSchema.City];
			}
			set
			{
				this[SyncMailContactSchema.City] = value;
			}
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x06005C33 RID: 23603 RVA: 0x00140CEC File Offset: 0x0013EEEC
		// (set) Token: 0x06005C34 RID: 23604 RVA: 0x00140CFE File Offset: 0x0013EEFE
		[Parameter(Mandatory = false)]
		public string Company
		{
			get
			{
				return (string)this[SyncMailContactSchema.Company];
			}
			set
			{
				this[SyncMailContactSchema.Company] = value;
			}
		}

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x06005C35 RID: 23605 RVA: 0x00140D0C File Offset: 0x0013EF0C
		// (set) Token: 0x06005C36 RID: 23606 RVA: 0x00140D1E File Offset: 0x0013EF1E
		[Parameter(Mandatory = false)]
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)this[SyncMailContactSchema.CountryOrRegion];
			}
			set
			{
				this[SyncMailContactSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x06005C37 RID: 23607 RVA: 0x00140D2C File Offset: 0x0013EF2C
		// (set) Token: 0x06005C38 RID: 23608 RVA: 0x00140D3E File Offset: 0x0013EF3E
		public string C
		{
			get
			{
				return (string)this[SyncMailContactSchema.C];
			}
			set
			{
				this[SyncMailContactSchema.C] = value;
			}
		}

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x06005C39 RID: 23609 RVA: 0x00140D4C File Offset: 0x0013EF4C
		// (set) Token: 0x06005C3A RID: 23610 RVA: 0x00140D5E File Offset: 0x0013EF5E
		public string Co
		{
			get
			{
				return (string)this[SyncMailContactSchema.Co];
			}
			set
			{
				this[SyncMailContactSchema.Co] = value;
			}
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x06005C3B RID: 23611 RVA: 0x00140D6C File Offset: 0x0013EF6C
		// (set) Token: 0x06005C3C RID: 23612 RVA: 0x00140D7E File Offset: 0x0013EF7E
		[Parameter(Mandatory = false)]
		public int CountryCode
		{
			get
			{
				return (int)this[SyncMailContactSchema.CountryCode];
			}
			set
			{
				this[SyncMailContactSchema.CountryCode] = value;
			}
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x06005C3D RID: 23613 RVA: 0x00140D91 File Offset: 0x0013EF91
		// (set) Token: 0x06005C3E RID: 23614 RVA: 0x00140DA3 File Offset: 0x0013EFA3
		[Parameter(Mandatory = false)]
		public string Department
		{
			get
			{
				return (string)this[SyncMailContactSchema.Department];
			}
			set
			{
				this[SyncMailContactSchema.Department] = value;
			}
		}

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x06005C3F RID: 23615 RVA: 0x00140DB1 File Offset: 0x0013EFB1
		// (set) Token: 0x06005C40 RID: 23616 RVA: 0x00140DC3 File Offset: 0x0013EFC3
		[Parameter(Mandatory = false)]
		public string Fax
		{
			get
			{
				return (string)this[SyncMailContactSchema.Fax];
			}
			set
			{
				this[SyncMailContactSchema.Fax] = value;
			}
		}

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x06005C41 RID: 23617 RVA: 0x00140DD1 File Offset: 0x0013EFD1
		// (set) Token: 0x06005C42 RID: 23618 RVA: 0x00140DE3 File Offset: 0x0013EFE3
		[Parameter(Mandatory = false)]
		public string FirstName
		{
			get
			{
				return (string)this[SyncMailContactSchema.FirstName];
			}
			set
			{
				this[SyncMailContactSchema.FirstName] = value;
			}
		}

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x06005C43 RID: 23619 RVA: 0x00140DF1 File Offset: 0x0013EFF1
		// (set) Token: 0x06005C44 RID: 23620 RVA: 0x00140E03 File Offset: 0x0013F003
		[Parameter(Mandatory = false)]
		public string HomePhone
		{
			get
			{
				return (string)this[SyncMailContactSchema.HomePhone];
			}
			set
			{
				this[SyncMailContactSchema.HomePhone] = value;
			}
		}

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x06005C45 RID: 23621 RVA: 0x00140E11 File Offset: 0x0013F011
		// (set) Token: 0x06005C46 RID: 23622 RVA: 0x00140E23 File Offset: 0x0013F023
		[Parameter(Mandatory = false)]
		public string Initials
		{
			get
			{
				return (string)this[SyncMailContactSchema.Initials];
			}
			set
			{
				this[SyncMailContactSchema.Initials] = value;
			}
		}

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x06005C47 RID: 23623 RVA: 0x00140E31 File Offset: 0x0013F031
		// (set) Token: 0x06005C48 RID: 23624 RVA: 0x00140E43 File Offset: 0x0013F043
		[Parameter(Mandatory = false)]
		public string LastName
		{
			get
			{
				return (string)this[SyncMailContactSchema.LastName];
			}
			set
			{
				this[SyncMailContactSchema.LastName] = value;
			}
		}

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x06005C49 RID: 23625 RVA: 0x00140E51 File Offset: 0x0013F051
		// (set) Token: 0x06005C4A RID: 23626 RVA: 0x00140E63 File Offset: 0x0013F063
		[Parameter(Mandatory = false)]
		public string MobilePhone
		{
			get
			{
				return (string)this[SyncMailContactSchema.MobilePhone];
			}
			set
			{
				this[SyncMailContactSchema.MobilePhone] = value;
			}
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x06005C4B RID: 23627 RVA: 0x00140E71 File Offset: 0x0013F071
		public ADObjectId Manager
		{
			get
			{
				return (ADObjectId)this[SyncMailboxSchema.Manager];
			}
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x06005C4C RID: 23628 RVA: 0x00140E83 File Offset: 0x0013F083
		// (set) Token: 0x06005C4D RID: 23629 RVA: 0x00140E95 File Offset: 0x0013F095
		[Parameter(Mandatory = false)]
		public string Office
		{
			get
			{
				return (string)this[SyncMailContactSchema.Office];
			}
			set
			{
				this[SyncMailContactSchema.Office] = value;
			}
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x06005C4E RID: 23630 RVA: 0x00140EA3 File Offset: 0x0013F0A3
		// (set) Token: 0x06005C4F RID: 23631 RVA: 0x00140EB5 File Offset: 0x0013F0B5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailContactSchema.OtherFax];
			}
			set
			{
				this[SyncMailContactSchema.OtherFax] = value;
			}
		}

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x06005C50 RID: 23632 RVA: 0x00140EC3 File Offset: 0x0013F0C3
		// (set) Token: 0x06005C51 RID: 23633 RVA: 0x00140ED5 File Offset: 0x0013F0D5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailContactSchema.OtherHomePhone];
			}
			set
			{
				this[SyncMailContactSchema.OtherHomePhone] = value;
			}
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x06005C52 RID: 23634 RVA: 0x00140EE3 File Offset: 0x0013F0E3
		// (set) Token: 0x06005C53 RID: 23635 RVA: 0x00140EF5 File Offset: 0x0013F0F5
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailContactSchema.OtherTelephone];
			}
			set
			{
				this[SyncMailContactSchema.OtherTelephone] = value;
			}
		}

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x06005C54 RID: 23636 RVA: 0x00140F03 File Offset: 0x0013F103
		// (set) Token: 0x06005C55 RID: 23637 RVA: 0x00140F15 File Offset: 0x0013F115
		[Parameter(Mandatory = false)]
		public string Pager
		{
			get
			{
				return (string)this[SyncMailContactSchema.Pager];
			}
			set
			{
				this[SyncMailContactSchema.Pager] = value;
			}
		}

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x06005C56 RID: 23638 RVA: 0x00140F23 File Offset: 0x0013F123
		// (set) Token: 0x06005C57 RID: 23639 RVA: 0x00140F35 File Offset: 0x0013F135
		[Parameter(Mandatory = false)]
		public string Phone
		{
			get
			{
				return (string)this[SyncMailContactSchema.Phone];
			}
			set
			{
				this[SyncMailContactSchema.Phone] = value;
			}
		}

		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x06005C58 RID: 23640 RVA: 0x00140F43 File Offset: 0x0013F143
		// (set) Token: 0x06005C59 RID: 23641 RVA: 0x00140F55 File Offset: 0x0013F155
		[Parameter(Mandatory = false)]
		public string PostalCode
		{
			get
			{
				return (string)this[SyncMailContactSchema.PostalCode];
			}
			set
			{
				this[SyncMailContactSchema.PostalCode] = value;
			}
		}

		// Token: 0x17002038 RID: 8248
		// (get) Token: 0x06005C5A RID: 23642 RVA: 0x00140F63 File Offset: 0x0013F163
		// (set) Token: 0x06005C5B RID: 23643 RVA: 0x00140F75 File Offset: 0x0013F175
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

		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x06005C5C RID: 23644 RVA: 0x00140F88 File Offset: 0x0013F188
		// (set) Token: 0x06005C5D RID: 23645 RVA: 0x00140F9A File Offset: 0x0013F19A
		[Parameter(Mandatory = false)]
		public int? ResourceCapacity
		{
			get
			{
				return (int?)this[ADRecipientSchema.ResourceCapacity];
			}
			set
			{
				this[ADRecipientSchema.ResourceCapacity] = value;
			}
		}

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x06005C5E RID: 23646 RVA: 0x00140FAD File Offset: 0x0013F1AD
		// (set) Token: 0x06005C5F RID: 23647 RVA: 0x00140FBF File Offset: 0x0013F1BF
		[Parameter(Mandatory = false)]
		public string StateOrProvince
		{
			get
			{
				return (string)this[SyncMailContactSchema.StateOrProvince];
			}
			set
			{
				this[SyncMailContactSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x06005C60 RID: 23648 RVA: 0x00140FCD File Offset: 0x0013F1CD
		// (set) Token: 0x06005C61 RID: 23649 RVA: 0x00140FDF File Offset: 0x0013F1DF
		[Parameter(Mandatory = false)]
		public string StreetAddress
		{
			get
			{
				return (string)this[SyncMailContactSchema.StreetAddress];
			}
			set
			{
				this[SyncMailContactSchema.StreetAddress] = value;
			}
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x06005C62 RID: 23650 RVA: 0x00140FED File Offset: 0x0013F1ED
		// (set) Token: 0x06005C63 RID: 23651 RVA: 0x00140FFF File Offset: 0x0013F1FF
		[Parameter(Mandatory = false)]
		public string TelephoneAssistant
		{
			get
			{
				return (string)this[SyncMailContactSchema.TelephoneAssistant];
			}
			set
			{
				this[SyncMailContactSchema.TelephoneAssistant] = value;
			}
		}

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x06005C64 RID: 23652 RVA: 0x0014100D File Offset: 0x0013F20D
		// (set) Token: 0x06005C65 RID: 23653 RVA: 0x0014101F File Offset: 0x0013F21F
		[Parameter(Mandatory = false)]
		public string Title
		{
			get
			{
				return (string)this[SyncMailContactSchema.Title];
			}
			set
			{
				this[SyncMailContactSchema.Title] = value;
			}
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x06005C66 RID: 23654 RVA: 0x0014102D File Offset: 0x0013F22D
		// (set) Token: 0x06005C67 RID: 23655 RVA: 0x0014103F File Offset: 0x0013F23F
		[Parameter(Mandatory = false)]
		public string WebPage
		{
			get
			{
				return (string)this[SyncMailContactSchema.WebPage];
			}
			set
			{
				this[SyncMailContactSchema.WebPage] = value;
			}
		}

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x06005C68 RID: 23656 RVA: 0x0014104D File Offset: 0x0013F24D
		// (set) Token: 0x06005C69 RID: 23657 RVA: 0x0014105F File Offset: 0x0013F25F
		public bool EndOfList
		{
			get
			{
				return (bool)this[SyncMailContactSchema.EndOfList];
			}
			internal set
			{
				this[SyncMailContactSchema.EndOfList] = value;
			}
		}

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x06005C6A RID: 23658 RVA: 0x00141072 File Offset: 0x0013F272
		// (set) Token: 0x06005C6B RID: 23659 RVA: 0x00141084 File Offset: 0x0013F284
		public byte[] Cookie
		{
			get
			{
				return (byte[])this[SyncMailContactSchema.Cookie];
			}
			internal set
			{
				this[SyncMailContactSchema.Cookie] = value;
			}
		}

		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x06005C6C RID: 23660 RVA: 0x00141092 File Offset: 0x0013F292
		// (set) Token: 0x06005C6D RID: 23661 RVA: 0x001410A4 File Offset: 0x0013F2A4
		[Parameter(Mandatory = false)]
		public int? SeniorityIndex
		{
			get
			{
				return (int?)this[SyncMailContactSchema.SeniorityIndex];
			}
			set
			{
				this[SyncMailContactSchema.SeniorityIndex] = value;
			}
		}

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x06005C6E RID: 23662 RVA: 0x001410B7 File Offset: 0x0013F2B7
		// (set) Token: 0x06005C6F RID: 23663 RVA: 0x001410C9 File Offset: 0x0013F2C9
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[SyncMailContactSchema.PhoneticDisplayName];
			}
			set
			{
				this[SyncMailContactSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x06005C70 RID: 23664 RVA: 0x001410D7 File Offset: 0x0013F2D7
		// (set) Token: 0x06005C71 RID: 23665 RVA: 0x001410E9 File Offset: 0x0013F2E9
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return (string)this[SyncMailContactSchema.OnPremisesObjectId];
			}
			set
			{
				this[SyncMailContactSchema.OnPremisesObjectId] = value;
			}
		}

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x06005C72 RID: 23666 RVA: 0x001410F7 File Offset: 0x0013F2F7
		// (set) Token: 0x06005C73 RID: 23667 RVA: 0x00141109 File Offset: 0x0013F309
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return (bool)this[SyncMailContactSchema.IsDirSynced];
			}
			set
			{
				this[SyncMailContactSchema.IsDirSynced] = value;
			}
		}

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x06005C74 RID: 23668 RVA: 0x0014111C File Offset: 0x0013F31C
		// (set) Token: 0x06005C75 RID: 23669 RVA: 0x0014112E File Offset: 0x0013F32E
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailContactSchema.DirSyncAuthorityMetadata];
			}
			set
			{
				this[SyncMailContactSchema.DirSyncAuthorityMetadata] = value;
			}
		}

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x06005C76 RID: 23670 RVA: 0x0014113C File Offset: 0x0013F33C
		// (set) Token: 0x06005C77 RID: 23671 RVA: 0x0014114E File Offset: 0x0013F34E
		[Parameter(Mandatory = false)]
		public bool ExcludedFromBackSync
		{
			get
			{
				return (bool)this[SyncMailContactSchema.ExcludedFromBackSync];
			}
			set
			{
				this[SyncMailContactSchema.ExcludedFromBackSync] = value;
			}
		}

		// Token: 0x04003EAD RID: 16045
		private static SyncMailContactSchema schema = ObjectSchema.GetInstance<SyncMailContactSchema>();
	}
}
