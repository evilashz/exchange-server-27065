using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Management.SnapIn;
using Microsoft.ManagementGUI;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000210 RID: 528
	public class UserLogonNameControl : ExchangeUserControl, IFormatModeProvider, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x060017E0 RID: 6112 RVA: 0x00064BDC File Offset: 0x00062DDC
		public UserLogonNameControl()
		{
			this.InitializeComponent();
			if (PSConnectionInfoSingleton.GetInstance().Type == OrganizationType.Cloud)
			{
				this.domainComboBoxPicker.Picker = new AutomatedObjectPicker(new AcceptedDomainUPNSuffixesConfigurable());
				this.domainComboBoxPicker.ValueMember = "SmtpDomainToString";
			}
			else
			{
				this.domainComboBoxPicker.Picker = new AutomatedObjectPicker(new UPNSuffixesConfigurable());
				this.domainComboBoxPicker.ValueMember = "CanonicalName";
			}
			this.domainComboBoxPicker.Picker.DataTableLoader.RefreshCompleted += this.DataTableLoader_RefreshCompleted;
			this.domainComboBoxPicker.ValueMemberConverter = new DomainNameConverter();
			this.userNameTextBox.Leave += this.Focus_Leave;
			this.userNameTextBox.TextChanged += this.userNameTextBox_TextChanged;
			this.userNameTextBox.FormatModeChanged += delegate(object param0, EventArgs param1)
			{
				this.OnFormatModeChanged(EventArgs.Empty);
			};
			this.domainComboBoxPicker.Leave += this.Focus_Leave;
			this.domainComboBoxPicker.SelectedValueChanged += this.domainComboBoxPicker_SelectedValueChanged;
			new TextBoxConstraintProvider(this, "UserLogonName", this.userNameTextBox);
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00064D20 File Offset: 0x00062F20
		private void DataTableLoader_RefreshCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (e.Error == null)
			{
				DataTable table = (sender as DataTableLoader).Table;
				if (PSConnectionInfoSingleton.GetInstance().Type == OrganizationType.Cloud)
				{
					DataRow dataRow = table.Rows.OfType<DataRow>().First((DataRow row) => (bool)row["Default"]);
					if (dataRow != null)
					{
						this.DefaultDomain = dataRow["SmtpDomainToString"].ToString();
					}
				}
				else if (table.Rows.Count > 0)
				{
					this.DefaultDomain = table.Rows[0]["CanonicalName"].ToString();
				}
				if (this.domainComboBoxPicker.SelectedValue == null)
				{
					this.domainComboBoxPicker.SelectedValue = this.DefaultDomain;
				}
			}
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00064DE7 File Offset: 0x00062FE7
		private void domainComboBoxPicker_SelectedValueChanged(object sender, EventArgs e)
		{
			this.OnUserLogonNameChanged(EventArgs.Empty);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00064DF4 File Offset: 0x00062FF4
		private void userNameTextBox_TextChanged(object sender, EventArgs e)
		{
			this.OnUserLogonNameChanged(EventArgs.Empty);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00064E01 File Offset: 0x00063001
		private void Focus_Leave(object sender, EventArgs e)
		{
			base.UpdateError();
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00064E0C File Offset: 0x0006300C
		private void InitializeComponent()
		{
			this.userNameTextBox = new ExchangeTextBox();
			this.domainComboBoxPicker = new ComboBoxPicker();
			this.tableLayoutPanel = new TableLayoutPanel();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.userNameTextBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.userNameTextBox.Location = new Point(0, 0);
			this.userNameTextBox.Margin = new Padding(3, 0, 0, 1);
			this.userNameTextBox.Name = "userNameTextBox";
			this.userNameTextBox.Size = new Size(123, 20);
			this.userNameTextBox.TabIndex = 2;
			this.domainComboBoxPicker.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.domainComboBoxPicker.Location = new Point(136, 0);
			this.domainComboBoxPicker.Margin = new Padding(0);
			this.domainComboBoxPicker.Name = "domainComboBoxPicker";
			this.domainComboBoxPicker.Size = new Size(124, 21);
			this.domainComboBoxPicker.TabIndex = 3;
			this.tableLayoutPanel.AutoSize = true;
			this.tableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel.ColumnCount = 3;
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 13f));
			this.tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.tableLayoutPanel.Controls.Add(this.userNameTextBox, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.domainComboBoxPicker, 2, 0);
			this.tableLayoutPanel.Dock = DockStyle.Top;
			this.tableLayoutPanel.Location = new Point(0, 0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.RowCount = 1;
			this.tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.tableLayoutPanel.Size = new Size(260, 21);
			this.tableLayoutPanel.TabIndex = 4;
			base.Controls.Add(this.tableLayoutPanel);
			base.Name = "UserLogonNameControl";
			base.Size = new Size(260, 21);
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00065085 File Offset: 0x00063285
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x00065090 File Offset: 0x00063290
		[DefaultValue(null)]
		public ADObjectId OrganizationalUnit
		{
			get
			{
				return this.organizationalUnit;
			}
			set
			{
				if (this.OrganizationalUnit != value)
				{
					this.organizationalUnit = value;
					if (OrganizationType.Cloud != PSConnectionInfoSingleton.GetInstance().Type)
					{
						(this.domainComboBoxPicker.Picker as AutomatedObjectPicker).InputValue("OrganizationalUnit", value);
						this.domainComboBoxPicker.Picker.PerformQuery(null, string.Empty);
					}
				}
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x000650EB File Offset: 0x000632EB
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x000650F3 File Offset: 0x000632F3
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DefaultDomain
		{
			get
			{
				return this.defaultDomain;
			}
			set
			{
				this.defaultDomain = value;
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x000650FC File Offset: 0x000632FC
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x00065160 File Offset: 0x00063360
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(null)]
		public string UserLogonName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(this.userNameTextBox.Text);
				if (!string.IsNullOrEmpty(this.userNameTextBox.Text) && this.domainComboBoxPicker.SelectedValue != null)
				{
					stringBuilder.Append("@" + this.domainComboBoxPicker.SelectedValue.ToString());
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value != this.UserLogonName)
				{
					this.suspendChangeNotification = true;
					int num = (value != null) ? value.LastIndexOf("@") : -1;
					if (num >= 0)
					{
						this.userNameTextBox.Text = value.Substring(0, num);
						string text = value.Substring(num + 1);
						if (!string.IsNullOrEmpty(text))
						{
							(this.domainComboBoxPicker.Picker as AutomatedObjectPicker).InputValue("OtherSuffix", text);
							this.domainComboBoxPicker.Picker.PerformQuery(null, string.Empty);
						}
						this.domainComboBoxPicker.SelectedValue = text;
					}
					else
					{
						this.userNameTextBox.Text = value;
						this.domainComboBoxPicker.SelectedValue = this.DefaultDomain;
					}
					this.suspendChangeNotification = false;
					this.OnUserLogonNameChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00065230 File Offset: 0x00063430
		protected virtual void OnUserLogonNameChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[UserLogonNameControl.EventUserLogonNameChanged];
			if (eventHandler != null && !this.suspendChangeNotification)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x1400009F RID: 159
		// (add) Token: 0x060017ED RID: 6125 RVA: 0x00065266 File Offset: 0x00063466
		// (remove) Token: 0x060017EE RID: 6126 RVA: 0x00065279 File Offset: 0x00063479
		public event EventHandler UserLogonNameChanged
		{
			add
			{
				base.Events.AddHandler(UserLogonNameControl.EventUserLogonNameChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(UserLogonNameControl.EventUserLogonNameChanged, value);
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x0006528C File Offset: 0x0006348C
		// (set) Token: 0x060017F0 RID: 6128 RVA: 0x00065299 File Offset: 0x00063499
		[DefaultValue(0)]
		public DisplayFormatMode FormatMode
		{
			get
			{
				return this.userNameTextBox.FormatMode;
			}
			set
			{
				this.userNameTextBox.FormatMode = value;
			}
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x000652A8 File Offset: 0x000634A8
		protected virtual void OnFormatModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[UserLogonNameControl.EventFormatModeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140000A0 RID: 160
		// (add) Token: 0x060017F2 RID: 6130 RVA: 0x000652D6 File Offset: 0x000634D6
		// (remove) Token: 0x060017F3 RID: 6131 RVA: 0x000652E9 File Offset: 0x000634E9
		public event EventHandler FormatModeChanged
		{
			add
			{
				base.Events.AddHandler(UserLogonNameControl.EventFormatModeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(UserLogonNameControl.EventFormatModeChanged, value);
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x000652FC File Offset: 0x000634FC
		protected override string ExposedPropertyName
		{
			get
			{
				return "UserLogonName";
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00065319 File Offset: 0x00063519
		void IFormatModeProvider.add_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged += A_1;
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00065322 File Offset: 0x00063522
		void IFormatModeProvider.remove_BindingContextChanged(EventHandler A_1)
		{
			base.BindingContextChanged -= A_1;
		}

		// Token: 0x040008EC RID: 2284
		private ExchangeTextBox userNameTextBox;

		// Token: 0x040008ED RID: 2285
		private ComboBoxPicker domainComboBoxPicker;

		// Token: 0x040008EE RID: 2286
		private TableLayoutPanel tableLayoutPanel;

		// Token: 0x040008EF RID: 2287
		private bool suspendChangeNotification;

		// Token: 0x040008F0 RID: 2288
		private ADObjectId organizationalUnit;

		// Token: 0x040008F1 RID: 2289
		private string defaultDomain;

		// Token: 0x040008F2 RID: 2290
		private static readonly object EventUserLogonNameChanged = new object();

		// Token: 0x040008F3 RID: 2291
		private static readonly object EventFormatModeChanged = new object();
	}
}
