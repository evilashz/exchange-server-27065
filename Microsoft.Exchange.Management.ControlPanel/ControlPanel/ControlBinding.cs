using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000593 RID: 1427
	public abstract class ControlBinding : Binding
	{
		// Token: 0x060041AF RID: 16815 RVA: 0x000C7F34 File Offset: 0x000C6134
		public ControlBinding(Control control)
		{
			this.Control = control;
		}

		// Token: 0x17002574 RID: 9588
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x000C7F43 File Offset: 0x000C6143
		// (set) Token: 0x060041B1 RID: 16817 RVA: 0x000C7F4B File Offset: 0x000C614B
		private protected Control Control { protected get; private set; }

		// Token: 0x17002575 RID: 9589
		// (get) Token: 0x060041B2 RID: 16818 RVA: 0x000C7F54 File Offset: 0x000C6154
		protected virtual string ClientID
		{
			get
			{
				if (!(this.Control is RadioButtonList))
				{
					return this.Control.ClientID;
				}
				return this.Control.UniqueID;
			}
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x000C7F8A File Offset: 0x000C618A
		public sealed override string ToJavaScript(IControlResolver resolver)
		{
			if (this.Control.Visible)
			{
				return this.ToJavaScriptWhenVisible(resolver);
			}
			return "null";
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000C7FA6 File Offset: 0x000C61A6
		protected virtual string ToJavaScriptWhenVisible(IControlResolver resolver)
		{
			return string.Format("new {0}('{1}')", base.GetType().Name, this.ClientID);
		}
	}
}
