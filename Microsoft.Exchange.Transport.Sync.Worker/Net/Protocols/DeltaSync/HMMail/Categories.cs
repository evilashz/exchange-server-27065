using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.HMMail
{
	// Token: 0x0200009B RID: 155
	[XmlRoot(ElementName = "Categories", Namespace = "HMMAIL:", IsNullable = false)]
	[Serializable]
	public class Categories : stringWithCharSetType
	{
	}
}
