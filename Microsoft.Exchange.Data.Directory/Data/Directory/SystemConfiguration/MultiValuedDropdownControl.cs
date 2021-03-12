using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002F0 RID: 752
	[Serializable]
	public sealed class MultiValuedDropdownControl : DetailsTemplateControl
	{
		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x00098ADB File Offset: 0x00096CDB
		// (set) Token: 0x06002309 RID: 8969 RVA: 0x00098AE3 File Offset: 0x00096CE3
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

		// Token: 0x0600230A RID: 8970 RVA: 0x00098B03 File Offset: 0x00096D03
		internal override DetailsTemplateControl.ControlTypes GetControlType()
		{
			return DetailsTemplateControl.ControlTypes.MultiValuedDropdown;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00098B07 File Offset: 0x00096D07
		internal override DetailsTemplateControl.AttributeControlTypes GetAttributeControlType()
		{
			return DetailsTemplateControl.AttributeControlTypes.MultiValued;
		}

		// Token: 0x0600230C RID: 8972 RVA: 0x00098B0A File Offset: 0x00096D0A
		public MultiValuedDropdownControl()
		{
			this.m_Text = DetailsTemplateControl.NoTextString;
		}

		// Token: 0x0600230D RID: 8973 RVA: 0x00098B1D File Offset: 0x00096D1D
		internal override DetailsTemplateControl.MapiPrefix GetMapiPrefix()
		{
			return DetailsTemplateControl.MapiPrefix.MultiValued;
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00098B24 File Offset: 0x00096D24
		internal override bool ValidateAttribute(MAPIPropertiesDictionary propertiesDictionary)
		{
			return base.ValidateAttributeHelper(propertiesDictionary);
		}

		// Token: 0x0600230F RID: 8975 RVA: 0x00098B2D File Offset: 0x00096D2D
		public override string ToString()
		{
			return "Multi-Valued Drop Down";
		}
	}
}
