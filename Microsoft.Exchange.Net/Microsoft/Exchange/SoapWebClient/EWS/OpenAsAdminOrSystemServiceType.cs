using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020006D1 RID: 1745
	[CLSCompliant(false)]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlRoot("OpenAsAdminOrSystemService", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[Serializable]
	public class OpenAsAdminOrSystemServiceType : SoapHeader
	{
		// Token: 0x04001F53 RID: 8019
		public ConnectingSIDType ConnectingSID;

		// Token: 0x04001F54 RID: 8020
		[XmlAttribute]
		public SpecialLogonType LogonType;

		// Token: 0x04001F55 RID: 8021
		[XmlAttribute]
		public int BudgetType;

		// Token: 0x04001F56 RID: 8022
		[XmlIgnore]
		public bool BudgetTypeSpecified;

		// Token: 0x04001F57 RID: 8023
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;
	}
}
