using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000833 RID: 2099
	[DataContract]
	internal class AggregatedViewRequest
	{
		// Token: 0x06002C85 RID: 11397 RVA: 0x00064E9D File Offset: 0x0006309D
		public AggregatedViewRequest(RequestType requestType, List<string> serversToInclude, uint resultSize)
		{
			this.RequestType = requestType.ToString();
			this.ServersToInclude = serversToInclude;
			this.ResultSize = resultSize;
			this.ClientInformation = new ClientInformation();
			this.ClientInformation.SetClientInformation();
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002C86 RID: 11398 RVA: 0x00064EDA File Offset: 0x000630DA
		// (set) Token: 0x06002C87 RID: 11399 RVA: 0x00064EE2 File Offset: 0x000630E2
		[DataMember(IsRequired = true)]
		public string RequestType { get; private set; }

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x00064EEB File Offset: 0x000630EB
		// (set) Token: 0x06002C89 RID: 11401 RVA: 0x00064EF3 File Offset: 0x000630F3
		[DataMember(IsRequired = true)]
		public List<string> ServersToInclude { get; private set; }

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x00064EFC File Offset: 0x000630FC
		// (set) Token: 0x06002C8B RID: 11403 RVA: 0x00064F04 File Offset: 0x00063104
		[DataMember(IsRequired = true)]
		public uint ResultSize { get; private set; }

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x00064F0D File Offset: 0x0006310D
		// (set) Token: 0x06002C8D RID: 11405 RVA: 0x00064F15 File Offset: 0x00063115
		[DataMember]
		public QueueAggregatedViewRequest QueueAggregatedViewRequest { get; set; }

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x00064F1E File Offset: 0x0006311E
		// (set) Token: 0x06002C8F RID: 11407 RVA: 0x00064F26 File Offset: 0x00063126
		[DataMember(IsRequired = true)]
		public ClientInformation ClientInformation { get; private set; }
	}
}
