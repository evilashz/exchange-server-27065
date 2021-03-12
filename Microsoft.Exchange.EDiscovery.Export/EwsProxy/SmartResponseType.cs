using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200016C RID: 364
	[DesignerCategory("code")]
	[XmlInclude(typeof(ForwardItemType))]
	[XmlInclude(typeof(ReplyAllToItemType))]
	[XmlInclude(typeof(ReplyToItemType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(CancelCalendarItemType))]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class SmartResponseType : SmartResponseBaseType
	{
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0002398C File Offset: 0x00021B8C
		// (set) Token: 0x06001053 RID: 4179 RVA: 0x00023994 File Offset: 0x00021B94
		public BodyType NewBodyContent
		{
			get
			{
				return this.newBodyContentField;
			}
			set
			{
				this.newBodyContentField = value;
			}
		}

		// Token: 0x04000B31 RID: 2865
		private BodyType newBodyContentField;
	}
}
