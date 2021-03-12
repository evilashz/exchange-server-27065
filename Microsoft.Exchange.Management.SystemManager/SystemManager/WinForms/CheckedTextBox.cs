using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ManagementGUI.Resources;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B4 RID: 436
	public class CheckedTextBox : CaptionedTextBox
	{
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x000459E7 File Offset: 0x00043BE7
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x000459EF File Offset: 0x00043BEF
		[DefaultValue("")]
		public string DefaultText
		{
			get
			{
				return this.defaultText;
			}
			set
			{
				this.defaultText = value;
			}
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x00045A88 File Offset: 0x00043C88
		public CheckedTextBox()
		{
			this.InitializeComponent();
			this.exchangeTextBox.DataBindings.Add("ReadOnly", this.checkboxCaption, "Checked", true, DataSourceUpdateMode.Never).Format += delegate(object sender, ConvertEventArgs e)
			{
				e.Value = !(bool)e.Value;
			};
			this.checkboxCaption.CheckedChanged += delegate(object param0, EventArgs param1)
			{
				if (this.checkboxCaption.Checked)
				{
					this.exchangeTextBox.Focus();
					this.Text = this.DefaultText;
					if (!string.IsNullOrEmpty(this.Text))
					{
						this.exchangeTextBox.SelectionStart = this.Text.Length;
						return;
					}
				}
				else
				{
					this.Text = string.Empty;
				}
			};
			base.BindingContextChanged += delegate(object param0, EventArgs param1)
			{
				this.DataBindings_CollectionChanged(null, new CollectionChangeEventArgs(CollectionChangeAction.Refresh, null));
			};
			base.DataBindings.CollectionChanged += this.DataBindings_CollectionChanged;
			base.Name = "CheckedTextBox";
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00045B5C File Offset: 0x00043D5C
		private void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
		{
			Binding binding = (Binding)e.Element;
			switch (e.Action)
			{
			case CollectionChangeAction.Add:
				if (binding.PropertyName == "Text")
				{
					this.SetUncheckedValueByPropertyType(binding);
					binding.Parse += this.Binding_Parse;
					binding.Format += this.Binding_Format;
					return;
				}
				break;
			case CollectionChangeAction.Remove:
				break;
			case CollectionChangeAction.Refresh:
				binding = base.DataBindings["Text"];
				if (binding != null)
				{
					binding.Parse -= this.Binding_Parse;
					binding.Format -= this.Binding_Format;
					this.SetUncheckedValueByPropertyType(binding);
					binding.Parse += this.Binding_Parse;
					binding.Format += this.Binding_Format;
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00045C35 File Offset: 0x00043E35
		private void Binding_Parse(object s, ConvertEventArgs e)
		{
			if (this.checkboxCaption.Checked && string.IsNullOrEmpty(this.exchangeTextBox.Text))
			{
				throw new ArgumentException(Strings.ValueCanNotBeEmpty);
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00045C68 File Offset: 0x00043E68
		private void Binding_Format(object s, ConvertEventArgs e)
		{
			this.checkboxCaption.Checked = (e.Value != null && !string.IsNullOrEmpty(e.Value.ToString()) && (this.uncheckedValue == null || !this.uncheckedValue.Equals(e.Value.ToString())));
			if (!this.checkboxCaption.Checked)
			{
				e.Value = string.Empty;
			}
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00045CDC File Offset: 0x00043EDC
		private void SetUncheckedValueByPropertyType(Binding binding)
		{
			if (binding.BindingManagerBase != null)
			{
				PropertyDescriptorCollection itemProperties = binding.BindingManagerBase.GetItemProperties();
				PropertyDescriptor propertyDescriptor = itemProperties.Find(binding.BindingMemberInfo.BindingField, true);
				if (propertyDescriptor != null)
				{
					FilterValuePropertyDescriptor filterValuePropertyDescriptor = propertyDescriptor as FilterValuePropertyDescriptor;
					Type type = (filterValuePropertyDescriptor != null) ? filterValuePropertyDescriptor.ValuePropertyType : propertyDescriptor.PropertyType;
					if (type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Unlimited<>) || (type.GetGenericTypeDefinition() == typeof(Nullable<>) && type.GetGenericArguments()[0].IsGenericType && type.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(Unlimited<>))))
					{
						this.uncheckedValue = this.unlimitedString;
						return;
					}
					this.uncheckedValue = null;
				}
			}
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00045DB0 File Offset: 0x00043FB0
		private void InitializeComponent()
		{
			this.checkboxCaption = new AutoHeightCheckBox();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.Controls.Add(this.checkboxCaption, 0, 0);
			this.checkboxCaption.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.checkboxCaption.Checked = true;
			this.checkboxCaption.CheckState = CheckState.Checked;
			this.checkboxCaption.Location = new Point(0, 0);
			this.checkboxCaption.Margin = new Padding(3, 2, 0, 1);
			this.checkboxCaption.Name = "checkboxCaption";
			this.checkboxCaption.TabIndex = 0;
			this.checkboxCaption.UseVisualStyleBackColor = true;
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x00045E88 File Offset: 0x00044088
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x00045E95 File Offset: 0x00044095
		[DefaultValue("")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Caption
		{
			get
			{
				return this.checkboxCaption.Text;
			}
			set
			{
				this.checkboxCaption.Text = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060011A6 RID: 4518 RVA: 0x00045EA3 File Offset: 0x000440A3
		// (set) Token: 0x060011A7 RID: 4519 RVA: 0x00045EC7 File Offset: 0x000440C7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue("")]
		[Browsable(true)]
		public override string Text
		{
			get
			{
				if (this.checkboxCaption == null || !this.checkboxCaption.Checked)
				{
					return this.uncheckedValue;
				}
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x040006BD RID: 1725
		private string defaultText = string.Empty;

		// Token: 0x040006BE RID: 1726
		private string unlimitedString = Strings.UnlimitedString;

		// Token: 0x040006BF RID: 1727
		private string uncheckedValue;

		// Token: 0x040006C0 RID: 1728
		private AutoHeightCheckBox checkboxCaption;
	}
}
