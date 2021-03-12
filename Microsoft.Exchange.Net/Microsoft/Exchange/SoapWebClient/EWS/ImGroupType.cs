using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001BB RID: 443
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class ImGroupType
	{
		// Token: 0x04000A30 RID: 2608
		public string DisplayName;

		// Token: 0x04000A31 RID: 2609
		public string GroupType;

		// Token: 0x04000A32 RID: 2610
		public ItemIdType ExchangeStoreId;

		// Token: 0x04000A33 RID: 2611
		[XmlArrayItem("ItemId", IsNullable = false)]
		public ItemIdType[] MemberCorrelationKey;

		// Token: 0x04000A34 RID: 2612
		public NonEmptyArrayOfExtendedPropertyType ExtendedProperties;

		// Token: 0x04000A35 RID: 2613
		public string SmtpAddress;
	}
}
