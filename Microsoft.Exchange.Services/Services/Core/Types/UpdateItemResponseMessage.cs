using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200057A RID: 1402
	[XmlType("UpdateItemResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateItemResponseMessage : ItemInfoResponseMessage
	{
		// Token: 0x0600270B RID: 9995 RVA: 0x000A6DD4 File Offset: 0x000A4FD4
		public UpdateItemResponseMessage()
		{
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000A6DDC File Offset: 0x000A4FDC
		internal UpdateItemResponseMessage(ServiceResultCode code, ServiceError error, ItemType item, ConflictResults conflictResults) : base(code, error, item)
		{
			this.ConflictResults = conflictResults;
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x000A6DEF File Offset: 0x000A4FEF
		// (set) Token: 0x0600270E RID: 9998 RVA: 0x000A6DF7 File Offset: 0x000A4FF7
		[DataMember(EmitDefaultValue = false)]
		[XmlElement("ConflictResults", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
		public ConflictResults ConflictResults { get; set; }
	}
}
