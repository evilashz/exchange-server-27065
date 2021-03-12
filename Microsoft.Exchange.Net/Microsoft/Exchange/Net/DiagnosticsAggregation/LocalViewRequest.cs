using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Net.DiagnosticsAggregation
{
	// Token: 0x02000831 RID: 2097
	[DataContract]
	internal class LocalViewRequest
	{
		// Token: 0x06002C79 RID: 11385 RVA: 0x00064E0A File Offset: 0x0006300A
		public LocalViewRequest(RequestType requestType)
		{
			this.RequestType = requestType.ToString();
			this.ClientInformation = new ClientInformation();
			this.ClientInformation.SetClientInformation();
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x00064E39 File Offset: 0x00063039
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x00064E41 File Offset: 0x00063041
		[DataMember(IsRequired = true)]
		public string RequestType { get; private set; }

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x00064E4A File Offset: 0x0006304A
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x00064E52 File Offset: 0x00063052
		[DataMember]
		public QueueLocalViewRequest QueueLocalViewRequest { get; set; }

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x00064E5B File Offset: 0x0006305B
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x00064E63 File Offset: 0x00063063
		[DataMember(IsRequired = true)]
		public ClientInformation ClientInformation { get; private set; }
	}
}
