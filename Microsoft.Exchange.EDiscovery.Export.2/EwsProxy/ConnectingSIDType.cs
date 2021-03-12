using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003AC RID: 940
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ConnectingSIDType
	{
		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0002A6C1 File Offset: 0x000288C1
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x0002A6C9 File Offset: 0x000288C9
		[XmlElement("SID", typeof(SIDType))]
		[XmlElement("PrimarySmtpAddress", typeof(PrimarySmtpAddressType))]
		[XmlElement("PrincipalName", typeof(PrincipalNameType))]
		[XmlElement("SmtpAddress", typeof(SmtpAddressType))]
		public object Item
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

		// Token: 0x04001360 RID: 4960
		private object itemField;
	}
}
