using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002B3 RID: 691
	[Serializable]
	internal sealed class CheckboxControl : DetailsTemplateControl
	{
		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0008C769 File Offset: 0x0008A969
		// (set) Token: 0x06001FBA RID: 8122 RVA: 0x0008C771 File Offset: 0x0008A971
		public string AttributeName
		{
			get
			{
				return this.m_AttributeName;
			}
			set
			{
				DetailsTemplateControl.ValidateAttributeName(value, this.GetAttributeControlType(), base.GetType().Name);
				this.m_AttributeName = value;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0008C791 File Offset: 0x0008A991
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x0008C799 File Offset: 0x0008A999
		public string Text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				if (this.m_Text != value)
				{
					DetailsTemplateControl.ValidateText(value, DetailsTemplateControl.TextLengths.Label);
					this.m_Text = value;
					base.NotifyPropertyChanged("Text");
				}
			}
		}

		// Token: 0x06001FBE RID: 8126 RVA: 0x0008C7CB File Offset: 0x0008A9CB
		internal override DetailsTemplateControl.AttributeControlTypes GetAttributeControlType()
		{
			return DetailsTemplateControl.AttributeControlTypes.Checkbox;
		}

		// Token: 0x06001FBF RID: 8127 RVA: 0x0008C7CE File Offset: 0x0008A9CE
		internal override DetailsTemplateControl.ControlTypes GetControlType()
		{
			return DetailsTemplateControl.ControlTypes.Checkbox;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0008C7D1 File Offset: 0x0008A9D1
		internal override bool ValidateAttribute(MAPIPropertiesDictionary propertiesDictionary)
		{
			return base.ValidateAttributeHelper(propertiesDictionary);
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0008C7DA File Offset: 0x0008A9DA
		internal override DetailsTemplateControl.MapiPrefix GetMapiPrefix()
		{
			return DetailsTemplateControl.MapiPrefix.Checkbox;
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0008C7DE File Offset: 0x0008A9DE
		public override string ToString()
		{
			return "Check Box";
		}
	}
}
