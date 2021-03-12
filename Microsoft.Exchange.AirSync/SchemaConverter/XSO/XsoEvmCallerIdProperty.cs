using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000219 RID: 537
	internal class XsoEvmCallerIdProperty : XsoEvmStringProperty
	{
		// Token: 0x06001489 RID: 5257 RVA: 0x00076B8C File Offset: 0x00074D8C
		public XsoEvmCallerIdProperty(PropertyType type) : base(null, type, new PropertyDefinition[]
		{
			MessageItemSchema.SenderTelephoneNumber,
			MessageItemSchema.PstnCallbackTelephoneNumber
		})
		{
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00076BBC File Offset: 0x00074DBC
		public override string StringData
		{
			get
			{
				string text = base.XsoItem.TryGetProperty(MessageItemSchema.PstnCallbackTelephoneNumber) as string;
				if (string.IsNullOrEmpty(text))
				{
					text = (base.XsoItem.TryGetProperty(MessageItemSchema.SenderTelephoneNumber) as string);
				}
				if (text != null && text.IndexOf("@", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					text = string.Empty;
				}
				if (string.IsNullOrEmpty(text))
				{
					base.State = PropertyState.SetToDefault;
				}
				return text;
			}
		}
	}
}
