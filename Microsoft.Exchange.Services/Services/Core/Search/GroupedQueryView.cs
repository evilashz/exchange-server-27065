using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.Search
{
	// Token: 0x0200026B RID: 619
	internal class GroupedQueryView : BaseQueryView
	{
		// Token: 0x0600102D RID: 4141 RVA: 0x0004DDA4 File Offset: 0x0004BFA4
		public GroupedQueryView(GroupedQueryResult groupedResult, int rowsToGet, int groupByPropDefIndex, BasePagingType pagingType)
		{
			this.CombineRPCGroupedViewData(groupedResult, rowsToGet, groupByPropDefIndex, pagingType);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0004DDC4 File Offset: 0x0004BFC4
		private static string GenerateGroupByKey(object groupByValue)
		{
			string text = string.Empty;
			byte[] array = groupByValue as byte[];
			if (array != null)
			{
				text = Convert.ToBase64String(array);
			}
			else
			{
				PropertyError propertyError = groupByValue as PropertyError;
				if (propertyError != null)
				{
					ExTraceGlobals.SearchTracer.TraceDebug(0L, "[GroupedQueryView::GenerateGroupByKey] GroupByValue was property error: " + propertyError.ToString());
					if (propertyError.PropertyErrorCode == PropertyErrorCode.NotFound)
					{
						text = string.Empty;
					}
					else
					{
						text = string.Format("[{0}]", propertyError.PropertyErrorCode.ToString());
					}
				}
				else
				{
					text = groupByValue.ToString();
				}
			}
			if (text.Length > 255)
			{
				text = text.Substring(0, 255);
			}
			return text;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0004DE60 File Offset: 0x0004C060
		private void CombineRPCGroupedViewData(GroupedQueryResult groupedQuery, int rowsToGet, int groupByPropDefIndex, BasePagingType pagingType)
		{
			int num = groupedQuery.EstimatedRowCount - groupedQuery.CurrentRow;
			this.totalItems = groupedQuery.EstimatedItemCount;
			if (rowsToGet > num)
			{
				rowsToGet = num;
			}
			int num2 = base.AllocateBudgetFoundObjects(rowsToGet, pagingType);
			bool flag = num2 > 0;
			while (flag)
			{
				this.CheckClientConnection();
				object[][][] viewArray = groupedQuery.GetViewArray(num2);
				if (num2 == 1 && viewArray.GetLength(0) == 0)
				{
					viewArray = groupedQuery.GetViewArray(2);
				}
				int length = viewArray.GetLength(0);
				int num3 = 0;
				for (int i = 0; i < length; i++)
				{
					object[][] array = viewArray[i];
					object groupByValue = array[0][groupByPropDefIndex];
					string key = GroupedQueryView.GenerateGroupByKey(groupByValue);
					List<object[]> list;
					if (!this.groupedView.TryGetValue(key, out list))
					{
						list = new List<object[]>();
						this.groupedView.Add(key, list);
					}
					int num4 = array.GetLength(0);
					for (int j = 0; j < num4; j++)
					{
						if (num2 == 0)
						{
							groupedQuery.SeekToOffset(SeekReference.OriginCurrent, j - num4);
							num4 = j;
							break;
						}
						list.Add(array[j]);
						num2--;
					}
					num3 += num4;
				}
				flag = (length > 0 && num2 > 0);
			}
			this.retrievedLastItem = (groupedQuery.CurrentRow == groupedQuery.EstimatedRowCount);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0004DF95 File Offset: 0x0004C195
		public override ItemType[] ConvertToItems(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession)
		{
			throw new InvalidOperationException("GroupedQueryView::ConvertToItemType");
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0004DFA4 File Offset: 0x0004C1A4
		public override FindItemParentWrapper ConvertToFindItemParentWrapper(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession, BasePageResult pageResult, QueryType queryType)
		{
			GroupType[] array = new GroupType[this.GetItemCount()];
			int num = 0;
			foreach (KeyValuePair<string, List<object[]>> keyValuePair in this.groupedView)
			{
				List<object[]> value = keyValuePair.Value;
				array[num] = new GroupType();
				array[num].GroupIndex = num;
				array[num].Items = new ItemType[value.Count];
				for (int i = 0; i < value.Count; i++)
				{
					this.CheckClientConnection();
					IDictionary<PropertyDefinition, object> rowData = BaseQueryView.GetRowData(propsToFetch, value[i]);
					StoreObjectType storeObjectType;
					ToServiceObjectForPropertyBagPropertyList toServiceObjectPropertyList = classDeterminer.GetToServiceObjectPropertyList(rowData, out storeObjectType);
					ItemType itemType = ItemType.CreateFromStoreObjectType(storeObjectType);
					toServiceObjectPropertyList.ConvertPropertiesToServiceObject(itemType, rowData, idAndSession);
					array[num].Items[i] = itemType;
				}
				num++;
			}
			return new FindItemParentWrapper(array, pageResult);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0004E098 File Offset: 0x0004C298
		public override BaseFolderType[] ConvertToFolderObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession)
		{
			throw new InvalidOperationException("GroupedQueryView::ConvertToFolderObjects");
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0004E0A4 File Offset: 0x0004C2A4
		public override ConversationType[] ConvertToConversationObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, IdAndSession idAndSession, RequestDetailsLogger logger)
		{
			throw new InvalidOperationException("GroupedQueryView::ConvertToConversationObjects");
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0004E0B0 File Offset: 0x0004C2B0
		public override Persona[] ConvertPersonViewToPersonaObjects(PropertyDefinition[] propsToFetch, PropertyListForViewRowDeterminer classDeterminer, MailboxSession mailboxSession)
		{
			throw new InvalidOperationException("GroupedQueryView::ConvertToPersonObjects");
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0004E0BC File Offset: 0x0004C2BC
		private int GetItemCount()
		{
			int num = 0;
			foreach (KeyValuePair<string, List<object[]>> keyValuePair in this.groupedView)
			{
				num += keyValuePair.Value.Count;
			}
			return num;
		}

		// Token: 0x04000C06 RID: 3078
		private const int MaxGroupByKeyLength = 255;

		// Token: 0x04000C07 RID: 3079
		private Dictionary<string, List<object[]>> groupedView = new Dictionary<string, List<object[]>>();
	}
}
