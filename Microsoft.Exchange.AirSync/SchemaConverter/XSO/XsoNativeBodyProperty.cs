using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200022A RID: 554
	[Serializable]
	internal class XsoNativeBodyProperty : XsoIntegerProperty
	{
		// Token: 0x060014D9 RID: 5337 RVA: 0x00079430 File Offset: 0x00077630
		public XsoNativeBodyProperty() : base(null, PropertyType.ReadOnly)
		{
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x0007943C File Offset: 0x0007763C
		public override int IntegerData
		{
			get
			{
				Item item = base.XsoItem as Item;
				if (item == null)
				{
					return 0;
				}
				BodyType result;
				switch (item.Body.Format)
				{
				case BodyFormat.TextPlain:
					result = BodyType.PlainText;
					break;
				case BodyFormat.TextHtml:
					result = BodyType.Html;
					break;
				case BodyFormat.ApplicationRtf:
					result = BodyType.Rtf;
					break;
				default:
					throw new ConversionException("Unknown BodyFormat implemented by XSO");
				}
				return (int)result;
			}
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00079494 File Offset: 0x00077694
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
		}
	}
}
