using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200013E RID: 318
	public class GeneralPageHeader : ExchangeUserControl, IFormatModeProvider, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x06000C7B RID: 3195 RVA: 0x0002CF68 File Offset: 0x0002B168
		public GeneralPageHeader()
		{
			this.InitializeComponent();
			this.displayNameTextBoxBorderStyle = this.displayNameTextBox.BorderStyle;
			this.displayNameTextBoxReadOnly = this.displayNameTextBox.ReadOnly;
			this.displayNameTextBox.TextChanged += delegate(object param0, EventArgs param1)
			{
				this.OnTextChanged(EventArgs.Empty);
			};
			this.displayNameTextBox.FormatModeChanged += delegate(object param0, EventArgs param1)
			{
				this.OnFormatModeChanged(EventArgs.Empty);
			};
			new TextBoxConstraintProvider(this, this.displayNameTextBox);
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0002CFF3 File Offset: 0x0002B1F3
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0002D000 File Offset: 0x0002B200
		[DefaultValue(0)]
		public DisplayFormatMode FormatMode
		{
			get
			{
				return this.displayNameTextBox.FormatMode;
			}
			set
			{
				this.displayNameTextBox.FormatMode = value;
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002D010 File Offset: 0x0002B210
		protected virtual void OnFormatModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[GeneralPageHeader.EventFormatModeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06000C7F RID: 3199 RVA: 0x0002D03E File Offset: 0x0002B23E
		// (remove) Token: 0x06000C80 RID: 3200 RVA: 0x0002D051 File Offset: 0x0002B251
		public event EventHandler FormatModeChanged
		{
			add
			{
				base.Events.AddHandler(GeneralPageHeader.EventFormatModeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(GeneralPageHeader.EventFormatModeChanged, value);
			}
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002D064 File Offset: 0x0002B264
		private void InitializeComponent()
		{
			this.headerNamePanel = new TableLayoutPanel();
			this.objectPictureBox = new ExchangePictureBox();
			this.displayNameTextBox = new ExchangeTextBox();
			this.headerNamePanel.SuspendLayout();
			((ISupportInitialize)this.objectPictureBox).BeginInit();
			base.SuspendLayout();
			this.headerNamePanel.AutoSize = true;
			this.headerNamePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.headerNamePanel.ColumnCount = 2;
			this.headerNamePanel.ColumnStyles.Add(new ColumnStyle());
			this.headerNamePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.headerNamePanel.Controls.Add(this.objectPictureBox, 0, 0);
			this.headerNamePanel.Controls.Add(this.displayNameTextBox, 1, 0);
			this.headerNamePanel.Dock = DockStyle.Fill;
			this.headerNamePanel.Location = new Point(0, 0);
			this.headerNamePanel.Margin = new Padding(0);
			this.headerNamePanel.Name = "headerNamePanel";
			this.headerNamePanel.RowCount = 1;
			this.headerNamePanel.RowStyles.Add(new RowStyle());
			this.headerNamePanel.Size = new Size(386, 34);
			this.headerNamePanel.TabIndex = 0;
			this.objectPictureBox.Location = new Point(0, 0);
			this.objectPictureBox.Margin = new Padding(0, 0, 0, 2);
			this.objectPictureBox.Name = "objectPictureBox";
			this.objectPictureBox.Size = new Size(32, 32);
			this.objectPictureBox.TabIndex = 1;
			this.objectPictureBox.TabStop = false;
			this.displayNameTextBox.Anchor = (AnchorStyles.Left | AnchorStyles.Right);
			this.displayNameTextBox.Location = new Point(44, 7);
			this.displayNameTextBox.Margin = new Padding(12, 0, 0, 0);
			this.displayNameTextBox.Name = "displayNameTextBox";
			this.displayNameTextBox.Size = new Size(342, 20);
			this.displayNameTextBox.TabIndex = 2;
			base.Controls.Add(this.headerNamePanel);
			this.Margin = new Padding(0);
			base.Name = "GeneralPageHeader";
			base.Size = new Size(386, 34);
			this.headerNamePanel.ResumeLayout(false);
			this.headerNamePanel.PerformLayout();
			((ISupportInitialize)this.objectPictureBox).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0002D2EB File Offset: 0x0002B4EB
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0002D2F8 File Offset: 0x0002B4F8
		public override string Text
		{
			get
			{
				return this.displayNameTextBox.Text;
			}
			set
			{
				this.displayNameTextBox.Text = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0002D306 File Offset: 0x0002B506
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0002D310 File Offset: 0x0002B510
		[DefaultValue(null)]
		public Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (this.Icon != value)
				{
					Bitmap image = IconLibrary.ToBitmap(value, this.objectPictureBox.Size);
					if (this.objectPictureBox.Image != null)
					{
						this.objectPictureBox.Image.Dispose();
					}
					this.objectPictureBox.Image = image;
					this.icon = value;
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0002D368 File Offset: 0x0002B568
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0002D370 File Offset: 0x0002B570
		[DefaultValue(true)]
		public bool CanChangeHeaderText
		{
			get
			{
				return this.canChangeHeaderText;
			}
			set
			{
				if (value != this.CanChangeHeaderText)
				{
					this.canChangeHeaderText = value;
					this.displayNameTextBoxReadOnly = !value;
					this.displayNameTextBoxBorderStyle = (value ? BorderStyle.Fixed3D : BorderStyle.None);
					this.RefreshDisplayNameTextBoxStyle();
				}
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0002D39F File Offset: 0x0002B59F
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0002D3A7 File Offset: 0x0002B5A7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Margin
		{
			get
			{
				return base.Margin;
			}
			set
			{
				base.Margin = value;
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002D3B0 File Offset: 0x0002B5B0
		private void RefreshDisplayNameTextBoxStyle()
		{
			if (base.IsHandleCreated)
			{
				this.displayNameTextBox.ReadOnly = this.displayNameTextBoxReadOnly;
				this.displayNameTextBox.BorderStyle = this.displayNameTextBoxBorderStyle;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002D3DC File Offset: 0x0002B5DC
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.RefreshDisplayNameTextBoxStyle();
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0002D3EB File Offset: 0x0002B5EB
		protected override string ExposedPropertyName
		{
			get
			{
				return "Text";
			}
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002D3FE File Offset: 0x0002B5FE
		void IFormatModeProvider.add_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged += A_1;
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002D407 File Offset: 0x0002B607
		void IFormatModeProvider.remove_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged -= A_1;
		}

		// Token: 0x0400050C RID: 1292
		private TableLayoutPanel headerNamePanel;

		// Token: 0x0400050D RID: 1293
		private ExchangeTextBox displayNameTextBox;

		// Token: 0x0400050E RID: 1294
		private ExchangePictureBox objectPictureBox;

		// Token: 0x0400050F RID: 1295
		private bool canChangeHeaderText = true;

		// Token: 0x04000510 RID: 1296
		private static readonly object EventFormatModeChanged = new object();

		// Token: 0x04000511 RID: 1297
		private Icon icon;

		// Token: 0x04000512 RID: 1298
		private BorderStyle displayNameTextBoxBorderStyle;

		// Token: 0x04000513 RID: 1299
		private bool displayNameTextBoxReadOnly;
	}
}
