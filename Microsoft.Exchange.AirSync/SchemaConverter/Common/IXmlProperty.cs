using System;
using System.Xml;

namespace Microsoft.Exchange.AirSync.SchemaConverter.Common
{
	// Token: 0x0200016D RID: 365
	internal interface IXmlProperty : IProperty
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001040 RID: 4160
		XmlNode XmlData { get; }
	}
}
