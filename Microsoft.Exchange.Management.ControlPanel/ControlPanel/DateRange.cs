using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200043B RID: 1083
	[DataContract]
	public class DateRange
	{
		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x06003605 RID: 13829 RVA: 0x000A76F1 File Offset: 0x000A58F1
		// (set) Token: 0x06003606 RID: 13830 RVA: 0x000A76F9 File Offset: 0x000A58F9
		[DataMember]
		public Identity BeforeDate { get; set; }

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x06003607 RID: 13831 RVA: 0x000A7702 File Offset: 0x000A5902
		// (set) Token: 0x06003608 RID: 13832 RVA: 0x000A770A File Offset: 0x000A590A
		[DataMember]
		public Identity AfterDate { get; set; }
	}
}
