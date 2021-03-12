using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000138 RID: 312
	public partial class ExchangeDialog : ExchangeForm
	{
		// Token: 0x06000C47 RID: 3143 RVA: 0x0002C0D0 File Offset: 0x0002A2D0
		public ExchangeDialog()
		{
			this.InitializeComponent();
			this.cancelButton.Text = Strings.Cancel;
			this.okButton.Text = Strings.Ok;
			this.helpButton.Text = Strings.Help;
			this.buttonsPanel.TabIndex = int.MaxValue;
			if (ExchangeDialog.lockImage == null)
			{
				Size empty = Size.Empty;
				empty.Width = Math.Min(this.lockButton.Width, this.lockButton.Height);
				empty.Height = empty.Width;
				ExchangeDialog.lockImage = IconLibrary.ToBitmap(Icons.LockIcon, empty);
			}
			this.lockButton.Image = ExchangeDialog.lockImage;
			this.lockButton.Visible = false;
			this.lockButton.FlatStyle = FlatStyle.Flat;
			this.lockButton.FlatAppearance.BorderSize = 0;
			this.lockButton.FlatAppearance.BorderColor = this.lockButton.BackColor;
			this.lockButton.FlatAppearance.MouseOverBackColor = this.lockButton.BackColor;
			this.lockButton.FlatAppearance.MouseDownBackColor = this.lockButton.BackColor;
			ToolTip toolTip = new ToolTip();
			toolTip.SetToolTip(this.lockButton, Strings.ShowLockButtonToolTipText);
			this.helpButton.Click += delegate(object param0, EventArgs param1)
			{
				this.OnHelpRequested(new HelpEventArgs(Point.Empty));
			};
			this.helpButton.Visible = false;
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0002C718 File Offset: 0x0002A918
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0002C725 File Offset: 0x0002A925
		[DefaultValue(true)]
		public bool OkEnabled
		{
			get
			{
				return this.okButton.Enabled;
			}
			set
			{
				this.okButton.Enabled = value;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0002C733 File Offset: 0x0002A933
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x0002C740 File Offset: 0x0002A940
		[DefaultValue(true)]
		public bool OkVisible
		{
			get
			{
				return this.okButton.Visible;
			}
			set
			{
				this.okButton.Visible = value;
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0002C74E File Offset: 0x0002A94E
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0002C75B File Offset: 0x0002A95B
		[DefaultValue(true)]
		public bool CancelEnabled
		{
			get
			{
				return this.cancelButton.Enabled;
			}
			set
			{
				this.cancelButton.Enabled = value;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0002C769 File Offset: 0x0002A969
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0002C776 File Offset: 0x0002A976
		[DefaultValue(true)]
		public bool CancelVisible
		{
			get
			{
				return this.cancelButton.Visible;
			}
			set
			{
				this.cancelButton.Visible = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0002C784 File Offset: 0x0002A984
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0002C791 File Offset: 0x0002A991
		[DefaultValue(false)]
		public bool LockVisible
		{
			get
			{
				return this.lockButton.Visible;
			}
			set
			{
				this.lockButton.Visible = value;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0002C79F File Offset: 0x0002A99F
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0002C7AC File Offset: 0x0002A9AC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string OkButtonText
		{
			get
			{
				return this.okButton.Text;
			}
			set
			{
				this.okButton.Text = value;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0002C7BA File Offset: 0x0002A9BA
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0002C7C7 File Offset: 0x0002A9C7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public string CancelButtonText
		{
			get
			{
				return this.cancelButton.Text;
			}
			set
			{
				this.cancelButton.Text = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0002C7D5 File Offset: 0x0002A9D5
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0002C7E2 File Offset: 0x0002A9E2
		[DefaultValue(false)]
		public bool HelpVisible
		{
			get
			{
				return this.helpButton.Visible;
			}
			set
			{
				this.helpButton.Visible = value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0002C7F0 File Offset: 0x0002A9F0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public TableLayoutPanel ButtonsPanel
		{
			get
			{
				return this.buttonsPanel;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0002C7F8 File Offset: 0x0002A9F8
		private void CloseOnClick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002C800 File Offset: 0x0002AA00
		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.buttonsPanel.SendToBack();
			base.OnLayout(levent);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0002C814 File Offset: 0x0002AA14
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			if (base.Owner == null)
			{
				this.HelpVisible = true;
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002C82C File Offset: 0x0002AA2C
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!this.HelpVisible)
			{
				hevent.Handled = true;
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x04000507 RID: 1287
		private static Bitmap lockImage;
	}
}
