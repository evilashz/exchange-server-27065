using System;
using System.ComponentModel;
using System.Drawing;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200015F RID: 351
	public abstract partial class SearchDialog : ExchangeForm
	{
		// Token: 0x06000E2B RID: 3627 RVA: 0x00035D14 File Offset: 0x00033F14
		protected SearchDialog()
		{
			this.InitializeComponent();
			this.okButton.Text = Strings.Ok;
			this.cancelButton.Text = Strings.Cancel;
			this.helpButton.Text = Strings.Help;
			base.Icon = Icons.ObjectPicker;
			this.fileToolStripMenuItem.Text = Strings.ObjectPickerFile;
			this.viewToolStripMenuItem.Text = Strings.ObjectPickerView;
			this.closeToolStripMenuItem.Text = Strings.ObjectPickerClose;
			this.scopeToolStripMenuItem.Text = Strings.ObjectPickerScope;
			this.modifyRecipientPickerScopeToolStripMenuItem.Text = Strings.ObjectPickerModifyRecipientPickerScope;
			this.modifyExpectedResultSizeMenuItem.Text = Strings.SearchDialogModifyExpectedResultSize;
			this.toolStripLabelForName.Text = Strings.ObjectPickerSearch;
			this.findNowCommand = new Command();
			this.findNowCommand.Text = Strings.ObjectPickerFindNow;
			this.findNowCommand.Description = Strings.ObjectPickerFindNowDescription;
			this.findNowCommand.Name = this.toolStripButtonFindNow.Name;
			this.toolStripButtonFindNow.Command = this.findNowCommand;
			this.findNowCommand.Execute += delegate(object param0, EventArgs param1)
			{
				this.FindNow();
			};
			this.clearCommand = new Command();
			this.clearCommand.Text = Strings.ObjectPickerClear;
			this.clearCommand.Description = Strings.ObjectPickerClearDescription;
			this.clearCommand.Name = this.toolStripButtonClearOrStop.Name;
			this.clearCommand.Execute += delegate(object param0, EventArgs param1)
			{
				this.OnClear();
			};
			this.stopCommand = new Command();
			this.stopCommand.Text = Strings.ObjectPickerStop;
			this.stopCommand.Description = Strings.ObjectPickerStopDescription;
			this.stopCommand.Name = this.toolStripButtonClearOrStop.Name;
			this.stopCommand.Execute += this.stopCommand_Execute;
			this.enableColumnFiltferingToolStripMenuItem.Text = Strings.ObjectPickerEnableColumnFiltering;
			this.enableColumnFiltferingToolStripMenuItem.ToolTipText = string.Empty;
			this.resultListViewRefreshCommand = new Command();
			this.resultListViewRefreshCommand.Name = "resultListViewRefreshCommand";
			this.resultListViewRefreshCommand.Execute += delegate(object param0, EventArgs param1)
			{
				this.OnResultListViewRefresh();
			};
			this.UpdateControls(false);
			this.toolStripTextBoxForName.KeyPress += delegate(object sender, KeyPressEventArgs e)
			{
				if (e.KeyChar == '\r')
				{
					e.Handled = true;
					this.FindNowCommand.Invoke();
				}
			};
			this.closeToolStripMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				base.DialogResult = DialogResult.Cancel;
			};
			this.modifyRecipientPickerScopeToolStripMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				this.OnModifyRecipientPickerScopeToolStripMenuItemClicked();
			};
			this.modifyExpectedResultSizeMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				this.OnModifyExpectedResultSizeMenuItemClicked();
			};
			this.helpButton.Visible = false;
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00036044 File Offset: 0x00034244
		protected virtual string ValidatingSearchText(string searchText)
		{
			string result = null;
			if (this.IsANRSearch() && SearchDialog.SearchTextConstraint != null)
			{
				if (searchText.Length < SearchDialog.SearchTextConstraint.MinLength)
				{
					result = Strings.SearchDialogSearchTextTooShort(SearchDialog.SearchTextConstraint.MinLength);
				}
				if (searchText.Length > SearchDialog.SearchTextConstraint.MaxLength)
				{
					result = Strings.SearchDialogSearchTextTooLong(SearchDialog.SearchTextConstraint.MaxLength);
				}
			}
			return result;
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x000360B1 File Offset: 0x000342B1
		public static ValidateLengthAttribute SearchTextConstraint
		{
			get
			{
				if (SearchDialog.searchTextConstraint == null)
				{
					SearchDialog.searchTextConstraint = new ValidateLengthAttribute(3, 5120);
				}
				return SearchDialog.searchTextConstraint;
			}
		}

		// Token: 0x06000E2E RID: 3630
		protected abstract bool IsANRSearch();

		// Token: 0x06000E2F RID: 3631
		protected abstract void OnFindNow();

		// Token: 0x06000E30 RID: 3632
		protected abstract void OnClear();

		// Token: 0x06000E31 RID: 3633
		protected abstract void OnStop();

		// Token: 0x06000E32 RID: 3634
		protected abstract void OnResultListViewRefresh();

		// Token: 0x06000E33 RID: 3635 RVA: 0x000360D0 File Offset: 0x000342D0
		protected void FindNow()
		{
			string text = this.ValidatingSearchText(this.SearchTextboxText.Trim());
			if (text != null)
			{
				this.ShowHintOnSearchTextBox(SearchDialog.SmallErrorIcon, Strings.SearchDialogInvalidSearchString, text);
				return;
			}
			this.OnFindNow();
		}

		// Token: 0x06000E34 RID: 3636
		protected abstract void OnModifyRecipientPickerScopeToolStripMenuItemClicked();

		// Token: 0x06000E35 RID: 3637
		protected abstract void OnModifyExpectedResultSizeMenuItemClicked();

		// Token: 0x06000E36 RID: 3638
		protected abstract void PerformQuery(object rootId, string searchText);

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x0003610F File Offset: 0x0003430F
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x00036117 File Offset: 0x00034317
		protected Panel ListControlPanel
		{
			get
			{
				return this.listControlPanel;
			}
			set
			{
				this.listControlPanel = value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00036120 File Offset: 0x00034320
		// (set) Token: 0x06000E3A RID: 3642 RVA: 0x00036128 File Offset: 0x00034328
		protected DataListView ResultListView
		{
			get
			{
				return this.resultListView;
			}
			set
			{
				if (this.ResultListView != null)
				{
					this.ResultListView.ItemsForRowsCreated -= this.resultListView_ItemsForRowsCreated;
					this.ResultListView.SelectionChanged -= this.resultListView_SelectionChanged;
					this.ResultListView.RefreshCommand = null;
				}
				this.resultListView = value;
				if (this.ResultListView != null)
				{
					this.enableColumnFiltferingToolStripMenuItem.Command = this.resultListView.ShowFilterCommand;
					this.enableColumnFiltferingToolStripMenuItem.ToolTipText = string.Empty;
					this.addRemoveColumnsToolStripMenuItem.Command = this.ResultListView.ShowColumnPickerCommand;
					this.addRemoveColumnsToolStripMenuItem.ToolTipText = string.Empty;
					this.ResultListView.ItemsForRowsCreated += this.resultListView_ItemsForRowsCreated;
					this.ResultListView.SelectionChanged += this.resultListView_SelectionChanged;
					this.ResultListView.RefreshCommand = this.resultListViewRefreshCommand;
				}
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x00036214 File Offset: 0x00034414
		// (set) Token: 0x06000E3C RID: 3644 RVA: 0x0003621C File Offset: 0x0003441C
		protected DataTableLoader DataTableLoader
		{
			get
			{
				return this.dataTableLoader;
			}
			set
			{
				if (this.DataTableLoader != null)
				{
					this.DataTableLoader.RefreshingChanged -= this.DataTableLoader_RefreshingChanged;
					this.DataTableLoader.RefreshCompleted -= this.DataTableLoader_RefreshCompleted;
					this.DataTableLoader.ProgressChanged -= this.DataTableLoader_ProgressChanged;
				}
				this.dataTableLoader = value;
				if (this.DataTableLoader != null)
				{
					this.DataTableLoader.RefreshingChanged += this.DataTableLoader_RefreshingChanged;
					this.DataTableLoader.RefreshCompleted += this.DataTableLoader_RefreshCompleted;
					this.DataTableLoader.ProgressChanged += this.DataTableLoader_ProgressChanged;
				}
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x000362CA File Offset: 0x000344CA
		// (set) Token: 0x06000E3E RID: 3646 RVA: 0x000362EF File Offset: 0x000344EF
		[DefaultValue(null)]
		protected ScopeSettings ScopeSettings
		{
			get
			{
				if (this.scopeSettings == null)
				{
					this.scopeSettings = new ScopeSettings(ADServerSettingsSingleton.GetInstance().ADServerSettings);
				}
				return this.scopeSettings;
			}
			set
			{
				this.scopeSettings = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x000362F8 File Offset: 0x000344F8
		// (set) Token: 0x06000E40 RID: 3648 RVA: 0x00036300 File Offset: 0x00034500
		protected string Caption
		{
			get
			{
				return this.caption;
			}
			set
			{
				this.caption = value;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x00036309 File Offset: 0x00034509
		// (set) Token: 0x06000E42 RID: 3650 RVA: 0x00036311 File Offset: 0x00034511
		private protected string SearchString
		{
			protected get
			{
				return this.searchString;
			}
			private set
			{
				this.searchString = value;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0003631A File Offset: 0x0003451A
		// (set) Token: 0x06000E44 RID: 3652 RVA: 0x00036327 File Offset: 0x00034527
		protected string SearchTextboxText
		{
			get
			{
				return this.toolStripTextBoxForName.Text;
			}
			set
			{
				this.toolStripTextBoxForName.Text = value;
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00036335 File Offset: 0x00034535
		protected Command FindNowCommand
		{
			get
			{
				return this.findNowCommand;
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x0003633D File Offset: 0x0003453D
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x0003634A File Offset: 0x0003454A
		protected bool SupportSearch
		{
			get
			{
				return this.toolStrip.Visible;
			}
			set
			{
				this.toolStrip.Visible = value;
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00036358 File Offset: 0x00034558
		// (set) Token: 0x06000E49 RID: 3657 RVA: 0x00036365 File Offset: 0x00034565
		protected bool SupportModifyScope
		{
			get
			{
				return this.scopeToolStripMenuItem.Visible;
			}
			set
			{
				this.scopeToolStripMenuItem.Visible = value;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00036373 File Offset: 0x00034573
		// (set) Token: 0x06000E4B RID: 3659 RVA: 0x0003638B File Offset: 0x0003458B
		protected bool SupportModifyResultSize
		{
			get
			{
				return this.viewToolStripMenuItem.DropDownItems.Contains(this.modifyExpectedResultSizeMenuItem);
			}
			set
			{
				if (this.SupportModifyResultSize != value)
				{
					if (value)
					{
						this.viewToolStripMenuItem.DropDownItems.Add(this.modifyExpectedResultSizeMenuItem);
						return;
					}
					this.viewToolStripMenuItem.DropDownItems.Remove(this.modifyExpectedResultSizeMenuItem);
				}
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x000363C7 File Offset: 0x000345C7
		// (set) Token: 0x06000E4D RID: 3661 RVA: 0x000363D4 File Offset: 0x000345D4
		protected bool ShowDialogButtons
		{
			get
			{
				return this.dialogButtonsPanel.Visible;
			}
			set
			{
				this.dialogButtonsPanel.Visible = value;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x000363E2 File Offset: 0x000345E2
		// (set) Token: 0x06000E4F RID: 3663 RVA: 0x000363EF File Offset: 0x000345EF
		protected bool ShowStatus
		{
			get
			{
				return this.loadStatusLabel.Visible;
			}
			set
			{
				this.loadStatusLabel.Visible = value;
				this.selectedCountLabel.Visible = value;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00036409 File Offset: 0x00034609
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x00036416 File Offset: 0x00034616
		protected string ModifyScopeMenuText
		{
			get
			{
				return this.modifyRecipientPickerScopeToolStripMenuItem.Text;
			}
			set
			{
				this.modifyRecipientPickerScopeToolStripMenuItem.Text = value;
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x00036424 File Offset: 0x00034624
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0003642C File Offset: 0x0003462C
		public bool DisableClearButtonForEmptySearchText
		{
			get
			{
				return this.disableClearButtonForEmptySearchText;
			}
			set
			{
				this.disableClearButtonForEmptySearchText = value;
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00036435 File Offset: 0x00034635
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x00036442 File Offset: 0x00034642
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

		// Token: 0x06000E56 RID: 3670 RVA: 0x00036450 File Offset: 0x00034650
		protected void PerformQueryForCurrentSearchString()
		{
			this.lastSearchIsCancelled = false;
			object rootId = (this.SupportModifyScope && this.ScopeSettings.DomainViewEnabled) ? this.ScopeSettings.OrganizationalUnit : null;
			this.PerformQuery(rootId, this.SearchString);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x00036495 File Offset: 0x00034695
		protected void StartNewSearch()
		{
			this.SearchString = this.toolStripTextBoxForName.Text.Trim();
			if (this.ResultListView != null)
			{
				this.ResultListView.Focus();
			}
			this.PerformQueryForCurrentSearchString();
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x000364C8 File Offset: 0x000346C8
		protected bool PromptToModifyRecipientScope(IUIService uiService, ExchangePropertyPageControl scopeControl)
		{
			if (scopeControl == null)
			{
				throw new ArgumentNullException("scopeControl");
			}
			using (PropertyPageDialog propertyPageDialog = new PropertyPageDialog(scopeControl))
			{
				ScopeSettings scopeSettings = new ScopeSettings();
				scopeSettings.CopyFrom(this.ScopeSettings);
				scopeControl.Context = new DataContext(new ExchangeDataHandler());
				scopeControl.Context.DataHandler.DataSource = scopeSettings;
				if (uiService.ShowDialog(propertyPageDialog) == DialogResult.OK && scopeSettings.ObjectState == ObjectState.Changed)
				{
					this.ScopeSettings.CopyChangesFrom(scopeSettings);
					this.UpdateText();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00036568 File Offset: 0x00034768
		protected void ShowHintOnSearchTextBox(Icon icon, string title, string text)
		{
			NativeMethods.EDITBALLOONTIP editballoontip = new NativeMethods.EDITBALLOONTIP(icon, title, text);
			Control control = this.toolStripTextBoxForName.Control;
			UnsafeNativeMethods.SendMessage(new HandleRef(control, control.Handle), 5379, (IntPtr)0, ref editballoontip);
			GC.KeepAlive(editballoontip);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000365BC File Offset: 0x000347BC
		protected void UpdateStatusLabelText()
		{
			string text = Strings.QueryCanceled;
			if (this.lastSearchIsCancelled && this.DataTableLoader.Table.Rows.Count == 0)
			{
				this.loadStatusLabel.Text = text;
				return;
			}
			this.loadStatusLabel.Text = (string.IsNullOrEmpty(this.ResultListView.BindingListViewFilter) ? Strings.ObjectsFound(this.DataTableLoader.Table.Rows.Count) : Strings.ObjectsFoundAndFiltered(this.ResultListView.Items.Count, this.DataTableLoader.Table.Rows.Count - this.ResultListView.Items.Count));
			if (this.lastSearchIsCancelled)
			{
				ToolStripStatusLabel toolStripStatusLabel = this.loadStatusLabel;
				toolStripStatusLabel.Text = toolStripStatusLabel.Text + " " + text;
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0003669D File Offset: 0x0003489D
		protected void ActiveSearchTextbox()
		{
			this.toolStripTextBoxForName.Focus();
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000366AA File Offset: 0x000348AA
		protected void RemoveColumnFilteringMenu()
		{
			this.viewToolStripMenuItem.DropDownItems.Remove(this.enableColumnFiltferingToolStripMenuItem);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000366C2 File Offset: 0x000348C2
		private void stopCommand_Execute(object sender, EventArgs e)
		{
			this.OnStop();
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000366CA File Offset: 0x000348CA
		private void DataTableLoader_ProgressChanged(object sender, RefreshProgressChangedEventArgs e)
		{
			this.UpdateStatusLabelText();
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000366D2 File Offset: 0x000348D2
		private void DataTableLoader_RefreshingChanged(object sender, EventArgs e)
		{
			this.UpdateControls(this.DataTableLoader.Refreshing);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000366E8 File Offset: 0x000348E8
		private void DataTableLoader_RefreshCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (!this.DataTableLoader.Refreshing)
			{
				if (e.Error == null && e.Cancelled)
				{
					this.lastSearchIsCancelled = true;
					this.UpdateStatusLabelText();
				}
				if (this.ResultListView.SelectedIndices.Count == 0 && this.ResultListView.Items.Count > 0)
				{
					this.ResultListView.Items[0].Selected = true;
					this.ResultListView.Items[0].Focused = true;
				}
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00036774 File Offset: 0x00034974
		private void UpdateControls(bool isSearching)
		{
			this.loadProgressBar.Visible = isSearching;
			if (isSearching)
			{
				this.loadStatusLabel.Text = Strings.Searching;
				this.toolStripButtonClearOrStop.Command = this.stopCommand;
				return;
			}
			this.toolStripButtonClearOrStop.Command = this.clearCommand;
			if (this.DisableClearButtonForEmptySearchText)
			{
				this.clearCommand.Enabled = !string.IsNullOrEmpty(this.toolStripTextBoxForName.Text);
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000367EE File Offset: 0x000349EE
		private void UpdateText()
		{
			this.Text = (this.SupportModifyScope ? Strings.ObjectPickerFormTextWithScope(this.Caption, this.ScopeSettings.ScopingDescription) : this.Caption);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00036824 File Offset: 0x00034A24
		private void resultListView_SelectionChanged(object sender, EventArgs e)
		{
			this.okButton.Enabled = (this.ResultListView.SelectedIndices.Count > 0);
			this.selectedCountLabel.Text = Strings.ObjectsSelected(this.ResultListView.SelectedIndices.Count);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00036874 File Offset: 0x00034A74
		private void resultListView_ItemsForRowsCreated(object sender, EventArgs e)
		{
			this.UpdateStatusLabelText();
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003687C File Offset: 0x00034A7C
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			this.UpdateText();
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003688B File Offset: 0x00034A8B
		protected override void OnClosing(CancelEventArgs e)
		{
			this.stopCommand.Invoke();
			base.OnClosing(e);
		}

		// Token: 0x040005C1 RID: 1473
		private Command findNowCommand;

		// Token: 0x040005C2 RID: 1474
		private Command clearCommand;

		// Token: 0x040005C3 RID: 1475
		private Command stopCommand;

		// Token: 0x040005C4 RID: 1476
		private Command resultListViewRefreshCommand;

		// Token: 0x040005CF RID: 1487
		private bool lastSearchIsCancelled;

		// Token: 0x040005D0 RID: 1488
		private static ValidateLengthAttribute searchTextConstraint;

		// Token: 0x040005D1 RID: 1489
		private static readonly Icon SmallErrorIcon = new Icon(Icons.Error, new Size(16, 16));

		// Token: 0x040005D2 RID: 1490
		private DataListView resultListView;

		// Token: 0x040005D3 RID: 1491
		private DataTableLoader dataTableLoader;

		// Token: 0x040005D4 RID: 1492
		private ScopeSettings scopeSettings;

		// Token: 0x040005D5 RID: 1493
		private string caption;

		// Token: 0x040005D6 RID: 1494
		private string searchString;

		// Token: 0x040005D7 RID: 1495
		private bool disableClearButtonForEmptySearchText;
	}
}
