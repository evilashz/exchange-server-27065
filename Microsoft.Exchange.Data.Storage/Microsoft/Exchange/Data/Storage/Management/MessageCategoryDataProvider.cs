using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A81 RID: 2689
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MessageCategoryDataProvider : XsoMailboxDataProviderBase
	{
		// Token: 0x17001B40 RID: 6976
		// (get) Token: 0x0600626C RID: 25196 RVA: 0x0019FBFB File Offset: 0x0019DDFB
		internal ADUser ADMailboxOwner
		{
			get
			{
				return this.adMailboxOwner;
			}
		}

		// Token: 0x0600626D RID: 25197 RVA: 0x0019FC03 File Offset: 0x0019DE03
		public MessageCategoryDataProvider(ADSessionSettings adSessionSettings, ADUser mailboxOwner, string action) : base(adSessionSettings, mailboxOwner, action)
		{
			this.adMailboxOwner = mailboxOwner;
		}

		// Token: 0x0600626E RID: 25198 RVA: 0x0019FC15 File Offset: 0x0019DE15
		internal MessageCategoryDataProvider()
		{
		}

		// Token: 0x0600626F RID: 25199 RVA: 0x0019FF38 File Offset: 0x0019E138
		protected override IEnumerable<T> InternalFindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize)
		{
			MessageCategoryId messageCategoryId = rootId as MessageCategoryId;
			if (sortBy != null)
			{
				throw new NotSupportedException("sortBy");
			}
			if (rootId != null && messageCategoryId == null)
			{
				throw new NotSupportedException("rootId");
			}
			MasterCategoryList categoryList = base.MailboxSession.GetMasterCategoryList();
			if (messageCategoryId == null || (messageCategoryId.Name == null && messageCategoryId.CategoryId == null))
			{
				foreach (Category category in categoryList)
				{
					yield return (T)((object)this.ConvertCategoryToPresentationObject(category));
				}
			}
			else if (messageCategoryId.CategoryId != null)
			{
				Category category2 = categoryList[messageCategoryId.CategoryId.Value];
				yield return (T)((object)this.ConvertCategoryToPresentationObject(category2));
			}
			else
			{
				Category category3 = categoryList[messageCategoryId.Name];
				if (category3 != null)
				{
					yield return (T)((object)this.ConvertCategoryToPresentationObject(category3));
				}
			}
			yield break;
		}

		// Token: 0x06006270 RID: 25200 RVA: 0x0019FF64 File Offset: 0x0019E164
		protected override void InternalSave(ConfigurableObject instance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006271 RID: 25201 RVA: 0x0019FF6B File Offset: 0x0019E16B
		protected override void InternalDelete(ConfigurableObject instance)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06006272 RID: 25202 RVA: 0x0019FF72 File Offset: 0x0019E172
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MessageCategoryDataProvider>(this);
		}

		// Token: 0x06006273 RID: 25203 RVA: 0x0019FF7A File Offset: 0x0019E17A
		protected override void InternalDispose(bool disposing)
		{
			base.InternalDispose(disposing);
		}

		// Token: 0x06006274 RID: 25204 RVA: 0x0019FF84 File Offset: 0x0019E184
		private MessageCategory ConvertCategoryToPresentationObject(Category category)
		{
			return new MessageCategory
			{
				MailboxOwnerId = base.MailboxSession.MailboxOwner.ObjectId,
				Name = category.Name,
				Color = category.Color,
				Guid = category.Guid
			};
		}

		// Token: 0x040037C4 RID: 14276
		private ADUser adMailboxOwner;
	}
}
