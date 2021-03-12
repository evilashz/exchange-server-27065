using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000228 RID: 552
	public class ComboBoxPicker : ExchangeUserControl
	{
		// Token: 0x0600197D RID: 6525 RVA: 0x0006EA2C File Offset: 0x0006CC2C
		public ComboBoxPicker()
		{
			this.InitializeComponent();
			this.objectTable = new DataTable("objectTable");
			this.objectTable.Columns.Add("ValueColumn", typeof(object));
			this.objectTable.Columns.Add("DisplayColumn", typeof(string));
			this.comboBox.ValueMember = "ValueColumn";
			this.comboBox.DisplayMember = "DisplayColumn";
			this.objectTable.DefaultView.Sort = "DisplayColumn";
			this.comboBox.DataSource = this.objectTable.DefaultView;
			this.comboBox.SelectedIndexChanged += this.comboBox_SelectedIndexChanged;
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x0006EAF7 File Offset: 0x0006CCF7
		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!this.suspendChangeNotification)
			{
				this.OnSelectedValueChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0006EB0C File Offset: 0x0006CD0C
		private void DataTableLoader_RefreshCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.suspendChangeNotification = true;
			object selectedValue = this.SelectedValue;
			DataTable table = (sender as DataTableLoader).Table;
			this.objectTable.Rows.Clear();
			foreach (object obj in table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DataRow dataRow2 = this.objectTable.NewRow();
				ConvertEventArgs convertEventArgs = new ConvertEventArgs(dataRow[this.ValueMember], dataRow[this.ValueMember].GetType());
				this.OnValueConvert(convertEventArgs);
				if (convertEventArgs.Value != null)
				{
					dataRow2["ValueColumn"] = convertEventArgs.Value;
					ValueToDisplayObjectConverter valueToDisplayObjectConverter = (this.ValueMemberConverter != null) ? this.ValueMemberConverter : new ToStringValueToDisplayObjectConverter();
					dataRow2["DisplayColumn"] = (string.IsNullOrEmpty(this.DisplayMember) ? valueToDisplayObjectConverter.Convert(dataRow2["ValueColumn"]) : valueToDisplayObjectConverter.Convert(dataRow[this.DisplayMember]));
					this.objectTable.Rows.Add(dataRow2);
				}
			}
			this.SelectedIndex = -1;
			this.SelectedValue = (this.delaySetSelectedValue ?? selectedValue);
			this.suspendChangeNotification = false;
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x0006EC70 File Offset: 0x0006CE70
		private int GetIndexByValueColumn(object value)
		{
			int result = -1;
			DataView dataView = this.comboBox.DataSource as DataView;
			if (value != null && dataView != null && !string.IsNullOrEmpty(this.comboBox.ValueMember))
			{
				for (int i = 0; i < dataView.Count; i++)
				{
					object obj = dataView[i][this.comboBox.ValueMember];
					if (value is string)
					{
						if ((this.IsCaseSensitive && string.Equals(value as string, obj as string, StringComparison.InvariantCulture)) || (!this.IsCaseSensitive && string.Equals(value as string, obj as string, StringComparison.InvariantCultureIgnoreCase)))
						{
							result = i;
							break;
						}
					}
					else if (object.Equals(value, obj))
					{
						result = i;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x0006ED2C File Offset: 0x0006CF2C
		private void InitializeComponent()
		{
			this.comboBox = new ExchangeComboBox();
			this.tableLayoutPanel = new TableLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.comboBox.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBox.Location = new Point(0, 0);
			this.comboBox.Margin = new Padding(0);
			this.comboBox.Name = "comboBox";
			this.comboBox.Size = new Size(120, 21);
			this.comboBox.TabIndex = 0;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 1;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Controls.Add(this.comboBox, 0, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Margin = new Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle());
			this.tableLayoutPanel.Size = new Size(120, 21);
			this.tableLayoutPanel.TabIndex = 1;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "ComboBoxPicker";
			base.Size = new Size(120, 21);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x0006EEF4 File Offset: 0x0006D0F4
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x0006EEFC File Offset: 0x0006D0FC
		[DefaultValue(null)]
		public ObjectPicker Picker
		{
			get
			{
				return this.picker;
			}
			set
			{
				if (value != this.Picker && value != null)
				{
					if (this.Picker != null)
					{
						this.picker.DataTableLoader.RefreshCompleted -= this.DataTableLoader_RefreshCompleted;
					}
					this.picker = value;
					this.picker.AllowMultiSelect = true;
					this.picker.DataTableLoader.RefreshCompleted += this.DataTableLoader_RefreshCompleted;
					this.picker.PerformQuery(null, this.QueryString);
				}
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x0006EF7A File Offset: 0x0006D17A
		// (set) Token: 0x06001985 RID: 6533 RVA: 0x0006EF91 File Offset: 0x0006D191
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get
			{
				return this.delaySetSelectedValue ?? this.comboBox.SelectedValue;
			}
			set
			{
				if (this.picker.DataTableLoader.Refreshing)
				{
					this.delaySetSelectedValue = value;
					return;
				}
				this.SelectedIndex = this.GetIndexByValueColumn(value);
				this.comboBox.Refresh();
				this.delaySetSelectedValue = null;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0006EFCC File Offset: 0x0006D1CC
		// (set) Token: 0x06001987 RID: 6535 RVA: 0x0006EFD4 File Offset: 0x0006D1D4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string ValueMember
		{
			get
			{
				return this.valueMember;
			}
			set
			{
				this.valueMember = value;
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001988 RID: 6536 RVA: 0x0006EFDD File Offset: 0x0006D1DD
		// (set) Token: 0x06001989 RID: 6537 RVA: 0x0006EFE5 File Offset: 0x0006D1E5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DisplayMember
		{
			get
			{
				return this.displayMember;
			}
			set
			{
				this.displayMember = value;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x0600198A RID: 6538 RVA: 0x0006EFEE File Offset: 0x0006D1EE
		// (set) Token: 0x0600198B RID: 6539 RVA: 0x0006EFF6 File Offset: 0x0006D1F6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string QueryString
		{
			get
			{
				return this.queryString;
			}
			set
			{
				this.queryString = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600198C RID: 6540 RVA: 0x0006EFFF File Offset: 0x0006D1FF
		// (set) Token: 0x0600198D RID: 6541 RVA: 0x0006F007 File Offset: 0x0006D207
		[DefaultValue(null)]
		public ValueToDisplayObjectConverter ValueMemberConverter
		{
			get
			{
				return this.converter;
			}
			set
			{
				this.converter = value;
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x0600198E RID: 6542 RVA: 0x0006F010 File Offset: 0x0006D210
		// (set) Token: 0x0600198F RID: 6543 RVA: 0x0006F01D File Offset: 0x0006D21D
		[DefaultValue(-1)]
		public int SelectedIndex
		{
			get
			{
				return this.comboBox.SelectedIndex;
			}
			set
			{
				this.comboBox.SelectedIndex = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001990 RID: 6544 RVA: 0x0006F02B File Offset: 0x0006D22B
		[DefaultValue(0)]
		public int ObjectCount
		{
			get
			{
				return this.objectTable.Rows.Count;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x0006F03D File Offset: 0x0006D23D
		// (set) Token: 0x06001992 RID: 6546 RVA: 0x0006F045 File Offset: 0x0006D245
		[DefaultValue(false)]
		public bool IsCaseSensitive
		{
			get
			{
				return this.isCaseSensitive;
			}
			set
			{
				this.isCaseSensitive = value;
			}
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x0006F050 File Offset: 0x0006D250
		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ComboBoxPicker.EventSelectedValueChanged];
			if (eventHandler != null && !this.suspendChangeNotification)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140000AC RID: 172
		// (add) Token: 0x06001994 RID: 6548 RVA: 0x0006F086 File Offset: 0x0006D286
		// (remove) Token: 0x06001995 RID: 6549 RVA: 0x0006F099 File Offset: 0x0006D299
		public event EventHandler SelectedValueChanged
		{
			add
			{
				base.Events.AddHandler(ComboBoxPicker.EventSelectedValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ComboBoxPicker.EventSelectedValueChanged, value);
			}
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x0006F0AC File Offset: 0x0006D2AC
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.Picker != null)
			{
				this.Picker.DataTableLoader.RefreshCompleted -= this.DataTableLoader_RefreshCompleted;
			}
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0006F0D9 File Offset: 0x0006D2D9
		protected virtual void OnValueConvert(ConvertEventArgs e)
		{
			if (this.ValueConvert != null)
			{
				this.ValueConvert(this, e);
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x0006F0F0 File Offset: 0x0006D2F0
		protected override string ExposedPropertyName
		{
			get
			{
				return "SelectedValue";
			}
		}

		// Token: 0x04000984 RID: 2436
		private const string ValueColumn = "ValueColumn";

		// Token: 0x04000985 RID: 2437
		private const string DisplayColumn = "DisplayColumn";

		// Token: 0x04000986 RID: 2438
		private ObjectPicker picker;

		// Token: 0x04000987 RID: 2439
		private ValueToDisplayObjectConverter converter;

		// Token: 0x04000988 RID: 2440
		private string queryString;

		// Token: 0x04000989 RID: 2441
		private string valueMember;

		// Token: 0x0400098A RID: 2442
		private string displayMember;

		// Token: 0x0400098B RID: 2443
		private ComboBox comboBox;

		// Token: 0x0400098C RID: 2444
		private DataTable objectTable;

		// Token: 0x0400098D RID: 2445
		private bool suspendChangeNotification;

		// Token: 0x0400098E RID: 2446
		private bool isCaseSensitive;

		// Token: 0x0400098F RID: 2447
		private TableLayoutPanel tableLayoutPanel;

		// Token: 0x04000990 RID: 2448
		private object delaySetSelectedValue;

		// Token: 0x04000991 RID: 2449
		private static readonly object EventSelectedValueChanged = new object();

		// Token: 0x04000992 RID: 2450
		public ConvertEventHandler ValueConvert;
	}
}
