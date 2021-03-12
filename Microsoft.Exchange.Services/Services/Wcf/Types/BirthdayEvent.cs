using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A41 RID: 2625
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class BirthdayEvent
	{
		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06004A23 RID: 18979 RVA: 0x00103628 File Offset: 0x00101828
		// (set) Token: 0x06004A24 RID: 18980 RVA: 0x00103630 File Offset: 0x00101830
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06004A25 RID: 18981 RVA: 0x00103639 File Offset: 0x00101839
		// (set) Token: 0x06004A26 RID: 18982 RVA: 0x00103641 File Offset: 0x00101841
		[DataMember]
		public ItemId ContactId { get; set; }

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x0010364A File Offset: 0x0010184A
		// (set) Token: 0x06004A28 RID: 18984 RVA: 0x00103652 File Offset: 0x00101852
		[DataMember]
		public ItemId PersonId { get; set; }

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06004A29 RID: 18985 RVA: 0x0010365B File Offset: 0x0010185B
		// (set) Token: 0x06004A2A RID: 18986 RVA: 0x00103663 File Offset: 0x00101863
		[DataMember(EmitDefaultValue = false)]
		[DateTimeString]
		public string Birthday { get; set; }

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x0010366C File Offset: 0x0010186C
		// (set) Token: 0x06004A2C RID: 18988 RVA: 0x00103674 File Offset: 0x00101874
		[DataMember]
		public string Attribution { get; set; }

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06004A2D RID: 18989 RVA: 0x0010367D File Offset: 0x0010187D
		// (set) Token: 0x06004A2E RID: 18990 RVA: 0x00103685 File Offset: 0x00101885
		[DataMember]
		public bool IsWritable { get; set; }
	}
}
