using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B0 RID: 432
	public class DataListViewResultPane : ResultPane, IExchangeFormOwner
	{
		// Token: 0x06001134 RID: 4404 RVA: 0x00044113 File Offset: 0x00042313
		public DataListViewResultPane() : this(null, null)
		{
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0004411D File Offset: 0x0004231D
		public DataListViewResultPane(IResultsLoaderConfiguration config) : this((config != null) ? config.BuildResultsLoaderProfile() : null, null)
		{
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x00044132 File Offset: 0x00042332
		public DataListViewResultPane(DataTableLoader loader) : this((loader != null) ? loader.ResultsLoaderProfile : null, loader)
		{
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00044147 File Offset: 0x00042347
		public DataListViewResultPane(ObjectPickerProfileLoader profileLoader, string profileName) : this(profileLoader.GetProfile(profileName))
		{
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00044156 File Offset: 0x00042356
		public DataListViewResultPane(ResultsLoaderProfile profile) : this(profile, null)
		{
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00044160 File Offset: 0x00042360
		protected DataListViewResultPane(ResultsLoaderProfile profile, DataTableLoader loader) : base(profile, loader)
		{
			this.listContextMenu = base.ContextMenu;
			base.ContextMenu = null;
			base.Name = "DataListViewResultPane";
			this.saveDefaultFilterCommand = new Command();
			this.saveDefaultFilterCommand.Description = LocalizedString.Empty;
			this.saveDefaultFilterCommand.Name = "commandSaveFilter";
			this.saveDefaultFilterCommand.Text = Strings.SaveAsDefaultFilterCommandText;
			this.saveDefaultFilterCommand.Execute += this.saveDefaultFilterCommand_Execute;
			base.ViewModeCommands.AddRange(new Command[]
			{
				CommandLoggingDialog.GetCommandLoggingCommand(),
				Command.CreateSeparator()
			});
			base.ViewModeCommands.Add(Theme.VisualEffectsCommands);
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00044230 File Offset: 0x00042430
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this.ObjectList != null)
				{
					WinformsHelper.SetDataSource(this.ObjectList, null, null);
				}
				else if (this.ListControl != null)
				{
					WinformsHelper.SetDataSource(this.ListControl, null, null);
				}
				this.FilterControl = null;
				this.ListControl = null;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00044284 File Offset: 0x00042484
		protected override void OnSetActive(EventArgs e)
		{
			if (this.RefreshableDataSource == null && base.ResultsLoaderProfile != null)
			{
				this.RefreshableDataSource = new DataTableLoader(base.ResultsLoaderProfile);
			}
			if (base.DataTableLoader != null && this.ListControl.DataSource == null)
			{
				if (this.ObjectList != null)
				{
					WinformsHelper.SetDataSource(this.ObjectList, (base.ResultsLoaderProfile == null) ? null : base.ResultsLoaderProfile.UIPresentationProfile, base.DataTableLoader);
				}
				else if (this.ListControl != null)
				{
					WinformsHelper.SetDataSource(this.ListControl, (base.ResultsLoaderProfile == null) ? null : base.ResultsLoaderProfile.UIPresentationProfile, base.DataTableLoader);
				}
			}
			base.OnSetActive(e);
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00044330 File Offset: 0x00042530
		private ObjectList ObjectList
		{
			get
			{
				if (base.Controls.Count <= 0)
				{
					return null;
				}
				return base.Controls[0] as ObjectList;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00044353 File Offset: 0x00042553
		// (set) Token: 0x0600113E RID: 4414 RVA: 0x0004436C File Offset: 0x0004256C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataListView ListControl
		{
			get
			{
				return this.listControl;
			}
			set
			{
				if (this.listControl != null)
				{
					if (this.SaveSettings)
					{
						this.PrivateSettings.SaveDataListViewSettings(this.listControl);
					}
					this.listControl.ColumnWidthChanged -= new ColumnWidthChangedEventHandler(this.MakeModified);
					this.listControl.ColumnReordered -= new ColumnReorderedEventHandler(this.MakeModified);
					this.listControl.ColumnClick -= new ColumnClickEventHandler(this.MakeModified);
					base.Components.Remove(this.listControl);
					this.listControl.SelectionChanged -= this.OnListSelectionChanged;
					this.listControl.ItemsForRowsCreated -= this.listControl_ItemsForRowsCreated;
					this.listControl.ShowSelectionPropertiesCommand = null;
					this.listControl.RefreshCommand = null;
					this.listControl.DeleteSelectionCommand = null;
					this.listControl.ContextMenu = null;
					this.listControl.DataSourceRefresher = null;
					base.ViewModeCommands.Remove(this.listControl.ShowColumnPickerCommand);
				}
				this.listControl = value;
				if (this.listControl != null)
				{
					if (base.ResultsLoaderProfile != null)
					{
						this.ListControl.AutoGenerateColumns = base.ResultsLoaderProfile.AutoGenerateColumns;
						this.LoadAvailableColumns();
						this.ListControl.SortProperty = base.ResultsLoaderProfile.SortProperty;
						this.ListControl.ImagePropertyName = base.ResultsLoaderProfile.ImageProperty;
						this.ListControl.SelectionNameProperty = base.ResultsLoaderProfile.NameProperty;
						this.Text = base.ResultsLoaderProfile.DisplayName;
						this.ListControl.IdentityProperty = base.ResultsLoaderProfile.DistinguishIdentity;
						this.ListControl.MultiSelect = base.ResultsLoaderProfile.MultiSelect;
					}
					this.PrivateSettings.LoadDataListViewSettings(this.listControl);
					this.listControl.ColumnWidthChanged += new ColumnWidthChangedEventHandler(this.MakeModified);
					this.listControl.ColumnReordered += new ColumnReorderedEventHandler(this.MakeModified);
					this.listControl.ColumnClick += new ColumnClickEventHandler(this.MakeModified);
					base.Components.Add(this.listControl);
					this.listControl.SelectionChanged += this.OnListSelectionChanged;
					this.listControl.ItemsForRowsCreated += this.listControl_ItemsForRowsCreated;
					this.listControl.ShowSelectionPropertiesCommand = new Command();
					this.listControl.ShowSelectionPropertiesCommand.Execute += delegate(object param0, EventArgs param1)
					{
						base.InvokeCurrentShowSelectionPropertiesCommand();
					};
					this.listControl.RefreshCommand = base.RefreshCommand;
					this.listControl.DeleteSelectionCommand = new Command();
					this.listControl.DeleteSelectionCommand.Execute += delegate(object param0, EventArgs param1)
					{
						base.InvokeCurrentDeleteSelectionCommand();
					};
					this.listControl.ContextMenu = this.listContextMenu;
					this.listControl.DataSourceRefresher = this.RefreshableDataSource;
					this.listControl.VirtualMode = this.listControl.SupportsVirtualMode;
					if ("" == this.ListControl.IdentityProperty)
					{
						this.ListControl.IdentityProperty = "Identity";
					}
					if ("" == this.ListControl.SelectionNameProperty)
					{
						this.ListControl.SelectionNameProperty = "Name";
					}
					if ("" == this.ListControl.SortProperty)
					{
						this.ListControl.SortProperty = "Name";
					}
					this.listControl.NoResultsLabelText = Strings.NoItemsToShow;
					this.listControl.DrawLockedString = !this.HasPermission();
					this.listControl.DrawLockedIcon = this.listControl.DrawLockedString;
					base.ViewModeCommands.Insert(2, this.listControl.ShowColumnPickerCommand);
					this.InitializeExportListCommands();
				}
			}
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00044740 File Offset: 0x00042940
		protected virtual void LoadAvailableColumns()
		{
			if (!base.ResultsLoaderProfile.AutoGenerateColumns)
			{
				this.ListControl.AvailableColumns.Clear();
				this.ListControl.AvailableColumns.AddRange(base.ResultsLoaderProfile.CreateColumnHeaders());
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x000447C4 File Offset: 0x000429C4
		private void InitializeExportListCommands()
		{
			if (this.exportListCommandSeparator == null)
			{
				this.exportListCommandSeparator = Command.CreateSeparator();
			}
			if (this.exportListCommand == null)
			{
				this.exportListCommand = new Command();
				this.exportListCommand.Text = Strings.ExportListDefaultCommandText;
				this.exportListCommand.Icon = Icons.ExportList;
				this.exportListCommand.Name = "ExportListCommand";
				this.exportListCommand.Execute += this.ExportListCommand_Execute;
			}
			base.ExportListCommands.AddRange(new Command[]
			{
				this.exportListCommandSeparator,
				this.exportListCommand
			});
			if (this.ListControl is DataTreeListView)
			{
				if (this.expandAllCommand == null)
				{
					this.expandAllCommand = new Command();
					this.expandAllCommand.Text = Strings.ExpandAllDefaultCommandText;
					this.expandAllCommand.Name = "ExpandAllCommand";
					this.expandAllCommand.Icon = Icons.ExpandAll;
					this.expandAllCommand.Execute += delegate(object param0, EventArgs param1)
					{
						DataTreeListView dataTreeListView = this.ListControl as DataTreeListView;
						if (dataTreeListView != null)
						{
							dataTreeListView.ExpandAll();
						}
					};
				}
				base.ExportListCommands.Insert(base.ExportListCommands.IndexOf(this.exportListCommand), this.expandAllCommand);
				if (this.collapseRootsCommand == null)
				{
					this.collapseRootsCommand = new Command();
					this.collapseRootsCommand.Text = Strings.CollapseAllDefaultCommandText;
					this.collapseRootsCommand.Name = "CollapseAllCommand";
					this.collapseRootsCommand.Icon = Icons.CollapseAll;
					this.collapseRootsCommand.Execute += delegate(object param0, EventArgs param1)
					{
						DataTreeListView dataTreeListView = this.ListControl as DataTreeListView;
						if (dataTreeListView != null)
						{
							dataTreeListView.CollapseRootItems();
						}
					};
				}
				base.ExportListCommands.Insert(base.ExportListCommands.IndexOf(this.exportListCommand), this.collapseRootsCommand);
			}
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00044988 File Offset: 0x00042B88
		protected void SetExpandAllCommandText(LocalizedString text)
		{
			this.expandAllCommand.Text = text;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0004499B File Offset: 0x00042B9B
		protected void SetCollapseRootsCommandText(LocalizedString text)
		{
			this.collapseRootsCommand.Text = text;
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000449AE File Offset: 0x00042BAE
		public override string SelectedObjectDetailsType
		{
			get
			{
				if (this.ListControl == null)
				{
					return base.SelectedObjectDetailsType;
				}
				return this.ListControl.SelectedObjectDetailsType;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001144 RID: 4420 RVA: 0x000449CA File Offset: 0x00042BCA
		public new IList SelectedObjects
		{
			get
			{
				if (this.ListControl == null)
				{
					return new object[0];
				}
				return this.ListControl.SelectedObjects;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x000449E6 File Offset: 0x00042BE6
		public new bool HasSelection
		{
			get
			{
				return this.SelectedObjects.Count > 0;
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x000449F6 File Offset: 0x00042BF6
		public new bool HasSingleSelection
		{
			get
			{
				return this.SelectedObjects.Count == 1;
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x00044A06 File Offset: 0x00042C06
		public new bool HasMultiSelection
		{
			get
			{
				return this.SelectedObjects.Count > 1;
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06001148 RID: 4424 RVA: 0x00044A16 File Offset: 0x00042C16
		public object SelectedObject
		{
			get
			{
				if (this.SelectedObjects.Count > 0)
				{
					return this.SelectedObjects[0];
				}
				return null;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x00044A34 File Offset: 0x00042C34
		public DataRowView SelectedDataRowView
		{
			get
			{
				return this.SelectedObject as DataRowView;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x00044A44 File Offset: 0x00042C44
		public DataRow SelectedDataRow
		{
			get
			{
				DataRowView selectedDataRowView = this.SelectedDataRowView;
				if (selectedDataRowView == null)
				{
					return null;
				}
				return selectedDataRowView.Row;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600114B RID: 4427 RVA: 0x00044A63 File Offset: 0x00042C63
		public Icon SelectedIcon
		{
			get
			{
				if (this.ListControl == null)
				{
					return null;
				}
				return this.ListControl.SelectedItemIcon;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x00044A7A File Offset: 0x00042C7A
		public object SelectedIdentity
		{
			get
			{
				if (this.ListControl == null)
				{
					return null;
				}
				return this.ListControl.SelectedIdentity;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600114D RID: 4429 RVA: 0x00044A91 File Offset: 0x00042C91
		public IList SelectedIdentities
		{
			get
			{
				if (this.ListControl == null)
				{
					return new object[0];
				}
				return this.ListControl.SelectedIdentities;
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00044AAD File Offset: 0x00042CAD
		internal WorkUnit[] GetSelectedWorkUnits()
		{
			return this.ListControl.GetSelectedWorkUnits();
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x0600114F RID: 4431 RVA: 0x00044ABA File Offset: 0x00042CBA
		protected virtual int MultiRowRefreshThreshold
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00044AC0 File Offset: 0x00042CC0
		public override IRefreshable GetSelectionRefreshObjects()
		{
			object[] array = this.SelectedIdentities.OfType<object>().ToArray<object>();
			if (array.Length <= this.MultiRowRefreshThreshold)
			{
				return new MultiRowRefreshObject(array, (ISupportFastRefresh)this.RefreshableDataSource);
			}
			return this.RefreshableDataSource;
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x00044B04 File Offset: 0x00042D04
		private void listControl_ItemsForRowsCreated(object sender, EventArgs e)
		{
			int itemsCountDisplayedAsStatus = this.GetItemsCountDisplayedAsStatus();
			if (itemsCountDisplayedAsStatus == 1)
			{
				base.Status = Strings.ItemCountSingle;
				return;
			}
			base.Status = Strings.ItemCountPlural(itemsCountDisplayedAsStatus);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00044B3E File Offset: 0x00042D3E
		protected virtual int GetItemsCountDisplayedAsStatus()
		{
			return this.ListControl.TotalItemsCount;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x00044B4B File Offset: 0x00042D4B
		private void MakeModified(object sender, EventArgs e)
		{
			base.IsModified = true;
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x00044B54 File Offset: 0x00042D54
		public Command SaveDefaultFilterCommand
		{
			get
			{
				return this.saveDefaultFilterCommand;
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00044B5C File Offset: 0x00042D5C
		private void saveDefaultFilterCommand_Execute(object sender, EventArgs e)
		{
			if (this.FilterControl.IsApplied)
			{
				base.IsModified = true;
				this.PrivateSettings.FilterExpression = this.FilterControl.PersistedExpression;
				return;
			}
			base.ShellUI.ShowError(Strings.ErrorHitApplyBeforeSaveFilter);
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x00044BA9 File Offset: 0x00042DA9
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x00044BB4 File Offset: 0x00042DB4
		[DefaultValue(null)]
		public FilterControl FilterControl
		{
			get
			{
				return this.filterControl;
			}
			set
			{
				if (this.FilterControl != value)
				{
					this.filterControl = value;
					if (this.FilterControl != null && base.ResultsLoaderProfile != null && base.ResultsLoaderProfile.UIPresentationProfile.FilterableProperties.Count > 0)
					{
						this.FilterControl.PropertiesToFilter.Clear();
						foreach (FilterablePropertyDescription item in base.ResultsLoaderProfile.UIPresentationProfile.FilterableProperties.Values)
						{
							this.FilterControl.PropertiesToFilter.Add(item);
						}
						this.FilterControl.ObjectSchema = base.ResultsLoaderProfile.FilterObjectSchema;
					}
					if (this.FilterControl == null)
					{
						if (base.ViewModeCommands.Contains(this.SaveDefaultFilterCommand))
						{
							base.ViewModeCommands.Remove(this.SaveDefaultFilterCommand);
						}
					}
					else
					{
						if (!base.ViewModeCommands.Contains(this.SaveDefaultFilterCommand))
						{
							base.ViewModeCommands.Add(this.SaveDefaultFilterCommand);
						}
						this.FilterControl.PersistedExpression = this.PrivateSettings.FilterExpression;
					}
					this.OnFilterControlChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x00044CFC File Offset: 0x00042EFC
		protected virtual void OnFilterControlChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataListViewResultPane.EventFilterControlChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400006D RID: 109
		// (add) Token: 0x06001159 RID: 4441 RVA: 0x00044D2A File Offset: 0x00042F2A
		// (remove) Token: 0x0600115A RID: 4442 RVA: 0x00044D3D File Offset: 0x00042F3D
		public event EventHandler FilterControlChanged
		{
			add
			{
				base.Events.AddHandler(DataListViewResultPane.EventFilterControlChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataListViewResultPane.EventFilterControlChanged, value);
			}
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x00044D50 File Offset: 0x00042F50
		private void OnListSelectionChanged(object sender, EventArgs e)
		{
			string text = this.ListControl.SelectionName;
			if (string.IsNullOrEmpty(text))
			{
				text = Strings.SelectionNameDoesNotExist(this.ListControl.Columns[this.ListControl.SelectionNameProperty].Text);
			}
			base.UpdateSelection(this.SelectedIdentities, text, null);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00044DAA File Offset: 0x00042FAA
		protected override void OnRefreshableDataSourceChanged(EventArgs e)
		{
			if (this.ListControl != null)
			{
				this.ListControl.DataSourceRefresher = this.RefreshableDataSource;
			}
			base.OnRefreshableDataSourceChanged(e);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00044DCC File Offset: 0x00042FCC
		protected override ExchangeSettings CreatePrivateSettings(IComponent owner)
		{
			return new DataListViewSettings(owner);
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x00044DD4 File Offset: 0x00042FD4
		public new DataListViewSettings PrivateSettings
		{
			get
			{
				return (DataListViewSettings)base.PrivateSettings;
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00044DE4 File Offset: 0x00042FE4
		public override void LoadComponentSettings()
		{
			base.LoadComponentSettings();
			if (this.ListControl != null)
			{
				this.PrivateSettings.LoadDataListViewSettings(this.ListControl);
			}
			if (this.FilterControl != null && this.PrivateSettings.FilterExpression != null)
			{
				this.FilterControl.PersistedExpression = this.PrivateSettings.FilterExpression;
			}
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00044E3B File Offset: 0x0004303B
		public override void ResetComponentSettings()
		{
			base.ResetComponentSettings();
			if (this.ListControl != null)
			{
				this.PrivateSettings.LoadDataListViewSettings(this.ListControl);
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x00044E5C File Offset: 0x0004305C
		public override void SaveComponentSettings()
		{
			if (this.ListControl != null)
			{
				this.PrivateSettings.SaveDataListViewSettings(this.ListControl);
			}
			base.SaveComponentSettings();
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00044E80 File Offset: 0x00043080
		void IExchangeFormOwner.OnExchangeFormClosed(ExchangeForm formToClose)
		{
			WizardForm wizardForm = formToClose as WizardForm;
			if (wizardForm != null && wizardForm.WizardPages.Count > 0 && this.ListControl != null)
			{
				WizardPage wizardPage = wizardForm.WizardPages[wizardForm.WizardPages.Count - 1];
				if (wizardPage != null && wizardPage.Context != null && wizardPage.Context.DataHandler != null && wizardPage.Context.DataHandler.SavedResults != null && wizardPage.Context.DataHandler.SavedResults.Count == 1)
				{
					this.ListControl.SelectItemBySpecifiedIdentity(this.GetIdentityFromObject(wizardPage.Context.DataHandler.SavedResults[0]), false);
				}
			}
			if (formToClose != null)
			{
				this.childrenFormsList.Remove(formToClose);
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x00044F49 File Offset: 0x00043149
		void IExchangeFormOwner.OnExchangeFormLoad(ExchangeForm form)
		{
			if (form != null)
			{
				this.childrenFormsList.Add(form);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x00044F5C File Offset: 0x0004315C
		protected virtual object GetIdentityFromObject(object newObject)
		{
			object obj = null;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(newObject);
			PropertyDescriptor propertyDescriptor = null;
			if (!string.IsNullOrEmpty(this.ListControl.IdentityProperty))
			{
				propertyDescriptor = properties[this.ListControl.IdentityProperty];
			}
			propertyDescriptor = (propertyDescriptor ?? properties["Identity"]);
			if (propertyDescriptor != null)
			{
				obj = propertyDescriptor.GetValue(newObject);
				ADObjectId adobjectId = obj as ADObjectId;
				if (adobjectId != null)
				{
					obj = new object[]
					{
						adobjectId,
						adobjectId.DistinguishedName,
						adobjectId.ToString(),
						adobjectId.ObjectGuid.ToString()
					};
				}
				else if (obj is Guid)
				{
					obj = ((Guid)obj).ToString();
				}
			}
			return obj;
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0004501C File Offset: 0x0004321C
		protected virtual object GetObjectRefreshCategory(object obj)
		{
			return null;
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00045020 File Offset: 0x00043220
		public virtual void RefreshObject(object obj)
		{
			this.RefreshObjects(new object[]
			{
				obj
			});
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x00045040 File Offset: 0x00043240
		public virtual void RefreshObjects(IList<object> list)
		{
			IProgress progress = base.CreateProgress(base.RefreshCommand.Text);
			IList<object> list2;
			IDictionary<object, IList<object>> dictionary = this.GroupObjectsByCategory(list, out list2);
			if (dictionary.Count > 0)
			{
				this.CreateRefreshableObjectForObjects(dictionary).Refresh(progress);
			}
			if (list2.Count > 0)
			{
				if (list2.Count <= this.MultiRowRefreshThreshold)
				{
					base.DataTableLoader.Refresh(progress, list2.ToArray<object>(), 0);
					return;
				}
				base.RefreshCommand.Invoke();
			}
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000450B8 File Offset: 0x000432B8
		public IRefreshable CreateRefreshableObjectForSelection(params object[] refreshCategories)
		{
			if (refreshCategories == null || refreshCategories.Length == 0)
			{
				throw new ArgumentNullException("refreshCategories");
			}
			object[] value = this.SelectedIdentities.Cast<object>().ToArray<object>();
			IDictionary<object, IList<object>> dictionary = new Dictionary<object, IList<object>>();
			foreach (object key in refreshCategories)
			{
				dictionary.Add(key, value);
			}
			return this.CreateRefreshableObjectForObjects(dictionary);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x00045118 File Offset: 0x00043318
		private IDictionary<object, IList<object>> GroupObjectsByCategory(IList<object> objects, out IList<object> noCategoryList)
		{
			Dictionary<object, IList<object>> dictionary = new Dictionary<object, IList<object>>();
			noCategoryList = new List<object>();
			foreach (object obj in objects)
			{
				object objectRefreshCategory = this.GetObjectRefreshCategory(obj);
				object obj2 = this.GetIdentityFromObject(obj);
				object[] array = obj2 as object[];
				if (array != null)
				{
					obj2 = array[0];
				}
				if (objectRefreshCategory == null)
				{
					noCategoryList.Add(obj2);
				}
				else if (dictionary.ContainsKey(objectRefreshCategory))
				{
					dictionary[objectRefreshCategory].Add(obj2);
				}
				else
				{
					dictionary[objectRefreshCategory] = new List<object>
					{
						obj2
					};
				}
			}
			return dictionary;
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000451CC File Offset: 0x000433CC
		private IRefreshable CreateRefreshableObjectForObjects(IDictionary<object, IList<object>> categoryToIdentitiesMap)
		{
			IList<object> list = new List<object>();
			foreach (KeyValuePair<object, IList<object>> keyValuePair in categoryToIdentitiesMap)
			{
				list.Add((keyValuePair.Value.Count <= this.MultiRowRefreshThreshold) ? new MultiRowRefreshRequest(keyValuePair.Key, keyValuePair.Value.ToArray<object>()) : keyValuePair.Key);
			}
			return base.CreateRefreshableObject(list.ToArray<object>());
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0004525C File Offset: 0x0004345C
		protected ICollection<ExchangeForm> ChildrenExchangeForms
		{
			get
			{
				return this.childrenFormsList.ToArray();
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x00045269 File Offset: 0x00043469
		private void ExportListCommand_Execute(object sender, EventArgs e)
		{
			if (this.ListControl != null)
			{
				WinformsHelper.ShowExportDialog(this, this.ListControl, base.ShellUI);
			}
		}

		// Token: 0x040006AB RID: 1707
		private DataListView listControl;

		// Token: 0x040006AC RID: 1708
		private Command saveDefaultFilterCommand;

		// Token: 0x040006AD RID: 1709
		private ContextMenu listContextMenu;

		// Token: 0x040006AE RID: 1710
		private Command exportListCommandSeparator;

		// Token: 0x040006AF RID: 1711
		private Command exportListCommand;

		// Token: 0x040006B0 RID: 1712
		private Command expandAllCommand;

		// Token: 0x040006B1 RID: 1713
		private Command collapseRootsCommand;

		// Token: 0x040006B2 RID: 1714
		private FilterControl filterControl;

		// Token: 0x040006B3 RID: 1715
		private static readonly object EventFilterControlChanged = new object();

		// Token: 0x040006B4 RID: 1716
		private List<ExchangeForm> childrenFormsList = new List<ExchangeForm>();
	}
}
