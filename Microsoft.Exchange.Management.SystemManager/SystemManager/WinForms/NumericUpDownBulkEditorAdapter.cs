using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001E1 RID: 481
	public sealed class NumericUpDownBulkEditorAdapter : BulkEditorAdapter
	{
		// Token: 0x060015C2 RID: 5570 RVA: 0x0005966D File Offset: 0x0005786D
		public NumericUpDownBulkEditorAdapter(Control ctrl) : base(ctrl)
		{
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00059676 File Offset: 0x00057876
		protected override void OnStateChanged(BulkEditorAdapter sender, BulkEditorStateEventArgs e)
		{
			base.OnStateChanged(sender, e);
			if (base["Value"] == 3)
			{
				base.HostControl.Enabled = false;
			}
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0005969C File Offset: 0x0005789C
		protected override IList<string> InnerGetManagedProperties()
		{
			IList<string> list = base.InnerGetManagedProperties();
			list.Add("Value");
			return list;
		}

		// Token: 0x040007D9 RID: 2009
		private const string ManagedPropertyName = "Value";
	}
}
