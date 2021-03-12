using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003DD RID: 989
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetWellKnownShapesResponse
	{
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001FAC RID: 8108 RVA: 0x000778D8 File Offset: 0x00075AD8
		// (set) Token: 0x06001FAD RID: 8109 RVA: 0x000778E0 File Offset: 0x00075AE0
		[DataMember]
		public string[] ShapeNames { get; set; }

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001FAE RID: 8110 RVA: 0x000778E9 File Offset: 0x00075AE9
		// (set) Token: 0x06001FAF RID: 8111 RVA: 0x000778F1 File Offset: 0x00075AF1
		[DataMember]
		public ResponseShape[] ResponseShapes { get; set; }
	}
}
