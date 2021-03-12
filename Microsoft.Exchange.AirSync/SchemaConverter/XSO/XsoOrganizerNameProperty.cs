using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200022C RID: 556
	[Serializable]
	internal class XsoOrganizerNameProperty : XsoStringProperty
	{
		// Token: 0x060014DF RID: 5343 RVA: 0x00079509 File Offset: 0x00077709
		public XsoOrganizerNameProperty() : base(ItemSchema.SentRepresentingDisplayName)
		{
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00079516 File Offset: 0x00077716
		protected override void InternalSetToDefault(IProperty srcProperty)
		{
		}
	}
}
