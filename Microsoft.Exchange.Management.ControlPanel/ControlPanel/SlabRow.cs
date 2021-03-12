using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Principal;
using System.Web.UI;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200065E RID: 1630
	[ParseChildren(true, "Content")]
	[DefaultProperty("Content")]
	public class SlabRow : SlabComponent
	{
		// Token: 0x060046D6 RID: 18134 RVA: 0x000D645F File Offset: 0x000D465F
		public SlabRow()
		{
			this.Content = new List<Control>();
		}

		// Token: 0x17002745 RID: 10053
		// (get) Token: 0x060046D7 RID: 18135 RVA: 0x000D6472 File Offset: 0x000D4672
		// (set) Token: 0x060046D8 RID: 18136 RVA: 0x000D647A File Offset: 0x000D467A
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<Control> Content { get; private set; }

		// Token: 0x060046D9 RID: 18137 RVA: 0x000D6484 File Offset: 0x000D4684
		internal void Refactor()
		{
			IPrincipal user = this.Context.User;
			for (int i = this.Content.Count - 1; i >= 0; i--)
			{
				SlabControl slabControl = this.Content[i] as SlabControl;
				if (slabControl != null && !slabControl.AccessibleToUser(user))
				{
					this.Content.RemoveAt(i);
					slabControl.Visible = false;
				}
			}
		}
	}
}
