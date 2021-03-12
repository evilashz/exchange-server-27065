using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A51 RID: 2641
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class FindPlacesRequest
	{
		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x0010533D File Offset: 0x0010353D
		// (set) Token: 0x06004AE3 RID: 19171 RVA: 0x00105345 File Offset: 0x00103545
		[DataMember]
		public string Query { get; set; }

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x0010534E File Offset: 0x0010354E
		// (set) Token: 0x06004AE5 RID: 19173 RVA: 0x00105356 File Offset: 0x00103556
		[DataMember]
		public string Id { get; set; }

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x0010535F File Offset: 0x0010355F
		// (set) Token: 0x06004AE7 RID: 19175 RVA: 0x00105367 File Offset: 0x00103567
		[DataMember]
		public PlacesSource Sources { get; set; }

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x00105370 File Offset: 0x00103570
		// (set) Token: 0x06004AE9 RID: 19177 RVA: 0x00105378 File Offset: 0x00103578
		[DataMember]
		public int MaxResults { get; set; }

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06004AEA RID: 19178 RVA: 0x00105381 File Offset: 0x00103581
		// (set) Token: 0x06004AEB RID: 19179 RVA: 0x00105389 File Offset: 0x00103589
		[DataMember(EmitDefaultValue = false)]
		public double? Latitude { get; set; }

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06004AEC RID: 19180 RVA: 0x00105392 File Offset: 0x00103592
		// (set) Token: 0x06004AED RID: 19181 RVA: 0x0010539A File Offset: 0x0010359A
		[DataMember(EmitDefaultValue = false)]
		public double? Longitude { get; set; }

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x001053A3 File Offset: 0x001035A3
		// (set) Token: 0x06004AEF RID: 19183 RVA: 0x001053AB File Offset: 0x001035AB
		[DataMember]
		public string Culture { get; set; }
	}
}
