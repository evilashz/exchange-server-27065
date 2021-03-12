using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004F7 RID: 1271
	public class EditMailGroupMaster : MasterPage, IMasterPage
	{
		// Token: 0x17002429 RID: 9257
		// (get) Token: 0x06003D79 RID: 15737 RVA: 0x000B89AC File Offset: 0x000B6BAC
		public ContentPlaceHolder ContentPlaceHolder
		{
			get
			{
				return ((IMasterPage)base.Master).ContentPlaceHolder;
			}
		}
	}
}
