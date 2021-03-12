using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ManagementGUI;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C7 RID: 455
	public class ConditionsEditorControl : BindableUserControl
	{
		// Token: 0x0600130B RID: 4875 RVA: 0x0004D2C8 File Offset: 0x0004B4C8
		public ConditionsEditorControl()
		{
			this.InitializeComponent();
			this.conditionListView.VirtualMode = false;
			this.conditionListView.AutoGenerateColumns = false;
			this.conditionListView.AvailableColumns.Add("Description", "Description", true);
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600130C RID: 4876 RVA: 0x0004D315 File Offset: 0x0004B515
		// (set) Token: 0x0600130D RID: 4877 RVA: 0x0004D327 File Offset: 0x0004B527
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public IList<ConditionDescriptor> ConditionDescriptors
		{
			get
			{
				return this.conditionListView.DataSource as IList<ConditionDescriptor>;
			}
			set
			{
				if (this.conditionListView.DataSource != value)
				{
					this.conditionListView.DataSource = value;
					this.InitializePhrasePresentationControl();
				}
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600130E RID: 4878 RVA: 0x0004D349 File Offset: 0x0004B549
		// (set) Token: 0x0600130F RID: 4879 RVA: 0x0004D351 File Offset: 0x0004B551
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public BindingList<PhraseDescriptor> PhraseDescriptors
		{
			get
			{
				return this.phraseDescriptors;
			}
			set
			{
				if (this.PhraseDescriptors != value)
				{
					this.DetachEventsWithPhraseDescriptors();
					this.phraseDescriptors = value;
					this.InitializePhrasePresentationControl();
					this.AttachEventsWithPhraseDescriptors();
				}
			}
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0004D378 File Offset: 0x0004B578
		private void AttachEventsWithPhraseDescriptors()
		{
			if (this.PhraseDescriptors != null)
			{
				this.PhraseDescriptors.ListChanged += this.phraseDescriptors_ListChanged;
				foreach (PhraseDescriptor phraseDescriptor in this.PhraseDescriptors)
				{
					phraseDescriptor.PhraseEditingPropertyUpdated += this.PhraseDescriptor_PhraseEditingPropertyUpdated;
				}
			}
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0004D3F0 File Offset: 0x0004B5F0
		private void DetachEventsWithPhraseDescriptors()
		{
			if (this.PhraseDescriptors != null)
			{
				this.PhraseDescriptors.ListChanged -= this.phraseDescriptors_ListChanged;
				foreach (PhraseDescriptor phraseDescriptor in this.PhraseDescriptors)
				{
					phraseDescriptor.PhraseEditingPropertyUpdated -= this.PhraseDescriptor_PhraseEditingPropertyUpdated;
				}
			}
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0004D468 File Offset: 0x0004B668
		private void PhraseDescriptor_PhraseEditingPropertyUpdated(object sender, PropertyChangedEventArgs e)
		{
			base.UpdateError();
			this.OnEditingPropertyUpdated(e.PropertyName);
		}

		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06001313 RID: 4883 RVA: 0x0004D47C File Offset: 0x0004B67C
		// (remove) Token: 0x06001314 RID: 4884 RVA: 0x0004D4B4 File Offset: 0x0004B6B4
		public event PropertyChangedEventHandler EditingPropertyUpdated;

		// Token: 0x06001315 RID: 4885 RVA: 0x0004D4E9 File Offset: 0x0004B6E9
		private void OnEditingPropertyUpdated(string propertyName)
		{
			if (this.EditingPropertyUpdated != null)
			{
				this.EditingPropertyUpdated(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0004D508 File Offset: 0x0004B708
		public bool HasConditionsSelected
		{
			get
			{
				bool result = false;
				if (this.ConditionDescriptors != null && this.PhraseDescriptors != null)
				{
					foreach (ConditionDescriptor conditionDescriptor in this.ConditionDescriptors)
					{
						if (this.GetPhraseByIndex(conditionDescriptor.Index).Used)
						{
							result = true;
							break;
						}
					}
				}
				return result;
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004D578 File Offset: 0x0004B778
		private void phraseDescriptors_ListChanged(object sender, ListChangedEventArgs e)
		{
			BindingList<PhraseDescriptor> bindingList = sender as BindingList<PhraseDescriptor>;
			if (e.ListChangedType == ListChangedType.ItemChanged)
			{
				this.UpdatePhrasePresentation(bindingList[e.NewIndex]);
			}
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004D5A8 File Offset: 0x0004B7A8
		private void InitializePhrasePresentationControl()
		{
			if (this.PhraseDescriptors != null && this.ConditionDescriptors != null)
			{
				this.ClearPhrasePresentationControl();
				foreach (PhraseDescriptor phraseDescriptor in this.PhraseDescriptors)
				{
					this.UpdatePhrasePresentation(phraseDescriptor);
				}
			}
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004D60C File Offset: 0x0004B80C
		private void ClearPhrasePresentationControl()
		{
			this.sentencePanel.SuspendLayout();
			try
			{
				for (int i = this.sentencePanel.Controls.Count - 1; i >= 0; i--)
				{
					Control control = this.sentencePanel.Controls[i];
					if (control is ConditionsEditorControl.PhrasePresentationControl)
					{
						this.sentencePanel.Controls.Remove(control);
						control.Dispose();
					}
				}
			}
			finally
			{
				this.sentencePanel.ResumeLayout(true);
			}
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004D694 File Offset: 0x0004B894
		private void conditionListView_UpdateItem(object sender, ItemCheckedEventArgs e)
		{
			PhraseDescriptor phraseByListViewItem = this.GetPhraseByListViewItem(e.Item);
			if (phraseByListViewItem != null)
			{
				e.Item.Checked = phraseByListViewItem.Used;
			}
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0004D6C4 File Offset: 0x0004B8C4
		private void conditionListView_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			ConditionDescriptor conditionDescriptor = this.conditionListView.GetRowFromItem(this.conditionListView.Items[e.Index]) as ConditionDescriptor;
			if (conditionDescriptor != null)
			{
				PhraseDescriptor phraseByIndex = this.GetPhraseByIndex(conditionDescriptor.Index);
				if (phraseByIndex != null)
				{
					phraseByIndex.Used = (e.NewValue == CheckState.Checked);
				}
			}
			base.UpdateError();
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0004D720 File Offset: 0x0004B920
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x0004D72D File Offset: 0x0004B92D
		[DefaultValue("")]
		public string SentenceDescriptionText
		{
			get
			{
				return this.sentenceDescriptionLabel.Text;
			}
			set
			{
				this.sentenceDescriptionLabel.Text = value;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004D73B File Offset: 0x0004B93B
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x0004D748 File Offset: 0x0004B948
		[DefaultValue("")]
		public string FirstStepText
		{
			get
			{
				return this.step1Label.Text;
			}
			set
			{
				this.step1Label.Text = value;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004D756 File Offset: 0x0004B956
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x0004D763 File Offset: 0x0004B963
		[DefaultValue("")]
		public string SecondStepText
		{
			get
			{
				return this.step2Label.Text;
			}
			set
			{
				this.step2Label.Text = value;
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0004D774 File Offset: 0x0004B974
		private void UpdatePhrasePresentation(PhraseDescriptor phraseDescriptor)
		{
			if (phraseDescriptor != null)
			{
				this.UpdateConditionItem(phraseDescriptor);
				ConditionsEditorControl.PhrasePresentationControl phrasePresentationControl = this.GetPhrasePresentationControlByPhraseDescriptor(phraseDescriptor);
				if (phraseDescriptor.Used && phrasePresentationControl == null)
				{
					this.sentencePanel.SuspendLayout();
					phrasePresentationControl = new ConditionsEditorControl.PhrasePresentationControl(phraseDescriptor);
					this.sentencePanel.Controls.Add(phrasePresentationControl);
					this.sentencePanel.Controls.SetChildIndex(phrasePresentationControl, this.GetIndexOfPhrasePresentationControl(phrasePresentationControl));
					this.UpdatePhrasePresentationControlPrefix();
					this.sentencePanel.ResumeLayout(true);
					this.sentencePanel.ScrollControlIntoView(phrasePresentationControl);
					return;
				}
				if (!phraseDescriptor.Used && phrasePresentationControl != null)
				{
					this.sentencePanel.SuspendLayout();
					this.sentencePanel.Controls.Remove(phrasePresentationControl);
					this.UpdatePhrasePresentationControlPrefix();
					this.sentencePanel.ResumeLayout(true);
					phrasePresentationControl.Dispose();
				}
			}
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0004D83C File Offset: 0x0004BA3C
		private void UpdatePhrasePresentationControlPrefix()
		{
			int previousGroupID = int.MinValue;
			for (int i = this.sentencePanel.Controls.Count - 1; i >= 0; i--)
			{
				ConditionsEditorControl.PhrasePresentationControl phrasePresentationControl = this.sentencePanel.Controls[i] as ConditionsEditorControl.PhrasePresentationControl;
				if (phrasePresentationControl != null && phrasePresentationControl.PhraseDescriptor != null)
				{
					phrasePresentationControl.UpdateText(previousGroupID);
					previousGroupID = phrasePresentationControl.PhraseDescriptor.GroupID;
				}
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0004D8A4 File Offset: 0x0004BAA4
		private void UpdateConditionItem(PhraseDescriptor phraseDescriptor)
		{
			if (this.ConditionDescriptors != null && phraseDescriptor != null)
			{
				ConditionDescriptor conditionByIndex = this.GetConditionByIndex(phraseDescriptor.Index);
				if (conditionByIndex != null)
				{
					ListViewItem itemFromRow = this.conditionListView.GetItemFromRow(conditionByIndex);
					if (itemFromRow != null)
					{
						itemFromRow.Checked = phraseDescriptor.Used;
					}
				}
			}
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0004D8E8 File Offset: 0x0004BAE8
		private int GetIndexOfPhrasePresentationControl(ConditionsEditorControl.PhrasePresentationControl phraseControl)
		{
			int i;
			for (i = 0; i < this.sentencePanel.Controls.Count - 1; i++)
			{
				ConditionsEditorControl.PhrasePresentationControl phrasePresentationControl = this.sentencePanel.Controls[i] as ConditionsEditorControl.PhrasePresentationControl;
				if (phrasePresentationControl == null || phraseControl.PhraseDescriptor.Index >= phrasePresentationControl.PhraseDescriptor.Index)
				{
					break;
				}
			}
			return i;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0004D948 File Offset: 0x0004BB48
		protected override UIValidationError[] GetValidationErrors()
		{
			List<UIValidationError> list = new List<UIValidationError>();
			if (this.ConditionDescriptors != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (ConditionDescriptor conditionDescriptor in this.ConditionDescriptors)
				{
					PhraseDescriptor phraseByIndex = this.GetPhraseByIndex(conditionDescriptor.Index);
					if (phraseByIndex != null)
					{
						ValidationError[] array = phraseByIndex.Validate();
						foreach (ValidationError validationError in array)
						{
							stringBuilder.AppendLine(validationError.Description);
						}
					}
				}
				if (stringBuilder.Length > 0)
				{
					list.Add(new UIValidationError(new LocalizedString(stringBuilder.ToString()), this.sentencePanel));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0004DA20 File Offset: 0x0004BC20
		private PhraseDescriptor GetPhraseByListViewItem(ListViewItem listViewItem)
		{
			PhraseDescriptor result = null;
			ConditionDescriptor conditionDescriptor = this.conditionListView.GetRowFromItem(listViewItem) as ConditionDescriptor;
			if (conditionDescriptor != null)
			{
				result = this.GetPhraseByIndex(conditionDescriptor.Index);
			}
			return result;
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0004DA54 File Offset: 0x0004BC54
		private PhraseDescriptor GetPhraseByIndex(int index)
		{
			PhraseDescriptor result = null;
			if (this.PhraseDescriptors != null)
			{
				foreach (PhraseDescriptor phraseDescriptor in this.PhraseDescriptors)
				{
					if (phraseDescriptor.Index == index)
					{
						result = phraseDescriptor;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004DAB4 File Offset: 0x0004BCB4
		private ConditionDescriptor GetConditionByIndex(int index)
		{
			ConditionDescriptor result = null;
			if (this.ConditionDescriptors != null)
			{
				foreach (ConditionDescriptor conditionDescriptor in this.ConditionDescriptors)
				{
					if (conditionDescriptor.Index == index)
					{
						result = conditionDescriptor;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004DB14 File Offset: 0x0004BD14
		private ConditionsEditorControl.PhrasePresentationControl GetPhrasePresentationControlByPhraseDescriptor(PhraseDescriptor phrase)
		{
			ConditionsEditorControl.PhrasePresentationControl result = null;
			foreach (object obj in this.sentencePanel.Controls)
			{
				Control control = (Control)obj;
				ConditionsEditorControl.PhrasePresentationControl phrasePresentationControl = control as ConditionsEditorControl.PhrasePresentationControl;
				if (phrasePresentationControl != null && phrasePresentationControl.PhraseDescriptor == phrase)
				{
					result = phrasePresentationControl;
					break;
				}
			}
			return result;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004DB88 File Offset: 0x0004BD88
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.step1Label = new Label();
			this.step2Label = new Label();
			this.sentencePanel = new ExchangeUserControl();
			this.sentenceDescriptionLabel = new AutoHeightLabel();
			this.conditionListView = new DataListView();
			((ISupportInitialize)base.BindingSource).BeginInit();
			this.tableLayoutPanel.SuspendLayout();
			this.sentencePanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.step1Label, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.step2Label, 0, 2);
			this.tableLayoutPanel.Controls.Add(this.sentencePanel, 0, 3);
			this.tableLayoutPanel.Controls.Add(this.conditionListView, 0, 1);
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(0, 0, 16, 0);
			this.tableLayoutPanel.RowCount = 4;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(400, 342);
			this.tableLayoutPanel.TabIndex = 0;
			this.step1Label.AutoSize = true;
			this.step1Label.Dock = DockStyle.Top;
			this.step1Label.Location = new Point(0, 0);
			this.step1Label.Margin = new Padding(0);
			this.step1Label.Name = "step1Label";
			this.step1Label.Size = new Size(384, 13);
			this.step1Label.TabIndex = 0;
			this.step2Label.AutoSize = true;
			this.step2Label.Dock = DockStyle.Top;
			this.step2Label.Location = new Point(0, 203);
			this.step2Label.Margin = new Padding(0, 9, 0, 0);
			this.step2Label.Name = "step2Label";
			this.step2Label.Size = new Size(384, 13);
			this.step2Label.TabIndex = 2;
			this.sentencePanel.AutoScroll = true;
			this.sentencePanel.BackColor = SystemColors.Window;
			this.sentencePanel.BorderStyle = BorderStyle.Fixed3D;
			this.sentencePanel.Controls.Add(this.sentenceDescriptionLabel);
			this.sentencePanel.Dock = DockStyle.Top;
			this.sentencePanel.Location = new Point(3, 219);
			this.sentencePanel.Margin = new Padding(3, 3, 0, 3);
			this.sentencePanel.MaximumSize = new Size(1024, 200);
			this.sentencePanel.MinimumSize = new Size(100, 100);
			this.sentencePanel.Name = "sentencePanel";
			this.sentencePanel.Size = new Size(381, 120);
			this.sentencePanel.TabIndex = 3;
			this.sentenceDescriptionLabel.Dock = DockStyle.Top;
			this.sentenceDescriptionLabel.LinkArea = new LinkArea(0, 0);
			this.sentenceDescriptionLabel.Location = new Point(0, 0);
			this.sentenceDescriptionLabel.Margin = new Padding(0);
			this.sentenceDescriptionLabel.Name = "sentenceDescriptionLabel";
			this.sentenceDescriptionLabel.Size = new Size(377, 16);
			this.sentenceDescriptionLabel.TabIndex = 0;
			this.conditionListView.CheckBoxes = true;
			this.conditionListView.DataSourceRefresher = null;
			this.conditionListView.Dock = DockStyle.Top;
			this.conditionListView.HeaderStyle = ColumnHeaderStyle.None;
			this.conditionListView.Location = new Point(3, 16);
			this.conditionListView.Margin = new Padding(3, 3, 0, 3);
			this.conditionListView.MaximumSize = new Size(1024, 230);
			this.conditionListView.MinimumSize = new Size(0, 100);
			this.conditionListView.MultiSelect = false;
			this.conditionListView.Name = "conditionListView";
			this.conditionListView.Size = new Size(381, 175);
			this.conditionListView.TabIndex = 1;
			this.conditionListView.UseCompatibleStateImageBehavior = false;
			this.conditionListView.UpdateItem += this.conditionListView_UpdateItem;
			this.conditionListView.ItemCheck += this.conditionListView_ItemCheck;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "ConditionsEditorControl";
			base.Size = new Size(400, 344);
			((ISupportInitialize)base.BindingSource).EndInit();
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.sentencePanel.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000719 RID: 1817
		private BindingList<PhraseDescriptor> phraseDescriptors;

		// Token: 0x0400071B RID: 1819
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x0400071C RID: 1820
		private Label step1Label;

		// Token: 0x0400071D RID: 1821
		private Label step2Label;

		// Token: 0x0400071E RID: 1822
		private ExchangeUserControl sentencePanel;

		// Token: 0x0400071F RID: 1823
		private DataListView conditionListView;

		// Token: 0x04000720 RID: 1824
		private AutoHeightLabel sentenceDescriptionLabel;

		// Token: 0x020001C9 RID: 457
		private class PhrasePresentationControl : LinkLabelCommand
		{
			// Token: 0x06001344 RID: 4932 RVA: 0x0004E548 File Offset: 0x0004C748
			public PhrasePresentationControl()
			{
				base.Name = "PhrasePresentationControl";
				this.ImageAlign = ContentAlignment.MiddleRight;
				this.TextAlign = ContentAlignment.MiddleLeft;
				this.DoubleBuffered = true;
				this.Padding = this.defaultPadding;
				this.Dock = DockStyle.Top;
				base.ListSeparator = this.DefaultListSeparator;
			}

			// Token: 0x06001345 RID: 4933 RVA: 0x0004E5BC File Offset: 0x0004C7BC
			public PhrasePresentationControl(PhraseDescriptor phrase) : this()
			{
				if (phrase == null)
				{
					throw new ArgumentNullException("phrase");
				}
				this.phraseDescriptor = phrase;
				base.TabIndex = this.PhraseDescriptor.Index;
				base.ListSeparator = this.DefaultListSeparator;
				base.Text = this.PhraseDescriptor.MarkupText;
				base.DataSource = this.PhraseDescriptor.DataSource;
			}

			// Token: 0x06001346 RID: 4934 RVA: 0x0004E624 File Offset: 0x0004C824
			public void UpdateText(int previousGroupID)
			{
				base.SuspendLayout();
				bool flag = previousGroupID == this.phraseDescriptor.GroupID;
				if (flag)
				{
					base.Text = string.Format(Strings.PhraseDescriptorFormat, this.PhraseDescriptor.PhrasePresentationPrefix, this.PhraseDescriptor.MarkupText);
				}
				else
				{
					base.Text = this.PhraseDescriptor.MarkupText;
				}
				this.Padding = (flag ? this.indentPadding : this.defaultPadding);
				base.ResumeLayout(true);
			}

			// Token: 0x1700046A RID: 1130
			// (get) Token: 0x06001347 RID: 4935 RVA: 0x0004E6A5 File Offset: 0x0004C8A5
			public PhraseDescriptor PhraseDescriptor
			{
				get
				{
					return this.phraseDescriptor;
				}
			}

			// Token: 0x1700046B RID: 1131
			// (get) Token: 0x06001348 RID: 4936 RVA: 0x0004E6AD File Offset: 0x0004C8AD
			// (set) Token: 0x06001349 RID: 4937 RVA: 0x0004E6B5 File Offset: 0x0004C8B5
			[DefaultValue(ContentAlignment.MiddleRight)]
			public ContentAlignment ImageAlign
			{
				get
				{
					return base.ImageAlign;
				}
				set
				{
					base.ImageAlign = value;
				}
			}

			// Token: 0x1700046C RID: 1132
			// (get) Token: 0x0600134A RID: 4938 RVA: 0x0004E6BE File Offset: 0x0004C8BE
			// (set) Token: 0x0600134B RID: 4939 RVA: 0x0004E6C6 File Offset: 0x0004C8C6
			[DefaultValue(ContentAlignment.MiddleLeft)]
			public ContentAlignment TextAlign
			{
				get
				{
					return base.TextAlign;
				}
				set
				{
					base.TextAlign = value;
				}
			}

			// Token: 0x1700046D RID: 1133
			// (get) Token: 0x0600134C RID: 4940 RVA: 0x0004E6CF File Offset: 0x0004C8CF
			// (set) Token: 0x0600134D RID: 4941 RVA: 0x0004E6D7 File Offset: 0x0004C8D7
			[DefaultValue(DockStyle.Top)]
			public DockStyle Dock
			{
				get
				{
					return base.Dock;
				}
				set
				{
					base.Dock = value;
				}
			}

			// Token: 0x1700046E RID: 1134
			// (get) Token: 0x0600134E RID: 4942 RVA: 0x0004E6E0 File Offset: 0x0004C8E0
			// (set) Token: 0x0600134F RID: 4943 RVA: 0x0004E6E8 File Offset: 0x0004C8E8
			public Padding Padding
			{
				get
				{
					return base.Padding;
				}
				set
				{
					base.Padding = value;
				}
			}

			// Token: 0x06001350 RID: 4944 RVA: 0x0004E6F4 File Offset: 0x0004C8F4
			private bool ShouldSerializePadding()
			{
				return !this.Padding.Equals(this.defaultPadding);
			}

			// Token: 0x06001351 RID: 4945 RVA: 0x0004E723 File Offset: 0x0004C923
			private void ResetPadding()
			{
				this.Padding = this.defaultPadding;
			}

			// Token: 0x1700046F RID: 1135
			// (get) Token: 0x06001352 RID: 4946 RVA: 0x0004E731 File Offset: 0x0004C931
			protected override string DefaultListSeparator
			{
				get
				{
					if (this.PhraseDescriptor != null)
					{
						return this.PhraseDescriptor.ListSeparator;
					}
					return string.Empty;
				}
			}

			// Token: 0x06001353 RID: 4947 RVA: 0x0004E74C File Offset: 0x0004C94C
			protected override void OnTextChanged(EventArgs e)
			{
				base.OnTextChanged(e);
				if (this.PhraseDescriptor != null)
				{
					if (!this.phraseDescriptor.IsValuesOfEditingPropertiesValid)
					{
						base.Image = ConditionsEditorControl.PhrasePresentationControl.edit;
						return;
					}
					base.Image = null;
				}
			}

			// Token: 0x06001354 RID: 4948 RVA: 0x0004E780 File Offset: 0x0004C980
			protected override void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
			{
				string text = (string)e.Link.LinkData;
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(base.DataSource)[text];
				UITypeEditor uitypeEditor = this.CreateEditor(propertyDescriptor);
				if (propertyDescriptor != null && base.DataSource != null && uitypeEditor != null)
				{
					IServiceProvider serviceProvider = (base.FindForm() as IServiceProvider) ?? new ServiceContainer();
					TypeDescriptorContext context = new TypeDescriptorContext(serviceProvider, base.DataSource, propertyDescriptor);
					base.SuspendUpdates();
					try
					{
						object value = propertyDescriptor.GetValue(base.DataSource);
						value = uitypeEditor.EditValue(context, serviceProvider, value);
						this.PhraseDescriptor.SetDataSourceProperty(text, value);
					}
					finally
					{
						base.ResumeUpdates();
					}
				}
			}

			// Token: 0x06001355 RID: 4949 RVA: 0x0004E834 File Offset: 0x0004CA34
			private UITypeEditor CreateEditor(PropertyDescriptor property)
			{
				UITypeEditor result = null;
				if (this.PhraseDescriptor != null)
				{
					result = this.PhraseDescriptor.CreateEditor(property);
				}
				return result;
			}

			// Token: 0x04000725 RID: 1829
			private static Bitmap edit = IconLibrary.ToSmallBitmap(Icons.Edit);

			// Token: 0x04000726 RID: 1830
			private Padding defaultPadding = new Padding(0, 0, 20, 0);

			// Token: 0x04000727 RID: 1831
			private Padding indentPadding = new Padding(8, 0, 20, 0);

			// Token: 0x04000728 RID: 1832
			private PhraseDescriptor phraseDescriptor;
		}
	}
}
