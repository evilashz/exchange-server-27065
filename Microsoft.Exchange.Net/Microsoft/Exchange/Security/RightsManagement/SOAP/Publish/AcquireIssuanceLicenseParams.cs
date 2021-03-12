using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Publish
{
	// Token: 0x020009D9 RID: 2521
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://microsoft.com/DRM/PublishingService")]
	[Serializable]
	public class AcquireIssuanceLicenseParams
	{
		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x0008C500 File Offset: 0x0008A700
		// (set) Token: 0x06003709 RID: 14089 RVA: 0x0008C508 File Offset: 0x0008A708
		public XmlNode UnsignedIssuanceLicense
		{
			get
			{
				return this.unsignedIssuanceLicenseField;
			}
			set
			{
				this.unsignedIssuanceLicenseField = value;
			}
		}

		// Token: 0x04002EE9 RID: 12009
		private XmlNode unsignedIssuanceLicenseField;
	}
}
