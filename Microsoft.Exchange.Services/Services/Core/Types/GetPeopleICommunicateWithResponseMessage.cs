using System;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200050B RID: 1291
	[DataContract(Name = "GetPeopleICommunicateWithResponseMessage", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetPeopleICommunicateWithResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public sealed class GetPeopleICommunicateWithResponseMessage : ResponseMessage
	{
		// Token: 0x06002531 RID: 9521 RVA: 0x000A57B5 File Offset: 0x000A39B5
		public GetPeopleICommunicateWithResponseMessage()
		{
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x000A57BD File Offset: 0x000A39BD
		internal GetPeopleICommunicateWithResponseMessage(ServiceResultCode code, ServiceError error, Stream stream) : base(code, error)
		{
			this.Stream = stream;
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x000A57CE File Offset: 0x000A39CE
		// (set) Token: 0x06002534 RID: 9524 RVA: 0x000A57D6 File Offset: 0x000A39D6
		internal Stream Stream { get; private set; }

		// Token: 0x06002535 RID: 9525 RVA: 0x000A57DF File Offset: 0x000A39DF
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetPeopleICommunicateWithResponseMessage;
		}
	}
}
