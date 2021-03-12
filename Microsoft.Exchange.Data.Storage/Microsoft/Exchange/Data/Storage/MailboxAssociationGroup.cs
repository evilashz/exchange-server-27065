using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007DB RID: 2011
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAssociationGroup : MailboxAssociationBaseItem, IMailboxAssociationGroup, IMailboxAssociationBaseItem, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06004B66 RID: 19302 RVA: 0x0013A83C File Offset: 0x00138A3C
		internal MailboxAssociationGroup(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x17001590 RID: 5520
		// (get) Token: 0x06004B67 RID: 19303 RVA: 0x0013A848 File Offset: 0x00138A48
		// (set) Token: 0x06004B68 RID: 19304 RVA: 0x0013A874 File Offset: 0x00138A74
		public ExDateTime PinDate
		{
			get
			{
				this.CheckDisposed("PinDate::get");
				return base.GetValueOrDefault<ExDateTime>(MailboxAssociationGroupSchema.PinDate, default(ExDateTime));
			}
			set
			{
				this.CheckDisposed("PinDate::set");
				this[MailboxAssociationGroupSchema.PinDate] = value;
			}
		}

		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06004B69 RID: 19305 RVA: 0x0013A892 File Offset: 0x00138A92
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MailboxAssociationGroupSchema.Instance;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06004B6A RID: 19306 RVA: 0x0013A8A4 File Offset: 0x00138AA4
		public override string AssociationItemClass
		{
			get
			{
				return "IPM.MailboxAssociation.Group";
			}
		}

		// Token: 0x06004B6B RID: 19307 RVA: 0x0013A8AC File Offset: 0x00138AAC
		public static MailboxAssociationGroup Create(StoreSession session, StoreId folderId)
		{
			MailboxAssociationGroup mailboxAssociationGroup = ItemBuilder.CreateNewItem<MailboxAssociationGroup>(session, folderId, ItemCreateInfo.MailboxAssociationGroupInfo);
			mailboxAssociationGroup[StoreObjectSchema.ItemClass] = "IPM.MailboxAssociation.Group";
			return mailboxAssociationGroup;
		}

		// Token: 0x06004B6C RID: 19308 RVA: 0x0013A8D7 File Offset: 0x00138AD7
		public new static MailboxAssociationGroup Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MailboxAssociationGroup>(session, storeId, MailboxAssociationGroupSchema.Instance, propsToReturn);
		}

		// Token: 0x06004B6D RID: 19309 RVA: 0x0013A8E8 File Offset: 0x00138AE8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			base.AppendDescriptionTo(stringBuilder);
			stringBuilder.Append(", PinDate=");
			stringBuilder.Append(this.PinDate);
			return stringBuilder.ToString();
		}
	}
}
