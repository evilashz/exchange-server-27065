using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200023A RID: 570
	internal class XsoSubjectProperty : XsoStringProperty
	{
		// Token: 0x06001516 RID: 5398 RVA: 0x0007BA2B File Offset: 0x00079C2B
		public XsoSubjectProperty() : base(ItemSchema.Subject)
		{
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0007BA38 File Offset: 0x00079C38
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
			base.XsoItem.DeleteProperties(new PropertyDefinition[]
			{
				base.PropertyDef
			});
		}
	}
}
