using System;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001A6 RID: 422
	public class DefaultAlignRule : IAlignRule
	{
		// Token: 0x060010B3 RID: 4275 RVA: 0x00041F14 File Offset: 0x00040114
		public void Apply(AlignUnitsCollection units)
		{
			Padding padding = new Padding(0);
			for (int i = 0; i < units.RowCount; i++)
			{
				AlignUnit maxUnitInRow = units.GetMaxUnitInRow(i);
				if (maxUnitInRow == null)
				{
					padding = new Padding(0);
				}
				else
				{
					AlignUnit minUnitInRow = units.GetMinUnitInRow(i);
					Padding padding2 = maxUnitInRow.CompareMargin - minUnitInRow.CompareMargin;
					padding2 = ((padding2.Vertical > maxUnitInRow.InlinedMargin.Vertical) ? padding2 : maxUnitInRow.InlinedMargin);
					foreach (AlignUnit alignUnit in units.GetAlignUnitsInRow(i))
					{
						alignUnit.ResultMargin = maxUnitInRow.CompareMargin - alignUnit.CompareMargin;
					}
					units.RowDeltaValue[i] = units.RowDeltaValue[i] - padding.Bottom - padding2.Top;
					if (units.RowDeltaValue[i] < DefaultAlignRule.MinimalVertical)
					{
						units.RowDeltaValue[i] = DefaultAlignRule.MinimalVertical;
					}
					padding = padding2;
				}
			}
			if (units.RowCount > 0)
			{
				units.RowDeltaValue[0] = 0;
			}
		}

		// Token: 0x0400067B RID: 1659
		public static int MinimalVertical = 3;
	}
}
