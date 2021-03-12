using System;
using System.ComponentModel;
using System.Windows.Forms.Design;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000022 RID: 34
	internal class ScrollbarControlDesigner : ControlDesigner
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000089C8 File Offset: 0x00006BC8
		public override SelectionRules SelectionRules
		{
			get
			{
				SelectionRules result = base.SelectionRules;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.Component)["AutoSize"];
				if (propertyDescriptor != null)
				{
					bool flag = (bool)propertyDescriptor.GetValue(base.Component);
					if (flag)
					{
						result = (SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.TopSizeable | SelectionRules.BottomSizeable);
					}
					else
					{
						result = (SelectionRules.Moveable | SelectionRules.Visible | SelectionRules.TopSizeable | SelectionRules.BottomSizeable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable);
					}
				}
				return result;
			}
		}
	}
}
