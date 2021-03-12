using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E6 RID: 486
	public class ExchangeTextBox : EnhancedTextBox
	{
		// Token: 0x060015D9 RID: 5593 RVA: 0x0005A026 File Offset: 0x00058226
		public ExchangeTextBox()
		{
			base.DataBindings.CollectionChanged += this.DataBindings_CollectionChanged;
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x0005A045 File Offset: 0x00058245
		protected override void OnBindingContextChanged(EventArgs e)
		{
			base.OnBindingContextChanged(e);
			this.DataBindings_CollectionChanged(null, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x0005A05C File Offset: 0x0005825C
		private void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			if (!base.DesignMode)
			{
				Binding binding = (Binding)e.Element;
				if (e.Action == CollectionChangeAction.Add && binding.PropertyName == "Text" && this.constraintProvider == null)
				{
					this.constraintProvider = new TextBoxConstraintProvider(this, this);
				}
			}
		}

		// Token: 0x040007E7 RID: 2023
		private TextBoxConstraintProvider constraintProvider;
	}
}
