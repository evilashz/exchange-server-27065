using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020005A0 RID: 1440
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "AttendeeCountsType")]
	[Serializable]
	public class AttendeeCountsType
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000AC8DE File Offset: 0x000AAADE
		// (set) Token: 0x060028C3 RID: 10435 RVA: 0x000AC8E6 File Offset: 0x000AAAE6
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public int RequiredAttendeesCount { get; set; }

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000AC8EF File Offset: 0x000AAAEF
		// (set) Token: 0x060028C5 RID: 10437 RVA: 0x000AC8F7 File Offset: 0x000AAAF7
		[DataMember(EmitDefaultValue = false, Order = 2)]
		public int OptionalAttendeesCount { get; set; }

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000AC900 File Offset: 0x000AAB00
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x000AC908 File Offset: 0x000AAB08
		[DataMember(EmitDefaultValue = false, Order = 3)]
		public int ResourcesCount { get; set; }
	}
}
