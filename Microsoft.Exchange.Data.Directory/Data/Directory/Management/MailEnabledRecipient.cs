using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006F5 RID: 1781
	[Serializable]
	public abstract class MailEnabledRecipient : ADPresentationObject
	{
		// Token: 0x06005361 RID: 21345 RVA: 0x00130B63 File Offset: 0x0012ED63
		protected MailEnabledRecipient()
		{
		}

		// Token: 0x06005362 RID: 21346 RVA: 0x00130B6B File Offset: 0x0012ED6B
		protected MailEnabledRecipient(ADObject dataObject) : base(dataObject)
		{
		}

		// Token: 0x17001B82 RID: 7042
		// (get) Token: 0x06005363 RID: 21347 RVA: 0x00130B74 File Offset: 0x0012ED74
		// (set) Token: 0x06005364 RID: 21348 RVA: 0x00130B86 File Offset: 0x0012ED86
		public MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.AcceptMessagesOnlyFrom];
			}
			set
			{
				this[MailEnabledRecipientSchema.AcceptMessagesOnlyFrom] = value;
			}
		}

		// Token: 0x17001B83 RID: 7043
		// (get) Token: 0x06005365 RID: 21349 RVA: 0x00130B94 File Offset: 0x0012ED94
		// (set) Token: 0x06005366 RID: 21350 RVA: 0x00130BA6 File Offset: 0x0012EDA6
		public MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.AcceptMessagesOnlyFromDLMembers];
			}
			set
			{
				this[MailEnabledRecipientSchema.AcceptMessagesOnlyFromDLMembers] = value;
			}
		}

		// Token: 0x17001B84 RID: 7044
		// (get) Token: 0x06005367 RID: 21351 RVA: 0x00130BB4 File Offset: 0x0012EDB4
		// (set) Token: 0x06005368 RID: 21352 RVA: 0x00130BC6 File Offset: 0x0012EDC6
		public MultiValuedProperty<ADObjectId> AcceptMessagesOnlyFromSendersOrMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers];
			}
			internal set
			{
				this[MailEnabledRecipientSchema.AcceptMessagesOnlyFromSendersOrMembers] = value;
			}
		}

		// Token: 0x17001B85 RID: 7045
		// (get) Token: 0x06005369 RID: 21353 RVA: 0x00130BD4 File Offset: 0x0012EDD4
		public MultiValuedProperty<ADObjectId> AddressListMembership
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.AddressListMembership];
			}
		}

		// Token: 0x17001B86 RID: 7046
		// (get) Token: 0x0600536A RID: 21354 RVA: 0x00130BE6 File Offset: 0x0012EDE6
		// (set) Token: 0x0600536B RID: 21355 RVA: 0x00130BF8 File Offset: 0x0012EDF8
		[Parameter(Mandatory = false)]
		public string Alias
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.Alias];
			}
			set
			{
				this[MailEnabledRecipientSchema.Alias] = value;
			}
		}

		// Token: 0x17001B87 RID: 7047
		// (get) Token: 0x0600536C RID: 21356 RVA: 0x00130C06 File Offset: 0x0012EE06
		// (set) Token: 0x0600536D RID: 21357 RVA: 0x00130C18 File Offset: 0x0012EE18
		public ADObjectId ArbitrationMailbox
		{
			get
			{
				return (ADObjectId)this[MailEnabledRecipientSchema.ArbitrationMailbox];
			}
			internal set
			{
				this[MailEnabledRecipientSchema.ArbitrationMailbox] = value;
			}
		}

		// Token: 0x17001B88 RID: 7048
		// (get) Token: 0x0600536E RID: 21358 RVA: 0x00130C26 File Offset: 0x0012EE26
		// (set) Token: 0x0600536F RID: 21359 RVA: 0x00130C38 File Offset: 0x0012EE38
		internal MultiValuedProperty<ADObjectId> BypassModerationFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.BypassModerationFrom];
			}
			set
			{
				this[MailEnabledRecipientSchema.BypassModerationFrom] = value;
			}
		}

		// Token: 0x17001B89 RID: 7049
		// (get) Token: 0x06005370 RID: 21360 RVA: 0x00130C46 File Offset: 0x0012EE46
		// (set) Token: 0x06005371 RID: 21361 RVA: 0x00130C58 File Offset: 0x0012EE58
		internal MultiValuedProperty<ADObjectId> BypassModerationFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.BypassModerationFromDLMembers];
			}
			set
			{
				this[MailEnabledRecipientSchema.BypassModerationFromDLMembers] = value;
			}
		}

		// Token: 0x17001B8A RID: 7050
		// (get) Token: 0x06005372 RID: 21362 RVA: 0x00130C66 File Offset: 0x0012EE66
		// (set) Token: 0x06005373 RID: 21363 RVA: 0x00130C78 File Offset: 0x0012EE78
		public MultiValuedProperty<ADObjectId> BypassModerationFromSendersOrMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.BypassModerationFromSendersOrMembers];
			}
			internal set
			{
				this[MailEnabledRecipientSchema.BypassModerationFromSendersOrMembers] = value;
			}
		}

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x06005374 RID: 21364 RVA: 0x00130C86 File Offset: 0x0012EE86
		public string OrganizationalUnit
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.OrganizationalUnit];
			}
		}

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x06005375 RID: 21365 RVA: 0x00130C98 File Offset: 0x0012EE98
		// (set) Token: 0x06005376 RID: 21366 RVA: 0x00130CAA File Offset: 0x0012EEAA
		[Parameter(Mandatory = false)]
		public string CustomAttribute1
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute1];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute1] = value;
			}
		}

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x06005377 RID: 21367 RVA: 0x00130CB8 File Offset: 0x0012EEB8
		// (set) Token: 0x06005378 RID: 21368 RVA: 0x00130CCA File Offset: 0x0012EECA
		[Parameter(Mandatory = false)]
		public string CustomAttribute10
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute10];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute10] = value;
			}
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x06005379 RID: 21369 RVA: 0x00130CD8 File Offset: 0x0012EED8
		// (set) Token: 0x0600537A RID: 21370 RVA: 0x00130CEA File Offset: 0x0012EEEA
		[Parameter(Mandatory = false)]
		public string CustomAttribute11
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute11];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute11] = value;
			}
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x0600537B RID: 21371 RVA: 0x00130CF8 File Offset: 0x0012EEF8
		// (set) Token: 0x0600537C RID: 21372 RVA: 0x00130D0A File Offset: 0x0012EF0A
		[Parameter(Mandatory = false)]
		public string CustomAttribute12
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute12];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute12] = value;
			}
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x0600537D RID: 21373 RVA: 0x00130D18 File Offset: 0x0012EF18
		// (set) Token: 0x0600537E RID: 21374 RVA: 0x00130D2A File Offset: 0x0012EF2A
		[Parameter(Mandatory = false)]
		public string CustomAttribute13
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute13];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute13] = value;
			}
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x0600537F RID: 21375 RVA: 0x00130D38 File Offset: 0x0012EF38
		// (set) Token: 0x06005380 RID: 21376 RVA: 0x00130D4A File Offset: 0x0012EF4A
		[Parameter(Mandatory = false)]
		public string CustomAttribute14
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute14];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute14] = value;
			}
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x06005381 RID: 21377 RVA: 0x00130D58 File Offset: 0x0012EF58
		// (set) Token: 0x06005382 RID: 21378 RVA: 0x00130D6A File Offset: 0x0012EF6A
		[Parameter(Mandatory = false)]
		public string CustomAttribute15
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute15];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute15] = value;
			}
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x00130D78 File Offset: 0x0012EF78
		// (set) Token: 0x06005384 RID: 21380 RVA: 0x00130D8A File Offset: 0x0012EF8A
		[Parameter(Mandatory = false)]
		public string CustomAttribute2
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute2];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute2] = value;
			}
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06005385 RID: 21381 RVA: 0x00130D98 File Offset: 0x0012EF98
		// (set) Token: 0x06005386 RID: 21382 RVA: 0x00130DAA File Offset: 0x0012EFAA
		[Parameter(Mandatory = false)]
		public string CustomAttribute3
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute3];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute3] = value;
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06005387 RID: 21383 RVA: 0x00130DB8 File Offset: 0x0012EFB8
		// (set) Token: 0x06005388 RID: 21384 RVA: 0x00130DCA File Offset: 0x0012EFCA
		[Parameter(Mandatory = false)]
		public string CustomAttribute4
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute4];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute4] = value;
			}
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06005389 RID: 21385 RVA: 0x00130DD8 File Offset: 0x0012EFD8
		// (set) Token: 0x0600538A RID: 21386 RVA: 0x00130DEA File Offset: 0x0012EFEA
		[Parameter(Mandatory = false)]
		public string CustomAttribute5
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute5];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute5] = value;
			}
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x0600538B RID: 21387 RVA: 0x00130DF8 File Offset: 0x0012EFF8
		// (set) Token: 0x0600538C RID: 21388 RVA: 0x00130E0A File Offset: 0x0012F00A
		[Parameter(Mandatory = false)]
		public string CustomAttribute6
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute6];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute6] = value;
			}
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x0600538D RID: 21389 RVA: 0x00130E18 File Offset: 0x0012F018
		// (set) Token: 0x0600538E RID: 21390 RVA: 0x00130E2A File Offset: 0x0012F02A
		[Parameter(Mandatory = false)]
		public string CustomAttribute7
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute7];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute7] = value;
			}
		}

		// Token: 0x17001B99 RID: 7065
		// (get) Token: 0x0600538F RID: 21391 RVA: 0x00130E38 File Offset: 0x0012F038
		// (set) Token: 0x06005390 RID: 21392 RVA: 0x00130E4A File Offset: 0x0012F04A
		[Parameter(Mandatory = false)]
		public string CustomAttribute8
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute8];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute8] = value;
			}
		}

		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x06005391 RID: 21393 RVA: 0x00130E58 File Offset: 0x0012F058
		// (set) Token: 0x06005392 RID: 21394 RVA: 0x00130E6A File Offset: 0x0012F06A
		[Parameter(Mandatory = false)]
		public string CustomAttribute9
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.CustomAttribute9];
			}
			set
			{
				this[MailEnabledRecipientSchema.CustomAttribute9] = value;
			}
		}

		// Token: 0x17001B9B RID: 7067
		// (get) Token: 0x06005393 RID: 21395 RVA: 0x00130E78 File Offset: 0x0012F078
		// (set) Token: 0x06005394 RID: 21396 RVA: 0x00130E8A File Offset: 0x0012F08A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.ExtensionCustomAttribute1];
			}
			set
			{
				this[MailEnabledRecipientSchema.ExtensionCustomAttribute1] = value;
			}
		}

		// Token: 0x17001B9C RID: 7068
		// (get) Token: 0x06005395 RID: 21397 RVA: 0x00130E98 File Offset: 0x0012F098
		// (set) Token: 0x06005396 RID: 21398 RVA: 0x00130EAA File Offset: 0x0012F0AA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.ExtensionCustomAttribute2];
			}
			set
			{
				this[MailEnabledRecipientSchema.ExtensionCustomAttribute2] = value;
			}
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06005397 RID: 21399 RVA: 0x00130EB8 File Offset: 0x0012F0B8
		// (set) Token: 0x06005398 RID: 21400 RVA: 0x00130ECA File Offset: 0x0012F0CA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.ExtensionCustomAttribute3];
			}
			set
			{
				this[MailEnabledRecipientSchema.ExtensionCustomAttribute3] = value;
			}
		}

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06005399 RID: 21401 RVA: 0x00130ED8 File Offset: 0x0012F0D8
		// (set) Token: 0x0600539A RID: 21402 RVA: 0x00130EEA File Offset: 0x0012F0EA
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.ExtensionCustomAttribute4];
			}
			set
			{
				this[MailEnabledRecipientSchema.ExtensionCustomAttribute4] = value;
			}
		}

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x0600539B RID: 21403 RVA: 0x00130EF8 File Offset: 0x0012F0F8
		// (set) Token: 0x0600539C RID: 21404 RVA: 0x00130F0A File Offset: 0x0012F10A
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> ExtensionCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.ExtensionCustomAttribute5];
			}
			set
			{
				this[MailEnabledRecipientSchema.ExtensionCustomAttribute5] = value;
			}
		}

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x0600539D RID: 21405 RVA: 0x00130F18 File Offset: 0x0012F118
		// (set) Token: 0x0600539E RID: 21406 RVA: 0x00130F2A File Offset: 0x0012F12A
		[Parameter(Mandatory = false)]
		public string DisplayName
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.DisplayName];
			}
			set
			{
				this[MailEnabledRecipientSchema.DisplayName] = value;
			}
		}

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x0600539F RID: 21407 RVA: 0x00130F38 File Offset: 0x0012F138
		// (set) Token: 0x060053A0 RID: 21408 RVA: 0x00130F4A File Offset: 0x0012F14A
		[Parameter(Mandatory = false)]
		public ProxyAddressCollection EmailAddresses
		{
			get
			{
				return (ProxyAddressCollection)this[MailEnabledRecipientSchema.EmailAddresses];
			}
			set
			{
				this[MailEnabledRecipientSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x060053A1 RID: 21409 RVA: 0x00130F58 File Offset: 0x0012F158
		// (set) Token: 0x060053A2 RID: 21410 RVA: 0x00130F6A File Offset: 0x0012F16A
		public MultiValuedProperty<ADObjectId> GrantSendOnBehalfTo
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.GrantSendOnBehalfTo];
			}
			set
			{
				this[MailEnabledRecipientSchema.GrantSendOnBehalfTo] = value;
			}
		}

		// Token: 0x17001BA3 RID: 7075
		// (get) Token: 0x060053A3 RID: 21411 RVA: 0x00130F78 File Offset: 0x0012F178
		public string ExternalDirectoryObjectId
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.ExternalDirectoryObjectId];
			}
		}

		// Token: 0x17001BA4 RID: 7076
		// (get) Token: 0x060053A4 RID: 21412 RVA: 0x00130F8A File Offset: 0x0012F18A
		// (set) Token: 0x060053A5 RID: 21413 RVA: 0x00130F9C File Offset: 0x0012F19C
		[Parameter(Mandatory = false)]
		public bool HiddenFromAddressListsEnabled
		{
			get
			{
				return (bool)this[MailEnabledRecipientSchema.HiddenFromAddressListsEnabled];
			}
			set
			{
				this[MailEnabledRecipientSchema.HiddenFromAddressListsEnabled] = value;
			}
		}

		// Token: 0x17001BA5 RID: 7077
		// (get) Token: 0x060053A6 RID: 21414 RVA: 0x00130FAF File Offset: 0x0012F1AF
		// (set) Token: 0x060053A7 RID: 21415 RVA: 0x00130FC1 File Offset: 0x0012F1C1
		[ProvisionalCloneEnabledState(CloneSet.CloneLimitedSet)]
		internal bool HiddenFromAddressListsValue
		{
			get
			{
				return (bool)this[MailEnabledRecipientSchema.HiddenFromAddressListsValue];
			}
			set
			{
				this[MailEnabledRecipientSchema.HiddenFromAddressListsValue] = value;
			}
		}

		// Token: 0x17001BA6 RID: 7078
		// (get) Token: 0x060053A8 RID: 21416 RVA: 0x00130FD4 File Offset: 0x0012F1D4
		public DateTime? LastExchangeChangedTime
		{
			get
			{
				return (DateTime?)this[MailEnabledRecipientSchema.LastExchangeChangedTime];
			}
		}

		// Token: 0x17001BA7 RID: 7079
		// (get) Token: 0x060053A9 RID: 21417 RVA: 0x00130FE6 File Offset: 0x0012F1E6
		public string LegacyExchangeDN
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.LegacyExchangeDN];
			}
		}

		// Token: 0x17001BA8 RID: 7080
		// (get) Token: 0x060053AA RID: 21418 RVA: 0x00130FF8 File Offset: 0x0012F1F8
		// (set) Token: 0x060053AB RID: 21419 RVA: 0x0013100A File Offset: 0x0012F20A
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MaxSendSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailEnabledRecipientSchema.MaxSendSize];
			}
			set
			{
				this[MailEnabledRecipientSchema.MaxSendSize] = value;
			}
		}

		// Token: 0x17001BA9 RID: 7081
		// (get) Token: 0x060053AC RID: 21420 RVA: 0x0013101D File Offset: 0x0012F21D
		// (set) Token: 0x060053AD RID: 21421 RVA: 0x0013102F File Offset: 0x0012F22F
		[Parameter(Mandatory = false)]
		[ProvisionalClone(CloneSet.CloneLimitedSet)]
		public Unlimited<ByteQuantifiedSize> MaxReceiveSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[MailEnabledRecipientSchema.MaxReceiveSize];
			}
			set
			{
				this[MailEnabledRecipientSchema.MaxReceiveSize] = value;
			}
		}

		// Token: 0x17001BAA RID: 7082
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x00131042 File Offset: 0x0012F242
		// (set) Token: 0x060053AF RID: 21423 RVA: 0x00131054 File Offset: 0x0012F254
		public MultiValuedProperty<ADObjectId> ModeratedBy
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.ModeratedBy];
			}
			set
			{
				this[MailEnabledRecipientSchema.ModeratedBy] = value;
			}
		}

		// Token: 0x17001BAB RID: 7083
		// (get) Token: 0x060053B0 RID: 21424 RVA: 0x00131062 File Offset: 0x0012F262
		// (set) Token: 0x060053B1 RID: 21425 RVA: 0x00131074 File Offset: 0x0012F274
		[Parameter(Mandatory = false)]
		public bool ModerationEnabled
		{
			get
			{
				return (bool)this[MailEnabledRecipientSchema.ModerationEnabled];
			}
			set
			{
				this[MailEnabledRecipientSchema.ModerationEnabled] = value;
			}
		}

		// Token: 0x17001BAC RID: 7084
		// (get) Token: 0x060053B2 RID: 21426 RVA: 0x00131087 File Offset: 0x0012F287
		public MultiValuedProperty<string> PoliciesIncluded
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.PoliciesIncluded];
			}
		}

		// Token: 0x17001BAD RID: 7085
		// (get) Token: 0x060053B3 RID: 21427 RVA: 0x00131099 File Offset: 0x0012F299
		public MultiValuedProperty<string> PoliciesExcluded
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.PoliciesExcluded];
			}
		}

		// Token: 0x17001BAE RID: 7086
		// (get) Token: 0x060053B4 RID: 21428 RVA: 0x001310AB File Offset: 0x0012F2AB
		// (set) Token: 0x060053B5 RID: 21429 RVA: 0x001310BD File Offset: 0x0012F2BD
		[Parameter(Mandatory = false)]
		public bool EmailAddressPolicyEnabled
		{
			get
			{
				return (bool)this[MailEnabledRecipientSchema.EmailAddressPolicyEnabled];
			}
			set
			{
				this[MailEnabledRecipientSchema.EmailAddressPolicyEnabled] = value;
			}
		}

		// Token: 0x17001BAF RID: 7087
		// (get) Token: 0x060053B6 RID: 21430 RVA: 0x001310D0 File Offset: 0x0012F2D0
		// (set) Token: 0x060053B7 RID: 21431 RVA: 0x001310E2 File Offset: 0x0012F2E2
		[Parameter(Mandatory = false)]
		public SmtpAddress PrimarySmtpAddress
		{
			get
			{
				return (SmtpAddress)this[MailEnabledRecipientSchema.PrimarySmtpAddress];
			}
			set
			{
				this[MailEnabledRecipientSchema.PrimarySmtpAddress] = value;
			}
		}

		// Token: 0x17001BB0 RID: 7088
		// (get) Token: 0x060053B8 RID: 21432 RVA: 0x001310F5 File Offset: 0x0012F2F5
		public RecipientType RecipientType
		{
			get
			{
				return (RecipientType)this[MailEnabledRecipientSchema.RecipientType];
			}
		}

		// Token: 0x17001BB1 RID: 7089
		// (get) Token: 0x060053B9 RID: 21433 RVA: 0x00131107 File Offset: 0x0012F307
		// (set) Token: 0x060053BA RID: 21434 RVA: 0x00131119 File Offset: 0x0012F319
		public RecipientTypeDetails RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails)this[MailEnabledRecipientSchema.RecipientTypeDetails];
			}
			internal set
			{
				this[MailEnabledRecipientSchema.RecipientTypeDetails] = value;
			}
		}

		// Token: 0x17001BB2 RID: 7090
		// (get) Token: 0x060053BB RID: 21435 RVA: 0x0013112C File Offset: 0x0012F32C
		// (set) Token: 0x060053BC RID: 21436 RVA: 0x0013113E File Offset: 0x0012F33E
		public MultiValuedProperty<ADObjectId> RejectMessagesFrom
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.RejectMessagesFrom];
			}
			set
			{
				this[MailEnabledRecipientSchema.RejectMessagesFrom] = value;
			}
		}

		// Token: 0x17001BB3 RID: 7091
		// (get) Token: 0x060053BD RID: 21437 RVA: 0x0013114C File Offset: 0x0012F34C
		// (set) Token: 0x060053BE RID: 21438 RVA: 0x0013115E File Offset: 0x0012F35E
		public MultiValuedProperty<ADObjectId> RejectMessagesFromDLMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.RejectMessagesFromDLMembers];
			}
			set
			{
				this[MailEnabledRecipientSchema.RejectMessagesFromDLMembers] = value;
			}
		}

		// Token: 0x17001BB4 RID: 7092
		// (get) Token: 0x060053BF RID: 21439 RVA: 0x0013116C File Offset: 0x0012F36C
		// (set) Token: 0x060053C0 RID: 21440 RVA: 0x0013117E File Offset: 0x0012F37E
		public MultiValuedProperty<ADObjectId> RejectMessagesFromSendersOrMembers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[MailEnabledRecipientSchema.RejectMessagesFromSendersOrMembers];
			}
			internal set
			{
				this[MailEnabledRecipientSchema.RejectMessagesFromSendersOrMembers] = value;
			}
		}

		// Token: 0x17001BB5 RID: 7093
		// (get) Token: 0x060053C1 RID: 21441 RVA: 0x0013118C File Offset: 0x0012F38C
		// (set) Token: 0x060053C2 RID: 21442 RVA: 0x0013119E File Offset: 0x0012F39E
		[Parameter(Mandatory = false)]
		public bool RequireSenderAuthenticationEnabled
		{
			get
			{
				return (bool)this[MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled];
			}
			set
			{
				this[MailEnabledRecipientSchema.RequireSenderAuthenticationEnabled] = value;
			}
		}

		// Token: 0x17001BB6 RID: 7094
		// (get) Token: 0x060053C3 RID: 21443 RVA: 0x001311B1 File Offset: 0x0012F3B1
		// (set) Token: 0x060053C4 RID: 21444 RVA: 0x001311C3 File Offset: 0x0012F3C3
		[Parameter(Mandatory = false)]
		public string SimpleDisplayName
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.SimpleDisplayName];
			}
			set
			{
				this[MailEnabledRecipientSchema.SimpleDisplayName] = value;
			}
		}

		// Token: 0x17001BB7 RID: 7095
		// (get) Token: 0x060053C5 RID: 21445 RVA: 0x001311D1 File Offset: 0x0012F3D1
		// (set) Token: 0x060053C6 RID: 21446 RVA: 0x001311E3 File Offset: 0x0012F3E3
		[Parameter(Mandatory = false)]
		public TransportModerationNotificationFlags SendModerationNotifications
		{
			get
			{
				return (TransportModerationNotificationFlags)this[MailEnabledRecipientSchema.SendModerationNotifications];
			}
			set
			{
				this[MailEnabledRecipientSchema.SendModerationNotifications] = value;
			}
		}

		// Token: 0x17001BB8 RID: 7096
		// (get) Token: 0x060053C7 RID: 21447 RVA: 0x001311F6 File Offset: 0x0012F3F6
		// (set) Token: 0x060053C8 RID: 21448 RVA: 0x00131208 File Offset: 0x0012F408
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> UMDtmfMap
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.UMDtmfMap];
			}
			set
			{
				this[MailEnabledRecipientSchema.UMDtmfMap] = value;
			}
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x060053C9 RID: 21449 RVA: 0x00131216 File Offset: 0x0012F416
		// (set) Token: 0x060053CA RID: 21450 RVA: 0x00131228 File Offset: 0x0012F428
		[Parameter(Mandatory = false)]
		public SmtpAddress WindowsEmailAddress
		{
			get
			{
				return (SmtpAddress)this[MailEnabledRecipientSchema.WindowsEmailAddress];
			}
			set
			{
				this[MailEnabledRecipientSchema.WindowsEmailAddress] = value;
			}
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x060053CB RID: 21451 RVA: 0x0013123B File Offset: 0x0012F43B
		// (set) Token: 0x060053CC RID: 21452 RVA: 0x0013124D File Offset: 0x0012F44D
		[Parameter(Mandatory = false)]
		public string MailTip
		{
			get
			{
				return (string)this[MailEnabledRecipientSchema.MailTip];
			}
			set
			{
				this[MailEnabledRecipientSchema.MailTip] = value;
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x060053CD RID: 21453 RVA: 0x0013125B File Offset: 0x0012F45B
		// (set) Token: 0x060053CE RID: 21454 RVA: 0x0013126D File Offset: 0x0012F46D
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> MailTipTranslations
		{
			get
			{
				return (MultiValuedProperty<string>)this[MailEnabledRecipientSchema.MailTipTranslations];
			}
			set
			{
				this[MailEnabledRecipientSchema.MailTipTranslations] = value;
			}
		}

		// Token: 0x060053CF RID: 21455 RVA: 0x0013127C File Offset: 0x0012F47C
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.DisplayName))
			{
				return this.DisplayName;
			}
			if (!string.IsNullOrEmpty(base.Name))
			{
				return base.Name;
			}
			if (base.Id != null)
			{
				return base.Id.ToString();
			}
			return base.ToString();
		}
	}
}
