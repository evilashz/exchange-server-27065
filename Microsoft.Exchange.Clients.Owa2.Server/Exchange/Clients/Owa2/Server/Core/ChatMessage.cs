using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200012D RID: 301
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ChatMessage
	{
		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00023235 File Offset: 0x00021435
		// (set) Token: 0x06000A0D RID: 2573 RVA: 0x0002323D File Offset: 0x0002143D
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 1)]
		public string Body { get; set; }

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00023246 File Offset: 0x00021446
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x0002324E File Offset: 0x0002144E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 2)]
		public string Format { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x00023257 File Offset: 0x00021457
		// (set) Token: 0x06000A11 RID: 2577 RVA: 0x0002325F File Offset: 0x0002145F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 3)]
		public string Subject { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00023268 File Offset: 0x00021468
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x00023270 File Offset: 0x00021470
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 4)]
		public int ChatSessionId { get; set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00023279 File Offset: 0x00021479
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00023281 File Offset: 0x00021481
		[DataMember(IsRequired = true, EmitDefaultValue = false, Order = 5)]
		public string[] Recipients { get; set; }
	}
}
