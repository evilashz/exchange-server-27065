using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000150 RID: 336
	public partial class PropertySheetDialog : ExchangeForm
	{
		// Token: 0x06000DB8 RID: 3512 RVA: 0x00034004 File Offset: 0x00032204
		public PropertySheetDialog()
		{
			this.InitializeComponent();
			base.Size = new Size(443, 507);
			this.cancelButton.Text = Strings.Cancel;
			this.okButton.Text = Strings.Ok;
			this.applyButton.Text = Strings.PropertySheetDialogApply;
			this.helpButton.Text = Strings.PropertySheetDialogHelp;
			if (PropertySheetDialog.lockImage == null)
			{
				Size empty = Size.Empty;
				empty.Width = Math.Min(this.lockButton.Width, this.lockButton.Height);
				empty.Height = empty.Width;
				PropertySheetDialog.lockImage = IconLibrary.ToBitmap(Icons.LockIcon, empty);
				PropertySheetDialog.commandLogPropertyExposureEnabledImage = IconLibrary.ToBitmap(Icons.CommandLogPropertyExposureEnabled, empty);
				PropertySheetDialog.commandLogPropertyExposureDisabledImage = IconLibrary.ToBitmap(Icons.CommandLogPropertyExposureDisabled, empty);
			}
			this.commandExposureButton.Image = PropertySheetDialog.commandLogPropertyExposureDisabledImage;
			this.lockButton.Image = PropertySheetDialog.lockImage;
			ToolTip toolTip = new ToolTip();
			toolTip.SetToolTip(this.lockButton, Strings.ShowLockButtonToolTipText);
			toolTip.SetToolTip(this.commandExposureButton, Strings.ShowEMSCommand);
			this.applyButton.Enabled = false;
			this.lockButton.Visible = false;
			this.lockButton.FlatStyle = FlatStyle.Flat;
			this.lockButton.FlatAppearance.BorderSize = 0;
			this.lockButton.FlatAppearance.BorderColor = this.lockButton.BackColor;
			this.lockButton.FlatAppearance.MouseOverBackColor = this.lockButton.BackColor;
			this.lockButton.FlatAppearance.MouseDownBackColor = this.lockButton.BackColor;
			this.commandExposureButton.Enabled = false;
			this.commandExposureButton.FlatStyle = FlatStyle.Flat;
			this.commandExposureButton.FlatAppearance.BorderSize = 0;
			this.commandExposureButton.FlatAppearance.BorderColor = this.commandExposureButton.BackColor;
			this.applyButton.Click += delegate(object param0, EventArgs param1)
			{
				this.PerformApply();
				this.SetActivePage((ExchangePropertyPageControl)this.tabControl.SelectedTab.Tag);
			};
			this.commandExposureButton.MouseEnter += delegate(object param0, EventArgs param1)
			{
				if (this.commandExposureButton.Enabled)
				{
					this.commandExposureButton.FlatStyle = FlatStyle.Standard;
				}
			};
			this.commandExposureButton.MouseLeave += delegate(object param0, EventArgs param1)
			{
				this.commandExposureButton.FlatStyle = FlatStyle.Flat;
			};
			this.commandExposureButton.Click += delegate(object param0, EventArgs param1)
			{
				if (this.isValid && this.isDirty && ((ExchangePropertyPageControl)this.tabControl.SelectedTab.Tag).OnKillActive())
				{
					List<DataHandler> list = new List<DataHandler>();
					ExchangePropertyPageControl[] array = (ExchangePropertyPageControl[])this.tabControl.Tag;
					foreach (ExchangePropertyPageControl exchangePropertyPageControl in array)
					{
						if (exchangePropertyPageControl.IsHandleCreated && exchangePropertyPageControl.Context != null && exchangePropertyPageControl.Context.IsDirty)
						{
							if (!exchangePropertyPageControl.TryApply())
							{
								return;
							}
							if (!list.Contains(exchangePropertyPageControl.DataHandler))
							{
								list.Add(exchangePropertyPageControl.DataHandler);
							}
						}
					}
					StringBuilder stringBuilder = new StringBuilder();
					foreach (DataHandler dataHandler in list)
					{
						stringBuilder.Append(dataHandler.CommandToRun);
					}
					using (PropertyPageDialog propertyPageDialog = new PropertyPageDialog(new PropertyPageCommandExposureControl
					{
						CommandToShow = stringBuilder.ToString()
					}))
					{
						propertyPageDialog.CancelVisible = false;
						((ExchangePage)this.tabControl.SelectedTab.Tag).ShowDialog(propertyPageDialog);
					}
				}
			};
			this.helpButton.Click += delegate(object param0, EventArgs param1)
			{
				this.OnHelpRequested(new HelpEventArgs(Point.Empty));
			};
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000342C4 File Offset: 0x000324C4
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled && this.tabControl.SelectedTab != null)
			{
				ExchangePage exchangePage = (ExchangePropertyPageControl)this.tabControl.SelectedTab.Tag;
				if (exchangePage != null)
				{
					ExchangeHelpService.ShowHelpFromPage(exchangePage);
					hevent.Handled = true;
				}
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00034314 File Offset: 0x00032514
		public PropertySheetDialog(string caption, ExchangePropertyPageControl[] pages) : this()
		{
			this.tabControl.Tag = pages;
			this.Text = caption;
			this.tabControl.Multiline = true;
			this.tabControl.SizeMode = TabSizeMode.FillToRight;
			foreach (ExchangePropertyPageControl exchangePropertyPageControl in pages)
			{
				this.maxPageSize = new Size(Math.Max(this.maxPageSize.Width, exchangePropertyPageControl.Width), Math.Max(this.maxPageSize.Height, exchangePropertyPageControl.Height));
				TabPage tabPage = new TabPage();
				tabPage.Tag = exchangePropertyPageControl;
				tabPage.Text = exchangePropertyPageControl.Text;
				tabPage.DataBindings.Add("Text", exchangePropertyPageControl, "Text");
				exchangePropertyPageControl.Dock = DockStyle.Fill;
				tabPage.Controls.Add(exchangePropertyPageControl);
				exchangePropertyPageControl.IsDirtyChanged += this.page_IsDirtyChanged;
				exchangePropertyPageControl.SetActived += this.page_SetActived;
				this.TabPages.Add(tabPage);
			}
			base.Load += this.PropertySheetDialog_Load;
			this.tabControl.Selecting += this.tabControl_Selecting;
			this.tabControl.Deselecting += this.tabControl_Deselecting;
			this.Apply += this.PropertySheetDialog_Apply;
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00034468 File Offset: 0x00032668
		private void PropertySheetDialog_Load(object sender, EventArgs e)
		{
			ExchangePropertyPageControl exchangePropertyPageControl = (ExchangePropertyPageControl)this.tabControl.SelectedTab.Tag;
			if (!this.maxPageSize.IsEmpty)
			{
				Size clientSize = exchangePropertyPageControl.ClientSize;
				Size sz = clientSize - this.maxPageSize;
				base.ClientSize -= sz;
			}
			this.SetActivePage(exchangePropertyPageControl);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000344C5 File Offset: 0x000326C5
		private void page_IsDirtyChanged(object sender, EventArgs e)
		{
			this.IsDirty |= ((ExchangePropertyPageControl)sender).IsDirty;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x000344DF File Offset: 0x000326DF
		private void page_SetActived(object sender, EventArgs e)
		{
			this.lockButton.Visible = ((ExchangePropertyPageControl)sender).HasLockedControls;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000344F7 File Offset: 0x000326F7
		private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
		{
			this.SetActivePage((ExchangePropertyPageControl)e.TabPage.Tag, this.tabControl.RequireFocusOnActivePageFirstChild);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003451A File Offset: 0x0003271A
		private void tabControl_Deselecting(object sender, TabControlCancelEventArgs e)
		{
			e.Cancel = !((ExchangePropertyPageControl)e.TabPage.Tag).OnKillActive();
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003453C File Offset: 0x0003273C
		private void PropertySheetDialog_Apply(object sender, CancelEventArgs e)
		{
			e.Cancel = !((ExchangePropertyPageControl)this.tabControl.SelectedTab.Tag).OnKillActive();
			if (!e.Cancel)
			{
				ExchangePropertyPageControl[] array = (ExchangePropertyPageControl[])this.tabControl.Tag;
				foreach (ExchangePropertyPageControl exchangePropertyPageControl in array)
				{
					if (exchangePropertyPageControl.IsHandleCreated)
					{
						exchangePropertyPageControl.Apply(e);
						if (e.Cancel)
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00034C92 File Offset: 0x00032E92
		private void CloseOnClick(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00034C9A File Offset: 0x00032E9A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TabControl.TabPageCollection TabPages
		{
			get
			{
				return this.tabControl.TabPages;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00034CA7 File Offset: 0x00032EA7
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x00034CB0 File Offset: 0x00032EB0
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
					this.applyButton.Enabled = (this.IsValid && this.IsDirty);
					this.commandExposureButton.Enabled = this.applyButton.Enabled;
					this.commandExposureButton.Image = (this.commandExposureButton.Enabled ? PropertySheetDialog.commandLogPropertyExposureEnabledImage : PropertySheetDialog.commandLogPropertyExposureDisabledImage);
					this.okButton.Enabled = this.IsValid;
					this.OnIsValidChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00034D40 File Offset: 0x00032F40
		protected virtual void OnIsValidChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertySheetDialog.EventIsValidChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06000DC7 RID: 3527 RVA: 0x00034D6E File Offset: 0x00032F6E
		// (remove) Token: 0x06000DC8 RID: 3528 RVA: 0x00034D81 File Offset: 0x00032F81
		public event EventHandler IsValidChanged
		{
			add
			{
				base.Events.AddHandler(PropertySheetDialog.EventIsValidChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertySheetDialog.EventIsValidChanged, value);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00034D94 File Offset: 0x00032F94
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x00034D9C File Offset: 0x00032F9C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(false)]
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
					this.applyButton.Enabled = (this.IsValid && this.IsDirty);
					this.commandExposureButton.Enabled = this.applyButton.Enabled;
					this.commandExposureButton.Image = (this.commandExposureButton.Enabled ? PropertySheetDialog.commandLogPropertyExposureEnabledImage : PropertySheetDialog.commandLogPropertyExposureDisabledImage);
					this.OnIsDirtyChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00034E1C File Offset: 0x0003301C
		protected virtual void OnIsDirtyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PropertySheetDialog.EventIsDirtyChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06000DCC RID: 3532 RVA: 0x00034E4A File Offset: 0x0003304A
		// (remove) Token: 0x06000DCD RID: 3533 RVA: 0x00034E5D File Offset: 0x0003305D
		public event EventHandler IsDirtyChanged
		{
			add
			{
				base.Events.AddHandler(PropertySheetDialog.EventIsDirtyChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertySheetDialog.EventIsDirtyChanged, value);
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00034E70 File Offset: 0x00033070
		public void PerformApply()
		{
			if (this.IsValid && this.IsDirty)
			{
				this.InternalPerformApply();
			}
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00034E8C File Offset: 0x0003308C
		private bool InternalPerformApply()
		{
			CancelEventArgs cancelEventArgs = new CancelEventArgs(false);
			this.OnApply(cancelEventArgs);
			if (!cancelEventArgs.Cancel)
			{
				this.IsDirty = false;
			}
			return !cancelEventArgs.Cancel;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00034EC0 File Offset: 0x000330C0
		protected virtual void OnApply(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[PropertySheetDialog.EventApply];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06000DD1 RID: 3537 RVA: 0x00034EEE File Offset: 0x000330EE
		// (remove) Token: 0x06000DD2 RID: 3538 RVA: 0x00034F01 File Offset: 0x00033101
		public event CancelEventHandler Apply
		{
			add
			{
				base.Events.AddHandler(PropertySheetDialog.EventApply, value);
			}
			remove
			{
				base.Events.RemoveHandler(PropertySheetDialog.EventApply, value);
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00034F14 File Offset: 0x00033114
		protected override void OnLoad(EventArgs e)
		{
			base.SelectNextControl(this.tabControl, true, true, true, true);
			base.OnLoad(e);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00034F2E File Offset: 0x0003312E
		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			if (!e.Cancel && base.DialogResult == DialogResult.OK)
			{
				e.Cancel = !this.InternalPerformApply();
				if (e.Cancel && !base.Modal)
				{
					base.DialogResult = DialogResult.None;
				}
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00034F70 File Offset: 0x00033170
		private void tabControl_ControlAdded(object sender, ControlEventArgs e)
		{
			TabPage tabPage = e.Control as TabPage;
			tabPage.BackColor = Color.Transparent;
			tabPage.UseVisualStyleBackColor = false;
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00034F9C File Offset: 0x0003319C
		private void SetActivePage(ExchangePage page, bool focusOnFirstChild)
		{
			base.BeginInvoke(new PropertySheetDialog.OnSetActiveMethodInvoker(page.OnSetActive), new object[]
			{
				focusOnFirstChild
			});
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00034FD0 File Offset: 0x000331D0
		private void SetActivePage(ExchangePage page)
		{
			base.BeginInvoke(new PropertySheetDialog.OnSetActiveMethodInvoker(page.OnSetActive), new object[]
			{
				true
			});
		}

		// Token: 0x04000581 RID: 1409
		private static Image commandLogPropertyExposureEnabledImage;

		// Token: 0x04000582 RID: 1410
		private static Image commandLogPropertyExposureDisabledImage;

		// Token: 0x04000583 RID: 1411
		private static Image lockImage;

		// Token: 0x04000584 RID: 1412
		private Size maxPageSize = Size.Empty;

		// Token: 0x04000585 RID: 1413
		private bool isValid = true;

		// Token: 0x04000586 RID: 1414
		private static readonly object EventIsValidChanged = new object();

		// Token: 0x04000587 RID: 1415
		private bool isDirty;

		// Token: 0x04000588 RID: 1416
		private static readonly object EventIsDirtyChanged = new object();

		// Token: 0x04000589 RID: 1417
		private static readonly object EventApply = new object();

		// Token: 0x02000151 RID: 337
		// (Invoke) Token: 0x06000DDF RID: 3551
		private delegate void OnSetActiveMethodInvoker(bool focusOnFirstChild);
	}
}
