using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200022D RID: 557
	public sealed partial class ObjectPickerForm : SearchDialog, ISelectedObjectsProvider
	{
		// Token: 0x060019AB RID: 6571 RVA: 0x0006F518 File Offset: 0x0006D718
		public ObjectPickerForm()
		{
			base.Name = "ObjectPickerForm";
			base.SupportModifyResultSize = false;
			DataListView dataListView = new DataListView();
			dataListView.AutoGenerateColumns = false;
			dataListView.Cursor = Cursors.Default;
			dataListView.Dock = DockStyle.Fill;
			dataListView.Location = new Point(0, 0);
			dataListView.Name = "resultListView";
			dataListView.Size = new Size(498, 333);
			dataListView.TabIndex = 2;
			dataListView.VirtualMode = true;
			dataListView.ItemActivate += this.resultListView_ItemActivate;
			base.ListControlPanel.Controls.Add(dataListView);
			base.ResultListView = dataListView;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0006F5C1 File Offset: 0x0006D7C1
		public ObjectPickerForm(ObjectPicker objectPicker) : this()
		{
			this.ObjectPicker = objectPicker;
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x0006F5D0 File Offset: 0x0006D7D0
		protected override void OnFindNow()
		{
			base.StartNewSearch();
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0006F5D8 File Offset: 0x0006D7D8
		protected override string ValidatingSearchText(string searchText)
		{
			if (!string.IsNullOrEmpty(searchText))
			{
				return base.ValidatingSearchText(searchText);
			}
			return null;
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0006F5EB File Offset: 0x0006D7EB
		protected override void OnStop()
		{
			base.DataTableLoader.CancelRefresh();
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0006F5F8 File Offset: 0x0006D7F8
		protected override void OnClear()
		{
			base.SearchTextboxText = string.Empty;
			base.ResultListView.RefreshCommand.Invoke();
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0006F615 File Offset: 0x0006D815
		protected override void PerformQuery(object rootId, string searchText)
		{
			this.ObjectPicker.PerformQuery(rootId, searchText);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x0006F624 File Offset: 0x0006D824
		protected override bool IsANRSearch()
		{
			if (this.ObjectPicker.ObjectPickerProfile != null)
			{
				foreach (AbstractDataTableFiller abstractDataTableFiller in this.ObjectPicker.ObjectPickerProfile.TableFillers)
				{
					if (abstractDataTableFiller.CommandBuilder.SearchType == null)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0006F698 File Offset: 0x0006D898
		protected override void OnResultListViewRefresh()
		{
			base.FindNow();
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x0006F6A0 File Offset: 0x0006D8A0
		protected override void OnModifyRecipientPickerScopeToolStripMenuItemClicked()
		{
			using (ModifyScopeSettingsControl modifyScopeSettingsControl = new ModifyScopeSettingsControl(this.ObjectPicker))
			{
				if (base.PromptToModifyRecipientScope(base.ShellUI, modifyScopeSettingsControl))
				{
					base.ResultListView.RefreshCommand.Invoke();
				}
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x0006F6F4 File Offset: 0x0006D8F4
		protected override void OnModifyExpectedResultSizeMenuItemClicked()
		{
			throw new NotImplementedException("The method or operation is not implemented.");
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0006F700 File Offset: 0x0006D900
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x0006F708 File Offset: 0x0006D908
		[DefaultValue(null)]
		public ObjectPicker ObjectPicker
		{
			get
			{
				return this.objectPicker;
			}
			set
			{
				if (this.ObjectPicker != value)
				{
					if (this.ObjectPicker != null)
					{
						if (base.DataTableLoader != null)
						{
							base.DataTableLoader.RefreshCompleted -= this.DataTableLoader_RefreshCompleted;
							base.DataTableLoader = null;
						}
						WinformsHelper.SetDataSource(base.ResultListView, null, null);
						base.ResultListView.DataSourceRefresher = null;
						this.resultsDataTable = null;
						this.CleanupColumns();
						base.Caption = null;
						base.ScopeSettings = null;
						base.ResultListView.SortProperty = null;
						base.ResultListView.IconLibrary = null;
						base.ResultListView.ImagePropertyName = null;
					}
					this.objectPicker = value;
					if (this.ObjectPicker != null)
					{
						base.Caption = this.ObjectPicker.Caption;
						base.ScopeSettings = this.ObjectPicker.ScopeSettings;
						base.ResultListView.MultiSelect = this.ObjectPicker.AllowMultiSelect;
						base.ResultListView.NoResultsLabelText = this.ObjectPicker.NoResultsLabelText;
						base.ResultListView.SelectionNameProperty = this.ObjectPicker.NameProperty;
						if (string.IsNullOrEmpty(this.ObjectPicker.IdentityProperty))
						{
							base.ResultListView.IdentityProperty = "Identity";
						}
						else
						{
							base.ResultListView.IdentityProperty = this.ObjectPicker.IdentityProperty;
						}
						if (!string.IsNullOrEmpty(this.ObjectPicker.DefaultSortProperty))
						{
							base.ResultListView.SortProperty = this.ObjectPicker.DefaultSortProperty;
						}
						if (this.ObjectPicker.ShowListItemIcon)
						{
							base.ResultListView.IconLibrary = ObjectPicker.ObjectClassIconLibrary;
							base.ResultListView.ImagePropertyName = this.ObjectPicker.ImageProperty;
						}
						base.SupportSearch = this.ObjectPicker.SupportSearch;
						base.SupportModifyScope = this.ObjectPicker.SupportModifyScope;
						base.DisableClearButtonForEmptySearchText = true;
						this.resultsDataTable = this.ObjectPicker.DataTableLoader.Table;
						this.SetupColumns();
						WinformsHelper.SetDataSource(base.ResultListView, (this.ObjectPicker.ObjectPickerProfile == null) ? null : this.ObjectPicker.ObjectPickerProfile.UIPresentationProfile, this.ObjectPicker.DataTableLoader);
						base.ResultListView.DataSourceRefresher = this.ObjectPicker.DataTableLoader;
						base.DataTableLoader = this.ObjectPicker.DataTableLoader;
						base.DataTableLoader.RefreshCompleted += this.DataTableLoader_RefreshCompleted;
					}
				}
			}
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x0006F96C File Offset: 0x0006DB6C
		private void DataTableLoader_RefreshCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (ExceptionHelper.IsUICriticalException(e.Error))
			{
				throw new ObjectPickerException(e.Error.Message, e.Error);
			}
			if (!this.ObjectPicker.DataTableLoader.Refreshing && e.Error != null)
			{
				string message;
				if (e.Error is SearchTooLargeException)
				{
					message = Strings.SearchTooLargeRefineYourSearch(e.Error.Message);
				}
				else if (ExceptionHelper.IsWellknownExceptionFromServer(e.Error.InnerException))
				{
					message = e.Error.InnerException.Message;
				}
				else
				{
					message = e.Error.Message;
				}
				base.ShowError(message);
			}
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x0006FA28 File Offset: 0x0006DC28
		private void SetupColumns()
		{
			this.columnHeaders = this.ObjectPicker.CreateColumnHeaders();
			if (this.ObjectPicker.ObjectPickerProfile == null)
			{
				DataColumnCollection columns = this.ObjectPicker.DataTableLoader.Table.Columns;
				this.dataColumnDictionary = new Dictionary<string, DataColumn>(columns.Count);
				foreach (object obj in columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					this.dataColumnDictionary.Add(dataColumn.ColumnName, dataColumn);
				}
				foreach (ExchangeColumnHeader exchangeColumnHeader in this.columnHeaders)
				{
					exchangeColumnHeader.VisibleChanged += this.header_VisibleChanged;
					this.header_VisibleChanged(exchangeColumnHeader, EventArgs.Empty);
				}
				base.ResultListView.ColumnsChanged += delegate(object param0, EventArgs param1)
				{
					base.ResultListView.RefreshCommand.Invoke();
				};
			}
			base.ResultListView.Columns.Clear();
			base.ResultListView.AvailableColumns.AddRange(this.columnHeaders);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x0006FB5C File Offset: 0x0006DD5C
		private void CleanupColumns()
		{
			if (this.columnHeaders != null)
			{
				foreach (ExchangeColumnHeader exchangeColumnHeader in this.columnHeaders)
				{
					exchangeColumnHeader.VisibleChanged -= this.header_VisibleChanged;
				}
			}
			this.columnHeaders = null;
			this.dataColumnDictionary = null;
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x0006FBAC File Offset: 0x0006DDAC
		private void header_VisibleChanged(object sender, EventArgs e)
		{
			ExchangeColumnHeader exchangeColumnHeader = (ExchangeColumnHeader)sender;
			ExTraceGlobals.ProgramFlowTracer.TraceDebug<ObjectPickerForm, string, bool>(0L, "-->ObjectPickerForm.header_VisibleChanged:{0}. Column name = {1}, Column visible = {2}).", this, exchangeColumnHeader.Name, exchangeColumnHeader.Visible);
			if (exchangeColumnHeader.Visible && !this.resultsDataTable.Columns.Contains(exchangeColumnHeader.Name) && this.dataColumnDictionary.ContainsKey(exchangeColumnHeader.Name))
			{
				this.resultsDataTable.Columns.Add(this.dataColumnDictionary[exchangeColumnHeader.Name]);
				ExTraceGlobals.DataFlowTracer.Information(0L, "*--ObjectPickerForm.header_VisibleChanged:{0}. Data column {1} is added, column type:{2}, column expression:{3}.", new object[]
				{
					this,
					this.dataColumnDictionary[exchangeColumnHeader.Name].ColumnName,
					this.dataColumnDictionary[exchangeColumnHeader.Name].DataType,
					this.dataColumnDictionary[exchangeColumnHeader.Name].Expression
				});
			}
			if (!exchangeColumnHeader.Visible && (string.IsNullOrEmpty(base.ResultListView.SortProperty) || base.ResultListView.SortProperty != exchangeColumnHeader.Name) && this.resultsDataTable.Columns.Contains(exchangeColumnHeader.Name))
			{
				DataColumn column = this.resultsDataTable.Columns[exchangeColumnHeader.Name];
				if (!ObjectPicker.GetIsRequiredDataColumnFlag(column))
				{
					this.resultsDataTable.Columns.Remove(exchangeColumnHeader.Name);
					ExTraceGlobals.DataFlowTracer.Information<ObjectPickerForm, string>(0L, "*--ObjectPickerForm.header_VisibleChanged:{0}. Data column {1} is removed.", this, exchangeColumnHeader.Name);
				}
			}
			ExTraceGlobals.ProgramFlowTracer.TraceDebug<ObjectPickerForm>(0L, "<--ObjectPickerForm.header_VisibleChanged:{0}.", this);
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x0006FD4F File Offset: 0x0006DF4F
		public DataTable SelectedObjects
		{
			get
			{
				return this.selectedObjects;
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0006FD58 File Offset: 0x0006DF58
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			this.selectedObjects = this.resultsDataTable.Clone();
			foreach (object obj in base.ResultListView.SelectedObjects)
			{
				DataRowView dataRowView = (DataRowView)obj;
				this.selectedObjects.Rows.Add(dataRowView.Row.ItemArray);
			}
			this.resultsDataTable.Clear();
			if (this.ObjectPicker.ObjectPickerProfile == null)
			{
				using (Dictionary<string, DataColumn>.ValueCollection.Enumerator enumerator2 = this.dataColumnDictionary.Values.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						DataColumn dataColumn = enumerator2.Current;
						if (!this.resultsDataTable.Columns.Contains(dataColumn.ColumnName))
						{
							this.resultsDataTable.Columns.Add(dataColumn);
						}
					}
					return;
				}
			}
			this.ObjectPicker.ObjectPickerProfile.SearchText = string.Empty;
			this.ObjectPicker.ObjectPickerProfile.Scope = null;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x0006FE8C File Offset: 0x0006E08C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			base.FindNowCommand.Invoke();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x0006FEA0 File Offset: 0x0006E0A0
		private void resultListView_ItemActivate(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x0006FEBC File Offset: 0x0006E0BC
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled)
			{
				ExchangeHelpService.ShowHelpFromHelpTopicId(this, this.objectPicker.HelpTopic);
				hevent.Handled = true;
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x0400099A RID: 2458
		private ObjectPicker objectPicker;

		// Token: 0x0400099B RID: 2459
		private DataTable resultsDataTable;

		// Token: 0x0400099C RID: 2460
		private DataTable selectedObjects;

		// Token: 0x0400099D RID: 2461
		private ExchangeColumnHeader[] columnHeaders;

		// Token: 0x0400099E RID: 2462
		private Dictionary<string, DataColumn> dataColumnDictionary;
	}
}
