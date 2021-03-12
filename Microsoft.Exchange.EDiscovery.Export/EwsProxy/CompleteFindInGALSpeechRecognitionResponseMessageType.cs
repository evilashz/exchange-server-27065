using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000CA RID: 202
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class CompleteFindInGALSpeechRecognitionResponseMessageType : ResponseMessageType
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00020379 File Offset: 0x0001E579
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x00020381 File Offset: 0x0001E581
		public RecognitionResultType RecognitionResult
		{
			get
			{
				return this.recognitionResultField;
			}
			set
			{
				this.recognitionResultField = value;
			}
		}

		// Token: 0x040005AF RID: 1455
		private RecognitionResultType recognitionResultField;
	}
}
