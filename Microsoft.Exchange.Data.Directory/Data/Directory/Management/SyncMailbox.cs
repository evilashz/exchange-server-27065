using System;
using System.Management.Automation;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000761 RID: 1889
	[ProvisioningObjectTag("SyncMailbox")]
	[Serializable]
	public class SyncMailbox : Mailbox
	{
		// Token: 0x17001FDF RID: 8159
		// (get) Token: 0x06005BA8 RID: 23464 RVA: 0x001402B3 File Offset: 0x0013E4B3
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return SyncMailbox.schema;
			}
		}

		// Token: 0x06005BA9 RID: 23465 RVA: 0x001402BA File Offset: 0x0013E4BA
		public SyncMailbox()
		{
			base.SetObjectClass("user");
		}

		// Token: 0x06005BAA RID: 23466 RVA: 0x001402CD File Offset: 0x0013E4CD
		public SyncMailbox(ADUser dataObject) : base(dataObject)
		{
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x001402D6 File Offset: 0x0013E4D6
		internal new static SyncMailbox FromDataObject(ADUser dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new SyncMailbox(dataObject);
		}

		// Token: 0x17001FE0 RID: 8160
		// (get) Token: 0x06005BAC RID: 23468 RVA: 0x001402E3 File Offset: 0x0013E4E3
		// (set) Token: 0x06005BAD RID: 23469 RVA: 0x001402F5 File Offset: 0x0013E4F5
		[Parameter(Mandatory = false)]
		public string AssistantName
		{
			get
			{
				return (string)this[SyncMailboxSchema.AssistantName];
			}
			set
			{
				this[SyncMailboxSchema.AssistantName] = value;
			}
		}

		// Token: 0x17001FE1 RID: 8161
		// (get) Token: 0x06005BAE RID: 23470 RVA: 0x00140303 File Offset: 0x0013E503
		// (set) Token: 0x06005BAF RID: 23471 RVA: 0x00140315 File Offset: 0x0013E515
		[Parameter(Mandatory = false)]
		public byte[] BlockedSendersHash
		{
			get
			{
				return (byte[])this[SyncMailboxSchema.BlockedSendersHash];
			}
			set
			{
				this[SyncMailboxSchema.BlockedSendersHash] = value;
			}
		}

		// Token: 0x17001FE2 RID: 8162
		// (get) Token: 0x06005BB0 RID: 23472 RVA: 0x00140323 File Offset: 0x0013E523
		// (set) Token: 0x06005BB1 RID: 23473 RVA: 0x0014032B File Offset: 0x0013E52B
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

		// Token: 0x17001FE3 RID: 8163
		// (get) Token: 0x06005BB2 RID: 23474 RVA: 0x00140334 File Offset: 0x0013E534
		// (set) Token: 0x06005BB3 RID: 23475 RVA: 0x0014033C File Offset: 0x0013E53C
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

		// Token: 0x17001FE4 RID: 8164
		// (get) Token: 0x06005BB4 RID: 23476 RVA: 0x00140345 File Offset: 0x0013E545
		public MultiValuedProperty<byte[]> Certificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MailboxSchema.UserCertificate];
			}
		}

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x06005BB5 RID: 23477 RVA: 0x00140357 File Offset: 0x0013E557
		// (set) Token: 0x06005BB6 RID: 23478 RVA: 0x00140369 File Offset: 0x0013E569
		[Parameter(Mandatory = false)]
		public SecurityIdentifier MasterAccountSid
		{
			get
			{
				return (SecurityIdentifier)this[MailboxSchema.MasterAccountSid];
			}
			set
			{
				this[MailboxSchema.MasterAccountSid] = value;
			}
		}

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x06005BB7 RID: 23479 RVA: 0x00140377 File Offset: 0x0013E577
		// (set) Token: 0x06005BB8 RID: 23480 RVA: 0x00140389 File Offset: 0x0013E589
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return (string)this[SyncMailboxSchema.Notes];
			}
			set
			{
				this[SyncMailboxSchema.Notes] = value;
			}
		}

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x06005BB9 RID: 23481 RVA: 0x00140397 File Offset: 0x0013E597
		// (set) Token: 0x06005BBA RID: 23482 RVA: 0x001403A9 File Offset: 0x0013E5A9
		public RecipientDisplayType? RecipientDisplayType
		{
			get
			{
				return (RecipientDisplayType?)this[SyncMailboxSchema.RecipientDisplayType];
			}
			internal set
			{
				this[SyncMailboxSchema.RecipientDisplayType] = value;
			}
		}

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x06005BBB RID: 23483 RVA: 0x001403BC File Offset: 0x0013E5BC
		// (set) Token: 0x06005BBC RID: 23484 RVA: 0x001403CE File Offset: 0x0013E5CE
		[Parameter(Mandatory = false)]
		public byte[] SafeRecipientsHash
		{
			get
			{
				return (byte[])this[SyncMailboxSchema.SafeRecipientsHash];
			}
			set
			{
				this[SyncMailboxSchema.SafeRecipientsHash] = value;
			}
		}

		// Token: 0x17001FE9 RID: 8169
		// (get) Token: 0x06005BBD RID: 23485 RVA: 0x001403DC File Offset: 0x0013E5DC
		// (set) Token: 0x06005BBE RID: 23486 RVA: 0x001403EE File Offset: 0x0013E5EE
		[Parameter(Mandatory = false)]
		public byte[] SafeSendersHash
		{
			get
			{
				return (byte[])this[SyncMailboxSchema.SafeSendersHash];
			}
			set
			{
				this[SyncMailboxSchema.SafeSendersHash] = value;
			}
		}

		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x06005BBF RID: 23487 RVA: 0x001403FC File Offset: 0x0013E5FC
		public MultiValuedProperty<byte[]> SMimeCertificate
		{
			get
			{
				return (MultiValuedProperty<byte[]>)this[MailboxSchema.UserSMimeCertificate];
			}
		}

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x06005BC0 RID: 23488 RVA: 0x0014040E File Offset: 0x0013E60E
		// (set) Token: 0x06005BC1 RID: 23489 RVA: 0x00140420 File Offset: 0x0013E620
		[Parameter(Mandatory = false)]
		public byte[] Picture
		{
			get
			{
				return (byte[])this[SyncMailboxSchema.ThumbnailPhoto];
			}
			set
			{
				this[SyncMailboxSchema.ThumbnailPhoto] = value;
			}
		}

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x06005BC2 RID: 23490 RVA: 0x0014042E File Offset: 0x0013E62E
		// (set) Token: 0x06005BC3 RID: 23491 RVA: 0x00140440 File Offset: 0x0013E640
		[Parameter(Mandatory = false)]
		public byte[] SpokenName
		{
			get
			{
				return (byte[])this[SyncMailboxSchema.UMSpokenName];
			}
			set
			{
				this[SyncMailboxSchema.UMSpokenName] = value;
			}
		}

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x06005BC4 RID: 23492 RVA: 0x0014044E File Offset: 0x0013E64E
		// (set) Token: 0x06005BC5 RID: 23493 RVA: 0x00140460 File Offset: 0x0013E660
		[Parameter(Mandatory = false)]
		public string DirSyncId
		{
			get
			{
				return (string)this[SyncMailboxSchema.DirSyncId];
			}
			set
			{
				this[SyncMailboxSchema.DirSyncId] = value;
			}
		}

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x06005BC6 RID: 23494 RVA: 0x0014046E File Offset: 0x0013E66E
		// (set) Token: 0x06005BC7 RID: 23495 RVA: 0x00140480 File Offset: 0x0013E680
		[Parameter(Mandatory = false)]
		public string City
		{
			get
			{
				return (string)this[SyncMailboxSchema.City];
			}
			set
			{
				this[SyncMailboxSchema.City] = value;
			}
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x06005BC8 RID: 23496 RVA: 0x0014048E File Offset: 0x0013E68E
		// (set) Token: 0x06005BC9 RID: 23497 RVA: 0x001404A0 File Offset: 0x0013E6A0
		[Parameter(Mandatory = false)]
		public string Company
		{
			get
			{
				return (string)this[SyncMailboxSchema.Company];
			}
			set
			{
				this[SyncMailboxSchema.Company] = value;
			}
		}

		// Token: 0x17001FF0 RID: 8176
		// (get) Token: 0x06005BCA RID: 23498 RVA: 0x001404AE File Offset: 0x0013E6AE
		// (set) Token: 0x06005BCB RID: 23499 RVA: 0x001404C0 File Offset: 0x0013E6C0
		[Parameter(Mandatory = false)]
		public CountryInfo CountryOrRegion
		{
			get
			{
				return (CountryInfo)this[SyncMailboxSchema.CountryOrRegion];
			}
			set
			{
				this[SyncMailboxSchema.CountryOrRegion] = value;
			}
		}

		// Token: 0x17001FF1 RID: 8177
		// (get) Token: 0x06005BCC RID: 23500 RVA: 0x001404CE File Offset: 0x0013E6CE
		// (set) Token: 0x06005BCD RID: 23501 RVA: 0x001404E0 File Offset: 0x0013E6E0
		public string C
		{
			get
			{
				return (string)this[SyncMailboxSchema.C];
			}
			set
			{
				this[SyncMailboxSchema.C] = value;
			}
		}

		// Token: 0x17001FF2 RID: 8178
		// (get) Token: 0x06005BCE RID: 23502 RVA: 0x001404EE File Offset: 0x0013E6EE
		// (set) Token: 0x06005BCF RID: 23503 RVA: 0x00140500 File Offset: 0x0013E700
		public string Co
		{
			get
			{
				return (string)this[SyncMailboxSchema.Co];
			}
			set
			{
				this[SyncMailboxSchema.Co] = value;
			}
		}

		// Token: 0x17001FF3 RID: 8179
		// (get) Token: 0x06005BD0 RID: 23504 RVA: 0x0014050E File Offset: 0x0013E70E
		// (set) Token: 0x06005BD1 RID: 23505 RVA: 0x00140520 File Offset: 0x0013E720
		[Parameter(Mandatory = false)]
		public int CountryCode
		{
			get
			{
				return (int)this[SyncMailboxSchema.CountryCode];
			}
			set
			{
				this[SyncMailboxSchema.CountryCode] = value;
			}
		}

		// Token: 0x17001FF4 RID: 8180
		// (get) Token: 0x06005BD2 RID: 23506 RVA: 0x00140533 File Offset: 0x0013E733
		// (set) Token: 0x06005BD3 RID: 23507 RVA: 0x00140545 File Offset: 0x0013E745
		[Parameter(Mandatory = false)]
		public string Department
		{
			get
			{
				return (string)this[SyncMailboxSchema.Department];
			}
			set
			{
				this[SyncMailboxSchema.Department] = value;
			}
		}

		// Token: 0x17001FF5 RID: 8181
		// (get) Token: 0x06005BD4 RID: 23508 RVA: 0x00140553 File Offset: 0x0013E753
		// (set) Token: 0x06005BD5 RID: 23509 RVA: 0x00140565 File Offset: 0x0013E765
		[Parameter(Mandatory = false)]
		public string Fax
		{
			get
			{
				return (string)this[SyncMailboxSchema.Fax];
			}
			set
			{
				this[SyncMailboxSchema.Fax] = value;
			}
		}

		// Token: 0x17001FF6 RID: 8182
		// (get) Token: 0x06005BD6 RID: 23510 RVA: 0x00140573 File Offset: 0x0013E773
		// (set) Token: 0x06005BD7 RID: 23511 RVA: 0x00140585 File Offset: 0x0013E785
		[Parameter(Mandatory = false)]
		public string FirstName
		{
			get
			{
				return (string)this[SyncMailboxSchema.FirstName];
			}
			set
			{
				this[SyncMailboxSchema.FirstName] = value;
			}
		}

		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x06005BD8 RID: 23512 RVA: 0x00140593 File Offset: 0x0013E793
		// (set) Token: 0x06005BD9 RID: 23513 RVA: 0x001405A5 File Offset: 0x0013E7A5
		[Parameter(Mandatory = false)]
		public string HomePhone
		{
			get
			{
				return (string)this[SyncMailboxSchema.HomePhone];
			}
			set
			{
				this[SyncMailboxSchema.HomePhone] = value;
			}
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x06005BDA RID: 23514 RVA: 0x001405B3 File Offset: 0x0013E7B3
		// (set) Token: 0x06005BDB RID: 23515 RVA: 0x001405C5 File Offset: 0x0013E7C5
		[Parameter(Mandatory = false)]
		public string Initials
		{
			get
			{
				return (string)this[SyncMailboxSchema.Initials];
			}
			set
			{
				this[SyncMailboxSchema.Initials] = value;
			}
		}

		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x06005BDC RID: 23516 RVA: 0x001405D3 File Offset: 0x0013E7D3
		// (set) Token: 0x06005BDD RID: 23517 RVA: 0x001405E5 File Offset: 0x0013E7E5
		[Parameter(Mandatory = false)]
		public string LastName
		{
			get
			{
				return (string)this[SyncMailboxSchema.LastName];
			}
			set
			{
				this[SyncMailboxSchema.LastName] = value;
			}
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x06005BDE RID: 23518 RVA: 0x001405F3 File Offset: 0x0013E7F3
		public ADObjectId Manager
		{
			get
			{
				return (ADObjectId)this[SyncMailboxSchema.Manager];
			}
		}

		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x06005BDF RID: 23519 RVA: 0x00140605 File Offset: 0x0013E805
		// (set) Token: 0x06005BE0 RID: 23520 RVA: 0x00140617 File Offset: 0x0013E817
		[Parameter(Mandatory = false)]
		public string MobilePhone
		{
			get
			{
				return (string)this[SyncMailboxSchema.MobilePhone];
			}
			set
			{
				this[SyncMailboxSchema.MobilePhone] = value;
			}
		}

		// Token: 0x17001FFC RID: 8188
		// (get) Token: 0x06005BE1 RID: 23521 RVA: 0x00140625 File Offset: 0x0013E825
		// (set) Token: 0x06005BE2 RID: 23522 RVA: 0x00140637 File Offset: 0x0013E837
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherFax
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailboxSchema.OtherFax];
			}
			set
			{
				this[SyncMailboxSchema.OtherFax] = value;
			}
		}

		// Token: 0x17001FFD RID: 8189
		// (get) Token: 0x06005BE3 RID: 23523 RVA: 0x00140645 File Offset: 0x0013E845
		// (set) Token: 0x06005BE4 RID: 23524 RVA: 0x00140657 File Offset: 0x0013E857
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherHomePhone
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailboxSchema.OtherHomePhone];
			}
			set
			{
				this[SyncMailboxSchema.OtherHomePhone] = value;
			}
		}

		// Token: 0x17001FFE RID: 8190
		// (get) Token: 0x06005BE5 RID: 23525 RVA: 0x00140665 File Offset: 0x0013E865
		// (set) Token: 0x06005BE6 RID: 23526 RVA: 0x00140677 File Offset: 0x0013E877
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> OtherTelephone
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailboxSchema.OtherTelephone];
			}
			set
			{
				this[SyncMailboxSchema.OtherTelephone] = value;
			}
		}

		// Token: 0x17001FFF RID: 8191
		// (get) Token: 0x06005BE7 RID: 23527 RVA: 0x00140685 File Offset: 0x0013E885
		// (set) Token: 0x06005BE8 RID: 23528 RVA: 0x00140697 File Offset: 0x0013E897
		[Parameter(Mandatory = false)]
		public string Pager
		{
			get
			{
				return (string)this[SyncMailboxSchema.Pager];
			}
			set
			{
				this[SyncMailboxSchema.Pager] = value;
			}
		}

		// Token: 0x17002000 RID: 8192
		// (get) Token: 0x06005BE9 RID: 23529 RVA: 0x001406A5 File Offset: 0x0013E8A5
		// (set) Token: 0x06005BEA RID: 23530 RVA: 0x001406B7 File Offset: 0x0013E8B7
		[Parameter(Mandatory = false)]
		public string Phone
		{
			get
			{
				return (string)this[SyncMailboxSchema.Phone];
			}
			set
			{
				this[SyncMailboxSchema.Phone] = value;
			}
		}

		// Token: 0x17002001 RID: 8193
		// (get) Token: 0x06005BEB RID: 23531 RVA: 0x001406C5 File Offset: 0x0013E8C5
		// (set) Token: 0x06005BEC RID: 23532 RVA: 0x001406D7 File Offset: 0x0013E8D7
		[Parameter(Mandatory = false)]
		public string PostalCode
		{
			get
			{
				return (string)this[SyncMailboxSchema.PostalCode];
			}
			set
			{
				this[SyncMailboxSchema.PostalCode] = value;
			}
		}

		// Token: 0x17002002 RID: 8194
		// (get) Token: 0x06005BED RID: 23533 RVA: 0x001406E5 File Offset: 0x0013E8E5
		// (set) Token: 0x06005BEE RID: 23534 RVA: 0x001406F7 File Offset: 0x0013E8F7
		[Parameter(Mandatory = false)]
		public string StateOrProvince
		{
			get
			{
				return (string)this[SyncMailboxSchema.StateOrProvince];
			}
			set
			{
				this[SyncMailboxSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x17002003 RID: 8195
		// (get) Token: 0x06005BEF RID: 23535 RVA: 0x00140705 File Offset: 0x0013E905
		// (set) Token: 0x06005BF0 RID: 23536 RVA: 0x00140717 File Offset: 0x0013E917
		[Parameter(Mandatory = false)]
		public string StreetAddress
		{
			get
			{
				return (string)this[SyncMailboxSchema.StreetAddress];
			}
			set
			{
				this[SyncMailboxSchema.StreetAddress] = value;
			}
		}

		// Token: 0x17002004 RID: 8196
		// (get) Token: 0x06005BF1 RID: 23537 RVA: 0x00140725 File Offset: 0x0013E925
		// (set) Token: 0x06005BF2 RID: 23538 RVA: 0x00140737 File Offset: 0x0013E937
		[Parameter(Mandatory = false)]
		public string TelephoneAssistant
		{
			get
			{
				return (string)this[SyncMailboxSchema.TelephoneAssistant];
			}
			set
			{
				this[SyncMailboxSchema.TelephoneAssistant] = value;
			}
		}

		// Token: 0x17002005 RID: 8197
		// (get) Token: 0x06005BF3 RID: 23539 RVA: 0x00140745 File Offset: 0x0013E945
		// (set) Token: 0x06005BF4 RID: 23540 RVA: 0x00140757 File Offset: 0x0013E957
		[Parameter(Mandatory = false)]
		public string Title
		{
			get
			{
				return (string)this[SyncMailboxSchema.Title];
			}
			set
			{
				this[SyncMailboxSchema.Title] = value;
			}
		}

		// Token: 0x17002006 RID: 8198
		// (get) Token: 0x06005BF5 RID: 23541 RVA: 0x00140765 File Offset: 0x0013E965
		// (set) Token: 0x06005BF6 RID: 23542 RVA: 0x00140777 File Offset: 0x0013E977
		[Parameter(Mandatory = false)]
		public string WebPage
		{
			get
			{
				return (string)this[SyncMailboxSchema.WebPage];
			}
			set
			{
				this[SyncMailboxSchema.WebPage] = value;
			}
		}

		// Token: 0x17002007 RID: 8199
		// (get) Token: 0x06005BF7 RID: 23543 RVA: 0x00140785 File Offset: 0x0013E985
		// (set) Token: 0x06005BF8 RID: 23544 RVA: 0x00140797 File Offset: 0x0013E997
		[Parameter(Mandatory = false)]
		public int? SeniorityIndex
		{
			get
			{
				return (int?)this[SyncMailboxSchema.SeniorityIndex];
			}
			set
			{
				this[SyncMailboxSchema.SeniorityIndex] = value;
			}
		}

		// Token: 0x17002008 RID: 8200
		// (get) Token: 0x06005BF9 RID: 23545 RVA: 0x001407AA File Offset: 0x0013E9AA
		// (set) Token: 0x06005BFA RID: 23546 RVA: 0x001407BC File Offset: 0x0013E9BC
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[SyncMailboxSchema.PhoneticDisplayName];
			}
			set
			{
				this[SyncMailboxSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x17002009 RID: 8201
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x001407CA File Offset: 0x0013E9CA
		public SecurityIdentifier Sid
		{
			get
			{
				return (SecurityIdentifier)this[SyncMailboxSchema.Sid];
			}
		}

		// Token: 0x1700200A RID: 8202
		// (get) Token: 0x06005BFC RID: 23548 RVA: 0x001407DC File Offset: 0x0013E9DC
		public MultiValuedProperty<SecurityIdentifier> SidHistory
		{
			get
			{
				return (MultiValuedProperty<SecurityIdentifier>)this[SyncMailboxSchema.SidHistory];
			}
		}

		// Token: 0x1700200B RID: 8203
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x001407EE File Offset: 0x0013E9EE
		// (set) Token: 0x06005BFE RID: 23550 RVA: 0x00140800 File Offset: 0x0013EA00
		public ReleaseTrack? ReleaseTrack
		{
			get
			{
				return (ReleaseTrack?)this[SyncMailboxSchema.ReleaseTrack];
			}
			set
			{
				this[SyncMailboxSchema.ReleaseTrack] = value;
			}
		}

		// Token: 0x1700200C RID: 8204
		// (get) Token: 0x06005BFF RID: 23551 RVA: 0x00140813 File Offset: 0x0013EA13
		// (set) Token: 0x06005C00 RID: 23552 RVA: 0x00140825 File Offset: 0x0013EA25
		public bool EndOfList
		{
			get
			{
				return (bool)this[SyncMailboxSchema.EndOfList];
			}
			internal set
			{
				this[SyncMailboxSchema.EndOfList] = value;
			}
		}

		// Token: 0x1700200D RID: 8205
		// (get) Token: 0x06005C01 RID: 23553 RVA: 0x00140838 File Offset: 0x0013EA38
		// (set) Token: 0x06005C02 RID: 23554 RVA: 0x0014084A File Offset: 0x0013EA4A
		public byte[] Cookie
		{
			get
			{
				return (byte[])this[SyncMailboxSchema.Cookie];
			}
			internal set
			{
				this[SyncMailboxSchema.Cookie] = value;
			}
		}

		// Token: 0x1700200E RID: 8206
		// (get) Token: 0x06005C03 RID: 23555 RVA: 0x00140858 File Offset: 0x0013EA58
		// (set) Token: 0x06005C04 RID: 23556 RVA: 0x0014086A File Offset: 0x0013EA6A
		public string MailboxPlanName
		{
			get
			{
				return (string)this[SyncMailboxSchema.MailboxPlanName];
			}
			internal set
			{
				this[SyncMailboxSchema.MailboxPlanName] = value;
			}
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x06005C05 RID: 23557 RVA: 0x00140878 File Offset: 0x0013EA78
		// (set) Token: 0x06005C06 RID: 23558 RVA: 0x0014088A File Offset: 0x0013EA8A
		[Parameter(Mandatory = false)]
		public string OnPremisesObjectId
		{
			get
			{
				return (string)this[SyncMailboxSchema.OnPremisesObjectId];
			}
			set
			{
				this[SyncMailboxSchema.OnPremisesObjectId] = value;
			}
		}

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x06005C07 RID: 23559 RVA: 0x00140898 File Offset: 0x0013EA98
		// (set) Token: 0x06005C08 RID: 23560 RVA: 0x001408AA File Offset: 0x0013EAAA
		[Parameter(Mandatory = false)]
		public bool IsDirSynced
		{
			get
			{
				return (bool)this[SyncMailboxSchema.IsDirSynced];
			}
			set
			{
				this[SyncMailboxSchema.IsDirSynced] = value;
			}
		}

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x06005C09 RID: 23561 RVA: 0x001408BD File Offset: 0x0013EABD
		// (set) Token: 0x06005C0A RID: 23562 RVA: 0x001408CF File Offset: 0x0013EACF
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> DirSyncAuthorityMetadata
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailboxSchema.DirSyncAuthorityMetadata];
			}
			set
			{
				this[SyncMailboxSchema.DirSyncAuthorityMetadata] = value;
			}
		}

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x001408DD File Offset: 0x0013EADD
		// (set) Token: 0x06005C0C RID: 23564 RVA: 0x001408EF File Offset: 0x0013EAEF
		[Parameter(Mandatory = false)]
		public bool ExcludedFromBackSync
		{
			get
			{
				return (bool)this[SyncMailboxSchema.ExcludedFromBackSync];
			}
			set
			{
				this[SyncMailboxSchema.ExcludedFromBackSync] = value;
			}
		}

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x06005C0D RID: 23565 RVA: 0x00140902 File Offset: 0x0013EB02
		// (set) Token: 0x06005C0E RID: 23566 RVA: 0x00140914 File Offset: 0x0013EB14
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> InPlaceHoldsRaw
		{
			get
			{
				return (MultiValuedProperty<string>)this[SyncMailboxSchema.InPlaceHoldsRaw];
			}
			set
			{
				this[SyncMailboxSchema.InPlaceHoldsRaw] = value;
			}
		}

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x06005C0F RID: 23567 RVA: 0x00140922 File Offset: 0x0013EB22
		// (set) Token: 0x06005C10 RID: 23568 RVA: 0x00140934 File Offset: 0x0013EB34
		[Parameter(Mandatory = false)]
		public bool LEOEnabled
		{
			get
			{
				return (bool)this[SyncMailboxSchema.LEOEnabled];
			}
			set
			{
				this[SyncMailboxSchema.LEOEnabled] = value;
			}
		}

		// Token: 0x17002015 RID: 8213
		// (get) Token: 0x06005C11 RID: 23569 RVA: 0x00140947 File Offset: 0x0013EB47
		// (set) Token: 0x06005C12 RID: 23570 RVA: 0x00140959 File Offset: 0x0013EB59
		[Parameter(Mandatory = false)]
		public bool AccountDisabled
		{
			get
			{
				return (bool)this[SyncMailboxSchema.AccountDisabled];
			}
			set
			{
				this[SyncMailboxSchema.AccountDisabled] = value;
			}
		}

		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x06005C13 RID: 23571 RVA: 0x0014096C File Offset: 0x0013EB6C
		// (set) Token: 0x06005C14 RID: 23572 RVA: 0x0014097E File Offset: 0x0013EB7E
		[Parameter(Mandatory = false)]
		public DateTime? StsRefreshTokensValidFrom
		{
			get
			{
				return (DateTime?)this[SyncMailboxSchema.StsRefreshTokensValidFrom];
			}
			set
			{
				this[SyncMailboxSchema.StsRefreshTokensValidFrom] = value;
			}
		}

		// Token: 0x04003E81 RID: 16001
		private static SyncMailboxSchema schema = ObjectSchema.GetInstance<SyncMailboxSchema>();
	}
}
