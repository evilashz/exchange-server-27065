using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200051F RID: 1311
	[XmlType("GetUMPromptResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetUMPromptResponseMessage : ResponseMessage
	{
		// Token: 0x0600259B RID: 9627 RVA: 0x000A5C85 File Offset: 0x000A3E85
		public GetUMPromptResponseMessage()
		{
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000A5C8D File Offset: 0x000A3E8D
		internal GetUMPromptResponseMessage(ServiceResultCode code, ServiceError error, GetUMPromptResponseMessage response) : base(code, error)
		{
			if (response != null)
			{
				this.AudioData = response.AudioData;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000A5CA6 File Offset: 0x000A3EA6
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x000A5CAE File Offset: 0x000A3EAE
		[XmlElement(ElementName = "AudioData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[DataMember(Name = "AudioData", IsRequired = false, EmitDefaultValue = false)]
		public string AudioData { get; set; }

		// Token: 0x0600259F RID: 9631 RVA: 0x000A5CB7 File Offset: 0x000A3EB7
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetUMPromptResponseMessage;
		}
	}
}
