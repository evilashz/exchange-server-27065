using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200057F RID: 1407
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "UploadItemsResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class UploadItemsResponseMessage : ResponseMessage
	{
		// Token: 0x06002718 RID: 10008 RVA: 0x000A6E6D File Offset: 0x000A506D
		public UploadItemsResponseMessage()
		{
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000A6E75 File Offset: 0x000A5075
		internal UploadItemsResponseMessage(ServiceResultCode code, ServiceError error, XmlNode itemId) : base(code, error)
		{
			this.ItemId = itemId;
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x000A6E86 File Offset: 0x000A5086
		// (set) Token: 0x0600271B RID: 10011 RVA: 0x000A6E8D File Offset: 0x000A508D
		[XmlNamespaceDeclarations]
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				return ResponseMessage.namespaces;
			}
			set
			{
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x000A6E8F File Offset: 0x000A508F
		// (set) Token: 0x0600271D RID: 10013 RVA: 0x000A6E98 File Offset: 0x000A5098
		[XmlAnyElement("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public XmlNode ItemId
		{
			get
			{
				return this.itemId;
			}
			set
			{
				if (value == null)
				{
					this.itemId = null;
					return;
				}
				XmlNode xmlNode = null;
				foreach (object obj in value.ChildNodes)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					if (xmlNode2.LocalName == "ItemId")
					{
						xmlNode = xmlNode2;
					}
				}
				this.itemId = xmlNode;
			}
		}

		// Token: 0x040018D2 RID: 6354
		private XmlNode itemId;
	}
}
