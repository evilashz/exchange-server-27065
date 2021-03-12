using System;
using System.Collections.Generic;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Local
{
	// Token: 0x02000079 RID: 121
	internal class DefinitionTable<TWorkDefinition> : SimpleTable<TWorkDefinition, int, TWorkDefinition> where TWorkDefinition : WorkDefinition
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001C636 File Offset: 0x0001A836
		public DefinitionTable() : base(WorkDefinitionIndex<TWorkDefinition>.Id(0))
		{
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001C644 File Offset: 0x0001A844
		protected override TWorkDefinition CreateSegment(TWorkDefinition item)
		{
			return item;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001C648 File Offset: 0x0001A848
		protected override bool AddToSegment(TWorkDefinition segment, TWorkDefinition item)
		{
			if (segment != item)
			{
				string message = string.Format("An item with the same primary key {0} already exists in the table. Existing definition name: {1}. New definition name: {2}", segment.Id, segment.Name, item.Name);
				throw new InvalidOperationException(message);
			}
			return true;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
		protected override IEnumerable<TWorkDefinition> GetItemsFromSegment<TKey>(TWorkDefinition segment, IIndexDescriptor<TWorkDefinition, TKey> indexDescriptor)
		{
			return new TWorkDefinition[]
			{
				segment
			};
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001C6C1 File Offset: 0x0001A8C1
		protected override IEnumerable<TWorkDefinition> GetItemsFromSegments<TKey>(IEnumerable<TWorkDefinition> segments, IIndexDescriptor<TWorkDefinition, TKey> indexDescriptor)
		{
			return segments;
		}
	}
}
