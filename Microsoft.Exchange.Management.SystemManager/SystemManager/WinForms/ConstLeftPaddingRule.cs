using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001A8 RID: 424
	public class ConstLeftPaddingRule : IAlignRule
	{
		// Token: 0x060010B9 RID: 4281 RVA: 0x00042138 File Offset: 0x00040338
		public ConstLeftPaddingRule(IList<Type> excludedTypes)
		{
			this.excludedTypes = (excludedTypes ?? new List<Type>());
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00042160 File Offset: 0x00040360
		public void Apply(AlignUnitsCollection collection)
		{
			foreach (AlignUnit alignUnit in collection.Units)
			{
				if (this.Match(alignUnit, collection) && alignUnit.ResultMargin.Left + alignUnit.Control.Padding.Left < this.leftPadding.Left)
				{
					alignUnit.ResultMargin += this.leftPadding;
				}
			}
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000421F8 File Offset: 0x000403F8
		private bool Match(AlignUnit unit, AlignUnitsCollection collection)
		{
			foreach (Type type in this.excludedTypes)
			{
				if (type.IsAssignableFrom(unit.Control.GetType()))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400067E RID: 1662
		private IList<Type> excludedTypes;

		// Token: 0x0400067F RID: 1663
		private Padding leftPadding = new Padding(3, 0, 0, 0);
	}
}
