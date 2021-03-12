using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000516 RID: 1302
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetSharingMetadataResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetSharingMetadataResponseMessage : ResponseMessage
	{
		// Token: 0x0600256B RID: 9579 RVA: 0x000A5A57 File Offset: 0x000A3C57
		public GetSharingMetadataResponseMessage()
		{
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x000A5A5F File Offset: 0x000A3C5F
		internal GetSharingMetadataResponseMessage(ServiceResultCode code, ServiceError error, EncryptionResults encryptionResults) : base(code, error)
		{
			if (encryptionResults != null)
			{
				this.encryptedSharedFolderDataCollection = encryptionResults.EncryptedSharedFolderDataCollection;
				this.invalidRecipients = encryptionResults.InvalidRecipients;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x000A5A84 File Offset: 0x000A3C84
		// (set) Token: 0x0600256E RID: 9582 RVA: 0x000A5A8C File Offset: 0x000A3C8C
		[XmlArrayItem("EncryptedSharedFolderData", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(EncryptedSharedFolderData))]
		[XmlArray("EncryptedSharedFolderDataCollection", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public EncryptedSharedFolderData[] EncryptedSharedFolderDataCollection
		{
			get
			{
				return this.encryptedSharedFolderDataCollection;
			}
			set
			{
				this.encryptedSharedFolderDataCollection = value;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x000A5A95 File Offset: 0x000A3C95
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x000A5A9D File Offset: 0x000A3C9D
		[XmlArray("InvalidRecipients", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		[XmlArrayItem("InvalidRecipient", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", Type = typeof(InvalidRecipient))]
		public InvalidRecipient[] InvalidRecipients
		{
			get
			{
				return this.invalidRecipients;
			}
			set
			{
				this.invalidRecipients = value;
			}
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000A5AA6 File Offset: 0x000A3CA6
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetSharingMetadataResponseMessage;
		}

		// Token: 0x040015BD RID: 5565
		private EncryptedSharedFolderData[] encryptedSharedFolderDataCollection;

		// Token: 0x040015BE RID: 5566
		private InvalidRecipient[] invalidRecipients;
	}
}
