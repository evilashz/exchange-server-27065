using System;
using System.Collections;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x02000066 RID: 102
	internal sealed class MessageListViewDataSource : ListViewDataSource
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x00018B85 File Offset: 0x00016D85
		public MessageListViewDataSource(Hashtable properties, Folder folder, SortBy[] sortBy) : base(properties, folder)
		{
			if (sortBy == null)
			{
				throw new ArgumentNullException("sortBy");
			}
			this.sortBy = sortBy;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00018BA4 File Offset: 0x00016DA4
		public override void LoadData(int startRange, int endRange)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewDataSource.LoadData(Start)");
			if (startRange < 1)
			{
				throw new ArgumentOutOfRangeException("startRange", "startRange must be greater than 0");
			}
			if (endRange < startRange)
			{
				throw new ArgumentOutOfRangeException("endRange", "endRange must be greater than or equal to startRange");
			}
			PropertyDefinition[] dataColumns = base.CreateProperyTable();
			if (0 < base.Folder.ItemCount)
			{
				using (QueryResult queryResult = base.Folder.ItemQuery(ItemQueryType.None, null, this.sortBy, dataColumns))
				{
					this.LoadData(queryResult, startRange, endRange);
					return;
				}
			}
			this.SetRangeNull();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00018C44 File Offset: 0x00016E44
		public override int LoadData(StoreObjectId storeObjectId, int itemsPerPage)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "ListViewDataSource.LoadData(Start)");
			if (storeObjectId == null)
			{
				throw new ArgumentNullException("storeObjectId");
			}
			if (itemsPerPage <= 0)
			{
				throw new ArgumentOutOfRangeException("itemsPerPage", "itemsPerPage has to be greater than zero");
			}
			PropertyDefinition[] dataColumns = base.CreateProperyTable();
			int num = 1;
			if (0 < base.Folder.ItemCount)
			{
				using (QueryResult queryResult = base.Folder.ItemQuery(ItemQueryType.None, null, this.sortBy, dataColumns))
				{
					int estimatedRowCount = queryResult.EstimatedRowCount;
					queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.Id, storeObjectId));
					int currentRow = queryResult.CurrentRow;
					num = currentRow / itemsPerPage + 1;
					if (num < 1 || currentRow >= estimatedRowCount)
					{
						num = 1;
					}
					int num2 = (num - 1) * itemsPerPage + 1;
					int endRange = num2 + itemsPerPage - 1;
					this.LoadData(queryResult, num2, endRange);
					return num;
				}
			}
			this.SetRangeNull();
			return num;
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00018D2C File Offset: 0x00016F2C
		private void LoadData(QueryResult queryResult, int startRange, int endRange)
		{
			if (startRange < 1)
			{
				throw new ArgumentOutOfRangeException("startRange", "startRange must be greater than 0");
			}
			if (endRange < startRange)
			{
				throw new ArgumentOutOfRangeException("endRange", "endRange must be greater than or equal to startRange");
			}
			int estimatedRowCount = queryResult.EstimatedRowCount;
			if (estimatedRowCount < startRange)
			{
				base.Items = null;
			}
			else
			{
				queryResult.SeekToOffset(SeekReference.OriginBeginning, startRange - 1);
				if (estimatedRowCount < endRange)
				{
					endRange = estimatedRowCount;
				}
				int rowCount = endRange - startRange + 1;
				base.Items = Utilities.FetchRowsFromQueryResult(queryResult, rowCount);
			}
			if (base.Folder.ItemCount == 0 || base.Items == null || base.Items.Length == 0)
			{
				this.SetRangeNull();
				return;
			}
			base.StartRange = startRange;
			base.EndRange = startRange + base.Items.Length - 1;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00018DD9 File Offset: 0x00016FD9
		private void SetRangeNull()
		{
			base.StartRange = 0;
			base.EndRange = -1;
		}

		// Token: 0x04000214 RID: 532
		private SortBy[] sortBy;
	}
}
