using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Connections.Eas.Model.Response.Autodiscover
{
	// Token: 0x020000BF RID: 191
	[XmlType(TypeName = "ServerType")]
	public enum ServerType
	{
		// Token: 0x0400056E RID: 1390
		MobileSync = 1,
		// Token: 0x0400056F RID: 1391
		CertEnroll
	}
}
