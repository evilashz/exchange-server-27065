using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Exchange.InfoWorker.Common.Availability.Proxy
{
	// Token: 0x02000132 RID: 306
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class ReplyBody
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x000250B1 File Offset: 0x000232B1
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x000250B9 File Offset: 0x000232B9
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

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x000250C2 File Offset: 0x000232C2
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x000250CA File Offset: 0x000232CA
		[XmlAttribute(Form = XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
		public string Lang
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

		// Token: 0x04000681 RID: 1665
		private string messageField;

		// Token: 0x04000682 RID: 1666
		private string langField;
	}
}
