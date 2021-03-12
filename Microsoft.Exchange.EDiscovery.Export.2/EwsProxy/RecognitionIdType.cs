using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000CD RID: 205
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class RecognitionIdType
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x000203C4 File Offset: 0x0001E5C4
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x000203CC File Offset: 0x0001E5CC
		[XmlAttribute]
		public string RequestId
		{
			get
			{
				return this.requestIdField;
			}
			set
			{
				this.requestIdField = value;
			}
		}

		// Token: 0x040005B2 RID: 1458
		private string requestIdField;
	}
}
