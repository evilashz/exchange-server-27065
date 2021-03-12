using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200021A RID: 538
	public class WizardOrientationPanel : Panel
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x00067538 File Offset: 0x00065738
		public WizardOrientationPanel()
		{
			this.InitializeComponent();
			this.DoubleBuffered = true;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00067554 File Offset: 0x00065754
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Wizard = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x00067568 File Offset: 0x00065768
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new TableLayoutPanel();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Size = new Size(156, 0);
			this.tableLayoutPanel.TabIndex = 0;
			base.Controls.Add(this.tableLayoutPanel);
			base.ResumeLayout();
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0006763E File Offset: 0x0006583E
		// (set) Token: 0x0600186F RID: 6255 RVA: 0x00067648 File Offset: 0x00065848
		[DefaultValue(null)]
		public Wizard Wizard
		{
			get
			{
				return this.wizard;
			}
			set
			{
				if (this.Wizard != value)
				{
					if (this.Wizard != null)
					{
						this.Wizard.WizardPageAdded -= this.wizard_WizardPageAdded;
						this.Wizard.WizardPageRemoved -= this.wizard_WizardPageRemoved;
						this.Wizard.WizardPageMoved -= new ControlMovedEventHandler(this.wizard_WizardPageMoved);
						this.Wizard.CurrentPageChanged -= this.wizard_CurrentPageChanged;
						this.tableLayoutPanel.SuspendLayout();
						try
						{
							for (int i = this.tableLayoutPanel.RowCount - 1; i >= 0; i--)
							{
								Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(0, i);
								if (controlFromPosition != null)
								{
									this.tableLayoutPanel.Controls.Remove(controlFromPosition);
									controlFromPosition.Dispose();
								}
							}
							this.tableLayoutPanel.RowStyles.Clear();
							this.tableLayoutPanel.RowCount = 0;
						}
						finally
						{
							this.tableLayoutPanel.ResumeLayout(false);
						}
					}
					this.wizard = value;
					if (this.Wizard != null)
					{
						this.Wizard.WizardPageAdded += this.wizard_WizardPageAdded;
						this.Wizard.WizardPageRemoved += this.wizard_WizardPageRemoved;
						this.Wizard.WizardPageMoved += new ControlMovedEventHandler(this.wizard_WizardPageMoved);
						this.Wizard.CurrentPageChanged += this.wizard_CurrentPageChanged;
						this.tableLayoutPanel.SuspendLayout();
						try
						{
							foreach (object obj in this.Wizard.WizardPages)
							{
								WizardPage control = (WizardPage)obj;
								this.wizard_WizardPageAdded(this.Wizard, new ControlEventArgs(control));
							}
						}
						finally
						{
							this.tableLayoutPanel.ResumeLayout(false);
						}
						this.ActiveRow = this.Wizard.CurrentPageIndex;
					}
				}
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x00067850 File Offset: 0x00065A50
		private void wizard_CurrentPageChanged(object sender, EventArgs e)
		{
			this.ActiveRow = this.Wizard.CurrentPageIndex;
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x00067864 File Offset: 0x00065A64
		private void wizard_WizardPageRemoved(object sender, ControlEventArgs e)
		{
			WizardPage wizardPage = (WizardPage)e.Control;
			wizardPage.TextChanged -= this.wizardPage_TextChanged;
			Control labelControl = this.tableLayoutPanel.Controls[wizardPage.Name + "Label"];
			this.RemovePageLabelControl(labelControl);
			this.ActiveRow = this.Wizard.CurrentPageIndex;
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000678C8 File Offset: 0x00065AC8
		private void wizard_WizardPageAdded(object sender, ControlEventArgs e)
		{
			WizardPage wizardPage = (WizardPage)e.Control;
			wizardPage.TextChanged += this.wizardPage_TextChanged;
			ExchangeLinkLabel exchangeLinkLabel = new ExchangeLinkLabel();
			exchangeLinkLabel.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			exchangeLinkLabel.AutoEllipsis = true;
			exchangeLinkLabel.AutoSize = true;
			exchangeLinkLabel.ImageAlign = ContentAlignment.MiddleLeft;
			exchangeLinkLabel.Image = WizardOrientationPanel.upcomingPageImage;
			exchangeLinkLabel.LinkArea = new LinkArea(0, 0);
			exchangeLinkLabel.MaximumSize = new Size(0, exchangeLinkLabel.Font.Height * 3 + 4 + 4);
			exchangeLinkLabel.Name = wizardPage.Name + "Label";
			exchangeLinkLabel.Padding = new Padding(20, 0, 0, 0);
			exchangeLinkLabel.Size = new Size(168, 26);
			exchangeLinkLabel.TabIndex = 4;
			exchangeLinkLabel.Text = wizardPage.Text;
			exchangeLinkLabel.TextAlign = ContentAlignment.MiddleLeft;
			exchangeLinkLabel.UseMnemonic = false;
			int index = this.Wizard.WizardPages.IndexOf(wizardPage);
			int num = 0;
			WizardPage wizardPage2 = wizardPage;
			while (wizardPage2.ParentPage != null && wizardPage2.ParentPage != null)
			{
				num++;
				wizardPage2 = wizardPage2.ParentPage;
			}
			exchangeLinkLabel.Padding = new Padding(WizardOrientationPanel.visitedPageImage.Width + 4, 0, 0, 0);
			exchangeLinkLabel.Margin = new Padding(4 + exchangeLinkLabel.Padding.Left * num, 4, 4, 4);
			this.InsertPageLabelControl(exchangeLinkLabel, index);
			this.ActiveRow = this.Wizard.CurrentPageIndex;
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x00067A34 File Offset: 0x00065C34
		private void wizard_WizardPageMoved(object sender, ControlMovedEventArgs e)
		{
			WizardPage wizardPage = (WizardPage)e.Control;
			Control control = this.tableLayoutPanel.Controls[wizardPage.Name + "Label"];
			if (control != null)
			{
				this.tableLayoutPanel.SuspendLayout();
				try
				{
					this.RemovePageLabelControl(control);
					this.InsertPageLabelControl(control, e.NewIndex);
				}
				finally
				{
					this.tableLayoutPanel.ResumeLayout();
				}
			}
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x00067AB0 File Offset: 0x00065CB0
		private void InsertPageLabelControl(Control labelControl, int index)
		{
			this.tableLayoutPanel.SuspendLayout();
			try
			{
				this.tableLayoutPanel.RowCount++;
				this.tableLayoutPanel.RowStyles.Insert(index, new RowStyle());
				for (int i = this.tableLayoutPanel.RowCount - 2; i >= index; i--)
				{
					Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(0, i);
					if (controlFromPosition != null)
					{
						this.tableLayoutPanel.SetRow(controlFromPosition, i + 1);
					}
				}
				this.tableLayoutPanel.Controls.Add(labelControl, 0, index);
			}
			finally
			{
				this.tableLayoutPanel.ResumeLayout();
			}
		}

		// Token: 0x06001875 RID: 6261 RVA: 0x00067B5C File Offset: 0x00065D5C
		private void RemovePageLabelControl(Control labelControl)
		{
			int row = this.tableLayoutPanel.GetRow(labelControl);
			this.tableLayoutPanel.SuspendLayout();
			try
			{
				this.tableLayoutPanel.Controls.Remove(labelControl);
				for (int i = row + 1; i < this.tableLayoutPanel.RowCount; i++)
				{
					Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(0, i);
					if (controlFromPosition != null)
					{
						this.tableLayoutPanel.SetRow(controlFromPosition, i - 1);
					}
				}
				this.tableLayoutPanel.RowStyles.RemoveAt(this.tableLayoutPanel.RowCount - 1);
				this.tableLayoutPanel.RowCount--;
			}
			finally
			{
				this.tableLayoutPanel.ResumeLayout();
			}
		}

		// Token: 0x06001876 RID: 6262 RVA: 0x00067C18 File Offset: 0x00065E18
		private void wizardPage_TextChanged(object sender, EventArgs e)
		{
			WizardPage wizardPage = (WizardPage)sender;
			int childIndex = this.Wizard.WizardPages.GetChildIndex(wizardPage);
			Control controlFromPosition = this.tableLayoutPanel.GetControlFromPosition(0, childIndex);
			controlFromPosition.Text = wizardPage.Text;
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001877 RID: 6263 RVA: 0x00067C58 File Offset: 0x00065E58
		// (set) Token: 0x06001878 RID: 6264 RVA: 0x00067C60 File Offset: 0x00065E60
		[DefaultValue(-1)]
		protected int ActiveRow
		{
			get
			{
				return this.activeRow;
			}
			set
			{
				this.activeRow = value;
				for (int i = 0; i < this.tableLayoutPanel.RowCount; i++)
				{
					ExchangeLinkLabel exchangeLinkLabel = (ExchangeLinkLabel)this.tableLayoutPanel.GetControlFromPosition(0, i);
					if (i < this.activeRow)
					{
						exchangeLinkLabel.Image = WizardOrientationPanel.visitedPageImage;
					}
					else if (i == this.activeRow)
					{
						if (this.ActiveRow == this.tableLayoutPanel.RowCount - 1)
						{
							exchangeLinkLabel.Image = WizardOrientationPanel.visitedPageImage;
						}
						else
						{
							exchangeLinkLabel.Image = WizardOrientationPanel.currentPageImage;
						}
					}
					else
					{
						exchangeLinkLabel.Image = WizardOrientationPanel.upcomingPageImage;
					}
				}
			}
		}

		// Token: 0x04000922 RID: 2338
		private const int DefaultLineHorizontalPadding = 4;

		// Token: 0x04000923 RID: 2339
		private const int DefaultLineTopPadding = 4;

		// Token: 0x04000924 RID: 2340
		private const int DefaultLineBottomPadding = 4;

		// Token: 0x04000925 RID: 2341
		private const int LineIndentPadding = 12;

		// Token: 0x04000926 RID: 2342
		private TableLayoutPanel tableLayoutPanel;

		// Token: 0x04000927 RID: 2343
		private static readonly Bitmap visitedPageImage = Icons.WizardVisitedPage;

		// Token: 0x04000928 RID: 2344
		private static readonly Bitmap currentPageImage = Icons.WizardCurrentPage;

		// Token: 0x04000929 RID: 2345
		private static readonly Bitmap upcomingPageImage = Icons.WizardUpcomingPage;

		// Token: 0x0400092A RID: 2346
		private Wizard wizard;

		// Token: 0x0400092B RID: 2347
		private int activeRow = -1;
	}
}
