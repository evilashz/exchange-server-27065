using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000132 RID: 306
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class InstantMessageContact
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000239FD File Offset: 0x00021BFD
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00023A05 File Offset: 0x00021C05
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string SipUri { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x00023A0E File Offset: 0x00021C0E
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x00023A16 File Offset: 0x00021C16
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public string DisplayName { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x00023A1F File Offset: 0x00021C1F
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00023A27 File Offset: 0x00021C27
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public string Title { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00023A30 File Offset: 0x00021C30
		// (set) Token: 0x06000A4F RID: 2639 RVA: 0x00023A38 File Offset: 0x00021C38
		[DataMember(EmitDefaultValue = false, Order = 4)]
		public string Company { get; set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00023A41 File Offset: 0x00021C41
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00023A49 File Offset: 0x00021C49
		[DataMember(EmitDefaultValue = false, Order = 5)]
		public string Office { get; set; }
	}
}
