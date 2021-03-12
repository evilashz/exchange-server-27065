using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200000D RID: 13
	public class TypedControlIDConverter<T> : ControlIDConverter
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002B1F File Offset: 0x00000D1F
		protected override bool FilterControl(Control control)
		{
			return typeof(T).IsInstanceOfType(control);
		}
	}
}
