using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.Email
{
	// Token: 0x0200008B RID: 139
	[XmlRoot(ElementName = "From", Namespace = "EMAIL:", IsNullable = false)]
	[Serializable]
	public class From : stringWithEncodingType2
	{
	}
}
