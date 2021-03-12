using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001FB RID: 507
	public class PickerTextBoxBase : ExchangeUserControl, IFormatModeProvider, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x060016E6 RID: 5862 RVA: 0x00060764 File Offset: 0x0005E964
		public PickerTextBoxBase()
		{
			this.InitializeComponent();
			base.Size = this.DefaultSize;
			this.browseButton.Text = Strings.BrowseButtonText;
			this.textBox.TextChanged += this.textBox_TextChanged;
			this.textBox.Validating += this.textBox_Validating;
			this.textBox.FormatModeChanged += delegate(object param0, EventArgs param1)
			{
				this.OnFormatModeChanged(EventArgs.Empty);
			};
			this.textBox.FocusSetted += delegate(object param0, EventArgs param1)
			{
				this.textBox.Modified = false;
			};
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00060816 File Offset: 0x0005EA16
		private void textBox_Validating(object sender, CancelEventArgs e)
		{
			this.RaiseValidatingEvent();
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x0006081E File Offset: 0x0005EA1E
		private void RaiseValidatingEvent()
		{
			if (!this.TextBoxReadOnly && this.textBox.Modified)
			{
				this.OnValidating(new CancelEventArgs());
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00060840 File Offset: 0x0005EA40
		private void textBox_TextChanged(object sender, EventArgs e)
		{
			this.OnTextChanged(EventArgs.Empty);
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x0006084D File Offset: 0x0005EA4D
		// (set) Token: 0x060016EB RID: 5867 RVA: 0x00060855 File Offset: 0x0005EA55
		[DefaultValue(true)]
		public bool TextBoxReadOnly
		{
			get
			{
				return this.textBoxReadOnly;
			}
			set
			{
				this.InitTextBoxReadOnly(value, this, this.ExposedPropertyName);
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00060865 File Offset: 0x0005EA65
		internal void InitTextBoxReadOnly(bool readOnly, IFormatModeProvider owner, string bindingPropertyName)
		{
			if (this.textBox.IsHandleCreated)
			{
				this.textBox.ReadOnly = readOnly;
			}
			this.textBoxReadOnly = readOnly;
			if (!this.TextBoxReadOnly)
			{
				new TextBoxConstraintProvider(owner, bindingPropertyName, this.textBox);
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0006089D File Offset: 0x0005EA9D
		protected override void OnHandleCreated(EventArgs e)
		{
			this.textBox.ReadOnly = this.TextBoxReadOnly;
			base.OnHandleCreated(e);
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x000608B8 File Offset: 0x0005EAB8
		private void BrowseButton_Click(object sender, EventArgs e)
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnBrowseButtonClick(cancelEventArgs);
			if (!cancelEventArgs.Cancel)
			{
				this.RaiseValidatingEvent();
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000608E0 File Offset: 0x0005EAE0
		protected virtual void OnBrowseButtonClick(CancelEventArgs e)
		{
			if (this.BrowseButtonClick != null)
			{
				this.BrowseButtonClick(this, e);
			}
		}

		// Token: 0x14000099 RID: 153
		// (add) Token: 0x060016F0 RID: 5872 RVA: 0x000608F8 File Offset: 0x0005EAF8
		// (remove) Token: 0x060016F1 RID: 5873 RVA: 0x00060930 File Offset: 0x0005EB30
		public event CancelEventHandler BrowseButtonClick;

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00060965 File Offset: 0x0005EB65
		protected override Size DefaultSize
		{
			get
			{
				return new Size(336, 25);
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00060973 File Offset: 0x0005EB73
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(0);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x0006097B File Offset: 0x0005EB7B
		// (set) Token: 0x060016F5 RID: 5877 RVA: 0x00060988 File Offset: 0x0005EB88
		public override string Text
		{
			get
			{
				return this.textBox.Text;
			}
			set
			{
				this.textBox.Text = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x00060996 File Offset: 0x0005EB96
		// (set) Token: 0x060016F7 RID: 5879 RVA: 0x000609B7 File Offset: 0x0005EBB7
		[DefaultValue("")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ToolTipText
		{
			get
			{
				if (this.toolTip != null)
				{
					return this.toolTip.GetToolTip(this.textBox);
				}
				return string.Empty;
			}
			set
			{
				if (this.toolTip == null)
				{
					this.toolTip = new ToolTip();
				}
				this.toolTip.SetToolTip(this.textBox, value);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x000609DE File Offset: 0x0005EBDE
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x000609EB File Offset: 0x0005EBEB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string ButtonText
		{
			get
			{
				return this.browseButton.Text;
			}
			set
			{
				this.browseButton.Text = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x000609F9 File Offset: 0x0005EBF9
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x00060A01 File Offset: 0x0005EC01
		[DefaultValue(true)]
		public bool CanBrowse
		{
			get
			{
				return this.canBrowse;
			}
			set
			{
				if (this.CanBrowse != value)
				{
					this.canBrowse = value;
					this.UpdateBrowseButtonState();
					this.OnCanBrowseChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00060A24 File Offset: 0x0005EC24
		protected void UpdateBrowseButtonState()
		{
			this.browseButton.Enabled = (this.CanBrowse && this.ButtonAvailable());
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00060A42 File Offset: 0x0005EC42
		protected virtual bool ButtonAvailable()
		{
			return true;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00060A48 File Offset: 0x0005EC48
		protected virtual void OnCanBrowseChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PickerTextBoxBase.EventCanBrowseChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400009A RID: 154
		// (add) Token: 0x060016FF RID: 5887 RVA: 0x00060A76 File Offset: 0x0005EC76
		// (remove) Token: 0x06001700 RID: 5888 RVA: 0x00060A89 File Offset: 0x0005EC89
		public event EventHandler CanBrowseChanged
		{
			add
			{
				base.Events.AddHandler(PickerTextBoxBase.EventCanBrowseChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PickerTextBoxBase.EventCanBrowseChanged, value);
			}
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00060A9C File Offset: 0x0005EC9C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00060ABC File Offset: 0x0005ECBC
		private void InitializeComponent()
		{
			this.browseButton = new ExchangeButton();
			this.tableLayoutPanel = new TableLayoutPanel();
			this.textBox = new ExchangeTextBox();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.browseButton.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.browseButton.AutoSize = true;
			this.browseButton.BackColor = SystemColors.Control;
			this.browseButton.Location = new Point(260, 0);
			this.browseButton.Margin = new Padding(3, 0, 0, 0);
			this.browseButton.MinimumSize = new Size(75, 23);
			this.browseButton.Name = "browseButton";
			this.browseButton.Size = new Size(76, 23);
			this.browseButton.TabIndex = 2;
			this.browseButton.UseVisualStyleBackColor = false;
			this.browseButton.UseCompatibleTextRendering = false;
			this.browseButton.Click += this.BrowseButton_Click;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 5f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
			this.tableLayoutPanel.Controls.Add(this.textBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.browseButton, 2, 0);
			this.tableLayoutPanel.Dock = DockStyle.Fill;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Size = new Size(336, 23);
			this.tableLayoutPanel.TabIndex = 3;
			this.textBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.textBox.Location = new Point(0, 2);
			this.textBox.Margin = new Padding(3, 2, 0, 1);
			this.textBox.Name = "textBox";
			this.textBox.ReadOnly = true;
			this.textBox.Size = new Size(252, 20);
			this.textBox.TabIndex = 1;
			base.Controls.Add(this.tableLayoutPanel);
			base.Margin = new Padding(0);
			base.Name = "PickerTextBoxBase";
			base.Size = new Size(336, 23);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x00060DBD File Offset: 0x0005EFBD
		// (set) Token: 0x06001704 RID: 5892 RVA: 0x00060DCA File Offset: 0x0005EFCA
		[DefaultValue(0)]
		public DisplayFormatMode FormatMode
		{
			get
			{
				return this.textBox.FormatMode;
			}
			set
			{
				this.textBox.FormatMode = value;
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x00060DD8 File Offset: 0x0005EFD8
		protected virtual void OnFormatModeChanged(EventArgs e)
		{
			if (this.FormatModeChanged != null)
			{
				this.FormatModeChanged(this, e);
			}
		}

		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06001706 RID: 5894 RVA: 0x00060DF0 File Offset: 0x0005EFF0
		// (remove) Token: 0x06001707 RID: 5895 RVA: 0x00060E28 File Offset: 0x0005F028
		public event EventHandler FormatModeChanged;

		// Token: 0x06001708 RID: 5896 RVA: 0x00060E69 File Offset: 0x0005F069
		void IFormatModeProvider.add_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged += A_1;
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x00060E72 File Offset: 0x0005F072
		void IFormatModeProvider.remove_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged -= A_1;
		}

		// Token: 0x04000882 RID: 2178
		private bool textBoxReadOnly = true;

		// Token: 0x04000884 RID: 2180
		private ToolTip toolTip;

		// Token: 0x04000885 RID: 2181
		private bool canBrowse = true;

		// Token: 0x04000886 RID: 2182
		private static readonly object EventCanBrowseChanged = new object();

		// Token: 0x04000887 RID: 2183
		private IContainer components;

		// Token: 0x04000888 RID: 2184
		private ExchangeButton browseButton;

		// Token: 0x04000889 RID: 2185
		private ExchangeTextBox textBox;

		// Token: 0x0400088A RID: 2186
		private TableLayoutPanel tableLayoutPanel;
	}
}
