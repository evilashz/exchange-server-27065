using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000CB RID: 203
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class RecognitionResultType
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00020392 File Offset: 0x0001E592
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x0002039A File Offset: 0x0001E59A
		[XmlAttribute]
		public string Result
		{
			get
			{
				return this.resultField;
			}
			set
			{
				this.resultField = value;
			}
		}

		// Token: 0x040005B0 RID: 1456
		private string resultField;
	}
}
