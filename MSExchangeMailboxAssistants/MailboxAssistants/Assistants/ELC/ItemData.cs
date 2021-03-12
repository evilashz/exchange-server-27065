using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x0200003E RID: 62
	internal class ItemData
	{
		// Token: 0x06000241 RID: 577 RVA: 0x0000E434 File Offset: 0x0000C634
		internal ItemData(VersionedId itemId, int messageSize)
		{
			this.id = itemId;
			this.messageReceivedDate = DateTime.MinValue;
			this.itemAuditLogData = null;
			this.parentId = null;
			this.messageSize = messageSize;
			this.enforcerType = ItemData.EnforcerType.None;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000E480 File Offset: 0x0000C680
		internal ItemData(VersionedId itemId, ItemData.EnforcerType enforcerType, int messageSize)
		{
			this.id = itemId;
			this.messageReceivedDate = DateTime.MinValue;
			this.itemAuditLogData = null;
			this.parentId = null;
			this.messageSize = messageSize;
			this.enforcerType = enforcerType;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		internal ItemData(VersionedId itemId, StoreObjectId parentId, int messageSize)
		{
			this.id = itemId;
			this.messageReceivedDate = DateTime.MinValue;
			this.itemAuditLogData = null;
			this.parentId = parentId;
			this.messageSize = messageSize;
			this.enforcerType = ItemData.EnforcerType.None;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000E518 File Offset: 0x0000C718
		internal ItemData(VersionedId itemId, StoreObjectId parentId, ItemData.EnforcerType enforcerType, int messageSize)
		{
			this.id = itemId;
			this.messageReceivedDate = DateTime.MinValue;
			this.itemAuditLogData = null;
			this.parentId = parentId;
			this.messageSize = messageSize;
			this.enforcerType = enforcerType;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000E565 File Offset: 0x0000C765
		internal ItemData(VersionedId itemId, DateTime messageReceivedDate, ItemAuditLogData itemAuditLogData, int messageSize)
		{
			this.id = itemId;
			this.messageReceivedDate = messageReceivedDate;
			this.itemAuditLogData = itemAuditLogData;
			this.parentId = null;
			this.messageSize = messageSize;
			this.enforcerType = ItemData.EnforcerType.None;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000E5A3 File Offset: 0x0000C7A3
		internal VersionedId Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000E5AB File Offset: 0x0000C7AB
		internal DateTime MessageReceivedDate
		{
			get
			{
				return this.messageReceivedDate;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000E5B3 File Offset: 0x0000C7B3
		internal int MessageSize
		{
			get
			{
				return this.messageSize;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000E5BB File Offset: 0x0000C7BB
		internal ItemAuditLogData ItemAuditLogData
		{
			get
			{
				return this.itemAuditLogData;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000E5C3 File Offset: 0x0000C7C3
		internal StoreObjectId ParentId
		{
			get
			{
				return this.parentId;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000E5CB File Offset: 0x0000C7CB
		internal ItemData.EnforcerType Enforcer
		{
			get
			{
				return this.enforcerType;
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		internal static StoreObjectId[] ListToStoreObjectIdArray(List<ItemData> itemDataList)
		{
			int num = (itemDataList == null) ? 0 : itemDataList.Count;
			StoreObjectId[] array = new StoreObjectId[num];
			if (itemDataList != null && itemDataList.Count > 0)
			{
				int num2 = 0;
				foreach (ItemData itemData in itemDataList)
				{
					array[num2] = itemData.Id.ObjectId;
					num2++;
				}
			}
			return array;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000E654 File Offset: 0x0000C854
		internal static Dictionary<ItemData.EnforcerType, int> GetNumberOfItemsProcessedByEachEnforcer(ItemData[] sourceArray, int sourceIndex, int size)
		{
			Dictionary<ItemData.EnforcerType, int> dictionary = new Dictionary<ItemData.EnforcerType, int>();
			for (int i = sourceIndex; i < sourceIndex + size; i++)
			{
				if (!dictionary.ContainsKey(sourceArray[i].Enforcer))
				{
					dictionary[sourceArray[i].Enforcer] = 0;
				}
				Dictionary<ItemData.EnforcerType, int> dictionary2;
				ItemData.EnforcerType enforcer;
				(dictionary2 = dictionary)[enforcer = sourceArray[i].Enforcer] = dictionary2[enforcer] + 1;
			}
			return dictionary;
		}

		// Token: 0x040001CD RID: 461
		private VersionedId id;

		// Token: 0x040001CE RID: 462
		private StoreObjectId parentId;

		// Token: 0x040001CF RID: 463
		private DateTime messageReceivedDate = DateTime.MinValue;

		// Token: 0x040001D0 RID: 464
		private int messageSize;

		// Token: 0x040001D1 RID: 465
		private ItemAuditLogData itemAuditLogData;

		// Token: 0x040001D2 RID: 466
		private ItemData.EnforcerType enforcerType;

		// Token: 0x0200003F RID: 63
		internal enum EnforcerType
		{
			// Token: 0x040001D4 RID: 468
			None,
			// Token: 0x040001D5 RID: 469
			DumpsterExpirationEnforcer,
			// Token: 0x040001D6 RID: 470
			DumpsterQuotaEnforcer,
			// Token: 0x040001D7 RID: 471
			DiscoveryHoldEnforcer,
			// Token: 0x040001D8 RID: 472
			ExpirationTagEnforcer
		}
	}
}
