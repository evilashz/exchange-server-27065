using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200031A RID: 794
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[Serializable]
	public class CompleteFindInGALSpeechRecognitionType : BaseRequestType
	{
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x00028B2C File Offset: 0x00026D2C
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x00028B34 File Offset: 0x00026D34
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

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x00028B3D File Offset: 0x00026D3D
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x00028B45 File Offset: 0x00026D45
		[XmlElement(DataType = "base64Binary")]
		public byte[] AudioData
		{
			get
			{
				return this.audioDataField;
			}
			set
			{
				this.audioDataField = value;
			}
		}

		// Token: 0x04001179 RID: 4473
		private RecognitionIdType recognitionIdField;

		// Token: 0x0400117A RID: 4474
		private byte[] audioDataField;
	}
}
