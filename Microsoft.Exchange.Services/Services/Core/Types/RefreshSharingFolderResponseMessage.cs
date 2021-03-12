using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000538 RID: 1336
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "RefreshSharingFolderResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class RefreshSharingFolderResponseMessage : ResponseMessage
	{
		// Token: 0x06002601 RID: 9729 RVA: 0x000A6201 File Offset: 0x000A4401
		public RefreshSharingFolderResponseMessage()
		{
		}

		// Token: 0x06002602 RID: 9730 RVA: 0x000A6209 File Offset: 0x000A4409
		internal RefreshSharingFolderResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002603 RID: 9731 RVA: 0x000A6213 File Offset: 0x000A4413
		public override ResponseType GetResponseType()
		{
			return ResponseType.RefreshSharingFolderResponseMessage;
		}
	}
}
