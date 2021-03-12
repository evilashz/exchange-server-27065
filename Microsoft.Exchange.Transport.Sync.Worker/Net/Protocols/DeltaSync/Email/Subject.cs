using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Protocols.DeltaSync.Email
{
	// Token: 0x0200008C RID: 140
	[XmlRoot(ElementName = "Subject", Namespace = "EMAIL:", IsNullable = false)]
	[Serializable]
	public class Subject : stringWithEncodingType2
	{
	}
}
