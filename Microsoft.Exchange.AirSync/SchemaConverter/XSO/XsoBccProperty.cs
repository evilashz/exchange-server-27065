using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x02000204 RID: 516
	[Serializable]
	internal class XsoBccProperty : XsoRecipientProperty
	{
		// Token: 0x06001405 RID: 5125 RVA: 0x00073B7C File Offset: 0x00071D7C
		public XsoBccProperty(PropertyType type) : base(RecipientItemType.Bcc, type)
		{
		}
	}
}
