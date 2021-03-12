using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000325 RID: 805
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetImItemListType : BaseRequestType
	{
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x00028C9D File Offset: 0x00026E9D
		// (set) Token: 0x06001A2F RID: 6703 RVA: 0x00028CA5 File Offset: 0x00026EA5
		[XmlArrayItem("ExtendedProperty", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public PathToExtendedFieldType[] ExtendedProperties
		{
			get
			{
				return this.extendedPropertiesField;
			}
			set
			{
				this.extendedPropertiesField = value;
			}
		}

		// Token: 0x04001194 RID: 4500
		private PathToExtendedFieldType[] extendedPropertiesField;
	}
}
