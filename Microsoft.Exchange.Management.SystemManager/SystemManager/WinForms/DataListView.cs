using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.Commands;
using Microsoft.ManagementGUI.Tasks;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001D0 RID: 464
	[ComplexBindingProperties("DataSource", "DataMember")]
	[Docking(DockingBehavior.AutoDock)]
	public class DataListView : ListView, IBulkEditor, IDataListViewBulkEditSupport, IBulkEditSupport
	{
		// Token: 0x060013B3 RID: 5043 RVA: 0x0005026C File Offset: 0x0004E46C
		public DataListView()
		{
			this.AllowColumnReorder = true;
			this.DoubleBuffered = true;
			this.availableColumns = new ExchangeColumnHeaderCollection();
			this.availableColumns.ListChanged += this.availableColumns_ListChanged;
			this.availableColumns.ListChanging += this.availableColumns_ListChanging;
			this.CreateDataBoundContextMenu();
			this.filterValues = new FilterValueCollection(this);
			this.FullRowSelect = true;
			this.HideSelection = false;
			base.ShowGroups = false;
			this.ShowItemToolTips = true;
			this.View = View.Details;
			Application.Idle += this.Application_Idle;
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00050387 File Offset: 0x0004E587
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x0005038F File Offset: 0x0004E58F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(true)]
		public new bool AllowColumnReorder
		{
			get
			{
				return base.AllowColumnReorder;
			}
			set
			{
				base.AllowColumnReorder = value;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00050398 File Offset: 0x0004E598
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x000503A0 File Offset: 0x0004E5A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(true)]
		[Browsable(false)]
		public new bool FullRowSelect
		{
			get
			{
				return base.FullRowSelect;
			}
			set
			{
				base.FullRowSelect = value;
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000503A9 File Offset: 0x0004E5A9
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000503B1 File Offset: 0x0004E5B1
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new bool HideSelection
		{
			get
			{
				return base.HideSelection;
			}
			set
			{
				base.HideSelection = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x000503BA File Offset: 0x0004E5BA
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x000503C4 File Offset: 0x0004E5C4
		[DefaultValue(false)]
		public new bool ShowGroups
		{
			get
			{
				return base.ShowGroups;
			}
			set
			{
				if (this.ShowGroups != value)
				{
					base.BeginUpdate();
					try
					{
						this.showInGroupsCommand.Checked = value;
						base.ShowGroups = value;
						if (this.SupportsGrouping)
						{
							if (value)
							{
								if (base.Columns[0].Name != this.SortProperty)
								{
									this.groupProperty = this.SortProperty;
									this.groupDirection = this.SortDirection;
								}
								base.VirtualMode = false;
							}
							this.ApplySort();
						}
					}
					finally
					{
						base.EndUpdate();
					}
				}
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0005045C File Offset: 0x0004E65C
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x00050464 File Offset: 0x0004E664
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(true)]
		[Browsable(false)]
		public new bool ShowItemToolTips
		{
			get
			{
				return base.ShowItemToolTips;
			}
			set
			{
				base.ShowItemToolTips = value;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0005046D File Offset: 0x0004E66D
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x00050475 File Offset: 0x0004E675
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool ShowSubitemIcon
		{
			get
			{
				return this.showSubitemIcon;
			}
			set
			{
				this.showSubitemIcon = value;
				if (base.IsHandleCreated)
				{
					this.UpdateShowSubitemIcon();
				}
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0005048C File Offset: 0x0004E68C
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x00050494 File Offset: 0x0004E694
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(View.Details)]
		public new View View
		{
			get
			{
				return base.View;
			}
			set
			{
				if (base.View != value)
				{
					if (value == View.LargeIcon || value == View.Tile)
					{
						base.LargeImageList = ((this.IconLibrary != null) ? this.IconLibrary.LargeImageList : null);
					}
					base.View = value;
					this.ApplySmartWidthOfColumns();
				}
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x000504CF File Offset: 0x0004E6CF
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x000504D7 File Offset: 0x0004E6D7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectedColumnIndex
		{
			get
			{
				return this.selectedColumnIndex;
			}
			set
			{
				if (this.SelectedColumnIndex != value)
				{
					this.selectedColumnIndex = value;
					if (base.IsHandleCreated)
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4236, (IntPtr)value, (IntPtr)0);
					}
				}
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x00050514 File Offset: 0x0004E714
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x0005051C File Offset: 0x0004E71C
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		public bool ShowFilter
		{
			get
			{
				return this.showfilter;
			}
			set
			{
				if (this.ShowFilter != value)
				{
					this.showfilter = value;
					if (this.showFilterCommand != null)
					{
						this.showFilterCommand.Checked = value;
					}
					if (base.IsHandleCreated)
					{
						ListViewItem focusedItem = base.FocusedItem;
						if (base.VirtualMode)
						{
							this.BackupItemsStates();
						}
						base.RecreateHandle();
						if (base.VirtualMode)
						{
							this.RestoreItemsStates(true);
						}
						if (focusedItem != null)
						{
							focusedItem.Focused = true;
						}
						base.Invalidate();
						if (!value)
						{
							this.OnFilterChanged(EventArgs.Empty);
						}
					}
				}
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x000505A0 File Offset: 0x0004E7A0
		[Browsable(false)]
		public string BindingListViewFilter
		{
			get
			{
				if (!this.ShowFilter)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = false;
				for (int i = 0; i < base.Columns.Count; i++)
				{
					ExchangeColumnHeader exchangeColumnHeader = base.Columns[i] as ExchangeColumnHeader;
					if (exchangeColumnHeader != null)
					{
						string bindingListViewFilter = exchangeColumnHeader.BindingListViewFilter;
						if (!string.IsNullOrEmpty(bindingListViewFilter))
						{
							if (flag)
							{
								stringBuilder.Append(" AND ");
							}
							stringBuilder.Append(bindingListViewFilter);
							flag = true;
						}
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x00050620 File Offset: 0x0004E820
		private void WmNotifyFilterButtonClick(ref Message m)
		{
			NativeMethods.NMHDFILTERBTNCLICK nmhdfilterbtnclick = (NativeMethods.NMHDFILTERBTNCLICK)m.GetLParam(typeof(NativeMethods.NMHDFILTERBTNCLICK));
			FilterButtonClickEventArgs filterButtonClickEventArgs = new FilterButtonClickEventArgs(base.Columns[nmhdfilterbtnclick.iItem], Rectangle.FromLTRB(nmhdfilterbtnclick.rc.left, nmhdfilterbtnclick.rc.top, nmhdfilterbtnclick.rc.right, nmhdfilterbtnclick.rc.bottom));
			this.OnFilterButtonClick(filterButtonClickEventArgs);
			m.Result = (filterButtonClickEventArgs.FilterChanged ? ((IntPtr)1) : IntPtr.Zero);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000506B4 File Offset: 0x0004E8B4
		protected virtual void OnFilterButtonClick(FilterButtonClickEventArgs e)
		{
			ExchangeColumnHeader exchangeColumnHeader = e.ColumnHeader as ExchangeColumnHeader;
			if (exchangeColumnHeader != null)
			{
				FilterOperator filterOperator = exchangeColumnHeader.FilterOperator;
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO(4, 0, 0, 0, 0);
				UnsafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), 0, scrollinfo);
				Size sz = new Size(scrollinfo.nPos, 0);
				exchangeColumnHeader.ContextMenu.Show(this, e.FilterButtonBounds.Location + e.FilterButtonBounds.Size - sz, LeftRightAlignment.Left);
				Application.DoEvents();
				e.FilterChanged = (filterOperator != exchangeColumnHeader.FilterOperator);
			}
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x00050754 File Offset: 0x0004E954
		private void WmNotifyFilterChange(ref Message m)
		{
			this.OnFilterChanged(EventArgs.Empty);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00050764 File Offset: 0x0004E964
		protected virtual void OnFilterChanged(EventArgs e)
		{
			if (this.SupportsFiltering)
			{
				string bindingListViewFilter = this.BindingListViewFilter;
				if (bindingListViewFilter != this.bindingListView.Filter)
				{
					if (base.VirtualMode)
					{
						this.BackupItemsStates();
					}
					this.bindingListView.Filter = bindingListViewFilter;
				}
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060013CB RID: 5067 RVA: 0x000507AD File Offset: 0x0004E9AD
		internal HandleRef HeaderHandle
		{
			get
			{
				return this.headerHandle;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x000507B5 File Offset: 0x0004E9B5
		private HandleRef TooltipHandle
		{
			get
			{
				return this.tooltipHandle;
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x000507BD File Offset: 0x0004E9BD
		public void EditFilter(int columnIndex)
		{
			this.EditFilter(columnIndex, true);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x000507C7 File Offset: 0x0004E9C7
		public void EditFilter(int columnIndex, bool discardUserChanges)
		{
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(this.HeaderHandle, 4631, (IntPtr)columnIndex, discardUserChanges);
			}
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x000507E9 File Offset: 0x0004E9E9
		public void ClearAllFilters()
		{
			this.ClearFilter(-1);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x000507F2 File Offset: 0x0004E9F2
		public void ClearFilter(int columnIndex)
		{
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(this.HeaderHandle, 4632, (IntPtr)columnIndex, (IntPtr)0);
			}
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x0005081C File Offset: 0x0004EA1C
		protected override void OnHandleCreated(EventArgs e)
		{
			HandleRef handleRef = new HandleRef(this, base.Handle);
			IntPtr handle = UnsafeNativeMethods.SendMessage(handleRef, 4127, (IntPtr)0, (IntPtr)0);
			this.headerHandle = new HandleRef(this, handle);
			IntPtr handle2 = UnsafeNativeMethods.SendMessage(handleRef, 4174, (IntPtr)0, (IntPtr)0);
			this.tooltipHandle = new HandleRef(this, handle2);
			if (this.ShowFilter)
			{
				int num = UnsafeNativeMethods.GetWindowLong(this.HeaderHandle, -16);
				num |= 256;
				UnsafeNativeMethods.SetWindowLong(this.HeaderHandle, -16, num);
			}
			base.Invalidate();
			UnsafeNativeMethods.InvalidateRect(handleRef, IntPtr.Zero, false);
			base.OnHandleCreated(e);
			this.UpdateHeaderSortArrow();
			this.UpdateShowSubitemIcon();
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x000508D4 File Offset: 0x0004EAD4
		private void UpdateShowSubitemIcon()
		{
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4150, (IntPtr)2, (IntPtr)(this.ShowSubitemIcon ? 2 : 0));
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00050904 File Offset: 0x0004EB04
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public FilterValueCollection FilterValues
		{
			get
			{
				return this.filterValues;
			}
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x00050920 File Offset: 0x0004EB20
		private void CreateDataBoundContextMenu()
		{
			this.contextMenuCommand = new Command();
			this.arrangeByCommand = new Command();
			this.showInGroupsCommand = new Command();
			this.arrangeByCommand.Name = "arrangeByCommand";
			this.arrangeByCommand.Text = Strings.ArrangeBy;
			this.showInGroupsCommand.Name = "showInGroupsCommand";
			this.showInGroupsCommand.Text = Strings.ShowInGroups;
			this.showInGroupsCommand.Execute += delegate(object param0, EventArgs param1)
			{
				this.ShowGroups = !this.ShowGroups;
			};
			this.contextMenuCommand.Commands.AddRange(new Command[]
			{
				this.ShowFilterCommand,
				this.arrangeByCommand
			});
			this.contextMenuCommand.Name = "contextMenu";
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x000509E9 File Offset: 0x0004EBE9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command ContextMenuCommand
		{
			get
			{
				return this.contextMenuCommand;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x000509F1 File Offset: 0x0004EBF1
		// (set) Token: 0x060013D7 RID: 5079 RVA: 0x000509F9 File Offset: 0x0004EBF9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Command DeleteSelectionCommand
		{
			get
			{
				return this.deleteSelectionCommand;
			}
			set
			{
				this.deleteSelectionCommand = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00050A02 File Offset: 0x0004EC02
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x00050A0A File Offset: 0x0004EC0A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command RefreshCommand
		{
			get
			{
				return this.refreshCommand;
			}
			set
			{
				this.refreshCommand = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x00050A13 File Offset: 0x0004EC13
		// (set) Token: 0x060013DB RID: 5083 RVA: 0x00050A1B File Offset: 0x0004EC1B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command ShowSelectionPropertiesCommand
		{
			get
			{
				return this.showSelectionPropertiesCommand;
			}
			set
			{
				this.showSelectionPropertiesCommand = value;
			}
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00050A24 File Offset: 0x0004EC24
		protected override void OnItemActivate(EventArgs e)
		{
			this.Application_Idle(this, EventArgs.Empty);
			base.OnItemActivate(e);
			if (this.showSelectionPropertiesCommand != null && this.showSelectionPropertiesCommand.Enabled)
			{
				this.showSelectionPropertiesCommand.Invoke();
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00050A59 File Offset: 0x0004EC59
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x00050A61 File Offset: 0x0004EC61
		[DefaultValue(-1)]
		public int ImageIndex
		{
			get
			{
				return this.imageIndex;
			}
			set
			{
				this.imageIndex = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00050A6A File Offset: 0x0004EC6A
		protected bool SupportsSearching
		{
			get
			{
				return this.bindingList != null && this.bindingList.SupportsSearching;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00050A81 File Offset: 0x0004EC81
		protected bool SupportsSorting
		{
			get
			{
				return this.bindingList != null && this.bindingList.SupportsSorting;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00050A98 File Offset: 0x0004EC98
		protected bool SupportsGrouping
		{
			get
			{
				return !base.VirtualMode && this.bindingListView != null && this.bindingListView.SupportsAdvancedSorting && base.Columns.Count > 1;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00050AC7 File Offset: 0x0004ECC7
		protected bool SupportsFiltering
		{
			get
			{
				return this.bindingListView != null && this.bindingListView.SupportsFiltering;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00050ADE File Offset: 0x0004ECDE
		protected internal bool IsCreatingItems
		{
			get
			{
				return this.isCreatingItems;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00050AE6 File Offset: 0x0004ECE6
		private bool IsShowingGroups
		{
			get
			{
				return this.SupportsGrouping && this.ShowGroups && null != this.GroupProperty;
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00050B06 File Offset: 0x0004ED06
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.DataSource = null;
				this.DataSourceRefresher = null;
				this.contextMenuCommand.Dispose();
				Application.Idle -= this.Application_Idle;
				this.IconLibrary = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00050B43 File Offset: 0x0004ED43
		// (set) Token: 0x060013E7 RID: 5095 RVA: 0x00050B4C File Offset: 0x0004ED4C
		[DefaultValue(null)]
		[RefreshProperties(RefreshProperties.All)]
		[AttributeProvider(typeof(IListSource))]
		[Description("Indicates the source of data for the list view control.")]
		[Category("Data")]
		public object DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				if (this.DataSource != value)
				{
					if (value != null && !(value is IList) && !(value is IListSource))
					{
						throw new ArgumentException(Strings.BadDataSourceForComplexBinding, "value");
					}
					this.EnforceValidDataMember(value);
					if (this.DataSource != null && this.DataSource == this.DataSourceRefresher)
					{
						this.DataSourceRefresher = null;
					}
					this.SetListManager(value, this.dataMember);
					if (this.DataSource != null && this.DataSourceRefresher == null)
					{
						this.DataSourceRefresher = (this.DataSource as IRefreshableNotification);
					}
				}
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x00050BDC File Offset: 0x0004EDDC
		// (set) Token: 0x060013E9 RID: 5097 RVA: 0x00050BE4 File Offset: 0x0004EDE4
		public IRefreshableNotification DataSourceRefresher
		{
			get
			{
				return this.dataSourceRefresher;
			}
			set
			{
				if (this.DataSourceRefresher != value)
				{
					bool isDataSourceRefreshing = this.IsDataSourceRefreshing;
					if (this.DataSourceRefresher != null)
					{
						this.DataSourceRefresher.RefreshingChanged -= this.DataSourceRefresher_RefreshingChanged;
					}
					this.dataSourceRefresher = value;
					if (this.DataSourceRefresher != null)
					{
						this.DataSourceRefresher.RefreshingChanged += this.DataSourceRefresher_RefreshingChanged;
						if (isDataSourceRefreshing != this.IsDataSourceRefreshing)
						{
							this.DataSourceRefresher_RefreshingChanged(this.DataSourceRefresher, EventArgs.Empty);
						}
					}
				}
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x00050C60 File Offset: 0x0004EE60
		private bool IsDataSourceRefreshing
		{
			get
			{
				return this.DataSourceRefresher != null && this.DataSourceRefresher.Refreshing;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x00050C77 File Offset: 0x0004EE77
		private bool DataSourceRefreshed
		{
			get
			{
				return this.DataSourceRefresher != null && this.DataSourceRefresher.Refreshed;
			}
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00050C90 File Offset: 0x0004EE90
		private void DataSourceRefresher_RefreshingChanged(object sender, EventArgs e)
		{
			bool refreshing = ((IRefreshableNotification)sender).Refreshing;
			if (refreshing)
			{
				this.Cursor = Cursors.AppStarting;
				this.BackupItemsStates();
				return;
			}
			this.Cursor = Cursors.Default;
			if (this.swCreateItems != null)
			{
				this.swCreateItems.Stop();
				this.swCreateItems = null;
			}
			if (base.IsHandleCreated)
			{
				if (this.needCreateItemsForRows)
				{
					this.EnsureItemsCreated();
				}
				else if (base.VirtualMode && !string.IsNullOrEmpty(this.SortProperty) && !(this.list is IPagedList))
				{
					this.CreateItems();
				}
				else
				{
					this.ApplySmartWidthOfColumns();
					this.TrySelectItemBySpecifiedIdentity();
				}
				this.Application_Idle(this, EventArgs.Empty);
				if (this.FocusedItemIndex < 0 && this.list.Count > 0)
				{
					if (base.SelectedIndices.Count > 0)
					{
						this.FirstSelectedItem.Focused = true;
					}
					else
					{
						this.ListManager.Position = 0;
						this.ListManager_PositionChanged(this.ListManager, EventArgs.Empty);
					}
					this.EnsureFocusedItemVisibleAsBefore();
				}
				this.ClearItemStatesBackup();
				this.selectedItemIdentity = null;
				this.UpdateNoResultsIndicator();
			}
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00050DAC File Offset: 0x0004EFAC
		private void BackupOffsetFocusedItem()
		{
			if (this.View == View.Details && this.focusedItemOffset < 0)
			{
				int focusedItemIndex = this.FocusedItemIndex;
				int topItemIndex = this.TopItemIndex;
				if (topItemIndex >= 0 && focusedItemIndex >= topItemIndex)
				{
					this.focusedItemOffset = focusedItemIndex - topItemIndex;
				}
			}
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x00050DEC File Offset: 0x0004EFEC
		private void EnsureFocusedItemVisibleAsBefore()
		{
			if (this.View == View.Details && this.FocusedItemIndex >= 0 && base.FocusedItem != null && this.focusedItemOffset >= 0)
			{
				base.FocusedItem.EnsureVisible();
				int fromIndex = this.FocusedItemIndex - this.focusedItemOffset;
				if (base.TopItem != null && base.Items.Count > 0)
				{
					this.Scroll(fromIndex, base.TopItem.Index);
				}
			}
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00050E60 File Offset: 0x0004F060
		private void Scroll(int fromIndex, int toIndex)
		{
			if (fromIndex != toIndex && this.View == View.Details && base.Items.Count > 0)
			{
				int num;
				if (fromIndex > toIndex)
				{
					num = Math.Min(fromIndex - toIndex, base.Items.Count - 1 - this.TopItemIndex);
				}
				else
				{
					num = Math.Max(fromIndex - toIndex, -this.TopItemIndex);
				}
				int height = base.Items[0].Bounds.Height;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4116, (IntPtr)0, (IntPtr)(height * num));
			}
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x00050F04 File Offset: 0x0004F104
		private void EnforceValidDataMember(object newDataSource)
		{
			if (newDataSource != null && !string.IsNullOrEmpty(this.DataMember) && this.BindingContext != null)
			{
				try
				{
					BindingManagerBase bindingManagerBase = this.BindingContext[newDataSource, this.DataMember];
				}
				catch (ArgumentException)
				{
					this.dataMember = "";
				}
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00050F5C File Offset: 0x0004F15C
		// (set) Token: 0x060013F2 RID: 5106 RVA: 0x00050F64 File Offset: 0x0004F164
		[Editor("System.Windows.Forms.Design.DataMemberListEditor, System.Design", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Indicates a sub-list of the DataSource to show in the list view control.")]
		[Category("Data")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
			set
			{
				if (value != this.dataMember)
				{
					this.SetListManager(this.DataSource, value);
				}
			}
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00050F81 File Offset: 0x0004F181
		public void SetDataBinding(object newDataSource, string newDataMember)
		{
			if (newDataSource != null && !(newDataSource is IList) && !(newDataSource is IListSource))
			{
				throw new ArgumentException(Strings.BadDataSourceForComplexBinding, "value");
			}
			this.SetListManager(newDataSource, newDataMember);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x00050FB4 File Offset: 0x0004F1B4
		private void SetListManager(object newDataSource, string newDataMember)
		{
			this.inSetListManager = true;
			try
			{
				newDataMember = ((newDataMember == null) ? "" : newDataMember);
				if (newDataSource != null && this.BindingContext != null)
				{
					this.ListManager = (CurrencyManager)this.BindingContext[newDataSource, newDataMember];
				}
				else
				{
					this.ListManager = null;
				}
				this.dataSource = newDataSource;
				this.dataMember = newDataMember;
				this.ApplySort();
				this.OnDataSourceChanged(EventArgs.Empty);
			}
			finally
			{
				this.inSetListManager = false;
			}
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0005103C File Offset: 0x0004F23C
		protected virtual void OnDataSourceChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataListView.EventDataSourceChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x060013F6 RID: 5110 RVA: 0x0005106A File Offset: 0x0004F26A
		// (remove) Token: 0x060013F7 RID: 5111 RVA: 0x0005107D File Offset: 0x0004F27D
		public event EventHandler DataSourceChanged
		{
			add
			{
				base.Events.AddHandler(DataListView.EventDataSourceChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataListView.EventDataSourceChanged, value);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x00051090 File Offset: 0x0004F290
		// (set) Token: 0x060013F9 RID: 5113 RVA: 0x00051098 File Offset: 0x0004F298
		private CurrencyManager ListManager
		{
			get
			{
				return this.listManager;
			}
			set
			{
				if (value != this.ListManager)
				{
					this.ClearItemStatesBackup();
					this.ClearItems(false);
					if (this.AutoGenerateColumns)
					{
						base.Columns.Clear();
					}
					this.ClearAllFilters();
					this.arrangeByCommand.Commands.Clear();
					if (this.ListManager != null)
					{
						this.ListManager.ListChanged -= this.ListManager_ListChanged;
						this.ListManager.PositionChanged -= this.ListManager_PositionChanged;
						this.list = null;
						this.bindingList = null;
						this.bindingListView = null;
						this.advancedBindingListView = null;
					}
					this.listManager = value;
					if (this.ListManager != null)
					{
						this.list = this.ListManager.List;
						this.bindingList = (this.list as IBindingList);
						this.bindingListView = (this.list as IBindingListView);
						this.advancedBindingListView = (this.list as IAdvancedBindingListView);
						this.ListManager.ListChanged += this.ListManager_ListChanged;
						this.ListManager.PositionChanged += this.ListManager_PositionChanged;
					}
					if (this.AutoGenerateColumns && base.HeaderStyle != ColumnHeaderStyle.None)
					{
						base.HeaderStyle = (this.SupportsSorting ? ColumnHeaderStyle.Clickable : ColumnHeaderStyle.Nonclickable);
					}
					this.ShowGroups = false;
					if (this.showFilterCommand != null)
					{
						this.showFilterCommand.Visible = this.SupportsFiltering;
					}
					this.arrangeByCommand.Visible = this.SupportsSorting;
					this.showInGroupsCommand.Visible = this.SupportsGrouping;
				}
			}
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x0005121E File Offset: 0x0004F41E
		protected override void OnBindingContextChanged(EventArgs e)
		{
			if (!this.inSetListManager)
			{
				this.SetListManager(this.DataSource, this.DataMember);
			}
			base.OnBindingContextChanged(e);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x00051244 File Offset: 0x0004F444
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			this.updateShell = true;
			base.OnSelectedIndexChanged(e);
			if (this.IsDataSourceRefreshing && !this.isCreatingItems)
			{
				if (!base.VirtualMode)
				{
					this.BackupOffsetFocusedItem();
					return;
				}
				if (base.SelectedIndices.Count > 0)
				{
					this.BackupItemsStates();
				}
			}
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x00051292 File Offset: 0x0004F492
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (base.IsHandleCreated && !this.isApplyingSmartWidthOfColumns)
			{
				base.BeginInvoke(new MethodInvoker(this.ApplySmartWidthOfColumns));
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x000512BE File Offset: 0x0004F4BE
		// (set) Token: 0x060013FE RID: 5118 RVA: 0x000512C6 File Offset: 0x0004F4C6
		[DefaultValue(true)]
		public bool AutoGenerateColumns
		{
			get
			{
				return this.autoGenerateColumns;
			}
			set
			{
				if (value != this.AutoGenerateColumns)
				{
					this.autoGenerateColumns = value;
					if (this.AutoGenerateColumns && this.DataSource != null)
					{
						base.Clear();
						this.CreateItems();
					}
				}
			}
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x000512F4 File Offset: 0x0004F4F4
		private void CreateDataBoundColumns()
		{
			if (!this.AutoGenerateColumns)
			{
				return;
			}
			PropertyDescriptorCollection itemProperties = this.ListManager.GetItemProperties();
			if (itemProperties.Count == 0 || typeof(string) == itemProperties[0].ComponentType)
			{
				ExchangeColumnHeader exchangeColumnHeader = new ExchangeColumnHeader();
				exchangeColumnHeader.Text = string.Empty;
				exchangeColumnHeader.Width = base.ClientRectangle.Width - SystemInformation.VerticalScrollBarWidth;
				base.Columns.Add(exchangeColumnHeader);
				return;
			}
			this.arrangeByCommand.Commands.Clear();
			foreach (object obj in itemProperties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (!typeof(ICollection).IsAssignableFrom(propertyDescriptor.PropertyType))
				{
					ExchangeColumnHeader exchangeColumnHeader2 = new ExchangeColumnHeader();
					exchangeColumnHeader2.Name = propertyDescriptor.Name;
					exchangeColumnHeader2.Text = propertyDescriptor.DisplayName;
					exchangeColumnHeader2.Width = 100;
					base.Columns.Add(exchangeColumnHeader2);
					this.AddArrangeByPropertyCommand(propertyDescriptor.Name, new LocalizedString(propertyDescriptor.DisplayName));
				}
			}
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00051468 File Offset: 0x0004F668
		public void AddArrangeByPropertyCommand(string columnName, LocalizedString commandDescription)
		{
			Command command = new Command();
			command.Name = columnName;
			command.Text = commandDescription;
			command.Execute += delegate(object sender, EventArgs e)
			{
				Command command2 = (Command)sender;
				if (this.IsShowingGroups)
				{
					this.ApplyGroup(command2.Name);
					return;
				}
				this.ApplySort(command2.Name);
			};
			this.arrangeByCommand.Commands.Add(command);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x000514B4 File Offset: 0x0004F6B4
		public void RemoveArrangeByPropertyCommand(string columnName)
		{
			CommandCollection commands = this.arrangeByCommand.Commands;
			commands.Remove(commands[columnName]);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x000514DC File Offset: 0x0004F6DC
		public void BackupItemsStates()
		{
			if (!base.VirtualMode)
			{
				using (IEnumerator enumerator = base.Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ListViewItem listViewItem = (ListViewItem)obj;
						object key = ((KeyValuePair<object, object>)this.item2RowMapping[listViewItem]).Key;
						if (!this.itemsStates.ContainsKey(key))
						{
							this.itemsStates[key] = this.GetItemStates(listViewItem);
						}
					}
					goto IL_D0;
				}
			}
			if (this.list != null && !this.needCreateItemsForRows && !this.isCreatingItems)
			{
				this.backupSelectedIdentities = this.GetPropertyValueFromSelectedObjects(this.IdentityProperty);
				if (base.FocusedItem != null)
				{
					this.focusedItemIdentity = this.GetRowIdentity(this.GetRowFromItem(base.FocusedItem));
				}
				this.BackupOffsetFocusedItem();
			}
			IL_D0:
			if (this.View == View.Details)
			{
				this.backupTopItemIndex = this.TopItemIndex;
			}
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x000515E0 File Offset: 0x0004F7E0
		public void RestoreItemsStates(bool restoreViewPoint)
		{
			if (base.VirtualMode)
			{
				if (this.focusedItemIdentity != null)
				{
					base.FocusedItem = this.GetItemFromIdentity(this.focusedItemIdentity);
				}
				if (this.backupSelectedIdentities != null && this.backupSelectedIdentities.Count > 0)
				{
					base.SelectedIndices.Clear();
					bool flag = this.backupSelectedIdentities[0] is IComparable;
					if (flag)
					{
						this.backupSelectedIdentities.Sort();
					}
					int num = 0;
					for (int i = base.Items.Count - 1; i >= 0; i--)
					{
						if (num >= this.backupSelectedIdentities.Count)
						{
							break;
						}
						object rowIdentity = this.GetRowIdentity(this.list[i]);
						bool flag2 = flag ? (this.backupSelectedIdentities.BinarySearch(rowIdentity) >= 0) : (this.backupSelectedIdentities.IndexOf(rowIdentity) >= 0);
						if (flag2)
						{
							base.SelectedIndices.Add(i);
							num++;
						}
					}
				}
			}
			else
			{
				foreach (object obj in base.Items)
				{
					ListViewItem item = (ListViewItem)obj;
					this.RestoreItemStates(item);
				}
			}
			if (this.isShowingContextMenu)
			{
				UnsafeNativeMethods.EndMenu();
			}
			if (restoreViewPoint)
			{
				this.EnsureFocusedItemVisibleAsBefore();
			}
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x00051748 File Offset: 0x0004F948
		protected virtual ListViewItemStates GetItemStates(ListViewItem item)
		{
			ListViewItemStates listViewItemStates = ListViewItemStates.Default;
			if (item.Selected)
			{
				listViewItemStates |= ListViewItemStates.Selected;
			}
			if (item.Focused)
			{
				listViewItemStates |= ListViewItemStates.Focused;
			}
			if (item.Checked)
			{
				listViewItemStates |= ListViewItemStates.Checked;
			}
			return listViewItemStates;
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0005177E File Offset: 0x0004F97E
		protected virtual void SetItemStates(ListViewItem item, ListViewItemStates itemStates)
		{
			item.Selected = ((itemStates & ListViewItemStates.Selected) != (ListViewItemStates)0);
			item.Focused = ((itemStates & ListViewItemStates.Focused) != (ListViewItemStates)0);
			item.Checked = ((itemStates & ListViewItemStates.Checked) != (ListViewItemStates)0);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x000517B0 File Offset: 0x0004F9B0
		public bool RestoreItemStates(ListViewItem item)
		{
			bool result = false;
			object key = ((KeyValuePair<object, object>)this.item2RowMapping[item]).Key;
			object obj = this.itemsStates[key];
			if (obj != null)
			{
				this.SetItemStates(item, (ListViewItemStates)obj);
				result = true;
			}
			return result;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000517F9 File Offset: 0x0004F9F9
		protected void ClearItemStatesBackup()
		{
			this.itemsStates.Clear();
			this.backupSelectedIdentities = null;
			this.focusedItemIdentity = null;
			this.focusedItemOffset = -1;
			this.backupTopItemIndex = -1;
		}

		// Token: 0x1400007E RID: 126
		// (add) Token: 0x06001408 RID: 5128 RVA: 0x00051824 File Offset: 0x0004FA24
		// (remove) Token: 0x06001409 RID: 5129 RVA: 0x0005185C File Offset: 0x0004FA5C
		public event EventHandler ItemsForRowsCreated;

		// Token: 0x0600140A RID: 5130 RVA: 0x00051891 File Offset: 0x0004FA91
		protected virtual void OnItemsForRowsCreated(EventArgs e)
		{
			if (this.ItemsForRowsCreated != null)
			{
				this.ItemsForRowsCreated(this, e);
			}
		}

		// Token: 0x1400007F RID: 127
		// (add) Token: 0x0600140B RID: 5131 RVA: 0x000518A8 File Offset: 0x0004FAA8
		// (remove) Token: 0x0600140C RID: 5132 RVA: 0x000518E0 File Offset: 0x0004FAE0
		public event EventHandler CreatingItemsForRows;

		// Token: 0x0600140D RID: 5133 RVA: 0x00051915 File Offset: 0x0004FB15
		protected virtual void OnCreatingItemsForRows(EventArgs e)
		{
			if (this.CreatingItemsForRows != null)
			{
				this.CreatingItemsForRows(this, e);
			}
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x0005192C File Offset: 0x0004FB2C
		private void EnsureDataSourceSortedIfNeeded()
		{
			if (this.SupportsSorting && (!this.IsDataSourceRefreshing || !base.VirtualMode) && !string.IsNullOrEmpty(this.SortProperty) && this.list.Count > 1 && !this.bindingList.IsSorted)
			{
				this.ApplySort();
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x00051980 File Offset: 0x0004FB80
		private void ClearItems(bool backupStatesBeforeClear)
		{
			if (backupStatesBeforeClear)
			{
				this.BackupItemsStates();
			}
			if (base.VirtualMode)
			{
				base.VirtualListSize = 0;
				this.cachedItems = new ListViewItem[0];
				base.SelectedIndices.Clear();
			}
			else
			{
				base.SelectedIndices.Clear();
				base.Items.Clear();
				base.Groups.Clear();
			}
			this.id2ItemMapping = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
			this.item2RowMapping = new Hashtable();
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x000519FC File Offset: 0x0004FBFC
		private void CreateItemsForRows()
		{
			if (base.VirtualMode)
			{
				int virtualListSize = base.VirtualListSize;
				int count = base.SelectedIndices.Count;
				int count2 = this.list.Count;
				bool flag = base.IsHandleCreated && base.VirtualMode && this.View == View.Details && !base.DesignMode;
				if (this.IsDataSourceRefreshing && !string.IsNullOrEmpty(this.SortProperty) && this.bindingList != null && !(this.list is IPagedList) && this.bindingList.SupportsSorting && this.bindingList.IsSorted)
				{
					this.bindingList.RemoveSort();
				}
				int num = Math.Min(this.TopItemIndex, count2 - 1);
				if ((virtualListSize > count2 || virtualListSize == 0) && this.IsDataSourceRefreshing)
				{
					this.ClearItems(false);
					num = ((count2 > 0) ? 0 : -1);
				}
				else if (flag && count2 > 0 && count2 < virtualListSize && this.FocusedItemIndex >= count2)
				{
					base.FocusedItem = base.Items[count2 - 1];
				}
				this.OnCreatingItemsForRows(EventArgs.Empty);
				try
				{
					base.VirtualListSize = count2;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				catch (NullReferenceException)
				{
				}
				if (flag && num >= 0)
				{
					this.Scroll(num, this.TopItemIndex);
				}
				if (virtualListSize == 0 || base.VirtualListSize < virtualListSize)
				{
					this.id2ItemMapping = new Hashtable(base.VirtualListSize, StringComparer.InvariantCultureIgnoreCase);
					this.item2RowMapping = new Hashtable(base.VirtualListSize);
					this.cachedItems = new ListViewItem[base.VirtualListSize];
				}
				else
				{
					this.id2ItemMapping.Clear();
					this.item2RowMapping.Clear();
					Array.Clear(this.cachedItems, 0, this.cachedItems.Length);
				}
				if (base.IsHandleCreated && virtualListSize == base.VirtualListSize)
				{
					base.Invalidate();
				}
				if (base.SelectedIndices.Count > 0 || count != base.SelectedIndices.Count)
				{
					this.OnSelectedIndexChanged(EventArgs.Empty);
					return;
				}
			}
			else
			{
				using (new ControlWaitCursor(this))
				{
					this.ClearItems(true);
					this.id2ItemMapping = new Hashtable(this.list.Count, StringComparer.InvariantCultureIgnoreCase);
					this.item2RowMapping = new Hashtable(this.list.Count);
					this.OnCreatingItemsForRows(EventArgs.Empty);
					ListViewItem[] array = new ListViewItem[this.list.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this.CreateListViewItemForRow(this.list[i]);
					}
					base.Items.AddRange(array);
					this.GroupListViewItems();
				}
			}
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00051CB8 File Offset: 0x0004FEB8
		public void EnsureItemsCreated()
		{
			if (this.needCreateItemsForRows)
			{
				this.needCreateItemsForRows = false;
				this.CreateItems();
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x00051CD0 File Offset: 0x0004FED0
		protected virtual void CreateItems()
		{
			if (base.InvokeRequired)
			{
				ExTraceGlobals.ProgramFlowTracer.TraceDebug((long)this.GetHashCode(), "DataListView.CreateItems called from background thread.");
				base.Invoke(new MethodInvoker(this.CreateItems));
				return;
			}
			ExTraceGlobals.ProgramFlowTracer.TraceDebug((long)this.GetHashCode(), "-->DataListView.CreateItems:");
			bool flag = base.Items.Count == 0;
			if (this.list != null)
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO(4, 0, 0, 0, 0);
				base.BeginUpdate();
				this.isCreatingItems = true;
				this.EnsureDataSourceSortedIfNeeded();
				try
				{
					if (base.Columns.Count == 0)
					{
						this.CreateDataBoundColumns();
					}
					if (base.Columns.Count > 0)
					{
						UnsafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), 0, scrollinfo);
						this.CreateItemsForRows();
					}
					this.OnItemsForRowsCreated(EventArgs.Empty);
					if (!base.VirtualMode || !this.IsDataSourceRefreshing)
					{
						this.RestoreItemsStates(false);
					}
					if (base.FocusedItem == null && this.list != null && this.list.Count > 0)
					{
						this.ListManager.Position = 0;
						this.ListManager_PositionChanged(this.ListManager, EventArgs.Empty);
					}
				}
				finally
				{
					this.isCreatingItems = false;
					base.EndUpdate();
				}
				NativeMethods.SCROLLINFO scrollinfo2 = new NativeMethods.SCROLLINFO(4, 0, 0, 0, 0);
				UnsafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), 0, scrollinfo2);
				if (scrollinfo2.nPos != scrollinfo.nPos)
				{
					int value = scrollinfo.nPos - scrollinfo2.nPos;
					UnsafeNativeMethods.SendMessage(new HandleRef(null, base.Handle), 4116, (IntPtr)value, IntPtr.Zero);
				}
				if ((!base.VirtualMode || !this.IsDataSourceRefreshing) && this.View == View.Details && (this.TopItemIndex == 0 || this.TopItemIndex == this.backupTopItemIndex) && base.FocusedItem != null)
				{
					this.EnsureFocusedItemVisibleAsBefore();
				}
				if (!this.IsDataSourceRefreshing)
				{
					this.TrySelectItemBySpecifiedIdentity();
					this.ClearItemStatesBackup();
				}
			}
			if (flag || !this.IsDataSourceRefreshing)
			{
				this.ApplySmartWidthOfColumns();
			}
			this.UpdateNoResultsIndicator();
			ExTraceGlobals.ProgramFlowTracer.TraceDebug((long)this.GetHashCode(), "<--DataListView.CreateItems:");
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00051EF4 File Offset: 0x000500F4
		protected ListViewItem CreateListViewItemForRow(object row)
		{
			ListViewItem listViewItem = this.CreateNewListViewItem(row);
			listViewItem.ImageIndex = this.ImageIndex;
			object rowIdentity = this.GetRowIdentity(row);
			this.id2ItemMapping[rowIdentity] = listViewItem;
			this.item2RowMapping[listViewItem] = new KeyValuePair<object, object>(rowIdentity, row);
			listViewItem.Tag = row;
			while (listViewItem.SubItems.Count < base.Columns.Count)
			{
				listViewItem.SubItems.Add(string.Empty);
			}
			ItemCheckedEventArgs e = new ItemCheckedEventArgs(listViewItem);
			this.OnUpdateItem(e);
			return listViewItem;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x00051F84 File Offset: 0x00050184
		public object GetRowIdentity(object row)
		{
			object result = row;
			if (row != null && !string.IsNullOrEmpty(this.IdentityProperty))
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(row);
				if (properties[this.IdentityProperty] != null)
				{
					result = DataListView.GetPropertyValue(row, properties[this.IdentityProperty]);
				}
			}
			return result;
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x00051FCC File Offset: 0x000501CC
		protected virtual ListViewItem CreateNewListViewItem(object row)
		{
			return new ListViewItem();
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00051FD4 File Offset: 0x000501D4
		protected virtual void OnUpdateItem(ItemCheckedEventArgs e)
		{
			ListViewItem item = e.Item;
			object rowFromItem = this.GetRowFromItem(item);
			if (rowFromItem != null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(rowFromItem);
				if (rowFromItem is Enum || properties.Count == 0 || typeof(string) == properties[0].ComponentType)
				{
					item.Text = this.GetSubItemTextForRow(rowFromItem, 0, properties);
					Color subItemForColorForRow = this.GetSubItemForColorForRow(rowFromItem, 0, properties);
					if (subItemForColorForRow != Color.Empty)
					{
						item.ForeColor = subItemForColorForRow;
					}
				}
				else
				{
					for (int i = 0; i < base.Columns.Count; i++)
					{
						item.SubItems[i].Text = this.GetSubItemTextForRow(rowFromItem, i, properties);
						Color subItemForColorForRow2 = this.GetSubItemForColorForRow(rowFromItem, i, properties);
						if (subItemForColorForRow2 != Color.Empty)
						{
							item.UseItemStyleForSubItems = false;
							item.SubItems[i].ForeColor = subItemForColorForRow2;
						}
					}
					string text = string.Empty;
					if (!string.IsNullOrEmpty(this.StatusPropertyName))
					{
						int num = 1;
						if (base.Columns.Count > num)
						{
							PropertyDescriptor propertyDescriptor = properties[this.StatusPropertyName];
							if (propertyDescriptor != null)
							{
								string text2 = DataListView.GetPropertyValue(rowFromItem, propertyDescriptor).ToString();
								if (!string.IsNullOrEmpty(text2))
								{
									text = text2;
									item.SubItems[num].Text = this.GetSubItemTextFromRow(rowFromItem, this.StatusPropertyName, properties);
								}
							}
						}
					}
					if (string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(this.ImagePropertyName))
					{
						PropertyDescriptor propertyDescriptor2 = properties[this.ImagePropertyName];
						if (propertyDescriptor2 != null)
						{
							text = DataListView.GetPropertyValue(rowFromItem, propertyDescriptor2).ToString();
						}
					}
					if (!string.IsNullOrEmpty(text))
					{
						item.ImageKey = text;
						if (base.VirtualMode && this.IconLibrary != null)
						{
							item.ImageIndex = this.IconLibrary.Icons.IndexOf(item.ImageKey);
						}
					}
				}
			}
			this.RaiseUpdateItem(e);
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x000521B9 File Offset: 0x000503B9
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x000521C1 File Offset: 0x000503C1
		public string StatusPropertyName { get; set; }

		// Token: 0x06001419 RID: 5145 RVA: 0x000521CC File Offset: 0x000503CC
		private string GetSubItemTextForRow(object row, int columnIndex, PropertyDescriptorCollection properties)
		{
			ColumnHeader columnHeader = base.Columns[columnIndex];
			return this.GetSubItemTextFromRow(row, columnHeader, properties);
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0005220C File Offset: 0x0005040C
		private string GetSubItemTextFromRow(object row, string columnName, PropertyDescriptorCollection properties)
		{
			ExchangeColumnHeader columnHeader = this.AvailableColumns.FirstOrDefault((ExchangeColumnHeader header) => header.Name == columnName);
			return this.FormatRawValueByColumn(this.GetRawValueFromRow(row, columnName, properties), columnHeader);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00052253 File Offset: 0x00050453
		private string GetSubItemTextFromRow(object row, ColumnHeader columnHeader, PropertyDescriptorCollection properties)
		{
			return this.FormatRawValueByColumn(this.GetRawValueFromRow(row, columnHeader.Name, properties), columnHeader);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x0005226C File Offset: 0x0005046C
		internal string FormatRawValueByColumn(object rawValue, ColumnHeader columnHeader)
		{
			ExchangeColumnHeader exchangeColumnHeader = columnHeader as ExchangeColumnHeader;
			string text = string.Empty;
			if (exchangeColumnHeader == null)
			{
				text = TextConverter.DefaultConverter.Format(null, rawValue, null);
			}
			else
			{
				ICustomFormatter customFormatter = exchangeColumnHeader.CustomFormatter ?? TextConverter.DefaultConverter;
				text = customFormatter.Format(exchangeColumnHeader.FormatString, rawValue, exchangeColumnHeader.FormatProvider);
				if (string.IsNullOrEmpty(text))
				{
					text = exchangeColumnHeader.DefaultEmptyText;
				}
			}
			return text;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x000522CD File Offset: 0x000504CD
		private object GetRawValueFromRow(object row, int columnIndex, PropertyDescriptorCollection properties)
		{
			return this.GetRawValueFromRow(row, base.Columns[columnIndex].Name, properties);
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x000522E8 File Offset: 0x000504E8
		private object GetRawValueFromRow(object row, string columnName, PropertyDescriptorCollection properties)
		{
			object obj = null;
			if (properties != null && properties.Count > 0 && !(row is Enum) && !(row is string) && !(row is EnumObject))
			{
				PropertyDescriptor propertyDescriptor = (columnName == "ToString()") ? ToStringPropertyDescriptor.DefaultPropertyDescriptor : properties[columnName];
				if (propertyDescriptor != null)
				{
					obj = DataListView.GetPropertyValue(row, propertyDescriptor);
				}
			}
			else
			{
				obj = row;
			}
			EnumObject enumObject = obj as EnumObject;
			if (enumObject != null)
			{
				obj = enumObject.Value;
			}
			return obj;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00052358 File Offset: 0x00050558
		private Color GetSubItemForColorForRow(object row, int columnIndex, PropertyDescriptorCollection properties)
		{
			object rawValueFromRow = this.GetRawValueFromRow(row, columnIndex, properties);
			ExchangeColumnHeader exchangeColumnHeader = base.Columns[columnIndex] as ExchangeColumnHeader;
			if (exchangeColumnHeader != null && exchangeColumnHeader.ToColorFormatter != null)
			{
				return exchangeColumnHeader.ToColorFormatter.Format(rawValueFromRow);
			}
			return Color.Empty;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x000523A0 File Offset: 0x000505A0
		protected void RaiseUpdateItem(ItemCheckedEventArgs e)
		{
			ItemCheckedEventHandler itemCheckedEventHandler = (ItemCheckedEventHandler)base.Events[DataListView.EventUpdateItem];
			if (itemCheckedEventHandler != null)
			{
				itemCheckedEventHandler(this, e);
			}
		}

		// Token: 0x14000080 RID: 128
		// (add) Token: 0x06001421 RID: 5153 RVA: 0x000523CE File Offset: 0x000505CE
		// (remove) Token: 0x06001422 RID: 5154 RVA: 0x000523E1 File Offset: 0x000505E1
		public event ItemCheckedEventHandler UpdateItem
		{
			add
			{
				base.Events.AddHandler(DataListView.EventUpdateItem, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataListView.EventUpdateItem, value);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x000523F4 File Offset: 0x000505F4
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x000523FC File Offset: 0x000505FC
		[Category("Data")]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
		public string ImagePropertyName
		{
			get
			{
				return this.imagePropertyName;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.ImagePropertyName != value)
				{
					this.imagePropertyName = value;
					this.CreateItems();
				}
			}
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00052424 File Offset: 0x00050624
		internal static object GetPropertyValue(object row, PropertyDescriptor propertyDescriptor)
		{
			object obj;
			try
			{
				obj = WinformsHelper.GetPropertyValue(row, propertyDescriptor);
				if (obj == null)
				{
					obj = "";
				}
			}
			catch (TargetInvocationException ex)
			{
				obj = ex.InnerException.Message;
			}
			return obj;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x00052464 File Offset: 0x00050664
		public bool HasSelection
		{
			get
			{
				return this.SelectedObjects.Count > 0;
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x00052474 File Offset: 0x00050674
		public bool HasSingleSelection
		{
			get
			{
				return this.SelectedObjects.Count == 1;
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x00052484 File Offset: 0x00050684
		public bool HasMultiSelection
		{
			get
			{
				return this.SelectedObjects.Count > 1;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x00052494 File Offset: 0x00050694
		public object SelectedObject
		{
			get
			{
				if (this.SelectedObjects.Count == 0)
				{
					return null;
				}
				return this.SelectedObjects[0];
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x000524B4 File Offset: 0x000506B4
		public IList SelectedObjects
		{
			get
			{
				if (this.selectedObjects == null)
				{
					ArrayList arrayList = new ArrayList(base.SelectedIndices.Count);
					foreach (object obj in base.SelectedIndices)
					{
						int index = (int)obj;
						object obj2 = base.VirtualMode ? this.list[index] : this.GetRowFromItem(base.Items[index]);
						if (obj2 != null)
						{
							arrayList.Add(obj2);
						}
					}
					if (this.SelectedObjectsSorter != null)
					{
						arrayList.Sort(this.SelectedObjectsSorter);
					}
					this.selectedObjects = ArrayList.ReadOnly(arrayList);
				}
				return this.selectedObjects;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x00052580 File Offset: 0x00050780
		// (set) Token: 0x0600142C RID: 5164 RVA: 0x00052588 File Offset: 0x00050788
		[DefaultValue(null)]
		public IComparer SelectedObjectsSorter
		{
			get
			{
				return this.selectedObjectsSorter;
			}
			set
			{
				this.selectedObjectsSorter = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x00052594 File Offset: 0x00050794
		public object SelectedIdentity
		{
			get
			{
				IList list = this.SelectedIdentities;
				if (list.Count > 0)
				{
					return list[0];
				}
				return null;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000525BA File Offset: 0x000507BA
		public IList SelectedIdentities
		{
			get
			{
				if (this.selectedIdentities == null)
				{
					this.selectedIdentities = ArrayList.ReadOnly(this.GetPropertyValueFromSelectedObjects(this.IdentityProperty));
				}
				return this.selectedIdentities;
			}
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x000525E4 File Offset: 0x000507E4
		private ArrayList GetPropertyValueFromSelectedObjects(string propertyName)
		{
			ArrayList arrayList = new ArrayList(this.SelectedObjects.Count);
			if (this.SelectedObjects.Count > 0 && !string.IsNullOrEmpty(propertyName))
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.SelectedObject)[propertyName];
				foreach (object obj in this.SelectedObjects)
				{
					if (propertyDescriptor != null)
					{
						arrayList.Add(DataListView.GetPropertyValue(obj, propertyDescriptor));
					}
					else
					{
						arrayList.Add(obj);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x0005268C File Offset: 0x0005088C
		internal WorkUnit[] GetSelectedWorkUnits()
		{
			return this.GetSelectedWorkUnits(this.IdentityProperty, null);
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x0005269C File Offset: 0x0005089C
		internal WorkUnit[] GetSelectedWorkUnits(string targetPropertyName, Type targetType)
		{
			if (string.IsNullOrEmpty(targetPropertyName))
			{
				throw new ArgumentNullException(targetPropertyName);
			}
			if (string.IsNullOrEmpty(this.SelectionNameProperty))
			{
				throw new InvalidOperationException("SelectionNameProperty must be set in DataListView before GetSelectedWorkUnits is called.");
			}
			if (this.IconLibrary == null)
			{
				throw new InvalidOperationException("IconLibrary must be set in DataListView before GetSelectedWorkUnits is called.");
			}
			WorkUnit[] result;
			using (new ControlWaitCursor(this))
			{
				IList list = this.SelectedObjects;
				WorkUnit[] array = new WorkUnit[list.Count];
				if (list.Count > 0)
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(list[0]);
					PropertyDescriptor propertyDescriptor = properties[this.SelectionNameProperty];
					PropertyDescriptor propertyDescriptor2 = string.IsNullOrEmpty(this.ImagePropertyName) ? null : properties[this.ImagePropertyName];
					PropertyDescriptor propertyDescriptor3 = properties[targetPropertyName];
					if (propertyDescriptor3 == null)
					{
						throw new InvalidOperationException(targetPropertyName + " must be valid in DataListView before GetSelectedWorkUnits is called.");
					}
					if (propertyDescriptor == null)
					{
						throw new InvalidOperationException("SelectionNameProperty must be valid in DataListView before GetSelectedWorkUnits is called.");
					}
					for (int i = 0; i < list.Count; i++)
					{
						object row = list[i];
						string text = (DataListView.GetPropertyValue(row, propertyDescriptor) ?? "").ToString();
						object obj = DataListView.GetPropertyValue(row, propertyDescriptor3);
						if (null != targetType)
						{
							ExTraceGlobals.DataFlowTracer.TraceDebug<object, Type>((long)this.GetHashCode(), "DataListView.GetSelectedWorkUnits is converting value {0} to {1}", obj, targetType);
							obj = LanguagePrimitives.ConvertTo(obj, targetType);
						}
						string name = null;
						if (propertyDescriptor2 != null)
						{
							name = (DataListView.GetPropertyValue(row, propertyDescriptor2) ?? "").ToString();
						}
						else
						{
							ListViewItem itemFromRow = this.GetItemFromRow(row);
							if (itemFromRow != null && !string.IsNullOrEmpty(itemFromRow.ImageKey))
							{
								name = itemFromRow.ImageKey;
							}
							else if (itemFromRow != null && itemFromRow.ImageIndex >= 0)
							{
								name = this.IconLibrary.Icons[itemFromRow.ImageIndex].Name;
							}
							else if (this.ImageIndex >= 0)
							{
								name = this.IconLibrary.Icons[this.ImageIndex].Name;
							}
						}
						Icon icon = (this.IconLibrary.Icons[name] != null) ? this.IconLibrary.Icons[name].Icon : null;
						array[i] = new WorkUnit(text, icon, obj);
					}
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x000528F0 File Offset: 0x00050AF0
		public void SelectItemBySpecifiedIdentity(object identity, bool delay)
		{
			this.selectedItemIdentity = identity;
			if (!delay && identity != null)
			{
				this.TrySelectItemBySpecifiedIdentity();
				this.Application_Idle(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00052914 File Offset: 0x00050B14
		internal void TrySelectItemBySpecifiedIdentity()
		{
			if (this.selectedItemIdentity == null)
			{
				return;
			}
			ListViewItem listViewItem = null;
			if (this.selectedItemIdentity is object[])
			{
				foreach (object identity in (object[])this.selectedItemIdentity)
				{
					listViewItem = this.GetItemFromIdentity(identity);
					if (listViewItem != null)
					{
						break;
					}
				}
			}
			else
			{
				listViewItem = this.GetItemFromIdentity(this.selectedItemIdentity);
			}
			if (listViewItem != null)
			{
				base.SelectedIndices.Clear();
				listViewItem.Selected = true;
				listViewItem.Focused = true;
				this.selectedItemIdentity = null;
				this.EnsureFocusedItemVisibleAsBefore();
				if (base.VirtualMode && this.IsDataSourceRefreshing)
				{
					this.BackupItemsStates();
				}
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x000529B0 File Offset: 0x00050BB0
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x000529B8 File Offset: 0x00050BB8
		[DefaultValue(false)]
		public bool HideSortArrow
		{
			get
			{
				return this.hideSortArrow;
			}
			set
			{
				if (value != this.hideSortArrow)
				{
					this.hideSortArrow = value;
					if (base.IsHandleCreated)
					{
						base.Invalidate();
					}
				}
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x000529D8 File Offset: 0x00050BD8
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x000529E0 File Offset: 0x00050BE0
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
		[Category("Data")]
		public string IdentityProperty
		{
			get
			{
				return this.identityProperty;
			}
			set
			{
				if (value == null)
				{
					this.identityProperty = string.Empty;
				}
				else
				{
					this.identityProperty = value;
				}
				if (this.DataSource != null)
				{
					this.CreateItems();
				}
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00052A07 File Offset: 0x00050C07
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x00052A10 File Offset: 0x00050C10
		[DefaultValue("")]
		[Category("Data")]
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
		public string SelectionNameProperty
		{
			get
			{
				return this.selectionNameProperty;
			}
			set
			{
				if (value == null)
				{
					this.selectionNameProperty = string.Empty;
					return;
				}
				this.selectionNameProperty = value;
				foreach (ExchangeColumnHeader exchangeColumnHeader in this.AvailableColumns)
				{
					if (exchangeColumnHeader.Name == this.SelectionNameProperty)
					{
						exchangeColumnHeader.Visible = true;
					}
				}
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00052A88 File Offset: 0x00050C88
		public string SelectionName
		{
			get
			{
				string result;
				switch (this.SelectedObjects.Count)
				{
				case 0:
					result = " ";
					break;
				case 1:
					if (!string.IsNullOrEmpty(this.SelectionNameProperty))
					{
						result = DataListView.GetPropertyValue(this.SelectedObject, TypeDescriptor.GetProperties(this.SelectedObject)[this.SelectionNameProperty]).ToString();
					}
					else
					{
						result = this.SelectedObject.ToString();
					}
					break;
				default:
					result = Strings.ItemsSelected(this.SelectedObjects.Count);
					break;
				}
				return result;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x00052B14 File Offset: 0x00050D14
		public string SelectedObjectDetailsType
		{
			get
			{
				string result = string.Empty;
				if (this.SelectedObjects.Count > 0 && !string.IsNullOrEmpty(this.SelectedObjectDetailsTypeProperty))
				{
					PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.SelectedObject);
					result = this.GetSubItemTextFromRow(this.SelectedObject, this.SelectedObjectDetailsTypeProperty, properties);
				}
				return result;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00052B63 File Offset: 0x00050D63
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x00052B6C File Offset: 0x00050D6C
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
		[Category("Data")]
		[DefaultValue("")]
		public string SelectedObjectDetailsTypeProperty
		{
			get
			{
				return this.selectedObjectDetailsTypeProperty;
			}
			set
			{
				if (value == null)
				{
					this.selectedObjectDetailsTypeProperty = string.Empty;
					return;
				}
				this.selectedObjectDetailsTypeProperty = value;
				foreach (ExchangeColumnHeader exchangeColumnHeader in this.AvailableColumns)
				{
					if (exchangeColumnHeader.Name == this.SelectedObjectDetailsTypeProperty)
					{
						exchangeColumnHeader.Visible = true;
					}
				}
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00052BE4 File Offset: 0x00050DE4
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00052BEC File Offset: 0x00050DEC
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
		[Category("Data")]
		[DefaultValue("")]
		public string SortProperty
		{
			get
			{
				return this.sortProperty;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.SortProperty != value)
				{
					this.sortProperty = value;
					this.ApplySort();
				}
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00052C13 File Offset: 0x00050E13
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x00052C1B File Offset: 0x00050E1B
		[Category("Data")]
		[DefaultValue(ListSortDirection.Ascending)]
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				if (this.SortDirection != value)
				{
					this.sortDirection = value;
					this.ApplySort();
				}
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00052C33 File Offset: 0x00050E33
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00052C3B File Offset: 0x00050E3B
		[Editor("System.Windows.Forms.Design.DataMemberFieldEditor, System.Design", typeof(UITypeEditor))]
		[Category("Data")]
		[DefaultValue("")]
		public string GroupProperty
		{
			get
			{
				return this.groupProperty;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (this.GroupProperty != value)
				{
					this.groupProperty = value;
					this.ApplySort();
				}
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00052C62 File Offset: 0x00050E62
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00052C6A File Offset: 0x00050E6A
		[DefaultValue(ListSortDirection.Ascending)]
		[Category("Data")]
		public ListSortDirection GroupDirection
		{
			get
			{
				return this.groupDirection;
			}
			set
			{
				if (this.GroupDirection != value)
				{
					this.groupDirection = value;
					this.ApplySort();
				}
			}
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00052C82 File Offset: 0x00050E82
		private void ApplySort(string property)
		{
			if (!(property == this.SortProperty))
			{
				this.SortProperty = property;
				return;
			}
			if (this.SortDirection == ListSortDirection.Ascending)
			{
				this.SortDirection = ListSortDirection.Descending;
				return;
			}
			this.SortDirection = ListSortDirection.Ascending;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00052CB1 File Offset: 0x00050EB1
		private void ApplyGroup(string property)
		{
			if (!(property == this.GroupProperty))
			{
				this.GroupProperty = property;
				return;
			}
			if (this.GroupDirection == ListSortDirection.Ascending)
			{
				this.GroupDirection = ListSortDirection.Descending;
				return;
			}
			this.GroupDirection = ListSortDirection.Ascending;
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00052CE0 File Offset: 0x00050EE0
		private void ApplySort()
		{
			if (this.SupportsSorting && !string.IsNullOrEmpty(this.SortProperty))
			{
				using (new ControlWaitCursor(this))
				{
					if (base.VirtualMode && !this.IsDataSourceRefreshing && !this.isCreatingItems)
					{
						this.BackupItemsStates();
					}
					PropertyDescriptorCollection itemProperties = this.ListManager.GetItemProperties();
					PropertyDescriptor property;
					if (this.SortProperty == "ToString()")
					{
						property = ToStringPropertyDescriptor.DefaultPropertyDescriptor;
					}
					else
					{
						property = itemProperties[this.SortProperty];
					}
					if (this.IsShowingGroups && !string.IsNullOrEmpty(this.GroupProperty))
					{
						PropertyDescriptor property2 = itemProperties[this.GroupProperty];
						this.bindingListView.ApplySort(new ListSortDescriptionCollection(new ListSortDescription[]
						{
							new ListSortDescription(property2, this.GroupDirection),
							new ListSortDescription(property, this.SortDirection)
						}));
					}
					else if (!base.VirtualMode || !this.IsDataSourceRefreshing)
					{
						this.bindingList.ApplySort(property, this.SortDirection);
					}
					string b = this.IsShowingGroups ? this.GroupProperty : this.SortProperty;
					CommandCollection commands = this.arrangeByCommand.Commands;
					for (int i = 0; i < commands.Count - 2; i++)
					{
						commands[i].Checked = (commands[i].Name == b);
					}
					this.UpdateHeaderSortArrow();
					return;
				}
			}
			this.ListManager_ListChanged(this.ListManager, new ListChangedEventArgs(ListChangedType.Reset, -1));
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00052E88 File Offset: 0x00051088
		private void UpdateHeaderSortArrow()
		{
			if (base.IsHandleCreated && !this.HideSortArrow && base.HeaderStyle == ColumnHeaderStyle.Clickable)
			{
				int num = -1;
				int num2 = -1;
				InternalNativeMethods.HDITEM hditem = default(InternalNativeMethods.HDITEM);
				hditem.mask = 4U;
				for (int i = 0; i < base.Columns.Count; i++)
				{
					InternalUnsafeNativeMethods.SendMessage(this.HeaderHandle, NativeMethods.HDM_GETITEM, (IntPtr)i, ref hditem);
					hditem.fmt &= -1537;
					InternalUnsafeNativeMethods.SendMessage(this.HeaderHandle, NativeMethods.HDM_SETITEM, (IntPtr)i, ref hditem);
					if (!string.IsNullOrEmpty(this.SortProperty) && this.SortProperty == base.Columns[i].Name)
					{
						num = i;
					}
					if (!string.IsNullOrEmpty(this.GroupProperty) && this.GroupProperty == base.Columns[i].Name)
					{
						num2 = i;
					}
				}
				this.SelectedColumnIndex = num;
				if (-1 != num)
				{
					InternalUnsafeNativeMethods.SendMessage(this.HeaderHandle, NativeMethods.HDM_GETITEM, (IntPtr)num, ref hditem);
					hditem.fmt &= -1537;
					hditem.fmt |= ((this.SortDirection == ListSortDirection.Ascending) ? 1024 : 512);
					InternalUnsafeNativeMethods.SendMessage(this.HeaderHandle, NativeMethods.HDM_SETITEM, (IntPtr)num, ref hditem);
				}
				if (this.ShowGroups && -1 != num2)
				{
					InternalUnsafeNativeMethods.SendMessage(this.HeaderHandle, NativeMethods.HDM_GETITEM, (IntPtr)num2, ref hditem);
					hditem.fmt &= -1537;
					hditem.fmt |= ((this.GroupDirection == ListSortDirection.Ascending) ? 1024 : 512);
					InternalUnsafeNativeMethods.SendMessage(this.HeaderHandle, NativeMethods.HDM_SETITEM, (IntPtr)num2, ref hditem);
				}
			}
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x0005306C File Offset: 0x0005126C
		private void GroupListViewItems()
		{
			if (this.IsShowingGroups)
			{
				PropertyDescriptor propertyDescriptor = this.ListManager.GetItemProperties()[this.GroupProperty];
				if (propertyDescriptor != null)
				{
					ControlWaitCursor controlWaitCursor;
					controlWaitCursor..ctor(this);
					try
					{
						base.BeginUpdate();
						try
						{
							base.Groups.Clear();
							ListViewGroup listViewGroup = new ListViewGroup();
							for (int i = 0; i < base.Items.Count; i++)
							{
								object rowFromItem = this.GetRowFromItem(base.Items[i]);
								if (rowFromItem != null)
								{
									object propertyValue = DataListView.GetPropertyValue(rowFromItem, propertyDescriptor);
									if (!object.Equals(listViewGroup.Tag, propertyValue))
									{
										string text = propertyValue.ToString();
										string headerText = text;
										listViewGroup = new ListViewGroup(text, headerText);
										listViewGroup.Tag = propertyValue;
										base.Groups.Add(listViewGroup);
									}
									base.Items[i].Group = listViewGroup;
								}
							}
							foreach (object obj in base.Groups)
							{
								ListViewGroup listViewGroup2 = (ListViewGroup)obj;
								listViewGroup2.Header = Strings.GroupHeader(listViewGroup2.Header, listViewGroup2.Items.Count);
							}
						}
						finally
						{
							base.EndUpdate();
						}
					}
					finally
					{
						controlWaitCursor.Dispose();
					}
				}
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00053210 File Offset: 0x00051410
		protected override void OnColumnClick(ColumnClickEventArgs e)
		{
			ExchangeColumnHeader exchangeColumnHeader = base.Columns[e.Column] as ExchangeColumnHeader;
			bool flag = true;
			if (exchangeColumnHeader != null)
			{
				flag = exchangeColumnHeader.IsSortable;
			}
			if (this.SupportsSorting && flag)
			{
				PropertyDescriptorCollection itemProperties = this.ListManager.GetItemProperties();
				PropertyDescriptor propertyDescriptor = itemProperties[base.Columns[e.Column].Name];
				if (propertyDescriptor != null)
				{
					bool flag2;
					if (this.advancedBindingListView != null)
					{
						flag2 = this.advancedBindingListView.IsSortSupported(propertyDescriptor.Name);
					}
					else
					{
						flag2 = typeof(IComparable).IsAssignableFrom(propertyDescriptor.PropertyType);
					}
					if (flag2)
					{
						if (this.IsShowingGroups && this.GroupProperty == propertyDescriptor.Name)
						{
							this.ApplyGroup(propertyDescriptor.Name);
						}
						else
						{
							this.ApplySort(propertyDescriptor.Name);
						}
					}
				}
			}
			base.OnColumnClick(e);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x000532F4 File Offset: 0x000514F4
		protected override void OnColumnReordered(ColumnReorderedEventArgs e)
		{
			ExchangeColumnHeader exchangeColumnHeader = e.Header as ExchangeColumnHeader;
			ExchangeColumnHeader exchangeColumnHeader2 = base.Columns[e.NewDisplayIndex] as ExchangeColumnHeader;
			if ((exchangeColumnHeader != null && !exchangeColumnHeader.IsReorderable) || (exchangeColumnHeader2 != null && !exchangeColumnHeader2.IsReorderable))
			{
				e.Cancel = true;
			}
			base.OnColumnReordered(e);
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00053348 File Offset: 0x00051548
		private void WmNotifyBeginDrag(ref Message m)
		{
			bool flag = false;
			if (!this.isDraggingNonReorderableColumn)
			{
				NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				if (nmheader.iItem >= 0)
				{
					ExchangeColumnHeader exchangeColumnHeader = base.Columns[nmheader.iItem] as ExchangeColumnHeader;
					if (exchangeColumnHeader != null && !exchangeColumnHeader.IsReorderable)
					{
						this.isDraggingNonReorderableColumn = true;
						m.Result = (IntPtr)1;
						flag = true;
					}
				}
			}
			else
			{
				m.Result = (IntPtr)1;
				flag = true;
			}
			if (!flag)
			{
				base.WndProc(ref m);
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000533D4 File Offset: 0x000515D4
		private void WmNotify(ref Message m)
		{
			int code = ((NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR))).code;
			if (code != -327)
			{
				switch (code)
				{
				case -313:
					this.WmNotifyFilterButtonClick(ref m);
					return;
				case -312:
					this.WmNotifyFilterChange(ref m);
					return;
				case -311:
					break;
				case -310:
					this.WmNotifyBeginDrag(ref m);
					return;
				case -309:
				case -308:
					goto IL_84;
				case -307:
					goto IL_55;
				default:
					if (code != -16)
					{
						goto IL_84;
					}
					break;
				}
				this.isDraggingNonReorderableColumn = false;
				base.WndProc(ref m);
				return;
				IL_84:
				base.WndProc(ref m);
				return;
			}
			IL_55:
			this.WmNotifyEndTrack(ref m);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x0005346C File Offset: 0x0005166C
		private void WmReflectNotify(ref Message m)
		{
			base.WndProc(ref m);
			if (((NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR))).code == NativeMethods.LVN_ENDLABELEDIT)
			{
				this.Application_Idle(this, EventArgs.Empty);
			}
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x000534B0 File Offset: 0x000516B0
		private void WmContextMenu(ref Message m)
		{
			this.isShowingContextMenu = true;
			try
			{
				if ((this.ContextMenu != null || this.ContextMenuStrip != null) && base.SelectedIndices.Count > 0 && (int)((long)m.LParam) == -1)
				{
					ListViewItem firstSelectedItem = this.FirstSelectedItem;
					Rectangle bounds = firstSelectedItem.GetBounds(ItemBoundsPortion.Icon);
					Point location = bounds.Location;
					location.X = bounds.X + bounds.Width / 2;
					location.Y = bounds.Y + bounds.Height / 2;
					if (location.X < base.ClientRectangle.Left)
					{
						location.X = base.ClientRectangle.Left;
					}
					if (location.X > base.ClientRectangle.Right)
					{
						location.X = base.ClientRectangle.Right;
					}
					if (location.Y < base.ClientRectangle.Top)
					{
						location.Y = base.ClientRectangle.Top;
					}
					if (location.Y > base.ClientRectangle.Bottom)
					{
						location.Y = base.ClientRectangle.Bottom;
					}
					if (this.ContextMenu != null)
					{
						this.ContextMenu.Show(this, location);
					}
					else if (this.ContextMenuStrip != null)
					{
						this.ContextMenuStrip.Show(this, location);
					}
				}
				else
				{
					int x = NativeMethods.LOWORD(m.LParam);
					int y = NativeMethods.HIWORD(m.LParam);
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					UnsafeNativeMethods.GetWindowRect(this.HeaderHandle, ref rect);
					if (!Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom).Contains(x, y))
					{
						base.WndProc(ref m);
					}
				}
			}
			finally
			{
				this.isShowingContextMenu = false;
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x000536B4 File Offset: 0x000518B4
		[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 78)
			{
				if (msg == 15)
				{
					base.WndProc(ref m);
					this.UpdateIndicator();
					return;
				}
				if (msg == 78)
				{
					this.WmNotify(ref m);
					return;
				}
			}
			else
			{
				if (msg == 123)
				{
					this.Application_Idle(this, EventArgs.Empty);
					this.WmContextMenu(ref m);
					return;
				}
				if (msg == 8270)
				{
					this.WmReflectNotify(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00053720 File Offset: 0x00051920
		private void UpdateIndicator()
		{
			if (this.DrawLockedIcon || this.DrawLockedString)
			{
				this.DrawLockedState();
				return;
			}
			if (this.ShowNoResultsIndicator || this.ShowBulkEditIndicator)
			{
				this.DrawIndicator(this.ShowNoResultsIndicator ? this.NoResultsLabelText : this.BulkEditingIndicatorText);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00053770 File Offset: 0x00051970
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x00053778 File Offset: 0x00051978
		public bool DrawLockedIcon { get; set; }

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00053781 File Offset: 0x00051981
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x00053789 File Offset: 0x00051989
		public bool DrawLockedString { get; set; }

		// Token: 0x06001457 RID: 5207 RVA: 0x00053794 File Offset: 0x00051994
		protected virtual void DrawLockedState()
		{
			base.Enabled = false;
			if (this.DrawLockedString)
			{
				int num = 16;
				string text = Strings.NotPermittedByRbac;
				Rectangle indicatorBounds = this.GetIndicatorBounds(text, num, num / 2);
				this.DrawIndicator(indicatorBounds, text);
				Rectangle bounds = new Rectangle(indicatorBounds.X - num, indicatorBounds.Y + (this.Font.Height - num) / 2, num, num);
				this.DrawIcon(bounds, Icons.LockIcon);
				return;
			}
			int num2 = 8;
			int num3 = (base.HeaderStyle != ColumnHeaderStyle.None) ? 16 : 0;
			Rectangle bounds2 = new Rectangle(base.ClientRectangle.Right - num2, base.ClientRectangle.Top + num3, num2, num2);
			this.DrawIcon(bounds2, Icons.LockIcon);
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x00053858 File Offset: 0x00051A58
		private void DrawIcon(Rectangle bounds, Icon icon)
		{
			using (Graphics graphics = base.CreateGraphics())
			{
				Color color = base.Enabled ? this.BackColor : SystemColors.Control;
				using (new SolidBrush(color))
				{
					graphics.DrawIcon(icon, bounds);
				}
			}
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x000538C8 File Offset: 0x00051AC8
		private void DrawIndicator(string text)
		{
			this.DrawIndicator(this.GetIndicatorBounds(text), text);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x000538D8 File Offset: 0x00051AD8
		private Rectangle GetIndicatorBounds(string text)
		{
			return this.GetIndicatorBounds(text, 0, 0);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x000538E4 File Offset: 0x00051AE4
		private Rectangle GetIndicatorBounds(string text, int minXBorder, int minYPosition)
		{
			int num = this.ShowFilter ? (this.Font.Height * 4) : (this.Font.Height * 2);
			if (base.HeaderStyle == ColumnHeaderStyle.None)
			{
				num = 10;
			}
			num = Math.Max(num, minYPosition);
			int num2 = Math.Max(minXBorder, this.Font.Height);
			Size size = TextRenderer.MeasureText(text, this.Font, new Size(base.Width - num2 * 2, base.Height - num), this.GetIndicatorFormatFlags());
			Point location = new Point((base.Width - size.Width) / 2, num);
			return new Rectangle(location, size);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00053984 File Offset: 0x00051B84
		private void DrawIndicator(Rectangle bounds, string text)
		{
			using (Graphics graphics = base.CreateGraphics())
			{
				TextRenderer.DrawText(graphics, text, this.Font, bounds, base.Enabled ? SystemColors.ControlText : SystemColors.GrayText, this.GetIndicatorFormatFlags());
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x000539DC File Offset: 0x00051BDC
		private TextFormatFlags GetIndicatorFormatFlags()
		{
			return TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x000539E4 File Offset: 0x00051BE4
		private void Application_Idle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated && !this.isShowingContextMenu)
			{
				if (this.needCreateItemsForRows && (this.swCreateItems == null || this.swCreateItems.ElapsedMilliseconds >= 100L))
				{
					this.needCreateItemsForRows = false;
					this.CreateItems();
					if (this.swCreateItems != null)
					{
						this.swCreateItems.Reset();
						this.swCreateItems.Start();
					}
				}
				if (!this.needCreateItemsForRows)
				{
					if (this.ListManager != null && base.FocusedItem != null && this.ListManager.Position >= 0 && base.FocusedItem.Index < this.ListManager.Count && this.ListManager.Position < this.ListManager.Count)
					{
						object rowFromItem = this.GetRowFromItem(base.FocusedItem);
						object rowIdentity = this.GetRowIdentity(this.ListManager.Current);
						object rowIdentity2 = this.GetRowIdentity(rowFromItem);
						if (!StringComparer.InvariantCultureIgnoreCase.Equals(rowIdentity, rowIdentity2))
						{
							int num = this.ListManager.List.IndexOf(rowFromItem);
							if (-1 != num)
							{
								this.ListManager.Position = num;
							}
						}
					}
					this.RaiseSelectionChanged();
				}
			}
		}

		// Token: 0x14000081 RID: 129
		// (add) Token: 0x0600145F RID: 5215 RVA: 0x00053B14 File Offset: 0x00051D14
		// (remove) Token: 0x06001460 RID: 5216 RVA: 0x00053B4C File Offset: 0x00051D4C
		public event EventHandler SelectionChanged;

		// Token: 0x06001461 RID: 5217 RVA: 0x00053B81 File Offset: 0x00051D81
		protected virtual void OnSelectionChanged(EventArgs e)
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, e);
			}
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00053B98 File Offset: 0x00051D98
		private void WmNotifyEndTrack(ref Message m)
		{
			base.WndProc(ref m);
			this.IsColumnsWidthDirty = true;
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x00053BA8 File Offset: 0x00051DA8
		// (set) Token: 0x06001464 RID: 5220 RVA: 0x00053BB0 File Offset: 0x00051DB0
		internal bool IsColumnsWidthDirty
		{
			get
			{
				return this.isColumnsWidthDirty;
			}
			set
			{
				this.isColumnsWidthDirty = value;
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00053BBC File Offset: 0x00051DBC
		protected void ApplySmartWidthOfColumns()
		{
			if (!base.IsHandleCreated || this.IsColumnsWidthDirty || this.View != View.Details || base.Columns.Count < 1 || this.isApplyingSmartWidthOfColumns)
			{
				return;
			}
			this.isApplyingSmartWidthOfColumns = true;
			try
			{
				if (base.HeaderStyle == ColumnHeaderStyle.None)
				{
					base.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				}
			}
			finally
			{
				this.isApplyingSmartWidthOfColumns = false;
			}
			if (this.ShowFilter)
			{
				base.Focus();
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x00053C68 File Offset: 0x00051E68
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Command ShowFilterCommand
		{
			get
			{
				if (this.showFilterCommand == null)
				{
					this.showFilterCommand = new Command();
					this.showFilterCommand.Name = "showFilterCommand";
					this.showFilterCommand.Text = Strings.ShowFilter;
					this.showFilterCommand.Execute += delegate(object param0, EventArgs param1)
					{
						this.ShowFilter = !this.ShowFilter;
						if (this.ShowFilter && base.Columns.Count > 0)
						{
							this.EditFilter(0);
						}
					};
					this.showFilterCommand.Checked = this.ShowFilter;
					this.showFilterCommand.Icon = Icons.CreateFilter;
					this.showFilterCommand.Visible = this.SupportsFiltering;
				}
				return this.showFilterCommand;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x00053D20 File Offset: 0x00051F20
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command InlineEditCommand
		{
			get
			{
				if (this.inlineEditCommand == null)
				{
					this.inlineEditCommand = new Command();
					this.inlineEditCommand.Enabled = this.CanInlineEdit;
					this.inlineEditCommand.Name = "inlineEditCommand";
					this.inlineEditCommand.Text = Strings.ListEditEdit;
					this.inlineEditCommand.Description = Strings.ListEditEditDescription;
					this.inlineEditCommand.Icon = Icons.Edit;
					this.inlineEditCommand.Execute += delegate(object param0, EventArgs param1)
					{
						this.ProcessCmdKeyRename();
					};
					this.SelectionChanged += delegate(object param0, EventArgs param1)
					{
						this.inlineEditCommand.Enabled = this.CanInlineEdit;
					};
				}
				return this.inlineEditCommand;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00053DDB File Offset: 0x00051FDB
		private bool CanInlineEdit
		{
			get
			{
				return base.LabelEdit && base.FocusedItem != null && base.SelectedIndices.Count == 1;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x00053E50 File Offset: 0x00052050
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Command RemoveCommand
		{
			get
			{
				if (this.removeCommand == null)
				{
					this.removeCommand = new Command();
					this.removeCommand.Enabled = this.CanRemove;
					this.removeCommand.Name = "removeCommand";
					this.removeCommand.Text = Strings.ListEditRemove;
					this.removeCommand.Style = 8;
					this.removeCommand.Description = Strings.ListEditRemoveDescription;
					this.removeCommand.Icon = Icons.Remove;
					this.removeCommand.Execute += delegate(object param0, EventArgs param1)
					{
						try
						{
							this.RemoveSelectedItems();
						}
						catch (InvalidOperationException ex)
						{
							this.UIService.ShowMessage(ex.Message);
						}
					};
					this.SelectionChanged += delegate(object param0, EventArgs param1)
					{
						this.removeCommand.Enabled = this.CanRemove;
					};
				}
				return this.removeCommand;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00053F17 File Offset: 0x00052117
		private IUIService UIService
		{
			get
			{
				return ((IUIService)this.GetService(typeof(IUIService))) ?? new UIService(this);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x00053F58 File Offset: 0x00052158
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command MoveUpCommand
		{
			get
			{
				if (this.moveUpCommand == null)
				{
					this.moveUpCommand = new Command();
					this.moveUpCommand.Enabled = this.CanMove(-1);
					this.moveUpCommand.Name = "moveUpCommand";
					this.moveUpCommand.Text = Strings.ListEditMoveUp;
					this.moveUpCommand.Description = Strings.ListEditMoveUpDescription;
					this.moveUpCommand.Icon = Icons.MoveUp;
					this.moveUpCommand.Execute += delegate(object param0, EventArgs param1)
					{
						this.MoveFocusedItem(-1);
					};
					this.SelectionChanged += delegate(object param0, EventArgs param1)
					{
						this.moveUpCommand.Enabled = this.CanMove(-1);
					};
				}
				return this.moveUpCommand;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x00054034 File Offset: 0x00052234
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Command MoveDownCommand
		{
			get
			{
				if (this.moveDownCommand == null)
				{
					this.moveDownCommand = new Command();
					this.moveDownCommand.Enabled = this.CanMove(1);
					this.moveDownCommand.Name = "moveDownCommand";
					this.moveDownCommand.Text = Strings.ListEditMoveDown;
					this.moveDownCommand.Description = Strings.ListEditMoveDownDescription;
					this.moveDownCommand.Icon = Icons.MoveDown;
					this.moveDownCommand.Execute += delegate(object param0, EventArgs param1)
					{
						this.MoveFocusedItem(1);
					};
					this.SelectionChanged += delegate(object param0, EventArgs param1)
					{
						this.moveDownCommand.Enabled = this.CanMove(1);
					};
				}
				return this.moveDownCommand;
			}
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000540F0 File Offset: 0x000522F0
		private bool CanMove(int delta)
		{
			bool result = false;
			if (base.FocusedItem != null && this.ListManager != null && this.View == View.Details)
			{
				object rowFromItem = this.GetRowFromItem(base.FocusedItem);
				if (rowFromItem != null)
				{
					int num = this.ListManager.List.IndexOf(rowFromItem);
					int num2 = num + delta;
					result = (num2 >= 0 && num2 < this.ListManager.Count);
				}
			}
			return result;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00054158 File Offset: 0x00052358
		private void MoveFocusedItem(int delta)
		{
			if (base.FocusedItem != null && this.ListManager != null)
			{
				object rowFromItem = this.GetRowFromItem(base.FocusedItem);
				if (rowFromItem != null)
				{
					if (base.VirtualMode)
					{
						this.BackupItemsStates();
					}
					int num = this.ListManager.List.IndexOf(rowFromItem);
					int index = num + delta;
					object value = this.ListManager.List[index];
					this.ListManager.List[index] = rowFromItem;
					this.ListManager.List[num] = value;
					if (this.bindingList == null)
					{
						this.CreateItems();
					}
					this.FocusOnDataRow(rowFromItem);
				}
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x000541F7 File Offset: 0x000523F7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ExchangeColumnHeaderCollection AvailableColumns
		{
			get
			{
				return this.availableColumns;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x00054484 File Offset: 0x00052684
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public Command ShowColumnPickerCommand
		{
			get
			{
				if (this.showColumnPickerCommand == null)
				{
					this.showColumnPickerCommand = new Command();
					this.showColumnPickerCommand.Name = "showColumnPickerCommand";
					this.showColumnPickerCommand.Text = Strings.ColumnPickerCommandText;
					this.showColumnPickerCommand.Execute += delegate(object param0, EventArgs param1)
					{
						ColumnPickerDialog columnPickerDialog = new ColumnPickerDialog(this);
						columnPickerDialog.Text = Strings.ColumnPickerDialogTitle;
						using (PropertyPageDialog propertyPageDialog = new PropertyPageDialog(columnPickerDialog))
						{
							propertyPageDialog.MinimumSize = columnPickerDialog.MinimumSize;
							propertyPageDialog.ClientSize = columnPickerDialog.ClientSize;
							if (DialogResult.OK == this.UIService.ShowDialog(propertyPageDialog))
							{
								Hashtable hashtable = null;
								if (this.ShowFilter)
								{
									base.Focus();
									hashtable = new Hashtable(base.Columns.Count, StringComparer.InvariantCultureIgnoreCase);
									foreach (object obj in base.Columns)
									{
										ExchangeColumnHeader exchangeColumnHeader = (ExchangeColumnHeader)obj;
										hashtable.Add(exchangeColumnHeader.Name, new object[]
										{
											exchangeColumnHeader.FilterOperator,
											exchangeColumnHeader.FilterValue
										});
									}
								}
								base.BeginUpdate();
								foreach (object obj2 in base.Columns)
								{
									ExchangeColumnHeader exchangeColumnHeader2 = (ExchangeColumnHeader)obj2;
									if (exchangeColumnHeader2.Name != this.SelectionNameProperty)
									{
										exchangeColumnHeader2.Visible = false;
									}
								}
								for (int i = 0; i < columnPickerDialog.DisplayedColumnNames.Count; i++)
								{
									ExchangeColumnHeader exchangeColumnHeader3 = this.AvailableColumns[columnPickerDialog.DisplayedColumnNames[i]];
									if (exchangeColumnHeader3.Name != this.SelectionNameProperty)
									{
										exchangeColumnHeader3.Visible = true;
										if (this.ShowFilter)
										{
											object[] array = (object[])hashtable[exchangeColumnHeader3.Name];
											if (array != null)
											{
												exchangeColumnHeader3.FilterOperator = (FilterOperator)array[0];
												if (!string.IsNullOrEmpty((string)array[1]))
												{
													exchangeColumnHeader3.FilterValue = (string)array[1];
												}
											}
										}
									}
									else
									{
										exchangeColumnHeader3.DisplayIndex = i;
									}
									if (this.ShowFilter)
									{
										this.OnFilterChanged(EventArgs.Empty);
									}
									this.UpdateHeaderSortArrow();
								}
								this.OnColumnsChanged(EventArgs.Empty);
								this.CreateItems();
								base.EndUpdate();
							}
						}
					};
				}
				return this.showColumnPickerCommand;
			}
		}

		// Token: 0x06001471 RID: 5233 RVA: 0x000544F0 File Offset: 0x000526F0
		protected virtual void OnColumnsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DataListView.EventColumnsChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x06001472 RID: 5234 RVA: 0x0005451E File Offset: 0x0005271E
		// (remove) Token: 0x06001473 RID: 5235 RVA: 0x00054531 File Offset: 0x00052731
		public event EventHandler ColumnsChanged
		{
			add
			{
				base.Events.AddHandler(DataListView.EventColumnsChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataListView.EventColumnsChanged, value);
			}
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00054544 File Offset: 0x00052744
		private void availableColumns_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemAdded)
			{
				ExchangeColumnHeader exchangeColumnHeader = this.availableColumns[e.NewIndex];
				exchangeColumnHeader.VisibleChanged += this.column_VisibleChanged;
				this.column_VisibleChanged(exchangeColumnHeader, EventArgs.Empty);
				exchangeColumnHeader.SetDefaultDisplayIndex();
				if (exchangeColumnHeader.Name == this.SelectionNameProperty || exchangeColumnHeader.Default)
				{
					exchangeColumnHeader.Visible = true;
				}
			}
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000545B4 File Offset: 0x000527B4
		private void availableColumns_ListChanging(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.ItemDeleted)
			{
				ExchangeColumnHeader exchangeColumnHeader = this.availableColumns[e.NewIndex];
				exchangeColumnHeader.VisibleChanged -= this.column_VisibleChanged;
				if (base.Columns.Contains(exchangeColumnHeader))
				{
					base.Columns.Remove(exchangeColumnHeader);
				}
			}
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00054608 File Offset: 0x00052808
		private void column_VisibleChanged(object sender, EventArgs e)
		{
			ExchangeColumnHeader exchangeColumnHeader = (ExchangeColumnHeader)sender;
			if (exchangeColumnHeader.Visible && !base.Columns.Contains(exchangeColumnHeader))
			{
				base.Columns.Add(exchangeColumnHeader);
				return;
			}
			if (!exchangeColumnHeader.Visible)
			{
				int width = exchangeColumnHeader.Width;
				base.Columns.Remove(exchangeColumnHeader);
				exchangeColumnHeader.Width = width;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00054662 File Offset: 0x00052862
		// (set) Token: 0x06001478 RID: 5240 RVA: 0x00054669 File Offset: 0x00052869
		public override bool RightToLeftLayout
		{
			get
			{
				return LayoutHelper.CultureInfoIsRightToLeft;
			}
			set
			{
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0005466B File Offset: 0x0005286B
		// (set) Token: 0x0600147A RID: 5242 RVA: 0x00054673 File Offset: 0x00052873
		[DefaultValue(false)]
		public bool AllowRemove
		{
			get
			{
				return this.allowRemove;
			}
			set
			{
				if (this.allowRemove != value)
				{
					this.allowRemove = value;
					this.RemoveCommand.Enabled = this.CanRemove;
				}
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00054698 File Offset: 0x00052898
		[Browsable(false)]
		public bool CanRemove
		{
			get
			{
				bool flag = null == this.DataSource;
				bool flag2 = (this.bindingList != null) ? this.bindingList.AllowRemove : (null != this.list);
				return this.AllowRemove && base.SelectedIndices.Count > 0 && (flag || flag2);
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000546F0 File Offset: 0x000528F0
		private void RemoveSelectedItems()
		{
			if (this.CanRemove)
			{
				int lastIndex = base.SelectedIndices[base.SelectedIndices.Count - 1];
				if (this.list != null)
				{
					this.ListManager.SuspendBinding();
					try
					{
						IList list = this.SelectedObjects;
						for (int i = list.Count - 1; i >= 0; i--)
						{
							int index = this.list.IndexOf(list[i]);
							this.list.RemoveAt(index);
						}
						goto IL_C7;
					}
					finally
					{
						this.ListManager.ResumeBinding();
						this.EnsureItemsCreated();
					}
				}
				base.BeginUpdate();
				try
				{
					for (int j = base.SelectedIndices.Count - 1; j >= 0; j--)
					{
						base.Items.RemoveAt(base.SelectedIndices[j]);
					}
				}
				finally
				{
					base.EndUpdate();
				}
				IL_C7:
				this.UpdateSelectionAfterRemove(lastIndex);
			}
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x000547E8 File Offset: 0x000529E8
		public void UpdateSelectionAfterRemove(int lastIndex)
		{
			if (base.Items.Count > lastIndex)
			{
				base.FocusedItem = base.Items[lastIndex];
			}
			else if (base.Items.Count > 0)
			{
				base.FocusedItem = base.Items[base.Items.Count - 1];
			}
			base.SelectedIndices.Clear();
			if (base.FocusedItem != null)
			{
				base.FocusedItem.Selected = true;
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00054862 File Offset: 0x00052A62
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.ShowFilter && UnsafeNativeMethods.GetKeyState(9) < 0 && (Control.ModifierKeys & Keys.Shift) == Keys.None)
			{
				this.EditFilter(0);
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00054894 File Offset: 0x00052A94
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			bool result = false;
			if (this.Focused)
			{
				if (this.ShowFilter && keyData == (Keys.LButton | Keys.Back | Keys.Shift))
				{
					this.EditFilter(0);
					result = true;
				}
				else
				{
					result = base.ProcessDialogKey(keyData);
				}
			}
			else if (keyData == Keys.Tab)
			{
				base.Focus();
				result = true;
			}
			else if (keyData == (Keys.LButton | Keys.Back | Keys.Shift))
			{
				result = base.Parent.SelectNextControl(this, false, true, true, true);
			}
			return result;
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000548FC File Offset: 0x00052AFC
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			bool flag = false;
			if (this.Focused)
			{
				if (keyData <= Keys.F2)
				{
					if (keyData == Keys.Delete)
					{
						flag = this.ProcessCmdKeyDelete();
						goto IL_50;
					}
					if (keyData == Keys.F2)
					{
						flag = this.ProcessCmdKeyRename();
						goto IL_50;
					}
				}
				else
				{
					if (keyData == Keys.F5)
					{
						flag = this.ProcessCmdKeyRefresh();
						goto IL_50;
					}
					if (keyData == (Keys)131137)
					{
						flag = this.ProcessCmdKeySelectAll();
						goto IL_50;
					}
				}
				flag = false;
				IL_50:
				if (!flag)
				{
					flag = base.ProcessCmdKey(ref msg, keyData);
				}
			}
			return flag;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00054968 File Offset: 0x00052B68
		private bool ProcessCmdKeySelectAll()
		{
			if (base.MultiSelect && base.Items.Count > 0)
			{
				using (new ControlWaitCursor(this))
				{
					base.BeginUpdate();
					try
					{
						NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
						lvitem.mask = 8;
						lvitem.state = 2;
						lvitem.stateMask = 2;
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4139, -1, ref lvitem);
					}
					finally
					{
						base.EndUpdate();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00054A0C File Offset: 0x00052C0C
		private bool ProcessCmdKeyRename()
		{
			if (base.LabelEdit && base.FocusedItem != null)
			{
				base.FocusedItem.BeginEdit();
				return true;
			}
			return false;
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00054A2C File Offset: 0x00052C2C
		private bool ProcessCmdKeyDelete()
		{
			this.Application_Idle(this, EventArgs.Empty);
			bool result = false;
			if (this.DeleteSelectionCommand != null)
			{
				if (this.DeleteSelectionCommand.Enabled && this.HasSelection)
				{
					this.DeleteSelectionCommand.Invoke();
					result = true;
				}
			}
			else if (this.CanRemove)
			{
				this.RemoveCommand.Invoke();
				result = true;
			}
			return result;
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00054A8C File Offset: 0x00052C8C
		private bool ProcessCmdKeyRefresh()
		{
			bool result = false;
			if (this.RefreshCommand != null && this.refreshCommand.Enabled)
			{
				this.RefreshCommand.Invoke();
				result = true;
			}
			return result;
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00054ABE File Offset: 0x00052CBE
		private void ListManager_PositionChanged(object sender, EventArgs e)
		{
			this.OnListManagerPositionChanged(e);
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00054AC8 File Offset: 0x00052CC8
		protected virtual void OnListManagerPositionChanged(EventArgs e)
		{
			if (this.ListManager.Position >= 0 && this.ListManager.Position < base.Items.Count && !this.needCreateItemsForRows && !this.IsDataSourceRefreshing && (!base.VirtualMode || this.ListManager.Count == base.Items.Count))
			{
				this.FocusOnDataRow(this.ListManager.Current);
			}
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00054B3C File Offset: 0x00052D3C
		private void ListManager_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (base.InvokeRequired)
			{
				base.Invoke(new ListChangedEventHandler(this.ListManager_ListChanged), new object[]
				{
					sender,
					e
				});
				return;
			}
			if (!this.isCreatingItems)
			{
				this.OnListManagerListChanged(e);
			}
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00054B84 File Offset: 0x00052D84
		protected virtual void OnListManagerListChanged(ListChangedEventArgs e)
		{
			ExTraceGlobals.ProgramFlowTracer.TraceDebug<ListChangedType, int, int>((long)this.GetHashCode(), "-->DataListView.OnListManagerListChanged:(ChangeType:{0}, NewIndex:{1}, OldIndex:{2})", e.ListChangedType, e.NewIndex, e.OldIndex);
			if (this.AutoGenerateColumns && (e.ListChangedType == ListChangedType.PropertyDescriptorAdded || e.ListChangedType == ListChangedType.PropertyDescriptorChanged || e.ListChangedType == ListChangedType.PropertyDescriptorDeleted))
			{
				ExTraceGlobals.ProgramFlowTracer.TraceDebug((long)this.GetHashCode(), "*--DataListView.OnListManagerListChanged:SchemaChanged");
				base.Columns.Clear();
			}
			if (!base.VirtualMode && e.ListChangedType != ListChangedType.ItemAdded)
			{
				this.BackupOffsetFocusedItem();
			}
			if (e.ListChangedType == ListChangedType.Reset || e.ListChangedType == ListChangedType.ItemDeleted)
			{
				base.SelectedIndices.Clear();
				this.RaiseSelectionChanged();
			}
			this.needCreateItemsForRows = true;
			if (this.swCreateItems == null && this.IsDataSourceRefreshing)
			{
				this.swCreateItems = new Stopwatch();
				this.swCreateItems.Start();
			}
			ExTraceGlobals.ProgramFlowTracer.TraceDebug((long)this.GetHashCode(), "<--DataListView.OnListManagerListChanged:");
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00054C79 File Offset: 0x00052E79
		internal void RaiseSelectionChanged()
		{
			if (base.IsHandleCreated && this.updateShell)
			{
				if (this.isShowingContextMenu)
				{
					UnsafeNativeMethods.EndMenu();
				}
				this.selectedObjects = null;
				this.selectedIdentities = null;
				this.OnSelectionChanged(EventArgs.Empty);
				this.updateShell = false;
			}
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00054CBC File Offset: 0x00052EBC
		private void UpdateNoResultsIndicator()
		{
			if (!string.IsNullOrEmpty(this.NoResultsLabelText))
			{
				if (base.InvokeRequired)
				{
					base.BeginInvoke(new MethodInvoker(this.UpdateNoResultsIndicator));
					return;
				}
				if (this.list != null && this.list.Count > 0)
				{
					this.ShowNoResultsIndicator = false;
				}
				else if (this.DataSourceRefreshed && !this.IsDataSourceRefreshing)
				{
					this.ShowNoResultsIndicator = true;
				}
				else if (this.ShowIndicatorOnceNoResults)
				{
					this.ShowNoResultsIndicator = true;
				}
				base.Invalidate();
			}
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x00054D40 File Offset: 0x00052F40
		public void FocusOnDataRow(object row)
		{
			ListViewItem itemFromRow = this.GetItemFromRow(row);
			if (itemFromRow != null && itemFromRow != base.FocusedItem)
			{
				base.FocusedItem = itemFromRow;
				if (!itemFromRow.Selected)
				{
					base.SelectedIndices.Clear();
					itemFromRow.Selected = true;
				}
				itemFromRow.EnsureVisible();
				this.Application_Idle(this, EventArgs.Empty);
				return;
			}
			if (itemFromRow == null)
			{
				this.SelectItemBySpecifiedIdentity(this.GetRowIdentity(row), false);
			}
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x00054DA8 File Offset: 0x00052FA8
		public object GetRowFromItem(ListViewItem item)
		{
			if (this.item2RowMapping == null)
			{
				return null;
			}
			return ((KeyValuePair<object, object>)this.item2RowMapping[item]).Value;
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00054DD8 File Offset: 0x00052FD8
		public ListViewItem GetItemFromRow(object row)
		{
			object rowIdentity = this.GetRowIdentity(row);
			ListViewItem listViewItem = (rowIdentity != null && this.id2ItemMapping != null) ? ((ListViewItem)this.id2ItemMapping[rowIdentity]) : null;
			if (listViewItem == null && base.VirtualMode && this.list != null)
			{
				int num = this.list.IndexOf(row);
				if (num >= 0 && num < base.Items.Count)
				{
					listViewItem = base.Items[num];
				}
			}
			return listViewItem;
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x00054E50 File Offset: 0x00053050
		public ListViewItem GetItemFromIdentity(object identity)
		{
			int itemIndexFromIdentity = this.GetItemIndexFromIdentity(identity);
			if (itemIndexFromIdentity < 0)
			{
				return null;
			}
			return base.Items[itemIndexFromIdentity];
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x00054E78 File Offset: 0x00053078
		public int GetItemIndexFromIdentity(object identity)
		{
			int i = -1;
			if (identity != null)
			{
				if (base.VirtualMode && this.list != null)
				{
					int num = Math.Min(this.list.Count, base.Items.Count);
					for (i = num - 1; i >= 0; i--)
					{
						if (identity.Equals(this.GetRowIdentity(this.list[i])))
						{
							break;
						}
					}
				}
				else
				{
					ListViewItem listViewItem = (this.id2ItemMapping != null) ? ((ListViewItem)this.id2ItemMapping[identity]) : null;
					i = ((listViewItem != null) ? listViewItem.Index : -1);
				}
			}
			return i;
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00054F0E File Offset: 0x0005310E
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00054F16 File Offset: 0x00053116
		[DefaultValue("")]
		public string NoResultsLabelText
		{
			get
			{
				return this.noResultsIndicatorText;
			}
			set
			{
				this.noResultsIndicatorText = value;
				if (string.IsNullOrEmpty(value) && this.ShowNoResultsIndicator)
				{
					this.ShowNoResultsIndicator = false;
				}
				this.UpdateNoResultsIndicator();
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00054F3C File Offset: 0x0005313C
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00054F44 File Offset: 0x00053144
		[DefaultValue(false)]
		public bool ShowIndicatorOnceNoResults
		{
			get
			{
				return this.showIndicatorOnceNoResults;
			}
			set
			{
				if (this.ShowIndicatorOnceNoResults != value)
				{
					this.showIndicatorOnceNoResults = value;
					this.UpdateNoResultsIndicator();
				}
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00054F5C File Offset: 0x0005315C
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00054F64 File Offset: 0x00053164
		[DefaultValue(false)]
		private bool ShowNoResultsIndicator { get; set; }

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00054F6D File Offset: 0x0005316D
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x00054F78 File Offset: 0x00053178
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(null)]
		public IconLibrary IconLibrary
		{
			get
			{
				return this.iconLibrary;
			}
			set
			{
				this.iconLibrary = value;
				ImageList imageList = base.SmallImageList = ((value != null) ? value.SmallImageList : null);
				if (this.View == View.LargeIcon || this.View == View.Tile)
				{
					imageList = (base.LargeImageList = ((value != null) ? value.LargeImageList : null));
				}
				if (imageList == null)
				{
					this.ImageIndex = -1;
					return;
				}
				if (this.ImageIndex == -1 && imageList.Images.Count > 0)
				{
					this.ImageIndex = 0;
				}
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x00054FF2 File Offset: 0x000531F2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("Use IconLibrary instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new ImageList SmallImageList
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x00054FF9 File Offset: 0x000531F9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use IconLibrary instead.")]
		[Browsable(false)]
		public new ImageList LargeImageList
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x00055000 File Offset: 0x00053200
		[Browsable(false)]
		public Icon SelectedItemIcon
		{
			get
			{
				Icon result = null;
				if (this.IconLibrary != null && base.SelectedIndices.Count > 0)
				{
					ListViewItem firstSelectedItem = this.FirstSelectedItem;
					result = this.IconLibrary.GetIcon(firstSelectedItem.ImageKey, firstSelectedItem.ImageIndex);
				}
				return result;
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00055045 File Offset: 0x00053245
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ListViewItem FirstSelectedItem
		{
			get
			{
				if (base.SelectedIndices.Count <= 0)
				{
					return null;
				}
				return base.Items[base.SelectedIndices[0]];
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0005506E File Offset: 0x0005326E
		private int TopItemIndex
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return -1;
				}
				return (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4135, 0, 0U);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00055097 File Offset: 0x00053297
		private int FocusedItemIndex
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return -1;
				}
				return (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4108, -1, 1U);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x000550C0 File Offset: 0x000532C0
		[Obsolete("Use SelectedIndices instead")]
		public new ListView.SelectedListViewItemCollection SelectedItems
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x000550C3 File Offset: 0x000532C3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int TotalItemsCount
		{
			get
			{
				return base.Items.Count;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x000550D0 File Offset: 0x000532D0
		[Browsable(false)]
		public virtual bool SupportsVirtualMode
		{
			get
			{
				return !this.ShowGroups && this.View != View.Tile;
			}
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x000550E8 File Offset: 0x000532E8
		protected override void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
		{
			base.OnRetrieveVirtualItem(e);
			if (e.Item == null && this.cachedItems != null && this.list != null)
			{
				if (e.ItemIndex >= this.cachedItems.Length && this.cachedItems.Length < base.VirtualListSize)
				{
					Array.Resize<ListViewItem>(ref this.cachedItems, base.VirtualListSize);
				}
				if (this.cachedItems[e.ItemIndex] == null || this.cachedItems[e.ItemIndex].SubItems.Count != base.Columns.Count)
				{
					if (e.ItemIndex < this.list.Count)
					{
						e.Item = (this.cachedItems[e.ItemIndex] = this.CreateListViewItemForRow(this.list[e.ItemIndex]));
						return;
					}
					e.Item = (this.cachedItems[e.ItemIndex] = this.CreateNewListViewItem(null));
					for (int i = base.Columns.Count - e.Item.SubItems.Count; i > 0; i--)
					{
						e.Item.SubItems.Add(string.Empty);
					}
					return;
				}
				else
				{
					e.Item = this.cachedItems[e.ItemIndex];
				}
			}
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00055234 File Offset: 0x00053434
		protected override void OnSearchForVirtualItem(SearchForVirtualItemEventArgs e)
		{
			this.EnsureItemsCreated();
			base.OnSearchForVirtualItem(e);
			if (e.Index < 0 && this.list != null && base.Items.Count > 0)
			{
				int num = e.StartIndex;
				if (num >= base.Items.Count)
				{
					num = 0;
				}
				int num2 = num;
				do
				{
					object obj = this.list[num2];
					if (obj != null)
					{
						PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
						for (int i = 0; i < base.Columns.Count; i++)
						{
							string subItemTextForRow = this.GetSubItemTextForRow(obj, i, properties);
							if (CultureInfo.CurrentCulture.CompareInfo.IsPrefix(subItemTextForRow, e.Text, CompareOptions.IgnoreCase))
							{
								e.Index = num2;
							}
							if (e.Index >= 0 || !e.IncludeSubItemsInSearch)
							{
								break;
							}
						}
					}
					num2++;
					if (num2 >= base.Items.Count)
					{
						num2 = 0;
					}
				}
				while (num2 != num && e.Index < 0);
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00055322 File Offset: 0x00053522
		protected override void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
		{
			base.OnVirtualItemsSelectionRangeChanged(e);
			if (e.IsSelected)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00055340 File Offset: 0x00053540
		internal void ExportListToFile(string fileName, Encoding fileEncoding, char separator)
		{
			this.EnsureItemsCreated();
			using (StreamWriter streamWriter = new StreamWriter(fileName, false, fileEncoding))
			{
				int[] array = new int[base.Columns.Count];
				for (int i = 0; i < base.Columns.Count; i++)
				{
					ColumnHeader columnHeader = base.Columns[i];
					array[columnHeader.DisplayIndex] = i;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (int index in array)
				{
					ColumnHeader columnHeader2 = base.Columns[index];
					stringBuilder.Append(DataListView.EscapeText(columnHeader2.Text));
					stringBuilder.Append(separator);
				}
				if (stringBuilder.Length > 0)
				{
					streamWriter.WriteLine(stringBuilder.ToString(0, stringBuilder.Length - 1));
				}
				else
				{
					streamWriter.WriteLine();
				}
				for (int k = 0; k < base.Items.Count; k++)
				{
					ListViewItem listViewItem = base.Items[k];
					stringBuilder.Length = 0;
					foreach (int index2 in array)
					{
						ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems[index2];
						stringBuilder.Append(DataListView.EscapeText(listViewSubItem.Text));
						stringBuilder.Append(separator);
					}
					if (stringBuilder.Length > 0)
					{
						streamWriter.WriteLine(stringBuilder.ToString(0, stringBuilder.Length - 1));
					}
					else
					{
						streamWriter.WriteLine();
					}
				}
			}
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x000554DC File Offset: 0x000536DC
		private static string EscapeText(string cellText)
		{
			string text = cellText;
			if (!string.IsNullOrEmpty(text))
			{
				text = text.Replace("\"", "\"\"");
				string text2 = ", \v\f\t\r\n\"";
				if (text.IndexOfAny(text2.ToCharArray()) >= 0)
				{
					text = "\"" + text + "\"";
				}
			}
			return text;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0005552B File Offset: 0x0005372B
		BulkEditorAdapter IBulkEditor.BulkEditorAdapter
		{
			get
			{
				if (this.bulkEditorAdapter == null)
				{
					this.bulkEditorAdapter = new DataListViewBulkEditorAdapter(this);
				}
				return this.bulkEditorAdapter;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x00055547 File Offset: 0x00053747
		// (set) Token: 0x060014A8 RID: 5288 RVA: 0x0005554F File Offset: 0x0005374F
		private bool ShowBulkEditIndicator { get; set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00055558 File Offset: 0x00053758
		// (set) Token: 0x060014AA RID: 5290 RVA: 0x00055560 File Offset: 0x00053760
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue("")]
		public string BulkEditingIndicatorText
		{
			get
			{
				return this.bulkEditingIndicatorText;
			}
			set
			{
				this.bulkEditingIndicatorText = value;
				this.ShowBulkEditIndicator = !string.IsNullOrEmpty(this.BulkEditingIndicatorText);
				base.Invalidate();
			}
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00055583 File Offset: 0x00053783
		public void FireDataSourceChanged()
		{
			this.OnDataSourceChanged(EventArgs.Empty);
		}

		// Token: 0x14000083 RID: 131
		// (add) Token: 0x060014AC RID: 5292 RVA: 0x00055590 File Offset: 0x00053790
		// (remove) Token: 0x060014AD RID: 5293 RVA: 0x000555C8 File Offset: 0x000537C8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler<PropertyChangedEventArgs> UserModified;

		// Token: 0x060014AE RID: 5294 RVA: 0x000555FD File Offset: 0x000537FD
		private void OnUserModified(EventArgs e)
		{
			if (this.UserModified != null)
			{
				this.UserModified(this, new PropertyChangedEventArgs("DataSource"));
			}
		}

		// Token: 0x04000748 RID: 1864
		private const int TimeIntervalForCreateItems = 100;

		// Token: 0x04000749 RID: 1865
		private const int DefaultSelectedColumnIndex = -1;

		// Token: 0x0400074A RID: 1866
		private const bool DefaultShowFilter = false;

		// Token: 0x0400074B RID: 1867
		private const int moveUpDelta = -1;

		// Token: 0x0400074C RID: 1868
		private const int moveDownDelta = 1;

		// Token: 0x0400074D RID: 1869
		private bool needCreateItemsForRows;

		// Token: 0x0400074E RID: 1870
		private bool isCreatingItems;

		// Token: 0x0400074F RID: 1871
		private ListViewItem[] cachedItems;

		// Token: 0x04000750 RID: 1872
		private object focusedItemIdentity;

		// Token: 0x04000751 RID: 1873
		private int backupTopItemIndex = -1;

		// Token: 0x04000752 RID: 1874
		private ArrayList backupSelectedIdentities;

		// Token: 0x04000753 RID: 1875
		private Stopwatch swCreateItems;

		// Token: 0x04000754 RID: 1876
		private bool showSubitemIcon;

		// Token: 0x04000755 RID: 1877
		private int selectedColumnIndex = -1;

		// Token: 0x04000756 RID: 1878
		private bool showfilter;

		// Token: 0x04000757 RID: 1879
		private HandleRef headerHandle;

		// Token: 0x04000758 RID: 1880
		private HandleRef tooltipHandle;

		// Token: 0x04000759 RID: 1881
		private FilterValueCollection filterValues;

		// Token: 0x0400075A RID: 1882
		private Command contextMenuCommand;

		// Token: 0x0400075B RID: 1883
		private Command arrangeByCommand;

		// Token: 0x0400075C RID: 1884
		private Command showInGroupsCommand;

		// Token: 0x0400075D RID: 1885
		private Command deleteSelectionCommand;

		// Token: 0x0400075E RID: 1886
		private Command refreshCommand;

		// Token: 0x0400075F RID: 1887
		private Command showSelectionPropertiesCommand;

		// Token: 0x04000760 RID: 1888
		private int imageIndex = -1;

		// Token: 0x04000761 RID: 1889
		private object dataSource;

		// Token: 0x04000762 RID: 1890
		private IRefreshableNotification dataSourceRefresher;

		// Token: 0x04000763 RID: 1891
		private int focusedItemOffset = -1;

		// Token: 0x04000764 RID: 1892
		private string dataMember = string.Empty;

		// Token: 0x04000765 RID: 1893
		private bool inSetListManager;

		// Token: 0x04000766 RID: 1894
		private static readonly object EventDataSourceChanged = new object();

		// Token: 0x04000767 RID: 1895
		private CurrencyManager listManager;

		// Token: 0x04000768 RID: 1896
		private IList list;

		// Token: 0x04000769 RID: 1897
		private IBindingList bindingList;

		// Token: 0x0400076A RID: 1898
		private IBindingListView bindingListView;

		// Token: 0x0400076B RID: 1899
		private IAdvancedBindingListView advancedBindingListView;

		// Token: 0x0400076C RID: 1900
		private bool autoGenerateColumns = true;

		// Token: 0x0400076F RID: 1903
		private static readonly object EventUpdateItem = new object();

		// Token: 0x04000770 RID: 1904
		private string imagePropertyName = "";

		// Token: 0x04000771 RID: 1905
		private IList selectedObjects;

		// Token: 0x04000772 RID: 1906
		private IComparer selectedObjectsSorter;

		// Token: 0x04000773 RID: 1907
		private IList selectedIdentities;

		// Token: 0x04000774 RID: 1908
		private object selectedItemIdentity;

		// Token: 0x04000775 RID: 1909
		private bool hideSortArrow;

		// Token: 0x04000776 RID: 1910
		private string identityProperty = string.Empty;

		// Token: 0x04000777 RID: 1911
		private string selectionNameProperty = string.Empty;

		// Token: 0x04000778 RID: 1912
		private string selectedObjectDetailsTypeProperty = string.Empty;

		// Token: 0x04000779 RID: 1913
		private string sortProperty = "";

		// Token: 0x0400077A RID: 1914
		private ListSortDirection sortDirection;

		// Token: 0x0400077B RID: 1915
		private string groupProperty = "";

		// Token: 0x0400077C RID: 1916
		private ListSortDirection groupDirection;

		// Token: 0x0400077D RID: 1917
		private bool isDraggingNonReorderableColumn;

		// Token: 0x0400077E RID: 1918
		private bool isShowingContextMenu;

		// Token: 0x0400077F RID: 1919
		private bool updateShell;

		// Token: 0x04000781 RID: 1921
		private bool isColumnsWidthDirty;

		// Token: 0x04000782 RID: 1922
		private bool isApplyingSmartWidthOfColumns;

		// Token: 0x04000783 RID: 1923
		private Command showFilterCommand;

		// Token: 0x04000784 RID: 1924
		private Command inlineEditCommand;

		// Token: 0x04000785 RID: 1925
		private Command removeCommand;

		// Token: 0x04000786 RID: 1926
		private Command moveUpCommand;

		// Token: 0x04000787 RID: 1927
		private Command moveDownCommand;

		// Token: 0x04000788 RID: 1928
		private ExchangeColumnHeaderCollection availableColumns;

		// Token: 0x04000789 RID: 1929
		private Command showColumnPickerCommand;

		// Token: 0x0400078A RID: 1930
		private static readonly object EventColumnsChanged = new object();

		// Token: 0x0400078B RID: 1931
		private bool allowRemove;

		// Token: 0x0400078C RID: 1932
		private string noResultsIndicatorText;

		// Token: 0x0400078D RID: 1933
		private bool showIndicatorOnceNoResults;

		// Token: 0x0400078E RID: 1934
		private Hashtable id2ItemMapping;

		// Token: 0x0400078F RID: 1935
		private Hashtable item2RowMapping;

		// Token: 0x04000790 RID: 1936
		private Hashtable itemsStates = new Hashtable();

		// Token: 0x04000791 RID: 1937
		private IconLibrary iconLibrary;

		// Token: 0x04000792 RID: 1938
		private BulkEditorAdapter bulkEditorAdapter;

		// Token: 0x04000793 RID: 1939
		private string bulkEditingIndicatorText;
	}
}
