using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000220 RID: 544
	public partial class ScheduleEditorDialog : ExchangeDialog
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x0006B874 File Offset: 0x00069A74
		public ScheduleEditorDialog()
		{
			this.InitializeComponent();
			this.Text = Strings.ScheduleEditorDialogTitle;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0006BABA File Offset: 0x00069CBA
		// (set) Token: 0x060018EC RID: 6380 RVA: 0x0006BAC7 File Offset: 0x00069CC7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Schedule Schedule
		{
			get
			{
				return this.scheduleEditor.Schedule;
			}
			set
			{
				this.scheduleEditor.Schedule = value;
			}
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0006BAD8 File Offset: 0x00069CD8
		protected override void OnLoad(EventArgs e)
		{
			int num = base.ClientSize.Height - base.ButtonsPanel.Height;
			int height = this.tableLayoutPanel.Height;
			int num2 = num - height;
			base.Height -= num2;
			base.OnLoad(e);
		}
	}
}
