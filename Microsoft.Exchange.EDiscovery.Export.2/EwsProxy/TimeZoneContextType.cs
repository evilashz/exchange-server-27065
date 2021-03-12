using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000A6 RID: 166
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[XmlRoot("TimeZoneContext", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[Serializable]
	public class TimeZoneContextType : SoapHeader
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0001FABC File Offset: 0x0001DCBC
		public TimeZoneDefinitionType TimeZoneDefinition
		{
			get
			{
				return this.timeZoneDefinitionField;
			}
			set
			{
				this.timeZoneDefinitionField = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0001FAC5 File Offset: 0x0001DCC5
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x0001FACD File Offset: 0x0001DCCD
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return this.anyAttrField;
			}
			set
			{
				this.anyAttrField = value;
			}
		}

		// Token: 0x0400035C RID: 860
		private TimeZoneDefinitionType timeZoneDefinitionField;

		// Token: 0x0400035D RID: 861
		private XmlAttribute[] anyAttrField;
	}
}
