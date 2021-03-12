using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000089 RID: 137
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class AlternateMailboxCollectionSetting : UserSetting
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0001F743 File Offset: 0x0001D943
		// (set) Token: 0x06000877 RID: 2167 RVA: 0x0001F74B File Offset: 0x0001D94B
		[XmlArray(IsNullable = true)]
		public AlternateMailbox[] AlternateMailboxes
		{
			get
			{
				return this.alternateMailboxesField;
			}
			set
			{
				this.alternateMailboxesField = value;
			}
		}

		// Token: 0x04000331 RID: 817
		private AlternateMailbox[] alternateMailboxesField;
	}
}
