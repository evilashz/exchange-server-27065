using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200037E RID: 894
	internal class ListViewNotificationDataSource : IListViewDataSource
	{
		// Token: 0x06002160 RID: 8544 RVA: 0x000BFBA0 File Offset: 0x000BDDA0
		public ListViewNotificationDataSource(UserContext userContext, StoreObjectId folderId, bool conversationMode, Dictionary<PropertyDefinition, int> propertyIndices, SortBy[] sortBy, QueryNotification notification)
		{
			this.userContext = userContext;
			this.propertyIndices = propertyIndices;
			this.notification = notification;
			this.folderId = folderId;
			this.sortBy = sortBy;
			this.conversationMode = conversationMode;
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x000BFBF6 File Offset: 0x000BDDF6
		public Folder Folder
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x000BFBF9 File Offset: 0x000BDDF9
		public SortBy[] SortBy
		{
			get
			{
				return this.sortBy;
			}
		}

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06002163 RID: 8547 RVA: 0x000BFC01 File Offset: 0x000BDE01
		public virtual int TotalCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x000BFC04 File Offset: 0x000BDE04
		public virtual int TotalItemCount
		{
			get
			{
				return this.TotalCount;
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x000BFC0C File Offset: 0x000BDE0C
		public int UnreadCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x000BFC0F File Offset: 0x000BDE0F
		public string ContainerId
		{
			get
			{
				return this.folderId.ToBase64String();
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x000BFC1C File Offset: 0x000BDE1C
		public bool UserHasRightToLoad
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000BFC1F File Offset: 0x000BDE1F
		public void Load(int startRange, int itemCount)
		{
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000BFC21 File Offset: 0x000BDE21
		public void Load(ObjectId seekToObjectId, SeekDirection seekDirection, int itemCount)
		{
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000BFC23 File Offset: 0x000BDE23
		public void Load(string seekValue, int itemCount)
		{
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000BFC25 File Offset: 0x000BDE25
		public bool LoadAdjacent(ObjectId adjacentObjectId, SeekDirection seekDirection, int itemCount)
		{
			return true;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000BFC28 File Offset: 0x000BDE28
		public object GetItemProperty(int item, PropertyDefinition propertyDefinition)
		{
			int num = this.propertyIndices[propertyDefinition];
			return this.notification.Row[num];
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000BFC50 File Offset: 0x000BDE50
		public virtual T GetItemProperty<T>(PropertyDefinition propertyDefinition, T defaultValue)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			if (!this.propertyIndices.ContainsKey(propertyDefinition))
			{
				return defaultValue;
			}
			int num = this.propertyIndices[propertyDefinition];
			object obj = this.notification.Row[num];
			if (obj == null || !(obj is T))
			{
				return defaultValue;
			}
			if (obj is ExDateTime)
			{
				obj = this.userContext.TimeZone.ConvertDateTime((ExDateTime)obj);
			}
			return (T)((object)obj);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x000BFCD0 File Offset: 0x000BDED0
		public virtual T GetItemProperty<T>(PropertyDefinition propertyDefinition) where T : class
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			int num = this.propertyIndices[propertyDefinition];
			return this.notification.Row[num] as T;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000BFD11 File Offset: 0x000BDF11
		public VersionedId GetItemPropertyVersionedId(int item, PropertyDefinition propertyDefinition)
		{
			return this.GetItemProperty(item, propertyDefinition) as VersionedId;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000BFD20 File Offset: 0x000BDF20
		public string GetItemPropertyString(int item, PropertyDefinition propertyDefinition)
		{
			string text = this.GetItemProperty(item, propertyDefinition) as string;
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x000BFD48 File Offset: 0x000BDF48
		public ExDateTime GetItemPropertyExDateTime(int item, PropertyDefinition propertyDefinition)
		{
			object itemProperty = this.GetItemProperty(item, propertyDefinition);
			if (itemProperty is DateTime)
			{
				throw new OwaInvalidInputException("List view item property must be ExDateTime not DateTime");
			}
			if (itemProperty is ExDateTime)
			{
				return (ExDateTime)itemProperty;
			}
			return ExDateTime.MinValue;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000BFD88 File Offset: 0x000BDF88
		public int GetItemPropertyInt(int item, PropertyDefinition propertyDefinition, int defaultValue)
		{
			object itemProperty = this.GetItemProperty(item, propertyDefinition);
			if (!(itemProperty is int))
			{
				return defaultValue;
			}
			return (int)itemProperty;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x000BFDB0 File Offset: 0x000BDFB0
		public bool GetItemPropertyBool(int item, PropertyDefinition propertyDefinition, bool defaultValue)
		{
			object itemProperty = this.GetItemProperty(item, propertyDefinition);
			if (!(itemProperty is bool))
			{
				return defaultValue;
			}
			return (bool)itemProperty;
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x000BFDD8 File Offset: 0x000BDFD8
		public string GetChangeKey()
		{
			VersionedId itemProperty = this.GetItemProperty<VersionedId>(ItemSchema.Id);
			return itemProperty.ChangeKeyAsBase64String();
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x000BFDF8 File Offset: 0x000BDFF8
		public string GetItemClass()
		{
			if (this.conversationMode)
			{
				return "IPM.Conversation";
			}
			string text = this.GetItemProperty<string>(StoreObjectSchema.ItemClass, null);
			if (string.IsNullOrEmpty(text))
			{
				text = "IPM.Unknown";
			}
			return text;
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x000BFE2F File Offset: 0x000BE02F
		// (set) Token: 0x06002177 RID: 8567 RVA: 0x000BFE37 File Offset: 0x000BE037
		public int StartRange
		{
			get
			{
				return this.startRange;
			}
			protected set
			{
				this.startRange = value;
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x000BFE40 File Offset: 0x000BE040
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x000BFE48 File Offset: 0x000BE048
		public int EndRange
		{
			get
			{
				return this.endRange;
			}
			protected set
			{
				this.endRange = value;
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x0600217A RID: 8570 RVA: 0x000BFE51 File Offset: 0x000BE051
		public int RangeCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x0600217B RID: 8571 RVA: 0x000BFE54 File Offset: 0x000BE054
		public virtual int CurrentItem
		{
			get
			{
				return this.currentItem;
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x000BFE5C File Offset: 0x000BE05C
		public virtual bool MoveNext()
		{
			return true;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x000BFE5F File Offset: 0x000BE05F
		public virtual void MoveToItem(int itemIndex)
		{
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x000BFE61 File Offset: 0x000BE061
		protected void SetIndexer(int index)
		{
			this.currentItem = index;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x000BFE6A File Offset: 0x000BE06A
		public virtual object GetCurrentItem()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x000BFE74 File Offset: 0x000BE074
		public string GetItemId()
		{
			if (this.conversationMode)
			{
				ConversationId itemProperty = this.GetItemProperty<ConversationId>(ConversationItemSchema.ConversationId);
				byte[] itemProperty2 = this.GetItemProperty<byte[]>(ItemSchema.InstanceKey);
				if (itemProperty != null)
				{
					return OwaStoreObjectId.CreateFromConversationIdForListViewNotification(itemProperty, this.folderId, itemProperty2).ToString();
				}
				return null;
			}
			else
			{
				VersionedId itemProperty3 = this.GetItemProperty<VersionedId>(ItemSchema.Id);
				if (itemProperty3 != null)
				{
					return OwaStoreObjectId.CreateFromItemId(itemProperty3.ObjectId, this.folderId, OwaStoreObjectIdType.MailBoxObject, null).ToString();
				}
				return null;
			}
		}

		// Token: 0x040017BC RID: 6076
		private const string ConversationItemType = "IPM.Conversation";

		// Token: 0x040017BD RID: 6077
		private const int TotalCountValue = 1;

		// Token: 0x040017BE RID: 6078
		private UserContext userContext;

		// Token: 0x040017BF RID: 6079
		private Dictionary<PropertyDefinition, int> propertyIndices;

		// Token: 0x040017C0 RID: 6080
		private StoreObjectId folderId;

		// Token: 0x040017C1 RID: 6081
		private int startRange = int.MinValue;

		// Token: 0x040017C2 RID: 6082
		private int endRange = int.MinValue;

		// Token: 0x040017C3 RID: 6083
		private int currentItem;

		// Token: 0x040017C4 RID: 6084
		private bool conversationMode;

		// Token: 0x040017C5 RID: 6085
		private SortBy[] sortBy;

		// Token: 0x040017C6 RID: 6086
		private QueryNotification notification;
	}
}
