using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B31 RID: 2865
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PerformInstantSearchResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public sealed class PerformInstantSearchResponse : ResponseMessage
	{
		// Token: 0x0600513C RID: 20796 RVA: 0x0010A406 File Offset: 0x00108606
		public PerformInstantSearchResponse() : this(null)
		{
		}

		// Token: 0x0600513D RID: 20797 RVA: 0x0010A40F File Offset: 0x0010860F
		public PerformInstantSearchResponse(InstantSearchPayloadType payload)
		{
			this.Payload = payload;
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x0600513E RID: 20798 RVA: 0x0010A41E File Offset: 0x0010861E
		// (set) Token: 0x0600513F RID: 20799 RVA: 0x0010A426 File Offset: 0x00108626
		[DataMember]
		public InstantSearchPayloadType Payload { get; set; }

		// Token: 0x06005140 RID: 20800 RVA: 0x0010A42F File Offset: 0x0010862F
		public override ResponseType GetResponseType()
		{
			return ResponseType.PerformInstantSearchResponseMessage;
		}
	}
}
