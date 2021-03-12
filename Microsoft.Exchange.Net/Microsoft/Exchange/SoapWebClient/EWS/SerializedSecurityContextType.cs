using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020006D2 RID: 1746
	[CLSCompliant(false)]
	[XmlRoot("SerializedSecurityContext", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SerializedSecurityContextType : SoapHeader
	{
		// Token: 0x04001F58 RID: 8024
		public string UserSid;

		// Token: 0x04001F59 RID: 8025
		[XmlArrayItem("GroupIdentifier", IsNullable = false)]
		public SidAndAttributesType[] GroupSids;

		// Token: 0x04001F5A RID: 8026
		[XmlArrayItem("RestrictedGroupIdentifier", IsNullable = false)]
		public SidAndAttributesType[] RestrictedGroupSids;

		// Token: 0x04001F5B RID: 8027
		public string PrimarySmtpAddress;

		// Token: 0x04001F5C RID: 8028
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;
	}
}
