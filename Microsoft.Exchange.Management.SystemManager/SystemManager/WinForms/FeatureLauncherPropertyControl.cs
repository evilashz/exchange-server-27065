using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001EC RID: 492
	public class FeatureLauncherPropertyControl : ExchangeUserControl, IFeatureLauncherBulkEditSupport, IBulkEditSupport, IGetInnerObject, ISpecifyPropertyState
	{
		// Token: 0x06001608 RID: 5640 RVA: 0x0005ABEC File Offset: 0x00058DEC
		public FeatureLauncherPropertyControl()
		{
			this.InitializeComponent();
			this.iconLibrary = new IconLibrary();
			this.iconLibrary.Icons.Add(Strings.PropertiesButtonText, Icons.Properties);
			this.iconLibrary.Icons.Add(Strings.EnableButtonText, Icons.Enable);
			this.iconLibrary.Icons.Add(Strings.DisableButtonText, Icons.Disable);
			this.featureListView.IconLibrary = this.iconLibrary;
			this.enableButton.Text = Strings.EnableButtonText;
			this.enableButton.ToolTipText = ExchangeUserControl.RemoveAccelerator(Strings.EnableButtonText);
			this.disableButton.Text = Strings.DisableButtonText;
			this.disableButton.ToolTipText = ExchangeUserControl.RemoveAccelerator(Strings.DisableButtonText);
			this.propertiesButton.Text = Strings.PropertiesButtonText;
			this.propertiesButton.ToolTipText = ExchangeUserControl.RemoveAccelerator(Strings.PropertiesButtonText);
			this.itemDescriptionDividerLabel.Text = Strings.DescriptionLabelText;
			this.featureColumnHeader.Text = Strings.FeatureColumnText;
			this.featureColumnHeader.Default = true;
			this.statusColumnHeader.Text = Strings.StatusColumnText;
			this.statusColumnHeader.Default = true;
			this.itemDescriptionLabel.Text = Strings.DefaultItemDescriptionText;
			this.InfoLabelText = string.Empty;
			this.buttonsToolStrip.ImageList = this.iconLibrary.SmallImageList;
			this.propertiesButton.ImageKey = Strings.PropertiesButtonText;
			this.propertiesButton.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.enableButton.ImageKey = Strings.EnableButtonText;
			this.enableButton.TextImageRelation = TextImageRelation.ImageBeforeText;
			this.disableButton.ImageKey = Strings.DisableButtonText;
			this.disableButton.TextImageRelation = TextImageRelation.ImageBeforeText;
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0005AE0C File Offset: 0x0005900C
		private ExchangePage ParentPage
		{
			get
			{
				for (Control parent = base.Parent; parent != null; parent = parent.Parent)
				{
					ExchangePage exchangePage = parent as ExchangePage;
					if (exchangePage != null)
					{
						return exchangePage;
					}
				}
				return null;
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0005AE3C File Offset: 0x0005903C
		public void SetBinding(BindingSource bindingSource)
		{
			if (!object.ReferenceEquals(this.dataSource, bindingSource))
			{
				if (this.dataSource != null)
				{
					foreach (object obj in this.FeatureListView.Items)
					{
						FeatureLauncherListViewItem featureLauncherListViewItem = (FeatureLauncherListViewItem)obj;
						if (!string.IsNullOrEmpty(featureLauncherListViewItem.StatusPropertyName) && base.DataBindings[featureLauncherListViewItem.StatusBindingName] != null)
						{
							base.DataBindings[featureLauncherListViewItem.StatusBindingName].Format -= this.Binding_Format;
							base.DataBindings[featureLauncherListViewItem.StatusBindingName].Parse -= this.Binding_Parse;
							base.DataBindings.Remove(base.DataBindings[featureLauncherListViewItem.StatusBindingName]);
						}
					}
				}
				this.dataSource = bindingSource;
				if (this.dataSource != null)
				{
					foreach (object obj2 in this.FeatureListView.Items)
					{
						FeatureLauncherListViewItem featureLauncherListViewItem2 = (FeatureLauncherListViewItem)obj2;
						if (!string.IsNullOrEmpty(featureLauncherListViewItem2.StatusPropertyName))
						{
							Binding binding = new Binding(featureLauncherListViewItem2.StatusBindingName, bindingSource, featureLauncherListViewItem2.StatusPropertyName, true, featureLauncherListViewItem2.DataSourceUpdateMode);
							binding.Format += this.Binding_Format;
							binding.Parse += this.Binding_Parse;
							base.DataBindings.Add(binding);
						}
					}
				}
			}
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0005AFF0 File Offset: 0x000591F0
		private void Binding_Parse(object sender, ConvertEventArgs e)
		{
			switch ((FeatureStatus)e.Value)
			{
			case FeatureStatus.Enabled:
				e.Value = true;
				return;
			case FeatureStatus.Disabled:
				e.Value = false;
				return;
			default:
				e.Value = DBNull.Value;
				return;
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0005B044 File Offset: 0x00059244
		private void Binding_Format(object sender, ConvertEventArgs e)
		{
			FeatureStatus featureStatus = FeatureStatus.Unknown;
			if (e.Value != null && typeof(bool).IsAssignableFrom(e.Value.GetType()))
			{
				featureStatus = (((bool)e.Value) ? FeatureStatus.Enabled : FeatureStatus.Disabled);
			}
			e.Value = featureStatus;
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0005B095 File Offset: 0x00059295
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.iconLibrary.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0005B0B4 File Offset: 0x000592B4
		private void InitializeComponent()
		{
			this.featureListView = new FeatureLauncherDataListView();
			this.featureColumnHeader = new ExchangeColumnHeader();
			this.statusColumnHeader = new ExchangeColumnHeader();
			this.itemDescriptionDividerLabel = new AutoHeightLabel();
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.itemDescriptionLabel = new Label();
			this.buttonsToolStrip = new TabbableToolStrip();
			this.propertiesButton = new ToolStripButton();
			this.enableButton = new ToolStripButton();
			this.disableButton = new ToolStripButton();
			this.infoLabel = new Label();
			this.tableLayoutPanel.SuspendLayout();
			this.buttonsToolStrip.SuspendLayout();
			base.SuspendLayout();
			this.featureListView.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.featureListView.AvailableColumns.AddRange(new ExchangeColumnHeader[]
			{
				this.featureColumnHeader,
				this.statusColumnHeader
			});
			this.featureListView.HideSelection = false;
			this.featureListView.Location = new Point(3, 38);
			this.featureListView.Margin = new Padding(3, 3, 0, 0);
			this.featureListView.MultiSelect = false;
			this.featureListView.Name = "featureListView";
			this.featureListView.Size = new Size(315, 184);
			this.featureListView.TabIndex = 1;
			this.featureListView.UseCompatibleStateImageBehavior = false;
			this.featureListView.ItemActivate += this.featureListView_ItemActivate;
			this.featureListView.ColumnClick += this.featureListView_ColumnClick;
			this.featureListView.SelectedIndexChanged += delegate(object param0, EventArgs param1)
			{
				this.UpdateStatusWhenSelectionChanged();
			};
			this.itemDescriptionDividerLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.itemDescriptionDividerLabel.Location = new Point(0, 234);
			this.itemDescriptionDividerLabel.Margin = new Padding(0, 12, 0, 0);
			this.itemDescriptionDividerLabel.Name = "itemDescriptionDividerLabel";
			this.itemDescriptionDividerLabel.ShowDivider = true;
			this.itemDescriptionDividerLabel.Size = new Size(318, 16);
			this.itemDescriptionDividerLabel.TabIndex = 2;
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.featureListView, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.itemDescriptionDividerLabel, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.itemDescriptionLabel, 0, 4);
			this.tableLayoutPanel.Controls.Add(this.buttonsToolStrip, 0, 1);
			this.tableLayoutPanel.Controls.Add(this.infoLabel, 0, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 5;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(318, 271);
			this.tableLayoutPanel.TabIndex = 0;
			this.itemDescriptionLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.itemDescriptionLabel.AutoSize = true;
			this.itemDescriptionLabel.Location = new Point(0, 258);
			this.itemDescriptionLabel.Margin = new Padding(0, 8, 0, 0);
			this.itemDescriptionLabel.Name = "itemDescriptionLabel";
			this.itemDescriptionLabel.Size = new Size(318, 13);
			this.itemDescriptionLabel.TabIndex = 3;
			this.itemDescriptionLabel.Text = "itemDescriptionLabel";
			this.itemDescriptionLabel.TextChanged += this.DescriptionLabelTextChanged;
			this.buttonsToolStrip.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.buttonsToolStrip.Dock = DockStyle.None;
			this.buttonsToolStrip.Items.AddRange(new ToolStripItem[]
			{
				this.propertiesButton,
				this.enableButton,
				this.disableButton
			});
			this.buttonsToolStrip.BackColor = Color.Transparent;
			this.buttonsToolStrip.LayoutStyle = ToolStripLayoutStyle.Flow;
			this.buttonsToolStrip.Location = new Point(16, 28);
			this.buttonsToolStrip.Margin = new Padding(3, 3, 0, 0);
			this.buttonsToolStrip.Name = "buttonsToolStrip";
			this.buttonsToolStrip.Size = new Size(386, 20);
			this.buttonsToolStrip.Stretch = true;
			this.buttonsToolStrip.TabIndex = 0;
			this.buttonsToolStrip.TabStop = true;
			this.buttonsToolStrip.Text = "buttonsToolStrip";
			this.propertiesButton.Enabled = false;
			this.propertiesButton.ImageTransparentColor = Color.Magenta;
			this.propertiesButton.Name = "propertiesButton";
			this.propertiesButton.Size = new Size(64, 19);
			this.propertiesButton.Text = "properties";
			this.propertiesButton.Click += this.PropertiesButton_Click;
			this.enableButton.Enabled = false;
			this.enableButton.ImageTransparentColor = Color.Magenta;
			this.enableButton.Name = "enableButton";
			this.enableButton.Size = new Size(46, 19);
			this.enableButton.Text = "enable";
			this.enableButton.Click += this.EnableButton_Click;
			this.disableButton.Enabled = false;
			this.disableButton.ImageTransparentColor = Color.Magenta;
			this.disableButton.Name = "disableButton";
			this.disableButton.Size = new Size(48, 19);
			this.disableButton.Text = "disable";
			this.disableButton.Click += this.DisableButton_Click;
			this.infoLabel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.infoLabel.AutoSize = true;
			this.infoLabel.Location = new Point(0, 0);
			this.infoLabel.Margin = new Padding(0);
			this.infoLabel.Name = "infoLabel";
			this.infoLabel.Size = new Size(318, 13);
			this.infoLabel.TabIndex = 4;
			this.infoLabel.Text = "infoLabel";
			this.infoLabel.Visible = false;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "FeatureLauncherPropertyControl";
			base.Size = new Size(318, 279);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.buttonsToolStrip.ResumeLayout(false);
			this.buttonsToolStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0005B821 File Offset: 0x00059A21
		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize.Width = base.Width;
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0005B83C File Offset: 0x00059A3C
		// (set) Token: 0x06001611 RID: 5649 RVA: 0x0005B844 File Offset: 0x00059A44
		[DefaultValue(true)]
		public bool EnablingButtonsVisible
		{
			get
			{
				return this.enablingButtonsVisible;
			}
			set
			{
				if (value != this.EnablingButtonsVisible)
				{
					this.enablingButtonsVisible = value;
					this.UpdateControlVisible();
				}
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001612 RID: 5650 RVA: 0x0005B85C File Offset: 0x00059A5C
		// (set) Token: 0x06001613 RID: 5651 RVA: 0x0005B864 File Offset: 0x00059A64
		[DefaultValue(true)]
		public bool PropertiesButtonVisible
		{
			get
			{
				return this.propertiesButtonVisible;
			}
			set
			{
				if (value != this.PropertiesButtonVisible)
				{
					this.propertiesButtonVisible = value;
					this.UpdateControlVisible();
				}
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x0005B87C File Offset: 0x00059A7C
		// (set) Token: 0x06001615 RID: 5653 RVA: 0x0005B889 File Offset: 0x00059A89
		[DefaultValue("")]
		public string InfoLabelText
		{
			get
			{
				return this.infoLabel.Text;
			}
			set
			{
				if (value != this.InfoLabelText)
				{
					this.infoLabel.Text = value;
					this.UpdateControlVisible();
				}
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x0005B8AB File Offset: 0x00059AAB
		// (set) Token: 0x06001617 RID: 5655 RVA: 0x0005B8B3 File Offset: 0x00059AB3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected FeatureLauncherListViewItem CurrentFeature
		{
			get
			{
				return this.currentFeature;
			}
			set
			{
				this.currentFeature = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001618 RID: 5656 RVA: 0x0005B8BC File Offset: 0x00059ABC
		// (set) Token: 0x06001619 RID: 5657 RVA: 0x0005B8C9 File Offset: 0x00059AC9
		[DefaultValue(ColumnHeaderStyle.Clickable)]
		public ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return this.featureListView.HeaderStyle;
			}
			set
			{
				this.featureListView.HeaderStyle = value;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600161A RID: 5658 RVA: 0x0005B8D7 File Offset: 0x00059AD7
		// (set) Token: 0x0600161B RID: 5659 RVA: 0x0005B8E4 File Offset: 0x00059AE4
		[DefaultValue(184)]
		public int FeatureListViewHeight
		{
			get
			{
				return this.featureListView.Height;
			}
			set
			{
				this.featureListView.Height = value;
			}
		}

		// Token: 0x17000531 RID: 1329
		public FeatureLauncherListViewItem this[string featureName]
		{
			get
			{
				return (FeatureLauncherListViewItem)this.featureListView.Items[featureName];
			}
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x0005B90C File Offset: 0x00059B0C
		public void Add(FeatureLauncherListViewItem item)
		{
			this.featureListView.Items.Add(item);
			if (item.Icon != null)
			{
				this.iconLibrary.Icons.Add(item.Text, item.Icon);
				item.ImageIndex = this.iconLibrary.Icons.Count - 1;
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0005B968 File Offset: 0x00059B68
		protected void RefreshList()
		{
			this.featureListView.BeginUpdate();
			this.featureListView.Sort();
			this.featureListView.Focus();
			if (this.CurrentFeature != null && this.CurrentFeature.Index >= 0)
			{
				this.featureListView.EnsureVisible(this.CurrentFeature.Index);
			}
			this.featureListView.EndUpdate();
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0005B9CE File Offset: 0x00059BCE
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			this.RefreshList();
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0005B9DD File Offset: 0x00059BDD
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			this.UpdateControlVisible();
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0005B9EC File Offset: 0x00059BEC
		private void UpdateControlVisible()
		{
			if (base.IsHandleCreated)
			{
				base.SuspendLayout();
				this.infoLabel.Visible = !string.IsNullOrEmpty(this.InfoLabelText);
				this.propertiesButton.Visible = this.PropertiesButtonVisible;
				this.enableButton.Visible = this.EnablingButtonsVisible;
				this.disableButton.Visible = this.EnablingButtonsVisible;
				this.featureListView.View = (this.EnablingButtonsVisible ? View.Details : View.List);
				base.ResumeLayout(false);
				base.PerformLayout();
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x0005BA78 File Offset: 0x00059C78
		protected virtual void UpdateStatusWhenSelectionChanged()
		{
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			if (this.featureListView.SelectedIndices.Count > 0)
			{
				this.CurrentFeature = (FeatureLauncherListViewItem)this.featureListView.FirstSelectedItem;
				string text = this.CurrentFeature.Description;
				bool flag = true;
				if (this.CurrentFeature.IsBanned)
				{
					flag = false;
					text = this.currentFeature.BannedMessage;
				}
				if (flag)
				{
					if (this.CurrentFeature.CanChangeStatus)
					{
						this.enableButton.Enabled = (FeatureStatus.Disabled == this.CurrentFeature.Status);
						this.disableButton.Enabled = (FeatureStatus.Enabled == this.CurrentFeature.Status);
					}
					this.SetPropertyButtonEnabled();
				}
				else
				{
					this.enableButton.Enabled = false;
					this.disableButton.Enabled = false;
					this.propertiesButton.Enabled = false;
				}
				this.itemDescriptionLabel.Text = text;
			}
			else
			{
				this.CurrentFeature = null;
				this.itemDescriptionLabel.Text = Strings.DefaultItemDescriptionText;
				this.enableButton.Enabled = false;
				this.disableButton.Enabled = false;
				this.propertiesButton.Enabled = false;
			}
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
			this.OnFeatureItemUpdated(EventArgs.Empty);
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x0005BBD6 File Offset: 0x00059DD6
		private void DescriptionLabelTextChanged(object sender, EventArgs args)
		{
			this.itemDescriptionLabel.Visible = !string.IsNullOrEmpty(this.itemDescriptionLabel.Text);
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x0005BBF8 File Offset: 0x00059DF8
		private void SetPropertyButtonEnabled()
		{
			this.propertiesButton.Enabled = (null != this.CurrentFeature.PropertyPageControl && (!this.CurrentFeature.EnablePropertiesButtonOnFeatureStatus || FeatureStatus.Enabled == this.CurrentFeature.Status));
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x0005BC44 File Offset: 0x00059E44
		private void EnableButton_Click(object sender, EventArgs e)
		{
			this.SetCurrentFeatureStatus(FeatureStatus.Enabled);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x0005BC4D File Offset: 0x00059E4D
		private void DisableButton_Click(object sender, EventArgs e)
		{
			this.SetCurrentFeatureStatus(FeatureStatus.Disabled);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x0005BC58 File Offset: 0x00059E58
		private void SetCurrentFeatureStatus(FeatureStatus status)
		{
			this.CurrentFeature.Status = status;
			bool flag = FeatureStatus.Enabled == status;
			this.enableButton.Enabled = !flag;
			this.disableButton.Enabled = flag;
			this.SetPropertyButtonEnabled();
			this.featureListView.Sort();
			base.NotifyExposedPropertyIsModified(this.CurrentFeature.StatusBindingName);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x0005BCB4 File Offset: 0x00059EB4
		private void PropertiesButton_Click(object sender, EventArgs e)
		{
			ExchangePropertyPageControl exchangePropertyPageControl = (ExchangePropertyPageControl)Activator.CreateInstance(this.CurrentFeature.PropertyPageControl);
			exchangePropertyPageControl.Context = new DataContext(AutomatedNestedDataHandler.CreateDataHandlerWithParentSchema(this.ParentPage.Context));
			this.ParentPage.ShowDialog(exchangePropertyPageControl);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x0005BCFF File Offset: 0x00059EFF
		private void featureListView_ItemActivate(object sender, EventArgs e)
		{
			if (this.propertiesButton.Enabled)
			{
				this.PropertiesButton_Click(sender, e);
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0005BD18 File Offset: 0x00059F18
		private void featureListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (this.featureListComparer == null)
			{
				this.featureListComparer = new FeatureLauncherPropertyControl.FeatureListItemComparer(e.Column, ListSortDirection.Ascending);
				this.featureListView.ListViewItemSorter = this.featureListComparer;
				return;
			}
			if (e.Column == this.featureListComparer.SortColumn)
			{
				if (this.featureListComparer.SortDirection == ListSortDirection.Ascending)
				{
					this.featureListComparer.SortDirection = ListSortDirection.Descending;
				}
				else
				{
					this.featureListComparer.SortDirection = ListSortDirection.Ascending;
				}
			}
			else
			{
				this.featureListComparer.SortColumn = e.Column;
			}
			this.featureListView.Sort();
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600162B RID: 5675 RVA: 0x0005BDA9 File Offset: 0x00059FA9
		internal DataListView FeatureListView
		{
			get
			{
				return this.featureListView;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x0005BDB1 File Offset: 0x00059FB1
		internal ToolStripButton PropertiesButton
		{
			get
			{
				return this.propertiesButton;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600162D RID: 5677 RVA: 0x0005BDB9 File Offset: 0x00059FB9
		internal ToolStripButton EnableButton
		{
			get
			{
				return this.enableButton;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600162E RID: 5678 RVA: 0x0005BDC1 File Offset: 0x00059FC1
		internal ToolStripButton DisableButton
		{
			get
			{
				return this.disableButton;
			}
		}

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x0600162F RID: 5679 RVA: 0x0005BDCC File Offset: 0x00059FCC
		// (remove) Token: 0x06001630 RID: 5680 RVA: 0x0005BE04 File Offset: 0x0005A004
		public event EventHandler FeatureItemUpdated;

		// Token: 0x06001631 RID: 5681 RVA: 0x0005BE39 File Offset: 0x0005A039
		private void OnFeatureItemUpdated(EventArgs e)
		{
			if (this.FeatureItemUpdated != null)
			{
				this.FeatureItemUpdated(this, e);
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0005BE50 File Offset: 0x0005A050
		protected override BulkEditorAdapter CreateBulkEditorAdapter()
		{
			return new FeatureLauncherBulkEditorAdapter(this);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0005BE58 File Offset: 0x0005A058
		private FeatureLauncherListViewItem GetFeatureListItemByPropertyName(string propertyName)
		{
			foreach (object obj in this.featureListView.Items)
			{
				FeatureLauncherListViewItem featureLauncherListViewItem = (FeatureLauncherListViewItem)obj;
				if (featureLauncherListViewItem.StatusBindingName.Equals(propertyName))
				{
					return featureLauncherListViewItem;
				}
			}
			return null;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0005BEC4 File Offset: 0x0005A0C4
		void ISpecifyPropertyState.SetPropertyState(string propertyName, PropertyState state, string message)
		{
			FeatureLauncherListViewItem featureListItemByPropertyName = this.GetFeatureListItemByPropertyName(propertyName);
			featureListItemByPropertyName.IsBanned = (state == PropertyState.UnsupportedVersion);
			featureListItemByPropertyName.BannedMessage = message;
			this.UpdateStatusWhenSelectionChanged();
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0005BEF0 File Offset: 0x0005A0F0
		object IGetInnerObject.GetObject(string identity)
		{
			return this.GetFeatureListItemByUniqueName(identity);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0005BEFC File Offset: 0x0005A0FC
		public override PropertyDescriptorCollection GetCustomProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (object obj in this.featureListView.Items)
			{
				FeatureLauncherListViewItem featureLauncherListViewItem = (FeatureLauncherListViewItem)obj;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(featureLauncherListViewItem)["Status"];
				propertyDescriptorCollection.Add(new DynamicPropertyDescriptor(base.GetType(), featureLauncherListViewItem.UniqueName, propertyDescriptor));
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0005BF88 File Offset: 0x0005A188
		private FeatureLauncherListViewItem GetFeatureListItemByUniqueName(string identity)
		{
			foreach (object obj in this.featureListView.Items)
			{
				FeatureLauncherListViewItem featureLauncherListViewItem = (FeatureLauncherListViewItem)obj;
				if (featureLauncherListViewItem.UniqueName.Equals(identity))
				{
					return featureLauncherListViewItem;
				}
			}
			return null;
		}

		// Token: 0x04000804 RID: 2052
		public const bool DefaultEnablingButtonsVisible = true;

		// Token: 0x04000805 RID: 2053
		public const bool DefaultPropertiesButtonVisible = true;

		// Token: 0x04000806 RID: 2054
		private const int DefaultFeatureListViewHeight = 184;

		// Token: 0x04000807 RID: 2055
		private DataListView featureListView;

		// Token: 0x04000808 RID: 2056
		private AutoHeightLabel itemDescriptionDividerLabel;

		// Token: 0x04000809 RID: 2057
		private ExchangeColumnHeader featureColumnHeader;

		// Token: 0x0400080A RID: 2058
		private ExchangeColumnHeader statusColumnHeader;

		// Token: 0x0400080B RID: 2059
		private FeatureLauncherPropertyControl.FeatureListItemComparer featureListComparer;

		// Token: 0x0400080C RID: 2060
		private bool enablingButtonsVisible = true;

		// Token: 0x0400080D RID: 2061
		private bool propertiesButtonVisible = true;

		// Token: 0x0400080E RID: 2062
		private FeatureLauncherListViewItem currentFeature;

		// Token: 0x0400080F RID: 2063
		private Label infoLabel;

		// Token: 0x04000810 RID: 2064
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x04000811 RID: 2065
		private Label itemDescriptionLabel;

		// Token: 0x04000812 RID: 2066
		private IconLibrary iconLibrary;

		// Token: 0x04000813 RID: 2067
		private TabbableToolStrip buttonsToolStrip;

		// Token: 0x04000814 RID: 2068
		private ToolStripButton propertiesButton;

		// Token: 0x04000815 RID: 2069
		private ToolStripButton enableButton;

		// Token: 0x04000816 RID: 2070
		private ToolStripButton disableButton;

		// Token: 0x04000817 RID: 2071
		private BindingSource dataSource;

		// Token: 0x020001ED RID: 493
		private class FeatureListItemComparer : IComparer
		{
			// Token: 0x06001639 RID: 5689 RVA: 0x0005BFF4 File Offset: 0x0005A1F4
			public FeatureListItemComparer(int sortColumn, ListSortDirection sortDirection)
			{
				this.sortColumn = sortColumn;
				this.sortDirection = sortDirection;
			}

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x0600163A RID: 5690 RVA: 0x0005C00A File Offset: 0x0005A20A
			// (set) Token: 0x0600163B RID: 5691 RVA: 0x0005C012 File Offset: 0x0005A212
			public int SortColumn
			{
				get
				{
					return this.sortColumn;
				}
				set
				{
					this.sortColumn = value;
				}
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x0600163C RID: 5692 RVA: 0x0005C01B File Offset: 0x0005A21B
			// (set) Token: 0x0600163D RID: 5693 RVA: 0x0005C023 File Offset: 0x0005A223
			public ListSortDirection SortDirection
			{
				get
				{
					return this.sortDirection;
				}
				set
				{
					this.sortDirection = value;
				}
			}

			// Token: 0x0600163E RID: 5694 RVA: 0x0005C02C File Offset: 0x0005A22C
			public int Compare(object x, object y)
			{
				string text = ((ListViewItem)x).SubItems[this.SortColumn].Text;
				string text2 = ((ListViewItem)y).SubItems[this.SortColumn].Text;
				int result;
				if (this.SortDirection == ListSortDirection.Ascending)
				{
					result = string.Compare(text, text2, false, CultureInfo.CurrentCulture);
				}
				else
				{
					result = string.Compare(text2, text, false, CultureInfo.CurrentCulture);
				}
				return result;
			}

			// Token: 0x04000819 RID: 2073
			private int sortColumn;

			// Token: 0x0400081A RID: 2074
			private ListSortDirection sortDirection;
		}
	}
}
