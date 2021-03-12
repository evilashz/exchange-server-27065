using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ManagementGUI.Resources;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200022F RID: 559
	public class PickerLauncherTextBox : PickerTextBoxBase
	{
		// Token: 0x060019DC RID: 6620 RVA: 0x000705C1 File Offset: 0x0006E7C1
		public PickerLauncherTextBox()
		{
			base.UpdateBrowseButtonState();
			base.Name = "PickerLauncherTextBox";
			base.TextChanged += this.PickerLauncherTextBox_TextChanged;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x000705EC File Offset: 0x0006E7EC
		private void PickerLauncherTextBox_TextChanged(object sender, EventArgs e)
		{
			if (!base.TextBoxReadOnly)
			{
				this.SelectedValue = (string.IsNullOrEmpty(this.Text) ? null : this.Text);
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x00070614 File Offset: 0x0006E814
		protected override void OnBrowseButtonClick(CancelEventArgs e)
		{
			base.OnBrowseButtonClick(e);
			bool allowMultiSelect = this.Picker.AllowMultiSelect;
			this.Picker.AllowMultiSelect = false;
			try
			{
				if (this.Picker.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(this.ValueMember))
					{
						base.NotifyExposedPropertyIsModified();
						this.SelectedValue = this.Picker.SelectedObjects.Rows[0][this.ValueMember];
					}
				}
				else
				{
					e.Cancel = true;
				}
			}
			catch (ObjectPickerException ex)
			{
				base.ShowError(ex.Message);
			}
			this.Picker.AllowMultiSelect = allowMultiSelect;
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x000706C0 File Offset: 0x0006E8C0
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x000706C8 File Offset: 0x0006E8C8
		[DefaultValue(null)]
		public ObjectPickerBase Picker
		{
			get
			{
				return this.picker;
			}
			set
			{
				if (value != this.Picker)
				{
					if (this.Picker != null)
					{
						base.Components.Remove(this.Picker);
					}
					this.picker = value;
					if (this.Picker != null)
					{
						base.Components.Add(this.Picker);
					}
					if (this.resolver != null)
					{
						this.resolver.ResolveObjectIdsCompleted -= this.resolver_ResolveObjectIdsCompleted;
					}
					if (value != null && value is ObjectPicker)
					{
						this.resolver = new ObjectResolver(value as ObjectPicker);
						this.resolver.ResolveObjectIdsCompleted += this.resolver_ResolveObjectIdsCompleted;
					}
					else
					{
						this.resolver = null;
					}
					base.UpdateBrowseButtonState();
				}
			}
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0007077C File Offset: 0x0006E97C
		private void resolver_ResolveObjectIdsCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			base.UpdateBrowseButtonState();
			ValueToDisplayObjectConverter valueToDisplayObjectConverter = (this.DisplayMemberConverter != null) ? this.DisplayMemberConverter : new ToStringValueToDisplayObjectConverter();
			object obj = null;
			if (this.resolver.ResolvedObjects != null && this.resolver.ResolvedObjects.Rows.Count > 0)
			{
				obj = this.resolver.ResolvedObjects.Rows[0][this.DisplayMember];
			}
			obj = (obj ?? this.SelectedValue);
			if (obj != null)
			{
				obj = valueToDisplayObjectConverter.Convert(obj);
			}
			this.Text = ((obj == null) ? string.Empty : obj.ToString());
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0007081B File Offset: 0x0006EA1B
		protected override bool ButtonAvailable()
		{
			return base.ButtonAvailable() && this.Picker != null && (this.resolver == null || !this.resolver.IsResolving);
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00070847 File Offset: 0x0006EA47
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x0007084F File Offset: 0x0006EA4F
		[DefaultValue(null)]
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

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00070858 File Offset: 0x0006EA58
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x00070860 File Offset: 0x0006EA60
		[DefaultValue(null)]
		internal ADPropertyDefinition ValueMemberPropertyDefinition
		{
			get
			{
				return this.valueMemberPropertyDefinition;
			}
			set
			{
				this.valueMemberPropertyDefinition = value;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00070869 File Offset: 0x0006EA69
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x00070871 File Offset: 0x0006EA71
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public ValueToDisplayObjectConverter ValueMemberConverter
		{
			get
			{
				return this.valueMemberConverter;
			}
			set
			{
				this.valueMemberConverter = value;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x0007087A File Offset: 0x0006EA7A
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x00070882 File Offset: 0x0006EA82
		[DefaultValue(null)]
		public ValueToDisplayObjectConverter DisplayMemberConverter
		{
			get
			{
				return this.displayMemberConverter;
			}
			set
			{
				this.displayMemberConverter = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0007088B File Offset: 0x0006EA8B
		// (set) Token: 0x060019EC RID: 6636 RVA: 0x00070894 File Offset: 0x0006EA94
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get
			{
				return this.selectedValue;
			}
			set
			{
				if ((this.selectedValue == null) ? (value != null) : (!this.selectedValue.Equals(value)))
				{
					this.selectedValue = value;
					this.picker.InputObject = value;
					this.UpdateDisplay();
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000708E8 File Offset: 0x0006EAE8
		internal void UpdateDisplay()
		{
			if (!base.TextBoxReadOnly || this.DisplayMember == null || string.Equals(this.ValueMember, this.DisplayMember))
			{
				ValueToDisplayObjectConverter valueToDisplayObjectConverter = (this.ValueMemberConverter != null) ? this.ValueMemberConverter : new ToStringValueToDisplayObjectConverter();
				this.Text = ((this.SelectedValue == null || DBNull.Value == this.SelectedValue) ? string.Empty : valueToDisplayObjectConverter.Convert(this.SelectedValue).ToString());
				return;
			}
			if (this.SelectedValue != null && this.Picker != null)
			{
				object obj = null;
				ValueToDisplayObjectConverter valueToDisplayObjectConverter2 = (this.DisplayMemberConverter != null) ? this.DisplayMemberConverter : new ToStringValueToDisplayObjectConverter();
				if (this.Picker.SelectedObjects != null && this.Picker.SelectedObjects.Rows.Count > 0)
				{
					obj = this.Picker.SelectedObjects.Rows[0][this.DisplayMember];
				}
				else if (this.resolver != null && (this.SelectedValue is ADObjectId || this.ValueMemberPropertyDefinition != null) && !string.IsNullOrEmpty(this.SelectedValue.ToString()))
				{
					ADPropertyDefinition property = this.ValueMemberPropertyDefinition ?? ADObjectSchema.Id;
					this.resolver.ResolveObjectIds(property, new List<object>(new object[]
					{
						this.SelectedValue
					}));
					base.UpdateBrowseButtonState();
				}
				else
				{
					obj = this.SelectedValue;
				}
				this.Text = ((obj == null) ? string.Empty : valueToDisplayObjectConverter2.Convert(obj).ToString());
				return;
			}
			this.Text = string.Empty;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00070A74 File Offset: 0x0006EC74
		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[PickerLauncherTextBox.EventSelectedValueChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x140000AF RID: 175
		// (add) Token: 0x060019EF RID: 6639 RVA: 0x00070AA2 File Offset: 0x0006ECA2
		// (remove) Token: 0x060019F0 RID: 6640 RVA: 0x00070AB5 File Offset: 0x0006ECB5
		public event EventHandler SelectedValueChanged
		{
			add
			{
				base.Events.AddHandler(PickerLauncherTextBox.EventSelectedValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(PickerLauncherTextBox.EventSelectedValueChanged, value);
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060019F1 RID: 6641 RVA: 0x00070AC8 File Offset: 0x0006ECC8
		// (set) Token: 0x060019F2 RID: 6642 RVA: 0x00070AD0 File Offset: 0x0006ECD0
		[DefaultValue(null)]
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

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00070AD9 File Offset: 0x0006ECD9
		// (set) Token: 0x060019F4 RID: 6644 RVA: 0x00070AE1 File Offset: 0x0006ECE1
		internal string DisplayedText
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00070AF4 File Offset: 0x0006ECF4
		protected override string ExposedPropertyName
		{
			get
			{
				return "SelectedValue";
			}
		}

		// Token: 0x040009A9 RID: 2473
		private ObjectPickerBase picker;

		// Token: 0x040009AA RID: 2474
		private ObjectResolver resolver;

		// Token: 0x040009AB RID: 2475
		private string displayMember;

		// Token: 0x040009AC RID: 2476
		private string valueMember;

		// Token: 0x040009AD RID: 2477
		private object selectedValue;

		// Token: 0x040009AE RID: 2478
		private ValueToDisplayObjectConverter valueMemberConverter;

		// Token: 0x040009AF RID: 2479
		private ValueToDisplayObjectConverter displayMemberConverter;

		// Token: 0x040009B0 RID: 2480
		private ADPropertyDefinition valueMemberPropertyDefinition;

		// Token: 0x040009B1 RID: 2481
		private static readonly object EventSelectedValueChanged = new object();
	}
}
