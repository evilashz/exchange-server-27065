using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200025D RID: 605
	internal class ConvertOutputPropertyEventArgs : EventArgs
	{
		// Token: 0x06001463 RID: 5219 RVA: 0x000400BC File Offset: 0x0003E2BC
		public ConvertOutputPropertyEventArgs(object value, ConfigurableObject configurableObject, PropertyDefinition property, string propertyInStr)
		{
			this.Value = value;
			this.ConfigurableObject = configurableObject;
			this.Property = property;
			this.PropertyInStr = propertyInStr;
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x000400E1 File Offset: 0x0003E2E1
		// (set) Token: 0x06001465 RID: 5221 RVA: 0x000400E9 File Offset: 0x0003E2E9
		public object Value { get; set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x000400F2 File Offset: 0x0003E2F2
		// (set) Token: 0x06001467 RID: 5223 RVA: 0x000400FA File Offset: 0x0003E2FA
		public PropertyDefinition Property { get; set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x00040103 File Offset: 0x0003E303
		// (set) Token: 0x06001469 RID: 5225 RVA: 0x0004010B File Offset: 0x0003E30B
		public string PropertyInStr { get; set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x00040114 File Offset: 0x0003E314
		// (set) Token: 0x0600146B RID: 5227 RVA: 0x0004011C File Offset: 0x0003E31C
		public ConfigurableObject ConfigurableObject { get; set; }
	}
}
