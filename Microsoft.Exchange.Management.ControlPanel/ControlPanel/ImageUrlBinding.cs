using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000595 RID: 1429
	public class ImageUrlBinding : ClientControlBinding
	{
		// Token: 0x060041B9 RID: 16825 RVA: 0x000C7FFC File Offset: 0x000C61FC
		public ImageUrlBinding(Control control, bool neverDirty) : base(control, "src")
		{
			this.neverDirty = neverDirty;
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000C8011 File Offset: 0x000C6211
		protected override string ToJavaScriptWhenVisible(IControlResolver resolver)
		{
			return string.Format("new ImageUrlBinding('{0}', {1})", this.ClientID, this.neverDirty ? "true" : "false");
		}

		// Token: 0x04002B62 RID: 11106
		private readonly bool neverDirty;
	}
}
