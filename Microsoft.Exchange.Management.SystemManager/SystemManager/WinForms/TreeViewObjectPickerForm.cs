using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.Management.SystemManager;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000230 RID: 560
	public partial class TreeViewObjectPickerForm : ExchangeForm, ISelectedObjectsProvider
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x00070B10 File Offset: 0x0006ED10
		public TreeViewObjectPickerForm()
		{
			this.InitializeComponent();
			base.Icon = Icons.ObjectPicker;
			this.fileToolStripMenuItem.Text = Strings.ObjectPickerFile;
			this.closeToolStripMenuItem.Text = Strings.ObjectPickerClose;
			this.helpButton.Text = Strings.Help;
			this.helpButton.Visible = false;
			this.cancelButton.Text = Strings.Cancel;
			this.okButton.Text = Strings.Ok;
			base.AcceptButton = this.okButton;
			this.closeToolStripMenuItem.Click += delegate(object param0, EventArgs param1)
			{
				base.DialogResult = DialogResult.Cancel;
			};
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x00070BDC File Offset: 0x0006EDDC
		public TreeViewObjectPickerForm(ObjectPicker objectPicker) : this()
		{
			this.ObjectPicker = objectPicker;
			DataTableLoader dataTableLoader = this.ObjectPicker.DataTableLoader;
			this.resultDataTable = dataTableLoader.Table.Clone();
			this.rootNodes = new BindingList<TreeViewObjectPickerForm.DataTreeNodeModel>();
			this.queryResults = dataTableLoader.Table;
			this.resultTreeView.LazyExpandAll = true;
			this.resultTreeView.ImageList = ObjectPicker.ObjectClassIconLibrary.SmallImageList;
			this.resultTreeView.DataSource = this.rootNodes;
			this.resultTreeView.NodePropertiesMapping.Add("Text", "Name");
			this.resultTreeView.NodePropertiesMapping.Add("ImageKey", "ImageKey");
			this.resultTreeView.NodePropertiesMapping.Add("SelectedImageKey", "ImageKey");
			this.resultTreeView.ChildRelation = "Children";
			base.Load += delegate(object param0, EventArgs param1)
			{
				this.RetriveChildrenNodes(null);
			};
			this.resultTreeView.BeforeExpand += this.resultTreeView_BeforeExpand;
			this.resultTreeView.AfterSelect += this.resultTreeView_AfterSelect;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00070D01 File Offset: 0x0006EF01
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x00070D0C File Offset: 0x0006EF0C
		[DefaultValue(null)]
		protected ObjectPicker ObjectPicker
		{
			get
			{
				return this.objectPicker;
			}
			set
			{
				this.objectPicker = value;
				this.Text = this.ObjectPicker.Caption;
				if (this.ObjectPicker.AllowMultiSelect)
				{
					ExTraceGlobals.DataFlowTracer.TraceDebug((long)this.GetHashCode(), "TreeViewObjectPickerForm only support single selection, ignore the AllowMultiSelect setting of the ObjectPicker");
				}
				this.ObjectPicker.DataTableLoader.RefreshCompleted += this.DataTableLoader_RefreshCompleted;
				this.ObjectPicker.DataTableLoader.ProgressChanged += this.DataTableLoader_ProgressChanged;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00070D8C File Offset: 0x0006EF8C
		public DataTable SelectedObjects
		{
			get
			{
				return this.selectedObjects;
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00070DF6 File Offset: 0x0006EFF6
		private void DataTableLoader_ProgressChanged(object sender, RefreshProgressChangedEventArgs e)
		{
			this.UpdateStatusLabelText();
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00070E00 File Offset: 0x0006F000
		private void DataTableLoader_RefreshCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (ExceptionHelper.IsUICriticalException(e.Error))
			{
				throw new ObjectPickerException(e.Error.Message, e.Error);
			}
			if (!this.ObjectPicker.DataTableLoader.Refreshing)
			{
				if (e.Error != null && !(e.Error is RootObjectNotFoundException))
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
				this.OnQueryCompleted(e);
				this.InProgress = this.ObjectPicker.DataTableLoader.Refreshing;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060019FF RID: 6655 RVA: 0x00070ED5 File Offset: 0x0006F0D5
		// (set) Token: 0x06001A00 RID: 6656 RVA: 0x00070EE0 File Offset: 0x0006F0E0
		private bool InProgress
		{
			get
			{
				return this.inProgress;
			}
			set
			{
				if (this.inProgress != value)
				{
					this.inProgress = value;
					this.loadProgressBar.Visible = this.InProgress;
					this.resultTreeView.UseWaitCursor = this.InProgress;
					if (this.InProgress)
					{
						this.loadStatusLabel.Text = Strings.Searching;
						this.Cursor = Cursors.AppStarting;
						return;
					}
					this.loadStatusLabel.Text = string.Empty;
					this.Cursor = Cursors.Default;
				}
			}
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00070F63 File Offset: 0x0006F163
		private void UpdateStatusLabelText()
		{
			this.loadStatusLabel.Text = Strings.ObjectsFound(this.queryResults.Rows.Count);
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00070F8C File Offset: 0x0006F18C
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			this.selectedObjects = this.resultDataTable.Clone();
			TreeViewObjectPickerForm.DataTreeNodeModel dataTreeNodeModel = this.resultTreeView.SelectedObject as TreeViewObjectPickerForm.DataTreeNodeModel;
			if (dataTreeNodeModel != null)
			{
				this.selectedObjects.Rows.Add(dataTreeNodeModel.InnerDataRow.ItemArray);
			}
			this.ObjectPicker.DataTableLoader.CancelRefresh();
			this.queryResults.Clear();
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00070FFC File Offset: 0x0006F1FC
		private void resultTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (this.ObjectPicker.DataTableLoader.Refreshing)
			{
				e.Cancel = true;
				return;
			}
			DataTreeNode dataTreeNode = e.Node as DataTreeNode;
			TreeViewObjectPickerForm.DataTreeNodeModel dataTreeNodeModel = dataTreeNode.DataSource as TreeViewObjectPickerForm.DataTreeNodeModel;
			if (!dataTreeNodeModel.IsChildrenReady)
			{
				e.Cancel = true;
				this.RetriveChildrenNodes(dataTreeNode);
			}
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00071054 File Offset: 0x0006F254
		private void RetriveChildrenNodes(DataTreeNode node)
		{
			this.nodeToExpand = node;
			object rootId = null;
			if (this.nodeToExpand != null)
			{
				rootId = (this.nodeToExpand.DataSource as TreeViewObjectPickerForm.DataTreeNodeModel).InnerDataRow;
			}
			this.ObjectPicker.PerformQuery(rootId, string.Empty);
			this.InProgress = true;
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000710A0 File Offset: 0x0006F2A0
		private void OnQueryCompleted(RunWorkerCompletedEventArgs e)
		{
			this.resultTreeView.Select();
			TreeViewObjectPickerForm.DataTreeNodeModel parent = (this.nodeToExpand != null) ? (this.nodeToExpand.DataSource as TreeViewObjectPickerForm.DataTreeNodeModel) : null;
			this.CreateTreeNodes(parent);
			if (this.nodeToExpand != null)
			{
				this.nodeToExpand.Expand();
				return;
			}
			if (this.resultTreeView.Nodes.Count > 0)
			{
				this.resultTreeView.SelectedNode = this.resultTreeView.Nodes[0];
				this.resultTreeView.SelectedNode.Expand();
			}
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00071130 File Offset: 0x0006F330
		private void CreateTreeNodes(TreeViewObjectPickerForm.DataTreeNodeModel parent)
		{
			this.resultDataTable.Merge(this.queryResults);
			BindingList<TreeViewObjectPickerForm.DataTreeNodeModel> bindingList = (parent == null) ? this.rootNodes : parent.Children;
			foreach (object obj in this.queryResults.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = this.resultDataTable.Rows.Find(dataRow[this.queryResults.PrimaryKey[0]]);
				if (this.ObjectPicker.ObjectPickerProfile == null || !(this.ObjectPicker.ObjectPickerProfile.Scope is DataRow) || !object.Equals(dataRow2[this.queryResults.PrimaryKey[0].ColumnName] as ADObjectId, (this.ObjectPicker.ObjectPickerProfile.Scope as DataRow)[this.queryResults.PrimaryKey[0].ColumnName]))
				{
					bindingList.Add(new TreeViewObjectPickerForm.DataTreeNodeModel(dataRow2, this.ObjectPicker));
				}
			}
			if (parent != null)
			{
				parent.IsChildrenReady = true;
			}
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x00071268 File Offset: 0x0006F468
		private void resultTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (!this.ObjectPicker.CanSelectRootObject && this.resultTreeView.SelectedNode.Parent == null)
			{
				this.okButton.Enabled = false;
				this.selectedCountLabel.Text = Strings.CannotSelectRootObject;
				return;
			}
			int num = (this.resultTreeView.SelectedNode != null) ? 1 : 0;
			this.okButton.Enabled = (num == 1);
			this.selectedCountLabel.Text = Strings.ObjectsSelected(num);
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x00071AF6 File Offset: 0x0006FCF6
		protected override void OnHelpRequested(HelpEventArgs hevent)
		{
			if (!hevent.Handled)
			{
				ExchangeHelpService.ShowHelpFromHelpTopicId(this, this.ObjectPicker.HelpTopic);
				hevent.Handled = true;
			}
			base.OnHelpRequested(hevent);
		}

		// Token: 0x040009C0 RID: 2496
		private DataTable queryResults;

		// Token: 0x040009C1 RID: 2497
		private DataTable resultDataTable;

		// Token: 0x040009C2 RID: 2498
		private BindingList<TreeViewObjectPickerForm.DataTreeNodeModel> rootNodes;

		// Token: 0x040009C3 RID: 2499
		private ObjectPicker objectPicker;

		// Token: 0x040009C4 RID: 2500
		private DataTable selectedObjects;

		// Token: 0x040009C5 RID: 2501
		private bool inProgress;

		// Token: 0x040009C6 RID: 2502
		private DataTreeNode nodeToExpand;

		// Token: 0x02000231 RID: 561
		internal class DataTreeNodeModel
		{
			// Token: 0x06001A0D RID: 6669 RVA: 0x00071B1F File Offset: 0x0006FD1F
			public DataTreeNodeModel(DataRow dataRow, ObjectPicker objectPicker)
			{
				this.InnerDataRow = dataRow;
				this.ObjectPicker = objectPicker;
			}

			// Token: 0x17000627 RID: 1575
			// (get) Token: 0x06001A0E RID: 6670 RVA: 0x00071B40 File Offset: 0x0006FD40
			// (set) Token: 0x06001A0F RID: 6671 RVA: 0x00071B48 File Offset: 0x0006FD48
			public DataRow InnerDataRow { get; private set; }

			// Token: 0x17000628 RID: 1576
			// (get) Token: 0x06001A10 RID: 6672 RVA: 0x00071B51 File Offset: 0x0006FD51
			// (set) Token: 0x06001A11 RID: 6673 RVA: 0x00071B59 File Offset: 0x0006FD59
			public ObjectPicker ObjectPicker { get; private set; }

			// Token: 0x17000629 RID: 1577
			// (get) Token: 0x06001A12 RID: 6674 RVA: 0x00071B64 File Offset: 0x0006FD64
			public string Name
			{
				get
				{
					string result = string.Empty;
					if (this.InnerDataRow.Table.Columns.Contains("Type") && string.Compare((string)this.InnerDataRow["Type"], "Domain", true, CultureInfo.InvariantCulture) == 0 && this.InnerDataRow["CanonicalName"] != DBNull.Value)
					{
						result = this.InnerDataRow["CanonicalName"].ToString().TrimEnd(new char[]
						{
							'/'
						});
					}
					else
					{
						result = this.InnerDataRow[this.ObjectPicker.NameProperty].ToString();
					}
					return result;
				}
			}

			// Token: 0x1700062A RID: 1578
			// (get) Token: 0x06001A13 RID: 6675 RVA: 0x00071C18 File Offset: 0x0006FE18
			public object ImageKey
			{
				get
				{
					return this.InnerDataRow[this.ObjectPicker.ImageProperty];
				}
			}

			// Token: 0x1700062B RID: 1579
			// (get) Token: 0x06001A14 RID: 6676 RVA: 0x00071C30 File Offset: 0x0006FE30
			public BindingList<TreeViewObjectPickerForm.DataTreeNodeModel> Children
			{
				get
				{
					return this.children;
				}
			}

			// Token: 0x1700062C RID: 1580
			// (get) Token: 0x06001A15 RID: 6677 RVA: 0x00071C38 File Offset: 0x0006FE38
			// (set) Token: 0x06001A16 RID: 6678 RVA: 0x00071C40 File Offset: 0x0006FE40
			public bool IsChildrenReady { get; set; }

			// Token: 0x040009C7 RID: 2503
			private BindingList<TreeViewObjectPickerForm.DataTreeNodeModel> children = new BindingList<TreeViewObjectPickerForm.DataTreeNodeModel>();
		}
	}
}
