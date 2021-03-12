using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000217 RID: 535
	public partial class WizardForm : ExchangeForm
	{
		// Token: 0x06001840 RID: 6208 RVA: 0x00066338 File Offset: 0x00064538
		public WizardForm()
		{
			this.InitializeComponent();
			this.title.Font = new Font(Control.DefaultFont.FontFamily, Control.DefaultFont.SizeInPoints + 2f, FontStyle.Bold);
			this.buttons.SuspendLayout();
			this.help.Command = this.wizard.Help;
			this.reset.Command = this.wizard.Reset;
			this.back.Command = this.wizard.Back;
			this.next.Command = this.wizard.Next;
			this.finish.Command = this.wizard.Finish;
			this.cancel.Command = this.wizard.Cancel;
			this.next.VisibleChanged += this.buttons_StateChanged;
			this.next.EnabledChanged += this.buttons_StateChanged;
			this.finish.VisibleChanged += this.buttons_StateChanged;
			this.finish.EnabledChanged += this.buttons_StateChanged;
			this.buttons.ResumeLayout();
			this.wizard.UpdatingButtons += delegate(object param0, EventArgs param1)
			{
				this.buttons.SuspendLayout();
			};
			this.wizard.ButtonsUpdated += delegate(object param0, EventArgs param1)
			{
				this.buttons.ResumeLayout();
			};
			this.wizard.TextChanged += delegate(object param0, EventArgs param1)
			{
				this.pageTitle.Text = this.wizard.Text;
			};
			Extensions.EnsureDoubleBuffer(this);
			Theme.UseVisualEffectsChanged += this.Theme_UseVisualEffectsChanged;
			this.Theme_UseVisualEffectsChanged(null, EventArgs.Empty);
			this.backgroundPanel.MouseDown += delegate(object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					UnsafeNativeMethods.ReleaseCapture();
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 161, 2, 0U);
				}
			};
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06001841 RID: 6209 RVA: 0x0006650D File Offset: 0x0006470D
		public override bool RightToLeftLayout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00066530 File Offset: 0x00064730
		private void Theme_UseVisualEffectsChanged(object sender, EventArgs e)
		{
			if (Theme.UseVisualEffects)
			{
				this.BackgroundImage = (LayoutHelper.IsRightToLeft(this) ? Icons.SilverWizardRTL : Icons.SilverWizard);
				this.BackgroundImageLayout = ImageLayout.Stretch;
				base.FormBorderStyle = FormBorderStyle.None;
				this.transparencyMask.SetTransparencyImage(this, LayoutHelper.IsRightToLeft(this) ? Icons.SilverWizardRTL : Icons.SilverWizard);
				this.transparencyMask.SetTransparencyKey(this, Color.Fuchsia);
				if (LayoutHelper.IsRightToLeft(this))
				{
					base.Region.Translate(-7, 0);
					if (Application.RenderWithVisualStyles)
					{
						base.Region.Translate(-10, 0);
						return;
					}
				}
			}
			else
			{
				this.BackgroundImage = null;
				this.transparencyMask.SetTransparencyImage(this, null);
				base.FormBorderStyle = FormBorderStyle.FixedDialog;
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00067136 File Offset: 0x00065336
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.title.Text = this.Text;
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00067150 File Offset: 0x00065350
		public Wizard Wizard
		{
			get
			{
				return this.wizard;
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x00067158 File Offset: 0x00065358
		// (set) Token: 0x06001848 RID: 6216 RVA: 0x00067160 File Offset: 0x00065360
		[DefaultValue(null)]
		public new Icon Icon
		{
			get
			{
				return this.icon;
			}
			set
			{
				if (value != this.Icon)
				{
					base.Icon = value;
					this.icon = value;
					if (this.Image != null)
					{
						this.Image.Dispose();
					}
					this.Image = IconLibrary.ToBitmap(value, new Size(64, 64));
				}
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001849 RID: 6217 RVA: 0x000671AC File Offset: 0x000653AC
		// (set) Token: 0x0600184A RID: 6218 RVA: 0x000671B9 File Offset: 0x000653B9
		private Image Image
		{
			get
			{
				return this.pictureBox.Image;
			}
			set
			{
				this.pictureBox.Image = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x0600184B RID: 6219 RVA: 0x000671C7 File Offset: 0x000653C7
		// (set) Token: 0x0600184C RID: 6220 RVA: 0x000671D4 File Offset: 0x000653D4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public WizardPage CurrentPage
		{
			get
			{
				return this.wizard.CurrentPage;
			}
			set
			{
				this.wizard.CurrentPage = value;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x0600184D RID: 6221 RVA: 0x000671E2 File Offset: 0x000653E2
		// (set) Token: 0x0600184E RID: 6222 RVA: 0x000671EF File Offset: 0x000653EF
		[DefaultValue(null)]
		public DataContext Context
		{
			get
			{
				return this.wizard.Context;
			}
			set
			{
				this.wizard.Context = value;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x000671FD File Offset: 0x000653FD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TypedControlCollection<WizardPage> WizardPages
		{
			get
			{
				return this.wizard.WizardPages;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001850 RID: 6224 RVA: 0x0006720A File Offset: 0x0006540A
		// (set) Token: 0x06001851 RID: 6225 RVA: 0x00067212 File Offset: 0x00065412
		[DefaultValue(null)]
		public IRefreshable RefreshOnFinish
		{
			get
			{
				return this.refreshOnFinish;
			}
			set
			{
				this.refreshOnFinish = value;
			}
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0006721B File Offset: 0x0006541B
		// (set) Token: 0x06001853 RID: 6227 RVA: 0x00067223 File Offset: 0x00065423
		[DefaultValue(null)]
		public Command OriginatingCommand
		{
			get
			{
				return this.originatingCommand;
			}
			set
			{
				this.originatingCommand = value;
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0006722C File Offset: 0x0006542C
		private void wizard_CurrentPageChanged(object sender, EventArgs e)
		{
			WizardPage currentPage = this.CurrentPage;
			if (currentPage == null || !currentPage.Enabled || currentPage.ActiveControl == null)
			{
				if (this.next.Enabled && this.next.Visible)
				{
					this.next.Select();
					return;
				}
				if (this.finish.Enabled && this.finish.Visible)
				{
					this.finish.Select();
					return;
				}
				if (this.cancel.Enabled && this.cancel.Visible)
				{
					this.cancel.Select();
				}
			}
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x000672C4 File Offset: 0x000654C4
		private void buttons_StateChanged(object sender, EventArgs e)
		{
			if (this.next.Enabled && this.next.Visible)
			{
				base.AcceptButton = this.next;
				if (this.cancel.Focused)
				{
					this.next.Select();
					return;
				}
			}
			else
			{
				if (this.finish.Enabled && this.finish.Visible)
				{
					base.AcceptButton = this.finish;
					this.finish.Select();
					return;
				}
				base.AcceptButton = null;
				this.wizard.Select();
			}
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00067354 File Offset: 0x00065554
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 274)
			{
				base.WndProc(ref m);
				return;
			}
			int num = NativeMethods.LOWORD(m.WParam) & 65520;
			if (num == 61488 || num == 61472)
			{
				m.Result = IntPtr.Zero;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000673B0 File Offset: 0x000655B0
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if ((Keys)131139 == keyData)
			{
				TridentsWizardPage tridentsWizardPage = this.CurrentPage as TridentsWizardPage;
				if (tridentsWizardPage != null)
				{
					WinformsHelper.SetDataObjectToClipboard(tridentsWizardPage.GetSummaryText(), false);
					return true;
				}
			}
			if ((Keys)262259 == keyData)
			{
				this.Cancel();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000673FA File Offset: 0x000655FA
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled && this.CurrentPage != null)
			{
				ExchangeHelpService.ShowHelpFromPage(this.CurrentPage);
				hevent.Handled = true;
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x00067425 File Offset: 0x00065625
		public void ShowHelp()
		{
			this.help.PerformClick();
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x00067432 File Offset: 0x00065632
		public void GoBack()
		{
			this.back.PerformClick();
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0006743F File Offset: 0x0006563F
		public void GoForward()
		{
			this.next.PerformClick();
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0006744C File Offset: 0x0006564C
		public void Finish()
		{
			this.finish.PerformClick();
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00067459 File Offset: 0x00065659
		public void Cancel()
		{
			this.cancel.PerformClick();
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x0600185E RID: 6238 RVA: 0x00067466 File Offset: 0x00065666
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool CanGoBack
		{
			get
			{
				return this.back.Enabled;
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x00067473 File Offset: 0x00065673
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool CanGoForward
		{
			get
			{
				return this.next.Enabled;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001860 RID: 6240 RVA: 0x00067480 File Offset: 0x00065680
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool CanFinish
		{
			get
			{
				return this.finish.Enabled;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001861 RID: 6241 RVA: 0x0006748D File Offset: 0x0006568D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public bool CanCancel
		{
			get
			{
				return this.cancel.Enabled;
			}
		}

		// Token: 0x0400091E RID: 2334
		private Icon icon;

		// Token: 0x04000920 RID: 2336
		private IRefreshable refreshOnFinish;

		// Token: 0x04000921 RID: 2337
		private Command originatingCommand;

		// Token: 0x02000218 RID: 536
		private class AntiAliasedLabel : Label
		{
			// Token: 0x06001866 RID: 6246 RVA: 0x0006749A File Offset: 0x0006569A
			public AntiAliasedLabel()
			{
				base.Name = "AntiAliasedLabel";
			}

			// Token: 0x06001867 RID: 6247 RVA: 0x000674AD File Offset: 0x000656AD
			protected override void OnPaint(PaintEventArgs e)
			{
				e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
				base.OnPaint(e);
			}

			// Token: 0x06001868 RID: 6248 RVA: 0x000674C4 File Offset: 0x000656C4
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg == 132)
				{
					m.Result = (IntPtr)(-1);
					return;
				}
				base.WndProc(ref m);
			}
		}

		// Token: 0x02000219 RID: 537
		private class WizardIcon : ExchangePictureBox
		{
			// Token: 0x06001869 RID: 6249 RVA: 0x000674F4 File Offset: 0x000656F4
			public WizardIcon()
			{
				base.Name = "WizardIcon";
			}

			// Token: 0x0600186A RID: 6250 RVA: 0x00067508 File Offset: 0x00065708
			[EditorBrowsable(EditorBrowsableState.Advanced)]
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg == 132)
				{
					m.Result = (IntPtr)(-1);
					return;
				}
				base.WndProc(ref m);
			}
		}
	}
}
