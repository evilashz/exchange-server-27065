using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200020F RID: 527
	public partial class TridentErrorDialog : BaseErrorDialog
	{
		// Token: 0x060017D9 RID: 6105 RVA: 0x00064904 File Offset: 0x00062B04
		public TridentErrorDialog()
		{
			this.InitializeComponent();
			Label label = new Label();
			label.AutoSize = true;
			label.Dock = DockStyle.Left;
			label.Name = "copyLabel";
			label.Text = Strings.CopyNote;
			base.ButtonsPanel.Controls.Add(label);
			base.ButtonsPanel.Controls.SetChildIndex(label, 0);
			this.tridentsPanel = new WorkUnitsPanel();
			this.tridentsPanel.Name = "tridentsPanel";
			this.tridentsPanel.Dock = DockStyle.Fill;
			this.tridentsPanel.Margin = new Padding(0);
			this.tridentsPanel.TaskState = 2;
			base.ContentPanel.Controls.Add(this.tridentsPanel);
			base.ContentPanel.Controls.SetChildIndex(this.tridentsPanel, 0);
			this.Text = Strings.TridentErrorDialogTitle(UIService.DefaultCaption);
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060017DB RID: 6107 RVA: 0x00064A4E File Offset: 0x00062C4E
		// (set) Token: 0x060017DC RID: 6108 RVA: 0x00064A5C File Offset: 0x00062C5C
		[DefaultValue(null)]
		internal IList<WorkUnit> Errors
		{
			get
			{
				return this.tridentsPanel.WorkUnits;
			}
			set
			{
				if (value != null)
				{
					foreach (WorkUnit workUnit in value)
					{
						workUnit.CanShowElapsedTime = false;
						workUnit.CanShowExecutedCommand = false;
					}
				}
				this.tridentsPanel.WorkUnits = value;
			}
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00064ABC File Offset: 0x00062CBC
		protected override void OnIsWarningOnlyChanged()
		{
			base.OnIsWarningOnlyChanged();
			this.Text = (base.IsWarningOnly ? Strings.TridentWarningDialogTitle(UIService.DefaultCaption) : Strings.TridentErrorDialogTitle(UIService.DefaultCaption));
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060017DE RID: 6110 RVA: 0x00064AF0 File Offset: 0x00062CF0
		public override string TechnicalDetails
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("--------------------------------------------------------");
				stringBuilder.AppendLine(this.Text);
				stringBuilder.AppendLine("--------------------------------------------------------");
				stringBuilder.AppendLine(base.Message);
				if (this.Errors != null)
				{
					foreach (WorkUnit workUnit in this.Errors)
					{
						stringBuilder.AppendLine();
						stringBuilder.AppendLine(workUnit.Summary);
					}
				}
				stringBuilder.AppendLine("--------------------------------------------------------");
				stringBuilder.AppendLine(Strings.Ok);
				stringBuilder.AppendLine("--------------------------------------------------------");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00064BBC File Offset: 0x00062DBC
		protected override void OnClosed(EventArgs e)
		{
			this.Errors = null;
			base.OnClosed(e);
		}

		// Token: 0x040008EB RID: 2283
		private WorkUnitsPanel tridentsPanel;
	}
}
