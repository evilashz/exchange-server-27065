using System;
using Microsoft.Exchange.Data.Directory.Provisioning;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001F7 RID: 503
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[ProvisioningObjectTag("ADPublicFolder")]
	[Serializable]
	public sealed class ADPublicFolder : ADRecipient
	{
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0006DAD1 File Offset: 0x0006BCD1
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADPublicFolder.schema;
			}
		}

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0006DAD8 File Offset: 0x0006BCD8
		internal override string ObjectCategoryName
		{
			get
			{
				return ADPublicFolder.ObjectCategoryNameInternal;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0006DADF File Offset: 0x0006BCDF
		public override string ObjectCategoryCN
		{
			get
			{
				return ADPublicFolder.ObjectCategoryCNInternal;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0006DAE6 File Offset: 0x0006BCE6
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADPublicFolder.MostDerivedClass;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0006DAED File Offset: 0x0006BCED
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001A2C RID: 6700 RVA: 0x0006DB00 File Offset: 0x0006BD00
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0006DB07 File Offset: 0x0006BD07
		internal ADPublicFolder(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0006DB11 File Offset: 0x0006BD11
		internal ADPublicFolder(IRecipientSession session, string commonName, ADObjectId containerId)
		{
			this.m_Session = session;
			base.SetId(containerId.GetChildId("CN", commonName));
			base.SetObjectClass(this.MostDerivedObjectClass);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0006DB3E File Offset: 0x0006BD3E
		public ADPublicFolder()
		{
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001A30 RID: 6704 RVA: 0x0006DB46 File Offset: 0x0006BD46
		// (set) Token: 0x06001A31 RID: 6705 RVA: 0x0006DB58 File Offset: 0x0006BD58
		public MultiValuedProperty<ADObjectId> Contacts
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ADPublicFolderSchema.Contacts];
			}
			internal set
			{
				this[ADPublicFolderSchema.Contacts] = value;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001A32 RID: 6706 RVA: 0x0006DB66 File Offset: 0x0006BD66
		// (set) Token: 0x06001A33 RID: 6707 RVA: 0x0006DB78 File Offset: 0x0006BD78
		public ADObjectId ContentMailbox
		{
			get
			{
				return (ADObjectId)this[ADRecipientSchema.DefaultPublicFolderMailbox];
			}
			internal set
			{
				this[ADRecipientSchema.DefaultPublicFolderMailbox] = value;
			}
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x0006DB86 File Offset: 0x0006BD86
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x0006DB98 File Offset: 0x0006BD98
		public ADObjectId Database
		{
			get
			{
				return (ADObjectId)this[ADPublicFolderSchema.Database];
			}
			set
			{
				this[ADPublicFolderSchema.Database] = value;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001A36 RID: 6710 RVA: 0x0006DBA6 File Offset: 0x0006BDA6
		// (set) Token: 0x06001A37 RID: 6711 RVA: 0x0006DBB8 File Offset: 0x0006BDB8
		public bool DeliverToMailboxAndForward
		{
			get
			{
				return (bool)this[ADPublicFolderSchema.DeliverToMailboxAndForward];
			}
			set
			{
				this[ADPublicFolderSchema.DeliverToMailboxAndForward] = value;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x0006DBCB File Offset: 0x0006BDCB
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x0006DBDD File Offset: 0x0006BDDD
		public string EntryId
		{
			get
			{
				return (string)this[ADPublicFolderSchema.EntryId];
			}
			internal set
			{
				this[ADPublicFolderSchema.EntryId] = value;
			}
		}

		// Token: 0x04000B6D RID: 2925
		private static readonly ADPublicFolderSchema schema = ObjectSchema.GetInstance<ADPublicFolderSchema>();

		// Token: 0x04000B6E RID: 2926
		internal static string MostDerivedClass = "publicFolder";

		// Token: 0x04000B6F RID: 2927
		internal static string ObjectCategoryNameInternal = "publicFolder";

		// Token: 0x04000B70 RID: 2928
		internal static string ObjectCategoryCNInternal = "ms-Exch-Public-Folder";
	}
}
