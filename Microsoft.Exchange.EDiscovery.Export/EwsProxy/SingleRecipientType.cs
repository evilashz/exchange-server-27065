using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200011D RID: 285
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SingleRecipientType
	{
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x00021CDF File Offset: 0x0001FEDF
		// (set) Token: 0x06000CF0 RID: 3312 RVA: 0x00021CE7 File Offset: 0x0001FEE7
		[XmlElement("Mailbox")]
		public EmailAddressType Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x04000902 RID: 2306
		private EmailAddressType itemField;
	}
}
