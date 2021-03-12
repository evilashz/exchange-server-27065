using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B4 RID: 1204
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CompleteFindInGALSpeechRecognitionResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CompleteFindInGALSpeechRecognitionResponseMessage : ResponseMessage
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x000A44E0 File Offset: 0x000A26E0
		public CompleteFindInGALSpeechRecognitionResponseMessage()
		{
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000A44E8 File Offset: 0x000A26E8
		internal CompleteFindInGALSpeechRecognitionResponseMessage(ServiceResultCode code, ServiceError error, RecognitionResult recognitionResult) : base(code, error)
		{
			this.RecognitionResult = recognitionResult;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060023DE RID: 9182 RVA: 0x000A44F9 File Offset: 0x000A26F9
		// (set) Token: 0x060023DF RID: 9183 RVA: 0x000A4501 File Offset: 0x000A2701
		[XmlElement(ElementName = "RecognitionResult", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "RecognitionResult", IsRequired = false, EmitDefaultValue = false)]
		public RecognitionResult RecognitionResult { get; set; }

		// Token: 0x060023E0 RID: 9184 RVA: 0x000A450A File Offset: 0x000A270A
		public override ResponseType GetResponseType()
		{
			return ResponseType.CompleteFindInGALSpeechRecognitionResponseMessage;
		}
	}
}
