using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000515 RID: 1301
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetSharingFolderResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetSharingFolderResponseMessage : ResponseMessage
	{
		// Token: 0x06002566 RID: 9574 RVA: 0x000A5A29 File Offset: 0x000A3C29
		public GetSharingFolderResponseMessage()
		{
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x000A5A31 File Offset: 0x000A3C31
		internal GetSharingFolderResponseMessage(ServiceResultCode code, ServiceError error, XmlElement sharingFolderId) : base(code, error)
		{
			this.SharingFolderId = sharingFolderId;
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x000A5A42 File Offset: 0x000A3C42
		// (set) Token: 0x06002569 RID: 9577 RVA: 0x000A5A4A File Offset: 0x000A3C4A
		[XmlAnyElement]
		public XmlElement SharingFolderId { get; set; }

		// Token: 0x0600256A RID: 9578 RVA: 0x000A5A53 File Offset: 0x000A3C53
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetSharingFolderResponseMessage;
		}
	}
}
