using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000112 RID: 274
	[DefaultProperty("BindingSource")]
	public class BindableUserControlBase : ExchangeUserControl
	{
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00022A94 File Offset: 0x00020C94
		public BindingSource BindingSource
		{
			get
			{
				return this.bindingSource;
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00022A9C File Offset: 0x00020C9C
		public BindableUserControlBase()
		{
			this.bindingSource = new BindingSource(base.Components);
			base.Name = "BindableUserControlBase";
		}

		// Token: 0x04000453 RID: 1107
		[AccessedThroughProperty("BindingSource")]
		private BindingSource bindingSource;
	}
}
