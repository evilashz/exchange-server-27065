using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000227 RID: 551
	public class CheckedPickerLauncherTextBox : ExchangeUserControl, IFormatModeProvider, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x0006E170 File Offset: 0x0006C370
		public CheckedPickerLauncherTextBox()
		{
			this.InitializeComponent();
			this.checkBox.CheckedChanged += this.checkBox_CheckedChanged;
			base.DataBindings.CollectionChanged += this.DataBindings_CollectionChanged;
			this.pickerLauncherTextBox.Validating += this.pickerLauncherTextBox_Validating;
			this.pickerLauncherTextBox.FormatModeChanged += delegate(object param0, EventArgs param1)
			{
				this.OnFormatModeChanged(EventArgs.Empty);
			};
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x0006E1EC File Offset: 0x0006C3EC
		private void pickerLauncherTextBox_Validating(object sender, CancelEventArgs e)
		{
			this.RaiseValidatingEvent();
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x0006E1F4 File Offset: 0x0006C3F4
		private void RaiseValidatingEvent()
		{
			this.OnValidating(new CancelEventArgs());
		}

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001949 RID: 6473 RVA: 0x0006E201 File Offset: 0x0006C401
		// (set) Token: 0x0600194A RID: 6474 RVA: 0x0006E20E File Offset: 0x0006C40E
		[DefaultValue(true)]
		public bool TextBoxReadOnly
		{
			get
			{
				return this.pickerLauncherTextBox.TextBoxReadOnly;
			}
			set
			{
				this.pickerLauncherTextBox.InitTextBoxReadOnly(value, this, this.ExposedPropertyName);
			}
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x0006E24C File Offset: 0x0006C44C
		private void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			Binding binding = (Binding)e.Element;
			CollectionChangeAction action = e.Action;
			if (action != CollectionChangeAction.Add)
			{
				return;
			}
			if (binding.PropertyName == this.ExposedPropertyName)
			{
				binding.DataSourceNullValue = null;
				(binding.DataSource as BindingSource).DataSourceChanged += delegate(object param0, EventArgs param1)
				{
					if (!this.checkBox.Checked)
					{
						this.pickerLauncherTextBox.SelectedValue = null;
					}
					this.pickerLauncherTextBox.UpdateDisplay();
				};
			}
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x0006E2B0 File Offset: 0x0006C4B0
		private void checkBox_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.checkBox.Checked;
			this.pickerLauncherTextBox.Enabled = @checked;
			this.OnCheckedChanged(EventArgs.Empty);
			if (!@checked || !CheckedPickerLauncherTextBox.isInitializedValueOfSelectedValue(this.pickerLauncherTextBox.SelectedValue))
			{
				this.OnSelectedValueChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x0006E300 File Offset: 0x0006C500
		private void pickerLauncherTextBox_SelectedValueChanged(object sender, EventArgs e)
		{
			if (this.Checked && !CheckedPickerLauncherTextBox.isInitializedValueOfSelectedValue(this.pickerLauncherTextBox.SelectedValue))
			{
				this.OnSelectedValueChanged(EventArgs.Empty);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0006E327 File Offset: 0x0006C527
		public override Control ErrorProviderAnchor
		{
			get
			{
				return this.pickerLauncherTextBox;
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x0006E330 File Offset: 0x0006C530
		protected override UIValidationError[] GetValidationErrors()
		{
			List<UIValidationError> list = new List<UIValidationError>();
			if (this.checkBox.Checked && CheckedPickerLauncherTextBox.isInitializedValueOfSelectedValue(this.SelectedValue))
			{
				UIValidationError item = new UIValidationError(Strings.SelectValueErrorMessage, this.pickerLauncherTextBox);
				list.Add(item);
			}
			return list.ToArray();
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x0006E37C File Offset: 0x0006C57C
		private void InitializeComponent()
		{
			this.tableLayoutPanel = new AutoTableLayoutPanel();
			this.checkBox = new AutoHeightCheckBox();
			this.pickerLauncherTextBox = new PickerLauncherTextBox();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.AutoLayout = true;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 16f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.checkBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.pickerLauncherTextBox, 1, 1);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new Padding(0, 0, 16, 0);
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(397, 46);
			this.tableLayoutPanel.TabIndex = 0;
			this.checkBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.tableLayoutPanel.SetColumnSpan(this.checkBox, 2);
			this.checkBox.Location = new Point(3, 0);
			this.checkBox.Margin = new Padding(3, 0, 0, 0);
			this.checkBox.Name = "checkBox";
			this.checkBox.Size = new Size(378, 17);
			this.checkBox.TabIndex = 0;
			this.checkBox.Text = "checkBox";
			this.checkBox.UseVisualStyleBackColor = true;
			this.pickerLauncherTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.pickerLauncherTextBox.AutoSize = true;
			this.pickerLauncherTextBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.pickerLauncherTextBox.Enabled = false;
			this.pickerLauncherTextBox.Location = new Point(16, 23);
			this.pickerLauncherTextBox.Margin = new Padding(0, 6, 0, 0);
			this.pickerLauncherTextBox.Name = "pickerLauncherTextBox";
			this.pickerLauncherTextBox.Size = new Size(365, 23);
			this.pickerLauncherTextBox.TabIndex = 1;
			this.pickerLauncherTextBox.SelectedValueChanged += this.pickerLauncherTextBox_SelectedValueChanged;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "CheckedPickerLauncherTextBox";
			base.Size = new Size(397, 46);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001951 RID: 6481 RVA: 0x0006E68B File Offset: 0x0006C88B
		// (set) Token: 0x06001952 RID: 6482 RVA: 0x0006E698 File Offset: 0x0006C898
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Caption
		{
			get
			{
				return this.checkBox.Text;
			}
			set
			{
				this.checkBox.Text = value;
			}
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001953 RID: 6483 RVA: 0x0006E6A6 File Offset: 0x0006C8A6
		// (set) Token: 0x06001954 RID: 6484 RVA: 0x0006E6B3 File Offset: 0x0006C8B3
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.checkBox.Checked;
			}
			set
			{
				this.checkBox.Checked = value;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001955 RID: 6485 RVA: 0x0006E6C1 File Offset: 0x0006C8C1
		// (set) Token: 0x06001956 RID: 6486 RVA: 0x0006E6CE File Offset: 0x0006C8CE
		[DefaultValue(null)]
		public ObjectPickerBase Picker
		{
			get
			{
				return this.pickerLauncherTextBox.Picker;
			}
			set
			{
				this.pickerLauncherTextBox.Picker = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001957 RID: 6487 RVA: 0x0006E6DC File Offset: 0x0006C8DC
		// (set) Token: 0x06001958 RID: 6488 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		[DefaultValue(null)]
		public object SelectedValue
		{
			get
			{
				if (this.checkBox.Checked)
				{
					return this.pickerLauncherTextBox.SelectedValue;
				}
				return null;
			}
			set
			{
				if (value != this.pickerLauncherTextBox.SelectedValue)
				{
					this.suspendChangeNotification = true;
					this.checkBox.Checked = !CheckedPickerLauncherTextBox.isInitializedValueOfSelectedValue(value);
					if (this.checkBox.Checked)
					{
						this.pickerLauncherTextBox.SelectedValue = value;
					}
					this.suspendChangeNotification = false;
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x0006E759 File Offset: 0x0006C959
		private static bool isInitializedValueOfSelectedValue(object value)
		{
			if (!(value is string))
			{
				return null == value;
			}
			return string.IsNullOrEmpty((string)value);
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600195A RID: 6490 RVA: 0x0006E773 File Offset: 0x0006C973
		// (set) Token: 0x0600195B RID: 6491 RVA: 0x0006E780 File Offset: 0x0006C980
		[DefaultValue(null)]
		public string ValueMember
		{
			get
			{
				return this.pickerLauncherTextBox.ValueMember;
			}
			set
			{
				this.pickerLauncherTextBox.ValueMember = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x0006E78E File Offset: 0x0006C98E
		// (set) Token: 0x0600195D RID: 6493 RVA: 0x0006E79B File Offset: 0x0006C99B
		[DefaultValue(null)]
		internal ADPropertyDefinition ValueMemberPropertyDefinition
		{
			get
			{
				return this.pickerLauncherTextBox.ValueMemberPropertyDefinition;
			}
			set
			{
				this.pickerLauncherTextBox.ValueMemberPropertyDefinition = value;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x0006E7A9 File Offset: 0x0006C9A9
		// (set) Token: 0x0600195F RID: 6495 RVA: 0x0006E7B6 File Offset: 0x0006C9B6
		[DefaultValue(null)]
		public ValueToDisplayObjectConverter ValueMemberConverter
		{
			get
			{
				return this.pickerLauncherTextBox.ValueMemberConverter;
			}
			set
			{
				this.pickerLauncherTextBox.ValueMemberConverter = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x0006E7C4 File Offset: 0x0006C9C4
		// (set) Token: 0x06001961 RID: 6497 RVA: 0x0006E7D1 File Offset: 0x0006C9D1
		[DefaultValue(null)]
		public string DisplayMember
		{
			get
			{
				return this.pickerLauncherTextBox.DisplayMember;
			}
			set
			{
				this.pickerLauncherTextBox.DisplayMember = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0006E7DF File Offset: 0x0006C9DF
		// (set) Token: 0x06001963 RID: 6499 RVA: 0x0006E7EC File Offset: 0x0006C9EC
		[DefaultValue(null)]
		public ValueToDisplayObjectConverter DisplayMemberConverter
		{
			get
			{
				return this.pickerLauncherTextBox.DisplayMemberConverter;
			}
			set
			{
				this.pickerLauncherTextBox.DisplayMemberConverter = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x0006E7FA File Offset: 0x0006C9FA
		// (set) Token: 0x06001965 RID: 6501 RVA: 0x0006E802 File Offset: 0x0006CA02
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public LocalizedString ErrorObjectType
		{
			get
			{
				return this.errorObjectType;
			}
			set
			{
				this.errorObjectType = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x0006E80B File Offset: 0x0006CA0B
		// (set) Token: 0x06001967 RID: 6503 RVA: 0x0006E818 File Offset: 0x0006CA18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ButtonText
		{
			get
			{
				return this.pickerLauncherTextBox.ButtonText;
			}
			set
			{
				this.pickerLauncherTextBox.ButtonText = value;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x0006E826 File Offset: 0x0006CA26
		// (set) Token: 0x06001969 RID: 6505 RVA: 0x0006E833 File Offset: 0x0006CA33
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanBrowse
		{
			get
			{
				return this.pickerLauncherTextBox.CanBrowse;
			}
			set
			{
				this.pickerLauncherTextBox.CanBrowse = value;
				this.checkBox.Enabled = value;
			}
		}

		// Token: 0x0600196A RID: 6506 RVA: 0x0006E850 File Offset: 0x0006CA50
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckedPickerLauncherTextBox.EventCheckedChanged];
			if (eventHandler != null && !this.suspendChangeNotification)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140000A9 RID: 169
		// (add) Token: 0x0600196B RID: 6507 RVA: 0x0006E886 File Offset: 0x0006CA86
		// (remove) Token: 0x0600196C RID: 6508 RVA: 0x0006E899 File Offset: 0x0006CA99
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckedPickerLauncherTextBox.EventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckedPickerLauncherTextBox.EventCheckedChanged, value);
			}
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x0006E8AC File Offset: 0x0006CAAC
		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			base.UpdateError();
			EventHandler eventHandler = (EventHandler)base.Events[CheckedPickerLauncherTextBox.EventSelectedValueChanged];
			if (eventHandler != null && !this.suspendChangeNotification)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140000AA RID: 170
		// (add) Token: 0x0600196E RID: 6510 RVA: 0x0006E8E8 File Offset: 0x0006CAE8
		// (remove) Token: 0x0600196F RID: 6511 RVA: 0x0006E8FB File Offset: 0x0006CAFB
		public event EventHandler SelectedValueChanged
		{
			add
			{
				base.Events.AddHandler(CheckedPickerLauncherTextBox.EventSelectedValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckedPickerLauncherTextBox.EventSelectedValueChanged, value);
			}
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x0006E90E File Offset: 0x0006CB0E
		public override Size GetPreferredSize(Size proposedSize)
		{
			return this.tableLayoutPanel.GetPreferredSize(proposedSize);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x0006E91C File Offset: 0x0006CB1C
		public void ClearContent()
		{
			this.suspendChangeNotification = true;
			this.checkBox.Checked = false;
			this.pickerLauncherTextBox.SelectedValue = null;
			this.pickerLauncherTextBox.UpdateDisplay();
			this.suspendChangeNotification = false;
			this.OnSelectedValueChanged(EventArgs.Empty);
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001972 RID: 6514 RVA: 0x0006E95A File Offset: 0x0006CB5A
		protected override string ExposedPropertyName
		{
			get
			{
				return "SelectedValue";
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001973 RID: 6515 RVA: 0x0006E961 File Offset: 0x0006CB61
		// (set) Token: 0x06001974 RID: 6516 RVA: 0x0006E96E File Offset: 0x0006CB6E
		[DefaultValue(0)]
		public DisplayFormatMode FormatMode
		{
			get
			{
				return this.pickerLauncherTextBox.FormatMode;
			}
			set
			{
				this.pickerLauncherTextBox.FormatMode = value;
			}
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0006E97C File Offset: 0x0006CB7C
		protected virtual void OnFormatModeChanged(EventArgs e)
		{
			if (this.FormatModeChanged != null)
			{
				this.FormatModeChanged(this, e);
			}
		}

		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06001976 RID: 6518 RVA: 0x0006E994 File Offset: 0x0006CB94
		// (remove) Token: 0x06001977 RID: 6519 RVA: 0x0006E9CC File Offset: 0x0006CBCC
		public event EventHandler FormatModeChanged;

		// Token: 0x06001978 RID: 6520 RVA: 0x0006EA17 File Offset: 0x0006CC17
		void IFormatModeProvider.add_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged += A_1;
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x0006EA20 File Offset: 0x0006CC20
		void IFormatModeProvider.remove_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged -= A_1;
		}

		// Token: 0x0400097C RID: 2428
		private AutoTableLayoutPanel tableLayoutPanel;

		// Token: 0x0400097D RID: 2429
		private AutoHeightCheckBox checkBox;

		// Token: 0x0400097E RID: 2430
		private PickerLauncherTextBox pickerLauncherTextBox;

		// Token: 0x0400097F RID: 2431
		private bool suspendChangeNotification;

		// Token: 0x04000980 RID: 2432
		private LocalizedString errorObjectType;

		// Token: 0x04000981 RID: 2433
		private static readonly object EventCheckedChanged = new object();

		// Token: 0x04000982 RID: 2434
		private static readonly object EventSelectedValueChanged = new object();
	}
}
