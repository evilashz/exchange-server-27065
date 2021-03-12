using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200024B RID: 587
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ReplyBody
	{
		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x000269BA File Offset: 0x00024BBA
		// (set) Token: 0x06001609 RID: 5641 RVA: 0x000269C2 File Offset: 0x00024BC2
		public string Message
		{
			get
			{
				return this.messageField;
			}
			set
			{
				this.messageField = value;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x000269CB File Offset: 0x00024BCB
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x000269D3 File Offset: 0x00024BD3
		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
		public string lang
		{
			get
			{
				return this.langField;
			}
			set
			{
				this.langField = value;
			}
		}

		// Token: 0x04000F26 RID: 3878
		private string messageField;

		// Token: 0x04000F27 RID: 3879
		private string langField;
	}
}
