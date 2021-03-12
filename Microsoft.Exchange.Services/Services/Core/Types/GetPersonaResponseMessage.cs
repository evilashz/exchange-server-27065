using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200050C RID: 1292
	[XmlType("GetPersonaResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetPersonaResponseMessage : ResponseMessage
	{
		// Token: 0x06002536 RID: 9526 RVA: 0x000A57E3 File Offset: 0x000A39E3
		public GetPersonaResponseMessage()
		{
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x000A57EB File Offset: 0x000A39EB
		internal GetPersonaResponseMessage(ServiceResultCode code, ServiceError error, GetPersonaResponseMessage response) : base(code, error)
		{
			this.persona = null;
			if (response != null)
			{
				this.persona = response.persona;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06002538 RID: 9528 RVA: 0x000A580B File Offset: 0x000A3A0B
		// (set) Token: 0x06002539 RID: 9529 RVA: 0x000A5813 File Offset: 0x000A3A13
		[DataMember]
		[XmlElement("Persona")]
		public Persona Persona
		{
			get
			{
				return this.persona;
			}
			set
			{
				this.persona = value;
			}
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x000A581C File Offset: 0x000A3A1C
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetPersonaResponseMessage;
		}

		// Token: 0x040015B2 RID: 5554
		private Persona persona;
	}
}
