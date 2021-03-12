using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020003BC RID: 956
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class ColumnAdjuster
	{
		// Token: 0x06002B9F RID: 11167 RVA: 0x000AE050 File Offset: 0x000AC250
		internal ColumnAdjuster(PropertyDefinition property)
		{
			this.property = property;
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x000AE066 File Offset: 0x000AC266
		internal int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x000AE070 File Offset: 0x000AC270
		internal static PropertyDefinition[] Adjust(PropertyDefinition[] columns, IList<ColumnAdjuster> columnsToAdjust)
		{
			int num = 0;
			for (int i = 0; i < columnsToAdjust.Count; i++)
			{
				ColumnAdjuster columnAdjuster = columnsToAdjust[i];
				int num2 = Array.IndexOf<PropertyDefinition>(columns, columnAdjuster.property);
				if (num2 == -1)
				{
					num++;
				}
				else
				{
					columnAdjuster.index = num2;
				}
			}
			if (num > 0)
			{
				PropertyDefinition[] array = new PropertyDefinition[columns.Length + num];
				Array.Copy(columns, array, columns.Length);
				int num3 = columns.Length;
				for (int j = 0; j < columnsToAdjust.Count; j++)
				{
					ColumnAdjuster columnAdjuster2 = columnsToAdjust[j];
					if (columnAdjuster2.index == -1)
					{
						array[num3] = columnAdjuster2.property;
						columnAdjuster2.index = num3;
						num3++;
					}
				}
				return array;
			}
			return columns;
		}

		// Token: 0x04001860 RID: 6240
		private PropertyDefinition property;

		// Token: 0x04001861 RID: 6241
		private int index = -1;
	}
}
