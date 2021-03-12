using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	internal class XsoCCProperty : XsoRecipientProperty
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x00074A84 File Offset: 0x00072C84
		public XsoCCProperty(PropertyType type) : base(RecipientItemType.Cc, type)
		{
		}
	}
}
