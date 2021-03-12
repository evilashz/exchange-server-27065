using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SnapIn;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200014F RID: 335
	public partial class PropertyPageDialog : ExchangeDialog
	{
		// Token: 0x06000D9A RID: 3482 RVA: 0x0003397E File Offset: 0x00031B7E
		public PropertyPageDialog()
		{
			this.InitializeComponent();
			this.AutoSizeControl = true;
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0003399A File Offset: 0x00031B9A
		public PropertyPageDialog(ExchangePropertyPageControl propertyPage) : this()
		{
			this.RegisterPropertyPage(propertyPage);
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x000339C0 File Offset: 0x00031BC0
		protected override string DefaultHelpTopic
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000339C7 File Offset: 0x00031BC7
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled && base.HelpVisible && this.Control != null && string.IsNullOrEmpty(base.HelpTopic))
			{
				ExchangeHelpService.ShowHelpFromPage(this.Control);
				hevent.Handled = true;
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x00033A84 File Offset: 0x00031C84
		[DefaultValue(null)]
		public ExchangePropertyPageControl Control
		{
			get
			{
				return this.control;
			}
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00033A8C File Offset: 0x00031C8C
		protected void RegisterPropertyPage(ExchangePropertyPageControl ctrl)
		{
			if (ctrl != null)
			{
				ctrl.TabIndex = 0;
				base.Controls.Add(ctrl);
				base.Controls.SetChildIndex(ctrl, 0);
				ctrl.IsDirtyChanged += this.page_IsDirtyChanged;
				base.Name += ctrl.Name;
				this.Text = ctrl.Text;
				ctrl.SetActived += this.page_SetActived;
				ctrl.OnSetActive();
				this.control = ctrl;
			}
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00033B10 File Offset: 0x00031D10
		private void page_SetActived(object sender, EventArgs e)
		{
			base.LockVisible = ((ExchangePropertyPageControl)sender).HasLockedControls;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00033B23 File Offset: 0x00031D23
		protected override void OnLoad(EventArgs e)
		{
			base.SuspendLayout();
			this.FitSizeToContent();
			base.ResumeLayout(true);
			base.OnLoad(e);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00033B40 File Offset: 0x00031D40
		internal void FitSizeToContent()
		{
			if (this.Control != null)
			{
				int num = base.ClientSize.Height - (this.GetPreferredHeightForControl() + base.ButtonsPanel.Height);
				base.Height -= num;
				this.Control.Dock = DockStyle.Fill;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00033B91 File Offset: 0x00031D91
		// (set) Token: 0x06000DA6 RID: 3494 RVA: 0x00033B99 File Offset: 0x00031D99
		[DefaultValue(true)]
		public bool AutoSizeControl { get; set; }

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00033BA4 File Offset: 0x00031DA4
		private int GetPreferredHeightForControl()
		{
			Size size = this.Control.Size;
			if (this.AutoSizeControl)
			{
				this.Control.Dock = DockStyle.Top;
				size = this.Control.GetPreferredSize(new Size(base.ClientSize.Width, 0));
			}
			return size.Height;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00033BF8 File Offset: 0x00031DF8
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			base.HelpVisible = (base.HelpVisible && (!string.IsNullOrEmpty(base.HelpTopic) || (this.Control != null && !string.IsNullOrEmpty(this.Control.HelpTopic))));
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00033C4C File Offset: 0x00031E4C
		protected override void OnClosing(CancelEventArgs e)
		{
			if (base.DialogResult == DialogResult.OK && this.Control != null)
			{
				e.Cancel = !this.Control.OnKillActive();
				if (!e.Cancel && this.Control.IsDirty)
				{
					this.Control.Apply(e);
				}
			}
			base.OnClosing(e);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00033CA6 File Offset: 0x00031EA6
		private void page_IsDirtyChanged(object sender, EventArgs e)
		{
			this.IsDirty |= ((ExchangePropertyPageControl)sender).IsDirty;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00033CC0 File Offset: 0x00031EC0
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x00033CC8 File Offset: 0x00031EC8
		[DefaultValue(true)]
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				if (this.IsValid != value)
				{
					this.isValid = value;
					if (this.LinkIsDirtyToOkEnabled)
					{
						base.OkEnabled = (this.IsValid && this.IsDirty);
					}
					this.OnIsValidChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00033D04 File Offset: 0x00031F04
		protected virtual void OnIsValidChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyPageDialog.EventIsValidChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06000DAE RID: 3502 RVA: 0x00033D32 File Offset: 0x00031F32
		// (remove) Token: 0x06000DAF RID: 3503 RVA: 0x00033D45 File Offset: 0x00031F45
		public event EventHandler IsValidChanged
		{
			add
			{
				base.Events.AddHandler(PropertyPageDialog.EventIsValidChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyPageDialog.EventIsValidChanged, value);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x00033D58 File Offset: 0x00031F58
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x00033D60 File Offset: 0x00031F60
		[DefaultValue(false)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDirty
		{
			get
			{
				return this.isDirty;
			}
			set
			{
				if (this.IsDirty != value)
				{
					this.isDirty = value;
					if (this.LinkIsDirtyToOkEnabled)
					{
						base.OkEnabled = (this.IsValid && this.IsDirty);
					}
					this.OnIsDirtyChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00033D9C File Offset: 0x00031F9C
		protected virtual void OnIsDirtyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertyPageDialog.EventIsDirtyChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06000DB3 RID: 3507 RVA: 0x00033DCA File Offset: 0x00031FCA
		// (remove) Token: 0x06000DB4 RID: 3508 RVA: 0x00033DDD File Offset: 0x00031FDD
		public event EventHandler IsDirtyChanged
		{
			add
			{
				base.Events.AddHandler(PropertyPageDialog.EventIsDirtyChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertyPageDialog.EventIsDirtyChanged, value);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00033DF0 File Offset: 0x00031FF0
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x00033DF8 File Offset: 0x00031FF8
		[DefaultValue(false)]
		public bool LinkIsDirtyToOkEnabled
		{
			get
			{
				return this.linkIsDirtyToOkEnabled;
			}
			set
			{
				if (value != this.linkIsDirtyToOkEnabled)
				{
					this.linkIsDirtyToOkEnabled = value;
					if (value)
					{
						base.OkEnabled = (this.IsValid && this.IsDirty);
					}
				}
			}
		}

		// Token: 0x04000570 RID: 1392
		private ExchangePropertyPageControl control;

		// Token: 0x04000572 RID: 1394
		private bool isValid = true;

		// Token: 0x04000573 RID: 1395
		private static readonly object EventIsValidChanged = new object();

		// Token: 0x04000574 RID: 1396
		private bool isDirty;

		// Token: 0x04000575 RID: 1397
		private static readonly object EventIsDirtyChanged = new object();

		// Token: 0x04000576 RID: 1398
		private bool linkIsDirtyToOkEnabled;
	}
}
