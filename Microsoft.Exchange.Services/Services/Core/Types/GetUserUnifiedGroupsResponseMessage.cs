using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000526 RID: 1318
	[XmlType("GetUserUnifiedGroupsResponseMessage", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class GetUserUnifiedGroupsResponseMessage : ResponseMessage
	{
		// Token: 0x060025C6 RID: 9670 RVA: 0x000A5F26 File Offset: 0x000A4126
		public GetUserUnifiedGroupsResponseMessage()
		{
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000A5F2E File Offset: 0x000A412E
		internal GetUserUnifiedGroupsResponseMessage(ServiceResultCode code, ServiceError error, GetUserUnifiedGroupsResponseMessage response) : base(code, error)
		{
			this.groupsSets = null;
			if (response != null)
			{
				this.groupsSets = response.GroupsSets;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x000A5F4E File Offset: 0x000A414E
		// (set) Token: 0x060025C9 RID: 9673 RVA: 0x000A5F56 File Offset: 0x000A4156
		[XmlArray(ElementName = "GroupsSets")]
		[XmlArrayItem(ElementName = "UnifiedGroupsSet", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(EmitDefaultValue = false)]
		public UnifiedGroupsSet[] GroupsSets
		{
			get
			{
				return this.groupsSets;
			}
			set
			{
				this.groupsSets = value;
			}
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000A5F5F File Offset: 0x000A415F
		public override ResponseType GetResponseType()
		{
			return ResponseType.GetUserUnifiedGroupsResponseMessage;
		}

		// Token: 0x040015D8 RID: 5592
		private UnifiedGroupsSet[] groupsSets;
	}
}
