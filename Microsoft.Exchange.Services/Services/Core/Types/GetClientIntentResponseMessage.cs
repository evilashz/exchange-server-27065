using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004EE RID: 1262
	[XmlType("GetClientIntentResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public sealed class GetClientIntentResponseMessage : ResponseMessage
	{
		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x000A51E3 File Offset: 0x000A33E3
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x000A51EB File Offset: 0x000A33EB
		[DataMember(Name = "ClientIntent")]
		[XmlElement("ClientIntent")]
		public ClientIntent ClientIntent { get; set; }

		// Token: 0x060024BC RID: 9404 RVA: 0x000A51F4 File Offset: 0x000A33F4
		public GetClientIntentResponseMessage()
		{
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000A51FC File Offset: 0x000A33FC
		internal GetClientIntentResponseMessage(ServiceResultCode code, ServiceError error, GetClientIntentResponseMessage result) : base(code, error)
		{
			if (result != null)
			{
				this.ClientIntent = result.ClientIntent;
			}
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000A5215 File Offset: 0x000A3415
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetClientIntentResponseMessage;
		}
	}
}
