using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001D9 RID: 473
	public partial class ExceptionDialog : BaseErrorDialog
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x00057628 File Offset: 0x00055828
		public ExceptionDialog()
		{
			ExchangeButton exchangeButton = new ExchangeButton();
			ExchangeButton exchangeButton2 = new ExchangeButton();
			this.additionalMessages = new ExchangeTextBox();
			AutoSizePanel autoSizePanel = new AutoSizePanel();
			Label label = new Label();
			TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
			base.SuspendLayout();
			base.ContentPanel.SuspendLayout();
			tableLayoutPanel.SuspendLayout();
			base.ButtonsPanel.SuspendLayout();
			tableLayoutPanel.AutoSize = true;
			tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			tableLayoutPanel.ColumnCount = 1;
			tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			tableLayoutPanel.Controls.Add(label, 0, 0);
			tableLayoutPanel.Controls.Add(autoSizePanel, 0, 1);
			tableLayoutPanel.Dock = DockStyle.Fill;
			tableLayoutPanel.RowCount = 2;
			tableLayoutPanel.RowStyles.Add(new RowStyle());
			tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			tableLayoutPanel.TabIndex = 0;
			exchangeButton.AutoSize = true;
			exchangeButton.Click += this.reportButton_Click;
			exchangeButton.Dock = DockStyle.Left;
			exchangeButton.FocusedAlwaysOnClick = false;
			exchangeButton.Margin = new Padding(0);
			exchangeButton.Name = "reportButton";
			exchangeButton.TabIndex = 1;
			exchangeButton.Text = Strings.SendWatsonReport;
			exchangeButton2.AutoSize = true;
			exchangeButton2.Click += this.helpButton_Click;
			exchangeButton2.Dock = DockStyle.Right;
			exchangeButton2.FocusedAlwaysOnClick = false;
			exchangeButton2.Margin = new Padding(8, 0, 0, 0);
			exchangeButton2.Name = "helpButton";
			exchangeButton2.TabIndex = 2;
			exchangeButton2.Text = "&Help";
			base.ButtonsPanel.Controls.Add(exchangeButton2);
			base.ButtonsPanel.ColumnCount = 4;
			base.ButtonsPanel.ColumnStyles.Insert(1, new ColumnStyle());
			base.ButtonsPanel.ColumnStyles.Add(new ColumnStyle());
			base.ButtonsPanel.Controls.Add(exchangeButton, 0, 0);
			base.ButtonsPanel.Controls.Add(exchangeButton2, 3, 0);
			this.additionalMessages.Location = new Point(0, 0);
			this.additionalMessages.Margin = new Padding(0);
			this.additionalMessages.Size = new Size(406, 147);
			this.additionalMessages.Name = "additionalMessages";
			this.additionalMessages.ReadOnly = true;
			this.additionalMessages.Multiline = true;
			this.additionalMessages.ScrollBars = ScrollBars.Vertical;
			this.additionalMessages.TabIndex = 0;
			this.additionalMessages.TabStop = false;
			this.additionalMessages.Dock = DockStyle.Fill;
			autoSizePanel.BackColor = SystemColors.Window;
			autoSizePanel.Controls.Add(this.additionalMessages);
			autoSizePanel.Dock = DockStyle.Fill;
			autoSizePanel.Margin = new Padding(3, 0, 0, 0);
			autoSizePanel.Name = "additionalMessagesPanel";
			autoSizePanel.Size = new Size(406, 147);
			autoSizePanel.TabIndex = 1;
			autoSizePanel.TabStop = false;
			label.AutoSize = true;
			label.Dock = DockStyle.Top;
			label.Font = new Font(this.Font, FontStyle.Bold);
			label.Location = new Point(0, 25);
			label.Name = "infoCaption";
			label.Size = new Size(130, 13);
			label.TabIndex = 0;
			label.Text = Strings.AdditionalInformation;
			base.ContentPanel.Controls.Add(tableLayoutPanel);
			base.ContentPanel.Controls.SetChildIndex(tableLayoutPanel, 0);
			base.Name = "ExceptionDialog";
			this.Text = Strings.ExceptionDialogTitle(UIService.DefaultCaption);
			base.ButtonsPanel.ResumeLayout(false);
			base.ButtonsPanel.PerformLayout();
			tableLayoutPanel.ResumeLayout(false);
			tableLayoutPanel.PerformLayout();
			base.ContentPanel.ResumeLayout(false);
			base.ContentPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
			exchangeButton2.Visible = false;
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00057A1C File Offset: 0x00055C1C
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x00057A24 File Offset: 0x00055C24
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
			set
			{
				this.exception = value;
				this.UpdateExceptionText();
			}
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00057A34 File Offset: 0x00055C34
		private void UpdateExceptionText()
		{
			StringBuilder stringBuilder = new StringBuilder();
			Exception innerException = this.exception;
			if (innerException != null)
			{
				base.Message = innerException.Message;
				stringBuilder = new StringBuilder();
				stringBuilder.AppendLine(innerException.StackTrace);
				stringBuilder.AppendLine();
				for (innerException = innerException.InnerException; innerException != null; innerException = innerException.InnerException)
				{
					stringBuilder.AppendLine(innerException.Message);
					stringBuilder.AppendLine(innerException.StackTrace);
					stringBuilder.AppendLine();
				}
				this.additionalMessages.Text = stringBuilder.ToString();
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00057ABC File Offset: 0x00055CBC
		public string ExceptionText
		{
			get
			{
				return base.Message;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00057AC4 File Offset: 0x00055CC4
		public override string TechnicalDetails
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				string[] array = new string[]
				{
					Application.CompanyName,
					Application.ProductName,
					Application.ProductVersion,
					this.ExceptionText,
					this.Exception.ToString()
				};
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array[i]);
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(Environment.NewLine);
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				for (int j = 0; j < assemblies.Length; j++)
				{
					stringBuilder.Append(assemblies[j].FullName);
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(Environment.NewLine);
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00057B94 File Offset: 0x00055D94
		private void reportButton_Click(object sender, EventArgs e)
		{
			using (new ControlWaitCursor(this))
			{
				ExWatson.SendReport(this.Exception);
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00057BD4 File Offset: 0x00055DD4
		private void helpButton_Click(object sender, EventArgs e)
		{
			this.OnHelpRequested(new HelpEventArgs(Point.Empty));
		}

		// Token: 0x040007B3 RID: 1971
		private ExchangeTextBox additionalMessages;

		// Token: 0x040007B4 RID: 1972
		private Exception exception;
	}
}
