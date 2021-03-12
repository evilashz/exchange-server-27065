using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020007DC RID: 2012
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxAssociationUser : MailboxAssociationBaseItem, IMailboxAssociationUser, IMailboxAssociationBaseItem, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x06004B6E RID: 19310 RVA: 0x0013A928 File Offset: 0x00138B28
		internal MailboxAssociationUser(ICoreItem coreItem) : base(coreItem)
		{
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06004B6F RID: 19311 RVA: 0x0013A931 File Offset: 0x00138B31
		// (set) Token: 0x06004B70 RID: 19312 RVA: 0x0013A94A File Offset: 0x00138B4A
		public string JoinedBy
		{
			get
			{
				this.CheckDisposed("JoinedBy::get");
				return base.GetValueOrDefault<string>(MailboxAssociationUserSchema.JoinedBy, null);
			}
			set
			{
				this.CheckDisposed("JoinedBy::set");
				this[MailboxAssociationUserSchema.JoinedBy] = value;
			}
		}

		// Token: 0x17001594 RID: 5524
		// (get) Token: 0x06004B71 RID: 19313 RVA: 0x0013A963 File Offset: 0x00138B63
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return MailboxAssociationUserSchema.Instance;
			}
		}

		// Token: 0x17001595 RID: 5525
		// (get) Token: 0x06004B72 RID: 19314 RVA: 0x0013A975 File Offset: 0x00138B75
		public override string AssociationItemClass
		{
			get
			{
				return "IPM.MailboxAssociation.User";
			}
		}

		// Token: 0x17001596 RID: 5526
		// (get) Token: 0x06004B73 RID: 19315 RVA: 0x0013A97C File Offset: 0x00138B7C
		// (set) Token: 0x06004B74 RID: 19316 RVA: 0x0013A9A8 File Offset: 0x00138BA8
		public ExDateTime LastVisitedDate
		{
			get
			{
				this.CheckDisposed("LastVisitedDate::get");
				return base.GetValueOrDefault<ExDateTime>(MailboxAssociationUserSchema.LastVisitedDate, default(ExDateTime));
			}
			set
			{
				this.CheckDisposed("LastVisitedDate::set");
				this[MailboxAssociationUserSchema.LastVisitedDate] = value;
			}
		}

		// Token: 0x06004B75 RID: 19317 RVA: 0x0013A9C8 File Offset: 0x00138BC8
		public static MailboxAssociationUser Create(StoreSession session, StoreId folderId)
		{
			MailboxAssociationUser mailboxAssociationUser = ItemBuilder.CreateNewItem<MailboxAssociationUser>(session, folderId, ItemCreateInfo.MailboxAssociationUserInfo);
			mailboxAssociationUser[StoreObjectSchema.ItemClass] = "IPM.MailboxAssociation.User";
			return mailboxAssociationUser;
		}

		// Token: 0x06004B76 RID: 19318 RVA: 0x0013A9F3 File Offset: 0x00138BF3
		public new static MailboxAssociationUser Bind(StoreSession session, StoreId storeId, ICollection<PropertyDefinition> propsToReturn)
		{
			return ItemBuilder.ItemBind<MailboxAssociationUser>(session, storeId, MailboxAssociationUserSchema.Instance, propsToReturn);
		}

		// Token: 0x06004B77 RID: 19319 RVA: 0x0013AA04 File Offset: 0x00138C04
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			base.AppendDescriptionTo(stringBuilder);
			stringBuilder.Append(", JoinedBy=");
			stringBuilder.Append(this.JoinedBy);
			stringBuilder.Append(", LastVisitedDate=");
			stringBuilder.Append(this.LastVisitedDate);
			return stringBuilder.ToString();
		}
	}
}
