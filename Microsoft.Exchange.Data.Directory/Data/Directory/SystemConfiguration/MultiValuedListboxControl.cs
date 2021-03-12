using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002F1 RID: 753
	[Serializable]
	public sealed class MultiValuedListboxControl : DetailsTemplateControl
	{
		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x00098B34 File Offset: 0x00096D34
		// (set) Token: 0x06002311 RID: 8977 RVA: 0x00098B3C File Offset: 0x00096D3C
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

		// Token: 0x06002312 RID: 8978 RVA: 0x00098B5C File Offset: 0x00096D5C
		internal override DetailsTemplateControl.ControlTypes GetControlType()
		{
			return DetailsTemplateControl.ControlTypes.MultiValuedListbox;
		}

		// Token: 0x06002313 RID: 8979 RVA: 0x00098B60 File Offset: 0x00096D60
		internal override DetailsTemplateControl.AttributeControlTypes GetAttributeControlType()
		{
			return DetailsTemplateControl.AttributeControlTypes.MultiValued;
		}

		// Token: 0x06002314 RID: 8980 RVA: 0x00098B63 File Offset: 0x00096D63
		public MultiValuedListboxControl()
		{
			this.m_Text = DetailsTemplateControl.NoTextString;
		}

		// Token: 0x06002315 RID: 8981 RVA: 0x00098B76 File Offset: 0x00096D76
		internal override DetailsTemplateControl.MapiPrefix GetMapiPrefix()
		{
			return DetailsTemplateControl.MapiPrefix.MultiValued;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00098B7D File Offset: 0x00096D7D
		internal override bool ValidateAttribute(MAPIPropertiesDictionary propertiesDictionary)
		{
			return base.ValidateAttributeHelper(propertiesDictionary);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00098B86 File Offset: 0x00096D86
		public override string ToString()
		{
			return "Multi-Valued List Box";
		}
	}
}
