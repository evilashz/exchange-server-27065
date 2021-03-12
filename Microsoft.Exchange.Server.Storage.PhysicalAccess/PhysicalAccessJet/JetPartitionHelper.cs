using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;

namespace Microsoft.Exchange.Server.Storage.PhysicalAccessJet
{
	// Token: 0x020000B1 RID: 177
	internal static class JetPartitionHelper
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x00025D88 File Offset: 0x00023F88
		public static object[] GetPartitionKeyValues(Table table, IList<Column> columns, IList<object> values)
		{
			object[] array = new object[table.SpecialCols.NumberOfPartioningColumns];
			for (int i = 0; i < columns.Count; i++)
			{
				JetPhysicalColumn jetPhysicalColumn = columns[i] as JetPhysicalColumn;
				if (jetPhysicalColumn.Index < table.SpecialCols.NumberOfPartioningColumns)
				{
					array[jetPhysicalColumn.Index] = values[i];
				}
			}
			return array;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00025DE8 File Offset: 0x00023FE8
		public static string GetPartitionName(Table table, IList<object> values, int numberOfPartitionValues)
		{
			if (numberOfPartitionValues == 1)
			{
				return table.Name + "_" + values[0].ToString();
			}
			if (numberOfPartitionValues == 2)
			{
				return string.Concat(new string[]
				{
					table.Name,
					"_",
					values[0].ToString(),
					"_",
					values[1].ToString()
				});
			}
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append(table.Name);
			for (int i = 0; i < numberOfPartitionValues; i++)
			{
				stringBuilder.Append("_");
				stringBuilder.Append(values[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00025EA8 File Offset: 0x000240A8
		public static void CheckPartitionKeys(Table table, Index index, StartStopKey startKey, StartStopKey stopKey)
		{
			for (int i = 0; i < table.SpecialCols.NumberOfPartioningColumns; i++)
			{
			}
			if (startKey.IsEmpty)
			{
				throw new ArgumentNullException("StartKey", "Partitioned tables must have start and stop keys");
			}
			if (stopKey.IsEmpty)
			{
				throw new ArgumentNullException("StopKey", "Partitioned tables must have start and stop keys");
			}
			for (int j = 0; j < table.SpecialCols.NumberOfPartioningColumns; j++)
			{
				if (startKey.Values[j] == null)
				{
					throw new ArgumentNullException("StartKey", "StartKey value must not be null.");
				}
				if (stopKey.Values[j] == null)
				{
					throw new ArgumentNullException("StopKey", "StopKey value must not be null.");
				}
				if (ValueHelper.ValuesCompare(startKey.Values[j], stopKey.Values[j]) != 0)
				{
					throw new ArgumentOutOfRangeException("StopKey", "StartKey and StopKey must have the same value for the partition key");
				}
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00025F85 File Offset: 0x00024185
		public static bool IsPartitioningColumn(Table table, PhysicalColumn column)
		{
			return column.Index >= 0 && column.Index < table.SpecialCols.NumberOfPartioningColumns;
		}
	}
}
