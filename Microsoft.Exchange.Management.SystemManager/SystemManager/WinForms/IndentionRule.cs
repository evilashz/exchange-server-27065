using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001A7 RID: 423
	public class IndentionRule : IAlignRule
	{
		// Token: 0x060010B6 RID: 4278 RVA: 0x00042054 File Offset: 0x00040254
		public IndentionRule(IList<Type> includedTypes)
		{
			this.includedTypes = includedTypes;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00042064 File Offset: 0x00040264
		public void Apply(AlignUnitsCollection collection)
		{
			int num = collection.ColumnCount;
			for (int i = 0; i < collection.RowCount; i++)
			{
				IList<AlignUnit> alignUnitsInRow = collection.GetAlignUnitsInRow(i);
				if (alignUnitsInRow.Count > 0)
				{
					AlignUnit alignUnit = alignUnitsInRow[0];
					if (alignUnit.Column < num && this.IsTypeMatch(alignUnit))
					{
						num = alignUnit.Column;
					}
					if (alignUnit.Column > num && collection.RowDeltaValue[i] > 8)
					{
						collection.RowDeltaValue[i] = 8;
					}
				}
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000420D8 File Offset: 0x000402D8
		private bool IsTypeMatch(AlignUnit unit)
		{
			foreach (Type type in this.includedTypes)
			{
				if (type.IsAssignableFrom(unit.Control.GetType()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400067C RID: 1660
		private const int DeltaValue = 8;

		// Token: 0x0400067D RID: 1661
		private IList<Type> includedTypes;
	}
}
