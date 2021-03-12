using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002BF RID: 703
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class UserConfigurationType
	{
		// Token: 0x040011FE RID: 4606
		public UserConfigurationNameType UserConfigurationName;

		// Token: 0x040011FF RID: 4607
		public ItemIdType ItemId;

		// Token: 0x04001200 RID: 4608
		[XmlArrayItem("DictionaryEntry", IsNullable = false)]
		public UserConfigurationDictionaryEntryType[] Dictionary;

		// Token: 0x04001201 RID: 4609
		[XmlElement(DataType = "base64Binary")]
		public byte[] XmlData;

		// Token: 0x04001202 RID: 4610
		[XmlElement(DataType = "base64Binary")]
		public byte[] BinaryData;
	}
}
