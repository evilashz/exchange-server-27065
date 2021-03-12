using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000247 RID: 583
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class MailTipsResponseMessageType : ResponseMessageType
	{
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0002681B File Offset: 0x00024A1B
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x00026823 File Offset: 0x00024A23
		public MailTips MailTips
		{
			get
			{
				return this.mailTipsField;
			}
			set
			{
				this.mailTipsField = value;
			}
		}

		// Token: 0x04000F03 RID: 3843
		private MailTips mailTipsField;
	}
}
