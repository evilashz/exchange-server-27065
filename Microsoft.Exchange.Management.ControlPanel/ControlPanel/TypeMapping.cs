using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000F6 RID: 246
	[DataContract]
	public class TypeMapping
	{
		// Token: 0x170019D4 RID: 6612
		// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0005C2F1 File Offset: 0x0005A4F1
		// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0005C2F9 File Offset: 0x0005A4F9
		[DataMember]
		public string Type { get; set; }

		// Token: 0x170019D5 RID: 6613
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0005C302 File Offset: 0x0005A502
		// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0005C30A File Offset: 0x0005A50A
		[DataMember]
		public string BaseUrl { get; set; }

		// Token: 0x170019D6 RID: 6614
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x0005C313 File Offset: 0x0005A513
		// (set) Token: 0x06001EAD RID: 7853 RVA: 0x0005C31B File Offset: 0x0005A51B
		[DataMember]
		public bool? InRole { get; set; }
	}
}
