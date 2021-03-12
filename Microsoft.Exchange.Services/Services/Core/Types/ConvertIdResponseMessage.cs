using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B7 RID: 1207
	[XmlType(TypeName = "ConvertIdResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class ConvertIdResponseMessage : ResponseMessage
	{
		// Token: 0x060023EB RID: 9195 RVA: 0x000A459E File Offset: 0x000A279E
		public ConvertIdResponseMessage()
		{
		}

		// Token: 0x060023EC RID: 9196 RVA: 0x000A45A6 File Offset: 0x000A27A6
		internal ConvertIdResponseMessage(ServiceResultCode code, ServiceError error, AlternateIdBase alternateId) : base(code, error)
		{
			this.alternateIdField = alternateId;
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060023ED RID: 9197 RVA: 0x000A45B7 File Offset: 0x000A27B7
		// (set) Token: 0x060023EE RID: 9198 RVA: 0x000A45BF File Offset: 0x000A27BF
		[DataMember(EmitDefaultValue = false)]
		public AlternateIdBase AlternateId
		{
			get
			{
				return this.alternateIdField;
			}
			set
			{
				this.alternateIdField = value;
			}
		}

		// Token: 0x04001565 RID: 5477
		private AlternateIdBase alternateIdField;
	}
}
