using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200052A RID: 1322
	public class NewMailGroupMaster : MasterPage, IMasterPage
	{
		// Token: 0x17002498 RID: 9368
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x000BD7AB File Offset: 0x000BB9AB
		public ContentPlaceHolder ContentPlaceHolder
		{
			get
			{
				return (base.Master as IMasterPage).ContentPlaceHolder;
			}
		}
	}
}
