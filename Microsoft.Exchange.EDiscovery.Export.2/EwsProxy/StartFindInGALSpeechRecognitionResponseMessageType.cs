using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000CC RID: 204
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class StartFindInGALSpeechRecognitionResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x000203AB File Offset: 0x0001E5AB
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x000203B3 File Offset: 0x0001E5B3
		public RecognitionIdType RecognitionId
		{
			get
			{
				return this.recognitionIdField;
			}
			set
			{
				this.recognitionIdField = value;
			}
		}

		// Token: 0x040005B1 RID: 1457
		private RecognitionIdType recognitionIdField;
	}
}
