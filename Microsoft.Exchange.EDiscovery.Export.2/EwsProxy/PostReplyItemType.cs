using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000173 RID: 371
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class PostReplyItemType : PostReplyItemBaseType
	{
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x000239D5 File Offset: 0x00021BD5
		// (set) Token: 0x0600105C RID: 4188 RVA: 0x000239DD File Offset: 0x00021BDD
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

		// Token: 0x04000B32 RID: 2866
		private BodyType newBodyContentField;
	}
}
