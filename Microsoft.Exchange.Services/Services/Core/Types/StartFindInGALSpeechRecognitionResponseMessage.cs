using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200055B RID: 1371
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("AddSpeechRecognitionResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class StartFindInGALSpeechRecognitionResponseMessage : ResponseMessage
	{
		// Token: 0x06002673 RID: 9843 RVA: 0x000A6669 File Offset: 0x000A4869
		public StartFindInGALSpeechRecognitionResponseMessage()
		{
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x000A6671 File Offset: 0x000A4871
		internal StartFindInGALSpeechRecognitionResponseMessage(ServiceResultCode code, ServiceError error, RecognitionId recognitionId) : base(code, error)
		{
			this.RecognitionId = recognitionId;
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06002675 RID: 9845 RVA: 0x000A6682 File Offset: 0x000A4882
		// (set) Token: 0x06002676 RID: 9846 RVA: 0x000A668A File Offset: 0x000A488A
		[XmlElement(ElementName = "RecognitionId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "RecognitionId", IsRequired = false, EmitDefaultValue = false)]
		public RecognitionId RecognitionId { get; set; }

		// Token: 0x06002677 RID: 9847 RVA: 0x000A6693 File Offset: 0x000A4893
		public override ResponseType GetResponseType()
		{
			return ResponseType.StartFindInGALSpeechRecognitionResponseMessage;
		}
	}
}
