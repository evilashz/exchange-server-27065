using System;
using Microsoft.Exchange.AirSync.SchemaConverter.Common;

namespace Microsoft.Exchange.AirSync.SchemaConverter.XSO
{
	// Token: 0x0200023F RID: 575
	[Serializable]
	internal class XsoUnlinkedProperty : XsoProperty, IUnlinkedProperty, IProperty
	{
		// Token: 0x0600152C RID: 5420 RVA: 0x0007BFEF File Offset: 0x0007A1EF
		public XsoUnlinkedProperty() : base(null, PropertyType.ReadOnly)
		{
		}
	}
}
