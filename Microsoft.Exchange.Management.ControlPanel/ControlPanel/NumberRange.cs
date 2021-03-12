using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000439 RID: 1081
	[DataContract]
	public class NumberRange
	{
		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000A7663 File Offset: 0x000A5863
		// (set) Token: 0x060035FC RID: 13820 RVA: 0x000A766B File Offset: 0x000A586B
		[DataMember]
		public int AtMost { get; set; }

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x000A7674 File Offset: 0x000A5874
		// (set) Token: 0x060035FE RID: 13822 RVA: 0x000A767C File Offset: 0x000A587C
		[DataMember]
		public int AtLeast { get; set; }
	}
}
