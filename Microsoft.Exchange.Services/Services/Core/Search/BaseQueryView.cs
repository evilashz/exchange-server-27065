using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200025E RID: 606
	internal abstract class BaseQueryView
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0004D157 File Offset: 0x0004B357
		// (set) Token: 0x06000FDE RID: 4062 RVA: 0x0004D15F File Offset: 0x0004B35F
		public bool RetrievedLastItem
		{
			get
			{
				return this.retrievedLastItem;
			}
			set
			{
				this.retrievedLastItem = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0004D168 File Offset: 0x0004B368
		public virtual int TotalItems
		{
			get
			{
				return this.totalItems;
			}
		}

		// Token: 0x06000FE0 RID: 4064
		public abstract ItemType[] ConvertToItems(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession);

		// Token: 0x06000FE1 RID: 4065
		public abstract FindItemParentWrapper ConvertToFindItemParentWrapper(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession, BasePageResult pageResult, QueryType queryType);

		// Token: 0x06000FE2 RID: 4066
		public abstract BaseFolderType[] ConvertToFolderObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession);

		// Token: 0x06000FE3 RID: 4067
		public abstract ConversationType[] ConvertToConversationObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession, RequestDetailsLogger logger);

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0004D170 File Offset: 0x0004B370
		public Persona[] ConvertPersonViewToPersonaObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession)
		{
			return this.ConvertPersonViewToPersonaObjects(propsToFetch, classDeterminer, idAndSession.Session as MailboxSession);
		}

		// Token: 0x06000FE5 RID: 4069
		public abstract Persona[] ConvertPersonViewToPersonaObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, MailboxSession storeSession);

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0004D188 File Offset: 0x0004B388
		internal static IDictionary<PropertyDefinition, object> GetRowData(PropertyDefinition[] keys, object[] viewRow)
		{
			IDictionary<PropertyDefinition, object> dictionary = new Dictionary<PropertyDefinition, object>();
			for (int i = 0; i < keys.Length; i++)
			{
				if (!(viewRow[i] is PropertyError))
				{
					dictionary[keys[i]] = viewRow[i];
				}
			}
			return dictionary;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004D1C0 File Offset: 0x0004B3C0
		protected bool CanAllocateFoundObjects(BasePagingType paging, uint foundCount, out int maxPossible)
		{
			if (!CallContext.Current.Budget.CanAllocateFoundObjects(foundCount, out maxPossible))
			{
				if (paging == null || !paging.BudgetInducedTruncationAllowed)
				{
					ExceededFindCountLimitException.Throw();
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0004D1E8 File Offset: 0x0004B3E8
		protected int AllocateBudgetFoundObjects(int rowsToFetch, BasePagingType paging)
		{
			int num = rowsToFetch;
			if (rowsToFetch > 0)
			{
				while (!this.IncrementBudgetFoundObjects(rowsToFetch, paging, out num))
				{
					rowsToFetch = num;
					if (rowsToFetch <= 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0004D211 File Offset: 0x0004B411
		private bool IncrementBudgetFoundObjects(int rowsObtained, BasePagingType paging, out int maxAllowed)
		{
			maxAllowed = rowsObtained;
			if (rowsObtained > 0 && CallContext.Current != null && !CallContext.Current.Budget.TryIncrementFoundObjectCount((uint)rowsObtained, out maxAllowed))
			{
				if (paging == null || !paging.BudgetInducedTruncationAllowed)
				{
					ExceededFindCountLimitException.Throw();
				}
				return false;
			}
			return true;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0004D247 File Offset: 0x0004B447
		protected virtual void CheckClientConnection()
		{
			if (CallContext.Current != null && !CallContext.Current.IsClientConnected)
			{
				BailOut.SetHTTPStatusAndClose(HttpStatusCode.NoContent);
			}
		}

		// Token: 0x04000BE3 RID: 3043
		protected bool retrievedLastItem;

		// Token: 0x04000BE4 RID: 3044
		protected int totalItems;
	}
}
