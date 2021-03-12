using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000229 RID: 553
	public partial class ComboBoxPickerDialog : ExchangeDialog
	{
		// Token: 0x0600199A RID: 6554 RVA: 0x0006F103 File Offset: 0x0006D303
		public ComboBoxPickerDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x0006F401 File Offset: 0x0006D601
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x0006F40E File Offset: 0x0006D60E
		[DefaultValue("")]
		public string Label
		{
			get
			{
				return this.label.Text;
			}
			set
			{
				this.label.Text = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x0006F41C File Offset: 0x0006D61C
		// (set) Token: 0x0600199F RID: 6559 RVA: 0x0006F429 File Offset: 0x0006D629
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return this.comboBox.DataSource;
			}
			set
			{
				this.comboBox.DataSource = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x0006F437 File Offset: 0x0006D637
		// (set) Token: 0x060019A1 RID: 6561 RVA: 0x0006F444 File Offset: 0x0006D644
		[DefaultValue("")]
		public string DisplayMember
		{
			get
			{
				return this.comboBox.DisplayMember;
			}
			set
			{
				this.comboBox.DisplayMember = value;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060019A2 RID: 6562 RVA: 0x0006F452 File Offset: 0x0006D652
		// (set) Token: 0x060019A3 RID: 6563 RVA: 0x0006F45F File Offset: 0x0006D65F
		[DefaultValue("")]
		public string ValueMember
		{
			get
			{
				return this.comboBox.ValueMember;
			}
			set
			{
				this.comboBox.ValueMember = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x0006F46D File Offset: 0x0006D66D
		// (set) Token: 0x060019A5 RID: 6565 RVA: 0x0006F47A File Offset: 0x0006D67A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object SelectedValue
		{
			get
			{
				return this.comboBox.SelectedValue;
			}
			set
			{
				this.comboBox.SelectedValue = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x0006F488 File Offset: 0x0006D688
		// (set) Token: 0x060019A7 RID: 6567 RVA: 0x0006F495 File Offset: 0x0006D695
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int SelectedIndex
		{
			get
			{
				return this.comboBox.SelectedIndex;
			}
			set
			{
				this.comboBox.SelectedIndex = value;
			}
		}
	}
}
