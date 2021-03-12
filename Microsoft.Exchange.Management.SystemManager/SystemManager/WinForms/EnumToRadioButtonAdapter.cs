using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x0200012F RID: 303
	public class EnumToRadioButtonAdapter : ExchangeUserControl
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x0002B4BF File Offset: 0x000296BF
		public EnumToRadioButtonAdapter()
		{
			base.Name = "EnumToRadioButtonAdapter";
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0002B4E8 File Offset: 0x000296E8
		public void AddRadioButtonToEnumEntry(RadioButton radioButton, Enum enumValue)
		{
			this.radioButtonToEnumDictionary[radioButton] = enumValue;
			this.enumToRadioButtonDictionary[enumValue] = radioButton;
			radioButton.CheckedChanged += this.RadioButton_CheckedChanged;
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0002B518 File Offset: 0x00029718
		private void RadioButton_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton radioButton = (RadioButton)sender;
			if (radioButton.Checked)
			{
				this.SelectedValue = this.radioButtonToEnumDictionary[radioButton];
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x0002B546 File Offset: 0x00029746
		// (set) Token: 0x06000C10 RID: 3088 RVA: 0x0002B550 File Offset: 0x00029750
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Enum SelectedValue
		{
			get
			{
				return this.selectedValue;
			}
			set
			{
				if (!object.Equals(value, this.SelectedValue))
				{
					RadioButton radioButton = (this.SelectedValue != null) ? this.enumToRadioButtonDictionary[this.SelectedValue] : null;
					this.selectedValue = value;
					RadioButton radioButton2 = (this.SelectedValue != null) ? this.enumToRadioButtonDictionary[this.SelectedValue] : null;
					if (radioButton2 != null)
					{
						radioButton2.Checked = true;
					}
					if (radioButton != null)
					{
						radioButton.Checked = false;
					}
					this.OnSelectedValueChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x0002B5CC File Offset: 0x000297CC
		protected virtual void OnSelectedValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[EnumToRadioButtonAdapter.EventSelectedValueChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06000C12 RID: 3090 RVA: 0x0002B5FA File Offset: 0x000297FA
		// (remove) Token: 0x06000C13 RID: 3091 RVA: 0x0002B60D File Offset: 0x0002980D
		public event EventHandler SelectedValueChanged
		{
			add
			{
				base.Events.AddHandler(EnumToRadioButtonAdapter.EventSelectedValueChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(EnumToRadioButtonAdapter.EventSelectedValueChanged, value);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002B620 File Offset: 0x00029820
		protected override string ExposedPropertyName
		{
			get
			{
				return "SelectedValue";
			}
		}

		// Token: 0x040004F2 RID: 1266
		private static readonly object EventSelectedValueChanged = new object();

		// Token: 0x040004F3 RID: 1267
		private Enum selectedValue;

		// Token: 0x040004F4 RID: 1268
		protected Dictionary<RadioButton, Enum> radioButtonToEnumDictionary = new Dictionary<RadioButton, Enum>();

		// Token: 0x040004F5 RID: 1269
		private Dictionary<Enum, RadioButton> enumToRadioButtonDictionary = new Dictionary<Enum, RadioButton>();
	}
}
