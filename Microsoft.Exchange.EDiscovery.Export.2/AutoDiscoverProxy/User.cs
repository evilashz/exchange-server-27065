using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000095 RID: 149
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class User
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x0001F919 File Offset: 0x0001DB19
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x0001F921 File Offset: 0x0001DB21
		[XmlElement(IsNullable = true)]
		public string Mailbox
		{
			get
			{
				return this.mailboxField;
			}
			set
			{
				this.mailboxField = value;
			}
		}

		// Token: 0x04000347 RID: 839
		private string mailboxField;
	}
}
